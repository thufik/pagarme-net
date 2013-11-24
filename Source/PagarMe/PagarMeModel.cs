#region License

// The MIT License (MIT)
// 
// Copyright (c) 2013 Pagar.me
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PagarMe.Serializer;

namespace PagarMe
{
    public abstract class PagarMeModel
    {
        private PagarMeProvider _provider;

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

        public PagarMeProvider Provider
        {
            get { return _provider; }
            internal set { _provider = value; }
        }

        [UrlIgnore]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; private set; }

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