using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x0200026E RID: 622
	public class Sleek2SByteInspector : Sleek2KeyValueInspector
	{
		// Token: 0x0600123A RID: 4666 RVA: 0x00075058 File Offset: 0x00073458
		public Sleek2SByteInspector()
		{
			base.name = "SByte_Inspector";
			this.field = new Sleek2SByteField();
			this.field.transform.reset();
			this.field.sbyteSubmitted += this.handleFieldSubmitted;
			base.valuePanel.addElement(this.field);
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x000750B9 File Offset: 0x000734B9
		// (set) Token: 0x0600123C RID: 4668 RVA: 0x000750C1 File Offset: 0x000734C1
		public Sleek2SByteField field { get; protected set; }

		// Token: 0x0600123D RID: 4669 RVA: 0x000750CA File Offset: 0x000734CA
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			this.field.fieldComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x000750FC File Offset: 0x000734FC
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
			this.field.value = (sbyte)base.inspectable.value;
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00075156 File Offset: 0x00073556
		protected void handleFieldSubmitted(Sleek2SByteField field, sbyte value)
		{
			base.inspectable.value = value;
		}
	}
}
