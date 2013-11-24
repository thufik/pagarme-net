using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagarMe;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var pagarme = new PagarMeProvider("ak_test_8ZDwjvWumtZmSE4xUVhqBVaSkuU3l9", "ek_test_nV2WKtwCedTzEGSLKQpbgDpRj8jdfR");
            var transaction = pagarme.Transactions.Find(3377);
        }
    }
}
