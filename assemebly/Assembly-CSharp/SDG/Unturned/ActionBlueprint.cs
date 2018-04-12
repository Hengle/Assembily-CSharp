using System;

namespace SDG.Unturned
{
	// Token: 0x02000502 RID: 1282
	public class ActionBlueprint
	{
		// Token: 0x06002308 RID: 8968 RVA: 0x000C3AE8 File Offset: 0x000C1EE8
		public ActionBlueprint(byte newID, bool newLink)
		{
			this._id = newID;
			this._isLink = newLink;
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x000C3AFE File Offset: 0x000C1EFE
		public byte id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x000C3B06 File Offset: 0x000C1F06
		public bool isLink
		{
			get
			{
				return this._isLink;
			}
		}

		// Token: 0x04001510 RID: 5392
		private byte _id;

		// Token: 0x04001511 RID: 5393
		private bool _isLink;
	}
}
