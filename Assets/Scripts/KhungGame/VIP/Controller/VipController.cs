//using Base;
//using GameProtocol.TOP;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Utilitis.Command;

//namespace Controller.Home.Vip
//{
//    public class VipController : UIController
//    {

//        public override void StartController(UIViewScript view, string TAG = "UIController")
//        {
//            base.StartController(view, TAG);
//            AddListenerHandler();
//        }

//        protected override void OnDestroyProcessor(object[] data)
//        {
//            base.OnDestroyProcessor(data);
//        }

//        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
//        private void RequestListVip(object[] param)
//        {
//            TOP3_Request request = new TOP3_Request();
//            Request(request.SerializeToBytes(), request.GetCommand());
//        }

//        private void HandlerResponseListVipTOP3(byte[] data)
//        {
//            TOP3_Response response = data.DeserializeFromBytes<TOP3_Response>();
//            if (response.ErrorCode != 0)
//            {
//                UpdateToView("ShowError", response.ErrorMsg);
//                return;
//            }
//            UpdateToView("ShowListVips", new object[] { response.Vips });
//        }

//        void AddListenerHandler()
//        {
//            AddListener(Command.TOP_COMMAND.TOP_3, HandlerResponseListVipTOP3);
//        }
//    }
//}
