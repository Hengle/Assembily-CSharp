using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200055B RID: 1371
	public class LevelStructures
	{
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x000DDB3D File Offset: 0x000DBF3D
		public static Transform models
		{
			get
			{
				return LevelStructures._models;
			}
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000DDB44 File Offset: 0x000DBF44
		public static void load()
		{
			LevelStructures._models = new GameObject().transform;
			LevelStructures.models.name = "Structures";
			LevelStructures.models.parent = Level.spawns;
			LevelStructures.models.tag = "Logic";
			LevelStructures.models.gameObject.layer = LayerMasks.LOGIC;
		}

		// Token: 0x04001792 RID: 6034
		private static Transform _models;
	}
}
