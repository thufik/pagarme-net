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

using System;
using Newtonsoft.Json;
using PagarMe.Serializer;

namespace PagarMe.Converters
{
    internal class TransactionStatusConverter : JsonConverter, IUrlConverter
    {
        public override bool CanWrite
        {
            get { return false; }
        }

        public object UrlConvert(object input)
        {
            switch ((TransactionStatus)input)
            {
                case TransactionStatus.Paid:
                    return "paid";
                case TransactionStatus.WaitingPayment:
                    return "waiting_payment";
                case TransactionStatus.Processing:
                    return "processing";
                case TransactionStatus.Refunded:
                    return "refunded";
                case TransactionStatus.Refused:
                    return "refused";
            }

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TransactionStatus);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            string status = reader.Value as string;
            TransactionStatus result = TransactionStatus.Local;

            switch (status)
            {
                case "processing":
                    result = TransactionStatus.Processing;
                    break;
                case "waiting_payment":
                    result = TransactionStatus.WaitingPayment;
                    break;
                case "refused":
                    result = TransactionStatus.Refused;
                    break;
                case "paid":
                    result = TransactionStatus.Paid;
                    break;
                case "refunded":
                    result = TransactionStatus.Refunded;
                    break;
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Status is read only.");
        }
    }
}