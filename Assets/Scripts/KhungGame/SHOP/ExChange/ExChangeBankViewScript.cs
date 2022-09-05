using CoreBase;
using CoreBase.Controller;
using GameProtocol.COU;
using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.DialogEx;

namespace View.Home.ExChange
{
    public class ExChangeBankViewScript : ViewScript
    {

        [SerializeField] private Dropdown drpBankName;
        [SerializeField] private InputField ipfMoney;
        [SerializeField] private InputField ipfAccountBank;
        [SerializeField] private InputField ipfUserNameBank;
        [SerializeField] private InputField ipfOtp;

        [SerializeField] private Sprite[] sprBanks;
        [SerializeField] private int[] lstPricesExchange;
        [SerializeField] private CardRateView[] lstCardRatesExchange;

        //private BankInfo[] bankInfo;
        private int indexBank;

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
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
            StartCoroutine(Utils.DelayAction(RequestBankInfo, 0.05f));
        }

        private void RequestBankInfo()
        {
            DialogExViewScript.Instance.ShowLoading(true);
            Controller.OnHandleUIEvent("RequestBankInfo");
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowNotify(object[] param)
        {
            DialogExViewScript.Instance.ShowLoading(false);
            DialogExViewScript.Instance.ShowNotification((string)param[0]);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowBankInfo(object[] param)
        {
            //bankInfo = (BankInfo[])param[0];
            //if (bankInfo != null)
            //{
            //    for (int i = 0; i < bankInfo.Length; i++)
            //    {
            //        Dropdown.OptionData optionData = new Dropdown.OptionData(bankInfo[i].BankName, sprBanks[bankInfo[i].BankId]);
            //        drpBankName.options.Add(optionData);
            //    }
            //    drpBankName.value = 1;
            //    drpBankName.onValueChanged.AddListener((index) => {
            //        indexBank = index;
            //        InitRate();
            //    });
            //    InitRate();
            //}
            DialogExViewScript.Instance.ShowLoading(false);
        }

        public void OnBtnRequestExChangeClicked()
        {
            string value = ipfMoney.text;
            if (string.IsNullOrEmpty(value))
            {
                ShowNotification(Languages.Language.GetKey("LOBBY_SHOP_EXCHANGE_NO_INPUTMONEY"));
                return;
            }

            string accountbank = ipfAccountBank.text;
            if (string.IsNullOrEmpty(accountbank))
            {
                ShowNotification(Languages.Language.GetKey("LOBBY_SHOP_EXCHANGE_NO_INPUTACCOUNTNUMBER"));
                return;
            }

            int money = int.Parse(value);

            string accountName = ipfUserNameBank.text;
            if (string.IsNullOrEmpty(accountName))
            {
                ShowNotification(Languages.Language.GetKey("LOBBY_SHOP_EXCHANGE_NO_INPUTACCOUNTNAME"));
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

            //Controller.OnHandleUIEvent("RequestExChangeBank", money, bankInfo[indexBank], accountbank, accountName, otp);
        }

        private void ShowNotification(string message)
        {
            DialogExViewScript.Instance.ShowNotification(message);
        }

        private void InitRate()
        {
            //var rate = bankInfo[indexBank].Rate;
            //for (int i = 0; i < lstPricesExchange.Length; i++)
            //{
            //    lstCardRatesExchange[i].gameObject.SetActive(true);
            //    lstCardRatesExchange[i].ShowData(lstPricesExchange[i], rate);
            //}
        }
    }
}
