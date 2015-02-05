//
// AbstractModel.cs
//
// Author:
//       Jonathan Lima <jonathan@pagar.me>
//
// Copyright (c) 2015 Pagar.me
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
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#if HAS_DYNAMIC
using System.Dynamic;
#endif

namespace PagarMe.Base
{
    public class AbstractModel
    #if HAS_DYNAMIC
        : DynamicObject
    #endif
    {
        private static Dictionary<string, Type> ModelMap = new Dictionary<string, Type>();

        static AbstractModel()
        {
            ModelMap.Add("object", typeof(AbstractModel));
            ModelMap.Add("transaction", typeof(Transaction));
            ModelMap.Add("card", typeof(Card));
            ModelMap.Add("customer", typeof(Customer));
        }

        private bool _loaded;
        private PagarMeService _service;
        private IDictionary<string, object> _keys;
        private IDictionary<string, object> _dirtyKeys;

        protected PagarMeService Service { get { return _service; } }

        public bool Loaded { get { return _loaded; } }

        public AbstractModel(PagarMeService service)
        {
            if (service == null)
                service = PagarMeService.GetDefaultService();

            _service = service;
            _keys = new Dictionary<string, object>();
            _dirtyKeys = new Dictionary<string, object>();
        }

        internal object ConvertObject(object obj)
        {
            if (obj is Array)
            {
                obj = ((object[])obj).Select((x) => ConvertObject(x));
            }
            else if (obj is JContainer)
            {
                obj = ConvertObject(((JContainer)obj).ToObject<Dictionary<string, object>>());
            }
            else if (obj is IEnumerable<KeyValuePair<string, object>>)
            {
                var type = typeof(AbstractModel);
                var keys = (IEnumerable<KeyValuePair<string, object>>)obj;
                object typeString = keys.FirstOrDefault((x) => x.Key == "object").Value;

                if (typeString != null)
                    ModelMap.TryGetValue(typeString.ToString(), out type);

                var model = (AbstractModel)Activator.CreateInstance(type, new object[] { _service });

                model.LoadFrom(keys);

                obj = model;
            }

            return obj;
        }

        internal void LoadFrom(string json)
        {
            LoadFrom(JsonConvert.DeserializeObject<Dictionary<string, object>>(json));
        }

        internal void LoadFrom(IEnumerable<KeyValuePair<string, object>> keys)
        {
            _keys = keys.Select((x) => new KeyValuePair<string, object>(x.Key, ConvertObject(x.Value))).ToDictionary((x) => x.Key, (x) => x.Value);

            ClearDirtyCache();

            _loaded = true;
        }

        internal KeyValuePair<string, object> ConvertKey(KeyValuePair<string, object> obj, bool full)
        {
            var key = obj.Key;
            var value = obj.Value;

            if (value is Array)
            {
                value = ((object[])value).Select((x) => ConvertKey(new KeyValuePair<string, object>("", x), full).Value);
            }
            else if (value is Model)
            {
                key += "_id";
                value = ((Model)value).Id;
            }
            else if (value is AbstractModel)
            {
                value = ((AbstractModel)value).GetKeys(full);
            }

            return new KeyValuePair<string, object>(key, value);
        }

        internal IEnumerable<KeyValuePair<string, object>> GetKeys(bool full)
        {
            IEnumerable<KeyValuePair<string, object>> keys;

            if (full)
                keys = _keys.Concat(_dirtyKeys);
            else
                keys = _dirtyKeys;

            keys = keys.ToList().Select((x) => {
                return ConvertKey(x, full);
            });

            return keys;
        }

        internal string ToJson(bool full = false)
        {
            return JsonConvert.SerializeObject(GetKeys(full).ToDictionary((x) => x.Key, (x) => x.Value));
        }

        protected T GetAttribute<T>(string name)
        {
            object result;

            if (!_dirtyKeys.TryGetValue(name, out result))
            if (!_keys.TryGetValue(name, out result))
                return default(T);

            if (typeof(T).GetTypeInfo().IsEnum)
                result = JValue.FromObject(result).ToObject(typeof(T));

            return (T)result;
        }

        protected void SetAttribute(string name, object value)
        {
            if (value is Enum)
                value = JValue.FromObject(value).ToObject<string>();

            _dirtyKeys[name] = value;
        }

        protected void ClearDirtyCache()
        {
            _dirtyKeys.Clear();
        }

        public object this[string key]
        {
            get { return GetAttribute<object>(key); }
            set { SetAttribute(key, value); }
        }

        #if HAS_DYNAMIC
        private string ConvertKeyName(string input)
        {
            var result = "";

            for (var i = 0; i < input.Length; i++)
            {
                if (i > 0 && char.IsUpper(input[i]))
                    result += "_" + input[i];
                else
                    result += input[i];
            }

            return result.ToLowerInvariant();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var name = ConvertKeyName(binder.Name);

            if (_dirtyKeys.TryGetValue(name, out result))
                return true;

            if (_keys.TryGetValue(name, out result))
                return true;

            return false;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _dirtyKeys[ConvertKeyName(binder.Name)] = value;
            return true;
        }
        #endif
    }
}

