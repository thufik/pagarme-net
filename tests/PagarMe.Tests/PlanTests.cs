using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PagarMe.Tests
{
    [TestFixture]
    public class PlanTests : PagarMeTestFixture
    {
        [Test]
        public void ValidationTest()
        {
            Assert.Throws<FormatException>(() => new Plan(CreateProvider())
            {
                Days = -10
            }.Save());

            Assert.Throws<FormatException>(() => new Plan(CreateProvider())
            {
                TrialDays = -10
            }.Save());

            Plan plan = CreateTestPlan();
            plan.Save();

            Assert.Greater(plan.Id, 0);
            int oldId = plan.Id;

            Assert.Throws<InvalidOperationException>(() => plan.Amount = 5.99m);
            Assert.Throws<InvalidOperationException>(() => plan.Days += 5);
            Assert.Throws<InvalidOperationException>(() => plan.TrialDays += 5);

            Assert.DoesNotThrow(() =>
            {
                plan.Name = "Plan Silver Test";
                plan.Color = "#ff0ff";
                
                // TODO: Fix API error?
                // plan.Save();
            });

            Assert.AreEqual(plan.Id, oldId);
        }
    }
}
