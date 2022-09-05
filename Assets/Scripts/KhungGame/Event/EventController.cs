
using CoreBase.Controller;
using GameProtocol.EVN;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Event
{
    public class EventController : UIController
    {

        EventEntity[] events;

        public EventController(IView view) : base(view)
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
        private void RequestEvent(object[] param)
        {
            if(events == null)
            {
                EVN0_Request request = new EVN0_Request();
                DataListener dataListener = new DataListener(IeHandleResponseEVN0);
                Network.Network.SendOperation(request, dataListener);
            }
            else
            {
                View.OnUpdateView("ShowEvents", new object[] { events });
            }
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void ReadEvent(object[] param)
        {
            int index = (int)param[0];
            if(events != null)
            {
                View.OnUpdateView("ShowContentEvent", events[index].ImageData);
            }
        }

        private IEnumerator IeHandleResponseEVN0(string coderun, Dictionary<byte, object> data)
        {
            EVN0_Response response = new EVN0_Response(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("CLoseEvent");
                yield break;
            }
            events = response.Data;
            View.OnUpdateView("ShowEvents", new object[] { events });
            yield return null;
        }
        
    }
}
