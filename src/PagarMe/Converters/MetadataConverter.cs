using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PagarMe.Serializer;

namespace PagarMe.Converters
{
    internal class MetadataConverter : JsonConverter, IUrlConverter
    {
        public override bool CanWrite
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JsonSerializer custom =
                JsonSerializer.Create(((ProviderWrapper)serializer.Context.Context).Provider.MetadataSerializerSettings);

            if (reader.TokenType == JsonToken.String)
                return custom.Deserialize(new StringReader((string)reader.Value), objectType);

            return custom.Deserialize(reader, objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public object UrlConvert(object input, UrlEncodingContext context)
        {
            return JsonConvert.SerializeObject(input, Formatting.None, context.MetadataSerializerSettings);
        }
    }
}
