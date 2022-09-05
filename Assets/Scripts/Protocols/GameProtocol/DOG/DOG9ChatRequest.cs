using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.DOG
{
    public class DOG9_ChatRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "AIG9";
        }

        public DOG9_ChatRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public DOG9_ChatRequest()
        {
        }

        [DataMember(Code = (byte)AIG_ParameterCode.Message, IsOptional = true)]
        public string Message { get; set; }
    }
}
