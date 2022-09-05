using AppConfig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplaceTextView : MonoBehaviour
{
    private Text txtValue;
    // Start is called before the first frame update
    private void Awake()
    {
        txtValue = GetComponent<Text>();
    }
    void Start()
    {
        if (txtValue)
        {
            string str = txtValue.text;
            if (str.Contains("[GOLD]"))
            {
                str = str.Replace("[GOLD]", ClientConfig.UserInfo.GOLD_TYPE);
                txtValue.text = str;
            }
        }
    }

}
