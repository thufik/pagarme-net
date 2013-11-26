using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PagarMe;

namespace TestApp
{
    static class Program
    {
        public class Test
        {
            [JsonProperty]
            public string A { get; set; }
        }

        static void Main(string[] args)
        {
            var pagarme = new PagarMeProvider("ak_test_RBORKsHflgcrO7gISMyhatMx8UyiJY", "ek_test_nV2WKtwCedTzEGSLKQpbgDpRj8jdfR");
            var creditcard = new CreditCard();

            creditcard.CardholderName = "Jose da Silva";
            creditcard.CardNumber = "5433229077370451";
            creditcard.CardExpirationDate = "1016";
            creditcard.CardCvv = "018";

            pagarme.MetadataSerializerSettings = new JsonSerializerSettings();
            pagarme.MetadataSerializerSettings.TypeNameHandling = TypeNameHandling.Objects;

            string cardHash = pagarme.GenerateCardHash(creditcard);
            var transaction = pagarme.PostTransaction(new TransactionSetup
            {
                Amount = 10.99m,
                PaymentMethod = PaymentMethod.CreditCard,
                CardHash = cardHash,
                Metadata = new
                {
                    Test = "hello",
                    A = new Test
                    {
                        A = "test"
                    }
                }
            });
        }
    }
}
