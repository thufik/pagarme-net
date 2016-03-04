using System;
using NUnit.Framework;
using PagarMe;

namespace PagarMe.Tests
{
	[TestFixture]
	public class FingerprintTests : PagarMeTestFixture
	{
		[Test]
		public void CheckFingerprint() {
			var expectedResult = "fd29daad6c47ff78c1604395320b60bac87830cb";
			var inputData = "{\"sample\":\"payload\",\"value\":true}";
			Assert.AreEqual(PagarMe.Utils.CalculateRequestHash(inputData), expectedResult);
			Assert.IsTrue(PagarMe.Utils.validateRequestSignature(inputData, expectedResult));
		}
	}
}
