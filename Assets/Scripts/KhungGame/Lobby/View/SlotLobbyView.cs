using AssetBundles;
using Base.Utils;
using Game.Gameconfig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.Home.Lobby
{
    public class SlotLobbyView : MonoBehaviour
    {
        //List<ItemSlotView> listSlots;
        Dictionary<string, ItemSlotView> dicSlots;
        public UIMyButton ManhThu;
        public UIMyButton HaiTac;
        public UIMyButton TamQuoc;
        public UIMyButton TaiXiu;
        public UIMyButton MiniPoker;
        public UIMyButton Slot3;

        //public GameObject[] objDownloads;
        //public Image[] sliders;
        //public Image[] sprIcons;

        protected bool isLoading = false;

        // Use this for initialization
        private void Awake()
        {
            //listSlots = new List<ItemSlotView>();
            dicSlots = new Dictionary<string, ItemSlotView>();
            dicSlots.Add(ClientGameConfig.GAMEID.TAMQUOC, TamQuoc.GetComponent<ItemSlotView>());
            dicSlots.Add(ClientGameConfig.GAMEID.HAITAC, HaiTac.GetComponent<ItemSlotView>());
            dicSlots.Add(ClientGameConfig.GAMEID.MANHTHU, ManhThu.GetComponent<ItemSlotView>());
//#if UNITY_ANDROID
//            if (GameConfig.APPFUNCTION.IsAppFullFunction) MyNhan.GetComponent<RectTransform>().localPosition = new Vector3(470, 60);
//            else MyNhan.GetComponent<RectTransform>().localPosition = Vector3.zero;
//            F1.gameObject.SetActive(GameConfig.APPFUNCTION.IsAppFullFunction);
//            TienCa.gameObject.SetActive(GameConfig.APPFUNCTION.IsAppFullFunction);
//#endif
        }

        //bool isF1, isTienCa, isMyNhan;

        void Start()
        {
            ManhThu._onClick.AddListener(GoToManhThu);
            HaiTac._onClick.AddListener(GoToHaiTac);
            TamQuoc._onClick.AddListener(GoToTamQuoc);
            TaiXiu._onClick.AddListener(ClickTaiXiu);
            MiniPoker._onClick.AddListener(ClickMiniPoker);
            Slot3._onClick.AddListener(ClickSlot3);
            //Debug.Log("START ----------------");
        }
        
        private void GoToTamQuoc()
        {
            if (isLoading)
                return;
            DialogEx.DialogExViewScript.Instance.ShowLoading(true);
            isLoading = true;
            ClientGameConfig.GAMEID.CURRENT_GAME_ID = ClientGameConfig.GAMEID.TAMQUOC;
            ClientGameConfig.CASH_TYPE.CASHTYPE = ClientGameConfig.CASH_TYPE.CASH.GOLD;
            //LoadAssetBundle.LoadScene(TagAssetBundle.Tag_Scene.TAG_SCENE_TAMQUOC, TagAssetBundle.SceneName.SLOT_TAMQUOC_SCENE);
        }

        private void GoToHaiTac()
        {
            if (isLoading)
                return;
            DialogEx.DialogExViewScript.Instance.ShowLoading(true);
            isLoading = true;
            ClientGameConfig.GAMEID.CURRENT_GAME_ID = ClientGameConfig.GAMEID.HAITAC;
            ClientGameConfig.CASH_TYPE.CASHTYPE = ClientGameConfig.CASH_TYPE.CASH.GOLD;
            //LoadAssetBundle.LoadScene(TagAssetBundle.Tag_Scene.TAG_SCENE_HAITAC, TagAssetBundle.SceneName.SLOT_HAITAC_SCENE);
        }

        private void GoToManhThu()
        {
            if (isLoading)
                return;
            DialogEx.DialogExViewScript.Instance.ShowLoading(true);
            isLoading = true;
            ClientGameConfig.GAMEID.CURRENT_GAME_ID = ClientGameConfig.GAMEID.MANHTHU;
            ClientGameConfig.CASH_TYPE.CASHTYPE = ClientGameConfig.CASH_TYPE.CASH.GOLD;
            //LoadAssetBundle.LoadScene(TagAssetBundle.Tag_Scene.TAG_SCENE_MANHTHU, TagAssetBundle.SceneName.SLOT_MANHTHU_SCENE);
        }
        

        public void ShowJackPot(string gameid, long[] jackpot, bool isEvent)
        {
            //#if UNITY_ANDROID
            //            if (!GameConfig.APPFUNCTION.IsAppFullFunction) return;
            //#endif
            //Debug.Log("ShowJackPot: " + gameid);
            if (dicSlots.ContainsKey(gameid))
            {
                dicSlots[gameid].UpdateJackPot(jackpot, isEvent);
            }
        }

        private void ClickTaiXiu()
        {
            EventManager.Instance.RaiseEventInTopic(EventManager.OPEN_TAIXIU);
        }

        private void ClickMiniPoker()
        {
            EventManager.Instance.RaiseEventInTopic(EventManager.OPEN_MINIPOKER);
        }

        private void ClickSlot3()
        {
            EventManager.Instance.RaiseEventInTopic(EventManager.OPEN_SLOT33HOAPHUNG);
        }

        private void OnEnable()
        {
            //if (coroutineGetJackpot != null) StopCoroutine(coroutineGetJackpot);
            //coroutineGetJackpot = StartCoroutine(IeGetJackpot());
        }

        private void OnDisable()
        {
            //if (coroutineGetJackpot != null) StopCoroutine(coroutineGetJackpot);
        }

        //Coroutine coroutineGetJackpot = null;


        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
