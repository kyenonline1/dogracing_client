using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

public enum CateShop
{
    IAP,
    EXCHANGE,
    HISTORY,
    AGENCY,
    TRANFER,
    CHARGER,
}

namespace View.Home.Shop
{
  
    public class ShopManagerView : UIPopupUtilities
    {
        [SerializeField] private int cateDefault;
        [SerializeField] private UIToggleTabsCate uiCates;

        private void Awake()
        {
            
        }

        public override void ClosePopup()
        {
            base.ClosePopup();
        }


        public void OpenByCate(CateShop cateIndex, Action action = null)
        {
            callbackClosePopup = action;
            OpenPopup();
            if (uiCates) uiCates.SelectCate((int)cateIndex);
        }
    }
}
