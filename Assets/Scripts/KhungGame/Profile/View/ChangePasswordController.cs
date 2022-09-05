using AppConfig;
using CoreBase.Controller;
using GameProtocol.ACC;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace View.Home.UpdateInfo
{
    public class ChangePasswordController : UIController
    {

        string password;

        public ChangePasswordController(IView view) : base(view)
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
        private void RequestChangePassword(object[] param)
        {
            password = (string)param[0];
            ACC1_Request request = new ACC1_Request()
            {
                Password = password,
                //OTP = (int)param[1]
            };
            DataListener dataListener = new DataListener(HandlerResponseACC1ChangeInfo);
            Network.Network.SendOperation(request, dataListener);
        }

        private IEnumerator HandlerResponseACC1ChangeInfo(string coderun, Dictionary<byte, object> data)
        {
            ACC1_Response response = new ACC1_Response(data);
            //Debug.Log("UPDATE IN FO ACC1 HandlerResponseACC1ChangeInfo: " + response.ErrorCode);
           

            View.OnUpdateView("ShowError", response.ErrorMsg);

            if (response.ErrorCode == 0)
            {
                ClientConfig.UserInfo.PASSWORD = password;
                password = string.Empty;
            }

            yield return null;
        }
    }
}