
using Base.Utils;
using CoreBase.Controller;
using Game.Gameconfig;
using GameProtocol.ANN;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilitis.Command;

namespace Controller.Home.Inbox
{
    public class InboxController : UIController
    {

        private Announce[] Annoucements;
        private Announce[] Personals;

        public InboxController(IView view) : base(view)
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
        

        #region Process View Call

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestAnnoucementANN0(object[] param)
        {
            if (ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_type == ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_Type.ANNOUNCEMENT)
            {
                if(Annoucements != null && Annoucements.Length > 0)
                {
                    View.OnUpdateView("ShowEmails", new object[] { Annoucements });
                    return;
                }
            }
            else
            {
                if (Personals != null && Personals.Length > 0)
                {
                    View.OnUpdateView("ShowEmails", new object[] { Personals });
                    return;
                }
            }

            DataListener dataListener = new DataListener(HandlerResponseANN0GetAnnoucement);
            ANN0_Request request = new ANN0_Request()
            {
                Type = (short)ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_type,
            };
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void ReadingEmail(object[] param)
        {
            //Debug.Log("ReadInbox");
            if (ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_type == ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_Type.ANNOUNCEMENT)
            {
                if (Annoucements != null)
                {
                    int id = (int)param[0];
                    //Debug.Log("ReadInbox : id: " + id);
                    var inbox = Annoucements.Where(ib => ib.AnnouneId == id).FirstOrDefault(); ;

                    //Debug.Log("ReadInbox : id: check null " + (inbox.FirstOrDefault() != null));
                    if (inbox != null)
                    {
                        View.OnUpdateView("ReadInbox", new object[] { inbox });

                        if(inbox.State == 0)
                        {
                            RequestReadOrDeleteInbox(inbox.AnnouneId, 0);
                        }
                    }
                }
            }
            else
            {
                if (Personals != null)
                {
                    int id = (int)param[0];
                    //Debug.Log("ReadInbox : id: " + id);
                    var inbox = Personals.Where(ib => ib.AnnouneId == id).FirstOrDefault(); ;

                    //Debug.Log("ReadInbox : id: check null " + (inbox.FirstOrDefault() != null));
                    if (inbox != null)
                    {
                        View.OnUpdateView("ReadInbox", new object[] { inbox });
                        if (inbox.State == 0)
                        {
                            RequestReadOrDeleteInbox(inbox.AnnouneId, 0);
                        }
                    }
                }
            }
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void DeleteInbox(object[] param)
        {
            //Debug.Log("DeleteInbox : " + (int)param[0] + " --- " + GameConfig.ANNOUCEMENT_TYPE.Annoucement_type);
            if (ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_type == ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_Type.ANNOUNCEMENT) return;
            int index = (int)param[0];
            if (index < 0) return;
            if (ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_type == ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_Type.ANNOUNCEMENT)
            {
                RemoveAnnounce(Annoucements, index);
            }
            else
            {
                RemoveAnnounce(Personals, index);
            }
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void ClearData(object[] param)
        {
            Annoucements = null;
            Personals = null;
        }

        #endregion

        #region Handler Response

        private IEnumerator HandlerResponseANN0GetAnnoucement(string coderun, Dictionary<byte, object> data)
        {
            ANN0_Response response = new ANN0_Response(data);

            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }
            switch (ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_type)
            {
                case ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_Type.ANNOUNCEMENT:
                    Annoucements = response.data;
                    View.OnUpdateView("ShowEmails", new object[] { Annoucements });
                    //ShowInbox(Annoucements, "ShowAnnouncement");
                    break;
                case ClientGameConfig.ANNOUCEMENT_TYPE.Annoucement_Type.PERSONAL:
                    Personals = response.data;
                    View.OnUpdateView("ShowEmails", new object[] { Personals });
                    //ShowInbox(Personals, "ShowInbox");
                    break;
            }
            yield return null;
        }

        private IEnumerator HandlerResponseANN1DeleteInbox(string coderun, Dictionary<byte, object> data)
        {
            ANN1_Response response = new ANN1_Response(data);

            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }
            yield return null;
        }

        private void HandlerResponseANN2SendInbox(byte[] data)
        {

        }

        private void ShowInbox(Announce[] announce, string function)
        {
            for (int i = 0; i < announce.Length; i++)
            {
                View.OnUpdateView(function, new object[] { announce[i] });
            }
            View.OnUpdateView("ShowInfoInboxFirst");
        }

        private void RemoveAnnounce(Announce[] announce, int id)
        {
            List<Announce> tempList = announce.ToList();
            var inbox = announce.Where(ibx => ibx.AnnouneId == id);
            Announce ib = inbox.FirstOrDefault();
            //Debug.Log("RemoveAnnounce " + (ib != null) + " -- announce " + announce.Length);
            if (ib != null)
            {
                tempList.Remove(ib);
                RequestReadOrDeleteInbox(id, 1);
                announce = tempList.ToArray();
            }
        }

        private void RequestReadOrDeleteInbox(int id, byte type)
        {
            //Debug.Log("RequestDeleteInbox " + id);
            DataListener dataListener = new DataListener(HandlerResponseANN1DeleteInbox);
            ANN1_Request request = new ANN1_Request()
            {
                AnnouneId = id,
                Type = type
            };
            Network.Network.SendOperation(request, dataListener);
        }

#endregion
        

    }
}
