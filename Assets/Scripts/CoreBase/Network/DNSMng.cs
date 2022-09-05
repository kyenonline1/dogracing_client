using UnityEngine;
using System.Collections;
using System.Text;
using Utilities.Cryptor;
using System;
using Utilities;
using System.Threading;
using System.Collections.Generic;
using Broadcast;
using Newtonsoft.Json.Linq;
using AppConfig;
using Utilities.Custom;
using Base;
using BaseConfig;

namespace Network
{
    public class DNSMng 
    {

        private static readonly string TAG = "DNSHelper";

        // New N-e for P
		public static readonly string N = "0x9815cc1f370084cd8ca4602863e01b0fb02cea5b4c5289c7L";//"0x9b49ecfb82fa33babdd148a054cef0183b73b921f4b99853L";
        public static readonly string e = "0x10001L";//"0x10001";
		public static readonly string d = "0x8a9872a88fab3d5b44ac17a3cc205e79b511731227b48361L";//"0x92f529fd779d5ab91e0be202c17ffb4fbae43dcc5db69661L";

        public static readonly byte[] LOCAL_ENCODE_KEY = Encoding.UTF8.GetBytes("HR3434C3G4G535N3UI3H4J3K5BYU");

        /**
         * khá»Ÿi táº¡o cÃ¡c domain ban Ä‘áº§u Ä‘á»ƒ cache dÆ°á»›i disk, khi release sáº½ ko dÃ¹ng tham sá»‘ nÃ y mÃ  dÃ¹ng tham sá»‘ Ä‘Ã£ Ä‘Æ°á»£c mÃ£ hÃ³a
         */
        private static string DOMAIN_DEFAULT_RAW;
        /**
         * khá»Ÿi táº¡o cÃ¡c domain ban Ä‘áº§u Ä‘á»ƒ cache dÆ°á»›i disk Ä‘Ã£ Ä‘Æ°á»£c mÃ£ hÃ³a
         */
       // private static readonly string DOMAIN_DEFAULT = "Mxw=";

		private static readonly string DNS_STORE_DEFAULT = "E2M4eD97Ah52Kw9bWV4jPggucBdAZSIcRmpULhUiYyF3MVwZbHZbHUEadmkeP3hEAHBsCFR1TSxRNSh/I3oWRm4uHEYbSShoWGMmC00pbBUOJgw2EGB/aTJyRQZ7YAhUBFApIk4peV4HYjIWV2BWbUI/fy5vLQtKI2EUD1YQMWwbDw==";//"E2M4eD97Ah52Kw9bWV4jPggucBdAZSIcRmpULhUiYyF3MVwZb3VYHkIZdWodPHtHA3NvC1d2Ti9SNit8IHkVRW0tH0UYSitrW2AlCE4qbxUOJgw2EGB/aTJyRQZ7YAhUBFApIk4peV4HYjIWV2BWbUI/fy5vLQtKI2EXDFUTMm8YDA==";

        private static readonly string KEY_PREF_HOST = "KEY_PREF_HOST";
        private static readonly string KEY_PREF_PORT = "KEY_PREF_PORT";
        private static readonly string KEY_PREF_DNS_STORE = "KEY_PREF_DNS_STORE";

        private static string[] DNSStore = { };

        internal static void InitHelper()
        {
			Debug.LogError ("InitHelper");
            //PlayerPrefs.DeleteAll();
            string dnsstore = DNSMng.Decode(PlayerPrefs.GetString(KEY_PREF_DNS_STORE, DNS_STORE_DEFAULT));
            JArray jdns = JArray.Parse(dnsstore);
            DNSMng.SetLinkGetDNS(jdns);

            string host = PlayerPrefs.GetString(KEY_PREF_HOST, ServerConfig.HOST_DEFAULT);
            int port = PlayerPrefs.GetInt(KEY_PREF_PORT, ServerConfig.PORT_DEFAULT);
            //TCPClient.Instance.SetServerHost(host, port);
            PhotonClient.Instance.SetServerHost(host, port);

			//doan code tao DNS_STORE_DEFAULT
//			string jsondns = "[\"http://ica123.com/cotuong/server.v2.json\"," +
//				"\"http://ica456.com/cotuong/server.v2.json\"," +
//				"\"http://ica789.com/cotuong/server.v2.json\"]";
//			string encode = DNSMng.Encode(jsondns);
//			LogMng.Log ("DNS", "default dns: " + encode);
        }

