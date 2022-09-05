
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppConfig;
using GameProtocol.SLC;
using Utilitis.Command;
using CoreBase.Controller;
using Interface;
using Listener;

namespace Controller.GamePlay.Slot
{
    public class RichestManController : UIController
    {
        public RichestManController(IView view) : base(view)
        {
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
        private void RequestRichestMan(object[] param)
        {
            DataListener dataListener = new DataListener(HandlerResponseSLT4);
            SLC4_Request request = new SLC4_Request()
            {
                GameId = (string)param[0],
            };
            Network.Network.SendOperation(request, dataListener);
        }


        IEnumerator HandlerResponseSLT4(string coderun, Dictionary<byte, object> data)
        {
            SLC4_Response response =new SLC4_Response(data);
            if (response.ErrorCode != 0)
            {
                yield break;
            }

            View.OnUpdateView("ShowDataRichestMan", new object[] { response.Tops });
            yield return null;
        }
        

    }
}
