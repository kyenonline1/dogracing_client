
using Base.Utils;
using Game.Gameconfig;
using PathologicalGames;
using System;
using UnityEngine;
using UnityEngine.UI;
using Utilites.ObjectPool;

namespace View.Home.Inbox
{
    public class ItemInboxView : MonoBehaviour
    {
        public Action<int> callbackClickEmail;
        public Action<int> callbackClickRemoveEmail;
        [SerializeField] private string poolName;
        [SerializeField] private Text txtTittleMail;
        [SerializeField] private Text txtTimeEnd;
        [SerializeField] private Image imgReading;
        [SerializeField] private Sprite[] sprReading;
        [SerializeField] private GameObject goBtnRemoveMail;

        private int mailId;
        private int mailType;
        private int reading;

        // Use this for initialization

        public void BtnClickItem()
        {
            if (callbackClickEmail != null) callbackClickEmail(mailId);
            if (reading == 0 && mailType != 1)
            {
                reading = 1;
                SetStateImage();
            }
        }

        public void SetData(int id, string tittle, string starttime, string endtime, int _reading, int _type)
        {
            //Debug.Log("SetData -------------------------");
            mailId = id;
            mailType = _type;
            reading = _reading;
            if(txtTittleMail) txtTittleMail.text = tittle;
            if (txtTittleMail) txtTimeEnd.text = starttime;
            if (goBtnRemoveMail)
            {
                goBtnRemoveMail.SetActive(mailType != 1);
            }
            SetStateImage();
        }

        private void SetStateImage()
        {
            if(reading == 0) // Chưa đọc
            {
                if (imgReading) imgReading.sprite = sprReading[0];
            }else // Đã đọc
            {
                if (imgReading) imgReading.sprite = sprReading[1];
            }
        }

        public void BtnRemoveClicked()
        {
            if (callbackClickRemoveEmail != null) callbackClickRemoveEmail(mailId);
            PoolManager.Pools[poolName].Despawn(transform);
        }
    }
}
