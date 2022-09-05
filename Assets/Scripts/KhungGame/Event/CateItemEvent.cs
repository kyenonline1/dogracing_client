using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CateItemEvent : MonoBehaviour {

    public delegate void dlgClickItem(int i);
    public dlgClickItem EventClickItem;

    public int ID;
    public Text txtContent;
    public Image bg;

    public void BtnClickItem()
    {
        if (EventClickItem != null) EventClickItem(ID);
    }

    public void SetData(int id, string tittle)
    {
        //Debug.Log("SetData -------------------------");
        this.ID = id;
        txtContent.text = tittle;
    }
}
