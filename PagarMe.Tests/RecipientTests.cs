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

			Assert.AreNotEqual (bank.Id, 0);

			var recipient = new Recipient () {
				TransferDay = 5,
				TransferEnabled = true,
				TransferInterval = TransferInterval.Weekly,
				BankAccount = bank
			};

			recipient.Save ();

			Assert.AreNotEqual (recipient.Id, 0);
		}

		[Test]
		public void CreateWithNewFields ()
		{
			var bank = CreateTestBankAccount ();

			bank.Save ();

			Assert.AreNotEqual (bank.Id, 0);

			var recipient = new Recipient () {
				AnticipatableVolumePercentage = 88,
				AutomaticAnticipationEnabled = true,
				TransferDay = 5,
				TransferEnabled = true,
				TransferInterval = TransferInterval.Weekly,
				BankAccount = bank
			};

			recipient.Save ();

			Assert.AreNotEqual (recipient.Id, 0);
		}
	}
}