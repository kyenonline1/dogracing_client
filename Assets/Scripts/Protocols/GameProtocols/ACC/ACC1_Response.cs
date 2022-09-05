
using GameProtocol.Protocol;
using System;
using System.Collections.Generic;

namespace GameProtocol.ACC
{
    [Serializable]
    public class ACC1_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ACC1";
        }
        public ACC1_Response(Dictionary<byte, object> data) : base(data)
        {
        }
    }
}
