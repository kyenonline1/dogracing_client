using GameProtocol.Base;
using GameProtocol.Protocol;
using System.Collections.Generic;

namespace GameProtocol.DOG
{
    public class DOG1LeaveGameResponse : ResponseBase
    {
        private Dictionary<byte, object> data;

        public override void SetCodeRun()
        {
            CodeRun = "DOG1";
        }

        public DOG1LeaveGameResponse()
        {
            Flag = 0;
        }

        public DOG1LeaveGameResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
