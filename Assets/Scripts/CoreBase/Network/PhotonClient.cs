
using System;
using System.Threading;
using System.Collections.Generic;
using Broadcast;
using System.Collections;
using UnityEngine;
using GameProtocol.Protocol;
using System.Diagnostics;
using System.Text;
using Listener;
using Utilities;
using AppConfig;
using Utilities.Custom;
//using GameProtocol.PNG;
using System.Net;
using System.Net.Sockets;
//using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;
//using AssetBundles;
using GameProtocol.PNG;
using BaseConfig;
using ExitGames.Client.Photon;
using GameProtocol.Base;
//using ExitGames.Client.Photon.LoadBalancing;

namespace Network
{
    public enum ClientState
    {
        INIT,
        CONNECTING,
        CONNECTED,
        READY,
        DISCONNECT,
        RECONNECTED,
        DEATH,
        CONNECT_FAIL
    }

    public enum Config
    {
        NORMAL,
        HARDLY
    }
	public class PhotonClient : IPhotonPeerListener
    {

        public static readonly string TAG = "PhotonClient";

        public delegate void ServerChangedHandler(bool success);

        public delegate void NetworkHandler(ClientState _state, params object[] _params);
        private event NetworkHandler HandleNetworkEvent;

        //private string[] DNSStore = { };


        //private readonly object StreamLock = new object();
        //private readonly object PeerLock = new object (); HĐ comment

        private PhotonPeer PTPeer;

        private string host = ServerConfig.HOST_DEFAULT;
        private int port = ServerConfig.PORT_DEFAULT;
        //private string server_name = ServerConfig.NAME_DEFAULT;

        private ClientState state = ClientState.INIT;

        /// <summary>
        /// so lan thuc hien ket noi ke tu lan ket noi dau tien</br>
        /// retry > 1 se thuc hien lay dsn moi
        /// duoc reset khi thuc hien ket noi thanh cong
        /// </summary>
        private int retry = 0;

        //private float TimePing = 30;
        private int RequestCount = 0;
        //enum IEReconnet
        //{
        //    NORMAL,
        //    LGI1
        //}
        //IEReconnet isReconnect = IEReconnet.NORMAL;
        private static readonly PhotonClient instance = new PhotonClient();
        private PhotonClient()
        {
            state = ClientState.INIT;

            HandleNetworkEvent += HandleNetworkImpl;
        }

        public static PhotonClient Instance
        {
            get
            {
                return instance;
            }
        }

        public void SetNetworkConfig(Config cfg)
        {
            switch (cfg)
            {
                case Config.NORMAL:
                    //TimePing = 10;
                    break;
                case Config.HARDLY:
                    //TimePing = 10;
                    break;
                default:
                    break;
            }
        }

        #region network state
        /// <summary>
        /// check network running or not
        /// </summary>
        public bool IsActive
        {
            get
            {
                bool active = false;
                lock (PhotonClient.Instance)
                {
                    active = state == ClientState.CONNECTING || state == ClientState.CONNECTED || state == ClientState.READY;
                }

                return active;
            }
        }

        /// <summary>
        /// check network connected or not
        /// </summary>
        public bool Is_Connected
        {
            get
            {
                bool connected = false;
                lock (PhotonClient.Instance)
                {
                    connected = state == ClientState.CONNECTED || state == ClientState.READY;
                }

                return connected;
            }
        }

        /// <summary>
        /// check network ready or not
        /// </summary>
        public bool IsReady
        {
            get
            {
                bool ready = false;
                lock (PhotonClient.Instance)
                {
                    ready = state == ClientState.READY;
                }

                return ready;
            }
        }

        #endregion

        Stopwatch watch;
        long startTime = 0;
        #region Start/Stop PhotonClient
        
        public void SetGameServerAddress(string host, int port)
        {
            ServerConfig.GAMESERVER_HOST = host;
            ServerConfig.GAMESERVER_PORT = port;
        }

