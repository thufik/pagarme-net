using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagarMe.Tests
{
	public class PagarMeTestFixture
	{
		static PagarMeTestFixture ()
		{
			PagarMeService.DefaultApiKey = "ak_test_AAAfFBJDvGNMA6YMEoxRyIrK0PlhLI";
			PagarMeService.DefaultEncryptionKey = "ek_test_D8fnTNOqaPBQx46QBiDprUzeophI7q";
		}


        public static Transfer CreateTestTransfer()
        {
            return new Transfer
            {
                Amount = 1000,
                BankAccountId = "17311434",
                RecipientId = "re_civy2bozv086vjk6e2xsmhd43"
            };
        }


		public static Plan CreateTestPlan ()
		{
			return new Plan () {
				Name = "Test Plan",
				Days = 30,
				TrialDays = 0,
				Amount = 1099,
				Color = "#787878",
				PaymentMethods = new PaymentMethod[] { PaymentMethod.CreditCard }
			};
		}

		public static BankAccount CreateTestBankAccount ()
		{
			return new BankAccount () {
				BankCode = "184",
				Agencia = "0808",
				AgenciaDv = "8",
				Conta = "08808",
				ContaDv = "8",
				DocumentNumber = "43591017833",
				LegalName = "TesteTestadoTestando"
			};
		}

		public static Transaction CreateTestTransaction ()
		{
			return new Transaction {
				Amount = 1099,
				PaymentMethod = PaymentMethod.CreditCard,
				CardHash = GetCardHash ()
			};
		}

        public static Transaction CreateTestBoletoTransaction()
        {
            return new Transaction
            {
                Amount = 1000,
                PaymentMethod = PaymentMethod.Boleto
            };
        }

		public static string GetCardHash ()
		{
			var creditcard = new CardHash ();

			creditcard.CardHolderName = "Jose da Silva";
			creditcard.CardNumber = "5433229077370451";
			creditcard.CardExpirationDate = "101";
			creditcard.CardCvv = "018";

			return creditcard.Generate ();
		}
	}
}
