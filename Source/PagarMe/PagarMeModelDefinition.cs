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
using System.Reflection;
using Newtonsoft.Json.Linq;
using Remotion.Linq;

namespace PagarMe
{
    internal class PagarMeModelDefinition
    {
        private readonly ConstructorInfo _ctor;
        private readonly string _endpoint;
        private readonly Type _type;

        public PagarMeModelDefinition(Type type)
        {
            _type = type;
            _ctor = _type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null,
                new[] {typeof(PagarMeProvider)}, null);

            PagarMeModelAttribute model = _type.GetCustomAttribute<PagarMeModelAttribute>();

            if (model == null)
                throw new InvalidOperationException("Class must be marked with PagarMeModelAttribute.");

            _endpoint = model.Endpoint;
        }

        public Type Type
        {
            get { return _type; }
        }

        public string Endpoint
        {
            get { return _endpoint; }
        }

        public PagarMeModelQuery<T> CreateQuery<T>(PagarMeProvider pagarme, QueryModel queryModel)
        {
            if (typeof(T) != _type)
                throw new InvalidOperationException("This class only supports " + _type);

            PagarMeModelQuery<T> query = new PagarMeModelQuery<T>(this, pagarme);
            queryModel.Accept(query);
            return query;
        }

        public object Build(JObject data, PagarMeProvider provider)
        {
            PagarMeModel model = (PagarMeModel)_ctor.Invoke(new object[] {provider});
            model.Refresh(data);
            return model;
        }
    }
}