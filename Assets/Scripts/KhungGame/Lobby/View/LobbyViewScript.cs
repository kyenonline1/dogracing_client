using AppConfig;
using AssetBundles;
using Base.Utils;
using Controller.Lobby;
using Game.Gameconfig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites.ObjectPool;
using View.DialogEx;
using View.HelpGame;
using View.Home.ExChange;
using View.Home.GiftCode;
using View.Home.IAP;
using View.Home.Inbox;
using View.Home.Top;
using View.Home.UpdateInfo;
using View.Home.Profile;
using View.Setting;
using View.Home.Vip;
using Utilities.Custom;
using System;
using Utilites;
using DG.Tweening;
using Newtonsoft.Json;
using CoreBase;
using Interface;
using CoreBase.Controller;
using System.Linq;
using GameProtocol.ATH;
using PathologicalGames;

namespace View.Home.Lobby
{
    public class LobbyViewScript : ScreenScript
    {

        public static LobbyViewScript Instance;

        public DOTweenLocation dOTweenLocation;
        public Text txtThongBao;

        [Header("Button Header")]
        [HideInInspector]
        public UIMyButton btnAvatar;
        public UIMyButton btnIAPRuby;
        public UIMyButton btnIAPCash;
        public UIMyButton btnX2Nap;
        public UIMyButton btnInbox;
        public UIMyButton btnSetting;
        public UIMyButton btnExit;
        public UIMyButton btnExitListBlind;

        [Header("Button Bottom")]
        public UIMyButton btnFanpage;
        public UIMyButton btnEventBanner;
        public UIMyButton btnExchange;
        public UIMyButton btnGiftCode;
        public UIMyButton btnSafe;
        public UIMyButton btnDaiLy;
        public UIMyButton btnMessenger,
            btnLuckyWheel;

        [Header("Footer")]
        [SerializeField] private Button btnShop;
        [SerializeField] private Button btnHome;
        [SerializeField] private Button btnCareer;
        [SerializeField] private Button btnProfile;
        [SerializeField] private GameObject imgSelected;

        [Header("Clubs")]
        [SerializeField] private GameObject goNoClubs;
        [SerializeField] private GameObject goHasClubs;
        [SerializeField] private Transform parrentClubs;

        private enum CateFooter
        {
            SHOP,
            HOME,
            CAREER,
            PROFILE
        }

        private CateFooter cateFooter = CateFooter.HOME;

        public Transform tranFooter;
        public Transform tranParrentFeature;

        [Header("Lobby Type")]
        public GameObject HomePanel;
        public Animator animClub;
        public Animator animModeGame;
        public GameObject LobbyPanel;
        public GameObject SpinTourPanel;



        [Header("Lobby Panel")]
        public ItemTableLobbyPanelView[] tableLobbyPanel;

        public GameObject OkeFullFunction,
            HeaderFullFunction;


        public AudioSource backgroundAudio;

        //public SlotLobbyView slotLobbyView;
        //public TopHuViewScript topHuView;
        public GameObject objEvent;

        public GameObject iconNewAnn;
        public Text txtCountAnn;


        public GameObject[] objBaoTri52La;

        // Use this for initialization

        protected override IController CreateController()
        {
            return new LobbyController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
            EventManager.Instance.UnSubscribeTopic(EventManager.CHANGE_MUSIC, ChangeMusic);
            EventManager.Instance.UnSubscribeTopic(EventManager.READINGINBOX, ReadingAnn);
            Destroy(Instance);
            StopAllCoroutines();
        }

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
            Instance = this;
            AddListenerButton();
            //if (!string.IsNullOrEmpty(ClientConfig.UserInfo.NICKNAME))
            //    EventManager.Instance.RaiseEventInTopic(EventManager.ENABLE_MINIGAME);
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
            RunStartEvent();
        }


