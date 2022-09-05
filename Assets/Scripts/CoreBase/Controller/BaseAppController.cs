using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Broadcast;
using Network;
using AppConfig;
using System.Collections;
using Game.Gameconfig;

namespace CoreBase.Controller
{
    public class BaseAppController : ScreenController
    {
        public BaseAppController(IView view) : base(view)
        {
        }

        #region Handle Messenger

        public override IEnumerator HandlePush(string coderun, Dictionary<byte, object> data)
        {
            if(coderun == "ATH1")
            {
                HandleLGI_1Push(data);
            }
            return base.HandlePush(coderun, data);
        }

        protected override void RegisterMessengers()
        {
            base.RegisterMessengers();
            
            BroadcastReceiver.Instance.AddMessenger(MessageCode.APP, this);
        }

        protected override void UnregisterMessengers()
        {
            base.UnregisterMessengers();

            BroadcastReceiver.Instance.RemoveMessenger(MessageCode.APP, this);
        }

        protected override void HandleAppMessage(MessageType Type, object Msg)
        {
            base.HandleAppMessage(Type, Msg);
            switch (Type)
            {
                case MessageType.CashChanged:
                    AppCashChange(Msg);
                    break;
                default:
                    break;
            }
        }
        protected virtual void AppCashChange(object Msg)
        {
            
        }
        #endregion

        #region Handle Push 
        protected override void RegisterPushHandlers()
        {
            base.RegisterPushHandlers();
            PushManager.Instance.RegisterPushHandler(ClientGameConfig.RequestCode.ATH, this);
            PushManager.Instance.RegisterPushHandler(ClientGameConfig.RequestCode.MSG, this);
            PushManager.Instance.RegisterPushHandler(ClientGameConfig.RequestCode.UDT, this);
            PushManager.Instance.RegisterPushHandler(ClientGameConfig.RequestCode.CMN, this);
        }

        public override void UnregisterPushHandlers()
        {
            base.UnregisterPushHandlers();
            PushManager.Instance.UnRegisterPushHandler(ClientGameConfig.RequestCode.ATH, this);
            PushManager.Instance.UnRegisterPushHandler(ClientGameConfig.RequestCode.MSG, this);
            PushManager.Instance.UnRegisterPushHandler(ClientGameConfig.RequestCode.UDT, this);
            PushManager.Instance.UnRegisterPushHandler(ClientGameConfig.RequestCode.CMN, this);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_PUSH_HANDLER, Name = "LGI_1")]
        void HandleLGI_1Push(Dictionary<byte, object> data)
        {
            //show dialog msg logout has 1 button OK, press OK to logout
            DoLogout();
            //yield return null;
        }

        #endregion

        private void DoLogout()
        {
            //clear userinfo
            ClientConfig.UserInfo.ClearUserInfo();
            //call logout facebook if need
            //...
            //View.OnUpdateView("GotoLoginScreen");
        }
    }
}