        public void StartPhotonClient()
        {
            //string IPv6_ADDR = ServerConfig.HOST_DEFAULT_V6;

            //try{
            //	LogMng.LogError(TAG, "StartPhotonClient");

            //	IPAddress ipa = IPAddress.Parse(IPv6_ADDR);
            //	IPEndPoint ipeh = new IPEndPoint(ipa, ServerConfig.PORT_DEFAULT);
            //	Socket connection = new Socket(
            //		AddressFamily.InterNetworkV6,
            //		SocketType.Stream,
            //		ProtocolType.Tcp);
            //	connection.Connect(ipeh);
            //	if(connection.Connected){
            //		this.host = ServerConfig.HOST_DEFAULT_V6;
            //		LogMng.LogError(TAG, "Connected to host ipv6");
            //	}
            //	else LogMng.LogError(TAG, "Not Connected to host ipv6");

            //}catch(Exception ex){
            //	LogMng.LogError(TAG, "AddressFamily.InterNetworkV6 " + ex.ToString());
            //}

            try
            {
                watch = Stopwatch.StartNew();

                if (string.IsNullOrEmpty(ServerConfig.GAMESERVER_HOST))
                {
#if UNITY_WEBGL
                    host = "ws://game.roll.vip";
                    port = ServerConfig.WEB_PORT_DEFAULT;
#else
                    host = ServerConfig.HOST_DEFAULT;
                    port = ServerConfig.PORT_DEFAULT;
#endif                    
                } 
                else
                {
                    host = ServerConfig.GAMESERVER_HOST;
                    port = ServerConfig.GAMESERVER_PORT;
                }


#if UNITY_WEBGL
                //port = ServerConfig.WEB_PORT_DEFAULT;
                //host = "ws://iway.kon.club";

                LogMng.Log(TAG, "WEBGL Connecting to " + host + ":" + port);

                //ConnectionProtocol protocol = ConnectionProtocol.Udp;
                //protocol = ConnectionProtocol.WebSocket;
                //if (PTPeer == null)
                //{
                //    LogMng.Log(TAG, "PhotonPeer = new PhotonPeer " + protocol.ToString());
                //    PTPeer = new PhotonPeer(this, protocol);
                //}
                //Type socketTcp = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp", false);
                //if (socketTcp == null)
                //{
                //    socketTcp = Type.GetType("ExitGames.Client.Photon.SocketWebTcp, Assembly-CSharp-firstpass", false);
                //}
                //if (socketTcp != null)
                //{
                //    PTPeer.SocketImplementationConfig[ConnectionProtocol.WebSocket] = socketTcp;
                //    PTPeer.SocketImplementationConfig[ConnectionProtocol.WebSocketSecure] = socketTcp;
                //}
                //PTPeer.Connect(string.Format("{0}:{1}", host, port), ServerConfig.NAME_DEFAULT);


                PTPeer = new PhotonPeer(this, ConnectionProtocol.WebSocket);
                PTPeer.SocketImplementation = typeof(SocketWebTcp);
#else
                //host = ServerConfig.HOST_DEFAULT;
                //port = ServerConfig.PORT_DEFAULT;
                PTPeer = new PhotonPeer(this, ConnectionProtocol.Tcp);
#endif
                //notify event network connecting
                this.HandleNetworkEvent(ClientState.CONNECTING, this.host, this.port);

                LogMng.Log(TAG, "Connecting to " + host + ":" + port);
				bool success = PTPeer.Connect(host + ":" + port, ServerConfig.NAME_DEFAULT);

                //bool success = PTPeer.Connect("10.10.6.28:4531", "social");
                if (!success)
                {
                    //Logger.LogException (new Exception ("Can not connect to address " + host + ":" + port));
                    this.HandleNetworkEvent(ClientState.CONNECT_FAIL, this.host, this.port);
                }
                else
                {
                    //Logger.Log (TAG, "Request connect to address " + host + ":" + port + " done");
                    //new Thread (() =>
                    //{
                    //	while (IsActive) {

                    //                       LogMng.Log(TAG, "PTPeer.Service: " + Thread.CurrentThread.GetHashCode());
                    //		PTPeer.Service ();

                    //		//goi ham service 5 lan trong 1s
                    //		lock (PeerLock) {
                    //			Monitor.Wait (PeerLock, TimeService);
                    //		}

                    //                       if(watch.ElapsedMilliseconds - startTime >= 2 * 60 * 1000)
                    //                       {
                    //                           this.HandleNetworkEvent(ClientState.DISCONNECT, null);
                    //                       }
                    //	}
                    //}).Start ();
                }
            }
            catch (Exception e)
            {
                this.HandleNetworkEvent(ClientState.CONNECT_FAIL, this.host, this.port);
                LogMng.LogException(e);
            }
        }
        //private int frame;
        public void FetchData()
        {
            try
            {
                if (IsActive)
                {
                    if(PTPeer != null)
                    PTPeer.Service();
                }
            }
            catch { }
            //frame++;
        }

        

