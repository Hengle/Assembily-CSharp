using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000545 RID: 1349
	public class LevelBarricades
	{
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x000CB7FB File Offset: 0x000C9BFB
		public static Transform models
		{
			get
			{
				return LevelBarricades._models;
			}
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000CB804 File Offset: 0x000C9C04
		public static void load()
		{
			LevelBarricades._models = new GameObject().transform;
			LevelBarricades.models.name = "Barricades";
			LevelBarricades.models.parent = Level.spawns;
			LevelBarricades.models.tag = "Logic";
			LevelBarricades.models.gameObject.layer = LayerMasks.LOGIC;
		}

		// Token: 0x04001658 RID: 5720
		private static Transform _models;
	}
}
