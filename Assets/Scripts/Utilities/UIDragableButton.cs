using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragableButton : UIMyButton, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform target;
    public bool AlwayVisible = true;

    protected Canvas canvas;
    private RectTransform rectTransform;
    private Vector3 mouseDownPos;

    protected float screenWidth;
    protected float screenHeight;
    protected float scaleRate;
    private float btnWidth;
    private float btnHeight;
    private bool _isDragging = false;
    private Vector3 tmp = new Vector3();

    protected override void Awake()
    {
        base.Awake();
        canvas = GetComponentInParent<Canvas>();
        //		#if UNITY_EDITOR
        //		var canvasRect = canvas.GetComponent<RectTransform> ();
        //		screenWidth = canvasRect.rect.width;
        //		screenHeight = canvasRect.rect.height;
        //		#else
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        //		#endif
        scaleRate = screenWidth / 1920;
    }

    protected override void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        btnWidth = rectTransform.rect.width;
        btnHeight = rectTransform.rect.height;
    }

    protected virtual void OnUpdatePosition(float x, float y, Vector2 pos)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDragging)
        {
            var x = Input.mousePosition.x;
            var y = Input.mousePosition.y;
            if (AlwayVisible)
            {
                x = Mathf.Min(x, screenWidth - scaleRate * btnWidth / 2);
                x = Mathf.Max(x, scaleRate * btnWidth / 2);
                y = Mathf.Min(y, screenHeight - scaleRate * btnHeight / 2);
                y = Mathf.Max(y, scaleRate * btnHeight / 2);
            }
            tmp.Set(x, y, 0);
            if (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                Vector3 pos = canvas.worldCamera.ScreenToWorldPoint(eventData.position);
                pos.z = 90;
                if (target == null)
                    transform.position = pos;
                else
                    target.position = pos;
                OnUpdatePosition(x, y, pos);
            }
            else
            {
                if (target == null)
                    transform.position = tmp;
                else
                    target.position = tmp;
                OnUpdatePosition(x, y, tmp);
            }
        }
    }

    public override void OnPointerDown(PointerEventData e)
    {
        base.OnPointerDown(e);
        mouseDownPos = target == null ? transform.localPosition : target.localPosition;
    }

    public override void OnPointerClick(PointerEventData e)
    {
        var currentPos = target == null ? transform.localPosition : target.localPosition;
        if ((Mathf.Abs(mouseDownPos.x - currentPos.x) > 1) || (Mathf.Abs(mouseDownPos.y - currentPos.y) > 1))
        {
            return;
        }
        else
            base.OnPointerClick(e);
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

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
    }
}