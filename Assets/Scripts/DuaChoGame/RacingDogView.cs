using CoreBase.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;
using CoreBase;
using Controller.GamePlay.DuaCho;
using GameProtocol.DOG;
using UnityEngine.UI;
using AppConfig;
using DG.Tweening;
using Base.Utils;
//using AssetBundles;
using System.Linq;
using View.DialogEx;
using System;
using Game.Gameconfig;
using AssetBundles;
using UnityEngine.SceneManagement;
using Utilites;

namespace View.GamePlay.DuaCho
{
    public class RacingDogView : ScreenScript
    {
        [SerializeField]
        Canvas canvas;
        public static RacingDogView Instance;
        [SerializeField]
        private GameObject OVcamera;

        private BettingViewScript _bettingView;
        private RaceTrackView _raceTrackView;
        private DogsManager _dogsManager;
        private AtlasRacingDog _atlasRacingDog;
        private RacingDogResultView _racingResult;
        private Image ScreenShort;
        private UIMyButton btnBack;
        [SerializeField]
        private Transform parentFeautures;

        Camera camOV;
        private int chipbet;
        [SerializeField] private Text txtChipBetValue;
        [SerializeField] private Transform tranChipInit;
        [SerializeField] private Text txtTotalBet;
        private long totalMeBet;

        private long[] Chip = new long[] { 100, 1000, 10000, 100000, 500000 };

        private Dictionary<int, long[]> dicChipByBlind = new Dictionary<int, long[]>()
        {
            { 1, new long[] {100, 1000, 10000, 100000, -1} },

            { 2, new long[] {1000, 10000, 100000, 500000, -1 } },

            { 3, new long[] {2000, 10000, 500000, 1000000, -1 } },

            { 4, new long[] {10000, 100000, 1000000, 5000000, -1 } },

            { 5, new long[] {50000, 500000, 5000000, 10000000, -1 } },
        };

        long[] curBlind;

        private UIMyButton btnChat;
        private GameObject ObjChat, ActiveChat;
        private bool isOpenChat;
        [SerializeField]
        private List<UIMyButton> listBtnQuickChat;
        private List<string> TextChat = new List<string>()
        {
            "1 Đi em ơi cố lên!!!",
            "2 Nào nhanh lên!!!",
            "3 Cố lên em!!!",
            "4 Nhất đi nhất đi!!",
            "5 Nhanh nữa nhanh nữa!!!",
            "6 nhất rồi nhanh lên nào",
            "Quá Nhọ",
            "Chơi vui quá",
            "Lộc đầy trên tay",
            "Quá hay",
            "Đầu hàng đi bạn ơi",
            "Bạn hiền chuẩn bị sang tiền",
            "Chơi lớn đi",
            "Ôi định mệnh",
        };

        [SerializeField]
        private InputField inputChat;
        [SerializeField]
        private ScrollRect scrollChat;
        [SerializeField]
        private Text txtChat;
        private List<string> lstChatInGame;

        void SetBlind()
        {
            Debug.LogError("SetBlind: " + ClientConfig.UserInfo.GOLD);
            if (ClientConfig.UserInfo.GOLD >= 0)
            {
                curBlind = Chip;
                SetChipBetValue();
                //if (ClientConfig.UserInfo.GOLD < 100000) curBlind = Chip;
                //else if (ClientConfig.UserInfo.GOLD < 500000) curBlind = dicChipByBlind[2];
                //else if (ClientConfig.UserInfo.GOLD < 2000000) curBlind = dicChipByBlind[3];
                //else if (ClientConfig.UserInfo.GOLD < 10000000) curBlind = dicChipByBlind[4];
                //else curBlind = dicChipByBlind[5];
                //if (_bettingView == null) Init();
                //_bettingView.InitChip(curBlind);
            }
        }

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
            Init();
            Instance = this;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
#if UNITY_STANDALONE || UNITY_EDITOR
            QualitySettings.vSyncCount = 2;
            Application.targetFrameRate = 100;
#elif UNITY_WEBGL
			Application.targetFrameRate = 80;
#elif UNITY_WP8 || UNITY_WP_8_1 || UNITY_WSAPlayer || UNITY_WSA
			Application.targetFrameRate = 30;
			QualitySettings.vSyncCount = 0;
#else
			Application.targetFrameRate = 40;
			QualitySettings.vSyncCount = 0;
#endif
            lstChatInGame = new List<string>();
        }

