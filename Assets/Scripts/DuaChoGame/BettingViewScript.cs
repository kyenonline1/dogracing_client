using AppConfig;
using DG.Tweening;
using GameProtocol.DOG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Utilites;
using Utilites.ObjectPool;

namespace View.GamePlay.DuaCho
{
    public class BettingViewScript : MonoBehaviour
    {
        [SerializeField]
        private RacingDogPlayerManager _playerManager;
        [SerializeField]
        private RacingDogDoorManager _bettingManager;
        //[SerializeField]
        //private ChipsBetManager _chipBetManager;
        [SerializeField]
        private CountDownTime countDownTime;
        [SerializeField]
        private UIMyButton btnHistory,
            btnRanking,
            btnHelp;
        [SerializeField]
        private UIMyButton btnIAP;
        [SerializeField]
        private Image[] imgResult;
        [SerializeField]
        private Text txtMoneyWin;
        [SerializeField]
        private GameObject goTotalWin;

        [SerializeField]
        private InputField inputMoney;
        [SerializeField]
        private GameObject ObjNotBetFullTime;

        // Use this for initialization
        private void Awake()
        {
            //_bettingManager = GetComponent<RacingDogDoorManager>();
            //_playerManager = transform.Find("Players").GetComponent<RacingDogPlayerManager>();
            ////_chipBetManager = transform.Find("Bg/Chips").GetComponent<ChipsBetManager>();
            ////countDownTime = transform.Find("Bg/Header/Slider/txtCountTime").GetComponent<CountDownTime>();
            //btnHistory = transform.Find("Bg/Buttons/btnHistory").GetComponent<UIMyButton>();
            //btnRanking = transform.Find("Bg/Buttons/btnRanking").GetComponent<UIMyButton>();
            //btnHelp = transform.Find("Bg/Buttons/btnHelp").GetComponent<UIMyButton>();
            ////btnIAP = transform.Find("Players/MePlayer/Info/bgInfo").GetComponent<Button>();
            //txtMoneyWin = transform.Find("Bg/RightContent/txtMoneyWin").GetComponent<Text>();
            //ObjNotBetFullTime = transform.Find("ObjNotBetFullTime").gameObject;
            if (btnHistory) btnHistory._onClick.AddListener(BtnHistoryClick);
            if (btnRanking) btnRanking._onClick.AddListener(BtnRankingClick);
            if (btnHelp) btnHelp._onClick.AddListener(BtnHelpClick);
            if (btnIAP) btnIAP._onClick.AddListener(BtnIAPClick);
             if(inputMoney) inputMoney.onEndEdit.AddListener((str)=> {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    long money = long.Parse(str);
                    if (money > ClientConfig.UserInfo.GOLD) money = ClientConfig.UserInfo.GOLD;
                    RacingDogView.Instance.RequestBetOtherChip(money);
                    OnSlectedInput(false);
                }
            });
        }
        
        private void BtnHistoryClick()
        {
            RacingDogView.Instance.BtnHistoryClick();
        }

        private void BtnRankingClick()
        {
            RacingDogView.Instance.BtnTopGameClick();
        }

        private void BtnHelpClick()
        {
            RacingDogView.Instance.BtnHelpClick();
        }

        private void BtnIAPClick()
        {
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowShop(CateShop.IAP);
        }

        public void OnBtnProfileClick()
        {
            if (TopFeauturesViewScript.instance) TopFeauturesViewScript.instance.ShowProfileInfo();
        }

        public void ShowObjNotBetFullTime(bool isShow)
        {
            ObjNotBetFullTime.SetActive(isShow);
        }

        public void InitChip(long[] chips)
        {
            //if(_chipBetManager == null) _chipBetManager = transform.Find("Bg/Chips").GetComponent<ChipsBetManager>();
            //_chipBetManager.InitChip(chips);
        }

