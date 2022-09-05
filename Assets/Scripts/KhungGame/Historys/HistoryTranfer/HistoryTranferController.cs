using CoreBase.Controller;
using GameProtocol.DIS;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.Home.History
{
    public class HistoryTranferController : UIController
    {
        public HistoryTranferController(IView view) : base(view)
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
        private void RequestHistoryTranfer(object[] param)
        {
            DIS2_Request request = new DIS2_Request();
            DataListener dataListener = new DataListener(HandlerResponseDIS2HistoryTranfer);
            Network.Network.SendOperation(request, dataListener);
        }

        private IEnumerator HandlerResponseDIS2HistoryTranfer(string coedrun, Dictionary<byte, object> data)
        {
            DIS2_Response response = new DIS2_Response(data);

            if(response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }

            View.OnUpdateView("ShowHistorysTranfer", new object[] { response.Data });

            yield return null;
        }
    }
}