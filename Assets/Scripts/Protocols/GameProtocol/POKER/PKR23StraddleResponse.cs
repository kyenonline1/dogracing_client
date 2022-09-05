using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKR23StraddleResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR23";
        }

        public PKR23StraddleResponse()
        {
        }

        public PKR23StraddleResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
