using Base.Utils;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.GamePlay.DuaCho
{
    public class BackgroundControl : MonoBehaviour
    {
        private float TIMER = 20f;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public float SET_TIMER
        {
            set
            {
                TIMER = value;
            }
        }

        public void SetBackground()
        {
            //Debug.Log("TIMER: -------------------------------- " + TIMER);
            //if (TIMER < 20)
            //{
            //    float pos = 0 - 768 * (20 - TIMER);
            //    //Debug.LogError("RollRacingTrack: Timmer:  " + TIMER + " , POS: " + pos);
            //    transform.localPosition = new Vector3(pos, 0, 0);
            //}
            //else transform.localPosition = new Vector3(0,0,0);
        }

        public void RollRacingTrack()
        {
            if (TIMER > 20f) TIMER = 20f;
            animator.PlayInFixedTime("BackgrounAnim", 0, 20 - TIMER);
            //if (TIMER == 0)
            //{
            //    EventManager.Instance.RaiseEventInTopic(EventManager.SCREEN_SHORT);
            //}
            //else
            //{
            //    transform.DOLocalMoveX(-15360, TIMER).OnComplete(() =>
            //    {
            //        //Debug.Log("ROLL BACK GROUND END -----------------");
            //        //Time.timeScale = 0;
            //        EventManager.Instance.RaiseEventInTopic(EventManager.SCREEN_SHORT);
            //    }).SetEase(Ease.Linear).WaitForCompletion();
            //}
        }
    }
}
