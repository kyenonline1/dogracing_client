using GameProtocol.Protocol;
using Photon.SocketServer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProtocol.MOM
{
    public class MOM0_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MOM0";
        }
        public MOM0_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public MOM0_Request()
        {

        }
    }
}
