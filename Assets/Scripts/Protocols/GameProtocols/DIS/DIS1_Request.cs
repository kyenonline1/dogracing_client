using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System;

namespace GameProtocol.DIS
{
    public class DIS1_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DIS1";
        }
        [DataMember(Code = (byte)DIS_ParameterCode.Nicknames, IsOptional = true)]
        public string[] Nicknames { get; set; }

        [DataMember(Code = (byte)DIS_ParameterCode.Gold, IsOptional = true)]
        public int[] Golds { get; set; }

        [DataMember(Code = (byte)DIS_ParameterCode.Capcha, IsOptional = true)]
        public string Capcha { get; set; }

        [DataMember(Code = (byte)DIS_ParameterCode.Reasons, IsOptional = true)]
        public string[] Reason { get; set; }


        public DIS1_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public DIS1_Request()
        {
        }
    }
}
