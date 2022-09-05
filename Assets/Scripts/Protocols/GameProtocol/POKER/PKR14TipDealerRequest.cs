using GameProtocol.Protocol;
using Photon.SocketServer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProtocol.PKR
{
    public class PKR14TipDealerRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR14";
        }

        public PKR14TipDealerRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PKR14TipDealerRequest()
        {
        }
    }
}