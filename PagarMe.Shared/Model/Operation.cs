using PagarMe.Base;
using PagarMe.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PagarMe.Model
{
    public class Operation : Base.Model
    {
        protected override string Endpoint { get { return "/operations"; } }

        public Operation() : this(null) { }

        public Operation(PagarMeService service) : base(service) { }

        public Operation(PagarMeService service, string endpointPrefix = "") :base(service, endpointPrefix) { }

        public OperationStatus Status
        {
            get { return GetAttribute<OperationStatus>("status"); }
        }

        public int BalanceAmount
        {
            get { return GetAttribute<int>("balance_amout"); }
        }

        public int BalanceOldAmount
        {
            get { return GetAttribute<int>("balance_old_amout"); }
        }

        public OperationType MovementType
        {
            get { return GetAttribute<OperationType>("movement_type"); }
        }

        public int Amount
        {
            get { return GetAttribute<int>("amount"); }
        }

        public int Fee
        {
            get { return GetAttribute<int>("fee"); }
        }

        public Payable MovementPayable
        {
            get { return (Payable)ChooseOperationType(OperationType.Payable); }
        }

        public BulkAnticipation MovementBulkAnticipation
        {
            get { return (BulkAnticipation)ChooseOperationType(OperationType.Anticipation); }
        }

        public Transfer MovementTransfer
        {
            get { return (Transfer)ChooseOperationType(OperationType.Transfer); }
        }

        private Base.Model ChooseOperationType(OperationType type)
        {
            switch (type)
            {
                case OperationType.Anticipation :
                    return ChooseMovementObject(type);
                    break;
                case OperationType.Payable :
                    return ChooseMovementObject(type);
                    break;
                case OperationType.Transfer :
                    return ChooseMovementObject(type);
                default :
                    return null;
            }
        }

        private Base.Model ChooseMovementObject(OperationType type)
        {
            switch (GetAttribute<OperationType>("type"))
            {
                case OperationType.Anticipation:

                    if (type == OperationType.Anticipation)
                        return GetAttribute<BulkAnticipation>("movement_object");
                    else
                        return null;
                    break;
                case OperationType.Payable:

                    if (type == OperationType.Payable)
                        return GetAttribute<Payable>("movement_object");
                    else
                        return null;
                    break;
                case OperationType.Transfer:

                    if (type == OperationType.Transfer)
                        return GetAttribute<Transfer>("movement_object");
                    else
                        return null;
                    break;
                default:
                    return null;
                    break;
            }
        }

        public Base.ModelCollection<Operation> History(string endpoint)
        {
           return new ModelCollection<Operation>(Service, endpoint + Endpoint, endpointPrefix ); 
        }
    }
}
