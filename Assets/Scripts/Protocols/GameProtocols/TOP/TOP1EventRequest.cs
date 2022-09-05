using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.TOP
{
    public class TOP1EventRequest : RequestBase
    {
        
        public override void SetCodeRun()
        {
            CodeRun = "TOP1";
        }

        [DataMember(Code = (byte)TOP_ParameterCode.Seasons, IsOptional = true)]
        public string Season { get; set; }

        public TOP1EventRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public TOP1EventRequest()
        {
        }
    }
}
