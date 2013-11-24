using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PagarMe.Serializer;

namespace PagarMe
{
    public abstract class PagarMeModel
    {
        private PagarMeProvider _provider;

        public PagarMeProvider Provider
        {
            get
            {
                return _provider;
            }
            internal set
            {
                _provider = value;
            }
        }

        [UrlIgnore]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; private set; }

        protected PagarMeModel()
        {
            
        }

        protected PagarMeModel(PagarMeProvider provider)
        {
            _provider = provider;
        }

        protected PagarMeModel(PagarMeProvider provider, PagarMeQueryResponse result)
        {
            _provider = provider;
            Refresh(result);
        }

        public void Refresh()
        {
            if (Provider == null)
                return;

            Refresh(
                new PagarMeQuery(Provider, "GET",
                    string.Format("{0}/{1}", GetType().GetCustomAttribute<PagarMeModelAttribute>().Endpoint, Id))
                    .Execute());
        }

        internal void Refresh(PagarMeQueryResponse response)
        {
            Refresh(JObject.Parse(response.Data));
        }

        internal void Refresh(JObject response)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Context = new StreamingContext(StreamingContextStates.All, new ProviderWrapper());
            JsonConvert.PopulateObject(response.ToString(Formatting.None), this, settings);
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            ProviderWrapper wrapper = ((ProviderWrapper)context.Context);

            if (wrapper.Provider == null)
                wrapper.Provider = _provider;
            else if (_provider == null)
                _provider = wrapper.Provider;
        }
    }
}
