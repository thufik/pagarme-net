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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PagarMe.Serializer;

namespace PagarMe
{
    /// <summary>
    ///     Base class for API objects
    /// </summary>
    public abstract class PagarMeModel
    {
        private readonly List<string> _dirtyValues;
        internal PagarMeProvider Provider;
        private bool _doNotTrack, _local;

        internal PagarMeModel()
        {
            _dirtyValues = new List<string>();
            _local = true;
        }

        internal PagarMeModel(PagarMeProvider provider)
            : this()
        {
            Provider = provider;
        }

        internal PagarMeModel(PagarMeProvider provider, PagarMeQueryResponse result)
            : this(provider)
        {
            Refresh(result);
        }

        /// <summary>
        ///     Object ID
        /// </summary>
        [UrlIgnore]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; private set; }

        /// <summary>
        ///     Date when the transaction was created
        /// </summary>
        [UrlIgnore]
        [JsonProperty(PropertyName = "date_created"), UsedImplicitly]
        public DateTime DateCreated { get; private set; }

        /// <summary>
        ///     Refresh the object data from the remote API
        /// </summary>
        [PublicAPI]
        public void Refresh()
        {
            if (Provider == null)
                return;

            Refresh(
                new PagarMeQuery(Provider, "GET",
                    string.Format("{0}/{1}", GetType().GetCustomAttribute<PagarMeModelAttribute>().Endpoint, Id))
                    .Execute());
        }

        /// <summary>
        ///     Save the object state
        /// </summary>
        [PublicAPI]
        public void Save()
        {
            if (Provider == null)
                throw new InvalidOperationException("The PagarMeProvider must be set in order to use this method.");

            UrlEncodingContext context = new UrlEncodingContext();

            context.MetadataSerializerSettings = Provider.MetadataSerializerSettings;

            Validate();

            PagarMeQuery query;

            if (_local)
                query = new PagarMeQuery(Provider, "POST",
                    GetType().GetCustomAttribute<PagarMeModelAttribute>().Endpoint);
            else
                query = new PagarMeQuery(Provider, "PUT",
                    string.Format("{0}/{1}", GetType().GetCustomAttribute<PagarMeModelAttribute>().Endpoint, Id));

            foreach (var tuple in UrlSerializer.Serialize(this, _dirtyValues, context))
                query.AddQuery(tuple.Item1, tuple.Item2);

            _doNotTrack = true;
            Refresh(query.Execute());
            _doNotTrack = false;
            _dirtyValues.Clear();
        }

        protected virtual void Validate()
        {
        }

        protected void AddToDirtyList(string name)
        {
            if (_doNotTrack)
                return;

            if (!_dirtyValues.Contains(name))
            {
                _dirtyValues.RemoveAll(t => t.StartsWith(name + "."));
                _dirtyValues.Add(name);
            }
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> property)
        {
            AddToDirtyList(property.GetMemberInfo().Name);
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
                wrapper.Provider = Provider;
            else if (Provider == null)
                Provider = wrapper.Provider;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            _local = false;
        }
    }
}