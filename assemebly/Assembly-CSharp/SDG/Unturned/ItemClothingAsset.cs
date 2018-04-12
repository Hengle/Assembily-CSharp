using System;

namespace SDG.Unturned
{
	// Token: 0x020003C7 RID: 967
	public class ItemClothingAsset : ItemAsset
	{
		// Token: 0x06001A74 RID: 6772 RVA: 0x00093C1C File Offset: 0x0009201C
		public ItemClothingAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (this.isPro)
			{
				this._armor = 1f;
			}
			else
			{
				this._armor = data.readSingle("Armor");
				if ((double)this.armor < 0.01)
				{
					this._armor = 1f;
				}
				this._proofWater = data.has("Proof_Water");
				this._proofFire = data.has("Proof_Fire");
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x00093CA2 File Offset: 0x000920A2
		public float armor
		{
			get
			{
				return this._armor;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x00093CAA File Offset: 0x000920AA
		public override bool showQuality
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x00093CAD File Offset: 0x000920AD
		public bool proofWater
		{
			get
			{
				return this._proofWater;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x00093CB5 File Offset: 0x000920B5
		public bool proofFire
		{
			get
			{
				return this._proofFire;
			}
		}

		// Token: 0x04000F47 RID: 3911
		protected float _armor;

		// Token: 0x04000F48 RID: 3912
		private bool _proofWater;

		// Token: 0x04000F49 RID: 3913
		private bool _proofFire;
	}
}
