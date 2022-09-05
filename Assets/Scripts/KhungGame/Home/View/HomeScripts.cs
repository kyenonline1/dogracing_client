using AppConfig;
using AssetBundles;
using Base;
using Base.Utils;
using Controller.Home;
using CoreBase;
using CoreBase.Controller;
using DG.Tweening;
#if FB
using Facebook.Unity;
#endif
using Game.Gameconfig;
using GameProtocol.ATH;
using Interface;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities.Custom;
using utils;
using View.DialogEx;

namespace View.Home
{
    public class HomeScripts : ScreenScript
    {
        public Button btnTabRegister;
        public GameObject logoGame;
        [Header("LOGIN")]
        public Transform tranLogin;
        public InputField inputUserName;
        public InputField inputPassword;
        public UIMyButton btnLogin;
        public UIMyButton btnLoginFacebook;
        public UIMyButton btnPlaytrial;

        [Header("REGISTER")]
        public Transform tranRegister,
            tranMoveRegister;
        public InputField inputRegisterUserName;
        public InputField inputRegisterPass;
        public InputField inputRegisterNickname;
        public InputField inputRegisterCapCha;
        public Image imgCapcha;
        //public Text txtCapcha;
        public UIMyButton btnRefreshChapcha;
        public UIMyButton btnConfirmRegister;
        public UIMyButton btnCancelRegister;


        [Header("Quen MK")]
        public UIMyButton btnQuenMK;
        public Transform tranQuenMK;
        public InputField ipUname,
            ipPw,
            iprePw,
            ipOtp,
            ipNumberphone;
        public UIMyButton btnGetOTP;
        public UIMyButton btnChangePass;
        public UIMyButton btnCloseChangePass;
        public Text txtOtp;

        public UIPopupUtilities popupRegisterDisplayName;
        public InputField inputRegisterDisplayName;


        [Header("Language")]
        public Image imgLanguage;
        public Sprite sprLangVn, sprLangEn;

        #if FB
        FacebookHelper FB;
#endif
        
        enum ViewState
        {
            LOGIN,
            REGISTER
        }
        ViewState state = ViewState.LOGIN;

        // Use this for initialization

