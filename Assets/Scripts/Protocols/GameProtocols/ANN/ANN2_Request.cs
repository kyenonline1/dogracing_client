
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.ANN
{
    public class ANN2_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ANN2";
        }
        [DataMember(Code = (byte)ANN_ParameterCode.Title, IsOptional = true)]
        public string Title { get; set; }

        [DataMember(Code = (byte)ANN_ParameterCode.Content, IsOptional = true)]
        public string Content { get; set; }

        [DataMember(Code = (byte)ANN_ParameterCode.UserReceive, IsOptional = true)]
        public string UserReceive { get; set; }

        public ANN2_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public ANN2_Request()
        {
        }
    }
}
