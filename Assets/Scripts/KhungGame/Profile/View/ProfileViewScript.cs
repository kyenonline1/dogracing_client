using AppConfig;
using AssetBundles;
using Base.Utils;
using Controller.Home.Profile;
using CoreBase;
using CoreBase.Controller;
using Game.Gameconfig;
using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;
using Utilites.ObjectPool;
using View.DialogEx;
using View.Home.Lobby;
using View.Home.UpdateInfo;

namespace View.Home.Profile
{
    public class ProfileViewScript : ViewScript
    {

        public enum CateType
        {
            PROFILE,
            CHANGEPASS,
            SECURITY,
            SAFE,
            INPUTNICKNAME
        }

        public CateType cateType = CateType.INPUTNICKNAME;

        public UIMyButton btnCateProfile,
            btnCateChangePass,
            btnCateSecurity,
            btnCateSafe;
        public Sprite sprCateSelected,
            sprCateUnSelected;

        public GameObject objProfile,
            objChangePass,
            objSecurity,
            objSafe,
            objInputNickName,
            objPopup;
        public InputField inputNickName;
        public UIMyButton btnUpdateNickName;

        public Image Avatar;
        //public Text txtVip;
        public Text txtID;
        public Text txtNickName;
        public Text txtCash;
        public Text txtRuby;
        public Text txtPhone;

        public UIMyButton btnUpdatePhone;
       // public UIMyButton btnVip;
        public UIMyButton btnClose;
        public UIMyButton btnEditAvatar;

        public Transform tranChangeAvatar;
        public Transform parentAvatar;
        public UIMyButton btnCloseChangeAvatar;

        

        private bool isOpen;

        // Use this for initialization

        protected override IController CreateController()
        {
            return new ProfileController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();

            EventManager.Instance.UnSubscribeTopic(EventManager.CHANGE_AVATAR_TOPIC, LoadAvatar);
            EventManager.Instance.UnSubscribeTopic(EventManager.CHANGE_NICKNAME_TOPIC, ChangeNickName);
            EventManager.Instance.UnSubscribeTopic(EventManager.CHANGE_NUMBERPHONE_TOPIC, ChangeNumberPhone);
        }

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
            RunAwakeEvent();
            AddListenerButton();
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
            RunStartEvent();
        }
        

        private void RunAwakeEvent()
        {
            EventManager.Instance.SubscribeTopic(EventManager.CHANGE_AVATAR_TOPIC, LoadAvatar);

            EventManager.Instance.SubscribeTopic(EventManager.CHANGE_NICKNAME_TOPIC, ChangeNickName);
            EventManager.Instance.SubscribeTopic(EventManager.CHANGE_NUMBERPHONE_TOPIC, ChangeNumberPhone);
           
        }

        private void RunStartEvent()
        {

            //if (string.IsNullOrEmpty(ClientConfig.UserInfo.NICKNAME) && GameConfig.APPFUNCTION.IsAppFullFunction)
            if (string.IsNullOrEmpty(ClientConfig.UserInfo.NICKNAME))
            {
                objPopup.SetActive(false);
                objInputNickName.SetActive(true);
            }
            else
            {
                isOpen = false;
                objPopup.SetActive(true);
                objInputNickName.SetActive(false);
                gameObject.SetActive(false);
                EventManager.Instance.RaiseEventInTopic(EventManager.OPEN_TAIXIU);
            }
            UpdateInfoViewScript updateInfoViewScript = objSecurity.GetComponent<UpdateInfoViewScript>();
            if (updateInfoViewScript)
            {
                //updateInfoViewScript.dlgRequestOTP = null;
                //updateInfoViewScript.dlgRequestOTP += RequestOTP;
                //updateInfoViewScript.dlgRequestUpdatePhone = null;
                //updateInfoViewScript.dlgRequestUpdatePhone += RequestUpdatePhone;
            }

            ChangePasswordView changePasswordView = objChangePass.GetComponent<ChangePasswordView>();
            if (changePasswordView)
            {
                changePasswordView.actionGetOTP = null;
                changePasswordView.actionGetOTP += new System.Action(()=> {
                    RequestOTP(ClientConfig.UserInfo.PHONE);
                });
                changePasswordView.dlgChangePass = null;
                changePasswordView.dlgChangePass += RequestChangePassword;
            }
        }
        

