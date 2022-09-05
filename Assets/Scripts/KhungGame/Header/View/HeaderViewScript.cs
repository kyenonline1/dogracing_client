using AppConfig;
using AssetBundles;
using Base.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;

public class HeaderViewScript : MonoBehaviour {

    public Text txtNickName;
    public Text txtVipPoint;
    public Text txtChip;
    public Text txtGold;
    public Text txtFlash;
    public Image imgAvatar;

	// Use this for initialization
	void Start () {

        UpdateFullInfo();
        EventManager.Instance.SubscribeTopic(EventManager.CHANGE_BALANCE, UpdateMoney);
        EventManager.Instance.SubscribeTopic(EventManager.CHANGE_AVATAR_TOPIC, UpdateAvatar);
        EventManager.Instance.SubscribeTopic(EventManager.CHANGE_NICKNAME_TOPIC, UpdateNickName);
    }

    private void UpdateFullInfo()
    {
        UpdateNickName();
        UpdateAvatar();
        UpdateVipPoint();
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        UpdateGold();
        UpdateChip();
        UpdateFlash();
    }

    private void UpdateNickName()
    {
        if(!string.IsNullOrEmpty(ClientConfig.UserInfo.NICKNAME))
        txtNickName.text = ClientConfig.UserInfo.NICKNAME;
    }
    
    private void UpdateAvatar()
    {
        try
        {
            //Debug.Log("UpdateAvatar: " + ClientConfig.UserInfo.AVATAR + " - " + ClientConfig.UserInfo.LOGIN_TYPE);
            if (ClientConfig.UserInfo.LOGIN_TYPE == ClientConfig.UserInfo.LoginType.FACEBOOK)
                StartCoroutine(ImageLoader.HTTPLoadImage(imgAvatar, ClientConfig.UserInfo.AVATAR));
            else
            {
                string str = (int.Parse(ClientConfig.UserInfo.AVATAR) < 10) ? string.Format("avatar0{0}", ClientConfig.UserInfo.AVATAR) : string.Format("avatar{0}", ClientConfig.UserInfo.AVATAR);
                //Debug.Log("Avtart: -0-------------------------   " + str);
                //LoadAssetBundle.LoadSpriteToAtlas(TagAssetBundle.Tag_UI.TAG_UI_COMMON, TagAssetBundle.AtlasName.LOBBY_AVATAR, str, (sprite) => {
                //    //Debug.Log("Load Avatar: " + (sprite == null) + " , image: " + (imgAvatar == null));
                //    if (sprite != null && imgAvatar != null) imgAvatar.sprite = sprite;
                //});

                //LoadAssetBundle.LoadSprite(imgAvatar, TagAssetBundle.Tag_UI.TAG_UI_KHUNGGAME, str);
            }
            //UpdateVip();
        }
        catch
        {

        }
    }

    private void UpdateVipPoint()
    {
        txtVipPoint.text = "ID: " + ClientConfig.UserInfo.ID;
    }

    private void UpdateChip()
    {
        //Debug.Log("UPDATE MONEY");
        txtChip.text = MoneyHelper.FormatRelativelyWithoutUnit(ClientConfig.UserInfo.SILVER);
    }
    
    private void UpdateGold()
    {
        txtGold.text = MoneyHelper.FormatRelativelyWithoutUnit(ClientConfig.UserInfo.GOLD);
    }

    private void UpdateFlash()
    {
        txtFlash.text = MoneyHelper.FormatRelativelyWithoutUnit(ClientConfig.UserInfo.GOLD_SAFE);
    }

    public void OnBtnTopClicked()
    {
        if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowTop();
    }

    //private void UpdateVip()
    //{
    //    //Debug.Log("LoadVip");
    //    //LoadAssetBundle.LoadSprite(bgVip, TagAssetBundle.Tag_UI.TAG_UI_KHUNGGAME, string.Format("vip{0}", ClientConfig.UserInfo.VIPTYPE));
    //    LoadAssetBundle.LoadSpriteToAtlas(TagAssetBundle.Tag_UI.TAG_UI_KHUNGGAME, TagAssetBundle.AtlasName.LOBBY_ATLAS, string.Format("vip{0}", ClientConfig.UserInfo.VIPTYPE), (sprite) => { if (sprite) bgVip.sprite = sprite; });
    //}

    private void OnDestroy()
    {
        EventManager.Instance.UnSubscribeTopic(EventManager.CHANGE_BALANCE, UpdateMoney);
        EventManager.Instance.UnSubscribeTopic(EventManager.CHANGE_AVATAR_TOPIC, UpdateAvatar);
        EventManager.Instance.UnSubscribeTopic(EventManager.CHANGE_NICKNAME_TOPIC, UpdateNickName);
    }
}
