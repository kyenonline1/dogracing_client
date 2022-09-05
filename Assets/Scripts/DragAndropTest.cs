using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndropTest : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{

    private Vector3 startingPosition;
    private Transform startingParent;
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
            startingPosition = gameObject.transform.position;
            startingParent = gameObject.transform.parent;
    }


    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        var screenPoint = new Vector3(eventData.position.x, eventData.position.y, 100);
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag: ");
        gameObject.transform.parent = startingParent;
            gameObject.transform.position = startingPosition;
            //GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop: ");
        
    }

    // Use this for initialization
    void Start()
    {
        startingPosition = gameObject.transform.position;
        Invoke("CancelDrag", 5);
    }

    void CancelDrag()
    {
        Debug.Log("CANCEL DRAG");
        //GetComponent<Image>().raycastTarget = false;
        GetComponent<DragAndropTest>().enabled = false;
        gameObject.transform.position = startingPosition;
    }

}