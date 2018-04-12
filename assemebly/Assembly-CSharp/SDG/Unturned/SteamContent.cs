using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000674 RID: 1652
	public class SteamContent
	{
		// Token: 0x06003005 RID: 12293 RVA: 0x0013D8E7 File Offset: 0x0013BCE7
		public SteamContent(PublishedFileId_t newPublishedFileID, string newPath, ESteamUGCType newType)
		{
			this._publishedFileID = newPublishedFileID;
			this._path = newPath;
			this._type = newType;
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06003006 RID: 12294 RVA: 0x0013D904 File Offset: 0x0013BD04
		public PublishedFileId_t publishedFileID
		{
			get
			{
				return this._publishedFileID;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06003007 RID: 12295 RVA: 0x0013D90C File Offset: 0x0013BD0C
		public string path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06003008 RID: 12296 RVA: 0x0013D914 File Offset: 0x0013BD14
		public ESteamUGCType type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04001FA4 RID: 8100
		private PublishedFileId_t _publishedFileID;

		// Token: 0x04001FA5 RID: 8101
		private string _path;

		// Token: 0x04001FA6 RID: 8102
		private ESteamUGCType _type;
	}
}
