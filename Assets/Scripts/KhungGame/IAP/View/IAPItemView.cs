using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;

namespace View.Home.IAP
{
    public class IAPItemView : MonoBehaviour
    {
        [SerializeField] private Text txtPrice;
        [SerializeField] private Text txtValue;

        public void InitDataIAP(long price, long money)
        {
            if (txtPrice) txtPrice.text = MoneyHelper.FormatNumberAbsolute(price) + " $";
            if (txtPrice) txtValue.text = MoneyHelper.FormatNumberAbsolute(money);
        }


    }

}