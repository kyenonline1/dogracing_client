using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.PNG
{
    public class PNG0_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PNG0";
        }

        public PNG0_Response(string coderun, Dictionary<byte, object> data) : base(data)
        {

        }
    }
}
