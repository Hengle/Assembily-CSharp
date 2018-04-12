using System;

namespace SDG.Unturned
{
	// Token: 0x020003EB RID: 1003
	public class ItemTankAsset : ItemBarricadeAsset
	{
		// Token: 0x06001B2F RID: 6959 RVA: 0x00097140 File Offset: 0x00095540
		public ItemTankAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._source = (ETankSource)Enum.Parse(typeof(ETankSource), data.readString("Source"), true);
			this._resource = data.readUInt16("Resource");
			this.resourceState = BitConverter.GetBytes(this.resource);
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x000971A0 File Offset: 0x000955A0
		public ETankSource source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x000971A8 File Offset: 0x000955A8
		public ushort resource
		{
			get
			{
				return this._resource;
			}
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x000971B0 File Offset: 0x000955B0
		public override byte[] getState(EItemOrigin origin)
		{
			byte[] array = new byte[2];
			if (origin == EItemOrigin.ADMIN)
			{
				array[0] = this.resourceState[0];
				array[1] = this.resourceState[1];
			}
			return array;
		}

		// Token: 0x04001003 RID: 4099
		protected ETankSource _source;

		// Token: 0x04001004 RID: 4100
		protected ushort _resource;

		// Token: 0x04001005 RID: 4101
		private byte[] resourceState;
	}
}
