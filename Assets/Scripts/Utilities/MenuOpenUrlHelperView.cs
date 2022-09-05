using Game.Gameconfig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpenUrlHelperView : MonoBehaviour
{
    
    public void OnBtnOpenMessengerClicked()
    {
        if (!string.IsNullOrEmpty(ClientGameConfig.APPFUNCTION.UrlMessenger))
        {
            Application.OpenURL(ClientGameConfig.APPFUNCTION.UrlMessenger);
        }
    }


    public void OnBtnFanpageClicked()
    {
        if (!string.IsNullOrEmpty(ClientGameConfig.APPFUNCTION.UrlFanpage))
        {
            Application.OpenURL(ClientGameConfig.APPFUNCTION.UrlFanpage);
        }
    }

    public void OnBtnTelegramClicked()
    {
        if (!string.IsNullOrEmpty(ClientGameConfig.APPFUNCTION.UrlTelegram))
        {
            Application.OpenURL(ClientGameConfig.APPFUNCTION.UrlTelegram);
        }
    }

    public void OnBtnTelegramOTPClicked()
    {
        if (!string.IsNullOrEmpty(ClientGameConfig.APPFUNCTION.UrlTelegramBOT))
        {
            Application.OpenURL(ClientGameConfig.APPFUNCTION.UrlTelegramBOT);
        }
    }


    public void OnBtnHotlineClicked()
    {
        if (!string.IsNullOrEmpty(ClientGameConfig.APPFUNCTION.Hotline))
        {
            string hotline = ClientGameConfig.APPFUNCTION.Hotline.Replace('.', ' ').Trim();
            string url = string.Format("tel://{0}", hotline);
            Application.OpenURL(url);
        }
    }


}
