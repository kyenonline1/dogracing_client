

using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.ANN
{
    public class ANN2_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ANN2";
        }

        public ANN2_Response(Dictionary<byte, object> data) : base(data)
        {

        }
    }
}
