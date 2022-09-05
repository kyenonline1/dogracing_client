using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;
using System.Reflection;

namespace GameProtocol.Protocol
{

    public class RequestBase : MessageBase
    {
        public RequestBase(IRpcProtocol rpcProtocol, OperationRequest operationRequest)
            : base(rpcProtocol, operationRequest)
        {
            
        }

        public RequestBase(IRpcProtocol rpcProtocol, OperationRequest operationRequest, SendParameters sendParameters)
            : base(rpcProtocol, operationRequest, sendParameters)
        {
        }

        public RequestBase(Dictionary<byte, object> dict)
            : base(dict)
        {
            SendParameters = new SendParameters();
        }
        public RequestBase()
            : base()
        {
            SendParameters = new SendParameters();
        }
    }

    //public class JRequestBase : RequestBase, INewMessageBase
    //{

    //}
}
