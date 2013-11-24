using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PagarMe.Converters;

namespace PagarMe
{
    public class TransactionSetup
    {
        [JsonProperty(PropertyName = "amount")]
        [JsonConverter(typeof(AmountConverter))]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "payment_method")]
        [JsonConverter(typeof(PaymentMethodConverter))]
        public PaymentMethod PaymentMethod { get; set; }

        [JsonProperty(PropertyName = "card_hash")]
        public string CardHash { get; set; }

        [JsonProperty(PropertyName = "customer")]
        public Customer Customer { get; set; }
    }
}
