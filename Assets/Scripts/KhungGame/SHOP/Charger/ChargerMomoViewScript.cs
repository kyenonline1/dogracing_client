using CoreBase;
using CoreBase.Controller;
using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.DialogEx;

namespace View.Home.Shop
{
    public class ChargerMomoViewScript : ViewScript
    {
        [Header("Momo")]
        [SerializeField] private Text txtNumberPhone;
        [SerializeField] private Text txtUserHolder;
        [SerializeField] private Text txtDescription;
        [SerializeField] private Text txtMinMaxMomo;
        [SerializeField]
        private CardRateView[] lstCardRatesMomo;
        [SerializeField]
        private int[] pricesMomo;
        [SerializeField]
        private GameObject objCopySuccess;

        protected override IController CreateController()
        {
            return new ChargerMomoController(this);
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
            StartCoroutine(Utils.DelayAction(GetInfoMomo, 0.01f));
        }

        void GetInfoMomo()
        {
            DialogExViewScript.Instance.ShowLoading(true);
            Controller.OnHandleUIEvent("RequestInfoMomo");
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowNotify(object[] data)
        {
            DialogExViewScript.Instance.ShowLoading(false);
            DialogExViewScript.Instance.ShowNotification((string)data[0]);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UpdateMomoInfo(object[] param)
        {
            DialogExViewScript.Instance.ShowLoading(false);
            if (txtNumberPhone) txtNumberPhone.text = (string)param[0];
            if (txtUserHolder) txtUserHolder.text = (string)param[1];
            float rate = (float)param[2];
            if (txtDescription) txtDescription.text = (string)param[3];
            int mincashin = (int)param[4];
            int maxcashin = (int)param[5];
            if (txtMinMaxMomo) txtMinMaxMomo.text = string.Format(Languages.Language.GetKey("LOBBY_CHARGE_MOMO_HAN_MUC_GIAO_DICH"), mincashin, maxcashin);
            for (int i = 0; i < lstCardRatesMomo.Length; i++)
            {
                lstCardRatesMomo[i].ShowData(pricesMomo[i], rate);
            }
        }

        public void CopyStkMomo()
        {
            string phone = txtNumberPhone.text;
            if (string.IsNullOrEmpty(phone)) return;
            Clipboard.SetText(phone);
            objCopySuccess.SetActive(true);
            if (coroutineCoppy != null) StopCoroutine(coroutineCoppy);
            coroutineCoppy = StartCoroutine(HideCoppy());
        }

        public void CopyDescriptionMomo()
        {
            string des = txtDescription.text;
            if (string.IsNullOrEmpty(des)) return;
            Clipboard.SetText(des);
            objCopySuccess.SetActive(true);
            if (coroutineCoppy != null) StopCoroutine(coroutineCoppy);
            coroutineCoppy = StartCoroutine(HideCoppy());
        }

        Coroutine coroutineCoppy;

        IEnumerator HideCoppy()
        {
            yield return new WaitForSeconds(3f);
            objCopySuccess.SetActive(false);
        }
    }
}
