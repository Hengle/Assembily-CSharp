using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000268 RID: 616
	public class Sleek2DoubleInspector : Sleek2KeyValueInspector
	{
		// Token: 0x06001214 RID: 4628 RVA: 0x00074878 File Offset: 0x00072C78
		public Sleek2DoubleInspector()
		{
			base.name = "Double_Inspector";
			this.field = new Sleek2DoubleField();
			this.field.transform.reset();
			this.field.doubleSubmitted += this.handleFieldSubmitted;
			base.valuePanel.addElement(this.field);
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x000748D9 File Offset: 0x00072CD9
		// (set) Token: 0x06001216 RID: 4630 RVA: 0x000748E1 File Offset: 0x00072CE1
		public Sleek2DoubleField field { get; protected set; }

		// Token: 0x06001217 RID: 4631 RVA: 0x000748EA File Offset: 0x00072CEA
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			this.field.fieldComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0007491C File Offset: 0x00072D1C
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
			this.field.value = (double)base.inspectable.value;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00074976 File Offset: 0x00072D76
		protected void handleFieldSubmitted(Sleek2DoubleField field, double value)
		{
			base.inspectable.value = value;
		}
	}
}
