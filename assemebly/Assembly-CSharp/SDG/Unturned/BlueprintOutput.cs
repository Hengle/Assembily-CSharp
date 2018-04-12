using System;

namespace SDG.Unturned
{
	// Token: 0x02000505 RID: 1285
	public class BlueprintOutput
	{
		// Token: 0x0600232B RID: 9003 RVA: 0x000C3FC8 File Offset: 0x000C23C8
		public BlueprintOutput(ushort newID, byte newAmount)
		{
			this._id = newID;
			this.amount = (ushort)newAmount;
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x000C3FDE File Offset: 0x000C23DE
		public ushort id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x0400152B RID: 5419
		private ushort _id;

		// Token: 0x0400152C RID: 5420
		public ushort amount;
	}
}
