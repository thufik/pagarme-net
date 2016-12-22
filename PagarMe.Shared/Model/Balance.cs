using PagarMe.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PagarMe.Model
{
    public class Balance : Base.Model
    {
        protected override string Endpoint { get { return "/balance"; } }

        public Balance() : this(null) { }

        public Balance(PagarMeService service) : base(service) { }

        public Balance(PagarMeService serivce, string endpointPrefix = "") : base(serivce, endpointPrefix) { }

        public Amount WaitingFunds
        {
            get { return GetAttribute<Amount>("waiting_funds"); }
        }

        public Amount Available
        {
            get { return GetAttribute<Amount>("available"); }
        }

        public Amount Transferred
        {
            get { return GetAttribute<Amount>("transferred"); }
        }

        public ModelCollection<Operation> Operations
        {
            get { return new Operation(Service, endpointPrefix).History(Endpoint); }
        }

        public class Amount : Base.Model
        {
            protected override string Endpoint { get { return "/balance"; } }

            public Amount(PagarMeService service) : base(service) { }

            public int amount
            {
                get { return GetAttribute<int>("amount"); }
            }
        }

    }
}
