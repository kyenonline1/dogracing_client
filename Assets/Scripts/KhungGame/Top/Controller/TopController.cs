
using CoreBase.Controller;
using GameProtocol.TOP;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitis.Command;

namespace Controller.Home.Top
{
    public class TopController : UIController
    {

        public TopController(IView view) : base(view)
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
        private void RequestTopCate(object[] param)
        {
            DataListener dataListener = new DataListener(HandlerResponseTopCateTOP0);
            TOP0CateRequest request = new TOP0CateRequest();
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestUserTopTOP1(object[] param)
        {
            DataListener dataListener = new DataListener(HandlerResponseUserTopTOP1);
            TOP1EventRequest request = new TOP1EventRequest();
            request.Season = (string)param[0];
            Network.Network.SendOperation(request, dataListener);
        }

        private IEnumerator HandlerResponseTopCateTOP0(string coderun, Dictionary<byte, object> data)
        {
            TOP0CateResponse response = new TOP0CateResponse(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }
            View.OnUpdateView("CacheSessions", new object[] { response.Seasons });
            if (response.Seasons != null && response.Seasons.Length > 0)
            {
                RequestUserTopTOP1(new object[] { response.Seasons[0] });
            }
            else
            {
                View.OnUpdateView("DisableViewTop");
            }
            yield return null;
        }

        private IEnumerator HandlerResponseUserTopTOP1(string coderun, Dictionary<byte, object> data)
        {
            TOP1EventResponse response = new TOP1EventResponse(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }
            View.OnUpdateView("ShowUserTops", new object[] { response });
            yield return null;
        }

        private IEnumerator HandlerResponseUserTopJackpot(string coderun, Dictionary<byte, object> data)
        {
            //TOP2_Response response = new TOP2_Response(data);
            //if (response.ErrorCode != 0)
            //{
            //    View.OnUpdateView("ShowError", response.ErrorMsg);
            //    yield break;
            //}
            //View.OnUpdateView("ShowUserTopJackpot", new object[] { response.Users });
            yield return null;
        }

    }
}
