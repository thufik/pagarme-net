using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagarMe.Tests
{
    public class PagarMeTestFixture
    {
        private static PagarMeProvider _provider;

        public static PagarMeProvider CreateProvider()
        {
            return
                _provider ?? (_provider =
                    new PagarMeProvider("ak_test_RBORKsHflgcrO7gISMyhatMx8UyiJY",
                        "ek_test_Ajej5CakM8QXGnA2lWX3AarwLWqspL"));
        }

        public static Plan CreateTestPlan()
        {
            return new Plan(CreateProvider())
            {
                Name = "Test Plan",
                Days = 30,
				TrialDays = 0,
                Amount = 10.99m,
                Color = "#787878"
            };
        }

        public static TransactionSetup CreateTestTransaction()
        {
            var creditcard = new CreditCard();

            creditcard.CardholderName = "Jose da Silva";
            creditcard.CardNumber = "5433229077370451";
            creditcard.CardExpirationDate = "1016";
            creditcard.CardCvv = "018";

            return new TransactionSetup
            {
                Amount = 10.99m,
                PaymentMethod = PaymentMethod.CreditCard,
                CardHash = GetCardHash()
            };
        }

        public static string GetCardHash()
        {
            var creditcard = new CreditCard();

            creditcard.CardholderName = "Jose da Silva";
            creditcard.CardNumber = "5433229077370451";
            creditcard.CardExpirationDate = "1016";
            creditcard.CardCvv = "018";

            return CreateProvider().GenerateCardHash(creditcard);
        }
    }
}
