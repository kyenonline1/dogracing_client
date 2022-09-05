using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.MAU
{
    public class MAUReadyResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU7";
        }

        public MAUReadyResponse()
        {

        }

        public MAUReadyResponse(Dictionary<byte, object> data) : base(data)
        {

        }
    }
}
