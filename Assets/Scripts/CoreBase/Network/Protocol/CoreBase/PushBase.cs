using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProtocol.Protocol
{
    public class PushBase: MessageBase
    {
        public PushBase(IRpcProtocol rpcProtocol, OperationRequest operationRequest, SendParameters sendParameters)
            : base(rpcProtocol, operationRequest, sendParameters)
        {
            Flag = 1;
        }

        public PushBase(IRpcProtocol rpcProtocol, OperationRequest operationRequest)
            : base(rpcProtocol, operationRequest) 
        {
            Flag = 1;
        }
        public PushBase()
            : base() 
        {
            Flag = 1;
        }

        public PushBase(Dictionary<byte, object> dict):base(dict)
        {
            Flag = 1;
        }
    }

    //public class JPushBase : PushBase, INewMessageBase
    //{

    //}
}
