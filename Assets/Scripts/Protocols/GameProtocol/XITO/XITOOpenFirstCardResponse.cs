using GameProtocol.Base;
using GameProtocol.Protocol;

namespace GameProtocol.XIT
{
    public class XITOOpenFirstCardResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "XIT1";
        }
    }
}
