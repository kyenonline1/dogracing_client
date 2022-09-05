using UnityEngine;
using System.Collections;
using Interface;
using System;
using System.Collections.Generic;
using System.Reflection;
using Utilities;
using CoreBase.Controller;
using DG.Tweening;
using utils;

namespace CoreBase
{
    [System.Reflection.Obfuscation(Feature = "renaming", Exclude = true)]
    internal class HandleUpdateEventAttribute : Attribute
    {
        public HandlerType EventType { get; set; }
    }
    [System.Reflection.Obfuscation(Feature = "renaming", Exclude = true)]
    public class ViewScript : MonoBehaviour, IView
    {
        protected delegate void DlgUpdateView(params object[] parameters);

        /// <summary>
        /// Dictionary<method_name, DlgUpdateView> - quản lý các PushHandler
        /// </summary>
        private Dictionary<string, DlgUpdateView> ViewUpdateEvents = new Dictionary<string, DlgUpdateView>();

        protected IController Controller = null;

        protected Action callbackClosePopup, callbackOpenPopup;

        [SerializeField] private CanvasGroup cvGroupPopup;

        public ScaleType typePopup = ScaleType.ScaleUp;

        protected ViewScript()
        {
            //LogMng.Log("ViewScript", "Contructor ViewScript called...");
        }

        void Awake()
        {
            InitGameScriptInAwake();
        }

        /// <summary>
        /// goi trong Awake
        /// </summary>
        protected virtual void InitGameScriptInAwake()
        {
            RegisterUpdateEvents();
        }

        // Use this for initialization
        void Start()
        {
            Controller = CreateController();
            if (Controller != null)
                Controller.StartController();
            InitGameScriptInStart();
        }

        /// <summary>
        /// goi trong Start
        /// </summary>
        protected virtual void InitGameScriptInStart()
        {

        }


        public virtual void OpenPopup()
        {
            gameObject.SetActive(true);
            if (cvGroupPopup)
            {
                switch (typePopup)
                {
                    case ScaleType.ScaleDown:
                    case ScaleType.ScaleUp:
                        cvGroupPopup.transform.localScale = typePopup == ScaleType.ScaleUp ? Vector3.zero : Vector3.one * 1.5f;
                        cvGroupPopup.transform.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack);
                        cvGroupPopup.DOFade(1, 0.35f).OnComplete(() => {
                            if (callbackOpenPopup != null) callbackOpenPopup();
                        });
                        break;
                    case ScaleType.Move:
                        cvGroupPopup.transform.localPosition = new Vector3(-400, 0, 0);
                        cvGroupPopup.transform.DOLocalMove(Vector3.zero, 0.35f).SetEase(Ease.OutBack);
                        cvGroupPopup.DOFade(1, 0.35f).OnComplete(() => {
                            if (callbackOpenPopup != null) callbackOpenPopup();
                        });
                        break;
                }
               
            }
        }


        public virtual void ClosePopup()
        {
            if (cvGroupPopup)
            {
                switch (typePopup)
                {
                    case ScaleType.ScaleDown:
                    case ScaleType.ScaleUp:
                        cvGroupPopup.transform.DOScale(Vector3.zero, 0.35f).SetEase(Ease.InBack);
                        cvGroupPopup.DOFade(0, 0.35f).OnComplete(() => {
                            if (callbackClosePopup != null) callbackClosePopup();
                            gameObject.SetActive(false);
                        });
                        break;
                    case ScaleType.Move:
                        cvGroupPopup.transform.DOLocalMove(new Vector3(-400, 0, 0), 0.35f).SetEase(Ease.OutBack);
                        cvGroupPopup.DOFade(0, 0.35f).OnComplete(() => {
                            if (callbackOpenPopup != null) callbackClosePopup();
                            gameObject.SetActive(false);
                        });
                        break;
                }
               
            }
        }

        /// <summary>
        /// Register các hàm gọi từ Controller -> View
        /// </summary>
        /// <param name="duv"></param>
        protected void RegisterUpdateEvent(DlgUpdateView duv)
        {
            ViewUpdateEvents.Add(duv.Method.Name, duv);
        }

        protected void RegisterUpdateEvents()
        {
            //LogMng.Log("UIController", "RegisterEventHandlers: " + GetType());
            MethodInfo[] methods = this.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            //LogMng.Log("UIController", "methods.count: " + methods.Length);

            foreach (MethodInfo m in methods)
            {
                var attributes = (HandleUIEventAttribute[])m.GetCustomAttributes(typeof(HandleUIEventAttribute), false);
                if (attributes == null || attributes.Length == 0) continue;
                var attribute = attributes[0];
                HandlerType event_type = attribute.EventType;
                //LogMng.Log("ViewScript", "attribute: " + (HandlerType)event_type);
                if (event_type == HandlerType.EVN_UPDATEUI_HANDLER)
                {
                    //LogMng.Log("UIController", "RegisterEventHandler: " + m.Name);
                    RegisterUpdateEvent((DlgUpdateView)Delegate.CreateDelegate(typeof(DlgUpdateView), this, m.Name));
                }
            }
        }


        /// <summary>
        /// goi trong start
        /// </summary>
        /// <returns></returns>
        protected virtual IController CreateController()
        {
            throw new NotImplementedException();

        }

        void IView.OnUpdateView(string Function, params object[] Params)
        {
            //LogMng.Log("ViewScript", "Call OnUpdateView: " + Function);
            if (ViewUpdateEvents.ContainsKey(Function))
                ViewUpdateEvents[Function](Params);
            else
                LogMng.Log("ViewScript", "Event " + Function + " has not been declaced !!!");
        }

        void OnDestroy()
        {
            DOTween.Complete(this);
            DestroyGameScript();
        }

        private void OnDisable()
        {
            //DOTween.Kill(this);
        }

        /// <summary>
        /// goi trong ondestroy
        /// </summary>
        protected virtual void DestroyGameScript()
        {
            if (Controller != null)
                Controller.StopController();
            this.StopAllCoroutines();
        }
    }
}
