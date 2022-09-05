using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.DialogEx;

public class ScurityViewScript : MonoBehaviour {

    public delegate void ChangeNumberPhone(string phone, int otp);
    public ChangeNumberPhone dlgChangeNumberPhone;

    public Action actionGetOTP;

    public InputField inputNumberPhone,
        inputOTP;

    public UIMyButton btnGetOTP,
        btnChangeNumberPhone;


    private void Awake()
    {
        btnGetOTP._onClick.AddListener(ClickGetOTP);
        btnChangeNumberPhone._onClick.AddListener(ClickChangeNumberPhone);
    }

    private void ClickGetOTP()
    {
        if (actionGetOTP != null) actionGetOTP();
    }

    private void ClickChangeNumberPhone()
    {
        string phone = inputNumberPhone.text;
        if (string.IsNullOrEmpty(phone))
        {
            DialogExViewScript.Instance.ShowNotification("Vui lòng nhập số điện thoại.");
            return;
        }

        string otp = inputOTP.text;
        if (string.IsNullOrEmpty(otp))
        {
            DialogExViewScript.Instance.ShowNotification("Vui lòng nhập mã OTP.");
            return;
        }

        if(dlgChangeNumberPhone != null)
        {
            dlgChangeNumberPhone(phone, int.Parse(otp));
        }
    }

}
