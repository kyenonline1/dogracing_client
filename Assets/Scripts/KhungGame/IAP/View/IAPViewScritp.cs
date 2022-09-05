using AppConfig;
using AssetBundles;
using Base;
using Controller.Home.IAP;
using CoreBase;
using CoreBase.Controller;
using DG.Tweening;
using Game.Gameconfig;
using GameProtocol.PAY;
using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;
using Utilites.ObjectPool;
using View.DialogEx;

namespace View.Home.IAP
{
    public class IAPViewScritp : ViewScript
    {
        [SerializeField]
        private List<IAPItemView> lstPackages;



#if UNITY_IAP
        private Purchaser purchaser;
#endif

        public override void ClosePopup()
        {
            base.ClosePopup();
        }

        public override void OpenPopup()
        {
            base.OpenPopup();
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
        }

        protected override IController CreateController()
        {
            return new IAPController(this);
        }

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
            InitProperties();
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
        }

        private void OnEnable()
        {
            DOVirtual.DelayedCall(0.1f, () =>
            {
                Controller.OnHandleUIEvent("RequestPAY1");
            });
        }

        private void InitProperties()
        {
#if UNITY_IAP
            purchaser = GetComponent<Purchaser>();
#endif
        }


        private void ShowLoading(bool isShow)
        {
            DialogExViewScript.Instance.ShowLoading(isShow);
        }

        private string GetName(string str)
        {
            string[] strs = str.Split('_');
            return strs[0];
        }

        private void DisableListItem()
        {
            for (int i = 0; i < lstPackages.Count; i++) lstPackages[i].gameObject.SetActive(false);
        }

#region Controller to View

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            ShowLoading(false);
            DialogExViewScript.Instance.ShowNotification((string)param[0]);
        }

        Package[] Packages;

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowPackages(object[] param)
        {
            DisableListItem();
            ShowLoading(false);
           Packages = (Package[])param[0];
            int length = Packages.Length;
#if UNITY_IAP
            if (purchaser) purchaser.InitializePurchasing(Packages);
#endif
            for (int i = 0; i < length; i++)
            {
                lstPackages[i].gameObject.SetActive(true);
                lstPackages[i].InitDataIAP((long)(Packages[i].Gold * Packages[i].Rate), Packages[i].Gold);
                lstPackages[i].GetComponent<UIMyButton>()._onClick.RemoveAllListeners();
                int index = i;
                lstPackages[i].GetComponent<UIMyButton>()._onClick.AddListener(()=> {
                    OnBtnPackageClicked(index);
                });
            }
        }
#endregion

        public void OnBtnPackageClicked(int index)
        {
#if UNITY_IAP
            if (purchaser) purchaser.BuyProductID(Packages[index].ProductId);
#endif
        }

        public void PurchaserSuccessGoogle(string SignedData, string Signature)
        {
            Controller.OnHandleUIEvent("RequestPAY4GoogleBilling", SignedData, Signature);
        }

        public void PurchaserSuccessApple(string ProductId, string transactionId, string receiptStr)
        {
            Controller.OnHandleUIEvent("RequestPAY3AppleBillind", ProductId, transactionId, receiptStr);
        }
    }
}