        private void Init()
        {
            _atlasRacingDog = GetComponent<AtlasRacingDog>();
            _dogsManager = transform.Find("PanelRackTrack/Dogs").GetComponent<DogsManager>();
            _bettingView = transform.Find("PanelBetting").GetComponent<BettingViewScript>();
            _raceTrackView = transform.Find("PanelRackTrack").GetComponent<RaceTrackView>();
            _racingResult = transform.Find("PanelResult").GetComponent<RacingDogResultView>();
            ScreenShort = transform.Find("ScreenShot").GetComponent<Image>();
            btnBack = transform.Find("PanelBetting/Bg/Buttons/btnBack").GetComponent<UIMyButton>();
            //parentFeautures = transform.Find("Feautures");
            //ObjChat = transform.Find("ChatInGame").gameObject;
            //ActiveChat = ObjChat.transform.Find("Backround").gameObject;
            //btnChat = transform.Find("ChatInGame/Btn/btnChat").GetComponent<UIMyButton>(); ;
            EventManager.Instance.SubscribeTopic(EventManager.SCREEN_SHORT, ScreenShortGame);
            btnBack._onClick.AddListener(BtnBackClick);
            //btnChat._onClick.AddListener(BtnChatClick);
            camOV = OVcamera.GetComponent<Camera>();
            ScreenShort.material = Resources.Load("Material/matGrayScale") as Material;
        }

        private void BtnBackClick()
        {
            Controller.OnHandleUIEvent("RequestOutGame");
            //GoToHome(TagAssetBundle.Tag_Scene.TAG_SCENE_HOME, TagAssetBundle.SceneName.HOME_SCENE);
        }

        public void BtnHistoryClick()
        {
            if (parentFeautures.Find("PanelHistory") == null)
            {
                //LoadAssetBundle.LoadPrefab(TagAssetBundle.Tag_Prefab.TAG_PREFAB_RACINGDOG, "PanelHistory", (history) =>
                //{
                //    history.transform.SetParent(parentFeautures);
                //    history.transform.localScale = Vector3.one;
                //    history.transform.localPosition = Vector3.zero;
                //    history.name = "PanelHistory";
                //    history.GetComponent<RacingDogHistoryView>().OpenHistory();
                //    SetOrderSortCanvas(2);
                //});
            }
            else
            {
                parentFeautures.Find("PanelHistory").gameObject.SetActive(true);
                parentFeautures.Find("PanelHistory").GetComponent<RacingDogHistoryView>().OpenHistory();
                SetOrderSortCanvas(2);
            }
        }

        public void SetOrderSortCanvas(int order)
        {
            //canvas.sortingOrder = order;
        }

        public void BtnTopGameClick()
        {
            if (parentFeautures.Find("PanelRichestMan") == null)
            {
                //LoadAssetBundle.LoadPrefab(TagAssetBundle.Tag_Prefab.TAG_PREFAB_RACINGDOG, "PanelRichestMan", (history) =>
                //{
                //    history.transform.SetParent(parentFeautures);
                //    history.transform.localScale = Vector3.one;
                //    history.transform.localPosition = Vector3.zero;
                //    history.name = "PanelRichestMan";
                //    history.GetComponent<Slot.RichestManViewScript>().OpenRichestMan(ClientGameConfig.GAMEID.DOG_RACING);
                //    SetOrderSortCanvas(2);
                //});
            }
            else
            {
                parentFeautures.Find("PanelRichestMan").gameObject.SetActive(true);
                parentFeautures.Find("PanelRichestMan").GetComponent<Slot.RichestManViewScript>().OpenRichestMan(ClientGameConfig.GAMEID.DOG_RACING);
                SetOrderSortCanvas(2);
            }
        }

