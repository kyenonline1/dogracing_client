using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.MAU
{
    public class MAUEndGamePush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU13";
        }

        public MAUEndGamePush()
        {

        }

        public MAUEndGamePush(Dictionary<byte, object> data) : base(data)
        {

        }


        [DataMember(Code = (byte)MAU_ParameterCode.PlayerCards, IsOptional = true)]
        public MauBinhPlayerCompare[] PlayerCards { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.PlayerUnits, IsOptional = true)]
        public MauBinhPlayerUnit[] PlayerUnits { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.Tinh_at, IsOptional = true)]
        public MauBinhPlayerCompare[] Tinh_at { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.Sap_ham, IsOptional = true)]
        public MauBinhPlayerCompare[] Sap_ham { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.SapLang, IsOptional = true)]
        public MauBinhPlayerCompare[] SapLang { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.Summary, IsOptional = true)]
        public MauBinhPlayerSummary[] Summary { get; set; }
    }
    
}
