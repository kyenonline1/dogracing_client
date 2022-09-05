using GameProtocol.Base;
using GameProtocol.Protocol;

namespace GameProtocol.XIT
{
    public class XITOLeaveTableResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "SBI_XIT6";
        }
    }
}