        void RunStartEvent()
        {
            if (ClientConfig.Sound.ENABLE_BGSOUND)
            {
                backgroundAudio.volume = 0;
                StartCoroutine(backgroundAudio.FadeSound(2, ClientConfig.Setting.VOLUM_MUSIC));
                backgroundAudio.Play();
            }
            if (EventManager.Instance != null)
            {
                //SuperLogger.Log("LISTEN TO CONNECTED EVENT");
                EventManager.Instance.SubscribeTopic(EventManager.CHANGE_MUSIC, ChangeMusic);
                EventManager.Instance.SubscribeTopic(EventManager.READINGINBOX, ReadingAnn);
            }
            DialogExViewScript.Instance.ShowLoading(false);
            ChangeLobbyType();
        }

        void ChangeMusic()
        {
            if (ClientConfig.Sound.ENABLE_BGSOUND)
            {
                if (!backgroundAudio.isPlaying) backgroundAudio.Play();
                StartCoroutine(backgroundAudio.FadeSound(1, ClientConfig.Setting.VOLUM_MUSIC));
            }
            else
            {
                backgroundAudio.Stop();
            }
        }

        #region Button Click
        private void AddListenerButton()
        {
            if (btnAvatar) btnAvatar._onClick.AddListener(BtnClickAvatar);
            if (btnIAPCash) btnIAPCash._onClick.AddListener(BtnClickIAP);
            if (btnIAPRuby) btnIAPRuby._onClick.AddListener(BtnClickIAP);
            if (btnInbox) btnInbox._onClick.AddListener(BtnClickInbox);
            if (btnSetting) btnSetting._onClick.AddListener(BtnClickSetting);
            if (btnEventBanner) btnEventBanner._onClick.AddListener(BtnClickEventBanner);
            if (btnFanpage) btnFanpage._onClick.AddListener(BtnClickFanpage);
            if (btnMessenger) btnMessenger._onClick.AddListener(BtnClickMessenger);
            if (btnExchange) btnExchange._onClick.AddListener(BtnClickExchange);
            if (btnExit) btnExit._onClick.AddListener(BtnClickExit);
            if (btnExitListBlind) btnExitListBlind._onClick.AddListener(BtnClickGoToListCardGame);
            if (btnGiftCode) btnGiftCode._onClick.AddListener(BtnClickGiftCode);
            if (btnLuckyWheel) btnLuckyWheel._onClick.AddListener(BtnClickLuckyWheel);
            if (btnSafe) btnSafe._onClick.AddListener(BtnClickSafe);
            if (btnDaiLy) btnDaiLy._onClick.AddListener(BtnClickDaiLy);
            if (btnX2Nap) btnX2Nap._onClick.AddListener(BtnClickX2Nap);

            if (btnShop) btnShop.onClick.AddListener(OnBtnShopClicked);
            if (btnHome) btnHome.onClick.AddListener(OnBtnHomeClicked);
            if (btnCareer) btnCareer.onClick.AddListener(OnBtnCarerrClicked);
            if (btnProfile) btnProfile.onClick.AddListener(OnBtnProfileClicked);
        }

        private void BtnClickSafe()
        {
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.OpenSafe(btnAvatar.GetComponent<Image>().sprite);
        }

        private void BtnClickDaiLy()
        {
            //Debug.Log("BtnClickDaiLy");
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowDaiLy();
        }

        private void BtnClickX2Nap()
        {
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowEventX2Nap();
        }

        public void OpenSecurity()
        {
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowSecutiry();
        }

        private void BtnClickAvatar()
        {
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowProfileInfo();
        }

        public void BtnClickIAP()
        {
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowNapThe();
        }

        public void BtnClickExChangeSilver()
        {
            //if (napThePanel)
            //{
            //    napThePanel.gameObject.SetActive(true);
            //    napThePanel.cateType = NapTheViewScript.CateType.GOLDTOSILVER;
            //    napThePanel.ChangeContentByCate();
            //}
        }

       

        private void BtnClickInbox()
        {
            ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_type = ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_Type.ANNOUNCEMENT;
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowInbox();
            //InitInbox();
        }

        private void BtnClickSetting()
        {
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowSetting();
        }

        private void BtnClickFanpage()
        {
           
#if UNITY_WEBGL
            Application.ExternalEval("window.open(GameConfig.APPFUNCTION.UrlFanpage);");
#else
            Application.OpenURL(ClientGameConfig.APPFUNCTION.UrlFanpage);
#endif
        }

