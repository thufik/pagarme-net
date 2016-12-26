using System;
using System.Collections.Generic;
using System.Text;

namespace PagarMe.Enumeration
{
    public enum BalanceOperationStatus
    {
        [Base.EnumValue("available")] Availables,
        [Base.EnumValue("transferred")] Transferred,
        [Base.EnumValue("waiting_funds")] WaitingFunds
    }
}
