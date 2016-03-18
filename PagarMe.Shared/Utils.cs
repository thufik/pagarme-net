using System;
using System.Text;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;

namespace PagarMe
{
	public class Utils {

		public static string CalculateRequestHash(string data) {
			return CalculateRequestHash(Encoding.UTF8.GetBytes(data));
		}

		public static string CalculateRequestHash(byte[] data) {
			HMac mac = new HMac(new Org.BouncyCastle.Crypto.Digests.Sha1Digest());
			mac.Init(new KeyParameter(Encoding.UTF8.GetBytes(PagarMeService.DefaultApiKey)));
			mac.BlockUpdate(data, 0, data.Length);
			byte[] result = new byte[mac.GetMacSize()];
			mac.DoFinal(result, 0);
			string hex = BitConverter.ToString(result).Replace("-", "").ToLower();
			return hex;
		}

		public static bool validateRequestSignature(string data, string fingerprint) {
			return validateRequestSignature(Encoding.UTF8.GetBytes(data), fingerprint);
		}

		public static bool validateRequestSignature(byte[] data, string fingerprint) {
			return CalculateRequestHash(data) == fingerprint;
		}
	}
}

