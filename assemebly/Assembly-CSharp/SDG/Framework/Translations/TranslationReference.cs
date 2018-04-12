using System;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.Utilities;

namespace SDG.Framework.Translations
{
	// Token: 0x02000207 RID: 519
	public struct TranslationReference : IFormattedFileReadable, IFormattedFileWritable, IEquatable<TranslationReference>
	{
		// Token: 0x06000F89 RID: 3977 RVA: 0x000687A8 File Offset: 0x00066BA8
		public TranslationReference(string path)
		{
			TranslationUtility.tryParse(path, out this.ns, out this.token);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x000687BD File Offset: 0x00066BBD
		public TranslationReference(string newNamespace, string newToken)
		{
			this.ns = newNamespace;
			this.token = newToken;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x000687CD File Offset: 0x00066BCD
		public bool isValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.ns) && !string.IsNullOrEmpty(this.token);
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x000687F0 File Offset: 0x00066BF0
		public void read(IFormattedFileReader reader)
		{
			reader = reader.readObject();
			if (reader == null)
			{
				return;
			}
			this.ns = reader.readValue<string>("Namespace");
			this.token = reader.readValue<string>("Token");
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00068823 File Offset: 0x00066C23
		public void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			writer.writeValue("Namespace", this.ns);
			writer.writeValue("Token", this.token);
			writer.endObject();
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00068853 File Offset: 0x00066C53
		public override string ToString()
		{
			return "#" + this.ns + "::" + this.token;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00068870 File Offset: 0x00066C70
		public static bool operator ==(TranslationReference x, TranslationReference y)
		{
			return x.ns == y.ns && x.token == y.token;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x000688A0 File Offset: 0x00066CA0
		public static bool operator !=(TranslationReference x, TranslationReference y)
		{
			return !(x == y);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x000688AC File Offset: 0x00066CAC
		public override int GetHashCode()
		{
			int num = 0;
			if (this.ns != null)
			{
				num ^= this.ns.GetHashCode();
			}
			if (this.token != null)
			{
				num ^= this.token.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x000688F0 File Offset: 0x00066CF0
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			TranslationReference translationReference = (TranslationReference)obj;
			return this.ns == translationReference.ns && this.token == translationReference.token;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00068938 File Offset: 0x00066D38
		public bool Equals(TranslationReference other)
		{
			return this.ns == other.ns && this.token == other.token;
		}

		// Token: 0x04000993 RID: 2451
		public static TranslationReference invalid = new TranslationReference(null, null);

		// Token: 0x04000994 RID: 2452
		public string ns;

		// Token: 0x04000995 RID: 2453
		public string token;
	}
}
