using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SDG.Unturned
{
	// Token: 0x0200079C RID: 1948
	public class PlayerGroupUI
	{
		// Token: 0x06003884 RID: 14468 RVA: 0x00199A88 File Offset: 0x00197E88
		public PlayerGroupUI()
		{
			PlayerGroupUI._container = new Sleek();
			PlayerGroupUI.container.sizeScale_X = 1f;
			PlayerGroupUI.container.sizeScale_Y = 1f;
			PlayerUI.container.add(PlayerGroupUI.container);
			PlayerGroupUI._groups = new List<SleekLabel>();
			for (int i = 0; i < Provider.clients.Count; i++)
			{
				PlayerGroupUI.addGroup(Provider.clients[i]);
			}
			if (PlayerGroupUI.<>f__mg$cache0 == null)
			{
				PlayerGroupUI.<>f__mg$cache0 = new Provider.EnemyConnected(PlayerGroupUI.onEnemyConnected);
			}
			Provider.onEnemyConnected = PlayerGroupUI.<>f__mg$cache0;
			if (PlayerGroupUI.<>f__mg$cache1 == null)
			{
				PlayerGroupUI.<>f__mg$cache1 = new Provider.EnemyDisconnected(PlayerGroupUI.onEnemyDisconnected);
			}
			Provider.onEnemyDisconnected = PlayerGroupUI.<>f__mg$cache1;
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06003885 RID: 14469 RVA: 0x00199B4B File Offset: 0x00197F4B
		public static Sleek container
		{
			get
			{
				return PlayerGroupUI._container;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06003886 RID: 14470 RVA: 0x00199B52 File Offset: 0x00197F52
		public static List<SleekLabel> groups
		{
			get
			{
				return PlayerGroupUI._groups;
			}
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x00199B5C File Offset: 0x00197F5C
		private static void addGroup(SteamPlayer player)
		{
			SleekLabel sleekLabel = new SleekLabel();
			sleekLabel.sizeOffset_X = 200;
			sleekLabel.sizeOffset_Y = 30;
			if (string.IsNullOrEmpty(player.playerID.nickName))
			{
				sleekLabel.text = player.playerID.characterName;
			}
			else
			{
				sleekLabel.text = player.playerID.nickName;
			}
			PlayerGroupUI.container.add(sleekLabel);
			sleekLabel.isVisible = false;
			PlayerGroupUI.groups.Add(sleekLabel);
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x00199BDB File Offset: 0x00197FDB
		private static void onEnemyConnected(SteamPlayer player)
		{
			PlayerGroupUI.addGroup(player);
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x00199BE4 File Offset: 0x00197FE4
		private static void onEnemyDisconnected(SteamPlayer player)
		{
			for (int i = 0; i < Provider.clients.Count; i++)
			{
				if (Provider.clients[i] == player)
				{
					PlayerGroupUI.container.remove(PlayerGroupUI.groups[i]);
					PlayerGroupUI.groups.RemoveAt(i);
				}
			}
		}

		// Token: 0x04002AC8 RID: 10952
		private static Sleek _container;

		// Token: 0x04002AC9 RID: 10953
		private static List<SleekLabel> _groups;

		// Token: 0x04002ACA RID: 10954
		[CompilerGenerated]
		private static Provider.EnemyConnected <>f__mg$cache0;

		// Token: 0x04002ACB RID: 10955
		[CompilerGenerated]
		private static Provider.EnemyDisconnected <>f__mg$cache1;
	}
}
