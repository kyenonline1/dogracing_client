using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.DOG
{
    public class DOG20ClearBettingResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DOG20";
        }

        public DOG20ClearBettingResponse()
        {
            Flag = 0;
        }

        public DOG20ClearBettingResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
