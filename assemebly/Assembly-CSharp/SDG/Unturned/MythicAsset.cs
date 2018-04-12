using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003F8 RID: 1016
	public class MythicAsset : Asset
	{
		// Token: 0x06001B61 RID: 7009 RVA: 0x00097924 File Offset: 0x00095D24
		public MythicAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (id < 500 && !bundle.hasResource && !data.has("Bypass_ID_Limit"))
			{
				throw new NotSupportedException("ID < 500");
			}
			if (!Dedicator.isDedicated)
			{
				this._systemArea = (GameObject)bundle.load("System_Area");
				this._systemHook = (GameObject)bundle.load("System_Hook");
				this._systemFirst = (GameObject)bundle.load("System_First");
				this._systemThird = (GameObject)bundle.load("System_Third");
			}
			bundle.unload();
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x000979D6 File Offset: 0x00095DD6
		public GameObject systemArea
		{
			get
			{
				return this._systemArea;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x000979DE File Offset: 0x00095DDE
		public GameObject systemHook
		{
			get
			{
				return this._systemHook;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x000979E6 File Offset: 0x00095DE6
		public GameObject systemFirst
		{
			get
			{
				return this._systemFirst;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x000979EE File Offset: 0x00095DEE
		public GameObject systemThird
		{
			get
			{
				return this._systemThird;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x000979F6 File Offset: 0x00095DF6
		public override EAssetType assetCategory
		{
			get
			{
				return EAssetType.MYTHIC;
			}
		}

		// Token: 0x0400103E RID: 4158
		protected GameObject _systemArea;

		// Token: 0x0400103F RID: 4159
		protected GameObject _systemHook;

		// Token: 0x04001040 RID: 4160
		protected GameObject _systemFirst;

		// Token: 0x04001041 RID: 4161
		protected GameObject _systemThird;
	}
}
