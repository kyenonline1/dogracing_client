
using GameProtocol.Protocol;
using System;
using System.Collections.Generic;

namespace GameProtocol.ANN
{
    public class ANN1_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ANN1";
        }

        public ANN1_Response(Dictionary<byte, object> data) : base(data)
        {

        }
    }
}
