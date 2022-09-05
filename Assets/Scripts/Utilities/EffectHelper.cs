using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    MOVE,
    SCALE,
    MOVE_X,
    ALPHA
}

public class EffectHelper : MonoBehaviour
{

    public EffectType effectType = EffectType.MOVE;
    public float DELAY_TIME;

    [SerializeField] private Vector3 vecFrom;
    [SerializeField] private Vector3 vecTo;

    private void OnEnable()
    {
        //Debug.Log("OnEnable: " + effectType);
        //DOTween.Complete(transform);
        switch (effectType)
        {
            case EffectType.MOVE:
                transform.localPosition = vecFrom;
                transform.DOLocalMoveY(vecTo.y, 0.35f).SetEase(Ease.OutBack).SetDelay(DELAY_TIME);
                break;
            case EffectType.MOVE_X:
                transform.localPosition = vecFrom;
                transform.DOLocalMoveX(vecTo.x, 0.55f).SetEase(Ease.OutBack).SetDelay(DELAY_TIME);
                break;
            case EffectType.SCALE:
                transform.localScale = vecFrom;
                transform.DOScale(vecTo, 0.55f).SetEase(Ease.OutBack).SetDelay(DELAY_TIME);
                break;
            case EffectType.ALPHA:
                CanvasGroup  cg = GetComponent<CanvasGroup>();
                if (cg) cg.alpha = 0;
                if (cg) cg.DOFade(1, 0.55f).SetDelay(DELAY_TIME);
                break;
        }
    }

    private void OnDisable()
    {
        //DOTween.Complete(transform);
        //DOTween.Kill(this);
    }

    private void OnDestroy()
    {
        //DOTween.Complete(transform);
        //DOTween.Kill(this);
    }
}
