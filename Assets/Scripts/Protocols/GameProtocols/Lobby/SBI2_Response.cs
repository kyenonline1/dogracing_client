using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.ATH
{
    public class SBI2_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "SBI_ATH2";
        }

        public SBI2_Response(Dictionary<byte, object> data) : base(data)
        {

        }
    }
}
