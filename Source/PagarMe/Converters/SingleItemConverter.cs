using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagarMe.Serializer;

namespace PagarMe.Converters
{
    internal class SingleItemConverter : IUrlConverter
    {
        public object UrlConvert(object input)
        {
            IList list = input as IList;

            if (list == null)
                return null;

            return list.Count > 0 ? list[0] : null;
        }
    }
}
