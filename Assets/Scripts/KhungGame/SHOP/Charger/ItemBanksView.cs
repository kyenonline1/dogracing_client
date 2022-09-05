using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.Home.Shop
{
    public class ItemBanksView : MonoBehaviour
    {
        public delegate void dlgClickBank(int index);
        public dlgClickBank callbackClickSelectBank;

        [SerializeField] private GameObject goSelected;

        private Image imgBank;
        private int indexBank;

        private void Awake()
        {
            GetComponent<UIMyButton>()._onClick.AddListener(OnBtnClickSelected);

        }

        public void SetIndex(int index, Sprite sprBank)
        {
            indexBank = index;
            imgBank = GetComponent<Image>();
            if (imgBank) imgBank.sprite = sprBank;
        }

        public void SetSelected(bool isSelected)
        {
            if (goSelected)
                goSelected.SetActive(isSelected);
            if (imgBank)
            {
                imgBank.color = isSelected ? Color.white : Color.gray;
            }
        }

        public void OnBtnClickSelected()
        {
            if (callbackClickSelectBank != null) callbackClickSelectBank(indexBank);
        }
    }
}