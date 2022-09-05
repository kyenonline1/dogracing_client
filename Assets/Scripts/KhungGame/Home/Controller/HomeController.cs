using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitis.Command;
using GameProtocol.UDT;
using AppConfig;
using Base.Utils;
using MsgPack;
using Game.Gameconfig;
using Utilities;
using GameProtocol.OTP;
using GameProtocol.ACC;
using CoreBase.Controller;
using Interface;
using Listener;
using GameProtocol.ATH;
using Broadcast;

namespace Controller.Home
{
    public class HomeController : ScreenController
    {
        private int version = 101; // update 06/5/2021

        public override void StartController()
        {
            base.StartController();
            //Debug.Log("StartController : " + connector.IsConnected);
            if (!Network.Network.IsNetworkConnected())
            {
                RegisterObjects();
                //View.OnUpdateView("ShowLoading");
                RunStartEvent(null);
            }
            else View.OnUpdateView("UDT0HandleResponse");
        }


        private void RegisterObjects()
        {
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.ATH.GameConfig), GameProtocol.ATH.GameConfig.RegisterType, GameProtocol.ATH.GameConfig.Serialize, GameProtocol.ATH.GameConfig.Desserialize);

           
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.ANN.Announce), GameProtocol.ANN.Announce.RegisterType, GameProtocol.ANN.Announce.Serialize, GameProtocol.ANN.Announce.Desserialize);
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.COU.CashoutHistory), GameProtocol.COU.CashoutHistory.RegisterType, GameProtocol.COU.CashoutHistory.Serialize, GameProtocol.COU.CashoutHistory.Desserialize);
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.COU.TelcoDetail), GameProtocol.COU.TelcoDetail.RegisterType, GameProtocol.COU.TelcoDetail.Serialize, GameProtocol.COU.TelcoDetail.Desserialize);
           
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.MSG.ChatDetail), GameProtocol.MSG.ChatDetail.RegisterType, GameProtocol.MSG.ChatDetail.Serialize, GameProtocol.MSG.ChatDetail.Desserialize);
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.PAY.Package), GameProtocol.PAY.Package.RegisterType, GameProtocol.PAY.Package.Serialize, GameProtocol.PAY.Package.Desserialize);
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.PAY.CateCharging), GameProtocol.PAY.CateCharging.RegisterType, GameProtocol.PAY.CateCharging.Serialize, GameProtocol.PAY.CateCharging.Desserialize);

            Network.Network.RegisterPhotonObject(typeof(GameProtocol.TOP.TopEvent), GameProtocol.TOP.TopEvent.RegisterType, GameProtocol.TOP.TopEvent.Serialize, GameProtocol.TOP.TopEvent.Desserialize);
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.TOP.TopCate), GameProtocol.TOP.TopCate.RegisterType, GameProtocol.TOP.TopCate.Serialize, GameProtocol.TOP.TopCate.Desserialize);

            Network.Network.RegisterPhotonObject(typeof(GameProtocol.PAY.ChargingHistory), GameProtocol.PAY.ChargingHistory.RegisterType, GameProtocol.PAY.ChargingHistory.Serialize, GameProtocol.PAY.ChargingHistory.Desserialize);
            
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.EVN.EventEntity), GameProtocol.EVN.EventEntity.RegisterType, GameProtocol.EVN.EventEntity.Serialize, GameProtocol.EVN.EventEntity.Desserialize);
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.EVN.DailyMission), GameProtocol.EVN.DailyMission.RegisterType, GameProtocol.EVN.DailyMission.Serialize, GameProtocol.EVN.DailyMission.Desserialize);
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.EVN.FirstCharging), GameProtocol.EVN.FirstCharging.RegisterType, GameProtocol.EVN.FirstCharging.Serialize, GameProtocol.EVN.FirstCharging.Desserialize);
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.EVN.Mission), GameProtocol.EVN.Mission.RegisterType, GameProtocol.EVN.Mission.Serialize, GameProtocol.EVN.Mission.Desserialize);

          
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.TOP.TopCate), GameProtocol.TOP.TopCate.RegisterType, GameProtocol.TOP.TopCate.Serialize, GameProtocol.TOP.TopCate.Desserialize);
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.TOP.TopEvent), GameProtocol.TOP.TopEvent.RegisterType, GameProtocol.TOP.TopEvent.Serialize, GameProtocol.TOP.TopEvent.Desserialize);

            Network.Network.RegisterPhotonObject(typeof(GameProtocol.DOG.DogRacing), GameProtocol.DOG.DogRacing.RegisterType, GameProtocol.DOG.DogRacing.Serialize, GameProtocol.DOG.DogRacing.Desserialize);
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.DOG.DogRacingHistory), GameProtocol.DOG.DogRacingHistory.RegisterType, GameProtocol.DOG.DogRacingHistory.Serialize, GameProtocol.DOG.DogRacingHistory.Desserialize);
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.DOG.DogRacingHistoryByUser), GameProtocol.DOG.DogRacingHistoryByUser.RegisterType, GameProtocol.DOG.DogRacingHistoryByUser.Serialize, GameProtocol.DOG.DogRacingHistoryByUser.Desserialize);
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.DOG.PlayerDog), GameProtocol.DOG.PlayerDog.RegisterType, GameProtocol.DOG.PlayerDog.Serialize, GameProtocol.DOG.PlayerDog.Desserialize);
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.DOG.DogSlot), GameProtocol.DOG.DogSlot.RegisterType, GameProtocol.DOG.DogSlot.Serialize, GameProtocol.DOG.DogSlot.Desserialize);
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.DOG.Segment), GameProtocol.DOG.Segment.RegisterType, GameProtocol.DOG.Segment.Serialize, GameProtocol.DOG.Segment.Desserialize);
            Network.Network.RegisterPhotonObject(typeof(GameProtocol.DOG.DogChat), GameProtocol.DOG.DogChat.RegisterType, GameProtocol.DOG.DogChat.Serialize, GameProtocol.DOG.DogChat.Desserialize);


        }

        protected override void HandleAppMessage(MessageType Type, object Msg)
        {
            Debug.Log("HandleAppMessage: " + Type);
            base.HandleAppMessage(Type, Msg);
            switch (Type)
            {
                case MessageType.LostSession:
                    View.OnUpdateView("ShowNotify", (string)Msg);
                    break;
                case MessageType.CheckUpdate:
                    View.OnUpdateView("HideLoading");
                    break;

            }
        }

        protected override void HandleNetworkMessage(MessageType Type, object Msg)
        {
            base.HandleNetworkMessage(Type, Msg);
            switch (Type)
            {
                case MessageType.LostSession:
                    View.OnUpdateView("ShowNotify", (string)Msg);
                    break;
                case MessageType.Ready:
                    RequestUDT0(null);
                    break;

            }
        }




        protected override void RegisterPushHandlers()
        {
            base.RegisterPushHandlers();
            BroadcastReceiver.Instance.AddMessenger(MessageCode.APP, this);
        }

        protected override void UnregisterMessengers()
        {
            base.UnregisterMessengers();
            BroadcastReceiver.Instance.RemoveMessenger(MessageCode.APP, this);
        }

        public override void UnregisterPushHandlers()
        {
            base.UnregisterPushHandlers();
        }

        protected override void RegisterMessengers()
        {
            base.RegisterMessengers();
        }

        #region Update Controller To View

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestUDT0(object[] param)
        {
            try
            {
                //Debug.Log("RequestUDT0");
                DataListener dataListener = new DataListener(OnReceivedUDT0);
                UDT0_Request request = new UDT0_Request() { Dbtor = ClientConfig.SoftWare.DBTOR, DeviceID = ClientConfig.HardWare.DEVICE, Platform = ClientConfig.HardWare.PLATFORM, Version = ClientConfig.SoftWare.VERSION, Language = Languages.Language.LANG == Languages.Languages.en ? "en" : "vn", };
                Network.Network.SendOperation(request, dataListener);
            }
            catch (System.Exception ex)
            {
                LogMng.Log("HOMECONTROLLER", "UDT " + ex.StackTrace);
            }
        }

        //string Uname;

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestLoginATH0(object[] param)
        {
            UserName = (string)param[0];
            Password = (string)param[1];
            //Debug.Log("RequestLoginATH0 1");
            DataListener dataListener = new DataListener(OnReceivedLoginAHT0);
            ATH0_Request request = new ATH0_Request()
            {
                Username = UserName,
                Password = Password,
                Session = ClientConfig.UserInfo.SESSION
            };
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestRegisterATH2(object[] param)
        {
            UserName = (string)param[0];
            Password = (string)param[1];
            string Nickname = (string)param[2];
            DataListener dataListener = new DataListener(OnReceivedRegisterAHT2);
            ATH2_Request request = new ATH2_Request()
            {
                Username = UserName,
                Password = Password,
                Nickname = Nickname,
                Captcha = (string)param[3]
            };
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RefreshCapcha(object[] param)
        {
            DataListener dataListener = new DataListener(OnReceivedCapchaATH6);
            ATH6_Request request = new ATH6_Request();
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestLoginFacebook(object[] param)
        {
            string id = (string)param[0];
            string name = (string)param[1];
            //string firstname = string.Empty;// (string)param[2];
            //string lastname = string.Empty;//(string)param[3];
            string avatar = "1";// (string)param[4];
            DataListener dataListener = new DataListener(OnReceivedLoginFacebookATH5);
            ATH5_Request request = new ATH5_Request()
            {
                FacebookID = id,
                Fullname = name,
                Firstname = string.Empty,
                Lastname = string.Empty,
                Avatar = avatar
            };
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestPlaytrial(object[] param)
        {
            DataListener dataListener = new DataListener(OnReceivedPlayTrialATH7);
            ATH7_Request request = new ATH7_Request();
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RegisterDisplayName(object[] param)
        {
            nickname = (string)param[0];
            ACC1_Request request = new ACC1_Request()
            {
                Nickname = nickname,
            };
            DataListener dataListener = new DataListener(HandlerResponseACC1ChangeInfo);
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestChangePassword(object[] param)
        {
            // ACC6_Request request = new ACC6_Request()
            // {
            //     UserName = (string)param[0],
            //     Password = (string)param[1],
            //     Otp = (int)param[2]
            // };
            // Debug.Log("RequestChangePassword");
            // Request(
            //request.SerializeToBytes()
            //, request.GetCommand());
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestOTP(object[] param)
        {
            DataListener dataListener = new DataListener(HandlerResponseGetOTP);
            OTP1_Request request = new OTP1_Request()
            {
                UserName = (string)param[0],
            };
            Network.Network.SendOperation(request, dataListener);
        }
        #endregion

        #region Process Response

        private IEnumerator OnReceivedUDT0(string coderun, Dictionary<byte, object> data)
        {
            UDT0_Response response = new UDT0_Response(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowErrorQuitGame", response.ErrorMsg);
                yield break;
            }

            ClientGameConfig.APPFUNCTION.UrlFanpage = response.FacebookUrl;
            try
            {
                if (!string.IsNullOrEmpty(response.BundleUrl))
                {
                    string[] str = response.BundleUrl.Split('|');
                    if (str.Length > 1)
                    {
                        int vs = 101; // ver public 107. 25052019
                        int.TryParse(str[0], out vs);
                        if (version < vs)
                        {
#if UNITY_ANDROID
                            View.OnUpdateView("ShowDataUpdateVersion", str[1]);
#elif UNITY_IOS || UNITY_IPHONE
                             View.OnUpdateView("ShowDataUpdateVersion", str[2]);
#elif UNITY_STANDALONE
                             View.OnUpdateView("ShowDataUpdateVersion", str[3]);
#else
                             View.OnUpdateView("ShowDataUpdateVersion", str[1]);
#endif

                            yield break;
                        }
                        if (str != null && str.Length >= 6)
                        {
                            // Debug.Log("OnReceivedUDT0: " + str[4]);
                            ClientGameConfig.APPFUNCTION.UrlMessenger = str[5];
                        }
                    }
                }
            }
            catch
            {
            }
            int gameOffline = PlayerPrefs.GetInt("gameoffline", 0);
            //if (gameOffline == 0)
            //{
            //    ClientGameConfig.APPFUNCTION.IsAppFullFunction = false;
            //    Game.ClientGameConfig.ClientGameConfig.APPFUNCTION.IsAppCharging = false;
            //}
            //else
            //{
            ClientGameConfig.APPFUNCTION.IsAppFullFunction = response.IsAppFullFunction;
            Game.Gameconfig.ClientGameConfig.APPFUNCTION.IsAppCharging = response.IsAppCharing;
            //}
            View.OnUpdateView("UDT0HandleResponse", response);
            yield return null;
            //Debug.Log("Set Config App");
        }


        private IEnumerator OnReceivedLoginAHT0(string coderun, Dictionary<byte, object> data)
        {
            ATH0_Response response = new ATH0_Response(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                ClientConfig.UserInfo.IS_LOGIN = false;
                yield break;
            }

            ClientConfig.UserInfo.IS_EVENTX2 = response.TotalMomo == 0 || response.TotalCharging == 0;
            //Debug.Log("IS_EVENTX2: " + ClientConfig.UserInfo.IS_EVENTX2 + " - " + (response.TotalMomo == 0 || response.TotalCharging == 0));

            ClientConfig.UserInfo.UNAME = UserName;
            ClientConfig.UserInfo.PASSWORD = Password;

            ClientConfig.UserInfo.GOLD_SAFE = response.GoldSafe;
            ClientConfig.UserInfo.ID = response.UserID;

            ClientGameConfig.TotalSpin = response.TotalSpin;
            ClientConfig.UserInfo.PHONE = response.PhoneNumber;
            View.OnUpdateView("LoginSuccess", response.Nickname, response.Silver, response.Gold, response.TableId, response.Session, response.UserID, response.CurrentVip, response.MaxVip, response.Avatar, response.VipType, response.GameId, response.VipName);
            yield return null;
        }

        private string UserName = string.Empty;
        private string Password = string.Empty;

        public HomeController(IView view) : base(view)
        {
        }

        private IEnumerator OnReceivedRegisterAHT2(string coderun, Dictionary<byte, object> data)
        {
            ATH2_Response response = new ATH2_Response(data);
            if (response.ErrorCode != 0)
            {
                 View.OnUpdateView("RegisterError", response.ErrorMsg);
                RefreshCapcha(null);
                yield break;
            }
            ClientConfig.UserInfo.NICKNAME = string.Empty;
            RequestLoginATH0(new object[] { UserName, Password });
            yield return null;
        }

        private IEnumerator OnReceivedLoginFacebookATH5(string coderun, Dictionary<byte, object> data)
        {
            ATH5_Response response = new  ATH5_Response(data);
            if (response.ErrorCode != 0)
            {
                 View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }
            ClientConfig.UserInfo.LOGIN_TYPE = ClientConfig.UserInfo.LoginType.FACEBOOK;
            ClientConfig.UserInfo.UNAME = response.Username;
            ClientConfig.UserInfo.PASSWORD = response.Password;
            //Debug.Log("Uname: " + ClientConfig.UserInfo.UNAME + " - : " + ClientConfig.UserInfo.PASSWORD);
            RequestLoginATH0(new object[] { response.Username, response.Password });
            yield return null;
        }

        private IEnumerator OnReceivedCapchaATH6(string coderun, Dictionary<byte, object> data)
        {

            ATH6_Response response = new ATH6_Response(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }
            View.OnUpdateView("HandleResponseRefreshCapcha", response.Url);
            yield return null;
        }

        private IEnumerator OnReceivedPlayTrialATH7(string coderun, Dictionary<byte, object> data)
        {
            ATH7_Response response = new ATH7_Response(data);
            View.OnUpdateView("HideLoading");
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }
            ClientConfig.UserInfo.LOGIN_TYPE = ClientConfig.UserInfo.LoginType.TRIAL;
            ClientConfig.UserInfo.UNAME = response.Username;
            ClientConfig.UserInfo.PASSWORD = response.Password;
            // Debug.Log("OnReceivedPlayTrialATH7: " + ClientConfig.UserInfo.UNAME + " - : " + ClientConfig.UserInfo.PASSWORD);
            //View.OnUpdateView("ShowPopupRegisterDisplayName");
            RequestLoginATH0(new object[] { response.Username, response.Password });
            yield return null;
        }

        string nickname;

        private IEnumerator HandlerResponseACC1ChangeInfo(string coderun, Dictionary<byte, object> data)
        {
            ACC1_Response response = new ACC1_Response(data);
            //Debug.Log("UPDATE IN FO ACC1 HandlerResponseACC1ChangeInfo: " + response.ErrorCode);
            if (!string.IsNullOrEmpty(response.ErrorMsg)) View.OnUpdateView("ShowError", response.ErrorMsg);
          
            if (!string.IsNullOrEmpty(nickname))
            {
                ClientConfig.UserInfo.NICKNAME = nickname;
                EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_NICKNAME_TOPIC);

                RequestLoginATH0(new object[] { ClientConfig.UserInfo.UNAME, ClientConfig.UserInfo.PASSWORD });
                View.OnUpdateView("DisablePopupRegisterDisplayName");
            }
          
            yield return null;
        }




        private IEnumerator HandlerResponseGetOTP(string coderun, Dictionary<byte, object> data)
        {
            OTP1_Response response = new OTP1_Response(data);
            View.OnUpdateView("ShowError", response.ErrorMsg);
            if (response.ErrorCode != 0)
            {
                yield break;
            }
             View.OnUpdateView("UDT0HandleResponse");
            yield return null;
        }

        //private IEnumerator HandlerResponseACC6ForGotPassword(string coderun, Dictionary<byte, object> data)
        //{
        //    ACC6_Response response = new ACC6_Response(data);
        //    if (!string.IsNullOrEmpty(response.ErrorMsg))
        //         View.OnUpdateView("ShowError", response.ErrorMsg);
        //    if (response.ErrorCode != 0)
        //    {
        //        yield break;
        //    }
        //     View.OnUpdateView("UDT0HandleResponse");
        //    yield return null;
        //}

#endregion
         [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        protected virtual void RunStartEvent(object[] param)
        {
            Network.Network.StartNetwork();
        }

        public override void StopController()
        {
            base.StopController();
        }
    }
}
