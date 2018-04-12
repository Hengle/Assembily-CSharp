using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SDG.Framework.UI.Components
{
	// Token: 0x02000218 RID: 536
	public class DragableTabDestination : MonoBehaviour, IDropHandler, IEventSystemHandler
	{
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000FF6 RID: 4086 RVA: 0x00069C5C File Offset: 0x0006805C
		// (remove) Token: 0x06000FF7 RID: 4087 RVA: 0x00069C94 File Offset: 0x00068094
		public event TabDockedHandler tabDocked;

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00069CCC File Offset: 0x000680CC
		public void OnDrop(PointerEventData eventData)
		{
			if (Sleek2DragManager.item is Sleek2WindowTab)
			{
				Sleek2DragManager.dropped = true;
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform as RectTransform, eventData.position, eventData.pressEventCamera, out vector);
				this.triggerTabDocked(Sleek2DragManager.item as Sleek2WindowTab, vector.x);
			}
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00069D24 File Offset: 0x00068124
		protected virtual void triggerTabDocked(Sleek2WindowTab tab, float offset)
		{
			if (this.tabDocked != null)
			{
				this.tabDocked(this.dock, tab, offset);
			}
		}

		// Token: 0x040009B6 RID: 2486
		public Sleek2WindowDock dock;
	}
}
