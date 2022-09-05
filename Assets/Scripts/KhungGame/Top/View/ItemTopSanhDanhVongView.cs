using AssetBundles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.Home.Top
{
    public class ItemTopSanhDanhVongView : MonoBehaviour
    {
        [SerializeField] private Image imgAvatar;
        [SerializeField] private Text txtUserName;

        public void SetData(string username, string avatarUrl)
        {
            if (txtUserName) txtUserName.text = username;
            if (imgAvatar)
            {
                if (string.IsNullOrEmpty(avatarUrl)) LoadAvatar(avatarUrl);
            }
        }

        private void LoadAvatar(string avatarurl)
        {
            if (avatarurl.Contains("http"))
                StartCoroutine(ImageLoader.HTTPLoadImage(imgAvatar, avatarurl));
            else
            {
                string str = (int.Parse(avatarurl) < 10) ? string.Format("avatar0{0}", avatarurl) : string.Format("avatar{0}", avatarurl);
                //LoadAssetBundle.LoadSpriteToAtlas(TagAssetBundle.Tag_UI.TAG_UI_COMMON, TagAssetBundle.AtlasName.LOBBY_AVATAR, str, (sprite) => {
                //    if (sprite != null && imgAvatar != null) imgAvatar.sprite = sprite;
                //});

            }
        }
    }
}
