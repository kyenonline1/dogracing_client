using CoreBase;
using CoreBase.Controller;
using GameProtocol.PAY;
using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Utilites;

namespace View.Home.Shop
{
    public class ChargerCardViewScript : ViewScript
    {
        [SerializeField] private Dropdown drdTelco;
        [SerializeField] private Dropdown drdPriceTelco;
        [SerializeField] private InputField ipfSerial;
        [SerializeField] private InputField ipfCardNumber;
        [SerializeField] private InputField ipfCapcha;
        [SerializeField] private Image imgCapcha;

        [SerializeField]
        private CardRateView[] lstCardRates;

        [SerializeField] private Sprite[] sprTelcos; 

        private int indexTelco;
        private int indexPriceTelco;

        private float rateChargerCard;
        private CateCharging[] cates;

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
            return new ChargerCardController(this);
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
            RequestTelcoCharge();
        }

        private void OnEnable()
        {
            StartCoroutine(Utils.DelayAction(RequestCapcha, 0.01f));
            if (ipfSerial) ipfSerial.text = string.Empty;
            if (ipfCardNumber) ipfCardNumber.text = string.Empty;
            if (ipfCapcha) ipfCapcha.text = string.Empty;
        }

        private void ShowLoading(bool isShow)
        {
            DialogEx.DialogExViewScript.Instance.ShowLoading(isShow);
        }

        private void ShowNotification(string message)
        {
            DialogEx.DialogExViewScript.Instance.ShowNotification(message);
        }

        private void ResetCardRate()
        {
            for (int i = 0; i < lstCardRates.Length; i++)
            {
                lstCardRates[i].gameObject.SetActive(false);
            }
        }

        private void InitDataCardRate(int[] cards, float rate)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                lstCardRates[i].gameObject.SetActive(true);
                lstCardRates[i].ShowData(cards[i], rate);
            }
        }


        private void OnValueTelcoChange(int index)
        {
            indexTelco = index;
            ResetCardRate();
            InitDataCardRate(cates[indexTelco].Amounts, rateChargerCard);

            drdPriceTelco.options = new List<Dropdown.OptionData>();
            for (int i = 0; i < cates[indexTelco].Amounts.Length; i++)
            {
                drdPriceTelco.options.Add(new Dropdown.OptionData(MoneyHelper.FormatNumberAbsolute(cates[indexTelco].Amounts[i])));
            }
            drdPriceTelco.onValueChanged.AddListener(OnValuePriceTelcoChange);

            drdPriceTelco.value = 1;
            drdPriceTelco.value = 0;
        }

        private void OnValuePriceTelcoChange(int index)
        {
            indexPriceTelco = index;
        }

        private void RequestTelcoCharge()
        {
            ShowLoading(true);
            Controller.OnHandleUIEvent("RequestPAY0GetCardInfo");
        }

        private void RequestCapcha()
        {
            Controller.OnHandleUIEvent("RequestCapcha");
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            ShowLoading(false);
            ShowNotification((string)param[0]);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowDataCardInfo(object[] data)
        {
            ShowLoading(false);
            ResetCardRate();
            cates = (CateCharging[])data[0];
            for (int i = 0; i < cates.Length; i++)
            {
                Debug.Log("CateName: " + cates[i].Name + " - cateType:  " + cates[i].Type);
            }
            if (cates != null)
            {
                rateChargerCard = (float)data[1];
                if (cates != null && cates.Length > 0)
                {
                    InitDataCardRate(cates[0].Amounts, rateChargerCard);
                    drdTelco.options = new List<Dropdown.OptionData>();
                    for (int i = 0; i < cates.Length; i++)
                    {
                        drdTelco.options.Add(new Dropdown.OptionData(cates[i].Name, sprTelcos[cates[i].Type]));
                    }

                    drdTelco.onValueChanged.AddListener(OnValueTelcoChange);

                    drdTelco.value = 1;
                    drdTelco.value = 0;
                }
            }
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowCapcha(object[] param)
        {
            string url = (string)param[0];
            //txtCapcha.text = url;
            StartCoroutine(LoadCapcha(url));
        }

        private IEnumerator LoadCapcha(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                using (UnityWebRequest wr = new UnityWebRequest(url))
                {
                    DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
                    wr.downloadHandler = texDl;
                    yield return wr.SendWebRequest();
                    if (!(wr.isNetworkError || wr.isHttpError))
                    {
                        Texture2D t = texDl.texture;
                        Sprite s = Sprite.Create(t, new Rect(0, 0, t.width, t.height),
                                                 Vector2.zero, 1f);
                        imgCapcha.sprite = s;
                    }
                    else ShowNotification(Languages.Language.GetKey("HOME_DIALOG_LOADCAPCHA_ERROR"));
                }
            }
            yield return null;
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ClearData(object[] data)
        {
            ipfCardNumber.text = ipfSerial.text = ipfCapcha.text = "";
            RequestCapcha();
        }

        public void OnBtnResetCapchaClicked()
        {
            ShowLoading(true);
            RequestCapcha();
        }

        public void OnBtnRequestChargeCardClicked()
        {
            string serial = ipfSerial.text;
            if (string.IsNullOrEmpty(serial))
            {
                ShowNotification(Languages.Language.GetKey("LOBBY_CHARGE_NO_INPUT_SERIAL"));
                return;
            }

            string cardnumber = ipfCardNumber.text;
            if (string.IsNullOrEmpty(cardnumber))
            {
                ShowNotification(Languages.Language.GetKey("LOBBY_CHARGE_NO_INPUT_CARD_NUMBER"));
                return;
            }

            string capcha = ipfCapcha.text;
            if (string.IsNullOrEmpty(capcha))
            {
                ShowNotification(Languages.Language.GetKey("LOBBY_TRANFER_NO_CAPCHA"));
                return;
            }

            Controller.OnHandleUIEvent("RequestNapThePAY2", cates[indexTelco].Name, serial, cardnumber, cates[indexTelco].Amounts[indexPriceTelco], capcha, 0);
        }
    }
}
