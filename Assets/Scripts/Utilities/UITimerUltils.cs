using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITimerUltils : MonoBehaviour
{
    [SerializeField] private Text txtTimer;

    /// <summary>
    /// true: Đếm ngược thời gian còn lại
    /// false: Đếm xuôi, tăng thời gian
    /// </summary>
    [SerializeField] private bool isCountDownTime;

    private float finishTime;
    private float remaintime;
    private UnityAction finishCallback;

    private bool isActive;
    private float totaltimer;


    /// <summary>
    /// </summary>
    /// <param name="time"> Seconds</param>
    /// <param name="callback"></param>
    public void SetTimerCountDown(long time, UnityAction callback = null)
    {
        finishTime = time;
        remaintime = 0;
        //Debug.Log("finishTime: " + finishTime + " - " + Time.realtimeSinceStartup);
        finishCallback = callback;
        gameObject.SetActive(true);
    }

    public void StartTimeUp(float totaltime = 0f)
    {
        isActive = true;
        totaltimer = totaltime;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Đổi kiểu đếm
    /// </summary>
    /// <param name="isType">true: đếm ngược, false: đếm tăng dần</param>
    public void ChangeType(bool isType)
    {
        isCountDownTime = isType;
    }

    private void StopTimeUp()
    {
        isActive = false;
        totaltimer = 0;
    }

    private void FixedUpdate()
    {
        if (isCountDownTime)
        {
            remaintime += Time.deltaTime;
            var countdownTime = (int)(finishTime - remaintime);
            //Debug.Log("countdownTime: " + countdownTime + " , finishTime: " + finishTime + " - remaintime" + remaintime);
            if (countdownTime < 0)
                countdownTime = 0;
            TimeSpan time = TimeSpan.FromSeconds(countdownTime);
            if (finishTime > 0)
            {
                string hours = (int)time.TotalHours > 9 ? string.Format("{0}", (int)time.TotalHours): string.Format("0{0}", (int)time.TotalHours);
                string minutes = (int)time.Minutes > 9 ? string.Format("{0}", (int)time.Minutes) : string.Format("0{0}", (int)time.Minutes);
                string seconds = (int)time.Seconds > 9 ? string.Format("{0}", (int)time.Seconds) : string.Format("0{0}", (int)time.Seconds);
                if ((int)time.TotalHours > 1)
                    txtTimer.text = string.Format("{0}:{1}:{2}", hours, minutes, seconds);
                else txtTimer.text = string.Format("{0}:{1}", minutes, seconds);
            }
            else
            {
                txtTimer.text = string.Empty;
                gameObject.SetActive(false);
            }
            if (countdownTime == 0)
            {
                if (finishCallback != null) finishCallback();
                finishCallback = null;
            }
        }
        else
        {
            if (isActive)
            {
                totaltimer += Time.deltaTime;

                TimeSpan time = TimeSpan.FromSeconds(totaltimer);
                //Debug.Log("countdownTime: " + totaltimer + " , time: " + time.TotalSeconds);
                if ((int)time.TotalHours > 1)
                {
                    var hours = (int)time.TotalHours < 10 ? string.Format("0{0}", (int)time.TotalHours) : string.Format("{0}", (int)time.TotalHours);
                    var minutes = time.Minutes < 10 ? string.Format("0{0}", time.Minutes) : string.Format("{0}", time.Minutes);
                    var seconds = time.Seconds < 10 ? string.Format("0{0}", time.Seconds) : string.Format("{0}", time.Seconds);
                    if (txtTimer) txtTimer.text = string.Format("{0}:{1}:{2}", hours, minutes, seconds);
                }
                else
                {
                    var minutes = time.Minutes < 10 ? string.Format("0{0}", time.Minutes) : string.Format("{0}", time.Minutes);
                    var seconds = time.Seconds < 10 ? string.Format("0{0}", time.Seconds) : string.Format("{0}", time.Seconds);
                   if(txtTimer) txtTimer.text = string.Format("{0}:{1}", minutes, seconds);
                }
            }
        }
    }

    private void OnEnable()
    {
        isActive = true;
        //SetTimerCountDown(120);
    }

    private void OnDisable()
    {
        finishCallback = null;
        isActive = false;
        finishTime = 0;
        remaintime = 0;
    }
}
