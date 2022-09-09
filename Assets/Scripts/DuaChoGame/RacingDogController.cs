using CoreBase.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;
using GameProtocol.DOG;
using GameProtocol.Protocol;
using Listener;
using Utilities.Custom;
using AppConfig;
using GameProtocol.ATH;
using GameProtocol.ACC;
using Broadcast;
using System.Linq;
using Game.Gameconfig;
using GameProtocol.MSG;

namespace Controller.GamePlay.DuaCho
{
    public class RacingDogController : ScreenController
    {

        private enum EGameState : byte
        {
            INNIT,
            WAITTING,
            BETTING,
            RACING,
            ENGAME,
            REVIEW,
            NONE
        }

        private EGameState GameState = EGameState.NONE;

        DogRacing[] dogRacing;

        Dictionary<string, PlayerObject> Players;
        Dictionary<int, bool> PlayerSits;

        int[] winslots;

        public RacingDogController(IView view) : base(view)
        {
        }

        public override void StartController()
        {
            Network.Network.AddSequenceCode(ClientGameConfig.RequestCode.DOG);
            base.StartController();
            Players = new Dictionary<string, PlayerObject>();
            InitPlayerSit();
            RequestGetDataDOG0();
            RequestGetChatDOG90();
        }

        void InitPlayerSit()
        {
            PlayerSits = new Dictionary<int, bool>()
            {
                {1, false },
                {2, false },
                {3, false },
                {4, false },
                {5, false },
                {6, false },
                {7, false },
                {8, false },
                {9, false },
                {10, false },
            };
        }

        public override void StopController()
        {
            Network.Network.RemoveSequenceCode(ClientGameConfig.RequestCode.DOG);
            base.StopController();
        }

        protected override void RegisterPushHandlers()
        {
            base.RegisterPushHandlers();
            Network.PushManager.Instance.RegisterPushHandler("DOG", this);
            Network.PushManager.Instance.RegisterPushHandler("ATH", this);
            Network.PushManager.Instance.RegisterPushHandler("ACC", this);
            Network.PushManager.Instance.RegisterPushHandler("AIG", this);
            BroadcastReceiver.Instance.AddMessenger(MessageCode.APP, this);
        }

        protected override void HandleNetworkDeath(object Msg)
        {
            base.HandleNetworkDeath(Msg);
        }

        protected override void HandleNetworkDisconnect(object Msg)
        {
            base.HandleNetworkDisconnect(Msg);
        }

        protected override void HandleNetworkLostSession(object Msg)
        {
            base.HandleNetworkLostSession(Msg);
        }

        protected override void HandleNetworkConnected(object Msg)
        {
            base.HandleNetworkConnected(Msg);
        }


        public override void UnregisterPushHandlers()
        {
            base.UnregisterPushHandlers();
            Network.PushManager.Instance.UnRegisterPushHandler("DOG", this);
            Network.PushManager.Instance.UnRegisterPushHandler("ATH", this);
            Network.PushManager.Instance.UnRegisterPushHandler("ACC", this);
            Network.PushManager.Instance.UnRegisterPushHandler("AIG", this);
            BroadcastReceiver.Instance.RemoveMessenger(MessageCode.APP, this);
        }

        protected override void HandleAppMessage(MessageType Type, object Msg)
        {
            base.HandleAppMessage(Type, Msg);
            switch (Type)
            {
                case MessageType.CashChanged:
                    if (GameState == EGameState.BETTING)
                        View.OnUpdateView("UpdateCashChange");
                    break;
            }
        }

        #region Request
        private void RequestGetDataDOG0()
        {
            DataListener dataListener = new DataListener(HandlerResponseDOG0);
            DOG0GetInfoRequest request = new DOG0GetInfoRequest();
            Network.Network.SendOperation(request, dataListener);
            GameState = EGameState.NONE;
        }

