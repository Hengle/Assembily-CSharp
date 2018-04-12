using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x02000387 RID: 903
	public class AssetDirectory
	{
		// Token: 0x06001926 RID: 6438 RVA: 0x0008D127 File Offset: 0x0008B527
		public AssetDirectory(string newName, AssetDirectory newParent)
		{
			this.name = newName;
			this.parent = newParent;
			this.assets = new List<Asset>();
			this.directories = new List<AssetDirectory>();
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x0008D153 File Offset: 0x0008B553
		// (set) Token: 0x06001928 RID: 6440 RVA: 0x0008D15B File Offset: 0x0008B55B
		public string name { get; protected set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x0008D164 File Offset: 0x0008B564
		// (set) Token: 0x0600192A RID: 6442 RVA: 0x0008D16C File Offset: 0x0008B56C
		public AssetDirectory parent { get; protected set; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x0008D175 File Offset: 0x0008B575
		// (set) Token: 0x0600192C RID: 6444 RVA: 0x0008D17D File Offset: 0x0008B57D
		public List<Asset> assets { get; protected set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x0008D186 File Offset: 0x0008B586
		// (set) Token: 0x0600192E RID: 6446 RVA: 0x0008D18E File Offset: 0x0008B58E
		public List<AssetDirectory> directories { get; protected set; }

		// Token: 0x0600192F RID: 6447 RVA: 0x0008D197 File Offset: 0x0008B597
		public virtual string getPath(string path)
		{
			path = '/' + this.name + path;
			if (this.parent != null)
			{
				path = this.parent.getPath(path);
			}
			return path;
		}
	}
}
