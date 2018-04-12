using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000272 RID: 626
	public class Sleek2StringInspector : Sleek2KeyValueInspector
	{
		// Token: 0x06001256 RID: 4694 RVA: 0x00075610 File Offset: 0x00073A10
		public Sleek2StringInspector()
		{
			base.name = "String_Inspector";
			this.field = new Sleek2Field();
			this.field.transform.reset();
			this.field.submitted += this.handleFieldSubmitted;
			base.valuePanel.addElement(this.field);
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x00075671 File Offset: 0x00073A71
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x00075679 File Offset: 0x00073A79
		public Sleek2Field field { get; protected set; }

		// Token: 0x06001259 RID: 4697 RVA: 0x00075682 File Offset: 0x00073A82
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			this.field.fieldComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x000756B4 File Offset: 0x00073AB4
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
			this.field.text = (string)base.inspectable.value;
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0007570E File Offset: 0x00073B0E
		protected void handleFieldSubmitted(Sleek2Field field, string value)
		{
			base.inspectable.value = value;
		}
	}
}
