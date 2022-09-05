
using Controller.Home.GiftCode;
using CoreBase;
using CoreBase.Controller;
using Interface;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using View.DialogEx;

namespace View.Home.GiftCode
{
    public class GiftCodeViewScript : ViewScript
    {

        private InputField inputGiftCode, inputCapcha;

        private UIMyButton btnClose, btnConfirm, btnRefreshCapcha;

        private GameObject popup;

        //public Text txtCapcha;
        private Image imageCapcha;

        private Sprite nomarl;

        // Use this for initialization

        protected override IController CreateController()
        {
            return new GiftCodeController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
        }

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
            InitProperties();
            AddListenerButton();
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
        }

#if UNITY_ANDROID || UNITY_IOS
        private void FixedUpdate()
        {
            if (inputCapcha != null && inputCapcha.isFocused) popup.transform.localPosition = new Vector3(0, 300, 0);
            else popup.transform.localPosition = Vector3.zero;
        }
#endif

        

        private void InitProperties()
        {
            popup = transform.Find("Popup").gameObject;
            inputGiftCode = transform.Find("Popup/Content/InputCode").GetComponent<InputField>();
            inputCapcha = transform.Find("Popup/Content/InputCapcha").GetComponent<InputField>();

            btnClose = transform.Find("Popup/ButtonClose").GetComponent<UIMyButton>();
            btnConfirm = transform.Find("Popup/Content/BtnOK").GetComponent<UIMyButton>();
            btnRefreshCapcha = transform.Find("Popup/Content/BtnRefreshCapcha").GetComponent<UIMyButton>();

            imageCapcha = transform.Find("Popup/Content/imgCapcha").GetComponent<Image>();
            nomarl = imageCapcha.sprite;
            
        }

        private void AddListenerButton()
        {
            btnClose._onClick.RemoveAllListeners();
            btnClose._onClick.AddListener(BtnClickClose);
            btnConfirm._onClick.RemoveAllListeners();
            btnConfirm._onClick.AddListener(BtnClickConfirm);
            btnRefreshCapcha._onClick.RemoveAllListeners();
            btnRefreshCapcha._onClick.AddListener(BtnClickRefreshCapcha);
        }

        private void BtnClickClose()
        {
            gameObject.SetActive(false);
        }

        private void BtnClickConfirm()
        {
            string giftcode = inputGiftCode.text;
            string capcha = inputCapcha.text;
            if (string.IsNullOrEmpty(giftcode))
            {
                DialogExViewScript.Instance.ShowNotification("Vui lòng nhập Mã Gift Code.");
            }
            else
            {
                if(!Regex.IsMatch(giftcode.ToLower(), "^[a-z0-9]*$"))
                {
                    DialogExViewScript.Instance.ShowNotification("Gift Code không hợp lệ. Gift Code gồm các chữ cái và các số 0-9. Không bao gồm các ký tự đặc biệt.");
                    return;
                }
                if (string.IsNullOrEmpty(capcha))
                {
                    DialogExViewScript.Instance.ShowNotification("Vui lòng nhập Mã Capcha.");
                }
                else
                {
                    ShowLoading(true);
                    Controller.OnHandleUIEvent("RequestGiftCodeATH9", giftcode, capcha);
                }
            }
        }

        private void BtnClickRefreshCapcha()
        {
            RefreshCapcha();
        }

        private void ClearInput()
        {
            inputGiftCode.text = string.Empty;
            inputCapcha.text = string.Empty;
        }

        private void ShowLoading(bool isShow)
        {
            DialogExViewScript.Instance.ShowLoading(isShow);
        }

        private void RefreshCapcha()
        {
            imageCapcha.sprite = nomarl;
            //txtCapcha.text = "";
            Controller.OnHandleUIEvent("RefreshCapcha");
        }

        private IEnumerator LoadCapcha(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                WWW www = new WWW(url);
                yield return www;
                if (string.IsNullOrEmpty(www.error))
                {
                    if (imageCapcha != null)
                    {
                        imageCapcha.sprite = Sprite.Create(www.texture, new Rect(0, 0, 150, 80), Vector2.zero);
                    }
                }
                else DialogExViewScript.Instance.ShowNotification("Tải capcha thất bại");
            }
            yield return null;
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UpdateCapcha(object[] param)
        {
            string url = (string)param[0];
            //txtCapcha.text = url;
            //Debug.Log("UpdateCapcha : " + url + " , img " + (imageCapcha == null));
            StartCoroutine(LoadCapcha(url));
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            DialogExViewScript.Instance.ShowLoading(false);
            DialogExViewScript.Instance.ShowNotification((string)param[0]);
            RefreshCapcha();
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ReceiveGiftCodeSuccess(object[] param)
        {
            DialogExViewScript.Instance.ShowLoading(false);
            ClearInput();
            RefreshCapcha();
        }

        public void OpenGiftCode()
        {
            if (btnRefreshCapcha == null) InitProperties();
            Invoke("RefreshCapcha", 0.3f);
        }
    }
}
