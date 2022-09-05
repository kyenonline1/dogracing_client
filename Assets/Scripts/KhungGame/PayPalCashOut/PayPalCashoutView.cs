using AppConfig;
using Controller.Home;
using CoreBase;
using CoreBase.Controller;
using GameProtocol.COU;
using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.DialogEx;

namespace View.Home
{
    public class PayPalCashoutView : ViewScript
    {
        [SerializeField] private InputField ipFirstName;
        [SerializeField] private InputField ipLastName;
        [SerializeField] private InputField ipEmail;
        [SerializeField] private InputField ipGoldCashOut;
        [SerializeField]
        private CardRateView[] lstCardRatesPaypal;

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
            ipGoldCashOut.onValueChanged.AddListener(OnValueChangeInputGold);
        }

        protected override IController CreateController()
        {
            return new PayPalCashoutController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
        }

        private void OnEnable()
        {
            ClearData(null);
        }

        public override void OpenPopup()
        {
            base.OpenPopup();
        }

        public override void ClosePopup()
        {
            base.ClosePopup();
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            DialogExViewScript.Instance.ShowLoading(false);
            DialogExViewScript.Instance.ShowNotification((string)param[0]);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowCateInfo(object[] param)
        {
            TelcoDetail telcoDetails = (TelcoDetail)param[0];
            float rate = (float)param[1];

            DisableListCardRate();

            for(int i = 0; i < telcoDetails.Items.Length; i++)
            {
                lstCardRatesPaypal[i].gameObject.SetActive(true);
                lstCardRatesPaypal[i].ShowData(telcoDetails.Items[i], rate);
            }

            DialogExViewScript.Instance.ShowLoading(false);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ClearData(object[] param)
        {
            ipFirstName.text = ipLastName.text = ipEmail.text = ipGoldCashOut.text = "";
        }


        public void OnBtnConfirmCashoutPaypalClicked()
        {
            var fistname = ipFirstName.text;
            if (string.IsNullOrEmpty(fistname))
            {
                DialogExViewScript.Instance.ShowNotification("Typing Paypal First Name");
                return;
            }

            var lastname = ipLastName.text;
            if (string.IsNullOrEmpty(lastname))
            {
                DialogExViewScript.Instance.ShowNotification("Typing Paypal Last Name");
                return;
            }

            var email = ipEmail.text;
            if (string.IsNullOrEmpty(email))
            {
                DialogExViewScript.Instance.ShowNotification("Typing Email");
                return;
            }

            var money = ipGoldCashOut.text;
            if (string.IsNullOrEmpty(email))
            {
                DialogExViewScript.Instance.ShowNotification("Typing Gold");
                return;
            }

            int gold = 0;
            int.TryParse(money, out gold);
            DialogExViewScript.Instance.ShowLoading(true);
            Controller.OnHandleUIEvent("RequestCashout", fistname, lastname, email, gold);
        }

        private void OnValueChangeInputGold(string value)
        {
            long gold = 0;
            long.TryParse(value, out gold);

            if (gold > ClientConfig.UserInfo.GOLD) ipGoldCashOut.text = ClientConfig.UserInfo.GOLD.ToString();
        }

        private void DisableListCardRate()
        {
            for (int i = 0; i < lstCardRatesPaypal.Length; i++) lstCardRatesPaypal[i].gameObject.SetActive(false);
        }
    }
}
