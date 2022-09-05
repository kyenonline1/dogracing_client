using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.GamePlay.DuaCho
{
    public class ItemHistoryView : MonoBehaviour
    {
        private Text txtSession,
            txtTimer;
        [SerializeField]
        private Transform[] sprDog;
        

        private int[] vector2;

        // Use this for initialization
        private void Awake()
        {
            InitData();
        }

        void InitData()
        {
            txtSession = transform.Find("txtPhien").GetComponent<Text>();
            txtTimer = transform.Find("txtTime").GetComponent<Text>();
            vector2 = new int[]
            {
                -145, 20, 180, 345, 510, 685
            };
        }

        public void InitData( long session, string timer, byte[] dogs)
        {
            if (vector2 == null) InitData();
            txtSession.text = string.Format("#{0}", session);
            txtTimer.text = timer;
            for(int i = 0; i < dogs.Length; i++)
            {
                Vector2 pos = new Vector2(vector2[i], 0);
                sprDog[dogs[i] - 1].localPosition = pos;
            }
        }
    }
}
