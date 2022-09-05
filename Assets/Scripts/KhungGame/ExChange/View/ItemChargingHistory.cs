using EnhancedUI.EnhancedScroller;
using UnityEngine.UI;
using Utilites;

namespace View.Common
{
    public class ItemChargingHistory : EnhancedScrollerCellView
    {

        public Text txtTime,
            txtService,
            txtInfo,
            txtPrice,
            txtAmount,
            txtStatus;

        public void InitData(int index, string time, string service, string info, long amount, float price, string status)
        {
            try
            {
                txtTime.text = time;
                txtService.text = service;
                txtPrice.text = UnityEngine.Mathf.Round(price) + " USD";
                txtAmount.text = MoneyHelper.FormatRelativelyWithoutUnit(amount);
                txtStatus.text = status;
                txtInfo.text = info;
            }
            catch
            {

            }
        }
    }
}
