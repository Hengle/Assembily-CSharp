using System;

namespace SDG.Unturned
{
	// Token: 0x0200038B RID: 907
	public struct ScannedFileInfo
	{
		// Token: 0x06001946 RID: 6470 RVA: 0x0008D32C File Offset: 0x0008B72C
		public ScannedFileInfo(string name, string assetPath, string dataPath, string bundlePath, bool dataUsePath, bool bundleUsePath, bool loadFromResources, bool canUse, AssetDirectory directory, EAssetOrigin origin, bool overrideExistingIDs)
		{
			this.name = name;
			this.assetPath = assetPath;
			this.dataPath = dataPath;
			this.bundlePath = bundlePath;
			this.dataUsePath = dataUsePath;
			this.bundleUsePath = bundleUsePath;
			this.loadFromResources = loadFromResources;
			this.canUse = canUse;
			this.directory = directory;
			this.origin = origin;
			this.overrideExistingIDs = overrideExistingIDs;
		}

		// Token: 0x04000D92 RID: 3474
		public string name;

		// Token: 0x04000D93 RID: 3475
		public string assetPath;

		// Token: 0x04000D94 RID: 3476
		public string dataPath;

		// Token: 0x04000D95 RID: 3477
		public string bundlePath;

		// Token: 0x04000D96 RID: 3478
		public bool dataUsePath;

		// Token: 0x04000D97 RID: 3479
		public bool bundleUsePath;

		// Token: 0x04000D98 RID: 3480
		public bool loadFromResources;

		// Token: 0x04000D99 RID: 3481
		public bool canUse;

		// Token: 0x04000D9A RID: 3482
		public AssetDirectory directory;

		// Token: 0x04000D9B RID: 3483
		public EAssetOrigin origin;

		// Token: 0x04000D9C RID: 3484
		public bool overrideExistingIDs;
	}
}