        public void BtnHelpClick()
        {
            if (parentFeautures.Find("PanelHelp") == null)
            {
                //LoadAssetBundle.LoadPrefab(TagAssetBundle.Tag_Prefab.TAG_PREFAB_RACINGDOG, "PanelHelp", (help) =>
                //{
                //    help.transform.SetParent(parentFeautures);
                //    help.transform.localScale = Vector3.one;
                //    help.transform.localPosition = Vector3.zero;
                //    help.name = "PanelHelp";
                //   // help.GetComponent<Slot.HelpSlotAngryBirdView>().OpenHelp(true);
                //    SetOrderSortCanvas(2);
                //});
            }
            else
            {
                parentFeautures.Find("PanelHelp").gameObject.SetActive(true);
                parentFeautures.Find("PanelHelp").GetComponent<utils.UIPopupUtilities>().OpenPopup();
                SetOrderSortCanvas(2);
            }
        }

        public void BtnClickIAP()
        {
            //if (ChargingBoxInitializer.Instance)
            //    ChargingBoxInitializer.Instance.ShowPopup(true);
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
            SetBlind();
            for (int i = 0; i < listBtnQuickChat.Count; i++)
            {
                int index = i;
                listBtnQuickChat[i]._onClick.AddListener(() => { ClickQuickChat(index); });
            }
        }

