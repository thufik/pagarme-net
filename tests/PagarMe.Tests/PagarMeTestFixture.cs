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
                TrialDays = 2,
                Amount = 10.99m,
                Color = "#787878"
            };
        }
    }
}
