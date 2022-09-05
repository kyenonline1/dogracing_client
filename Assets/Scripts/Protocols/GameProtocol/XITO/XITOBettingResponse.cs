using GameProtocol.Base;
using GameProtocol.Protocol;

namespace GameProtocol.XIT
{
    public class XITOBettingResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "XIT5";
        }
    }
}
