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

using JetBrains.Annotations;
using Newtonsoft.Json;
using PagarMe.Converters;

namespace PagarMe
{
    /// <summary>
    ///     Transaction creation data
    /// </summary>
    [PublicAPI]
    public class TransactionSetup
    {
        /// <summary>
        ///     Amount in R$
        /// </summary>
        [PublicAPI]
        [JsonProperty(PropertyName = "amount")]
        [JsonConverter(typeof(AmountConverter))]
        public decimal Amount { get; set; }

        /// <summary>
        ///     Payment method
        /// </summary>
        [PublicAPI]
        [JsonProperty(PropertyName = "payment_method")]
        [JsonConverter(typeof(PaymentMethodConverter))]
        public PaymentMethod PaymentMethod { get; set; }

        /// <summary>
        ///     Card hash
        /// </summary>
        /// <remarks>
        ///     This hash should be generated in the web browser or using the CreditCard class
        /// </remarks>
        [PublicAPI]
        [JsonProperty(PropertyName = "card_hash")]
        public string CardHash { get; set; }

        /// <summary>
        ///     Customer owning this transaction
        /// </summary>
        /// <remarks>
        ///     Optional if antifraud is disabled
        /// </remarks>
        [PublicAPI]
        [JsonProperty(PropertyName = "customer")]
        public Customer Customer { get; set; }

        /// <summary>
        ///     URL to send updates about this transaction
        /// </summary>
        [PublicAPI]
        [JsonProperty(PropertyName = "postback_url")]
        public string PostbackUrl { get; set; }
    }
}