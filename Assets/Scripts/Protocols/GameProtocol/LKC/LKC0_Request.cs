using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.LKC
{
    /// <summary>
    /// Lấy thông tin Lucky Card hiện tại theo mức cược
    /// </summary>
    public class LKC0_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "LKC0";
        }

        [DataMember(Code = (byte)LKC_ParameterCode.Blind, IsOptional = true)]
        public int Blind { get; set; }

        public LKC0_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public LKC0_Request()
        {
        }
    }
}
