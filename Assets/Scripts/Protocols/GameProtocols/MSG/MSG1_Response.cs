
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.MSG
{
    public class MSG1_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MSG1";
        }
        public MSG1_Response(Dictionary<byte, object> data) : base(data)
        {

        }
    }
}
