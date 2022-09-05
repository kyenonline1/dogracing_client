using System.Collections.Generic;
using Interface;
using System.Collections;
using UnityEngine;
using System;
using AppConfig;
using GameProtocol.Protocol;
using Utilities.Custom;
using Utilities;
using GameProtocol.Base;

namespace Network
{
    public class PushManager
    {

        //private static readonly string TAG = "PushProcessor";

        //internal delegate void InvokeEvent(Dictionary<string, object> param);
        
        /// <summary>
        /// <code, controller>, luu cac controller theo code xu ly
        /// </summary>
        private Dictionary<string, HashSet<IPushHandler>> PushHandlers = new Dictionary<string, HashSet<IPushHandler>>();

        private DefaultPushHandler mDefaultPushHandler = new DefaultPushHandler();
        
        /// <summary>
        /// list of unprocess packet
        /// </summary>
        private List<Dictionary<byte, object>> UnProcessDataQueue = new List<Dictionary<byte, object>>();

        /// <summary>
        /// truong hop user logout thi co 1 push ve, luc nay ko co controller nao xu ly nen se wait, sau do user login vao 1 tai khoan khac
        /// khi do push nay duoc xu ly, nhung khi nay thi push nay khong phai cua user moi nen se bi sai
        /// giai phap: neu client chuyen scene sang lan thu 2 thi bo qua het cac push dang wait
        /// </summary>
       // private int SwitchSceneTimes = 0;

        private static PushManager instance = new PushManager();
        private PushManager()
        {
            //DefaultPushHandler dph = new DefaultPushHandler();
            //register nhưng xử lý ngầm ngoài UI
            //RegisterPushHandler(GameConfig.RequestCode.LGI, dph);

            mDefaultPushHandler.UnProcessDataQueue = UnProcessDataQueue;
        }
        internal static PushManager Instance
        {
            get { return instance; }
        }

        internal void RegisterPushHandler(string code, IPushHandler controller)
        {
            //LogMng.Log("", "RegisterController " + code);
            HashSet<IPushHandler> uics;

            if (!PushHandlers.ContainsKey(code))
            {
                //LogMng.Log("", "Controller 111111111111111 " + controller.GetType() );
                uics = new HashSet<IPushHandler>();
                PushHandlers.Add(code, uics);
                //UIControllers.Remove(code);
            }
            else
            {
                //LogMng.Log("", "Controller 222222222222222222222" + controller.GetType());
                uics = PushHandlers[code];
                uics.Remove(controller);
            }
            //add new controller cooresponding with code
            uics.Add(controller);
            //LogMng.Log(TAG, "Controller 333333333333333" + controller.GetType());
            //duyet unprocessdatapush day vao controller xu ly
            if (UnProcessDataQueue.Count > 0)
            {
                //LogMng.Log(TAG, "Controller " + controller.GetType() + " process UnProcessDataQueue-------------");
                MonoInstance.Instance.StartCoroutine(processUnProcessData(code, controller));
            }
        }

        private IEnumerator processUnProcessData(string code, IPushHandler controller)
        {
            List<Dictionary<byte, object>> clone = new List<Dictionary<byte, object>>(UnProcessDataQueue);
            UnProcessDataQueue.Clear();
            List<Dictionary<byte, object>>.Enumerator browser = clone.GetEnumerator();

            while (browser.MoveNext())
            {
                Dictionary<byte, object> data = browser.Current;
                
                string p_code = ((string)data[(byte)ParameterCode.CodeRun]).Substring(0, 3);

                //LogMng.Log(TAG, "Controller " + controller.GetType().Name + " process " + p_code + ", " + code + ", " + (p_code == code) + " -------------");

                if (p_code == code)
                {
                    //LogMng.Log(TAG, "0000000000000000000000000-------------------");
                    IEnumerator ienum = controller.HandlePush((string)data[(byte)ParameterCode.CodeRun], data);

                    while (ienum.MoveNext())
                        yield return ienum.Current;
                }
                else
                    UnProcessDataQueue.Add(data);//un process, re add
            }

            clone.Clear();
        }

        internal void UnRegisterPushHandler(string code, IPushHandler controller)
        {
            //Logger.Log(TAG, "UnRegisterController " + code);
            HashSet<IPushHandler> uics;
            if (PushHandlers.ContainsKey(code))
            {
                uics = PushHandlers[code];
                uics.Remove(controller);
            }
        }

        internal void UnRegisterAllPushHandler()
        {
            PushHandlers.Clear();
        }

        //[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
        internal IEnumerator HandlePush(string coderun, Dictionary<byte, object> data)
        {
            string code = coderun.Substring(0, 3);
            //LogMng.Log("PUSHMANAGER", "HandlePush: 1111111111111111111111 " + PushHandlers.ContainsKey(code) + " , code: " + code);
            if (PushHandlers.ContainsKey(code))
            {
                HashSet<IPushHandler> uics = PushHandlers[code];

                HashSet<IPushHandler> hash_browser = new HashSet<IPushHandler>(uics);//, uics.Comparer);
                //LogMng.Log("PUSHMANAGER", "HandlePush 2222222222222222222222222" + coderun + ": controller count = " + uics.Count + ", ");
                if (hash_browser.Count > 0)
                {
                    HashSet<IPushHandler>.Enumerator browser = hash_browser.GetEnumerator();
                    while (browser.MoveNext())
                    {
                        IPushHandler iph = browser.Current;
                        IEnumerator IEnumPush = iph.HandlePush(coderun, data);
                        while (IEnumPush.MoveNext())
                            yield return IEnumPush.Current;

                       // LogMng.Log("PUSHMANAGER", "HandlePush 3333333333333333333333" + coderun + ": controller: " );
                    }
                }
                else
                {
                    //Debug.LogError("HERRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
                    IEnumerator IEnumPush = mDefaultPushHandler.HandlePush(coderun, data);
                    while (IEnumPush.MoveNext())
                    {
                        //Debug.LogError("HERRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR ---------------------------");
                        yield return IEnumPush.Current;
                    }
                }
            }
            else
            {
                //Debug.LogError("HERRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR ---------------0000000000000000------------" + " , code: " + code);
                if (!code.Equals("BSF") && !code.Equals("DOG"))
                {
                    IEnumerator IEnumPush = mDefaultPushHandler.HandlePush(coderun, data);
                    while (IEnumPush.MoveNext())
                        yield return IEnumPush.Current;
                }
            }
            yield return new WaitForEndOfFrame();
        }

        /// <summary>
        /// truong hop user logout thi co 1 push ve, luc nay ko co controller nao xu ly nen se wait, sau do user login vao 1 tai khoan khac
        /// khi do push nay duoc xu ly, nhung khi nay thi push nay khong phai cua user moi nen se bi sai
        /// giai phap: cài đặt một DefaultPushHandler xử lý các hàm quan trọng như LGI-1...
        /// </summary>
        internal class DefaultPushHandler : IPushHandler
        {
            internal List<Dictionary<byte, object>> UnProcessDataQueue;
            public IEnumerator HandlePush(string coderun, Dictionary<byte, object> data)
            {
                //LogMng.Log(TAG, "DefaultPushHandler handle " + coderun + "--------------------");              
                UnProcessDataQueue.Add(data);
                yield return null;
            }
        }
    }
}