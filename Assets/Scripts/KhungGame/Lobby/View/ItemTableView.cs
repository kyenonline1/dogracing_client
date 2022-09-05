using AppConfig;
using EnhancedUI.EnhancedScroller;
using Game.Gameconfig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;
using View.DialogEx;
using View.Home.Lobby;

public class ItemTableView : EnhancedScrollerCellView
{

    public delegate void ClickJoinTable(int index);
    public ClickJoinTable dlgJoinTable;

    //private int indexCell;
    private long tableId;
    private byte tableName;
    private int blind;
    private string gameName;
    public Text txtTableID;
    public Text txtBlind;
    public Text txtTableName;
    public GameObject[] objMaxPlayer;
    public GameObject[] objCurPlayer;
    public GameObject background;

    // Use this for initialization
    private void Awake()
    {
        GetComponent<UIMyButton>()._onClick.AddListener(ClickJoinGame);
    }

    private void ClickJoinGame()
    {
        if (LobbyViewScript.Instance)
        {
            int minCash = 0;
            switch (gameName)
            {
                case "POKER":
                case "LIENG":
                case "XITO":
                    minCash = 10;
                    break;
                case "MAU_BINH":
                    minCash = 72;
                    break;
                case "TLMNSL":
                case "TLMN":
                    minCash = 26;
                    break;
                case "XOC_DIA":
                    minCash = 1;
                    break;
                case "SAM":
                    minCash = 20;
                    break;
                case "PHOM":
                    minCash = 10;
                    break;
                default:
                    DialogExViewScript.Instance.ShowNotification("Game Không tồn tại.");
                    return;
            }
            if ((ClientGameConfig.CASH_TYPE.CASHTYPE == 0 && ClientConfig.UserInfo.SILVER < blind * minCash)
                           || (ClientGameConfig.CASH_TYPE.CASHTYPE == ClientGameConfig.CASH_TYPE.CASH.GOLD && ClientConfig.UserInfo.GOLD < blind * minCash))
            {
                DialogExViewScript.Instance.ShowNotification("Không đủ tiền vào bàn");
                return;
            }
            LobbyViewScript.Instance.RequestJoinGame(blind, tableName, tableId);
        }
        //if (dlgJoinTable != null)
        //{
        //    dlgJoinTable(indexCell);
        //}
    }

    public void SetData(long _tbId, int _blind, byte tbName, int maxPlayer, int curPlayer, int idxCell, string gameid)
    {
        //indexCell = idxCell;
        tableName = tbName;
        tableId = _tbId;
        blind = _blind;
        gameName = gameid;
        txtTableID.text = _tbId.ToString();
        txtBlind.text = MoneyHelper.FormatNumberAbsolute(_blind);
        txtTableName.text = Game.Gameconfig.ClientGameConfig.GetTableName(tbName, gameid);
        for (int i = 0; i < objMaxPlayer.Length; i++) objMaxPlayer[i].SetActive(false);
        for (int i = 0; i < objCurPlayer.Length; i++) objCurPlayer[i].SetActive(false);
        for (int i = 0; i < maxPlayer; i++) objMaxPlayer[i].SetActive(true);
        for (int i = 0; i < curPlayer; i++) objCurPlayer[i].SetActive(true);
        background.SetActive(idxCell % 2 == 0);
    }
}
