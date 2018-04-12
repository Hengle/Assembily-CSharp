using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SDG.Framework.UI.Components
{
	// Token: 0x02000223 RID: 547
	[AddComponentMenu("UI/Separator")]
	public class Separator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler, IEventSystemHandler
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0006AB18 File Offset: 0x00068F18
		// (set) Token: 0x06001032 RID: 4146 RVA: 0x0006AB20 File Offset: 0x00068F20
		public bool aActive
		{
			get
			{
				return this._aActive;
			}
			set
			{
				if (this.aActive != value)
				{
					this._aActive = value;
					this.updateVisuals();
				}
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x0006AB3B File Offset: 0x00068F3B
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x0006AB43 File Offset: 0x00068F43
		public bool bActive
		{
			get
			{
				return this._bActive;
			}
			set
			{
				if (this.bActive != value)
				{
					this._bActive = value;
					this.updateVisuals();
				}
			}
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0006AB60 File Offset: 0x00068F60
		private void updateVisuals()
		{
			if (this.a == null || this.b == null)
			{
				return;
			}
			RectTransform rectTransform = base.transform as RectTransform;
			if (this.aActive != this.a.gameObject.activeSelf)
			{
				this.a.gameObject.SetActive(this.aActive);
			}
			if (this.bActive != this.b.gameObject.activeSelf)
			{
				this.b.gameObject.SetActive(this.bActive);
			}
			Image component = rectTransform.GetComponent<Image>();
			if (component != null && component.enabled != (this.aActive && this.bActive))
			{
				component.enabled = (this.aActive && this.bActive);
			}
			this.value = Mathf.Clamp(this.value, this.min, this.max);
			float num = this.value;
			float num2 = this.padding;
			if (!this.aActive)
			{
				num = 0f;
				num2 = 0f;
			}
			else if (!this.bActive)
			{
				num = 1f;
				num2 = 0f;
			}
			this.tracker.Clear();
			Separator.EDirection edirection = this.direction;
			if (edirection != Separator.EDirection.HORIZONTAL)
			{
				if (edirection == Separator.EDirection.VERTICAL)
				{
					this.tracker.Add(this, rectTransform, DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
					this.tracker.Add(this, this.a, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY | DrivenTransformProperties.PivotX | DrivenTransformProperties.PivotY);
					this.tracker.Add(this, this.b, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY | DrivenTransformProperties.PivotX | DrivenTransformProperties.PivotY);
					rectTransform.anchorMin = new Vector2(0f, num);
					rectTransform.anchorMax = new Vector2(1f, num);
					rectTransform.sizeDelta = new Vector2(0f, this.width);
					this.a.anchorMin = Vector2.zero;
					this.a.anchorMax = new Vector2(1f, num);
					this.a.sizeDelta = new Vector2(0f, -num2);
					this.a.anchoredPosition = Vector2.zero;
					this.a.pivot = new Vector2(0.5f, 0f);
					this.b.anchorMin = new Vector2(0f, num);
					this.b.anchorMax = Vector2.one;
					this.b.sizeDelta = new Vector2(0f, -num2);
					this.b.anchoredPosition = new Vector2(0f, num2);
					this.b.pivot = new Vector2(0.5f, 0f);
				}
			}
			else
			{
				this.tracker.Add(this, rectTransform, DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
				this.tracker.Add(this, this.a, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY | DrivenTransformProperties.PivotX | DrivenTransformProperties.PivotY);
				this.tracker.Add(this, this.b, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY | DrivenTransformProperties.PivotX | DrivenTransformProperties.PivotY);
				rectTransform.anchorMin = new Vector2(num, 0f);
				rectTransform.anchorMax = new Vector2(num, 1f);
				rectTransform.sizeDelta = new Vector2(this.width, 0f);
				this.a.anchorMin = Vector2.zero;
				this.a.anchorMax = new Vector2(num, 1f);
				this.a.sizeDelta = new Vector2(-num2, 0f);
				this.a.anchoredPosition = Vector2.zero;
				this.a.pivot = new Vector2(0f, 0.5f);
				this.b.anchorMin = new Vector2(num, 0f);
				this.b.anchorMax = Vector2.one;
				this.b.sizeDelta = new Vector2(-num2, 0f);
				this.b.anchoredPosition = new Vector2(num2, 0f);
				this.b.pivot = new Vector2(0f, 0.5f);
			}
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0006AF74 File Offset: 0x00069374
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (Sleek2DragManager.isDragging)
			{
				return;
			}
			Sleek2Pointer.cursor = ((this.direction != Separator.EDirection.HORIZONTAL) ? Resources.Load<Texture2D>("UI/Cursors/Cursor_Vertical") : Resources.Load<Texture2D>("UI/Cursors/Cursor_Horizontal"));
			Sleek2Pointer.hotspot = new Vector2(10f, 10f);
			this.isHovering = true;
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0006AFD0 File Offset: 0x000693D0
		public void OnPointerExit(PointerEventData eventData)
		{
			if (Sleek2DragManager.isDragging || this.isCursor)
			{
				return;
			}
			Sleek2Pointer.cursor = null;
			this.isHovering = false;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0006AFF8 File Offset: 0x000693F8
		public void OnPointerDown(PointerEventData eventData)
		{
			if (Sleek2DragManager.isDragging)
			{
				return;
			}
			RectTransform rect = base.transform as RectTransform;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, eventData.position, eventData.pressEventCamera, out this.offset);
			this.isCursor = true;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0006B03C File Offset: 0x0006943C
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (Sleek2DragManager.isDragging)
			{
				return;
			}
			Sleek2DragManager.isDragging = true;
			this.isDragging = true;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0006B058 File Offset: 0x00069458
		public void OnDrag(PointerEventData eventData)
		{
			if (!this.isDragging)
			{
				return;
			}
			RectTransform rectTransform = base.transform.parent as RectTransform;
			if (rectTransform == null)
			{
				return;
			}
			Vector2 vector;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out vector))
			{
				return;
			}
			Separator.EDirection edirection = this.direction;
			if (edirection != Separator.EDirection.HORIZONTAL)
			{
				if (edirection == Separator.EDirection.VERTICAL)
				{
					this.value = (vector.y - this.offset.y + rectTransform.rect.size.y * rectTransform.pivot.y) / rectTransform.rect.size.y;
				}
			}
			else
			{
				this.value = (vector.x - this.offset.x + rectTransform.rect.size.x * rectTransform.pivot.x) / rectTransform.rect.size.x;
			}
			this.updateVisuals();
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0006B187 File Offset: 0x00069587
		public void OnEndDrag(PointerEventData eventData)
		{
			if (!this.isDragging)
			{
				return;
			}
			Sleek2DragManager.isDragging = false;
			this.isDragging = false;
			Sleek2Pointer.cursor = null;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0006B1A8 File Offset: 0x000695A8
		public void OnPointerUp(PointerEventData eventData)
		{
			this.isCursor = false;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0006B1B1 File Offset: 0x000695B1
		private void Start()
		{
			this.updateVisuals();
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0006B1B9 File Offset: 0x000695B9
		private void OnValidate()
		{
			this.updateVisuals();
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0006B1C4 File Offset: 0x000695C4
		private void OnDisable()
		{
			this.tracker.Clear();
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

		// Token: 0x040009CF RID: 2511
		public Separator.EDirection direction;

		// Token: 0x040009D0 RID: 2512
		public float min;

		// Token: 0x040009D1 RID: 2513
		public float max;

		// Token: 0x040009D2 RID: 2514
		public float value;

		// Token: 0x040009D3 RID: 2515
		public float width;

		// Token: 0x040009D4 RID: 2516
		public float padding;

		// Token: 0x040009D5 RID: 2517
		private bool isDragging;

		// Token: 0x040009D6 RID: 2518
		private bool isCursor;

		// Token: 0x040009D7 RID: 2519
		private bool isHovering;

		// Token: 0x040009D8 RID: 2520
		[SerializeField]
		private bool _aActive = true;

		// Token: 0x040009D9 RID: 2521
		[SerializeField]
		private bool _bActive = true;

		// Token: 0x040009DA RID: 2522
		public RectTransform a;

		// Token: 0x040009DB RID: 2523
		public RectTransform b;

		// Token: 0x040009DC RID: 2524
		private DrivenRectTransformTracker tracker;

		// Token: 0x040009DD RID: 2525
		private Vector2 offset;

		// Token: 0x02000224 RID: 548
		public enum EDirection
		{
			// Token: 0x040009DF RID: 2527
			HORIZONTAL,
			// Token: 0x040009E0 RID: 2528
			VERTICAL
		}
	}
}
