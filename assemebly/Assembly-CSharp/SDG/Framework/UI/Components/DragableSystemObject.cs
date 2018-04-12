using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SDG.Framework.UI.Components
{
	// Token: 0x02000214 RID: 532
	public class DragableSystemObject : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x06000FDD RID: 4061 RVA: 0x00069778 File Offset: 0x00067B78
		public void OnPointerDown(PointerEventData eventData)
		{
			if (Sleek2DragManager.isDragging)
			{
				return;
			}
			this.offset = new Vector2(eventData.position.x - this.target.position.x, eventData.position.y - this.target.position.y);
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x000697E0 File Offset: 0x00067BE0
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

		// Token: 0x06000FDF RID: 4063 RVA: 0x0006987C File Offset: 0x00067C7C
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

		// Token: 0x06000FE0 RID: 4064 RVA: 0x000698F4 File Offset: 0x00067CF4
		public void OnEndDrag(PointerEventData eventData)
		{
			if (!this.isDragging)
			{
				return;
			}
			if (!Sleek2DragManager.dropped)
			{
				this.target.localPosition = this.origin;
			}
			UnityEngine.Object.Destroy(this.blockCanvas);
			Sleek2DragManager.isDragging = false;
			this.isDragging = false;
			Sleek2DragManager.item = null;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00069946 File Offset: 0x00067D46
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

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00069982 File Offset: 0x00067D82
		private void Reset()
		{
			this.target = (base.transform as RectTransform);
		}

		// Token: 0x040009A9 RID: 2473
		public RectTransform target;

		// Token: 0x040009AA RID: 2474
		public object source;

		// Token: 0x040009AB RID: 2475
		private bool isDragging;

		// Token: 0x040009AC RID: 2476
		private Vector2 offset;

		// Token: 0x040009AD RID: 2477
		private Vector3 origin;

		// Token: 0x040009AE RID: 2478
		private Canvas blockCanvas;
	}
}
