using GameProtocol.DOG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace View.GamePlay.DuaCho
{
    public class RacingDogDoorManager : MonoBehaviour
    {
        [SerializeField]
        private RacingDogDoorBettingView[] racingDogDoorBettingViews;
        [SerializeField]
        private RacingDogDoorBettingView[] racingDogDoorBettingLeftViews;

        private void Awake()
        {
            for(int i = 0; i < racingDogDoorBettingViews.Length; i++)
            {
                racingDogDoorBettingViews[i].dlgClickDoor = DoorBettingClick;
            }
            for (int i = 0; i < racingDogDoorBettingLeftViews.Length; i++)
            {
                racingDogDoorBettingLeftViews[i].dlgClickDoor = DoorBettingClick;
            }
        }


        public void SetSlotId()
        {

        }

        public RacingDogDoorBettingView GetDoorBetByPos(int slotid)
        {
            if(slotid > 100)
            {
                return racingDogDoorBettingViews.Where(d => d.SlotID == slotid).FirstOrDefault();
            }
            else
            {
                return racingDogDoorBettingLeftViews.Where(d => d.SlotID == slotid).FirstOrDefault();
            }
          
        }


        public void DoorBettingClick(int index)
        {
            //Debug.Log("Click Betting Door: " + index);
            if(RacingDogView.Instance != null)
            {
                RacingDogView.Instance.RequestBetting(index);
            }
        }

        public void ResetDataDoorBet()
        {
            for (int i = 0; i < racingDogDoorBettingViews.Length; i++)
            {
                racingDogDoorBettingViews[i].ResetData();
            }
            for (int i = 0; i < racingDogDoorBettingLeftViews.Length; i++)
            {
                racingDogDoorBettingLeftViews[i].ResetData();
            }
        }

        public void UpdateSlotIdBetting(DogSlot[] dogSlots)
        {

        }

        public void ShowEffectWinDoor(int[] winslots)
        {

            var rightId = winslots.Where(s => s > 100).ToArray();
            if(rightId != null && rightId.Length > 0)
            {
                for(int i = 0; i < racingDogDoorBettingViews.Length; i++)
                {
                    for(int j = 0; j < rightId.Length; j++)
                    {
                        //Debug.Log("SlotID: " + racingDogDoorBettingViews[i].SlotID + " , rightId: " + rightId[j]);
                        if(racingDogDoorBettingViews[i].SlotID == rightId[j])
                        {
                            racingDogDoorBettingViews[i].ActiveWinDoor();
                            break;
                        }
                        else
                        {
                            racingDogDoorBettingViews[i].BlurDoor();
                        }
                    }
                }
            }

            var leftId = winslots.Where(s => s <= 100).ToArray();
            if (leftId != null && leftId.Length > 0)
            {
                for (int i = 0; i < racingDogDoorBettingLeftViews.Length; i++)
                {
                    for (int j = 0; j < leftId.Length; j++)
                    {
                        if (racingDogDoorBettingLeftViews[i].SlotID == leftId[j])
                        {
                            racingDogDoorBettingLeftViews[i].ActiveWinDoor();
                            break;
                        }
                        else
                        {
                            racingDogDoorBettingLeftViews[i].BlurDoor();
                        }
                    }
                }
            }
        }

        public void InitSlotIdBetting(DogSlot[] dogSlots)
        {
            Debug.Log("Totasl Slot: " + dogSlots.Length);
            var leftSlot = dogSlots.Where(s => s.SlotId < 100).OrderBy(s => s.SlotId).ToArray();
            if(leftSlot != null)
            {
                Debug.Log("Totasl leftSlot: " + leftSlot.Length);
                for (int i = 0; i < leftSlot.Length; i++)
                {
                    racingDogDoorBettingLeftViews[i].SlotID = leftSlot[i].SlotId;
                    racingDogDoorBettingLeftViews[i].UpdateWinFactors(leftSlot[i].Factor);
                }
            }

            var rightSlot = dogSlots.Where(s => s.SlotId > 100).OrderBy(s => s.SlotId).ToArray();
            if (rightSlot != null)
            {
                Debug.Log("Totasl rightSlot: " + rightSlot.Length);
                for (int i = 0; i < rightSlot.Length; i++)
                {
                    racingDogDoorBettingViews[i].SlotID = rightSlot[i].SlotId;
                    racingDogDoorBettingViews[i].UpdateWinFactors(leftSlot[i].Factor);
                }
            }
        }

        public bool IsBetInSession()
        {
            var isRightBet = racingDogDoorBettingViews.Any(d => d.Mybet > 0);
            Debug.Log("isRightBet: " + isRightBet);
            if (isRightBet) return true;
            var isLeftBet = racingDogDoorBettingLeftViews.Any(d => d.Mybet > 0);
            Debug.Log("isLeftBet: " + isLeftBet);
            if (isLeftBet) return true;
            return false;
        }
    }
}
