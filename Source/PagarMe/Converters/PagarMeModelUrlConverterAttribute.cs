using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagarMe.Converters
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class PagarMeModelUrlConverterAttribute : Attribute
    {
        public Type ConverterType { get; private set; }

        public PagarMeModelUrlConverterAttribute(Type type)
        {
            ConverterType = type;
        }
    }
}
