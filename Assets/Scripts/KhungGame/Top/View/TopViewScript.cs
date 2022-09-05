
using AssetBundles;
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
using Utilites;
using Utilites.ObjectPool;
using View.DialogEx;

namespace View.Home.Top
{
    public enum TopPlayer
    {
        NONE = -1,
        NORMAL,
        MTT,
        CLB,
        TOTALBET
    }

    public class TopViewScript : ViewScript
    {

        [SerializeField] private UIToggleTabsCate cates;
        [SerializeField] private Transform tranParentItem;
        [SerializeField] private GameObject txtNoData;
        [SerializeField] private GameObject goUserTop;
        [SerializeField] private Text txtSession;
        [SerializeField] private Text txtRemainTime;

        [SerializeField] private Image imgUserTop1;
        [SerializeField] private Text txtPlayerTop1;
        [SerializeField] private Text txtMoneyTop1;

        private TOP1EventResponse response;
        private string[] sessions;

        private string POOL_NAME = "TopPlayers";

        private TopPlayer topPlayerCate = TopPlayer.NONE;

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

        private void OnEnable()
        {
            if (cates) cates.SelectCate(0);
            topPlayerCate = TopPlayer.NORMAL;
            StartCoroutine(Utils.DelayAction(RequestTop, 0.01f));
        }

        private void RequestTop()
        {
            ShowLoading(true);
            if (response != null)
            {
                ShowUserTops(new object[] { response });
                return;
            }
            Controller.OnHandleUIEvent("RequestTopCate");
        }

        private void ShowLoading(bool isShow)
        {
            DialogExViewScript.Instance.ShowLoading(isShow);
        }

        private void RecycleItemMail()
        {
            PoolManager.Pools[POOL_NAME].DespawnAll();
        }

        public void OnBtnClickCate(int indexCate)
        {
            if ((int)topPlayerCate == indexCate) return;
            else topPlayerCate = (TopPlayer)indexCate;
            RequestTop();
        }


        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            ShowLoading(false);
            DialogExViewScript.Instance.ShowDialog((string)param[0]);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void CacheSessions(object[] param)
        {
            sessions = (string[])param[0];
            if (sessions != null && sessions.Length > 0)
            {
                if (txtSession) txtSession.text = sessions[0];
            }
        }

        /// <summary>
        /// Chưa có sự kiện đua top nào: ẩn toàn bộ nội dung
        /// </summary>
        /// <param name="param"></param>
        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void DisableViewTop(object[] param)
        {
            ShowLoading(false);
            if (txtNoData) txtNoData.SetActive(true);
            if (goUserTop) goUserTop.SetActive(false);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowUserTops(object[] param)
        {
            response = (TOP1EventResponse)param[0];

            switch (topPlayerCate)
            {
                case TopPlayer.NORMAL:
                    if (response.NLHs != null && response.NLHs.Length > 0)
                    {
                        ShowListItemUserTop(response.NLHs);
                        ShowUserTopInfo(response.NLHs[0]);
                    }
                    else DisableViewTop(null);
                    break;
                case TopPlayer.MTT:
                    if (response.MTTs != null && response.MTTs.Length > 0)
                    {
                        ShowListItemUserTop(response.MTTs);
                        ShowUserTopInfo(response.MTTs[0]);
                    }
                    else DisableViewTop(null);
                    break;
                case TopPlayer.CLB:
                    if (response.CLUBs != null && response.CLUBs.Length > 0)
                    {
                        ShowListItemUserTop(response.CLUBs);
                        ShowUserTopInfo(response.CLUBs[0]);
                    }
                    else DisableViewTop(null);
                    break;
                case TopPlayer.TOTALBET:
                    if (response.BETs != null && response.BETs.Length > 0)
                    {
                        ShowListItemUserTop(response.BETs);
                        ShowUserTopInfo(response.BETs[0]);
                    }
                    else DisableViewTop(null);
                    break;
            }
            if (!string.IsNullOrEmpty(response.TimeRemain))
                ShowSessionTime(response.TimeRemain);
            ShowLoading(false);
        }

        private void ShowSessionTime(string timer)
        {
            //System.DateTime session = System.DateTime.ParseExact(timer, "dd-MM-yy HH:mm:ss",
            //                      System.Globalization.CultureInfo.InvariantCulture);
            if (txtRemainTime) txtRemainTime.text = timer;// string.Format(Languages.Language.GetKey("LOBBY_TOP_TOP_REMAINTIME"), session.Day, session.Hour);
        }

        private void ShowListItemUserTop(TopEvent[] tops)
        {
            int length = tops.Length;
            for(int i = 0; i < length; i++)
            {
                var go = PoolManager.Pools[POOL_NAME].Spawn("ItemTopPlayerView", Vector3.zero, Quaternion.identity, tranParentItem);
                go.localScale = Vector3.one;
                go.localPosition = Vector3.zero;
                var itemTop = go.GetComponent<ItemTopPlayerView>();
                if (itemTop != null)
                {
                    itemTop.SetData(i, tops[i].Name, tops[i].Avatar, tops[i].Score, topPlayerCate == TopPlayer.CLB ? 1 : 0);
                }
                else PoolManager.Pools[POOL_NAME].Despawn(go.transform);
            }
        }

        private void ShowUserTopInfo(TopEvent top)
        {
            if (goUserTop) goUserTop.SetActive(true);
            if (imgUserTop1) LoadAvatar(top.Avatar, topPlayerCate == TopPlayer.CLB ? 1 : 0);
            if (txtPlayerTop1) txtPlayerTop1.text = top.Name;
            if (txtMoneyTop1) txtMoneyTop1.text = MoneyHelper.FormatNumberAbsolute(top.Score);
        }

        private void LoadAvatar(string avatar, int itemType)
        {
            if (imgUserTop1)
            {
                if (avatar.Contains("http"))
                {
                    StartCoroutine(ImageLoader.HTTPLoadImage(imgUserTop1, avatar));
                }
                else
                {
                    if (itemType == 0) //Player
                    {
                        string str = (int.Parse(avatar) < 10) ? string.Format("avatar0{0}", avatar) : string.Format("avatar{0}", avatar);
                        //LoadAssetBundle.LoadSpriteToAtlas(TagAssetBundle.Tag_UI.TAG_UI_COMMON, TagAssetBundle.AtlasName.LOBBY_AVATAR, str, (sprite) =>
                        //{
                        //    if (sprite != null && imgUserTop1 != null) imgUserTop1.sprite = sprite;
                        //});
                    }
                    else // ClUb
                    {
                        string str = (int.Parse(avatar) < 10) ? string.Format("club_ava_{0}", avatar) : string.Format("club_ava_{0}", avatar);
                        //LoadAssetBundle.LoadSpriteToAtlas(TagAssetBundle.Tag_UI.TAG_UI_COMMON, TagAssetBundle.AtlasName.LOBBY_AVATAR, str, (sprite) =>
                        //{
                        //    if (sprite != null && imgUserTop1 != null) imgUserTop1.sprite = sprite;
                        //});
                    }
                }
            }
        }

        public void OnBtnSanhDanhVongClicked()
        {
            if(sessions == null || sessions.Length < 2)
            {
                DialogExViewScript.Instance.ShowDialog("HOME_DIALOG_NODATA");
                return;
            }
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowTopDanhVong(sessions);
        }
    }
}
