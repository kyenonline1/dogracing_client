using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKR1StandUpResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR1";
        }

        public PKR1StandUpResponse()
        {
        }

        public PKR1StandUpResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