        public void StopPhotonClient()
        {
            LogMng.LogError(TAG, "StopPhotonClient");
            retry = 0;
            RequestCount = 0;
            //TimePing = 30;
            if (watch != null)
                watch.Stop();
            state = ClientState.INIT;
            try
            {
                PTPeer.Disconnect();
                PTPeer = null;
            }
            catch (Exception e)
            {
                LogMng.LogError(TAG, "StopPhotonClient" + e.StackTrace);
            }
            DataProcessor.Instance.StopProcessor();
        }

        //private void Connect (string address)
        //{

        //}
#endregion

        public void SendOperationPT(MessageBase operation, DataListener listener)
        {
            if (PTPeer == null || !IsReady) return;
            DataProcessor.Instance.AddDataListener(listener);
            operation.SenderId = listener.GetHashCode();
            //PTPeer.Service();
            RequestCount += 1;
            //TimeService = 60;
            //lock (PeerLock) {
            //	Monitor.Pulse (PeerLock);
            //}
            SendOptions sendOptions = new SendOptions()
            {
            };
            sendOptions.Channel = 0;
            sendOptions.Encrypt = false;
            sendOptions.Reliability = PTPeer.IsEncryptionAvailable;
            PTPeer.SendOperation(0, operation.ToDictionary(), sendOptions);
            //PTPeer.OpCustom(0, operation.ToDictionary(), true, 0, PTPeer.IsEncryptionAvailable);
            startTime = watch.ElapsedMilliseconds;
            if (!operation.CodeRun.Equals("PNG0") && !operation.CodeRun.Equals("MBS3") && !operation.CodeRun.Equals("JKP1") && !operation.CodeRun.Equals("MBS1") && !operation.CodeRun.Equals("SLC1") && !operation.CodeRun.Equals("SLC0") && !operation.CodeRun.Equals("MSG2"))
                LogMng.Log(TAG, "Send: " + operation.CodeRun + ", Packet = "
                    + Packet.DictToString2(operation.ToDictionary()));
            //            if (operation.CodeRun.Equals("UDT0_V2"))
            //                LogMng.Log (TAG, "Send: " + operation.CodeRun + ", Packet = " + Packet.DictToString2 (operation.ToDictionary ()));
        }

#region PhotonClient Event
		public virtual void DebugReturn(DebugLevel level, string message)
        {
            LogMng.Log(TAG, "DebugReturn: Message = " + message + ", ThreadId = " + Thread.CurrentThread.GetHashCode());
        }

        /// <summary>
        /// received server push here
        /// </summary>
        /// <param name="eventData"></param>
		public virtual void OnEvent(EventData eventData)
        {
            //LogMng.Log("OnEvent: -----------------------------------------------------", "");
            try
            {
                //LogMng.Log("OnEvent: ", eventData.Code);
                Dictionary<byte, object> data = eventData.Parameters;
                data[(byte)ParameterCode.Flag] = Packet.FLAG_PASSIVE;
                DataProcessor.Instance.HandlerReceivedData(data);
                //if (eventData.Parameters[(byte)GameProtocol.Protocol.ParameterCode.CodeRun].Equals("LGI-1"))
                //    isReconnect = IEReconnet.LGI1;
                //else isReconnect = IEReconnet.NORMAL;
                //foreach(byte b in eventData.Parameters.Keys)
                //{
                //    LogMng.Log("Key: ", b + " , value: " + eventData.Parameters[b].ToString());
                //}
                LogMng.Log(TAG, "Push: " + eventData.Parameters[(byte)ParameterCode.CodeRun] + ", Packet = " + Packet.DictToString2(eventData.Parameters) + ", EventData.Code = " + eventData.Code + ", ThreadId = " + Thread.CurrentThread.GetHashCode());
            }
            catch (Exception e)
            {
                LogMng.Log(TAG, "OnEvent Exception : " + e.StackTrace + ", Source: " + e.Source);
            }
        }

