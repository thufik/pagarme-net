using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagarMe.Serializer
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class UrlMutatorAttribute : Attribute
    {
        public Type ConverterType { get; private set; }

        public UrlMutatorAttribute(Type type)
        {
            ConverterType = type;
        }
    }
}