        /// <summary>
        /// Ham chi chay trong mainthread
        /// lan luot duyet tung link trong mang DNSStore de lay server host moi, ket qua tra ve bao gom DNSStore moi va server host</br>
        /// can kiem tra ket qua xem da expire chua
        /// </summary>
        /// <param name="counter"></param>
        /// <returns></returns>
        private static bool GetHostFromDNSStore(int index)
        {
            LogMng.Log(TAG, "GetHostFromDNSLink: " + DNSStore.Length + ", " + index);

            //kiem tra neu danh sach link rong hoac da duyet het tat ca cac link thi tra lai ket qua
            if (DNSStore == null || DNSStore.Length == 0 || index >= DNSStore.Length)
                return false;

            //thong bao cho cac listener biet dang chuyen toi server nao
            string strmsg = "Dang ket noi to Server " + (index + 1);
            Dictionary<string, object> msg = new Dictionary<string, object>();
            msg.Add("msg", strmsg);
            BroadcastReceiver.Instance.BroadcastMessage(MessageCode.NETWORK, MessageType.Connecting, msg);

            bool success = DNSMng.GetDNSStoreFromLink(DNSStore[index]);
            LogMng.Log(TAG, "Sucess: " + success);
			if (success){
				Debug.LogError ("DM CLGT");
                return true;
			}
            //duyet link tiep theo trong mang
            return GetHostFromDNSStore(index + 1);
        }

        internal static IEnumerator SocketConnectFailImplRunOnUI(PhotonClient.ServerChangedHandler HandleResult)
        {
            bool success = GetHostFromDNSStore(0);
			Debug.LogError ("SocketConnectFailImplRunOnUI " + success);
            HandleResult(success);
            yield return null;
        }

        /// <summary>
        /// Ham chi chay trong mainthread</br>
        /// lay cau hinh server tu 1 link
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        internal static bool GetDNSStoreFromLink(string link)
        {
            LogMng.Log(TAG, "GetDNSStoreFromLink: " + link);
            // == 1: dang test, xem trong connection.xml
            try
            {
                WWW www = new WWW(link);

                Coroutine c = MonoInstance.Instance.StartCoroutine(HTTPRequest(www));
                lock (www)
                {
                    Monitor.Wait(www, ServerConfig.HTTP_TIME_OUT);
                }

                string data = "{}";
                try
                {
                    if (www.error != null)
                    {
                        LogMng.LogError(TAG, link + " Error: " + www.error.ToString());
                        return false;
                    }

                    data = www.text;
                }
                catch (Exception e)
                {
                    //neu www not yet to download (timeout) thi huy coroutine cu di de start cai moi
                    MonoInstance.Instance.StopCoroutine(c);
                    LogMng.LogException(e);
                    return false;
                }

                LogMng.Log(TAG, "Data of link " + link + ":\n" + data);

                JObject jencode = JObject.Parse(data);
                String listIP = DNSMng.DecodeDNS(jencode);
                if (listIP == null)
                    return false;

                JObject jraw = JObject.Parse(listIP);

                //time in format ("yyyy-MM-dd' 'HH:mm:ss")
                string expire = jraw["expired"].ToObject<string>();
                DateTime de = DateTime.Parse(expire);
                DateTime du = DateTime.Now;

                LogMng.Log(TAG, "Check time: " + de + ", " + du + ", " + (de.CompareTo(du) < 0));

                if (de.CompareTo(du) < 0)
                {
                    return false;
                }

                if (jraw["code"].ToObject<int>() != 0)
                {
                    LogMng.Log(TAG, "has error");
                    //luu msg bao loi vao cache
                    PlayerPrefs.SetString("get_dns_error", jraw["msg"].ToObject<string>());
                    PlayerPrefs.Save();
                    return false;
                }

                //luu link dns va server host moi
                SaveNewServerAddressAndDNSStore(jraw);

                return true;
            }
            catch (Exception e)
            {
                LogMng.LogException(e);
                return false;
            }
        }

