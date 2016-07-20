using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PagarMe.Tests
{
	[TestFixture]
	public class RecipientTests : PagarMeTestFixture
	{
		[Test]
		public void CreateWithOldFields ()
		{
			var bank = CreateTestBankAccount ();

			bank.Save ();

			Assert.IsNotNull (bank.Id);

			var recipient = new Recipient () {
				TransferDay = 5,
				TransferEnabled = true,
				TransferInterval = TransferInterval.Weekly,
				BankAccount = bank
			};

			recipient.Save ();

			Assert.IsNotNull (recipient.Id);
		}

		[Test]
		public void CreateWithNewFields ()
		{
			var bank = CreateTestBankAccount ();

			bank.Save ();

			Assert.IsNotNull (bank.Id);

			var recipient = new Recipient () {
				AnticipatableVolumePercentage = 88,
				AutomaticAnticipationEnabled = true,
				TransferDay = 5,
				TransferEnabled = true,
				TransferInterval = TransferInterval.Weekly,
				BankAccount = bank
			};

			recipient.Save ();

			Assert.IsNotNull (recipient.Id);
			Assert.AreEqual (recipient.AnticipatableVolumePercentage, 88);
			Assert.AreEqual (recipient.AutomaticAnticipationEnabled, true);
		}
	}
}