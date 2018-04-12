using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SDG.Framework.UI.Components
{
	// Token: 0x0200020B RID: 523
	public class ContextDropdown : MonoBehaviour, IPointerExitHandler, IEventSystemHandler
	{
		// Token: 0x06000FB6 RID: 4022 RVA: 0x00068FFF File Offset: 0x000673FF
		public void OnPointerExit(PointerEventData eventData)
		{
			this.element.destroy();
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0006900C File Offset: 0x0006740C
		protected void Awake()
		{
			this.canvasOverride = base.gameObject.getOrAddComponent<Canvas>();
			this.canvasOverride.overrideSorting = true;
			this.canvasOverride.sortingOrder = 30000;
			this.raycasterOverride = base.gameObject.getOrAddComponent<GraphicRaycaster>();
		}

		// Token: 0x0400099C RID: 2460
		public Sleek2Element element;

		// Token: 0x0400099D RID: 2461
		protected Canvas canvasOverride;

		// Token: 0x0400099E RID: 2462
		protected GraphicRaycaster raycasterOverride;
	}
}
