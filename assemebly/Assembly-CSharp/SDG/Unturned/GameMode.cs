using System;
using SDG.Framework.Devkit;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004BA RID: 1210
	public class GameMode : IDevkitHierarchySpawnable
	{
		// Token: 0x06002062 RID: 8290 RVA: 0x000B1D48 File Offset: 0x000B0148
		public GameMode()
		{
			Debug.Log(this);
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x000B1D56 File Offset: 0x000B0156
		public void devkitHierarchySpawn()
		{
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x000B1D58 File Offset: 0x000B0158
		public virtual GameObject getPlayerGameObject(SteamPlayerID playerID)
		{
			if (Dedicator.isDedicated)
			{
				return UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Characters/Player_Dedicated"));
			}
			if (playerID.steamID == Provider.client)
			{
				return UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Characters/Player_Server"));
			}
			return UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Characters/Player_Client"));
		}
	}
}
