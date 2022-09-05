using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR

using UnityEditor;

static internal class UIButtonEditor
{
    [MenuItem("GameObject/UI/UI Scale Button", false)]
    public static void AddUIButton(MenuCommand menuCommand)
    {
        GameObject parent = menuCommand.context as GameObject;

        GameObject go = new GameObject("Button");
        go.AddComponent<RectTransform>();
        GameObjectUtility.SetParentAndAlign(go, parent);

        Image image = go.AddComponent<Image>();
        image.type = Image.Type.Sliced;

        UIMyButton bt = go.AddComponent<UIMyButton>();
    }
}
#endif


public class UIMyButton : Selectable, IPointerDownHandler, IPointerClickHandler, IPointerUpHandler
{
    public UnityEvent _onClick;

    private Vector3 normalScale;
    private bool isPlayingEffect;

    [SerializeField]
    private bool isEnable = true;
    private GrayScale grayscale;

    //private Vector3 pointerClickPos;

    protected override void Awake()
    {
        normalScale = transform.localScale;
        if (grayscale == null) grayscale = GetComponent<GrayScale>();
    }

    public override void OnPointerDown(PointerEventData e)
    {
        if (!isEnable) return;
        //Debug.LogError("Pointer Down");
        //MasterAudio.ChangeVariationClip("buttonclick");
        //AudioManager.PlaySoundInGroup("Sound", "buttonclick");
        //AudioManager.PlaySound(SoundGroup.Sound, SoundType.ButtonClick);
        //DOTween.Complete(transform);
        //pointerClickPos = Input.mousePosition;
        transform.DOScale(normalScale * 0.9f, 0.15f).SetUpdate(true);
    }

    public override void OnPointerUp(PointerEventData e)
    {
        if (!isEnable) return;
        //Debug.LogError("Pointer Up");
        DOTween.Complete(transform);
        transform.DOScale(normalScale, 0.9f).SetUpdate(true);
        //_onClick.Invoke();
    }


    public virtual void OnPointerClick(PointerEventData e)
    {
        //Debug.LogError("Pointer Click");
        if (!isEnable) return;
        if (isPlayingEffect) return;
        //if (ClientConfig.Setting.Sound) audioSource.Play();
        DOTween.Complete(transform);
        transform.localScale = normalScale * 0.75f;
        CoReleaseAnimation();
    }

    private void CoReleaseAnimation()
    {
        isPlayingEffect = true;
        var sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(normalScale * 1.1f, 0.15f));
        sequence.Append(transform.DOScale(normalScale, 0.15f));

        sequence.SetUpdate(true);

        sequence.OnComplete(() =>
        {
            isPlayingEffect = false;
            //if (pointerClickPos.x == Input.mousePosition.x && pointerClickPos.y == Input.mousePosition.y)
            //    _onClick.Invoke();
            //if (Mathf.Abs(pointerClickPos.x - Input.mousePosition.x) < 5 && Mathf.Abs(pointerClickPos.y - Input.mousePosition.y) < 5)
            //_onClick.Invoke();
        });
        _onClick.Invoke();
    }

    protected override void OnEnable()
    {
        DOTween.Complete(transform);
        //transform.localScale = normalScale;
        isPlayingEffect = false;
    }

    protected override void OnDisable()
    {
        DOTween.Complete(transform);
        transform.localScale = normalScale;
        isPlayingEffect = false;
    }

    public void SetEnableUIButton(bool enable)
    {
        if (enable != isEnable)
        {
            isEnable = enable;
            if (grayscale == null) grayscale = GetComponent<GrayScale>();
            if (grayscale != null) grayscale.SetGrayScale(!isEnable);
        }
    }

}

