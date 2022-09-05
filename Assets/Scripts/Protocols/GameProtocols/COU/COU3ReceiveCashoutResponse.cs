using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.COU
{
    public class COU3ReceiveCashoutResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "COU3";
        }

        [DataMember(Code = (byte)COU_ParameterCode.Seri, IsOptional = true)]
        public string Seri { get; set; }

        [DataMember(Code = (byte)COU_ParameterCode.NumberCard, IsOptional = true)]
        public string NumberCard { get; set; }

        public COU3ReceiveCashoutResponse(Dictionary<byte, object> data) : base(data)
        {

        }

        public COU3ReceiveCashoutResponse(IRpcProtocol rpcProtocol, OperationRequest operationRequest) : base(rpcProtocol, operationRequest)
        {
        }

        public COU3ReceiveCashoutResponse()
        {
        }
    }
    
}
