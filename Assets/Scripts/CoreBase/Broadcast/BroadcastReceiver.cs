using UnityEngine;
using System.Collections.Generic;

namespace Broadcast
{
    /// <summary>
    /// Gui thong bao toi tat ca cac messenger dang ky lang nghe
    /// </summary>
    public class BroadcastReceiver
    {

        private static BroadcastReceiver instance = new BroadcastReceiver();
        public static BroadcastReceiver Instance
        {
            get{
                return instance;
            }
        }

        private Dictionary<MessageCode, List<Messenger>> Messengers = new Dictionary<MessageCode, List<Messenger>>();

        public void BroadcastMessage(MessageCode MsgCode, MessageType MsgType, object Msg)
        {
            if (Messengers.ContainsKey(MsgCode))
            {
                List<Messenger> Msgs = Messengers[MsgCode];
                Messenger[] arrMsgs = new Messenger[Msgs.Count];
                Msgs.CopyTo(arrMsgs);
                foreach (Messenger msg in arrMsgs)
                {
                    msg.HandleMessage(MsgCode, MsgType, Msg);
                }
                //List<Messenger>.Enumerator en = Msgs.GetEnumerator();
                //while (en.MoveNext())
                //{
                //    Messenger msg = en.Current;
                //    msg.HandleMessage(MsgCode, MsgType, Msg);
                //}
            }
        }

        public void AddMessenger(MessageCode MsgCode, Messenger Messenger)
        {
            List<Messenger> Msgs = null;
            if (!Messengers.ContainsKey(MsgCode))
            {
                Msgs = new List<Messenger>();
                Messengers.Add(MsgCode, Msgs);
            }
            else
            {
                Msgs = Messengers[MsgCode];
            }

            Msgs.Add(Messenger);
        }

        public void RemoveMessenger(MessageCode MsgCode, Messenger Messenger)
        {
            if (Messengers.ContainsKey(MsgCode))
            {
                List<Messenger> Msgs = Messengers[MsgCode];
                Msgs.Remove(Messenger);
            }
        }

        public void ClearMessengers()
        {
            Dictionary<MessageCode, List<Messenger>>.Enumerator en = Messengers.GetEnumerator();
            while (en.MoveNext())
            {
                KeyValuePair<MessageCode, List<Messenger>> pair = en.Current;
                pair.Value.Clear();
            }

            Messengers.Clear();
        }
    }
}
