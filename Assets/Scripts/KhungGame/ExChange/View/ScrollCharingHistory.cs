using EnhancedUI.EnhancedScroller;
using GameProtocol.PAY;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace View.Common
{
    public class ScrollCharingHistory : MonoBehaviour, IEnhancedScrollerDelegate
    {

        public EnhancedScrollerCellView cellViewPrefab;
        public EnhancedScroller scroller;
        ChargingHistory[] datas;
        public delegate void ClickCharingCard(string id, string seri, string code, int amount);
        public ClickCharingCard dlgCharing;

        private void Awake()
        {
            datas = new ChargingHistory[0];
            scroller.Delegate = this;
        }

        public void InitData(ChargingHistory[] _datas)
        {
            datas = _datas;
            Debug.Log("INIT DATA: " + datas.Length);
            scroller.ReloadData();
        }


        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            ItemChargingHistory cellView = scroller.GetCellView(cellViewPrefab) as ItemChargingHistory;
            cellView.InitData(dataIndex + 1, datas[dataIndex].CreateTime, datas[dataIndex].Telco, datas[dataIndex].OrderId, datas[dataIndex].Amount, datas[dataIndex].Price, datas[dataIndex].Status);
            //cellView.dlgCharing = null;
            //cellView.dlgCharing += SendCharging;
            // return the cell to the scroller
            return cellView;
        }

        private void SendCharging(string id, string seri, string code, int amount)
        {
            if (this.dlgCharing != null) this.dlgCharing(id, seri, code, amount);
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
