using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PagarMe
{
    public class CustomerAddress : IFreezable
    {
        private bool _freezed;
        private string _street, _complementary, _number;
        private string _neighborhood, _city, _state;
        private string _zipcode, _country;

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
