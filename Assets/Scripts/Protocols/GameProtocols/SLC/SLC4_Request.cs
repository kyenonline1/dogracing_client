
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.SLC
{
    public class SLC4_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "SLC4";
        }
        [DataMember(Code = (byte)SLC_ParameterCode.GameId, IsOptional = true)]
        public string GameId { get; set; }

        public SLC4_Request(IRpcProtocol protocol, OperationRequest operationRequest) :
            base(protocol, operationRequest)
        {
        }

        public SLC4_Request()
        {
        }
    }
}
