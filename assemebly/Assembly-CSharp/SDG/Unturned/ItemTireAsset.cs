using System;

namespace SDG.Unturned
{
	// Token: 0x020003EE RID: 1006
	public class ItemTireAsset : ItemVehicleRepairToolAsset
	{
		// Token: 0x06001B3D RID: 6973 RVA: 0x0009736C File Offset: 0x0009576C
		public ItemTireAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._mode = (EUseableTireMode)Enum.Parse(typeof(EUseableTireMode), data.readString("Mode"), true);
			bundle.unload();
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x000973A5 File Offset: 0x000957A5
		public EUseableTireMode mode
		{
			get
			{
				return this._mode;
			}
		}

		// Token: 0x04001011 RID: 4113
		private EUseableTireMode _mode;
	}
}
