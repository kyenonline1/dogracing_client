using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.DOG
{
    public class DOG9ChatResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "AIG9";
        }
        public DOG9ChatResponse()
        {
        }

        public DOG9ChatResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
