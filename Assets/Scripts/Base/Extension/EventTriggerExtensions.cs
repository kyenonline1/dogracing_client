using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Base.Extension
{
	public static class EventTriggerExtensions {
		public static void AddPointerClickEvent(this EventTrigger eventTrigger, UnityAction<BaseEventData> callback)
		{
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(callback);
			eventTrigger.triggers.Add(entry);
		}
	}
}
