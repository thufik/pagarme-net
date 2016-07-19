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
		public void CreateWithPlan ()
		{
			var plan = CreateTestPlan ();

			plan.Save ();

			Assert.AreNotEqual (plan.Id, 0);

			var subscription = new Subscription {
				CardHash = GetCardHash (),
				Customer = new Customer {
					Email = "josedasilva@pagar.me"
				},
				Plan = plan
			};

			subscription.Save ();

			Assert.AreEqual (subscription.Status, SubscriptionStatus.Paid);
		}

		[Test]
		public void CreateWithPlanAndNewCard ()
		{
			var plan = CreateTestPlan ();

			plan.Save ();

			Assert.AreNotEqual (plan.Id, 0);

			var subscription = new Subscription {
				CardCvv = "651",
				CardExpirationDate = "0921",
				CardHolderName = "Jose da Silva",
				CardNumber = "4242424242424242",
				Customer = new Customer {
					Email = "josedasilva@pagar.me"
				},
				Plan = plan
			};

			try {
				subscription.Save ();
				Assert.Fail ();
			} catch (PagarMeException ex) {
				Assert.AreEqual (ex.Error.Errors [0].Message, "Não foi possível realizar uma transação nesse cartão de crédito.");	
			}
		}

		[Test]
		public void CancelSubscription ()
		{
			var plan = CreateTestPlan ();
			plan.Save ();

			Assert.AreNotEqual (plan.Id, 0);

			var subscription = new Subscription {
				CardHash = GetCardHash (),
				Customer = new Customer {
					Email = "josedasilva@pagar.me"
				},
				Plan = plan
			};

			subscription.Save ();
			subscription.Cancel ();

			Assert.AreEqual (subscription.Status, SubscriptionStatus.Canceled);
		}
	}
}
