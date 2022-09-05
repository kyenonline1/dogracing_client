using AppConfig;
using Base.Utils;
using CoreBase.Controller;
using GameProtocol.DIS;
using GameProtocol.OTP;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitis.Command;

public class DaiLyController : UIController
{
    public DaiLyController(IView view) : base(view)
    {
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
    private void RequestListDaiLy(object[] param)
    {
        DataListener dataListener = new DataListener(ResponseListDaiLy);
        DIS0_Request request = new DIS0_Request();
        Network.Network.SendOperation(request, dataListener);
    }

   
    private IEnumerator ResponseListDaiLy(string coderun, Dictionary<byte, object> data)
    {
        DIS0_Response response = new DIS0_Response(data);
        if (response.ErrorCode != 0)
        {
            View.OnUpdateView("ShowError", response.ErrorMsg);
            yield break;
        }
        View.OnUpdateView("CloseLoading");
        View.OnUpdateView("ShowListDaiLy", new object[] { response.Data, response.Rate });
        yield return null;
    }

    private IEnumerator ResponseTranferCoin(string coderun, Dictionary<byte, object> data)
    {
        DIS1_Response response = new DIS1_Response(data);
        if (response.ErrorCode != 0)
        {
            yield break;
        }
        View.OnUpdateView("CloseLoading");
        ClientConfig.UserInfo.GOLD = response.CurrentGold;
        EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_BALANCE);
        yield return null;
    }

    private IEnumerator HandlerResponseOTP(string coderun, Dictionary<byte, object> data)
    {
        OTP0_Response response = new OTP0_Response(data);
        if (response.ErrorCode != 0)
        {
            View.OnUpdateView("ShowError", response.ErrorMsg);
            yield break;
        }
        View.OnUpdateView("CloseLoading");

        yield return null;
    }



}
