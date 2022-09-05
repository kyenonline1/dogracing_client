using Base.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.GamePlay.Slot;

namespace View.Home.Lobby
{
    public class ItemGameTopHuView : MonoBehaviour
    {
        public Image imgIconGame, imgEvent;
        public Text txtGameName;
        public EffectText txtJackpotMoney;
        public Sprite[] sprXhu;
        public Sprite sprEvent;

        private string GameId;

        public void InitData(Sprite spricongame, string gamename, long money, int xHu, int respot, bool isEvent, int curblind, string gameid)
        {
            //Debug.LogFormat("InitData: {0} , xhu: {1} , gameObject.name : {2} , isEvent: {3}, curBlind: {4}", gamename, xHu, gameObject.name, isEvent, curblind);
            GameId = gameid;

            if (imgIconGame) imgIconGame.sprite = spricongame;
            if (txtGameName) txtGameName.text = gamename;
            if (txtJackpotMoney)
            {
                txtJackpotMoney.END_VALUE = money;
                txtJackpotMoney.Effect();
                txtJackpotMoney.START_VALUE = money;
            }


            if (imgEvent)
            {
                if (!isEvent)
                {
                    imgEvent.gameObject.SetActive(false);
                    return;
                }
                //if (potcount == 0 && curpot == 0)
                //{
                //    imgEvent.gameObject.SetActive(false);
                //    return;
                //}

                imgEvent.gameObject.SetActive(true);

                if (respot == 0)
                {
                    if (isEvent) imgEvent.sprite = sprEvent;
                    else imgEvent.gameObject.SetActive(false);
                }
                else
                {
                    if (xHu == 2) imgEvent.sprite = sprXhu[xHu];
                }
                imgEvent.SetNativeSize();
            }
        }

        public void ClickJoinGame()
        {
            //Debug.Log("ClickJoinGame: " + GameId);
            //switch (GameId)
            //{
            //    case "MINI_TAIXIU":
            //        EventManager.Instance.RaiseEventInTopic(EventManager.OPEN_TAIXIU);
            //        break;
            //    case "F1":
            //        EventManager.Instance.RaiseEventInTopic(EventManager.OPEN_F1SLOT);
            //        break;
            //    case "MYNHAN":
            //        EventManager.Instance.RaiseEventInTopic(EventManager.OPEN_MYNHAN);
            //        break;
            //    case "TIENCA":
            //        EventManager.Instance.RaiseEventInTopic(EventManager.OPEN_TIENCA);
            //        break;
            //    case "MINI_POKER":
            //        EventManager.Instance.RaiseEventInTopic(EventManager.OPEN_MINIPOKER);
            //        break;
            //    case "HOAQUA":
            //        DialogEx.DialogExViewScript.Instance.ShowNotification("Game đang phát triển");
            //        break;
            //}
        }
    }
}
