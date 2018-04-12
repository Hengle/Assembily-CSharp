using System;
using System.IO;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.IO.FormattedFiles.KeyValueTables;

namespace SDG.Unturned
{
	// Token: 0x02000386 RID: 902
	public abstract class Asset : IDirtyable, IFormattedFileReadable, IFormattedFileWritable
	{
		// Token: 0x06001913 RID: 6419 RVA: 0x0005BB1C File Offset: 0x00059F1C
		public Asset()
		{
			this.name = base.GetType().Name;
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0005BB3C File Offset: 0x00059F3C
		public Asset(Bundle bundle, Local localization, byte[] hash)
		{
			if (bundle != null)
			{
				this.name = bundle.name;
			}
			else
			{
				this.name = "Asset_" + this.id;
			}
			this.id = 0;
			this.canUse = true;
			this.hash = hash;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0005BBA0 File Offset: 0x00059FA0
		public Asset(Bundle bundle, Data data, Local localization, ushort id)
		{
			if (bundle != null)
			{
				this.name = bundle.name;
			}
			else
			{
				this.name = "Asset_" + id;
			}
			this.id = id;
			this.canUse = true;
			if (data != null)
			{
				this.hash = data.hash;
				if (data.has("Asset_Origin_Override"))
				{
					this.assetOrigin = (EAssetOrigin)Enum.Parse(typeof(EAssetOrigin), data.readString("Asset_Origin_Override"), true);
				}
			}
			else
			{
				this.hash = new byte[20];
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x0005BC51 File Offset: 0x0005A051
		// (set) Token: 0x06001917 RID: 6423 RVA: 0x0005BC59 File Offset: 0x0005A059
		public bool isDirty
		{
			get
			{
				return this._isDirty;
			}
			set
			{
				if (this.isDirty == value)
				{
					return;
				}
				this._isDirty = value;
				if (this.isDirty)
				{
					DirtyManager.markDirty(this);
				}
				else
				{
					DirtyManager.markClean(this);
				}
			}
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0005BC8C File Offset: 0x0005A08C
		public virtual string getFilePath()
		{
			string path = '/' + this.name + ".asset";
			return this.directory.getPath(path);
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0005BCC0 File Offset: 0x0005A0C0
		public void save()
		{
			string filePath = this.getFilePath();
			string directoryName = Path.GetDirectoryName(filePath);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (StreamWriter streamWriter = new StreamWriter(filePath))
			{
				IFormattedFileWriter writer = new KeyValueTableWriter(streamWriter);
				this.write(writer);
			}
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x0005BD24 File Offset: 0x0005A124
		public virtual void read(IFormattedFileReader reader)
		{
			if (reader == null)
			{
				return;
			}
			reader = reader.readObject();
			this.readAsset(reader);
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0005BD3C File Offset: 0x0005A13C
		protected virtual void readAsset(IFormattedFileReader reader)
		{
			this.id = reader.readValue<ushort>("ID");
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0005BD50 File Offset: 0x0005A150
		public virtual void write(IFormattedFileWriter writer)
		{
			writer.beginObject("Metadata");
			writer.writeValue<Guid>("GUID", this.GUID);
			writer.writeValue<Type>("Type", base.GetType());
			writer.endObject();
			writer.beginObject("Asset");
			this.writeAsset(writer);
			writer.endObject();
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0005BDA8 File Offset: 0x0005A1A8
		protected virtual void writeAsset(IFormattedFileWriter writer)
		{
			writer.writeValue<ushort>("ID", this.id);
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0005BDBB File Offset: 0x0005A1BB
		public AssetReference<T> getReferenceTo<T>() where T : Asset
		{
			return new AssetReference<T>(this.GUID);
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x0005BDC8 File Offset: 0x0005A1C8
		// (set) Token: 0x06001920 RID: 6432 RVA: 0x0005BDD0 File Offset: 0x0005A1D0
		public EAssetOrigin assetOrigin
		{
			get
			{
				return this._assetOrigin;
			}
			set
			{
				if (this.assetOrigin != EAssetOrigin.MISC)
				{
					return;
				}
				this._assetOrigin = value;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x0005BDE6 File Offset: 0x0005A1E6
		// (set) Token: 0x06001922 RID: 6434 RVA: 0x0005BDEE File Offset: 0x0005A1EE
		public byte[] hash { get; protected set; }

		// Token: 0x06001923 RID: 6435 RVA: 0x0005BDF7 File Offset: 0x0005A1F7
		public virtual void clearHash()
		{
			this.hash = new byte[20];
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x0005BE06 File Offset: 0x0005A206
		public virtual EAssetType assetCategory
		{
			get
			{
				return EAssetType.NONE;
			}
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0005BE09 File Offset: 0x0005A209
		public override string ToString()
		{
			return this.id + " - " + this.name;
		}

		// Token: 0x04000D83 RID: 3459
		protected bool _isDirty;

		// Token: 0x04000D84 RID: 3460
		public string name;

		// Token: 0x04000D85 RID: 3461
		[Inspectable("#SDG::Asset.Asset.ID.Name", null)]
		public ushort id;

		// Token: 0x04000D86 RID: 3462
		public AssetDirectory directory;

		// Token: 0x04000D87 RID: 3463
		[Inspectable("#SDG::Asset.Asset.GUID.Name", "#SDG::Asset.Asset.GUID.Tooltip")]
		public Guid GUID;

		// Token: 0x04000D88 RID: 3464
		protected EAssetOrigin _assetOrigin = EAssetOrigin.MISC;

		// Token: 0x04000D89 RID: 3465
		public string absoluteOriginFilePath;

		// Token: 0x04000D8A RID: 3466
		public bool canUse;
	}
}
