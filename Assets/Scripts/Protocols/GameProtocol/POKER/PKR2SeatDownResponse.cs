using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKR2SeatDownResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR2";
        }

        public PKR2SeatDownResponse()
        {
        }

        public PKR2SeatDownResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
