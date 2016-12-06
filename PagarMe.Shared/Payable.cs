using System;
using System.Collections.Generic;
using System.Text;
using PagarMe.Base;

namespace PagarMe
{
    public class Payable : Base.Model
    {

        protected override string Endpoint { get { return "/payables"; } }

        public Payable() : this(null) { }

        public Payable(PagarMeService service) : base(service) {}

        public Payable(PagarMeService service, string x):base(service,x) { }

        public PayableStatus Status
        {
            get { return GetAttribute<PayableStatus>("status"); }
            private set { SetAttribute("status", value); }
        }

        public int Amount
        {
            get { return GetAttribute<int>("amount"); }
            set { SetAttribute("amount", value); }
        }

        public int Fee
        {
            get { return GetAttribute<int>("fee"); }
            private set { SetAttribute("fee", value); }
        }

        public string RecipientId
        {
            get { return GetAttribute<string>("recipient_id"); }
            set { SetAttribute("recipient_id", value); }
        }

        public int TransactionId
        {
            get { return GetAttribute<int>("transaction_id"); }
            set { SetAttribute("transaction_id", value); }
        }


/*
        public Transaction Transaction(int id)
        {
            PagarMeService.GetDefaultService()            
        }
  */      


    }
}
