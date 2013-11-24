using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PagarMe.Serializer;

namespace PagarMe.Converters
{
    internal class CustomerDocumentTypeConverter : JsonConverter, IUrlConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CustomerDocumentType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string status = reader.Value as string;
            CustomerDocumentType result = CustomerDocumentType.None;

            switch (status)
            {
                case "cpf":
                    result = CustomerDocumentType.Cpf;
                    break;
                case "cnpj":
                    result = CustomerDocumentType.Cnpj;
                    break;
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string result = null;

            switch ((CustomerDocumentType)value)
            {
                case CustomerDocumentType.Cpf:
                    result = "cpf";
                    break;
                case CustomerDocumentType.Cnpj:
                    result = "cnpj";
                    break;
            }

            if (result != null)
                writer.WriteValue(result);
            else
                writer.WriteNull();
        }

        public object UrlConvert(object input)
        {
            switch ((CustomerDocumentType)input)
            {
                case CustomerDocumentType.Cpf:
                    return "cpf";
                case CustomerDocumentType.Cnpj:
                    return "cnpj";
            }

            return null;
        }
    }
}
