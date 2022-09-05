using AppConfig;
using Base.Utils;
using CoreBase.Controller;
using GameProtocol.COU;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Controller.Home { 
    public class PayPalCashoutController : UIController
    {
        public PayPalCashoutController(IView view) : base(view)
        {
        }

        public override void StartController()
        {
            base.StartController();
            RequestCateInfo();
        }

        public override void StopController()
        {
            base.StopController();
        }

        private void RequestCateInfo()
        {
            COU0_Request request = new COU0_Request();
            DataListener dataListener = new DataListener(HandlerResponseCOU0_CateInfo);
            Network.Network.SendOperation(request, dataListener);
        }


        IEnumerator HandlerResponseCOU0_CateInfo(string coderun, Dictionary<byte, object> data)
        {

            COU0_Response response = new COU0_Response(data);

            if(response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }
            if(response.telcoDetails != null && response.telcoDetails.Length > 0)
            View.OnUpdateView("ShowCateInfo", new object[] { response.telcoDetails[0], response.Rate });

            yield return null;
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestCashout(object[] param)
        {
            COU1CashoutRequest request = new COU1CashoutRequest()
            {
                FirtName  = (string)param[0],
                LastName = (string)param[1],
                Email = (string)param[2],
                Amount = (int)param[3]
            };
            DataListener dataListener = new DataListener(HandlerResponseCOU1_CashOut);
            Network.Network.SendOperation(request, dataListener);
        }

        IEnumerator HandlerResponseCOU1_CashOut(string coderun, Dictionary<byte, object> data)
        {

            COU1CashoutResponse response = new COU1CashoutResponse(data);

            View.OnUpdateView("ShowError", response.ErrorMsg);
            if (response.ErrorCode != 0)
            {
                yield break;
            }
            View.OnUpdateView("ClearData");
            ClientConfig.UserInfo.GOLD = response.Gold;
            EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_BALANCE);

            yield return null;
        }
    }
}