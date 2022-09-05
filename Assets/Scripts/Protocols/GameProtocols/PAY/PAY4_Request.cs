
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.PAY
{

    public class PAY4_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PAY4";
        }

        [DataMember(Code = (byte)PAY_ParameterCode.SignedData, IsOptional = true)]
        public string SignedData { get; set; }

        [DataMember(Code = (byte)PAY_ParameterCode.Signature, IsOptional = true)]
        public string Signature { get; set; }

        public PAY4_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PAY4_Request()
        {
        }
    }
}
