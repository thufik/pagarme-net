using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PagarMe.Tests
{
    [TestFixture]
    class TransferTest : PagarMeTestFixture
    {
        [Test]
        [ExpectedException(typeof(PagarMeException))]
        public void CreateTransferWithDifferentBankAccount()
        {
            BankAccount bank = PagarMeTestFixture.CreateTestBankAccount();
            bank.Save();
            Recipient recipient = PagarMeTestFixture.CreateRecipientWithAnotherBankAccount();
            recipient.Save();
            Transfer transfer = PagarMeTestFixture.CreateTestTransfer(bank.Id, recipient.Id);
            transfer.Save();
        }
        [Test]
        public void CreateTransfer()
        {

            BankAccount bank = PagarMeTestFixture.CreateTestBankAccount();
            bank.Save();
            Recipient recipient = PagarMeTestFixture.CreateRecipient(bank);
            recipient.Save();

            Transaction transaction = PagarMeTestFixture.CreateBoletoSplitRuleTransaction(recipient);
            transaction.Save();
            transaction.Status = TransactionStatus.Paid;
            transaction.Save();

            Transfer transfer = PagarMeTestFixture.CreateTestTransfer(bank.Id, recipient.Id);
            transfer.Save();
            Assert.IsTrue(transfer.Status == TransferStatus.PedingTransfer);

        }


    }
}
