using AppConfig;
using Base;
using Controller.Home.UpdateInfo;
using CoreBase;
using CoreBase.Controller;
using Interface;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using View.DialogEx;

namespace View.Home.UpdateInfo
{
    public class UpdateInfoViewScript : ViewScript
    {
        [SerializeField] private UIToggleTabsCate cates;

        [SerializeField] private InputField inputPhoneNumber, inputOTP;


        [SerializeField] private InputField inputPhoneNumberChange;
        [SerializeField] private InputField inputOtpChange;

        //public Text txtButtonOTP;

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
        }

        public override void OpenPopup()
        {
            base.OpenPopup();
        }

        public override void ClosePopup()
        {
            base.ClosePopup();
        }

        protected override IController CreateController()
        {
            return new UpdateInfoController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
        }

        private void OnEnable()
        {
            //Debug.Log("Phone Number: " + ClientConfig.UserInfo.PHONE); 
            if (cates) cates.SelectCate(0);

            if (!string.IsNullOrEmpty(ClientConfig.UserInfo.PHONE))
            {
                if (ClientConfig.UserInfo.PHONE.Length > 9)
                {
                    string phone = ClientConfig.UserInfo.PHONE.Substring(ClientConfig.UserInfo.PHONE.Length - 4);
                    inputPhoneNumber.text = "******" + phone;
                }
                else inputPhoneNumber.text = ClientConfig.UserInfo.PHONE;
            }

            if (inputOTP) inputOTP.text = string.Empty;
            if (inputOtpChange) inputOtpChange.text = string.Empty;
            if (inputPhoneNumberChange) inputPhoneNumberChange.text = string.Empty;
        }

        //private void OnDisable()
        //{
        //    DisAbleButtonOTP(true);
        //}


        public void BtnClickUpdatePhone()
        {
            string phone = inputPhoneNumber.text;
            string _otp = inputOTP.text;
            if (string.IsNullOrEmpty(phone))
            {
                DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("HOME_CHANGE_PHONE_NO_INPUT_PHONE"));
            }
            else if (string.IsNullOrEmpty(_otp))
            {
                DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("HOME_CHANGE_PHONE_NO_INPUT_OTP"));
            }
            else
            {
                if(phone.Length < 9 || !Regex.IsMatch(phone.ToLower(), "^[0-9]*$"))
                {
                    DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("HOME_CHANGE_PHONE_NO_INPUT_VALIDATE"));
                    return;
                }
                DialogExViewScript.Instance.ShowLoading(true);
                int otp = -1;
                int.TryParse(_otp,out otp);
                inputOTP.text = string.Empty;
                Controller.OnHandleUIEvent("UpdatePhone", phone, otp);
            }
        }

        public void BtnClickGetOTP()
        {
            string phone = string.IsNullOrEmpty(ClientConfig.UserInfo.PHONE) ?  inputPhoneNumber.text : ClientConfig.UserInfo.PHONE;
            if (string.IsNullOrEmpty(phone))
            {
                DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("HOME_CHANGE_PHONE_NO_INPUT_PHONE"));
                return;
            }
            if (phone.Length < 9 || !Regex.IsMatch(phone.ToLower(), "^[0-9]*$"))
            {
                DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("HOME_CHANGE_PHONE_NO_INPUT_VALIDATE"));
                return;
            }
            DialogExViewScript.Instance.ShowLoading(true);
            Controller.OnHandleUIEvent("RequestOTP", phone);
        }

        public void OnBtnClickChangePhone()
        {
            string phone = inputPhoneNumberChange.text;
            string _otp = inputOtpChange.text;
            if (string.IsNullOrEmpty(phone))
            {
                DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("HOME_CHANGE_PHONE_NO_INPUT_PHONE"));
            }
            else if (string.IsNullOrEmpty(_otp))
            {
                DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("HOME_CHANGE_PHONE_NO_INPUT_OTP"));
            }
            else
            {
                if (phone.Length < 9 || !Regex.IsMatch(phone.ToLower(), "^[0-9]*$"))
                {
                    DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("HOME_CHANGE_PHONE_NO_INPUT_VALIDATE"));
                    return;
                }
                DialogExViewScript.Instance.ShowLoading(true);
                int otp = -1;
                int.TryParse(_otp, out otp);
                inputOtpChange.text = string.Empty;
                Controller.OnHandleUIEvent("UpdatePhone", phone, otp);
            }
        }

        public void BtnClickGetOTPChangePhone()
        {

            if (string.IsNullOrEmpty(ClientConfig.UserInfo.PHONE))
            {
                DialogExViewScript.Instance.ShowDialog(Languages.Language.GetKey("HOME_CHANGE_PHONE_NO_UPDATE_PHONE"), ()=> {
                    if (cates) cates.SelectCate(0);
                }, OkLabel: Languages.Language.GetKey("HOME_SECURITY_UPDATE_PHONE"));
                return;
            }

            //TODO: lấy OTP từ sđt cũ
           
            DialogExViewScript.Instance.ShowLoading(true);
            Controller.OnHandleUIEvent("RequestOTP", ClientConfig.UserInfo.PHONE);
        }


        private int Timer = 0;

        private void DisAbleButtonOTP(bool show)
        {
            //btnGetOTP.interactable = show;
            //btnGetOTP.GetComponent<UIMyButton>().enabled = show;
            //btnGetOTP.GetComponentInChildren<Image>().color = show ? Color.white : new Color(1, 1, 1, 0.5f);
        }

        private void ShowTimer()
        {
            //Debug.Log("Show Timer : " + Timer);
            if (Timer <= 0)
            {
                CancelInvoke("ShowTimer");
                DisAbleButtonOTP(true);
                Timer = 0;
                //if (txtButtonOTP)
                //    txtButtonOTP.text = "Lấy OTP";
            }
            else
            {
                Timer--;
                if (Timer <= 0)
                {
                    CancelInvoke("ShowTimer");
                    DisAbleButtonOTP(true);
                    Timer = 0;
                    //if (txtButtonOTP)
                    //    txtButtonOTP.text = "LẤY OTP";
                    return;
                }
                //txtButtonOTP.text = string.Format("{0}({1})", "LẤY OTP", Timer);
            }
        }
        
        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UpdateInfoSuccess(object[] param)
        {
            ClientConfig.UserInfo.PHONE = inputPhoneNumber.text;
            inputPhoneNumber.text = string.Empty;
            DialogExViewScript.Instance.ShowLoading(false);
            DialogExViewScript.Instance.ShowNotification((string)param[0]);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            DialogExViewScript.Instance.ShowNotification((string)param[0]);
            DialogExViewScript.Instance.ShowLoading(false);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void CloseLoading(object[] param)
        {
            DialogExViewScript.Instance.ShowLoading(false);
        }

       
    }
}
