using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PagarMe.Tests
{
    [TestFixture]
    public class SubscriptionTests : PagarMeTestFixture
    {
        [Test]
        public void CreateWithPlan()
        {
            var plan = CreateTestPlan();
            plan.Save();

            Assert.AreNotEqual(plan.Id, 0);

            var setup = new SubscriptionSetup
            {
				CardHash = GetCardHash(),
				Customer = new Customer
				{
					Email = "josedasilva@pagar.me"
				},
                Plan = plan.Id
            };

            CreateProvider().PostSubscription(setup);
        }

        [Test]
        public void CreateWithoutPlan()
        {
            var setup = new SubscriptionSetup
            {
                CardHash = GetCardHash(),
				Customer = new Customer
				{
					Email = "josedasilva@pagar.me"
				}
            };

            CreateProvider().PostSubscription(setup).Charge(10.99m);
        }
    }
}
