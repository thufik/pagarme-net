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
using System.Linq;
using Remotion.Linq;

namespace PagarMe
{
    internal class PagarMeQueryExecutor : IQueryExecutor
    {
        private static readonly Dictionary<Type, PagarMeModelDefinition> ModelCache =
            new Dictionary<Type, PagarMeModelDefinition>();

        private readonly PagarMeProvider _pagarme;

        public PagarMeQueryExecutor(PagarMeProvider pagarme)
        {
            _pagarme = pagarme;
        }

        public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
        {
            Type type = typeof(T);
            PagarMeModelDefinition model;

            lock (ModelCache)
                if (!ModelCache.TryGetValue(type, out model))
                    ModelCache[type] = model = new PagarMeModelDefinition(type);

            return model.CreateQuery<T>(_pagarme, queryModel).Execute();
        }

        public T ExecuteScalar<T>(QueryModel queryModel)
        {
            return ExecuteCollection<T>(queryModel).Single();
        }

        public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
        {
            var sequence = ExecuteCollection<T>(queryModel);
            return returnDefaultWhenEmpty ? sequence.SingleOrDefault() : sequence.Single();
        }
    }
}