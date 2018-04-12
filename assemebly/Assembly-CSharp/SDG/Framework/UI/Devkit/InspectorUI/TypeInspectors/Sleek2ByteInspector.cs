using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000267 RID: 615
	public class Sleek2ByteInspector : Sleek2KeyValueInspector
	{
		// Token: 0x0600120E RID: 4622 RVA: 0x00074764 File Offset: 0x00072B64
		public Sleek2ByteInspector()
		{
			base.name = "Byte_Inspector";
			this.field = new Sleek2ByteField();
			this.field.transform.reset();
			this.field.byteSubmitted += this.handleFieldSubmitted;
			base.valuePanel.addElement(this.field);
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x000747C5 File Offset: 0x00072BC5
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x000747CD File Offset: 0x00072BCD
		public Sleek2ByteField field { get; protected set; }

		// Token: 0x06001211 RID: 4625 RVA: 0x000747D6 File Offset: 0x00072BD6
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			this.field.fieldComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00074808 File Offset: 0x00072C08
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
			this.field.value = (byte)base.inspectable.value;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00074862 File Offset: 0x00072C62
		protected void handleFieldSubmitted(Sleek2ByteField field, byte value)
		{
			base.inspectable.value = value;
		}
	}
}
