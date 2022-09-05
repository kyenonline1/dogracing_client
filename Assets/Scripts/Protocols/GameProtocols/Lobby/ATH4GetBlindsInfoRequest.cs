using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using System;

namespace GameProtocol.ATH
{
    public class ATH4GetBlindsInfoRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH4";
        }

        public ATH4GetBlindsInfoRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public ATH4GetBlindsInfoRequest()
        {
        }
    }
}
