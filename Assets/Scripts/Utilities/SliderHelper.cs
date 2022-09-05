using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderHelper : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Action OnSliderPointerDown;
    public Action OnSliderPointerUp;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnSliderPointerDown != null) OnSliderPointerDown.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnSliderPointerUp != null) OnSliderPointerUp.Invoke();
    }
}

