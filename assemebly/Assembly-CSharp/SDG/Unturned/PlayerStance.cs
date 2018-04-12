using System;
using SDG.Framework.Water;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000649 RID: 1609
	public class PlayerStance : PlayerCaller
	{
		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06002E57 RID: 11863 RVA: 0x00129868 File Offset: 0x00127C68
		// (set) Token: 0x06002E58 RID: 11864 RVA: 0x00129870 File Offset: 0x00127C70
		public EPlayerStance stance
		{
			get
			{
				return this._stance;
			}
			set
			{
				this._stance = value;
				if (value != this.changeStance)
				{
					if (this.stance == EPlayerStance.STAND || this.stance == EPlayerStance.SPRINT || this.stance == EPlayerStance.CLIMB || this.stance == EPlayerStance.SWIM || this.stance == EPlayerStance.DRIVING || this.stance == EPlayerStance.SITTING)
					{
						base.player.movement.setSize(EPlayerHeight.STAND);
					}
					else if (this.stance == EPlayerStance.CROUCH)
					{
						base.player.movement.setSize(EPlayerHeight.CROUCH);
					}
					else if (this.stance == EPlayerStance.PRONE)
					{
						base.player.movement.setSize(EPlayerHeight.PRONE);
					}
				}
				this.changeStance = this.stance;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06002E59 RID: 11865 RVA: 0x00129938 File Offset: 0x00127D38
		public float radius
		{
			get
			{
				if (base.player.movement.nav != 255 && ZombieManager.regions[(int)base.player.movement.nav].isHyper)
				{
					return 24f;
				}
				if (this.stance == EPlayerStance.DRIVING)
				{
					if (base.player.movement.getVehicle().sirensOn)
					{
						return PlayerStance.DETECT_FORWARD;
					}
					if (base.player.movement.getVehicle().speed > 0f)
					{
						return PlayerStance.DETECT_FORWARD * base.player.movement.getVehicle().speed / base.player.movement.getVehicle().asset.speedMax;
					}
					return PlayerStance.DETECT_BACKWARD * base.player.movement.getVehicle().speed / base.player.movement.getVehicle().asset.speedMin;
				}
				else
				{
					if (this.stance == EPlayerStance.SITTING)
					{
						return 0f;
					}
					if (this.stance == EPlayerStance.SPRINT)
					{
						return PlayerStance.DETECT_SPRINT * ((!base.player.movement.isMoving) ? 1f : PlayerStance.DETECT_MOVE);
					}
					if (this.stance == EPlayerStance.STAND || this.stance == EPlayerStance.SWIM)
					{
						float num = 1f - base.player.skills.mastery(1, 0) * 0.5f;
						return PlayerStance.DETECT_STAND * ((!base.player.movement.isMoving) ? 1f : PlayerStance.DETECT_MOVE) * num;
					}
					float num2 = 1f - base.player.skills.mastery(1, 0) * 0.75f;
					if (this.stance == EPlayerStance.CROUCH || this.stance == EPlayerStance.CLIMB)
					{
						return PlayerStance.DETECT_CROUCH * ((!base.player.movement.isMoving) ? 1f : PlayerStance.DETECT_MOVE) * num2;
					}
					if (this.stance == EPlayerStance.PRONE)
					{
						return PlayerStance.DETECT_PRONE * ((!base.player.movement.isMoving) ? 1f : PlayerStance.DETECT_MOVE) * num2;
					}
					return 0f;
				}
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06002E5A RID: 11866 RVA: 0x00129B8C File Offset: 0x00127F8C
		public bool crouch
		{
			get
			{
				return this._crouch;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x00129B94 File Offset: 0x00127F94
		public bool prone
		{
			get
			{
				return this._prone;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002E5C RID: 11868 RVA: 0x00129B9C File Offset: 0x00127F9C
		public bool sprint
		{
			get
			{
				return this._sprint;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002E5D RID: 11869 RVA: 0x00129BA4 File Offset: 0x00127FA4
		public bool isSubmerged
		{
			get
			{
				return this._isSubmerged;
			}
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x00129BAC File Offset: 0x00127FAC
		private bool checkSpace(float height)
		{
			return Physics.OverlapSphereNonAlloc(base.transform.position + Vector3.up * height, PlayerStance.RADIUS, PlayerStance.checkColliders, RayMasks.BLOCK_STANCE, QueryTriggerInteraction.Ignore) == 0;
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x00129BE1 File Offset: 0x00127FE1
		public void checkStance(EPlayerStance newStance)
		{
			this.checkStance(newStance, false);
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x00129BEC File Offset: 0x00127FEC
		public void checkStance(EPlayerStance newStance, bool all)
		{
			if (base.player.movement.getVehicle() != null && newStance != EPlayerStance.DRIVING && newStance != EPlayerStance.SITTING)
			{
				return;
			}
			if (newStance == this.stance)
			{
				return;
			}
			if ((newStance == EPlayerStance.PRONE || newStance == EPlayerStance.CROUCH) && (!base.player.movement.isGrounded || base.player.movement.fall > 0f))
			{
				return;
			}
			if (newStance == EPlayerStance.STAND && (this.stance == EPlayerStance.CROUCH || this.stance == EPlayerStance.PRONE))
			{
				if (Time.realtimeSinceStartup - this.lastStance <= PlayerStance.COOLDOWN)
				{
					return;
				}
				this.lastStance = Time.realtimeSinceStartup;
				if (!this.checkSpace(1.5f))
				{
					return;
				}
				if (!this.checkSpace(1f))
				{
					return;
				}
				if (!this.checkSpace(0.5f))
				{
					return;
				}
			}
			if (newStance == EPlayerStance.CROUCH && this.stance == EPlayerStance.PRONE)
			{
				if (Time.realtimeSinceStartup - this.lastStance <= PlayerStance.COOLDOWN)
				{
					return;
				}
				this.lastStance = Time.realtimeSinceStartup;
				if (!this.checkSpace(1f))
				{
					return;
				}
				if (!this.checkSpace(0.5f))
				{
					return;
				}
			}
			if (Provider.isServer)
			{
				if (base.player.animator.gesture == EPlayerGesture.INVENTORY_START)
				{
					if (newStance != EPlayerStance.STAND && newStance != EPlayerStance.SPRINT && newStance != EPlayerStance.CROUCH)
					{
						base.player.animator.sendGesture(EPlayerGesture.INVENTORY_STOP, false);
					}
				}
				else if (base.player.animator.gesture == EPlayerGesture.SURRENDER_START)
				{
					base.player.animator.sendGesture(EPlayerGesture.SURRENDER_STOP, true);
				}
				else if (base.player.animator.gesture == EPlayerGesture.REST_START)
				{
					base.player.animator.sendGesture(EPlayerGesture.REST_STOP, true);
				}
			}
			this.stance = newStance;
			if (this.onStanceUpdated != null)
			{
				this.onStanceUpdated();
			}
			if (Provider.isServer)
			{
				if (all)
				{
					base.channel.send("tellStance", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
					{
						(byte)this.stance
					});
				}
				else
				{
					base.channel.send("tellStance", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
					{
						(byte)this.stance
					});
				}
			}
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x00129E68 File Offset: 0x00128268
		[SteamCall]
		public void tellStance(CSteamID steamID, byte newStance)
		{
			if (base.channel.checkServer(steamID))
			{
				this.stance = (EPlayerStance)newStance;
				if (this.stance == EPlayerStance.CROUCH)
				{
					if (ControlsSettings.crouching == EControlMode.TOGGLE)
					{
						this._crouch = true;
						this._prone = false;
					}
				}
				else if (this.stance == EPlayerStance.PRONE && ControlsSettings.proning == EControlMode.TOGGLE)
				{
					this._crouch = false;
					this._prone = true;
				}
			}
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x00129EDC File Offset: 0x001282DC
		[SteamCall]
		public void askStance(CSteamID steamID)
		{
			if (Provider.isServer)
			{
				base.channel.send("tellStance", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					(byte)this.stance
				});
			}
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x00129F10 File Offset: 0x00128310
		public void simulate(uint simulation, bool inputCrouch, bool inputProne, bool inputSprint)
		{
			this._isSubmerged = WaterUtility.isPointUnderwater(base.player.look.aim.position);
			if (this.stance == EPlayerStance.CLIMB || ((this.stance == EPlayerStance.STAND || this.stance == EPlayerStance.SPRINT || this.stance == EPlayerStance.SWIM) && !base.player.equipment.isBusy))
			{
				Physics.Raycast(base.transform.position + Vector3.up * 0.5f, base.transform.forward, out this.ladder, 0.75f, RayMasks.LADDER_INTERACT);
				if (this.ladder.transform != null && this.ladder.transform.CompareTag("Ladder") && Mathf.Abs(Vector3.Dot(this.ladder.normal, this.ladder.transform.up)) > 0.9f)
				{
					if (this.stance != EPlayerStance.CLIMB)
					{
						Vector3 vector = new Vector3(this.ladder.transform.position.x, this.ladder.point.y - 0.5f, this.ladder.transform.position.z) + this.ladder.normal * 0.5f;
						if (!Physics.CapsuleCast(base.transform.position + new Vector3(0f, PlayerStance.RADIUS, 0f), base.transform.position + new Vector3(0f, PlayerMovement.HEIGHT_STAND - PlayerStance.RADIUS, 0f), PlayerStance.RADIUS, (vector - base.transform.position).normalized, (vector - base.transform.position).magnitude, RayMasks.BLOCK_LADDER, QueryTriggerInteraction.Ignore))
						{
							base.transform.position = vector;
							this.checkStance(EPlayerStance.CLIMB);
						}
					}
					if (this.stance == EPlayerStance.CLIMB)
					{
						return;
					}
				}
				else if (this.stance == EPlayerStance.CLIMB)
				{
					this.checkStance(EPlayerStance.STAND);
				}
			}
			bool flag = WaterUtility.isPointUnderwater(base.transform.position);
			if (WaterUtility.isPointUnderwater(base.transform.position + new Vector3(0f, 1.25f, 0f)))
			{
				if (this.stance != EPlayerStance.SWIM)
				{
					this.checkStance(EPlayerStance.SWIM);
				}
				return;
			}
			if (flag)
			{
				if (this.stance != EPlayerStance.STAND && this.stance != EPlayerStance.SPRINT)
				{
					this.checkStance(EPlayerStance.STAND);
				}
			}
			else if (this.stance == EPlayerStance.SWIM)
			{
				this.checkStance(EPlayerStance.STAND);
			}
			if (this.stance != EPlayerStance.CLIMB && this.stance != EPlayerStance.SITTING && this.stance != EPlayerStance.DRIVING)
			{
				if (inputCrouch != this.lastCrouch)
				{
					this.lastCrouch = inputCrouch;
					if (!flag)
					{
						if (inputCrouch)
						{
							this.checkStance(EPlayerStance.CROUCH);
						}
						else if (this.stance == EPlayerStance.CROUCH)
						{
							this.checkStance(EPlayerStance.STAND);
						}
					}
				}
				if (inputProne != this.lastProne)
				{
					this.lastProne = inputProne;
					if (!flag)
					{
						if (inputProne)
						{
							this.checkStance(EPlayerStance.PRONE);
						}
						else if (this.stance == EPlayerStance.PRONE)
						{
							this.checkStance(EPlayerStance.STAND);
						}
					}
				}
				if (inputSprint != this.lastSprint)
				{
					this.lastSprint = inputSprint;
					if (inputSprint)
					{
						if (this.stance == EPlayerStance.STAND && !base.player.life.isBroken && base.player.life.stamina > 0 && (double)base.player.movement.multiplier > 0.9 && base.player.movement.isMoving)
						{
							this.checkStance(EPlayerStance.SPRINT);
						}
					}
					else if (this.stance == EPlayerStance.SPRINT)
					{
						this.checkStance(EPlayerStance.STAND);
					}
				}
				if (this.stance == EPlayerStance.SPRINT && (base.player.life.isBroken || base.player.life.stamina == 0 || (double)base.player.movement.multiplier < 0.9 || !base.player.movement.isMoving))
				{
					this.checkStance(EPlayerStance.STAND);
				}
			}
			else
			{
				this.lastCrouch = false;
				this.lastProne = false;
				this.lastSprint = false;
			}
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x0012A3C5 File Offset: 0x001287C5
		private void onLifeUpdated(bool isDead)
		{
			if (!isDead)
			{
				this.checkStance(EPlayerStance.STAND);
			}
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x0012A3D4 File Offset: 0x001287D4
		private void Update()
		{
			if (base.channel.isOwner && !PlayerUI.window.showCursor)
			{
				if (!base.player.look.isOrbiting)
				{
					if (Input.GetKey(ControlsSettings.stance))
					{
						if (this.isHolding)
						{
							if (Time.realtimeSinceStartup - this.lastHold > 0.33f)
							{
								this._crouch = false;
								this._prone = true;
							}
						}
						else
						{
							this.isHolding = true;
							this.lastHold = Time.realtimeSinceStartup;
						}
					}
					else if (this.isHolding)
					{
						if (Time.realtimeSinceStartup - this.lastHold < 0.33f)
						{
							if (this.crouch)
							{
								this._crouch = false;
								this._prone = false;
							}
							else
							{
								this._crouch = true;
								this._prone = false;
							}
						}
						this.isHolding = false;
					}
					if (ControlsSettings.crouching == EControlMode.TOGGLE)
					{
						if (Input.GetKey(ControlsSettings.crouch) != this.flipCrouch)
						{
							this.flipCrouch = Input.GetKey(ControlsSettings.crouch);
							if (this.flipCrouch)
							{
								this._crouch = !this.crouch;
							}
						}
					}
					else
					{
						this._crouch = Input.GetKey(ControlsSettings.crouch);
						this.flipCrouch = this.crouch;
					}
					if (ControlsSettings.proning == EControlMode.TOGGLE)
					{
						if (Input.GetKey(ControlsSettings.prone) != this.flipProne)
						{
							this.flipProne = Input.GetKey(ControlsSettings.prone);
							if (this.flipProne)
							{
								this._prone = !this.prone;
							}
						}
					}
					else
					{
						this._prone = Input.GetKey(ControlsSettings.prone);
						this.flipProne = this.prone;
					}
					if (ControlsSettings.sprinting == EControlMode.TOGGLE)
					{
						if (Input.GetKey(ControlsSettings.sprint) != this.flipSprint)
						{
							this.flipSprint = Input.GetKey(ControlsSettings.sprint);
							if (this.flipSprint)
							{
								this._sprint = !this.sprint;
							}
						}
					}
					else
					{
						this._sprint = Input.GetKey(ControlsSettings.sprint);
						this.flipSprint = this.sprint;
					}
				}
				if ((this.stance == EPlayerStance.PRONE || this.stance == EPlayerStance.CROUCH) && Input.GetKey(ControlsSettings.jump))
				{
					this._crouch = false;
					this._prone = false;
				}
				if (this.stance == EPlayerStance.CLIMB || this.stance == EPlayerStance.SITTING || this.stance == EPlayerStance.DRIVING)
				{
					this._crouch = false;
					this._prone = false;
				}
				if (PlayerUI.window.showCursor)
				{
					this._sprint = false;
				}
			}
			if (Provider.isServer && (double)(Time.realtimeSinceStartup - this.lastDetect) > 0.1)
			{
				this.lastDetect = Time.realtimeSinceStartup;
				if (!base.player.life.isDead)
				{
					AlertTool.alert(base.player, base.transform.position, this.radius, this.stance != EPlayerStance.SPRINT && this.stance != EPlayerStance.DRIVING, base.player.look.aim.forward, base.player.isSpotOn);
				}
			}
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x0012A71C File Offset: 0x00128B1C
		private void Start()
		{
			this._stance = EPlayerStance.STAND;
			if (base.channel.isOwner || Provider.isServer)
			{
				this.lastStance = float.MinValue;
				PlayerLife life = base.player.life;
				life.onLifeUpdated = (LifeUpdated)Delegate.Combine(life.onLifeUpdated, new LifeUpdated(this.onLifeUpdated));
			}
			if (Provider.isServer && (!this.checkSpace(1.5f) || !this.checkSpace(1f) || !this.checkSpace(0.5f)))
			{
				this.stance = EPlayerStance.PRONE;
			}
			if (!Provider.isServer)
			{
				base.channel.send("askStance", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
			}
		}

		// Token: 0x04001DAC RID: 7596
		public static Collider[] checkColliders = new Collider[1];

		// Token: 0x04001DAD RID: 7597
		public static readonly float COOLDOWN = 0.75f;

		// Token: 0x04001DAE RID: 7598
		public static readonly float RADIUS = 0.4f;

		// Token: 0x04001DAF RID: 7599
		public static readonly float DETECT_MOVE = 1.1f;

		// Token: 0x04001DB0 RID: 7600
		public static readonly float DETECT_FORWARD = 48f;

		// Token: 0x04001DB1 RID: 7601
		public static readonly float DETECT_BACKWARD = 24f;

		// Token: 0x04001DB2 RID: 7602
		public static readonly float DETECT_SPRINT = 20f;

		// Token: 0x04001DB3 RID: 7603
		public static readonly float DETECT_STAND = 12f;

		// Token: 0x04001DB4 RID: 7604
		public static readonly float DETECT_CROUCH = 5f;

		// Token: 0x04001DB5 RID: 7605
		public static readonly float DETECT_PRONE = 2f;

		// Token: 0x04001DB6 RID: 7606
		public StanceUpdated onStanceUpdated;

		// Token: 0x04001DB7 RID: 7607
		private EPlayerStance changeStance;

		// Token: 0x04001DB8 RID: 7608
		private EPlayerStance _stance;

		// Token: 0x04001DB9 RID: 7609
		private float lastStance;

		// Token: 0x04001DBA RID: 7610
		private float lastDetect;

		// Token: 0x04001DBB RID: 7611
		private float lastHold;

		// Token: 0x04001DBC RID: 7612
		private bool isHolding;

		// Token: 0x04001DBD RID: 7613
		private bool flipCrouch;

		// Token: 0x04001DBE RID: 7614
		private bool lastCrouch;

		// Token: 0x04001DBF RID: 7615
		private bool _crouch;

		// Token: 0x04001DC0 RID: 7616
		private bool flipProne;

		// Token: 0x04001DC1 RID: 7617
		private bool lastProne;

		// Token: 0x04001DC2 RID: 7618
		private bool _prone;

		// Token: 0x04001DC3 RID: 7619
		private bool flipSprint;

		// Token: 0x04001DC4 RID: 7620
		private bool lastSprint;

		// Token: 0x04001DC5 RID: 7621
		private bool _sprint;

		// Token: 0x04001DC6 RID: 7622
		private bool _isSubmerged;

		// Token: 0x04001DC7 RID: 7623
		private RaycastHit ladder;
	}
}
