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
	internal class SubscriptionStatusConverter : JsonConverter, IUrlConverter
	{
		public override bool CanWrite
		{
			get { return false; }
		}

		public object UrlConvert(object input, UrlEncodingContext context)
		{
			switch ((SubscriptionStatus)input)
			{
			case SubscriptionStatus.Paid:
				return "paid";
			case SubscriptionStatus.PendingPayment:
				return "pending_payment";
			case SubscriptionStatus.Unpaid:
				return "unpaid";
			case SubscriptionStatus.Canceled:
				return "canceled";
			case SubscriptionStatus.Trialing:
				return "trialing";
			}

			return null;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(SubscriptionStatus);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
			JsonSerializer serializer)
		{
			string status = reader.Value as string;
			SubscriptionStatus result = SubscriptionStatus.Local;

			switch (status)
			{
			case "paid":
				result = SubscriptionStatus.Paid;
				break;
			case "pending_payment":
				result = SubscriptionStatus.PendingPayment;
				break;
			case "unpaid":
				result = SubscriptionStatus.Unpaid;
				break;
			case "canceled":
				result = SubscriptionStatus.Canceled;
				break;
			case "trialing":
				result = SubscriptionStatus.Trialing;
				break;
			}

			return result;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
	}
}
