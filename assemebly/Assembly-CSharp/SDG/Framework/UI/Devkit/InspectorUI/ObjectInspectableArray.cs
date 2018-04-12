using System;
using SDG.Framework.Devkit;
using SDG.Framework.Translations;

namespace SDG.Framework.UI.Devkit.InspectorUI
{
	// Token: 0x02000251 RID: 593
	public class ObjectInspectableArray : ObjectInspectableInfo
	{
		// Token: 0x06001143 RID: 4419 RVA: 0x0007134F File Offset: 0x0006F74F
		public ObjectInspectableArray(ObjectInspectableInfo newParent, IDirtyable newDirtyable, Array newArray, Type newType, int newIndex, TranslationReference newName, TranslationReference newTooltip) : base(newParent, newDirtyable, newArray, newType, newName, newTooltip)
		{
			this.array = newArray;
			this.index = newIndex;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0007136F File Offset: 0x0006F76F
		// (set) Token: 0x06001145 RID: 4421 RVA: 0x00071377 File Offset: 0x0006F777
		public Array array { get; protected set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x00071380 File Offset: 0x0006F780
		// (set) Token: 0x06001147 RID: 4423 RVA: 0x00071388 File Offset: 0x0006F788
		public int index { get; protected set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x00071391 File Offset: 0x0006F791
		public override bool canRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x00071394 File Offset: 0x0006F794
		public override bool canWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00071397 File Offset: 0x0006F797
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x000713AC File Offset: 0x0006F7AC
		public override object value
		{
			get
			{
				return this.array.GetValue(this.index);
			}
			set
			{
				object value2 = this.value;
				if (value2 == null || value == null)
				{
					if (value2 == value)
					{
						return;
					}
				}
				else if (value2.Equals(value))
				{
					return;
				}
				this.array.SetValue(value, this.index);
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x000713F8 File Offset: 0x0006F7F8
		public override void copyValue(object newValue)
		{
			if (base.parent != null)
			{
				base.parent.copyValue(base.instance);
			}
		}
	}
}
