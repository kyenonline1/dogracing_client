using AppConfig;
using Base;
using Controller.Common.Chat;
using CoreBase;
using CoreBase.Controller;
using Interface;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.Common.Chat
{
    public class ChatViewScript : ViewScript
    {

        public List<Image> imgChatEmotions;
        public List<Transform> itemQuickChats;
        public Transform gridQuickChat,
            tranChatText,
            tranEmotions;
        public Text txtChat;
        public InputField inputChat;
        public UIMyButton btnQuickChat,
            btnEmotions,
            btnTextChat,
            btnSend;
        public Button btnHideChat;

        //private Dictionary<string, Transform> listQuickChat;

        private Animator animator;

        private List<string> listTextChat = new List<string>();


        public delegate void ShowText(long userid, string nickname, string message);
        public ShowText dlgShowTextChat;

        public delegate void ShowEmotion(long userid, string nickname, Sprite emotion);
        public ShowEmotion dlgShowEmotion;

        // Use this for initialization

        protected override IController CreateController()
        {
            return new ChatController(this);
        }

        protected override void InitGameScriptInAwake()
        {
            base.InitGameScriptInAwake();
            RunAwakeEvent();
            AddListeners();
        }

        protected override void InitGameScriptInStart()
        {
            base.InitGameScriptInStart();
        }

        protected override void DestroyGameScript()
        {
            base.DestroyGameScript();
        }

        private void RunAwakeEvent()
        {
            animator = GetComponent<Animator>();
            tranChatText = transform.Find("Popup/BackGround/TextChat");
            tranEmotions = transform.Find("Popup/BackGround/Emoticons");
            gridQuickChat = transform.Find("Popup/BackGround/GridTextQuickChat");
            //listQuickChat = new Dictionary<string, Transform>();

            inputChat = transform.Find("Popup/BackGround/Buttons/InputField").GetComponent<InputField>();
            btnQuickChat = transform.Find("Popup/BackGround/Buttons/btnQuickChat").GetComponent<UIMyButton>();
            btnEmotions = transform.Find("Popup/BackGround/Buttons/btnChatEmotion").GetComponent<UIMyButton>();
            btnTextChat = transform.Find("Popup/BackGround/Buttons/btnChatText").GetComponent<UIMyButton>();
            btnSend = transform.Find("Popup/BackGround/Buttons/btnSendChat").GetComponent<UIMyButton>();
            btnHideChat = transform.Find("BgHide").GetComponent<Button>();
            txtChat = tranChatText.Find("ScrollView/Content").GetComponent<Text>();
        }
        

        private void AddListeners()
        {
            btnQuickChat._onClick.AddListener(BtnShowQuickChatClick);
            btnEmotions._onClick.AddListener(BtnShowEmotionChatClick);
            btnTextChat._onClick.AddListener(BtnShowTextChatClick);
            btnSend._onClick.AddListener(BtnSendChatClick);
            btnHideChat.onClick.AddListener(BtnCloseChatClick);
        }

        private void BtnShowQuickChatClick()
        {
            tranChatText.gameObject.SetActive(false);
            tranEmotions.gameObject.SetActive(false);
            gridQuickChat.gameObject.SetActive(true);
        }

        private void BtnShowEmotionChatClick()
        {
            tranChatText.gameObject.SetActive(false);
            tranEmotions.gameObject.SetActive(true);
            gridQuickChat.gameObject.SetActive(false);
        }

        private void BtnShowTextChatClick()
        {
            tranChatText.gameObject.SetActive(true);
            tranEmotions.gameObject.SetActive(false);
            gridQuickChat.gameObject.SetActive(false);
        }

        private void BtnSendChatClick()
        {
            string msg = inputChat.text;
            if (string.IsNullOrEmpty(msg)) return;
            SendChat((byte)0, msg);
            inputChat.text = "";
            //FillTextChat(ClientConfig.UserInfo.NICKNAME, inputChat.text);
        }



        private void BtnCloseChatClick()
        {
            animator.Play("ClosePanelChat", 0);
        }

        public void SendChatEmotion(int index)
        {
            //Debug.Log("SendChatEmotionP: " + index);
            SendChat((byte)1, string.Format("{0}", index));
        }

        private void SendChat(byte type, string message)
        {
            Controller.OnHandleUIEvent("SendChat", message, type);
            BtnCloseChatClick();
        }

        public void SendQuickChat(int index)
        {
            //Debug.Log("SendQuickChat: " + index);
            SendChat((byte)0, itemQuickChats[index].GetComponentInChildren<Text>().text);
            //FillTextChat(ClientConfig.UserInfo.NICKNAME, itemQuickChats[index].GetComponentInChildren<Text>().text);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowPlayerChatEmotion(object[] param)
        {
            Debug.Log("ShowPlayerChatEmotion: ");
            string nickname = (string)param[0];
            string message = (string)param[1];
            long userid = (long)param[2];

            int index = int.Parse(message);
            ShowEmotionChat(userid,nickname, index);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
        private void ShowPlayerChat(object[] param)
        {
            string nickname = (string)param[0];
            string message = (string)param[1];
            FillTextChat(nickname, message);
            long userid = (long)param[2];
            ShowTextChat(userid, nickname, message);
        }

        private void FillTextChat(string nickname, string message)
        {

            string str = string.Format((nickname.Equals(ClientConfig.UserInfo.NICKNAME) ? "\n<color=yellow>{0}: </color>{1}" : "\n<color=blue>{0}: </color>{1}"), nickname, message);
            listTextChat.Add(str);
            while (listTextChat.Count > 20) listTextChat.RemoveAt(0);
            string msg = string.Join(string.Empty, listTextChat.ToArray());
            //Debug.Log("FillTextChat: " + listTextChat.Count);
            txtChat.text = msg;
        }

        private void ShowTextChat(long userid, string nickname, string message)
        {
            if (dlgShowTextChat != null)
            {
                dlgShowTextChat(userid, nickname, message);
            }
        }

        private void ShowEmotionChat(long userid, string nickname, int emotion)
        {
            Debug.Log("---- SHow emotion chat: " + (dlgShowEmotion == null) + " , nickname: " + nickname);
            if (dlgShowEmotion != null)
                dlgShowEmotion(userid, nickname, imgChatEmotions[emotion].sprite);
        }

        public void OpenChat()
        {
            if (btnHideChat == null)
            {
                RunAwakeEvent();
                AddListeners();
            }
            animator.Play("OpenPanelChat", 0);
            //BtnShowTextChatClick();
            if (listTextChat.Count > 0)
                txtChat.text = string.Join(string.Empty, listTextChat.ToArray());
        }
        
    }
}
