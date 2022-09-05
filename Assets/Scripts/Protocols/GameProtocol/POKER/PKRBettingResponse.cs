using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKRBettingResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR5";
        }

        public PKRBettingResponse()
        {
        }

        public PKRBettingResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
