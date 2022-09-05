using GameProtocol.Base;
using GameProtocol.Protocol;
using System;
using System.Collections.Generic;

namespace GameProtocol.ATH
{
    public class ATH2_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH2";
        }

        public ATH2_Response(Dictionary<byte, object> dict) : base(dict)
        {

        }
    }
}
