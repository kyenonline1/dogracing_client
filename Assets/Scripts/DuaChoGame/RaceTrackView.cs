
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using AppConfig;

namespace View.GamePlay.DuaCho
{
    public class RaceTrackView : MonoBehaviour
    {
        private BackgroundControl _controlRacingTrack;
        private Transform transformTimerStart;
        private PlayerChatInRacingDogManager _PlayerManager;
        private Slider sliderTimeRacing;
        private Transform bgLongSat;
        private Transform OpenLongSat;
        private Animator animatorOpenLongSat;


        private void Awake()
        {
            _controlRacingTrack = transform.Find("BackgroundAnim").GetComponent<BackgroundControl>();
            transformTimerStart = transform.Find("Timer");
            sliderTimeRacing = transform.Find("RankDogs/Bg/Slider").GetComponent<Slider>();
            _PlayerManager = transform.Find("Players").GetComponent<PlayerChatInRacingDogManager>();
            bgLongSat = transform.Find("LongSat");
            OpenLongSat = transform.Find("OpenLongSat");
            animatorOpenLongSat = OpenLongSat.GetComponent<Animator>();
        }

        public void UpdateRacingTrack(float timer)
        {
            //Debug.LogError("UpdateRacingTrack " + timer);
            if(_controlRacingTrack)
            _controlRacingTrack.SET_TIMER = timer;
            
        }
        public void ResetBackground()
        {
            if (_controlRacingTrack)
                _controlRacingTrack.SetBackground();
        }

        public void RollRacingTrack()
        {
            if (_controlRacingTrack)
                _controlRacingTrack.RollRacingTrack();

            bgLongSat.gameObject.SetActive(false);
            OpenLongSat.gameObject.SetActive(false);
        }

        Coroutine CountTimeStartGame;

        public void CoutDownStartGame()
        {
            if (CountTimeStartGame != null)
            {
                StopCoroutine(CountTimeStartGame);
                return;
            }
            CountTimeStartGame = StartCoroutine(IECountTimeStartGame());
        }

        private IEnumerator IECountTimeStartGame()
        {
            //Debug.Log("IECountTimeStartGame----------- START");
            bgLongSat.gameObject.SetActive(true);
            OpenLongSat.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            transformTimerStart.gameObject.SetActive(true);
            ClientConfig.Sound.PlaySound(ClientConfig.Sound.SoundId.DuaCho_background_bip0);
            yield return new WaitForSeconds(1f);
            ClientConfig.Sound.PlaySound(ClientConfig.Sound.SoundId.DuaCho_background_bip0);
            yield return new WaitForSeconds(1f);
            ClientConfig.Sound.PlaySound(ClientConfig.Sound.SoundId.DuaCho_background_bip1);
            yield return new WaitForSeconds(0.9f);
            //animatorOpenLongSat.Play("OpenLongSat", 0);
            //yield return new WaitForSeconds(0.1f);
            //transformTimerStart.gameObject.SetActive(false);
            ////Debug.Log("IECountTimeStartGame----------- DONE");
            //CountTimeStartGame = null;
        }

        public IEnumerator IEOpenLongSat()
        {
            animatorOpenLongSat.Play("OpenLongSat", 0);
            yield return new WaitForSeconds(0.1f);
            transformTimerStart.gameObject.SetActive(false);
            //Debug.Log("IECountTimeStartGame----------- DONE");
            CountTimeStartGame = null;
        }

        public void SlierTimeRacing(float timer)
        {
            //Debug.Log("SlierTimeRacing ---------- : " + timer + " , " + (1 - timer / 20));
            sliderTimeRacing.value = 1 - timer / 20;
            DOTween.To((time) =>
            {
                //Debug.Log("SlierTimeRacing ----------  Tweeen : " + (1 - time / timer));
                sliderTimeRacing.value = 1 - time/20;
            }, timer, 0, timer).SetEase(Ease.Linear);
        }

        public void PlayerChatInRacing(int pos, string txt)
        {
            return;
            if (pos != -1)
            {
                PlayerChatInRacingDog player = _PlayerManager.GetPlayerByPos(pos);
                if (player != null)
                    player.PlayerChat(txt);
            }
            else
            {
                PlayerChatInRacingDog player = _PlayerManager.OtherPlayer;
                if (player != null)
                    player.PlayerChat(txt);
            }
        }

        public void ClearAllPlayerInfo()
        {
            return;
            _PlayerManager.ClearAllPlayerInfo();
        }

        public void RemovePlayer(int pos)
        {
            return;
            //PlayerChatInRacingDog player = _PlayerManager.GetPlayerByPos(pos);
            //if(player != null)
            //{
            //    player.HidePlayer();
            //}
        }

        public void FillBasicPlayerInfo(int pos, string avatar, string nickname, int vip = 0)
        {
            return;
            //Debug.Log("FillBasicPlayerInfo " + nickname);
            PlayerChatInRacingDog playerView = _PlayerManager.GetPlayerByPos(pos);
            if (playerView != null)
            {
                //Debug.Log("FillBasicPlayerInfo  222 " + nickname);
                playerView.FillBasicInfo(nickname, avatar, vip);
            }
        }
    }
}
