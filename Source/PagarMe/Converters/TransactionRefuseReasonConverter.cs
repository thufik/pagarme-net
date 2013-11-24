using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PagarMe.Serializer;

namespace PagarMe.Converters
{
    internal class TransactionRefuseReasonConverter : JsonConverter, IUrlConverter
    {
        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TransactionRefuseReason);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string status = reader.Value as string;
            TransactionRefuseReason result = TransactionRefuseReason.None;

            switch (status)
            {
                case "acquirer":
                    result = TransactionRefuseReason.Acquirer;
                    break;
                case "antifraud":
                    result = TransactionRefuseReason.Antifraud;
                    break;
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Status is read only.");
        }

        public object UrlConvert(object input)
        {
            switch ((TransactionRefuseReason)input)
            {
                case TransactionRefuseReason.Acquirer:
                    return "acquirer";
                case TransactionRefuseReason.Antifraud:
                    return "antifraud";
            }

            return null;
        }
    }
}
