using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.HIS
{
    public class HIS2_SaveHistoryRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "HIS2";
        }

        public HIS2_SaveHistoryRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public HIS2_SaveHistoryRequest()
        {
        }

        [DataMember(Code = (byte)HIS_ParameterCode.TableId, IsOptional = true)]
        public int TableId { get; set; }

        [DataMember(Code = (byte)HIS_ParameterCode.Gamesession, IsOptional = true)]
        public int Gamesession { get; set; }

        [DataMember(Code = (byte)HIS_ParameterCode.IsSave, IsOptional = true)]
        public bool IsSave { get; set; }
        ///// <summary>
        ///// Xóa nó khỏi bộ sưu tập
        ///// </summary>
        //[DataMember(Code = (byte)PKR_ParameterCode.IsDelete, IsOptional = true)]
        //public bool IsDelete { get; set; }
    }
}