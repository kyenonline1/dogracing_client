using System;
using System.Collections.Generic;
using System.Text;

namespace Photon.SocketServer.Rpc {
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DataMember : Attribute {
        public string Name { get; set; }
        public bool IsOptional { get; set; }
        public byte Code { get; set; }

    }
}
