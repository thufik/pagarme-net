using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PagarMe.Converters
{
    public class CustomerSexConverter : JsonConverter
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
                case "m":
                    result = CustumerSex.Male;
                    break;
                case "f":
                    result = CustumerSex.Female;
                    break;
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string result = null;

            switch ((CustumerSex)value)
            {
                case CustumerSex.Male:
                    result = "m";
                    break;
                case CustumerSex.Female:
                    result = "f";
                    break;
            }

            if (result != null)
                writer.WriteValue(result);
            else
                writer.WriteNull();
        }
    }
}
