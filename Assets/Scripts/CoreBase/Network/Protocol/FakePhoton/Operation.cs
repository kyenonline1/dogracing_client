using System;
using System.Text;

namespace Photon.SocketServer.Rpc {
    public class Operation {
        public Operation(IRpcProtocol rpcProtocol, OperationRequest operationRequest) {
            throw new InvalidOperationException("This method only available on server side\n Use Operation(Dictionary) instead!");
        }

        public Operation() {

        }
    }
}
