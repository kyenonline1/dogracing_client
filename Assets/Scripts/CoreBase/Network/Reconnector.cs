using UnityEngine;
using System.Collections;
using AppConfig;
using Listener;
using Broadcast;
using System.Collections.Generic;
using GameProtocol.Protocol;
using GameProtocol.UDT;
//using GameProtocol.LGI;
using Utilities.Custom;
//using GameProtocol.Lobby;
using Game.Gameconfig;
using GameProtocol.Base;
using GameProtocol.ATH;
//using GameProtocol.Lobby;

namespace Network
{
    internal class Reconnector
    {
        
        internal static bool CheckReconnect()
        {
            //string oldsession;
            //Debug.Log("CheckReconnect, SESSION: " + ClientConfig.UserInfo.SESSION);
            //Debug.Log("CheckReconnect, IS_LOGGED_IN: " + ClientConfig.UserInfo.IS_LOGGED_IN);
            if (!string.IsNullOrEmpty(ClientConfig.UserInfo.SESSION))
            {
                //send udt
                //Debug.Log("Check Reconnet: ");
                BroadcastReceiver.Instance.BroadcastMessage(MessageCode.APP, MessageType.CheckUpdate, null);
                DataListener udt0listener = new DataListener(UDT0HandleUpdate);
                UDT0_Request udt0request = new UDT0_Request()
                {
                    //CellId = "",
                    Dbtor = ClientConfig.SoftWare.DBTOR,
                    DeviceID = ClientConfig.HardWare.IMEI,
                    Language = Languages.Language.LANG == Languages.Languages.en ? "en" : "vn",
                    Platform = ClientConfig.HardWare.PLATFORM,
                    Version = ClientConfig.SoftWare.VERSION,
                    SenderId = udt0listener.GetHashCode(),
                    
                };

				Network.SendOperation(udt0request, udt0listener);
                return true;
            }

            return false;
        }

