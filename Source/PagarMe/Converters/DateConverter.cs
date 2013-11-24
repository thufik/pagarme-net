using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagarMe.Serializer;

namespace PagarMe.Converters
{
    public class DateConverter : IUrlConverter
    {
        public object UrlConvert(object input)
        {
            return ((DateTime)input).ToString("MM-dd-yyyy");
        }
    }
}
