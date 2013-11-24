using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PagarMe.Serializer;

namespace PagarMe
{
    public class CustomerPhone : IFreezable
    {
        private bool _freezed;
        private int _ddi, _ddd, _number;

        [UrlIgnore]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; private set; }

        [JsonProperty(PropertyName = "ddi")]
        public int Ddi
        {
            get { return _ddi; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _ddi = value;
            }
        }

        [JsonProperty(PropertyName = "ddd")]
        public int Ddd
        {
            get { return _ddd; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _ddd = value;
            }
        }

        [JsonProperty(PropertyName = "number")]
        public int Number
        {
            get { return _number; }
            set
            {
                if (_freezed)
                    throw new InvalidOperationException("This value is read-only.");

                _number = value;
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
