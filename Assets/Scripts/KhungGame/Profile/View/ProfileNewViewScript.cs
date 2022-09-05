using AppConfig;
using AssetBundles;
using Base.Utils;
using Common.Card;
using Controller.Home.Profile;
using CoreBase;
using CoreBase.Controller;
using Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;
using View.DialogEx;

namespace View.Home.Profile
{
    public class ProfileNewViewScript : ViewScript
    {

        [SerializeField] private Image imgAvatar;
        [SerializeField] private Text txtMoney;
        [SerializeField] private Text txtUserName;
        [SerializeField] private Text txtNickName;
        [SerializeField] private Text txtID;
        [SerializeField] private Text txtEmail;
        [SerializeField] private UIToggleTabsCate cataAvatars;

        [SerializeField]
        private InputField ipEmail,
            ipNickName,
            ipPassword,
             ipRePassword,
              ipOldPassword;

        private int indexAvatar;

        public override void ClosePopup()
        {
            base.ClosePopup();
        }

        public override void OpenPopup()
        {
            base.OpenPopup();
            //if (ipNickName) ipNickName.gameObject.SetActive(ClientConfig.UserInfo.LOGIN_TYPE == ClientConfig.UserInfo.LoginType.TRIAL);
            //if (ipPassword) ipPassword.gameObject.SetActive(ClientConfig.UserInfo.LOGIN_TYPE == ClientConfig.UserInfo.LoginType.TRIAL);
        }

        public void OpenProfile(Action callback = null)
        {
            if (callback != null) callbackClosePopup = callback;
            OpenPopup();
        }

        protected override IController CreateController()
        {
            return new ProfileController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
            EventManager.Instance.UnSubscribeTopic(EventManager.CHANGE_AVATAR_TOPIC, LoadAvatar);
        }

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
            SetBaseInfo();
            EventManager.Instance.SubscribeTopic(EventManager.CHANGE_AVATAR_TOPIC, LoadAvatar);
        }

        private void OnEnable()
        {
            StartCoroutine(Utils.DelayAction(() => { RequestProfileInfo(); }, 0.05f));
        }

        private void RequestProfileInfo()
        {
            Controller.OnHandleUIEvent("RequestACC0GetInfo", ClientConfig.UserInfo.ID);
        }

