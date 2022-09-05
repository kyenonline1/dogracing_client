using AppConfig;
using Base;
using Base.Utils;
using CoreBase.Controller;
using Game.Gameconfig;
using GameProtocol.PAY;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitis.Command;

namespace Controller.Home.IAP
{

    public class IAPController : UIController
    {
        public IAPController(IView view) : base(view)
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
        private void RequestCatePAY0(object[] param)
        {
            PAY0_Request request = new PAY0_Request();
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestPAY1(object[] param)
        {
            PAY1_Request request = new PAY1_Request()
            {
#if UNITY_ANDROID
                Type = "Google"
#else
                Type = "Apple"
#endif
            };

            DataListener dataListener = new DataListener(HandlerResponsePAY1GetPackage);
            Network.Network.SendOperation(request, dataListener);
        }


        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestPAY3AppleBillind(object[] param)
        {
            string productId = (string)param[0];
            long Transaction = (long)param[1];
            string receiptData = (string)param[2];
            PAY3_Request request = new PAY3_Request()
            {
                ProductId = productId,
                ReceiptData = receiptData,
                Transaction = Transaction,
                Quantity = "1",// int
                PurchaseDate = ""
            };
            DataListener dataListener = new DataListener(HandlerResponsePAY3AppleBilling);
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestPAY4GoogleBilling(object[] param)
        {
            //string productId = (string)param[0];
            string SignedData = (string)param[1];
            string Signature = (string)param[2];
            PAY4_Request request = new PAY4_Request()
            {
                SignedData = SignedData,
                Signature = Signature
            };
            DataListener dataListener = new DataListener(HandlerResponsePAY4GoogleBilling);
            Network.Network.SendOperation(request, dataListener);
        }

        private int IndexPackage;

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RememPackageCharingCard(object[] param)
        {
            IndexPackage = (int)param[0];
        }

        private void PurchaserSuccess(object[] param)
        {

        }

        private Package[] packages;

    

        private IEnumerator HandlerResponsePAY1GetPackage(string coderun, Dictionary<byte, object> data)
        {
            Debug.Log("HandlerResponsePAY1GetPackage");
            PAY1_Response response = new PAY1_Response(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }
            packages = response.Packages;
            View.OnUpdateView("ShowPackages", new object[] { packages });
        }

        private IEnumerator HandlerResponsePAY2CharingCard(string coderun, Dictionary<byte, object> data)
        {
            PAY2_Response response = new PAY2_Response(data);
            View.OnUpdateView("ShowError", response.ErrorMsg);
            if (response.ErrorCode != 0)
            {
                yield break;
            }
            ClientConfig.UserInfo.GOLD = response.Gold;
            EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_BALANCE);
        }


        private IEnumerator HandlerResponsePAY3AppleBilling(string coderun, Dictionary<byte, object> data)
        {
            PAY3_Response response = new PAY3_Response(data);
            View.OnUpdateView("ShowError", response.ErrorMsg);
            if (response.ErrorCode != 0)
            {
                yield break;
            }
            ClientConfig.UserInfo.GOLD = response.Gold;
            EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_BALANCE);
        }

        private IEnumerator HandlerResponsePAY4GoogleBilling(string coderun, Dictionary<byte, object> data)
        {
            PAY4_Response response = new PAY4_Response(data);
            View.OnUpdateView("ShowError", response.ErrorMsg);
            if (response.ErrorCode != 0)
            {
                yield break;
            }
            ClientConfig.UserInfo.GOLD = response.Gold;
            EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_BALANCE);
        }

    }
}
