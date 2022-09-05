
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.Common.Help;
using View.Setting;

namespace View.Common.Chat
{
    public class ChatInit : MonoBehaviour
    {
        public UIMyButton btnChat;
        //public UIMyButton btnHelp;
        public UIMyButton btnSetting;
        public Transform[] tranChatText;
        public Transform[] tranChatEmotion;
        public GameObject panelChat,
            panelSetting;
        private List<Text> textChats;
        private List<Image> imgEmotions;

        public GameObject panelHelp;

        public delegate int GetPosition(long userid);
        public GetPosition dlgGetPosition;

        private void Awake()
        {
            textChats = new List<Text>();
            imgEmotions = new List<Image>();
            for (int i = 0; i < tranChatText.Length; i++)
                textChats.Add(tranChatText[i].GetComponentInChildren<Text>());
            for (int i = 0; i < tranChatEmotion.Length; i++)
                imgEmotions.Add(tranChatEmotion[i].GetComponentInChildren<Image>());
            btnChat._onClick.AddListener(BtnClickOpenChat);
            //btnHelp._onClick.AddListener(BtnClickOpenHelp);
            btnSetting._onClick.AddListener(BtnClickOpenSetting);
            if (panelChat != null)
            {
                panelChat.GetComponent<ChatViewScript>().dlgShowTextChat += ShowTextChat;
                panelChat.GetComponent<ChatViewScript>().dlgShowEmotion += ShowEmotionChat;
            }
        }

        private void BtnClickOpenChat()
        {
            //Debug.Log("Click Open Chat");
            if (panelChat != null)
            {
                panelChat.GetComponent<ChatViewScript>().OpenChat();
            }
            //else
            //{
            //    LoadAssetBundle.LoadPrefab(TagAssetBundle.Tag_Prefab.TAG_PREFAB_COMMON, "PanelChat", (chat) =>
            //    {
            //        if (chat)
            //        {
            //            chat.name = "PanelChat";
            //            panelChat = chat;
            //            panelChat.transform.SetParent(transform);
            //            panelChat.transform.localScale = Vector3.one;
            //            panelChat.transform.localPosition = Vector3.zero;
            //            panelChat.GetComponent<ChatViewScript>().dlgShowTextChat += ShowTextChat;
            //            panelChat.GetComponent<ChatViewScript>().dlgShowEmotion += ShowEmotionChat;
            //            panelChat.GetComponent<ChatViewScript>().OpenChat();
            //        }
            //    }, null);
            //}
        }

        private void BtnClickOpenHelp()
        {
            if (panelHelp != null)
            {
                panelHelp.GetComponent<HelpInGameView>().OpenHelp();
            }
            //else
            //{
            //    LoadAssetBundle.LoadPrefab(TagAssetBundle.Tag_Prefab.TAG_PREFAB_COMMON, "PanelHelpIngame", (help) =>
            //    {
            //        help.name = "PanelHelp";
            //        panelHelp = help;
            //        panelHelp.transform.SetParent(transform);
            //        panelHelp.transform.localScale = Vector3.one;
            //        panelHelp.transform.localPosition = Vector3.zero;
            //        panelHelp.GetComponent<HelpInGameView>().OpenHelp();
            //    }, null);
            //}
        }

        private void BtnClickOpenSetting()
        {
            if (panelSetting != null)
            {
                panelSetting.GetComponent<SettingViewScript>().OpenSetting();
            }
            //else
            //{
            //    LoadAssetBundle.LoadPrefab(TagAssetBundle.Tag_Prefab.TAG_PREFAB_KHUNGGAME, "Setting", (setting) =>
            //    {
            //        setting.name = "PanelSetting";
            //        panelSetting = setting;
            //        panelSetting.transform.SetParent(transform);
            //        panelSetting.transform.localScale = Vector3.one;
            //        panelSetting.transform.localPosition = Vector3.zero;
            //        panelSetting.GetComponent<SettingViewScript>().OpenSetting();
            //    }, null);
            //}
        }

        private void ShowTextChat(long userid, string nickname, string message)
        {
            try
            {
                Debug.Log("ShowTextChat: " + nickname);
                if (dlgGetPosition == null) return;
                int pos = dlgGetPosition(userid);
                Debug.Log("ShowTextChat: " + pos);
                if (pos == -1) return;
                textChats[pos].text = message;
                tranChatText[pos].GetComponent<Animator>().Play("ShowTextChat", 0);
            }
            catch
            {

            }
        }

        private void ShowEmotionChat(long userid, string nickname, Sprite emotion)
        {
            try
            {
                Debug.Log("ShowEmotionChat: " + nickname);
                if (dlgGetPosition == null) return;
                int pos = dlgGetPosition(userid);
                Debug.Log("ShowEmotionChat: " + pos);
                if (pos == -1) return;
                imgEmotions[pos].sprite = emotion;
                tranChatEmotion[pos].GetComponent<Animator>().Play("ShowEmotion", 0);
            }
            catch
            {

            }
        }
    }
}
