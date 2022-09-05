using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MiniGame.MiniPoker {
    public class DragHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {
		Vector2 posBeginDrag;
        GroupDragHandler groupDragHandler;
        RectTransform rtGroupDragHandler;
        RectTransform rectTransform;
        RectTransform rtParent;
        float scaleFactor;

        Coroutine coroutine;
		// Use this for initialization
		void Start () {
            groupDragHandler = GetComponentInParent<GroupDragHandler>();
            if(groupDragHandler){
                rtGroupDragHandler = groupDragHandler.GetComponent<RectTransform>();
            }
            rectTransform = GetComponent<RectTransform>();
            rtParent = transform.parent.GetComponent<RectTransform>();
            scaleFactor = GetComponentInParent<Canvas>().scaleFactor;
        }

        void OnEnable()
        {
            if(groupDragHandler == null) groupDragHandler = GetComponentInParent<GroupDragHandler>();
            if (groupDragHandler != null) groupDragHandler.GDOnEnable(this);
        }

        void OnDisable()
        {
            if (groupDragHandler == null) groupDragHandler = GetComponentInParent<GroupDragHandler>();
            if (groupDragHandler != null) groupDragHandler.GDOnDisable(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (groupDragHandler != null) groupDragHandler.OnPointerDown(this, eventData);
            if (transform.parent.name.Equals("InGame"))
            {
                Transform tran = transform.parent;
                if(tran.parent != null)
                {
                    tran.parent.SetSiblingIndex(3);
                }
            }
        }

		public void OnBeginDrag (PointerEventData eventData){
            Vector2 pPos = rtParent.anchoredPosition;
			posBeginDrag = Vector2.zero;
            posBeginDrag.x = eventData.position.x/scaleFactor - pPos.x;
            posBeginDrag.y = eventData.position.y/scaleFactor - pPos.y;
            if (groupDragHandler != null) groupDragHandler.OnBeginDrag(this, eventData);
		}

		public void OnDrag (PointerEventData eventData){
            Vector2 pos = eventData.position/scaleFactor;
			pos.x -= posBeginDrag.x;
			pos.y -= posBeginDrag.y;
            rtParent.anchoredPosition = pos;
            if (groupDragHandler != null) groupDragHandler.OnDrag(this, eventData);
		}

		public void OnEndDrag (PointerEventData eventData){
            if(groupDragHandler && rtGroupDragHandler){
                Vector2 pos = rtParent.anchoredPosition;
                bool canUpdate = false;
                if (pos.x < -rtGroupDragHandler.sizeDelta.x / 2)
                {
                    pos.x = -rtGroupDragHandler.sizeDelta.x / 2;
                    canUpdate = true;
                }
                else if (pos.x > rtGroupDragHandler.sizeDelta.x/2)
                {
                    pos.x = rtGroupDragHandler.sizeDelta.x/2;
                    canUpdate = true;
                }
                if (pos.y < -rtGroupDragHandler.sizeDelta.y / 2)
                {
                    pos.y = -rtGroupDragHandler.sizeDelta.y / 2;
                    canUpdate = true;
                }
                else if (pos.y > rtGroupDragHandler.sizeDelta.y/2)
                {
                    pos.y = rtGroupDragHandler.sizeDelta.y/2;
                    canUpdate = true;
                }

                if(canUpdate){
                    if (coroutine != null) StopCoroutine(coroutine);
                    coroutine = StartCoroutine(MoveTo(pos, 0.2f));
                }
            }
            if (groupDragHandler != null) groupDragHandler.OnEndDrag(this, eventData);
		}

        IEnumerator MoveTo(Vector2 pos, float duration){
            float currentTime = 0;
            Vector2 startPos = rtParent.anchoredPosition;
            while (currentTime <= duration){
                currentTime += Time.deltaTime;
                rtParent.anchoredPosition = Vector2.Lerp(startPos, pos, currentTime / duration);
                yield return null;
            }
        }

        //T FindComponentInParent<T>(Transform tf){
        //    object t;
        //    if(tf.parent){
        //        t = tf.parent.GetComponent<T>();
        //        if (t != null)
        //        {
        //            Debug.Log((T)t);
        //            return (T)t;
        //        }
        //        else
        //        {
        //            Debug.Log("else");
        //            return FindComponentInParent<T>(transform.parent);
        //        }
        //    }
        //    return default(T);
        //}
    }
}
