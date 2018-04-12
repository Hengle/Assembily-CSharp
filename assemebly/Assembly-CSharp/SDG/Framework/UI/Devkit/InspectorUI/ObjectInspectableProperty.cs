using System;
using System.Reflection;
using SDG.Framework.Devkit;
using SDG.Framework.Devkit.Transactions;
using SDG.Framework.Translations;

namespace SDG.Framework.UI.Devkit.InspectorUI
{
	// Token: 0x02000255 RID: 597
	public class ObjectInspectableProperty : ObjectInspectableInfo
	{
		// Token: 0x06001172 RID: 4466 RVA: 0x000716A6 File Offset: 0x0006FAA6
		public ObjectInspectableProperty(ObjectInspectableInfo newParent, PropertyInfo newProperty, IDirtyable newDirtyable, object newInstance, TranslationReference newName, TranslationReference newTooltip) : base(newParent, newDirtyable, newInstance, newProperty.PropertyType, newName, newTooltip)
		{
			this.property = newProperty;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x000716C3 File Offset: 0x0006FAC3
		// (set) Token: 0x06001174 RID: 4468 RVA: 0x000716CB File Offset: 0x0006FACB
		public PropertyInfo property { get; protected set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x000716D4 File Offset: 0x0006FAD4
		public override bool canRead
		{
			get
			{
				return this.property.GetGetMethod() != null;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x000716E7 File Offset: 0x0006FAE7
		public override bool canWrite
		{
			get
			{
				return this.property.GetSetMethod() != null;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x000716FA File Offset: 0x0006FAFA
		// (set) Token: 0x06001178 RID: 4472 RVA: 0x00071710 File Offset: 0x0006FB10
		public override object value
		{
			get
			{
				return this.property.GetValue(base.instance, null);
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
				TranslatedText translatedText = new TranslatedText(new TranslationReference("SDG", "Devkit.Transactions.Property_Delta"));
				translatedText.format(new object[]
				{
					base.instance,
					this.property.Name,
					(value2 == null) ? "nullptr" : value2.ToString(),
					(value == null) ? "nullptr" : value.ToString()
				});
				DevkitTransactionManager.beginTransaction(translatedText);
				DevkitTransactionUtility.recordObjectDelta(base.instance);
				this.property.SetValue(base.instance, value, null);
				if (base.dirtyable != null)
				{
					base.dirtyable.isDirty = true;
				}
				DevkitTransactionManager.endTransaction();
			}
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x000717F8 File Offset: 0x0006FBF8
		public override void copyValue(object newValue)
		{
			if (base.type.IsValueType)
			{
				DevkitTransactionUtility.recordObjectDelta(base.instance);
				this.property.SetValue(base.instance, newValue, null);
			}
			if (base.parent != null)
			{
				base.parent.copyValue(base.instance);
			}
		}
	}
}