        public IEnumerator UpdateMoneyWin(long money)
        {
            yield return new WaitForSeconds(2f);
            if (goTotalWin) goTotalWin.SetActive(true);
            txtMoneyWin.text = string.Format(money >= 0 ? "+{0}" : "{0}", MoneyHelper.FormatNumberAbsolute(money));
            //txtMoneyWin.gameObject.SetActive(true);
            //txtMoneyWin.transform.DOLocalMoveY(-420, 1);
            //txtMoneyWin.transform.DOScale(2f, 1);
            _playerManager.GetPlayerByPos(0).UpdateMoney(ClientConfig.UserInfo.GOLD);
            yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(8f));
            if (goTotalWin) goTotalWin.SetActive(false);
            //txtMoneyWin.gameObject.SetActive(false);
            //txtMoneyWin.transform.localPosition = new Vector2(-160, -250);
        }

        public void UpdateMyMoney(long cash)
        {
            RacingDogPlayerView RacingDogPlayerView = _playerManager.GetPlayerByPos(0);
            if (RacingDogPlayerView != null)
            {
                RacingDogPlayerView.UpdateMoney(cash);
            }
        }

        public void FillBasicPlayerInfo(int pos, string avatar, long cash, string nickname, int vip = 0)
        {
            RacingDogPlayerView RacingDogPlayerView = _playerManager.GetPlayerByPos(pos);
            if(RacingDogPlayerView != null)
            {
                RacingDogPlayerView.FillBasicInfo(nickname, cash, avatar, vip);
            }
        }

        public void ClearAllPlayerInfo()
        {
            _playerManager.ClearAllPlayerInfo();
        }

        public void RemovePlayer(int pos)
        {
            RacingDogPlayerView player = _playerManager.GetPlayerByPos(pos);
            if(player != null)
            {
                player.HidePlayer();
            }
        }
        
        public void OnSlectedInput(bool selected)
        {
            inputMoney.text = string.Empty;

            if (selected)
            {
                inputMoney.gameObject.SetActive(selected);
                inputMoney.Select();
            }
            else
            {
                inputMoney.DeactivateInputField();
                inputMoney.gameObject.SetActive(selected);
            }
        }
        
        public void UpdateCountTime(long remaintime, bool isYellow)
        {
            if (countDownTime) countDownTime.CountTime(remaintime, isYellow);
        }

        public void UpdateTotalMoney(int slotid, long totalmoney)
        {
            RacingDogDoorBettingView racingDogDoorManager = _bettingManager.GetDoorBetByPos(slotid);
            if (racingDogDoorManager != null) racingDogDoorManager.UpdateTotalMoney(totalmoney);
        }

        public void UpdateWinFactors(int id, float winFactors, short state)
        {
            RacingDogDoorBettingView racingDogDoorManager = _bettingManager.GetDoorBetByPos(id);
            if (racingDogDoorManager != null)
            {
                racingDogDoorManager.UpdateWinFactors(winFactors);
                racingDogDoorManager.SetStateFactor(state);
            }
        }

        public void UpdatMyBets(int slotid, long mybet)
        {
            RacingDogDoorBettingView racingDogDoorManager = _bettingManager.GetDoorBetByPos(slotid);
            if (racingDogDoorManager != null)
            {
                racingDogDoorManager.ResetMyBet();
                racingDogDoorManager.UpdateMyBet(mybet);
            }
            
        }

        public void UpdateMyBetOneDoor(long cash, int id)
        {
            Debug.Log("UpdateMyBetOneDoor: " + cash + " , id: " + id + " , Check Null: " + (_bettingManager.GetDoorBetByPos(id) == null));
            RacingDogDoorBettingView racingDogDoorManager = _bettingManager.GetDoorBetByPos(id);
            if (racingDogDoorManager != null) racingDogDoorManager.UpdateMyBet(cash);
        }

        public void UpdateToTalMoneyOneDoor(long cash, int id)
        {
            RacingDogDoorBettingView racingDogDoorManager = _bettingManager.GetDoorBetByPos(id);
            if (racingDogDoorManager != null) racingDogDoorManager.UpdateTotalMoney(cash);
        }

        private Vector3 GetPos(int pos)
        {
            RacingDogDoorBettingView doorBettingView = _bettingManager.GetDoorBetByPos(pos);
            if (doorBettingView != null) return doorBettingView.GetPos();
            return Vector3.zero;
        }

        public IEnumerator PlayerBetChip(int pos, int id, long total, long currentcash, Sprite sprChip)
        {
            RacingDogPlayerView RacingDogPlayerView = _playerManager.GetPlayerByPos(pos);
            if (RacingDogPlayerView != null)
            {
                _playerManager.GetPlayerByPos(pos).UpdateMoney(currentcash);
                StartCoroutine(IEMoveChipToDoorBet(id, _playerManager.GetPlayerByPos(pos).transform, new Vector2(0.3f,0.3f), sprChip));
                yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.5f));
                UpdateToTalMoneyOneDoor(total, id);
            }
        }

        public void PlayerChatInRacing(int pos, string txt)
        {
            RacingDogPlayerView player = _playerManager.GetPlayerByPos(pos);
            if (player != null)
                player.PlayerChat(txt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos">vị trí player bet</param>
        /// <param name="id">id cửa player đặt</param>
        /// <param name="total">tổng tiền tại cửa đó</param>
        /// <param name="currentcash">số tiền còn lại</param>
        /// <param name="chipbet">số tiền player bet</param>
        /// <returns></returns>
        public IEnumerator PlayerAllInChip(int pos, int id, long total, long currentcash, long chipbet)
        {
            RacingDogPlayerView RacingDogPlayerView = _playerManager.GetPlayerByPos(pos);
            if (RacingDogPlayerView != null)
            {
                _playerManager.GetPlayerByPos(pos).UpdateMoney(currentcash);
                Dictionary<long, long> dic = ConvertToSmallerCoin(chipbet);
                foreach(long key in dic.Keys)
                {
                    Sprite sprite = RacingDogView.Instance.GetSpriteChip(key);
                    for (int i = 0; i < dic[key]; i++)
                    {
                        StartCoroutine(IEMoveChipToDoorBet(id, _playerManager.GetPlayerByPos(pos).transform, new Vector2(0.3f, 0.3f), sprite));
                        yield return null;
                    }
                }
                yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.5f));
                UpdateToTalMoneyOneDoor(total, id);
            }
        }

        public IEnumerator MeAllIn(int id, long chipbet)
        {
            RacingDogPlayerView RacingDogPlayerView = _playerManager.GetPlayerByPos(0);
            if (RacingDogPlayerView != null)
            {
                Dictionary<long, long> dic = ConvertToSmallerCoin(chipbet);
                foreach (long key in dic.Keys)
                {
                    Sprite sprite = RacingDogView.Instance.GetSpriteChip(key);
                    for (int i = 0; i < dic[key]; i++)
                    {
                        StartCoroutine(IEMoveChipToDoorBet(id, _playerManager.GetPlayerByPos(0).transform, Vector2.one, sprite));
                        yield return null;
                    }
                }
                yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.5f));
            }
        }

        public void ResetDataDoorBet()
        {
            _bettingManager.ResetDataDoorBet();
        }

        //public Transform GetChipSelect()
        //{
        //    return _chipBetManager.GetChipSelect();
        //}

        public IEnumerator IEMoveChipToDoorBet(int pos, Transform oldparent, Vector2 scale, Sprite sprChip)
        {
            GameObject chip = ObjectPool.Spawn(ObjectPool.instance.startupPools[0].prefab, oldparent, Vector3.zero, Quaternion.identity);
            chip.transform.localScale = scale;
            chip.GetComponent<Image>().sprite = sprChip;
            chip.transform.SetParent(_bettingManager.GetDoorBetByPos(pos).transform);
            chip.transform.DOScale(0.3f, 0.5f).WaitForCompletion();
            yield return chip.transform.DOLocalMove(GetPos(pos), 0.5f).WaitForCompletion();
            yield return null;
            ObjectPool.Recycle(chip);
        }

        public IEnumerator IeJumpChip(GameObject chipObj, Vector3 toPos, Vector3 newPos, float dur)
        {
            StartCoroutine(chipObj.GetComponent<Image>().FadeIn(dur));
            yield return chipObj.transform.DOLocalJump(toPos, 10f, 1, dur).WaitForCompletion();
            chipObj.transform.localPosition = newPos;
            yield return null;
            ObjectPool.Recycle(chipObj);
        }

        private long[] arrMoney = new long[] { 100, 500, 1000, 2000, 5000, 10000, 50000, 100000, 500000, 1000000, 5000000, 10000000, 50000000, 1000000000 };
        private long CountChip = 0;

        public Dictionary<long, long> ConvertToSmallerCoin(long money)
        {
            Dictionary<long, long> result = new Dictionary<long, long>();
            //Dictionary<long, long> resultReturn = new Dictionary<long, long>();
            long temp = money;
            CountChip = 0;
            for (int i = arrMoney.Length - 1; i >= 0; i--)
            {
                if (temp / arrMoney[i] > 0)
                {
                    result.Add(arrMoney[i], temp / arrMoney[i]);
                    CountChip += temp / arrMoney[i];
                    temp = money % arrMoney[i];
                }
            }
            return result;
        }

        public void ShowResult(int index, int id)
        {
             RacingDogView.Instance.GetAtlasDog(imgResult[index], id);
        }

        public void ClearDoorbet()
        {
            if (_bettingManager) _bettingManager.ResetDataDoorBet();
        }

        public void InitSlotIdBetting(DogSlot[] dogSlots)
        {
            if (_bettingManager) _bettingManager.InitSlotIdBetting(dogSlots);
        }

        public void UpdateSlotIdBetting(DogSlot[] dogSlots, bool isMeClearBet)
        {
            for(int i = 0; i < dogSlots.Length; i++)
            {
                UpdateTotalMoney(dogSlots[i].SlotId, dogSlots[i].TotalBeting);
                SetStateFactor(dogSlots[i].SlotId, dogSlots[i].State);
                if(isMeClearBet) UpdatMyBets(dogSlots[i].SlotId, 0);

            }
        }

        public void ShowEffectWinDoor(int[] winslots)
        {
            if (_bettingManager) _bettingManager.ShowEffectWinDoor(winslots);
            ResetTotalMeBet();
        }

        private async void ResetTotalMeBet()
        {
            await Task.Delay(10000);
            if (RacingDogView.Instance) RacingDogView.Instance.ClearTotalbet();
        }

        private void SetStateFactor(int slotid, short state)
        {
            RacingDogDoorBettingView racingDogDoorManager = _bettingManager.GetDoorBetByPos(slotid);
            if (racingDogDoorManager != null) racingDogDoorManager.SetStateFactor(state);
        }

        public void OnBtnRequestClearBetClicked()
        {
            var isCanClearBet = _bettingManager.IsBetInSession();
            Debug.Log("isCanClearBet: " + isCanClearBet);
            if (isCanClearBet)
                RacingDogView.Instance.RequestClearBet();
        }
    }
}
