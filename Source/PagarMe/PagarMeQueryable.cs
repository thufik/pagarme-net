using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;

namespace PagarMe
{
    public class PagarMeQueryable<T> : QueryableBase<T> where T : PagarMeModel
    {
        public PagarMeQueryable(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

        internal PagarMeQueryable(PagarMeProvider pagarme)
            : base(new DefaultQueryProvider(typeof(PagarMeQueryable<>), QueryParser.CreateDefault(), new PagarMeQueryExecutor(pagarme)))
        {
        }
    }
}
