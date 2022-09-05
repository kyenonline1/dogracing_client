using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.PKR
{
    public class PKRCashInRequest : RequestBase
    {

        public override void SetCodeRun()
        {
            CodeRun = "PKR0";
        }

        [DataMember(Code = (byte)PKR_ParameterCode.CashIn, IsOptional = true)]
        public long CashIn { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Cate, IsOptional = true)]
        public byte Cate { get; set; } // 0 Normal, 1 : Rebuy, 2 : AddOn

        public PKRCashInRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PKRCashInRequest()
        {
        }
    }
}
