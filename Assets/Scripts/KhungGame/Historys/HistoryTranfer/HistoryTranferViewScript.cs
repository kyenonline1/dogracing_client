using CoreBase;
using CoreBase.Controller;
using GameProtocol.DIS;
using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.Home.History
{
    public class HistoryTranferViewScript : ViewScript
    {

        [SerializeField] private ScrollTranferHistoryView scrollTranferHistoryView;
        [SerializeField] private GameObject goNoData;

        public override void ClosePopup()
        {
            base.ClosePopup();
        }

        public override void OpenPopup()
        {
            base.OpenPopup();
        }

        protected override IController CreateController()
        {
            return new HistoryTranferController(this);
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

        private void OnEnable()
        {
            StartCoroutine(Utils.DelayAction(RequestHistoryTranfer, 0.01f));
        }

        private void RequestHistoryTranfer()
        {
            ShowLoading(true);
            Controller.OnHandleUIEvent("RequestHistoryTranfer");
        }

        private void ShowLoading(bool isShow)
        {
            DialogEx.DialogExViewScript.Instance.ShowLoading(isShow);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            ShowLoading(false);
            DialogEx.DialogExViewScript.Instance.ShowDialog((string)param[0]);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowHistorysTranfer(object[] param)
        {
            TranferHistory[] historys = (TranferHistory[])param[0];

            if (goNoData)
            {
                goNoData.SetActive(historys == null || historys.Length <= 0);
            }

            if(historys != null && historys.Length > 0)
            {
                if (scrollTranferHistoryView) scrollTranferHistoryView.InitData(historys);
            }

            ShowLoading(false);
        }

    }
}