using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;

namespace View.Home.History
{
    public class ItemHistoryTranferView : EnhancedScrollerCellView
    {
        [SerializeField] private Image imgBackground;
        [SerializeField] private Text txtCreateTime;
        [SerializeField] private Text txtUserTranfer;
        [SerializeField] private Text txtUserReceived;
        [SerializeField] private Text txtMoneyTranfer;
        [SerializeField] private Text txtContentTranfer;

        public void SetData(int cellindex,string time, string usertranfer, string userreceived, long money, string content)
        {
            Debug.Log("SetData: " + cellIndex + " - " + (imgBackground == null));
            if (imgBackground) imgBackground.color = cellIndex % 2 == 0 ? Color.white : new Color(1, 1, 1, 0.001f);
            if (txtCreateTime) txtCreateTime.text = time;
            if (txtUserTranfer) txtUserTranfer.text = usertranfer;
            if (txtUserReceived) txtUserReceived.text = userreceived;
            if (txtMoneyTranfer) txtMoneyTranfer.text = MoneyHelper.FormatNumberAbsolute(money);
            if (txtContentTranfer) txtContentTranfer.text = content;
        }
    }
}