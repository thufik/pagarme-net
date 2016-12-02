using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PagarMe.Base;

namespace PagarMe.Tests
{
    [TestFixture]
    class PayableTest
    {
        [Test]
        public void GetPayable()
        {
            Payable payable = PagarMeTestFixture.returnPayable(288836);
            Assert.IsNotNull(payable);
        }
        [Test]
        public void GetAllPayables()
        {
            Transaction trans = PagarMeTestFixture.CreateTestTransaction();
            trans.Save();
            trans.Status = TransactionStatus.Paid;
            trans.Save();

            Payable pay = new Payable()
            {
                //yableStatus = PayableStatus.Paid
            };
            Payable[] payables = PagarMeService.GetDefaultService().Payables.FindAll(pay).ToArray();//PagarMeTestFixture.returnAllPayables();
            Assert.IsNotNull(payables);
        }



    }
}
