using System;
using System.IO;
using SDG.Framework.Debug;
using SDG.Framework.IO.FormattedFiles;

namespace SDG.Framework.UI.Devkit.WorkshopUI
{
	// Token: 0x020002A1 RID: 673
	public class SkinInfo : IFormattedFileReadable, IFormattedFileWritable
	{
		// Token: 0x060013BF RID: 5055 RVA: 0x0007E93B File Offset: 0x0007CD3B
		public SkinInfo()
		{
			this.albedoPath = new InspectableFilePath("*.png");
			this.metallicPath = new InspectableFilePath("*.png");
			this.emissionPath = new InspectableFilePath("*.png");
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x0007E973 File Offset: 0x0007CD73
		// (set) Token: 0x060013C1 RID: 5057 RVA: 0x0007E97B File Offset: 0x0007CD7B
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Creator_Wizard.Albedo_Path.Name", null)]
		public InspectableFilePath albedoPath
		{
			get
			{
				return this._albedoPath;
			}
			set
			{
				this._albedoPath = value;
				this.triggerChanged();
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x0007E98A File Offset: 0x0007CD8A
		// (set) Token: 0x060013C3 RID: 5059 RVA: 0x0007E992 File Offset: 0x0007CD92
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Creator_Wizard.Metallic_Path.Name", null)]
		public InspectableFilePath metallicPath
		{
			get
			{
				return this._metallicPath;
			}
			set
			{
				this._metallicPath = value;
				this.triggerChanged();
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x0007E9A1 File Offset: 0x0007CDA1
		// (set) Token: 0x060013C5 RID: 5061 RVA: 0x0007E9A9 File Offset: 0x0007CDA9
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Creator_Wizard.Emission_Path.Name", null)]
		public InspectableFilePath emissionPath
		{
			get
			{
				return this._emissionPath;
			}
			set
			{
				this._emissionPath = value;
				this.triggerChanged();
			}
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x060013C6 RID: 5062 RVA: 0x0007E9B8 File Offset: 0x0007CDB8
		// (remove) Token: 0x060013C7 RID: 5063 RVA: 0x0007E9F0 File Offset: 0x0007CDF0
		public event SkinInfo.SkinInfoChangedHandler changed;

		// Token: 0x060013C8 RID: 5064 RVA: 0x0007EA26 File Offset: 0x0007CE26
		public virtual void read(IFormattedFileReader reader)
		{
			reader = reader.readObject();
			this.readInfo(reader);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0007EA38 File Offset: 0x0007CE38
		protected virtual void readInfo(IFormattedFileReader reader)
		{
			InspectableFilePath inspectableFilePath = this.albedoPath;
			inspectableFilePath.absolutePath = reader.readValue("Albedo");
			this.albedoPath = inspectableFilePath;
			inspectableFilePath = this.metallicPath;
			inspectableFilePath.absolutePath = reader.readValue("Metallic");
			this.metallicPath = inspectableFilePath;
			inspectableFilePath = this.emissionPath;
			inspectableFilePath.absolutePath = reader.readValue("Emission");
			this.emissionPath = inspectableFilePath;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0007EAA5 File Offset: 0x0007CEA5
		public virtual void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			this.writeInfo(writer);
			writer.endObject();
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0007EABC File Offset: 0x0007CEBC
		protected virtual void writeTexture(IFormattedFileWriter writer, string source, string name)
		{
			if (File.Exists(source))
			{
				string text = this.absolutePath + this.relativePath;
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				string str = "/" + name + ".png";
				string text2 = text + str;
				if (File.Exists(text2))
				{
					File.Delete(text2);
				}
				File.Copy(source, text2);
				writer.writeValue(name, this.relativePath + str);
			}
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0007EB3C File Offset: 0x0007CF3C
		protected virtual void writeInfo(IFormattedFileWriter writer)
		{
			this.writeTexture(writer, this.albedoPath.absolutePath, "Albedo");
			this.writeTexture(writer, this.metallicPath.absolutePath, "Metallic");
			this.writeTexture(writer, this.emissionPath.absolutePath, "Emission");
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0007EB98 File Offset: 0x0007CF98
		protected virtual void triggerChanged()
		{
			SkinInfo.SkinInfoChangedHandler skinInfoChangedHandler = this.changed;
			if (skinInfoChangedHandler != null)
			{
				skinInfoChangedHandler(this);
			}
		}

		// Token: 0x04000B5D RID: 2909
		public string absolutePath;

		// Token: 0x04000B5E RID: 2910
		public string relativePath;

		// Token: 0x04000B5F RID: 2911
		protected InspectableFilePath _albedoPath;

		// Token: 0x04000B60 RID: 2912
		protected InspectableFilePath _metallicPath;

		// Token: 0x04000B61 RID: 2913
		protected InspectableFilePath _emissionPath;

		// Token: 0x020002A2 RID: 674
		// (Invoke) Token: 0x060013CF RID: 5071
		public delegate void SkinInfoChangedHandler(SkinInfo info);
	}
}
