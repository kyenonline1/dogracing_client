using CoreBase;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Interface;
using Controller.GamePlay.DuaCho;
using GameProtocol.DOG;
using Utilites.ObjectPool;

namespace View.GamePlay.DuaCho
{
    public class RacingDogHistoryView : ViewScript
    {

        private Transform bgHide,
            tranPopup;
        private UIMyButton btnClose;

        private ScrollRect scrollRect;
        private Transform ContentSesion,
            ContentBeting,
            tranSession,
            tranBetting;

        private Button btnSession,
            btnBetting;

        // Use this for initialization
        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
            InitData();
            RegisterUpdateEvent(ShowItemSession);
            RegisterUpdateEvent(ShowItemBetting);
            RegisterUpdateEvent(ShowError);
        }

        protected override IController CreateController()
        {
            return new RacingDogHistoryController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
            StopAllCoroutines();
        }

        private void InitData()
        {
            bgHide = transform.Find("bgHide");
            tranPopup = transform.Find("Popup");
            btnSession = tranPopup.Find("Content/btnSessionHistory").GetComponent<Button>();
            btnBetting = tranPopup.Find("Content/btnBetHistory").GetComponent<Button>();
            tranSession = tranPopup.Find("Content/HeaderPhien");
            tranBetting = tranPopup.Find("Content/HeaderBetting");
            btnClose = tranPopup.Find("Content/btnClose").GetComponent<UIMyButton>();
            scrollRect = tranPopup.Find("Content/ScrollView").GetComponent<ScrollRect>();
            ContentSesion = tranPopup.Find("Content/ScrollView/ContentSesion");
            ContentBeting = tranPopup.Find("Content/ScrollView/ContentBeting");

            btnSession.onClick.RemoveAllListeners();
            btnBetting.onClick.RemoveAllListeners();
            btnClose._onClick.RemoveAllListeners();

            btnSession.onClick.AddListener(BtnSessionClick);
            btnBetting.onClick.AddListener(BtnBettingClick);
            btnClose._onClick.AddListener(BtnCloseClick);
        }

        private void BtnSessionClick()
        {
            DialogEx.DialogExViewScript.Instance.ShowLoading(true);
            ShowStateButton(true, false);
            scrollRect.content = ContentSesion.GetComponent<RectTransform>();
            Controller.OnHandleUIEvent("RequestGetHistorySession");
        }

        private void BtnBettingClick()
        {
            DialogEx.DialogExViewScript.Instance.ShowLoading(true);
            ShowStateButton(false, true);
            scrollRect.content = ContentBeting.GetComponent<RectTransform>();
            Controller.OnHandleUIEvent("RequestGetHistoryBetting");
        }

        private void ShowStateButton(bool isSession, bool isBetting)
        {
            btnBetting.transform.Find("select").gameObject.SetActive(isBetting);
            btnBetting.transform.Find("unselect").gameObject.SetActive(!isBetting);
            btnSession.transform.Find("select").gameObject.SetActive(isSession);
            btnSession.transform.Find("unselect").gameObject.SetActive(!isSession);
            ContentSesion.gameObject.SetActive(isSession);
            ContentBeting.gameObject.SetActive(isBetting);
            tranSession.gameObject.SetActive(isSession);
            tranBetting.gameObject.SetActive(isBetting);
        }

        private void BtnCloseClick()
        {
            if (DuaCho.RacingDogView.Instance) DuaCho.RacingDogView.Instance.SetOrderSortCanvas(0);
            ObjectPool.RecycleAll(ObjectPool.instance.startupPools[2].prefab);
            ObjectPool.RecycleAll(ObjectPool.instance.startupPools[3].prefab);
            //tranPopup.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).OnComplete(() => {
            //    bgHide.gameObject.SetActive(false);
            //    gameObject.SetActive(false);
            //});

            ClosePopup();
            Controller.OnHandleUIEvent("ClearData");
        }


        public void OpenHistory()
        {
            OpenPopup();
            if (scrollRect == null) InitData();
            bgHide.gameObject.SetActive(true);
            ShowStateButton(true, false);
            tranPopup.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).OnComplete(() => {
                BtnSessionClick();
            });
        }

        private void ShowItemSession(object[] param)
        {
            DialogEx.DialogExViewScript.Instance.ShowLoading(false);
            DogRacingHistory[] dogRacingHistory = (DogRacingHistory[])param[0];
            int length = dogRacingHistory.Length;
            for (int i = 0; i < length; i++)
            {
                GameObject item = ObjectPool.Spawn(ObjectPool.instance.startupPools[2].prefab, ContentSesion, Vector3.zero, Quaternion.identity);
                item.GetComponent<ItemHistoryView>().InitData(dogRacingHistory[i].GameSession, dogRacingHistory[i].CreateTime, dogRacingHistory[i].Result);
            }
            if (scrollRect != null) scrollRect.normalizedPosition = Vector2.up;
        }

        private void ShowItemBetting(object[] param)
        {
            DialogEx.DialogExViewScript.Instance.ShowLoading(false);
            DogRacingHistoryByUser[] dogRacingHistoryByUser = (DogRacingHistoryByUser[])param[0];
            int length = dogRacingHistoryByUser.Length;
            for (int i = 0; i < length; i++)
            {
                GameObject item = ObjectPool.Spawn(ObjectPool.instance.startupPools[3].prefab, ContentBeting, Vector3.zero, Quaternion.identity);
                item.GetComponent<ItemHistoryBetingView>().InitData(dogRacingHistoryByUser[i].GameSession, dogRacingHistoryByUser[i].CreateTime, dogRacingHistoryByUser[i].Result, dogRacingHistoryByUser[i].CashBets, dogRacingHistoryByUser[i].WinCash);
            }
            if (scrollRect != null) scrollRect.normalizedPosition = Vector2.up;
        }

        private void ShowError(object[] param)
        {
            DialogEx.DialogExViewScript.Instance.ShowLoading(false);
        }

    }
}
