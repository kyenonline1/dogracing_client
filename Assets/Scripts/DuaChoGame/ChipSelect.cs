using Game.Gameconfig;
using UnityEngine;
using View.GamePlay.DuaCho;

namespace View.GamePlay.TaiXiu
{
    public class ChipSelect : MonoBehaviour
    {

        public delegate void SelectChip(int index);
        public SelectChip dlgSelectChip;

        private int index;

        private Transform tranSelect;

        // Use this for initialization
        private void Awake()
        {
            tranSelect = transform.Find("chipselect");

            GetComponent<UIMyButton>()._onClick.AddListener(ClickSelect);
        }


        public int INDEX
        {
            set
            {
                index = value;
            }
        }
        public void SelectedChip(bool isSelect)
        {
            tranSelect.gameObject.SetActive(isSelect);
        }

        private void ClickSelect()
        {
           // Debug.Log("ClickSelect : " + index + " ,GameConfig.GameID.CURRENT_GAME_ID: " + GameConfig.GameID.CURRENT_GAME_ID);
           
            if(ClientGameConfig.GAMEID.CURRENT_GAME_ID == ClientGameConfig.GAMEID.DOG_RACING)
            {
                if (dlgSelectChip != null) dlgSelectChip(index);
                if (RacingDogView.Instance != null) RacingDogView.Instance.CHIP_BET = index;
            }
            else
            {
                if (dlgSelectChip != null) dlgSelectChip(index);
            }
        }

    }
}
