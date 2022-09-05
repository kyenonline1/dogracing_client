using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;

namespace View.GamePlay.DuaCho
{
    public class ItemHistoryBetingView : MonoBehaviour
    {
        [SerializeField]
        private Text txtSession,
            txtTime,
            txtTotalBet,
            txtWin;

        [SerializeField]
        private Transform[] sprDogs;
        private int[] vector2;
        // Use this for initialization
        private void Awake()
        {
            InitData();
        }

        private void InitData()
        {
            //txtSession = transform.Find("txtPhien").GetComponent<Text>();
            //txtTime = transform.Find("txtTime").GetComponent<Text>();
            //txtTotalBet = transform.Find("txtTotalBet").GetComponent<Text>();
            //txtWin = transform.Find("txtWin").GetComponent<Text>();
            vector2 = new int[]
            {
                -246, -150, -52, 45, 142, 239
            };
        }

        public void InitData( long session, string timer, byte[] dogs, string moneydoor, long moneywin)
        {
            if (txtWin == null) InitData();
            txtSession.text = string.Format("#{0}", session);
            txtTime.text = timer;
            for(int i  = 0; i < dogs.Length; i++)
            {
                sprDogs[dogs[i] - 1].transform.SetSiblingIndex(i);
                //Vector2 pos = new Vector2(vector2[i], 20);
                //sprDogs[dogs[i]].localPosition = pos;
            }

            txtTotalBet.text = moneydoor;

            //long totalbet = 0;
            //for (int i = 0; i < txtBetDoor.Length; i++) txtBetDoor[i].text = string.Empty;
            //for (int i = 0; i < moneydoor.Length; i++)
            //{
            //    totalbet += moneydoor[i];
            //    txtBetDoor[i].text = MoneyHelper.FormatRelativelyWithoutUnit(moneydoor[i]);
            //}
            //txtTotalBet.text = MoneyHelper.FormatRelativelyWithoutUnit(totalbet);
            txtWin.text = MoneyHelper.FormatRelativelyWithoutUnit(moneywin);
        }

    }
}
