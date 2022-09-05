using UnityEngine;
using System.Collections.Generic;
using Interface;
using System.Collections;
using System.Threading;
using System;

namespace Utilities.Custom
{
    public class MonoInstance : MonoBehaviour
    {
        public static readonly string TAG = "MonoInstance";
        
        private static MonoInstance instance = null;
        private static int UnityThreadId;
//        static MonoInstance()
//        {
//            ////NGUIDebug.Log("check MonoBehaviourHelper: instance = " + (instance == null) + " --------------------");
//            if (instance == null)
//            {
//                instance = (new GameObject("_BaseMonoInstance")).AddComponent<MonoInstance>();
//            }
//        }
        public static MonoInstance Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (new GameObject("_BaseMonoInstance")).AddComponent<MonoInstance>();
                }
                return instance;
            }

            private set
            {
                if (instance == null) instance = value;
            }
        }

        private InputManager InputMng;

        private TaskManager TaskMng;
        
        public void Init()
        {
            InputMng = new InputManager();
            TaskMng = new TaskManager(this);
        }

        void Awake()
        {
            if (InputMng == null || TaskMng == null)
                Init();
            DontDestroyOnLoad(gameObject);
            if (instance == null) instance = this;
            UnityThreadId = System.Threading.Thread.CurrentThread.GetHashCode();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && InputMng != null)
            {
                InputMng.HandleEscape();
            }

            if (TaskMng != null)
                TaskMng.BrowseAndExecuteTask();
        }

        /// <summary>
        /// gui 1 event duoc yeu cau chay trong main thread, event nay co the bi interrupt neu can
        /// </summary>
        /// <param name="coroutine"></param>
        public void ExecuteIEnumerator(IEnumerator coroutine)
        {
            if (TaskMng != null)
                TaskMng.ExecuteIEnumerator(coroutine);
        }
        
        public void KillCoroutine(IEnumerator ienu)
        {
            if (TaskMng != null)
                TaskMng.KillCoroutine(ienu);
        }

        public void AttachIEscapable(ICloseable closeable)
        {
            if (InputMng != null)
                InputMng.AttachIEscapable(closeable);
        }

        public void DetachIEscapable(ICloseable closeable)
        {
            if (InputMng != null)
                InputMng.DetachIEscapable(closeable);
        }

        void OnDestroy()
        {
            InputMng = null;
            TaskMng = null;
			//DestroyImmediate (instance);
            LogMng.Log(TAG, "MonoHelper Destroy--------------------");
        }

        void OnApplicationQuit()
        {
            Network.Network.StopNetwork();
            Destroy(gameObject);
        }

        private class TaskManager
        {
            private MonoBehaviour MonoInstance;

            /// <summary>
            /// luu cac xu ly chi co the thuc hien duoc trong mainthread duoc gui tu cac thread ben ngoai</br>
            /// hien tai doi voi cac DontDestroy coroutine thi chua biet khi nao coroutine ket thuc de huy reference
            /// </summary>
            private HashSet<IEnumerator> IEnumeratorMng = new HashSet<IEnumerator>();

            internal TaskManager(MonoBehaviour mono)
            {
                MonoInstance = mono;
            }

            internal void BrowseAndExecuteTask()
            {
                lock (IEnumeratorMng)
                {
                    if (IEnumeratorMng.Count == 0)
                        return;

                    IEnumerator[] arrMsgs = new IEnumerator[IEnumeratorMng.Count];
                    IEnumeratorMng.CopyTo(arrMsgs);
                    //foreach (IEnumerator iemu in arrMsgs)
                    //{
                    //    Coroutine c = MonoInstance.StartCoroutine(iemu);
                    //}

                    IEnumeratorMng.Clear();
                }
            }

            /// <summary>
            /// gui 1 event duoc yeu cau chay trong main thread, event nay co the bi interrupt neu can
            /// </summary>
            /// <param name="coroutine"></param>
            internal void ExecuteIEnumerator(IEnumerator coroutine)
            {
                //Logger.Log(TAG, "ExecuteIEnumerator: " + coroutine.GetType() + ", " + coroutine.GetHashCode());
                if (System.Threading.Thread.CurrentThread.GetHashCode() == UnityThreadId)
                {
                    //Logger.LogException(new ArgumentException("IEnumerator duoc goi trong MainThread: " + coroutine.GetType()));
                    MonoInstance.StartCoroutine(coroutine);
                    return;
                }
                lock (IEnumeratorMng)
                {
                    IEnumeratorMng.Add(coroutine);
                }

            }

            internal void KillCoroutine(IEnumerator ienu)
            {
                MonoInstance.StopCoroutine(ienu);
                lock (IEnumeratorMng)
                {
                    IEnumeratorMng.Remove(ienu);
                }
            }
        }

        private class InputManager
        {
            /// <summary>
            /// xu ly backpress cho man hinh/dialog top
            /// </summary>
            private HashSet<ICloseable> IEscapableHash = new HashSet<ICloseable>();

            private List<ICloseable> IEscapableList = new List<ICloseable>();

            internal void HandleEscape()
            {
                lock (IEscapableList)
                {
                    if (IEscapableList.Count > 0)
                    {
                        ICloseable ies = IEscapableList[IEscapableList.Count - 1];
                        ies.Close();
                    }
                }
            }

            internal void AttachIEscapable(ICloseable closeable)
            {
                lock (IEscapableList)
                {
                    if (IEscapableHash.Contains(closeable))
                    {
                        IEscapableHash.Remove(closeable);
                        IEscapableList.Remove(closeable);
                    }
                    IEscapableHash.Add(closeable);
                    IEscapableList.Insert(0, closeable);
                    
                }
            }

            internal void DetachIEscapable(ICloseable closeable)
            {
                lock (IEscapableList)
                {
                    if (IEscapableHash.Contains(closeable))
                    {
                        IEscapableHash.Remove(closeable);
                        IEscapableList.Remove(closeable);
                    }
                }
            }
        }
    }
}
