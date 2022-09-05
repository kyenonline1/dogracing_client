using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using System;
using Utilites;

namespace View.GamePlay.Slot
{
    public class EffectText : MonoBehaviour
    {

        private long startvalue, endvalue;
        private Text txtMoney;
        [SerializeField]
        private float duration = 10f;

        public long START_VALUE
        {
            set
            {
                startvalue = value;
            }
        }
        public long END_VALUE
        {
            set
            {
                endvalue = value;
            }
        }

        public float DURATION
        {
            set
            {
                duration = value;
            }
        }

        // Use this for initialization
        void Awake()
        {
            //Debug.LogError("AWAKE TEXT EFFECT");
            //START_VALUE = 0;
            //END_VALUE = 300;

            txtMoney = transform.GetComponent<Text>();

        }
        void OnEnable()
        {
            //Debug.LogError("START TEXT EFFECT : " + startvalue + " , endvalue: " + endvalue);
            Effect();
        }

        public void Effect()
        {
            //Debug.LogError("START TEXT EFFECT : " + startvalue + " , endvalue: " + endvalue  + " ,duration: " + duration) ;
            if ( endvalue == 0)
                return;
            if (startvalue == endvalue) return;
            if (txtMoney)
            {
                txtMoney.text = string.Empty;
                DOTween.To((money) =>
                {
                    if (txtMoney)
                        txtMoney.text = MoneyHelper.FormatNumberAbsolute(long.Parse(String.Format("{0:0}", money)));
                }, startvalue, endvalue, duration);
            }
            //startvalue = endvalue;
        }
    }
}
