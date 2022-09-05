using CoreBase.Controller;
using GameProtocol.COU;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.Common
{
    public class HistoryExchangeController : UIController
    {
        public HistoryExchangeController(IView view) : base(view)
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
        private void RequestHistoryExChange(object[] param) 
        {
            COU2HisotiesCashoutRequest request = new COU2HisotiesCashoutRequest();
            DataListener dataListener = new DataListener(HandlerResponseCOU2HistoryExChange);
            Network.Network.SendOperation(request, dataListener);
        }

        private IEnumerator HandlerResponseCOU2HistoryExChange(string coderun, Dictionary<byte, object> data)
        {
            COU2HistoriesCashoutResponse response = new COU2HistoriesCashoutResponse(data);
            if(response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }

            View.OnUpdateView("ShowHistorys", new object[] { response.Histories });
        }
    }
}