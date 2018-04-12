using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200058C RID: 1420
	public class BeaconManager : MonoBehaviour
	{
		// Token: 0x0600279F RID: 10143 RVA: 0x000F08D8 File Offset: 0x000EECD8
		public static int getParticipants(byte nav)
		{
			int num = 0;
			for (int i = 0; i < Provider.clients.Count; i++)
			{
				SteamPlayer steamPlayer = Provider.clients[i];
				if (!(steamPlayer.player == null) && !(steamPlayer.player.movement == null) && !(steamPlayer.player.life == null) && !steamPlayer.player.life.isDead)
				{
					if (steamPlayer.player.movement.nav == nav)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x000F0980 File Offset: 0x000EED80
		public static InteractableBeacon checkBeacon(byte nav)
		{
			if (BeaconManager.beacons[(int)nav].Count > 0)
			{
				return BeaconManager.beacons[(int)nav][0];
			}
			return null;
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000F09A3 File Offset: 0x000EEDA3
		public static void registerBeacon(byte nav, InteractableBeacon beacon)
		{
			if (!LevelNavigation.checkSafe(nav))
			{
				return;
			}
			BeaconManager.beacons[(int)nav].Add(beacon);
			if (BeaconManager.onBeaconUpdated != null)
			{
				BeaconManager.onBeaconUpdated(nav, BeaconManager.beacons[(int)nav].Count > 0);
			}
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000F09E2 File Offset: 0x000EEDE2
		public static void deregisterBeacon(byte nav, InteractableBeacon beacon)
		{
			if (!LevelNavigation.checkSafe(nav))
			{
				return;
			}
			BeaconManager.beacons[(int)nav].Remove(beacon);
			if (BeaconManager.onBeaconUpdated != null)
			{
				BeaconManager.onBeaconUpdated(nav, BeaconManager.beacons[(int)nav].Count > 0);
			}
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000F0A24 File Offset: 0x000EEE24
		private void onLevelLoaded(int level)
		{
			if (LevelNavigation.bounds == null)
			{
				return;
			}
			BeaconManager.beacons = new List<InteractableBeacon>[LevelNavigation.bounds.Count];
			for (int i = 0; i < BeaconManager.beacons.Length; i++)
			{
				BeaconManager.beacons[i] = new List<InteractableBeacon>();
			}
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x000F0A74 File Offset: 0x000EEE74
		private void Start()
		{
			Level.onPrePreLevelLoaded = (PrePreLevelLoaded)Delegate.Combine(Level.onPrePreLevelLoaded, new PrePreLevelLoaded(this.onLevelLoaded));
		}

		// Token: 0x040018C9 RID: 6345
		private static List<InteractableBeacon>[] beacons;

		// Token: 0x040018CA RID: 6346
		public static BeaconUpdated onBeaconUpdated;
	}
}
