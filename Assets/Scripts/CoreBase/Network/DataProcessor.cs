using UnityEngine;
using System.Collections.Generic;
using Listener;
using System;
using GameProtocol.Protocol;
using System.Threading;
using Utilities;
using AppConfig;
using System.Collections;
using System.Linq;
using Game.Gameconfig;
using BaseConfig;
using GameProtocol.Base;

namespace Network
{
    [System.Reflection.Obfuscation(Feature = "renaming", Exclude = true)]
    public class DataProcessor : MonoBehaviour
    {
        private static readonly string TAG = "DataProcessor";

        internal delegate IEnumerator FinishPacketNotify(bool sequently, string crt_name);

        //private static readonly string SEQUENCE_PREFIX = "@30";
        private static HashSet<string> SEQUENCE_PREFIX = new HashSet<string>() { ClientGameConfig.RequestCode.LGI };

        /// <summary>
        /// luu cac listener lang nghe du lieu response
        /// </summary>
        private Dictionary<int, DataListener> DataListeners = new Dictionary<int, DataListener>();

        /// <summary>
        /// luu cac packet cho xu ly tuần tự
        /// </summary>
        private List<Dictionary<byte, object>> DataQueue = new List<Dictionary<byte, object>>();

        private Dictionary<byte, object> Current = null;

        /// <summary>
        /// luu cac packet cho xu ly độc lập, song song
        /// </summary>
        private List<Dictionary<byte, object>> DataQueueConcurrently = new List<Dictionary<byte, object>>();
        /// <summary>
        /// concurrent_count: số lượng coroutine xu ly goi tin doc lap tối đa đồng thời được chạy
        /// </summary>
        private int ccr_count = 0;

        //private object HandlingPacket = null;

        static DataProcessor()
        {
            //if (instance == null)
            //{
            //    GameObject obj = new GameObject();
            //    obj.name = "_DataProcessorInstance";
            //    instance = obj.AddComponent<DataProcessor>();
            //}
        }

