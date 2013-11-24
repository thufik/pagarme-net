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
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PagarMe.Serializer;

namespace PagarMe
{
    public class CustomerAddress : IFreezable
    {
        private string _city;
        private string _complementary;
        private string _country;
        private bool _freezed;
        private string _neighborhood;
        private string _number;
        private string _state;
        private string _street;
        private string _zipcode;

        [UrlIgnore]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; private set; }

        [JsonProperty(PropertyName = "street")]
        public string Street
        {
            get { return _street; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _street = value;
            }
        }

        [JsonProperty(PropertyName = "complementary")]
        public string Complementary
        {
            get { return _complementary; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _complementary = value;
            }
        }

        [JsonProperty(PropertyName = "street_number")]
        public string Number
        {
            get { return _number; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _number = value;
            }
        }

        [JsonProperty(PropertyName = "neighborhood")]
        public string Neighborhood
        {
            get { return _neighborhood; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _neighborhood = value;
            }
        }

        [JsonProperty(PropertyName = "city")]
        public string City
        {
            get { return _city; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _city = value;
            }
        }

        [JsonProperty(PropertyName = "state")]
        public string State
        {
            get { return _state; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _state = value;
            }
        }

        [JsonProperty(PropertyName = "zipcode")]
        public string ZipCode
        {
            get { return _zipcode; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _zipcode = value;
            }
        }

        [JsonProperty(PropertyName = "country")]
        public string Country
        {
            get { return _country; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _country = value;
            }
        }

        void IFreezable.Freeze()
        {
            Freeze();
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Freeze();
        }

        internal void Freeze()
        {
            _freezed = true;
        }
    }
}