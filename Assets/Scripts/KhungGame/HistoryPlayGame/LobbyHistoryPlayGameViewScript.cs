using CoreBase;
using CoreBase.Controller;
using GameProtocol.HIS;
using Interface;
using PathologicalGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.Home.History
{

    public enum CateHistory
    {
        NONE = -1,
        HISTORY,
        COLLECTION
    }

    public class LobbyHistoryPlayGameViewScript : ViewScript
    {
        [SerializeField] private Text txtTotalHistory;

        [Header("Lịch sử Chơi Game")]
        [SerializeField] private Transform parrentItemHistory;
        [SerializeField] private Text txtPageViewHistory;
        [SerializeField] private GameObject goNoDataHistory;

        [Header("Lịch sử Bộ Sưu Tập")]
        [SerializeField] private Transform parrentItemCollection;
        [SerializeField] private Text txtPageViewCollection;
        [SerializeField] private GameObject goNoDataCollection;

        private CateHistory cateHistory = CateHistory.NONE;

        private string POOL_ITEM_NAME = "ItemHistoryGame";
        private string POOL_NAME = "HistoryLobbyGame";

        /// <summary>
        /// Lịch sử bt
        /// </summary>
        private byte currentPageHistory = 1;
        private byte totalPageHistory = 1;
        /// <summary>
        /// Lịch sử bộ sưu tập
        /// </summary>
        private byte currentPageCollection = 1;
        private byte totalPageCollection = 1;

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
            return new LobbyHistoryPlayGameController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
        }

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
            if (callbackOpenPopup == null)
            {
                callbackOpenPopup = () =>
                {
                    OnBtnCateHistoryClicked();
                };
            }
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
        }

        private void RequestHistorys(byte currentPage, bool isSave = false)
        {
            ShowLoading(true);
            Controller.OnHandleUIEvent("RequestHistorys", currentPage, isSave);
        }


        private void ShowLoading(bool isShow)
        {
            if (DialogEx.DialogExViewScript.Instance) DialogEx.DialogExViewScript.Instance.ShowLoading(isShow);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            ShowLoading(false);
            if (DialogEx.DialogExViewScript.Instance) DialogEx.DialogExViewScript.Instance.ShowDialog((string)param[0]);
        }


        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowHistorys(object[] param)
        {
            PokerHistory[] histories = (PokerHistory[])param[0];

            switch (cateHistory)
            {
                case CateHistory.HISTORY:
                    ShowHistorys(histories);
                    totalPageHistory = (byte)param[1];
                    ShowPageViewHistory();
                    break;
                case CateHistory.COLLECTION:
                    ShowHistorysCollection(histories);
                    totalPageCollection = (byte)param[1];
                    ShowPageViewCollection();
                    break;
            }

            ShowLoading(false);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void AddOrRemoveSuccess(object[] param)
        {
            RequestHistorys(currentPageCollection, cateHistory == CateHistory.COLLECTION);
        }


        private void ShowHistorys(PokerHistory[] histories)
        {
            RecyclePool();
            if (histories != null && histories.Length > 0)
            {
                if (goNoDataHistory) goNoDataHistory.SetActive(false);
                int length = histories.Length;
                for (int i = 0; i < length; i++)
                {
                    var go = PoolManager.Pools[POOL_NAME].Spawn(POOL_ITEM_NAME, Vector3.zero, Quaternion.identity, parrentItemHistory);
                    var Item = go.GetComponent<ItemLobbyHistoryPlayGameView>();
                    if (Item)
                    {
                        Item.SetData(histories[i].TableId, histories[i].GameSeasion, histories[i].HandCard, histories[i].StartTime, histories[i].GameId, histories[i].Blind,
                            histories[i].Ante, histories[i].WinLoseCash, histories[i].IsSave, cateHistory == CateHistory.COLLECTION);
                        Item.callbackClickAddCollection = null;
                        Item.callbackClickAddCollection = CallbackAddCollection;
                    }
                    else PoolManager.Pools[POOL_NAME].Despawn(go.transform);
                }
            }
            else
            {
                if (goNoDataHistory) goNoDataHistory.SetActive(true);
            }
        }

        private void ShowHistorysCollection(PokerHistory[] histories)
        {
            RecyclePool();
            if (histories != null && histories.Length > 0)
            {
                if (goNoDataCollection) goNoDataCollection.SetActive(false);
                int length = histories.Length;
                for (int i = 0; i < length; i++)
                {
                    var go = PoolManager.Pools[POOL_NAME].Spawn(POOL_ITEM_NAME, Vector3.zero, Quaternion.identity, parrentItemCollection);
                    var Item = go.GetComponent<ItemLobbyHistoryPlayGameView>();
                    if (Item)
                    {
                        Item.SetData(histories[i].TableId, histories[i].GameSeasion, histories[i].HandCard, histories[i].StartTime, histories[i].GameId, histories[i].Blind,
                            histories[i].Ante, histories[i].WinLoseCash, histories[i].IsSave, cateHistory == CateHistory.COLLECTION);
                        Item.callbackClickAddCollection = null;
                        Item.callbackClickAddCollection = CallbackAddCollection;
                    }
                    else PoolManager.Pools[POOL_NAME].Despawn(go.transform);
                }
            }
            else
            {
                if (goNoDataCollection) goNoDataCollection.SetActive(true);
            }
        }


        private void ShowPageViewHistory()
        {
            //Debug.Log("ShowPageViewHistory: " + currentPageHistory + " - " + totalPageHistory);
            if (totalPageHistory < 1) totalPageHistory = 1;
            if (txtPageViewHistory) txtPageViewHistory.text = string.Format("{0}/{1}", currentPageHistory, totalPageHistory);
        }

        private void ShowPageViewCollection()
        {
            if (totalPageCollection < 1) totalPageCollection = 1;
            if (txtPageViewCollection) txtPageViewCollection.text = string.Format("{0}/{1}", currentPageCollection, totalPageCollection);
        }

        private void RecyclePool()
        {
            PoolManager.Pools[POOL_NAME].DespawnAll();
        }

        private void CallbackAddCollection(int tableid, int gamesession, string gameId, bool isSave, bool isDeletedHistory)
        {
            //Debug.LogFormat("tableid: {0} - gamesession: {1} - isSave: {2} - isDeletedHistory: {3} - gameId {4}", tableid, gamesession, isSave, isDeletedHistory, gameId);
            Controller.OnHandleUIEvent("RequestRemoveOrAddColectionHistory", tableid, gamesession,  isSave, isDeletedHistory);
        }

        public void OnBtnCateHistoryClicked()
        {
            if (cateHistory == CateHistory.HISTORY) return;
            cateHistory = CateHistory.HISTORY;
            currentPageHistory = 1;
            totalPageHistory = 1;
            RequestHistorys(currentPageHistory);
        }

        public void OnBtnCateCollectionClicked()
        {
            if (cateHistory == CateHistory.COLLECTION) return;
            cateHistory = CateHistory.COLLECTION;
            currentPageCollection = 1;
            totalPageCollection = 1;
            RequestHistorys(currentPageCollection, true);
        }

        public void OnBtnNextHistoryClicked()
        {
            currentPageHistory += 1;
            if (currentPageHistory > totalPageHistory)
            {
                currentPageHistory = totalPageHistory;
                return;
            }
            ShowPageViewHistory();
            RequestHistorys(currentPageHistory);
        }

        public void OnBtnPriviousHistoryClicked()
        {
            currentPageHistory -= 1;
            if (currentPageHistory < 1)
            {
                currentPageHistory = 1;
                return;
            }
            ShowPageViewHistory();
            RequestHistorys(currentPageHistory);
        }

        public void OnBtnNextCollectionClicked()
        {
            currentPageCollection += 1;
            if (currentPageCollection > totalPageCollection)
            {
                currentPageCollection = totalPageCollection;
                return;
            }
            ShowPageViewCollection();
            RequestHistorys(currentPageCollection, true);
        }

        public void OnBtnPriviousCollectionClicked()
        {
            currentPageCollection -= 1;
            if (currentPageCollection < 1)
            {
                currentPageCollection = 1;
                return;
            }
            ShowPageViewCollection();
            RequestHistorys(currentPageCollection, true);
        }
    }
}