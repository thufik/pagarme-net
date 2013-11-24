using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Remotion.Linq;

namespace PagarMe
{
    internal class PagarMeModelDefinition
    {
        private readonly Type _type;
        private readonly string _endpoint;

        public Type Type
        {
            get { return _type; }
        }

        public string Endpoint
        {
            get { return _endpoint; }
        }


        public PagarMeModelDefinition(Type type)
        {
            _type = type;

            PagarMeModelAttribute model = _type.GetCustomAttribute<PagarMeModelAttribute>();

            if (model == null)
                throw new InvalidOperationException("Class must be marked with PagarMeModelAttribute.");

            _endpoint = model.Endpoint;
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
            PagarMeModel model = (PagarMeModel)Activator.CreateInstance(_type, provider);
            model.Refresh(data);
            return model;
        }
    }
}
