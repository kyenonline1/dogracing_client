
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller.GamePlay.Slot;
using UnityEngine.UI;
using DG.Tweening;
using Utilites.ObjectPool;
using GameProtocol.SLC;
using CoreBase;
using Interface;
using CoreBase.Controller;

namespace View.GamePlay.Slot
{
    public class RichestManViewScript : ViewScript
    {

        private ScrollRect scrollRect;
        private Transform tranParentItem,
            tranPopup,
            bgHide;
        private UIMyButton btnClose;
        [SerializeField]
        private GameObject preVinhDanh;

        List<GameObject> lstPools;

        private bool isClose;


        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
            InitData();
            CreatePool();
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
        }

        protected override IController CreateController()
        {
            return new RichestManController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
        }
       

        private void InitData()
        {
            bgHide = transform.Find("bgHide");
            tranPopup = transform.Find("Popup");
            btnClose = tranPopup.Find("Background/btnClose").GetComponent<UIMyButton>();
            scrollRect = tranPopup.Find("Background/Content/ScrollView").GetComponent<ScrollRect>();
            tranParentItem = scrollRect.transform.Find("Content");
            btnClose._onClick.AddListener(CloseRechestMan);
        }
        
        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowDataRichestMan(object[] param)
        {
            try
            {
                isClose = false;
                GameProtocol.SLC.TopGame[] topGame = (GameProtocol.SLC.TopGame[])param[0];
                int lenghth = topGame.Length;
                for (int i = 0; i < lenghth; i++)
                {
                    //GameObject go = ObjectPool.Spawn(ObjectPool.instance.startupPools[1].prefab, tranParentItem, Vector3.zero, Quaternion.identity);
                    lstPools[i].gameObject.SetActive(true);
                    lstPools[i].GetComponent<ItemRichestMan>().InitData(topGame[i].Nickname, topGame[i].Blind, topGame[i].WinCash, i + 1);
                }
                scrollRect.normalizedPosition = Vector3.up;
                isClose = true;
            }
            catch { isClose = true; }
        }
        
        void CreatePool()
        {
            lstPools = new List<GameObject>();
            for (int i = 0; i < 30; i++)
            {
                GameObject go1 = Instantiate(preVinhDanh, Vector3.zero, Quaternion.identity, tranParentItem);
                go1.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                lstPools.Add(go1);
                go1.SetActive(false);
            }
        }

        void DeactivePool()
        {
            for (int i = 0; i < lstPools.Count; i++) lstPools[i].SetActive(false);
        }

        GameObject GetGameObjectItem()
        {
            foreach (GameObject go in lstPools)
            {
                if (!go.activeSelf)
                {
                    go.SetActive(true);
                    return go;
                }
            }
            GameObject go1 = Instantiate(preVinhDanh, Vector3.zero, Quaternion.identity, tranParentItem);
            lstPools.Add(go1);
            go1.SetActive(true);
            return go1;
        }

        public void OpenRichestMan(string GameID)
        {
            OpenPopup();
            if (tranParentItem == null) InitData();
            bgHide.gameObject.SetActive(true);
            isClose = true;
            tranPopup.DOScale(Vector3.one, 0.3f).OnComplete(()=> {
                Controller.OnHandleUIEvent("RequestRichestMan", GameID);
            }).SetEase(Ease.OutBack);
        }

        private void CloseRechestMan()
        {
            if (!isClose) return;
            //tranPopup.DOScale(Vector3.zero, 0.3f).OnComplete(() => {
            //    bgHide.gameObject.SetActive(false);
            //}).SetEase(Ease.InBack);
            ClosePopup();
            DeactivePool();
            //ObjectPool.RecycleAll(ObjectPool.instance.startupPools[1].prefab);
        }
    }
}
