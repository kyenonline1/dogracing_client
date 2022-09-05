using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKROpenCardResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR6";
        }

        public PKROpenCardResponse()
        {
        }

        public PKROpenCardResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
