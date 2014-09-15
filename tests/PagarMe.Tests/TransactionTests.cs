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
            var transaction = CreateProvider().PostTransaction(CreateTestTransaction());

            Assert.IsTrue(transaction.Status == TransactionStatus.Paid);
        }

        [Test]
        public void Authorize()
        {
            var transaction = CreateProvider().PostTransaction(CreateTestTransaction(), false);

            Assert.IsTrue(transaction.Status == TransactionStatus.Authorized);

            transaction.Capture();

            Assert.IsTrue(transaction.Status == TransactionStatus.Paid);
        }

        [Test]
        public void Refund()
        {
            var transaction = CreateProvider().PostTransaction(CreateTestTransaction());

            transaction.Refund();

            Assert.IsTrue(transaction.Status == TransactionStatus.Refunded);
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
