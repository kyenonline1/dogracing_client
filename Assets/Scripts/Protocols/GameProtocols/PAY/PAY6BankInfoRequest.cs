using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.PAY
{
    public class PAY6BankInfoRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PAY6";
        }
        
        public PAY6BankInfoRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PAY6BankInfoRequest()
        {
        }
    }
}
