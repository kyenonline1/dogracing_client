using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;

namespace View.GamePlay.Slot
{
    public class ItemRichestMan : MonoBehaviour
    {
        private Text txtTimer,
            txtAccount,
            //txtBlind,
            txtWin;
        //txtDetail;

        private void Awake()
        {
            InitData();
        }

        private void InitData()
        {
            txtTimer = transform.Find("txtTimer").GetComponent<Text>();
            txtAccount = transform.Find("txtAccount").GetComponent<Text>();
            //txtBlind = transform.Find("txtBlind").GetComponent<Text>();
            txtWin = transform.Find("txtWin").GetComponent<Text>();
            //txtDetail = transform.Find("txtDetail").GetComponent<Text>();
        }

        public void InitData(string account, long blind, long winmoney, int stt)
        {
            try
            {
                if (txtWin == null) InitData();
                txtTimer.text = string.Format("{0}", stt);
                txtAccount.text = account;
                //txtBlind.text = MoneyHelper.FormatNumberAbsolute(blind);
                txtWin.text = MoneyHelper.FormatNumberAbsolute(winmoney);
                //txtDetail.text = detail;
            }
            catch
            {

            }
        }
    }
}
