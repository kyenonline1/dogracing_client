using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.GamePlay.DuaCho
{
    public class AtlasRacingDog : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] Dogs;
        [SerializeField]
        private Sprite[] sprTopDog;
        [SerializeField]
        private Sprite[] sprite;
        [SerializeField]
        private Sprite[] sprAvatars;


        Dictionary<long, int> dictionaryChip;

        // Use this for initialization
        private void Awake()
        {
            dictionaryChip = new Dictionary<long, int>()
            {
                {100 ,0 },
                {500 ,1 },
                {1000 ,2 },
                {2000 ,3 },
                {5000 ,4 },
                {10000 ,5 },
                {50000 ,6 },
                {100000 ,7 },
                {500000 ,8 },
                {1000000 ,9 },
                {5000000 ,10 },
                {10000000 ,11 },
                {50000000 ,12 },
                {1000000000 ,13 },
            };
        }

        public void GetDog(Image img, int dog)
        {
            if(img != null)
            {
                img.sprite = Dogs[dog];
            }
        }

        public void GetSprTopDog(Image img, int top)
        {
            if (img != null)
            {
                img.sprite = sprTopDog[top];
            }
        }

        public Sprite GetSprChip(long chip)
        {
            //Debug.Log(" -------------------------------: " + chip + " --- :" + dictionaryChip[chip]);
            if (dictionaryChip.ContainsKey(chip)) return sprite[dictionaryChip[chip]];
            return null;
        }

        public Sprite GetAvatar(int index)
        {
            if (index < sprAvatars.Length)
            {
                return sprAvatars[index];
            }
            return null;
        }
    }
}
