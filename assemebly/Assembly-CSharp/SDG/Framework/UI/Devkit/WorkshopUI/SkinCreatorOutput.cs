using System;
using SDG.Framework.Debug;
using SDG.Framework.IO.FormattedFiles;

namespace SDG.Framework.UI.Devkit.WorkshopUI
{
	// Token: 0x020002A4 RID: 676
	public class SkinCreatorOutput : IFormattedFileReadable, IFormattedFileWritable
	{
		// Token: 0x060013D7 RID: 5079 RVA: 0x0007EC0C File Offset: 0x0007D00C
		public SkinCreatorOutput()
		{
			this.primarySkin = new SkinInfo();
			this.primarySkin.changed += this.handleSkinInfoChanged;
			this.secondarySkins = new InspectableList<SecondarySkinInfo>();
			this.secondarySkins.inspectorAdded += this.handleSecondarySkinsInspectorAdded;
			this.secondarySkins.inspectorRemoved += this.handleSecondarySkinsInspectorRemoved;
			this.secondarySkins.inspectorChanged += this.handleSecondarySkinsInspectorChanged;
			this.tertiarySkin = new SkinInfo();
			this.tertiarySkin.changed += this.handleSkinInfoChanged;
			this.attachmentSkin = new SkinInfo();
			this.attachmentSkin.changed += this.handleSkinInfoChanged;
			this.outputPath = default(InspectableDirectoryPath);
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x0007ECEA File Offset: 0x0007D0EA
		// (set) Token: 0x060013D9 RID: 5081 RVA: 0x0007ECF2 File Offset: 0x0007D0F2
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Creator_Wizard.Item_ID.Name", null)]
		public ushort itemID
		{
			get
			{
				return this._itemID;
			}
			set
			{
				this._itemID = value;
				this.triggerChanged();
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x0007ED01 File Offset: 0x0007D101
		// (set) Token: 0x060013DB RID: 5083 RVA: 0x0007ED09 File Offset: 0x0007D109
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Creator_Wizard.Primary_Skin.Name", null)]
		public SkinInfo primarySkin { get; protected set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x0007ED12 File Offset: 0x0007D112
		// (set) Token: 0x060013DD RID: 5085 RVA: 0x0007ED1A File Offset: 0x0007D11A
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Creator_Wizard.Secondary_Skins.Name", null)]
		public InspectableList<SecondarySkinInfo> secondarySkins { get; protected set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x0007ED23 File Offset: 0x0007D123
		// (set) Token: 0x060013DF RID: 5087 RVA: 0x0007ED2B File Offset: 0x0007D12B
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Creator_Wizard.Tertiary_Skin.Name", null)]
		public SkinInfo tertiarySkin { get; protected set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0007ED34 File Offset: 0x0007D134
		// (set) Token: 0x060013E1 RID: 5089 RVA: 0x0007ED3C File Offset: 0x0007D13C
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Creator_Wizard.Attachment_Skin.Name", null)]
		public SkinInfo attachmentSkin { get; protected set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x0007ED45 File Offset: 0x0007D145
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x0007ED4D File Offset: 0x0007D14D
		[Inspectable("#SDG::Devkit.Window.UGC_Skin_Creator_Wizard.Output_Path.Name", null)]
		public InspectableDirectoryPath outputPath
		{
			get
			{
				return this._outputPath;
			}
			set
			{
				this._outputPath = value;
				this.triggerChanged();
			}
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x060013E4 RID: 5092 RVA: 0x0007ED5C File Offset: 0x0007D15C
		// (remove) Token: 0x060013E5 RID: 5093 RVA: 0x0007ED94 File Offset: 0x0007D194
		public event SkinCreatorOutput.SkinCreatorOutputChangedHandler changed;

		// Token: 0x060013E6 RID: 5094 RVA: 0x0007EDCC File Offset: 0x0007D1CC
		public virtual void read(IFormattedFileReader reader)
		{
			if (reader == null)
			{
				return;
			}
			reader = reader.readObject();
			this.itemID = reader.readValue<ushort>("Item_ID");
			this.primarySkin = reader.readValue<SkinInfo>("Primary_Skin");
			int num = reader.readArrayLength("Secondary_Skins");
			this.secondarySkins = new InspectableList<SecondarySkinInfo>(num);
			for (int i = 0; i < num; i++)
			{
				this.secondarySkins.Add(reader.readValue<SecondarySkinInfo>(i));
			}
			this.tertiarySkin = reader.readValue<SkinInfo>("Tertiary_Skin");
			this.attachmentSkin = reader.readValue<SkinInfo>("Attachment_Skin");
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0007EE68 File Offset: 0x0007D268
		public virtual void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			writer.writeValue<ushort>("Item_ID", this.itemID);
			this.primarySkin.absolutePath = this.outputPath.absolutePath;
			this.primarySkin.relativePath = "/Primary_Skin";
			writer.writeValue<SkinInfo>("Primary_Skin", this.primarySkin);
			writer.writeKey("Secondary_Skins");
			writer.beginArray();
			for (int i = 0; i < this.secondarySkins.Count; i++)
			{
				this.secondarySkins[i].absolutePath = this.outputPath.absolutePath;
				this.secondarySkins[i].relativePath = "/Secondary_Skin_" + this.secondarySkins[i].itemID;
				writer.writeValue<SecondarySkinInfo>(this.secondarySkins[i]);
			}
			writer.endArray();
			this.tertiarySkin.absolutePath = this.outputPath.absolutePath;
			this.tertiarySkin.relativePath = "/Tertiary_Skin";
			writer.writeValue<SkinInfo>("Tertiary_Skin", this.tertiarySkin);
			this.attachmentSkin.absolutePath = this.outputPath.absolutePath;
			this.attachmentSkin.relativePath = "/Attachment_Skin";
			writer.writeValue<SkinInfo>("Attachment_Skin", this.attachmentSkin);
			writer.endObject();
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0007EFD6 File Offset: 0x0007D3D6
		protected virtual void handleSkinInfoChanged(SkinInfo info)
		{
			this.triggerChanged();
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0007EFE0 File Offset: 0x0007D3E0
		protected virtual void handleSecondarySkinsInspectorAdded(IInspectableList list, object instance)
		{
			SecondarySkinInfo secondarySkinInfo = instance as SecondarySkinInfo;
			secondarySkinInfo.changed += this.handleSkinInfoChanged;
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0007F008 File Offset: 0x0007D408
		protected virtual void handleSecondarySkinsInspectorRemoved(IInspectableList list, object instance)
		{
			SecondarySkinInfo secondarySkinInfo = instance as SecondarySkinInfo;
			secondarySkinInfo.changed -= this.handleSkinInfoChanged;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0007F02F File Offset: 0x0007D42F
		protected virtual void handleSecondarySkinsInspectorChanged(IInspectableList list)
		{
			this.triggerChanged();
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0007F038 File Offset: 0x0007D438
		protected virtual void triggerChanged()
		{
			SkinCreatorOutput.SkinCreatorOutputChangedHandler skinCreatorOutputChangedHandler = this.changed;
			if (skinCreatorOutputChangedHandler != null)
			{
				skinCreatorOutputChangedHandler(this);
			}
		}

		// Token: 0x04000B64 RID: 2916
		protected ushort _itemID;

		// Token: 0x04000B69 RID: 2921
		protected InspectableDirectoryPath _outputPath;

		// Token: 0x020002A5 RID: 677
		// (Invoke) Token: 0x060013EE RID: 5102
		public delegate void SkinCreatorOutputChangedHandler(SkinCreatorOutput output);
	}
}