        private void SetBaseInfo()
        {
            if (txtUserName) txtUserName.text = ClientConfig.UserInfo.UNAME;
            if (txtNickName) txtNickName.text = ClientConfig.UserInfo.NICKNAME;
            if (txtMoney) txtMoney.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.GOLD);
            if (txtID) txtID.text = string.Format("ID: {0}", ClientConfig.UserInfo.ID);
            SetActiveGmail();
            LoadAvatar();
        }

        private void SetActiveGmail()
        {
            Debug.Log("SetActiveGmail " + ClientConfig.UserInfo.EMAIL);
            if (!string.IsNullOrEmpty(ClientConfig.UserInfo.EMAIL))
            {
                if (ipEmail)
                {
                    ipEmail.text = ClientConfig.UserInfo.EMAIL;
                    ipEmail.interactable = false;
                    ipEmail.DeactivateInputField();
                }
            }
            else
            {
                if (ipEmail)
                {
                    ipEmail.text = "";
                    ipEmail.ActivateInputField();
                }

            }
        }

        private void LoadAvatar()
        {
            try
            {
                if (ClientConfig.UserInfo.LOGIN_TYPE == ClientConfig.UserInfo.LoginType.FACEBOOK)
                    StartCoroutine(ImageLoader.HTTPLoadImage(imgAvatar, ClientConfig.UserInfo.AVATAR));
                else
                {
                    int avtId = -1;
                    int.TryParse(ClientConfig.UserInfo.AVATAR, out avtId);
                       // string str = (int.Parse(ClientConfig.UserInfo.AVATAR) < 10) ? string.Format("avatar0{0}", ClientConfig.UserInfo.AVATAR) : string.Format("avatar{0}", ClientConfig.UserInfo.AVATAR);
                    if (imgAvatar != null) imgAvatar.sprite = CardAtlas.Instance.GetAvatar(avtId);
                   
                    //Debug.Log("Avtart: -0-------------------------   " + str);
                    //LoadAssetBundle.LoadSpriteToAtlas(TagAssetBundle.Tag_UI.TAG_UI_COMMON, TagAssetBundle.AtlasName.LOBBY_AVATAR, str, (sprite) => {
                    //});
                }
            }
            catch
            {

            }
        }

        private void ShowLoading(bool isShow)
        {
            if (DialogExViewScript.Instance) DialogExViewScript.Instance.ShowLoading(isShow);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UpdateDataProfile(object[] param)
        {
            string email = (string)param[0];
            if (txtEmail) txtEmail.text = string.IsNullOrEmpty(email) ? "Email: Chưa cập nhật." : string.Format("Email: {0}", email);
            SetBaseInfo();
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            ShowLoading(false);
            if (DialogExViewScript.Instance) DialogExViewScript.Instance.ShowDialog((string)param[0]);
        }


        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void CloseLoading(object[] param)
        {
            ShowLoading(false);
        }



        public void OnBtnReviewTheHandClicked()
        {
            ClosePopup();
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowReviewHand();
        }

        public void OnBtnLienHeClicked()
        {

        }

        public void OnBtnSettingClicked()
        {
            ClosePopup();
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowSetting();
        }

        public void OnBtnChangePasswordClicked()
        {
            ClosePopup();
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowChangePassword();
        }

        public void OnBtnSecurityClicked()
        {
            ClosePopup();
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowSecutiry();
        }

        public void OnBtnRedeemGiftCodeClicked()
        {
            ClosePopup();
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowGiftCode();
        }

        public void OnBtnCashInPaypalClicked()
        {
            ClosePopup();
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowShop(CateShop.IAP);
        }

        public void OnBtnEditProfileClicked()
        {
            if (cataAvatars)
            {
                int avatardefault = 0;
                if (!ClientConfig.UserInfo.AVATAR.Contains("http"))
                    int.TryParse(ClientConfig.UserInfo.AVATAR, out avatardefault);
                cataAvatars.SelectCate(avatardefault);
            }
        }

        public void OnBtnSelectedAvatarClicked(int index)
        {
            indexAvatar = index;
        }

        public void OnBtnConfirmChangeAvatarClicked()
        {
            if (ClientConfig.UserInfo.AVATAR.Equals(indexAvatar.ToString())) return;
            ShowLoading(true);
            Controller.OnHandleUIEvent("ChangeAvatar", indexAvatar);
        }


        public void OnBtnClickChangeInfoClicked()
        {
            string email = string.IsNullOrEmpty(ClientConfig.UserInfo.EMAIL) ? ipEmail.text : string.Empty;
            string password = ipPassword.text;
            string nickname = ipNickName.text;

            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password) && string.IsNullOrEmpty(nickname))
            {
                DialogExViewScript.Instance.ShowDialog("Please enter information to update.");
                return;
            }

            if (!string.IsNullOrEmpty(password))
            {
                string repass = ipRePassword.text;
                if (string.IsNullOrEmpty(repass))
                {
                    DialogExViewScript.Instance.ShowDialog("Please enter a new password to update.");
                    return;
                }
                else
                {
                    if (!repass.Equals(password))
                    {
                        DialogExViewScript.Instance.ShowDialog("New password does not match.");
                        return;
                    }
                }
                string oldpass = ipOldPassword.text;
                if (string.IsNullOrEmpty(oldpass))
                {
                    DialogExViewScript.Instance.ShowDialog("Please enter old password to update.");
                    return;
                }
                else
                {
                    if (!oldpass.Equals(ClientConfig.UserInfo.PASSWORD))
                    {
                        DialogExViewScript.Instance.ShowDialog("Old password does not match.");
                        return;
                    }
                }
            }

            ShowLoading(true);

            Controller.OnHandleUIEvent("ChangeInfoClicked", nickname, email, password);
        }
    }
}
