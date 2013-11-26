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
    internal class CustomerDocumentTypeConverter : JsonConverter, IUrlConverter
    {
        public object UrlConvert(object input, UrlEncodingContext context)
        {
            switch ((CustomerDocumentType)input)
            {
                case CustomerDocumentType.Cpf:
                    return "cpf";
                case CustomerDocumentType.Cnpj:
                    return "cnpj";
            }

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CustomerDocumentType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            string status = reader.Value as string;
            CustomerDocumentType result = CustomerDocumentType.None;

            switch (status)
            {
                case "cpf":
                    result = CustomerDocumentType.Cpf;
                    break;
                case "cnpj":
                    result = CustomerDocumentType.Cnpj;
                    break;
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            object result = UrlConvert(value, null);

            if (result != null)
                writer.WriteValue(result);
            else
                writer.WriteNull();
        }
    }
}