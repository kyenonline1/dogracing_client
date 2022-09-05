using AppConfig;
using CoreBase;
using CoreBase.Controller;
using Game.Gameconfig;
using GameProtocol.COU;
using GameProtocol.PAY;
using Interface;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utilites;
using View.Common;
using View.DialogEx;
using View.Home.ExChange;

public class NapTheViewScript : ViewScript
{

    public Sprite sprCateSelected, sprCateUnSelected;

    public UIMyButton cateCharging, cateExChange, cateHistory, cateGoldToSilver, cateChargingHistory, cateMomo;
    public Image imgNapLoc, imgExchange, imgLsDT, imgLSNap, imgMomo;
    public GameObject objCharing, objExChange, objHistory, objExChangeGoldToSilver, objChargingHistory, objMomo;

    public Dropdown dropdownCardType,
        dropdownValueCard;
    public InputField inputSeri,
        inputCode,
        inputCapcha;
    //public Text txtCapcha;
    public Image imgCapcha;
    public Transform tranCardType;
    public Text txtVuiCoin;
    public RectTransform tranDropDownCardType;
    private string cardType;
    [SerializeField]
    private int valueCard = 10000;

    private float rateCharging;
    private int[] listCardValue;
    [SerializeField]
    private CardRateView[] lstCardRates;

    [Header("LS NAP THE")]
    public ScrollCharingHistory scrollCharingHistory;

    [Header("Doi Thuong")]
    //public Text[] txtTileDoiTHuong;  [SerializeField]
    [SerializeField]
    private int[] lstPricesExchange;
    [SerializeField]
    private CardRateView[] lstCardRatesExchange;
    public ItemExChangeView[] Telecos;
    //public InputField InputOTP;
    public Sprite[] sprTelcos;
    private Dictionary<string, Sprite> mappingSprTelco;


    [Header("Ls DOi Thuong")]
    public ScrollDataHistory scrollerHistory;

    [Header("Doi Gold Ra Silver")]
    public InputField inputChipExChange;
    public UIMyButton btnExChange;
    public Text txtCurGold, txtSilver, txtChipBacReceived;

    public Text txtTitle;

    [Header("Chi Tiết thẻ")]
    public Transform ObjDetailCard;
    public Text txtCardName,
        txtSeriCard,
        txtMaThe;
    public Button btnCloseDetail,
        btnNapTheDetail;

    [Header("Momo")]
    public Text txtNumberPhone;
    public Text txtUserHolder;
    public Text txtNickName;
    public Text txtTitleRateMomo;
    [SerializeField]
    private CardRateView[] lstCardRatesMomo;
    [SerializeField]
    private int[] pricesMomo;
    [SerializeField]
    private GameObject objCopySuccess;

    public enum CateType
    {
        CHARGING,
        EXCHANGE,
        HISOTRYEX,
        GOLDTOSILVER,
        CHARGINGHISTORY,
        MOMO
    }

    public CateType cateType = CateType.CHARGING;

    // Use this for initialization


    protected override IController CreateController()
    {
        return new NapTheController(this);
    }

    protected override void DestroyGameScript()
    {
        base.DestroyGameScript();
    }

