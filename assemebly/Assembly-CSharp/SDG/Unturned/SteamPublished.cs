using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x0200067B RID: 1659
	public class SteamPublished
	{
		// Token: 0x06003074 RID: 12404 RVA: 0x0013EB48 File Offset: 0x0013CF48
		public SteamPublished(string newName, PublishedFileId_t newID)
		{
			this._name = newName;
			this._id = newID;
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06003075 RID: 12405 RVA: 0x0013EB5E File Offset: 0x0013CF5E
		public string name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06003076 RID: 12406 RVA: 0x0013EB66 File Offset: 0x0013CF66
		public PublishedFileId_t id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x04002001 RID: 8193
		private string _name;

		// Token: 0x04002002 RID: 8194
		private PublishedFileId_t _id;
	}
}
