using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x0200038F RID: 911
	public class ContentDirectory
	{
		// Token: 0x0600197C RID: 6524 RVA: 0x00090678 File Offset: 0x0008EA78
		public ContentDirectory(string newName, ContentDirectory newParent)
		{
			this.name = newName;
			this.parent = newParent;
			this.files = new List<ContentFile>();
			this.directories = new Dictionary<string, ContentDirectory>();
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x0600197D RID: 6525 RVA: 0x000906A4 File Offset: 0x0008EAA4
		// (set) Token: 0x0600197E RID: 6526 RVA: 0x000906AC File Offset: 0x0008EAAC
		public string name { get; protected set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x000906B5 File Offset: 0x0008EAB5
		// (set) Token: 0x06001980 RID: 6528 RVA: 0x000906BD File Offset: 0x0008EABD
		public ContentDirectory parent { get; protected set; }

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x000906C6 File Offset: 0x0008EAC6
		// (set) Token: 0x06001982 RID: 6530 RVA: 0x000906CE File Offset: 0x0008EACE
		public List<ContentFile> files { get; protected set; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x000906D7 File Offset: 0x0008EAD7
		// (set) Token: 0x06001984 RID: 6532 RVA: 0x000906DF File Offset: 0x0008EADF
		public Dictionary<string, ContentDirectory> directories { get; protected set; }
	}
}
