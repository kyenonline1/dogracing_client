using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.PKR
{
    public class PKR20BuyInsuranceRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR20";
        }

        public PKR20BuyInsuranceRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PKR20BuyInsuranceRequest()
        {
        }

        [DataMember(Code = (byte)PKR_ParameterCode.Gold, IsOptional = true)]
        public long Gold { get; set; }
    }
}