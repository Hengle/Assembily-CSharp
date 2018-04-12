using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SDG.Framework.UI.Components
{
	// Token: 0x02000228 RID: 552
	public class Viewport : UIBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0006B2D3 File Offset: 0x000696D3
		// (set) Token: 0x0600104F RID: 4175 RVA: 0x0006B2DA File Offset: 0x000696DA
		public static Rect screenRect { get; protected set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0006B2E2 File Offset: 0x000696E2
		// (set) Token: 0x06001051 RID: 4177 RVA: 0x0006B2EC File Offset: 0x000696EC
		public static bool hasPointer
		{
			get
			{
				return Viewport._hasPointer;
			}
			protected set
			{
				if (Viewport.hasPointer == value)
				{
					return;
				}
				bool hasPointer = Viewport.hasPointer;
				Viewport._hasPointer = value;
				Viewport.triggerHasPointerChanged(hasPointer, Viewport.hasPointer);
			}
		}

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06001052 RID: 4178 RVA: 0x0006B31C File Offset: 0x0006971C
		// (remove) Token: 0x06001053 RID: 4179 RVA: 0x0006B350 File Offset: 0x00069750
		public static event ViewportHasPointerChanged hasPointerChanged;

		// Token: 0x06001054 RID: 4180 RVA: 0x0006B384 File Offset: 0x00069784
		protected static void triggerHasPointerChanged(bool oldHasPointer, bool newHasPointer)
		{
			if (Viewport.hasPointerChanged != null)
			{
				Viewport.hasPointerChanged(oldHasPointer, newHasPointer);
			}
		}

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06001055 RID: 4181 RVA: 0x0006B39C File Offset: 0x0006979C
		// (remove) Token: 0x06001056 RID: 4182 RVA: 0x0006B3D4 File Offset: 0x000697D4
		public event ViewportDimensionsChangedHandler dimensionsChanged;

		// Token: 0x06001057 RID: 4183 RVA: 0x0006B40A File Offset: 0x0006980A
		protected void triggerDimensionsChanged()
		{
			if (this.dimensionsChanged != null)
			{
				this.dimensionsChanged(this);
			}
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0006B424 File Offset: 0x00069824
		protected virtual void updateScreenRect()
		{
			Vector2 vector = Vector2.Scale(this.rectTransform.rect.size, this.rectTransform.lossyScale);
			Viewport.screenRect = new Rect(this.rectTransform.position.x - this.rectTransform.pivot.x * vector.x, this.rectTransform.position.y - this.rectTransform.pivot.y * vector.y, vector.x, vector.y);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0006B4D4 File Offset: 0x000698D4
		protected override void OnRectTransformDimensionsChange()
		{
			this.updateScreenRect();
			int num = (int)Viewport.screenRect.width;
			int num2 = (int)Viewport.screenRect.height;
			if (this.width != num || this.height != num2)
			{
				this.width = num;
				this.height = num2;
				this.triggerDimensionsChanged();
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0006B534 File Offset: 0x00069934
		protected override void Awake()
		{
			this.rectTransform = (base.transform as RectTransform);
			this.updateScreenRect();
			this.width = (int)Viewport.screenRect.width;
			this.height = (int)Viewport.screenRect.height;
			base.Awake();
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0006B586 File Offset: 0x00069986
		protected override void OnDisable()
		{
			if (this.containsPointer)
			{
				this.containsPointer = false;
				Viewport.hasPointer = false;
			}
			base.OnDisable();
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0006B5A6 File Offset: 0x000699A6
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (!this.containsPointer)
			{
				this.containsPointer = true;
				Viewport.hasPointer = true;
			}
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0006B5C0 File Offset: 0x000699C0
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.containsPointer)
			{
				this.containsPointer = false;
				Viewport.hasPointer = false;
			}
		}

		// Token: 0x040009E4 RID: 2532
		protected static bool _hasPointer;

		// Token: 0x040009E7 RID: 2535
		protected bool containsPointer;

		// Token: 0x040009E8 RID: 2536
		protected RectTransform rectTransform;

		// Token: 0x040009E9 RID: 2537
		protected int width;

		// Token: 0x040009EA RID: 2538
		protected int height;
	}
}
