using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggleTabsCate : MonoBehaviour
{
    [SerializeField] private TabCateView[] cates;

    [SerializeField] private GameObject[] goPopupCate;

    [SerializeField] private Image imgTittle;
    [SerializeField] private Sprite[] sprTittles;
    [SerializeField] private int tabDefault;

    private void Awake()
    {
        for(int i = 0; i < cates.Length; i++)
        {
            int index = i;
            cates[i].GetComponent<UIMyButton>()._onClick.AddListener(() =>
            {
                OnBtnCateClicked(index);
            });
        }
    }

    private void OnEnable()
    {
        if(cates != null && cates.Length > 0 && tabDefault >= 0)
        {
            OnBtnCateClicked(tabDefault);
        }
    }

    private void OnBtnCateClicked(int index)
    {
        if (cates[index].IsSelect) return;
        if(imgTittle != null && sprTittles != null && sprTittles.Length > 0)
        {
            imgTittle.sprite = sprTittles[index];
            imgTittle.SetNativeSize();
        }
        for (int i = 0; i < cates.Length; i++)
        {
            cates[i].SelectTab(index == i);
            if (goPopupCate != null && goPopupCate.Length > 0)
                goPopupCate[i].SetActive(index == i);
        }
    }

    public void SelectCate(int index)
    {
        if (cates != null && cates.Length > 0)
        {
            OnBtnCateClicked(index);
        }
    }
}
