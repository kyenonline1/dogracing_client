using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.Events;

public class DOTweenLocation : MonoBehaviour, UITween
{
	public RectTransform rectTransform;

	public bool IsSetFromValue;
	public Vector2 FromLocation;
	public Vector2 ToLocation;
	public float Duration;

	public bool isTweening = false;
    public UnityAction onPlayForwardAction;

    void Awake(){
		if (rectTransform == null) rectTransform = GetComponent<RectTransform> ();
	}

    //void Start ( )
    //{
    //    PlayForward ( );
    //}

    public void PlayForward ( )
    {
        if ( isTweening )
            return;

        if ( IsSetFromValue )
        {
            rectTransform.localPosition = FromLocation;
        }
      
        isTweening = true;
        rectTransform.DOLocalMove ( ToLocation , Duration , true ).SetEase ( Ease.Linear ).OnComplete ( ( ) =>
        {
            isTweening = false;
            if ( onPlayForwardAction != null )
            {
                onPlayForwardAction ( );
            }
        } );
	}

	public void PlayReverse()
	{
		if (isTweening)
			return;
		if (IsSetFromValue)
            rectTransform.localPosition = FromLocation;
		rectTransform.DOLocalMove(ToLocation, Duration, true).SetEase(Ease.Linear).OnComplete(()=>{ isTweening = false; });
	}
}
