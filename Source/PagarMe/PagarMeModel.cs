using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PagarMe
{
    public abstract class PagarMeModel
    {
        private readonly PagarMeProvider _provider;

        public PagarMeProvider Provider
        {
            get
            {
                return _provider;
            }
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; private set; }

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
            Refresh(
                new PagarMeQuery(Provider, "GET",
                    string.Format("{0}/{1}", GetType().GetCustomAttribute<PagarMeModelAttribute>().Endpoint, Id))
                    .Execute());
        }

        internal void Refresh(PagarMeQueryResponse response)
        {
            JsonConvert.PopulateObject(response.Data, this);
        }

        internal void Refresh(JObject response)
        {
            // FIXME: Should exist a better way to do this
            JsonConvert.PopulateObject(response.ToString(Formatting.None), this);
        }
    }
}
