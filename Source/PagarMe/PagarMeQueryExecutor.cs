using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Remotion.Linq;

namespace PagarMe
{
    internal class PagarMeQueryExecutor : IQueryExecutor
    {
        private static readonly Dictionary<Type, PagarMeModelDefinition> ModelCache = new Dictionary<Type, PagarMeModelDefinition>(); 
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
