using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.MAU
{
    public class MAUCancelSortCardResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU14";
        }

        public MAUCancelSortCardResponse()
        {

        }

        public MAUCancelSortCardResponse(Dictionary<byte, object> data) : base(data)
        {

        }
    }
}