        private void AddListenerButton()
        {
            btnCateProfile._onClick.AddListener(ClickBtnCateProfile);
            btnCateChangePass._onClick.AddListener(ClickBtnCateChangePass);
            btnCateSecurity._onClick.AddListener(ClickBtnCateSecurity);
            btnCateSafe._onClick.AddListener(ClickBtnCateSafe);
            
            btnEditAvatar._onClick.AddListener(BtnClickShowChangeAvatar);
            btnUpdatePhone._onClick.AddListener(BtnClickUpdatePhone);
            btnClose._onClick.AddListener(BtnClickClose);
            btnCloseChangeAvatar._onClick.AddListener(BtnClickCloseChangeAvatar);

            btnUpdateNickName._onClick.AddListener(BtnClickUpdateNickName);
            SafeViewScript safeView = objSafe.GetComponent<SafeViewScript>();
            if (safeView)
            {
                safeView.dlgSendGoldToSafe = null;
                safeView.dlgSendGoldToSafe += SetGoldToSafe;
                safeView.dlgGetGoldToSafe = null;
                safeView.dlgGetGoldToSafe += GetGoldToSafe;
                safeView.dlgGetOTP = ()=> { RequestOTP(ClientConfig.UserInfo.PHONE); };
            }

        }


        private void ClickBtnCateProfile()
        {
            if (cateType == CateType.PROFILE) return;
            cateType = CateType.PROFILE;
            OpenContentByCate();
        }

        private void ClickBtnCateChangePass()
        {
            if (cateType == CateType.CHANGEPASS) return;
            cateType = CateType.CHANGEPASS;
            OpenContentByCate();
        }

        private void ClickBtnCateSecurity()
        {
            if (cateType == CateType.SECURITY) return;
            cateType = CateType.SECURITY;
            OpenContentByCate();
        }

        private void ClickBtnCateSafe()
        {
            if (cateType == CateType.SAFE) return;
            cateType = CateType.SAFE;
            OpenContentByCate();
        }

        public void OpenContentByCate()
        {
            btnCateProfile.transform.Find("Image").GetComponentInChildren<Image>().sprite = cateType == CateType.PROFILE ? sprCateSelected : sprCateUnSelected;
            btnCateChangePass.transform.Find("Image").GetComponentInChildren<Image>().sprite = cateType == CateType.CHANGEPASS ? sprCateSelected : sprCateUnSelected;
            btnCateSecurity.transform.Find("Image").GetComponentInChildren<Image>().sprite = cateType == CateType.SECURITY ? sprCateSelected : sprCateUnSelected;
            btnCateSafe.transform.Find("Image").GetComponentInChildren<Image>().sprite = cateType == CateType.SAFE ? sprCateSelected : sprCateUnSelected;
            objProfile.SetActive(cateType == CateType.PROFILE);
            objChangePass.SetActive(cateType == CateType.CHANGEPASS);
            objSecurity.SetActive(cateType == CateType.SECURITY);
            objSafe.SetActive(cateType == CateType.SAFE);
        }

        private void RequestACC0(long ID)
        {
            Controller.OnHandleUIEvent("RequestACC0GetInfo", ID);
        }

        private void BtnClickUpdatePhone()
        {
            ClickBtnCateSecurity();
        }

