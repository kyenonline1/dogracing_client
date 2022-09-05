using Base.Utils;
using DG.Tweening;
using GameProtocol.DOG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.GamePlay.DuaCho
{
    public class DogRunControl : MonoBehaviour
    {
        private Segment[] Segments;
        private Transform tranAnim;
        private Transform tranIdle;
        [SerializeField]
        private Transform RankDog;
        private int curSegement;
        [SerializeField] private Vector2 defaultPosStart;

        // Use this for initialization
        private void Awake()
        {
            Segments = new Segment[5]
            {
                new Segment(){Time = 4, Position = 387},
                new Segment(){Time = 4, Position = 0},
                new Segment(){Time = 4, Position = 250},
                new Segment(){Time = 4, Position = -150},
                new Segment(){Time = 4, Position = 0},
            };
            tranAnim = transform.Find("Dog");
            tranIdle = transform.Find("IDLE");
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(Random.Range(1,2));
            //StartCoroutine(RunDog());
        }
        float TimeBegin;
       // bool isTimeBegin = false;
        float pos;
        public void SetPos(Segment[] segments,float position, int curSeg)
        {
            try
            {
                curSegement = curSeg;
                pos = Mathf.Round(position);
                int timer = 0;
                for (int i = 0; i < segments.Length; i++)
                {
                    ////Debug.Log("segments: BEFORE " + segments[i].Position + " , timer: " + segments[i].Time + " , Start: position: " + position + " , timer : " + timer);

                    segments[i].Time = segments[i].Time - timer;// i * 5 - timer;//
                    timer += segments[i].Time;
                    if (i == curSeg && pos > -387)
                    {
                       // isTimeBegin = (pos - segments[i].Position != 0);
                        ////Debug.Log("---------------------------------------: " + i + " ,: " + isTimeBegin);
                        if (i > 0)
                        {
                            TimeBegin = Mathf.Abs(getTime(segments[i - 1].Position, segments[i].Position, segments[i].Time, position));
                        }
                        else
                        {
                            TimeBegin = Mathf.Abs(getTime(-600, segments[i].Position, segments[i].Time, position));
                        }
                    }
                    ////Debug.Log("segments: " + segments[i].Position + " , timer: " + segments[i].Time + " , Start: position: " + position + " , TimeBegin: " + TimeBegin + " , timer : " + timer);
                }
                Segments = segments;
                Vector2 vector2 = new Vector2(Mathf.Round(pos), Mathf.Round(transform.localPosition.y));
                //Debug.Log("SET POSITION : " + vector2);
                transform.localPosition = curSegement == 0 ? defaultPosStart : vector2;
            }
            catch
            {
                Vector2 vector2 = new Vector2(0, Mathf.Round(transform.localPosition.y));
                //Debug.Log("SET POSITION : " + vector2);
                transform.localPosition = curSegement == 0 ? defaultPosStart : vector2;
                transform.localPosition = vector2;
            }
        }

        private Coroutine coroutineRunDog = null;
        public void StartRunDog()
        {
            if (coroutineRunDog != null) StopCoroutine(coroutineRunDog);
            coroutineRunDog = StartCoroutine(RunDog());
        }

        public IEnumerator RunDog()
        {
            //IEnumerator enumerator = dicPosDog.GetEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    yield return transform.DOLocalMoveX((float)enumerator.Current, 0.5f).WaitForCompletion();
            //}
            //tranIdle.gameObject.SetActive(true);
            //yield return new WaitForSeconds(2f);
            tranIdle.gameObject.SetActive(false);
            tranAnim.gameObject.SetActive(true);
            int index = curSegement == 0 ? 1 : curSegement;
            //Debug.Log("RunDog: " + curSegement + " , TimeBegin: " + TimeBegin + " , index: " + index);
            for (int i = index; i < Segments.Length; i++)
            {
                //Segments[i].Time = 5;
                //Debug.Log("START GO TO DOG SEGEMENT: " + i + "  " + Segments[i].Time + " , TO POS: " + Segments[i].Position + " ---- Dog  " + gameObject.name);
                //float posMove = float.Parse(string.Format("{0:0.00}", Segments[i].Position));
                int posMove = Segments[i].Position;
                //Debug.Log("START GO TO DOG SEGEMENT: " + posMove + " , Segement Pos: " + Segments[i].Position);
                if ((curSegement == i && curSegement > 0 && TimeBegin > 0) || ((posMove - Segments[i].Position == 0) && curSegement == i && curSegement > 0))
                {
                    if (pos - posMove == 0) yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(TimeBegin));
                    else
                        yield return transform.DOLocalMoveX(posMove + 1, TimeBegin).SetEase(Ease.Linear).WaitForCompletion();
                }
                else
                {
                    if (pos - posMove == 0) yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(Segments[i].Time));
                    else
                        yield return transform.DOLocalMoveX(posMove + 1, Segments[i].Time).SetEase(Ease.Linear).WaitForCompletion();
                }
                //Debug.Log("END --------------- GO TO DOG SEGEMENT: " + i);
            }
            ////Debug.Log("END RUN DOG -------------------------");
            int _pos = Segments[Segments.Length - 1].Position;
            if (_pos != 0)
            {
                float timer = ((float)Mathf.Abs(_pos) / 840f);
                yield return transform.DOLocalMoveX(0, timer).SetEase(Ease.Linear).WaitForCompletion();
            }
            if (RankDog) RankDog.DOKill();
            if (RacingDogView.Instance.Index < 3)
            {
                Time.timeScale = 0.001f;
                EventManager.Instance.RaiseEventInTopic(EventManager.SCREEN_SHORT);
            }else Time.timeScale = 1f;
            //Debug.Log("----------------------: " + ((float)Segments[Segments.Length - 1].Position + 1200) + " , segement 5: " + (float)Segments[Segments.Length - 1].Position);
            yield return transform.DOLocalMoveX(1000, 1f).SetEase(Ease.Linear).WaitForCompletion();
            tranAnim.gameObject.SetActive(false);
            //tranIdle.gameObject.SetActive(false);
        }

        private float getTime(int posStart, int posEnd, int timer, float curpos)
        {
            float time = Mathf.Abs(posEnd - curpos) * timer / Mathf.Abs(posEnd - posStart);
            //Debug.Log("VAN TOC: " + time + " , dog: " + gameObject.name);
            return Mathf.Round(time);
        }

        private void OnDisable()
        {
            if (coroutineRunDog != null) StopCoroutine(coroutineRunDog);
            DOTween.Kill(this, false);
            StopAllCoroutines();
            tranAnim.gameObject.SetActive(false);
            tranIdle.gameObject.SetActive(false);
            // Vector2 vector2 = new Vector2(-387, transform.localPosition.y);
            //transform.localPosition = vector2;
        }

        private void OnDestroy()
        {
            if (coroutineRunDog != null) StopCoroutine(coroutineRunDog);
            DOTween.Kill(this, false);
            StopAllCoroutines();
            tranAnim.gameObject.SetActive(false);
            tranIdle.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        public void TweenRankDog(float toPos)
        {
            RankDog.DOLocalMoveX(toPos, 0.25f);
        }
    }
}
