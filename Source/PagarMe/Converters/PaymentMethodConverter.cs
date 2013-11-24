using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PagarMe.Serializer;

namespace PagarMe.Converters
{
    internal class PaymentMethodConverter : JsonConverter, IUrlConverter
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
            writer.WriteValue(UrlConvert(value));
        }

        public object UrlConvert(object input)
        {
            switch ((PaymentMethod)input)
            {
                case PaymentMethod.CreditCard:
                    return "credit_card";
                case PaymentMethod.Boleto:
                    return "boleto";
            }

            return null;
        }
    }
}
