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

namespace PagarMe
{
    /// <summary>
    ///     Credit card information
    /// </summary>
    public class CreditCard
    {
        /// <summary>
        ///     Credit card number
        /// </summary>
        [JsonProperty(PropertyName = "card_number"), UsedImplicitly]
        public string CardNumber { get; set; }

        /// <summary>
        ///     Credit card owner name
        /// </summary>
        [JsonProperty(PropertyName = "card_holder_name"), UsedImplicitly]
        public string CardholderName { get; set; }

        /// <summary>
        ///     Credit card expiration date
        /// </summary>
        /// <remarks>
        ///     Date in format mmyy, eg. 1016
        /// </remarks>
        [JsonProperty(PropertyName = "card_expiration_date"), UsedImplicitly]
        public string CardExpirationDate { get; set; }

        /// <summary>
        ///     Card verification number
        /// </summary>
        [JsonProperty(PropertyName = "card_cvv"), UsedImplicitly]
        public string CardCvv { get; set; }
    }
}