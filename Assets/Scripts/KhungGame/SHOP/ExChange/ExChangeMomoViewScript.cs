using CoreBase;
using CoreBase.Controller;
using Game.Gameconfig;
using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.DialogEx;

namespace View.Home.ExChange
{
    public class ExChangeMomoViewScript : ViewScript
    {
        [SerializeField] private InputField ipfMoney;
        [SerializeField] private InputField ipfAccountMomo;
        [SerializeField] private InputField ipfOtp;
        [SerializeField] private int[] lstPricesExchange;
        [SerializeField] private CardRateView[] lstCardRatesExchange;


        protected override IController CreateController()
        {
            return new ExChangeController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
        }

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
            InitRate();
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
        }

        public void OnBtnRequestExChangeMomoClicked()
        {
            string value = ipfMoney.text;
            if (string.IsNullOrEmpty(value))
            {
                ShowNotification(Languages.Language.GetKey("LOBBY_SHOP_EXCHANGE_NO_INPUTMONEY"));
                return;
            }

            int money = int.Parse(value);

            string account = ipfAccountMomo.text;
            if (string.IsNullOrEmpty(account))
            {
                ShowNotification(Languages.Language.GetKey("LOBBY_SHOP_EXCHANGE_NO_INPUT_MOMO_ACCOUNT"));
                return;
            }

            string otp = string.Empty;
            if (ipfOtp.gameObject.activeSelf)
            {
                otp = ipfOtp.text;
                if (string.IsNullOrEmpty(otp))
                {
                    ShowNotification(Languages.Language.GetKey("UPDATEINFO_INPUTOTP"));
                    return;
                }
            }

            DialogExViewScript.Instance.ShowLoading(true);
            Controller.OnHandleUIEvent("RequestExChangeItem", money, account);
        }

        private void ShowNotification(string message)
        {
            DialogExViewScript.Instance.ShowNotification(message);
        }

        private void InitRate()
        {
            var rate = ClientGameConfig.EXCHANGE_TYPE.rateMomo;
            for (int i = 0; i < lstPricesExchange.Length; i++)
            {
                lstCardRatesExchange[i].gameObject.SetActive(true);
                lstCardRatesExchange[i].ShowData(lstPricesExchange[i], rate);
            }
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowNotify(object[] param)
        {
            DialogExViewScript.Instance.ShowLoading(false);
            ShowNotification((string)param[0]);
        }
    }
}