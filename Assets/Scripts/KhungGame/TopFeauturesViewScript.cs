using AppConfig;
using Game.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;
using View.Home.GiftCode;
using View.Home.History;
using View.Home.Inbox;
using View.Home.Profile;
using View.Home.Shop;
using View.Home.Top;
using View.Home.UpdateInfo;
using View.Setting;

public class TopFeauturesViewScript : MonoBehaviour
{
    public static TopFeauturesViewScript instance;

    [SerializeField] private ProfileNewViewScript profile;

    [SerializeField] private ShopManagerView shop;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        instance = this;
    }

    private void Start()
    {
        if (ClientConfig.UserInfo.IS_EVENTX2)
        {
            //ShowEventX2Nap();
        }
    }

    public void OpenSecurity(Sprite avatar)
    {
        if (profile != null)
        {
            //profile.gameObject.SetActive(true);
            //profile.cateType = ProfileViewScript.CateType.SECURITY;
            //profile.OpenContentByCate();
            //profile.OpenProfile(ClientConfig.UserInfo.ID
            //   , ClientConfig.UserInfo.NICKNAME, avatar);
        }
    }

    public void OpenSafe(Sprite avatar)
    {
        if (profile != null)
        {
            //profile.gameObject.SetActive(true);
            //profile.cateType = ProfileViewScript.CateType.SAFE;
            //profile.OpenContentByCate();
            //profile.OpenProfile(ClientConfig.UserInfo.ID
            //   , ClientConfig.UserInfo.NICKNAME, avatar);
        }
    }

    public void ShowProfileInfo(Action action = null)
    {
        if (profile != null)
        {
            profile.OpenProfile(action);
            //profile.gameObject.SetActive(true);
            //profile.cateType = ProfileViewScript.CateType.PROFILE;
            //profile.OpenContentByCate();
            //profile.OpenProfile(ClientConfig.UserInfo.ID
            //   , ClientConfig.UserInfo.NICKNAME, avatar);
        }
    }

    public void ShowNapThe()
    {
        
    }

    public void ShowNapMomo()
    {
        
    }


    public void ShowInbox()
    {
       
    }

    public void ShowSetting()
    {
       
    }

   

    public void ShowMission(Action action = null)
    {
       
    }

    public void ShowEventBanner()
    {
    }

    public void ShowDaiLy()
    {
        
    }

    public void ShowExchange()
    {
        
    }

    public void ShowGiftCode()
    {
       
    }

    public void ShowEventX2Nap()
    {
       
    }

    public void ShowChangePassword()
    {
       
    }

    public void ShowSecutiry()
    {
       
    }

    public void ShowShop(CateShop cate, Action action = null)
    {
        if (shop)
        {
            shop.OpenByCate(cate, action);
        }
    }

    public void ShowReviewHand()
    {
       
    }

    public void ShowTop()
    {
      
    }

    public void ShowTopDanhVong(string[] session)
    {
      
    }

    public void ShowHistoryDetail(int tableId, int gamesseion, string gameId)
    {
      
    }

    public void CloseHistoryDetail()
    {
        
    }
}
