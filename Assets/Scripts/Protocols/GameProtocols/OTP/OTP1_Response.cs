

using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.OTP
{
    public class OTP1_Response : ResponseBase
    {

        public override void SetCodeRun()
        {
            CodeRun = "OTP1";
        }

        public OTP1_Response(Dictionary<byte, object> data) : base(data)
        {

        }

    }
}
