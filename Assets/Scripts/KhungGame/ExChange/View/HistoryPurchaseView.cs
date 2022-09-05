using CoreBase;
using CoreBase.Controller;
using GameProtocol.PAY;
using Interface;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using View.DialogEx;

namespace View.Common
{
    public class HistoryPurchaseView : ViewScript
    {
        [SerializeField] private ScrollCharingHistory scrollCharingHistory;
        private ChargingHistory[] chargingHistories;

        protected override IController CreateController()
        {
            return new HistoryPurchaseController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
        }

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
        }

        private async void OnEnable()
        {
            await Task.Delay(200);
            DialogEx.DialogExViewScript.Instance.ShowLoading(true);
            Controller.OnHandleUIEvent("RequestHistoryPurchase");
        }


        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            DialogExViewScript.Instance.ShowLoading(false);
            DialogExViewScript.Instance.ShowNotification((string)param[0]);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowHistorys(object[] param)
        {
            chargingHistories = (ChargingHistory[])param[0];
            if(chargingHistories != null)
            {
                scrollCharingHistory.InitData(chargingHistories);
            }
            DialogExViewScript.Instance.ShowLoading(false);
        }

        private void OnDisable()
        {
            scrollCharingHistory.InitData(new ChargingHistory[0]);
        }

    }
}