        /// <summary>
        /// received server response here
        /// </summary>
        /// <param name="operationResponse"></param>
		public virtual void OnOperationResponse(OperationResponse operationResponse)
        {
            try
            {
                //UnityEngine.Debug.Log("Received: " + operationResponse.Parameters[(byte)GameProtocol.Protocol.ParameterCode.CodeRun]);
                //UnityEngine.Debug.Log("OnOperationResponse: " + operationResponse.Parameters.Count);
                //if (operationResponse.OperationCode == 0) return;
                RequestCount -= 1;
                //UnityEngine.Debug.Log("OnOperationResponse: " + operationResponse.OperationCode);
                //if (RequestCount <= 0)
                //   TimeService = 500;
                //				if(operationResponse.Parameters != null && operationResponse.Parameters[(byte)ParameterCode.CodeRun].Equals("UDT0_V2"))
                //				{
                //					LogMng.Log(TAG, "Received: " + operationResponse.Parameters[(byte)ParameterCode.CodeRun] + ", Packet = " + Packet.DictToString2(operationResponse.Parameters));
                //				}
                //if (operationResponse.ReturnCode != (short)ReturnCode.OK)
                //{
                //    LogMng.LogError (TAG, "Response Error: " + operationResponse.DebugMessage);
                //    return;
                //}

                //Packet packet = new Packet(operationResponse.OperationCode, operationResponse.Parameters);
                //packet.Flag = Packet.FLAG_ACTIVE;
                //Logger.Log(TAG, "Received: " + packet.ToString());
                //DataProcessor.Instance.HandlerReceivedPacket(packet);

                Dictionary<byte, object> data = operationResponse.Parameters;
                //UnityEngine.Debug.Log("OnOperationResponse");

                //LogMng.Log(TAG, "Received: " + operationResponse.Parameters[(byte)GameProtocol.Protocol.ParameterCode.CodeRun] + ", Packet = " + Packet.DictToString2(operationResponse.Parameters) + " -- Packet.Length = " + (Encoding.UTF8.GetBytes(Packet.DictToString2(operationResponse.Parameters)).Length) + ", OperationCode = " + operationResponse.OperationCode + ", ReturnCode = " + operationResponse.ReturnCode + ", ResponseTime = " + (watch.ElapsedMilliseconds - startTime) + ", ThreadId = " + Thread.CurrentThread.GetHashCode());
                DataProcessor.Instance.HandlerReceivedData(data);


                if (operationResponse.Parameters != null && !operationResponse.Parameters[(byte)ParameterCode.CodeRun].Equals("CMN1")
                    && !operationResponse.Parameters[(byte)ParameterCode.CodeRun].Equals("MBS1")
                    && !operationResponse.Parameters[(byte)ParameterCode.CodeRun].Equals("PNG0")
                    && !operationResponse.Parameters[(byte)ParameterCode.CodeRun].Equals("JKP0") 
                    && !operationResponse.Parameters[(byte)ParameterCode.CodeRun].Equals("SLC0")//0 1
                    && !operationResponse.Parameters[(byte)ParameterCode.CodeRun].Equals("SLC1")
                    && !operationResponse.Parameters[(byte)ParameterCode.CodeRun].Equals("MSG2"))
                {
                    //string json = Newtonsoft.Json.JsonConvert.SerializeObject(operationResponse.Parameters, Newtonsoft.Json.Formatting.Indented);
                    //LogMng.Log(TAG, "Newtonsoft JsonConvert Received: " + json.Replace("\n", ""));
                    LogMng.Log(TAG, "Received: " + operationResponse.Parameters[(byte)ParameterCode.CodeRun] + ", Packet = " + Packet.DictToString2(operationResponse.Parameters) + " -- Packet.Length = " + (Encoding.UTF8.GetBytes(Packet.DictToString2(operationResponse.Parameters)).Length) + ", OperationCode = " + operationResponse.OperationCode + ", ReturnCode = " + operationResponse.ReturnCode + ", ResponseTime = " + (watch.ElapsedMilliseconds - startTime) + ", ThreadId = " + Thread.CurrentThread.GetHashCode());
                }
            }
            catch (Exception e)
            {
                LogMng.Log(TAG, "OnOperationResponse Exception : " + e.StackTrace + ", Source: " + e.Source);
            }
        }

