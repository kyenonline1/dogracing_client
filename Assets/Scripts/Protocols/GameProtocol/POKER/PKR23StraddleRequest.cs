using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System;

namespace GameProtocol.PKR
{
    public class PKR23StraddleRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR23";
        }
        [DataMember(Code = (byte)PKR_ParameterCode.CashBet, IsOptional = true)]
        public long CashBet { get; set; } //-1 : Fail

        public PKR23StraddleRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PKR23StraddleRequest()
        {
        }
    }
}
