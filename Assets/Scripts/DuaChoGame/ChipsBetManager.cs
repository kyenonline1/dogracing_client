using AppConfig;
using Game.Gameconfig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;
using View.GamePlay.TaiXiu;

namespace View.GamePlay.DuaCho
{
    public class ChipsBetManager : MonoBehaviour
    {
        [SerializeField]
        private ChipSelect[] chipSelects;
        [SerializeField] private Text txtChipbet;
        private int indexChip = 0;

        //private Transform tranAllin;
        // Use this for initialization
        private void Awake()
        {
            for (int i = 0; i < chipSelects.Length; i++)
            {
                chipSelects[i].INDEX = i;
                chipSelects[i].dlgSelectChip = SelectChip;
            }
            //tranAllin = transform.Find("Allin");
        }

        private void SelectChip(int index)
        {
            indexChip = index;
            for (int i = 0; i < chipSelects.Length; i++)
            {
                chipSelects[i].SelectedChip(index == i);
            }
            //ShowAllIn(index == chipSelects.Length - 1);
        }

        public void InitChip(long[] blind)
        {
            if (blind == null) return;
            for (int i = 0; i < 5; i++)
            {
                chipSelects[i].GetComponentInChildren<Text>().text = MoneyHelper.FormatRelativelyWithoutUnit(blind[i]);
            }
        }

        Coroutine ieShowAllin = null;

        void ShowAllIn(bool isShow)
        {
            if (isShow)
            {
                if (ieShowAllin != null)
                    StopCoroutine(ieShowAllin);
                ieShowAllin = StartCoroutine(ShowAllIn());
            }
            else
            {
                if (ieShowAllin != null)
                    StopCoroutine(ieShowAllin);
                //tranAllin.gameObject.SetActive(false);
            }
        }

        IEnumerator ShowAllIn()
        {
            //tranAllin.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            //tranAllin.gameObject.SetActive(false);
        }

        public Transform GetChipSelect()
        {
            if (indexChip >= 0) return chipSelects[indexChip].transform;
            return null;
        }

        public int CHIP_INDEX
        {
            get
            {
                return indexChip;
            }
        }
    }
}
