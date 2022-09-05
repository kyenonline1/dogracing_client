using AssetBundles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;

namespace View.Home.Top
{
    public class ItemTopPlayerView : MonoBehaviour
    {
        [SerializeField] private Image imgTop;
        [SerializeField] private Sprite[] sprTops;
        [SerializeField] private Text txtTop;
        [SerializeField] private Image imgAvatar;
        [SerializeField] private Text txtPlayer;
        [SerializeField] private Text txtMoney;

        private int itemType; //0: player, 1 Club


        public void SetData(int index, string player, string avatar, long money, int itemType)
        {

            try
            {
                this.itemType = itemType;
                if (imgTop)
                {
                    imgTop.gameObject.SetActive(index < 3);
                    if (txtTop) txtTop.gameObject.SetActive(index >= 3);
                    if (index < 3)
                    {
                        imgTop.sprite = sprTops[index];
                        imgTop.SetNativeSize();
                    }
                    else
                    {
                        if (txtTop) txtTop.text = string.Format("{0}", index + 1);
                    }
                }

                if (!string.IsNullOrEmpty(avatar)) LoadAvatar(avatar);
                if (txtPlayer) txtPlayer.text = player;
                if (txtMoney) txtMoney.text = MoneyHelper.FormatNumberAbsolute(money);

            }
            catch(Exception ex)
            {
                Debug.LogError("ExceptioN: " + ex.Message);
                Debug.LogError("ExceptioN: " + ex.StackTrace);
            }
        }

        private void LoadAvatar(string avatar)
        {
            if (imgAvatar)
            {
                if (avatar.Contains("http"))
                {
                    StartCoroutine(ImageLoader.HTTPLoadImage(imgAvatar, avatar));
                }
                else
                {
                    if (itemType == 0)
                    {
                        string str = (int.Parse(avatar) < 10) ? string.Format("avatar0{0}", avatar) : string.Format("avatar{0}", avatar);
                        //LoadAssetBundle.LoadSpriteToAtlas(TagAssetBundle.Tag_UI.TAG_UI_COMMON, TagAssetBundle.AtlasName.LOBBY_AVATAR, str, (sprite) =>
                        //{
                        //    if (sprite != null && imgAvatar != null) imgAvatar.sprite = sprite;
                        //});
                    }
                    else
                    {
                        string str = (int.Parse(avatar) < 10) ? string.Format("club_ava_{0}", avatar) : string.Format("club_ava_{0}", avatar);
                        //LoadAssetBundle.LoadSpriteToAtlas(TagAssetBundle.Tag_UI.TAG_UI_COMMON, TagAssetBundle.AtlasName.LOBBY_AVATAR, str, (sprite) =>
                        //{
                        //    if (sprite != null && imgAvatar != null) imgAvatar.sprite = sprite;
                        //});
                    }
                }
            }
        }
    }
}
