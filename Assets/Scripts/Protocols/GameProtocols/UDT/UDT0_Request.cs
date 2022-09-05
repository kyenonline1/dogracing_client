
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System;

namespace GameProtocol.UDT
{
    [Serializable]
    public class UDT0_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "UDT0";
        }
        [DataMember(Code = (byte)UDT_ParameterCode.DeviceID, IsOptional = true)]
        public string DeviceID { get; set; }

        [DataMember(Code = (byte)UDT_ParameterCode.Platform, IsOptional = true)]
        public string Platform { get; set; }

        [DataMember(Code = (byte)UDT_ParameterCode.Version, IsOptional = true)]
        public string Version { get; set; }

        [DataMember(Code = (byte)UDT_ParameterCode.Language, IsOptional = true)]
        public string Language { get; set; }

        [DataMember(Code = (byte)UDT_ParameterCode.Dbtor, IsOptional = true)]
        public string Dbtor { get; set; }

        public UDT0_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public UDT0_Request()
        {
        }
    }
}
