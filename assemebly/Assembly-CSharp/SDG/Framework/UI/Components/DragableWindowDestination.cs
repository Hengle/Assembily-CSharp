using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SDG.Framework.UI.Components
{
	// Token: 0x0200021C RID: 540
	public class DragableWindowDestination : MonoBehaviour, IDropHandler, IEventSystemHandler
	{
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06001008 RID: 4104 RVA: 0x00069E38 File Offset: 0x00068238
		// (remove) Token: 0x06001009 RID: 4105 RVA: 0x00069E70 File Offset: 0x00068270
		public event WindowDockedHandler windowDocked;

		// Token: 0x0600100A RID: 4106 RVA: 0x00069EA8 File Offset: 0x000682A8
		public void OnDrop(PointerEventData eventData)
		{
			if (Sleek2DragManager.item is Sleek2WindowTab)
			{
				RectTransform rectTransform = base.transform as RectTransform;
				Rect rect = rectTransform.rect;
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out vector);
				float num;
				ESleek2PartitionDirection esleek2PartitionDirection;
				if (vector.x < rect.width / 2f)
				{
					num = vector.x;
					esleek2PartitionDirection = ESleek2PartitionDirection.LEFT;
				}
				else
				{
					num = rect.width - vector.x;
					esleek2PartitionDirection = ESleek2PartitionDirection.RIGHT;
				}
				float num2;
				ESleek2PartitionDirection esleek2PartitionDirection2;
				if (vector.y < rect.height / 2f)
				{
					num2 = vector.y;
					esleek2PartitionDirection2 = ESleek2PartitionDirection.DOWN;
				}
				else
				{
					num2 = rect.height - vector.y;
					esleek2PartitionDirection2 = ESleek2PartitionDirection.UP;
				}
				ESleek2PartitionDirection esleek2PartitionDirection3;
				if (num < 64f || num2 < 64f)
				{
					if (num < num2)
					{
						esleek2PartitionDirection3 = esleek2PartitionDirection;
					}
					else
					{
						esleek2PartitionDirection3 = esleek2PartitionDirection2;
					}
				}
				else
				{
					esleek2PartitionDirection3 = ESleek2PartitionDirection.NONE;
				}
				if (esleek2PartitionDirection3 != ESleek2PartitionDirection.NONE)
				{
					Sleek2WindowTab sleek2WindowTab = Sleek2DragManager.item as Sleek2WindowTab;
					if (sleek2WindowTab.window.dock != this.dock || sleek2WindowTab.window.dock.windows.Count > 1)
					{
						Sleek2DragManager.dropped = true;
						this.triggerWindowDocked(sleek2WindowTab, esleek2PartitionDirection3);
					}
				}
			}
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00069FF0 File Offset: 0x000683F0
		protected virtual void triggerWindowDocked(Sleek2WindowTab tab, ESleek2PartitionDirection direction)
		{
			if (this.windowDocked != null)
			{
				this.windowDocked(this.dock, tab, direction);
			}
		}

		// Token: 0x040009B9 RID: 2489
		public Sleek2WindowDock dock;
	}
}
