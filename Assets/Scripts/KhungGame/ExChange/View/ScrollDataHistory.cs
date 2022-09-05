using EnhancedUI.EnhancedScroller;
using GameProtocol.COU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View.Home.ExChange;

public class ScrollDataHistory : MonoBehaviour, IEnhancedScrollerDelegate
{
    public EnhancedScrollerCellView cellViewPrefab;
    public EnhancedScroller scroller;
    CashoutHistory[] datas;
    public delegate void ClickCharingCard(int transaction);
    public ClickCharingCard dlgCharing;

    public delegate void ClickGetDetail(int transaction, string id, string name, string seri, string code, int amount);
    public ClickGetDetail dlgGetDetail;
    

    private void Awake()
    {
        datas = new CashoutHistory[0];
        scroller.Delegate = this;
    }

    public void InitData(CashoutHistory[] _datas)
    {
        datas = _datas;
        //Debug.Log("INIT DATA: " + datas.Length);
        scroller.ReloadData();
    }
    

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        ItemHistoryView cellView = scroller.GetCellView(cellViewPrefab) as ItemHistoryView;
        //cellView.SetData(dataIndex + 1, datas[dataIndex].ItemName, datas[dataIndex].TimeCashout, datas[dataIndex].Status, datas[dataIndex].Seri, datas[dataIndex].NumberCard, datas[dataIndex].ItemId, datas[dataIndex].TransId, datas[dataIndex].Amount);
        //cellView.dlgCharing = null;
        //cellView.dlgCharing += SendCharging;
        //cellView.dlgCardDetail = null;
        //cellView.dlgCardDetail += GetCardDetail;
        // return the cell to the scroller
        return cellView;
    }

    private void SendCharging(int transaction)
    {
        if (this.dlgCharing != null) this.dlgCharing(transaction);
    }

    private void GetCardDetail(int transaction, string id, string name, string seri, string code, int amount)
    {
        if (this.dlgGetDetail != null) this.dlgGetDetail(transaction, id, name, seri, code, amount);
    }
    
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return (dataIndex % 2 == 0 ? 30f : 100f);
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return datas.Length;
    }
}
