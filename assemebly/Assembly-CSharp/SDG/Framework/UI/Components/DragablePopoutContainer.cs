using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SDG.Framework.UI.Components
{
	// Token: 0x02000213 RID: 531
	public class DragablePopoutContainer : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x06000FD6 RID: 4054 RVA: 0x00069404 File Offset: 0x00067804
		public void OnPointerDown(PointerEventData eventData)
		{
			if (Sleek2DragManager.isDragging)
			{
				return;
			}
			if (this.target == null)
			{
				return;
			}
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			RectTransform rectTransform = this.target.parent as RectTransform;
			if (rectTransform == null)
			{
				return;
			}
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out this.offset))
			{
				return;
			}
			this.offset.x = (this.offset.x + rectTransform.rect.size.x * rectTransform.pivot.x) / rectTransform.rect.size.x;
			this.offset.y = (this.offset.y + rectTransform.rect.size.y * rectTransform.pivot.y) / rectTransform.rect.size.y;
			this.target.SetAsLastSibling();
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00069530 File Offset: 0x00067930
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (Sleek2DragManager.isDragging)
			{
				return;
			}
			if (this.target == null)
			{
				return;
			}
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.min = this.target.anchorMin;
			this.max = this.target.anchorMax;
			Sleek2DragManager.isDragging = true;
			this.isDragging = true;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00069598 File Offset: 0x00067998
		public void OnDrag(PointerEventData eventData)
		{
			if (!this.isDragging)
			{
				return;
			}
			RectTransform rectTransform = this.target.parent as RectTransform;
			if (rectTransform == null)
			{
				return;
			}
			Vector2 a;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out a))
			{
				return;
			}
			a.x = (a.x + rectTransform.rect.size.x * rectTransform.pivot.x) / rectTransform.rect.size.x;
			a.y = (a.y + rectTransform.rect.size.y * rectTransform.pivot.y) / rectTransform.rect.size.y;
			Vector2 b = a - this.offset;
			b.x = Mathf.Clamp(b.x, -this.min.x, 1f - this.max.x);
			b.y = Mathf.Clamp(b.y, -this.min.y, 1f - this.max.y);
			this.target.anchorMin = this.min + b;
			this.target.anchorMax = this.max + b;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00069725 File Offset: 0x00067B25
		public void OnEndDrag(PointerEventData eventData)
		{
			if (!this.isDragging)
			{
				return;
			}
			Sleek2DragManager.isDragging = false;
			this.isDragging = false;
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00069740 File Offset: 0x00067B40
		private void OnDisable()
		{
			if (this.isDragging)
			{
				Sleek2DragManager.isDragging = false;
				this.isDragging = false;
			}
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0006975A File Offset: 0x00067B5A
		private void Reset()
		{
			this.target = (base.transform as RectTransform);
		}

		// Token: 0x040009A4 RID: 2468
		public RectTransform target;

		// Token: 0x040009A5 RID: 2469
		private bool isDragging;

		// Token: 0x040009A6 RID: 2470
		private Vector2 offset;

		// Token: 0x040009A7 RID: 2471
		private Vector2 min;

		// Token: 0x040009A8 RID: 2472
		private Vector2 max;
	}
}
