using System;
using SDG.Framework.Debug;
using SDG.Framework.IO.FormattedFiles;

namespace SDG.Framework.UI.Devkit.WorkshopUI
{
	// Token: 0x020002A3 RID: 675
	public class SecondarySkinInfo : SkinInfo
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x0007EBC1 File Offset: 0x0007CFC1
		// (set) Token: 0x060013D4 RID: 5076 RVA: 0x0007EBC9 File Offset: 0x0007CFC9
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

		// Token: 0x060013D5 RID: 5077 RVA: 0x0007EBD8 File Offset: 0x0007CFD8
		protected override void readInfo(IFormattedFileReader reader)
		{
			base.readInfo(reader);
			this.itemID = reader.readValue<ushort>("Item_ID");
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0007EBF2 File Offset: 0x0007CFF2
		protected override void writeInfo(IFormattedFileWriter writer)
		{
			base.writeInfo(writer);
			writer.writeValue<ushort>("Item_ID", this.itemID);
		}

		// Token: 0x04000B63 RID: 2915
		protected ushort _itemID;
	}
}
