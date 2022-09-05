using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseConfig
{
	public class ServerConfig {
		public static string Host;
		public static int Port;
       
        static ServerConfig()
		{
#if SERVER_DEV
			Host = "140.82.23.114";
#else
            Host = ""; //"35.220.249.57";//
#endif
#if WS
            Port = 63;
#else
            Port = 62;
#endif
		}

        public static readonly string HOST_DEFAULT_V6 = "[2001:19f0:4400:4211:5400:ff:fe3a:8e00]";


        //public static string HOST_DEFAULT = "149.28.159.188";//"35.240.134.28";//  "35.220.155.105";// 
        public static string HOST_DEFAULT = "140.82.23.114";

        public static int WEB_PORT_DEFAULT = 9090;
        public static int PORT_DEFAULT = 4530;
        public static string NAME_DEFAULT { get; set; }

        public static string GAMESERVER_HOST = string.Empty;
        public static int GAMESERVER_PORT = 0;


        public static readonly int PING_TIME = 10000;
        public static readonly int HTTP_TIME_OUT = 5000;
        public static readonly int MAX_CONNECT_RETRY_TIMES = 5;


        public static int PNG0_REPEAT_TIME = 6;
        public static readonly int ATH8_UPDATE_JACKPOT_TIME = 10;

    }

}
