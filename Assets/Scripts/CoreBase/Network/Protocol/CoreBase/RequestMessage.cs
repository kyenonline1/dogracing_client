using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;
using System.Reflection;

namespace GameProtocol.Protocol
{

    public class RequestMessage : MessageBase
    {
        public RequestMessage(IRpcProtocol rpcProtocol, OperationRequest operationRequest)
            : base(rpcProtocol, operationRequest)
        {
            
        }

        public RequestMessage(IRpcProtocol rpcProtocol, OperationRequest operationRequest, SendParameters sendParameters)
            : base(rpcProtocol, operationRequest, sendParameters)
        {
        }

        public RequestMessage(Dictionary<byte, object> dict)
            : base(dict)
        {
            SendParameters = new SendParameters();
        }
        public RequestMessage()
            : base()
        {
            SendParameters = new SendParameters();
        }
    }

}
