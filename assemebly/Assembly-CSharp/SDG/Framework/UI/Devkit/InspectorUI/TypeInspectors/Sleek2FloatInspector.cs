using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000261 RID: 609
	public class Sleek2FloatInspector : Sleek2KeyValueInspector
	{
		// Token: 0x060011DD RID: 4573 RVA: 0x00074014 File Offset: 0x00072414
		public Sleek2FloatInspector()
		{
			base.name = "Float_Inspector";
			this.field = new Sleek2FloatField();
			this.field.transform.reset();
			this.field.floatSubmitted += this.handleFieldSubmitted;
			base.valuePanel.addElement(this.field);
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x00074075 File Offset: 0x00072475
		// (set) Token: 0x060011DF RID: 4575 RVA: 0x0007407D File Offset: 0x0007247D
		public Sleek2FloatField field { get; protected set; }

		// Token: 0x060011E0 RID: 4576 RVA: 0x00074086 File Offset: 0x00072486
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			this.field.fieldComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x000740B8 File Offset: 0x000724B8
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
			this.field.value = (float)base.inspectable.value;
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00074112 File Offset: 0x00072512
		protected void handleFieldSubmitted(Sleek2FloatField field, float value)
		{
			base.inspectable.value = value;
		}
	}
}
