using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProtocol.Protocol
{
    public class PushMessage: MessageBase
    {
        public PushMessage(IRpcProtocol rpcProtocol, OperationRequest operationRequest, SendParameters sendParameters)
            : base(rpcProtocol, operationRequest, sendParameters)
        {
            Flag = 1;
        }

        public PushMessage(IRpcProtocol rpcProtocol, OperationRequest operationRequest)
            : base(rpcProtocol, operationRequest) 
        {
            Flag = 1;
        }
        public PushMessage()
            : base() 
        {
            Flag = 1;
        }

        public PushMessage(Dictionary<byte, object> dict):base(dict)
        {
            Flag = 1;
        }
    }
}
