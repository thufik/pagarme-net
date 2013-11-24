using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PagarMe.Converters
{
    internal class PagarMeModelConverter<T> : JsonConverter where T : PagarMeModel
    {
        private readonly ConstructorInfo _ctor;

        public PagarMeModelConverter()
        {
            _ctor = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(PagarMeProvider) }, null);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            PagarMeModel model = (PagarMeModel)_ctor.Invoke(new object[] { ((ProviderWrapper)serializer.Context.Context).Provider });
            serializer.Populate(reader, model);
            return model;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
