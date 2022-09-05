using AssetBundles;
using Common.Card;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;

namespace View.Home.History
{
    public class ItemLobbyHistoryPlayGameView : MonoBehaviour
    {
        public Action<int, int, string, bool, bool> callbackClickAddCollection;

        [SerializeField] private Image[] handCards;
        [SerializeField] private Text txtDateTime;
        [SerializeField] private Text txtGameType;
        [SerializeField] private Text txtBlind;
        [SerializeField] private Text txtMoneyResult;
        [SerializeField] private GameObject goCollection;
        [SerializeField] private GameObject goCollectionSelected;
        [SerializeField] private GameObject goRemoveFromCollection;

        private int tableId;
        private int gameSession;
        private string gameId;
        private bool isSave;

        public void SetData(int tableid, int gamesseion, int[] handcards, string timer, string gametype, int blind, int ante, long moneyresult, bool isSave, bool isCateCollection)
        {
            this.tableId = tableid;
            this.gameSession = gamesseion;
            this.gameId = gametype;
            this.isSave = isSave;

            if (handCards != null)
            {
                for(int i = 0; i < handcards.Length; i++)
                {
                    handCards[i].sprite = CardAtlas.Instance.ConvertToSprite(handcards[i]);
                }
            }
            if (txtDateTime) txtDateTime.text = timer;
            switch (gametype)
            {
                case "POKER":
                    if (txtGameType) txtGameType.text = Languages.Language.GetKey("LOBBY_TOP_LOBBY");
                    break;
                case "SPIN":
                    if (txtGameType) txtGameType.text = Languages.Language.GetKey("LOBBY_SPINUP_TITTLE");
                    break;
                case "TOUR":
                    if (txtGameType) txtGameType.text = Languages.Language.GetKey("LOBBY_TOP_MTT");
                    break;
            }
            if(ante > 0)
            {
                if (txtBlind) txtBlind.text = string.Format("{0}/{1}({2})", MoneyHelper.FormatRelativelyWithoutUnit(blind), MoneyHelper.FormatRelativelyWithoutUnit(blind * 2),
                     MoneyHelper.FormatRelativelyWithoutUnit(ante));
            }
            else
            {
                if (txtBlind) txtBlind.text = string.Format("{0}/{1}", MoneyHelper.FormatRelativelyWithoutUnit(blind), MoneyHelper.FormatRelativelyWithoutUnit(blind * 2));
            }
            if (txtMoneyResult) txtMoneyResult.text = MoneyHelper.FormatNumberAbsolute(moneyresult);
            if (goCollection) goCollection.SetActive(!isCateCollection);
            if (goCollectionSelected) goCollectionSelected.SetActive(isSave);
            if (goRemoveFromCollection) goRemoveFromCollection.SetActive(isCateCollection);
        }

        public void OnBtnAddCollectionClicked()
        {
            if(callbackClickAddCollection != null)
            {
                callbackClickAddCollection(tableId, gameSession, gameId, !isSave, false);
            }
        }

        public void OnBtnRemoveHistoryClicked()
        {
            if (callbackClickAddCollection != null)
            {
                callbackClickAddCollection(tableId, gameSession, gameId, false, true);
            }
        }

        public void OnBtnShowDetailClicked()
        {
            Game.Gameconfig.ClientGameConfig.PokerHistoryInfo.TableId = tableId;
            Game.Gameconfig.ClientGameConfig.PokerHistoryInfo.Gamesession = gameSession;
            Game.Gameconfig.ClientGameConfig.PokerHistoryInfo.GameId = gameId;
            if (DialogEx.DialogExViewScript.Instance) DialogEx.DialogExViewScript.Instance.ShowLoading(true);
            //LoadAssetBundle.LoadScene(TagAssetBundle.Tag_Scene.TAG_SCENE_POKER, TagAssetBundle.SceneName.POKER_REVIEW_SCENE);
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.CloseHistoryDetail();

            //if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowHistoryDetail(tableId, gameSession, gameId);
        }
    }
}