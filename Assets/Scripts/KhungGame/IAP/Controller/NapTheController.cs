using AppConfig;
using Base.Utils;
using CoreBase.Controller;
using GameProtocol.ACC;
using GameProtocol.ATH;
using GameProtocol.COU;
using GameProtocol.MOM;
using GameProtocol.PAY;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;
using Utilitis.Command;

public class NapTheController : UIController {

    private TelcoDetail[] telcoDetails;
    private float Rate;

    public override void StartController()
    {
        base.StartController();
    }

    public override void StopController()
    {
        base.StopController();
    }


    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
    private void RequestPAY0GetCardInfo(object[] data)
    {
        PAY0_Request request = new PAY0_Request()
        {
            Cate = 2
        };
        DataListener dataListener = new DataListener(HandlerResponsePAY0CardInfo);
        //LogMng.Log("NAPTHE", "Request Card Info");
        Network.Network.SendOperation(request, dataListener);
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
    private void RequestCapCha(object[] data)
    {
        ATH6_Request request = new ATH6_Request();
        DataListener dataListener = new DataListener(HandlerResponseATH6CapCha);
        Network.Network.SendOperation(request, dataListener);
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
    private void RequestNapThePAY2(object[] data)
    {
        string type = (string)data[0];
        PAY2_Request request = new PAY2_Request()
        {
            Type = string.IsNullOrEmpty(type) ? "viettel" : (string)data[0],
            Seri = (string)data[1],
            CardNumber = (string)data[2],
            Amount = (int)data[3],
            TransId = (int)data[4],
        };
        DataListener dataListener = new DataListener(HandlerResponsePAY2NapThe);
        Network.Network.SendOperation(request, dataListener);
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
    private void RequestNapLaiCOU4(object[] param)
    {
        COU4ReChargingRequest request = new COU4ReChargingRequest()
        {
            TransactionId = (int)param[0]
        };
        DataListener dataListener = new DataListener(HandlerResponseCOU4);
        Network.Network.SendOperation(request, dataListener);
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
    private void RequestGetCardDetailCOU3(object[] param)
    {
        COU3ReceiveCashoutRequest request = new COU3ReceiveCashoutRequest()
        {
            TransactionId = (int)param[0]
        };
        DataListener dataListener = new DataListener(HandlerResponseCOU3);
        Network.Network.SendOperation(request, dataListener);
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
    private void RequestGetTelco(object[] data)
    {
        if (telcoDetails != null)
        {
            View.OnUpdateView("ShowTelcos", new object[] { telcoDetails, Rate });
            return;
        }
        COU0_Request request = new COU0_Request();

        DataListener dataListener = new DataListener(HandlerResponseCOU0_GetTelcos);
        Network.Network.SendOperation(request, dataListener);
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
    private void RequestGetListItem(object[] param)
    {
        COU0_Request request = new COU0_Request();
        DataListener dataListener = new DataListener(HandlerResponseCOU0_GetTelcos);
        Network.Network.SendOperation(request, dataListener);
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
    private void RequestExChangeItem(object[] param)
    {
        COU1CashoutRequest request = new COU1CashoutRequest()
        {
            //ItemID = (string)param[0],
            //Price = (int)param[1]
        };
        DataListener dataListener = new DataListener(HandlerResponseCOU1_ExChange);
        Network.Network.SendOperation(request, dataListener);
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
    private void RequestCOU2History(object[] param)
    {
        COU2HisotiesCashoutRequest request = new COU2HisotiesCashoutRequest();
        DataListener dataListener = new DataListener(HandlerResponseCOU2History);
        Network.Network.SendOperation(request, dataListener);
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
    private void ClearDataHistory(object[] param)
    {
        if (datasHistory != null) datasHistory = null;
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
    private void RequestChargingHistory(object[] param)
    {
        PAY5_Request request = new PAY5_Request();
        DataListener dataListener = new DataListener(HandlerResponsePAY5ChargingHistory);
        Network.Network.SendOperation(request, dataListener);
    }



    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
    private void RequestInfoMomo(object[] param)
    {
        MOM0_Request request = new MOM0_Request();
        DataListener dataListener = new DataListener(HandlerResponseMOM0_MomoInfo);
        Network.Network.SendOperation(request, dataListener);
    }


    private IEnumerator HandlerResponsePAY0CardInfo(string coderun, Dictionary<byte, object> data)
    {
        PAY0_Response response = new PAY0_Response(data);
        if (response.ErrorCode != 0)
        {
            View.OnUpdateView("ShowNotify", response.ErrorMsg);
            yield break;
        }
        View.OnUpdateView("ShowDataCardInfo", new object[] { response.Cates, response.CardRate });
        yield return null;
    }

    private IEnumerator HandlerResponseATH6CapCha(string coderun, Dictionary<byte, object> data)
    {
        ATH6_Response response = new ATH6_Response(data);
        if (response.ErrorCode != 0)
        {
            View.OnUpdateView("ShowNotify", response.ErrorMsg);
            yield break;
        }
        View.OnUpdateView("ShowCapcha", response.Url);
        yield return null;
    }

    private IEnumerator HandlerResponsePAY2NapThe(string coderun, Dictionary<byte, object> data)
    {
        PAY2_Response response = new PAY2_Response(data);
        if (response.ErrorCode != 0)
        {
            View.OnUpdateView("ShowNotify", response.ErrorMsg);
            yield break;
        }
        if (response.Gold > 0)
        {
            ClientConfig.UserInfo.GOLD = response.Gold;
            Base.Utils.EventManager.Instance.RaiseEventInTopic(Base.Utils.EventManager.CHANGE_BALANCE);
        }
        View.OnUpdateView("ShowDialog", response.ErrorMsg);
        View.OnUpdateView("ClearData");
        yield return null;
    }

    private IEnumerator HandlerResponseCOU0_GetTelcos(string coderun, Dictionary<byte, object> data)
    {
        COU0_Response response = new COU0_Response(data);
        if (response.ErrorCode != 0)
        {
            View.OnUpdateView("ShowNotify", response.ErrorMsg);
            yield break;
        }
        telcoDetails = response.telcoDetails;
        Rate = response.Rate;
        View.OnUpdateView("ShowTelcos", new object[] { telcoDetails, Rate });
        yield return null;
    }

    private IEnumerator HandlerResponseCOU1_ExChange(string coderun, Dictionary<byte, object> data)
    {
        COU1CashoutResponse response = new COU1CashoutResponse(data);
        if (response.ErrorCode != 0)
        {
            View.OnUpdateView("ShowNotify", response.ErrorMsg);
            //View.OnUpdateView("ExChangeError");
            yield break;
        }
        View.OnUpdateView("ShowNotify", response.ErrorMsg);
        //View.OnUpdateView("ExChangeSuccess");
        ClientConfig.UserInfo.GOLD = response.Gold;
        //Broadcast.BroadcastReceiver.Instance.BroadcastMessage(Broadcast.MessageCode.APP, Broadcast.MessageType.CashChanged, null);
        EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_BALANCE);
        yield return null;
    }



    CashoutHistory[] datasHistory;
    private byte page;
    private long senderId;

    public NapTheController(IView view) : base(view)
    {
    }

    private IEnumerator HandlerResponseCOU2History(string coderun, Dictionary<byte, object> data)
    {
        COU2HistoriesCashoutResponse response = new COU2HistoriesCashoutResponse(data);
        if (response.ErrorCode != 0)
        {
            View.OnUpdateView("ShowNotify", response.ErrorMsg);
            yield break;
        }
        View.OnUpdateView("ShowListHistory", new object[] { response.Histories });
        yield return null;
    }

    private IEnumerator HandlerResponseCOU3(string coderun, Dictionary<byte, object> data)
    {
        COU3ReceiveCashoutResponse response = new COU3ReceiveCashoutResponse(data);
        //Debug.Log("HandlerResponseCOU3");
        if (response.ErrorCode != 0)
        {
            View.OnUpdateView("ShowNotify", response.ErrorMsg);
            yield break;
        }
        //Debug.LogFormat("HandlerResponseCOU3 {0} - {1} ", response.Seri, response.NumberCard);
        View.OnUpdateView("ShowCardDetail", response.Seri, response.NumberCard);
        yield return null;
    }

    private IEnumerator HandlerResponseCOU4(string coderun, Dictionary<byte, object> data)
    {
        COU4RechargingResponse response = new COU4RechargingResponse(data);
        View.OnUpdateView("ShowNotify", response.ErrorMsg);
        if (response.ErrorCode != 0)
        {
            yield break;
        }
        ClientConfig.UserInfo.GOLD = response.Gold;
        EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_BALANCE);
        yield return null;
    }


    private IEnumerator HandlerResponsePAY5ChargingHistory(string coderun, Dictionary<byte, object> data)
    {
        PAY5_Response response = new PAY5_Response(data);
        if (response.ErrorCode != 0)
        {
            View.OnUpdateView("ShowNotify", response.ErrorMsg);
            yield break;
        }
        View.OnUpdateView("ResponseChargingHistory", new object[] { response.Histories });
        yield return null;
    }

    private IEnumerator HandlerResponseMOM0_MomoInfo(string coderun, Dictionary<byte, object> data)
    {
        MOM0_Response response = new MOM0_Response(data);
        if (response.ErrorCode != 0)
        {
            View.OnUpdateView("ShowNotify", response.ErrorMsg);
            yield break;
        }
        View.OnUpdateView("UpdateMomoInfo", new object[] { response.NumberPhone, response.UserName, response.Rate, response.Content });
        yield return null;
    }

    //private void HandlerResponseChangeGoldToSilver(byte[] data)
    //{
    //    ACC5_Response response = data.DeserializeFromBytes<ACC5_Response>();
    //    if (response.ErrorCode != 0)
    //    {
    //        UpdateToView("ShowNotify", response.ErrorMsg);
    //        return;
    //    }
    //    ClientConfig.UserInfo.GOLD = response.Gold;
    //    ClientConfig.UserInfo.SILVER = response.Silver;
    //    EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_BALANCE);
    //    UpdateToView("ExChangeGoldToSilverSuccess");
    //}
}