    protected override void InitGameScriptInAwake()
    {
        base.InitGameScriptInAwake();
        dropdownCardType.onValueChanged.AddListener((index) =>
        {
            if (cates != null && cates.Length > 0)
            {
                listCardValue = cates[index].Amounts;
                cardType = cates[index].Name;
                ResetCardRate();
                InitDataCardRate(cates[index].Amounts, rateCharging);
            }
        });

        mappingSprTelco = new Dictionary<string, Sprite>();
        mappingSprTelco.Add("Viettel", sprTelcos[0]);
        mappingSprTelco.Add("Vina", sprTelcos[1]);
        mappingSprTelco.Add("Mobi", sprTelcos[2]);

        cateCharging._onClick.AddListener(ClickCharging);
        cateChargingHistory._onClick.AddListener(ClickChargingHistory);
        cateExChange._onClick.AddListener(ClickExChange);
        cateHistory._onClick.AddListener(ClickHistoryExChange);
        cateMomo._onClick.AddListener(ClickMomo);
        cateGoldToSilver._onClick.AddListener(ClickGOLDTOSILVER);
        btnExChange._onClick.AddListener(ClickChangeGoldToSilver);
        btnCloseDetail.onClick.AddListener(CloseDetailClick);
//#if !UNITY_STANDALONE
//        cateCharging.gameObject.SetActive(GameConfig.APPFUNCTION.IsAppCharging);
//        cateChargingHistory.gameObject.SetActive(GameConfig.APPFUNCTION.IsAppCharging);
//        cateExChange.gameObject.SetActive(GameConfig.APPFUNCTION.IsAppFullFunction);
//        cateHistory.gameObject.SetActive(GameConfig.APPFUNCTION.IsAppFullFunction);
//#endif
        inputChipExChange.onValueChanged.AddListener(ConvertToChipBac);

        dropdownValueCard.onValueChanged.AddListener((index) =>
        {
            if (index < listCardValue.Length)
            {
                valueCard = listCardValue[index];
            }
            //else valueCard = listCardValue[0];

            //switch (index)
            //{
            //    case 0:
            //        valueCard = 10000;
            //        break;
            //    case 1:
            //        valueCard = 20000;
            //        break;
            //    case 2:
            //        valueCard = 50000;
            //        break;
            //    case 3:
            //        valueCard = 100000;
            //        break;
            //    case 4:
            //        valueCard = 200000;
            //        break;
            //    case 5:
            //        valueCard = 500000;
            //        break;
            //}
        });
    }

    protected override void InitGameScriptInStart()
    {
        base.InitGameScriptInStart();
    }


    public void ShowNapThe()
    {
        switch (cateType)
        {
            case CateType.CHARGING:
                break;
            case CateType.EXCHANGE:
                break;
            case CateType.HISOTRYEX:
                break;
        }
    }

    public void CloseNapThe()
    {
        inputCapcha.text = inputCode.text = inputSeri.text = inputChipExChange.text = "";
        gameObject.SetActive(false);
    }

    public void ClickNapThe()
    {
        if (string.IsNullOrEmpty(cardType))
        {
            DialogExViewScript.Instance.ShowNotification("Chưa chọn loại thẻ cần nạp.");
            return;
        }
        if (valueCard <= 0)
        {
            DialogExViewScript.Instance.ShowNotification("Chưa chọn mện giá thẻ cần nạp.");
            return;
        }
        string seri = inputSeri.text;
        if (string.IsNullOrEmpty(seri))
        {
            DialogExViewScript.Instance.ShowNotification("Chưa nhập số Seri");
            return;
        }
        string code = inputCode.text;
        if (string.IsNullOrEmpty(code))
        {
            DialogExViewScript.Instance.ShowNotification("Chưa nhập Mã thẻ");
            return;
        }
        //string capcha = inputCapcha.text;
        //if (string.IsNullOrEmpty(capcha))
        //{
        //    DialogExViewScript.Instance.ShowNotification("Chưa nhập Capcha");
        //    return;
        //}
        //Thẻ cào Viettel có số seri 11 ký tự thì mã thẻ có 13 số.
        //Thẻ cào Viettel có số seri 14 ký tự thì mã thẻ có 15 số.
        //Dãy số seri thẻ Mobifone có: 15 số., mã 12 số
        //Theo quy định thì mã thẻ cào Vinaphone bao gồm 2 loại là 12 số và 14 số.seri có 14 số
        Debug.Log("NAP THE: SERI: " + seri.Length + " , mathe: " + code.Length + " , Telco: " + cates[dropdownCardType.value].Type);
        switch (cates[dropdownCardType.value].Type)
        {
            case 2: //VT
                if (seri.Length != 11 && seri.Length != 14)
                {
                    DialogExViewScript.Instance.ShowDialog("Seri không hợp lệ, Seri gồm 11 hoặc 14 ký tự.");
                    DialogExViewScript.Instance.ShowLoading(false);
                }
                else
                {
                    if (seri.Length == 11)
                    {
                        if (code.Length == 13)
                        {
                            DialogExViewScript.Instance.ShowLoading(true);
                            Controller.OnHandleUIEvent("RequestNapThePAY2", cates[dropdownCardType.value].Name, seri, code, valueCard, 0);
                        }
                        else
                        {
                            DialogExViewScript.Instance.ShowDialog("Mã thẻ không hợp lệ, Mã thẻ gồm 13 ký tự.");
                            DialogExViewScript.Instance.ShowLoading(false);
                        }
                    }
                    else if (seri.Length == 14)
                    {
                        if (code.Length == 15)
                        {
                            DialogExViewScript.Instance.ShowLoading(true);
                            Controller.OnHandleUIEvent("RequestNapThePAY2", cates[dropdownCardType.value].Name, seri, code, valueCard, 0);
                        }
                        else
                        {
                            DialogExViewScript.Instance.ShowDialog("Mã thẻ không hợp lệ, Mã thẻ gồm 15 ký tự.");
                            DialogExViewScript.Instance.ShowLoading(false);
                        }
                    }
                    else
                    {
                        DialogExViewScript.Instance.ShowDialog("Mã thẻ hoặc Seri không hợp lệ. Vui lòng kiểm tra lại.");
                    }
                }
                break;
            case 4: //MB
                if (seri.Length != 15)
                {
                    DialogExViewScript.Instance.ShowDialog("Seri không hợp lệ, Seri gồm 15 ký tự.");
                    DialogExViewScript.Instance.ShowLoading(false);
                }
                else
                {
                    if (code.Length != 12)
                    {
                        DialogExViewScript.Instance.ShowDialog("Mã thẻ không hợp lệ, Mã thẻ gồm 12 ký tự.");
                        DialogExViewScript.Instance.ShowLoading(false);
                    }
                    else
                    {
                        DialogExViewScript.Instance.ShowLoading(true);
                        Controller.OnHandleUIEvent("RequestNapThePAY2", cates[dropdownCardType.value].Name, seri, code, valueCard, 0);
                    }
                }
                break;
            case 3: //VN
                if (code.Length != 12 && code.Length != 14)
                {
                    DialogExViewScript.Instance.ShowDialog("Mã thẻ không hợp lệ, Mã thẻ gồm 12 hoặc 14 ký tự.");
                    DialogExViewScript.Instance.ShowLoading(false);
                }
                else
                {
                    if (seri.Length != 14)
                    {
                        DialogExViewScript.Instance.ShowDialog("Seri không hợp lệ, Seri gồm 14 ký tự.");
                        DialogExViewScript.Instance.ShowLoading(false);
                    }
                    else
                    {
                        DialogExViewScript.Instance.ShowLoading(true);
                        Controller.OnHandleUIEvent("RequestNapThePAY2", cates[dropdownCardType.value].Name, seri, code, valueCard, 0);
                    }
                }
                break;
        }
    }

