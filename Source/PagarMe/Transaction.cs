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
using Newtonsoft.Json;
using PagarMe.Converters;

namespace PagarMe
{
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

        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof(TransactionStatusConverter))]
        public TransactionStatus Status { get; private set; }

        [JsonProperty(PropertyName = "refuse_reason")]
        [JsonConverter(typeof(TransactionRefuseReasonConverter))]
        public TransactionRefuseReason RefuseReason { get; private set; }

        [JsonProperty(PropertyName = "date_created")]
        public DateTime DateCreated { get; private set; }

        [JsonProperty(PropertyName = "amount")]
        [JsonConverter(typeof(AmountConverter))]
        public decimal Amount { get; private set; }

        [JsonProperty(PropertyName = "installments")]
        public int Installments { get; private set; }

        [JsonProperty(PropertyName = "card_holder_name")]
        public string CardHolderName { get; private set; }

        [JsonProperty(PropertyName = "card_last_digits")]
        public string CardLastDigits { get; private set; }

        [JsonProperty(PropertyName = "card_brand")]
        public string CardBrand { get; private set; }

        [JsonProperty(PropertyName = "postback_url")]
        public string PostbackUrl { get; private set; }

        [JsonProperty(PropertyName = "payment_method")]
        [JsonConverter(typeof(PaymentMethodConverter))]
        public PaymentMethod PaymentMethod { get; private set; }

        [JsonProperty(PropertyName = "antifraud_score")]
        public int? AntifraudScore { get; private set; }

        [JsonProperty(PropertyName = "boleto_url")]
        public string BoletoUrl { get; private set; }

        [JsonProperty(PropertyName = "boleto_barcode")]
        public string BoletoBarcode { get; private set; }

        [JsonProperty(PropertyName = "subscription_id")]
        public string SubscriptionId { get; private set; }

        [JsonProperty(PropertyName = "customer")]
        [JsonConverter(typeof(PagarMeModelConverter<Customer>))]
        public Customer Customer { get; private set; }

        [JsonProperty(PropertyName = "address")]
        public CustomerAddress Address { get; private set; }

        [JsonProperty(PropertyName = "phone")]
        public CustomerPhone Phone { get; private set; }

        public void Refund()
        {
            Refresh(new PagarMeQuery(Provider, "POST", string.Format("transactions/{0}/refund", Id)).Execute());
        }

        public override string ToString()
        {
            return string.Format("#{0} {1:#.00}", Id, Amount);
        }
    }
}