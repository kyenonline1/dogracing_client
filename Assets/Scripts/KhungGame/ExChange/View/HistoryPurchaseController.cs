using CoreBase.Controller;
using GameProtocol.PAY;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.Common
{
    public class HistoryPurchaseController : UIController
    {
        public HistoryPurchaseController(IView view) : base(view)
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
        private void RequestHistoryPurchase(object[] param)
        {
            PAY5_Request request = new PAY5_Request();
            DataListener dataListener = new DataListener(HandlerResponsePAY5_HistoryPurchase);
            Network.Network.SendOperation(request, dataListener);
        }

        private IEnumerator HandlerResponsePAY5_HistoryPurchase(string coderun, Dictionary<byte, object> data)
        {
            PAY5_Response response = new PAY5_Response(data);
            if(response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }

            View.OnUpdateView("ShowHistorys", new object[] { response.Histories });

        }

    }
}