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
using System.Dynamic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using PagarMe.Converters;

namespace PagarMe
{
    /// <summary>
    ///     Represents a transaction
    /// </summary>
    [PagarMeModel("transactions")]
    public class Transaction : PagarMeModel
    {
        internal Transaction(PagarMeProvider provider)
            : base(provider)
        {
        }

        internal Transaction(PagarMeProvider provider, PagarMeQueryResponse result)
            : base(provider, result)
        {
        }

        /// <summary>
        ///     Transaction status
        /// </summary>
        [JsonProperty(PropertyName = "status"), UsedImplicitly]
        [JsonConverter(typeof(TransactionStatusConverter))]
        public TransactionStatus Status { get; private set; }

        /// <summary>
        ///     Transaction refuse reason
        /// </summary>
        [JsonProperty(PropertyName = "refuse_reason"), UsedImplicitly]
        [JsonConverter(typeof(TransactionRefuseReasonConverter))]
        public TransactionRefuseReason RefuseReason { get; private set; }

        /// <summary>
        ///     Transaction value
        /// </summary>
        [JsonProperty(PropertyName = "amount"), UsedImplicitly]
        [JsonConverter(typeof(AmountConverter))]
        public decimal Amount { get; private set; }

        /// <summary>
        ///     Number of installments
        /// </summary>
        [JsonProperty(PropertyName = "installments"), UsedImplicitly]
        public int Installments { get; private set; }

        /// <summary>
        ///     Cardholder name
        /// </summary>
        [JsonProperty(PropertyName = "card_holder_name"), UsedImplicitly]
        public string CardHolderName { get; private set; }

        /// <summary>
        ///     Credit card last digits
        /// </summary>
        [JsonProperty(PropertyName = "card_last_digits"), UsedImplicitly]
        public string CardLastDigits { get; private set; }

        /// <summary>
        ///     Credit card brand
        /// </summary>
        [JsonProperty(PropertyName = "card_brand"), UsedImplicitly]
        public string CardBrand { get; private set; }

        /// <summary>
        ///     Postback URL
        /// </summary>
        [JsonProperty(PropertyName = "postback_url"), UsedImplicitly]
        public string PostbackUrl { get; private set; }

        /// <summary>
        ///     Payment method
        /// </summary>
        [JsonProperty(PropertyName = "payment_method"), UsedImplicitly]
        [JsonConverter(typeof(PaymentMethodConverter))]
        public PaymentMethod PaymentMethod { get; private set; }

        /// <summary>
        ///     Antifraud score
        /// </summary>
        [JsonProperty(PropertyName = "antifraud_score"), UsedImplicitly]
        public int? AntifraudScore { get; private set; }

        /// <summary>
        ///     URL to the boleto for priting
        /// </summary>
        [JsonProperty(PropertyName = "boleto_url"), UsedImplicitly]
        public string BoletoUrl { get; private set; }

        /// <summary>
        ///     Boleto barcode
        /// </summary>
        [JsonProperty(PropertyName = "boleto_barcode"), UsedImplicitly]
        public string BoletoBarcode { get; private set; }

        /// <summary>
        ///     Subscription ID associated with this transaction
        /// </summary>
        [JsonProperty(PropertyName = "subscription_id"), UsedImplicitly]
        public int? SubscriptionId { get; private set; }

        /// <summary>
        ///     Customer information associated with this transaction
        /// </summary>
        [JsonProperty(PropertyName = "customer"), UsedImplicitly]
        [JsonConverter(typeof(PagarMeModelConverter<Customer>))]
        public Customer Customer { get; private set; }

        /// <summary>
        ///     Address used in this transaction
        /// </summary>
        [JsonProperty(PropertyName = "address"), UsedImplicitly]
        public CustomerAddress Address { get; private set; }

        /// <summary>
        ///     Phone used in this transaction
        /// </summary>
        [JsonProperty(PropertyName = "phone"), UsedImplicitly]
        public CustomerPhone Phone { get; private set; }

        /// <summary>
        ///     Transaction metadata
        /// </summary>
        [JsonProperty(PropertyName = "metadata"), UsedImplicitly]
        [JsonConverter(typeof(MetadataConverter))]
        public dynamic Metadata { get; private set; }

        /// <summary>
        ///     Chargeback the transaction
        /// </summary>
        [PublicAPI]
        public void Refund()
        {
            Refresh(new PagarMeQuery(Provider, "POST", string.Format("transactions/{0}/refund", Id)).Execute());
        }

        /// <summary>
        ///     Retrieves the subscription associated to this transaction
        /// </summary>
        [PublicAPI]
        public Subscription GetSubscription()
        {
            if (!SubscriptionId.HasValue)
                throw new InvalidOperationException("This transaction isn't associated to any subscription.");

            return Provider.Subscriptions.Find(SubscriptionId.Value);
        }

        /// <summary>
        ///     Converts this class to it string representation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("#{0} {1:#.00}", Id, Amount);
        }
    }
}