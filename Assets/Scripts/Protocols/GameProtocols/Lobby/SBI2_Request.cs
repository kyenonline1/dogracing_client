using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System;

namespace GameProtocol.ATH
{
    public class SBI2_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "SBI_ATH2";
        }

[       DataMember(Code = (byte)ATH_ParameterCode.TableId, IsOptional = true)]
        public long TableId { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.GameId, IsOptional = true)]
        public string GameID { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.Blind, IsOptional = true)]
        public int Blind { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.CashType, IsOptional = true)]
        public byte CashType { get; set; }

        public SBI2_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public SBI2_Request()
        {
        }
    }
}