        private void RequestOTP(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                DialogExViewScript.Instance.ShowNotification("Bạn Chưa đăng ký số điện thoại.");
                return;
            }
            Controller.OnHandleUIEvent("RequestOTP", phone);
        }

        private void RequestUpdatePhone(string phone,int otp)
        {
            Controller.OnHandleUIEvent("UpdateNumberPhone", phone, otp);
        }
        private void RequestChangePassword(string password, int otp)
        {
            Controller.OnHandleUIEvent("RequestChangePassword", password, otp);
        }

        private List<Transform> avatars;

        private void BtnClickShowChangeAvatar()
        {
            if(ClientConfig.UserInfo.LOGIN_TYPE == ClientConfig.UserInfo.LoginType.FACEBOOK)
            {
                DialogExViewScript.Instance.ShowNotification("Bạn không thể đổi Avatar cho tài khoản Facebook!");
                return;
            }
            tranChangeAvatar.gameObject.SetActive(true);
            Debug.Log("BtnClickShowChangeAvatar : " + (avatarconfig != null));
            if (avatarconfig != null)
            {

                if (avatars == null)
                    avatars = new List<Transform>();
                else return;
                //Debug.Log("BtnClickShowChangeAvatar : " + avatarconfig.Length);
                string strname = string.Empty;
                for (int i = 0; i < avatarconfig.Length; i++)
                {
                   // Debug.Log("avatarconfig : " + avatarconfig[i]);
                    strname = (avatarconfig[i] < 10) ? string.Format("avatar0{0}", avatarconfig[i]) : string.Format("avatar{0}", avatarconfig[i]);
                    GameObject item = Utilites.ObjectPool.ObjectPool.Spawn(Utilites.ObjectPool.ObjectPool.instance.startupPools[3].prefab, parentAvatar, Vector3.zero, Quaternion.identity);
                    //LoadAssetBundle.LoadSpriteToAtlas(TagAssetBundle.Tag_UI.TAG_UI_KHUNGGAME, TagAssetBundle.AtlasName.LOBBY_AVATAR, strname, (sprite) => { if (sprite) item.transform.Find("avatar").GetComponent<Image>().sprite = sprite; });
                   
                    item.transform.Find("avatar").GetComponent<UIMyButton>()._onClick.AddListener(() => BtnClickChooseAvatar(item.transform.Find("avatar")));
                    avatars.Add(item.transform.Find("avatar"));
                }
            }
        }

        private void BtnClickChooseAvatar(Transform item)
        {
            int index = avatars.IndexOf(item);
            //Debug.Log("Index: " + index);
            DialogExViewScript.Instance.ShowFullPopup("Bạn có chắc chắn muốn đổi Avatar này?", () => {
                Controller.OnHandleUIEvent("ChangeAvatar", index);
                ShowAvatarSelect(index);
            });
        }

        private void ShowAvatarSelect(int index)
        {
            //Debug.Log("ShowAvatarSelect: " + index + " , avatars: " + avatars.Count);
            for(int i = 0; i < avatars.Count; i++)
            {
                //Debug.Log("Avatar: " + i + " , name: ");
                //Debug.Log("Avatar: " + i + " , name: " + avatars[i].parent.GetChild(1).name);
                avatars[i].parent.GetChild(1).gameObject.SetActive(index == i);
            }
        }
        

        private void BtnClickClose()
        {
            isOpen = false;
            gameObject.SetActive(false);
        }

        private void BtnClickCloseChangeAvatar()
        {
            tranChangeAvatar.gameObject.SetActive(false);
        }
        

        private void ShowLoading(bool isShow)
        {
            DialogExViewScript.Instance.ShowLoading(isShow);
        }
        #region Update view by Contrller

       [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            ShowLoading(false);
            DialogExViewScript.Instance.ShowNotification((string)param[0]);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowPopupUpdateNickName(object[] param)
        {
            objPopup.SetActive(false);
            objInputNickName.SetActive(true);
        }

       [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void CloseLoading(object[] param)
        {
            ShowLoading(false);
        }

       [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UpdateDataProfile(object[] param)
        {
            ShowLoading(false);
            if (!isOpen) return;
            txtCash.text =MoneyHelper.FormatNumberAbsolute((long)param[1]);
            txtRuby.text = MoneyHelper.FormatNumberAbsolute((long)param[2]);
            //int curVip = (int)param[3];
            //int maxVip = (int)param[4];
            //int viptype = (int)param[6];
            if (!string.IsNullOrEmpty(ClientConfig.UserInfo.PHONE) && ClientConfig.UserInfo.PHONE.Length > 9)
            {
                string phone = ClientConfig.UserInfo.PHONE.Substring(ClientConfig.UserInfo.PHONE.Length - 4);
                txtPhone.text = string.Format("{0}: <color=white>{1}</color>", "SĐT", "******" + phone);
            }
            else
            {
                txtPhone.text = string.Format("{0} <color=white>{1}</color>", "Chưa đăng ký SĐT bảo mật", ClientConfig.UserInfo.PHONE);
                if (btnUpdatePhone) btnUpdatePhone.gameObject.SetActive(true);
            }
        }

        int[] avatarconfig;

       [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UpdateAvatarDefault(object[] param)
        {
            avatarconfig = (int[])param[0];
        }

       [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UpdateGoldAndSafe(object[] param)
        {

        }
        
        #endregion


        private IEnumerator IEDelayRequest(long ID)
        {
            yield return new WaitForSeconds(0.1f);
            if (isOpen)
            {
                RequestACC0(ID);
            }
        }


        public void OpenProfile(long ID, string nickname, Sprite sprite)
        {
            objPopup.SetActive(true);
            objInputNickName.SetActive(false);
            if (cateType == CateType.PROFILE)
            {
                ShowLoading(true);
                //btnUpdatePhone.gameObject.SetActive(nickname.Equals(ClientConfig.UserInfo.NICKNAME));
                Avatar.sprite = sprite;
                txtID.text = string.Format("ID: {0}", ID);
                txtNickName.text = string.Format("{0}", nickname);
                isOpen = true;
                //Debug.Log("3333");
                StartCoroutine(IEDelayRequest(ID));
            }
        }
        

        private void BtnClickUpdateNickName()
        {
            string nickname = inputNickName.text;
            if (string.IsNullOrEmpty(nickname))
            {
                DialogExViewScript.Instance.ShowNotification("Vui lòng nhập tên hiển thị.");
                return;
            }
            Controller.OnHandleUIEvent("UpdateNickname", nickname);
        }


        void LoadAvatar()
        {
            string str = (int.Parse(ClientConfig.UserInfo.AVATAR) < 10) ? string.Format("avatar0{0}", ClientConfig.UserInfo.AVATAR) : string.Format("avatar{0}", ClientConfig.UserInfo.AVATAR);
            //LoadAssetBundle.LoadSpriteToAtlas(TagAssetBundle.Tag_UI.TAG_UI_KHUNGGAME, TagAssetBundle.AtlasName.LOBBY_AVATAR, str, (spr) => { if (spr) Avatar.sprite = spr; });
           
        }

        void ChangeNickName()
        {
            //EventManager.Instance.RaiseEventInTopic(EventManager.ENABLE_MINIGAME);
            objInputNickName.SetActive(false);
            objPopup.SetActive(true);
            isOpen = false;
            gameObject.SetActive(false);
        }

        void ChangeNumberPhone()
        {
            txtPhone.text = string.Format("{0}: <color=white>{1}</color>", "SĐT", ClientConfig.UserInfo.PHONE);
            ClickBtnCateProfile();
            //objUpdatePhone.SetActive(false);
        }

        public void SetGoldToSafe(long money)
        {
            Controller.OnHandleUIEvent("SetGoldToSafe", money);
        }

        public void GetGoldToSafe(long money, int otp)
        {
            Controller.OnHandleUIEvent("GetGoldToSafe", money, otp);
        }
        
    }
}
