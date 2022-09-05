using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System;

namespace GameProtocol.ATH
{
    public class ATH5_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH5";
        }
        [DataMember(Code = (byte)ATH_ParameterCode.FacebookId, IsOptional = true)]
        public string FacebookID { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.Fullname, IsOptional = true)]
        public string Fullname { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.Firstname, IsOptional = true)]
        public string Firstname { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.Lastname, IsOptional = true)]
        public string Lastname { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.Avatar, IsOptional = true)]
        public string Avatar { get; set; }

        public ATH5_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public ATH5_Request()
        {
        }
    }
}
