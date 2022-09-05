using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.COU
{
    public class COU1CashoutRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "COU1";
        }
        [DataMember(Code = (byte)COU_ParameterCode.Email, IsOptional = true)]
        public string Email { get; set; }

        [DataMember(Code = (byte)COU_ParameterCode.Gold, IsOptional = true)]
        public int Amount { get; set; }
        
        [DataMember(Code = (byte)COU_ParameterCode.FirtName, IsOptional = true)]
        public string FirtName { get; set; }

        [DataMember(Code = (byte)COU_ParameterCode.LastName, IsOptional = true)]
        public string LastName { get; set; }

        public COU1CashoutRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public COU1CashoutRequest()
        {
        }
    }
}
