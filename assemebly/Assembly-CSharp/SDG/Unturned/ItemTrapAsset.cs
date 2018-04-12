using System;

namespace SDG.Unturned
{
	// Token: 0x020003F0 RID: 1008
	public class ItemTrapAsset : ItemBarricadeAsset
	{
		// Token: 0x06001B42 RID: 6978 RVA: 0x000973B0 File Offset: 0x000957B0
		public ItemTrapAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
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
			this._isBroken = data.has("Broken");
			this._isExplosive = data.has("Explosive");
			this.damageTires = data.has("Damage_Tires");
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x000974C6 File Offset: 0x000958C6
		public float range2
		{
			get
			{
				return this._range2;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x000974CE File Offset: 0x000958CE
		public ushort explosion2
		{
			get
			{
				return this._explosion2;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x000974D6 File Offset: 0x000958D6
		public bool isBroken
		{
			get
			{
				return this._isBroken;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x000974DE File Offset: 0x000958DE
		public bool isExplosive
		{
			get
			{
				return this._isExplosive;
			}
		}

		// Token: 0x04001013 RID: 4115
		protected float _range2;

		// Token: 0x04001014 RID: 4116
		public float playerDamage;

		// Token: 0x04001015 RID: 4117
		public float zombieDamage;

		// Token: 0x04001016 RID: 4118
		public float animalDamage;

		// Token: 0x04001017 RID: 4119
		public float barricadeDamage;

		// Token: 0x04001018 RID: 4120
		public float structureDamage;

		// Token: 0x04001019 RID: 4121
		public float vehicleDamage;

		// Token: 0x0400101A RID: 4122
		public float resourceDamage;

		// Token: 0x0400101B RID: 4123
		public float objectDamage;

		// Token: 0x0400101C RID: 4124
		private ushort _explosion2;

		// Token: 0x0400101D RID: 4125
		protected bool _isBroken;

		// Token: 0x0400101E RID: 4126
		protected bool _isExplosive;

		// Token: 0x0400101F RID: 4127
		public bool damageTires;
	}
}