        private void RequestGetChatDOG90()
        {
            DataListener dataListener = new DataListener(HandlerResponseChatDOG90);
            DOG90ChatHistoriesRequest request = new DOG90ChatHistoriesRequest();
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestBetting(object[] param)
        {
            int dogid = (int)param[0];
            long cashbet = (long)param[1];
            DataListener dataListener = new DataListener(HandlerResponseDOG2);
            DOG2BettingRequest request = new DOG2BettingRequest()
            {
                SlotId = (int)dogid,
                CashBet = (int)cashbet
            };
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestOutGame(object[] param)
        {
            DataListener dataListener = new DataListener(HandlerResponseDOG1);
            DOG1LeaveGameRequest request = new DOG1LeaveGameRequest();
            Network.Network.SendOperation(request, dataListener);

            DataListener dataListenerATH = new DataListener(HandlerResponseATH3);
            ATH3_Request requestLogout = new ATH3_Request();
            Network.Network.SendOperation(requestLogout, dataListenerATH);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestClearBet(object[] param)
        {
            DataListener dataListener = new DataListener(HandlerResponseDOG20);
            DOG20ClearBettingRequest request = new DOG20ClearBettingRequest();
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void StartNetWork(object[] param)
        {
            Network.Network.StartNetwork();
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void StopNetWork(object[] param)
        {
            Network.Network.StopNetwork();
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void SendChat(object[] param)
        {
            string msg = (string)param[0];
            DataListener dataListener = new DataListener(HandlerResponseDOG9Chat);
            DOG9_ChatRequest request = new DOG9_ChatRequest()
            {
                Message = msg
            };
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void ResetView(object[] param)
        {
            MonoInstance.Instance.StopAllCoroutines();
            ResetGame();
        }
        #endregion


        #region Response
        private IEnumerator HandlerResponseDOG0(string coderun, Dictionary<byte, object> data)
        {
            DOG0GetInfoResponse response = new DOG0GetInfoResponse(data); 
            if (response.ErrorCode != (short)ReturnCode.OK)
            {
                yield break;
            }
            Debug.Log("response.DogRacings: " + response.DogRacings.Length + " , GameState: " + GameState + " ,response.GameState: " + response.GameState);
            InitPlayerSit();
            View.OnUpdateView("ShowStateGame", response.GameState, response.RemainTime);
            View.OnUpdateView("InitSlotIdBetting", new object[] { response.TotalBets });
            dogRacing = response.DogRacings;
            View.OnUpdateView("ShowLoading");
            switch (response.GameState)
            {
                case (byte)EGameState.INNIT:
                    break;
                case (byte)EGameState.WAITTING:
                    int length1 = response.Players.Length;
                    FillPlayerInfo(0, ClientConfig.UserInfo.AVATAR, ClientConfig.UserInfo.GOLD, ClientConfig.UserInfo.NICKNAME, EGameState.RACING);
                    for (int i = 0; i < length1; i++)
                    {
                        FillPlayerInfo(i + 1, response.Players[i].Avatar, response.Players[i].Cash, response.Players[i].Nickname, EGameState.RACING);
                    }
                    yield return new WaitForEndOfFrame();
                    View.OnUpdateView("CoutDownStartGame");
                    yield return MonoInstance.Instance.StartCoroutine(CoroutineUtil.WaitForRealSeconds(3f));
                    break;
                case (byte)EGameState.BETTING:
                    yield return MonoInstance.Instance.StartCoroutine(ProcessDataBetting(response));
                    break;
                case (byte)EGameState.RACING:
                    int length = response.Players.Length;
                    FillPlayerInfo(0, ClientConfig.UserInfo.AVATAR, ClientConfig.UserInfo.GOLD, ClientConfig.UserInfo.NICKNAME, EGameState.RACING);
                    for (int i = 0; i < length; i++)
                    {
                        FillPlayerInfo(i + 1, response.Players[i].Avatar, response.Players[i].Cash, response.Players[i].Nickname, EGameState.RACING);
                    }
                    yield return new WaitForEndOfFrame();
                    yield return MonoInstance.Instance.StartCoroutine(ProcessDataRacing(dogRacing, response.RemainTime, false));
                    yield return MonoInstance.Instance.StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.2f));
                    break;
                case (byte)EGameState.ENGAME:
                    yield return MonoInstance.Instance.StartCoroutine(ProcessDataEndGame());
                    break;
                case (byte)EGameState.REVIEW:
                    yield return MonoInstance.Instance.StartCoroutine(ProcessDataBetting(response));
                    break;
            }
            GameState = (EGameState)response.GameState;
            ////Debug.Log(("response.DogRacings: GameState : " + GameState);
            MonoInstance.Instance.StartCoroutine(ProcessDataQueue());
            yield return null;
        }

        private IEnumerator HandlerResponseDOG1(string coderun, Dictionary<byte, object> data)
        {
            DOG1LeaveGameResponse response = new DOG1LeaveGameResponse(data);

            View.OnUpdateView("ShowHome");
            if (response.ErrorCode != (short)ReturnCode.OK)
            {
                yield break;
            }
            yield return null;
        }

        private IEnumerator HandlerResponseChatDOG90(string coderun, Dictionary<byte, object> data)
        {
            DOG90ChatHistoriesResponse response = new DOG90ChatHistoriesResponse(data);

            if (response.ErrorCode != (short)ReturnCode.OK)
            {
                yield break;
            }
            if (response.Histories != null)
            {
                //foreach (DogChat chat in response.Histories)
                //    View.OnUpdateView("ShowChatHistorys", new object[] { chat.Nickname , chat.Message });
                View.OnUpdateView("ShowChatHistorys", new object[] { response.Histories });
            }
            yield return null;
        }


        private IEnumerator HandlerResponseDOG20(string coderun, Dictionary<byte, object> data)
        {
            DOG20ClearBettingResponse response = new DOG20ClearBettingResponse(data);

            if (response.ErrorCode != (short)ReturnCode.OK)
            {
                yield break;
            }
            yield return null;
        }

        private IEnumerator HandlerResponseDOG9Chat(string coderun, Dictionary<byte, object> data)
        {
            DOG9ChatResponse response = new DOG9ChatResponse(data);
            if (response.ErrorCode != (short)ReturnCode.OK)
            {
                yield break;
            }
            yield return null;
        }

        private IEnumerator HandlerResponseDOG2(string coderun, Dictionary<byte, object> data)
        {
            DOG2BettingResponse response = new DOG2BettingResponse(data);
            if (response.ErrorCode != (short)ReturnCode.OK)
            {
                ////Debug.Log(("ShowPopupError");

                View.OnUpdateView("ShowDialog", response.ErrorCode == 2 ? (byte)1 : (byte)0, response.ErrorMsg);
                yield break;
            }
            yield return null;
        }

        private IEnumerator HandlerResponseATH3(string coderun, Dictionary<byte, object> data)
        {
            ATH3_Response response = new ATH3_Response(data);
            yield return null;
        }

        #endregion

        #region Push
        /// <summary>
        /// Những Push nào về trước khi Init Xong bàn thì sẽ add vào Queue này và xử lý sau khi Init xong bàn game(COT1)
        /// </summary>
        List<IEnumerator> DataQueue = new List<IEnumerator>();

        /// <summary>
        /// Hàm này được gọi khi dựng SBI5 xong -> cuối hàm khi gán lại TableState ở Client thì chủ động gọi hàm này
        /// </summary>
        /// <returns></returns>
        IEnumerator ProcessDataQueue()
        {
            ////Debug.Log(("ProcessData: " + GameState);
            if (GameState != EGameState.INNIT)
            {
                IEnumerator ienum = DataQueue.GetEnumerator();
                while (ienum.MoveNext())
                {
                    yield return ienum.Current;
                }
                DataQueue.Clear();
            }
            yield return null;
        }

        public override IEnumerator HandlePush(string coderun, Dictionary<byte, object> data)
        {
            switch (coderun)
            {
                case "DOG0":
                    yield return MonoInstance.Instance.StartCoroutine(HandlerPushDOG0(data));
                    break;
                case "DOG2":
                    yield return MonoInstance.Instance.StartCoroutine(HandlerPushDOG2(data));
                    break;
                case "DOG3":
                    yield return MonoInstance.Instance.StartCoroutine(HanderPushDOG3(data));
                    break;
                case "DOG4":
                    yield return MonoInstance.Instance.StartCoroutine(HandlerPushDOG4(data));
                    break;
                case "DOG5":
                    yield return MonoInstance.Instance.StartCoroutine(HandlerPushDOG5(data));
                    break;
                case "DOG6":
                    yield return MonoInstance.Instance.StartCoroutine(HandlerPushDOG6(data));
                    break;
                case "DOG20":
                    yield return MonoInstance.Instance.StartCoroutine(HandlerPushDOG20ClearBet(data));
                    break;
                case "ATH0":
                    MonoInstance.Instance.StartCoroutine(OnPush_LoginOnOtherDeviceATH0(data));
                    break;
                case "ACC5":
                    MonoInstance.Instance.StartCoroutine(PushACC5ADDKOIN(data));
                    break;
                case "AIG9":
                    MonoInstance.Instance.StartCoroutine(HandlerPushDOG9Chat(data));
                    break;
            }
            yield return null;
        }

        private IEnumerator HandlerPushDOG0(Dictionary<byte, object> data)
        {
            if (GameState == EGameState.NONE)
            {
                DataQueue.Add(HandlerPushDOG0(data));
                yield break;
            }
            DOG0JoinGamePush push = new DOG0JoinGamePush(data);
            if (push.Player.Cash == -1)
            {
                if (Players.ContainsKey(push.Player.Nickname))
                {
                    if (GameState == EGameState.BETTING)
                        View.OnUpdateView("RemovePlayer", Players[push.Player.Nickname].POSITION);
                    else if (GameState == EGameState.RACING || GameState == EGameState.WAITTING)
                        View.OnUpdateView("RemovePlayerInRacing", Players[push.Player.Nickname].POSITION);
                    if (PlayerSits.ContainsKey(Players[push.Player.Nickname].POSITION)) PlayerSits[Players[push.Player.Nickname].POSITION] = false;
                }
            }
            else
            {
                int pos = Players.Count;
                try
                {
                    var listpos = PlayerSits.Where(p => p.Value == false);
                    if (listpos != null && listpos.Count() > 0)
                    {
                        //Debug.Log("HANDLE PUSH BSF0 : " + listpos.Count());
                        pos = listpos.FirstOrDefault().Key;
                        //Debug.Log("------------- POSITION: " + pos);
                    }
                }
                catch { }
                if (GameState == EGameState.BETTING)
                    FillPlayerInfo(Players.ContainsKey(push.Player.Nickname) ? Players[push.Player.Nickname].POSITION : pos, push.Player.Avatar, push.Player.Cash, push.Player.Nickname, EGameState.BETTING);
                else if (GameState == EGameState.RACING || GameState == EGameState.WAITTING)
                    FillPlayerInfo(Players.ContainsKey(push.Player.Nickname) ? Players[push.Player.Nickname].POSITION : pos, push.Player.Avatar, push.Player.Cash, push.Player.Nickname, EGameState.RACING);
            }
            yield return null;
        }

        private IEnumerator HandlerPushDOG2(Dictionary<byte, object> data)
        {
            if (GameState == EGameState.NONE || GameState != EGameState.BETTING)
            {
                DataQueue.Add(HandlerPushDOG2(data));
                yield break;
            }
            DOG2BettingPush push = new DOG2BettingPush(data);
            if (Players.ContainsKey(push.Nickname))
            {
                if (push.Nickname.Equals(ClientConfig.UserInfo.NICKNAME)) ClientConfig.UserInfo.GOLD = push.CurrentCash;
                Players[push.Nickname].MONEY = push.CurrentCash;
                ////Debug.Log(("HandlerPushDOG2");
                View.OnUpdateView("PlayerBettingChip", Players[push.Nickname].POSITION, push.DogId, push.CashBet, push.CurrentCash, push.TotalBet);
            }
            yield return null;
        }

        private bool isPush3 = false;
        private long cashwin = 0;
        private IEnumerator HanderPushDOG3(Dictionary<byte, object> data)
        {
            if (GameState == EGameState.NONE)
            {
                DataQueue.Add(HanderPushDOG3(data));
                yield break;
            }
            DOG3_Push push = new DOG3_Push(data);
            ClientConfig.UserInfo.GOLD = push.CurrentCash;
            isPush3 = true;
            cashwin = push.WinCash;
            yield return null;
        }

        private IEnumerator HandlerPushDOG4(Dictionary<byte, object> data)
        {
            if (GameState == EGameState.NONE)
            {
                DataQueue.Add(HandlerPushDOG4(data));
                yield break;
            }
            DOG4StartRacingPush push = new DOG4StartRacingPush(data);
            GameState = EGameState.RACING;
            //View.OnUpdateView("ShowStateGame", (byte)GameState, push.RemainTime);
            dogRacing = push.DogRacings;
            winslots = push.WinSlots;
            yield return MonoInstance.Instance.StartCoroutine(ProcessDataRacing(dogRacing, push.RemainTime, true));
            yield return MonoInstance.Instance.StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.5f));
        }

        private IEnumerator HandlerPushDOG5(Dictionary<byte, object> data)
        {
            if (GameState == EGameState.NONE)
            {
                DataQueue.Add(HandlerPushDOG5(data));
                yield break;
            }
            DOG5ChangeGameStatePush push = new DOG5ChangeGameStatePush(data);
            GameState = (EGameState)push.GameState;
            View.OnUpdateView("ShowStateGame", push.GameState, push.RemainTime);
            yield return null;
            switch (GameState)
            {
                case EGameState.WAITTING:
                    View.OnUpdateView("CoutDownStartGame");
                    View.OnUpdateView("ClearAllPlayerInfoInRacing");
                    yield return new WaitForEndOfFrame();
                    foreach (PlayerObject p in Players.Values)
                    {
                        FillPlayerInfo(p.POSITION, p.AVATAR, p.MONEY, p.UNAME, EGameState.RACING);
                    }
                    yield return MonoInstance.Instance.StartCoroutine(CoroutineUtil.WaitForRealSeconds(3f));
                    break;
                case EGameState.ENGAME:
                    yield return MonoInstance.Instance.StartCoroutine(ProcessDataEndGame());
                    yield return MonoInstance.Instance.StartCoroutine(CoroutineUtil.WaitForRealSeconds(4f));
                    break;
            }
            yield return null;
        }

        private IEnumerator HandlerPushDOG6(Dictionary<byte, object> data)
        {
            if (GameState == EGameState.NONE)
            {
                DataQueue.Add(HandlerPushDOG6(data));
                yield break;
            }
            ////Debug.Log(Error("HandlerPushDOG6");
            DOG6StartGamePush push = new DOG6StartGamePush(data);
            GameState = (EGameState)push.GameState;
            View.OnUpdateView("ShowStateGame", push.GameState, push.RemainTime);

            switch (GameState)
            {
                case EGameState.BETTING:
                    try
                    {
                        long[] money = new long[] { 0, 0, 0, 0, 0, 0 };
                        View.OnUpdateView("ClearAllPlayerInfo");
                        View.OnUpdateView("ClearDoorbet");
                        InitPlayerSit();
                        Players.Clear();
                        //View.OnUpdateView("UpdateTotalBet", money);
                        for (int i = 0; i < push.WinFactors.Length; i++)
                        {
                            View.OnUpdateView("UpdateWinFactorsEndGame", new object[] { push.WinFactors[i].SlotId, push.WinFactors[i].Factor, push.WinFactors[i].State });
                        }
                        //View.OnUpdateView("UpdateMoneyMyBet", new object[] { money });
                        FillPlayerInfo(0, ClientConfig.UserInfo.AVATAR, ClientConfig.UserInfo.GOLD, ClientConfig.UserInfo.NICKNAME, EGameState.BETTING);
                        int length = push.Players.Length;
                        for (int i = 0; i < length; i++)
                        {
                            FillPlayerInfo(i + 1, push.Players[i].Avatar, push.Players[i].Cash, push.Players[i].Nickname, EGameState.BETTING);
                        }
                        if (dogRacing != null)
                            View.OnUpdateView("UpdateResultBettingWhenEndGame", new object[] { dogRacing });
                        if (isPush3)
                        {
                            View.OnUpdateView("ShowMoneyWin", cashwin);
                            isPush3 = false;
                        }

                        if(winslots != null && winslots.Length > 0)
                        {
                            View.OnUpdateView("ShowEffectWinDoor", new object[] { winslots });
                            winslots = null;
                        }
                    }
                    catch { }
                    yield return MonoInstance.Instance.StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.25f));
                    break;
            }
            yield return null;
        }

        private IEnumerator HandlerPushDOG20ClearBet(Dictionary<byte, object> data)
        {
            if (GameState == EGameState.NONE)
            {
                DataQueue.Add(HandlerPushDOG20ClearBet(data));
                yield break;
            }
            DOG20ClearBettingPush push = new DOG20ClearBettingPush(data);
            bool isMeClearBet = false;
            if (Players.ContainsKey(push.Nickname))
            {
                if (ClientConfig.UserInfo.NICKNAME.Equals(push.Nickname))
                {
                    isMeClearBet = true;
                    ClientConfig.UserInfo.GOLD = push.CurrentCash;
                    Base.Utils.EventManager.Instance.RaiseEventInTopic(Base.Utils.EventManager.CHANGE_BALANCE);
                    FillPlayerInfo(0, ClientConfig.UserInfo.AVATAR, ClientConfig.UserInfo.GOLD, ClientConfig.UserInfo.NICKNAME, EGameState.BETTING);
                }
            }
            View.OnUpdateView("UpdateSlotIdBetting", new object[] { push.TotalBets, isMeClearBet });
            yield return null;
        }

        IEnumerator HandlerPushDOG9Chat(Dictionary<byte, object> data)
        {
            DOG9_ChatPush push = new DOG9_ChatPush(data);
            if (Players.ContainsKey(push.Nickname))
            {
                PlayerObject p = Players[push.Nickname];
                if (GameState == EGameState.BETTING)
                    View.OnUpdateView("PlayerChatInBeting", p.POSITION, push.Message, push.Nickname);
                //else if (GameState == EGameState.RACING || GameState == EGameState.WAITTING)
                //    View.OnUpdateView("PlayerChatInRacing", p.POSITION, push.Message);
            }
            //else
            //{
            //    if (GameState == EGameState.RACING || GameState == EGameState.WAITTING)
            //        View.OnUpdateView("OtherPlayerChatInRacing", push.Message);
            //}
            yield return null;
        }


        IEnumerator OnPush_LoginOnOtherDeviceATH0(Dictionary<byte, object> data)
        {
            ////Debug.Log(Error("OnPush_LoginOnOtherDevice");
            ATH1_Push push = new ATH1_Push(data);
            View.OnUpdateView("OnLoginOnOtherDeviceATH0", push.Message);
            yield return null;
        }

        IEnumerator PushACC5ADDKOIN(Dictionary<byte, object> data)
        {
            ////Debug.Log(Error("OnPush_LoginOnOtherDevice");
            MSG3_Notify_Push push = new MSG3_Notify_Push(data);

            if (push.Type == 0 && push.CurrentCash > 0)
            {
                PlayerPrefs.SetInt("playerCharging", 1);
                ClientConfig.UserInfo.GOLD = push.CurrentCash;
                Base.Utils.EventManager.Instance.RaiseEventInTopic(Base.Utils.EventManager.CHANGE_BALANCE);
            }
            View.OnUpdateView("Msg3Push", push.Type, push.Message);
            yield return null;
        }


        #endregion

        #region Process Logic Game

        private IEnumerator ProcessDataRacing(DogRacing[] dogRacings, long remaintime, bool isPush4)
        {
            //Debug.Log(("ProcessDataRacing");
            //for(int i = 0; i < dogRacings.Length; i++)
            //{
            //    Debug.Log("DOG: " + dogRacings[i].DogId + " , Cur Pos: " + dogRacings[i].Position  + " , Cur Segement: " + dogRacings[i].CurrentSegment);
            //    for(int j = 0; j < dogRacings[i].Segments.Length; j++)
            //    {
            //        Debug.Log("DOG: " + dogRacings[i].DogId + ", Segement: " + dogRacings[i].Segments[j].ToString());
            //    }
            //}

            View.OnUpdateView("UpdateRacingGame", new object[] { dogRacings, remaintime, isPush4 });
            yield return MonoInstance.Instance.StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.1f));
            View.OnUpdateView("StartRacingDog", new object[] { dogRacings, remaintime });
            yield return null;
        }


