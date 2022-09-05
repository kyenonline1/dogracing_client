
using CoreBase.Controller;
using GameProtocol.ACC;
using GameProtocol.OTP;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitis.Command;

namespace Controller.Home.UpdateInfo
{
    public class UpdateInfoController : UIController
    {
        public UpdateInfoController(IView view) : base(view)
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
        private void UpdatePhone(object[] param)
        {
            DataListener listener = new DataListener(HandlerResponseUpdateInfo);
            ACC1_Request request = new ACC1_Request()
            {
                NumberPhone = (string)param[0]
            };
            Network.Network.SendOperation(request, listener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestOTP(object[] param)
        {
            DataListener listener = new DataListener(HandlerResponseGetOTP);
            OTP0_Request request = new OTP0_Request()
            {
                NumberPhone = (string)param[0],
            };

            Network.Network.SendOperation(request, listener);
        }

        private IEnumerator HandlerResponseUpdateInfo(string coderun, Dictionary<byte, object> data)
        {
            Debug.Log("UPDATE IN FO ACC1");
            ACC1_Response response = new ACC1_Response(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }
            View.OnUpdateView("CloseLoading");
            View.OnUpdateView("UpdateInfoSuccess", response.ErrorMsg);
            yield return null;
        }

        private IEnumerator HandlerResponseGetOTP(string coderun, Dictionary<byte, object> data)
        {
            OTP0_Response response = new OTP0_Response(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break; ;
            }
            View.OnUpdateView("CloseLoading");
            yield return null;
        }

    }
}
