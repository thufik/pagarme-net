using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagarMe
{
    public class PagarMeProvider
    {
        private readonly string _apiKey, _encryptionKey;
        private readonly PagarMeQueryable<Transaction> _transactions;

        public string ApiKey
        {
            get { return _apiKey; }
        }

        public string EncryptionKey
        {
            get { return _encryptionKey; }
        }

        public PagarMeQueryable<Transaction> Transactions
        {
            get { return _transactions; }
        }

        public PagarMeProvider(string apiKey, string encryptionKey)
        {
            _apiKey = apiKey;
            _encryptionKey = encryptionKey;
            _transactions = new PagarMeQueryable<Transaction>(this);
        }
    }
}
