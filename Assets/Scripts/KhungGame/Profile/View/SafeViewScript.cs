using AppConfig;
using Base.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;
using View.DialogEx;

public class SafeViewScript : MonoBehaviour {

    public delegate void SendGoldToSafe(long gold);
    public SendGoldToSafe dlgSendGoldToSafe;
    public delegate void GetGoldToSafe(long gold, int otp);
    public GetGoldToSafe dlgGetGoldToSafe;

    public Action dlgGetOTP;

    public Text txtChipGold,
        txtChipSafe;

    public UIMyButton btnSet,
        btnGet;
    public GameObject objSet,
        objGet;
    public UIMyButton[] btnBlinds;

    [Header("Set")]
    public InputField inputSetChipGold;
    /// <summary>
    ///btnSetAll: Xét toàn bộ chip vàng hiện tại,  btnSetSafe: xác nhận gửi tiền vào Két
    /// </summary>
    public UIMyButton btnSetAll,
        btnSetSafe;

    public Sprite sprButtonSelected,
        sprButtonUnSelected;

    [Header("Get")]
    public InputField inputGetChipGold;
    public InputField inputGetOTP;
    public UIMyButton btnGetOTP,
        btnGetChipGold,
        btnGetAllChipGold;

    private int[] blinds;
    private bool isSet;
    private long curMoney;

    private void Awake()
    {
        blinds = new int[]
        {
            5000, 10000, 50000, 100000
        };

        for(int i = 0; i < 4; i++)
        {
            int index = i;
            btnBlinds[i]._onClick.AddListener(() => { ClickBlind(index); });
        }

        btnGet._onClick.AddListener(ClickBtnShowGet);
        btnSet._onClick.AddListener(ClickBtnShowSet);

        btnSetAll._onClick.AddListener(ClickBtnSetAll);
        btnSetSafe._onClick.AddListener(ClickBtnSetSafe);

        btnGetOTP._onClick.AddListener(ClickBtnGetOTP);
        btnGetAllChipGold._onClick.AddListener(ClickBtnGetAllChip);
        btnGetChipGold._onClick.AddListener(ClickBtnGetChip);
        EventManager.Instance.SubscribeTopic(EventManager.CHANGE_BALANCE, ChangeBlance);
    }

