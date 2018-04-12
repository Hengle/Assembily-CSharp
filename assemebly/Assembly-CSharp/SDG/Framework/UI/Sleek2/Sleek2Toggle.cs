using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002E6 RID: 742
	public class Sleek2Toggle : Sleek2Image
	{
		// Token: 0x06001559 RID: 5465 RVA: 0x000823F4 File Offset: 0x000807F4
		public Sleek2Toggle()
		{
			base.gameObject.name = "Toggle";
			base.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Button_Background");
			base.imageComponent.type = Image.Type.Sliced;
			this.toggleComponent = base.gameObject.AddComponent<Toggle>();
			this.toggleComponent.onValueChanged.AddListener(new UnityAction<bool>(this.handleToggleValueChanged));
			this.checkmark = new Sleek2Image();
			this.checkmark.name = "Checkmark";
			this.checkmark.transform.anchorMin = new Vector2(0f, 0f);
			this.checkmark.transform.anchorMax = new Vector2(1f, 1f);
			this.checkmark.transform.offsetMin = new Vector2(5f, 5f);
			this.checkmark.transform.offsetMax = new Vector2(-5f, -5f);
			this.checkmark.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Checkmark");
			this.checkmark.imageComponent.color = Sleek2Config.darkTextColor;
			this.toggleComponent.graphic = this.checkmark.imageComponent;
			this.addElement(this.checkmark);
		}

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x0600155A RID: 5466 RVA: 0x00082550 File Offset: 0x00080950
		// (remove) Token: 0x0600155B RID: 5467 RVA: 0x00082588 File Offset: 0x00080988
		public event ToggleToggledHandler toggled;

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x000825BE File Offset: 0x000809BE
		// (set) Token: 0x0600155D RID: 5469 RVA: 0x000825C6 File Offset: 0x000809C6
		public Toggle toggleComponent { get; protected set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x000825CF File Offset: 0x000809CF
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x000825D7 File Offset: 0x000809D7
		public Sleek2Image checkmark { get; protected set; }

		// Token: 0x06001560 RID: 5472 RVA: 0x000825E0 File Offset: 0x000809E0
		protected virtual void triggerToggled(bool isOn)
		{
			if (this.toggled != null)
			{
				this.toggled(this, isOn);
			}
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x000825FA File Offset: 0x000809FA
		protected virtual void handleToggleValueChanged(bool isOn)
		{
			this.triggerToggled(isOn);
		}
	}
}
