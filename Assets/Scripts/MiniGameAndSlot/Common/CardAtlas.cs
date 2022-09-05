using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Card
{
	public class CardAtlas : MonoBehaviour {

        public static CardAtlas Instance;

        [SerializeField]
		protected Sprite[] Cards;
        [SerializeField]
        private Sprite cardHide;

        [SerializeField] private Sprite[] sprActions;

        [SerializeField] private Sprite[] sprAvatars;

        protected Dictionary<string, Sprite> CardMap = new Dictionary<string, Sprite>();

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        /// 
        private void Awake()
        {
            Instance = this;
        }

        void Start()
		{
			foreach(var card in Cards)
			{
				CardMap[card.name] = card;
			}
            MappingCard();
        }

		public virtual Sprite GetCard(string name)
		{
			if(CardMap.ContainsKey(name))
				return CardMap[name];
			return null;
		}

		public virtual Sprite GetCard(int id)
		{
			return GetCard("Cards_" + id);
		}

        protected Dictionary<int, int> mappingCard = new Dictionary<int, int>();

        protected virtual void MappingCard()
        {
            mappingCard.Add(1, 12); // ID = 11 errror
            mappingCard.Add(2, 0);
            mappingCard.Add(3, 1);
            mappingCard.Add(4, 2);
            mappingCard.Add(5, 3);
            mappingCard.Add(6, 4);
            mappingCard.Add(7, 5);
            mappingCard.Add(8, 6);
            mappingCard.Add(9, 7);
            mappingCard.Add(10, 8);
            mappingCard.Add(11, 9);
            mappingCard.Add(12, 10);
            mappingCard.Add(13, 11);
            mappingCard.Add(14, 12);
        }

        public virtual Sprite ConvertToSprite(int id)
        {
            if (id > 0)
            {
                int rank = id / 10;
                int suit = id % 10;
                //Debug.Log("Rank: " + rank + " , Suit: " + suit + ", id= " + id);
                string sprName = string.Format("Cards_{0}", mappingCard[rank] + suit * 13);
                return GetCard(sprName);
            }
            else return cardHide;
        }

        /// <summary>
        /// 0: Fold, 1: Check, 2: Call, 3: Raise, 4: AllIn, 5: Small Blind, 6: Big blind
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Sprite GetSpriteActions(int index)
        {
            if (index > sprActions.Length) return sprActions[0];
            return sprActions[index];
        }

        public Sprite GetAvatar(int index)
        {
            if (index < sprAvatars.Length)
            {
                return sprAvatars[index];
            }
            return null;
        }

        private void OnDestroy()
        {
            Destroy(Instance);
        }
    }
}
