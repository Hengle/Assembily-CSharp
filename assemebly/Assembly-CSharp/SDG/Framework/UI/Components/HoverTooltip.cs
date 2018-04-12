using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SDG.Framework.UI.Components
{
	// Token: 0x02000220 RID: 544
	public class HoverTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
	{
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x0600101A RID: 4122 RVA: 0x0006A254 File Offset: 0x00068654
		// (remove) Token: 0x0600101B RID: 4123 RVA: 0x0006A28C File Offset: 0x0006868C
		public event BeginTooltip beginTooltip;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x0600101C RID: 4124 RVA: 0x0006A2C4 File Offset: 0x000686C4
		// (remove) Token: 0x0600101D RID: 4125 RVA: 0x0006A2FC File Offset: 0x000686FC
		public event EndTooltip endTooltip;

		// Token: 0x0600101E RID: 4126 RVA: 0x0006A332 File Offset: 0x00068732
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.element = this.triggerBeginTooltip();
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0006A340 File Offset: 0x00068740
		public void OnPointerExit(PointerEventData eventData)
		{
			this.triggerEndTooltip(this.element);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0006A34E File Offset: 0x0006874E
		protected virtual Sleek2Element triggerBeginTooltip()
		{
			if (this.beginTooltip != null)
			{
				return this.beginTooltip(this);
			}
			return null;
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0006A369 File Offset: 0x00068769
		protected virtual void triggerEndTooltip(Sleek2Element element)
		{
			if (this.endTooltip != null)
			{
				this.endTooltip(this, element);
			}
		}

		// Token: 0x040009C2 RID: 2498
		protected Sleek2Element element;
	}
}
