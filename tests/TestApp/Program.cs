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

using Newtonsoft.Json;
using PagarMe;

namespace TestApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var pagarme = new PagarMeProvider("ak_test_RBORKsHflgcrO7gISMyhatMx8UyiJY",
                "ek_test_nV2WKtwCedTzEGSLKQpbgDpRj8jdfR");
            var creditcard = new CreditCard();

            creditcard.CardholderName = "Jose da Silva";
            creditcard.CardNumber = "5433229077370451";
            creditcard.CardExpirationDate = "1016";
            creditcard.CardCvv = "018";

            pagarme.MetadataSerializerSettings = new JsonSerializerSettings();
            pagarme.MetadataSerializerSettings.TypeNameHandling = TypeNameHandling.Objects;

            string cardHash = pagarme.GenerateCardHash(creditcard);
            var transaction = pagarme.PostTransaction(new TransactionSetup
            {
                Amount = 10.99m,
                PaymentMethod = PaymentMethod.CreditCard,
                CardHash = cardHash,
                Metadata = new
                {
                    Test = "hello",
                    A = new Test
                    {
                        A = "test"
                    }
                }
            });
            transaction.Refresh();
        }

        public class Test
        {
            [JsonProperty]
            public string A { get; set; }
        }
    }
}