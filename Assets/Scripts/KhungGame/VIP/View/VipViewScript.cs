//using Base;
//using Controller.Home.Vip;
//using GameProtocol.TOP;
//using UnityEngine;
//using Utilites.ObjectPool;
//using View.DialogEx;
//using View.Home.Lobby;

//namespace View.Home.Vip
//{
//    public class VipViewScript : UIViewScript
//    {

//        private Transform parrentVip;
//        private UIMyButton btnCloseVip;

//        // Use this for initialization
//        private void Awake()
//        {
//            RunAwakeEvent();
//            InitProperties();
//            AddListenerButton();
//        }
//        private void RunAwakeEvent()
//        {
//            StartView(new VipController(), "VipViewScript");
//            Controller.StartController(this, "VipController");
//        }
        

//        private void InitProperties()
//        {
//            parrentVip = transform.Find("Popup/ScrollView/Viewport/Content");
//            btnCloseVip = transform.Find("Popup/BtnCloseVip").GetComponent<UIMyButton>();
//        }

//        private void AddListenerButton()
//        {
//            btnCloseVip._onClick.AddListener(BtnClickCloseVip);
//        }

//        private void BtnClickCloseVip()
//        {
//            ObjectPool.RecycleAll(ObjectPool.instance.startupPools[12].prefab);
//            gameObject.SetActive(false);
//        }

//        private void ShowLoading(bool isShow)
//        {
//            DialogExViewScript.Instance.ShowLoading(isShow);
//        }

//        #region Update view by Contrller

//        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
//        private void ShowError(object[] param)
//        {
//            ShowLoading(false);
//            DialogExViewScript.Instance.ShowNotification((string)param[0]);
//        }

//        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
//        private void ShowListVips(object[] param)
//        {
//            VipCate[] vips = (VipCate[])param[0];
//            int length = vips.Length;
//            ShowLoading(false);
//            for (int i = 0; i < length; i++)
//            {
//                //Debug.Log("VipType: " + vips[i].VipType);
//                GameObject vip = ObjectPool.Spawn(ObjectPool.instance.startupPools[12].prefab, parrentVip, Vector3.zero, Quaternion.identity);
//                vip.GetComponent<ItemVipView>().SetData(vips[i].VipName, vips[i].MinVip, vips[i].VipType, OpenIAP);
//            }
//        }

//        #endregion

//        private void OpenIAP()
//        {
//            BtnClickCloseVip();
//            LobbyViewScript.Instance.BtnClickIAP();
//        }

//        public void OpenVip()
//        {
//            if (btnCloseVip == null)
//            {
//                InitProperties();
//                AddListenerButton();
//            }
//            UpdateToController("RequestListVip");
//        }

//        public override void OnDestroyProcessor()
//        {
//            base.OnDestroyProcessor();
//        }
//    }
//}
