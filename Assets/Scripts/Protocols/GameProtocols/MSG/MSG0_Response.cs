
using GameProtocol.Protocol;

namespace GameProtocol.MSG
{
    public class MSG0_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MSG0";
        }
    }
}
