using System;

namespace SDG.Unturned
{
	// Token: 0x02000506 RID: 1286
	public class BlueprintSupply
	{
		// Token: 0x0600232D RID: 9005 RVA: 0x000C3FE6 File Offset: 0x000C23E6
		public BlueprintSupply(ushort newID, bool newCritical, byte newAmount)
		{
			this._id = newID;
			this._isCritical = newCritical;
			this.amount = (ushort)newAmount;
			this.hasAmount = 0;
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x0600232E RID: 9006 RVA: 0x000C400A File Offset: 0x000C240A
		public ushort id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x000C4012 File Offset: 0x000C2412
		public bool isCritical
		{
			get
			{
				return this._isCritical;
			}
		}

		// Token: 0x0400152D RID: 5421
		private ushort _id;

		// Token: 0x0400152E RID: 5422
		private bool _isCritical;

		// Token: 0x0400152F RID: 5423
		public ushort amount;

		// Token: 0x04001530 RID: 5424
		public ushort hasAmount;
	}
}
