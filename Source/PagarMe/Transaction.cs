using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using PagarMe.Converters;

namespace PagarMe
{
    [PagarMeModel("transactions")]
    public class Transaction : PagarMeModel
    {
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

        internal Transaction(PagarMeProvider provider)
            : base(provider)
        {
        }

        internal Transaction(PagarMeProvider provider, PagarMeQueryResponse result)
            : base(provider, result)
        {
            
        }

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
