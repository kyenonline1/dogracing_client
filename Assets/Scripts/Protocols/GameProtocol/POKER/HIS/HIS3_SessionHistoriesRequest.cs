using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.HIS
{
    public class HIS3_SessionHistoriesRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "HIS3";
        }

        public HIS3_SessionHistoriesRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public HIS3_SessionHistoriesRequest()
        {
        }
        
    }
}