    public void ClickRefestCapCha()
    {
        DialogExViewScript.Instance.ShowLoading(true);
        //txtCapcha.text = "";
        //imgCapcha.sprite = nomarl;
        Controller.OnHandleUIEvent("RequestCapCha");
    }

    public void ClickCharging()
    {
//#if UNITY_ANDROID
//        if (!GameConfig.APPFUNCTION.IsAppCharging)
//        {
//            DialogExViewScript.Instance.ShowNotification("Chức năng đang phát triển");
//            return;
//        }
//#endif
        if (cateType == CateType.CHARGING) return;
        cateType = CateType.CHARGING;
        ChangeContentByCate();
    }


    private void ClickChargingHistory()
    {
//#if UNITY_ANDROID
//        if (!GameConfig.APPFUNCTION.IsAppCharging)
//        {
//            DialogExViewScript.Instance.ShowNotification("Chức năng đang phát triển");
//            return;
//        }
//#endif
        if (cateType == CateType.CHARGINGHISTORY) return;
        cateType = CateType.CHARGINGHISTORY;
        ChangeContentByCate();
    }
    public void ClickExChange()
    {
//#if UNITY_ANDROID
//        if (!GameConfig.APPFUNCTION.IsAppFullFunction)
//        {
//            DialogExViewScript.Instance.ShowNotification("Chức năng đang phát triển");
//            return;
//        }
//#endif
        if (cateType == CateType.EXCHANGE) return;
        cateType = CateType.EXCHANGE;
        ChangeContentByCate();
    }

    public void ClickHistoryExChange()
    {
//#if UNITY_ANDROID
//        if (!GameConfig.APPFUNCTION.IsAppFullFunction)
//        {
//            DialogExViewScript.Instance.ShowNotification("Chức năng đang phát triển");
//            return;
//        }
//#endif
        if (cateType == CateType.HISOTRYEX) return;
        cateType = CateType.HISOTRYEX;
        ChangeContentByCate();
    }

