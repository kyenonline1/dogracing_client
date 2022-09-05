using EnhancedUI.EnhancedScroller;
using GameProtocol.COU;
using GameProtocol.PAY;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace View.Common
{
    public class ScrollExChangeHistory : MonoBehaviour, IEnhancedScrollerDelegate
    {

        public EnhancedScrollerCellView cellViewPrefab;
        public EnhancedScroller scroller;
        CashoutHistory[] datas;

        private void Awake()
        {
            datas = new CashoutHistory[0];
            scroller.Delegate = this;
        }

        public void InitData(CashoutHistory[] _datas)
        {
            datas = _datas;
            Debug.Log("INIT DATA: " + datas.Length);
            scroller.ReloadData();
        }


        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            ItemExChangeHistory cellView = scroller.GetCellView(cellViewPrefab) as ItemExChangeHistory;
            cellView.InitData(dataIndex + 1, datas[dataIndex].TimeCashout, datas[dataIndex].Email, datas[dataIndex].Firstname + " " + datas[dataIndex].Lastname, datas[dataIndex].Amount, datas[dataIndex].Status);
            //cellView.dlgCharing = null;
            //cellView.dlgCharing += SendCharging;
            // return the cell to the scroller
            return cellView;
        }


        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return (dataIndex % 2 == 0 ? 72f : 72f);
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return datas.Length;
        }
    }
}

//public class ChargingHistory
//{
//    public string Seri;
//    public string Code;
//    public string Status;
//    public string Telco;
//    public int Amount;
//    public string TimeCharging;
//}

//public class ChargingHistorys
//{
//    public List<ChargingHistory> Histories;
//    public ChargingHistorys()
//    {
//        Histories = new List<ChargingHistory>();
//    }
//}
