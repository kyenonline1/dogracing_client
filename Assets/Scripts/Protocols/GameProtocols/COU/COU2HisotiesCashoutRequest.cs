using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.COU
{
    public class COU2HisotiesCashoutRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "COU2";
        }
        public COU2HisotiesCashoutRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public COU2HisotiesCashoutRequest()
        {
        }
    }
}
