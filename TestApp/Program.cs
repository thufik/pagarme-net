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
                Amount = 1975.50m,
                PaymentMethod = PaymentMethod.CreditCard,
                CardNumber = "4901720080344448",
                CardHolderName = "Jose da Silva",
                CardExpirationDate = "1215",
                CardCvv = "314",
                Customer = new Customer
                {
                    Name = "José",
                    DocumentNumber = "51472745531",
                    Email = "josedasilva@gmail.com",
                    Sex = CustumerSex.Male,
                    BornAt = new DateTime(1969, 07, 20),
                    Addresses =
                    {
                        new CustomerAddress
                        {
                            Street = "Av Manuel",
                            Number = "2941",
                            Complementary = "5 andar",
                            Neighborhood = "Itaim Bibi",
                            ZipCode = "01452000"
                        }
                    },
                    Phones =
                    {
                        new CustomerPhone
                        {
                            Ddd = 11,
                            Number = 981836482
                        }
                    }
                }
            });
            var transactions = pagarme.Transactions.ToArray();
        }
    }
}
