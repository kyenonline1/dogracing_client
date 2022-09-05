using GameProtocol.Base;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;
using System.Reflection;

namespace GameProtocol.Protocol
{

    public class ResponseBase : MessageBase {
        public ResponseBase(IRpcProtocol rpcProtocol, OperationRequest operationRequest, SendParameters sendParameters)
            : base(rpcProtocol, operationRequest, sendParameters)
        {
            Flag = 0;
        }

        public ResponseBase(IRpcProtocol rpcProtocol, OperationRequest operationRequest)
            : base(rpcProtocol, operationRequest)
        {
            Flag = 0;
        }
        public ResponseBase(Dictionary<byte, object> dict)
            : base(dict) 
        {
            Flag = 0;
        }
        public ResponseBase()
            : base() 
        {
            Flag = 0;
        }


        [DataMember(Code = (byte)ParameterCode.ErrorCode, IsOptional = true)]
        public short ErrorCode { get; set; }



//#if SERVER
        private string errorMsg;

        [DataMember(Code = (byte)ParameterCode.ErrorMsg, IsOptional = true)]
        public string ErrorMsg { 
            get{return errorMsg;} 
            set{
                //chỉ dùng một trong hai biến là ErrorMsg hoặc là TransErrorMsg
                //do đó khi gán biến này thì phải xóa biến kia
                transErrorMsg = null;
                errorMsg = value;
            } 
        }

        private TransString transErrorMsg;

        public TransString TransErrorMsg { 
            get { return transErrorMsg; } 
            set {
                //chỉ dùng một trong hai biến là ErrorMsg hoặc là TransErrorMsg
                //do đó khi gán biến này thì phải xóa biến kia
                errorMsg = null;
                transErrorMsg = value;
            } 
        }
//#elif CLIENT
//        [DataMember(Code = (byte)ParameterCode.ErrorMsg, IsOptional = true)]
//        public string ErrorMsg { get;set;}
//#endif
    }


    //public class JResponseBase : ResponseBase, INewMessageBase
    //{

    //}
}
