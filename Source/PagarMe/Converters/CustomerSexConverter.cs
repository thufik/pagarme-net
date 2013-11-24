using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PagarMe.Serializer;

namespace PagarMe.Converters
{
    internal class CustomerSexConverter : JsonConverter, IUrlConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CustumerSex);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string status = reader.Value as string;
            CustumerSex result = CustumerSex.None;

            switch (status)
            {
                case "M":
                    result = CustumerSex.Male;
                    break;
                case "F":
                    result = CustumerSex.Female;
                    break;
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            object result = UrlConvert(value);

            if (result != null)
                writer.WriteValue(result);
            else
                writer.WriteNull();
        }

        public object UrlConvert(object input)
        {
            switch ((CustumerSex)input)
            {
                case CustumerSex.Male:
                    return "M";
                case CustumerSex.Female:
                    return "F";
            }

            return null;
        }
    }
}
