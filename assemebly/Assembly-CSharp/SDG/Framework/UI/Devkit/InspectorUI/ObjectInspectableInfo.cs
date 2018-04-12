using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.Translations;

namespace SDG.Framework.UI.Devkit.InspectorUI
{
	// Token: 0x02000253 RID: 595
	public abstract class ObjectInspectableInfo
	{
		// Token: 0x06001155 RID: 4437 RVA: 0x00071279 File Offset: 0x0006F679
		public ObjectInspectableInfo(ObjectInspectableInfo newParent, IDirtyable newDirtyable, object newInstance, Type newType, TranslationReference newName, TranslationReference newTooltip)
		{
			this.parent = newParent;
			this.dirtyable = newDirtyable;
			this.instance = newInstance;
			this.type = newType;
			this.name = newName;
			this.tooltip = newTooltip;
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x000712AE File Offset: 0x0006F6AE
		// (set) Token: 0x06001157 RID: 4439 RVA: 0x000712B6 File Offset: 0x0006F6B6
		public ObjectInspectableInfo parent { get; protected set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x000712BF File Offset: 0x0006F6BF
		// (set) Token: 0x06001159 RID: 4441 RVA: 0x000712C7 File Offset: 0x0006F6C7
		public IDirtyable dirtyable { get; protected set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x000712D0 File Offset: 0x0006F6D0
		// (set) Token: 0x0600115B RID: 4443 RVA: 0x000712D8 File Offset: 0x0006F6D8
		public object instance { get; protected set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x000712E1 File Offset: 0x0006F6E1
		// (set) Token: 0x0600115D RID: 4445 RVA: 0x000712E9 File Offset: 0x0006F6E9
		public Type type { get; protected set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x000712F2 File Offset: 0x0006F6F2
		// (set) Token: 0x0600115F RID: 4447 RVA: 0x000712FA File Offset: 0x0006F6FA
		public TranslationReference name { get; protected set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x00071303 File Offset: 0x0006F703
		// (set) Token: 0x06001161 RID: 4449 RVA: 0x0007130B File Offset: 0x0006F70B
		public TranslationReference tooltip { get; protected set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06001162 RID: 4450
		public abstract bool canRead { get; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06001163 RID: 4451
		public abstract bool canWrite { get; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06001164 RID: 4452
		// (set) Token: 0x06001165 RID: 4453
		public abstract object value { get; set; }

		// Token: 0x06001166 RID: 4454
		public abstract void copyValue(object newValue);

		// Token: 0x06001167 RID: 4455 RVA: 0x00071314 File Offset: 0x0006F714
		public virtual void validate()
		{
			if (this.instance == null)
			{
				return;
			}
			if (this.instance is IInspectorValidateable)
			{
				IInspectorValidateable inspectorValidateable = this.instance as IInspectorValidateable;
				inspectorValidateable.inspectorValidate();
			}
		}
	}
}
