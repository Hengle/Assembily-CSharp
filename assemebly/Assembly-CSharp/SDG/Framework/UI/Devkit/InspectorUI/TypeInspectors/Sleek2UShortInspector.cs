using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000275 RID: 629
	public class Sleek2UShortInspector : Sleek2KeyValueInspector
	{
		// Token: 0x06001268 RID: 4712 RVA: 0x00075944 File Offset: 0x00073D44
		public Sleek2UShortInspector()
		{
			base.name = "UShort_Inspector";
			this.field = new Sleek2UShortField();
			this.field.transform.reset();
			this.field.ushortSubmitted += this.handleFieldSubmitted;
			base.valuePanel.addElement(this.field);
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x000759A5 File Offset: 0x00073DA5
		// (set) Token: 0x0600126A RID: 4714 RVA: 0x000759AD File Offset: 0x00073DAD
		public Sleek2UShortField field { get; protected set; }

		// Token: 0x0600126B RID: 4715 RVA: 0x000759B6 File Offset: 0x00073DB6
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			this.field.fieldComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x000759E8 File Offset: 0x00073DE8
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
			this.field.value = (ushort)base.inspectable.value;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00075A42 File Offset: 0x00073E42
		protected void handleFieldSubmitted(Sleek2UShortField field, ushort value)
		{
			base.inspectable.value = value;
		}
	}
}
