using UnityEngine;
using System.Collections.Generic;

namespace Broadcast
{
	/// <summary>
	/// loai thong bao
	/// </summary>
	public enum MessageCode
	{
		NETWORK,
		APP
	}

	/// <summary>
	/// trang thai thong bao
	/// </summary>
	public enum MessageType
	{
		/// <summary>
		/// cac trang thai cua network
		/// </summary>
		Connecting,
		Connected,
		Ready,
		Disconnect,
		Reconnected,
		Death,
		LostSession,
        CheckUpdate,
        UpdateChange,
		Ping,

        /// <summary>
        /// app
        /// </summary>
		LangChanged,
        SoundChanged,
        BgSoundChanged,
        CashChanged,
        UpdateInfo
	}

	public interface Messenger
	{

		void HandleMessage (MessageCode Code, MessageType Type, object Msg);
	}
}
