using System;
using System.Collections.Generic;
using System.Reflection;
using SDG.Framework.Utilities;
using UnityEngine;

namespace SDG.Framework.Devkit.Transactions
{
	// Token: 0x0200017C RID: 380
	public class DevkitObjectDeltaTransaction : IDevkitTransaction
	{
		// Token: 0x06000B64 RID: 2916 RVA: 0x0005A217 File Offset: 0x00058617
		public DevkitObjectDeltaTransaction(object newInstance)
		{
			this.instance = newInstance;
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0005A226 File Offset: 0x00058626
		public bool delta
		{
			get
			{
				return this.deltas.Count > 0;
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0005A238 File Offset: 0x00058638
		public void undo()
		{
			for (int i = 0; i < this.deltas.Count; i++)
			{
				this.deltas[i].undo(this.instance);
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0005A278 File Offset: 0x00058678
		public void redo()
		{
			for (int i = 0; i < this.deltas.Count; i++)
			{
				this.deltas[i].redo(this.instance);
			}
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0005A2B8 File Offset: 0x000586B8
		public void begin()
		{
			this.tempFields = ListPool<object>.claim();
			this.tempProperties = ListPool<object>.claim();
			Type type = this.instance.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			{
				try
				{
					FieldInfo fieldInfo = fields[i];
					object value = fieldInfo.GetValue(this.instance);
					this.tempFields.Add(value);
				}
				catch
				{
					this.tempFields.Add(null);
				}
			}
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			for (int j = 0; j < properties.Length; j++)
			{
				try
				{
					PropertyInfo propertyInfo = properties[j];
					if (propertyInfo.CanRead && propertyInfo.CanWrite)
					{
						object value2 = propertyInfo.GetValue(this.instance, null);
						this.tempProperties.Add(value2);
					}
					else
					{
						this.tempProperties.Add(null);
					}
				}
				catch
				{
					this.tempProperties.Add(null);
				}
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0005A3E0 File Offset: 0x000587E0
		public void end()
		{
			this.deltas = ListPool<ITransactionDelta>.claim();
			Type type = this.instance.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			{
				try
				{
					FieldInfo fieldInfo = fields[i];
					object value = fieldInfo.GetValue(this.instance);
					if (this.changed(this.tempFields[i], value))
					{
						this.deltas.Add(new TransactionFieldDelta(fieldInfo, this.tempFields[i], value));
					}
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			for (int j = 0; j < properties.Length; j++)
			{
				try
				{
					PropertyInfo propertyInfo = properties[j];
					if (propertyInfo.CanRead && propertyInfo.CanWrite)
					{
						object value2 = propertyInfo.GetValue(this.instance, null);
						if (this.changed(this.tempProperties[j], value2))
						{
							this.deltas.Add(new TransactionPropertyDelta(propertyInfo, this.tempProperties[j], value2));
						}
					}
				}
				catch (Exception exception2)
				{
					Debug.LogException(exception2);
				}
			}
			ListPool<object>.release(this.tempFields);
			ListPool<object>.release(this.tempProperties);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0005A55C File Offset: 0x0005895C
		public void forget()
		{
			if (this.deltas != null)
			{
				ListPool<ITransactionDelta>.release(this.deltas);
				this.deltas = null;
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0005A57B File Offset: 0x0005897B
		protected bool changed(object before, object after)
		{
			if (before == null || after == null)
			{
				return before != after;
			}
			return !before.Equals(after);
		}

		// Token: 0x0400083D RID: 2109
		protected object instance;

		// Token: 0x0400083E RID: 2110
		protected List<object> tempFields;

		// Token: 0x0400083F RID: 2111
		protected List<object> tempProperties;

		// Token: 0x04000840 RID: 2112
		protected List<ITransactionDelta> deltas;
	}
}
