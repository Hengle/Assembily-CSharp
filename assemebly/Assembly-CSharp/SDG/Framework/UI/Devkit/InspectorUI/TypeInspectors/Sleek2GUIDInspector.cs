using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x0200026A RID: 618
	public class Sleek2GUIDInspector : Sleek2KeyValueInspector
	{
		// Token: 0x06001220 RID: 4640 RVA: 0x00074AB4 File Offset: 0x00072EB4
		public Sleek2GUIDInspector()
		{
			base.name = "GUID_Inspector";
			this.field = new Sleek2Field();
			this.field.transform.reset();
			this.field.submitted += this.handleFieldSubmitted;
			base.valuePanel.addElement(this.field);
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x00074B15 File Offset: 0x00072F15
		// (set) Token: 0x06001222 RID: 4642 RVA: 0x00074B1D File Offset: 0x00072F1D
		public Sleek2Field field { get; protected set; }

		// Token: 0x06001223 RID: 4643 RVA: 0x00074B26 File Offset: 0x00072F26
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			this.field.fieldComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00074B58 File Offset: 0x00072F58
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
			this.field.fieldComponent.text = ((Guid)base.inspectable.value).ToString("N");
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00074BC4 File Offset: 0x00072FC4
		protected void handleFieldSubmitted(Sleek2Field field, string value)
		{
			base.inspectable.value = new Guid(value);
		}
	}
}
