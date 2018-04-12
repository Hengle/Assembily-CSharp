using System;
using System.Collections.Generic;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SDG.Framework.UI.Components
{
	// Token: 0x02000222 RID: 546
	public class ResizeHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x06001026 RID: 4134 RVA: 0x0006A444 File Offset: 0x00068844
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (Sleek2DragManager.isDragging)
			{
				return;
			}
			if ((this.horizontalPosition && this.verticalPosition) || (this.horizontalSize && this.verticalSize))
			{
				Sleek2Pointer.cursor = Resources.Load<Texture2D>("UI/Cursors/Cursor_Diagonal_45");
			}
			else if ((this.horizontalPosition && this.verticalSize) || (this.horizontalSize && this.verticalPosition))
			{
				Sleek2Pointer.cursor = Resources.Load<Texture2D>("UI/Cursors/Cursor_Diagonal_135");
			}
			else if (this.horizontalPosition || this.horizontalSize)
			{
				Sleek2Pointer.cursor = Resources.Load<Texture2D>("UI/Cursors/Cursor_Horizontal");
			}
			else if (this.verticalPosition || this.verticalSize)
			{
				Sleek2Pointer.cursor = Resources.Load<Texture2D>("UI/Cursors/Cursor_Vertical");
			}
			Sleek2Pointer.hotspot = new Vector2(10f, 10f);
			this.isHovering = true;
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0006A546 File Offset: 0x00068946
		public void OnPointerExit(PointerEventData eventData)
		{
			if (Sleek2DragManager.isDragging || this.isCursor)
			{
				return;
			}
			Sleek2Pointer.cursor = null;
			this.isHovering = false;
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0006A56C File Offset: 0x0006896C
		public void OnPointerDown(PointerEventData eventData)
		{
			if (Sleek2DragManager.isDragging)
			{
				return;
			}
			RectTransform rectTransform = this.targetTransform.parent as RectTransform;
			if (rectTransform == null)
			{
				return;
			}
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out this.offset))
			{
				return;
			}
			this.offset.x = this.offset.x - this.targetTransform.localPosition.x;
			if (this.horizontalSize)
			{
				this.offset.x = this.offset.x - this.targetTransform.rect.size.x;
			}
			this.offset.y = this.offset.y - this.targetTransform.localPosition.y;
			if (this.verticalSize)
			{
				this.offset.y = this.offset.y - this.targetTransform.rect.size.y;
			}
			this.isCursor = true;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0006A684 File Offset: 0x00068A84
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (Sleek2DragManager.isDragging)
			{
				return;
			}
			this.targetTransform.GetComponentsInChildren<LayoutGroup>(false, ResizeHandle.layoutGroups);
			for (int i = ResizeHandle.layoutGroups.Count - 1; i >= 0; i--)
			{
				if (!ResizeHandle.layoutGroups[i].enabled)
				{
					ResizeHandle.layoutGroups.RemoveAt(i);
				}
				else
				{
					ResizeHandle.layoutGroups[i].enabled = false;
				}
			}
			Sleek2DragManager.isDragging = true;
			this.isDragging = true;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0006A710 File Offset: 0x00068B10
		public void OnDrag(PointerEventData eventData)
		{
			if (!this.isDragging)
			{
				return;
			}
			RectTransform rectTransform = this.targetTransform.parent as RectTransform;
			if (rectTransform == null)
			{
				return;
			}
			Vector2 vector;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out vector))
			{
				return;
			}
			if (this.horizontalPosition)
			{
				this.targetTransform.anchorMin = new Vector2(Mathf.Clamp((vector.x - this.offset.x + rectTransform.rect.size.x * rectTransform.pivot.x) / rectTransform.rect.size.x, 0f, Mathf.Max(this.targetTransform.anchorMax.x - this.min, 0f)), this.targetTransform.anchorMin.y);
			}
			if (this.horizontalSize)
			{
				this.targetTransform.anchorMax = new Vector2(Mathf.Clamp((vector.x - this.offset.x + rectTransform.rect.size.x * rectTransform.pivot.x) / rectTransform.rect.size.x, Mathf.Min(this.targetTransform.anchorMin.x + this.min, 1f), 1f), this.targetTransform.anchorMax.y);
			}
			if (this.verticalPosition)
			{
				this.targetTransform.anchorMin = new Vector2(this.targetTransform.anchorMin.x, Mathf.Clamp((vector.y - this.offset.y + rectTransform.rect.size.y * rectTransform.pivot.y) / rectTransform.rect.size.y, 0f, Mathf.Max(this.targetTransform.anchorMax.y - this.min, 0f)));
			}
			if (this.verticalSize)
			{
				this.targetTransform.anchorMax = new Vector2(this.targetTransform.anchorMax.x, Mathf.Clamp((vector.y - this.offset.y + rectTransform.rect.size.y * rectTransform.pivot.y) / rectTransform.rect.size.y, Mathf.Min(this.targetTransform.anchorMin.y + this.min, 1f), 1f));
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0006AA2C File Offset: 0x00068E2C
		public void OnEndDrag(PointerEventData eventData)
		{
			if (!this.isDragging)
			{
				return;
			}
			Sleek2DragManager.isDragging = false;
			this.isDragging = false;
			Sleek2Pointer.cursor = null;
			for (int i = 0; i < ResizeHandle.layoutGroups.Count; i++)
			{
				ResizeHandle.layoutGroups[i].enabled = true;
			}
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0006AA84 File Offset: 0x00068E84
		public void OnPointerUp(PointerEventData eventData)
		{
			this.isCursor = false;
			this.isHovering = false;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0006AA94 File Offset: 0x00068E94
		private void OnDisable()
		{
			if (this.isDragging)
			{
				Sleek2DragManager.isDragging = false;
				this.isDragging = false;
			}
			if (this.isCursor || this.isHovering)
			{
				this.isCursor = false;
				this.isHovering = false;
				Sleek2Pointer.cursor = null;
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0006AAE3 File Offset: 0x00068EE3
		private void Reset()
		{
			this.targetTransform = (base.transform as RectTransform);
		}

		// Token: 0x040009C4 RID: 2500
		private static List<LayoutGroup> layoutGroups = new List<LayoutGroup>();

		// Token: 0x040009C5 RID: 2501
		public RectTransform targetTransform;

		// Token: 0x040009C6 RID: 2502
		public bool horizontalPosition;

		// Token: 0x040009C7 RID: 2503
		public bool horizontalSize;

		// Token: 0x040009C8 RID: 2504
		public bool verticalPosition;

		// Token: 0x040009C9 RID: 2505
		public bool verticalSize;

		// Token: 0x040009CA RID: 2506
		public float min = 0.1f;

		// Token: 0x040009CB RID: 2507
		private Vector2 offset;

		// Token: 0x040009CC RID: 2508
		private bool isDragging;

		// Token: 0x040009CD RID: 2509
		private bool isCursor;

		// Token: 0x040009CE RID: 2510
		private bool isHovering;
	}
}
