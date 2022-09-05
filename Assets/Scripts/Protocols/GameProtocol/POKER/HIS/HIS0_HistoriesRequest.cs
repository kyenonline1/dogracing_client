using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.HIS
{
    public class HIS0_HistoriesRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "HIS0";
        }

        public HIS0_HistoriesRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public HIS0_HistoriesRequest()
        {
        }


        [DataMember(Code = (byte)HIS_ParameterCode.IsSave, IsOptional = true)]
        public bool IsSave { get; set; }


        [DataMember(Code = (byte)HIS_ParameterCode.CurrentPage, IsOptional = true)]
        public byte CurrentPage { get; set; }
    }
}