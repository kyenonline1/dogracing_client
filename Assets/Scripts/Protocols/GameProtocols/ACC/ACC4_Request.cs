

using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.ACC
{
    public class ACC4_Request : RequestBase
    {

        public override void SetCodeRun()
        {
            CodeRun = "ACC4";
        }

        public ACC4_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public ACC4_Request()
        {
        }

        [DataMember(Code = (byte)ACC_ParameterCode.Gold, IsOptional = true)]
        public long Gold { get; set; }
        [DataMember(Code = (byte)ACC_ParameterCode.OTP, IsOptional = true)]
        public int OTP { get; set; }
    }
}
