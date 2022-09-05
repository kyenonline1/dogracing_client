
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.MSG
{
    public class MSG2_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MSG2";
        }

        [DataMember(Code = (byte)MSG_ParameterCode.Type, IsOptional = true)]
        public byte Type { get; set; }

        public MSG2_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public MSG2_Request()
        {
        }
    }
}
