#region License

// The MIT License (MIT)
// 
// Copyright (c) 2013 Pagar.me
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

using System.Linq;
using Newtonsoft.Json;
using PagarMe;

namespace TestApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var pagarme = new PagarMeProvider("ak_test_KGXIjQ4GicOa2BLGZrDRTR5qNQxDWo",
                "ek_test_nV2WKtwCedTzEGSLKQpbgDpRj8jdfR");

            var customer = new Customer
            {
                Email = "jonathan@pagar.me"
            };

            customer.Addresses.Add(new CustomerAddress
                {
                    ZipCode = "22260000",
                    City = "São Paulo",
                    Neighborhood = "Jardim Paulistano",
                    State = "São Paulo",
                    Country = "Brasil",
                    Number = "2941",
                    Complementary = "8 andar",
                    Street = "Av Brigadeiro Faria Lima"
                });

            customer.Phones.Add(new CustomerPhone
                {
                    Ddd = 11,
                    Number = 962617113
                });

            var transaction = pagarme.PostTransaction(new TransactionSetup
                {
                    Amount = 10.99m,
                    PaymentMethod = PaymentMethod.CreditCard,
                    Customer = customer,
                    CardHash = pagarme.GenerateCardHash(new CreditCard {
                        CardNumber = "4242424242424242",
                        CardCvv = "321",
                        CardExpirationDate = "1117",
                        CardholderName = "Test"
                    })
                });

            transaction.Refund();
        }
    }
}