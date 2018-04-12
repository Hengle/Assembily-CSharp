using System;
using System.IO;

namespace SDG.Unturned
{
	// Token: 0x02000390 RID: 912
	public class ContentFile
	{
		// Token: 0x06001985 RID: 6533 RVA: 0x000906E8 File Offset: 0x0008EAE8
		public ContentFile(RootContentDirectory newRootDirectory, ContentDirectory newDirectory, string newPath, string newFile)
		{
			this.rootDirectory = newRootDirectory;
			this.directory = newDirectory;
			this.path = newPath;
			this.file = newFile;
			this.name = Path.GetFileNameWithoutExtension(this.file);
			this.guessedType = ContentTypeGuesserRegistry.guess(Path.GetExtension(this.file));
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x0009073F File Offset: 0x0008EB3F
		// (set) Token: 0x06001987 RID: 6535 RVA: 0x00090747 File Offset: 0x0008EB47
		public RootContentDirectory rootDirectory { get; protected set; }

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x00090750 File Offset: 0x0008EB50
		// (set) Token: 0x06001989 RID: 6537 RVA: 0x00090758 File Offset: 0x0008EB58
		public ContentDirectory directory { get; protected set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600198A RID: 6538 RVA: 0x00090761 File Offset: 0x0008EB61
		// (set) Token: 0x0600198B RID: 6539 RVA: 0x00090769 File Offset: 0x0008EB69
		public string path { get; protected set; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x00090772 File Offset: 0x0008EB72
		// (set) Token: 0x0600198D RID: 6541 RVA: 0x0009077A File Offset: 0x0008EB7A
		public string file { get; protected set; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x00090783 File Offset: 0x0008EB83
		// (set) Token: 0x0600198F RID: 6543 RVA: 0x0009078B File Offset: 0x0008EB8B
		public string name { get; protected set; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x00090794 File Offset: 0x0008EB94
		// (set) Token: 0x06001991 RID: 6545 RVA: 0x0009079C File Offset: 0x0008EB9C
		public Type guessedType { get; protected set; }
	}
}
