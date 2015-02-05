//
// ModelCollection.cs
//
// Author:
//       Jonathan Lima <jonathan@pagar.me>
//
// Copyright (c) 2014 Pagar.me
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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#if HAS_ASYNC
using System.Threading.Tasks;
#endif

namespace PagarMe.Base
{
    public class ModelCollection<TModel> where TModel : Model
    {
        private PagarMeService _service;

        internal ModelCollection()
            : this(null)
        {

        }

        internal ModelCollection(PagarMeService service)
        {
            if (service == null)
                service = PagarMeService.GetDefaultService();

            _service = service;
        }

        public TModel Find(int id, bool load = true)
        {
            return Find(id.ToString(), load);
        }

        public TModel Find(string id, bool load = true)
        {
            var model = (TModel)Activator.CreateInstance(typeof(TModel), new object[] { _service });

            if (load)
                model.Refresh(id);
            else
                model.SetId(id);

            return model;
        }

        #if HAS_ASYNC
        public Task<TModel> FindAsync(int id, bool load = true)
        {
            return FindAsync(id.ToString(), load);
        }

        public async Task<TModel> FindAsync(string id, bool load = true)
        {
            var model = (TModel)Activator.CreateInstance(typeof(TModel), new object[] { _service });

            if (load)
                await model.RefreshAsync(id);
            else
                model.SetId(id);

            return model;
        }
        #endif

        /*public TModel Find(Expression<Func<TModel, bool>> match)
        {

        }

        public async Task<TModel> FindAsync(Expression<Func<TModel, bool>> match)
        {

        }
            
        public IEnumerable<TModel> FindAll(Expression<Func<TModel, bool>> match)
        {

        }

        public async Task<IEnumerable<TModel>> FindAllAsync(Expression<Func<TModel, bool>> match)
        {

        }

        public int Count(Expression<Func<TModel, bool>> match)
        {

        }

        public async Task<int> CountAsync(Expression<Func<TModel, bool>> match)
        {

        }*/
    }
}

