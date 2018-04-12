using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200046C RID: 1132
	public class CommandTeleport : Command
	{
		// Token: 0x06001E2E RID: 7726 RVA: 0x000A4B9C File Offset: 0x000A2F9C
		public CommandTeleport(Local newLocalization)
		{
			this.localization = newLocalization;
			this._command = this.localization.format("TeleportCommandText");
			this._info = this.localization.format("TeleportInfoText");
			this._help = this.localization.format("TeleportHelpText");
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x000A4BF8 File Offset: 0x000A2FF8
		protected override void execute(CSteamID executorID, string parameter)
		{
			if (!Provider.isServer)
			{
				CommandWindow.LogError(this.localization.format("NotRunningErrorText"));
				return;
			}
			string[] componentsFromSerial = Parser.getComponentsFromSerial(parameter, '/');
			if (componentsFromSerial.Length < 1 || componentsFromSerial.Length > 2)
			{
				CommandWindow.LogError(this.localization.format("InvalidParameterErrorText"));
				return;
			}
			bool flag = componentsFromSerial.Length == 1;
			SteamPlayer steamPlayer;
			if (flag)
			{
				steamPlayer = PlayerTool.getSteamPlayer(executorID);
			}
			else
			{
				PlayerTool.tryGetSteamPlayer(componentsFromSerial[0], out steamPlayer);
			}
			if (steamPlayer == null)
			{
				CommandWindow.LogError(this.localization.format("NoPlayerErrorText", new object[]
				{
					componentsFromSerial[0]
				}));
				return;
			}
			if (steamPlayer.player.movement.getVehicle() != null)
			{
				CommandWindow.LogError(this.localization.format("NoVehicleErrorText"));
				return;
			}
			SteamPlayer steamPlayer2;
			if (PlayerTool.tryGetSteamPlayer(componentsFromSerial[(!flag) ? 1 : 0], out steamPlayer2))
			{
				steamPlayer.player.sendTeleport(steamPlayer2.player.transform.position, MeasurementTool.angleToByte(steamPlayer2.player.transform.rotation.eulerAngles.y));
				CommandWindow.Log(this.localization.format("TeleportText", new object[]
				{
					steamPlayer.playerID.playerName,
					steamPlayer2.playerID.playerName
				}));
			}
			else if (componentsFromSerial[(!flag) ? 1 : 0].Equals(this.localization.format("WaypointCommand"), StringComparison.InvariantCultureIgnoreCase) && steamPlayer.player.quests.isMarkerPlaced)
			{
				Vector3 markerPosition = steamPlayer.player.quests.markerPosition;
				markerPosition.y = 1024f;
				RaycastHit raycastHit;
				if (Physics.Raycast(markerPosition, Vector3.down, out raycastHit, 2048f, RayMasks.WAYPOINT))
				{
					steamPlayer.player.sendTeleport(raycastHit.point + Vector3.up, MeasurementTool.angleToByte(steamPlayer.player.transform.rotation.eulerAngles.y));
					CommandWindow.Log(this.localization.format("TeleportText", new object[]
					{
						steamPlayer.playerID.playerName,
						this.localization.format("WaypointText")
					}));
				}
			}
			else
			{
				Node node = null;
				for (int i = 0; i < LevelNodes.nodes.Count; i++)
				{
					if (LevelNodes.nodes[i].type == ENodeType.LOCATION && NameTool.checkNames(componentsFromSerial[(!flag) ? 1 : 0], ((LocationNode)LevelNodes.nodes[i]).name))
					{
						node = LevelNodes.nodes[i];
						break;
					}
				}
				if (node != null)
				{
					steamPlayer.player.sendTeleport(node.point, MeasurementTool.angleToByte(steamPlayer.player.transform.rotation.eulerAngles.y));
					CommandWindow.Log(this.localization.format("TeleportText", new object[]
					{
						steamPlayer.playerID.playerName,
						((LocationNode)node).name
					}));
				}
				else
				{
					CommandWindow.LogError(this.localization.format("NoLocationErrorText", new object[]
					{
						componentsFromSerial[(!flag) ? 1 : 0]
					}));
				}
			}
		}
	}
}
