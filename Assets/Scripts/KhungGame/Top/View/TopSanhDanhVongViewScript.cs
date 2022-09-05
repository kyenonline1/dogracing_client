using Controller.Home.Top;
using CoreBase;
using CoreBase.Controller;
using GameProtocol.TOP;
using Interface;
using PathologicalGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.DialogEx;

namespace View.Home.Top
{
    public class TopSanhDanhVongViewScript : ViewScript
    {

        [SerializeField] private GameObject goTopUsers;
        /// <summary>
        /// 0: NLH, 1: MTT, 2 CLUB, 3: Bet
        /// </summary>
        [SerializeField] private ItemTopSanhDanhVongView[] topDanhVongs;

        [SerializeField] private Text txtSession;
        [SerializeField] private Transform tranParentItems;
        [SerializeField] private GameObject btnNext;
        [SerializeField] private GameObject btnPrivious;

        private int indexSession = 1;

        private string POOL_NAME_PLAYER = "TopDanhVong";

        private string[] sessions;

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
            return new TopController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
        }

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
        }

        private void RequestTopDanhVong()
        {
            ShowLoading(true);
            Controller.OnHandleUIEvent("RequestUserTopTOP1", sessions[indexSession]);
        }


        private void ShowLoading(bool isShow)
        {
            DialogExViewScript.Instance.ShowLoading(isShow);
        }


        private void RecycleItemMail()
        {
            PoolManager.Pools[POOL_NAME_PLAYER].DespawnAll();
        }


        private void SetActiveButtonNextAndPrivious()
        {
            if(sessions == null || sessions.Length < 2)
            {
                if (btnNext) btnNext.SetActive(false);
                if (btnPrivious) btnPrivious.SetActive(false);
            }
            else
            {
                if(indexSession <= 1)
                {
                    if (btnPrivious) btnPrivious.SetActive(false);
                }
                else
                {
                    if (btnPrivious) btnPrivious.SetActive(true);
                }

                if (indexSession >= sessions.Length -1)
                {
                    if (btnNext) btnNext.SetActive(false);
                }
                else
                {
                    if (btnNext) btnNext.SetActive(true);
                }
            }
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            ShowLoading(false);
            DialogExViewScript.Instance.ShowDialog((string)param[0]);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowUserTops(object[] param)
        {
            TOP1EventResponse response = (TOP1EventResponse)param[0];

            RecycleItemMail();

            if (goTopUsers) goTopUsers.SetActive(true);

            if (response.NLHs != null && response.NLHs.Length > 0)
            {
                ShowItemCate(Languages.Language.GetKey("LOBBY_TOP_LOBBY"));
                ShowListItemUserTop(response.NLHs, 0);
                if (topDanhVongs != null) topDanhVongs[0].SetData(response.NLHs[0].Name, response.NLHs[0].Avatar);
            }
            else
            {
                if (topDanhVongs != null) topDanhVongs[0].gameObject.SetActive(false);
            }

            if (response.MTTs != null && response.MTTs.Length > 0)
            {
                ShowItemCate(Languages.Language.GetKey("LOBBY_TOP_MTT"));
                ShowListItemUserTop(response.MTTs, 0);
                if (topDanhVongs != null) topDanhVongs[1].SetData(response.MTTs[0].Name, response.MTTs[0].Avatar);
            }
            else
            {
                if (topDanhVongs != null) topDanhVongs[1].gameObject.SetActive(false);
            }

            if (response.CLUBs != null && response.CLUBs.Length > 0)
            {
                ShowItemCate(Languages.Language.GetKey("LOBBY_TOP_CLUB"));
                ShowListItemUserTop(response.CLUBs, 1);
                if (topDanhVongs != null) topDanhVongs[2].SetData(response.CLUBs[0].Name, response.CLUBs[0].Avatar);
            }
            else
            {
                if (topDanhVongs != null) topDanhVongs[2].gameObject.SetActive(false);
            }

            if (response.BETs != null && response.BETs.Length > 0)
            {
                ShowItemCate(Languages.Language.GetKey("LOBBY_TOP_TOTAL_BET"));
                ShowListItemUserTop(response.BETs, 0);
                if (topDanhVongs != null) topDanhVongs[3].SetData(response.BETs[0].Name, response.BETs[0].Avatar);
            }
            else
            {
                if (topDanhVongs != null) topDanhVongs[3].gameObject.SetActive(false);
            }

            ShowLoading(false);
        }

        private void ShowItemCate(string catename)
        {
            var go = PoolManager.Pools[POOL_NAME_PLAYER].Spawn("ItemTopType", Vector3.zero, Quaternion.identity, tranParentItems);
            go.localScale = Vector3.one;
            go.localPosition = Vector3.zero;
            var itemCate = go.GetComponent<ItemCateTypeView>();
            if (itemCate != null)
            {
                itemCate.SetData(catename);
            }
            else PoolManager.Pools[POOL_NAME_PLAYER].Despawn(go.transform);
        }

        private void ShowListItemUserTop(TopEvent[] tops, int itemType)
        {
            int length = tops.Length;
            for (int i = 0; i < length; i++)
            {
                var go = PoolManager.Pools[POOL_NAME_PLAYER].Spawn("ItemTopPlayerView", Vector3.zero, Quaternion.identity, tranParentItems);
                go.localScale = Vector3.one;
                go.localPosition = Vector3.zero;
                var itemTop = go.GetComponent<ItemTopPlayerView>();
                if (itemTop != null)
                {
                    itemTop.SetData(i, tops[i].Name, tops[i].Avatar, tops[i].Score, itemType);
                }
                else PoolManager.Pools[POOL_NAME_PLAYER].Despawn(go.transform);
            }
        }


        public void OpenDanhVong(string[] sessions)
        {
            this.sessions = sessions;
            SetActiveButtonNextAndPrivious();
            OpenPopup();
            callbackOpenPopup = () =>
            {
                if (sessions != null || sessions.Length > 1)
                {
                    indexSession = 1;
                    RequestTopDanhVong();
                    if (txtSession) txtSession.text = sessions[indexSession];
                }
                else if (txtSession) txtSession.text = string.Empty;
            };
        }


        public void OnBtnNextClicked()
        {
            indexSession += 1;
            if (indexSession >= sessions.Length) indexSession = sessions.Length - 1;
            SetActiveButtonNextAndPrivious();
            RequestTopDanhVong();
        }

        public void OnBtnPriviousClicked()
        {
            indexSession -= 1;
            if (indexSession < 1) indexSession = 1;
            SetActiveButtonNextAndPrivious();
            RequestTopDanhVong();
        }

    }
}
