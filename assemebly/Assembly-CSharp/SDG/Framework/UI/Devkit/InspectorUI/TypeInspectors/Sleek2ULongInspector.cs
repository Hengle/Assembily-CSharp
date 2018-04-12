using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000274 RID: 628
	public class Sleek2ULongInspector : Sleek2KeyValueInspector
	{
		// Token: 0x06001262 RID: 4706 RVA: 0x00075830 File Offset: 0x00073C30
		public Sleek2ULongInspector()
		{
			base.name = "ULong_Inspector";
			this.field = new Sleek2ULongField();
			this.field.transform.reset();
			this.field.ulongSubmitted += this.handleFieldSubmitted;
			base.valuePanel.addElement(this.field);
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x00075891 File Offset: 0x00073C91
		// (set) Token: 0x06001264 RID: 4708 RVA: 0x00075899 File Offset: 0x00073C99
		public Sleek2ULongField field { get; protected set; }

		// Token: 0x06001265 RID: 4709 RVA: 0x000758A2 File Offset: 0x00073CA2
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			this.field.fieldComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x000758D4 File Offset: 0x00073CD4
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
			this.field.value = (ulong)base.inspectable.value;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0007592E File Offset: 0x00073D2E
		protected void handleFieldSubmitted(Sleek2ULongField field, ulong value)
		{
			base.inspectable.value = value;
		}
	}
}
