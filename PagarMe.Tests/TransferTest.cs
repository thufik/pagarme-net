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
        public void CreateTransfer(bool hasBank)
        {
            Transfer transfer = CreateTestTransfer();
            transfer.Save();

            Assert.IsNotNull(transfer.Id);
        }


    }
}
