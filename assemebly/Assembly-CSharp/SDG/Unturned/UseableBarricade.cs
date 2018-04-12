using System;
using System.Collections.Generic;
using SDG.Framework.Devkit;
using SDG.Framework.Water;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007AE RID: 1966
	public class UseableBarricade : Useable
	{
		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x0600391F RID: 14623 RVA: 0x001A3647 File Offset: 0x001A1A47
		private bool isUseable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime;
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06003920 RID: 14624 RVA: 0x001A365D File Offset: 0x001A1A5D
		private bool isBuildable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime * 0.8f;
			}
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x001A367C File Offset: 0x001A1A7C
		[SteamCall]
		public void askBarricadeVehicle(CSteamID steamID, Vector3 newPoint, float newAngle_X, float newAngle_Y, float newAngle_Z, ushort plant)
		{
			if (base.channel.checkOwner(steamID))
			{
				BarricadeRegion barricadeRegion;
				if (BarricadeManager.tryGetRegion(0, 0, plant, out barricadeRegion) && (base.player.look.aim.position - barricadeRegion.parent.position).sqrMagnitude < 4096f)
				{
					this.parent = barricadeRegion.parent;
					this.parentVehicle = DamageTool.getVehicle(this.parent);
					this.point = newPoint;
					if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.FREEFORM || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.CHARGE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.CLOCK || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.NOTE)
					{
						this.angle_x = newAngle_X;
						this.angle_z = newAngle_Z;
					}
					else
					{
						this.angle_x = 0f;
						this.angle_z = 0f;
					}
					this.angle_y = newAngle_Y;
					this.rotate_x = 0f;
					this.rotate_y = 0f;
					this.rotate_z = 0f;
					this.isValid = this.checkClaims();
				}
				this.wasAsked = true;
			}
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x001A37F0 File Offset: 0x001A1BF0
		[SteamCall]
		public void askBarricadeNone(CSteamID steamID, Vector3 newPoint, float newAngle_X, float newAngle_Y, float newAngle_Z)
		{
			if (base.channel.checkOwner(steamID))
			{
				if ((newPoint - base.player.look.aim.position).sqrMagnitude < 256f)
				{
					this.parent = null;
					this.parentVehicle = null;
					this.point = newPoint;
					if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.FREEFORM || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.CHARGE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.CLOCK || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.NOTE)
					{
						this.angle_x = newAngle_X;
						this.angle_z = newAngle_Z;
					}
					else
					{
						this.angle_x = 0f;
						this.angle_z = 0f;
					}
					this.angle_y = newAngle_Y;
					this.rotate_x = 0f;
					this.rotate_y = 0f;
					this.rotate_z = 0f;
					this.isValid = this.checkClaims();
				}
				this.wasAsked = true;
			}
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x001A393A File Offset: 0x001A1D3A
		private bool check()
		{
			return this.checkSpace() && this.checkClaims();
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x001A3958 File Offset: 0x001A1D58
		private bool checkClaims()
		{
			if (base.player.movement.isSafe && base.player.movement.isSafeInfo.noBuildables)
			{
				if (base.channel.isOwner)
				{
					PlayerUI.hint(null, EPlayerMessage.SAFEZONE);
				}
				return false;
			}
			Vector3 vector = this.point;
			if (!base.channel.isOwner && this.parent != null)
			{
				vector = this.parent.TransformPoint(this.point);
			}
			if (((ItemBarricadeAsset)base.player.equipment.asset).build != EBuild.CHARGE && !((ItemBarricadeAsset)base.player.equipment.asset).bypassClaim)
			{
				if (!ClaimManager.checkCanBuild(vector, base.channel.owner.playerID.steamID, base.player.quests.groupID, ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.CLAIM))
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.CLAIM);
					}
					return false;
				}
				if (PlayerClipVolumeUtility.isPointInsideVolume(vector))
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BOUNDS);
					}
					return false;
				}
			}
			if ((Level.info == null || Level.info.type == ELevelType.ARENA) && ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.BED)
			{
				return false;
			}
			if ((((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.BED || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SENTRY) && this.parent != null && this.parentVehicle != null && this.parentVehicle.asset != null && !this.parentVehicle.asset.supportsMobileBuildables)
			{
				if (base.channel.isOwner)
				{
					PlayerUI.hint(null, EPlayerMessage.MOBILE);
				}
				return false;
			}
			if ((Level.info == null || Level.info.type != ELevelType.ARENA) && !LevelPlayers.checkCanBuild(vector))
			{
				if (base.channel.isOwner)
				{
					PlayerUI.hint(null, EPlayerMessage.SPAWN);
				}
				return false;
			}
			if (WaterUtility.isPointUnderwater(vector) && (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.CAMPFIRE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.TORCH))
			{
				if (base.channel.isOwner)
				{
					PlayerUI.hint(null, EPlayerMessage.UNDERWATER);
				}
				return false;
			}
			if (Dedicator.isDedicated)
			{
				this.boundsRotation = BarricadeManager.getRotation((ItemBarricadeAsset)base.player.equipment.asset, this.angle_x + this.rotate_x, this.angle_y + this.rotate_y, this.angle_z + this.rotate_z);
			}
			else
			{
				this.boundsRotation = this.help.rotation;
			}
			if (Physics.OverlapBoxNonAlloc(this.point + this.boundsRotation * this.boundsCenter, this.boundsOverlap, UseableBarricade.checkColliders, this.boundsRotation, RayMasks.BLOCK_CHAR_BUILDABLE_OVERLAP, QueryTriggerInteraction.Collide) > 0)
			{
				if (base.channel.isOwner)
				{
					PlayerUI.hint(null, EPlayerMessage.BLOCKED);
				}
				return false;
			}
			if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.DOOR || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.GATE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SHUTTER)
			{
				Vector3 halfExtents = this.boundsExtents;
				halfExtents.x -= 0.25f;
				halfExtents.y -= 0.5f;
				halfExtents.z += 0.6f;
				if (Physics.OverlapBoxNonAlloc(this.point + this.boundsRotation * this.boundsCenter, halfExtents, UseableBarricade.checkColliders, this.boundsRotation, RayMasks.BLOCK_DOOR_OPENING) > 0)
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
				bool flag = false;
				bool flag2 = false;
				if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.DOOR)
				{
					flag = true;
					flag2 = this.boundsDoubleDoor;
				}
				else if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.GATE)
				{
					flag = this.boundsDoubleDoor;
					flag2 = this.boundsDoubleDoor;
				}
				else if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SHUTTER)
				{
					flag = true;
					flag2 = true;
				}
				if (flag && Physics.OverlapSphereNonAlloc(this.point + this.boundsRotation * new Vector3(-this.boundsExtents.x, 0f, this.boundsExtents.x), 0.75f, UseableBarricade.checkColliders, RayMasks.BLOCK_DOOR_OPENING) > 0)
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
				if (flag2 && Physics.OverlapSphereNonAlloc(this.point + this.boundsRotation * new Vector3(this.boundsExtents.x, 0f, this.boundsExtents.x), 0.75f, UseableBarricade.checkColliders, RayMasks.BLOCK_DOOR_OPENING) > 0)
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x001A3F58 File Offset: 0x001A2358
		private bool checkSpace()
		{
			this.angle_y = base.player.look.yaw;
			if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.FORTIFICATION || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SHUTTER || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.GLASS)
			{
				Physics.Raycast(base.player.look.aim.position, base.player.look.aim.forward, out this.hit, ((ItemBarricadeAsset)base.player.equipment.asset).range, RayMasks.SLOTS_INTERACT);
				if (!(this.hit.transform != null))
				{
					this.point = Vector3.zero;
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.WINDOW);
					}
					return false;
				}
				if (!this.hit.transform.CompareTag("Logic") || !(this.hit.transform.name == "Slot"))
				{
					this.point = Vector3.zero;
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.WINDOW);
					}
					return false;
				}
				this.point = this.hit.point - this.hit.normal * ((ItemBarricadeAsset)base.player.equipment.asset).offset;
				this.angle_y = this.hit.transform.rotation.eulerAngles.y;
				if (Mathf.Abs(Vector3.Dot(this.hit.transform.right, Vector3.up)) > 0.5f)
				{
					if (Vector3.Dot(MainCamera.instance.transform.forward, this.hit.transform.forward) < 0f)
					{
						this.angle_y += 180f;
					}
				}
				else if (Vector3.Dot(MainCamera.instance.transform.forward, this.hit.transform.up) > 0f)
				{
					this.angle_y += 180f;
				}
				if ((((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SHUTTER || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.GLASS) && (this.hit.transform.parent.CompareTag("Barricade") || this.hit.transform.parent.CompareTag("Structure")))
				{
					this.point = this.hit.transform.position - this.hit.normal * ((ItemBarricadeAsset)base.player.equipment.asset).offset;
				}
				if (!Level.checkSafe(this.point))
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BOUNDS);
					}
					return false;
				}
				if (Physics.OverlapSphereNonAlloc(this.point, ((ItemBarricadeAsset)base.player.equipment.asset).radius, UseableBarricade.checkColliders, RayMasks.BLOCK_WINDOW) > 0)
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
				return true;
			}
			else if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.BARRICADE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.TANK || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.LIBRARY || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.BARREL_RAIN || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.VEHICLE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.BED || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.STORAGE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.MANNEQUIN || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SENTRY || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.GENERATOR || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SPOT || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.CAMPFIRE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.OVEN || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.CLAIM || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SPIKE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SAFEZONE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.OXYGENATOR || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.BEACON || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SIGN || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.STEREO)
			{
				Physics.SphereCast(base.player.look.aim.position, 0.1f, base.player.look.aim.forward, out this.hit, ((ItemBarricadeAsset)base.player.equipment.asset).range, RayMasks.BARRICADE_INTERACT);
				if (!(this.hit.transform != null))
				{
					this.point = Vector3.zero;
					return false;
				}
				if (this.hit.normal.y < 0.01f)
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
				if ((double)this.hit.normal.y > 0.75)
				{
					this.point = this.hit.point + this.hit.normal * ((ItemBarricadeAsset)base.player.equipment.asset).offset;
				}
				else
				{
					this.point = this.hit.point + Vector3.up * ((ItemBarricadeAsset)base.player.equipment.asset).offset;
				}
				if (!Level.checkSafe(this.point))
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BOUNDS);
					}
					return false;
				}
				if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.BEACON && (!LevelNavigation.checkSafeFakeNav(this.point) || this.parent != null))
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.NAV);
					}
					return false;
				}
				if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.BED)
				{
					if (Physics.OverlapSphereNonAlloc(this.point + Vector3.up, 0.99f + ((ItemBarricadeAsset)base.player.equipment.asset).offset, UseableBarricade.checkColliders, RayMasks.BLOCK_BARRICADE) > 0)
					{
						if (base.channel.isOwner)
						{
							PlayerUI.hint(null, EPlayerMessage.BLOCKED);
						}
						return false;
					}
				}
				else if (Physics.OverlapSphereNonAlloc(this.point, ((ItemBarricadeAsset)base.player.equipment.asset).radius, UseableBarricade.checkColliders, RayMasks.BLOCK_BARRICADE) > 0)
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
				return true;
			}
			else if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.WIRE)
			{
				Physics.SphereCast(base.player.look.aim.position, 0.1f, base.player.look.aim.forward, out this.hit, ((ItemBarricadeAsset)base.player.equipment.asset).range, RayMasks.BARRICADE_INTERACT);
				if (!(this.hit.transform != null))
				{
					this.point = Vector3.zero;
					return false;
				}
				this.point = this.hit.point + this.hit.normal * ((ItemBarricadeAsset)base.player.equipment.asset).offset;
				if (!Level.checkSafe(this.point))
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BOUNDS);
					}
					return false;
				}
				if (Physics.OverlapSphereNonAlloc(this.point, ((ItemBarricadeAsset)base.player.equipment.asset).radius, UseableBarricade.checkColliders, RayMasks.BLOCK_BARRICADE) > 0)
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
				return true;
			}
			else if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.FARM || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.OIL)
			{
				Physics.SphereCast(base.player.look.aim.position, 0.1f, base.player.look.aim.forward, out this.hit, ((ItemBarricadeAsset)base.player.equipment.asset).range, RayMasks.BARRICADE_INTERACT);
				if (!(this.hit.transform != null))
				{
					this.point = Vector3.zero;
					return false;
				}
				if ((double)this.hit.normal.y > 0.75)
				{
					this.point = this.hit.point + this.hit.normal * ((ItemBarricadeAsset)base.player.equipment.asset).offset;
				}
				else
				{
					this.point = this.hit.point + Vector3.up * ((ItemBarricadeAsset)base.player.equipment.asset).offset;
				}
				if (this.hit.transform.CompareTag("Ground"))
				{
					EPhysicsMaterial ephysicsMaterial = PhysicsTool.checkMaterial(this.point);
					if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.FARM)
					{
						if (ephysicsMaterial != EPhysicsMaterial.FOLIAGE_STATIC)
						{
							if (base.channel.isOwner)
							{
								PlayerUI.hint(null, EPlayerMessage.SOIL);
							}
							return false;
						}
					}
					else if (ephysicsMaterial == EPhysicsMaterial.CONCRETE_STATIC)
					{
						if (base.channel.isOwner)
						{
							PlayerUI.hint(null, EPlayerMessage.OIL);
						}
						return false;
					}
				}
				else
				{
					if (((ItemBarricadeAsset)base.player.equipment.asset).build != EBuild.FARM)
					{
						if (base.channel.isOwner)
						{
							PlayerUI.hint(null, EPlayerMessage.OIL);
						}
						return false;
					}
					EPhysicsMaterial ephysicsMaterial2 = PhysicsTool.checkMaterial(this.hit.collider);
					if (ephysicsMaterial2 != EPhysicsMaterial.FOLIAGE_STATIC)
					{
						if (base.channel.isOwner)
						{
							PlayerUI.hint(null, EPlayerMessage.SOIL);
						}
						return false;
					}
				}
				if (!Level.checkSafe(this.point))
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BOUNDS);
					}
					return false;
				}
				if (Physics.OverlapSphereNonAlloc(this.point, ((ItemBarricadeAsset)base.player.equipment.asset).radius, UseableBarricade.checkColliders, RayMasks.BLOCK_BARRICADE) > 0)
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
				return true;
			}
			else if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.DOOR)
			{
				Physics.SphereCast(base.player.look.aim.position, 0.1f, base.player.look.aim.forward, out this.hit, ((ItemBarricadeAsset)base.player.equipment.asset).range, RayMasks.SLOTS_INTERACT);
				if (!(this.hit.transform != null))
				{
					this.point = Vector3.zero;
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.DOORWAY);
					}
					return false;
				}
				if (!this.hit.transform.CompareTag("Logic") || !(this.hit.transform.name == "Door"))
				{
					this.point = Vector3.zero;
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.DOORWAY);
					}
					return false;
				}
				this.point = this.hit.transform.position;
				this.angle_y = this.hit.transform.rotation.eulerAngles.y;
				if (Vector3.Dot(MainCamera.instance.transform.forward, this.hit.transform.forward) < 0f)
				{
					this.angle_y += 180f;
				}
				if (!Level.checkSafe(this.point))
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BOUNDS);
					}
					return false;
				}
				if (Physics.OverlapSphereNonAlloc(this.point, ((ItemBarricadeAsset)base.player.equipment.asset).radius, UseableBarricade.checkColliders, RayMasks.BLOCK_FRAME) > 0)
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
				return true;
			}
			else if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.HATCH)
			{
				Physics.SphereCast(base.player.look.aim.position, 0.1f, base.player.look.aim.forward, out this.hit, ((ItemBarricadeAsset)base.player.equipment.asset).range, RayMasks.SLOTS_INTERACT);
				if (!(this.hit.transform != null))
				{
					this.point = Vector3.zero;
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.TRAPDOOR);
					}
					return false;
				}
				if (!this.hit.transform.CompareTag("Logic") || !(this.hit.transform.name == "Hatch"))
				{
					this.point = Vector3.zero;
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.TRAPDOOR);
					}
					return false;
				}
				this.point = this.hit.transform.position;
				this.angle_y = this.hit.transform.rotation.eulerAngles.y;
				float num = Vector3.Dot(MainCamera.instance.transform.forward, this.hit.transform.forward);
				float num2 = Vector3.Dot(MainCamera.instance.transform.forward, this.hit.transform.right);
				float num3 = Vector3.Dot(MainCamera.instance.transform.forward, -this.hit.transform.forward);
				float num4 = Vector3.Dot(MainCamera.instance.transform.forward, -this.hit.transform.right);
				float num5 = num;
				if (num2 < num5)
				{
					num5 = num2;
					this.angle_y = this.hit.transform.rotation.eulerAngles.y + 90f;
				}
				if (num3 < num5)
				{
					num5 = num3;
					this.angle_y = this.hit.transform.rotation.eulerAngles.y + 180f;
				}
				if (num4 < num5)
				{
					this.angle_y = this.hit.transform.rotation.eulerAngles.y + 270f;
				}
				this.angle_y += 180f;
				if (!Level.checkSafe(this.point))
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BOUNDS);
					}
					return false;
				}
				if (Physics.OverlapSphereNonAlloc(this.point, ((ItemBarricadeAsset)base.player.equipment.asset).radius, UseableBarricade.checkColliders, RayMasks.BLOCK_FRAME) > 0)
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
				return true;
			}
			else if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.GATE)
			{
				Physics.SphereCast(base.player.look.aim.position, 0.1f, base.player.look.aim.forward, out this.hit, ((ItemBarricadeAsset)base.player.equipment.asset).range, RayMasks.SLOTS_INTERACT);
				if (!(this.hit.transform != null))
				{
					this.point = Vector3.zero;
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.GARAGE);
					}
					return false;
				}
				if (!this.hit.transform.CompareTag("Logic") || !(this.hit.transform.name == "Gate"))
				{
					this.point = Vector3.zero;
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.GARAGE);
					}
					return false;
				}
				this.point = this.hit.transform.position;
				this.angle_y = this.hit.transform.rotation.eulerAngles.y;
				if (Mathf.Abs(Vector3.Dot(this.hit.transform.up, Vector3.up)) > 0.5f)
				{
					if (Vector3.Dot(MainCamera.instance.transform.forward, this.hit.transform.forward) < 0f)
					{
						this.angle_y += 180f;
					}
				}
				else if (Vector3.Dot(MainCamera.instance.transform.forward, this.hit.transform.up) > 0f)
				{
					this.angle_y += 180f;
				}
				if (!Level.checkSafe(this.point))
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BOUNDS);
					}
					return false;
				}
				if (Physics.OverlapSphereNonAlloc(this.point, ((ItemBarricadeAsset)base.player.equipment.asset).radius, UseableBarricade.checkColliders, RayMasks.BLOCK_FRAME) > 0)
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
				if (Physics.OverlapSphereNonAlloc(this.point + this.hit.transform.forward * -1.5f + this.hit.transform.up * -2f, 0.25f, UseableBarricade.checkColliders, RayMasks.BLOCK_FRAME) > 0)
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
				return true;
			}
			else if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.LADDER)
			{
				Physics.SphereCast(base.player.look.aim.position, 0.1f, base.player.look.aim.forward, out this.hit, ((ItemBarricadeAsset)base.player.equipment.asset).range, RayMasks.LADDERS_INTERACT);
				if (this.hit.transform != null)
				{
					if (this.hit.transform.CompareTag("Logic") && this.hit.transform.name == "Climb")
					{
						this.point = this.hit.transform.position;
						this.angle_y = this.hit.transform.rotation.eulerAngles.y;
						if (Physics.OverlapSphereNonAlloc(this.point + this.hit.transform.up * 0.5f, 0.1f, UseableBarricade.checkColliders, RayMasks.BLOCK_BARRICADE) > 0)
						{
							if (base.channel.isOwner)
							{
								PlayerUI.hint(null, EPlayerMessage.BLOCKED);
							}
							return false;
						}
						if (Physics.OverlapSphereNonAlloc(this.point + this.hit.transform.up * -0.5f, 0.1f, UseableBarricade.checkColliders, RayMasks.BLOCK_BARRICADE) > 0)
						{
							if (base.channel.isOwner)
							{
								PlayerUI.hint(null, EPlayerMessage.BLOCKED);
							}
							return false;
						}
					}
					else
					{
						if (Mathf.Abs(this.hit.normal.y) < 0.1f)
						{
							this.point = this.hit.point + this.hit.normal * ((ItemBarricadeAsset)base.player.equipment.asset).offset;
							this.angle_y = Quaternion.LookRotation(this.hit.normal).eulerAngles.y;
							if (Physics.OverlapSphereNonAlloc(this.point + Quaternion.Euler(0f, this.angle_y, 0f) * Vector3.right * 0.5f, 0.1f, UseableBarricade.checkColliders, RayMasks.BLOCK_BARRICADE) > 0)
							{
								if (base.channel.isOwner)
								{
									PlayerUI.hint(null, EPlayerMessage.BLOCKED);
								}
								return false;
							}
							if (Physics.OverlapSphereNonAlloc(this.point + Quaternion.Euler(0f, this.angle_y, 0f) * Vector3.left * 0.5f, 0.1f, UseableBarricade.checkColliders, RayMasks.BLOCK_BARRICADE) > 0)
							{
								if (base.channel.isOwner)
								{
									PlayerUI.hint(null, EPlayerMessage.BLOCKED);
								}
								return false;
							}
						}
						else
						{
							if (this.hit.normal.y > 0.75f)
							{
								this.point = this.hit.point + this.hit.normal * StructureManager.HEIGHT;
							}
							else
							{
								this.point = this.hit.point + Vector3.up * StructureManager.HEIGHT;
							}
							if (Physics.OverlapSphereNonAlloc(this.point, 0.5f, UseableBarricade.checkColliders, RayMasks.BLOCK_BARRICADE) > 0)
							{
								if (base.channel.isOwner)
								{
									PlayerUI.hint(null, EPlayerMessage.BLOCKED);
								}
								return false;
							}
						}
						if (!Level.checkSafe(this.point))
						{
							if (base.channel.isOwner)
							{
								PlayerUI.hint(null, EPlayerMessage.BOUNDS);
							}
							return false;
						}
					}
					return true;
				}
				this.point = Vector3.zero;
				return false;
			}
			else if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.TORCH || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.STORAGE_WALL || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SIGN_WALL || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.CAGE)
			{
				Physics.SphereCast(base.player.look.aim.position, 0.1f, base.player.look.aim.forward, out this.hit, ((ItemBarricadeAsset)base.player.equipment.asset).range, RayMasks.BARRICADE_INTERACT);
				if (!(this.hit.transform != null) || Mathf.Abs(this.hit.normal.y) >= 0.1f)
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.WALL);
					}
					this.point = Vector3.zero;
					return false;
				}
				this.point = this.hit.point + this.hit.normal * ((ItemBarricadeAsset)base.player.equipment.asset).offset;
				this.angle_y = Quaternion.LookRotation(this.hit.normal).eulerAngles.y;
				if (Physics.OverlapSphereNonAlloc(this.point, 0.1f, UseableBarricade.checkColliders, RayMasks.BLOCK_BARRICADE) > 0)
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
				if (!Level.checkSafe(this.point))
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BOUNDS);
					}
					return false;
				}
				return true;
			}
			else if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.FREEFORM)
			{
				Physics.SphereCast(base.player.look.aim.position, 0.1f, base.player.look.aim.forward, out this.hit, ((ItemBarricadeAsset)base.player.equipment.asset).range, RayMasks.BARRICADE_INTERACT);
				if (!(this.hit.transform != null))
				{
					this.point = Vector3.zero;
					return false;
				}
				Quaternion quaternion = Quaternion.Euler(0f, this.angle_y + this.rotate_y, 0f);
				quaternion *= Quaternion.Euler(-90f + this.angle_x + this.rotate_x, 0f, 0f);
				quaternion *= Quaternion.Euler(0f, this.angle_z + this.rotate_z, 0f);
				this.point = this.hit.point + this.hit.normal * -0.125f + quaternion * Vector3.forward * ((ItemBarricadeAsset)base.player.equipment.asset).offset;
				if (!Level.checkSafe(this.point))
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BOUNDS);
					}
					return false;
				}
				if (Physics.OverlapSphereNonAlloc(this.point, ((ItemBarricadeAsset)base.player.equipment.asset).radius, UseableBarricade.checkColliders, RayMasks.BLOCK_BARRICADE) > 0)
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
				return true;
			}
			else
			{
				if (((ItemBarricadeAsset)base.player.equipment.asset).build != EBuild.CHARGE && ((ItemBarricadeAsset)base.player.equipment.asset).build != EBuild.CLOCK && ((ItemBarricadeAsset)base.player.equipment.asset).build != EBuild.NOTE)
				{
					this.point = Vector3.zero;
					return false;
				}
				Physics.SphereCast(base.player.look.aim.position, 0.1f, base.player.look.aim.forward, out this.hit, ((ItemBarricadeAsset)base.player.equipment.asset).range, RayMasks.BARRICADE_INTERACT);
				if (this.hit.transform != null)
				{
					Vector3 eulerAngles = Quaternion.LookRotation(this.hit.normal).eulerAngles;
					this.angle_x = eulerAngles.x;
					this.angle_y = eulerAngles.y;
					this.angle_z = eulerAngles.z;
					this.point = this.hit.point + this.hit.normal * ((ItemBarricadeAsset)base.player.equipment.asset).offset;
					return true;
				}
				this.point = Vector3.zero;
				return false;
			}
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x001A5E76 File Offset: 0x001A4276
		private void build()
		{
			this.startedUse = Time.realtimeSinceStartup;
			this.isUsing = true;
			this.isBuilding = true;
			base.player.animator.play("Use", false);
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x001A5EA7 File Offset: 0x001A42A7
		[SteamCall]
		public void askBuild(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.build();
			}
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x001A5ED8 File Offset: 0x001A42D8
		public override void startPrimary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			if (base.player.movement.getVehicle() != null)
			{
				return;
			}
			if ((!Dedicator.isDedicated) ? this.check() : this.isValid)
			{
				if (base.channel.isOwner)
				{
					if (this.parent != null)
					{
						byte b;
						byte b2;
						ushort num;
						BarricadeRegion barricadeRegion;
						if (BarricadeManager.tryGetPlant(this.parent, out b, out b2, out num, out barricadeRegion))
						{
							base.channel.send("askBarricadeVehicle", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								this.parent.InverseTransformPoint(this.point),
								this.angle_x + this.rotate_x,
								this.angle_y + this.rotate_y - this.parent.localRotation.eulerAngles.y,
								this.angle_z + this.rotate_z,
								num
							});
						}
					}
					else
					{
						base.channel.send("askBarricadeNone", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							this.point,
							this.angle_x + this.rotate_x,
							this.angle_y + this.rotate_y,
							this.angle_z + this.rotate_z
						});
					}
				}
				base.player.equipment.isBusy = true;
				this.build();
				if (Provider.isServer)
				{
					base.channel.send("askBuild", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
				}
			}
			else if (Dedicator.isDedicated && this.wasAsked)
			{
				base.player.equipment.dequip();
			}
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x001A60D8 File Offset: 0x001A44D8
		public override void startSecondary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.GLASS || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.CHARGE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.CLOCK || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.NOTE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.FORTIFICATION || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.DOOR || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.GATE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SHUTTER || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.HATCH || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.TORCH || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.CAGE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.STORAGE_WALL || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SIGN_WALL)
			{
				return;
			}
			base.player.look.isIgnoringInput = true;
			this.isRotating = true;
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x001A62BC File Offset: 0x001A46BC
		public override void stopSecondary()
		{
			base.player.look.isIgnoringInput = false;
			this.isRotating = false;
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x001A62D8 File Offset: 0x001A46D8
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			this.useTime = base.player.animator.getAnimationLength("Use");
			if (Dedicator.isDedicated)
			{
				if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.MANNEQUIN)
				{
					this.boundsUse = true;
					this.boundsCenter = new Vector3(0f, 0f, -0.05f);
					this.boundsExtents = new Vector3(1.175f, 0.2f, 1.05f);
				}
				else
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(((ItemBarricadeAsset)base.player.equipment.asset).clip, Vector3.zero, Quaternion.identity);
					gameObject.name = "Helper";
					Collider collider;
					if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.DOOR || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.GATE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SHUTTER)
					{
						collider = gameObject.transform.FindChild("Placeholder").GetComponent<Collider>();
						this.boundsDoubleDoor = (gameObject.transform.FindChild("Skeleton").FindChild("Hinge") == null);
					}
					else
					{
						collider = gameObject.GetComponentInChildren<Collider>();
					}
					if (collider != null)
					{
						this.boundsUse = true;
						this.boundsCenter = gameObject.transform.InverseTransformPoint(collider.bounds.center);
						this.boundsExtents = collider.bounds.extents;
					}
					UnityEngine.Object.Destroy(gameObject);
				}
				this.boundsOverlap = this.boundsExtents + new Vector3(0.5f, 0.5f, 0.5f);
			}
			if (base.channel.isOwner)
			{
				this.help = BarricadeTool.getBarricade(null, 0, Vector3.zero, Quaternion.identity, base.player.equipment.itemID, base.player.equipment.state);
				this.guide = this.help.FindChild("Root");
				if (this.guide == null)
				{
					this.guide = this.help;
				}
				HighlighterTool.help(this.guide, this.isValid, ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SENTRY);
				this.arrow = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Build/Arrow"))).transform;
				this.arrow.name = "Arrow";
				this.arrow.parent = this.help;
				this.arrow.localPosition = Vector3.zero;
				if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.DOOR || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.GATE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SHUTTER || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.HATCH)
				{
					this.arrow.localRotation = Quaternion.identity;
				}
				else
				{
					this.arrow.localRotation = Quaternion.Euler(90f, 0f, 0f);
				}
				Collider collider2;
				if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.DOOR || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.GATE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SHUTTER)
				{
					collider2 = this.help.FindChild("Placeholder").GetComponent<Collider>();
					this.boundsDoubleDoor = (this.help.FindChild("Skeleton").FindChild("Hinge") == null);
				}
				else
				{
					collider2 = this.help.GetComponentInChildren<Collider>();
				}
				if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.MANNEQUIN)
				{
					this.boundsUse = true;
					this.boundsCenter = new Vector3(0f, 0f, -0.05f);
					this.boundsExtents = new Vector3(1.175f, 0.2f, 1.05f);
					if (collider2 != null)
					{
						UnityEngine.Object.Destroy(collider2);
					}
				}
				else if (collider2 != null)
				{
					this.boundsUse = true;
					this.boundsCenter = this.help.InverseTransformPoint(collider2.bounds.center);
					this.boundsExtents = collider2.bounds.extents;
					UnityEngine.Object.Destroy(collider2);
				}
				this.boundsOverlap = this.boundsExtents + new Vector3(0.5f, 0.5f, 0.5f);
				if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.GLASS)
				{
					WaterHeightTransparentSort componentInChildren = this.help.GetComponentInChildren<WaterHeightTransparentSort>(true);
					if (componentInChildren != null)
					{
						UnityEngine.Object.Destroy(componentInChildren);
					}
				}
				HighlighterTool.help(this.arrow, this.isValid);
				if (this.help.FindChild("Radius") != null)
				{
					this.isPower = true;
					this.powerPoint = Vector3.zero;
					this.claimsInRadius = new List<InteractableClaim>();
					this.generatorsInRadius = new List<InteractableGenerator>();
					this.safezonesInRadius = new List<InteractableSafezone>();
					this.oxygenatorsInRadius = new List<InteractableOxygenator>();
					if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.CLAIM || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.GENERATOR || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SAFEZONE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.OXYGENATOR)
					{
						this.help.FindChild("Radius").gameObject.SetActive(true);
					}
				}
				Interactable component = this.help.GetComponent<Interactable>();
				if (component != null)
				{
					UnityEngine.Object.Destroy(component);
				}
				if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.MANNEQUIN)
				{
					this.help.GetComponentsInChildren<Collider>(true, UseableBarricade.colliders);
					for (int i = 0; i < UseableBarricade.colliders.Count; i++)
					{
						UnityEngine.Object.Destroy(UseableBarricade.colliders[i]);
					}
				}
				if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SPIKE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.WIRE)
				{
					UnityEngine.Object.Destroy(this.help.FindChild("Trap").GetComponent<InteractableTrap>());
				}
				if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.BEACON)
				{
					UnityEngine.Object.Destroy(this.help.GetComponent<InteractableBeacon>());
				}
				if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.DOOR || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.GATE || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SHUTTER || ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.HATCH)
				{
					if (this.help.FindChild("Placeholder") != null)
					{
						UnityEngine.Object.Destroy(this.help.FindChild("Placeholder").gameObject);
					}
					List<InteractableDoorHinge> list = new List<InteractableDoorHinge>();
					this.help.GetComponentsInChildren<InteractableDoorHinge>(list);
					for (int j = 0; j < list.Count; j++)
					{
						InteractableDoorHinge interactableDoorHinge = list[j];
						if (interactableDoorHinge.transform.FindChild("Clip") != null)
						{
							UnityEngine.Object.Destroy(interactableDoorHinge.transform.FindChild("Clip").gameObject);
						}
						if (interactableDoorHinge.transform.FindChild("Nav") != null)
						{
							UnityEngine.Object.Destroy(interactableDoorHinge.transform.FindChild("Nav").gameObject);
						}
						UnityEngine.Object.Destroy(interactableDoorHinge.transform.GetComponent<Collider>());
						UnityEngine.Object.Destroy(interactableDoorHinge);
					}
				}
				else
				{
					if (this.help.FindChild("Clip") != null)
					{
						UnityEngine.Object.Destroy(this.help.FindChild("Clip").gameObject);
					}
					if (this.help.FindChild("Nav") != null)
					{
						UnityEngine.Object.Destroy(this.help.FindChild("Nav").gameObject);
					}
					if (this.help.FindChild("Ladder") != null)
					{
						UnityEngine.Object.Destroy(this.help.FindChild("Ladder").gameObject);
					}
					if (this.help.FindChild("Block") != null)
					{
						UnityEngine.Object.Destroy(this.help.FindChild("Block").gameObject);
					}
				}
				for (int k = 0; k < 2; k++)
				{
					if (!(this.help.FindChild("Climb") != null))
					{
						break;
					}
					UnityEngine.Object.Destroy(this.help.FindChild("Climb").gameObject);
				}
			}
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x001A6D20 File Offset: 0x001A5120
		public override void dequip()
		{
			base.player.look.isIgnoringInput = false;
			this.isRotating = false;
			if (base.channel.isOwner)
			{
				if (this.help != null)
				{
					UnityEngine.Object.Destroy(this.help.gameObject);
				}
				if (this.isPower)
				{
					for (int i = 0; i < this.claimsInRadius.Count; i++)
					{
						if (!(this.claimsInRadius[i] == null))
						{
							this.claimsInRadius[i].transform.FindChild("Radius").gameObject.SetActive(false);
						}
					}
					this.claimsInRadius.Clear();
					for (int j = 0; j < this.generatorsInRadius.Count; j++)
					{
						if (!(this.generatorsInRadius[j] == null))
						{
							this.generatorsInRadius[j].transform.FindChild("Radius").gameObject.SetActive(false);
						}
					}
					this.generatorsInRadius.Clear();
					for (int k = 0; k < this.safezonesInRadius.Count; k++)
					{
						if (!(this.safezonesInRadius[k] == null))
						{
							this.safezonesInRadius[k].transform.FindChild("Radius").gameObject.SetActive(false);
						}
					}
					this.safezonesInRadius.Clear();
					for (int l = 0; l < this.oxygenatorsInRadius.Count; l++)
					{
						if (!(this.oxygenatorsInRadius[l] == null))
						{
							this.oxygenatorsInRadius[l].transform.FindChild("Radius").gameObject.SetActive(false);
						}
					}
					this.oxygenatorsInRadius.Clear();
				}
			}
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x001A6F28 File Offset: 0x001A5328
		public override void simulate(uint simulation, bool inputSteady)
		{
			if (this.isUsing && this.isUseable)
			{
				base.player.equipment.isBusy = false;
				if (Provider.isServer)
				{
					if (this.boundsUse && Physics.OverlapBoxNonAlloc(this.point + this.boundsRotation * this.boundsCenter, this.boundsOverlap, UseableBarricade.checkColliders, this.boundsRotation, RayMasks.BLOCK_CHAR_BUILDABLE_OVERLAP, QueryTriggerInteraction.Collide) > 0)
					{
						base.player.equipment.dequip();
					}
					else if (this.parentVehicle != null && this.parentVehicle.isGoingToRespawn)
					{
						base.player.equipment.dequip();
					}
					else
					{
						ItemBarricadeAsset itemBarricadeAsset = (ItemBarricadeAsset)base.player.equipment.asset;
						if (itemBarricadeAsset != null)
						{
							base.player.sendStat(EPlayerStat.FOUND_BUILDABLES);
							if (itemBarricadeAsset.build == EBuild.VEHICLE)
							{
								VehicleManager.spawnVehicle(itemBarricadeAsset.explosion, this.point, Quaternion.Euler(this.angle_x + this.rotate_x, this.angle_y + this.rotate_y, this.angle_z + this.rotate_z));
							}
							else
							{
								Barricade barricade = new Barricade(base.player.equipment.itemID, itemBarricadeAsset.health, itemBarricadeAsset.getState(), itemBarricadeAsset);
								if (itemBarricadeAsset.build == EBuild.DOOR || itemBarricadeAsset.build == EBuild.GATE || itemBarricadeAsset.build == EBuild.SHUTTER || itemBarricadeAsset.build == EBuild.SIGN || itemBarricadeAsset.build == EBuild.SIGN_WALL || itemBarricadeAsset.build == EBuild.NOTE || itemBarricadeAsset.build == EBuild.HATCH)
								{
									BitConverter.GetBytes(base.channel.owner.playerID.steamID.m_SteamID).CopyTo(barricade.state, 0);
									BitConverter.GetBytes(base.player.quests.groupID.m_SteamID).CopyTo(barricade.state, 8);
								}
								else if (itemBarricadeAsset.build == EBuild.BED)
								{
									BitConverter.GetBytes(CSteamID.Nil.m_SteamID).CopyTo(barricade.state, 0);
								}
								else if (itemBarricadeAsset.build == EBuild.STORAGE || itemBarricadeAsset.build == EBuild.STORAGE_WALL || itemBarricadeAsset.build == EBuild.MANNEQUIN || itemBarricadeAsset.build == EBuild.SENTRY || itemBarricadeAsset.build == EBuild.LIBRARY || itemBarricadeAsset.build == EBuild.MANNEQUIN)
								{
									BitConverter.GetBytes(base.channel.owner.playerID.steamID.m_SteamID).CopyTo(barricade.state, 0);
									BitConverter.GetBytes(base.player.quests.groupID.m_SteamID).CopyTo(barricade.state, 8);
								}
								else if (itemBarricadeAsset.build == EBuild.FARM)
								{
									BitConverter.GetBytes(Provider.time - (uint)(((ItemFarmAsset)base.player.equipment.asset).growth * (base.player.skills.mastery(2, 5) * 0.25f))).CopyTo(barricade.state, 0);
								}
								else if (itemBarricadeAsset.build == EBuild.TORCH || itemBarricadeAsset.build == EBuild.CAMPFIRE || itemBarricadeAsset.build == EBuild.OVEN || itemBarricadeAsset.build == EBuild.SPOT || itemBarricadeAsset.build == EBuild.SAFEZONE || itemBarricadeAsset.build == EBuild.OXYGENATOR || itemBarricadeAsset.build == EBuild.CAGE)
								{
									barricade.state[0] = 1;
								}
								else if (itemBarricadeAsset.build == EBuild.GENERATOR)
								{
									barricade.state[0] = 1;
								}
								else if (itemBarricadeAsset.build == EBuild.STEREO)
								{
									barricade.state[16] = 100;
								}
								BarricadeManager.dropBarricade(barricade, this.parent, this.point, this.angle_x + this.rotate_x, this.angle_y + this.rotate_y, this.angle_z + this.rotate_z, base.channel.owner.playerID.steamID.m_SteamID, base.player.quests.groupID.m_SteamID);
							}
						}
						base.player.equipment.use();
					}
				}
			}
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x001A73A0 File Offset: 0x001A57A0
		public override void tick()
		{
			if (this.isBuilding && this.isBuildable)
			{
				this.isBuilding = false;
				if (!Dedicator.isDedicated)
				{
					base.player.playSound(((ItemBarricadeAsset)base.player.equipment.asset).use);
				}
				if (Provider.isServer)
				{
					AlertTool.alert(base.transform.position, 8f);
				}
			}
			if (base.channel.isOwner)
			{
				if (this.help == null)
				{
					return;
				}
				if (this.isUsing)
				{
					return;
				}
				if (this.isRotating)
				{
					if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.FREEFORM)
					{
						if (ControlsSettings.invert)
						{
							this.input_x += ControlsSettings.look * 2f * Input.GetAxis("mouse_y");
						}
						else
						{
							this.input_x -= ControlsSettings.look * 2f * Input.GetAxis("mouse_y");
						}
					}
					this.input_y += ControlsSettings.look * 2f * Input.GetAxis("mouse_x");
					if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.FREEFORM)
					{
						this.input_z += ControlsSettings.look * 30f * Input.GetAxis("mouse_z");
					}
					if (Input.GetKey(ControlsSettings.snap))
					{
						this.rotate_x = (float)((int)(this.input_x / 15f)) * 15f;
						this.rotate_y = (float)((int)(this.input_y / 15f)) * 15f;
						this.rotate_z = (float)((int)(this.input_z / 15f)) * 15f;
					}
					else
					{
						this.rotate_x = this.input_x;
						this.rotate_y = this.input_y;
						this.rotate_z = this.input_z;
					}
				}
				if (this.check())
				{
					if (!this.isValid)
					{
						this.isValid = true;
						HighlighterTool.help(this.guide, this.isValid, ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SENTRY);
						if (this.arrow != null)
						{
							HighlighterTool.help(this.arrow, this.isValid);
						}
					}
				}
				else if (this.isValid)
				{
					this.isValid = false;
					HighlighterTool.help(this.guide, this.isValid, ((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SENTRY);
					if (this.arrow != null)
					{
						HighlighterTool.help(this.arrow, this.isValid);
					}
				}
				if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.VEHICLE)
				{
					this.parent = null;
				}
				else if (this.hit.transform != null && this.hit.transform.parent != null && this.hit.transform.parent.parent != null && this.hit.transform.parent.parent.CompareTag("Vehicle"))
				{
					this.parent = this.hit.transform.parent.parent;
				}
				else if (this.hit.transform != null && this.hit.transform.parent != null && this.hit.transform.parent.CompareTag("Vehicle"))
				{
					this.parent = this.hit.transform.parent;
				}
				else if (this.hit.transform != null && this.hit.transform.CompareTag("Vehicle"))
				{
					this.parent = this.hit.transform;
				}
				else
				{
					this.parent = null;
				}
				if (this.parent != null)
				{
					this.parentVehicle = DamageTool.getVehicle(this.parent);
				}
				else
				{
					this.parentVehicle = null;
				}
				bool flag = this.help.parent != this.parent;
				if (flag)
				{
					this.help.parent = this.parent;
					this.help.gameObject.SetActive(false);
					this.help.gameObject.SetActive(true);
				}
				if (this.parent != null)
				{
					this.help.localPosition = this.parent.InverseTransformPoint(this.point);
					this.help.localRotation = Quaternion.Euler(0f, this.angle_y + this.rotate_y - this.parent.localRotation.eulerAngles.y, 0f);
					this.help.localRotation *= Quaternion.Euler((float)((((ItemBarricadeAsset)base.player.equipment.asset).build != EBuild.DOOR && ((ItemBarricadeAsset)base.player.equipment.asset).build != EBuild.GATE && ((ItemBarricadeAsset)base.player.equipment.asset).build != EBuild.SHUTTER && ((ItemBarricadeAsset)base.player.equipment.asset).build != EBuild.HATCH) ? -90 : 0) + this.angle_x + this.rotate_x, 0f, 0f);
					this.help.localRotation *= Quaternion.Euler(0f, this.angle_z + this.rotate_z, 0f);
				}
				else
				{
					this.help.position = this.point;
					this.help.rotation = Quaternion.Euler(0f, this.angle_y + this.rotate_y, 0f);
					this.help.rotation *= Quaternion.Euler((float)((((ItemBarricadeAsset)base.player.equipment.asset).build != EBuild.DOOR && ((ItemBarricadeAsset)base.player.equipment.asset).build != EBuild.GATE && ((ItemBarricadeAsset)base.player.equipment.asset).build != EBuild.SHUTTER && ((ItemBarricadeAsset)base.player.equipment.asset).build != EBuild.HATCH) ? -90 : 0) + this.angle_x + this.rotate_x, 0f, 0f);
					this.help.rotation *= Quaternion.Euler(0f, this.angle_z + this.rotate_z, 0f);
				}
				if (this.isPower)
				{
					bool flag2 = flag;
					if ((base.transform.position - this.powerPoint).sqrMagnitude > 1f)
					{
						this.powerPoint = base.transform.position;
						flag2 = true;
					}
					if (flag2)
					{
						for (int i = 0; i < this.claimsInRadius.Count; i++)
						{
							if (!(this.claimsInRadius[i] == null))
							{
								this.claimsInRadius[i].transform.FindChild("Radius").gameObject.SetActive(false);
							}
						}
						this.claimsInRadius.Clear();
						for (int j = 0; j < this.generatorsInRadius.Count; j++)
						{
							if (!(this.generatorsInRadius[j] == null))
							{
								this.generatorsInRadius[j].transform.FindChild("Radius").gameObject.SetActive(false);
							}
						}
						this.generatorsInRadius.Clear();
						for (int k = 0; k < this.safezonesInRadius.Count; k++)
						{
							if (!(this.safezonesInRadius[k] == null))
							{
								this.safezonesInRadius[k].transform.FindChild("Radius").gameObject.SetActive(false);
							}
						}
						this.safezonesInRadius.Clear();
						for (int l = 0; l < this.oxygenatorsInRadius.Count; l++)
						{
							if (!(this.oxygenatorsInRadius[l] == null))
							{
								this.oxygenatorsInRadius[l].transform.FindChild("Radius").gameObject.SetActive(false);
							}
						}
						this.oxygenatorsInRadius.Clear();
						byte b;
						byte b2;
						ushort plant;
						BarricadeRegion barricadeRegion;
						BarricadeManager.tryGetPlant(this.parent, out b, out b2, out plant, out barricadeRegion);
						if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.CLAIM)
						{
							PowerTool.checkInteractables<InteractableClaim>(this.powerPoint, 64f, plant, this.claimsInRadius);
							for (int m = 0; m < this.claimsInRadius.Count; m++)
							{
								if (!(this.claimsInRadius[m] == null))
								{
									this.claimsInRadius[m].transform.FindChild("Radius").gameObject.SetActive(true);
								}
							}
						}
						else
						{
							PowerTool.checkInteractables<InteractableGenerator>(this.powerPoint, 64f, plant, this.generatorsInRadius);
							for (int n = 0; n < this.generatorsInRadius.Count; n++)
							{
								if (!(this.generatorsInRadius[n] == null))
								{
									this.generatorsInRadius[n].transform.FindChild("Radius").gameObject.SetActive(true);
								}
							}
						}
						if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.SAFEZONE)
						{
							PowerTool.checkInteractables<InteractableSafezone>(this.powerPoint, 64f, plant, this.safezonesInRadius);
							for (int num = 0; num < this.safezonesInRadius.Count; num++)
							{
								if (!(this.safezonesInRadius[num] == null))
								{
									this.safezonesInRadius[num].transform.FindChild("Radius").gameObject.SetActive(true);
								}
							}
						}
						if (((ItemBarricadeAsset)base.player.equipment.asset).build == EBuild.OXYGENATOR)
						{
							PowerTool.checkInteractables<InteractableOxygenator>(this.powerPoint, 64f, plant, this.oxygenatorsInRadius);
							for (int num2 = 0; num2 < this.oxygenatorsInRadius.Count; num2++)
							{
								if (!(this.oxygenatorsInRadius[num2] == null))
								{
									this.oxygenatorsInRadius[num2].transform.FindChild("Radius").gameObject.SetActive(true);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x04002BFB RID: 11259
		private static List<Collider> colliders = new List<Collider>();

		// Token: 0x04002BFC RID: 11260
		private static Collider[] checkColliders = new Collider[1];

		// Token: 0x04002BFD RID: 11261
		private Transform parent;

		// Token: 0x04002BFE RID: 11262
		private Transform help;

		// Token: 0x04002BFF RID: 11263
		private Transform guide;

		// Token: 0x04002C00 RID: 11264
		private Transform arrow;

		// Token: 0x04002C01 RID: 11265
		private InteractableVehicle parentVehicle;

		// Token: 0x04002C02 RID: 11266
		private bool boundsUse;

		// Token: 0x04002C03 RID: 11267
		private bool boundsDoubleDoor;

		// Token: 0x04002C04 RID: 11268
		private Vector3 boundsCenter;

		// Token: 0x04002C05 RID: 11269
		private Vector3 boundsExtents;

		// Token: 0x04002C06 RID: 11270
		private Vector3 boundsOverlap;

		// Token: 0x04002C07 RID: 11271
		private Quaternion boundsRotation;

		// Token: 0x04002C08 RID: 11272
		private float startedUse;

		// Token: 0x04002C09 RID: 11273
		private float useTime;

		// Token: 0x04002C0A RID: 11274
		private bool isRotating;

		// Token: 0x04002C0B RID: 11275
		private bool isBuilding;

		// Token: 0x04002C0C RID: 11276
		private bool isUsing;

		// Token: 0x04002C0D RID: 11277
		private bool isValid;

		// Token: 0x04002C0E RID: 11278
		private bool wasAsked;

		// Token: 0x04002C0F RID: 11279
		private RaycastHit hit;

		// Token: 0x04002C10 RID: 11280
		private Vector3 point;

		// Token: 0x04002C11 RID: 11281
		private float angle_x;

		// Token: 0x04002C12 RID: 11282
		private float angle_y;

		// Token: 0x04002C13 RID: 11283
		private float angle_z;

		// Token: 0x04002C14 RID: 11284
		private float rotate_x;

		// Token: 0x04002C15 RID: 11285
		private float rotate_y;

		// Token: 0x04002C16 RID: 11286
		private float rotate_z;

		// Token: 0x04002C17 RID: 11287
		private float input_x;

		// Token: 0x04002C18 RID: 11288
		private float input_y;

		// Token: 0x04002C19 RID: 11289
		private float input_z;

		// Token: 0x04002C1A RID: 11290
		private bool isPower;

		// Token: 0x04002C1B RID: 11291
		private Vector3 powerPoint;

		// Token: 0x04002C1C RID: 11292
		private List<InteractableClaim> claimsInRadius;

		// Token: 0x04002C1D RID: 11293
		private List<InteractableGenerator> generatorsInRadius;

		// Token: 0x04002C1E RID: 11294
		private List<InteractableSafezone> safezonesInRadius;

		// Token: 0x04002C1F RID: 11295
		private List<InteractableOxygenator> oxygenatorsInRadius;
	}
}
