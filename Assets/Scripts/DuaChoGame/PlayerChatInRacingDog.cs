//using AssetBundles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.GamePlay.DuaCho
{
    public class PlayerChatInRacingDog : MonoBehaviour
    {

        private Transform tranMask;
        private Image imgAvatar, imgVip;
        private string UserName;
        public Vector3 basePosition;
        
        private GameObject objChat;
        private Text txtChat;

        private Coroutine coroutineChat = null;

        public string USERNAME
        {
            get
            {
                return UserName;
            }
        }
        // Use this for initialization
        private void Awake()
        {
            tranMask = transform.Find("BgAvatar/Mask");
            imgAvatar = tranMask.Find("Avatar").GetComponent<Image>();
            //imgVip = transform.Find("BgAvatar/Vip").GetComponent<Image>();
            objChat = transform.Find("Chat").gameObject;
            txtChat = objChat.GetComponent<Text>();
        }

        public void FillBasicInfo(string username, string avatar, int vip)
        {
            UserName = username;
            imgAvatar.gameObject.SetActive(true);
            if (vip > 0)
            {
                //imgVip.gameObject.SetActive(true);
                ////Debug.Log("Fill Vip: " + vip);
                //LoadAssetBundle.LoadSpriteToAtlas(TagAssetBundle.Tag_UI.TAG_UI_RACINGDOG, TagAssetBundle.AtlasName.DUACHO_ATLAS, string.Format("vip{0}", vip), (spr) => { if (spr) imgVip.sprite = spr; });
            }
                //TODO: Update Avatar
            if (imgAvatar != null)
            {
                if (avatar.Contains("facebook"))
                {
                    StartCoroutine(ImageLoader.HTTPLoadImage(imgAvatar, avatar));
                }
                else
                {
                    int avtId = -1;
                    if (int.TryParse(avatar, out avtId))
                    {
                        string strId = (avtId < 10) ? string.Format("avatar0{0}", avtId) : string.Format("avatar{0}", avtId);
                        //Debug.Log("Load image error 3333333333" + ", image url: " + strId);
                        //LoadAssetBundle.LoadSpriteToAtlas(TagAssetBundle.Tag_UI.TAG_UI_KHUNGGAME, TagAssetBundle.AtlasName.LOBBY_AVATAR, strId, (sprite) => {
                        //    //Debug.Log("Load Avatar: " + (sprite == null) + " , image: " + (imgAvatar == null));
                        //    if (sprite) imgAvatar.sprite = sprite;
                        //});

                        if(RacingDogView.Instance)
                        {
                            imgAvatar.sprite = RacingDogView.Instance.GetAvatar(avtId);
                        }
                    }
                }
            }
        }

        public void HidePlayer()
        {
            imgAvatar.sprite = null;
            imgAvatar.gameObject.SetActive(false);
            //imgVip.gameObject.SetActive(false);
            UserName = string.Empty;
        }

        public void PlayerChat(string txt)
        {
            try
            {
                txtChat.text = txt;
                if (coroutineChat != null) StopCoroutine(coroutineChat);
                coroutineChat = StartCoroutine(IeChat());
            }
            catch
            {

            }
        }

        private IEnumerator IeChat()
        {
            objChat.SetActive(true);
            yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(3f));
            objChat.SetActive(false);
        }

    }
}
