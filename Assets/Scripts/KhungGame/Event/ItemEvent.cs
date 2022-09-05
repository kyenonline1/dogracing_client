using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Event
{
    public class ItemEvent : MonoBehaviour
    {
        public Action<int> callbackReadBanner;

        [SerializeField] private Image icon;

        private int index;

        public void ShowIcon(string url, int index)
        {
            this.index = index;
            if (icon.gameObject.activeInHierarchy)
                StartCoroutine(ImageLoader.HTTPLoadImage(icon, url));
        }

        public void OnBtnBannerClicked()
        {
            if (callbackReadBanner != null) callbackReadBanner(index);
        }
    }
}
