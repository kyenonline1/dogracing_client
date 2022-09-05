using AppConfig;
using AssetBundles;
using GameProtocol.UDT;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities.Custom;
using Utilitis.Command;

using GameProtocol.PAY;
using GameProtocol.OTP;
using GameProtocol.MSG;
using Base.Utils;
using CoreBase.Controller;
using Interface;
using GameProtocol.ATH;
using Listener;
using Broadcast;
using GameProtocol.Protocol;
using GameProtocol.XIT;
using GameProtocol.PKR;
using GameProtocol.MAU;
using Game.Gameconfig;

namespace Controller.Lobby
{
    public class LobbyController : ScreenController
    {
        GameConfig[] games;

        BlindInfo[] blindInfoLobby;

        public override void StartController()
        {
            base.StartController();
        }

        public override void StopController()
        {
            base.StopController();
        }
        

        #region Update Controller to View

        public LobbyController(IView view) : base(view)
        {
        }

        public override IEnumerator HandlePush(string coderun, Dictionary<byte, object> data)
        {
            switch (coderun)
            {
                case "MSG2":
                    HandlerPushMessageMSG2(data);
                    break;
                case "MSG3":
                    HandlerPushMSG3(data);
                    break; 
                case "ATH1":
                    IEHandlerPushLGI_1LogoutOtherDevice(data);
                    break;
                case "MAU1":
                    HandlerPushJoinMauBinhMAU1(data);
                    break;
                case "SBI_PKR2":
                    HandlerPushJoinPokerPKR_SBI2(data);
                    break;
                case "TMN1":
                    HandlerPushJoinTLMN(data);
                    break;
                case "SBI_LIE2":
                    HandlerPushJoinLieng(data);
                    break;
                case "PHO1":
                    HandlerPushJoinTala(data);
                    break;
                case "SBI_XIT2":
                    HandlerPushJoinXITO(data);
                    break;
                case "TOU2":
                    break;

            }
            return base.HandlePush(coderun, data);
        }

        protected override void RegisterPushHandlers()
        {
            base.RegisterPushHandlers();
            Network.PushManager.Instance.RegisterPushHandler("ATH", this);
            Network.PushManager.Instance.RegisterPushHandler("MSG", this);
            Network.PushManager.Instance.RegisterPushHandler("SBI", this);
            Network.PushManager.Instance.RegisterPushHandler("PKR", this);
            Network.PushManager.Instance.RegisterPushHandler("TMN", this);
            Network.PushManager.Instance.RegisterPushHandler("LIE", this);
            Network.PushManager.Instance.RegisterPushHandler("PHO", this);
            Network.PushManager.Instance.RegisterPushHandler("XIT", this);
            Network.PushManager.Instance.RegisterPushHandler("MAU", this);
            Network.PushManager.Instance.RegisterPushHandler("TOU", this);
            BroadcastReceiver.Instance.AddMessenger(MessageCode.APP, this);
        }

        public override void UnregisterPushHandlers()
        {
            base.UnregisterPushHandlers();
            Network.PushManager.Instance.UnRegisterPushHandler("ATH", this);
            Network.PushManager.Instance.UnRegisterPushHandler("MSG", this);
            Network.PushManager.Instance.UnRegisterPushHandler("SBI", this);
            Network.PushManager.Instance.UnRegisterPushHandler("PKR", this);
            Network.PushManager.Instance.UnRegisterPushHandler("TMN", this);
            Network.PushManager.Instance.UnRegisterPushHandler("LIE", this);
            Network.PushManager.Instance.UnRegisterPushHandler("PHO", this);
            Network.PushManager.Instance.UnRegisterPushHandler("XIT", this);
            Network.PushManager.Instance.UnRegisterPushHandler("MAU", this);
            Network.PushManager.Instance.UnRegisterPushHandler("TOU", this);
        }

        protected override void UnregisterMessengers()
        {
            base.UnregisterMessengers();
            BroadcastReceiver.Instance.RemoveMessenger(MessageCode.APP, this);
        }

