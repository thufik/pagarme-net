using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PagarMe.Tests
{
    [TestFixture]
    public class TransactionTests : PagarMeTestFixture
    {
        [Test]
        public void Charge()
        {
            CreateProvider().PostTransaction(CreateTestTransaction());
        }

        [Test]
        public void Refund()
        {
            CreateProvider().PostTransaction(CreateTestTransaction()).Refund();
        }

        [Test]
        public void SendMetadata()
        {
            var setup = CreateTestTransaction();
            setup.Metadata = new
            {
                test = "uhuul"
            };

            var transaction = CreateProvider().PostTransaction(setup);
            Assert.IsTrue(transaction.Metadata.test == "uhuul");
        }
    }
}
