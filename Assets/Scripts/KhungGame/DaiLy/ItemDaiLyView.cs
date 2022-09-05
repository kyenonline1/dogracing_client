using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public delegate void OnClickTranfer(string name, string nickname);

public class ItemDaiLyView : MonoBehaviour {

    [SerializeField] private Image imgBackground;
    [SerializeField]
    private Text txtStt;
    [SerializeField]
    private Text txtName;
    [SerializeField]
    private Text txtAccount;
    [SerializeField]
    private Text txtPhone;
    [SerializeField]
    private GameObject objFacebook;
    [SerializeField]
    private GameObject objZalo;
    [SerializeField]
    private GameObject objTelegram;



    private string linkFacebook, dailyname, nickname, zalo, telegram;
    private long id;

    public OnClickTranfer dlgTranfer;

    public void SetData(int stt, string displayname, string phone, string distributorname, string linkfb, string zalo, string telegram)
    {
        if (imgBackground) imgBackground.SetAlpha(stt % 2 == 0 ? 1f : 0.001f, 0f);
        if (txtStt) txtStt.text = string.Format("{0}", stt + 1);
        if (txtName) txtName.text = displayname;
        if (txtPhone) txtPhone.text = string.Format("{0}", phone);
        if (txtAccount) txtAccount.text = distributorname;

        nickname = displayname;
        dailyname = distributorname;
        linkFacebook = linkfb;
        this.zalo = zalo;
        this.telegram = telegram;

        if (objFacebook) objFacebook.SetActive(!string.IsNullOrEmpty(linkFacebook));
        if (objZalo) objZalo.SetActive(!string.IsNullOrEmpty(zalo));
        if (objTelegram) objTelegram.SetActive(!string.IsNullOrEmpty(telegram));
    }

    public void ClickFacebook()
    {
        if(!string.IsNullOrEmpty(linkFacebook))
            Application.OpenURL(linkFacebook);
    }

    public void ClickZalo()
    {
        if (!string.IsNullOrEmpty(zalo))
            Application.OpenURL(zalo);
    }

    public void ClickTelegram()
    {
        if (!string.IsNullOrEmpty(telegram))
            Application.OpenURL(telegram);
    }

    public void ClickTranfer()
    {
        if(dlgTranfer != null)
        {
            dlgTranfer(nickname, dailyname);
        }
    }
}
