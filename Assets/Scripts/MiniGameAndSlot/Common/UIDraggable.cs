using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MiniGame.Common
{
	public class UIDraggable : MonoBehaviour, IDragHandler {

		protected RectTransform rect;

		/// <summary>
		/// Start is called on the frame when a script is enabled just before
		/// any of the Update methods is called the first time.
		/// </summary>
		void Start()
		{
			rect = GetComponent<RectTransform>();
		}

		public void OnDrag(PointerEventData eventData)
		{
			if(rect == null)
				return;
			var pointerData = eventData as UnityEngine.EventSystems.PointerEventData;
			if (pointerData == null) { return; }
	
	
			var currentPosition = rect.position;
			currentPosition.x += pointerData.delta.x * 0.0926f;
			currentPosition.y += pointerData.delta.y * 0.0926f;
			rect.position = currentPosition;
		}
	}

}
