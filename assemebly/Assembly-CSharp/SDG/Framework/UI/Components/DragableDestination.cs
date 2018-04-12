using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SDG.Framework.UI.Components
{
	// Token: 0x02000212 RID: 530
	public class DragableDestination : MonoBehaviour, IDropHandler, IEventSystemHandler
	{
		// Token: 0x06000FD4 RID: 4052 RVA: 0x000693EE File Offset: 0x000677EE
		public void OnDrop(PointerEventData eventData)
		{
			this.dropHandler.OnDrop(eventData);
		}

		// Token: 0x040009A3 RID: 2467
		public IDropHandler dropHandler;
	}
}