        protected override void RegisterMessengers()
        {
            base.RegisterMessengers();
        }


        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void StartConnect(object[] param)
        {
            Network.Network.StartNetwork();
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void StopNetwork(object[] param)
        {
            Network.Network.StopNetwork();
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestLogOut(object[] param)
        {
            DataListener dataListener = new DataListener(HandlerResbonseBase);
            ATH3_Request request = new ATH3_Request();
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestGetListGame(object[] param)
        {
            ATH8_Request request = new ATH8_Request();
            DataListener dataListener = new DataListener(HandlerResponseListGameATH8);
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void GetListTable(object[] param)
        {
            if (games == null) return;
            string gameid = (string)param[0];
            //Debug.Log("GetListTable: " + gameid);
            var game = games.Where(g => g.GameId.Equals(gameid)).FirstOrDefault();
            if (game != null)
            {
                View.OnUpdateView("ShowListTable", new object[] { game.Blind, game.Gamename });
            }
            else
            {
                RequestGetListGame(null);
            }
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestListTableLobbyATH4(object[] param)
        {
            if(blindInfoLobby == null || blindInfoLobby.Length == 0)
            {
                RequestATH4GetTable();
                return;
            }
            //View.OnUpdateView("ShowListTableLobbyPanel", new object[] { blindInfoLobby });
        }

        private void RequestATH4GetTable()
        {
            ATH4GetBlindsInfoRequest request = new ATH4GetBlindsInfoRequest();
            DataListener dataListener = new DataListener(HandlerResponeListTable);
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void ClickTableLobbyPanel(object[] param)
        {
            int index = (int)param[0];
            if (blindInfoLobby == null || index >= blindInfoLobby.Length) return;

            var table = blindInfoLobby[index];
            if (table.Active)
            {
                if(ClientConfig.UserInfo.GOLD < table.MinCashIn)
                {
                    View.OnUpdateView("ShowNotEnoughMoney");
                    return;
                }
                //Game.Gameconfig.ClientGameConfig.LOBBY_TYPE.MinCashIn = table.MinCashIn;
                RequestJoinGame(new object[] { table.Blind, (byte)1 });
            }
        }

        //[HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        //private void RequestTOU0GetListTable(object[] param)
        //{
        //    TOU0GetToursRequest request = new TOU0GetToursRequest();
        //    request.Cate = (int)Game.Gameconfig.ClientGameConfig.LOBBY_TYPE.lobbySpinTour;
        //    DataListener dataListener = new DataListener(HandlerResponeTOU0ListTableSpinTour);
        //    Network.Network.SendOperation(request, dataListener);
        //}

       
        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestJoinGame(object[] param)
        {
            //Debug.Log("DRequestJoinGame");
            View.OnUpdateView("ShowLoading");
            int blind = (int)param[0];
            byte cashtype = (byte)param[1];
            switch (Game.Gameconfig.ClientGameConfig.LOBBY_TYPE.Lobby_type)
            {
                case ClientGameConfig.LOBBY_TYPE.LobbyType.LOBBY:
                    Game.Gameconfig.ClientGameConfig.GAMEID.CURRENT_GAME_ID = "POKER";
                    break;
                case ClientGameConfig.LOBBY_TYPE.LobbyType.SPINUP:
                    Game.Gameconfig.ClientGameConfig.GAMEID.CURRENT_GAME_ID = "SPIN";
                    break;
                case ClientGameConfig.LOBBY_TYPE.LobbyType.TOURNAMENT:
                    Game.Gameconfig.ClientGameConfig.GAMEID.CURRENT_GAME_ID = "TOUR";
                    break;
                default:
                    Game.Gameconfig.ClientGameConfig.GAMEID.CURRENT_GAME_ID = "POKER";
                    break;
            }
            SBI2_Request request = new SBI2_Request()
            {
                CashType = cashtype,
                Blind = blind,
                GameID = Game.Gameconfig.ClientGameConfig.GAMEID.CURRENT_GAME_ID// (string)DataDispatcher.Instance().GetExtra(Game.Gameconfig.GameConfig.KEY_DATADISPATCHER.SCENE_LOBBY, Game.Gameconfig.GameConfig.KEY_DATADISPATCHER.KEY_GAMEID)
            };
            DataListener dataListener = new DataListener(HandlerResbonseBase);
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestJoinGameBuyId(object[] param)
        {
            //Debug.Log("DRequestJoinGame");
            View.OnUpdateView("ShowLoading");
            SBI2_Request request = new SBI2_Request()
            {
              TableId = (long)param[0],
              GameID = (string)param[1]
            };
            DataListener dataListener = new DataListener(HandlerResbonseBase);
            Network.Network.SendOperation(request, dataListener);
        }



        private IEnumerator HandlerResbonseBase(string coderun, Dictionary<byte, object> data)
        {
            ResponseBase response = new ResponseBase(data);
            if(response.ErrorCode != 0)
            {
                View.OnUpdateView("DisableLoading");
                View.OnUpdateView("ShowError", response.ErrorMsg);
            }
            yield return null;
        }

       


        #endregion

        #region Process Response

        private void HandlerPushMessageMSG2(Dictionary<byte, object> data)
        {
            MSG2_Push push = new MSG2_Push(data);
            View.OnUpdateView("OnGetBroadCastMSG2", new object[] { push.Messages });
        }

        private void HandlerPushMSG3(Dictionary<byte, object> data)
        {
            MSG3_Notify_Push push = new MSG3_Notify_Push(data);
            if (push.Type == 0 && push.CurrentCash > 0)
            {
                PlayerPrefs.SetInt("playerCharging", 1);
                ClientConfig.UserInfo.GOLD = push.CurrentCash;
                Base.Utils.EventManager.Instance.RaiseEventInTopic(Base.Utils.EventManager.CHANGE_BALANCE);
            }
            View.OnUpdateView("Msg3Push", push.Type, push.Message);
        }

        private void IEHandlerPushLGI_1LogoutOtherDevice(Dictionary<byte, object> data)
        {
            ATH1_Push push = new ATH1_Push(data);
            Utilities.LogMng.Log("ShowDialogLogOutOtherDevice", push.Message);
            View.OnUpdateView("ShowDialogLogOutOtherDevice", push.Message);
        }


        private IEnumerator HandlerResponeListTable(string coderun, Dictionary<byte, object> data)
        {
            ATH4GetBlindsInfoResponse response = new ATH4GetBlindsInfoResponse(data);
            if(response.ErrorCode != 0)
            {
                View.OnUpdateView("DisableLoading");
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }

            blindInfoLobby = response.BlindsInfo;
            View.OnUpdateView("ShowListTableLobbyPanel", new object[] { blindInfoLobby });
            yield return null;
        }

        private IEnumerator HandlerResponseListGameATH8(string coderun, Dictionary<byte, object> data)
        {
            ATH8_Response response = new ATH8_Response(data);
            if (response.ErrorCode == 0)
            {
                games = response.Games;
                View.OnUpdateView("UpdateGame52", new object[] { games });
            }
            yield return null;
        }

      

       

      

        #endregion

        #region Handle Push Data

       
        private void HandlerPushJoinMauBinhMAU1(Dictionary<byte, object> data)
        {

            MAUJoinGamePush push = new MAUJoinGamePush(data);
            if (push.ErrorCode != 0)
            {
                View.OnUpdateView("JoinGameError", push.ErrorMsg);
                return;
            }
            //SaveDataAndJoinGame(push.TableId, TagAssetBundle.Tag_Scene.TAG_SCENE_MAUBINH, TagAssetBundle.SceneName.MAUBINH_SCENE);
        }

        private void HandlerPushJoinPokerPKR_SBI2(Dictionary<byte, object> data)
        {
            PKRJoinGamePush push = new PKRJoinGamePush(data);
            Debug.Log("HandlerPushJoinPokerPKR_SBI2: " + push.ErrorCode);
            if (push.ErrorCode != 0)
            {
                View.OnUpdateView("JoinGameError", push.ErrorMsg);
                return;
            }
            //SaveDataAndJoinGame(push.TableId, TagAssetBundle.Tag_Scene.TAG_SCENE_POKER, TagAssetBundle.SceneName.POKER_SCENE);

        }

        //private void HandlerPushJoinTLMB(byte[] data)
        //{
        //    GameProtocol.TienLen.TIL1_Push push = data.DeserializeFromBytes<GameProtocol.TienLen.TIL1_Push>();
        //    if (push.ErrorCode != 0)
        //    {
        //        View.OnUpdateView("JoinGameError", push.ErrorMsg);
        //        return;
        //    }
        //    SaveDataAndJoinGame(push.TableId, TagAssetBundle.Tag_Scene.TAG_SCENE_TLMB, TagAssetBundle.SceneName.TLMB_SCENE);
        //}

        private void HandlerPushJoinTLMN(Dictionary<byte, object> data)
        {
            //TMNJoinGamePush push = new TMNJoinGamePush(data);
            //if (push.ErrorCode != 0)
            //{
            //    View.OnUpdateView("JoinGameError", push.ErrorMsg);
            //    return;
            //}
            //if (Game.Gameconfig.ClientGameConfig.GAMEID.CURRENT_GAME_ID.Equals(Game.Gameconfig.ClientGameConfig.GAMEID.TLMN))
            //    SaveDataAndJoinGame(push.TableId, TagAssetBundle.Tag_Scene.TAG_SCENE_TLMN, TagAssetBundle.SceneName.TLMN_SCENE);
            //else SaveDataAndJoinGame(push.TableId, TagAssetBundle.Tag_Scene.TAG_SCENE_TLMNSOLO, TagAssetBundle.SceneName.TLMNSL_SCENE);
        }

        //private void HandlerPushJoinXOCDIA(byte[] data)
        //{
        //    XOC1_Push push = data.DeserializeFromBytes<XOC1_Push>();
        //    if (push.ErrorCode != 0)
        //    {
        //        View.OnUpdateView("JoinGameError", push.ErrorMsg);
        //        return;
        //    }
        //    SaveDataAndJoinGame(push.TableId, TagAssetBundle.Tag_Scene.TAG_SCENE_XOCDIA, TagAssetBundle.SceneName.XOCDIA_SCENE);
        //}

        private void HandlerPushJoinLieng(Dictionary<byte, object> data)
        {
            //LIEJoinGamePush push = new LIEJoinGamePush(data);
            //if (push.ErrorCode != 0)
            //{
            //    View.OnUpdateView("JoinGameError", push.ErrorMsg);
            //    return;
            //}
            //SaveDataAndJoinGame(push.TableId, TagAssetBundle.Tag_Scene.TAG_SCENE_LIENG, TagAssetBundle.SceneName.LIENG_SCENE);
        }

        private void HandlerPushJoinTala(Dictionary<byte, object> data)
        {
            //PHO1_Push push = new PHO1_Push(data);
            //if (push.ErrorCode != 0)
            //{
            //    View.OnUpdateView("JoinGameError", push.ErrorMsg);
            //    return;
            //}
            //SaveDataAndJoinGame(push.TableId, TagAssetBundle.Tag_Scene.TAG_SCENE_TALA, TagAssetBundle.SceneName.TALA_SCENE);
        }

        //private void HandlerPushJoinBaCay(Dictionary<byte, object> data)
        //{
        //    BCC.BCC1Push push = data.DeserializeFromBytes<BCC.BCC1Push>();
        //    if (push.ErrorCode != 0)
        //    {
        //        View.OnUpdateView("JoinGameError", push.ErrorMsg);
        //        return;
        //    }
        //    SaveDataAndJoinGame(push.TableId, TagAssetBundle.Tag_Scene.TAG_SCENE_BACAY, TagAssetBundle.SceneName.BACAY_SCENE);
        //}

        private void HandlerPushJoinXITO(Dictionary<byte, object> data)
        {
            XITOJoinGamePush push = new XITOJoinGamePush(data);
            if (push.ErrorCode != 0)
            {
                View.OnUpdateView("JoinGameError", push.ErrorMsg);
                return;
            }
            //SaveDataAndJoinGame(push.TableId, TagAssetBundle.Tag_Scene.TAG_SCENE_XITO, TagAssetBundle.SceneName.XITO_SCENE);
        }

        //private void HandlerPushJoinSamLoc(byte[] data)
        //{
        //    SAM.SAM1Push push = data.DeserializeFromBytes<SAM.SAM1Push>();
        //    if (push.ErrorCode != 0)
        //    {
        //        View.OnUpdateView("JoinGameError", push.ErrorMsg);
        //        return;
        //    }
        //    SaveDataAndJoinGame(push.TableId, TagAssetBundle.Tag_Scene.TAG_SCENE_SAMLOC, TagAssetBundle.SceneName.SAMLOC_SCENE);
        //}


        //private void HandlerPushPay5ChargingSuccess(byte[] data)
        //{
        //    PAY5_Push push = data.DeserializeFromBytes<PAY5_Push>();
        //    ClientConfig.UserInfo.GOLD = push.Gold;
        //    View.OnUpdateView("ShowNotifiChargingSuccess", push.Amount);
        //}

        #endregion

        private void SaveDataAndJoinGame(long TableId, string TagScene, string SceneName)
        {

            if (DataDispatcher.Instance().GetExtras(Game.Gameconfig.ClientGameConfig.KEY_DATADISPATCHER.SCENE_LOBBY) == null)
            {
                DataDispatcher.Instance().SetExtras(Game.Gameconfig.ClientGameConfig.KEY_DATADISPATCHER.SCENE_LOBBY).PutExtra(Game.Gameconfig.ClientGameConfig.KEY_DATADISPATCHER.KEY_TABLEID, TableId);
            }
            else
            {
                DataDispatcher.Instance().GetExtras(Game.Gameconfig.ClientGameConfig.KEY_DATADISPATCHER.SCENE_LOBBY).PutExtra(Game.Gameconfig.ClientGameConfig.KEY_DATADISPATCHER.KEY_TABLEID, TableId);
            }
            
            View.OnUpdateView("JoinGameSuccess", TagScene, SceneName);
        }


    }
}
