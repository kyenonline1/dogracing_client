
using AppConfig;
using AssetBundles;
using System;
using UnityEngine;
using UnityEngine.UI;
using View.Home.Lobby;

namespace View.Home.Vip
{
    public class ItemVipView : MonoBehaviour
    {

        private Text txtVip;
        private Text txtVipPoint;
        private Slider sldVipPoint;
        private Image avatar;
        private Image bgVip;
        [HideInInspector]
        public UIMyButton btnIAP;

        // Use this for initialization
        private void Awake()
        {
            InitProperties();
        }

        private void InitProperties()
        {
            txtVip = transform.Find("txtVip").GetComponent<Text>();
            txtVipPoint = transform.Find("txtVP").GetComponent<Text>();
            sldVipPoint = transform.Find("ProgressBar").GetComponent<Slider>();
            avatar = transform.Find("Avatar").GetComponent<Image>();
            bgVip = transform.Find("bgVip").GetComponent<Image>();
            btnIAP = transform.Find("BtnIAP").GetComponent<UIMyButton>();
        }

        public void SetData(string vip, int maxVp, int type, Action callback)
        {
            if (btnIAP == null) InitProperties();
            //txtVip.text = string.Format("{0}: {1}", Languages.Language.GetKey("HOME_PROFILE_VIP"), vip);
            txtVip.text = vip;
            txtVipPoint.text = string.Format("{0}/<color=#FBFF00FF>{1}</color>", ClientConfig.UserInfo.CURVIP, maxVp);
            sldVipPoint.value = (float)ClientConfig.UserInfo.CURVIP / (float)maxVp;
            //LoadAssetBundle.LoadSpriteToAtlas(TagAssetBundle.Tag_UI.TAG_UI_KHUNGGAME, TagAssetBundle.AtlasName.LOBBY_ATLAS, string.Format("vip{0}", type), (sprite) => { if (sprite) bgVip.sprite = sprite; });
            this.avatar.sprite = LobbyViewScript.Instance.btnAvatar.GetComponent<Image>().sprite;
            btnIAP._onClick.RemoveAllListeners();
            btnIAP._onClick.AddListener(()=> { callback(); });
        }
    }
}
