using AppConfig;
using CoreBase;
using CoreBase.Controller;
using GameProtocol.DIS;
using Interface;
using PathologicalGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;
using View.DialogEx;
using View.Home.Shop;

public class DaiLyViewScript : ViewScript
{
    [SerializeField] private ShopManagerView shop;
    [SerializeField] private TranferViewScript tranfer;

    [SerializeField]
    private Transform tranContent;

    private Distributor[] agencys;
    private float Rate;

    // Use this for initialization

    protected override void InitGameScriptInAwake()
    {
        base.InitGameScriptInAwake();
        //FakeAgengy();
        callbackOpenPopup = RequestListDaiLy;
    }
    protected override void InitGameScriptInStart()
    {
        base.InitGameScriptInStart();
    }

    protected override IController CreateController()
    {
        return new DaiLyController(this);
    }

    private void OnEnable()
    {
        OpenPopup();
    }

    private void OnDisable()
    {
        RecyclePool();
    }


    private void RequestListDaiLy()
    {
        if (agencys != null)
        {
            ShowItemDaiLy();
            return;
        }
        Controller.OnHandleUIEvent("RequestListDaiLy");
    }

    protected override void DestroyGameScript()
    {
        base.DestroyGameScript();
    }


    //public void ConvertMoneyInput(string value)
    //{
    //    //string strmn = value.Replace(".", "");
    //    if (value.Contains(".")) return;
    //    if (!string.IsNullOrEmpty(value))
    //    {
    //        long money = long.Parse(value);
    //        Debug.Log("Money:" + money + " - " + MoneyHelper.FormatNumberAbsolute(money));
    //        inputVuiCoin.text = MoneyHelper.FormatNumberAbsolute(money);
    //    }
    //}

    //public void ConvertMoneyReInput(string value)
    //{
    //    if (value.Contains(".")) return;
    //    //string strmn = value.Replace(".", "");
    //    if (!string.IsNullOrEmpty(value))
    //    {
    //        long money = long.Parse(value);
    //        reinputVuiCoin.text = MoneyHelper.FormatNumberAbsolute(money);
    //    }
    //}

    //public void ClickConfirmTranfer()
    //{
    //    string money = inputVuiCoin.text;
    //    string remoney = reinputVuiCoin.text;
    //    string description = inputDescription.text;

    //    if (string.IsNullOrEmpty(money))
    //    {
    //        DialogExViewScript.Instance.ShowNotification("Chưa nhập số tiền.");
    //       // DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("DAYLY_INPUTMONEY"));
    //        return;
    //    }
    //    if (string.IsNullOrEmpty(remoney))
    //    {

    //        DialogExViewScript.Instance.ShowNotification("Chưa nhập lại số tiền.");
    //        // DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("DAYLY_INPUTREMONEY"));
    //        return;
    //    }
    //    if (string.IsNullOrEmpty(description))
    //    {
    //        DialogExViewScript.Instance.ShowNotification("Chưa nhập mô tả.");
    //        //DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("DAYLY_INPUTDESCRIPTION"));
    //        return;
    //    }

    //    int _mn = int.Parse(money);
    //    long _reMn = long.Parse(remoney);
    //    if (_mn != _reMn)
    //    {
    //        DialogExViewScript.Instance.ShowNotification("Số tiền không khớp.");
    //        // DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("DAYLY_MONEYOTHER"));
    //        return;
    //    }
    //    if (_mn > ClientConfig.UserInfo.GOLD)
    //    {
    //        DialogExViewScript.Instance.ShowFullPopup("Số dư không đủ, vui lòng nạp thêm.", ()=> {
    //            tranTranfer.gameObject.SetActive(false);
    //            CloseDaiLy();
    //        }, OkLabel: "Nạp Lộc");
    //        // DialogExViewScript.Instance.ShowNotification(Languages.Language.GetKey("TX_NOTENOUNGHMONEY"));
    //        return;
    //    }
    //    DialogExViewScript.Instance.ShowLoading(true);
    //    Controller.OnHandleUIEvent("RequestTranferCoin", _mn, IDDaily, description);
    //}