        private static DataProcessor instance = null;
        internal static DataProcessor Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "_DataProcessorInstance";
                    instance = obj.AddComponent<DataProcessor>();
                }
                return instance;
            }
        }


        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (instance == null) instance = this;
            //WhileUpdatePhoton();
        }
        
        internal void AddDataListener(DataListener listener)
        {
            if (listener != null)
                DataListeners.Add(listener.GetHashCode(), listener);
        }

        internal void AddSequenceCode(string code)
        {
            if (SEQUENCE_PREFIX.Contains(code))
                return;
            SEQUENCE_PREFIX.Add(code);
        }
        internal void RemoveSequenceCode(string code)
        {
            if (SEQUENCE_PREFIX.Contains(code))
                SEQUENCE_PREFIX.Remove(code);
        }

        public void RemoveQueueByCodeRun(string coderun)
        {
            var listData = DataQueue.Where(q => ((string)q[(byte)ParameterCode.CodeRun]).Substring(0, 3).Equals(coderun));
            if (listData != null && listData.Count() > 0)
            {
                foreach (var data in listData)
                {
                    DataQueue.Remove(data);
                }
                GC.Collect();
            }
        }

        internal void StartProcessor()
        {
            
        }

        void Update()
        {
            if(!isUpdate)
            PhotonClient.Instance.FetchData();
        }

        Thread _threadPing;
        float time;
        private bool isUpdate;
        private void WhileUpdatePhoton()
        {
            isUpdate = true;

            _threadPing = new Thread(() => {
                while (isUpdate)
                {
                    //if (Time.time - time > 0.15f)
                    {
                        // time = Time.time;
                        //Debug.Log("Ping Photon");
                        PhotonClient.Instance.FetchData();
                        Thread.Sleep(10);
                    }
                }
            });
            _threadPing.Start();
        }


        public bool HasRequest()
        {
            return DataListeners.Count > 0;
        }

        internal void HandlerReceivedData(Dictionary<byte, object> data)
        {
            //LogMng.Log("DataProcessor:", "HandlerReceivedData: " + (string)data[(byte)ParameterCode.CodeRun] + ", coderun: " + (string)data[(byte)ParameterCode.CodeRun] + " , CUR: " + (Current == null));
            if (SEQUENCE_PREFIX.Contains(((string)data[(byte)ParameterCode.CodeRun]).Substring(0, 3)))
            {
                //neu chua co goi tin nao duoc xu ly thi thuc hien xu ly luon, nguoc lai thi add vao queue cho goi tin truoc do xu ly xong
                if (Current == null)
                    StartCoroutine(HandleData(Current = data, true, "sequently" + data.GetHashCode()));
                else
                    DataQueue.Add(data);
                return;
            }

            if (ccr_count < ClientGameConfig.ConstantValue.NUM_THREAD_DATA)
            {
                ccr_count++;
                StartCoroutine(HandleData(data, false, "concurrent" + ccr_count));
            }
            else
                DataQueueConcurrently.Add(data);
        }

        [System.Reflection.Obfuscation(Feature = "renaming", Exclude = true)]
        private IEnumerator HandleData(Dictionary<byte, object> data, bool sequently, string crt_name)
        {
            //LogMng.Log(TAG, "Coroutine: \"" + crt_name + "\" process seq: " + sequently + " running !!!" + " , " + ((byte)data[(byte)ParameterCode.Flag] == Packet.FLAG_ACTIVE) + " , code: " + (string)data[(byte)ParameterCode.CodeRun]);
            PacketHandleEnd EndEvent = new PacketHandleEnd(FinishOnePacket, sequently, crt_name);
            if ((byte)data[(byte)ParameterCode.Flag] == Packet.FLAG_ACTIVE)
            {
                int key_hashcode = (int)data[(byte)ParameterCode.SenderId];
                if (!DataListeners.ContainsKey(key_hashcode))
                {
                    IEnumerator iend0 = EndEvent.OnEnd();
                    while (iend0.MoveNext())
                        yield return iend0.Current;
                    yield break;
                }
                DataListener listener = DataListeners[key_hashcode];
                DataListeners.Remove(key_hashcode);

                IEnumerator ienum = ProcessActivePacket(data, listener);
                while (ienum.MoveNext())
                    yield return ienum.Current;

                IEnumerator iend = EndEvent.OnEnd();
                while (iend.MoveNext())
                    yield return iend.Current;
                yield break;
            }

            if ((byte)data[(byte)ParameterCode.Flag] == Packet.FLAG_PASSIVE)
            {
                IEnumerator ienum = ProcessPassivePacket(data);
                while (ienum.MoveNext())
                    yield return ienum.Current;

                IEnumerator iend = EndEvent.OnEnd();
                while (iend.MoveNext())
                    yield return iend.Current;
                yield break;
            }
        }
        [System.Reflection.Obfuscation(Feature = "renaming", Exclude = true)]
        private IEnumerator ProcessActivePacket(Dictionary<byte, object> data, DataListener listener)
        {
            //LogMng.Log("ProcessActivePacket: ", (string)data[(byte)ParameterCode.CodeRun]);
            IEnumerator ienum = listener.Invoke((string)data[(byte)ParameterCode.CodeRun], data);
            while (ienum.MoveNext())
                yield return ienum.Current;
        }

        private IEnumerator ProcessPassivePacket(Dictionary<byte, object> data)
        {
            //LogMng.Log("ProcessPassivePacket Push", (string)data[(byte)ParameterCode.CodeRun]);
            IEnumerator ienum = PushManager.Instance.HandlePush((string)data[(byte)ParameterCode.CodeRun], data);
            while (ienum.MoveNext())
                yield return ienum.Current;
        }

        private IEnumerator FinishOnePacket(bool sequently, string crt_name)
        {
            IEnumerator ienum = null;
            if (sequently)
            {
                if (DataQueue.Count > 0)
                {
                    Dictionary<byte, object> next = Current = DataQueue[0];
                    DataQueue.Remove(next);
                    ienum = HandleData(next, true, crt_name);
                }
                else // khong con goi tin tuan tu nao de xu ly
                    Current = null;
            }
            else
            {
                if (DataQueueConcurrently.Count > 0)
                {
                    Dictionary<byte, object> next = DataQueueConcurrently[0];
                    DataQueueConcurrently.Remove(next);
                    ienum = HandleData(next, false, crt_name);
                }
                else //ket thuc 1 coroutine xu ly goi tin doc lap, cap nhat ccr_count
                    ccr_count--;
            }

            //neu khong con goi tin nao thi ket thuc coroutine
            if (ienum == null) yield break;

            //neu con thi goi vong lap xu ly goi tin tiep theo
            while (ienum.MoveNext())
                yield return ienum.Current;
        }

        #region free memory

        internal void StopProcessor()
        {
            
        }

        internal void ClearProcessor()
        {
            DataQueue.Clear();
            DataQueueConcurrently.Clear();
            DataListeners.Clear();
        }

        internal void ClearProcessorWhenOutGame()
        {
            DataQueue.Clear();
        }

        void OnDestroy()
        {
            ClearProcessor();
			instance = null;
            //LogMng.Log(TAG, "MonoHelper Destroy--------------------");
        }

        void OnApplicationQuit()
        {
            isUpdate = false;
            if (_threadPing != null)
            {
                _threadPing.Abort();
            }
            Destroy(gameObject);
        }

#if UNITY_IOS
        private void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                ServerConfig.PNG0_REPEAT_TIME = 6;
            }
            else
            {

                ServerConfig.PNG0_REPEAT_TIME = 1;
            }
        }
#endif
#if UNITY_ANDROID
        private void OnApplicationPause(bool pause)
        {
            if (!pause)
            {
                ServerConfig.PNG0_REPEAT_TIME = 6;
            }
            else
            {

                ServerConfig.PNG0_REPEAT_TIME = 1;
                ClientGameConfig.isAutoReconnect = true;
            }
        }
#endif
#endregion

        private class PacketHandleEnd
        {
            DataProcessor.FinishPacketNotify notify;
            bool sequently = true;
            string crt_name = string.Empty;//for debug
            internal PacketHandleEnd(DataProcessor.FinishPacketNotify notify, bool sequently = true, string crt_name = "sequently")
            {
                this.notify = notify;
                this.sequently = sequently;
                this.crt_name = crt_name;
            }

            internal IEnumerator OnEnd()
            {
                yield return notify.Invoke(sequently, crt_name);
            }
        }
    }
}
