using System;
using System.Collections.Generic;
using System.Text;

namespace PagarMe.Enumeration
{
    public enum OperationStatus
    {
        [Base.EnumValue("available")] Availables,
        [Base.EnumValue("transferred")] Transferred,
        [Base.EnumValue("waiting_funds")] WaitingFunds
    }
}
