using System;
using System.Collections;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.Translations;

namespace SDG.Framework.UI.Devkit.InspectorUI
{
	// Token: 0x02000254 RID: 596
	public class ObjectInspectableList : ObjectInspectableInfo
	{
		// Token: 0x06001168 RID: 4456 RVA: 0x000715B6 File Offset: 0x0006F9B6
		public ObjectInspectableList(ObjectInspectableInfo newParent, IDirtyable newDirtyable, IList newList, Type newType, int newIndex, TranslationReference newName, TranslationReference newTooltip) : base(newParent, newDirtyable, newList, newType, newName, newTooltip)
		{
			this.list = newList;
			this.index = newIndex;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x000715D6 File Offset: 0x0006F9D6
		// (set) Token: 0x0600116A RID: 4458 RVA: 0x000715DE File Offset: 0x0006F9DE
		public IList list { get; protected set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x000715E7 File Offset: 0x0006F9E7
		// (set) Token: 0x0600116C RID: 4460 RVA: 0x000715EF File Offset: 0x0006F9EF
		public int index { get; protected set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x000715F8 File Offset: 0x0006F9F8
		public override bool canRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x000715FB File Offset: 0x0006F9FB
		public override bool canWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x000715FE File Offset: 0x0006F9FE
		// (set) Token: 0x06001170 RID: 4464 RVA: 0x00071614 File Offset: 0x0006FA14
		public override object value
		{
			get
			{
				return this.list[this.index];
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
				this.list[this.index] = value;
				if (this.list is IInspectableList)
				{
					IInspectableList inspectableList = this.list as IInspectableList;
					inspectableList.inspectorSet(this.index);
				}
			}
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00071688 File Offset: 0x0006FA88
		public override void copyValue(object newValue)
		{
			if (base.parent != null)
			{
				base.parent.copyValue(base.instance);
			}
		}
	}
}
