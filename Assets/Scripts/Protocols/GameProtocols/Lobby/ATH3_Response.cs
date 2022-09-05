using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.ATH
{
    public class ATH3_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH3";
        }

        public ATH3_Response(Dictionary<byte, object> dict) : base(dict)
        {

        }
    }
}
