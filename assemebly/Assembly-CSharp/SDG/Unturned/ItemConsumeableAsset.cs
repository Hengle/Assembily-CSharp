using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003C9 RID: 969
	public class ItemConsumeableAsset : ItemWeaponAsset
	{
		// Token: 0x06001A7B RID: 6779 RVA: 0x00094AD4 File Offset: 0x00092ED4
		public ItemConsumeableAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._use = (AudioClip)bundle.load("Use");
			this._health = data.readByte("Health");
			this._food = data.readByte("Food");
			this._water = data.readByte("Water");
			this._virus = data.readByte("Virus");
			this._disinfectant = data.readByte("Disinfectant");
			this._energy = data.readByte("Energy");
			this._vision = data.readByte("Vision");
			this._warmth = data.readUInt32("Warmth");
			this._hasBleeding = data.has("Bleeding");
			this._hasBroken = data.has("Broken");
			this._hasAid = data.has("Aid");
			this.foodConstrainsWater = (this.food >= this.water);
			this.explosion = data.readUInt16("Explosion");
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x00094BE5 File Offset: 0x00092FE5
		public AudioClip use
		{
			get
			{
				return this._use;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x00094BED File Offset: 0x00092FED
		public byte health
		{
			get
			{
				return this._health;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x00094BF5 File Offset: 0x00092FF5
		public byte food
		{
			get
			{
				return this._food;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x00094BFD File Offset: 0x00092FFD
		public byte water
		{
			get
			{
				return this._water;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x00094C05 File Offset: 0x00093005
		public byte virus
		{
			get
			{
				return this._virus;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x00094C0D File Offset: 0x0009300D
		public byte disinfectant
		{
			get
			{
				return this._disinfectant;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x00094C15 File Offset: 0x00093015
		public byte energy
		{
			get
			{
				return this._energy;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x00094C1D File Offset: 0x0009301D
		public byte vision
		{
			get
			{
				return this._vision;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x00094C25 File Offset: 0x00093025
		public uint warmth
		{
			get
			{
				return this._warmth;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x00094C2D File Offset: 0x0009302D
		public bool hasBleeding
		{
			get
			{
				return this._hasBleeding;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x00094C35 File Offset: 0x00093035
		public bool hasBroken
		{
			get
			{
				return this._hasBroken;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x00094C3D File Offset: 0x0009303D
		public bool hasAid
		{
			get
			{
				return this._hasAid;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x00094C45 File Offset: 0x00093045
		// (set) Token: 0x06001A89 RID: 6793 RVA: 0x00094C4D File Offset: 0x0009304D
		public bool foodConstrainsWater { get; protected set; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x00094C56 File Offset: 0x00093056
		public override bool showQuality
		{
			get
			{
				return this.type == EItemType.FOOD || this.type == EItemType.WATER;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x00094C72 File Offset: 0x00093072
		// (set) Token: 0x06001A8C RID: 6796 RVA: 0x00094C7A File Offset: 0x0009307A
		public ushort explosion { get; protected set; }

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x00094C83 File Offset: 0x00093083
		public override bool isDangerous
		{
			get
			{
				return this.explosion > 0 || base.isDangerous;
			}
		}

		// Token: 0x04000F4B RID: 3915
		protected AudioClip _use;

		// Token: 0x04000F4C RID: 3916
		private byte _health;

		// Token: 0x04000F4D RID: 3917
		private byte _food;

		// Token: 0x04000F4E RID: 3918
		private byte _water;

		// Token: 0x04000F4F RID: 3919
		private byte _virus;

		// Token: 0x04000F50 RID: 3920
		private byte _disinfectant;

		// Token: 0x04000F51 RID: 3921
		private byte _energy;

		// Token: 0x04000F52 RID: 3922
		private byte _vision;

		// Token: 0x04000F53 RID: 3923
		private uint _warmth;

		// Token: 0x04000F54 RID: 3924
		private bool _hasBleeding;

		// Token: 0x04000F55 RID: 3925
		private bool _hasBroken;

		// Token: 0x04000F56 RID: 3926
		private bool _hasAid;
	}
}
