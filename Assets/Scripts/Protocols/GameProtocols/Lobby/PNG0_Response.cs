using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.ATH
{
    public class PNG0_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PNG0";
        }

        public PNG0_Response(Dictionary<byte, object> data) : base(data)
        {

        }
    }
}
