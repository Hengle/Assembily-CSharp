using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SDG.Framework.UI.Components
{
	// Token: 0x02000216 RID: 534
	public class DragableTab : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000FE8 RID: 4072 RVA: 0x000699A0 File Offset: 0x00067DA0
		// (remove) Token: 0x06000FE9 RID: 4073 RVA: 0x000699D8 File Offset: 0x00067DD8
		public event PopoutTabHandler popoutTab;

		// Token: 0x06000FEA RID: 4074 RVA: 0x00069A10 File Offset: 0x00067E10
		public void OnPointerDown(PointerEventData eventData)
		{
			if (Sleek2DragManager.isDragging)
			{
				return;
			}
			this.offset = new Vector2(eventData.position.x - this.target.position.x, eventData.position.y - this.target.position.y);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00069A78 File Offset: 0x00067E78
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (Sleek2DragManager.isDragging)
			{
				return;
			}
			if (this.target == null || this.source == null)
			{
				return;
			}
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.origin = this.target.localPosition;
			this.blockCanvas = this.target.gameObject.getOrAddComponent<Canvas>();
			this.blockCanvas.overrideSorting = true;
			this.blockCanvas.sortingOrder = 30000;
			Sleek2DragManager.isDragging = true;
			this.isDragging = true;
			Sleek2DragManager.item = this.source;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00069B14 File Offset: 0x00067F14
		public void OnDrag(PointerEventData eventData)
		{
			if (!this.isDragging)
			{
				return;
			}
			Vector2 position = eventData.position;
			position.x = Mathf.Clamp(position.x, 0f, (float)Screen.width);
			position.y = Mathf.Clamp(position.y, 0f, (float)Screen.height);
			this.target.position = position - this.offset;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00069B8C File Offset: 0x00067F8C
		public void OnEndDrag(PointerEventData eventData)
		{
			if (!this.isDragging)
			{
				return;
			}
			if (!Sleek2DragManager.dropped)
			{
				this.target.localPosition = this.origin;
				this.triggerPopoutTab(eventData.position);
			}
			UnityEngine.Object.Destroy(this.blockCanvas);
			Sleek2DragManager.isDragging = false;
			this.isDragging = false;
			Sleek2DragManager.item = null;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00069BEA File Offset: 0x00067FEA
		protected void triggerPopoutTab(Vector2 position)
		{
			if (this.popoutTab != null)
			{
				this.popoutTab(this, position);
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00069C04 File Offset: 0x00068004
		protected void OnDisable()
		{
			if (this.isDragging)
			{
				this.target.localPosition = this.origin;
				UnityEngine.Object.Destroy(this.blockCanvas);
				Sleek2DragManager.isDragging = false;
				this.isDragging = false;
				Sleek2DragManager.item = null;
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00069C40 File Offset: 0x00068040
		private void Reset()
		{
			this.target = (base.transform as RectTransform);
		}

		// Token: 0x040009AF RID: 2479
		public RectTransform target;

		// Token: 0x040009B0 RID: 2480
		public object source;

		// Token: 0x040009B1 RID: 2481
		private bool isDragging;

		// Token: 0x040009B3 RID: 2483
		private Vector2 offset;

		// Token: 0x040009B4 RID: 2484
		private Vector3 origin;

		// Token: 0x040009B5 RID: 2485
		private Canvas blockCanvas;
	}
}
