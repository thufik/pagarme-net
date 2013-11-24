using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagarMe.Converters
{
    internal class AmountUrlConverter : PagarMeModelUrlConverter
    {
        public override object Convert(object input)
        {
            return decimal.Round(((decimal)input), 2) * 100m;
        }
    }
}
