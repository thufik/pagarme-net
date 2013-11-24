using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagarMe
{
    public class PagarMeException : Exception
    {
        internal PagarMeException(PagarMeQueryResponse response)
        {
            
        }
    }
}