        protected override IController CreateController()
        {
            return new RacingDogController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
            if (EventManager.Instance)
                EventManager.Instance.UnSubscribeTopic(EventManager.SCREEN_SHORT, ScreenShortGame);
            Destroy(Instance);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        protected override void OnReconnected(params object[] parameters)
        {
            base.OnReconnected(parameters);
        }


        public int CHIP_BET
        {
            set
            {
                chipbet = value;
            }
        }

        #region Controller call 

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowStateGame(object[] param)
        {
            try
            {
                byte state = (byte)param[0];
                //camOV.gameObject.SetActive(false);
                switch (state)
                {
                    case 0://INNIT,WAITTING,BETTING
                        DialogEx.DialogExViewScript.Instance.ShowLoading(true);
                        _bettingView.gameObject.SetActive(false);
                        _raceTrackView.gameObject.SetActive(false);
                        _racingResult.gameObject.SetActive(false);
                        break;
                    case 2:
                        //ClientConfig.Sound.StopBackgroundSound(ClientConfig.Sound.SoundId.DuaCho_background_racing);
                        ClientConfig.Sound.StopBackgroundSound();
                        //ClientConfig.Sound.PlayLoopSound(ClientConfig.Sound.SoundId.DuaCho_background_bet);
                        _bettingView.gameObject.SetActive(true);
                        _bettingView.ShowObjNotBetFullTime(false);
                        _raceTrackView.gameObject.SetActive(false);
                        _racingResult.gameObject.SetActive(false);
                        long remaintime = (long)param[1];
                        _bettingView.UpdateCountTime(remaintime, true);
                        break;
                    case 1:
                    case 3://RACING,WAITTING
                           //ClientConfig.Sound.StopBackgroundSound(ClientConfig.Sound.SoundId.DuaCho_background_bet);
                        ClientConfig.Sound.StopBackgroundSound();
                        //if (state == 3)
                        ClientConfig.Sound.PlayLoopSound(ClientConfig.Sound.SoundId.DuaCho_background_racing);
                        _raceTrackView.gameObject.SetActive(true);
                        if (state == 1)
                        {
                            _raceTrackView.UpdateRacingTrack(20);
                            _raceTrackView.ResetBackground();
                        }
                        if (_bettingView.gameObject.activeInHierarchy)
                        {
                            _bettingView.ShowObjNotBetFullTime(false);
                            _bettingView.OnSlectedInput(false);
                        }
                        _bettingView.gameObject.SetActive(false);
                        _racingResult.gameObject.SetActive(false);
                        break;
                    case 4://ENGAME
                        if (_raceTrackView != null && _raceTrackView.gameObject.activeInHierarchy)
                        {
                            _raceTrackView.UpdateRacingTrack(21);
                            _raceTrackView.ResetBackground();
                        }
                        _bettingView.gameObject.SetActive(false);
                        _raceTrackView.gameObject.SetActive(false);
                        _racingResult.gameObject.SetActive(true);
                        break;
                    case 5:
                        _bettingView.gameObject.SetActive(true);
                        _bettingView.ShowObjNotBetFullTime(true);
                        long time = (long)param[1];
                        _bettingView.UpdateCountTime(time, false);
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("ShowStateGame: Exception: " + e.StackTrace);
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UpdateResultBetting(object[] param)
        {
            try
            {
                DogRacing[] dogRacing = (DogRacing[])param[0];
                int length = dogRacing.Length;
                for (int i = 0; i < 3; i++)
                {
                    _bettingView.ShowResult(i, dogRacing[i].DogId);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("UpdateResultBetting: Exception: " + e.StackTrace);
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void InitSlotIdBetting(object[] param)
        {
            DogSlot[] dogSlots = (DogSlot[])param[0];
            if (_bettingView) _bettingView.InitSlotIdBetting(dogSlots);
        }
        
        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UpdateSlotIdBetting(object[] param)
        {
            DogSlot[] dogSlots = (DogSlot[])param[0];
            bool isMeClearBet = (bool)param[1];
            if (_bettingView) _bettingView.UpdateSlotIdBetting(dogSlots, isMeClearBet);
            totalMeBet = 0;
            if (isMeClearBet) txtTotalBet.text = "0";
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowChatHistorys(object[] param)
        {
            DogChat[] dogChats = (DogChat[])param[0];
            if (dogChats != null)
            {
                foreach(DogChat chat in dogChats)
                {
                    if (lstChatInGame.Count >= 30)
                    {
                        lstChatInGame.RemoveAt(0);
                        string txtChat = string.Format("<color=yellow>{0}:</color> {1}", chat.Nickname, chat.Message);
                        lstChatInGame.Add(txtChat);
                    }
                    else
                    {
                        string txtChat = string.Format("<color=yellow>{0}:</color> {1}", chat.Nickname, chat.Message);
                        lstChatInGame.Add(txtChat);
                    }
                }
                FillTextChat();
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void PlayerChatInBeting(object[] param)
        {
            int pos = (int)param[0];
            string msg = (string)param[1];
            string nickname = (string)param[2];
            if (_bettingView.gameObject.activeInHierarchy)
            {
                _bettingView.PlayerChatInRacing(pos, msg);
            }
            if(lstChatInGame.Count >= 30)
            {
                lstChatInGame.RemoveAt(0);
                string txtChat = string.Format("<color=yellow>{0}:</color> {1}", nickname, msg);
                lstChatInGame.Add(txtChat);
            }
            else
            {
                string txtChat = string.Format("<color=yellow>{0}:</color> {1}", nickname, msg);
                lstChatInGame.Add(txtChat);
            }
            FillTextChat();
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void PlayerChatInRacing(object[] param)
        {
            int pos = (int)param[0];
            string msg = (string)param[1];
            if (_raceTrackView.gameObject.activeInHierarchy)
            {
                _raceTrackView.PlayerChatInRacing(pos, msg);
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void OtherPlayerChatInRacing(object[] param)
        {
            string msg = (string)param[0];
            if (_raceTrackView.gameObject.activeInHierarchy)
            {
                _raceTrackView.PlayerChatInRacing(-1, msg);
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UpdateResultBettingWhenEndGame(object[] param)
        {
            try
            {
                DogRacing[] dogRacing = (DogRacing[])param[0];
                if (dogRacing[0].Segments.Length > 0)
                {
                    var dogs = dogRacing.OrderBy(dog => dog.Segments[4].Position);
                    for (int i = 5; i > 2; i--)
                    {
                        byte id = dogs.ElementAt<DogRacing>(i).DogId;
                        _bettingView.ShowResult(5 - i, id - 1);
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        byte id = dogRacing[i].DogId;
                        _bettingView.ShowResult(i, id - 1);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("UpdateResultBettingWhenEndGame: Exception: " + e.StackTrace);
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UpdateRacingGame(object[] param)
        {
           // try
            {
                DogRacing[] dogRacings = (DogRacing[])param[0];
                long timer = (long)param[1];
                //bool isPush4 = (bool)param[1];
                _raceTrackView.UpdateRacingTrack(timer);
                _raceTrackView.ResetBackground();
                for (int i = 0; i < 6; i++)
                {
                    //Debug.Log("UpdateRacingGame: " + dogRacings[i].DogId + " , POSITION: " + dogRacings[i].Position);
                    _dogsManager.GetDog(dogRacings[i].DogId - 1).SetPos(dogRacings[i].Segments, dogRacings[i].Position, dogRacings[i].CurrentSegment);
                }
                _dogsManager.ResetSetRankDog(dogRacings);
            }
            //catch (Exception e)
            //{
            //    Debug.LogError("UpdateRacingGame: Exception: " + e.Message);
            //    Debug.LogError("UpdateRacingGame: Exception: " + e.StackTrace);
            //}
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void CoutDownStartGame(object[] param)
        {
            //Debug.Log("CoutDownStartGame:: " + (_raceTrackView == null));
            if (_raceTrackView != null)
                _raceTrackView.CoutDownStartGame();
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void StartRacingDog(object[] param)
        {
            try
            {
                StartCoroutine(_raceTrackView.IEOpenLongSat());
                DogRacing[] dogRacings = (DogRacing[])param[0];
                long timer = (long)param[1];
                for (int i = 0; i < 6; i++)
                {
                    //Debug.Log("StartRacingDog: " + dogRacings[i].DogId + " , POSITION: " + dogRacings[i].Position);
                    StartCoroutine(_dogsManager.GetDog(dogRacings[i].DogId - 1).RunDog());
                }
                _raceTrackView.RollRacingTrack();
                _raceTrackView.SlierTimeRacing(timer);
                _dogsManager.UpdateRankingDog();
            }
            catch (Exception e)
            {
                Debug.LogError("StartRacingDog: Exception: " + e.StackTrace);
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UpdateTotalBet(object[] param)
        {
            int id = (int)param[0];
            long totalbet = (long)param[1];
            _bettingView.UpdateTotalMoney(id, totalbet);
        }
        
        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ClearDoorbet(object[] param)
        {
            _bettingView.ClearDoorbet();
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowMoneyWin(object[] param)
        {
            long moneywin = (long)param[0];
            StartCoroutine(_bettingView.UpdateMoneyWin(moneywin));
            SetBlind();
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UpdateWinFactors(object[] param)
        {
            int id = (int)param[0];
            float winFactors = (float)param[1];
            short state = (short)param[2];
            _bettingView.UpdateWinFactors(id, winFactors, state);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowEffectWinDoor(object[] param)
        {
            int[] winslots = (int[])param[0];
            if (_bettingView) _bettingView.ShowEffectWinDoor(winslots);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UpdateMoneyMyBet(object[] param)
        {
            int id = (int)param[0];
            long mybets = (long)param[1];
            _bettingView.UpdatMyBets(id, mybets);
            totalMeBet += mybets;
            if (txtTotalBet) txtTotalBet.text = MoneyHelper.FormatNumberAbsolute(totalMeBet);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UpdateMyMoney(object[] param)
        {
            long total = (long)param[0];
            long currentmoney = (long)param[1];
            _bettingView.UpdateToTalMoneyOneDoor(total, indexDoorBet);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void FillBasicPlayerInfo(object[] param)
        {
            int pos = (int)param[0];
            string avatar = (string)param[1];
            long cash = (long)param[2];
            string nickname = (string)param[3];
            _bettingView.FillBasicPlayerInfo(pos, avatar, cash, nickname);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void FillBasicPlayerInfoInRacing(object[] param)
        {
            int pos = (int)param[0];
            string avatar = (string)param[1];
            long cash = (long)param[2];
            string nickname = (string)param[3];
            _raceTrackView.FillBasicPlayerInfo(pos, avatar, nickname);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void UpdateCashChange(object[] param)
        {
            _bettingView.UpdateMyMoney(ClientConfig.UserInfo.GOLD);
            SetBlind();
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void PlayerBettingChip(object[] param)
        {
            //Players[push.Nickname].POSITION, push.DogId, push.CashBet, push.CurrentCash, push.Nickname
            try
            {
                int pos = (int)param[0];
                int id = (int)param[1];
                long cashbet = (long)param[2];
                long currentCash = (long)param[3];
                long TotalBet = (long)param[4];
                if (pos != 0)
                {
                    switch (cashbet)
                    {
                        case 100:
                        case 500:
                        case 1000:
                        case 2000:
                        case 5000:
                        case 10000:
                        case 50000:
                        case 100000:
                        case 500000:
                        case 1000000:
                        case 5000000:
                        case 10000000:
                        case 1000000000:
                            if (_bettingView)
                                StartCoroutine(_bettingView.PlayerBetChip(pos, id, TotalBet, currentCash, _atlasRacingDog.GetSprChip(cashbet)));
                            break;
                        default:
                            //TODO Convert Chip
                            if (_bettingView)
                                StartCoroutine(_bettingView.PlayerAllInChip(pos, id, TotalBet, currentCash, cashbet));
                            break;
                    }
                }
                else
                {
                    if (_bettingView)
                    {
                        _bettingView.UpdateMyMoney(currentCash);
                        _bettingView.UpdateMyBetOneDoor(cashbet, id);
                        _bettingView.UpdateToTalMoneyOneDoor(TotalBet, id);
                        totalMeBet += cashbet;
                        if (txtTotalBet) txtTotalBet.text = MoneyHelper.FormatNumberAbsolute(totalMeBet);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("PlayerBettingChip: Exception: " + e.StackTrace);
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowDialog(object[] param)
        {
            byte typePopup = (byte)param[0];
            string message = (string)param[1];
            DialogEx.DialogExViewScript.Instance.ShowDialog(message, () => {
                if (typePopup == 1)
                {
                    BtnClickIAP();
                }
            });
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowLoading(object[] param)
        {
            DialogExViewScript.Instance.ShowLoading(false);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ClearAllPlayerInfo(object[] param)
        {
            if (_bettingView.gameObject.activeInHierarchy)
            {
                _bettingView.ClearAllPlayerInfo();
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void RemovePlayer(object[] param)
        {
            if (_bettingView.gameObject.activeInHierarchy)
            {
                int pos = (int)param[0];
                _bettingView.RemovePlayer(pos);
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void RemovePlayerInRacing(object[] param)
        {
            if (_raceTrackView.gameObject.activeInHierarchy)
            {
                int pos = (int)param[0];
                _raceTrackView.RemovePlayer(pos);
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ClearAllPlayerInfoInRacing(object[] param)
        {
            if (_raceTrackView.gameObject.activeInHierarchy)
            {
                _raceTrackView.ClearAllPlayerInfo();
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowResult(object[] param)
        {
            DogRacing[] dogRacing = (DogRacing[])param[0];
            _racingResult.ShowResult(dogRacing);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowHome(object[] param)
        {
            GoToHome(TagAssetBundle.Tag_Scene.TAG_SCENE_KHUNGGAME, TagAssetBundle.SceneName.HOME_SCENE);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void OnLoginOnOtherDeviceATH0(params object[] param)
        {
            DialogExViewScript.Instance.ShowDialog((string)param[0], () => {
                ClientConfig.UserInfo.ClearUserInfo();
                GoToHome(TagAssetBundle.Tag_Scene.TAG_SCENE_KHUNGGAME, TagAssetBundle.SceneName.HOME_SCENE);
            });
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowNoti(object[] param)
        {
            DialogEx.DialogExViewScript.Instance.ShowNotification((string)param[0]);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        void Msg3Push(object[] param)
        {
            try
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
            catch (System.Exception ex)
            {
                Debug.LogError("Exception: " + ex.Message);
                Debug.LogError("Exception: " + ex.StackTrace);
            }
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        protected override void OpenRetryPopup(params object[] parameters)
        {
            base.OpenRetryPopup(parameters);
            DialogExViewScript.Instance.ShowLoading(false);
            Controller.OnHandleUIEvent("StartNetWork");
            //DialogEx.DialogExViewScript.Instance.ShowFullPopup("Mất kết nối đến máy chủ, Xin vui lòng thử lại!", () => {
            //    Controller.OnHandleUIEvent("StartNetWork");
            //}, ()=> {
            //    Application.Quit();
            //}, OkLabel: "Thử lại", CancelLabel: "Thoát Game", depth: DialogEx.DialogExViewScript.depth.prioritize);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        protected override void OnLoginSuccess(params object[] parameters)
        {
            base.OnLoginSuccess(parameters);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        protected override void OpenReconnecting(params object[] parameters)
        {
            base.OpenReconnecting(parameters);
            DialogEx.DialogExViewScript.Instance.ShowLoading(true);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        protected override void HideReconnecting(params object[] parameters)
        {
            base.HideReconnecting(parameters);
            DialogEx.DialogExViewScript.Instance.ShowLoading(false);
        }
        #endregion

        #region Effect

        #endregion


        #region CHAT IN GAME
        private void BtnChatClick()
        {
            if (!isOpenChat)
            {
                ActiveChat.SetActive(true);
                ObjChat.transform.DOLocalMoveX(0, 0.3f).OnComplete(() => { isOpenChat = true; }).WaitForCompletion();
            }
            else
            {
                ObjChat.transform.DOLocalMoveX(-215, 0.3f).OnComplete(() => { isOpenChat = false; ActiveChat.SetActive(false); }).WaitForCompletion();
            }
        }

        private void ClickQuickChat(int index)
        {
            try
            {
                Controller.OnHandleUIEvent("SendChat", TextChat[index]);
            }
            catch { }
        }

        private void FillTextChat()
        {
            try
            {
                string totalchat = string.Empty;
                for (int i = 0; i < lstChatInGame.Count; i++)
                {
                    totalchat += lstChatInGame[i];
                    if (i < lstChatInGame.Count - 1) totalchat += "\n";
                }
                if (txtChat) txtChat.text = totalchat;
                if (scrollChat) scrollChat.normalizedPosition = Vector2.down;
                if (inputChat) inputChat.text = "";
            }catch(Exception ex)
            {
                Debug.LogError("Chat Exeption: " + ex.Message);
                Debug.LogError("Chat Exeption: " + ex.StackTrace);
            }
        }

        public void OnBtnSendChat()
        {
            string chat = inputChat.text;
            if (string.IsNullOrEmpty(chat)) return;
            Controller.OnHandleUIEvent("SendChat", chat);
        }

        #endregion


        private void GoToHome(string sceneBundleName, string sceneName)
        {
            GC.Collect();
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            //LoadAssetBundle.LoadScene(sceneBundleName, sceneName);
        }

        public void ScreenShortGame()
        {
            Index += 1;
            //Debug.LogError("ScreenShortGame: " + Index);
            if (Index > 3) return;
            StartCoroutine(IEScreenShort());
        }

        public void ClearTotalbet()
        {
            totalMeBet = 0;
            if (txtTotalBet) txtTotalBet.text = MoneyHelper.FormatNumberAbsolute(totalMeBet);
        }

        //private bool isTimeScale = false;
        //private int counttime = 0;
        private IEnumerator IEScreenShort()
        {
            yield return new WaitForSeconds(0.001f);
            Time.timeScale = 1f;
            //Debug.LogError("ScreenShortGame 1111111111 : " + Time.timeScale);
            yield break;
            //camOV.gameObject.SetActive(true);
            //ScreenShort.gameObject.SetActive(false);
            //yield return new WaitForEndOfFrame();
            //isTimeScale = true;
            //counttime = 0;
            //RenderTexture currentRT = RenderTexture.active;

            //RenderTexture.active = camOV.targetTexture;
            //camOV.Render();
            //Texture2D imageOverview = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            //imageOverview.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            //imageOverview.Apply();
            //ScreenShort.gameObject.SetActive(true);
            //ScreenShort.sprite = Sprite.Create(imageOverview, new Rect(0, 0, imageOverview.width, imageOverview.height), new Vector2(0.5f, 0.5f));
            //Debug.Log("IEScreenShort");
            //RenderTexture.active = currentRT;
        }

        private int indexDoorBet;
        internal int Index;

        public void RequestBetting(int index)
        {
            indexDoorBet = index;
            //Debug.Log("_bettingView.GetChipSelect(): " + curBlind[chipbet] + " , chipbet : " + chipbet);
            if (curBlind == null) SetBlind();
            if (ClientConfig.UserInfo.GOLD >= curBlind[chipbet])
            {
                if (curBlind[chipbet] == -1)
                {
                    //StartCoroutine(_bettingView.MeAllIn(index, ClientConfig.UserInfo.GOLD));
                    _bettingView.OnSlectedInput(true);
                }
                else
                {
                    StartCoroutine(_bettingView.IEMoveChipToDoorBet(index, tranChipInit /*_bettingView.GetChipSelect()*/, Vector2.one, _atlasRacingDog.GetSprChip(curBlind[chipbet])));
                    Controller.OnHandleUIEvent("RequestBetting", index, (curBlind[chipbet] == -1) ? ClientConfig.UserInfo.GOLD : curBlind[chipbet]);
                }
            }
            else
            {
                ShowDialog(new object[] { (byte)1, "Số tiền không đủ để chơi. Vui lòng nạp thêm tiền!" });
            }
        }

        public void RequestBetOtherChip(long chip)
        {
            if (chip <= 0) return;
            Controller.OnHandleUIEvent("RequestBetting", indexDoorBet, chip);
        }

        public void GetAtlasDog(Image img, int pos)
        {
            _atlasRacingDog.GetDog(img, pos);
        }

        public void GetSprDog(Image img, int pos)
        {
            _atlasRacingDog.GetSprTopDog(img, pos);
        }

        public Sprite GetSpriteChip(long chip)
        {
            return _atlasRacingDog.GetSprChip(chip);
        }

        public Sprite GetAvatar(int index)
        {
            return _atlasRacingDog.GetAvatar(index);
        }

        public void OnBtnBetClicked()
        {
            chipbet++;
            if (chipbet > curBlind.Length - 1) chipbet = 0;
            SetChipBetValue();
        }

        public void RequestClearBet()
        {
            Controller.OnHandleUIEvent("RequestClearBet");
        }

        private void SetChipBetValue()
        {
            if (txtChipBetValue) txtChipBetValue.text = MoneyHelper.FormatRelativelyWithoutUnit(curBlind[chipbet]);
        }

#if UNITY_WEBGL

        float time;
        private void OnApplicationFocus(bool focus)
        {
            try
            {
                if (focus)
                {
                    //Debug.Log("OnApplicationFocus: " + (Time.realtimeSinceStartup - time));
                    if(Time.realtimeSinceStartup - time > 1200)
                    {
                        Controller.OnHandleUIEvent("ResetView");
                    }
                    Network.Network.AddSequenceCode("DOG");
                }
                else
                {
                    time = Time.realtimeSinceStartup;
                    //Debug.Log("OnApplicationFocus111111111111 :  " + time);
                    Network.Network.RemoveSequenceCode("DOG");
                }
            }
            catch { }
        }
#endif
    }
}
