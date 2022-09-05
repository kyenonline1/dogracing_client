using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;

namespace View.Home.ExChange
{
    public class ItemHistoryView : EnhancedScrollerCellView
    {
        public delegate void ClickCharingCard(int transaction);
        public ClickCharingCard dlgCharing;
        public delegate void ClickCardDetail(int transaction,string id, string name, string seri, string code, int amount);
        public ClickCardDetail dlgCardDetail;
        private string ID, Seri, Code, CardName;
        private int _transid, status, amount;
        public Text txtStt;
        public Text txtVatPham;
        public Text txtTime;
        public Text txtStatus;
        public UIMyButton btnNapthe,
            btnDetail;

        // Use this for initialization
        private void Awake()
        {
            btnNapthe._onClick.AddListener(ClickNapThe);
            btnDetail._onClick.AddListener(ClickedCardDetail);
        }

        private void ClickNapThe()
        {
            if (status == 1)
            {
                if (dlgCharing != null)
                {
                    dlgCharing(_transid);
                    btnNapthe.gameObject.SetActive(false);
                    btnDetail.gameObject.SetActive(false);
                }
            }else if(status == 3)
            {
                if (dlgCardDetail != null)
                {
                    dlgCardDetail(_transid, ID, CardName, Seri, Code, amount);
                    btnNapthe.gameObject.SetActive(false);
                }
            }
        }

        private void ClickedCardDetail()
        {
            if (dlgCardDetail != null)
            {
                dlgCardDetail(_transid, ID, CardName, Seri, Code, amount);
                btnNapthe.gameObject.SetActive(false);
            }
        }

        public void SetData(int stt, string name, string time, byte status, string seri, string code, string id, int transid, int amount)
        {
            
            Seri = seri;
            Code = code;
            _transid = transid;
            this.amount = amount;
            ID = id;
            //CardName = "Thẻ " + name + " " + MoneyHelper.FormatRelativelyWithoutUnit(amount);
            Debug.Log("CardName: " + CardName + " - name: " + name + " amount : " + amount + " status: " + status + " transid: " + transid);
            this.status = status;
            txtStt.text = string.Format("{0}", stt);
           // Debug.Log("SET DATA ITEM LSDT: " + seri + " - " + code);
            //if(string.IsNullOrEmpty(seri) && string.IsNullOrEmpty(code))
            txtVatPham.text = name;
            //else txtVatPham.text = string.Format("{0}\nSeri:{1}\n{2}:{3}", name + " <color=yellow>" + MoneyHelper.FormatRelativelyWithoutUnit(_amount) + "</color>", seri, "MT", code);
            txtTime.text = time;
            //0: Chưa duyệt, : 1 đã duyệt, 2 : Đã hủy, 3 : Đã nhận thẻ, 4 : Đã Nạp lại
            switch (status)
            {
                case 0: txtStatus.text = "Chưa duyệt";
                    break;
                case 1:
                    txtStatus.text = "Đã duyệt";
                    break;
                case 2:
                    txtStatus.text = "Đã hủy";
                    break;
                case 3:
                    txtStatus.text = "Đã nhận thẻ";
                    break;
                case 4:
                    txtStatus.text = "Đã Nạp lại";
                    break;
                default:
                    txtStatus.text = "Đã tiếp nhận";
                    break;
            }

            btnNapthe.gameObject.SetActive(status == 1 || status == 3);
            btnDetail.gameObject.SetActive(status == 1 || status == 3);
        }
    }
}
