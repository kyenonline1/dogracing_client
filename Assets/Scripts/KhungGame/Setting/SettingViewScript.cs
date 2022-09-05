using AppConfig;
using Base.Utils;
using Controller.Setting;
using CoreBase;
using Interface;
using UnityEngine;
using UnityEngine.UI;
using View.Home.Lobby;

namespace View.Setting
{
    public class SettingViewScript : ViewScript
    {
        
        public UIMyButton btnExit;

        public Image imgSoundEffect;
        public Sprite sprSoundOn, sprSoundOff;


        public Image imgLanguage;
        public Sprite sprLangVn, sprLangEn;

        //public GameObject objSoundOn, objSoundOff,
        //    objMusicOn, objMusicOff;

        //public Slider sldSound;
        //public Slider sldMusic;

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
            AddListenerOnToggle();
        }

        private void OnEnable()
        {
            ChangeSoundImg();
            ChangeLangImg();
        }

        protected override IController CreateController()
        {
            return new SettingController(this);
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
        }

        public override void OpenPopup()
        {
            base.OpenPopup();
        }

        public override void ClosePopup()
        {
            base.ClosePopup();
        }



        private void AddListenerOnToggle()
        {
            btnExit._onClick.AddListener(BtnClickClose);
            //sldSound.onValueChanged.AddListener(EventClickSliderSound);
            //sldMusic.onValueChanged.AddListener(EventClickSliderMusic);
        }
        

        public void SoundClicked()
        {
            ClientConfig.Sound.ENABLE = !ClientConfig.Sound.ENABLE;
            //objSoundOn.SetActive(ClientConfig.Sound.ENABLE);
            //objSoundOff.SetActive(!ClientConfig.Sound.ENABLE);
        }

        public void MusicClicked()
        {
            ClientConfig.Sound.ENABLE_BGSOUND = !ClientConfig.Sound.ENABLE_BGSOUND;
            ClientConfig.Sound.ENABLE = ClientConfig.Sound.ENABLE_BGSOUND;
            //objMusicOn.SetActive(ClientConfig.Sound.ENABLE_BGSOUND);
            //objMusicOff.SetActive(!ClientConfig.Sound.ENABLE_BGSOUND);
            EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_MUSIC);
            ChangeSoundImg();
        }

        private void ChangeSoundImg()
        {
            if (imgSoundEffect) imgSoundEffect.sprite = ClientConfig.Sound.ENABLE_BGSOUND ? sprSoundOn : sprSoundOff;
        }


        private void EventClickSliderSound(float value)
        {
            ClientConfig.Setting.VOLUM_SOUND = value;
            if (value <= 0)
            {
                ClientConfig.Sound.ENABLE = false;
            }
            else
            {
                ClientConfig.Sound.ENABLE = true;
            }
        }

        private void EventClickSliderMusic(float value)
        {
            ClientConfig.Setting.VOLUM_MUSIC = value;
            if (value <= 0)
            {
                ClientConfig.Sound.ENABLE_BGSOUND = false;
            }
            else
            {
                ClientConfig.Sound.ENABLE_BGSOUND = true;
            }
            EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_MUSIC);
        }
        

        private void ChangeLangImg()
        {
            if (imgLanguage)
            {
                imgLanguage.sprite = Languages.Language.LANG == Languages.Languages.en ? sprLangEn : sprLangVn;
            }
        }


        public void BtnChangeLanguageClicked()
        {
            if(Languages.Language.LANG == Languages.Languages.en)
            {
                Languages.Language.LANG = Languages.Languages.vn;
            }else Languages.Language.LANG = Languages.Languages.en;

            Game.Gameconfig.ClientGameConfig.Language = (int)Languages.Language.LANG;
            Languages.Language.ChangeLanguage(Languages.Language.LANG);
            ChangeLangImg();
        }

        public void BtnChangePasswordClicked()
        {
            ClosePopup();
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowChangePassword();
        }


        public void BtnRedeemCodeClicked()
        {
            ClosePopup();
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowGiftCode();
        }

        public void ClickLogOut()
        {
            if (LobbyViewScript.Instance) LobbyViewScript.Instance.BtnClickExit();
            BtnClickClose();
        }

        private void BtnClickClose()
        {
            ClosePopup();
        }

        public void OpenSetting()
        {
            OpenPopup();
        }


    }
}
