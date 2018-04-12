using System;
using System.Collections.Generic;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.Landscapes;
using SDG.Framework.Water;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000634 RID: 1588
	public class PlayerMovement : PlayerCaller, ILandscaleHoleVolumeInteractionHandler
	{
		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06002D34 RID: 11572 RVA: 0x001220F6 File Offset: 0x001204F6
		// (set) Token: 0x06002D35 RID: 11573 RVA: 0x001220FD File Offset: 0x001204FD
		[TerminalCommandProperty("misc.dont_enable_this", "seriously, don't enable this unless Nelson suggested it", false)]
		public static bool forceTrustClient
		{
			get
			{
				return PlayerMovement._forceTrustClient;
			}
			set
			{
				PlayerMovement._forceTrustClient = value;
				TerminalUtility.printCommandPass("Set dont_enable_this to: " + PlayerMovement.forceTrustClient);
			}
		}

		// Token: 0x14000082 RID: 130
		// (add) Token: 0x06002D36 RID: 11574 RVA: 0x00122120 File Offset: 0x00120520
		// (remove) Token: 0x06002D37 RID: 11575 RVA: 0x00122158 File Offset: 0x00120558
		public event PlayerNavChanged PlayerNavChanged;

		// Token: 0x06002D38 RID: 11576 RVA: 0x0012218E File Offset: 0x0012058E
		private void TriggerPlayerNavChanged(byte oldNav, byte newNav)
		{
			if (this.PlayerNavChanged == null)
			{
				return;
			}
			this.PlayerNavChanged(this, oldNav, newNav);
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06002D39 RID: 11577 RVA: 0x001221AA File Offset: 0x001205AA
		public float totalGravityMultiplier
		{
			get
			{
				return this.itemGravityMultiplier * this.pluginGravityMultiplier;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06002D3A RID: 11578 RVA: 0x001221B9 File Offset: 0x001205B9
		// (set) Token: 0x06002D3B RID: 11579 RVA: 0x001221C1 File Offset: 0x001205C1
		public LandscapeHoleVolume landscapeHoleVolume { get; protected set; }

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06002D3C RID: 11580 RVA: 0x001221CA File Offset: 0x001205CA
		public bool landscapeHoleAutoIgnoreTerrainCollision
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x001221CD File Offset: 0x001205CD
		public void landscapeHoleBeginCollision(LandscapeHoleVolume volume, List<TerrainCollider> terrainColliders)
		{
			this.landscapeHoleVolume = volume;
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x001221D6 File Offset: 0x001205D6
		public void landscapeHoleEndCollision(LandscapeHoleVolume volume, List<TerrainCollider> terrainColliders)
		{
			if (volume == this.landscapeHoleVolume)
			{
				this.landscapeHoleVolume = null;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06002D3F RID: 11583 RVA: 0x001221F0 File Offset: 0x001205F0
		public bool isGrounded
		{
			get
			{
				return this._isGrounded;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06002D40 RID: 11584 RVA: 0x001221F8 File Offset: 0x001205F8
		// (set) Token: 0x06002D41 RID: 11585 RVA: 0x00122200 File Offset: 0x00120600
		public bool isSafe
		{
			get
			{
				return this._isSafe;
			}
			set
			{
				this._isSafe = value;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06002D42 RID: 11586 RVA: 0x00122209 File Offset: 0x00120609
		// (set) Token: 0x06002D43 RID: 11587 RVA: 0x00122211 File Offset: 0x00120611
		public bool isRadiated
		{
			get
			{
				return this._isRadiated;
			}
			set
			{
				this._isRadiated = value;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06002D44 RID: 11588 RVA: 0x0012221A File Offset: 0x0012061A
		// (set) Token: 0x06002D45 RID: 11589 RVA: 0x00122222 File Offset: 0x00120622
		public PurchaseNode purchaseNode
		{
			get
			{
				return this._purchaseNode;
			}
			set
			{
				this._purchaseNode = value;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06002D46 RID: 11590 RVA: 0x0012222B File Offset: 0x0012062B
		// (set) Token: 0x06002D47 RID: 11591 RVA: 0x00122233 File Offset: 0x00120633
		public IAmbianceNode effectNode { get; private set; }

		// Token: 0x06002D48 RID: 11592 RVA: 0x0012223C File Offset: 0x0012063C
		public void setSize(EPlayerHeight newHeight)
		{
			if (newHeight == this.height)
			{
				return;
			}
			this.height = newHeight;
			this.applySize();
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x00122258 File Offset: 0x00120658
		private void applySize()
		{
			float num;
			switch (this.height)
			{
			case EPlayerHeight.STAND:
				num = PlayerMovement.HEIGHT_STAND;
				break;
			case EPlayerHeight.CROUCH:
				num = PlayerMovement.HEIGHT_CROUCH;
				break;
			case EPlayerHeight.PRONE:
				num = PlayerMovement.HEIGHT_PRONE;
				break;
			default:
				num = 2f;
				break;
			}
			if ((base.channel.isOwner || Provider.isServer) && this.controller != null)
			{
				this.controller.height = num;
				this.controller.center = new Vector3(0f, num / 2f, 0f);
				if (this.wasSizeAppliedYet)
				{
					base.transform.localPosition += new Vector3(0f, 0.02f, 0f);
				}
				this.wasSizeAppliedYet = true;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06002D4A RID: 11594 RVA: 0x00122343 File Offset: 0x00120743
		public bool isMoving
		{
			get
			{
				return this._isMoving;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06002D4B RID: 11595 RVA: 0x0012234C File Offset: 0x0012074C
		public float speed
		{
			get
			{
				if (base.player.stance.stance == EPlayerStance.SWIM)
				{
					return PlayerMovement.SPEED_SWIM * (1f + base.player.skills.mastery(0, 5) * 0.25f) * this._multiplier;
				}
				float num = 1f + base.player.skills.mastery(0, 4) * 0.25f;
				if (base.player.stance.stance == EPlayerStance.CLIMB)
				{
					return PlayerMovement.SPEED_CLIMB * num * this._multiplier;
				}
				if (base.player.stance.stance == EPlayerStance.SPRINT)
				{
					return PlayerMovement.SPEED_SPRINT * num * this._multiplier;
				}
				if (base.player.stance.stance == EPlayerStance.STAND)
				{
					return PlayerMovement.SPEED_STAND * num * this._multiplier;
				}
				if (base.player.stance.stance == EPlayerStance.CROUCH)
				{
					return PlayerMovement.SPEED_CROUCH * num * this._multiplier;
				}
				if (base.player.stance.stance == EPlayerStance.PRONE)
				{
					return PlayerMovement.SPEED_PRONE * num * this._multiplier;
				}
				return 0f;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06002D4C RID: 11596 RVA: 0x00122477 File Offset: 0x00120877
		public Vector3 move
		{
			get
			{
				return this._move;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06002D4D RID: 11597 RVA: 0x0012247F File Offset: 0x0012087F
		public byte region_x
		{
			get
			{
				return this._region_x;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06002D4E RID: 11598 RVA: 0x00122487 File Offset: 0x00120887
		public byte region_y
		{
			get
			{
				return this._region_y;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06002D4F RID: 11599 RVA: 0x0012248F File Offset: 0x0012088F
		public byte bound
		{
			get
			{
				return this._bound;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06002D50 RID: 11600 RVA: 0x00122497 File Offset: 0x00120897
		public byte nav
		{
			get
			{
				return this._nav;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06002D51 RID: 11601 RVA: 0x0012249F File Offset: 0x0012089F
		public LoadedRegion[,] loadedRegions
		{
			get
			{
				return this._loadedRegions;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x001224A7 File Offset: 0x001208A7
		public LoadedBound[] loadedBounds
		{
			get
			{
				return this._loadedBounds;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002D53 RID: 11603 RVA: 0x001224AF File Offset: 0x001208AF
		// (set) Token: 0x06002D54 RID: 11604 RVA: 0x001224B7 File Offset: 0x001208B7
		public float fall { get; private set; }

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002D55 RID: 11605 RVA: 0x001224C0 File Offset: 0x001208C0
		public Vector3 real
		{
			get
			{
				return this._real;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002D56 RID: 11606 RVA: 0x001224C8 File Offset: 0x001208C8
		public byte horizontal
		{
			get
			{
				return this._horizontal;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x001224D0 File Offset: 0x001208D0
		public byte vertical
		{
			get
			{
				return this._vertical;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06002D58 RID: 11608 RVA: 0x001224D8 File Offset: 0x001208D8
		public bool jump
		{
			get
			{
				return this._jump;
			}
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x001224E0 File Offset: 0x001208E0
		public InteractableVehicle getVehicle()
		{
			return this.vehicle;
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x001224E8 File Offset: 0x001208E8
		public byte getSeat()
		{
			return this.seat;
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x001224F0 File Offset: 0x001208F0
		private void updateVehicle()
		{
			InteractableVehicle interactableVehicle = this.vehicle;
			this.vehicle = this.seatingVehicle;
			this.seat = this.seatingSeat;
			bool flag = this.vehicle != null && this.seat == 0;
			if (this.vehicle == null)
			{
				base.player.transform.parent = this.seatingTransform;
				base.player.askTeleport(Provider.server, this.seatingPosition, this.seatingAngle);
			}
			if (base.channel.isOwner)
			{
				bool flag2;
				if (flag && Level.info != null && Level.info.name.ToLower() != "tutorial" && Provider.provider.achievementsService.getAchievement("Wheel", out flag2) && !flag2)
				{
					Provider.provider.achievementsService.setAchievement("Wheel");
				}
				if (this.vehicle != null)
				{
					PlayerUI.disableDot();
					if (base.player.equipment.asset != null && base.player.equipment.asset.type == EItemType.GUN)
					{
						if (base.player.look.perspective == EPlayerPerspective.THIRD)
						{
							PlayerUI.disableCrosshair();
						}
						else
						{
							PlayerUI.enableCrosshair();
						}
					}
				}
				else if (base.player.equipment.asset != null && base.player.equipment.asset.type == EItemType.GUN)
				{
					PlayerUI.enableCrosshair();
				}
				else
				{
					PlayerUI.enableDot();
				}
			}
			if (base.channel.isOwner || Provider.isServer)
			{
				this.controller.enabled = (this.vehicle == null);
				if (this.vehicle != null)
				{
					if (flag)
					{
						base.player.stance.checkStance(EPlayerStance.DRIVING);
					}
					else
					{
						base.player.stance.checkStance(EPlayerStance.SITTING);
					}
				}
				else
				{
					base.player.stance.checkStance(EPlayerStance.STAND);
				}
			}
			if (base.channel.isOwner)
			{
				if (this.onSeated != null)
				{
					this.onSeated(flag, this.vehicle != null, interactableVehicle != null, interactableVehicle, this.vehicle);
				}
				if (flag && this.onVehicleUpdated != null)
				{
					ushort newFuel;
					ushort maxFuel;
					this.vehicle.getDisplayFuel(out newFuel, out maxFuel);
					this.onVehicleUpdated(!this.vehicle.isUnderwater && !this.vehicle.isDead, newFuel, maxFuel, this.vehicle.spedometer, this.vehicle.asset.speedMin, this.vehicle.asset.speedMax, this.vehicle.health, this.vehicle.asset.health, this.vehicle.batteryCharge);
				}
				if (this.vehicle != null)
				{
					if (flag)
					{
						if (interactableVehicle == null)
						{
							PlayerUI.message(EPlayerMessage.VEHICLE_EXIT, string.Empty);
						}
						else
						{
							PlayerUI.message(EPlayerMessage.VEHICLE_SWAP, string.Empty);
						}
					}
					else
					{
						PlayerUI.message(EPlayerMessage.VEHICLE_SWAP, string.Empty);
					}
				}
			}
			if (this.vehicle != null)
			{
				base.player.transform.parent = this.seatingTransform;
				base.player.transform.localPosition = this.seatingPosition;
				base.player.transform.localRotation = Quaternion.identity;
				base.player.look.updateLook();
				if ((base.channel.isOwner || Provider.isServer) && this.landscapeHoleVolume != null)
				{
					this.landscapeHoleVolume.endCollision(this.controller);
				}
			}
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x001228F4 File Offset: 0x00120CF4
		public void setVehicle(InteractableVehicle newVehicle, byte newSeat, Transform newSeatingTransform, Vector3 newSeatingPosition, byte newSeatingAngle, bool forceUpdate)
		{
			this.isSeating = true;
			this.seatingVehicle = newVehicle;
			this.seatingSeat = newSeat;
			this.seatingTransform = newSeatingTransform;
			this.seatingPosition = newSeatingPosition;
			this.seatingAngle = newSeatingAngle;
			if ((base.channel.isOwner || Provider.isServer) && !base.player.life.isDead && !forceUpdate)
			{
				return;
			}
			this.updateVehicle();
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x0012296A File Offset: 0x00120D6A
		[SteamCall]
		public void tellPluginGravityMultiplier(CSteamID steamID, float newPluginGravityMultiplier)
		{
			if (!base.channel.checkServer(steamID))
			{
				return;
			}
			this.pluginGravityMultiplier = newPluginGravityMultiplier;
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x00122985 File Offset: 0x00120D85
		public void sendPluginGravityMultiplier(float newPluginGravityMultiplier)
		{
			this.pluginGravityMultiplier = newPluginGravityMultiplier;
			if (!base.channel.isOwner)
			{
				base.channel.send("tellPluginGravityMultiplier", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					newPluginGravityMultiplier
				});
			}
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x001229C0 File Offset: 0x00120DC0
		[SteamCall]
		public void tellRecov(CSteamID steamID, Vector3 newPosition, int newRecov)
		{
			if (base.channel.checkServer(steamID))
			{
				if (base.player.stance.stance == EPlayerStance.DRIVING || base.player.stance.stance == EPlayerStance.SITTING)
				{
					return;
				}
				this._real = newPosition;
				this.lastUpdatePos = base.transform.position;
				if (this.nsb != null)
				{
					this.nsb.updateLastSnapshot(new PitchYawSnapshotInfo(this.lastUpdatePos, base.player.look.pitch, base.player.look.yaw));
				}
				base.transform.localPosition = newPosition;
				this.isFrozen = true;
				base.player.input.recov = newRecov;
			}
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x00122A90 File Offset: 0x00120E90
		public void tellState(Vector3 newPosition, byte newPitch, byte newYaw)
		{
			if (base.channel.isOwner)
			{
				return;
			}
			this.checkGround(newPosition);
			this.lastUpdatePos = newPosition;
			if (this.nsb != null)
			{
				this.nsb.addNewSnapshot(new PitchYawSnapshotInfo(newPosition, (float)newPitch, (float)newYaw * 2f));
			}
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x00122AE8 File Offset: 0x00120EE8
		public void updateMovement()
		{
			this.lastUpdatePos = base.transform.localPosition;
			if (this.nsb != null)
			{
				this.nsb.updateLastSnapshot(new PitchYawSnapshotInfo(this.lastUpdatePos, base.player.look.pitch, base.player.look.yaw));
			}
			this._real = base.transform.position;
			if (base.channel.isOwner || Provider.isServer)
			{
				this.isRecovering = false;
				this.lastRecover = Time.realtimeSinceStartup;
				this.isFrozen = false;
			}
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x00122B90 File Offset: 0x00120F90
		private void checkGround(Vector3 position)
		{
			this.material = EPhysicsMaterial.NONE;
			int num = RayMasks.BLOCK_COLLISION;
			LandscapeHoleVolume landscapeHoleVolume;
			if (LandscapeHoleUtility.isPointInsideHoleVolume(position, out landscapeHoleVolume))
			{
				num &= ~RayMasks.GROUND;
			}
			Physics.SphereCast(position + new Vector3(0f, 0.45f, 0f), 0.45f, Vector3.down, out this.ground, 0.125f, num, QueryTriggerInteraction.Ignore);
			this._isGrounded = (this.ground.transform != null);
			if ((base.channel.isOwner || Provider.isServer) && this.controller.isGrounded)
			{
				this._isGrounded = true;
			}
			if (base.player.stance.stance == EPlayerStance.CLIMB || base.player.stance.stance == EPlayerStance.SWIM)
			{
				this._isGrounded = true;
			}
			if (base.player.stance.stance == EPlayerStance.CLIMB)
			{
				this.material = EPhysicsMaterial.TILE_STATIC;
			}
			else if (base.player.stance.stance == EPlayerStance.SWIM || WaterUtility.isPointUnderwater(base.transform.position))
			{
				this.material = EPhysicsMaterial.WATER_STATIC;
			}
			else if (this.ground.transform != null)
			{
				if (this.ground.transform.CompareTag("Ground"))
				{
					this.material = PhysicsTool.checkMaterial(base.transform.position);
				}
				else
				{
					this.material = PhysicsTool.checkMaterial(this.ground.collider);
				}
			}
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x00122D2C File Offset: 0x0012112C
		private void onVisionUpdated(bool isViewing)
		{
			if (isViewing)
			{
				this.warp_x = (((double)UnityEngine.Random.value >= 0.25) ? 1 : -1);
				this.warp_y = (((double)UnityEngine.Random.value >= 0.25) ? 1 : -1);
			}
			else
			{
				this.warp_x = 1;
				this.warp_y = 1;
			}
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x00122D94 File Offset: 0x00121194
		private void onLifeUpdated(bool isDead)
		{
			byte b;
			Vector3 point;
			byte angle;
			if (isDead && this.vehicle != null && this.vehicle.forceRemovePlayer(out b, base.channel.owner.playerID.steamID, out point, out angle))
			{
				VehicleManager.sendExitVehicle(this.vehicle, b, point, angle, false);
			}
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x00122DF4 File Offset: 0x001211F4
		public void simulate()
		{
			this.updateRegionAndBound();
			if (base.channel.isOwner)
			{
				this.lastUpdatePos = base.transform.position;
			}
			if (this.isSeating)
			{
				this.isSeating = false;
				this.updateVehicle();
				return;
			}
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x00122E44 File Offset: 0x00121244
		public void simulate(uint simulation, int recov, bool inputBrake, bool inputStamina, Vector3 point, float angle_x, float angle_y, float angle_z, float speed, float physicsSpeed, int turn, float delta)
		{
			this.updateRegionAndBound();
			if (base.channel.isOwner)
			{
				this.lastUpdatePos = base.transform.position;
			}
			if (this.isSeating)
			{
				this.isSeating = false;
				this.updateVehicle();
				return;
			}
			if (base.player.stance.stance == EPlayerStance.DRIVING)
			{
				this.fell = base.transform.position.y;
				if (this.vehicle != null)
				{
					this.vehicle.simulate(simulation, recov, inputStamina, point, Quaternion.Euler(angle_x, angle_y, angle_z), speed, physicsSpeed, turn, delta);
				}
			}
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x00122EF8 File Offset: 0x001212F8
		public void simulate(uint simulation, bool inputJump, Vector3 point, float delta)
		{
			this.updateRegionAndBound();
			if (base.channel.isOwner)
			{
				this.lastUpdatePos = base.transform.position;
			}
			if (this.isSeating)
			{
				this.isSeating = false;
				this.updateVehicle();
				return;
			}
			if (this.isAllowed)
			{
				if ((point - base.transform.position).sqrMagnitude > 1f)
				{
					return;
				}
				this.isAllowed = false;
				this.fell = base.transform.position.y;
			}
			if (this.isRecovering)
			{
				if ((point - this.real).sqrMagnitude > 0.01f)
				{
					if (Time.realtimeSinceStartup - this.lastRecover > 5f)
					{
						this.lastRecover = Time.realtimeSinceStartup;
						base.channel.send("tellRecov", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							this.real,
							base.player.input.recov
						});
					}
					return;
				}
				this.isRecovering = false;
				return;
			}
			else
			{
				if (this.isFrozen)
				{
					this.isFrozen = false;
					return;
				}
				if (base.player.stance.stance == EPlayerStance.SITTING)
				{
					this._isMoving = false;
					this.fell = base.transform.position.y;
					return;
				}
				if (base.player.stance.stance == EPlayerStance.DRIVING)
				{
					this._isMoving = false;
					return;
				}
				this._isMoving = ((point - base.transform.position).sqrMagnitude > 0.25f);
				this.checkGround(base.transform.position);
				if (base.player.stance.stance == EPlayerStance.CLIMB || base.player.stance.stance == EPlayerStance.SWIM)
				{
					this.fell = base.transform.position.y;
				}
				else if (this.lastGrounded != this.isGrounded)
				{
					this.lastGrounded = this.isGrounded;
					if (this.isGrounded && this.onLanded != null)
					{
						this.onLanded(base.transform.position.y - this.fell);
					}
					this.fell = base.transform.position.y;
				}
				if (inputJump && this.isGrounded && !base.player.life.isBroken && (float)base.player.life.stamina >= 10f * (1f - base.player.skills.mastery(0, 6) * 0.5f) && (base.player.stance.stance == EPlayerStance.STAND || base.player.stance.stance == EPlayerStance.SPRINT))
				{
					base.player.life.askTire((byte)(10f * (1f - base.player.skills.mastery(0, 6) * 0.5f)));
				}
				if (Mathf.Pow(point.x - this.real.x, 2f) + Mathf.Pow(point.z - this.real.z, 2f) > Mathf.Pow(this.speed * delta, 2f) + 1f)
				{
					this.isRecovering = true;
					this.lastRecover = Time.realtimeSinceStartup;
					base.channel.send("tellRecov", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						this.real,
						base.player.input.recov
					});
					return;
				}
				if (point.y < this.real.y)
				{
					if (point.y - this.real.y < Physics.gravity.y * delta - 0.1f)
					{
						this.isRecovering = true;
						this.lastRecover = Time.realtimeSinceStartup;
						base.channel.send("tellPosition", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							this.real
						});
						return;
					}
				}
				else if (point.y - this.real.y > 9f * delta + 0.1f)
				{
					this.isRecovering = true;
					this.lastRecover = Time.realtimeSinceStartup;
					base.channel.send("tellPosition", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						this.real
					});
					return;
				}
				base.GetComponent<Rigidbody>().MovePosition(point);
				this._real = point;
				return;
			}
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x001233FC File Offset: 0x001217FC
		public void simulate(uint simulation, int recov, int input_x, int input_y, float look_x, float look_y, bool inputJump, bool inputSprint, Vector3 target, float delta)
		{
			this.updateRegionAndBound();
			if (base.channel.isOwner)
			{
				this.lastUpdatePos = base.transform.position;
			}
			if (this.isSeating)
			{
				this.isSeating = false;
				this.updateVehicle();
				if (!base.channel.isOwner)
				{
					return;
				}
			}
			if (this.isAllowed)
			{
				if (base.channel.isOwner)
				{
					this.isAllowed = false;
					this.fell = base.transform.position.y;
					this.fall = 0f;
					this.fall2 = 0f;
					return;
				}
				if ((target - base.transform.position).sqrMagnitude > 0.01f)
				{
					return;
				}
				this.isAllowed = false;
				this.fell = base.transform.position.y;
				this.fall = 0f;
				this.fall2 = 0f;
				return;
			}
			else if (Provider.isServer && this.isRecovering)
			{
				if (recov < base.player.input.recov)
				{
					if (Time.realtimeSinceStartup - this.lastRecover > 5f)
					{
						this.lastRecover = Time.realtimeSinceStartup;
						base.channel.send("tellRecov", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							this.real,
							base.player.input.recov
						});
					}
					return;
				}
				this.isRecovering = false;
				this.fall = 0f;
				this.fall2 = 0f;
				return;
			}
			else
			{
				if (this.isFrozen)
				{
					this.isFrozen = false;
					this.fall = 0f;
					this.fall2 = 0f;
					return;
				}
				this._move.x = (float)input_x;
				this._move.z = (float)input_y;
				if (base.player.stance.stance == EPlayerStance.SITTING)
				{
					this._isMoving = false;
					this.checkGround(base.transform.position);
					this.fell = base.transform.position.y;
					this.fall2 = 0f;
					if (this.getVehicle() != null && this.getVehicle().passengers[(int)this.getSeat()].turret != null && (Mathf.Abs((int)(base.player.look.lastAngle - base.player.look.angle)) > 1 || Mathf.Abs((int)(base.player.look.lastRot - base.player.look.rot)) > 1))
					{
						base.player.look.lastAngle = base.player.look.angle;
						base.player.look.lastRot = base.player.look.rot;
						if (this.canAddSimulationResultsToUpdates)
						{
							this.updates.Add(new PlayerStateUpdate(this.real, base.player.look.angle, base.player.look.rot));
						}
					}
					return;
				}
				if (base.player.stance.stance == EPlayerStance.DRIVING)
				{
					this._isMoving = false;
					this.checkGround(base.transform.position);
					this.fell = base.transform.position.y;
					this.fall2 = 0f;
					if (base.channel.isOwner)
					{
						this.vehicle.simulate(simulation, recov, input_x, input_y, look_x, look_y, inputJump, inputSprint, delta);
						if (this.onVehicleUpdated != null)
						{
							ushort newFuel;
							ushort maxFuel;
							this.vehicle.getDisplayFuel(out newFuel, out maxFuel);
							this.onVehicleUpdated(!this.vehicle.isUnderwater && !this.vehicle.isDead, newFuel, maxFuel, this.vehicle.speed, this.vehicle.asset.speedMin, this.vehicle.asset.speedMax, this.vehicle.health, this.vehicle.asset.health, this.vehicle.batteryCharge);
						}
					}
					return;
				}
				if (base.player.stance.stance == EPlayerStance.CLIMB)
				{
					this.fall = PlayerMovement.JUMP;
					this._isMoving = ((double)Mathf.Abs(this.move.x) > 0.1 || (double)Mathf.Abs(this.move.z) > 0.1);
					this.checkGround(base.transform.position);
					this.fell = base.transform.position.y;
					this.fall2 = 0f;
					this.direction = this.move.normalized * this.speed / 2f;
					this.controller.Move(Vector3.up * this.direction.z * delta);
				}
				else if (base.player.stance.stance == EPlayerStance.SWIM)
				{
					this._isMoving = ((double)Mathf.Abs(this.move.x) > 0.1 || (double)Mathf.Abs(this.move.z) > 0.1);
					this.checkGround(base.transform.position);
					this.fell = base.transform.position.y;
					this.fall2 = 0f;
					this.direction = this.move.normalized * this.speed * 1.5f;
					if (base.player.stance.isSubmerged || (base.player.look.pitch > 110f && (double)this.move.z > 0.1))
					{
						this.fall += Physics.gravity.y * delta / 7f;
						if (this.fall < Physics.gravity.y / 7f)
						{
							this.fall = Physics.gravity.y / 7f;
						}
						if (inputJump)
						{
							this.fall = PlayerMovement.SWIM;
						}
						this.controller.Move(base.player.look.aim.rotation * this.direction * delta + Vector3.up * this.fall * delta);
					}
					else
					{
						bool flag;
						float num;
						WaterUtility.getUnderwaterInfo(base.transform.position, out flag, out num);
						this.fall = (num - 1.275f - base.transform.position.y) / 8f;
						this.controller.Move(base.transform.rotation * this.direction * delta + Vector3.up * this.fall * delta);
					}
				}
				else
				{
					if (!base.channel.isOwner || !Level.isLoading)
					{
						this.fall += Physics.gravity.y * ((this.fall > 0f) ? 1f : this.totalGravityMultiplier) * delta * 3f;
						if (this.fall < Physics.gravity.y * 2f * this.totalGravityMultiplier)
						{
							this.fall = Physics.gravity.y * 2f * this.totalGravityMultiplier;
						}
					}
					this._isMoving = ((double)Mathf.Abs(this.move.x) > 0.1 || (double)Mathf.Abs(this.move.z) > 0.1);
					this.checkGround(base.transform.position);
					if (this.lastGrounded != this.isGrounded)
					{
						this.lastGrounded = this.isGrounded;
						if (this.isGrounded && this.onLanded != null)
						{
							this.onLanded(base.transform.position.y - this.fell);
						}
						this.fell = base.transform.position.y;
					}
					if (inputJump && this.isGrounded && !base.player.life.isBroken && (float)base.player.life.stamina >= 10f * (1f - base.player.skills.mastery(0, 6) * 0.5f) && (base.player.stance.stance == EPlayerStance.STAND || base.player.stance.stance == EPlayerStance.SPRINT))
					{
						this.fall = PlayerMovement.JUMP * (1f + base.player.skills.mastery(0, 6) * 0.25f);
						base.player.life.askTire((byte)(10f * (1f - base.player.skills.mastery(0, 6) * 0.5f)));
					}
					if (this.isGrounded && this.ground.transform != null)
					{
						this.slope = Mathf.Lerp(this.slope, this.ground.normal.y, delta);
					}
					else
					{
						this.slope = Mathf.Lerp(this.slope, 1f, delta);
					}
					this._multiplier = Mathf.Lerp(this._multiplier, this.multiplier, delta);
					if (this.material == EPhysicsMaterial.ICE_STATIC)
					{
						this.direction = Vector3.Lerp(this.direction, base.transform.rotation * this.move.normalized * this.speed * this.slope * delta, delta);
					}
					else if (this.material == EPhysicsMaterial.METAL_SLIP)
					{
						float num2;
						if (this.slope < 0.75f)
						{
							num2 = 0f;
						}
						else
						{
							num2 = Mathf.Lerp(0f, 1f, (this.slope - 0.75f) * 4f);
						}
						this.direction = Vector3.Lerp(this.direction, base.transform.rotation * this.move.normalized * this.speed * this.slope * delta * 2f, (!this.isMoving) ? (0.5f * num2 * delta) : (2f * delta));
					}
					else
					{
						this.direction = base.transform.rotation * this.move.normalized * this.speed * this.slope * delta;
					}
					Vector3 vector = this.direction;
					if (this.isGrounded)
					{
						float num3 = Vector3.Angle(Vector3.up, this.ground.normal);
						if (num3 > 59f)
						{
							this.fall2 += 16f * delta;
							if (this.fall2 > 128f)
							{
								this.fall2 = 128f;
							}
							Vector3 lhs = Vector3.Cross(Vector3.up, this.ground.normal);
							Vector3 a = Vector3.Cross(lhs, this.ground.normal);
							vector += a * this.fall2 * delta;
						}
						else
						{
							this.fall2 = 0f;
						}
					}
					vector += Vector3.up * this.fall * delta;
					this.controller.Move(vector);
				}
				if (!base.channel.isOwner && Provider.isServer)
				{
					bool flag2 = Dedicator.serverVisibility == ESteamServerVisibility.LAN || PlayerMovement.forceTrustClient;
					if (flag2)
					{
						base.transform.localPosition = target;
						this._real = target;
					}
					else
					{
						this._real = base.transform.localPosition;
						if ((target - this.real).sqrMagnitude > 0.01f)
						{
							this.isRecovering = true;
							this.lastRecover = Time.realtimeSinceStartup;
							base.player.input.recov++;
							base.channel.send("tellRecov", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								this.real,
								base.player.input.recov
							});
						}
					}
					if (this.updates != null && (Mathf.Abs((int)(base.player.look.lastAngle - base.player.look.angle)) > 1 || Mathf.Abs((int)(base.player.look.lastRot - base.player.look.rot)) > 1 || Mathf.Abs(this.lastUpdatePos.x - this.real.x) > Provider.UPDATE_DISTANCE || Mathf.Abs(this.lastUpdatePos.y - this.real.y) > Provider.UPDATE_DISTANCE || Mathf.Abs(this.lastUpdatePos.z - this.real.z) > Provider.UPDATE_DISTANCE))
					{
						base.player.look.lastAngle = base.player.look.angle;
						base.player.look.lastRot = base.player.look.rot;
						this.lastUpdatePos = this.real;
						if (this.canAddSimulationResultsToUpdates)
						{
							this.updates.Add(new PlayerStateUpdate(this.real, base.player.look.angle, base.player.look.rot));
						}
						else
						{
							this.updates.Add(new PlayerStateUpdate(Vector3.zero, 0, 0));
						}
					}
				}
				return;
			}
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x00124368 File Offset: 0x00122768
		private void onPerspectiveUpdated(EPlayerPerspective newPerspective)
		{
			if (this.vehicle != null && base.player.equipment.asset != null && base.player.equipment.asset.type == EItemType.GUN)
			{
				if (newPerspective == EPlayerPerspective.THIRD)
				{
					PlayerUI.disableCrosshair();
				}
				else
				{
					PlayerUI.enableCrosshair();
				}
			}
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x001243CC File Offset: 0x001227CC
		private void Update()
		{
			if (this.nsb != null)
			{
				this.snapshot = (PitchYawSnapshotInfo)this.nsb.getCurrentSnapshot();
			}
			if (base.channel.isOwner)
			{
				if (!PlayerUI.window.showCursor && !LoadingUI.isBlocked)
				{
					this._jump = Input.GetKey(ControlsSettings.jump);
					if (this.getVehicle() != null)
					{
						if (Input.GetKeyDown(ControlsSettings.locker))
						{
							VehicleManager.sendVehicleLock();
						}
						if (Input.GetKeyDown(ControlsSettings.primary))
						{
							VehicleManager.sendVehicleHorn();
						}
						if (Input.GetKeyDown(ControlsSettings.secondary))
						{
							VehicleManager.sendVehicleHeadlights();
						}
						if (Input.GetKeyDown(ControlsSettings.other))
						{
							VehicleManager.sendVehicleBonus();
						}
					}
					if (this.getVehicle() != null && this.getVehicle().asset != null && (this.getVehicle().asset.engine == EEngine.PLANE || this.getVehicle().asset.engine == EEngine.HELICOPTER || this.getVehicle().asset.engine == EEngine.BLIMP))
					{
						if (Input.GetKey(ControlsSettings.yawLeft))
						{
							this.input_x = -1;
						}
						else if (Input.GetKey(ControlsSettings.yawRight))
						{
							this.input_x = 1;
						}
						else
						{
							this.input_x = 0;
						}
						if (Input.GetKey(ControlsSettings.thrustIncrease))
						{
							this.input_y = 1;
						}
						else if (Input.GetKey(ControlsSettings.thrustDecrease))
						{
							this.input_y = -1;
						}
						else
						{
							this.input_y = 0;
						}
					}
					else
					{
						if (Input.GetKey(ControlsSettings.left))
						{
							this.input_x = -1;
						}
						else if (Input.GetKey(ControlsSettings.right))
						{
							this.input_x = 1;
						}
						else
						{
							this.input_x = 0;
						}
						if (Input.GetKey(ControlsSettings.up))
						{
							this.input_y = 1;
						}
						else if (Input.GetKey(ControlsSettings.down))
						{
							this.input_y = -1;
						}
						else
						{
							this.input_y = 0;
						}
					}
				}
				else
				{
					this._jump = false;
					this.input_x = 0;
					this.input_y = 0;
				}
				this.input_x *= this.warp_x;
				this.input_y *= this.warp_y;
				if (base.player.look.isOrbiting)
				{
					this._jump = false;
					this._horizontal = 1;
					this._vertical = 1;
				}
				else
				{
					this._horizontal = (byte)(this.input_x + 1);
					this._vertical = (byte)(this.input_y + 1);
				}
			}
			if (!Dedicator.isDedicated && Time.time - this.lastFootstep > 1.75f / this.speed)
			{
				this.lastFootstep = Time.time;
				if (!base.channel.isOwner)
				{
					this.checkGround(base.transform.position);
				}
				if (this.isGrounded && this.isMoving && base.player.stance.stance != EPlayerStance.PRONE && this.material != EPhysicsMaterial.NONE)
				{
					if (this.material == EPhysicsMaterial.WATER_STATIC)
					{
						if (base.player.stance.stance == EPlayerStance.SWIM)
						{
							base.player.playSound((AudioClip)Resources.Load("Sounds/Physics/Water/Footsteps/Swim"), 0.1f);
						}
						else
						{
							base.player.playSound((AudioClip)Resources.Load("Sounds/Physics/Water/Footsteps/Splash"), 0.2f);
						}
					}
					else
					{
						float num = 1f - base.player.skills.mastery(1, 0) * 0.75f;
						if (base.player.stance.stance == EPlayerStance.CROUCH)
						{
							num *= 0.5f;
						}
						num *= 0.2f;
						if (num > 0.01f)
						{
							if (this.material == EPhysicsMaterial.CLOTH_DYNAMIC || this.material == EPhysicsMaterial.CLOTH_STATIC)
							{
								base.player.playSound((AudioClip)Resources.Load("Sounds/Physics/Tile/Footsteps/Tile_" + UnityEngine.Random.Range(0, 4)), num);
							}
							else if (this.material == EPhysicsMaterial.TILE_DYNAMIC || this.material == EPhysicsMaterial.TILE_STATIC)
							{
								base.player.playSound((AudioClip)Resources.Load("Sounds/Physics/Tile/Footsteps/Tile_" + UnityEngine.Random.Range(0, 4)), num);
							}
							else if (this.material == EPhysicsMaterial.CONCRETE_DYNAMIC || this.material == EPhysicsMaterial.CONCRETE_STATIC)
							{
								base.player.playSound((AudioClip)Resources.Load("Sounds/Physics/Concrete/Footsteps/Concrete_" + UnityEngine.Random.Range(0, 4)), num);
							}
							else if (this.material == EPhysicsMaterial.GRAVEL_DYNAMIC || this.material == EPhysicsMaterial.GRAVEL_STATIC)
							{
								base.player.playSound((AudioClip)Resources.Load("Sounds/Physics/Gravel/Footsteps/Gravel_" + UnityEngine.Random.Range(0, 4)), num);
							}
							else if (this.material == EPhysicsMaterial.METAL_DYNAMIC || this.material == EPhysicsMaterial.METAL_STATIC || this.material == EPhysicsMaterial.METAL_SLIP)
							{
								base.player.playSound((AudioClip)Resources.Load("Sounds/Physics/Metal/Footsteps/Metal_" + UnityEngine.Random.Range(0, 5)), num);
							}
							else if (this.material == EPhysicsMaterial.WOOD_DYNAMIC || this.material == EPhysicsMaterial.WOOD_STATIC)
							{
								base.player.playSound((AudioClip)Resources.Load("Sounds/Physics/Wood/Footsteps/Wood_" + UnityEngine.Random.Range(0, 11)), num);
							}
							else if (this.material == EPhysicsMaterial.FOLIAGE_STATIC || this.material == EPhysicsMaterial.FOLIAGE_DYNAMIC)
							{
								base.player.playSound((AudioClip)Resources.Load("Sounds/Physics/Foliage/Footsteps/Foliage_" + UnityEngine.Random.Range(0, 7)), num);
							}
							else if (this.material == EPhysicsMaterial.SNOW_STATIC || this.material == EPhysicsMaterial.ICE_STATIC)
							{
								base.player.playSound((AudioClip)Resources.Load("Sounds/Physics/Snow/Footsteps/Snow_" + UnityEngine.Random.Range(0, 7)), num);
							}
						}
					}
				}
			}
			if (base.channel.isOwner)
			{
				if (base.player.look.isOrbiting && (!base.player.workzone.isBuilding || Input.GetKey(ControlsSettings.secondary)))
				{
					float d = 4f;
					if (Input.GetKey(ControlsSettings.modify))
					{
						d = 16f;
					}
					else if (Input.GetKey(ControlsSettings.other))
					{
						d = 1f;
					}
					base.player.look.orbitPosition += MainCamera.instance.transform.right * (float)this.input_x * Time.deltaTime * d;
					base.player.look.orbitPosition += MainCamera.instance.transform.forward * (float)this.input_y * Time.deltaTime * d;
					float d2 = 0f;
					if (Input.GetKey(ControlsSettings.ascend))
					{
						d2 = 1f;
					}
					else if (Input.GetKey(ControlsSettings.descend))
					{
						d2 = -1f;
					}
					base.player.look.orbitPosition += Vector3.up * d2 * Time.deltaTime * d;
				}
				if (base.player.stance.stance == EPlayerStance.DRIVING || base.player.stance.stance == EPlayerStance.SITTING)
				{
					base.player.first.localPosition = Vector3.zero;
					base.player.third.localPosition = Vector3.zero;
					this.fell = base.transform.position.y;
					this.fall2 = 0f;
				}
				else
				{
					base.player.first.position = Vector3.Lerp(this.lastUpdatePos, base.transform.position, (Time.realtimeSinceStartup - base.player.input.tick) / PlayerInput.RATE);
					if (base.player.stance.stance == EPlayerStance.PRONE)
					{
						base.player.first.position += Vector3.down * 0.1f;
					}
					base.player.third.position = base.player.first.position;
				}
				base.player.look.aim.parent.transform.position = base.player.first.position;
				if (this.vehicle != null)
				{
					if ((base.transform.position - this.lastStatPos).sqrMagnitude > 1024f)
					{
						this.lastStatPos = base.transform.position;
					}
					else if (Time.realtimeSinceStartup - this.lastStatTime > 1f)
					{
						this.lastStatTime = Time.realtimeSinceStartup;
						if ((base.transform.position - this.lastStatPos).sqrMagnitude > 0.1f)
						{
							int num2;
							if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Travel_Vehicle", out num2))
							{
								Provider.provider.statisticsService.userStatisticsService.setStatistic("Travel_Vehicle", num2 + (int)(base.transform.position - this.lastStatPos).magnitude);
							}
							this.lastStatPos = base.transform.position;
						}
					}
				}
				else if ((base.transform.position - this.lastStatPos).sqrMagnitude > 256f)
				{
					this.lastStatPos = base.transform.position;
				}
				else if (Time.realtimeSinceStartup - this.lastStatTime > 1f)
				{
					this.lastStatTime = Time.realtimeSinceStartup;
					if ((base.transform.position - this.lastStatPos).sqrMagnitude > 0.1f)
					{
						int num3;
						if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Travel_Foot", out num3))
						{
							Provider.provider.statisticsService.userStatisticsService.setStatistic("Travel_Foot", num3 + (int)(base.transform.position - this.lastStatPos).magnitude);
						}
						this.lastStatPos = base.transform.position;
					}
				}
			}
			else if (!Provider.isServer)
			{
				if (base.player.stance.stance == EPlayerStance.SITTING || base.player.stance.stance == EPlayerStance.DRIVING)
				{
					this._isMoving = false;
					base.transform.localPosition = Vector3.zero;
				}
				else
				{
					if (Mathf.Abs(this.lastUpdatePos.x - base.transform.position.x) > 0.01f || Mathf.Abs(this.lastUpdatePos.y - base.transform.position.y) > 0.01f || Mathf.Abs(this.lastUpdatePos.z - base.transform.position.z) > 0.01f)
					{
						this._isMoving = true;
					}
					else
					{
						this._isMoving = false;
					}
					base.transform.localPosition = this.snapshot.pos;
				}
			}
			if (!base.channel.isOwner && base.player.third != null)
			{
				if (base.player.stance.stance == EPlayerStance.PRONE)
				{
					base.player.third.localPosition = new Vector3(0f, -0.1f, 0f);
				}
				else
				{
					base.player.third.localPosition = Vector3.zero;
				}
			}
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x0012506C File Offset: 0x0012346C
		private void updateRegionAndBound()
		{
			byte b;
			byte b2;
			if (Regions.tryGetCoordinate(base.transform.position, out b, out b2) && (b != this.region_x || b2 != this.region_y))
			{
				byte region_x = this.region_x;
				byte region_y = this.region_y;
				this._region_x = b;
				this._region_y = b2;
				this.updateRegionOld_X = region_x;
				this.updateRegionOld_Y = region_y;
				this.updateRegionNew_X = b;
				this.updateRegionNew_Y = b2;
				this.updateRegionIndex = 0;
			}
			if (this.updateRegionIndex < 6)
			{
				bool flag = true;
				if (this.onRegionUpdated != null)
				{
					this.onRegionUpdated(base.player, this.updateRegionOld_X, this.updateRegionOld_Y, this.updateRegionNew_X, this.updateRegionNew_Y, this.updateRegionIndex, ref flag);
				}
				if (flag)
				{
					this.updateRegionIndex += 1;
				}
			}
			byte b3;
			LevelNavigation.tryGetBounds(base.transform.position, out b3);
			if (b3 != this.bound)
			{
				byte bound = this.bound;
				this._bound = b3;
				if (this.onBoundUpdated != null)
				{
					this.onBoundUpdated(base.player, bound, b3);
				}
			}
			if (Provider.isServer)
			{
				byte b4;
				LevelNavigation.tryGetNavigation(base.transform.position, out b4);
				if (b4 != this.nav)
				{
					byte nav = this.nav;
					this._nav = b4;
					this.TriggerPlayerNavChanged(nav, b4);
				}
			}
			bool flag2 = LevelNodes.isPointInsideSafezone(base.transform.position, out this.isSafeInfo);
			bool flag3 = false;
			PurchaseNode purchaseNode = null;
			this.effectNode = null;
			this.inRain = !Level.info.configData.Use_Rain_Volumes;
			this.inSnow = LevelLighting.isPositionSnowy(base.transform.position);
			for (int i = 0; i < LevelNodes.nodes.Count; i++)
			{
				Node node = LevelNodes.nodes[i];
				if (node.type == ENodeType.PURCHASE)
				{
					if (purchaseNode == null)
					{
						PurchaseNode purchaseNode2 = (PurchaseNode)node;
						if ((base.transform.position - purchaseNode2.point).sqrMagnitude < purchaseNode2.radius)
						{
							purchaseNode = purchaseNode2;
						}
					}
				}
				else if (node.type == ENodeType.DEADZONE)
				{
					if (!flag3)
					{
						DeadzoneNode deadzoneNode = (DeadzoneNode)node;
						if ((base.transform.position - deadzoneNode.point).sqrMagnitude < deadzoneNode.radius)
						{
							flag3 = true;
						}
					}
				}
				else if (node.type == ENodeType.EFFECT)
				{
					if (base.channel.isOwner)
					{
						if (this.effectNode == null)
						{
							EffectNode effectNode = (EffectNode)node;
							if (effectNode.shape == ENodeShape.SPHERE)
							{
								if ((base.transform.position - effectNode.point).sqrMagnitude < effectNode.radius)
								{
									this.effectNode = effectNode;
								}
							}
							else if (effectNode.shape == ENodeShape.BOX && Mathf.Abs(base.transform.position.x - effectNode.point.x) < effectNode.bounds.x && Mathf.Abs(base.transform.position.y - effectNode.point.y) < effectNode.bounds.y && Mathf.Abs(base.transform.position.z - effectNode.point.z) < effectNode.bounds.z)
							{
								this.effectNode = effectNode;
							}
						}
					}
				}
			}
			AmbianceVolume ambianceVolume;
			if (AmbianceUtility.isPointInsideVolume(base.transform.position, out ambianceVolume))
			{
				this.effectNode = ambianceVolume;
				if (!this.inRain && Level.info.configData.Use_Rain_Volumes)
				{
					this.inRain = ambianceVolume.canRain;
				}
				if (!this.inSnow && Level.info.configData.Use_Snow_Volumes)
				{
					this.inSnow = ambianceVolume.canSnow;
				}
			}
			this.inSnow &= (LevelLighting.snowyness == ELightingSnow.BLIZZARD);
			DeadzoneVolume deadzoneVolume;
			if (!flag3 && DeadzoneUtility.isPointInsideVolume(base.transform.position, out deadzoneVolume))
			{
				flag3 = true;
			}
			if (flag2 != this.isSafe)
			{
				this._isSafe = flag2;
				if (this.onSafetyUpdated != null)
				{
					this.onSafetyUpdated(this.isSafe);
				}
			}
			if (flag3 != this.isRadiated)
			{
				this._isRadiated = flag3;
				if (this.onRadiationUpdated != null)
				{
					this.onRadiationUpdated(this.isRadiated);
				}
			}
			if (purchaseNode != this.purchaseNode)
			{
				this._purchaseNode = purchaseNode;
				if (this.onPurchaseUpdated != null)
				{
					this.onPurchaseUpdated(this.purchaseNode);
				}
			}
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x001255A0 File Offset: 0x001239A0
		private void Start()
		{
			this._multiplier = 1f;
			this.multiplier = 1f;
			this.itemGravityMultiplier = 1f;
			this.pluginGravityMultiplier = 1f;
			this.slope = 1f;
			this._region_x = byte.MaxValue;
			this._region_y = byte.MaxValue;
			this._bound = byte.MaxValue;
			this._nav = byte.MaxValue;
			if (base.channel.isOwner || Provider.isServer)
			{
				this._loadedRegions = new LoadedRegion[(int)Regions.WORLD_SIZE, (int)Regions.WORLD_SIZE];
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						this.loadedRegions[(int)b, (int)b2] = new LoadedRegion();
					}
				}
				this._loadedBounds = new LoadedBound[LevelNavigation.bounds.Count];
				byte b3 = 0;
				while ((int)b3 < LevelNavigation.bounds.Count)
				{
					this.loadedBounds[(int)b3] = new LoadedBound();
					b3 += 1;
				}
			}
			this.warp_x = 1;
			this.warp_y = 1;
			if (Provider.isServer || base.channel.isOwner)
			{
				this.controller = base.GetComponent<CharacterController>();
				PlayerLook look = base.player.look;
				look.onPerspectiveUpdated = (PerspectiveUpdated)Delegate.Combine(look.onPerspectiveUpdated, new PerspectiveUpdated(this.onPerspectiveUpdated));
			}
			if (Provider.isServer)
			{
				PlayerLife life = base.player.life;
				life.onVisionUpdated = (VisionUpdated)Delegate.Combine(life.onVisionUpdated, new VisionUpdated(this.onVisionUpdated));
				PlayerLife life2 = base.player.life;
				life2.onLifeUpdated = (LifeUpdated)Delegate.Combine(life2.onLifeUpdated, new LifeUpdated(this.onLifeUpdated));
			}
			else
			{
				this.nsb = new NetworkSnapshotBuffer(Provider.UPDATE_TIME, Provider.UPDATE_DELAY);
			}
			this.applySize();
			if (Dedicator.isDedicated)
			{
				base.gameObject.AddComponent<Rigidbody>();
				base.GetComponent<Rigidbody>().useGravity = false;
				base.GetComponent<Rigidbody>().isKinematic = true;
			}
			this.updateMovement();
			this.updates = new List<PlayerStateUpdate>();
			this.canAddSimulationResultsToUpdates = true;
			this.lastFootstep = Time.time;
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x001257F0 File Offset: 0x00123BF0
		private void OnDrawGizmos()
		{
			if (this.nsb == null)
			{
				return;
			}
			for (int i = 0; i < this.nsb.snapshots.Length; i++)
			{
				if (this.nsb.snapshots[i].timestamp <= 0.01f)
				{
					return;
				}
				PitchYawSnapshotInfo pitchYawSnapshotInfo = (PitchYawSnapshotInfo)this.nsb.snapshots[i].info;
				Gizmos.DrawLine(pitchYawSnapshotInfo.pos, pitchYawSnapshotInfo.pos + Vector3.up * 2f);
			}
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x0012588B File Offset: 0x00123C8B
		private void OnDestroy()
		{
			this.updates = null;
		}

		// Token: 0x04001D27 RID: 7463
		public static readonly float HEIGHT_STAND = 2f;

		// Token: 0x04001D28 RID: 7464
		public static readonly float HEIGHT_CROUCH = 1.2f;

		// Token: 0x04001D29 RID: 7465
		public static readonly float HEIGHT_PRONE = 0.8f;

		// Token: 0x04001D2A RID: 7466
		private static bool _forceTrustClient;

		// Token: 0x04001D2B RID: 7467
		public Landed onLanded;

		// Token: 0x04001D2C RID: 7468
		public Seated onSeated;

		// Token: 0x04001D2D RID: 7469
		public VehicleUpdated onVehicleUpdated;

		// Token: 0x04001D2E RID: 7470
		public SafetyUpdated onSafetyUpdated;

		// Token: 0x04001D2F RID: 7471
		public RadiationUpdated onRadiationUpdated;

		// Token: 0x04001D30 RID: 7472
		public PurchaseUpdated onPurchaseUpdated;

		// Token: 0x04001D31 RID: 7473
		public PlayerRegionUpdated onRegionUpdated;

		// Token: 0x04001D32 RID: 7474
		public PlayerBoundUpdated onBoundUpdated;

		// Token: 0x04001D34 RID: 7476
		private static readonly float SPEED_CLIMB = 4.5f;

		// Token: 0x04001D35 RID: 7477
		private static readonly float SPEED_SWIM = 3f;

		// Token: 0x04001D36 RID: 7478
		private static readonly float SPEED_SPRINT = 7f;

		// Token: 0x04001D37 RID: 7479
		private static readonly float SPEED_STAND = 4.5f;

		// Token: 0x04001D38 RID: 7480
		private static readonly float SPEED_CROUCH = 2.5f;

		// Token: 0x04001D39 RID: 7481
		private static readonly float SPEED_PRONE = 1.5f;

		// Token: 0x04001D3A RID: 7482
		private static readonly float JUMP = 7f;

		// Token: 0x04001D3B RID: 7483
		private static readonly float SWIM = 3f;

		// Token: 0x04001D3C RID: 7484
		private CharacterController controller;

		// Token: 0x04001D3D RID: 7485
		public float _multiplier;

		// Token: 0x04001D3E RID: 7486
		public float multiplier;

		// Token: 0x04001D3F RID: 7487
		public float itemGravityMultiplier;

		// Token: 0x04001D40 RID: 7488
		public float pluginGravityMultiplier;

		// Token: 0x04001D41 RID: 7489
		private float slope;

		// Token: 0x04001D42 RID: 7490
		private float fell;

		// Token: 0x04001D43 RID: 7491
		private bool lastGrounded;

		// Token: 0x04001D44 RID: 7492
		private float lastFootstep;

		// Token: 0x04001D46 RID: 7494
		private bool _isGrounded;

		// Token: 0x04001D47 RID: 7495
		private bool _isSafe;

		// Token: 0x04001D48 RID: 7496
		public SafezoneNode isSafeInfo;

		// Token: 0x04001D49 RID: 7497
		private bool _isRadiated;

		// Token: 0x04001D4A RID: 7498
		private PurchaseNode _purchaseNode;

		// Token: 0x04001D4C RID: 7500
		public bool inRain;

		// Token: 0x04001D4D RID: 7501
		public bool inSnow;

		// Token: 0x04001D4E RID: 7502
		private EPhysicsMaterial material;

		// Token: 0x04001D4F RID: 7503
		private RaycastHit ground;

		// Token: 0x04001D50 RID: 7504
		private EPlayerHeight height;

		// Token: 0x04001D51 RID: 7505
		private bool wasSizeAppliedYet;

		// Token: 0x04001D52 RID: 7506
		private bool _isMoving;

		// Token: 0x04001D53 RID: 7507
		private Vector3 _move;

		// Token: 0x04001D54 RID: 7508
		private byte _region_x;

		// Token: 0x04001D55 RID: 7509
		private byte _region_y;

		// Token: 0x04001D56 RID: 7510
		private byte _bound;

		// Token: 0x04001D57 RID: 7511
		private byte _nav;

		// Token: 0x04001D58 RID: 7512
		private byte updateRegionOld_X;

		// Token: 0x04001D59 RID: 7513
		private byte updateRegionOld_Y;

		// Token: 0x04001D5A RID: 7514
		private byte updateRegionNew_X;

		// Token: 0x04001D5B RID: 7515
		private byte updateRegionNew_Y;

		// Token: 0x04001D5C RID: 7516
		private byte updateRegionIndex;

		// Token: 0x04001D5D RID: 7517
		private LoadedRegion[,] _loadedRegions;

		// Token: 0x04001D5E RID: 7518
		private LoadedBound[] _loadedBounds;

		// Token: 0x04001D5F RID: 7519
		private Vector3 direction;

		// Token: 0x04001D61 RID: 7521
		private float fall2;

		// Token: 0x04001D62 RID: 7522
		private Vector3 _real;

		// Token: 0x04001D63 RID: 7523
		private Vector3 lastUpdatePos;

		// Token: 0x04001D64 RID: 7524
		public PitchYawSnapshotInfo snapshot;

		// Token: 0x04001D65 RID: 7525
		private NetworkSnapshotBuffer nsb;

		// Token: 0x04001D66 RID: 7526
		private float lastTick;

		// Token: 0x04001D67 RID: 7527
		private byte _horizontal;

		// Token: 0x04001D68 RID: 7528
		private byte _vertical;

		// Token: 0x04001D69 RID: 7529
		private int warp_x;

		// Token: 0x04001D6A RID: 7530
		private int warp_y;

		// Token: 0x04001D6B RID: 7531
		private int input_x;

		// Token: 0x04001D6C RID: 7532
		private int input_y;

		// Token: 0x04001D6D RID: 7533
		private bool _jump;

		// Token: 0x04001D6E RID: 7534
		private bool isRecovering;

		// Token: 0x04001D6F RID: 7535
		private float lastRecover;

		// Token: 0x04001D70 RID: 7536
		private bool isFrozen;

		// Token: 0x04001D71 RID: 7537
		public bool isAllowed;

		// Token: 0x04001D72 RID: 7538
		[Obsolete]
		public bool isUpdated;

		// Token: 0x04001D73 RID: 7539
		public List<PlayerStateUpdate> updates;

		// Token: 0x04001D74 RID: 7540
		public bool canAddSimulationResultsToUpdates;

		// Token: 0x04001D75 RID: 7541
		private bool isSeating;

		// Token: 0x04001D76 RID: 7542
		private InteractableVehicle seatingVehicle;

		// Token: 0x04001D77 RID: 7543
		private byte seatingSeat;

		// Token: 0x04001D78 RID: 7544
		private Transform seatingTransform;

		// Token: 0x04001D79 RID: 7545
		private Vector3 seatingPosition;

		// Token: 0x04001D7A RID: 7546
		private byte seatingAngle;

		// Token: 0x04001D7B RID: 7547
		private Vector3 lastStatPos;

		// Token: 0x04001D7C RID: 7548
		private float lastStatTime;

		// Token: 0x04001D7D RID: 7549
		private InteractableVehicle vehicle;

		// Token: 0x04001D7E RID: 7550
		private byte seat;
	}
}
