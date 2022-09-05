using CoreBase;
using CoreBase.Controller;
using Interface;
using PathologicalGames;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Utilites;

namespace View.Home.Shop
{
    public class TranferUserViewScript : ViewScript
    {
        [SerializeField] private Transform parentItems;
        [SerializeField] private InputField ipfCapcha;
        [SerializeField] private Image imgCapcha;
        [SerializeField] private Text txtTotalMoneyTranfer;
        [SerializeField]
        private List<ItemUserTranferView> itemUsers;

        private int TotalMoneyTranfer;

        #region Override function
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
            return new TranferController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
        }

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();

            itemUsers = new List<ItemUserTranferView>();
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
        }
        #endregion

        private void OnEnable()
        {
            ResetData();
        }

        private void ResetData()
        {
            itemUsers.Clear();
            
            RecycleItem();
            AddItemUserTranfer();
            TotalMoneyTranfer = 0;
            if (ipfCapcha) ipfCapcha.text = string.Empty;
            if (txtTotalMoneyTranfer) txtTotalMoneyTranfer.text = "0";
            StartCoroutine(Utils.DelayAction(RequestCapcha, 0.01f));
        }

        private void RecycleItem()
        {
            PoolManager.Pools["UserTranfer"].DespawnAll();
        }

        private void RequestCapcha()
        {
            Controller.OnHandleUIEvent("RequestCapcha");
        }

        private void AddMoneyTranfer()
        {
            if (itemUsers != null && itemUsers.Count > 0)
            {
                TotalMoneyTranfer = itemUsers.Sum(x => x.Money);
                if (txtTotalMoneyTranfer) txtTotalMoneyTranfer.text = MoneyHelper.FormatNumberAbsolute(TotalMoneyTranfer);
            }else if (txtTotalMoneyTranfer) txtTotalMoneyTranfer.text = "0";
        }

        private void AddItemUserTranfer()
        {
            var go = PoolManager.Pools["UserTranfer"].Spawn("ItemUserTranferView", Vector3.zero, Quaternion.identity, parentItems);
            go.localScale = Vector3.one;
            go.SetAsLastSibling();
            var item = go.GetComponent<ItemUserTranferView>();
            if (item)
            {
                itemUsers.Add(item);
                item.IndexItem = itemUsers.Count - 1;
                item.callbackRemoveItem = null;
                item.callbackRemoveItem = RemoveItemUser;

                item.callbackAddMoneyItem = null;
                item.callbackAddMoneyItem = AddMoneyTranfer;
            }
            else PoolManager.Pools["UserTranfer"].Despawn(go);
            CheckListUserEnableButtonRemove();
        }

        private void RemoveItemUser(int index)
        {
            Debug.Log("RemoveItemUser: " + index + ", itemUsers.Count: " + itemUsers.Count);
            if (itemUsers != null && itemUsers.Count > 0)
            {
                if (index < itemUsers.Count)
                {
                    itemUsers.RemoveAt(index);
                    CheckListUserEnableButtonRemove();
                    ResetIndexItem();
                }
            }
            AddMoneyTranfer();
        }

        private void ResetIndexItem()
        {
            if (itemUsers != null && itemUsers.Count > 0)
            {
                for (int i = 0; i < itemUsers.Count; i++)
                {
                    itemUsers[i].IndexItem = i;
                }
            }
        }
        private void CheckListUserEnableButtonRemove()
        {
            Debug.Log("itemUsers.Count = " + itemUsers.Count);
            if (itemUsers != null && itemUsers.Count > 0)
            {
                int count = itemUsers.Count;
                if (count > 1)
                {
                    for (int i = 0; i < count; i++) itemUsers[i].ShowButtonRemove(true);
                }
                else
                {
                    for (int i = 0; i < count; i++) itemUsers[i].ShowButtonRemove(false);
                }
            }
        }

        private void ShowLoading(bool isShow)
        {
            DialogEx.DialogExViewScript.Instance.ShowLoading(isShow);
        }

        private void ShowNotification(string message)
        {
            DialogEx.DialogExViewScript.Instance.ShowNotification(message);
        }

        public void OnBtnAddUserClicked()
        {
            if (itemUsers == null) return;
            if(itemUsers.Count == 5)
            {
                ShowNotification(Languages.Language.GetKey("LOBBY_TRANFER_MAX_ADDUSER"));
                return;
            }
            AddItemUserTranfer();
        }

        public void OnBtnCapchaClicked()
        {
            ShowLoading(true);
            RequestCapcha();
        }

        public void OnBtnTranferClicked()
        {
            if (itemUsers == null || itemUsers.Count < 1)
            {
                ShowNotification(Languages.Language.GetKey("LOBBY_TRANFER_NO_ADDUSER"));
                return;
            }

            bool isDone = false;
            for(int i = 0; i < itemUsers.Count; i++)
            {
                isDone = itemUsers[i].CheckValidate();
                if (isDone) continue;
                else return;
            }

            string capcha = ipfCapcha.text;
            if (string.IsNullOrEmpty(capcha))
            {
                ShowNotification(Languages.Language.GetKey("LOBBY_TRANFER_NO_CAPCHA"));
                return;
            }

            if (isDone)
            {
                List<string> usernames = new List<string>();
                List<int> moneys = new List<int>();
                List<string> descriptions = new List<string>();
                for (int i = 0; i < itemUsers.Count; i++)
                {
                    usernames.Add(itemUsers[i].UserName);
                    moneys.Add(itemUsers[i].Money);
                    descriptions.Add(itemUsers[i].Description);
                }
                ShowLoading(true);
                RequestCapcha();
                ShowNotification("Chuyển khoản thành công.");
                Controller.OnHandleUIEvent("RequestTranferCoin", new object[] { usernames.ToArray(), moneys.ToArray(), descriptions.ToArray(), capcha });
            }
        }


        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowError(object[] param)
        {
            ShowLoading(false);
            ShowNotification((string)param[0]);
        }

        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ClearData(object[] param)
        {
            ResetData();
        }


        [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowCapcha(object[] param)
        {
            ShowLoading(false);
            string urlcapcha = (string)param[0];
            StartCoroutine(LoadCapcha(urlcapcha));
        }

        private IEnumerator LoadCapcha(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                using (UnityWebRequest wr = new UnityWebRequest(url))
                {
                    DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
                    wr.downloadHandler = texDl;
                    yield return wr.SendWebRequest();
                    if (!(wr.isNetworkError || wr.isHttpError))
                    {
                        Texture2D t = texDl.texture;
                        Sprite s = Sprite.Create(t, new Rect(0, 0, t.width, t.height),
                                                 Vector2.zero, 1f);
                        imgCapcha.sprite = s;
                    }
                    else ShowNotification(Languages.Language.GetKey("HOME_DIALOG_LOADCAPCHA_ERROR"));
                }
            }
            yield return null;
        }

    }
}