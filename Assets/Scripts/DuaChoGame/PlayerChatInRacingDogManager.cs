using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace View.GamePlay.DuaCho
{
    public class PlayerChatInRacingDogManager : MonoBehaviour
    {

        [SerializeField]
        private PlayerChatInRacingDog[] Players;
        
        public PlayerChatInRacingDog OtherPlayer;

        public PlayerChatInRacingDog GetPlayerByPos(int pos)
        {
            if (pos < 0 || pos >= Players.Length) return null;
            return Players[pos];
        }

        public PlayerChatInRacingDog GetPlayerByName(string username)
        {
            var player = Players.Where(p => p.USERNAME != null && p.USERNAME.Equals(username));
            return player.FirstOrDefault();
        }

        public void ClearAllPlayerInfo()
        {
            //for (int i = 0; i < Players.Length; i++)
            //{
            //    if (Players[i] != null)
            //    {
            //        Players[i].HidePlayer();
            //    }
            //}
        }
    }
}
