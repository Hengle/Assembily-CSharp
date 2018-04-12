using System;
using System.Runtime.InteropServices;
using SDG.Framework.IO.FormattedFiles;

namespace SDG.Unturned
{
	// Token: 0x02000389 RID: 905
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct AssetReference<T> : IAssetReference, IFormattedFileReadable, IFormattedFileWritable, IEquatable<AssetReference<T>> where T : Asset
	{
		// Token: 0x06001933 RID: 6451 RVA: 0x0008D1C8 File Offset: 0x0008B5C8
		public AssetReference(Guid GUID)
		{
			this.GUID = GUID;
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0008D1D1 File Offset: 0x0008B5D1
		public AssetReference(IAssetReference assetReference)
		{
			this.GUID = assetReference.GUID;
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x0008D1DF File Offset: 0x0008B5DF
		// (set) Token: 0x06001936 RID: 6454 RVA: 0x0008D1E7 File Offset: 0x0008B5E7
		public Guid GUID { get; set; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x0008D1F0 File Offset: 0x0008B5F0
		public bool isValid
		{
			get
			{
				return this.GUID != Guid.Empty;
			}
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x0008D202 File Offset: 0x0008B602
		public bool isReferenceTo(Asset asset)
		{
			return asset != null && this.GUID == asset.GUID;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x0008D21E File Offset: 0x0008B61E
		public void read(IFormattedFileReader reader)
		{
			reader = reader.readObject();
			if (reader == null)
			{
				return;
			}
			this.GUID = reader.readValue<Guid>("GUID");
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0008D240 File Offset: 0x0008B640
		public void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			writer.writeValue<Guid>("GUID", this.GUID);
			writer.endObject();
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0008D25F File Offset: 0x0008B65F
		public static bool operator ==(AssetReference<T> a, AssetReference<T> b)
		{
			return a.GUID == b.GUID;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x0008D274 File Offset: 0x0008B674
		public static bool operator !=(AssetReference<T> a, AssetReference<T> b)
		{
			return !(a == b);
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0008D280 File Offset: 0x0008B680
		public override int GetHashCode()
		{
			return this.GUID.GetHashCode();
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0008D2A4 File Offset: 0x0008B6A4
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			AssetReference<T> assetReference = (AssetReference<T>)obj;
			return this.GUID.Equals(assetReference.GUID);
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0008D2D8 File Offset: 0x0008B6D8
		public override string ToString()
		{
			return this.GUID.ToString("N");
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0008D2F8 File Offset: 0x0008B6F8
		public bool Equals(AssetReference<T> other)
		{
			return this.GUID.Equals(other.GUID);
		}

		// Token: 0x04000D90 RID: 3472
		public static AssetReference<T> invalid = new AssetReference<T>(Guid.Empty);
	}
}
