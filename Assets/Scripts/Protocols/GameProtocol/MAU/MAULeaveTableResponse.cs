using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.MAU
{
    public class MAULeaveTableResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU5";
        }

        public MAULeaveTableResponse()
        {

        }

        public MAULeaveTableResponse(Dictionary<byte, object> data) : base(data)
        {

        }
    }
}
