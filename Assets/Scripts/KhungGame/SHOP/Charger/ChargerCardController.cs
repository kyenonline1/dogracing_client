using AppConfig;
using CoreBase.Controller;
using GameProtocol.ATH;
using GameProtocol.PAY;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.Home.Shop
{
    public class ChargerCardController : UIController
    {
        public ChargerCardController(IView view) : base(view)
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
        private void RequestPAY0GetCardInfo(object[] data)
        {
            PAY0_Request request = new PAY0_Request()
            {
                Cate = 2
            };
            DataListener dataListener = new DataListener(HandlerResponsePAY0CardInfo);
            //LogMng.Log("NAPTHE", "Request Card Info");
            Network.Network.SendOperation(request, dataListener);
        }

        private IEnumerator HandlerResponsePAY0CardInfo(string coderun, Dictionary<byte, object> data)
        {
            PAY0_Response response = new PAY0_Response(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }
            View.OnUpdateView("ShowDataCardInfo", new object[] { response.Cates, response.CardRate });
            yield return null;
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

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestNapThePAY2(object[] data)
        {
            PAY2_Request request = new PAY2_Request()
            {
                Type = (string)data[0],
                Seri = (string)data[1],
                CardNumber = (string)data[2],
                Amount = (int)data[3],
                Capcha = (string)data[4],
                TransId = (int)data[5],
            };
            DataListener dataListener = new DataListener(HandlerResponsePAY2NapThe);
            Network.Network.SendOperation(request, dataListener);
        }

        private IEnumerator HandlerResponsePAY2NapThe(string coderun, Dictionary<byte, object> data)
        {
            PAY2_Response response = new PAY2_Response(data);
            View.OnUpdateView("ShowDialog", response.ErrorMsg);

            if (response.ErrorCode != 0)
            {
                yield break;
            }
            if (response.Gold > 0)
            {
                ClientConfig.UserInfo.GOLD = response.Gold;
                Base.Utils.EventManager.Instance.RaiseEventInTopic(Base.Utils.EventManager.CHANGE_BALANCE);
            }
            View.OnUpdateView("ClearData");
            yield return null;
        }

    }
}