    public void ClickGOLDTOSILVER()
    {
        if (cateType == CateType.GOLDTOSILVER) return;
        cateType = CateType.GOLDTOSILVER;
        ChangeContentByCate();
    }

    public void ClickMomo()
    {
        if (cateType == CateType.MOMO) return;
        cateType = CateType.MOMO;
        ChangeContentByCate();
    }

    public void ChangeContentByCate()
    {
        //Debug.Log("ChangeContentByCate: " + cateType);
        DialogExViewScript.Instance.ShowLoading(true);
        objCharing.SetActive(cateType == CateType.CHARGING);
        objExChange.SetActive(cateType == CateType.EXCHANGE);
        objHistory.SetActive(cateType == CateType.HISOTRYEX);
        objMomo.SetActive(cateType == CateType.MOMO);
        //objExChangeGoldToSilver.SetActive(cateType == CateType.GOLDTOSILVER);
        objChargingHistory.SetActive(cateType == CateType.CHARGINGHISTORY);
        imgNapLoc.sprite = cateType == CateType.CHARGING ? sprCateSelected : sprCateUnSelected;
        imgExchange.sprite = cateType == CateType.EXCHANGE ? sprCateSelected : sprCateUnSelected;
        imgLsDT.sprite = cateType == CateType.HISOTRYEX ? sprCateSelected : sprCateUnSelected;
        imgLSNap.sprite = cateType == CateType.CHARGINGHISTORY ? sprCateSelected : sprCateUnSelected;
        imgMomo.sprite = cateType == CateType.MOMO ? sprCateSelected : sprCateUnSelected;
        //cateGoldToSilver.GetComponent<Image>().sprite = cateType == CateType.GOLDTOSILVER ? sprCateSelected : sprCateUnSelected;
        //cateChargingHistory.GetComponent<Image>().sprite = cateType == CateType.CHARGINGHISTORY ? sprCateSelected : sprCateUnSelected;

        //Debug.Log("ChangeContentByCate 1111: " + cateType);
        switch (cateType)
        {
            case CateType.CHARGING:
                //txtCapcha.text = "";
                //imgCapcha.sprite = nomarl;
                Invoke("GetInfoCard", 0.25f);
                txtTitle.text = "NẠP LỘC";
                break;
            case CateType.EXCHANGE:
                Invoke("GetTelcos", 0.1f);
                txtTitle.text = "ĐỔI QUÀ";
                break;
            case CateType.HISOTRYEX:
                Invoke("GetHistory", 0.1f);
                txtTitle.text = "LỊCH SỬ ĐỔI QUÀ";
                break;
            case CateType.GOLDTOSILVER:
                if (txtCurGold) txtCurGold.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.GOLD);
                if (txtSilver) txtSilver.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.SILVER);
                DialogExViewScript.Instance.ShowLoading(false);
                break;
            case CateType.CHARGINGHISTORY:
                Invoke("GetChargingHistory", 0.1f);
                break;
            case CateType.MOMO:
                Invoke("GetInfoMomo", 0.1f);
                break;
        }
    }


    void GetInfoCard()
    {
        Debug.Log("GetInfoCard");
        Controller.OnHandleUIEvent("RequestPAY0GetCardInfo");
        Controller.OnHandleUIEvent("RequestCapCha");
    }

    void GetTelcos()
    {
        //Debug.Log("GET TELCO");
        Controller.OnHandleUIEvent("RequestGetTelco");
    }

    void GetHistory()
    {
        Controller.OnHandleUIEvent("RequestCOU2History"); 
             Controller.OnHandleUIEvent("ClearDataHistory"); 
    }

    void GetChargingHistory()
    {
        Controller.OnHandleUIEvent("RequestChargingHistory");
        //string url = Network.Domain + string.Format(Network.KEYNAME_INGAME.LOBBY_CHARGINGHISTORY, ClientConfig.UserInfo.ID);
        //Network.Instance.RequestWWForm(url, ResponseChargingHistory, "CHARGING HISTORY", () =>
        //{
        //    DialogExViewScript.Instance.ShowLoading(false);
        //});
    }

    void GetInfoMomo()
    {
        Controller.OnHandleUIEvent("RequestInfoMomo");
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
    private void ResponseChargingHistory(object[] data)
    {
        DialogExViewScript.Instance.ShowLoading(false);
        ChargingHistory[] histories = (ChargingHistory[])data[0];
        Debug.Log("ResponseChargingHistory");
       // ChargingHistorys chargingHistorys = JsonConvert.DeserializeObject<ChargingHistorys>(data);
        if(histories != null && histories.Length > 0)
            scrollCharingHistory.InitData(histories);
        else DialogExViewScript.Instance.ShowNotification("Chưa có giao dịch.");
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
    private void ShowNotify(object[] data)
    {
        DialogExViewScript.Instance.ShowLoading(false);
        DialogExViewScript.Instance.ShowNotification((string)data[0]);
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
    private void ShowDialog(object[] data)
    {
        DialogExViewScript.Instance.ShowLoading(false);
        DialogExViewScript.Instance.ShowDialog((string)data[0]);
    }

    CateCharging[] cates;

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
    private void ShowDataCardInfo(object[] data)
    {
        DialogExViewScript.Instance.ShowLoading(false);
        ResetCardRate();
        cates = (CateCharging[])data[0];
        for(int i = 0; i < cates.Length; i++)
        {
            Debug.Log("CateName: " + cates[i].Name + " - cateType:  " + cates[i].Type);
        }
        if (cates != null)
        {
            rateCharging = (float)data[1];
            if (cates != null && cates.Length > 0)
            {
                InitDataCardRate(cates[0].Amounts, rateCharging);
                //tranDropDownCardType.anchoredPosition = new Vector2(0, 75 * cates.Length);
                tranDropDownCardType.sizeDelta = new Vector2(0, 75 * cates.Length);
                dropdownCardType.options = new List<Dropdown.OptionData>();
                for (int i = 0; i < cates.Length; i++)
                {
                    dropdownCardType.options.Add(new Dropdown.OptionData(cates[i].Name));
                    //tranCardType.Find(cates[i].Name.ToLower()).gameObject.SetActive(true);
                }
                dropdownCardType.value = 1;
                dropdownCardType.value = 0;
                dropdownValueCard.options = new List<Dropdown.OptionData>();

                for (int i = 0; i < cates[0].Amounts.Length; i++)
                {
                    dropdownValueCard.options.Add(new Dropdown.OptionData(cates[0].Amounts[i].ToString()));
                }
                listCardValue = cates[0].Amounts;
                valueCard = cates[0].Amounts[0];
                dropdownValueCard.value = 1;
                dropdownValueCard.value = 0;
            }
        }
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
    private void ClearData(object[] data)
    {
        inputCode.text = inputSeri.text = "";
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
    private void ShowCapcha(object[] param)
    {
        string url = (string)param[0];
        //txtCapcha.text = url;
        StartCoroutine(LoadCapcha(url));
    }



    private IEnumerator LoadCapcha(string url)
    {
        if (!string.IsNullOrEmpty(url))
        {
            WWW www = new WWW(url);
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                if (imgCapcha != null)
                {
                    imgCapcha.sprite = Sprite.Create(www.texture, new Rect(0, 0, 155, 80), Vector2.zero);
                }
            }
            else DialogExViewScript.Instance.ShowNotification("Tải Capcha thất bại.");
        }
        DialogExViewScript.Instance.ShowLoading(false);
        yield return null;
    }

    //------------------------------------ DOI THUONG--------
    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
    private void ShowTelcos(object[] param)
    {
        try
        {
            TelcoDetail[] telcoDetails = (TelcoDetail[])param[0];
            float rate = (float)param[1];
            for (int i = 0; i < lstCardRatesExchange.Length; i++) lstCardRatesExchange[i].gameObject.SetActive(false);

            for(int i = 0; i < lstPricesExchange.Length; i++)
            {
                lstCardRatesExchange[i].gameObject.SetActive(true);
                lstCardRatesExchange[i].ShowData(lstPricesExchange[i], rate);
            }

            //Debug.Log("RATE: " + rate);
            //txtTileDoiTHuong[0].text = string.Format(". Thẻ 20K cần {0} Lộc", MoneyHelper.FormatRelativelyWithoutUnit((long)Math.Round(20000 + 20000 * rate)));
            //txtTileDoiTHuong[0].text = string.Format(". Thẻ 50K cần {0} Lộc", MoneyHelper.FormatRelativelyWithoutUnit((long)Math.Round(50000 + 50000 * rate)));
            //txtTileDoiTHuong[1].text = string.Format(". Thẻ 100K cần {0} Lộc", MoneyHelper.FormatRelativelyWithoutUnit((long)Math.Round(100000 + 100000 * rate)));
            //txtTileDoiTHuong[2].text = string.Format(". Thẻ 200K cần {0} Lộc", MoneyHelper.FormatRelativelyWithoutUnit((long)Math.Round(200000 + 200000 * rate)));
            //txtTileDoiTHuong[3].text = string.Format(". Thẻ 500K cần {0} Lộc", MoneyHelper.FormatRelativelyWithoutUnit((long)Math.Round(500000 + 500000 * rate)));
            if (telcoDetails != null && telcoDetails.Length > 0)
            {
                for (int i = 0; i < telcoDetails.Length; i++)
                {
                    //Debug.Log("ShowTelcos ------------- : " + telcoDetails[i].TelcoName + " - " + telcoDetails[i].TelcoId);
                    var telco = Telecos.Where(t => t.name.Equals(telcoDetails[i].TelcoId)).FirstOrDefault();
                    if (telco)
                    {
                        //Debug.Log("ShowTelcos ------------- : " + telcoDetails[i].TelcoId);
                        if (mappingSprTelco.ContainsKey(telcoDetails[i].TelcoId))
                        {
                            telco.InitData(telcoDetails[i].TelcoName, mappingSprTelco[telcoDetails[i].TelcoId], telcoDetails[i].TelcoId, telcoDetails[i].Items, (float)param[1]);
                            telco.EventClickItem = null;
                            telco.EventClickItem += RequestExChange;
                        }
                        continue;
                    }
                }
            }
            DialogExViewScript.Instance.ShowLoading(false);
        }
        catch (Exception ex)
        {
            Debug.LogError("Exception ShowTelcos: " + ex.Message);
            Debug.LogError("Exception ShowTelcos - StackTrace : " + ex.StackTrace);
        }
    }

    private void ResetCardRate()
    {
        for(int i = 0; i < lstCardRates.Length; i++)
        {
            lstCardRates[i].gameObject.SetActive(false);
        }
    }

    private void InitDataCardRate(int[] cards, float rate)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            lstCardRates[i].gameObject.SetActive(true);
            lstCardRates[i].ShowData(cards[i], rate);
        }
    }


    private void RequestExChange(string id, int index)
    {
        Controller.OnHandleUIEvent("RequestExChangeItem", id, index);
    }

    //---------------------------------------- LICH SU DOI THUONG------------------
    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
    private void ShowListHistory(object[] param)
    {
       // Debug.Log("ShowListHistory");
        DialogExViewScript.Instance.ShowLoading(false);
        CashoutHistory[] CashoutHistorys = (CashoutHistory[])param[0];
        int length = CashoutHistorys.Length;
        //Debug.Log("ShowListHistory -- " + length);
        scrollerHistory.dlgCharing = null;
        scrollerHistory.dlgCharing += RequestAutoNapthe;
        scrollerHistory.dlgGetDetail = null;
        scrollerHistory.dlgGetDetail += RequestCardDetail;
        scrollerHistory.InitData(CashoutHistorys);
    }


    private void RequestAutoNapthe(int transaction)
    {
        DialogExViewScript.Instance.ShowLoading(true);
        Controller.OnHandleUIEvent("RequestNapLaiCOU4", transaction);
    }
    
    private void RequestCardDetail(int transaction, string id, string name, string seri, string code, int amount)
    {
        Id = id;
        ShowChiTietThe(name, seri, code, transaction, amount);
        if (string.IsNullOrEmpty(seri) && string.IsNullOrEmpty(code))
        {
            DialogExViewScript.Instance.ShowLoading(true);
            Controller.OnHandleUIEvent("RequestGetCardDetailCOU3", transaction);
        }
    }
    //------------------- ĐỔi vàng sang bạc

    private void ConvertToChipBac(string value)
    {
        if (string.IsNullOrEmpty(value)) txtChipBacReceived.text = "0";
        else txtChipBacReceived.text = MoneyHelper.FormatNumberAbsolute(long.Parse(value) * 100);
    }
    private void ClickChangeGoldToSilver()
    {
        string cash = inputChipExChange.text;
        if (string.IsNullOrEmpty(cash))
        {
            DialogExViewScript.Instance.ShowDialog("Chưa nhập số Lộc cần đổi");
            return;
        }
        DialogExViewScript.Instance.ShowLoading(true);
        Controller.OnHandleUIEvent("RequestChangeGoldToSilver", long.Parse(cash));
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
    private void ExChangeGoldToSilverSuccess(object[] param)
    {
        DialogExViewScript.Instance.ShowLoading(false);
        if (cateType == CateType.GOLDTOSILVER)
        {
            if (txtCurGold) txtCurGold.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.GOLD);
            if (txtSilver) txtSilver.text = MoneyHelper.FormatNumberAbsolute(ClientConfig.UserInfo.SILVER);
        }
    }

    private int transaction, amount;
    private string Id;
    // Chi tiết thẻ
    public void ShowChiTietThe(string name, string seri, string mathe, int _transaction, int _amount)
    {
        //Debug.Log("Show Chi Tiet The: " + name + " , seri: " + seri + " , ma the: " + mathe);
        ObjDetailCard.gameObject.SetActive(true);
        txtCardName.text = name;
        txtSeriCard.text = string.Format("Seri: {0}", seri);
        txtMaThe.text = string.Format("Mã thẻ: {0}", mathe);
        transaction = _transaction;
        amount = _amount;
        if (string.IsNullOrEmpty(seri) && string.IsNullOrEmpty(mathe)) return;
        btnNapTheDetail.onClick.RemoveAllListeners();
        btnNapTheDetail.onClick.AddListener(() =>
        {
            Controller.OnHandleUIEvent("RequestNapThePAY2", Id, seri,mathe, amount, transaction);
        });
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
    private void ShowCardDetail(object[] param)
    {
        Debug.Log("NAP THE DETAIL");
        DialogExViewScript.Instance.ShowLoading(false);
        txtSeriCard.text = string.Format("Seri: {0}", (string)param[0]);
        txtMaThe.text = string.Format("Mã thẻ: {0}", (string)param[1]);
        btnNapTheDetail.onClick.RemoveAllListeners();
        btnNapTheDetail.onClick.AddListener(() =>
        {
            Debug.Log("NAP THE DETAIL");
            Controller.OnHandleUIEvent("RequestNapThePAY2", Id, (string)param[0], (string)param[1], 0, transaction);
        });
    }

    private void CloseDetailClick()
    {
        ObjDetailCard.gameObject.SetActive(false);
    }

    /// MOMO
    /// 
    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
    private void UpdateMomoInfo(object[] param)
    {
        DialogExViewScript.Instance.ShowLoading(false);
        if (txtNumberPhone) txtNumberPhone.text = (string)param[0];
        if (txtUserHolder) txtUserHolder.text = (string)param[1];
        float rate = (float)param[2];
        if (txtNickName) txtNickName.text = (string)param[3];
        if (txtTitleRateMomo) txtTitleRateMomo.text = string.Format("NẠP MOMO KHUYẾN MÃI {0}%", Mathf.RoundToInt(rate - 1) * 100);
        for (int i = 0; i < lstCardRatesMomo.Length; i++)
        {
            lstCardRatesMomo[i].ShowData(pricesMomo[i], rate);
        }
    }

    public void CopyStkMomo()
    {
        string phone = txtNumberPhone.text;
        if (string.IsNullOrEmpty(phone)) return;
        Clipboard.SetText(phone);
        objCopySuccess.SetActive(true);
        if (coroutineCoppy != null) StopCoroutine(coroutineCoppy);
        coroutineCoppy = StartCoroutine(HideCoppy());
    }

    public void CopyDescriptionMomo()
    {
        string des = txtNickName.text;
        if (string.IsNullOrEmpty(des)) return;
        Clipboard.SetText(des);
        objCopySuccess.SetActive(true);
        if (coroutineCoppy != null) StopCoroutine(coroutineCoppy);
        coroutineCoppy = StartCoroutine(HideCoppy());
    }

    Coroutine coroutineCoppy;

    IEnumerator HideCoppy()
    {
        yield return new WaitForSeconds(3f);
        objCopySuccess.SetActive(false);
    }
}
