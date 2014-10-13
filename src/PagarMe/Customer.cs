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
    /// <summary>
    ///     Represents a customer
    /// </summary>
    [PagarMeModel("customers")]
    public class Customer : PagarMeModel
    {
        private readonly FreezableCollection<CustomerAddress> _addresses;
        private readonly FreezableCollection<CustomerPhone> _phones;
        private DateTime? _bornAt;

        private string _documentNumber;
        private CustomerDocumentType _documentType;
        private string _email;
        private bool _freezed;
        private string _name;
        private CustumerSex _sex;

        /// <summary>
        ///     Create a new customer
        /// </summary>
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

        /// <summary>
        ///     Customer name
        /// </summary>
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

        /// <summary>
        ///     Customer email
        /// </summary>
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

        /// <summary>
        ///     Customer document number, CPF or CPNJ
        /// </summary>
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

        /// <summary>
        ///     Customer document type
        /// </summary>
        /// <remarks>
        ///     Don't need to be set when creating a new customer.
        /// </remarks>
        [UrlIgnore]
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

        /// <summary>
        ///     Customer sex
        /// </summary>
        /// <remarks>
        ///     Optional when creating a new customer.
        /// </remarks>
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

        /// <summary>
        ///     Customer birthday
        /// </summary>
        /// <remarks>
        ///     Optional when creating a new customer.
        /// </remarks>
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

        /// <summary>
        ///     Addresses of the customer
        /// </summary>
        /// <remarks>
        ///     When sending a transaction, just the first one in this list will be used.
        /// </remarks>
        [JsonProperty(PropertyName = "addresses"), UsedImplicitly]
        [UrlMutator(typeof(SingleItemConverter), FieldName = "address")]
        public ICollection<CustomerAddress> Addresses
        {
            get { return _addresses; }
        }

        /// <summary>
        ///     Phones of the customer
        /// </summary>
        /// <remarks>
        ///     When sending a transaction, just the first one in this list will be used.
        /// </remarks>
        [JsonProperty(PropertyName = "phones"), UsedImplicitly]
        [UrlMutator(typeof(SingleItemConverter), FieldName = "phone")]
        public ICollection<CustomerPhone> Phones
        {
            get { return _phones; }
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            _freezed = false;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            _freezed = true;
        }

        /// <summary>
        ///     Converts this class to it string representation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("#{0} {1}", Id, Name);
        }
    }
}