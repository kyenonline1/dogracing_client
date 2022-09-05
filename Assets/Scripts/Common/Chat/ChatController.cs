using Base;
using CoreBase.Controller;
using GameProtocol.MSG;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitis.Command;

namespace Controller.Common.Chat
{
    public class ChatController : UIController
    {
        public ChatController(IView view) : base(view)
        {
        }

        protected override void RegisterPushHandlers()
        {
            base.RegisterPushHandlers();
            Network.PushManager.Instance.RegisterPushHandler("MSG", this);
        }

        public override void UnregisterPushHandlers()
        {
            base.UnregisterPushHandlers();
            Network.PushManager.Instance.UnRegisterPushHandler("MSG", this);
        }

        public override IEnumerator HandlePush(string coderun, Dictionary<byte, object> data)
        {
            switch (coderun)
            {
                case "MSG0":
                    HandlerPushPlayerChat_MSG0_Push(data);
                    break;
            }
            yield return base.HandlePush(coderun, data);
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
        private void SendChat(object[] param)
        {
            string mesage = (string)param[0];
            byte type = (byte)param[1];
            MSG0_Request request = new MSG0_Request()
            {
                Message = mesage,
                Type = type
            };
            DataListener dataListener = new DataListener(HandlerResponseMeChat);
            Network.Network.SendOperation(request, dataListener);
            //Debug.Log("CONTROLLER SENDCHAT");
        }


        private IEnumerator HandlerResponseMeChat(string coderun, Dictionary<byte, object> data)
        {
            yield return null;
        }

        private void HandlerPushPlayerChat_MSG0_Push(Dictionary<byte, object> data)
        {
            MSG0_Push push = new MSG0_Push(data);
            Debug.Log("HandlePush Chat: ");
            if (push.Type == 1)
            {
                View.OnUpdateView("ShowPlayerChatEmotion", push.Nickname, push.Message, push.UserId);
            }
            else
            {
                View.OnUpdateView("ShowPlayerChat", push.Nickname, push.Message, push.UserId);
            }
        }

        void AddListeners()
        {
            //AddListener(Command.CHAT_COMMAND.MSG_0, HandlerResponseMeChat);
            //AddListener(Command.CHAT_COMMAND.MSG_0_push, HandlerPushPlayerChat_MSG0_Push);
        }


    }
}
