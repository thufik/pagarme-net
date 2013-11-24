using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagarMe.Serializer
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class UrlIgnoreAttribute : Attribute
    {
    }
}
