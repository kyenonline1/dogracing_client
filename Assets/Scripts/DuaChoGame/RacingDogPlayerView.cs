//using AssetBundles;
using AppConfig;
using Base.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;

namespace View.GamePlay.DuaCho
{
    public class RacingDogPlayerView : MonoBehaviour
    {

        private Transform tranMask,
            tranInfo;
        private Image imgAvatar;
        private Text txtUserName,
            txtMoney;
        private string UserName;
        public Vector3 basePosition;
        private Image imgVip;
        private GameObject objChat;
        private Text txtChat;
        [SerializeField]
        private bool isMe;

        private Coroutine coroutineChat = null;


        // Use this for initialization
        private void Awake()
        {
            Init();
            if (isMe)
            {
                EventManager.Instance.SubscribeTopic(EventManager.CHANGE_BALANCE, UpdateMyMoney);
                EventManager.Instance.SubscribeTopic(EventManager.CHANGE_AVATAR_TOPIC, UpdateAvatar);
                EventManager.Instance.SubscribeTopic(EventManager.CHANGE_NICKNAME_TOPIC, UpdateNickName);
            }
        }

        private void Start()
        {
            basePosition = txtMoney.transform.localPosition;
        }

        private void Init()
        {
            tranMask = transform.Find("BgAvatar/Mask");
            tranInfo = transform.Find("Info");
            imgAvatar = tranMask.Find("Avatar").GetComponent<Image>();
            txtUserName = tranInfo.Find("bgInfo/txtUserName").GetComponent<Text>();
            imgVip = transform.Find("BgAvatar/Vip").GetComponent<Image>();
            txtMoney = tranInfo.Find("bgInfo/txtMoney").GetComponent<Text>();
            objChat = transform.Find("Chat").gameObject;
            txtChat = transform.Find("Chat/Text").GetComponent<Text>();
        }
        

        public void FillBasicInfo(string username, long money, string avatar, int vip = 0)
        {
            UserName = username;
            imgAvatar.gameObject.SetActive(true);
            tranInfo.gameObject.SetActive(true);
            txtUserName.text = username;
            txtMoney.text = MoneyHelper.FormatNumberAbsolute(money);
            if (vip > 0)
            {
                imgVip.gameObject.SetActive(true);
                UpdateVip(1);
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
                        if (RacingDogView.Instance)
                        {
                            imgAvatar.sprite = RacingDogView.Instance.GetAvatar(avtId);
                        }
                    }
                }
            }
        }

        private void UpdateMyMoney()
        {
            txtMoney.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.GOLD);
        }

        private void UpdateAvatar()
        {
            if (!isMe) return;
            if (imgAvatar != null)
            {
                string avatar = ClientConfig.UserInfo.AVATAR;
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
                        if (RacingDogView.Instance)
                        {
                            imgAvatar.sprite = RacingDogView.Instance.GetAvatar(avtId);
                        }
                    }
                }
            }
        }

        private void UpdateNickName()
        {
            if (!isMe) return;
            txtUserName.text = ClientConfig.UserInfo.NICKNAME;
        }

        public void UpdateMoney(long money)
        {
            txtMoney.text = MoneyHelper.FormatNumberAbsolute(money);
        }

        public void HidePlayer()
        {
            imgAvatar.sprite = null;
            tranInfo.gameObject.SetActive(false);
            imgAvatar.gameObject.SetActive(false);
            imgVip.gameObject.SetActive(false);
            UserName = string.Empty;
            txtUserName.text = txtMoney.text = string.Empty;
        }

        public string USERNAME
        {
            get
            {
                return UserName;
            }
        }

        public Transform GetTranParent()
        {
            return txtMoney.transform;
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

        private void UpdateVip(int vip)
        {
            //LoadAssetBundle.LoadSpriteToAtlas(TagAssetBundle.Tag_UI.TAG_UI_RACINGDOG, TagAssetBundle.AtlasName.DUACHO_ATLAS,string.Format("vip{0}", vip), (spr) => { if (spr) imgVip.sprite = spr; });
        }

        private IEnumerator IeChat()
        {
            objChat.SetActive(true);
            yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(3f));
            objChat.SetActive(false);
        }

        private void OnDestroy()
        {
            if (isMe)
            {
                EventManager.Instance.UnSubscribeTopic(EventManager.CHANGE_BALANCE, UpdateMyMoney);
                EventManager.Instance.UnSubscribeTopic(EventManager.CHANGE_AVATAR_TOPIC, UpdateAvatar);
                EventManager.Instance.UnSubscribeTopic(EventManager.CHANGE_NICKNAME_TOPIC, UpdateNickName);
            }
        }
    }
}
