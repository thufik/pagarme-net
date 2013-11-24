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
            var transaction = pagarme.PostTransaction(new TransactionSetup
            {
                Amount = 1975,
                PaymentMethod = PaymentMethod.Boleto,
                Customer = new Customer
                {
                    Name = "José",
                    DocumentNumber = "51472745531",
                    Email = "jose@gmail.com",
                    Addresses =
                    {
                        new CustomerAddress
                        {
                            Street = "Av Manuel",
                            Number = "2941",
                            Complementary = "5 andar"
                        }
                    }
                }
            });
        }
    }
}
