using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PagarMe.Converters
{
    public class TransactionStatusConverter : JsonConverter
    {
        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TransactionStatus);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string status = reader.Value as string;
            TransactionStatus result = TransactionStatus.Local;

            switch (status)
            {
                case "processing":
                    result = TransactionStatus.Processing;
                    break;
                case "waiting_payment":
                    result = TransactionStatus.WaitingPayment;
                    break;
                case "refused":
                    result = TransactionStatus.Refused;
                    break;
                case "paid":
                    result = TransactionStatus.Paid;
                    break;
                case "refunded":
                    result = TransactionStatus.Refunded;
                    break;
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Status is read only.");
        }
    }
}
