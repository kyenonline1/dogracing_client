
using Base.Utils;
using CoreBase;
using CoreBase.Controller;
using GameProtocol.EVN;
using Interface;
using PathologicalGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using Utilites.ObjectPool;
using utils;
using View.DialogEx;

namespace Game.Event
{
    public class EventViewScript : ViewScript
    {
        [SerializeField] private Transform tranParentItem;
        [SerializeField] private UIPopupUtilities popupReadEvent;
        [SerializeField] private Image imgEvent;


        protected override IController CreateController()
        {
            return new EventController(this);
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

            RequestEvent();
        }

        public override void OpenPopup()
        {
            base.OpenPopup();
        }

        public override void ClosePopup()
        {
            base.ClosePopup();
        }


        private void RequestEvent()
        {
            ShowLoading(true);
            Controller.OnHandleUIEvent("RequestEvent");
        }

        private void ShowLoading(bool isShow)
        {
            DialogExViewScript.Instance.ShowLoading(isShow);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowEvents(object[] param)
        {
            try
            {
                EventEntity[] eventEntities = (EventEntity[])param[0];
                if (eventEntities != null && eventEntities.Length > 0)
                {
                    for (int i = 0; i < eventEntities.Length; i++)
                    {
                        var go = PoolManager.Pools["EventBanner"].Spawn("ItemBannerView", Vector3.zero, Quaternion.identity, tranParentItem);
                        go.localPosition = Vector3.zero;
                        go.localScale = Vector3.one;
                        var banner = go.GetComponent<ItemEvent>();
                        if (banner != null)
                        {
                            banner.ShowIcon(eventEntities[i].ImageBanner, i);
                            banner.callbackReadBanner = null;
                            banner.callbackReadBanner = ReadEvent;
                        }
                        else
                        {
                            PoolManager.Pools["EventBanner"].Despawn(go.transform);
                        }
                    }
                }
                ShowLoading(false);
            }
            catch (Exception ex)
            {
                Debug.LogError("ShowEvents: " + ex.Message);
                Debug.LogError("ShowEvents: StackTrace " + ex.StackTrace);
            }
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowContentEvent(object[] param)
        {
            string url = (string)param[0];
            if (popupReadEvent)
            {
                popupReadEvent.OpenPopup();
                if (!string.IsNullOrEmpty(url))
                {
                    imgEvent.gameObject.SetActive(true);
                    imgEvent.color = new Color(1, 1, 1, 0);
                    imgEvent.FadeOut(5, 1);
                    StartCoroutine(ImageLoader.HTTPLoadImage(imgEvent, url));
                }
            }
        }


        private void ReadEvent(int index)
        {
            Controller.OnHandleUIEvent("ReadEvent", index);
        }

    }
}
