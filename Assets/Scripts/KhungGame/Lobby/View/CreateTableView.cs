using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;
using View.Home.Lobby;

public class CreateTableView : MonoBehaviour {

    public UIMyButton btnClose,
        btnCreateTable,
        btnCashGold,
        btnCashSilver;
    public Text txtTableName;
    public UIMyButton[] btnBlinds;
    public Image[] imgBlind;
    public Image imgCashGold,
        imgCashSilver;
    public Text[] txtBlind;
    public Sprite sprEnable,
        sprDisable;
    public RectTransform tranBlinds;
    private int[] blinds;
    private byte curType,
        curTableName;
    private int blindSelected;
    private string curgame;
    // Use this for initialization
    private void Awake()
    {
        for (int i = 0; i < btnBlinds.Length; i++)
        {
            int idx = i;
            btnBlinds[i]._onClick.AddListener(() => UpdateSpriteBlind(idx));
        }

        btnClose._onClick.AddListener(BtnCloseClick);
        btnCashGold._onClick.AddListener(BtnClickCashGold);
        btnCashSilver._onClick.AddListener(BtnClickCashSilver);
        btnCreateTable._onClick.AddListener(BtnClickCreateTable);
    }
    void Start () {
		
	}

    public void OpenCreateTable(int[] _blinds, string gameId)
    {
        try
        {
            blinds = _blinds;
            curType = (byte)Game.Gameconfig.ClientGameConfig.CASH_TYPE.CASHTYPE;
            if(curType == 0)
            {
                imgCashGold.sprite = sprDisable;
                imgCashSilver.sprite = sprEnable;
            }
            else
            {
                imgCashSilver.sprite = sprDisable;
                imgCashGold.sprite = sprEnable;
            }
            curgame = gameId;
            gameObject.SetActive(true);
            SetPositionBlind();
            for (int i = 0; i < btnBlinds.Length; i++) btnBlinds[i].gameObject.SetActive(false);
            for (int i = 0; i < _blinds.Length; i++)
            {
                btnBlinds[i].gameObject.SetActive(true);
                txtBlind[i].text = MoneyHelper.FormatRelativelyWithoutUnit(_blinds[i]);
            }
            UpdateSpriteBlind(0);
        }
        catch (Exception ex)
        {
            Debug.Log("UpdateSpriteBlind Exception: " + ex.StackTrace);
        }
    }

    private void UpdateSpriteBlind(int index)
    {
        try
        {
            blindSelected = blinds[index];
            for (int i = 0; i < imgBlind.Length; i++)
            {
                if (i == index) imgBlind[i].sprite = sprEnable;
                else imgBlind[i].sprite = sprDisable;
            }
            curTableName = Game.Gameconfig.ClientGameConfig.GetByteTableName(curgame);
            txtTableName.text = Game.Gameconfig.ClientGameConfig.GetTableName(curTableName, curgame);
        }
        catch(Exception ex)
        {
            Debug.Log("UpdateSpriteBlind Exception: " + ex.StackTrace);
        }
    }

    private void BtnClickCashSilver()
    {
        imgCashGold.sprite = sprDisable;
        imgCashSilver.sprite = sprEnable;
        curType = 0;
        if (curgame.Equals("XOC_DIA"))
        {
            blinds = new int[7] { 1000, 5000, 10000, 50000, 100000, 300000, 1000000 };
            OpenCreateTable(blinds, curgame);
        }
    }

    private void BtnClickCashGold()
    {
        imgCashSilver.sprite = sprDisable;
        imgCashGold.sprite = sprEnable;
        curType = 1;
        if (curgame.Equals("XOC_DIA"))
        {
            blinds = new int[4] { 100, 1000, 10000, 100000 };
            OpenCreateTable(blinds, curgame);
        }
    }

    private void BtnClickCreateTable()
    {
        Game.Gameconfig.ClientGameConfig.CASH_TYPE.CASHTYPE = (Game.Gameconfig.ClientGameConfig.CASH_TYPE.CASH)curType;
        if (LobbyViewScript.Instance) LobbyViewScript.Instance.RequestJoinGame(blindSelected, curTableName);
    }

    private void BtnCloseClick()
    {
        gameObject.SetActive(false);
    }

    private void SetPositionBlind()
    {
        if (blinds.Length == 3)
        {
            tranBlinds.localPosition = new Vector3(-55, 65);
        }
        else if (blinds.Length == 4)
        {
            tranBlinds.localPosition = new Vector3(-110, 65);
        }
        else
        {
            tranBlinds.localPosition = new Vector3(-110, 85);
        }
    }

   

}