    private void OnEnable()
    {
        ClickBtnShowSet();
        txtChipGold.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.GOLD);
        txtChipSafe.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.GOLD_SAFE);
    }

    private void ChangeBlance()
    {
        txtChipGold.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.GOLD);
        txtChipSafe.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.GOLD_SAFE);
        if (isSet)
        {
            inputSetChipGold.text = string.Empty;
            curMoney = 0;
        }
        else
        {
            inputGetChipGold.text = string.Empty;
            inputGetOTP.text = string.Empty;
            curMoney = 0;
        }
    }

    private void ClickBtnShowSet()
    {
        if (isSet) return;
        isSet = true;
        btnSet.GetComponent<Image>().sprite = sprButtonSelected;
        btnGet.GetComponent<Image>().sprite = sprButtonUnSelected;
        objSet.SetActive(true);
        objGet.SetActive(false);
        curMoney = 0;
    }

    private void ClickBtnShowGet()
    {
        if (!isSet) return;
        isSet = false;
        btnGet.GetComponent<Image>().sprite = sprButtonSelected;
        btnSet.GetComponent<Image>().sprite = sprButtonUnSelected;
        objSet.SetActive(false);
        objGet.SetActive(true);
        curMoney = 0;
    }

    private void ClickBlind(int index)
    {
        if (isSet)
        {
            if (ClientConfig.UserInfo.GOLD < 5000)
            {
                DialogExViewScript.Instance.ShowDialog("Số Chip Vàng không đủ.");
                return;
            }
            if (ClientConfig.UserInfo.GOLD - (curMoney + blinds[index]) < 0)
            {
                DialogExViewScript.Instance.ShowDialog("Số Chip Vàng không đủ.");
                return;
            }
            curMoney += blinds[index];
            txtChipGold.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.GOLD - curMoney);
            txtChipSafe.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.GOLD_SAFE + curMoney);
            inputSetChipGold.text = MoneyHelper.FormatNumberAbsolute(curMoney);
        }
        else
        {
            if (ClientConfig.UserInfo.GOLD_SAFE < 5000)
            {
                DialogExViewScript.Instance.ShowDialog("Số Chip Vàng trong Két không đủ.");
                return;
            }

            if (ClientConfig.UserInfo.GOLD_SAFE - (curMoney + blinds[index]) < 0)
            {
                DialogExViewScript.Instance.ShowDialog("Số Chip Vàng trong Két không đủ.");
                return;
            }
            curMoney += blinds[index];
            txtChipGold.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.GOLD + curMoney);
            txtChipSafe.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.GOLD_SAFE - curMoney);
            inputGetChipGold.text = MoneyHelper.FormatNumberAbsolute(curMoney);
        }
    }

    private void ClickBtnSetAll()
    {
        if (ClientConfig.UserInfo.GOLD <= 0)
        {
            DialogExViewScript.Instance.ShowDialog("Số Chip Vàng không đủ.");
            return;
        }
        curMoney = ClientConfig.UserInfo.GOLD;
        txtChipGold.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.GOLD - curMoney);
        txtChipSafe.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.GOLD_SAFE + curMoney);
        inputSetChipGold.text = MoneyHelper.FormatNumberAbsolute(curMoney);
    }

    /// <summary>
    /// CLick Xác nhận gửi tiền vào Két
    /// </summary>
    private void ClickBtnSetSafe()
    {
        //TODO; Send gold to safe
        if (!string.IsNullOrEmpty(inputSetChipGold.text))
        {
            long money = long.Parse(inputSetChipGold.text);
            if (money > 0) curMoney = money;
        }
        if(curMoney <= 0)
        {
            DialogExViewScript.Instance.ShowNotification("Vui lòng nhập số Chip Vàng cần gửi");
            return;
        }

        if (curMoney > ClientConfig.UserInfo.GOLD)
        {
            DialogExViewScript.Instance.ShowNotification("Số Chip Vàng hiện tại không đủ để gửi vào Két.");
            return;
        }
        if (dlgSendGoldToSafe != null) dlgSendGoldToSafe(curMoney);
    }

    private void ClickBtnGetOTP()
    {
        if (dlgGetOTP != null) dlgGetOTP();
    }

    private void ClickBtnGetAllChip()
    {

        if (ClientConfig.UserInfo.GOLD_SAFE <= 0)
        {
            DialogExViewScript.Instance.ShowDialog("Số Chip Vàng không đủ.");
            return;
        }
        curMoney = ClientConfig.UserInfo.GOLD_SAFE;
        txtChipGold.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.GOLD + curMoney);
        txtChipSafe.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.GOLD_SAFE - curMoney);
        inputGetChipGold.text = MoneyHelper.FormatNumberAbsolute(curMoney);
    }

    /// <summary>
    /// Click xác nhận rút tiền từ két sắt
    /// </summary>
    private void ClickBtnGetChip()
    {
        //TODO: Get Gold to safe
        if (!string.IsNullOrEmpty(inputGetChipGold.text))
        {
            long money = long.Parse(inputGetChipGold.text);
            if (money > 0) curMoney = money;
        }
        if (curMoney <= 0)
        {
            DialogExViewScript.Instance.ShowNotification("Vui lòng nhập số Chip Vàng cần rút");
            return;
        }

        string otp = inputGetOTP.text;
        if (string.IsNullOrEmpty(otp))
        {
            DialogExViewScript.Instance.ShowNotification("Vui lòng nhập mã OTP.");
            return;
        }

        if (curMoney > ClientConfig.UserInfo.GOLD_SAFE)
        {
            DialogExViewScript.Instance.ShowNotification("Số Chip Vàng trong két không đủ");
            return;
        }

        if (dlgGetGoldToSafe != null) dlgGetGoldToSafe(curMoney, int.Parse(otp));
    }
    private void OnDestroy()
    {

        EventManager.Instance.UnSubscribeTopic(EventManager.CHANGE_BALANCE, ChangeBlance);
    }
}
