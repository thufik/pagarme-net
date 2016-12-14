using System;
using System.Collections.Generic;
using System.Text;
using PagarMe.Enumeration;

namespace PagarMe.Model
{
    public class BulkAnticipation : Base.Model
    {
        protected override string Endpoint { get { return "/bulk_anticipations"; } }

        public BulkAnticipation() : this(null) { }

        public BulkAnticipation(PagarMeService service) : base(service) { }

        public BulkAnticipationStatus Status
        {
            get { return GetAttribute<BulkAnticipationStatus>("status"); }
            set { SetAttribute("status", value); }
        }

        public TimeFrame Timeframe
        {
            get { return GetAttribute<TimeFrame>("timeframe"); }
            set { SetAttribute("timeframe", value); }
        }

        public string PaymentDate
        {
            get { return GetAttribute<string>("payment_date"); }
            set { SetAttribute("payment_date", value); }
        }

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
