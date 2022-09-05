using AppConfig;
using Base.Utils;
using CoreBase.Controller;
using GameProtocol.COU;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.Home.ExChange
{
    public class ExChangeController : UIController
    {
        private TelcoDetail[] telcoDetails;
        private float Rate;

        public ExChangeController(IView view) : base(view)
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
        private void RequestGetTelco(object[] data)
        {
            if (telcoDetails != null)
            {
                View.OnUpdateView("ShowTelcos", new object[] { telcoDetails, Rate });
                return;
            }
            COU0_Request request = new COU0_Request();

            DataListener dataListener = new DataListener(HandlerResponseCOU0_GetTelcos);
            Network.Network.SendOperation(request, dataListener);
        }

        private IEnumerator HandlerResponseCOU0_GetTelcos(string coderun, Dictionary<byte, object> data)
        {
            COU0_Response response = new COU0_Response(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowNotify", response.ErrorMsg);
                yield break;
            }
            telcoDetails = response.telcoDetails;
            Rate = response.Rate;
            View.OnUpdateView("ShowTelcos", new object[] { telcoDetails, Rate });
            yield return null;
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestExChangeCard(object[] param)
        {
            COU1CashoutRequest request = new COU1CashoutRequest()
            {
                //Telco = (string)param[0],
                //Amount = (int)param[1],
                //Otp = (string)param[2],
                //CateCashout = 0,
            };
            DataListener dataListener = new DataListener(HandlerResponseCOU1_ExChange);
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestExChangeBank(object[] param)
        {
            COU1CashoutRequest request = new COU1CashoutRequest()
            {
                //Amount = (int)param[0],
                //Telco = (string)param[1],
                //AccountId = (string)param[2],
                //AccountName = (string)param[3],
                //CateCashout = 2,
            };
            DataListener dataListener = new DataListener(HandlerResponseCOU1_ExChange);
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestExChangeItem(object[] param)
        {
            COU1CashoutRequest request = new COU1CashoutRequest()
            {
                //Amount = (int)param[0],
                //AccountId = (string)param[1],
                //CateCashout = 1,
            };
            DataListener dataListener = new DataListener(HandlerResponseCOU1_ExChange);
            Network.Network.SendOperation(request, dataListener);
        }

        private IEnumerator HandlerResponseCOU1_ExChange(string coderun, Dictionary<byte, object> data)
        {
            COU1CashoutResponse response = new COU1CashoutResponse(data);
            View.OnUpdateView("ShowNotify", response.ErrorMsg);
            if (response.ErrorCode != 0)
            {
                yield break;
            }
            ClientConfig.UserInfo.GOLD = response.Gold;
            //Broadcast.BroadcastReceiver.Instance.BroadcastMessage(Broadcast.MessageCode.APP, Broadcast.MessageType.CashChanged, null);
            EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_BALANCE);
            yield return null;
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
            //    View.OnUpdateView("ShowNotify", response.ErrorMsg);
            //    yield break;
            //}

            //View.OnUpdateView("ShowBankInfo", new object[] { response.Banks });

            yield return null;
        }

    }
}