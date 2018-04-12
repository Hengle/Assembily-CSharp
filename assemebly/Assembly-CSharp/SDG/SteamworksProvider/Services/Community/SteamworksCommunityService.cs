using System;
using System.Collections.Generic;
using SDG.Provider.Services;
using SDG.Provider.Services.Community;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace SDG.SteamworksProvider.Services.Community
{
	// Token: 0x02000369 RID: 873
	public class SteamworksCommunityService : Service, ICommunityService, IService
	{
		// Token: 0x060017DF RID: 6111 RVA: 0x000885B7 File Offset: 0x000869B7
		public SteamworksCommunityService()
		{
			this.cachedGroups = new Dictionary<CSteamID, SteamGroup>();
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x000885CA File Offset: 0x000869CA
		public void setStatus(string status)
		{
			SteamFriends.SetRichPresence("status", status);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x000885D8 File Offset: 0x000869D8
		public Texture2D getIcon(int id)
		{
			uint num;
			uint num2;
			if (id == -1 || !SteamUtils.GetImageSize(id, out num, out num2))
			{
				return null;
			}
			Texture2D texture2D = new Texture2D((int)num, (int)num2, TextureFormat.RGBA32, false);
			texture2D.name = "Steam_Community_Icon_Buffer";
			texture2D.hideFlags = HideFlags.HideAndDontSave;
			byte[] array = new byte[num * num2 * 4u];
			if (SteamUtils.GetImageRGBA(id, array, array.Length))
			{
				texture2D.LoadRawTextureData(array);
				texture2D.Apply();
				Texture2D texture2D2 = new Texture2D((int)num, (int)num2, TextureFormat.RGBA32, false, true);
				texture2D2.name = "Steam_Community_Icon";
				texture2D2.hideFlags = HideFlags.HideAndDontSave;
				int num3 = 0;
				while ((long)num3 < (long)((ulong)num2))
				{
					texture2D2.SetPixels(0, num3, (int)num, 1, texture2D.GetPixels(0, (int)(num2 - 1u - (uint)num3), (int)num, 1));
					num3++;
				}
				texture2D2.Apply();
				UnityEngine.Object.DestroyImmediate(texture2D);
				return texture2D2;
			}
			UnityEngine.Object.DestroyImmediate(texture2D);
			return null;
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x000886AD File Offset: 0x00086AAD
		public Texture2D getIcon(CSteamID steamID)
		{
			return this.getIcon(SteamFriends.GetSmallFriendAvatar(steamID));
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x000886BC File Offset: 0x00086ABC
		public SteamGroup getCachedGroup(CSteamID steamID)
		{
			SteamGroup result;
			this.cachedGroups.TryGetValue(steamID, out result);
			return result;
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x000886DC File Offset: 0x00086ADC
		public SteamGroup[] getGroups()
		{
			SteamGroup[] array = new SteamGroup[SteamFriends.GetClanCount()];
			for (int i = 0; i < array.Length; i++)
			{
				CSteamID clanByIndex = SteamFriends.GetClanByIndex(i);
				SteamGroup steamGroup = this.getCachedGroup(clanByIndex);
				if (steamGroup == null)
				{
					string clanName = SteamFriends.GetClanName(clanByIndex);
					Texture2D icon = this.getIcon(clanByIndex);
					steamGroup = new SteamGroup(clanByIndex, clanName, icon);
					this.cachedGroups.Add(clanByIndex, steamGroup);
				}
				array[i] = steamGroup;
			}
			return array;
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x0008874C File Offset: 0x00086B4C
		public bool checkGroup(CSteamID steamID)
		{
			for (int i = 0; i < SteamFriends.GetClanCount(); i++)
			{
				if (SteamFriends.GetClanByIndex(i) == steamID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000CC6 RID: 3270
		private Dictionary<CSteamID, SteamGroup> cachedGroups;
	}
}
