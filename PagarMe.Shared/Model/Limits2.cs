using System;
using System.Collections.Generic;
using System.Text;

namespace PagarMe.Model
{
    class Limits2 : Base.Model
    {
        protected override string Endpoint { get { return "/limits"; } }

        public Limits2() : base(null) { }

        public Limits2(PagarMeService service) : base(service) { }

        public Limits2(PagarMeService service, string endpointPrefix) : base(service, endpointPrefix) { }

        public BulkAnticipationLimits Maximum
        {
            get { return GetAttribute<BulkAnticipationLimits>("maximum"); }
            set { SetAttribute("maximum", value); }
        }

        public BulkAnticipationLimits Minimum
        {
            get { return GetAttribute<BulkAnticipationLimits>("minimum"); }
            set { SetAttribute("minimum", value); }
        }

        public class BulkAnticipationLimits : Base.Model
        {
            protected override string Endpoint { get { return "/limits"; } }

            public BulkAnticipationLimits() : base(null) { }

            public BulkAnticipationLimits(PagarMeService serivce) : base(serivce) { }

            public BulkAnticipationLimits(PagarMeService serivce, string endpointPrefix = "") : base(serivce, endpointPrefix) { }

            public int Amount
            {
                get { return GetAttribute<int>("amount"); }
                set { SetAttribute("amount", value); }
            }

            public int Fee
            {
                get { return GetAttribute<int>("fee"); }
                set { SetAttribute("fee", value); }
            }

            public int AnticipationFee
            {
                get { return GetAttribute<int>("anticipation_fee"); }
                set { SetAttribute("anticipation_fee", value); }
            }
        }

    }
}
