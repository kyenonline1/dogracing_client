using CoreBase;
using CoreBase.Controller;
using GameProtocol.COU;
using Interface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utilites;
using View.DialogEx;

namespace View.Home.ExChange
{
    public class ExChangeCardViewScript : ViewScript
    {
        [SerializeField] private Dropdown drpCateTelco;
        [SerializeField] private Dropdown drpPrice;
        [SerializeField] private CardRateView[] lstCardRatesExchange;
        [SerializeField] private InputField ipfOTP;

        [SerializeField] private Sprite[] sprTelcos;

        TelcoDetail[] telcoDetails;
        private int indexTelco;
        private int indexPrice;
        private float rateCard;


        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
            drpCateTelco.onValueChanged.AddListener((index) =>
            {
                indexTelco = index;
                InitPriceByTelco();
            });

            drpPrice.onValueChanged.AddListener((index) =>
            {
                indexPrice = index;
            });
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
        }

        protected override IController CreateController()
        {
            return new ExChangeController(this);
        }

        private void OnEnable()
        {

            StartCoroutine(Utils.DelayAction(GetTelcos, 0.05f));
        }

        void GetTelcos()
        {
            Controller.OnHandleUIEvent("RequestGetTelco");
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowNotify(object[] param)
        {
            DialogExViewScript.Instance.ShowLoading(false);
            DialogExViewScript.Instance.ShowNotification((string)param[0]);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowTelcos(object[] param)
        {
            try
            {
                telcoDetails = (TelcoDetail[])param[0];
                rateCard = (float)param[1];
                for (int i = 0; i < lstCardRatesExchange.Length; i++) lstCardRatesExchange[i].gameObject.SetActive(false);

                if (telcoDetails != null && telcoDetails.Length > 0)
                {
                    drpCateTelco.options.Clear();
                    for (int i = 0; i < telcoDetails.Length; i++)
                    {
                        //Debug.Log("ShowTelcos ------------- : " + telcoDetails[i].TelcoName + " - " + telcoDetails[i].TelcoId);
                        //drpCateTelco.options.Add(new Dropdown.OptionData(telcoDetails[i].TelcoName, sprTelcos[telcoDetails[i].Type]));
                    }
                    drpCateTelco.value = 0;
                    indexTelco = 0;
                    InitPriceByTelco();
                }
                DialogExViewScript.Instance.ShowLoading(false);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Exception ShowTelcos: " + ex.Message);
                Debug.LogError("Exception ShowTelcos - StackTrace : " + ex.StackTrace);
            }
        }

        private void InitPriceByTelco()
        {
            if(telcoDetails != null &&  indexTelco < telcoDetails.Length)
            {
                var prices = telcoDetails[indexTelco].Items;
                drpPrice.options.Clear();
                for(int i = 0; i < prices.Length; i++)
                {
                    drpPrice.options.Add(new Dropdown.OptionData(MoneyHelper.FormatNumberAbsolute(prices[i])));
                }

                drpPrice.value = 0;
                indexPrice = 0;
                ResetCardRate();
                for (int i = 0; i < prices.Length; i++)
                {
                    lstCardRatesExchange[i].gameObject.SetActive(true);
                    lstCardRatesExchange[i].ShowData(prices[i], rateCard);
                }
            }
        }

        private void ResetCardRate()
        {
            for (int i = 0; i < lstCardRatesExchange.Length; i++)
            {
                lstCardRatesExchange[i].gameObject.SetActive(false);
            }
        }

        private void InitDataCardRate(int[] cards, float rate)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                lstCardRatesExchange[i].gameObject.SetActive(true);
                lstCardRatesExchange[i].ShowData(cards[i], rate);
            }
        }


        public void OnBtnConfirmExchangeClicked()
        {
            string otp = string.Empty;
            if (ipfOTP.gameObject.activeSelf)
            {
                otp = ipfOTP.text;
                if (string.IsNullOrEmpty(otp))
                {
                    DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("UPDATEINFO_INPUTOTP"));
                    return;
                }
            }
            Controller.OnHandleUIEvent("RequestExChangeCard", telcoDetails[indexTelco].TelcoId, telcoDetails[indexTelco].Items[indexPrice], otp);
        }

    }
}