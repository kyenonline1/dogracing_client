using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKRLeaveTableResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "SBI_PKR6";
        }

        public PKRLeaveTableResponse()
        {
        }

        public PKRLeaveTableResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
