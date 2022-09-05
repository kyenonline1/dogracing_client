using UnityEngine;
using Listener;
using GameProtocol.Protocol;
using System;

using System.Reflection;
using ExitGames.Client.Photon;

namespace Network
{
	public class Network
	{

		public static void StartNetwork ()
		{
            //DNSMng.InitHelper();
            if (PhotonClient.Instance != null)
                PhotonClient.Instance.StartPhotonClient();
            DataProcessor.Instance.StartProcessor();
		}

        public static void StopNetwork()
        {
            if(PhotonClient.Instance != null)
                PhotonClient.Instance.StopPhotonClient();
        }

        public static bool RegisterPhotonObject(Type customType, byte code, SerializeMethod serializeMethod, DeserializeMethod deserializeMethod)
        {
            return PhotonClient.Instance.RegisterObject(customType, code, serializeMethod, deserializeMethod);
        }

#if UNITY_EDITOR
        static string TempPhotonObjects = string.Empty, TempPhotonFailObjects = string.Empty;
        public static string GetPhotonObjectLog(bool success)
        {
            if (success) return TempPhotonObjects;
            return TempPhotonFailObjects;
        }
#endif

//        public static void RegisterPhotonObject(Type customType)
//        {
//            MethodInfo m = customType.GetMethod("GetByteRegister");
//            bool reg_success = Network.RegisterPhotonObject(customType, (byte)m.Invoke(null, null), (SerializeMethod)Delegate.CreateDelegate(typeof(SerializeMethod), customType.GetMethod("Serialize")), (DeserializeMethod)Delegate.CreateDelegate(typeof(DeserializeMethod), customType.GetMethod("Deserialize")));
//#if UNITY_EDITOR
//            if (reg_success)
//            {
//                TempPhotonObjects += customType.ToString() + "&" + (byte)m.Invoke(null, null) + ", ";
//            }
//            else {
//                TempPhotonFailObjects += customType.ToString() + "&" + (byte)m.Invoke(null, null) + ", ";
//            }
//#endif
//        }

        public static void SendOperation (MessageBase operation, DataListener listener)
		{
			operation.Flag = Packet.FLAG_ACTIVE;

			PhotonClient.Instance.SendOperationPT (operation, listener);
		}

        public static bool IsNetworkConnected()
        {
            return PhotonClient.Instance.Is_Connected;
        }

        public static bool IsNetworkReady()
        {
            if (PhotonClient.Instance == null) return false;
            return PhotonClient.Instance.IsReady;
        }

        public static void SetNetworkHardly(Config config)
        {
            if(PhotonClient.Instance != null)
                PhotonClient.Instance.SetNetworkConfig(config);
        }

		public static void AddSequenceCode(string code){
            if (DataProcessor.Instance != null)
                DataProcessor.Instance.AddSequenceCode (code);
		}
        public static void RemoveSequenceCode(string code)
        {
            if (DataProcessor.Instance != null)
                DataProcessor.Instance.RemoveSequenceCode(code);
        }

        public static void ClearSequenceCoderun(string coderun)
        {
            if (DataProcessor.Instance != null)
                DataProcessor.Instance.RemoveQueueByCodeRun(coderun);
        }
        
    }
}