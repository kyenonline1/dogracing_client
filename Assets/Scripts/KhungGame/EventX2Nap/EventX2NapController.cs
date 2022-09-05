using AppConfig;
using CoreBase.Controller;
using GameProtocol.EVN;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventX2NapController : UIController
{
    public EventX2NapController(IView view) : base(view)
    {
    }

    public override void StartController()
    {
        base.StartController();
        RequestFirstCharging(null);
    }

    public override void StopController()
    {
        base.StopController();
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
    private void RequestFirstCharging(object[] param)
    {
        EVN6FirstChargingRequest request = new EVN6FirstChargingRequest();
        DataListener dataListener = new DataListener(HandlerResponseEVN6FirstCharging);
        Network.Network.SendOperation(request, dataListener);
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
    private void RequestReceiving(object[] param)
    {
        EVN7FirstChargingReceiverRequest request = new EVN7FirstChargingReceiverRequest();
        DataListener dataListener = new DataListener(HandlerResponseEVN7Receiving);
        Network.Network.SendOperation(request, dataListener);
    }

    private IEnumerator HandlerResponseEVN6FirstCharging(string coderun, Dictionary<byte, object> data)
    {
        EVN6FirstChargingResponse response = new EVN6FirstChargingResponse(data);
        if(response.ErrorCode != 0)
        {
            View.OnUpdateView("ClosePopupFirstCharging");
            yield break;
        }

        View.OnUpdateView("UpdateInfoFirstCharging", new object[] { response.Events });

        yield return null;
    }

    private IEnumerator HandlerResponseEVN7Receiving(string coderun, Dictionary<byte, object> data)
    {
        EVN7FirstChargingReceiverResponse response = new EVN7FirstChargingReceiverResponse(data);
        View.OnUpdateView("ShowError", response.ErrorMsg);
        if (response.ErrorCode != 0)
        {
            yield break;
        }

        ClientConfig.UserInfo.GOLD = response.CurrentRuby;
        Base.Utils.EventManager.Instance.RaiseEventInTopic(Base.Utils.EventManager.CHANGE_BALANCE);
        yield return null;
    }
}
