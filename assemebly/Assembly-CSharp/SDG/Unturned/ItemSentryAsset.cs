using System;

namespace SDG.Unturned
{
	// Token: 0x020003E4 RID: 996
	public class ItemSentryAsset : ItemStorageAsset
	{
		// Token: 0x06001B07 RID: 6919 RVA: 0x00096CF0 File Offset: 0x000950F0
		public ItemSentryAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (data.has("Mode"))
			{
				this._sentryMode = (ESentryMode)Enum.Parse(typeof(ESentryMode), data.readString("Mode"), true);
			}
			else
			{
				this._sentryMode = ESentryMode.NEUTRAL;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x00096D4A File Offset: 0x0009514A
		public ESentryMode sentryMode
		{
			get
			{
				return this._sentryMode;
			}
		}

		// Token: 0x04000FE4 RID: 4068
		protected ESentryMode _sentryMode;
	}
}
