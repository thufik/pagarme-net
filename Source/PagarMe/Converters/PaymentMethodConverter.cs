using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PagarMe.Converters
{
    public class PaymentMethodConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(PaymentMethod);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string status = reader.Value as string;
            PaymentMethod result = PaymentMethod.CreditCard;

            switch (status)
            {
                case "credit_card":
                    result = PaymentMethod.CreditCard;
                    break;
                case "boleto":
                    result = PaymentMethod.Boleto;
                    break;
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string result = "";

            switch ((PaymentMethod)value)
            {
                case PaymentMethod.CreditCard:
                    result = "credit_card";
                    break;
                case PaymentMethod.Boleto:
                    result = "boleto";
                    break;
            }

            writer.WriteValue(result);
        }
    }
}
