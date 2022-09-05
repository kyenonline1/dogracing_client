using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKRCashInResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR0";
        }

        public PKRCashInResponse()
        {
        }

        public PKRCashInResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