        private IEnumerator ProcessDataBetting(DOG0GetInfoResponse response)
        {
            try
            {
                for (int i = 0; i < response.TotalBets.Length; i++)
                {
                    View.OnUpdateView("UpdateTotalBet", new object[] { response.TotalBets[i].SlotId, response.TotalBets[i].TotalBeting });
                    View.OnUpdateView("UpdateWinFactors", new object[] { response.TotalBets[i].SlotId, response.TotalBets[i].Factor , response.TotalBets[i].State });
                }

                for (int i = 0; i < response.CurrentBets.Length; i++)
                {
                    View.OnUpdateView("UpdateMoneyMyBet", new object[] { response.CurrentBets[i].SlotId, response.CurrentBets[i].TotalBeting });
                }
               
                int length = response.Players.Length;
                FillPlayerInfo(0, ClientConfig.UserInfo.AVATAR, ClientConfig.UserInfo.GOLD, ClientConfig.UserInfo.NICKNAME, EGameState.BETTING);
                for (int i = 0; i < length; i++)
                {
                    FillPlayerInfo(i + 1, response.Players[i].Avatar, response.Players[i].Cash, response.Players[i].Nickname, EGameState.BETTING);
                }
                View.OnUpdateView("UpdateResultBetting", new object[] { response.DogRacings });
            }
            catch(System.Exception ex) {
                Debug.LogError("ProcessDataBetting Exeption: " + ex.Message);
                Debug.LogError("ProcessDataBetting Exeption: " + ex.StackTrace);
            }
            yield return MonoInstance.Instance.StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.25f));
        }

        private IEnumerator ProcessDataEndGame()
        {
            if (dogRacing != null) View.OnUpdateView("ShowResult", new object[] { dogRacing });
            yield return MonoInstance.Instance.StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.25f));
        }

        private void FillPlayerInfo(int pos, string avatar, long cash, string nickname, EGameState eGameState)
        {
            //Debug.LogError("FillPlayerInfo : " + nickname + " , pos: " + pos + ", ContainsKey: " + Players.ContainsKey(nickname) + " , eGameState: " + eGameState);
            try
            {
                if (cash == -1)
                {
                    if (Players.ContainsKey(nickname))
                    {
                        View.OnUpdateView("RemovePlayer", Players[nickname].POSITION);
                        if (PlayerSits.ContainsKey(Players[nickname].POSITION)) PlayerSits[Players[nickname].POSITION] = false;
                        Players.Remove(nickname);
                    }
                    return;
                }
                if (Players.ContainsKey(nickname))
                {
                    Players[nickname].POSITION = pos;
                    Players[nickname].InitData(nickname, cash, avatar);
                    if (PlayerSits.ContainsKey(Players[nickname].POSITION))
                        PlayerSits[Players[nickname].POSITION] = true;
                }
                else
                {
                    PlayerObject player = new PlayerObject()
                    {
                        MONEY = cash,
                        POSITION = pos
                    };
                    player.InitData(nickname, cash, avatar);
                    Players.Add(nickname, player);
                    if (PlayerSits.ContainsKey(Players[nickname].POSITION))
                        PlayerSits[Players[nickname].POSITION] = true;
                }
                if (eGameState == EGameState.RACING) View.OnUpdateView("FillBasicPlayerInfoInRacing", Players[nickname].POSITION, avatar, cash, nickname);
                else
                    View.OnUpdateView("FillBasicPlayerInfo", Players[nickname].POSITION, avatar, cash, nickname);

            }
            catch(System.Exception ex) {
                Debug.LogError("FillPlayerInfo  exeption: : " + ex.Message);
                Debug.LogError("FillPlayerInfo  exeption: : " + ex.StackTrace);
            }
        }

        #endregion
        protected override void HandleNetworkReconnected(object Msg)
        {
            base.HandleNetworkReconnected(Msg);
            ResetGame();
        }

        private void ResetGame()
        {
            //if(GameState == EGameState.BETTING)
            //    View.OnUpdateView("ResetData");
            GameState = EGameState.INNIT;
            View.OnUpdateView("ShowStateGame", GameState, 0);
            RequestGetDataDOG0();
        }
    }
}

public static class CoroutineUtil
{
    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }
}