    [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
    private void ShowError(object[] param)
    {
        DialogExViewScript.Instance.ShowLoading(false);
        DialogExViewScript.Instance.ShowNotification((string)param[0]);
    }

    [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
    private void ShowListDaiLy(object[] param)
    {
        agencys = (Distributor[])param[0];
        Rate = (float)param[1];
        if (agencys != null)
        {
            ShowItemDaiLy();
        }
    }

    [HandleUIEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
    private void CloseLoading(object[] param)
    {
        DialogExViewScript.Instance.ShowLoading(false);
    }


    void ShowItemDaiLy()
    {
        //var DaiLy = Shuffle(agencys);
        for (int i = 0; i < agencys.Length; i++)
        {

            var go = PoolManager.Pools["AgencyBig"].Spawn("ItemAgencyBig", Vector3.zero, Quaternion.identity, tranContent);

            ItemDaiLyView itemDaiLyView = go.GetComponent<ItemDaiLyView>();
            if (itemDaiLyView)
            {
                itemDaiLyView.gameObject.SetActive(true);
                itemDaiLyView.SetData(i, agencys[i].DistributorName, agencys[i].Phone, agencys[i].Nickname, agencys[i].FacebookUrl,
                     agencys[i].Zalo, agencys[i].Telegram);
                itemDaiLyView.dlgTranfer = null;
                itemDaiLyView.dlgTranfer += DlgShowTranfer;
            }
            else PoolManager.Pools["AgencyBig"].Despawn(go.transform);
        }
    }

    Distributor[] Shuffle(Distributor[] arrDL)
    {
        int m = arrDL.Length;
        var random = new System.Random();
        //var temp = new List<Distributor>();
        for (int i = arrDL.Length; i > 1; i--)
        {
            // Pick random element to swap.
            int j = random.Next(i); // 0 <= j <= i-1
                                    // Swap.
            Distributor tmp = arrDL[j];
            arrDL[j] = arrDL[i - 1];
            arrDL[i - 1] = tmp;
        }
        return arrDL;
    }

    public void DlgShowTranfer(string name, string nickname)
    {
        if (shop) shop.OpenByCate(CateShop.TRANFER);
        if (tranfer) tranfer.ShowUserTranfer(name, Rate, nickname);
    }

    private void RecyclePool()
    {
        PoolManager.Pools["AgencyBig"].DespawnAll();
    }

    private void FakeAgengy()
    {

        List<string> lstName = new List<string>()
            {
                "Vip Poker Master",
                "Đại Lý Master",
                "Đại lý thần tốc",
                "Hệ thống thần tốc",
                "Tuấn Sài Gòn",
                "Hưng Hà Nội",
                "Vip Toàn Quốc",
                "Vip Đại Lý PokerMaster",
                "Bình Gold",
                "Tập đoàn Hưng Thịnh",
            };

        List<string> lstNickName = new List<string>()
            {
                "pokermaster",
                "dailymaster",
                "dailythantoc",
                "thantoc30s",
                "tuansaigon",
                "hunghanoi",
                "viptoanquoc",
                "msminhtrang",
                "binhgold",
                "tapdoanhungthinh",
            };

        List<string> sdt = new List<string>()
            {
                "094999999",
                "091111111",
                "094222222",
                "096666666",
                "094333333",
                "094444444",
                "094555555",
                "094666666",
                "094777777",
                "094888888",
            };

        List<Distributor> lsDL = new List<Distributor>();

        for (int i = 0; i < 10; i++)
        {
            var dl = new Distributor();
            dl.Nickname = lstNickName[i];
            dl.DistributorName = lstName[i];
            dl.Phone = sdt[i];
            dl.FacebookUrl = "https://www.24h.com.vn/";
            dl.Zalo = "https://www.24h.com.vn/";
            dl.Telegram = "https://www.24h.com.vn/";
            lsDL.Add(dl);
        }
        agencys = lsDL.ToArray();
    }
}

public class DaiLyBui99
{
    public int ID;
    public string Name;
    public string Address;
    public string Facebook;
    public string Phone;

    public DaiLyBui99(int id, string name, string phone, string address, string facebook)
    {
        ID = id;
        Name = name;
        Phone = phone;
        Address = address;
        Facebook = facebook;
    }
}
