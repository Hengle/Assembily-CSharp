using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000273 RID: 627
	public class Sleek2UIntInspector : Sleek2KeyValueInspector
	{
		// Token: 0x0600125C RID: 4700 RVA: 0x0007571C File Offset: 0x00073B1C
		public Sleek2UIntInspector()
		{
			base.name = "UInt_Inspector";
			this.field = new Sleek2UIntField();
			this.field.transform.reset();
			this.field.uintSubmitted += this.handleFieldSubmitted;
			base.valuePanel.addElement(this.field);
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x0007577D File Offset: 0x00073B7D
		// (set) Token: 0x0600125E RID: 4702 RVA: 0x00075785 File Offset: 0x00073B85
		public Sleek2UIntField field { get; protected set; }

		// Token: 0x0600125F RID: 4703 RVA: 0x0007578E File Offset: 0x00073B8E
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			this.field.fieldComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x000757C0 File Offset: 0x00073BC0
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
			this.field.value = (uint)base.inspectable.value;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0007581A File Offset: 0x00073C1A
		protected void handleFieldSubmitted(Sleek2UIntField field, uint value)
		{
			base.inspectable.value = value;
		}
	}
}
