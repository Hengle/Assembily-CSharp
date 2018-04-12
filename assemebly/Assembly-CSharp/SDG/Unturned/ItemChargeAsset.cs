using System;

namespace SDG.Unturned
{
	// Token: 0x020003C6 RID: 966
	public class ItemChargeAsset : ItemBarricadeAsset
	{
		// Token: 0x06001A71 RID: 6769 RVA: 0x00094408 File Offset: 0x00092808
		public ItemChargeAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._range2 = data.readSingle("Range2");
			this.playerDamage = data.readSingle("Player_Damage");
			this.zombieDamage = data.readSingle("Zombie_Damage");
			this.animalDamage = data.readSingle("Animal_Damage");
			this.barricadeDamage = data.readSingle("Barricade_Damage");
			this.structureDamage = data.readSingle("Structure_Damage");
			this.vehicleDamage = data.readSingle("Vehicle_Damage");
			this.resourceDamage = data.readSingle("Resource_Damage");
			if (data.has("Object_Damage"))
			{
				this.objectDamage = data.readSingle("Object_Damage");
			}
			else
			{
				this.objectDamage = this.resourceDamage;
			}
			this._explosion2 = data.readUInt16("Explosion2");
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x000944EB File Offset: 0x000928EB
		public float range2
		{
			get
			{
				return this._range2;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x000944F3 File Offset: 0x000928F3
		public ushort explosion2
		{
			get
			{
				return this._explosion2;
			}
		}

		// Token: 0x04000F3D RID: 3901
		protected float _range2;

		// Token: 0x04000F3E RID: 3902
		public float playerDamage;

		// Token: 0x04000F3F RID: 3903
		public float zombieDamage;

		// Token: 0x04000F40 RID: 3904
		public float animalDamage;

		// Token: 0x04000F41 RID: 3905
		public float barricadeDamage;

		// Token: 0x04000F42 RID: 3906
		public float structureDamage;

		// Token: 0x04000F43 RID: 3907
		public float vehicleDamage;

		// Token: 0x04000F44 RID: 3908
		public float resourceDamage;

		// Token: 0x04000F45 RID: 3909
		public float objectDamage;

		// Token: 0x04000F46 RID: 3910
		private ushort _explosion2;
	}
}
