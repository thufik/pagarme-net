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
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using JetBrains.Annotations;
using Newtonsoft.Json;
using PagarMe.Converters;

namespace PagarMe
{
    /// <summary>
    ///     Represents a subscription
    /// </summary>
    [PagarMeModel("subscriptions")]
    public class Subscription : PagarMeModel
    {
        internal Subscription(PagarMeProvider provider)
            : base(provider)
        {
        }

        internal Subscription(PagarMeProvider provider, PagarMeQueryResponse result)
            : base(provider, result)
        {
        }

        /// <summary>
        ///     Subscription plan
        /// </summary>
        [JsonProperty(PropertyName = "plan"), UsedImplicitly]
        [JsonConverter(typeof(PagarMeModelConverter<Plan>))]
        public Plan Plan { get; private set; }

        /// <summary>
        ///     Transaction status
        /// </summary>
        [JsonProperty(PropertyName = "status"), UsedImplicitly]
        public string Status { get; private set; }

        /// <summary>
        ///     Current period start date
        /// </summary>
        [JsonProperty(PropertyName = "current_period_start"), UsedImplicitly]
        public DateTime? CurrentPeriodStart { get; private set; }

        /// <summary>
        ///     Current period end date
        /// </summary>
        [JsonProperty(PropertyName = "current_period_end"), UsedImplicitly]
        public DateTime? CurrentPeriodEnd { get; private set; }

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
        ///     Customer Email
        /// </summary>
        [JsonProperty(PropertyName = "customer_email"), UsedImplicitly]
        public string CustomerEmail { get; private set; }

        /// <summary>
        ///     Customer information associated with this transaction
        /// </summary>
        [UsedImplicitly]
        [JsonProperty(PropertyName = "transactions", ItemConverterType = typeof(PagarMeModelConverter<Transaction>))]
        public IEnumerable<Transaction> Transactions { get; private set; }

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
        ///     Subscription metadata
        /// </summary>
        [JsonProperty(PropertyName = "metadata"), UsedImplicitly]
        [JsonConverter(typeof(MetadataConverter))]
        public dynamic Metadata { get; private set; }

        internal Subscription()
        {
        }

        /// <summary>
        ///     Cancels the subscription
        /// </summary>
        [PublicAPI]
        public void CancelSubscription()
        {
            Refresh(new PagarMeQuery(Provider, "DELETE", string.Format("subscriptions/{0}", Id)).Execute());
        }

        /// <summary>
        ///     Charges the subscription
        /// </summary>
        [PublicAPI]
        public void Charge(decimal value)
        {
            var query = new PagarMeQuery(Provider, "POST", string.Format("subscriptions/{0}", Id));

            query.AddQuery("amount", AmountConverter.Convert(value).ToString(CultureInfo.InvariantCulture));

            Refresh(query.Execute());
        }

        /// <summary>
        ///     Converts this class to it string representation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("#{0} {1}", Id, CustomerEmail);
        }
    }
}