using AppConfig;
using CoreBase;
using CoreBase.Controller;
using Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.DialogEx;

namespace View.Home.UpdateInfo
{
    public class ChangePasswordView : ViewScript
    {

        public delegate void ChangePass(string password, int otp);
        public ChangePass dlgChangePass;
        public Action actionGetOTP;
        public InputField inputOldPass,
            inputNewPass,
            inputReNewPass,
            inputOTP;

        public Text txtButtonOTP;

        public UIMyButton btnGetOtp,
            btnChangePassword;


        private void ClickGetOTP()
        {
            if (actionGetOTP != null) actionGetOTP();
            DisAbleButtonOTP(false);
            Timer = 60;
            ShowTimer();
            InvokeRepeating("ShowTimer", 1f, 1f);
        }

        private int Timer = 0;

        private void DisAbleButtonOTP(bool show)
        {
            btnGetOtp.interactable = show;
            btnGetOtp.GetComponent<UIMyButton>().enabled = show;
            btnGetOtp.GetComponentInChildren<Image>().color = show ? Color.white : new Color(1, 1, 1, 0.5f);
        }

        private void ShowTimer()
        {
            //Debug.Log("Show Timer : " + Timer);
            if (Timer <= 0)
            {
                CancelInvoke("ShowTimer");
                DisAbleButtonOTP(true);
                Timer = 0;
                if (txtButtonOTP)
                    txtButtonOTP.text = "Lấy OTP";
            }
            else
            {
                Timer--;
                if (Timer <= 0)
                {
                    CancelInvoke("ShowTimer");
                    DisAbleButtonOTP(true);
                    Timer = 0;
                    if (txtButtonOTP)
                        txtButtonOTP.text = "LẤY OTP";
                    return;
                }
                txtButtonOTP.text = string.Format("{0}({1})", "LẤY OTP", Timer);
            }
        }

        private void ClickChangePassword()
        {
            Debug.Log("Click change password");
            string oldpass = inputOldPass.text;
            if (string.IsNullOrEmpty(oldpass))
            {
                DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("HOME_CHANGE_PASS_NO_INPUTPASS"));
                return;
            }
            if (!oldpass.Equals(ClientConfig.UserInfo.PASSWORD))
            {
                DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("HOME_CHANGE_PASS_NO_PASS_NOT_MATCH"));
                return;
            }

            string newpass = inputNewPass.text;
            if (string.IsNullOrEmpty(newpass))
            {
                DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("HOME_CHANGE_PASS_NO_INPUT_NEWPASS"));
                return;
            }

            string renewpass = inputReNewPass.text;
            if (string.IsNullOrEmpty(renewpass))
            {
                DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("HOME_CHANGE_PASS_NO_INPUT_RENEWPASS"));
                return;
            }

            if (!newpass.Equals(renewpass))
            {
                DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("HOME_CHANGE_PASS_NEW_PASS_NOT_MATCH"));
                return;
            }

            int otp = 0;

            if (inputOTP.gameObject.activeSelf)
            {
                string strotp = inputOTP.text;
                if (string.IsNullOrEmpty(strotp))
                {
                    DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("HOME_DIALOG_TYPE_OTP"));
                    return;
                }
                int.TryParse(strotp, out otp);
            }

            Controller.OnHandleUIEvent("RequestChangePassword", newpass, otp);
           
        }

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
            btnGetOtp._onClick.AddListener(ClickGetOTP);
            btnChangePassword._onClick.AddListener(ClickChangePassword);
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
            return new ChangePasswordController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            if (inputOldPass) inputOldPass.text = string.Empty;
            if (inputNewPass) inputNewPass.text = string.Empty;
            if (inputReNewPass) inputReNewPass.text = string.Empty;
            if (inputOTP) inputOTP.text = string.Empty;
            ShowLoading(false);
            if (DialogExViewScript.Instance) DialogExViewScript.Instance.ShowDialog((string)param[0]);
        }

        private void ShowLoading(bool isShow)
        {
            if (DialogExViewScript.Instance) DialogExViewScript.Instance.ShowLoading(isShow);
        }
    }
}