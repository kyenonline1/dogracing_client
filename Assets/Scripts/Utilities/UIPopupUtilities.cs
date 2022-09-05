using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace utils
{
    public enum ScaleType
    {
        ScaleUp,
        ScaleDown,
        Move
    }

    public class UIPopupUtilities : MonoBehaviour
    {
        protected Action callbackClosePopup, callbackOpenPopup;

        [SerializeField] private CanvasGroup cvGroupPopup;

        public ScaleType typePopup = ScaleType.ScaleUp;

        public virtual void OpenPopup()
        {
            gameObject.SetActive(true);
            if (cvGroupPopup)
            {
                switch (typePopup)
                {
                    case ScaleType.ScaleDown:
                    case ScaleType.ScaleUp:
                        cvGroupPopup.transform.localScale = typePopup == ScaleType.ScaleUp ? Vector3.zero : Vector3.one * 1.5f;
                        cvGroupPopup.transform.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack);
                        cvGroupPopup.DOFade(1, 0.35f).OnComplete(() => {
                            if (callbackOpenPopup != null) callbackOpenPopup();
                        });
                        break;
                    case ScaleType.Move:
                        cvGroupPopup.transform.localPosition = new Vector3(-400, 0, 0);
                        cvGroupPopup.transform.DOLocalMove(Vector3.zero, 0.35f).SetEase(Ease.OutBack);
                        cvGroupPopup.DOFade(1, 0.35f).OnComplete(() => {
                            if (callbackOpenPopup != null) callbackOpenPopup();
                        });
                        break;
                }

            }
        }


        public virtual void ClosePopup()
        {
            if (cvGroupPopup)
            {
                switch (typePopup)
                {
                    case ScaleType.ScaleDown:
                    case ScaleType.ScaleUp:
                        cvGroupPopup.transform.DOScale(Vector3.zero, 0.35f).SetEase(Ease.InBack);
                        cvGroupPopup.DOFade(0, 0.35f).OnComplete(() => {
                            if (callbackClosePopup != null) callbackClosePopup();
                            gameObject.SetActive(false);
                        });
                        break;
                    case ScaleType.Move:
                        cvGroupPopup.transform.DOLocalMove(new Vector3(-400, 0, 0), 0.35f).SetEase(Ease.OutBack);
                        cvGroupPopup.DOFade(0, 0.35f).OnComplete(() => {
                            if (callbackOpenPopup != null) callbackClosePopup();
                            gameObject.SetActive(false);
                        });
                        break;
                }

            }
        }
    }
}