        private static IEnumerator UDT0HandleUpdate(string coderun, Dictionary<byte, object> data)
        {
			UDT0_Response response = new UDT0_Response(data);

            if (response.ErrorCode == (short)ReturnCode.OK) {
                //ClientConfig.UserInfo.IS_ACTIVE_EXCHANGE = response.IsExChange == 1;
                //ClientConfig.UserInfo.IS_ACTIVE_AGENCY = response.IsDistributor == 1;
                //ClientConfig.UserInfo.IS_ACTIVE_VIPKON = !string.IsNullOrEmpty(response.VipKonUrl);
                ClientGameConfig.APPFUNCTION.UrlFanpage = response.FacebookUrl;
                ClientGameConfig.APPFUNCTION.UrlFanpage = response.FacebookUrl;
                ClientGameConfig.APPFUNCTION.UrlMessenger = response.MessengerUrl;
                ClientGameConfig.APPFUNCTION.UrlTelegram = response.TelegramUrl;
                ClientGameConfig.APPFUNCTION.UrlTelegramBOT = response.TelegramBotUrl;
                ClientGameConfig.APPFUNCTION.Hotline = response.Hotline;
                if (string.IsNullOrEmpty(ClientConfig.UserInfo.UNAME) || string.IsNullOrEmpty(ClientConfig.UserInfo.PASSWORD))
                {
                   // BroadcastReceiver.Instance.BroadcastMessage(MessageCode.NETWORK, MessageType.LostSession, response);
                    yield break;
                }
                if (!string.IsNullOrEmpty(response.BundleUrl))
                {
                    string[] str = response.BundleUrl.Split('|');
                    Debug.Log("RECONECTOR: " + str.Length);
                    if (str != null && str.Length >= 6)
                    {
                        ClientGameConfig.APPFUNCTION.UrlMessenger = str[5];
                    }
                }
                DataListener listener = new DataListener(ATH0HandleReconect);
                ATH0_Request request = new ATH0_Request()
                {
                    Username = ClientConfig.UserInfo.UNAME,
                    Password = ClientConfig.UserInfo.PASSWORD,
                    Session = ClientConfig.UserInfo.SESSION,
                    SenderId = listener.GetHashCode()
                };
                Network.SendOperation(request, listener);
                //DataDispatcher.Instance ().SetExtras ("MainGame").PutExtra("APP_FUNCTION",response.IsAppFullFunction).PutExtra("APP_TRYAL",response.ShowTrial).PutExtra("APP_SHOW_CUSTOM_SERVICE", response.IsShowCustomerService);
                //DataDispatcher.Instance().SetExtras("Splash").PutExtra("BUNDLE_URL", response.BundleUrl);
            }
            BroadcastReceiver.Instance.BroadcastMessage(MessageCode.APP, MessageType.UpdateChange, response);
            
            yield return null;
        }
        private static IEnumerator ATH0HandleReconect(string coderun, Dictionary<byte,object> data)
        {
            
            if ((short)data[(byte)ParameterCode.ErrorCode] != (short)ReturnCode.OK)
            {
                //Debug.LogError("--------------------ATH0HandleReconect---------ERROR------------");
                BroadcastReceiver.Instance.BroadcastMessage(MessageCode.NETWORK, MessageType.LostSession, data[(byte)ParameterCode.ErrorMsg]);
                yield break;
            }
            ATH0_Response response = new ATH0_Response(data);
            UpdateUserInfo(response);
           // Debug.LogError("--------------------ATH0HandleReconect--------------------- : " + response.UserID);
            BroadcastReceiver.Instance.BroadcastMessage(MessageCode.NETWORK, MessageType.Reconnected, response);

            if (Base.Utils.EventManager.Instance) Base.Utils.EventManager.Instance.RaiseEventInTopic(Base.Utils.EventManager.CHANGE_BALANCE);
            Broadcast.BroadcastReceiver.Instance.BroadcastMessage(Broadcast.MessageCode.APP, Broadcast.MessageType.CashChanged, null);
            Broadcast.BroadcastReceiver.Instance.BroadcastMessage(Broadcast.MessageCode.APP, Broadcast.MessageType.UpdateInfo, null);
            
            yield return null;
        }
        /// <summary>
		/// {"cash": cash_in_acc, "avatar": "avatar_url", "gameid": "gameid", "tableid": "tableid"}
		/// </summary>
		/// <param name="code"></param>
		/// <param name="type"></param>
		/// <param name="data"></param>
//		private static IEnumerator LGI3HandleReconnect(string coderun, Dictionary<byte, object> data)
//        {
//            if ((short)data[(byte)ParameterCode.ErrorCode] != (short)ReturnCode.OK)
//            {
//                BroadcastReceiver.Instance.BroadcastMessage(MessageCode.NETWORK, MessageType.LostSession, data[(byte)ParameterCode.ErrorMsg]);
//                yield break;
//            }
//
//            LGI3_Response response = new LGI3_Response(data);
//            UpdateUserInfo(response);
//
//            //response.GameId
//            DataDispatcher.Instance().SetExtras(ClientGameConfig.KeyDatadispatcher.LOBBY).PutExtra(ClientGameConfig.KeyDatadispatcher.TABLEID, response.TableId);
//
//            //Debug.LogError("LGI3HandleReconnect, TableId: " + response.TableId);
//
//            BroadcastReceiver.Instance.BroadcastMessage(MessageCode.NETWORK, MessageType.Reconnected, response);
//            yield return null;
//        }
        private static void UpdateUserInfo(ATH0_Response response)
        {

            ClientConfig.UserInfo.IS_EVENTX2 = response.TotalMomo == 0 || response.TotalCharging == 0;
            ClientConfig.UserInfo.ID = response.UserID;
            ClientConfig.UserInfo.NICKNAME = response.Nickname;
            ClientConfig.UserInfo.AVATAR = response.Avatar;
            ClientConfig.UserInfo.GOLD = response.Gold;
            ClientConfig.UserInfo.SILVER = response.Silver;
            ClientConfig.UserInfo.CURVIP = response.CurrentVip;
            ClientConfig.UserInfo.SESSION = response.Session;
            ClientConfig.UserInfo.IS_LOGIN = true;
            ClientConfig.UserInfo.PHONE = response.PhoneNumber;
            ClientGameConfig.TotalSpin = response.TotalSpin;
            //Debug.Log("Reconnecttor: " + ClientConfig.UserInfo.PHONE);
        }
//        private static void UpdateUserInfo(LGI3_Response response)
//        {
//            ClientConfig.UserInfo.SESSION = response.Session;
//            ClientConfig.UserInfo.RUBY = response.CashGold;
//            ClientConfig.UserInfo.CASH = response.CashSilver;
//        }
    }
}
