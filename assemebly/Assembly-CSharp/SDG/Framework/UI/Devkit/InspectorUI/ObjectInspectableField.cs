using System;
using System.Reflection;
using SDG.Framework.Devkit;
using SDG.Framework.Devkit.Transactions;
using SDG.Framework.Translations;

namespace SDG.Framework.UI.Devkit.InspectorUI
{
	// Token: 0x02000252 RID: 594
	public class ObjectInspectableField : ObjectInspectableInfo
	{
		// Token: 0x0600114D RID: 4429 RVA: 0x00071416 File Offset: 0x0006F816
		public ObjectInspectableField(ObjectInspectableInfo newParent, FieldInfo newField, IDirtyable newDirtyable, object newInstance, TranslationReference newName, TranslationReference newTooltip) : base(newParent, newDirtyable, newInstance, newField.FieldType, newName, newTooltip)
		{
			this.field = newField;
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x00071433 File Offset: 0x0006F833
		// (set) Token: 0x0600114F RID: 4431 RVA: 0x0007143B File Offset: 0x0006F83B
		public FieldInfo field { get; protected set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x00071444 File Offset: 0x0006F844
		public override bool canRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x00071447 File Offset: 0x0006F847
		public override bool canWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x0007144A File Offset: 0x0006F84A
		// (set) Token: 0x06001153 RID: 4435 RVA: 0x00071460 File Offset: 0x0006F860
		public override object value
		{
			get
			{
				return this.field.GetValue(base.instance);
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
				TranslatedText translatedText = new TranslatedText(new TranslationReference("SDG", "Devkit.Transactions.Field_Delta"));
				translatedText.format(new object[]
				{
					base.instance,
					this.field.Name,
					(value2 == null) ? "nullptr" : value2.ToString(),
					(value == null) ? "nullptr" : value.ToString()
				});
				DevkitTransactionManager.beginTransaction(translatedText);
				DevkitTransactionUtility.recordObjectDelta(base.instance);
				this.field.SetValue(base.instance, value);
				if (base.dirtyable != null)
				{
					base.dirtyable.isDirty = true;
				}
				if (base.parent != null)
				{
					base.parent.copyValue(base.instance);
				}
				DevkitTransactionManager.endTransaction();
			}
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00071560 File Offset: 0x0006F960
		public override void copyValue(object newValue)
		{
			if (base.type.IsValueType)
			{
				DevkitTransactionUtility.recordObjectDelta(base.instance);
				this.field.SetValue(base.instance, newValue);
			}
			if (base.parent != null)
			{
				base.parent.copyValue(base.instance);
			}
		}
	}
}
