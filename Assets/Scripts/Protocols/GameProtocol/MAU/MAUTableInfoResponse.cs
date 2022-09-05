using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.MAU
{
    public class MAUTableInfoResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU4";
        }

        public MAUTableInfoResponse()
        {

        }

        public MAUTableInfoResponse(Dictionary<byte, object> data) : base(data)
        {

        }

        [DataMember(Code = (byte)MAU_ParameterCode.Blind, IsOptional = true)]
        public int Blind { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.TableState, IsOptional = true)]
        public int TableState { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.TimeRemain, IsOptional = true)]
        public int TimeRemain { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.TableId, IsOptional = true)]
        public long TableID { get; set; }
        
        [DataMember(Code = (byte)MAU_ParameterCode.TimeConfigs, IsOptional = true)]
        public MauBinhTimeConfig TimeConfigs { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.Players, IsOptional = true)]
        public MauBinhPlayer[] Players { get; set; }
    }
    
}
