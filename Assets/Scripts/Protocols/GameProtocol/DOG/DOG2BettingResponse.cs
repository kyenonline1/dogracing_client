using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.DOG
{
    public class DOG2BettingResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DOG2";
        }

        public DOG2BettingResponse()
        {
            Flag = 0;
        }

        public DOG2BettingResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
