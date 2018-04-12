using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x0200026F RID: 623
	public class Sleek2ShortInspector : Sleek2KeyValueInspector
	{
		// Token: 0x06001240 RID: 4672 RVA: 0x0007516C File Offset: 0x0007356C
		public Sleek2ShortInspector()
		{
			base.name = "Short_Inspector";
			this.field = new Sleek2ShortField();
			this.field.transform.reset();
			this.field.shortSubmitted += this.handleFieldSubmitted;
			base.valuePanel.addElement(this.field);
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x000751CD File Offset: 0x000735CD
		// (set) Token: 0x06001242 RID: 4674 RVA: 0x000751D5 File Offset: 0x000735D5
		public Sleek2ShortField field { get; protected set; }

		// Token: 0x06001243 RID: 4675 RVA: 0x000751DE File Offset: 0x000735DE
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			this.field.fieldComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00075210 File Offset: 0x00073610
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
			this.field.value = (short)base.inspectable.value;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0007526A File Offset: 0x0007366A
		protected void handleFieldSubmitted(Sleek2ShortField field, short value)
		{
			base.inspectable.value = value;
		}
	}
}
