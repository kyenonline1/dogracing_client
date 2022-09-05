using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.XIT
{
    public class XITOOpenFirstCardRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "XIT1";
        }
        [DataMember(Code = (byte)XIT_ParameterCode.Mask, IsOptional = true)]
        public int Mask { get; set; }

        public XITOOpenFirstCardRequest(IRpcProtocol protocol, OperationRequest operationRequest) :
            base(protocol, operationRequest)
        {
        }

        public XITOOpenFirstCardRequest()
        {
        }
    }
}
