using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.Home.Lobby
{
    public class ItemTableLobbyPanelView : MonoBehaviour
    {
        [SerializeField] private Text txtRegion;
        [SerializeField] private Text txtValueBlind;
        [SerializeField] private Text txtValueMinCashin;
        [SerializeField] private Text txtUserPlayGame;
        [SerializeField] private GameObject objFlash;
        [SerializeField] private GameObject objWinner;
        [SerializeField] private GameObject objAnte;
        [SerializeField] private GameObject objStraddle;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <param name="valueBlind"></param>
        /// <param name="valueMincashin"></param>
        /// <param name="countUser"></param>
        /// <param name="tableType">0: normal, 1: flash, 2: winner</param>
        public void UpdateDataTableInfo(string region, string valueBlind, string valueMincashin, string countUser, int tableType, bool isAnte, bool isStraddle)
        {
            if (txtRegion) txtRegion.text = region;
            if (txtValueBlind) txtValueBlind.text = valueBlind;
            if (txtValueMinCashin) txtValueMinCashin.text = valueMincashin;
            if (txtUserPlayGame) txtUserPlayGame.text = countUser;
            if (objFlash) objFlash.SetActive(tableType == 1);
            if (objWinner) objWinner.SetActive(tableType == 2);
            if (objAnte) objAnte.SetActive(isAnte);
            if (objStraddle) objStraddle.SetActive(isStraddle);
        }
    }
}