using CoreBase.Controller;
using GameProtocol.ATH;
using GameProtocol.COU;
using GameProtocol.PAY;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.Home.Shop
{
    public class ChargerBankController : UIController
    {
        public ChargerBankController(IView view) : base(view)
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
        private void RequestBankInfo(object[] param)
        {
            //COU5BankInfosRequest request = new COU5BankInfosRequest();
            //DataListener dataListener = new DataListener(HanderResponseBankInfoCOU5);
            //Network.Network.SendOperation(request, dataListener);
        }

        private IEnumerator HanderResponseBankInfoCOU5(string coderun, Dictionary<byte, object> data)
        {
            //COU5BankInfosResponse response = new COU5BankInfosResponse(data);
            //if (response.ErrorCode != 0)
            //{
            //    View.OnUpdateView("ShowError", response.ErrorMsg);
            //    yield break;
            //}

            //View.OnUpdateView("ShowBankInfo", new object[] { response.Banks });

            yield return null;
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestAccountBankInfo(object[] param)
        {
            PAY6BankInfoRequest request = new PAY6BankInfoRequest();
            DataListener dataListener = new DataListener(HanderResponseBankInfoPAY6);
            Network.Network.SendOperation(request, dataListener);
        }

        private IEnumerator HanderResponseBankInfoPAY6(string coderun, Dictionary<byte, object> data)
        {
            PAY6BankInfoResponse response = new PAY6BankInfoResponse(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }

            View.OnUpdateView("ShowBankInfo", new object[] { response.BankName, response.AccountName, response.AccountId, response.TransferContent, response.CardRate });

            yield return null;
        }


        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestCapcha(object[] param)
        {
            DataListener dataListener = new DataListener(HandlerResponseCapcha);
            ATH6_Request request = new ATH6_Request();
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
            View.OnUpdateView("ShowCapcha", response.Url);
            yield return null;
        }
    }
}
