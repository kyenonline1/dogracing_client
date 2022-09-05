using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MiniGame.MiniPoker
{

    public class GroupDragHandler : MonoBehaviour
    {
        public void GDOnEnable(DragHandler dragHandler)
        {
            Transform[] childes = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                childes[i] = transform.GetChild(i);
            }
            int siblingIdx = -1;
            for (int i = 0; i < childes.Length; i++)
            {
                //CanvasGroup canvasGroup = childes[i].GetComponent<CanvasGroup>();
                //if (!canvasGroup) canvasGroup = childes[i].GetComponentInChildren<CanvasGroup>();
                //Debug.Log(canvasGroup);
                if (childes[i].GetComponentInChildren<DragHandler>() == dragHandler)
                {
                    childes[i].SetSiblingIndex(transform.childCount - 1);
                    //if (canvasGroup) canvasGroup.alpha = 1;
                }
                else
                {
                    siblingIdx++;
                    childes[i].SetSiblingIndex(siblingIdx);
                    //if (canvasGroup) canvasGroup.alpha = 0.5f;
                }
            }
        }

        public void GDOnDisable(DragHandler dragHandler)
        {
            Transform[] childes = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                childes[i] = transform.GetChild(i);
            }
            int siblingIdx = childes.Length - 1;
            bool showed = false;
            for (int i = childes.Length - 1; i >= 0; i--)
            {
                CanvasGroup canvasGroup = childes[i].GetComponent<CanvasGroup>();
                if (!canvasGroup) canvasGroup = childes[i].GetComponentInChildren<CanvasGroup>();
                if (canvasGroup != null && canvasGroup.gameObject.activeSelf && !showed)
                {
                    showed = true;
                    childes[i].SetSiblingIndex(transform.childCount - 1);
                    //if (canvasGroup) canvasGroup.alpha = 1;
                }
                else
                {
                    siblingIdx--;
                    childes[i].SetSiblingIndex(siblingIdx);
                    //if (canvasGroup) canvasGroup.alpha = 0.5f;
                }
            }
        }

        public void OnPointerDown(DragHandler dragHandler, PointerEventData eventData)
        {
            Transform[] childes = new Transform[transform.childCount];
            for(int i = 0; i < transform.childCount; i++){
                childes[i] = transform.GetChild(i);
            }
            int siblingIdx = -1;
            for (int i = 0; i < childes.Length; i++)
            {
                //CanvasGroup canvasGroup = childes[i].GetComponent<CanvasGroup>();
                //if (!canvasGroup) canvasGroup = childes[i].GetComponentInChildren<CanvasGroup>();
                if (childes[i].GetComponentInChildren<DragHandler>() == dragHandler)
                {
                    childes[i].SetSiblingIndex(transform.childCount - 1);
                    //if(canvasGroup) canvasGroup.alpha = 1;
                }
                else
                {
                    siblingIdx++;
                    childes[i].SetSiblingIndex(siblingIdx);
                    //if(canvasGroup) canvasGroup.alpha = 0.5f;
                }
            }
        }

        public void OnBeginDrag(DragHandler dragHandler, PointerEventData eventData)
        {
            OnPointerDown(dragHandler, eventData);
        }

        public void OnDrag(DragHandler dragHandler, PointerEventData eventData)
        {

        }

        public void OnEndDrag(DragHandler dragHandler, PointerEventData eventData)
        {
            
        }

        public DragHandler[] GetDragHandlers(){
            return GetComponentsInChildren<DragHandler>();
        } 
    }
}