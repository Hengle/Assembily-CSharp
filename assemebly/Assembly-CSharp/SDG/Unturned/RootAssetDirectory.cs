using System;

namespace SDG.Unturned
{
	// Token: 0x0200041D RID: 1053
	public class RootAssetDirectory : AssetDirectory
	{
		// Token: 0x06001C9A RID: 7322 RVA: 0x0009B987 File Offset: 0x00099D87
		public RootAssetDirectory(string newPath, string newName) : base(newName, null)
		{
			this.path = newPath;
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x0009B998 File Offset: 0x00099D98
		// (set) Token: 0x06001C9C RID: 7324 RVA: 0x0009B9A0 File Offset: 0x00099DA0
		public string path { get; protected set; }

		// Token: 0x06001C9D RID: 7325 RVA: 0x0009B9A9 File Offset: 0x00099DA9
		public override string getPath(string path)
		{
			return this.path + path;
		}
	}
}
