
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System;

namespace GameProtocol.ANN
{
    public class ANN0_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ANN0";
        }
        [DataMember(Code = (byte)ANN_ParameterCode.Type, IsOptional = true)]
        public short Type { get; set; }

        [DataMember(Code = (byte)ANN_ParameterCode.ClubId, IsOptional = true)]
        public int ClubId { get; set; }

        public ANN0_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public ANN0_Request()
        {
        }
    }
}
