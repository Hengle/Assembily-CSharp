using System;
using System.Reflection;

namespace SDG.Framework.Devkit.Transactions
{
	// Token: 0x02000186 RID: 390
	public struct TransactionFieldDelta : ITransactionDelta
	{
		// Token: 0x06000BB2 RID: 2994 RVA: 0x0005AC93 File Offset: 0x00059093
		public TransactionFieldDelta(FieldInfo newField)
		{
			this = new TransactionFieldDelta(newField, null, null);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0005AC9E File Offset: 0x0005909E
		public TransactionFieldDelta(FieldInfo newField, object newBefore, object newAfter)
		{
			this.field = newField;
			this.before = newBefore;
			this.after = newAfter;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0005ACB5 File Offset: 0x000590B5
		public void undo(object instance)
		{
			this.field.SetValue(instance, this.before);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0005ACC9 File Offset: 0x000590C9
		public void redo(object instance)
		{
			this.field.SetValue(instance, this.after);
		}

		// Token: 0x04000851 RID: 2129
		public FieldInfo field;

		// Token: 0x04000852 RID: 2130
		public object before;

		// Token: 0x04000853 RID: 2131
		public object after;
	}
}