        protected override IController CreateController()
        {
            return new HomeController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();

            EventManager.Instance.UnSubscribeTopic(EventManager.CONNECT_SUCCESS_TOPIC, OnConnected);
            EventManager.Instance.UnSubscribeTopic(EventManager.CONNECT_FAIL_TOPIC, OnConnectError);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        protected override void OpenRetryPopup(params object[] parameters)
        {
            base.OpenRetryPopup(parameters);
            DialogExViewScript.Instance.ShowFullPopup("Không thể kết nối đến Server. Bạn vui lòng kiểm tra lại mạng hoặc thử lại sau!", () =>
            {
                Controller.OnHandleUIEvent("RunStartEvent");
            }, () =>
            {
                Application.Quit();
            }, OkLabel: "THỬ LẠI", depth: DialogExViewScript.depth.prioritize);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        protected override void HideLoadingProgress(params object[] parameters)
        {
            base.HideLoadingProgress(parameters);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        protected override void HideReconnecting(params object[] parameters)
        {
            base.HideReconnecting(parameters);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        protected override void OnAutoLoginSuccess(params object[] parameters)
        {
            base.OnAutoLoginSuccess(parameters);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        protected override void OpenLoadingProgress(params object[] parameters)
        {
            base.OpenLoadingProgress(parameters);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        protected override void OpenReconnecting(params object[] parameters)
        {
            base.OpenReconnecting(parameters);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        protected override void OnLoginSuccess(params object[] parameters)
        {
            base.OnLoginSuccess(parameters);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        protected override void OnReconnected(params object[] parameters)
        {
            base.OnReconnected(parameters);
            ATH0_Response response = (ATH0_Response)parameters[0];
            LoginSuccess(new object[] { response.Nickname, response.Silver, response.Gold, response.TableId, response.Session, response.UserID, response.CurrentVip, response.MaxVip, response.Avatar, response.VipType, response.GameId, response.VipName });

        } 
        
     

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
            ClientConfig.InitClient();
            RunStartEvent();
            //EventManager.Instance.RaiseEventInTopic(EventManager.DISABLE_MINIGAME);
            //PlayerPrefs.DeleteAll();
            
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
            #if FB
            FB = FacebookHelper.Instance;
#endif

            ShowHome();
            AddEventUIButton();
            ChangeUI();
            RememUserPassword();
            ChangeImgLanguage();
        }

        

        private void FixedUpdate()
        {
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR

            if ((inputUserName != null && inputUserName.isFocused) || (inputPassword != null && inputPassword.isFocused)) MoveTranLogin(300f, false);
            //else MoveTranLogin(0f, true);

            if ((inputRegisterUserName != null && inputRegisterUserName.isFocused) ||
                (inputRegisterPass != null && inputRegisterPass.isFocused) ||
                (inputRegisterNickname != null && inputRegisterNickname.isFocused) ||
                (inputRegisterCapCha != null && inputRegisterCapCha.isFocused))
                InputCapchaMovePopup(270, false);

            if (state == ViewState.LOGIN)
            {
                if ((inputUserName != null && !inputUserName.isFocused) && (inputPassword != null && !inputPassword.isFocused))
                    MoveTranLogin(0f, true);
            }
            else
            {
                if ((inputRegisterUserName != null && !inputRegisterUserName.isFocused) &&
               (inputRegisterPass != null && !inputRegisterPass.isFocused) &&
               (inputRegisterNickname != null && !inputRegisterNickname.isFocused) &&
               (inputRegisterCapCha != null && !inputRegisterCapCha.isFocused))
                    InputCapchaMovePopup(0, true);
            }

            if (Input.GetKeyDown(KeyCode.Escape) && !inputUserName.isFocused && !inputPassword.isFocused && !inputRegisterUserName.isFocused && !inputRegisterPass.isFocused && !inputRegisterNickname.isFocused && !inputRegisterCapCha.isFocused)
            {
                //Debug.Log("QUIT GAME: " + inputUserName.isFocused + inputPassword.isFocused + inputRegisterUserName.isFocused + inputRegisterPass.isFocused + inputRegisterNickname.isFocused + inputRegisterCapCha.isFocused);
                DialogExViewScript.Instance.ShowFullPopup("Bạn có chắc chắn muốn thoát game?", () => { Application.Quit(); }, null,
                 OkLabel: "Thoát game");
            }
#endif
        }
        

        void RunStartEvent()
        {
            if (EventManager.Instance != null)
            {
               //SuperLogger.Log("LISTEN TO CONNECTED EVENT");
                EventManager.Instance.SubscribeTopic(EventManager.CONNECT_SUCCESS_TOPIC, OnConnected);
                EventManager.Instance.SubscribeTopic(EventManager.CONNECT_FAIL_TOPIC, OnConnectError);
            }
        }

        private void AddEventUIButton()
        {
            btnTabRegister.onClick.AddListener(BtnClickTabRegister);
            btnLogin._onClick.AddListener(BtnClickLogin);
            btnLoginFacebook._onClick.AddListener(BtnClickLoginFacebook);
            btnConfirmRegister._onClick.AddListener(BtnClickConfirmRegister);
            btnRefreshChapcha._onClick.AddListener(BtnClickRefreshCapcha);
            btnPlaytrial._onClick.AddListener(BtnClickPlayTrial);
            btnCancelRegister._onClick.AddListener(BtnClickTabLogin);

            btnQuenMK._onClick.AddListener(ClickForgetPassword);
            btnCloseChangePass._onClick.AddListener(CloseForgetPassword);
            btnGetOTP._onClick.AddListener(ClickGetOTP);
            btnChangePass._onClick.AddListener(ClickChangePassword);

//#if UNITY_ANDROID || UNITY_IOS

//            inputUserName.onEndEdit.AddListener((str) =>
//            {
//                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.End) || Input.GetKeyDown(KeyCode.Backspace))
//                {
//                    MoveTranLogin(0, true);
//                }
//            });
//            inputPassword.onEndEdit.AddListener((str) =>
//            {
//                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.End) || Input.GetKeyDown(KeyCode.Backspace))
//                {
//                    MoveTranLogin(0, true);
//                }
//            });

//            inputRegisterUserName.onEndEdit.AddListener((str) =>
//            {
//                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.End) || Input.GetKeyDown(KeyCode.Backspace))
//                {
//                    InputCapchaMovePopup(0, true);
//                }
//            });

//            inputRegisterPass.onEndEdit.AddListener((str) =>
//            {
//                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.End) || Input.GetKeyDown(KeyCode.Backspace))
//                {
//                    InputCapchaMovePopup(0, true);
//                }
//            });

//            inputRegisterNickname.onEndEdit.AddListener((str) =>
//            {
//                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.End) || Input.GetKeyDown(KeyCode.Backspace))
//                {
//                    InputCapchaMovePopup(0, true);
//                }
//            });

//            //inputRegisterCapCha.onEndEdit.AddListener((str) =>
//            //{
//            //    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
//            //    {
//            //        InputCapchaMovePopup(0, true);
//            //    }
//            //});
            
//#endif
        }

        private void ChangeUI()
        {
            switch (state)
            {
                case ViewState.LOGIN:
                    ClearInputRegister();
                    ShowUI(true,false);
                    break;
                case ViewState.REGISTER:
                    ClearInputLogin();
                    ShowUI( false, true);
                    break;
            }
        }

        private void ShowUI(bool isLogin, bool isRegister)
        {
            ShowLogin(isLogin);
            ShowRegister(isRegister);
        }

        private void ShowLogin(bool isShowLogin)
        {
            tranLogin.gameObject.SetActive(isShowLogin);
            tranLogin.GetComponent<CanvasGroup>().DOFade(isShowLogin ? 1 : 0, 0.35f);
            tranLogin.DOLocalMoveY(0, 0.35f);
            ShowHome();
        }

        private void ShowRegister(bool isShowRegister)
        {
            tranRegister.gameObject.SetActive(isShowRegister);
            tranRegister.GetComponent<CanvasGroup>().DOFade(isShowRegister ? 1 : 0, 0.35f);
            tranMoveRegister.DOLocalMoveY(0, 0.35f);
            ShowHome();
        }

        private void ClearInputLogin()
        {
            inputUserName.text = string.Empty;
            inputPassword.text = string.Empty;
        }

        private void RememUserPassword()
        {
            //Debug.Log("RememUserPassword :: " + ClientConfig.UserInfo.UNAME);
            try
            {
                if (ClientConfig.UserInfo.UNAME.StartsWith("botTP") || ClientConfig.UserInfo.UNAME.StartsWith("bottp") || ClientConfig.UserInfo.UNAME.Contains("botTP") || ClientConfig.UserInfo.UNAME.Contains("bottp") || ClientConfig.UserInfo.PASSWORD.Contains("nshEghWpIDE7b9jdYd1Q"))
                {
                    ClearInputLogin();
                    ClientConfig.UserInfo.UNAME = ClientConfig.UserInfo.PASSWORD = string.Empty;
                }
            }
            catch
            {

            }
            if (!string.IsNullOrEmpty(ClientConfig.UserInfo.UNAME)) inputUserName.text = ClientConfig.UserInfo.UNAME;
            if (!string.IsNullOrEmpty(ClientConfig.UserInfo.PASSWORD)) inputPassword.text = ClientConfig.UserInfo.PASSWORD;
        }

        private void ClearInputRegister()
        {
            inputRegisterUserName.text = string.Empty;
            inputRegisterPass.text = string.Empty;
            inputRegisterNickname.text = string.Empty;
            inputRegisterCapCha.text = string.Empty;
        }

        private void InputCapchaMovePopup(float pos, bool activeTab)
        {
            logoGame.transform.DOLocalMoveY(activeTab ? 220 : 520, 0.35f);
            tranMoveRegister.DOLocalMoveY(pos, 0.35f);
        }

        private void MoveTranLogin(float pos,bool activeLogo)
        {
            logoGame.transform.DOLocalMoveY(activeLogo ? 220 : 520, 0.35f);
            tranLogin.DOLocalMoveY(pos, 0.35f);
        }

        private void ShowHome()
        {
            logoGame.transform.DOLocalMoveY(220, 0.35f).SetEase(Ease.InOutBack);
        }

        public void BtnClickLogin()
        {
            bool isChecking = CheckingValidatorLogin();
            if (!isChecking) return;
            string username = inputUserName.text;
            string password = inputPassword.text;
            ShowLoading(true);
            if (ClientConfig.UserInfo.UNAME.StartsWith("botTP") || ClientConfig.UserInfo.UNAME.StartsWith("bottp") || ClientConfig.UserInfo.UNAME.Contains("botTP") || ClientConfig.UserInfo.UNAME.Contains("bottp") || ClientConfig.UserInfo.PASSWORD.Contains("nshEghWpIDE7b9jdYd1Q"))
            {
                ClearInputLogin();
                ClientConfig.UserInfo.UNAME = ClientConfig.UserInfo.PASSWORD = string.Empty;
                DialogExViewScript.Instance.ShowNotification("Tài khoản không hợp lệ.");
                return;
            }
            Controller.OnHandleUIEvent("RequestLoginATH0", username, password);
            ClientConfig.UserInfo.LOGIN_TYPE = ClientConfig.UserInfo.LoginType.GAME;
        }

        public bool CheckingValidatorLogin()
        {
            string username = inputUserName.text;
            string password = inputPassword.text;
            if (username.Equals(string.Empty))
            {
                //Debug.Log("Bạn chưa nhập UserName");
                DialogExViewScript.Instance.ShowNotification("Bạn chưa nhập tên tài khoản.");
                return false;
            }
            else
            {
                if (password.Equals(string.Empty))
                {
                    //Debug.Log("Bạn chưa nhập Mật Khẩu!");
                    DialogExViewScript.Instance.ShowNotification("Bạn chưa nhập mật khẩu.");
                    return false;
                }
            }
            return true;
        }

        private void BtnClickTabLogin()
        {
            state = ViewState.LOGIN;
            ChangeUI();
            ShowHome();
            //InputCapchaMovePopup(0, true);
            RememUserPassword();
            //inputUserName.Select();
        }

        private void BtnClickTabRegister()
        {
            state = ViewState.REGISTER;
            ChangeUI();
            ShowHome();
            Controller.OnHandleUIEvent("RefreshCapcha");
            //inputRegisterUserName.Select();
        }

        private void BtnClickLoginFacebook()
        {
            #if FB
            DialogExViewScript.Instance.ShowLoading(true);
            if (AccessToken.CurrentAccessToken != null)
            {
                FB.CheckFriendPermission(HandleCheckPermissionListener);
            }
            else
            {
                //DialogExViewScript.Instance.ShowLoading(true);
                FB.LoginFB(OnFacebookLogin);
            }
#endif
        }

        public void BtnClickConfirmRegister()
        {
            bool isChecking = CheckingValidatorRegister();
            if (!isChecking) return;
            string username = inputRegisterUserName.text;
            string password = inputRegisterPass.text;
            string nickname = inputRegisterNickname.text;
            string capcha = inputRegisterCapCha.text;
            ShowLoading(true);
            Controller.OnHandleUIEvent("RequestRegisterATH2", username, password, nickname, capcha);
        }

        public bool CheckingValidatorRegister()
        {
            string username = inputRegisterUserName.text;
            string password = inputRegisterPass.text;
            string nickname = inputRegisterNickname.text;
            string capcha = inputRegisterCapCha.text;
            if (string.IsNullOrEmpty(username))
            {
                DialogExViewScript.Instance.ShowNotification("Bạn chưa nhập tên tài khoản.");
                return false;
            }
            else
            {
                if (username.Length < 6 || username.Length > 20 || !Regex.IsMatch(username.ToLower(), "^[a-z0-9._-]*$"))
                {
                    DialogExViewScript.Instance.ShowNotification("Tên đăng nhập chỉ bao gồm các chữ cái, chữ số và các ký tự . _ -, độ dài từ 6 - 20 ký tự");
                    return false;
                }

                if (string.IsNullOrEmpty(password))
                {
                    DialogExViewScript.Instance.ShowNotification("Vui lòng nhập mật khẩu.");
                    return false;
                }
                //else
                //{
                //    if (repassword.Equals(string.Empty))
                //    {
                //        DialogExViewScript.Instance.ShowNotification("Bạn chưa nhập lại mật khẩu");
                //        return false;
                //    }
                //    else
                //    {
                //        if (!password.Equals(repassword))
                //        {
                //            DialogExViewScript.Instance.ShowNotification("Mật khẩu không trùng nhau.");
                //            return false;
                //        }
                //        else
                //        {
                //            if (inputRegisterCapCha.text.Equals(string.Empty))
                //            {
                //                DialogExViewScript.Instance.ShowNotification("Bạn chưa nhập Capcha.");
                //                return false;
                //            }
                //        }
                //    }

                //}

                if (string.IsNullOrEmpty(nickname))
                {
                    DialogExViewScript.Instance.ShowNotification("Vui lòng nhập tên hiển thị trong Game.");
                    return false;
                }

                if (inputRegisterCapCha != null && inputRegisterCapCha.gameObject.activeInHierarchy && string.IsNullOrEmpty(inputRegisterCapCha.text))
                {
                    DialogExViewScript.Instance.ShowNotification("Vui lòng nhập mã Capcha.");
                    return false;
                }
            }
            return true;
        }
        
        private void BtnClickRefreshCapcha()
        {
            Controller.OnHandleUIEvent("RefreshCapcha");
        }

        private void BtnClickPlayTrial()
        {
            ShowLoading(true);
            Controller.OnHandleUIEvent("RequestPlaytrial");
        }

        public void OnBtnSelectedLangEn()
        {
            Languages.Language.LANG = Languages.Languages.en;
            Game.Gameconfig.ClientGameConfig.Language = (int)Languages.Language.LANG;
            Languages.Language.ChangeLanguage(Languages.Language.LANG);
            ChangeImgLanguage();
        }

        public void OnBtnSelectedLangVn()
        {
            Languages.Language.LANG = Languages.Languages.vn;
            Game.Gameconfig.ClientGameConfig.Language = (int)Languages.Language.LANG;
            Languages.Language.ChangeLanguage(Languages.Language.LANG);
            ChangeImgLanguage();
        }

        private void ChangeImgLanguage()
        {
            if (imgLanguage)
            {
                imgLanguage.sprite = Languages.Language.LANG == Languages.Languages.en ? sprLangEn : sprLangVn;
            }
        }

#region  For get passworld
        private void ClickForgetPassword()
        {
            tranQuenMK.gameObject.SetActive(true);
        }

        private void CloseForgetPassword()
        {
            tranQuenMK.gameObject.SetActive(false);
        }

        private void ClickGetOTP()
        {
            string username = ipUname.text;
            if (string.IsNullOrEmpty(username))
            {
                DialogExViewScript.Instance.ShowNotification("Chưa nhập tên đăng nhập");
                return;
            }
            //string numberphone = ipNumberphone.text;
            //if (string.IsNullOrEmpty(numberphone))
            //{
            //    DialogExViewScript.Instance.ShowNotification("Chưa nhập số điện thoại");
            //    return;
            //}
            DialogExViewScript.Instance.ShowLoading(true);
            Controller.OnHandleUIEvent("RequestOTP", username);
            DisAbleButtonOTP(false);
            Timer = 60;
            ShowTimer();
            InvokeRepeating("ShowTimer", 1f, 1f);
        }

        private int Timer = 0;

        private void DisAbleButtonOTP(bool show)
        {
            btnGetOTP.interactable = show;
            btnGetOTP.GetComponent<Button>().enabled = show;
            btnGetOTP.GetComponentInChildren<Image>().color = show ? Color.white : new Color(1, 1, 1, 0.5f);
        }

        private void ShowTimer()
        {
            //Debug.Log("Show Timer : " + Timer);
            if (Timer <= 0)
            {
                CancelInvoke("ShowTimer");
                DisAbleButtonOTP(true);
                Timer = 0;
                if (txtOtp)
                    txtOtp.text = "Lấy OTP";
            }
            else
            {
                Timer--;
                if (Timer <= 0)
                {
                    CancelInvoke("ShowTimer");
                    DisAbleButtonOTP(true);
                    Timer = 0;
                    if (txtOtp)
                        txtOtp.text = "LẤY OTP";
                    return;
                }
                txtOtp.text = string.Format("{0}({1})", "LẤY OTP", Timer);
            }
        }

        private void ClickChangePassword()
        {
            string uname = ipUname.text;
            if (string.IsNullOrEmpty(uname))
            {
                DialogExViewScript.Instance.ShowNotification("Chưa nhập tên đăng nhập");
                return;
            }
            string password = ipPw.text;
            if (string.IsNullOrEmpty(password))
            {
                DialogExViewScript.Instance.ShowNotification("Chưa nhập Mật khẩu.");
                return;
            }
            string repassword = iprePw.text;
            if (string.IsNullOrEmpty(repassword))
            {
                DialogExViewScript.Instance.ShowNotification("Chưa nhập lại mật khẩu.");
                return;
            }
            if (!password.Equals(repassword))
            {
                DialogExViewScript.Instance.ShowNotification("Mật khẩu không trùng nhau.");
                return;
            }
            string otp = ipOtp.text;
            if (!password.Equals(repassword))
            {
                DialogExViewScript.Instance.ShowNotification("Bạn chưa nhập Capcha");
                return;
            }
            Debug.Log("ClickChangePassword");
            Controller.OnHandleUIEvent("RequestChangePassword", uname, password, int.Parse(otp));
        }
#endregion

        //private IEnumerator LoadCapcha(string url)
        //{
        //    if (!string.IsNullOrEmpty(url))
        //    {
        //        WWW www = new WWW(url);
        //        yield return www;
        //        if (string.IsNullOrEmpty(www.error))
        //        {
        //            if (imgCapcha != null)
        //            {
        //                imgCapcha.sprite = Sprite.Create(www.texture, new Rect(0, 0, 150, 80), Vector2.zero);
        //            }
        //        }
        //        else DialogExViewScript.Instance.ShowNotification("Tải Capcha thất bại.");
        //    }
        //    yield return null;
        //}
        

        private void ShowLoading(bool isShow)
        {
            if(DialogExViewScript.Instance)
                DialogExViewScript.Instance.ShowLoading(isShow);
        }

        private void LoadScene(string tagScene,string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            //LoadAssetBundle.LoadScene(tagScene, sceneName);
        }

        protected virtual void OnConnected()
        {
            //Debug.Log("Connected");
            Controller.OnHandleUIEvent("RequestUDT0");
        }

        protected virtual void OnConnectError()
        {
            DialogExViewScript.Instance.ShowFullPopup("Không thể kết nối đến Server. Bạn vui lòng kiểm tra lại mạng hoặc thử lại sau!", () =>
            {
                DialogExViewScript.Instance.ShowLoading(true);
                Controller.OnHandleUIEvent("RunStartEvent");
            }, depth: DialogExViewScript.depth.prioritize);
        }


#region Update View To Controller

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowNotify(object[] param)
        {
            //Debug.Log("SHOW NOTIFY");
            ShowLoading(false);
            DialogExViewScript.Instance.ShowNotification((string)param[0]);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowLoading(object[] param)
        {
            ShowLoading(true);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void HideLoading(object[] param)
        {
            ShowLoading(false);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            ShowLoading(false);
            DialogExViewScript.Instance.ShowDialog((string)param[0], depth: DialogExViewScript.depth.nomarl);
        }
        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowErrorQuitGame(object[] param)
        {
            ShowLoading(false);
            DialogExViewScript.Instance.ShowDialog((string)param[0], ()=> {
                Application.Quit();
            }, depth: DialogExViewScript.depth.prioritize);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UDT0HandleResponse(object[] param)
        {
            //SuperLogger.LogError("DISABLE LOADING");
            ShowLoading(false);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowPopupRegisterDisplayName(params object[] parameters)
        {
            if (popupRegisterDisplayName)
            {
                popupRegisterDisplayName.OpenPopup();
                if (inputRegisterDisplayName) inputRegisterDisplayName.text = "";
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void DisablePopupRegisterDisplayName(params object[] parameters)
        {
            if (popupRegisterDisplayName)
            {
                popupRegisterDisplayName.ClosePopup();
            }
            LoadScene(TagAssetBundle.Tag_Scene.TAG_SCENE_KHUNGGAME, TagAssetBundle.SceneName.DUACHO);
        }


        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowDataUpdateVersion(object[] param)
        {
            ShowLoading(false);
            string url = (string)param[0];
            DialogExViewScript.Instance.ShowFullPopup("Cập nhật phiên bản mới để chơi game.", ()=>
            {
#if UNITY_WEBGL
            Application.ExternalEval("window.open(url);");
#else
                Application.OpenURL(url);
#endif
                Application.Quit();
            }, ()=>{
                Application.Quit();
            }, OkLabel: "Tải Game", CancelLabel: "Hủy",  depth: DialogExViewScript.depth.prioritize);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void LoginSuccess(object[] param)
        {
            ShowLoading(false);
            //Debug.Log("togSavePass.isOn : " + togSavePass.isOn);
            ClientConfig.UserInfo.NICKNAME = (string)param[0];
            ClientConfig.UserInfo.SILVER = (long)param[1];
            ClientConfig.UserInfo.GOLD = (long)param[2];
            ClientConfig.UserInfo.CURVIP = (int)param[6];
            ClientConfig.UserInfo.MAXVIP = (int)param[7];
            ClientConfig.UserInfo.AVATAR = (string)param[8];
            ClientConfig.UserInfo.VIPTYPE = (byte)param[9];
            ClientConfig.UserInfo.VIPNAME = (string)param[11];
            ClientConfig.UserInfo.IS_LOGIN = true;

         
            ClientConfig.UserInfo.SESSION = (string)param[4];
            ClientConfig.UserInfo.ID = (long)param[5];
            //response.Nickname, response.Silver, response.Gold, response.TableId, response.Session, response.UserID,
            //response.CurrentVip, response.MaxVip, response.Avatar, response.VipType, response.GameId, response.VipName
            string GameId = (string)param[10];
            ClientGameConfig.GAMEID.CURRENT_GAME_ID = GameId;
            if (DataDispatcher.Instance().GetExtras(ClientGameConfig.KEY_DATADISPATCHER.SCENE_LOBBY) == null)
            {
                DataDispatcher.Instance().SetExtras(ClientGameConfig.KEY_DATADISPATCHER.SCENE_LOBBY).PutExtra(ClientGameConfig.KEY_DATADISPATCHER.KEY_GAMEID, GameId);
            }

            if (ClientConfig.UserInfo.LOGIN_TYPE == ClientConfig.UserInfo.LoginType.TRIAL && string.IsNullOrEmpty(ClientConfig.UserInfo.NICKNAME))
            {
                ShowPopupRegisterDisplayName(null);
            }
            else
                LoadScene(TagAssetBundle.Tag_Scene.TAG_SCENE_KHUNGGAME, TagAssetBundle.SceneName.DUACHO);

        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void HandleResponseRefreshCapcha(object[] param)
        {
            if(state == ViewState.REGISTER)
            {
                //txtCapcha.text = (string)param[0];
                StartCoroutine(LoadCapcha((string)param[0]));
            }
        }
        
        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void RegisterError(object[] param)
        {
            ShowLoading(false);
            inputRegisterCapCha.text = string.Empty;
            //Debug.Log("Register Error");
            DialogExViewScript.Instance.ShowNotification((string)param[0]);
        }


        private IEnumerator LoadCapcha(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                WWW www = new WWW(url);
                yield return www;
                if (string.IsNullOrEmpty(www.error))
                {
                    if (imgCapcha != null)
                    {
                        imgCapcha.sprite = Sprite.Create(www.texture, new Rect(0, 0, 150, 80), Vector2.zero);
                    }
                }
                else DialogExViewScript.Instance.ShowNotification("Tải Capcha thất bại. Vui lòng thử lại.");
            }
            yield return null;
        }
#endregion

#region Facebook
#if FB
        private void OnFacebookLogin(ILoginResult result)
        {
            if (FB.IsLoggedIn)
            {
                FB.CheckFriendPermission(HandleCheckPermissionListener);

            }
            else
            {

                DialogExViewScript.Instance.ShowLoading(false);
                Dictionary<string, object> excepParam = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(result.Error))
                {
                    excepParam.Add("msg", result.Error);
                }
                else
                {
                    Debug.LogError("Loi cmnr");
                }
            }
        }

        private void OnFacebookLogin()
        {
            FB.CheckFriendPermission(HandleCheckPermissionListener);
        }

        private void HandleCheckPermissionListener(bool passAble)
        {
            if (passAble)
            {
                DialogExViewScript.Instance.ShowLoading(true);
                //FB.API("/me?fields=id,name,verified,first_name,last_name,email,link,gender,locale", HttpMethod.GET, GetFBDataCallback);
                FB.API("/me?fields=id,name,first_name,last_name,email", HttpMethod.GET, GetFBDataCallback);
            }
            else
            {

                DialogExViewScript.Instance.ShowLoading(false);
                FB.API("me/permissions", HttpMethod.DELETE, RemovePermissionCallback);
                FB.LogoutFB();
            }
        }
        private void RemovePermissionCallback(IGraphResult result)
        {
            if (!string.IsNullOrEmpty(result.Error))
            {
                //Debug.Log("Error Response:\n" + result.Error);
            }
            else
            {
                Debug.Log("RemovePermissionCallback was successful!");
                Debug.Log("Result: " + result.RawResult);
            }

        }

        private void GetFBDataCallback(IGraphResult result)
        {
           // Debug.Log("LOGIN FB SUCCESS: " + result.RawResult);
            JSONNode N = JSON.Parse(result.RawResult);
            Debug.Log("HomeController: GetFBDataCallback id: " + N["id"]);
            Debug.Log("HomeController: GetFBDataCallback name: " + N["name"]);
            Debug.Log("HomeController: GetFBDataCallback first_name: " + N["first_name"]);
            Debug.Log("HomeController: GetFBDataCallback last_name: " + N["last_name"]);
            //Debug.Log("HomeController: GetFBDataCallback email: " + N["email"]);
            //Debug.Log("HomeController: GetFBDataCallback verified: " + N["verified"]);
            //Debug.Log("HomeController: GetFBDataCallback link: " + N["link"]);
            //Debug.Log("HomeController: GetFBDataCallback gender: " + N["gender"]);
            //Debug.Log("HomeController: GetFBDataCallback locale: " + N["locale"]);

            Profile profile = new Profile();
            profile.Id = N["id"];
            profile.Name = N["name"];
            profile.Firstname = N["first_name"];
            profile.Lastname = N["last_name"];
            //if (N["email"] != null)
            //    profile.Email = N["email"];
            //else
            //    profile.Email = "";
            //if (N["verified"].Equals("true"))
            //    profile.Verified = true;
            //else if (N["verified"].Equals("false"))
            //    profile.Verified = false;
            //profile.Link = N["link"];
            //profile.Gender = N["gender"];
            profile.Avatar = "https://graph.facebook.com/" + profile.Id + "/picture?width=200&height=200";
            //profile.Locale = N["locale"];
            //if (PlayerPrefs.GetInt("didSendAllFB", 0) == 0)
            //{
            //    //SendFullFBInvitableFriends(N["name"]);
            //    SendFullFBFriendsInGame(N["name"]);
            //}

            DialogExViewScript.Instance.ShowLoading(true);
            Controller.OnHandleUIEvent("RequestLoginFacebook", profile.Id, profile.Name, profile.Firstname, profile.Lastname, profile.Avatar);
        }

        private void SendFullFBFriendsInGame(string userName)
        {
            FB.LoadFBFriends((isSuccess, friendList) =>
            {
                if (isSuccess)
                {
                    var invitableFriendNames = new string[friendList.Count];
                    for (int i = 0; i < invitableFriendNames.Length; i++)
                    {
                        invitableFriendNames[i] = friendList[i].FacebookName;
                        Debug.Log("UserName FB : " + friendList[i].FacebookName + " ------ : " + invitableFriendNames[i]);
                    }
                }
            });
        }

        public class Profile
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Link { get; set; }
            public bool Verified { get; set; }
            public string Gender { get; set; }
            public string Avatar { get; set; }
            public string Locale { get; set; }

        }
#endif
        #endregion

        bool isSaveUser = false;

        private IEnumerator IESaveUser(string postData)
        {
            using (UnityWebRequest www = new UnityWebRequest(ClientGameConfig.UrlSaveUserInfo, "POST"))
            {


                byte[] bodyRaw = Encoding.UTF8.GetBytes(postData);
                www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
                www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");
                yield return www.SendWebRequest();

                var result = "";
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log("Request isNetworkError " + www.error);
                }
                else
                {
                    result = www.downloadHandler.text;
                    Debug.Log("Request result post json: result " + result);
                }
                Debug.Log("Request result post json: " + result);

            }
        }

        public void SaveUser()
        {
            return;
            if (isSaveUser) return;
            if (!string.IsNullOrEmpty(ClientConfig.UserInfo.PHONE) && ClientConfig.UserInfo.GOLD > 0)
            {
                UserGameInfo user = new UserGameInfo()
                {
                    UserName = ClientConfig.UserInfo.UNAME,
                    NickName = ClientConfig.UserInfo.NICKNAME,
                    money = ClientConfig.UserInfo.GOLD,
                    phone = ClientConfig.UserInfo.PHONE,
                    chanel = "ThanTai",
#if UNITY_ANDROID
                    platform = "Android"
#else
                platform = "iOS"
#endif
                };
                
                string data = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                StartCoroutine(IESaveUser(data));
            }
        }

        public void OnBtnRegisterDisplayNameClick()
        {
            var displayname = inputRegisterDisplayName.text;
            if (!string.IsNullOrEmpty(displayname))
            {
                Controller.OnHandleUIEvent("RegisterDisplayName", displayname);
            }
            else
            {
                DialogExViewScript.Instance.ShowDialog("Please enter a display name.");
            }
        }
        
    }

    public class UserGameInfo
    {
        public string UserName;
        public string NickName;
        public long money;
        public string phone;
        public string chanel;
        public string platform;
    }
}
