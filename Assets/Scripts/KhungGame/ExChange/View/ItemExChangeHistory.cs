using EnhancedUI.EnhancedScroller;
using UnityEngine.UI;
using Utilites;

namespace View.Common
{
    public class ItemExChangeHistory : EnhancedScrollerCellView
    {

        public Text txtTime,
            txtEmail,
            txtAccountHolder,
            txtAmount,
            txtStatus;

        public void InitData(int index, string time, string email, string accountholder, long amount, byte status)
        {
            try
            {
                txtTime.text = time;
                txtEmail.text = email;
                txtAmount.text = MoneyHelper.FormatRelativelyWithoutUnit(amount);
                ////0: Chưa duyệt, : 1 đã duyệt, 2 : Đã hủy, 3 : Đã nhận thẻ, 4 : Đã Nạp lại
                switch (status) {
                    case 0:
                        txtStatus.text = "Chưa duyệt";
                        break;
                    case 1:
                        txtStatus.text = "Thành công";
                        break;
                    case 2:
                        txtStatus.text = "Thất bại";
                        break;
                }
                txtAccountHolder.text = accountholder;
            }
            catch
            {

            }
        }
    }
}
