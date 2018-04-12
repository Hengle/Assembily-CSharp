using System;

namespace SDG.Unturned
{
	// Token: 0x020003D1 RID: 977
	public class ItemGeneratorAsset : ItemBarricadeAsset
	{
		// Token: 0x06001AA3 RID: 6819 RVA: 0x00094F1D File Offset: 0x0009331D
		public ItemGeneratorAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._capacity = data.readUInt16("Capacity");
			this._wirerange = data.readSingle("Wirerange");
			this._burn = data.readSingle("Burn");
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x00094F5D File Offset: 0x0009335D
		public ushort capacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x00094F65 File Offset: 0x00093365
		public float wirerange
		{
			get
			{
				return this._wirerange;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x00094F6D File Offset: 0x0009336D
		public float burn
		{
			get
			{
				return this._burn;
			}
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00094F75 File Offset: 0x00093375
		public override byte[] getState(EItemOrigin origin)
		{
			return new byte[3];
		}

		// Token: 0x04000F66 RID: 3942
		protected ushort _capacity;

		// Token: 0x04000F67 RID: 3943
		protected float _wirerange;

		// Token: 0x04000F68 RID: 3944
		protected float _burn;
	}
}
