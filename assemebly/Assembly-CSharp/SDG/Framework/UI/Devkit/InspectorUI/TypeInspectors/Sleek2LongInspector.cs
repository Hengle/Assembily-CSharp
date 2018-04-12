using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x0200026C RID: 620
	public class Sleek2LongInspector : Sleek2KeyValueInspector
	{
		// Token: 0x0600122C RID: 4652 RVA: 0x00074CF0 File Offset: 0x000730F0
		public Sleek2LongInspector()
		{
			base.name = "Long_Inspector";
			this.field = new Sleek2LongField();
			this.field.transform.reset();
			this.field.longSubmitted += this.handleFieldSubmitted;
			base.valuePanel.addElement(this.field);
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x00074D51 File Offset: 0x00073151
		// (set) Token: 0x0600122E RID: 4654 RVA: 0x00074D59 File Offset: 0x00073159
		public Sleek2LongField field { get; protected set; }

		// Token: 0x0600122F RID: 4655 RVA: 0x00074D62 File Offset: 0x00073162
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			this.field.fieldComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00074D94 File Offset: 0x00073194
		public override void refresh()
		{
			if (base.inspectable == null || !base.inspectable.canRead)
			{
				return;
			}
			if (this.field.fieldComponent.isFocused)
			{
				return;
			}
			this.field.value = (long)base.inspectable.value;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00074DEE File Offset: 0x000731EE
		protected void handleFieldSubmitted(Sleek2LongField field, long value)
		{
			base.inspectable.value = value;
		}
	}
}
