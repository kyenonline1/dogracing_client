using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.PKR
{
    public class PKR2SeatDownRequest : RequestBase
    {

        public override void SetCodeRun()
        {
            CodeRun = "PKR2";
        }

        [DataMember(Code = (byte)PKR_ParameterCode.Position, IsOptional = true)]
        public int Position { get; set; }

        public PKR2SeatDownRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PKR2SeatDownRequest()
        {
        }
    }
}
