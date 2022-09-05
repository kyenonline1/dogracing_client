
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System;

namespace GameProtocol.ANN
{
    public class ANN1_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ANN1";
        }

        [DataMember(Code = (byte)ANN_ParameterCode.AnnouneId, IsOptional = true)]
        public int AnnouneId { get; set; }

        [DataMember(Code = (byte)ANN_ParameterCode.Type, IsOptional = true)]
        public byte Type { get; set; }// 0 read, 1 delete

        public ANN1_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public ANN1_Request()
        {
        }
    }
}
