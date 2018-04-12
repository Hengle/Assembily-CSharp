using System;

namespace SDG.Unturned
{
	// Token: 0x0200042A RID: 1066
	public class VendorAsset : Asset
	{
		// Token: 0x06001D2C RID: 7468 RVA: 0x0009D240 File Offset: 0x0009B640
		public VendorAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (id < 2000 && !bundle.hasResource && !data.has("Bypass_ID_Limit"))
			{
				throw new NotSupportedException("ID < 2000");
			}
			this.vendorName = localization.format("Name");
			this.vendorName = ItemTool.filterRarityRichText(this.vendorName);
			this.vendorDescription = localization.format("Description");
			this.vendorDescription = ItemTool.filterRarityRichText(this.vendorDescription);
			this.buying = new VendorBuying[(int)data.readByte("Buying")];
			byte b = 0;
			while ((int)b < this.buying.Length)
			{
				ushort newID = data.readUInt16("Buying_" + b + "_ID");
				uint newCost = data.readUInt32("Buying_" + b + "_Cost");
				INPCCondition[] array = new INPCCondition[(int)data.readByte("Buying_" + b + "_Conditions")];
				NPCTool.readConditions(data, localization, "Buying_" + b + "_Condition_", array, string.Concat(new object[]
				{
					"vendor ",
					id,
					" buying ",
					b
				}));
				this.buying[(int)b] = new VendorBuying(b, newID, newCost, array);
				b += 1;
			}
			this.selling = new VendorSelling[(int)data.readByte("Selling")];
			byte b2 = 0;
			while ((int)b2 < this.selling.Length)
			{
				ushort newID2 = data.readUInt16("Selling_" + b2 + "_ID");
				uint newCost2 = data.readUInt32("Selling_" + b2 + "_Cost");
				INPCCondition[] array2 = new INPCCondition[(int)data.readByte("Selling_" + b2 + "_Conditions")];
				NPCTool.readConditions(data, localization, "Selling_" + b2 + "_Condition_", array2, string.Concat(new object[]
				{
					"vendor ",
					id,
					" selling ",
					b2
				}));
				this.selling[(int)b2] = new VendorSelling(b2, newID2, newCost2, array2);
				b2 += 1;
			}
			bundle.unload();
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001D2D RID: 7469 RVA: 0x0009D4B0 File Offset: 0x0009B8B0
		// (set) Token: 0x06001D2E RID: 7470 RVA: 0x0009D4B8 File Offset: 0x0009B8B8
		public string vendorName { get; protected set; }

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001D2F RID: 7471 RVA: 0x0009D4C1 File Offset: 0x0009B8C1
		// (set) Token: 0x06001D30 RID: 7472 RVA: 0x0009D4C9 File Offset: 0x0009B8C9
		public string vendorDescription { get; protected set; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x0009D4D2 File Offset: 0x0009B8D2
		// (set) Token: 0x06001D32 RID: 7474 RVA: 0x0009D4DA File Offset: 0x0009B8DA
		public VendorBuying[] buying { get; protected set; }

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x0009D4E3 File Offset: 0x0009B8E3
		// (set) Token: 0x06001D34 RID: 7476 RVA: 0x0009D4EB File Offset: 0x0009B8EB
		public VendorSelling[] selling { get; protected set; }

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x0009D4F4 File Offset: 0x0009B8F4
		public override EAssetType assetCategory
		{
			get
			{
				return EAssetType.NPC;
			}
		}
	}
}
