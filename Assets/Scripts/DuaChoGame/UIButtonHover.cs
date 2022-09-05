using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonHover : Button {

    Transform hover;
  

    protected override void Awake()
    {
        base.Awake();
        hover = transform.Find("hover");
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (hover) hover.gameObject.SetActive(false);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (hover) hover.gameObject.SetActive(true);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (hover) hover.gameObject.SetActive(true);
    }
    

}
