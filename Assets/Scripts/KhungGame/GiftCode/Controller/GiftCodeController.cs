using AppConfig;
using Base.Utils;
using CoreBase.Controller;
using GameProtocol.ATH;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitis.Command;

namespace Controller.Home.GiftCode
{
    public class GiftCodeController : UIController
    {
        public GiftCodeController(IView view) : base(view)
        {
        }

        public override void StartController()
        {
            base.StartController();
        }

        public override void StopController()
        {
            base.StopController();
        }


        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RefreshCapcha(object[] param)
        {
            DataListener dataListener = new DataListener(HandlerResponseCapcha);
            ATH6_Request request = new ATH6_Request();
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestGiftCodeATH9(object[] param)
        {
            string giftcode = (string)param[0];
            string capcha = (string)param[1];
            DataListener dataListener = new DataListener(HandlerResponseGiftCodeATH9);
            ATH9_Request request = new ATH9_Request()
            {
                GiftCode = giftcode,
                Captcha = capcha
            };
            Network.Network.SendOperation(request, dataListener);
        }

        private IEnumerator HandlerResponseCapcha(string coderun, Dictionary<byte, object> data)
        {
            ATH6_Response response = new ATH6_Response(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }
            View.OnUpdateView("UpdateCapcha", response.Url);
            yield return null;
        }

        private IEnumerator HandlerResponseGiftCodeATH9(string coderun, Dictionary<byte, object> data)
        {
            ATH9_Response response = new ATH9_Response(data);
            View.OnUpdateView("ShowError", response.ErrorMsg);
            if (response.ErrorCode != 0)
            {
                yield break;
            }
            ClientConfig.UserInfo.GOLD = response.Gold;
            EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_BALANCE);
            yield return null;
        }
    }
}
