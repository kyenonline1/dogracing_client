using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;

public class CardRateView : MonoBehaviour
{
    public Text txtPrices,
        txtValue;

    public void ShowData(long price, float rate)
    {
        if (txtPrices) txtPrices.text = MoneyHelper.FormatNumberAbsolute(price) + " $";
        long _price = -1;
        long.TryParse(Mathf.Round(price * rate).ToString(), out _price);
        if (txtValue) txtValue.text = MoneyHelper.FormatNumberAbsolute(_price);
    }
}
