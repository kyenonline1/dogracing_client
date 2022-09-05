using Game.Gameconfig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilites;
using View.GamePlay.Slot;

namespace View.Home.Lobby
{
    public class ItemSlotView : MonoBehaviour
    {
        public GameObject objEvent;

        [SerializeField]
        EffectText[] txtJackpots;
        

        public void UpdateJackPot(long[] jackpot, bool isevent)
        {
            if (objEvent) objEvent.SetActive(isevent);
            for (int i = 0; i < txtJackpots.Length; i++)
            {
                txtJackpots[i].END_VALUE = jackpot[i];
                txtJackpots[i].Effect();
            }
            for (int i = 0; i < txtJackpots.Length; i++)
            {
                txtJackpots[i].START_VALUE = jackpot[i];
            }
        }
        
    }
}
