using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemFirstChargingView : MonoBehaviour
{
    [SerializeField] private string cateName;

    [SerializeField] private GameObject objNap;
    [SerializeField] private GameObject objReceiving;
    [SerializeField] private GameObject objReceived;

    public UnityAction callback;

    public string CateName
    {
        get
        {
            return cateName;
        }
    }

    public void InitData(byte status)
    {
        if (objNap) objNap.SetActive(status == 0);
        if (objReceiving) objReceiving.SetActive(status == 1);
        if (objReceived) objReceived.SetActive(status == 2);
    }

    public void OnClickReceiving()
    {
        if (callback != null) callback();
    }
}
