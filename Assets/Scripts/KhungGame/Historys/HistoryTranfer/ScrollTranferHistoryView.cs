using EnhancedUI.EnhancedScroller;
using GameProtocol.DIS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.Home.History
{
    public class ScrollTranferHistoryView : MonoBehaviour, IEnhancedScrollerDelegate
    {

        public EnhancedScrollerCellView cellViewPrefab;
        public EnhancedScroller scroller;
        TranferHistory[] datas;

        private void Awake()
        {
            datas = new TranferHistory[0];
            scroller.Delegate = this;
        }
        public void InitData(TranferHistory[] _datas)
        {
            datas = _datas;
            scroller.ReloadData();
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            ItemHistoryTranferView cellView = scroller.GetCellView(cellViewPrefab) as ItemHistoryTranferView;
            cellView.SetData(dataIndex, datas[dataIndex].CreateTime, datas[dataIndex].UserTranfer, datas[dataIndex].UserReceived, datas[dataIndex].Amount, datas[dataIndex].Content);

            // return the cell to the scroller
            return cellView;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return (dataIndex % 2 == 0 ? 70f : 70f);
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return datas.Length;
        }
    }
}