		public virtual void OnStatusChanged(StatusCode statusCode)
        {
            LogMng.Log(TAG, "Network State: OnStatusChanged " + statusCode);

            switch (statusCode)
            {
                case StatusCode.Connect:
                    bool success = false;
#if !UNITY_WEBGL
                    success = PTPeer.EstablishEncryption();
#endif
                    if (!success)
                        this.HandleNetworkEvent(ClientState.READY, null);
                    PTPeer.TimePingInterval = ServerConfig.PING_TIME;
                    break;
                case StatusCode.EncryptionEstablished:
                    this.HandleNetworkEvent(ClientState.READY, null);
                    break;
                case StatusCode.EncryptionFailedToEstablish:
                    this.HandleNetworkEvent(ClientState.READY, null);
                    break;

                case StatusCode.Disconnect:
                case StatusCode.DisconnectByServerLogic:
                case StatusCode.Exception:
                case StatusCode.ExceptionOnReceive:
                case StatusCode.TimeoutDisconnect:
                case StatusCode.SendError:
                    this.HandleNetworkEvent(ClientState.DISCONNECT, null);
                    break;

                case StatusCode.DisconnectByServerUserLimit:
                case StatusCode.DisconnectByServerTimeout:
                    this.HandleNetworkEvent(ClientState.DEATH, null);
                    break;

                case StatusCode.ExceptionOnConnect:
                    this.HandleNetworkEvent(ClientState.CONNECT_FAIL, null);
                    break;

                case StatusCode.SecurityExceptionOnConnect:
                    //loi lap trinh
                    LogMng.LogError(TAG, "Loi lap trinh: StatusCode.SecurityExceptionOnConnect");
                    break;
            }
        }

#endregion

#region Implement network event: cai dat cac xu ly khi network phat sinh 1 su kien

        private void HandleNetworkImpl(ClientState state, params object[] param)
        {
            LogMng.Log(TAG, "Network State: pre_state = " + this.state + ", state = " + state);
            ClientState oldstate = this.state;
            this.state = state;

            switch (state)
            {
                case ClientState.INIT:
                    break;
                case ClientState.CONNECTING:
                    SocketConnectingImpl(param);
                    break;
                case ClientState.CONNECTED:
                    SocketConnectedImpl(param);
                    break;
                case ClientState.READY:
                    LogMng.Log(TAG, "HandleNetworkImpl: oldstate = " + oldstate + ", state = " + state + ", " + (oldstate != ClientState.READY));
                    if (oldstate != ClientState.READY)
                        SocketReadyImpl(param);
                    break;
                case ClientState.DISCONNECT:
                    if (oldstate == ClientState.CONNECT_FAIL || oldstate == ClientState.DEATH || oldstate == ClientState.INIT)
                        this.state = oldstate;
                    else
                        SocketDisconnectedImpl(param);
                    break;
                case ClientState.RECONNECTED:
                    SocketReconnectedImpl(param);
                    break;
                case ClientState.DEATH:
                    SocketConnectDeathImpl(param);
                    break;
                case ClientState.CONNECT_FAIL:
                    SocketConnectFailImpl(param);
                    break;
            }

        }

