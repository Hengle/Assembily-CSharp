using System;
using SDG.Framework.Devkit;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000415 RID: 1045
	public class NPCTeleportReward : INPCReward
	{
		// Token: 0x06001C27 RID: 7207 RVA: 0x000994F1 File Offset: 0x000978F1
		public NPCTeleportReward(string newSpawnpoint, string newText) : base(newText)
		{
			this.spawnpoint = newSpawnpoint;
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x00099501 File Offset: 0x00097901
		// (set) Token: 0x06001C29 RID: 7209 RVA: 0x00099509 File Offset: 0x00097909
		public string spawnpoint { get; protected set; }

		// Token: 0x06001C2A RID: 7210 RVA: 0x00099514 File Offset: 0x00097914
		public override void grantReward(Player player, bool shouldSend)
		{
			if (!Provider.isServer)
			{
				return;
			}
			Spawnpoint spawnpoint = SpawnpointSystem.getSpawnpoint(this.spawnpoint);
			if (spawnpoint == null)
			{
				Debug.LogError("Failed to find NPC teleport reward spawnpoint: " + this.spawnpoint);
				return;
			}
			player.sendTeleport(spawnpoint.transform.position, MeasurementTool.angleToByte(spawnpoint.transform.rotation.eulerAngles.y));
		}
	}
}
