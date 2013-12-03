#region License

// The MIT License (MIT)
// 
// Copyright (c) 2013 Pagar.me
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

using System;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;
using Mono.Security;
using Newtonsoft.Json;
using PagarMe.Serializer;

namespace PagarMe
{
    /// <summary>
    ///     Root class for accessing Pagar.me API
    /// </summary>
    public class PagarMeProvider
    {
        private readonly string _apiKey;
        private readonly PagarMeQueryable<Customer> _customers;
        private readonly string _encryptionKey;
        private readonly PagarMeQueryable<Plan> _plans;
        private readonly PagarMeQueryable<Subscription> _subscriptions;
        private readonly PagarMeQueryable<Transaction> _transactions;

        /// <summary>
        ///     Instantiate a new PagarMeProvider
        /// </summary>
        /// <param name="apiKey">API key</param>
        /// <param name="encryptionKey">Encryption key</param>
        public PagarMeProvider(string apiKey, string encryptionKey)
        {
            if (apiKey == null || !apiKey.StartsWith("ak_"))
                throw new ArgumentException("Invalid API key.", "apiKey");

            if (encryptionKey == null || !encryptionKey.StartsWith("ek_"))
                throw new ArgumentException("Invalid encryption key.", "encryptionKey");

            _apiKey = apiKey;
            _encryptionKey = encryptionKey;
            _transactions = new PagarMeQueryable<Transaction>(this);
            _customers = new PagarMeQueryable<Customer>(this);
            _plans = new PagarMeQueryable<Plan>(this);
            _subscriptions = new PagarMeQueryable<Subscription>(this);
        }

        /// <summary>
        ///     Currently used API key
        /// </summary>
        [PublicAPI]
        public string ApiKey
        {
            get { return _apiKey; }
        }

        /// <summary>
        ///     Currently used encryption key
        /// </summary>
        [PublicAPI]
        public string EncryptionKey
        {
            get { return _encryptionKey; }
        }

        /// <summary>
        ///     Transactions collection to be accessed via LINQ
        /// </summary>
        [PublicAPI]
        public PagarMeQueryable<Transaction> Transactions
        {
            get { return _transactions; }
        }

        /// <summary>
        ///     Customers collection to be accessed via LINQ
        /// </summary>
        [PublicAPI]
        public PagarMeQueryable<Customer> Customers
        {
            get { return _customers; }
        }

        /// <summary>
        ///     Plans collection to be accessed via LINQ
        /// </summary>
        [PublicAPI]
        public PagarMeQueryable<Plan> Plans
        {
            get { return _plans; }
        }

        /// <summary>
        ///     Subscriptions collection to be accessed via LINQ
        /// </summary>
        [PublicAPI]
        public PagarMeQueryable<Subscription> Subscriptions
        {
            get { return _subscriptions; }
        }

        /// <summary>
        ///     Settings to serialize metadata as JSON
        /// </summary>
        [PublicAPI]
        public JsonSerializerSettings MetadataSerializerSettings { get; set; }

        /// <summary>
        ///     Creates a new transaction
        /// </summary>
        /// <param name="setup">Transaction data</param>
        /// <returns>Transaction object representing the new transaction</returns>
        [PublicAPI]
        public Transaction PostTransaction(TransactionSetup setup)
        {
            UrlEncodingContext context = new UrlEncodingContext();

            context.MetadataSerializerSettings = MetadataSerializerSettings;

            PagarMeQuery query = new PagarMeQuery(this, "POST", "transactions");

            ValidateTransaction(setup);

            foreach (var tuple in UrlSerializer.Serialize(setup, null, context))
                query.AddQuery(tuple.Item1, tuple.Item2);

            return new Transaction(this, query.Execute());
        }

        /// <summary>
        ///     Creates a new subscription
        /// </summary>
        /// <param name="setup">Subscription data</param>
        /// <returns>Transaction object representing the new transaction</returns>
        [PublicAPI]
        public Subscription PostSubscription(SubscriptionSetup setup)
        {
            UrlEncodingContext context = new UrlEncodingContext();

            context.MetadataSerializerSettings = MetadataSerializerSettings;

            PagarMeQuery query = new PagarMeQuery(this, "POST", "subscriptions");

            ValidateSubscription(setup);

            foreach (var tuple in UrlSerializer.Serialize(setup, null, context))
                query.AddQuery(tuple.Item1, tuple.Item2);

            return new Subscription(this, query.Execute());
        }

        /// <summary>
        ///     Generates a secure hash for a credit card
        /// </summary>
        /// <param name="creditCard">Credit card information</param>
        /// <returns>Credit card hash</returns>
        [PublicAPI]
        public string GenerateCardHash(CreditCard creditCard)
        {
            string hashParameters = UrlSerializer.Serialize(creditCard).Aggregate("",
                (current, tuple) =>
                    current + (Uri.EscapeUriString(tuple.Item1) + "=" + Uri.EscapeUriString(tuple.Item2) + "&"))
                .TrimEnd('&');

            PagarMeQuery keyQuery = new PagarMeQuery(this, "GET", "transactions/card_hash_key");
            keyQuery.AddQuery("encryption_key", _encryptionKey);

            PagarMeQueryResponse keyResponse = keyQuery.Execute();
            keyResponse.Validate();

            CardHashKey key = JsonConvert.DeserializeObject<CardHashKey>(keyResponse.Data);
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                RSAParameters parameters = new RSAParameters();
                string publicKeyData = key.PublicKey.Substring(27, key.PublicKey.Length - 52);
                byte[] data = Convert.FromBase64String(publicKeyData);
                ASN1 root = new ASN1(data);
                ASN1 keyRoot = new ASN1(root[1].Value.Skip(1).ToArray());

                parameters.Modulus = keyRoot[0].Value.Skip(1).ToArray();
                parameters.Exponent = keyRoot[1].Value;
                rsa.ImportParameters(parameters);

                return key.Id + "_" + Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(hashParameters), false));
            }
        }

        internal static void ValidateSubscription(SubscriptionSetup setup)
        {
            ValidateTransaction(setup, true);

            if (setup.Plan < 0)
                throw new FormatException("Plan ID must be equal or greater than zero.");
        }

        internal static void ValidateTransaction(TransactionSetup setup, bool subset = false)
        {
            if (setup.PaymentMethod == PaymentMethod.CreditCard && string.IsNullOrEmpty(setup.CardHash))
                throw new FormatException("CardHash is required.");

            if (!subset)
            {
                if (setup.Amount <= 0)
                    throw new FormatException("Amount must be greater than zero.");
            }
        }
    }
}