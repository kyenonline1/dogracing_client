using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.EVN
{
    public class EVN8MissionsRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "EVN8";
        }

        [DataMember(Code = (byte)EVN_ParameterCode.Cate, IsOptional = false)]
        public int Cate { get; set; }//0 : DailyMission, 1 : Once Only

        public EVN8MissionsRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public EVN8MissionsRequest()
        {
        }
    }
}
