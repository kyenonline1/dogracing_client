using Base.Utils;
using Game.Gameconfig;
using UnityEngine;
using UnityEngine.UI;
using Utilites;
using Utilities;

namespace View.Home.Lobby
{
    public class ItemBlindView : MonoBehaviour
    {

        public delegate void dlgClickBlind(int i);
        public dlgClickBlind EventClickBlind;

        public Text txtBlind;
        //public Image iconVip,
        //    iconNomarl;
        private int blind;

        // Use this for initialization
        private void Awake()
        {
            EventManager.Instance.SubscribeTopic(EventManager.CHANGE_CASH_TYPE_LOBBY, ChangeCashType);
        }

        private void OnEnable()
        {
            ChangeCashType();
        }
        void Start()
        {
        }

        private void ChangeCashType()
        {
            //Debug.Log("ChangeCashType: " + GameConfig.CASH_TYPE.CASHTYPE);
            //iconVip.gameObject.SetActive(
            //    GameConfig.CASH_TYPE.CASHTYPE == 1);
            //iconNomarl.gameObject.SetActive(
            //    GameConfig.CASH_TYPE.CASHTYPE == 0);
        }

        public void SetData(int blind)
        {
            this.blind = blind;
            txtBlind.text = MoneyHelper.FormatNumberAbsolute(blind);
        }

        public void BtnClickBlind()
        {
            LogMng.Log("BtnClickBlind: " ,blind);
            if (EventClickBlind != null) EventClickBlind(blind);
        }

        private void OnDestroy()
        {
            EventManager.Instance.UnSubscribeTopic(EventManager.CHANGE_CASH_TYPE_LOBBY, ChangeCashType);
        }
    }
}
