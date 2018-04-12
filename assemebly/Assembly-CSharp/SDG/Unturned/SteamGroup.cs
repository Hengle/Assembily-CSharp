using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000675 RID: 1653
	public class SteamGroup
	{
		// Token: 0x06003009 RID: 12297 RVA: 0x0013D91C File Offset: 0x0013BD1C
		public SteamGroup(CSteamID newSteamID, string newName, Texture2D newIcon)
		{
			this._steamID = newSteamID;
			this._name = newName;
			this._icon = newIcon;
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x0600300A RID: 12298 RVA: 0x0013D939 File Offset: 0x0013BD39
		public CSteamID steamID
		{
			get
			{
				return this._steamID;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x0600300B RID: 12299 RVA: 0x0013D941 File Offset: 0x0013BD41
		public string name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x0600300C RID: 12300 RVA: 0x0013D949 File Offset: 0x0013BD49
		public Texture2D icon
		{
			get
			{
				return this._icon;
			}
		}

		// Token: 0x04001FA7 RID: 8103
		private CSteamID _steamID;

		// Token: 0x04001FA8 RID: 8104
		private string _name;

		// Token: 0x04001FA9 RID: 8105
		private Texture2D _icon;
	}
}
