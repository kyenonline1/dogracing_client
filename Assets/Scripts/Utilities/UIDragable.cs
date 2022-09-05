using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
    [RequireComponent(typeof(RectTransform))]
    public class UIDragable : UIMyButton, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public RectTransform target;
        [Tooltip("Number of pixels of the window that must stay inside the canvas view.")]
        public int KeepWindowInCanvasX = 100;
        public int KeepWindowInCanvasY =  100;
        public UnityEvent _onDragFinish;

        public static bool ResetCoords = false;

        private RectTransform m_transform = null;
        private bool _isDragging = false;
        private Vector3 m_originalCoods = Vector3.zero;
        private Canvas m_canvas;
       // private RectTransform m_canvasRectTransform;

        //TEST:
        public RectTransform m_RectTransform;

        private Vector3 mouseDownPos;

        protected override void Awake()
        {
            base.Awake();
            if (target == null) m_transform = GetComponent<RectTransform>();
            else m_transform = target;
        }

        // Use this for initialization
        protected override void Start()
        {
            m_originalCoods = m_transform.position;
            m_canvas = GetComponentInParent<Canvas>();
           // m_canvasRectTransform = m_canvas.GetComponent<RectTransform>();
        }

        void Update()
        {
            if (ResetCoords)
                resetCoordinatePosition();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDragging)
            {
                var delta = ScreenToCanvas(eventData.position) - ScreenToCanvas(eventData.position - eventData.delta);
                //Debug.Log("OnDrag: " + delta);
                m_transform.localPosition = ScreenToCanvas(eventData.position - eventData.delta);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject == null)
                return;

            if (eventData.pointerCurrentRaycast.gameObject.name == name)
            {
                _isDragging = true;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
            if (_onDragFinish != null) _onDragFinish.Invoke();
        }

        void resetCoordinatePosition()
        {
            //Debug.Log("resetCoordinatePosition");
            m_transform.position = m_originalCoods;
            ResetCoords = false;
        }

        private Vector3 ScreenToCanvas(Vector3 screenPosition)
        {
            Vector3 localPosition;
            Vector2 min;
            Vector2 max;
            var canvasSize = m_RectTransform.sizeDelta;

            if (m_canvas.renderMode == RenderMode.ScreenSpaceOverlay || (m_canvas.renderMode == RenderMode.ScreenSpaceCamera && m_canvas.worldCamera == null))
            {
                localPosition = screenPosition;

                min = Vector2.zero;
                max = canvasSize;
                //Debug.Log("1111111111111111: " + min + " --------------- Max: " + max);
            }
            else
            {
                var ray = m_canvas.worldCamera.ScreenPointToRay(screenPosition);
                var plane = new Plane(m_RectTransform.forward, m_RectTransform.position);

                float distance;
                if (plane.Raycast(ray, out distance) == false)
                {
                    throw new Exception("Is it practically possible?");
                };
                var worldPosition = ray.origin + ray.direction * distance;
                localPosition = m_RectTransform.InverseTransformPoint(worldPosition);

                min = -Vector2.Scale(canvasSize, m_RectTransform.pivot);
                max = Vector2.Scale(canvasSize, Vector2.one - m_RectTransform.pivot);
                //Debug.Log("2222222222222222222222: " + min + " --------------- Max: " + max + " --- localPosition: " + localPosition);
            }

            // keep window inside canvas
            localPosition.x = Mathf.Clamp(localPosition.x, min.x + KeepWindowInCanvasX, max.x - KeepWindowInCanvasX);
            localPosition.y = Mathf.Clamp(localPosition.y, min.y + KeepWindowInCanvasY, max.y - KeepWindowInCanvasY);
            //Debug.Log("--- localPosition: " + localPosition);
            return localPosition;
        }

        public override void OnPointerDown(PointerEventData e)
        {
            base.OnPointerDown(e);
            mouseDownPos = m_transform.localPosition;
        }

        public override void OnPointerClick(PointerEventData e)
        {
            var currentPos = m_transform.localPosition;
            if ((Mathf.Abs(mouseDownPos.x - currentPos.x) > 1) || (Mathf.Abs(mouseDownPos.y - currentPos.y) > 1))
            {
                return;
            }
            else
                base.OnPointerClick(e);
        }
    }
}