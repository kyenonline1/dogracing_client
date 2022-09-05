using AppConfig;
using System;
using UnityEngine;
using UnityEngine.UI;
using Utilites;
using Utilities;

namespace View.Home.ExChange
{
    public class ItemExChangeView : MonoBehaviour
    {

        public delegate void dlgClickItem(string id, int price);
        public dlgClickItem EventClickItem;

        public Dropdown dropdown;
        public float rate;

        public Image icon;
        public UIMyButton buttonExChange;
        int[] Prices;
        private string ID;
        private string telcoName;
        private string strConfirm;

        // Use this for initialization
        private void Awake()
        {
            buttonExChange._onClick.AddListener(BtnClickExChangeItem);
        }

        public void InitData(string telconame, Sprite spr, string _id, int[] prices, float _rate)
        {
            Prices = prices;
            ID = _id;
            icon.sprite = spr;
            telcoName = telconame;
            rate = _rate;
            dropdown.options.Clear();
            LogMng.Log("ItemDT ", prices.Length);
            for (int i = 0; i < prices.Length; i++)
            {
                dropdown.options.Add(new Dropdown.OptionData(MoneyHelper.FormatNumberAbsolute(prices[i])));
            }
            dropdown.captionText.text = MoneyHelper.FormatNumberAbsolute(prices[0]);
        }

        public void BtnClickExChangeItem()
        {
            //Debug.Log("BtnClickExChangeItem " + (EventClickItem != null));
            long value =  /*Prices[dropdown.value] + */ (int)Math.Round(Prices[dropdown.value] * rate);
            if(ClientConfig.UserInfo.GOLD < value)
            {
                DialogEx.DialogExViewScript.Instance.ShowDialog("Số Chip Vàng không đủ để thực hiện.");
                return;
            }
            strConfirm = string.Format("Bạn có muốn đổi <color=yellow> {0} </color> với giá <color=yellow> {1} </color>", telcoName, MoneyHelper.FormatNumberAbsolute(value));
            DialogEx.DialogExViewScript.Instance.ShowFullPopup(strConfirm, () =>
            {
                DialogEx.DialogExViewScript.Instance.ShowLoading(true);
                if (EventClickItem != null) EventClickItem(ID, Prices[dropdown.value]);
            }, null);
        }
    }
}
