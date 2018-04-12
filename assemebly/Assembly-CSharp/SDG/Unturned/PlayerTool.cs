using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000741 RID: 1857
	public class PlayerTool
	{
		// Token: 0x06003454 RID: 13396 RVA: 0x00156B84 File Offset: 0x00154F84
		private static string getRepKey(int rep)
		{
			string result = string.Empty;
			if (rep <= -200)
			{
				result = "Villain";
			}
			else if (rep <= -100)
			{
				result = "Bandit";
			}
			else if (rep <= -33)
			{
				result = "Gangster";
			}
			else if (rep <= -8)
			{
				result = "Outlaw";
			}
			else if (rep < 0)
			{
				result = "Thug";
			}
			else if (rep >= 200)
			{
				result = "Paragon";
			}
			else if (rep >= 100)
			{
				result = "Sheriff";
			}
			else if (rep >= 33)
			{
				result = "Deputy";
			}
			else if (rep >= 8)
			{
				result = "Constable";
			}
			else if (rep > 0)
			{
				result = "Vigilante";
			}
			else if (rep == 0)
			{
				result = "Neutral";
			}
			return result;
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x00156C65 File Offset: 0x00155065
		public static Texture2D getRepTexture(int rep)
		{
			return (Texture2D)Resources.Load("Reputation/" + PlayerTool.getRepKey(rep));
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x00156C81 File Offset: 0x00155081
		public static string getRepTitle(int rep)
		{
			return PlayerDashboardInformationUI.localization.format("Rep", new object[]
			{
				PlayerDashboardInformationUI.localization.format("Rep_" + PlayerTool.getRepKey(rep)),
				rep
			});
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x00156CC0 File Offset: 0x001550C0
		public static Color getRepColor(int rep)
		{
			if (rep == 0)
			{
				return Color.white;
			}
			if (rep < 0)
			{
				float num = (float)Mathf.Min(Mathf.Abs(rep), 200) / 200f;
				if (num < 0.5f)
				{
					return Color.Lerp(Color.white, Palette.COLOR_Y, num * 2f);
				}
				return Color.Lerp(Palette.COLOR_Y, Palette.COLOR_R, (num - 0.5f) * 2f);
			}
			else
			{
				if (rep > 0)
				{
					float t = (float)Mathf.Min(Mathf.Abs(rep), 200) / 200f;
					return Color.Lerp(Color.white, Palette.COLOR_G, t);
				}
				return Color.white;
			}
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x00156D6C File Offset: 0x0015516C
		public static void getPlayersInRadius(Vector3 center, float sqrRadius, List<Player> result)
		{
			for (int i = 0; i < Provider.clients.Count; i++)
			{
				Player player = Provider.clients[i].player;
				if (!(player == null))
				{
					if ((player.transform.position - center).sqrMagnitude < sqrRadius)
					{
						result.Add(player);
					}
				}
			}
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x00156DDC File Offset: 0x001551DC
		public static SteamPlayer[] getSteamPlayers()
		{
			return Provider.clients.ToArray();
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x00156DE8 File Offset: 0x001551E8
		public static SteamPlayer getSteamPlayer(string name)
		{
			for (int i = 0; i < Provider.clients.Count; i++)
			{
				if (NameTool.checkNames(name, Provider.clients[i].playerID.playerName) || NameTool.checkNames(name, Provider.clients[i].playerID.characterName))
				{
					return Provider.clients[i];
				}
			}
			return null;
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x00156E60 File Offset: 0x00155260
		public static SteamPlayer getSteamPlayer(ulong steamID)
		{
			for (int i = 0; i < Provider.clients.Count; i++)
			{
				if (Provider.clients[i].playerID.steamID.m_SteamID == steamID)
				{
					return Provider.clients[i];
				}
			}
			return null;
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x00156EB8 File Offset: 0x001552B8
		public static SteamPlayer getSteamPlayer(CSteamID steamID)
		{
			for (int i = 0; i < Provider.clients.Count; i++)
			{
				if (Provider.clients[i].playerID.steamID == steamID)
				{
					return Provider.clients[i];
				}
			}
			return null;
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x00156F10 File Offset: 0x00155310
		public static Transform getPlayerModel(CSteamID steamID)
		{
			SteamPlayer steamPlayer = PlayerTool.getSteamPlayer(steamID);
			if (steamPlayer != null && steamPlayer.model != null)
			{
				return steamPlayer.model;
			}
			return null;
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x00156F44 File Offset: 0x00155344
		public static Player getPlayer(CSteamID steamID)
		{
			SteamPlayer steamPlayer = PlayerTool.getSteamPlayer(steamID);
			if (steamPlayer != null && steamPlayer.player != null)
			{
				return steamPlayer.player;
			}
			return null;
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x00156F78 File Offset: 0x00155378
		public static Transform getPlayerModel(string name)
		{
			SteamPlayer steamPlayer = PlayerTool.getSteamPlayer(name);
			if (steamPlayer != null && steamPlayer.model != null)
			{
				return steamPlayer.model;
			}
			return null;
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x00156FAC File Offset: 0x001553AC
		public static Player getPlayer(string name)
		{
			SteamPlayer steamPlayer = PlayerTool.getSteamPlayer(name);
			if (steamPlayer != null && steamPlayer.player != null)
			{
				return steamPlayer.player;
			}
			return null;
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x00156FE0 File Offset: 0x001553E0
		public static bool tryGetSteamPlayer(string input, out SteamPlayer player)
		{
			player = null;
			ulong steamID;
			if (ulong.TryParse(input, out steamID))
			{
				player = PlayerTool.getSteamPlayer(steamID);
				return player != null;
			}
			player = PlayerTool.getSteamPlayer(input);
			return player != null;
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x00157020 File Offset: 0x00155420
		public static bool tryGetSteamID(string input, out CSteamID steamID)
		{
			steamID = CSteamID.Nil;
			ulong ulSteamID;
			if (ulong.TryParse(input, out ulSteamID))
			{
				steamID = new CSteamID(ulSteamID);
				return true;
			}
			SteamPlayer steamPlayer = PlayerTool.getSteamPlayer(input);
			if (steamPlayer != null)
			{
				steamID = steamPlayer.playerID.steamID;
				return true;
			}
			return false;
		}
	}
}
