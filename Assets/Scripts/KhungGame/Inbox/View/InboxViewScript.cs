
using Base.Utils;
using Controller.Home.Inbox;
using CoreBase;
using CoreBase.Controller;
using Game.Gameconfig;
using GameProtocol.ANN;
using Interface;
using PathologicalGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites.ObjectPool;
using utils;
using View.DialogEx;

namespace View.Home.Inbox
{
    public class InboxViewScript : ViewScript
    {
        [SerializeField] private UIToggleTabsCate cates;
        [SerializeField] private Transform tranParentItem;
        [SerializeField] private GameObject txtNoData;

        [SerializeField] private UIPopupUtilities popupReading;
        [SerializeField] private Text txtTittleReading;
        [SerializeField] private Text txtContentReading;
        [SerializeField] private Text txtTimeEmail;

        private string POOL_NAME = "Emails";
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
            return new InboxController(this);
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
            ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_type = ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_Type.ANNOUNCEMENT;
            StartCoroutine(Utils.DelayAction(RequestMail, 0.01f));
        }

        private void OnDisable()
        {
            RecycleItemMail();
            Controller.OnHandleUIEvent("ClearData");
        }

        private void RequestMail()
        {
            ShowLoading(true);
            Controller.OnHandleUIEvent("RequestAnnoucementANN0");
        }

        private void ShowLoading(bool isShow)
        {
            DialogExViewScript.Instance.ShowLoading(isShow);
        }

        private void RecycleItemMail()
        {
            PoolManager.Pools[POOL_NAME].DespawnAll();
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            ShowLoading(false);
            DialogExViewScript.Instance.ShowDialog((string)param[0]);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowEmails(object[] param)
        {
            Announce[] mails = (Announce[])param[0];

            RecycleItemMail();
            if (mails == null || mails.Length < 1)
            {
                if (txtNoData) txtNoData.SetActive(true);
            }
            else
            {
                if (txtNoData) txtNoData.SetActive(false);
                for(int i = 0; i < mails.Length; i++)
                {
                    var go = PoolManager.Pools[POOL_NAME].Spawn("ItemEmailView", Vector3.zero, Quaternion.identity, tranParentItem);
                    go.localScale = Vector3.one;
                    ItemInboxView inbox = go.GetComponent<ItemInboxView>();
                    if (inbox != null)
                    {
                        inbox.SetData(mails[i].AnnouneId, mails[i].Title, mails[i].StartTime, mails[i].EndTime, mails[i].State, mails[i].Type);
                        inbox.callbackClickEmail = ReadingMail;
                        inbox.callbackClickRemoveEmail = RemoveMail;
                    }
                    else PoolManager.Pools[POOL_NAME].Despawn(go.transform);
                }
            }

            ShowLoading(false);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ReadInbox(object[] param)
        {
            Announce mail = (Announce)param[0];
            if(mail != null)
            {
                if (popupReading) popupReading.OpenPopup();
                if (txtTittleReading) txtTittleReading.text = mail.Title;
                if (txtContentReading) txtContentReading.text = mail.Content;
                if (txtTimeEmail) txtTimeEmail.text = string.Format("{0} - {1}", mail.StartTime, mail.EndTime);
            }
        }

        private void ReadingMail(int mailid)
        {
            Controller.OnHandleUIEvent("ReadingEmail", mailid);
        }

        private void RemoveMail(int mailid)
        {
            Controller.OnHandleUIEvent("RemoveEmail", mailid);
        }

        public void OnBtnCateEmailClicked(int index)
        {
            if (ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_type == (ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_Type)index) return;
            ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_type = (ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_Type)index;
            RequestMail();
        }
    }
}
