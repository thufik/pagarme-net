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

			Assert.AreEqual(CreateProvider().PostSubscription(setup).Status, SubscriptionStatus.Paid);
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

			var subscription = CreateProvider().PostSubscription(setup);

			Assert.AreEqual(subscription.Status, SubscriptionStatus.PendingPayment);

			subscription.Charge(10.99m);

			Assert.AreEqual(subscription.Status, SubscriptionStatus.Paid);
        }

		[Test]
		public void CancelSubscription()
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

			var subscription = CreateProvider().PostSubscription(setup);

			subscription.CancelSubscription();

			Assert.AreEqual(subscription.Status, SubscriptionStatus.Canceled);
		}
    }
}
