using CoreBase.Controller;
using GameProtocol.MOM;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.Home.Shop
{
    public class ChargerMomoController : UIController
    {
        public ChargerMomoController(IView view) : base(view)
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
        private void RequestInfoMomo(object[] param)
        {
            MOM0_Request request = new MOM0_Request();
            DataListener dataListener = new DataListener(HandlerResponseMOM0_MomoInfo);
            Network.Network.SendOperation(request, dataListener);
        }

        private IEnumerator HandlerResponseMOM0_MomoInfo(string coderun, Dictionary<byte, object> data)
        {
            MOM0_Response response = new MOM0_Response(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowNotify", response.ErrorMsg);
                yield break;
            }
            View.OnUpdateView("UpdateMomoInfo", new object[] { response.NumberPhone, response.UserName, response.Rate, response.Content, response.MinCashIn, response.MaxCashIn });
            yield return null;
        }
    }
}
