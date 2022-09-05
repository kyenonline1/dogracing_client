using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Utilites;

namespace View.GamePlay.DuaCho
{
    public class RacingDogDoorBettingView : MonoBehaviour
    {

        public delegate void ClickDoor(int index);
        public ClickDoor dlgClickDoor;

        private Vector2 size;
        [SerializeField]
        private Text txtTotalBet,
            txtFactor,
            txtMyBet;
        [SerializeField] private GameObject goUp;
        [SerializeField] private GameObject goDown;
        [SerializeField] private Image bgDoor;
        [SerializeField] private GameObject goWinEffect;
        [SerializeField] private GameObject goWinOther;
        [SerializeField] private GameObject goMeWin;
        [SerializeField] private Text txtMeWinDoor;
        [SerializeField] private Color colorDisable;
        [SerializeField] private int slotId;
        private float factor;
        private long cacheMeBet;
        // Use this for initialization
        private void Awake()
        {
            size = GetComponent<RectTransform>().sizeDelta;
            GetComponent<UIMyButton>()._onClick.AddListener(ClickBetting);
            ResetData();
        }

        private void Start()
        {
        }
        public Vector2 GetPos()
        {
            Vector2 vec = new Vector2(Random.Range(-(size.x / 2) + 10, (size.x / 2) - 10), Random.Range(-(size.y / 2) + 10, (size.y / 2) - 10));
            return vec;
        }

        public int SlotID
        {
            set
            {
                slotId = value;
            }
            get
            {
                return slotId;
            }
        }

        public void BlurDoor()
        {
            if (bgDoor) bgDoor.color = colorDisable;
            if (goWinEffect) goWinEffect.SetActive(false);
            if (goMeWin) goMeWin.SetActive(false);
            if (goWinOther) goWinOther.SetActive(false);
            SetNormal();
        }

        private async void SetNormal()
        {
            await Task.Delay(10000);
            if (bgDoor) bgDoor.color = Color.white;
            if (goWinEffect) goWinEffect.SetActive(false);
            if (goMeWin) goMeWin.SetActive(false);
            if (goWinOther) goWinOther.SetActive(false);

        }

        public void ActiveWinDoor()
        {
            if (bgDoor) bgDoor.color = Color.white;
            Debug.Log("Active windoor: " + slotId + ", cacheMeBet: " + cacheMeBet);
            if(cacheMeBet > 0)
            {
                if (goWinOther) goWinOther.SetActive(false);
                if (goWinEffect) goWinEffect.SetActive(true);
                Debug.Log("factor: " + factor + ", moneywin: " + (cacheMeBet * factor));
                if (goMeWin) goMeWin.SetActive(true);
                if (txtMeWinDoor) txtMeWinDoor.text = MoneyHelper.FormatRelativelyWithoutUnit((long)(cacheMeBet * factor));
                cacheMeBet = 0;
            }
            else
            {
                if (goWinOther) goWinOther.SetActive(true);
            }
            SetNormal();
        }

        public long Mybet { get => mybet; set => mybet = value; }

        public void UpdateWinFactors(float winFactors)
        {
            factor = winFactors;
            txtFactor.text = string.Format("x{0}", winFactors);
        }

        public void SetStateFactor(short state)
        {
            // -1 : down, 0 : normal, 1 : up
            if (goDown) goDown.SetActive(state == -1);
            if (goUp) goUp.SetActive(state == 1);
        }

        public void UpdateTotalMoney(long money)
        {
            txtTotalBet.text = MoneyHelper.FormatRelativelyWithoutUnit(money);
        }

        private long mybet = 0;
        public void UpdateMyBet(long money)
        {
            Mybet += money;
            if (Mybet > 0)
                txtMyBet.text = MoneyHelper.FormatRelativelyWithoutUnit(Mybet);
            else txtMyBet.text = string.Empty;
        }
        public void ResetMyBet()
        {
            Mybet = 0;
        }

        public void ResetData()
        {
            cacheMeBet = Mybet;
            Mybet = 0;
            txtTotalBet.text = "0";
            txtMyBet.text = string.Empty;
        }

        public void ClickBetting()
        {
            if (dlgClickDoor != null) dlgClickDoor(slotId);
        }
    }
}