        /// <summary>
        /// su kien socket dang ket noi
        /// </summary>
        /// <param name="param"></param>
        private void SocketConnectingImpl(params object[] param)
        {
            //Logger.Log (TAG, "Start Connect to: " + param [0] + ":" + param [1]);
            this.state = ClientState.CONNECTING;

            Dictionary<string, object> msg = new Dictionary<string, object>();
            msg.Add("host", param[0]);
            msg.Add("port", param[1]);
            BroadcastReceiver.Instance.BroadcastMessage(MessageCode.NETWORK, MessageType.Connecting, msg);
        }

        /// <summary>
        /// su kien socket mo ket noi thanh cong, cho trao doi key ma hoa voi server
        /// </summary>
        /// <param name="param"></param>
        private void SocketConnectedImpl(params object[] param)
        {
            this.state = ClientState.CONNECTED;


            Dictionary<string, object> msg = new Dictionary<string, object>();
            msg.Add("host", param[0]);
            msg.Add("port", param[1]);
            BroadcastReceiver.Instance.BroadcastMessage(MessageCode.NETWORK, MessageType.Connected, msg);
        }

        /// <summary>
        /// su kien client trao doi key ma hoa voi server hoan thanh, san sang nhan request tu cac layer tren
        /// </summary>
        /// <param name="param"></param>
        private void SocketReadyImpl(params object[] param)
        {
            //UnityEngine.Debug.Log("SocketReadyImpl");
            //LogMng.Log(TAG, "SocketReadyImpl: " + Reconnector.CheckReconnect() + " , DataProcessor.Instance = " + (DataProcessor.Instance == null));

            //UnityEngine.Debug.Log("SocketReadyImpl 111: " + (DataProcessor.Instance == null));
            //reset so lan ket noi bi loi
            this.retry = 0;

            DataProcessor.Instance.StartProcessor();

            //UnityEngine.Debug.Log("SocketReadyImpl 2222: ");
            try
            {
                bool has_reconnect = Reconnector.CheckReconnect();
                //UnityEngine.Debug.Log("SocketReadyImpl 33333: " + has_reconnect);
                LogMng.Log(TAG, "has_reconnect: " + has_reconnect);
                if (!has_reconnect)
                {
                    BroadcastReceiver.Instance.BroadcastMessage(MessageCode.NETWORK, MessageType.Ready, null);
                }
                MonoInstance.Instance.ExecuteIEnumerator(PingCoroutine());
            }catch(Exception ex)
            {
                UnityEngine.Debug.LogError(TAG + " - Exeption: " + ex.Message);
                UnityEngine.Debug.LogError(TAG + " - Exeption: " + ex.StackTrace);
            }
        }

        /// <summary>
        /// xay ra dut ket noi trong qua trinh runtime
        /// </summary>
        private void SocketDisconnectedImpl(params object[] param)
        {
            StopPhotonClient();
            LogMng.Log("DISCONNECT: ", "" );
            //if (isReconnect == IEReconnet.NORMAL)
            {
                BroadcastReceiver.Instance.BroadcastMessage(MessageCode.NETWORK, MessageType.Disconnect, null);
                //thuc hien ket noi lai
                StartPhotonClient();
            }
            //else LoadLobby();
        }

        /// <summary>
        /// thuc hien ket noi lai thanh cong, thong bao de load lai ban game neu nhu o trong ban, hoac khong lam gi ca neu o ngoai game
        /// </summary>
        private void SocketReconnectedImpl(params object[] param)
        {
            BroadcastReceiver.Instance.BroadcastMessage(MessageCode.NETWORK, MessageType.Reconnected, null);
            //thuc hien ket noi lai
            MonoInstance.Instance.ExecuteIEnumerator(PingCoroutine());
        }

