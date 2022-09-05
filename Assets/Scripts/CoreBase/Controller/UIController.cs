using UnityEngine;
using System.Collections;
using Interface;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Utilities;
using Broadcast;

namespace CoreBase.Controller
{
    internal class HandleUIEventAttribute : Attribute
    {
        public HandlerType EventType { get; set; }
        /// <summary>
        /// Name: Coderun, su dung cho ham push
        /// </summary>
        public string Name { get; set; }
    }
    public class UIController : IController, IPushHandler, Messenger
    {
        protected delegate void DlgHandleUIEvent(params object[] parameters);

        protected delegate IEnumerator DlgHandlePush(Dictionary<byte, object> data);

        /// <summary>
        /// Dictionary<coderun, IPushHandler> - quản lý các PushHandler
        /// </summary>
        private readonly Dictionary<string, DlgHandlePush> PushHandlers = new Dictionary<string, DlgHandlePush>();

        /// <summary>
        /// Dictionary<method_name, DlgHandleUIEvent> - quản lý các PushHandler
        /// </summary>
        private readonly Dictionary<string, DlgHandleUIEvent> EventHandlers = new Dictionary<string, DlgHandleUIEvent>();

        protected IView View = null;
        public UIController(IView view)
        {
            this.View = view;
        }
        public virtual void StartController()
        {
            RegisterEventHandlers();
            RegisterMessengers();
            RegisterPushHandlers();
        }

        public virtual void StopController()
        {
            UnregisterMessengers();
            UnregisterPushHandlers();
        }

        #region Register events
        protected void RegisterEventHandler(DlgHandleUIEvent eh)
        {
            EventHandlers.Add(eh.Method.Name, eh);
        }

        /// <summary>
        /// đăng ký các delegate handler ui event + handler push ở đây 
        /// </summary>
        protected void RegisterEventHandlers()
        {
            //LogMng.Log("UIController", "RegisterEventHandlers: " + GetType());
            MethodInfo[] methods = this.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            //LogMng.Log("UIController", "methods.count: " + methods.Length);

            foreach (MethodInfo m in methods)
            {
                var attributes = (HandleUIEventAttribute[])m.GetCustomAttributes(typeof(HandleUIEventAttribute), false);
                if (attributes == null || attributes.Length == 0) continue;
                var attribute = attributes[0];
                HandlerType event_type = attribute.EventType;
                //LogMng.Log("UIController", "attribute: " + (HandlerType)event_type);
                if (event_type == HandlerType.EVN_VIEW_HANDLER)
                {
                    //LogMng.Log("UIController", "RegisterEventHandler: " + m.Name);
                    RegisterEventHandler((DlgHandleUIEvent)Delegate.CreateDelegate(typeof(DlgHandleUIEvent), this, m.Name));
                }
                else if(event_type == HandlerType.EVN_PUSH_HANDLER)
                {
                    //LogMng.LogError("UIController", "RegisterPushHandler: " + attribute.Name + ", " + m.GetType());
                    PushHandlers.Add(attribute.Name, (DlgHandlePush)Delegate.CreateDelegate(typeof(DlgHandlePush), this, m.Name));
                }
            }
        }

        #endregion

        #region handle view event
        void IController.OnHandleUIEvent(string Function, params object[] Params)
        {
            if (EventHandlers.ContainsKey(Function))
                EventHandlers[Function].Invoke(Params);
            else
                LogMng.Log("UIController", "Handler " + Function + " has not been declaced !!!");
        }
        #endregion

        #region handle push event
        protected virtual void RegisterPushHandlers()
        {

        }

        public virtual void UnregisterPushHandlers()
        {

        }

        public virtual IEnumerator HandlePush(string coderun, Dictionary<byte, object> data)
        {
            //LogMng.LogError("UIController", "HandlePush: " + coderun + ", " + PushHandlers.GetHashCode() + ", " + PushHandlers.ContainsKey(coderun));
            if (PushHandlers.ContainsKey(coderun))
            {
                //khi register 1 code, co the controller ko xu ly het cac push cua code do
                IEnumerator ienu = PushHandlers[coderun].Invoke(data);
                while (ienu.MoveNext())
                    yield return ienu.Current;
                yield return new WaitForEndOfFrame();
            }

            yield return null;
        }


        #endregion

        #region handle message
        protected virtual void RegisterMessengers()
        {

        }

        protected virtual void UnregisterMessengers()
        {

        }
        void Messenger.HandleMessage(MessageCode Code, MessageType Type, object Msg)
        {
            switch (Code)
            {
                case MessageCode.APP:
                    HandleAppMessage(Type, Msg);
                    break;
                case MessageCode.NETWORK:
                    HandleNetworkMessage(Type, Msg);
                    break;
                default:
                    break;
            }
        }

        #region handle APP message
        protected virtual void HandleAppMessage(MessageType Type, object Msg)
        {
            //IEnumerator e = this.WaitForSeconds(2);
            //throw new NotImplementedException();
        }
        #endregion

        #region handle Network message
        protected virtual void HandleNetworkMessage(MessageType Type, object Msg)
        {
            //throw new NotImplementedException();
        }
        #endregion
        #endregion
    }
}