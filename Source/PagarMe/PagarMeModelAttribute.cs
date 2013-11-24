using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagarMe
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal class PagarMeModelAttribute : Attribute
    {
        public string Endpoint { get; private set; }

        public PagarMeModelAttribute(string endpoint)
        {
            Endpoint = endpoint;
        }
    }
}
