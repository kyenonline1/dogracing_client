using UnityEngine;
using System.Collections;
using Interface;

namespace CoreBase.Controller
{
    public class BaseGameController : BaseAppController
    {
        public BaseGameController(IView view) : base(view)
        {

        }

       
        public enum TableMode
        {
            GAME_PLAY =0, PLAY_VIDEO =1
        }

		protected override void HandleNetworkMessage (Broadcast.MessageType Type, object Msg)
		{
			if (Type == Broadcast.MessageType.Ping) 
			{
				float delta_time = (float)Msg;
				View.OnUpdateView ("UpdatePing", delta_time);
				return;
			}
			base.HandleNetworkMessage (Type, Msg);
		}
        
    }
}

