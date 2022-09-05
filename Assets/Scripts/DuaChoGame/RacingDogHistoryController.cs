using CoreBase.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;
using Listener;
using GameProtocol.DOG;
using GameProtocol.Protocol;

namespace Controller.GamePlay.DuaCho
{
    public class RacingDogHistoryController : UIController
    {

        DogRacingHistory[] dogRacingHistory;
        DogRacingHistoryByUser[] dogRacingHistoryByUser;

        public RacingDogHistoryController(IView view) : base(view)
        {
        }

        public override void StartController()
        {
            base.StartController();
            RegisterEventHandler(RequestGetHistorySession);
            RegisterEventHandler(RequestGetHistoryBetting);
            RegisterEventHandler(ClearData);
        }

        public override void StopController()
        {
            base.StopController();
        }

        private void RequestGetHistorySession(object[] param)
        {
            if (dogRacingHistory != null)
            {
                View.OnUpdateView("ShowError");
                return;
            }
            DataListener dataListener = new DataListener(IEHanderPushSession);
            DOG7DogRacingHistoriesRequest request = new DOG7DogRacingHistoriesRequest();
            Network.Network.SendOperation(request, dataListener);
        }

        private void RequestGetHistoryBetting(object[] param)
        {
            if (dogRacingHistoryByUser != null)
            {
                View.OnUpdateView("ShowError");
                return;
            }
            DataListener dataListener = new DataListener(IEHanderPushBetting);
            DOG8BettingHistoriesRequest request = new DOG8BettingHistoriesRequest();
            Network.Network.SendOperation(request, dataListener);
        }

        private void ClearData(object[] param)
        {
            dogRacingHistory = null;
            dogRacingHistoryByUser = null;
        }

        private IEnumerator IEHanderPushSession(string coderun, Dictionary<byte, object> data)
        {
            DOG7DogRacingHistoriesResponse response = new DOG7DogRacingHistoriesResponse(data);
            if (response.ErrorCode != (short)ReturnCode.OK)
            {
                View.OnUpdateView("ShowError");
                yield break;
            }
            dogRacingHistory = response.Histories;
            View.OnUpdateView("ShowItemSession", new object[] { dogRacingHistory });
            yield return null;
        }

        private IEnumerator IEHanderPushBetting(string coderun, Dictionary<byte, object> data)
        {
            DOG8BettingHistoriesResponse response = new DOG8BettingHistoriesResponse(data);
            if (response.ErrorCode != (short)ReturnCode.OK)
            {
                View.OnUpdateView("ShowError");
                yield break;
            }
            dogRacingHistoryByUser = response.Histories;
            View.OnUpdateView("ShowItemBetting", new object[] { dogRacingHistoryByUser });
            yield return null;
        }

    }
}