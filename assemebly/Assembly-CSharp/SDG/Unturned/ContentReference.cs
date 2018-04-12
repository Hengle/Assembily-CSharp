using System;
using System.Runtime.InteropServices;
using SDG.Framework.IO.FormattedFiles;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000392 RID: 914
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct ContentReference<T> : IContentReference, IFormattedFileReadable, IFormattedFileWritable, IEquatable<ContentReference<T>> where T : UnityEngine.Object
	{
		// Token: 0x06001997 RID: 6551 RVA: 0x000907A5 File Offset: 0x0008EBA5
		public ContentReference(string newName, string newPath)
		{
			this.name = newName;
			this.path = newPath;
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x000907B5 File Offset: 0x0008EBB5
		public ContentReference(IContentReference contentReference)
		{
			this.name = contentReference.name;
			this.path = contentReference.path;
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001999 RID: 6553 RVA: 0x000907CF File Offset: 0x0008EBCF
		// (set) Token: 0x0600199A RID: 6554 RVA: 0x000907D7 File Offset: 0x0008EBD7
		public string name { get; set; }

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x000907E0 File Offset: 0x0008EBE0
		// (set) Token: 0x0600199C RID: 6556 RVA: 0x000907E8 File Offset: 0x0008EBE8
		public string path { get; set; }

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x000907F1 File Offset: 0x0008EBF1
		public bool isValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.name) && !string.IsNullOrEmpty(this.path);
			}
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00090814 File Offset: 0x0008EC14
		public bool isReferenceTo(ContentFile file)
		{
			return file != null && this.name == file.rootDirectory.name && this.path == file.path;
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x0009084B File Offset: 0x0008EC4B
		public void read(IFormattedFileReader reader)
		{
			reader = reader.readObject();
			if (reader == null)
			{
				return;
			}
			this.name = reader.readValue("Name");
			this.path = reader.readValue("Path");
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x0009087E File Offset: 0x0008EC7E
		public void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			writer.writeValue("Name", this.name);
			writer.writeValue("Path", this.path);
			writer.endObject();
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x000908AE File Offset: 0x0008ECAE
		public static bool operator ==(ContentReference<T> a, ContentReference<T> b)
		{
			return a.name == b.name && a.path == b.path;
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x000908DE File Offset: 0x0008ECDE
		public static bool operator !=(ContentReference<T> a, ContentReference<T> b)
		{
			return !(a == b);
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x000908EA File Offset: 0x0008ECEA
		public override int GetHashCode()
		{
			return this.name.GetHashCode() ^ this.path.GetHashCode();
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x00090904 File Offset: 0x0008ED04
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			ContentReference<T> contentReference = (ContentReference<T>)obj;
			return this.name == contentReference.name && this.path == contentReference.path;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0009094C File Offset: 0x0008ED4C
		public override string ToString()
		{
			return "#" + this.name + "::" + this.path;
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00090969 File Offset: 0x0008ED69
		public bool Equals(ContentReference<T> other)
		{
			return this.name == other.name && this.path == other.path;
		}

		// Token: 0x04000DBF RID: 3519
		public static ContentReference<T> invalid = new ContentReference<T>(null, null);
	}
}
