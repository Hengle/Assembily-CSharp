using System;
using System.Reflection;

namespace SDG.Framework.Devkit.Transactions
{
	// Token: 0x02000187 RID: 391
	public struct TransactionPropertyDelta : ITransactionDelta
	{
		// Token: 0x06000BB6 RID: 2998 RVA: 0x0005ACDD File Offset: 0x000590DD
		public TransactionPropertyDelta(PropertyInfo newProperty)
		{
			this = new TransactionPropertyDelta(newProperty, null, null);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0005ACE8 File Offset: 0x000590E8
		public TransactionPropertyDelta(PropertyInfo newProperty, object newBefore, object newAfter)
		{
			this.property = newProperty;
			this.before = newBefore;
			this.after = newAfter;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0005ACFF File Offset: 0x000590FF
		public void undo(object instance)
		{
			this.property.SetValue(instance, this.before, null);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0005AD14 File Offset: 0x00059114
		public void redo(object instance)
		{
			this.property.SetValue(instance, this.after, null);
		}

		// Token: 0x04000854 RID: 2132
		public PropertyInfo property;

		// Token: 0x04000855 RID: 2133
		public object before;

		// Token: 0x04000856 RID: 2134
		public object after;
	}
}
