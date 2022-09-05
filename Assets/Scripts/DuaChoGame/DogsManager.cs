using Base.Utils;
using GameProtocol.DOG;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace View.GamePlay.DuaCho
{
    public class DogsManager : MonoBehaviour
    {
        public DogRunControl[] Dogs;
        public Transform[] rankDogs;

        //private DogRacing[] dogRacing;

        private Vector2[] posDog;

        private void Awake()
        {
            posDog = new Vector2[]
            {
                new Vector2(650,-4),
                new Vector2(370,-4),
                new Vector2(100,-4),
                new Vector2(-175,-4),
                new Vector2(-450,-4),
                new Vector2(-725,-4),
            };
            EventManager.Instance.SubscribeTopic(EventManager.SCREEN_SHORT, ScreenShortGame);
        }

        public DogRunControl GetDog(int index)
        {
            if (index < 0 || index > 5) return null;
            return Dogs[index];
        }

        public void ResetSetRankDog(DogRacing[] _dogRacing)
        {
            //dogRacing = _dogRacing;
            for(int i = 0; i < 6; i++)
            {
                rankDogs[i].localPosition = posDog[i];
            }
        }

        public void UpdateRankingDog()
        {
            StartCoroutine(IeUpdateRankDog());
        }

        IEnumerator IeUpdateRankDog()
        {
            while (isWhile)
            {
                var newDogs = Dogs.OrderBy(dog => dog.transform.localPosition.x);
                for (int i = 0; i < 6; i++)
                {
                    newDogs.ElementAt<DogRunControl>(i).TweenRankDog(posDog[5 - i].x);
                }
                yield return new WaitForSeconds(0.25f);
            }
        }

        private void ScreenShortGame()
        {
            isWhile = false;
            this.StopAllCoroutines();
        }

        private bool isWhile;
        private void OnEnable()
        {
            isWhile = true;
        }

        private void OnDisable()
        {
            isWhile = false;
        }

        private void OnDestroy()
        {
            EventManager.Instance.UnSubscribeTopic(EventManager.SCREEN_SHORT, ScreenShortGame);
        }
    }
}
