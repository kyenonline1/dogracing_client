using AppConfig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;

public class ConvertMoneyToInputField : MonoBehaviour
{

    public InputField tbValue;
    public Text txtValue;
    [SerializeField]
    private string keyValueDefault;
    [SerializeField]
    private bool isUnLimited;
    [SerializeField]
    private Color colorDefault;
    [SerializeField]
    private Color colorActive;


    private void Awake()
    {
        if (isUnLimited) tbValue.onValueChanged.AddListener(OnValueChangeInputValueUnLimited);
        else tbValue.onValueChanged.AddListener(OnValueChangeInputValue);
    }

    private void OnEnable()
    {
        InitTextDefault();
    }

    private void InitTextDefault()
    {
        if (txtValue)
        {
            txtValue.text = Languages.Language.GetKey(keyValueDefault);
            txtValue.fontStyle = FontStyle.Italic;
            txtValue.color = colorDefault;
        }
    }
    
    public void OnValueChangeInputValue(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            txtValue.text = "";
            return;
        }
        long money = 0;
        long.TryParse(value, out money);
        if (money > ClientConfig.UserInfo.GOLD)
        {
            money = ClientConfig.UserInfo.GOLD;
            tbValue.text = money.ToString();
        }
        txtValue.color = colorActive;
        txtValue.fontStyle = FontStyle.Normal;
        txtValue.text = MoneyHelper.FormatNumberAbsolute(money);
    }

    public void OnValueChangeInputValueUnLimited(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            InitTextDefault();
            return;
        }
        long money = 0;
        long.TryParse(value, out money);
        txtValue.color = colorActive;
        txtValue.fontStyle = FontStyle.Normal;
        txtValue.text = MoneyHelper.FormatNumberAbsolute(money);
    }
}