        private void BtnClickMessenger()
        {
            //string link = string.Format("https://m.me/{0}", "Lộc-Vip-Club-6211906919310900");
            Application.OpenURL(ClientGameConfig.APPFUNCTION.UrlMessenger);
        }

        private void BtnClickEventBanner()
        {
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowEventBanner();
        }
        

        private void BtnClickLuckyWheel()
        {
            EventManager.Instance.RaiseEventInTopic(EventManager.OPEN_LUCKYWHEEL);
        }
        

        private void BtnClickExchange()
        {
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowExchange();
            //if (napThePanel)
            //{
            //    napThePanel.gameObject.SetActive(true);
            //    napThePanel.cateType = NapTheViewScript.CateType.EXCHANGE;
            //    napThePanel.ChangeContentByCate();
            //}
           
        }

        private void BtnClickGiftCode()
        {
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowGiftCode();
        }

        private void BtnClickGoToListCardGame()
        {
        }

        public void BtnClickExit()
        {
            DialogExViewScript.Instance.ShowFullPopup(Languages.Language.GetKey("HOME_DIALOG_CONFIRM_LOGOUT"), () =>
            {
                Controller.OnHandleUIEvent("RequestLogOut");
                ClientConfig.UserInfo.ClearUserInfo();
                Game.Gameconfig.ClientGameConfig.LOBBY_TYPE.Lobby_type = ClientGameConfig.LOBBY_TYPE.LobbyType.MAIN;
                LoadScene(TagAssetBundle.Tag_Scene.TAG_SCENE_KHUNGGAME, TagAssetBundle.SceneName.HOME_SCENE);

            }, OkLabel: "HOME_DIALOG_OKE_LOGOUT", CancelLabel: "HOME_DIALOG_CANCEL");
        }

        public void BtnLobbyClicked()
        {
            Game.Gameconfig.ClientGameConfig.LOBBY_TYPE.Lobby_type = ClientGameConfig.LOBBY_TYPE.LobbyType.LOBBY;
            StartCoroutine(ChangeHomePanelToLobbyPanel());

        }

        public void BtnTournamentClicked()
        {
            Game.Gameconfig.ClientGameConfig.LOBBY_TYPE.Lobby_type = ClientGameConfig.LOBBY_TYPE.LobbyType.TOURNAMENT;
            Game.Gameconfig.ClientGameConfig.LOBBY_TYPE.lobbySpinTour = ClientGameConfig.LOBBY_TYPE.LobbySpinTour.TOUR;
            StartCoroutine(ChangeHomePanelToSpinTourPanel());
        }

        public void BtnSpinupClicked()
        {
            Game.Gameconfig.ClientGameConfig.LOBBY_TYPE.Lobby_type = ClientGameConfig.LOBBY_TYPE.LobbyType.SPINUP;
            Game.Gameconfig.ClientGameConfig.LOBBY_TYPE.lobbySpinTour = ClientGameConfig.LOBBY_TYPE.LobbySpinTour.SPIN;
            StartCoroutine(ChangeHomePanelToSpinTourPanel());
        }

        public void BtnClickShowHomePanel()
        {
            Game.Gameconfig.ClientGameConfig.LOBBY_TYPE.Lobby_type = ClientGameConfig.LOBBY_TYPE.LobbyType.MAIN;
            ShowLobbyPanel(false);
            ShowSpinTourPanel(false);
            ShowHomePanel(true);
            ShowClubPanel();
            ShowModeGamePanel();
        }

      
#endregion

#region Call Controller
        
        private void RequestListGame()
        {
            Controller.OnHandleUIEvent("RequestListGame");
        }

        private void RequestBlind(string gameid)
        {
            if (Controller != null)
            {
                DialogExViewScript.Instance.ShowLoading(true);
                Controller.OnHandleUIEvent("RequestGetTablesGame", gameid);
                //Controller.OnHandleUIEvent("RequestBlindInGame", gameid);
            }
        }

