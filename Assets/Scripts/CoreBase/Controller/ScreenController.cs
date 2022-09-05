using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Broadcast;
using Interface;
using Utilities;
using AppConfig;
using Listener;
using System.Collections;
using GameProtocol.Protocol;
using GameProtocol.ATH;
//using GameProtocol.Lobby;

namespace CoreBase.Controller
{
    public class ScreenController : PopupController
    {
        private const string TAG = "ScreenController";

        public ScreenController(IView view) : base(view)
        {
        }

        protected override void RegisterMessengers()
        {
            base.RegisterMessengers();
            BroadcastReceiver.Instance.AddMessenger(MessageCode.NETWORK, this);
        }

        protected override void UnregisterMessengers()
        {
            base.UnregisterMessengers();
            BroadcastReceiver.Instance.RemoveMessenger(MessageCode.NETWORK, this);
        }

        #region Network message
        protected override void HandleNetworkMessage(MessageType Type, object Msg)
        {
			//LogMng.LogError (TAG, "Type : " + Type);
            switch (Type)
            {
                case MessageType.Connecting:
                    HandleNetworkConnecting(Msg);
                    break;
                case MessageType.Connected:
                    HandleNetworkConnected(Msg);
                    break;
                case MessageType.Ready:
                    HandleNetworkReady(Msg);
                    break;
                case MessageType.Disconnect:
                    HandleNetworkDisconnect(Msg);
                    break;
                case MessageType.Reconnected:
                    HandleNetworkReconnected(Msg);
                    break;
                case MessageType.LostSession:
                    HandleNetworkLostSession(Msg);
                    break;
                case MessageType.Death:
                    HandleNetworkDeath(Msg);
                    break;

                case MessageType.CheckUpdate:
                    HandleNetworkCheckUpdate(Msg);
                    break;
                case MessageType.UpdateChange:
                    HandleNetworkUpdateChange(Msg);
                    break;
            }
        }

        protected virtual void HandleNetworkConnecting(object Msg)
        {
            LogMng.Log(TAG, "HandleNetworkConnecting---------------");
            View.OnUpdateView("OpenReconnecting");
        }

        protected virtual void HandleNetworkConnected(object Msg)
        {
            Game.Gameconfig.ClientGameConfig.isAutoReconnect = false;
            LogMng.Log(TAG, "HandleNetworkConnected---------------");
           
        }

        protected virtual void HandleNetworkReady(object Msg)
        {
            LogMng.Log(TAG, "HandleNetworkReady---------------");
            View.OnUpdateView("HideReconnecting");
        }

        protected virtual void HandleNetworkDisconnect(object Msg)
        {
            LogMng.Log(TAG, "HandleNetworkDisconnect---------------");
            View.OnUpdateView("OpenReconnecting");
        }

        protected virtual void HandleNetworkReconnected(object Msg)
        {
            LogMng.Log(TAG, "HandleNetworkReconnected---------------");
            View.OnUpdateView("HideReconnecting");
            View.OnUpdateView("HideLoadingProgress");
            View.OnUpdateView("OnReconnected", (ATH0_Response)Msg);
        }

        protected virtual void HandleNetworkLostSession(object Msg)
        {
            LogMng.Log(TAG, "HandleNetworkLostSession---------------");
            View.OnUpdateView("HideReconnecting");
            if(!string.IsNullOrEmpty(ClientConfig.UserInfo.UNAME)
                && !string.IsNullOrEmpty(ClientConfig.UserInfo.PASSWORD)){
                View.OnUpdateView("OpenLoadingProgress");
                AutoLogin();
            }
            else
            {
                ShowHome();
            }
        }

        protected virtual void HandleNetworkDeath(object Msg)
        {
            LogMng.Log(TAG, "HandleNetworkDeath---------------");
            View.OnUpdateView("OpenRetryPopup");
        }

        protected virtual void HandleNetworkCheckUpdate(object Msg)
        {
            LogMng.Log(TAG, "HandleNetworkCheckUpdate---------------");
        }

        protected virtual void HandleNetworkUpdateChange(object Msg)
        {
            LogMng.Log(TAG, "HandleNetworkUpdateChange---------------");
        }
        #endregion

        protected virtual void AutoLogin()
        {
            LogMng.Log(TAG, "Auto Login : " + ClientConfig.UserInfo.UNAME + " , " + ClientConfig.UserInfo.PASSWORD);
            DataListener listener = new DataListener(ATH0HandleRespone);
            ATH0_Request request = new ATH0_Request()
            {
                Username = ClientConfig.UserInfo.UNAME,
                Password = ClientConfig.UserInfo.PASSWORD,
                SenderId = listener.GetHashCode()
            };
            Network.Network.SendOperation(request, listener);
        }

        IEnumerator ATH0HandleRespone(string coderun,Dictionary<byte,object> data)
        {
            LogMng.Log(TAG, "SceneController : LGI0 Handle Respone");
            ATH0_Response response = new ATH0_Response(data);
            //if Error
            if (response.ErrorCode != (short)ReturnCode.OK)
            {
                View.OnUpdateView("HideLoadingProgress");
                View.OnUpdateView("GoToHome");
                yield break;
            }
            ClientConfig.UserInfo.IS_EVENTX2 = response.TotalMomo == 0 || response.TotalCharging == 0;
            //if Success
            View.OnUpdateView("HideLoadingProgress");
            ClientConfig.UserInfo.GOLD_SAFE = response.GoldSafe;
            ClientConfig.UserInfo.ID = response.UserID;
            ClientConfig.UserInfo.PHONE = response.PhoneNumber;
            //string uname, string nickname, string passord, long id, string session, string avatar, long cash, int curvip, string vipname, int maxvip,byte _viptype , LoginType type
            ClientConfig.UserInfo.SetUserInfo(ClientConfig.UserInfo.UNAME, ClientConfig.UserInfo.NICKNAME, ClientConfig.UserInfo.PASSWORD, response.UserID, response.Session, response.Avatar, response.Gold, response.Silver, response.CurrentVip,response.VipName, response.MaxVip , response.VipType, ClientConfig.UserInfo.LOGIN_TYPE);
            View.OnUpdateView("OnAutoLoginSuccess", response.GameId, response.TableId);
            yield return null;
        }
        /// <summary>
        /// Show Home : Register, Login, Playtrial, LoginFB...
        /// </summary>
        protected void ShowHome()
        {
            ClientConfig.UserInfo.IS_LOGIN = false;
            View.OnUpdateView("GoToHome");
        }
    }
}
