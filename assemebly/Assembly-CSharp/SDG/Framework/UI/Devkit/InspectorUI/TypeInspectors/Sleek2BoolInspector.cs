using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000266 RID: 614
	public class Sleek2BoolInspector : Sleek2KeyValueInspector
	{
		// Token: 0x06001208 RID: 4616 RVA: 0x00074604 File Offset: 0x00072A04
		public Sleek2BoolInspector()
		{
			base.name = "Bool_Inspector";
			this.toggle = new Sleek2Toggle();
			this.toggle.transform.anchorMin = new Vector2(0f, 0f);
			this.toggle.transform.anchorMax = new Vector2(0f, 0f);
			this.toggle.transform.pivot = new Vector2(0f, 0f);
			this.toggle.transform.sizeDelta = new Vector2(30f, 30f);
			this.toggle.toggled += this.handleToggleToggled;
			base.valuePanel.addElement(this.toggle);
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x000746D1 File Offset: 0x00072AD1
		// (set) Token: 0x0600120A RID: 4618 RVA: 0x000746D9 File Offset: 0x00072AD9
		public Sleek2Toggle toggle { get; protected set; }

		// Token: 0x0600120B RID: 4619 RVA: 0x000746E2 File Offset: 0x00072AE2
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			this.toggle.toggleComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00074712 File Offset: 0x00072B12
		public override void refresh()
		{
			if (base.inspectable == null || !base.inspectable.canRead)
			{
				return;
			}
			this.toggle.toggleComponent.isOn = (bool)base.inspectable.value;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00074750 File Offset: 0x00072B50
		protected void handleToggleToggled(Sleek2Toggle toggle, bool isOn)
		{
			base.inspectable.value = isOn;
		}
	}
}
