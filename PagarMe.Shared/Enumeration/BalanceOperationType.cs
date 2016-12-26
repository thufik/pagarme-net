using System;
using System.Collections.Generic;
using System.Text;

namespace PagarMe.Enumeration
{
    public enum OperationType
    {
        [Base.EnumValue("payable")] Payable,
        [Base.EnumValue("anticipation")] Anticipation,
        [Base.EnumValue("transfer")] Transfer
    }
}
