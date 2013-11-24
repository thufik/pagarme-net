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
using System.Collections.Generic;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Newtonsoft.Json;
using PagarMe.Converters;
using PagarMe.Serializer;

namespace PagarMe
{
    [PagarMeModel("customers")]
    public class Customer : PagarMeModel
    {
        private readonly FreezableCollection<CustomerAddress> _addresses;
        private readonly FreezableCollection<CustomerPhone> _phones;
        private DateTime? _bornAt, _dateCreated;

        private string _documentNumber;
        private CustomerDocumentType _documentType;
        private string _email;
        private bool _freezed;
        private string _name;
        private CustumerSex _sex;

        public Customer()
            : this(null)
        {
        }

        internal Customer(PagarMeProvider provider)
            : base(provider)
        {
            _addresses = new FreezableCollection<CustomerAddress>();
            _phones = new FreezableCollection<CustomerPhone>();
        }

        [JsonProperty(PropertyName = "name"), UsedImplicitly]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _name = value;
            }
        }

        [JsonProperty(PropertyName = "email"), UsedImplicitly]
        public string Email
        {
            get { return _email; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _email = value;
            }
        }

        [JsonProperty(PropertyName = "document_number"), UsedImplicitly]
        public string DocumentNumber
        {
            get { return _documentNumber; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _documentNumber = value;
            }
        }

        [JsonProperty(PropertyName = "document_type"), UsedImplicitly]
        [JsonConverter(typeof(CustomerDocumentTypeConverter))]
        public CustomerDocumentType DocumentType
        {
            get { return _documentType; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _documentType = value;
            }
        }

        [JsonProperty(PropertyName = "sex"), UsedImplicitly]
        [JsonConverter(typeof(CustomerSexConverter))]
        public CustumerSex Sex
        {
            get { return _sex; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _sex = value;
            }
        }

        [JsonProperty(PropertyName = "born_at"), UsedImplicitly]
        [UrlConverter(typeof(DateConverter))]
        public DateTime? BornAt
        {
            get { return _bornAt; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _bornAt = value;
            }
        }

        [JsonProperty(PropertyName = "date_created"), UsedImplicitly]
        public DateTime? DateCreated
        {
            get { return _dateCreated; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _dateCreated = value;
            }
        }

        [JsonProperty(PropertyName = "addresses"), UsedImplicitly]
        [UrlMutator(typeof(SingleItemConverter))]
        public ICollection<CustomerAddress> Addresses
        {
            get { return _addresses; }
        }

        [JsonProperty(PropertyName = "phones"), UsedImplicitly]
        [UrlMutator(typeof(SingleItemConverter))]
        public ICollection<CustomerPhone> Phones
        {
            get { return _phones; }
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            _freezed = true;
        }

        public override string ToString()
        {
            return string.Format("#{0} {1}", Id, Name);
        }
    }
}