using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.GamePlay.DuaCho
{
    public class CountDownTime : MonoBehaviour
    {
        [SerializeField] private Text txtCountTime;
        [SerializeField] private Slider sldTimer;


        float remainTime;
        long totaltime;

        private void Update()
        {
            if (remainTime >= 0)
            {
                remainTime -= Time.deltaTime;
                //Debug.Log("remainTime" + remainTime + " ,  --------: " + string.Format("{0:0}", remainTime));
                //totaltime = long.Parse(string.Format("{0:0}", remainTime));
                txtCountTime.text = string.Format("{0:0}", remainTime);
                if (sldTimer) sldTimer.value = remainTime / (float)totaltime;
            }
            if (remainTime < 0)
            {
                txtCountTime.text = "0";
            }
        }

        public void CountTime(long remaintime, bool isYellow)
        {
            this.totaltime = remaintime;
            this.remainTime = remaintime;
            txtCountTime.color = isYellow ? Color.yellow : Color.red;
            //if (coroutineCountDown != null) StopCoroutine(coroutineCountDown);
            //coroutineCountDown = StartCoroutine(IECountDownTime(remaintime, isYellow));
        }
        

        Coroutine coroutineCountDown = null;

        IEnumerator IECountDownTime(long _remaintime, bool isYellow)
        {
            if (txtCountTime)
            {
                txtCountTime.color = isYellow ? Color.yellow : Color.red;
                txtCountTime.text = _remaintime < 10 ? string.Format("0{0}", _remaintime) : string.Format("{0}", _remaintime);
            }
            long time = _remaintime;
            for (int i = 0; i < time; i++)
            {
                yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(timer));
                _remaintime -= 1;
                if(txtCountTime) txtCountTime.text = _remaintime < 10 ? string.Format("0{0}", _remaintime) : string.Format("{0}", _remaintime);
            }
            txtCountTime.text = "00";
        }

        float timer = 1;

        //private void OnApplicationFocus(bool focus)
        //{
        //    if (focus)
        //    {
        //        timer = 1;
        //    }
        //    else
        //    {
        //        timer = 0.35f;
        //    }
        //}
    }
}
