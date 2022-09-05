
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.PAY
{
    public class PAY0_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PAY0";
        }

        //0:IAP, 1 : Card
        [DataMember(Code = (byte)PAY_ParameterCode.Cate, IsOptional = true)]
        public short Cate { get; set; }

        public PAY0_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PAY0_Request()
        {
        }
    }
}
