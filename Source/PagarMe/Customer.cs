using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PagarMe.Converters;
using PagarMe.Serializer;

namespace PagarMe
{
    [PagarMeModel("customers")]
    public class Customer : PagarMeModel, IFreezable
    {
        private readonly FreezableCollection<CustomerAddress> _addresses;
        private readonly FreezableCollection<CustomerPhone> _phones;

        private bool _freezed;
        private string _name, _email, _documentNumber;
        private CustomerDocumentType _documentType;
        private CustumerSex _sex;
        private DateTime? _bornAt, _dateCreated;

        [JsonProperty(PropertyName = "name")]
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

        [JsonProperty(PropertyName = "email")]
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

        [JsonProperty(PropertyName = "document_number")]
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

        [JsonProperty(PropertyName = "document_type")]
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

        [JsonProperty(PropertyName = "sex")]
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

        [JsonProperty(PropertyName = "born_at")]
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

        [JsonProperty(PropertyName = "date_created")]
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

        [JsonProperty(PropertyName = "addresses")]
        [UrlMutator(typeof(SingleItemConverter))]
        public ICollection<CustomerAddress> Addresses
        {
            get { return _addresses; }
        }

        [JsonProperty(PropertyName = "phones")]
        [UrlMutator(typeof(SingleItemConverter))]
        public ICollection<CustomerPhone> Phones
        {
            get { return _phones; }
        }

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
            _addresses.Freeze();
            _phones.Freeze();
            _freezed = true;
        }

        public override string ToString()
        {
            return string.Format("#{0} {1}", Id, Name);
        }
    }
}