        private static IEnumerator HTTPRequest(WWW www)
        {
            yield return www;
            lock (www)
            {
                Monitor.Pulse(www);
            }
        }

        internal static String DecodeDNS(JObject jencode)
        {

            //		String N = "0xc74ce48657a6946b85ae033fcc75e852f583a68af43b8b11L";
            //		String e = "0x10001";

            RSA rsa = new RSA(BigIntFactory.Create(e), BigIntFactory.Create(e), BigIntFactory.Create(N));

            string cipher = jencode["cipher"].ToObject<string>();
            byte[] key = rsa.decrypt(cipher);

            string crypto = jencode["crypto"].ToObject<string>();
            string checksum = Utilities.Utils.Crypto.MD5Hash(crypto).Substring(0, 8);
            // Logger.d(TAG, "checksum: " + checksum);

            string signature = jencode["signature"].ToObject<string>();
            string csum = Encoding.UTF8.GetString(rsa.decrypt(signature));
            // Logger.d(TAG, "csum: " + csum);

            if (csum.Equals(checksum))
            {
                string cryptoDecoded = Encoding.UTF8.GetString(XORCrypto.xor_decrypt(key, Convert.FromBase64String(crypto)));

                LogMng.Log(TAG, "CryptoDecoded: " + cryptoDecoded);
                return cryptoDecoded;
            }
            else
            {
                LogMng.Log(TAG, "csum is not equal checksum");
                return null;
            }
        }

        private static void SaveNewServerAddressAndDNSStore(JObject jraw)
        {
            //JSONArray jlocation = jraw["ip2location"].AsArray;
            //string jlocEnc = EncoderHelper.Encode(jlocation.ToString());

            JArray jdns = jraw["dns"].ToObject<JArray>();
            string jdnsEnc = DNSMng.Encode(jdns.ToString());
            //luu vao cache
            PlayerPrefs.SetString(KEY_PREF_DNS_STORE, jdnsEnc);
            //PhotonClient.Instance.SetLinkGetDNS(jdns);
            DNSMng.SetLinkGetDNS(jdns);

            JObject jdefserver = jraw["server"].ToObject<JObject>();
            string host = jdefserver["ip"].ToObject<string>();
            int port = jdefserver["port"].ToObject<int>();

            //TCPClient.Instance.SetServerHost(host, port);
            PhotonClient.Instance.SetServerHost(host, port);
            //luu vao cache
            PlayerPrefs.SetString(KEY_PREF_HOST, host);
            PlayerPrefs.SetInt(KEY_PREF_PORT, port);

            PlayerPrefs.Save();
			LogMng.Log(TAG, "-----------------Saved UDT5-----------------" + jdnsEnc);
        }

        private static void SetLinkGetDNS(JArray jdns)
        {
            LogMng.Log(TAG, "setLinkGetDNS: " + jdns.ToString());
            DNSStore = new string[jdns.Count];
            for (int i = 0; i < DNSStore.Length; i++)
                DNSStore[i] = jdns[i].ToObject<string>();
        }

        internal static string Encode(string raw)
        {
            return Convert.ToBase64String(XORCrypto.xor_encrypt(LOCAL_ENCODE_KEY, Encoding.UTF8.GetBytes(raw)));
        }

        internal static string Decode(String encode)
        {
            return Encoding.UTF8.GetString(XORCrypto.xor_decrypt(LOCAL_ENCODE_KEY, Convert.FromBase64String(encode)));
        }
    }
}
