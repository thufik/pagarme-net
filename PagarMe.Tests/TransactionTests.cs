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
            var transaction = CreateTestTransaction();

            transaction.Save();

            Assert.IsTrue(transaction.Status == TransactionStatus.Paid);
        }

        [Test]
        public void Authorize()
        {
            var transaction = CreateTestTransaction();

            transaction.ShouldCapture = false;
            transaction.Save();

            Assert.IsTrue(transaction.Status == TransactionStatus.Authorized);

            transaction.Capture();

            Assert.IsTrue(transaction.Status == TransactionStatus.Paid);
        }

        [Test]
        public void Refund()
        {
            var transaction = CreateTestTransaction();

            transaction.Save();
            transaction.Refund();

            Assert.IsTrue(transaction.Status == TransactionStatus.Refunded);
        }

        [Test]
        public void PartialRefund()
        {
            var transaction = CreateTestTransaction();

            transaction.Save();
            int amountToBeRefunded = 100;
            transaction.Refund(amountToBeRefunded);

            Assert.IsTrue(transaction.Status == TransactionStatus.Paid);
            Assert.IsTrue(transaction.RefundedAmount == amountToBeRefunded);
        }


        [Test]
        public void SendMetadata()
        {
            var transaction = CreateTestTransaction();

            transaction.Metadata["test"] = "uhuul";

            transaction.Save();

            Assert.IsTrue(transaction.Metadata["test"].ToString() == "uhuul");
        }
    }

    [TestFixture]
    public class TransactionEventTests : PagarMeTestFixture
    {
        [Test]
        public void HasEvents()
        {
            var transaction = CreateTestTransaction();
            transaction.Save();
            var events = transaction.Events.FindAll(new Event());
            Assert.AreEqual(1, events.Count());
        }

        [Test]
        public void ThrowsExceptionIfIdNull()
        {
            try
            {
                var transaction = CreateTestTransaction();
                var events = transaction.Events;
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
            }
        }

        [Test]
        public void HasProperties()
        {
            var transaction = CreateTestTransaction();
            transaction.Save();

            var transactionEvent = transaction.Events.FindAll(new Event()).First();
            Assert.IsTrue(transactionEvent.DateCreated.HasValue);

            Assert.IsNotEmpty(transactionEvent.Model);
            Assert.IsNotEmpty(transactionEvent.ModelId);
            Assert.IsNotEmpty(transactionEvent.Id);
            Assert.IsNotEmpty(transactionEvent.Name);
            Assert.IsNotEmpty((string) transactionEvent.Payload["current_status"]);
            Assert.IsNotEmpty((string) transactionEvent.Payload["old_status"]);
            Assert.IsNotEmpty((string) transactionEvent.Payload["desired_status"]);
        }
    }
}
