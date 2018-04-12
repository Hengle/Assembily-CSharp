using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SDG.Framework.UI.Components
{
	// Token: 0x0200021D RID: 541
	public class HoverDropdownButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
	{
		// Token: 0x0600100D RID: 4109 RVA: 0x0006A018 File Offset: 0x00068418
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.dropdown == null)
			{
				return;
			}
			bool flag = true;
			if (base.transform.parent != null && base.transform.parent.parent != null && base.transform.parent.parent.GetComponent<HoverDropdownButton>() == null)
			{
				this.canvasOverride = base.gameObject.getOrAddComponent<Canvas>();
				this.canvasOverride.overrideSorting = true;
				this.canvasOverride.sortingOrder = 30000;
				this.raycasterOverride = base.gameObject.getOrAddComponent<GraphicRaycaster>();
				flag = false;
			}
			this.tracker.Clear();
			this.tracker.Add(this, this.dropdown, DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.PivotX | DrivenTransformProperties.PivotY);
			if (flag)
			{
				this.dropdown.anchorMin = new Vector2(1f, 1f);
				this.dropdown.anchorMax = new Vector2(2f, 1f);
			}
			else
			{
				this.dropdown.anchorMin = Vector2.zero;
				this.dropdown.anchorMax = new Vector2(1f, 0f);
			}
			this.dropdown.pivot = new Vector2(0.5f, 1f);
			this.dropdown.gameObject.SetActive(true);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0006A180 File Offset: 0x00068580
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.dropdown == null)
			{
				return;
			}
			this.dropdown.gameObject.SetActive(false);
			if (this.raycasterOverride != null)
			{
				UnityEngine.Object.Destroy(this.raycasterOverride);
			}
			if (this.canvasOverride != null)
			{
				UnityEngine.Object.Destroy(this.canvasOverride);
			}
			base.GetComponentsInChildren<IPointerExitHandler>(true, HoverDropdownButton.handlers);
			for (int i = 0; i < HoverDropdownButton.handlers.Count; i++)
			{
				if (HoverDropdownButton.handlers[i] != this)
				{
					HoverDropdownButton.handlers[i].OnPointerExit(eventData);
				}
			}
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0006A231 File Offset: 0x00068631
		protected void OnDisable()
		{
			this.tracker.Clear();
		}

		// Token: 0x040009BB RID: 2491
		protected static List<IPointerExitHandler> handlers = new List<IPointerExitHandler>();

		// Token: 0x040009BC RID: 2492
		public RectTransform dropdown;

		// Token: 0x040009BD RID: 2493
		protected Canvas canvasOverride;

		// Token: 0x040009BE RID: 2494
		protected GraphicRaycaster raycasterOverride;

		// Token: 0x040009BF RID: 2495
		protected DrivenRectTransformTracker tracker;
	}
}
