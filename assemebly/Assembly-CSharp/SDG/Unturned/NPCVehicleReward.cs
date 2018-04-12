using System;
using SDG.Framework.Devkit;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000417 RID: 1047
	public class NPCVehicleReward : INPCReward
	{
		// Token: 0x06001C30 RID: 7216 RVA: 0x0009969C File Offset: 0x00097A9C
		public NPCVehicleReward(ushort newID, string newSpawnpoint, string newText) : base(newText)
		{
			this.id = newID;
			this.spawnpoint = newSpawnpoint;
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001C31 RID: 7217 RVA: 0x000996B3 File Offset: 0x00097AB3
		// (set) Token: 0x06001C32 RID: 7218 RVA: 0x000996BB File Offset: 0x00097ABB
		public ushort id { get; protected set; }

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x000996C4 File Offset: 0x00097AC4
		// (set) Token: 0x06001C34 RID: 7220 RVA: 0x000996CC File Offset: 0x00097ACC
		public string spawnpoint { get; protected set; }

		// Token: 0x06001C35 RID: 7221 RVA: 0x000996D8 File Offset: 0x00097AD8
		public override void grantReward(Player player, bool shouldSend)
		{
			if (!Provider.isServer)
			{
				return;
			}
			Spawnpoint spawnpoint = SpawnpointSystem.getSpawnpoint(this.spawnpoint);
			if (spawnpoint == null)
			{
				Debug.LogError("Failed to find NPC vehicle reward spawnpoint: " + this.spawnpoint);
				return;
			}
			VehicleManager.spawnVehicle(this.id, spawnpoint.transform.position, spawnpoint.transform.rotation);
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x00099740 File Offset: 0x00097B40
		public override string formatReward(Player player)
		{
			if (string.IsNullOrEmpty(this.text))
			{
				this.text = PlayerNPCQuestUI.localization.read("Reward_Vehicle");
			}
			VehicleAsset vehicleAsset = Assets.find(EAssetType.VEHICLE, this.id) as VehicleAsset;
			string arg;
			if (vehicleAsset != null)
			{
				arg = string.Concat(new string[]
				{
					"<color=",
					Palette.hex(ItemTool.getRarityColorUI(vehicleAsset.rarity)),
					">",
					vehicleAsset.vehicleName,
					"</color>"
				});
			}
			else
			{
				arg = "?";
			}
			return string.Format(this.text, arg);
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x000997E8 File Offset: 0x00097BE8
		public override Sleek createUI(Player player)
		{
			string text = this.formatReward(player);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			if (!(Assets.find(EAssetType.VEHICLE, this.id) is VehicleAsset))
			{
				return null;
			}
			SleekBox sleekBox = new SleekBox();
			sleekBox.sizeOffset_Y = 30;
			sleekBox.sizeScale_X = 1f;
			sleekBox.add(new SleekLabel
			{
				positionOffset_X = 5,
				sizeOffset_X = -10,
				sizeScale_X = 1f,
				sizeScale_Y = 1f,
				fontAlignment = TextAnchor.MiddleLeft,
				foregroundTint = ESleekTint.NONE,
				isRich = true,
				text = text
			});
			return sleekBox;
		}
	}
}
