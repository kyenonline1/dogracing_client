using PathologicalGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace View.Home.Shop
{
    public class ItemUserTranferView : MonoBehaviour
    {
        [SerializeField] private InputField ipfUserName;
        [SerializeField] private InputField ipfMoneyTranfer;
        [SerializeField] private InputField ipfDescription;
        [SerializeField] private GameObject goBtnRemove;

        public UnityAction<int> callbackRemoveItem;
        public UnityAction callbackAddMoneyItem;

        private int indexItem;
        public int IndexItem
        {
            private get
            {
                return indexItem;
            }
            set
            {
                indexItem = value;
            }
        }


        private string username;
        public string UserName
        {
            get
            {
                return username;
            }
        }

        private int money;
        public int Money
        {
            get
            {
                return money;
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
        }

        private void Awake()
        {
            if (ipfMoneyTranfer) ipfMoneyTranfer.onEndEdit.AddListener(OnEndValueChange);
        }

        private void OnEnable()
        {
            if (ipfUserName) ipfUserName.text = string.Empty;
            if (ipfMoneyTranfer) ipfMoneyTranfer.text = string.Empty;
            if (ipfDescription) ipfDescription.text = string.Empty;
        }

        private void ShowNotification(string message)
        {
            DialogEx.DialogExViewScript.Instance.ShowNotification(message);
        }

        public bool CheckValidate()
        {
            username = ipfUserName.text;
            if (string.IsNullOrEmpty(username))
            {
                ShowNotification(Languages.Language.GetKey("LOBBY_TRANFER_NO_USER_RECEICED"));
                return false;
            }

            string value = ipfMoneyTranfer.text;
            if (string.IsNullOrEmpty(value))
            {
                ShowNotification(Languages.Language.GetKey("LOBBY_TRANFER_NO_MONEY_RECEICED"));
                return false;
            }
            int.TryParse(value, out money);

            description = ipfDescription.text;
            //if (string.IsNullOrEmpty(description)) return false;

            return true;
        }

        public void OnBtnRemoveItemClicked()
        {
            if (callbackRemoveItem != null) callbackRemoveItem(indexItem);
            PoolManager.Pools["UserTranfer"].Despawn(this.transform);
        }

        public void ShowButtonRemove(bool isShow)
        {
            if (goBtnRemove) goBtnRemove.SetActive(isShow);
        }

        private void OnEndValueChange(string value)
        {
            Debug.Log("OnEndValue Change: " + value);
            int.TryParse(value, out money);
            if (callbackAddMoneyItem != null) callbackAddMoneyItem();
        }
    }
}