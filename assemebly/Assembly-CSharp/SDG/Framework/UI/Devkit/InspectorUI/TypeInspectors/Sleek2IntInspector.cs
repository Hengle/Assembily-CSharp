using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x0200026B RID: 619
	public class Sleek2IntInspector : Sleek2KeyValueInspector
	{
		// Token: 0x06001226 RID: 4646 RVA: 0x00074BDC File Offset: 0x00072FDC
		public Sleek2IntInspector()
		{
			base.name = "Int_Inspector";
			this.field = new Sleek2IntField();
			this.field.transform.reset();
			this.field.intSubmitted += this.handleFieldSubmitted;
			base.valuePanel.addElement(this.field);
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x00074C3D File Offset: 0x0007303D
		// (set) Token: 0x06001228 RID: 4648 RVA: 0x00074C45 File Offset: 0x00073045
		public Sleek2IntField field { get; protected set; }

		// Token: 0x06001229 RID: 4649 RVA: 0x00074C4E File Offset: 0x0007304E
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			this.field.fieldComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00074C80 File Offset: 0x00073080
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
			this.field.value = (int)base.inspectable.value;
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00074CDA File Offset: 0x000730DA
		protected void handleFieldSubmitted(Sleek2IntField field, int value)
		{
			base.inspectable.value = value;
		}
	}
}
