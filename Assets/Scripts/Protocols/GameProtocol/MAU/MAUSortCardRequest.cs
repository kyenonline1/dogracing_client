using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.MAU
{
    public class MAUSortCardRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU11";
        }

        [DataMember(Code = (byte)MAU_ParameterCode.HandCard, IsOptional = true)]
        public int[] HandCard { get; set; }

        public MAUSortCardRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public MAUSortCardRequest()
        {
        }
    }
}
