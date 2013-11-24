using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagarMe
{
    public static class PagarMeModelExtensions
    {
        public static T Find<T>(this PagarMeQueryable<T> queryable, int id) where T : PagarMeModel
        {
            return queryable.Single(t => t.Id == id);
        }
    }
}