        /// <summary>
        /// khong ket noi duoc toi server
        /// </summary>
        private void SocketConnectDeathImpl(params object[] param)
        {
            StopPhotonClient();
            LogMng.Log("SocketConnectDeathImpl: ","");

            BroadcastReceiver.Instance.BroadcastMessage(MessageCode.NETWORK, MessageType.Death, null);
            //if (isReconnect == IEReconnet.NORMAL)
            //else LoadLobby();
        }
        void LoadLobby()
        {
            //CoUp.View.SceneType.SCENE_TYPE = CoUp.View.SceneType.ESCENE_TYPE.LOGIN;
            //CoreBase.Extention.DialogEx.Instance.ShowDialog("Tài khoản đăng nhập trên thiết bị khác!", () => {
            //    CoUp.View.SceneManagerViewScript.Instance.ShowLogin();
            //},depth: CoreBase.Extention.DialogEx.depth.prioritize);
        }
        /// <summary>
        /// xu ly su kien ket noi toi server bi loi
        /// </summary>
        /// <param name="param"></param>
        private void SocketConnectFailImpl(params object[] param)
        {
            if (retry >= ServerConfig.MAX_CONNECT_RETRY_TIMES)
                this.HandleNetworkEvent(ClientState.DEATH, null);
            else
            {
                retry += 1;
                LogMng.LogError("SocketConnectFailImpl", "retry: " + retry);

                //request mainthread xu ly do viec request http bang class WWW bat buoc phai thuc hien trong mainthread
                //MonoBehaviourHelper.Instance.ExecuteIEnumerator(SocketConnectFailImplRunOnUI());
                //MonoBehaviourHelper.Instance.ExecuteIEnumerator (DNSHelper.SocketConnectFailImplRunOnUI (ServerChangedResult));
#if UNITY_WEBGL
                this.HandleNetworkEvent(ClientState.DEATH, null);
#else
                MonoInstance.Instance.ExecuteIEnumerator(CheckConnectionToMasterServer());
#endif
            }
        }

        private IEnumerator CheckConnectionToMasterServer()
        {
            //Ping pingMasterServer = new Ping("8.8.8.8");
            WWW www = new WWW("http://www.google.com.vn");

            //Debug.Log(pingMasterServer.ip);
            float startTime = Time.time;
            while (!www.isDone && Time.time < startTime + 5.0f)
            {
                yield return new WaitForSeconds(0.2f);
            }

            if (www.isDone && www.error == null)
            {
                //process network available
                MonoInstance.Instance.ExecuteIEnumerator(DNSMng.SocketConnectFailImplRunOnUI(ServerChangedResult));
            }
            else
            {
                LogMng.LogError("CheckConnectionToMasterServer", "Error: " + www.error);
                //process network not available
                this.HandleNetworkEvent(ClientState.DEATH, null);
            }
        }

#endregion
        private float send_time = 0f;
        private IEnumerator PingCoroutine()
        {
            //LogMng.Log(TAG, "Send PNG0: " + IsReady);
            //while (IsReady && !GameConfig.SceneName.HOME_SCENE.Equals(SceneManager.GetActiveScene().name))
            while (IsReady)
            {
                //if (!TagAssetBundle.SceneName.HOME_SCENE.Equals(SceneManager.GetActiveScene().name))
                {
                    //LogMng.Log(TAG, "Send PNG0..." + System.DateTime.Now);
                    
                    //yield return new WaitUntil(()=>  frame == 600);
                    send_time = Time.realtimeSinceStartup;
                    //#if UNITY_WEBGL
                    DataListener listener = new DataListener(PingHandleResponse);
                    PNG0_Request ping = new PNG0_Request();
                    SendOperationPT(ping, listener);
                    //#endif
                    //frame = 0;
                }
                //IF NOT, send ATH8 get list game + update jackpot
                yield return new WaitForSeconds(ServerConfig.PNG0_REPEAT_TIME);

            }
        }

        private IEnumerator PingHandleResponse(string coderun, Dictionary<byte, object> data)
        {
            //PNG0_Response response = new PNG0_Response(data);
            float recei_time = Time.realtimeSinceStartup;
            float delta_time = recei_time - send_time;
            //LogMng.Log ("PING", "delta_time: " + delta_time);
            BroadcastReceiver.Instance.BroadcastMessage(MessageCode.NETWORK, MessageType.Ping, delta_time);
            yield return null;
        }

#region DNS lay server address tu cac link dns

        /// <summary>
        /// Ham chi chay trong mainthread
        /// lan luot duyet tung link trong mang DNSStore de lay server host moi, ket qua tra ve bao gom DNSStore moi va server host</br>
        /// can kiem tra ket qua xem da expire chua
        /// </summary>
        /// <param name="counter"></param>
        /// <returns></returns>
        //private bool GetHostFromDNSStore(int index)
        //{
        //    Logger.Log(TAG, "GetHostFromDNSLink: " + DNSStore.Length + ", " + index);

