//
// Program.cs
//
// Author:
//       Jonathan Lima <jonathan@pagar.me>
//
// Copyright (c) 2014 Pagar.me
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
using System;
using System.Collections.Generic;
using System.Linq;
using PagarMe;
using Newtonsoft.Json;

namespace Playground
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            PagarMeService.DefaultApiKey = "ak_test_KGXIjQ4GicOa2BLGZrDRTR5qNQxDWo";
            PagarMeService.DefaultEncryptionKey = "ek_test_Ec8KhxISQ1tug1b8bCGxC2nXfxqRmk";

            var creditCard = new PagarMe.CardHash();

            creditCard.CardCvv = "123";
            creditCard.CardExpirationDate = "1018";
            creditCard.CardHolderName = "Jonathan";
            creditCard.CardNumber = "5268630325858009";

            var cardHash = creditCard.Generate();;

            var customer = new Customer();

            customer.Name = "Jonathan Lima";
            customer.Email = "jonathan@pagar.me";
            customer.Phone = new Phone() { Ddd = "11", Number = "962617113" };
            customer.DocumentNumber = "05737104141";
            customer.Address = new Address()
            {
                Street = "Rua Agenor de Lima Franco",
                StreetNumber = "116",
                Zipcode = "05537120",
                City = "São Paulo",
                State = "São Paulo",
                Country = "Brasil",
                Complementary = "APTO 34A",
                Neighborhood = "Jardim Peri Peri"
            };

            var transaction = new Transaction();

            transaction.CardHash = cardHash;
            transaction.Customer = customer;
            transaction.Amount = 1099;

            transaction.Save();

            var score = transaction.AntifraudScore;

        
        }
    }
}

