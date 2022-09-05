using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using PathologicalGames;
using utils;

namespace View.Popup
{
    public delegate void OnButtonClick();
    public class PopupViewScript : UIPopupUtilities
    {
        //public static PopupViewScript Instance;
        [HideInInspector]
        public UIMyButton ButtonOk, ButtonCancel,ButtonOkFull;
        [HideInInspector]
        public Text txt_Content, txt_Title;
        private Transform objScale, tranPopup;

        [HideInInspector]
        public float WAIT_TIME;
        private Coroutine IEClosePopup = null;
        private bool isOpen;
		public Action CallbackNoAction = null;

        public Action acctionOkeClicked = null;
        public Action acctionCancelClicked = null;
        //private int importane = -1;
        //private string tittle_keydefault = "THÔNG BÁO"; // Khi lam ngon ngu thi se la key


        void Awake()
        {
            //GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
            //GetComponent<RectTransform>().anchorMin = Vector3.zero;
            //GetComponent<RectTransform>().anchorMax = Vector3.zero;
            //GetComponent<RectTransform>().localScale = Vector3.one;
            InitProperties();
            ButtonOk._onClick.AddListener(OnBtnOkeClicked);
			ButtonOkFull._onClick.AddListener(OnBtnOkeClicked);
            ButtonCancel._onClick.AddListener(OnBtnCancelClicked);
            //callbackOpenPopup = () =>
            //{
            //    GetComponent<RectTransform>().localScale = Vector3.one;
            //    GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
            //};

            callbackClosePopup = () => {
                Debug.Log(" PoolManager.Pools: " + PoolManager.Pools.ContainsKey("PopupView"));
                PoolManager.Pools["PopupView"].Despawn(this.transform); 
            };
        }
        void InitProperties()
        {
            ButtonOk = transform.Find("Popup/BtnOkeCenter").GetComponent<UIMyButton>();
			ButtonOkFull = transform.Find("Popup/BtnOke").GetComponent<UIMyButton>();
            ButtonCancel = transform.Find("Popup/BtnCancel").GetComponent<UIMyButton>();
            txt_Title = transform.Find("Popup/txtTittle").GetComponent<Text>();
            txt_Content = transform.Find("Popup/txtContent").GetComponent<Text>();
        }
        public void OnShow()
        {
            isOpen = true;
            //tranPopup.gameObject.SetActive(true);
            //objScale.DOScale(Vector3.one, 0.3f);

            OpenPopup();

            if (WAIT_TIME > 0)
            {
                if (IEClosePopup != null)
                {
                    StopCoroutine(IEClosePopup);
                    IEClosePopup = null;
                }

                IEClosePopup = StartCoroutine(IEClosePopupWhenUserNotAction());
            }
        }

        private void OnBtnOkeClicked()
        {
            if (acctionOkeClicked != null) acctionOkeClicked();
            if (IEClosePopup != null)
            {
                StopCoroutine(IEClosePopup);
                IEClosePopup = null;
            }
            else
            {
                ClosePopup();
                isOpen = false;
            }
        }

        private void OnBtnCancelClicked()
        {
            if (acctionCancelClicked != null) acctionCancelClicked();
            if (IEClosePopup != null)
            {
                StopCoroutine(IEClosePopup);
                IEClosePopup = null;
            }
            else
            {
                ClosePopup();
                isOpen = false;
            }
        }

        public override void ClosePopup()
        {
            base.ClosePopup();
        }

        IEnumerator IEClosePopupWhenUserNotAction()
        {
            yield return new WaitForSeconds(WAIT_TIME);
			if (CallbackNoAction != null)
				CallbackNoAction ();
            if (isOpen) ClosePopup();

            isOpen = false;
        }
    }
}