        public void RequestJoinGame(int blind, byte tablename, long tableid = 0)
        {
            DialogExViewScript.Instance.ShowLoading(true);
            Controller.OnHandleUIEvent("RequestJoinGame", blind,
                ClientGameConfig.CASH_TYPE.CASHTYPE, tablename, tableid);
        } 

        public void RequestJoinGameBuyId(long tableid, string gameid)
        {
            DialogExViewScript.Instance.ShowLoading(true);
            Controller.OnHandleUIEvent("RequestJoinGameBuyId", tableid, gameid);
        }

        public void InitInbox()
        {
            //if(inbox != null)
            //{
            //    inbox.gameObject.SetActive(true);
            //    inbox.OpenInbox();
            //}
        }


        private void BtnClickBlind(int blind)
        {
            Debug.Log("Click blind: " + blind);
            DialogExViewScript.Instance.ShowLoading(true);
            Controller.OnHandleUIEvent("RequestJoinGame", blind,
               ClientGameConfig.CASH_TYPE.CASHTYPE);
        }

        #endregion

        #region Process Controller Call to View


        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        void Msg3Push(object[] param)
        {
            byte type = (byte)param[0];
            string msg = (string)param[1];
            if (type == 0)
            {
                DialogExViewScript.Instance.ShowDialog(msg);
            }
            else
            {
                DialogExViewScript.Instance.ShowDialog(msg, () => { Application.Quit(); });
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowLoading(object[] param)
        {
            DialogExViewScript.Instance.ShowLoadingScreen(true);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            DialogExViewScript.Instance.ShowNotification((string)param[0]);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void DisableLoading(object[] param)
        {
            DialogExViewScript.Instance.ShowLoading(false);
            DialogExViewScript.Instance.ShowLoadingScreen(false);
        }


        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void LogOutAnotherDevice(object[] param)
        {
            DialogExViewScript.Instance.ShowDialog((string)param[0], ()=>{
                LoadScene(TagAssetBundle.Tag_Scene.TAG_SCENE_KHUNGGAME, TagAssetBundle.SceneName.HOME_SCENE);
            });
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void GoToHome(object[] param)
        {
            LoadScene(TagAssetBundle.Tag_Scene.TAG_SCENE_KHUNGGAME, TagAssetBundle.SceneName.HOME_SCENE);
        }

        private void LoginSuccess(object[] param)
        {

        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void JoinGameSuccess(object[] param)
        {
            string tagScene = (string)param[0];
            string sceneName = (string)param[1];
            LoadScene(tagScene, sceneName);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UpdateGame52(object[] param)
        {
            //GameConfig[] games = (GameConfig[])param[0];
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowListTable(object[] param)
        {
            string gamename = (string)param[1];

            //if (tranTables.childCount != 0) Utilites.ObjectPool.ObjectPool.RecycleAll(Utilites.ObjectPool.ObjectPool.instance.startupPools[5].prefab);
            //int[] blinds = (int[])param[0];
            //for (int i = 0; i < blinds.Length; i++)
            //{
            //    GameObject item = Utilites.ObjectPool.ObjectPool.Spawn(Utilites.ObjectPool.ObjectPool.instance.startupPools[5].prefab, tranTables, Vector3.zero, Quaternion.identity);
            //    item.transform.localScale = Vector3.one;
            //    item.GetComponent<RectTransform>().localPosition = Vector3.zero;
            //    item.GetComponent<ItemBlindView>().SetData(blinds[i]);
            //    item.GetComponent<ItemBlindView>().EventClickBlind = null;
            //    item.GetComponent<ItemBlindView>().EventClickBlind = BtnClickBlind;
            //}

        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowListTableLobbyPanel(object[] param)
        {
            RecycleAllItemTableLobbtNLH();
            BlindInfo[] blindInfos = (BlindInfo[])param[0];
            if(blindInfos != null && tableLobbyPanel != null)
            {
                int length = blindInfos.Length;
                int lengthPool = tableLobbyPanel.Length;
                for (int i = 0; i < length; i++)
                {
                    if(i < lengthPool && blindInfos[i].Active)
                    {
                        string valueblind = string.Format("{0}/{1}({2})", MoneyHelper.FormatRelativelyWithoutUnit(blindInfos[i].Blind),
                            MoneyHelper.FormatRelativelyWithoutUnit(blindInfos[i].Blind * 2), MoneyHelper.FormatRelativelyWithoutUnit(blindInfos[i].Blind));
                        tableLobbyPanel[i].gameObject.SetActive(true);
                        tableLobbyPanel[i].UpdateDataTableInfo(blindInfos[i].Region, valueblind,
                            MoneyHelper.FormatRelativelyWithoutUnit(blindInfos[i].MinCashIn),
                            MoneyHelper.FormatNumberAbsolute(blindInfos[i].UsersOnline), blindInfos[i].Type, blindInfos[i].isAnte > 0, blindInfos[i].isStraddle);
                    }
                }
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowNotEnoughMoney(object[] param)
        {
            DialogExViewScript.Instance.ShowLoading(false);
            DialogExViewScript.Instance.ShowFullPopup(Languages.Language.GetKey("HOME_DIALOG_ENOUND_MONEY"), ()=> {
                Debug.Log("Show nạp");
            }, OkLabel: "HOME_DIALOG_CHARGER_NOW", CancelLabel: "HOME_DIALOG_LATER");
        }


        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void JoinGameError(object[] param)
        {
            DialogExViewScript.Instance.ShowLoading(false);
            DialogExViewScript.Instance.ShowNotification((string)param[0]);
        }


        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowNotifiChargingSuccess(object[] param)
        {
            try
            {
                DialogExViewScript.Instance.ShowLoading(false);
                int cash = (int)param[0];
                DialogExViewScript.Instance.ShowDialog("Quý khách vừa nạp thành công thẻ cào mệnh giá " + MoneyHelper.FormatNumberAbsolute(cash));
                EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_BALANCE);
            }
            catch
            {

            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowPopupConfirmJoinTour(object[] param)
        {
            string message = (string)param[1];
            if (!string.IsNullOrEmpty(message))
            {
                int tourId = (int)param[0];
                DialogExViewScript.Instance.ShowFullPopup(message, () =>
                {
                    Controller.OnHandleUIEvent("RequestJoinTour", tourId);
                }, Tittle: Languages.Language.GetKey("LOBBY_JOIN_TOUR_TITTLE"), OkLabel: Languages.Language.GetKey("LOBBY_OK_JOINTOUR"),
                CancelLabel: Languages.Language.GetKey("HOME_DIALOG_CLOSE"), depth: DialogExViewScript.depth.important);
            }
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowDialogLogOutOtherDevice(object[] param)
        {
            string message = (string)param[0];

            DialogExViewScript.Instance.ShowDialog(message, () =>
            {
                LoadScene(TagAssetBundle.Tag_Scene.TAG_SCENE_KHUNGGAME, TagAssetBundle.SceneName.HOME_SCENE);
            });
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowNoClub(object[] param)
        {
            if (goNoClubs) goNoClubs.SetActive(true);
            if (goHasClubs) goHasClubs.SetActive(false);
            DialogExViewScript.Instance.ShowLoading(false);
        }
        
        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowClubsInfo(object[] param)
        {
            RecycleAllItemClub();

          
            DialogExViewScript.Instance.ShowLoading(false);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowClubInfo(object[] param)
        {
            int clubid = (int)param[0];
            long cash = (long)param[1];
           // if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowClubInfo(clubid, 0);
        }

        private void RecycleAllItemClub()
        {
            PoolManager.Pools["ItemClubLobby"].DespawnAll();
        }

        private void RecycleAllItemTableLobbtNLH()
        {
           for(int i =0; i < tableLobbyPanel.Length; i++)
            {
                tableLobbyPanel[i].gameObject.SetActive(false);
            }
        }


        private string[] broadCards;
        bool isFirstBroadCard = true;
        /// <summary>
        /// MSG2 - Broad Cast
        /// </summary>
        /// <param name="param"></param>
        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void OnGetBroadCastMSG2(params object[] param)
        {
            if (param != null && param.Length > 0)
            {
                broadCards = (string[])param[0];
                dOTweenLocation.onPlayForwardAction += PlayForwardAction;
                if (isFirstBroadCard)
                {
                    isFirstBroadCard = false;


                    txtThongBao.text = "Chúc mừng ";
                    foreach (var item in broadCards)
                    {
                        string message = (string)item;
                        txtThongBao.text += message + "       ";
                    }

                    StartCoroutine(DisplayBroadCastText());

                }
            }
        }


        private IEnumerator DisplayBroadCastText()
        {
            yield return new WaitForSeconds(0.5f);

            RectTransform rectTransform = dOTweenLocation.GetComponent<RectTransform>();

            float textWith = rectTransform.rect.width;
            float textHeight = rectTransform.rect.height;
            dOTweenLocation.FromLocation = new Vector2(textWith / 2 + 350, 0);
            dOTweenLocation.ToLocation = new Vector2(-textWith / 2 - 350, 0);
            dOTweenLocation.Duration = (textWith / 2 + 350) / 50;
            dOTweenLocation.PlayForward();

        }

        private void PlayForwardAction()
        {
            //RectTransform rectTransform = dOTweenLocation.GetComponentInChildren<RectTransform>();
            txtThongBao.text = "Chúc mừng ";
            foreach (var item in broadCards)
            {
                string message = (string)item;
                txtThongBao.text += message + "      ";
            }

            StartCoroutine(DisplayBroadCastText());

        }

#endregion
#region Process View

     

        private void ChangeLobbyType()
        {
            Debug.Log("ChangeLobbyType: " + ClientGameConfig.LOBBY_TYPE.Lobby_type);
            switch (ClientGameConfig.LOBBY_TYPE.Lobby_type)
            {
                case ClientGameConfig.LOBBY_TYPE.LobbyType.MAIN:
                    ShowHomePanel(true);
                    break;
                case ClientGameConfig.LOBBY_TYPE.LobbyType.LOBBY:
                    ShowHomePanel(false);
                    ShowLobbyPanel(true);
                    break;
                case ClientGameConfig.LOBBY_TYPE.LobbyType.TOURNAMENT:
                case ClientGameConfig.LOBBY_TYPE.LobbyType.SPINUP:
                    ShowHomePanel(false);
                    ShowSpinTourPanel(true);
                    //Controller.OnHandleUIEvent("GetListTable", ClientGameConfig.GAMEID.CURRENT_GAME_ID);
                    break;
            }
        }


        
        private IEnumerator ChangeHomePanelToLobbyPanel()
        {
            CloseClubPanel();
            CloseModeGamePanel();
            yield return new WaitForSeconds(0.5f);
            ShowHomePanel(false);
            ShowLobbyPanel(true);
        }

        private void ChangeLobbyPanelToHomePanel()
        {
            ShowLobbyPanel(false);
            ShowHomePanel(true);
            ShowClubPanel();
            ShowModeGamePanel();
        }

        private IEnumerator ChangeHomePanelToSpinTourPanel()
        {
            CloseClubPanel();
            CloseModeGamePanel();
            yield return new WaitForSeconds(0.5f);
            ShowHomePanel(false);
            ShowSpinTourPanel(true);
        }

        private void ChangeSpinTourPanelToHomePanel()
        {
            ShowSpinTourPanel(false);
            ShowHomePanel(true);
            ShowClubPanel();
            ShowModeGamePanel();
        }

        private void ShowHomePanel(bool isShow)
        {
            if (isShow) ClientGameConfig.LOBBY_TYPE.lobbySpinTour = ClientGameConfig.LOBBY_TYPE.LobbySpinTour.NONE;
            //Debug.Log("ShowHomePanel: " + ClientGameConfig.LOBBY_TYPE.lobbySpinTour);
            if (HomePanel) HomePanel.SetActive(isShow);
            if (isShow)
            {
                StartCoroutine(Utils.DelayAction(() =>
                {
                    RequestGetClubs();
                }, 0.1f));
            }
        }

        private void CloseClubPanel()
        {
            if (animClub != null && animClub.gameObject.activeInHierarchy) animClub.Play("AnimCloseClub", 0, 0);
        }

        private void ShowClubPanel()
        {
            if (animClub != null && animClub.gameObject.activeInHierarchy) animClub.Play("AnimShowNoClub", 0, 0);
        }

        private void ShowModeGamePanel()
        {
            if (animModeGame != null) animModeGame.Play("AnimShowModeGame", 0, 0);
        }

        private void CloseModeGamePanel()
        {
            if (animModeGame != null) animModeGame.Play("AnimCloseModeGame", 0, 0);
        }

        private void ShowLobbyPanel(bool isShow)
        {
            if (LobbyPanel) LobbyPanel.SetActive(isShow);
            if (isShow)
            {
                Controller.OnHandleUIEvent("RequestListTableLobbyATH4");
            }
        }

        private void ShowSpinTourPanel(bool isShow)
        {
            if (SpinTourPanel) SpinTourPanel.SetActive(isShow);
            if (isShow)
            {
                //Controller.OnHandleUIEvent("RequestTOU0GetListTable");
            }
        }

        private void ChangeCateFooter()
        {
            switch (cateFooter)
            {
                case CateFooter.SHOP:
                    SetSelected(-255);
                    if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowShop(CateShop.AGENCY, OnBtnHomeClicked);
                    break;
                case CateFooter.HOME:
                    SetSelected(-75);
                    break;
                case CateFooter.CAREER:
                    if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowMission();
                    SetSelected(90);
                    break;
                case CateFooter.PROFILE:
                    SetSelected(265);
                    if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowProfileInfo(OnBtnHomeClicked);
                    break;
            }
        }

        private void SetSelected(float posX)
        {
            if (imgSelected) imgSelected.transform.DOLocalMoveX(posX, 0.15f);
        }

        private void OnBtnShopClicked()
        {
            if (cateFooter == CateFooter.SHOP) return;
            cateFooter = CateFooter.SHOP;
            ChangeCateFooter();
        }

        private void OnBtnHomeClicked()
        {
            if (cateFooter == CateFooter.HOME) return;
            cateFooter = CateFooter.HOME;
            ChangeCateFooter();
        }

        private void OnBtnCarerrClicked()
        {
            if (cateFooter == CateFooter.CAREER) return;
            cateFooter = CateFooter.CAREER;
            ChangeCateFooter();
        }

        private void OnBtnProfileClicked()
        {
            if (cateFooter == CateFooter.PROFILE) return;
            cateFooter = CateFooter.PROFILE;
            ChangeCateFooter();
        }



        private void LoadScene(string TagName, string sceneName)
        {
            //Debug.Log("LoadScene: " + LoadAssetBundle.isCheckDownload(TagName));

            StartCoroutine(backgroundAudio.FadeSound(1f, 0));
            //LoadAssetBundle.LoadScene(TagName, sceneName);
            //SceneManager.LoadScene(sceneName);
        }

        public void ClickTableLobbyPanel(int index)
        {
            Controller.OnHandleUIEvent("ClickTableLobbyPanel", index);
        }

        public void OnBtnRequestCreateClub()
        {
            DialogExViewScript.Instance.ShowLoading(true);
            Controller.OnHandleUIEvent("RequestCreateClubCLB0");
        }

        public void OnBtnRequestJoinClub()
        {
            //  if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowJoinClub();
        }

        public void RequestGetClubs()
        {
            Controller.OnHandleUIEvent("RequestGetClubs");
        }

        #endregion


        #region Check new Ann

        //Coroutine coroutineCheckingAnn = null;

        IEnumerator IeCheckingAnn()
        {
            while (true)
            {
                //Debug.Log("GET JACKPOT: ");
                RequestCheckingAnn();
                yield return new WaitForSeconds(120f);
            }
        }

        private void RequestCheckingAnn()
        {
            //string url = Network.Domain + string.Format(Network.KEYNAME_INGAME.LOBBY_COUNTINBOX, ClientConfig.UserInfo.ID);
            //Network.Instance.RequestWWForm(url, CallbackResponseSuccess, "Checking ANN", CallbackError);
        }

        private void CallbackResponseSuccess(string data)
        {
            try
            {
                GetCountAnnounce response = JsonConvert.DeserializeObject<GetCountAnnounce>(data);
                ClientGameConfig.ANNOUCEMENT_TYPE.countTotal = response.NewTotal;
                ClientGameConfig.ANNOUCEMENT_TYPE.countSystem = response.NewSystem;
                ClientGameConfig.ANNOUCEMENT_TYPE.countAnn = response.NewAnn;
                iconNewAnn.SetActive(ClientGameConfig.ANNOUCEMENT_TYPE.countTotal > 0);
                if (ClientGameConfig.ANNOUCEMENT_TYPE.countTotal > 0) txtCountAnn.text = ClientGameConfig.ANNOUCEMENT_TYPE.countTotal.ToString();
            }
            catch
            {
                iconNewAnn.SetActive(false);
            }
        }

        private void ReadingAnn()
        {
            ClientGameConfig.ANNOUCEMENT_TYPE.countAnn--;
            ClientGameConfig.ANNOUCEMENT_TYPE.countTotal--;
            Debug.Log("READING MAIL: " + ClientGameConfig.ANNOUCEMENT_TYPE.countTotal + " -- " + ClientGameConfig.ANNOUCEMENT_TYPE.countAnn);
            iconNewAnn.SetActive(ClientGameConfig.ANNOUCEMENT_TYPE.countTotal > 0);
            if (ClientGameConfig.ANNOUCEMENT_TYPE.countTotal > 0) txtCountAnn.text = ClientGameConfig.ANNOUCEMENT_TYPE.countTotal.ToString();
        }

        private void CallbackError()
        {
            iconNewAnn.SetActive(false);
        }

        #endregion
        protected override void HideLoadingProgress(params object[] parameters)
        {
            base.HideLoadingProgress(parameters);
            DialogExViewScript.Instance.ShowLoading(false);
        }

        protected override void HideReconnecting(params object[] parameters)
        {
            base.HideReconnecting(parameters);
            DialogExViewScript.Instance.ShowLoading(false);
        }

        protected override void OnAutoLoginSuccess(params object[] parameters)
        {
            base.OnAutoLoginSuccess(parameters);
            DialogExViewScript.Instance.ShowLoading(false);
        }


        protected override void OnLoginSuccess(params object[] parameters)
        {
            base.OnLoginSuccess(parameters);
            DialogExViewScript.Instance.ShowLoading(false);
        }

        protected override void OpenLoadingProgress(params object[] parameters)
        {
            base.OpenLoadingProgress(parameters);
        }

        protected override void OnReconnected(params object[] parameters)
        {
            base.OnReconnected(parameters);
        }

        protected override void OpenReconnecting(params object[] parameters)
        {
            base.OpenReconnecting(parameters);
        }

        protected override void OpenRetryPopup(params object[] parameters)
        {
            base.OpenRetryPopup(parameters);

            if (ClientGameConfig.isAutoReconnect)
            {
                Controller.OnHandleUIEvent("StartConnect");
                if (DialogExViewScript.Instance) DialogExViewScript.Instance.ShowLoading(true);
            }
            else
            {
                DialogExViewScript.Instance.ShowFullPopup(Languages.Language.GetKey("ANNOUCEMENT_NOCONNECT_SERVER"), () =>
                {
                    Controller.OnHandleUIEvent("StartConnect");
                    DialogExViewScript.Instance.ShowLoading(true);
                }, () =>
                {
                    Application.Quit();
                }, OkLabel: "HOME_DIALOG_TRYCONNECT", depth: DialogExViewScript.depth.prioritize);
            }

        }

        //private void OnApplicationFocus(bool focus)
        //{
        //    Debug.LogError("OnApplicationFocus: " + focus);
        //    //if (!focus)
        //    //{
        //    //    Controller.OnHandleUIEvent("StopNetwork");
        //    //}
        //}
    }

    public class GetCountAnnounce
    {
        public int NewTotal;
        public int NewAnn;
        public int NewSystem;

    }
}
