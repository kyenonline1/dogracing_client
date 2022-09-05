
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.MSG
{
    public class MSG0_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MSG0";
        }

        [DataMember(Code = (byte)MSG_ParameterCode.Message, IsOptional = true)]
        public string Message { get; set; }

        [DataMember(Code = (byte)MSG_ParameterCode.Type, IsOptional = true)]
        public byte Type { get; set; }

        public MSG0_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public MSG0_Request()
        {
        }
    }
}
