using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
#if UNITY_EDITOR

using UnityEditor;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

static internal class UIPressButtonEditor
{
    [MenuItem("GameObject/UI/UI Press Button", false)]
    public static void AddUIPressButton(MenuCommand menuCommand)
    {
        GameObject parent = menuCommand.context as GameObject;

        GameObject go = new GameObject("Button");
        go.AddComponent<RectTransform>();
        GameObjectUtility.SetParentAndAlign(go, parent);

        Image image = go.AddComponent<Image>();
        image.type = Image.Type.Sliced;

        UIPressButton bt = go.AddComponent<UIPressButton>();
    }
}
#endif

public class UIPressButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent _onClick;

    public Sprite normalSprite;
    public Sprite pressSprite;

    private Image image;
    private Coroutine clickCoroutine;
    private bool isPressing = false;

    public virtual void Awake()
    {
        image = GetComponent<Image>();
    }

    private void SetSpriteImage(Sprite sprite)
    {
        if (image != null)
            image.sprite = sprite;
    }

    public virtual void OnPointerEnter(PointerEventData e)
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && !isPressing)
#endif
        {
            isPressing = true;
            SetSpriteImage(pressSprite);
            clickCoroutine = StartCoroutine(OnButtonPress());
        }
    }

    IEnumerator OnButtonPress()
    {
        if (_onClick != null) _onClick.Invoke();
        yield return new WaitForSeconds(0.5f);
        var count = 0;
        while (isPressing)
        {
            if (_onClick != null) _onClick.Invoke();
            yield return new WaitForSeconds(count < 5 ? 0.2f : 0.1f);
            count++;
        }
    }

    public virtual void OnPointerExit(PointerEventData e)
    {
        if (clickCoroutine != null) StopCoroutine(clickCoroutine);
        isPressing = false;
        SetSpriteImage(normalSprite);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
#if UNITY_EDITOR
        if (!isPressing)
        {
            isPressing = true;
            SetSpriteImage(pressSprite);
            clickCoroutine = StartCoroutine(OnButtonPress());
        }
#endif
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(clickCoroutine != null)
        StopCoroutine(clickCoroutine);
        isPressing = false;
        SetSpriteImage(normalSprite);
    }
}
