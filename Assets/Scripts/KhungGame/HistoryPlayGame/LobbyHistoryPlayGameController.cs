using CoreBase.Controller;
using GameProtocol.HIS;
using Interface;
using Listener;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.Home.History
{
    public class LobbyHistoryPlayGameController : UIController
    {
        public LobbyHistoryPlayGameController(IView view) : base(view)
        {
        }

        public override void StartController()
        {
            base.StartController();
        }

        public override void StopController()
        {
            base.StopController();
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestHistorys(object[] param)
        {
            HIS0_HistoriesRequest request = new HIS0_HistoriesRequest();
            request.CurrentPage = (byte)param[0];
            request.IsSave = (bool)param[1];
            DataListener dataListener = new DataListener(HandlerResponseHistorys);
            Network.Network.SendOperation(request, dataListener);
        }

        private IEnumerator HandlerResponseHistorys(string coderun, Dictionary<byte, object> data)
        {
            HIS0_HistoriesResponse response = new HIS0_HistoriesResponse(data);
            if(response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }
            //if(response.Histories.Length <= 0)
            //{
            //    FakeData();
            //    yield break;
            //}
            View.OnUpdateView("ShowHistorys", new object[] { response.Histories, response.TotalPage });
            yield return null;
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestRemoveOrAddColectionHistory(object[] param)
        {
            HIS2_SaveHistoryRequest request = new HIS2_SaveHistoryRequest();
            request.TableId = (int)param[0];
            request.Gamesession = (int)param[1];
            request.IsSave = (bool)param[2];
            //request.IsDelete = (bool)param[3];
            DataListener dataListener = new DataListener(HanlderResponseRemoveOrAdHistoryPKR17);
            Network.Network.SendOperation(request, dataListener);
        }

        private IEnumerator HanlderResponseRemoveOrAdHistoryPKR17(string coderun, Dictionary<byte, object> data)
        {
            HIS2_SaveHistoryResponse response = new HIS2_SaveHistoryResponse(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }
            View.OnUpdateView("AddOrRemoveSuccess");
            yield return null;
        }

        private void FakeData()
        {
            HIS0_HistoriesResponse response = new HIS0_HistoriesResponse();

            var histories = new List<PokerHistory>();
            for(int i = 0; i < 10; i++)
            {
                PokerHistory history = new PokerHistory();
                history.TableId = i;
                history.GameSeasion = i;
                history.GameId = "POKER";
                history.StartTime = DateTime.Now.ToString("dd/MM/yyy HH:mm:ss");
                history.Blind = 1000 * (i + 1);
                history.Ante = 50 * (i + 1);
                history.WinLoseCash = UnityEngine.Random.Range(-100000, 500000);
                history.IsSave = i % 2 == 0;
                history.HandCard = new int[] { 12, 13 };
                histories.Add(history);

            }

            response.Histories = histories.ToArray();
            response.TotalPage = 1;
            View.OnUpdateView("ShowHistorys", new object[] { response.Histories, response.TotalPage });
        }
    }
}
