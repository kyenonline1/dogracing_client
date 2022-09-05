using AppConfig;
using Base.Utils;
using CoreBase.Controller;
using GameProtocol.ATH;
using GameProtocol.DIS;
using GameProtocol.OTP;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.Home.Shop
{
    public class TranferController : UIController
    {
        public TranferController(IView view) : base(view)
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
        private void RequestListDaiLy(object[] param)
        {
            DataListener dataListener = new DataListener(ResponseListDaiLy);
            DIS0_Request request = new DIS0_Request();
            Network.Network.SendOperation(request, dataListener);
        }


        private IEnumerator ResponseListDaiLy(string coderun, Dictionary<byte, object> data)
        {
            DIS0_Response response = new DIS0_Response(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }
            View.OnUpdateView("ShowListDaiLy", new object[] { response.Data, response.Rate });
            yield return null;
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestTranferCoin(object[] param)
        {
            DataListener dataListener = new DataListener(ResponseTranferCoin);
            DIS1_Request request = new DIS1_Request();
            request.Nicknames = (string[])param[0];
            request.Golds = (int[])param[1];
            request.Reason = (string[])param[2];
            request.Capcha = (string)param[3];
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestCapcha(object[] param)
        {
            DataListener dataListener = new DataListener(HandlerResponseCapchaATH6);
            ATH6_Request request = new ATH6_Request()
            {
            };
            Network.Network.SendOperation(request, dataListener);
        }


        private IEnumerator ResponseTranferCoin(string coderun, Dictionary<byte, object> data)
        {
            DIS1_Response response = new DIS1_Response(data);
            View.OnUpdateView("ShowError", response.ErrorMsg);
            if (response.ErrorCode != 0)
            {
                yield break;
            }
            View.OnUpdateView("ClearData");
            ClientConfig.UserInfo.GOLD = response.CurrentGold;
            EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_BALANCE);
            yield return null;
        }

        private IEnumerator HandlerResponseCapchaATH6(string coderun, Dictionary<byte, object> data)
        {
            ATH6_Response response = new ATH6_Response(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }

            View.OnUpdateView("ShowCapcha", response.Url);

            yield return null;
        }

    }
}