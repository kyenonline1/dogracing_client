using Game.Gameconfig;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace View.Home.Lobby
{
    public class TopHuViewScript : MonoBehaviour
    {
        public UIMyButton btn100, btn1k, btn10k;
        public Text txtblind100, txtblind1k, txtblind10k;
        public Sprite[] sprIconGames;
        public ItemGameTopHuView[] itemGames;

        public Sprite sprActive, sprOff;

        Dictionary<string, int> mappingGameIdToSprite;
        Dictionary<string, string> mappingGameNameByGameID;
        int curBlind = 2;
        //Dictionary<string, GameProtocol.SLC.EventXhu[]> games;
        private void Awake()
        {
            mappingGameIdToSprite = new Dictionary<string, int>();
            mappingGameIdToSprite.Add(ClientGameConfig.GAMEID.TAMQUOC, 0);
            mappingGameIdToSprite.Add(ClientGameConfig.GAMEID.HAITAC, 1);
            mappingGameIdToSprite.Add(ClientGameConfig.GAMEID.MANHTHU, 2);
            mappingGameIdToSprite.Add(ClientGameConfig.GAMEID.SLOT3, 3);
            mappingGameIdToSprite.Add(ClientGameConfig.GAMEID.MINIPOKER, 4);
            mappingGameNameByGameID = new Dictionary<string, string>();

            mappingGameNameByGameID.Add(ClientGameConfig.GAMEID.TAMQUOC, "Vua Tam Quốc");
            mappingGameNameByGameID.Add(ClientGameConfig.GAMEID.HAITAC, "Prirates");
            mappingGameNameByGameID.Add(ClientGameConfig.GAMEID.MANHTHU, "Cuồng Ma");
            mappingGameNameByGameID.Add(ClientGameConfig.GAMEID.MINIPOKER, "Mini Poker");
            mappingGameNameByGameID.Add(ClientGameConfig.GAMEID.SLOT3, "Hỏa Phụng");
            btn100._onClick.AddListener(BtnBlind100Clicked);
            btn1k._onClick.AddListener(BtnBlind1kClicked);
            btn10k._onClick.AddListener(BtnBlind10kClicked);
           
        }

        private void BtnBlind100Clicked()
        {
            curBlind = 2;
            btn100.GetComponent<Image>().sprite = sprActive;
            btn1k.GetComponent<Image>().sprite = sprOff;
            btn10k.GetComponent<Image>().sprite = sprOff;
            txtblind100.color = Color.yellow;
            txtblind1k.color = txtblind10k.color = Color.gray;
            ProcessViewTopJackpot();
        }

        private void BtnBlind1kClicked()
        {
            curBlind = 1;
            btn100.GetComponent<Image>().sprite = sprOff;
            btn1k.GetComponent<Image>().sprite = sprActive;
            btn10k.GetComponent<Image>().sprite = sprOff;
            txtblind1k.color = Color.yellow;
            txtblind100.color = txtblind10k.color = Color.gray;
            ProcessViewTopJackpot();
        }

        private void BtnBlind10kClicked()
        {
            curBlind = 0;
            btn100.GetComponent<Image>().sprite = sprOff;
            btn1k.GetComponent<Image>().sprite = sprOff;
            btn10k.GetComponent<Image>().sprite = sprActive;
            txtblind10k.color = Color.yellow;
            txtblind1k.color = txtblind100.color = Color.gray;
            ProcessViewTopJackpot();
        }

        private void OnEnable()
        {
            ProcessViewTopJackpot();
        }

        public void ProcessViewTopJackpot()
        {
            //if (games == null) return;
            //if (mappingGameIdToSprite == null) return;
            //int index = 0;
            //foreach(var game in games)
            //{
            //    //Debug.Log("ProcessViewTopJackpot: " + game.Key + " - " + mappingGameIdToSprite.ContainsKey(game.Key) + " - blind: " + curBlind + " index: " + index);
            //    if (mappingGameIdToSprite.ContainsKey(game.Key))
            //    {
            //        GameProtocol.SLC.EventXhu grGameID = game.Value[2 - curBlind];
            //        //Debug.Log("grGameID: " + grGameID.GameId + " - " + grGameID.Blind + " - " + grGameID.Jackpot);
            //        itemGames[index].InitData(sprIconGames[mappingGameIdToSprite[game.Key]], mappingGameNameByGameID[game.Key], grGameID.Jackpot, grGameID.XHu,
            //            grGameID.RestPot,
            //            grGameID.IsEvent, curBlind, grGameID.GameId);
            //        index += 1;
            //    }
            //}

            
        }

        //public void SetData(GameProtocol.Lobby.GameSlotConfig[] _games)
        //{
        //    games = _games;
        //    if (games != null)
        //        ProcessViewTopJackpot();
        //}

        //public void SetData(Dictionary<string, GameProtocol.SLC.EventXhu[]> _games)
        //{
        //    games = _games;
        //    if (gameObject.activeInHierarchy && games != null)
        //        ProcessViewTopJackpot();
        //}


    }
}
