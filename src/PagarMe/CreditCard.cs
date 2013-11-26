using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
