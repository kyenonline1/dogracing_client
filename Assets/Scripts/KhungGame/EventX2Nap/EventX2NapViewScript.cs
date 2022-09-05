using CoreBase;
using CoreBase.Controller;
using GameProtocol.EVN;
using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View.DialogEx;

public class EventX2NapViewScript : ViewScript
{
    [SerializeField]
    private UIMyButton btnNapThe,
        btnNapMomo,
        btnNapDaiLy;
    [SerializeField] private ItemFirstChargingView[] itemsFristCharging;
    protected override void InitGameScriptInAwake()
    {
        base.InitGameScriptInAwake();
        btnNapThe._onClick.AddListener(OnBtnNapTheClicked);
        btnNapMomo._onClick.AddListener(OnBtnNapMomoClicked);
        btnNapDaiLy._onClick.AddListener(OnBtnNapMomoClicked);
    }

    protected override void InitGameScriptInStart()
    {
        base.InitGameScriptInStart();
    }

    protected override IController CreateController()
    {
        return new EventX2NapController(this);
    }


    private void OnEnable()
    {
        for (int i = 0; i < itemsFristCharging.Length; i++) itemsFristCharging[i].gameObject.SetActive(false);
        if (DialogExViewScript.Instance) DialogExViewScript.Instance.ShowLoading(true);
        if (Controller != null) Controller.OnHandleUIEvent("RequestFirstCharging");
    }

    private void OnBtnNapTheClicked()
    {
        gameObject.SetActive(false);
        if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowNapThe();
    }

    private void OnBtnNapMomoClicked()
    {
        gameObject.SetActive(false);
        if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowNapMomo();
    }

    private void OnBtnNapDaiLyClicked()
    {
        gameObject.SetActive(false);
        //if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowNapMomo();
    }


    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]  
    private void ClosePopupFirstCharging(object[] param)
    {
        gameObject.SetActive(false);
        if (DialogExViewScript.Instance)
        {
            DialogExViewScript.Instance.ShowLoading(false);
            DialogExViewScript.Instance.ShowNotification("Sự kiện đã kết thúc.");
        }
    }

    [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_UPDATEUI_HANDLER)]
    private void UpdateInfoFirstCharging(object[] param)
    {
        DialogExViewScript.Instance.ShowLoading(false);
        FirstCharging[] firstChargings = (FirstCharging[])param[0];
        if(firstChargings != null)
        {
            for(int i = 0; i < itemsFristCharging.Length; i++)
            {
                for(int j = 0; j < firstChargings.Length; j++)
                {
                    if (itemsFristCharging[i].CateName.Equals(firstChargings[j].Cate))
                    {
                        itemsFristCharging[i].gameObject.SetActive(true);
                        itemsFristCharging[i].InitData(firstChargings[j].Status);
                        itemsFristCharging[i].callback = null;
                        itemsFristCharging[i].callback = () =>
                        {
                            int id = firstChargings[j].Id;
                            OnClickReceiving(id);
                        };
                    }
                }
            }
        }
        else
        {
            gameObject.SetActive(false);
            DialogExViewScript.Instance.ShowNotification("Sự kiện đã kết thúc.");
        }
    }

    private void OnClickReceiving(int id)
    {
        DialogExViewScript.Instance.ShowLoading(true);
        Controller.OnHandleUIEvent("RequestReceiving", id);
    }
}
