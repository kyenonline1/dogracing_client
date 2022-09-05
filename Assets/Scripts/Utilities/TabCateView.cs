using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabCateView : MonoBehaviour
{
    [SerializeField] private GameObject imgSelect;
    //[SerializeField] private Sprite sprSelected;
    //[SerializeField] private Sprite sprUnSelect;

    [SerializeField] private bool isSelected;

    public bool IsSelect
    {
        get
        {
            return isSelected;
        }
    }

    private void Awake()
    {
        if (imgSelect) imgSelect.SetActive(isSelected);
    }

    public void SelectTab(bool isSelect)
    {
        isSelected = isSelect;
        if (imgSelect) imgSelect.SetActive(isSelected);
        //if (imgSelect)
        //{
        //    imgSelect.sprite = isSelect ? sprSelected : sprUnSelect;
        //}
    }
}