        //    //kiem tra neu danh sach link rong hoac da duyet het tat ca cac link thi tra lai ket qua
        //    if (DNSStore == null || DNSStore.Length == 0 || index >= DNSStore.Length || retry >= 5)
        //        return false;

        //    //thong bao cho cac listener biet dang chuyen toi server nao
        //    string strmsg = "Dang ket noi to Server " + (index + 1);
        //    Dictionary<string, object> msg = new Dictionary<string, object>();
        //    msg.Add("msg", strmsg);
        //    BroadcastReceiver.Instance.BroadcastMessage(MessageCode.NETWORK, MessageType.Connecting, msg);

        //    bool success = DNSHelper.GetDNSStoreFromLink(DNSStore[index]);
        //    Logger.Log(TAG, "Sucess: " + success);
        //    if (success)
        //        return true;
        //    //duyet link tiep theo trong mang
        //    return GetHostFromDNSStore(index + 1);
        //}

        //private IEnumerator SocketConnectFailImplRunOnUI()
        //{
        //    retry += 1;
        //    bool success = GetHostFromDNSStore(0);
        //    if (success)
        //    {
        //        StartPhotonClient();
        //    }
        //    else
        //    {
        //        this.HandleNetwork(ClientState.DEATH, null);
        //    }
        //    yield return null;
        //}

        //public void SetLinkGetDNS(JSONArray jdns)
        //{
        //    Logger.Log(TAG, "setLinkGetDNS: " + jdns.ToString());
        //    DNSStore = new string[jdns.Count];
        //    for (int i = 0; i < DNSStore.Length; i++)
        //        DNSStore[i] = jdns[i];
        //}

        /// <summary>
        /// set server address - host and port
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public void SetServerHost(string host, int port)
        {
            this.host = host;
            this.port = port;
        }

        private void ServerChangedResult(bool success)
        {
            //UnityEngine.Debug.LogError ("ServerChangedResult ----------------  " + success);
            if (success)
            {
                StartPhotonClient();
            }
            else
                HandleNetworkEvent(ClientState.DEATH, null);
        }

        public bool RegisterObject(Type customType, byte code, SerializeMethod serializeMethod, DeserializeMethod deserializeMethod)
        {
            bool sucsess = PhotonPeer.RegisterType(customType, code, serializeMethod, deserializeMethod);
            Client.Photon.Protocol.TryRegisterType(customType, code, serializeMethod, deserializeMethod);
            return sucsess;
        }

        public int GetPing()
        {
            return PTPeer.RoundTripTime;
        }
        //		#if !UNITY_WEBGL
        //		bool needKeep = false;
        //		public void keepConnectionWhenAppPause(bool nk)
        //		{
        //			LogMng.Log(TAG, "keepConnectionWhenAppPause: " + nk + ", time: " + Time.realtimeSinceStartup);
        //			needKeep = nk;
        //			if (needKeep)
        //			{
        //				Thread thread = new Thread(() => {
        //					LogMng.Log(TAG, "keepConnectionWhenAppPause: thead running.........");
        //					while (needKeep)
        //					{
        //						LogMng.Log(TAG, "keepConnectionWhenAppPause: thead send ping.........");
        //						DataListener listener = new DataListener (PingHandleResponse);
        //						PNG0_Request ping = new PNG0_Request ();
        //						SendOperation(ping, listener);
        //						int timeping = 3000;
        //						while (timeping > 0)
        //						{
        //							lock (PeerLock)
        //							{
        //								Monitor.Wait(PeerLock, 200);
        //								timeping -= 200;
        //							}
        //							LogMng.Log(TAG, "keepConnectionWhenAppPause: sevice called........");
        //							PTPeer.Service();
        //						}
        //					}
        //				});
        //				LogMng.Log(TAG, "keepConnectionWhenAppPause: thead start.........");
        //				thread.Start();
        //			}
        //		}
        //		#endif
#endregion

    }
}