using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003DA RID: 986
	public class ItemMagazineAsset : ItemCaliberAsset
	{
		// Token: 0x06001AD9 RID: 6873 RVA: 0x000965B0 File Offset: 0x000949B0
		public ItemMagazineAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._magazine = (GameObject)bundle.load("Magazine");
			this._pellets = data.readByte("Pellets");
			if (this.pellets < 1)
			{
				this._pellets = 1;
			}
			this._stuck = data.readByte("Stuck");
			this._range = data.readSingle("Range");
			this.playerDamage = data.readSingle("Player_Damage");
			this.zombieDamage = data.readSingle("Zombie_Damage");
			this.animalDamage = data.readSingle("Animal_Damage");
			this.barricadeDamage = data.readSingle("Barricade_Damage");
			this.structureDamage = data.readSingle("Structure_Damage");
			this.vehicleDamage = data.readSingle("Vehicle_Damage");
			this.resourceDamage = data.readSingle("Resource_Damage");
			this._explosion = data.readUInt16("Explosion");
			if (data.has("Object_Damage"))
			{
				this.objectDamage = data.readSingle("Object_Damage");
			}
			else
			{
				this.objectDamage = this.resourceDamage;
			}
			this._tracer = data.readUInt16("Tracer");
			this._impact = data.readUInt16("Impact");
			this._speed = data.readSingle("Speed");
			if (this.speed < 0.01f)
			{
				this._speed = 1f;
			}
			this._isExplosive = data.has("Explosive");
			this._deleteEmpty = data.has("Delete_Empty");
			bundle.unload();
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x00096754 File Offset: 0x00094B54
		public GameObject magazine
		{
			get
			{
				return this._magazine;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x0009675C File Offset: 0x00094B5C
		public byte pellets
		{
			get
			{
				return this._pellets;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x00096764 File Offset: 0x00094B64
		public byte stuck
		{
			get
			{
				return this._stuck;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001ADD RID: 6877 RVA: 0x0009676C File Offset: 0x00094B6C
		public float range
		{
			get
			{
				return this._range;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x00096774 File Offset: 0x00094B74
		public ushort explosion
		{
			get
			{
				return this._explosion;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001ADF RID: 6879 RVA: 0x0009677C File Offset: 0x00094B7C
		public ushort tracer
		{
			get
			{
				return this._tracer;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x00096784 File Offset: 0x00094B84
		public ushort impact
		{
			get
			{
				return this._impact;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x0009678C File Offset: 0x00094B8C
		public override bool showQuality
		{
			get
			{
				return this.stuck > 0;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x00096797 File Offset: 0x00094B97
		public float speed
		{
			get
			{
				return this._speed;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x0009679F File Offset: 0x00094B9F
		public bool isExplosive
		{
			get
			{
				return this._isExplosive;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x000967A7 File Offset: 0x00094BA7
		public bool deleteEmpty
		{
			get
			{
				return this._deleteEmpty;
			}
		}

		// Token: 0x04000FB4 RID: 4020
		protected GameObject _magazine;

		// Token: 0x04000FB5 RID: 4021
		private byte _pellets;

		// Token: 0x04000FB6 RID: 4022
		private byte _stuck;

		// Token: 0x04000FB7 RID: 4023
		protected float _range;

		// Token: 0x04000FB8 RID: 4024
		public float playerDamage;

		// Token: 0x04000FB9 RID: 4025
		public float zombieDamage;

		// Token: 0x04000FBA RID: 4026
		public float animalDamage;

		// Token: 0x04000FBB RID: 4027
		public float barricadeDamage;

		// Token: 0x04000FBC RID: 4028
		public float structureDamage;

		// Token: 0x04000FBD RID: 4029
		public float vehicleDamage;

		// Token: 0x04000FBE RID: 4030
		public float resourceDamage;

		// Token: 0x04000FBF RID: 4031
		public float objectDamage;

		// Token: 0x04000FC0 RID: 4032
		private ushort _explosion;

		// Token: 0x04000FC1 RID: 4033
		private ushort _tracer;

		// Token: 0x04000FC2 RID: 4034
		private ushort _impact;

		// Token: 0x04000FC3 RID: 4035
		private float _speed;

		// Token: 0x04000FC4 RID: 4036
		protected bool _isExplosive;

		// Token: 0x04000FC5 RID: 4037
		private bool _deleteEmpty;
	}
}
