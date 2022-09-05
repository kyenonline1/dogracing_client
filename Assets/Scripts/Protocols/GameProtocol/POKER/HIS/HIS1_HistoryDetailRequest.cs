using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.HIS
{
    public class HIS1_HistoryDetailRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "HIS1";
        }

        public HIS1_HistoryDetailRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public HIS1_HistoryDetailRequest()
        {
        }

        [DataMember(Code = (byte)HIS_ParameterCode.TableId, IsOptional = true)]
        public int TableId { get; set; }

        [DataMember(Code = (byte)HIS_ParameterCode.Gamesession, IsOptional = true)]
        public int Gamesession { get; set; }

        [DataMember(Code = (byte)HIS_ParameterCode.GameId, IsOptional = true)]
        public string GameId { get; set; }
    }
}