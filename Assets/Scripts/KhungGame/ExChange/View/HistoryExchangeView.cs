using CoreBase;
using CoreBase.Controller;
using GameProtocol.COU;
using Interface;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using View.DialogEx;

namespace View.Common
{
    public class HistoryExchangeView : ViewScript
    {

        [SerializeField] private ScrollExChangeHistory scrollExChangeHistory;
        CashoutHistory[] cashoutHistories;


        protected override IController CreateController()
        {
            return new HistoryExchangeController(this);
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
            Controller.OnHandleUIEvent("RequestHistoryExChange");
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
            cashoutHistories = (CashoutHistory[])param[0];
            if (cashoutHistories != null)
            {
                scrollExChangeHistory.InitData(cashoutHistories);
            }
            DialogExViewScript.Instance.ShowLoading(false);
        }

        private void OnDisable()
        {
            scrollExChangeHistory.InitData(new CashoutHistory[0]);
        }
    }
}
