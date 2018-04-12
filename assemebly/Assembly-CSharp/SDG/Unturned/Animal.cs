using System;
using System.Collections.Generic;
using SDG.Framework.Water;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000381 RID: 897
	public class Animal : MonoBehaviour
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x00089E54 File Offset: 0x00088254
		// (set) Token: 0x060018B0 RID: 6320 RVA: 0x00089E5C File Offset: 0x0008825C
		public Vector3 target { get; private set; }

		// Token: 0x060018B1 RID: 6321 RVA: 0x00089E68 File Offset: 0x00088268
		private void updateTicking()
		{
			if (this.isFleeing || this.isWandering || this.isHunting)
			{
				if (this.isTicking)
				{
					return;
				}
				this.isTicking = true;
				AnimalManager.tickingAnimals.Add(this);
				this.lastTick = Time.time;
			}
			else
			{
				if (!this.isTicking)
				{
					return;
				}
				this.isTicking = false;
				AnimalManager.tickingAnimals.RemoveFast(this);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x00089EE2 File Offset: 0x000882E2
		public bool isFleeing
		{
			get
			{
				return this._isFleeing;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x00089EEA File Offset: 0x000882EA
		public float lastDead
		{
			get
			{
				return this._lastDead;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x00089EF2 File Offset: 0x000882F2
		public AnimalAsset asset
		{
			get
			{
				return this._asset;
			}
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00089EFC File Offset: 0x000882FC
		public void askEat()
		{
			if (this.isDead)
			{
				return;
			}
			this.lastEat = Time.time;
			this.eatDelay = UnityEngine.Random.Range(4f, 8f);
			this.isPlayingEat = true;
			if (!Dedicator.isDedicated)
			{
				this.animator.Play("Eat");
			}
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00089F58 File Offset: 0x00088358
		public void askGlance()
		{
			if (this.isDead)
			{
				return;
			}
			this.lastGlance = Time.time;
			this.glanceDelay = UnityEngine.Random.Range(4f, 8f);
			this.isPlayingGlance = true;
			if (!Dedicator.isDedicated)
			{
				this.animator.Play("Glance_" + UnityEngine.Random.Range(0, 2));
			}
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00089FC4 File Offset: 0x000883C4
		public void askStartle()
		{
			if (this.isDead)
			{
				return;
			}
			this.lastStartle = Time.time;
			this.isPlayingStartle = true;
			if (!Dedicator.isDedicated)
			{
				this.animator.Play("Startle");
			}
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0008A000 File Offset: 0x00088400
		public void askAttack()
		{
			if (this.isDead)
			{
				return;
			}
			this.lastAttack = Time.time;
			this.isPlayingAttack = true;
			if (!Dedicator.isDedicated)
			{
				if (this.animator["Attack"] != null)
				{
					this.animator.Play("Attack");
				}
				if (this.asset != null && this.asset.roars != null && this.asset.roars.Length > 0 && Time.time - this.startedRoar > 1f)
				{
					this.startedRoar = Time.time;
					base.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.9f, 1.1f);
					base.GetComponent<AudioSource>().PlayOneShot(this.asset.roars[UnityEngine.Random.Range(0, this.asset.roars.Length)]);
				}
			}
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0008A0F4 File Offset: 0x000884F4
		public void askPanic()
		{
			if (this.isDead)
			{
				return;
			}
			if (!Dedicator.isDedicated && this.asset != null && this.asset.panics != null && this.asset.panics.Length > 0 && Time.time - this.startedPanic > 1f)
			{
				this.startedPanic = Time.time;
				base.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.9f, 1.1f);
				base.GetComponent<AudioSource>().PlayOneShot(this.asset.panics[UnityEngine.Random.Range(0, this.asset.panics.Length)]);
			}
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0008A1AC File Offset: 0x000885AC
		public void askDamage(byte amount, Vector3 newRagdoll, out EPlayerKill kill, out uint xp, bool trackKill = true)
		{
			kill = EPlayerKill.NONE;
			xp = 0u;
			if (amount == 0 || this.isDead)
			{
				return;
			}
			if (!this.isDead)
			{
				if ((ushort)amount >= this.health)
				{
					this.health = 0;
				}
				else
				{
					this.health -= (ushort)amount;
				}
				this.ragdoll = newRagdoll;
				if (this.health == 0)
				{
					kill = EPlayerKill.ANIMAL;
					if (this.asset != null)
					{
						xp = this.asset.rewardXP;
					}
					AnimalManager.dropLoot(this);
					AnimalManager.sendAnimalDead(this, this.ragdoll);
					if (trackKill)
					{
						for (int i = 0; i < Provider.clients.Count; i++)
						{
							SteamPlayer steamPlayer = Provider.clients[i];
							if (!(steamPlayer.player == null) && !(steamPlayer.player.movement == null) && !(steamPlayer.player.life == null) && !steamPlayer.player.life.isDead)
							{
								if ((steamPlayer.player.transform.position - base.transform.position).sqrMagnitude < 262144f)
								{
									steamPlayer.player.quests.trackAnimalKill(this);
								}
							}
						}
					}
				}
				else if (this.asset != null && this.asset.panics != null && this.asset.panics.Length > 0)
				{
					AnimalManager.sendAnimalPanic(this);
				}
				this.lastRegen = Time.time;
			}
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0008A350 File Offset: 0x00088750
		public void sendRevive(Vector3 position, float angle)
		{
			AnimalManager.sendAnimalAlive(this, position, MeasurementTool.angleToByte(angle));
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0008A360 File Offset: 0x00088760
		private bool checkTargetValid(Vector3 point)
		{
			if (!Level.checkSafeIncludingClipVolumes(point))
			{
				return false;
			}
			float height = LevelGround.getHeight(point);
			return !WaterUtility.isPointUnderwater(new Vector3(point.x, height - 1f, point.z));
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0008A3A4 File Offset: 0x000887A4
		private Vector3 getFleeTarget(Vector3 normal)
		{
			Vector3 vector = base.transform.position + normal * 64f + new Vector3(UnityEngine.Random.Range(-8f, 8f), 0f, UnityEngine.Random.Range(-8f, 8f));
			if (!this.checkTargetValid(vector))
			{
				vector = base.transform.position + normal * 32f + new Vector3(UnityEngine.Random.Range(-8f, 8f), 0f, UnityEngine.Random.Range(-8f, 8f));
				if (!this.checkTargetValid(vector))
				{
					vector = base.transform.position + normal * -32f + new Vector3(UnityEngine.Random.Range(-8f, 8f), 0f, UnityEngine.Random.Range(-8f, 8f));
					if (!this.checkTargetValid(vector))
					{
						vector = base.transform.position + normal * -16f + new Vector3(UnityEngine.Random.Range(-8f, 8f), 0f, UnityEngine.Random.Range(-8f, 8f));
					}
				}
			}
			return vector;
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0008A4FC File Offset: 0x000888FC
		private void getWanderTarget()
		{
			Vector3 vector;
			if (this.isStuck)
			{
				vector = base.transform.position + new Vector3(UnityEngine.Random.Range(-8f, 8f), 0f, UnityEngine.Random.Range(-8f, 8f));
				if (!this.checkTargetValid(vector))
				{
					return;
				}
			}
			else if ((base.transform.position - this.pack.getAverageAnimalPoint()).sqrMagnitude > 256f)
			{
				vector = this.pack.getAverageAnimalPoint() + new Vector3(UnityEngine.Random.Range(-8f, 8f), 0f, UnityEngine.Random.Range(-8f, 8f));
			}
			else
			{
				Vector3 wanderDirection = this.pack.getWanderDirection();
				vector = base.transform.position + wanderDirection * UnityEngine.Random.Range(6f, 8f) + new Vector3(UnityEngine.Random.Range(-4f, 4f), 0f, UnityEngine.Random.Range(-4f, 4f));
				if (!this.checkTargetValid(vector))
				{
					vector = base.transform.position - wanderDirection * UnityEngine.Random.Range(6f, 8f) + new Vector3(UnityEngine.Random.Range(-4f, 4f), 0f, UnityEngine.Random.Range(-4f, 4f));
					if (!this.checkTargetValid(vector))
					{
						return;
					}
					this.pack.wanderAngle += UnityEngine.Random.Range(160f, 200f);
				}
				else
				{
					this.pack.wanderAngle += UnityEngine.Random.Range(-20f, 20f);
				}
			}
			this.target = vector;
			this.isWandering = true;
			this.updateTicking();
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0008A6F0 File Offset: 0x00088AF0
		public bool checkAlert(Player newPlayer)
		{
			return this.player != newPlayer;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0008A700 File Offset: 0x00088B00
		public void alertPlayer(Player newPlayer, bool sendToPack)
		{
			if (sendToPack)
			{
				for (int i = 0; i < this.pack.animals.Count; i++)
				{
					Animal animal = this.pack.animals[i];
					if (!(animal == null) && !(animal == this))
					{
						animal.alertPlayer(newPlayer, false);
					}
				}
			}
			if (this.isDead)
			{
				return;
			}
			if (this.player == newPlayer)
			{
				return;
			}
			if (this.player == null)
			{
				this._isFleeing = false;
				this.isWandering = false;
				this.isHunting = true;
				this.updateTicking();
				this.lastStuck = Time.time;
				this.player = newPlayer;
			}
			else if ((newPlayer.transform.position - base.transform.position).sqrMagnitude < (this.player.transform.position - base.transform.position).sqrMagnitude)
			{
				this._isFleeing = false;
				this.isWandering = false;
				this.isHunting = true;
				this.updateTicking();
				this.player = newPlayer;
			}
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0008A840 File Offset: 0x00088C40
		public void alertPoint(Vector3 newPosition, bool sendToPack)
		{
			this.alertDirection((base.transform.position - newPosition).normalized, sendToPack);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0008A870 File Offset: 0x00088C70
		public void alertDirection(Vector3 newDirection, bool sendToPack)
		{
			if (sendToPack)
			{
				for (int i = 0; i < this.pack.animals.Count; i++)
				{
					Animal animal = this.pack.animals[i];
					if (!(animal == null) && !(animal == this))
					{
						animal.alertDirection(newDirection, false);
					}
				}
			}
			if (this.isDead)
			{
				return;
			}
			if (this.isStuck)
			{
				return;
			}
			if (this.isHunting)
			{
				return;
			}
			if (!this.isFleeing)
			{
				AnimalManager.sendAnimalStartle(this);
			}
			this._isFleeing = true;
			this.isWandering = false;
			this.isHunting = false;
			this.updateTicking();
			this.target = this.getFleeTarget(newDirection);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0008A938 File Offset: 0x00088D38
		private void stop()
		{
			this.isMoving = false;
			this.isRunning = false;
			this._isFleeing = false;
			this.isWandering = false;
			this.isHunting = false;
			this.updateTicking();
			this.isStuck = false;
			this.player = null;
			this.target = base.transform.position;
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0008A990 File Offset: 0x00088D90
		public void tellAlive(Vector3 newPosition, byte newAngle)
		{
			this.isDead = false;
			base.transform.position = newPosition;
			base.transform.rotation = Quaternion.Euler(0f, (float)(newAngle * 2), 0f);
			this.updateLife();
			this.updateStates();
			this.reset();
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0008A9E0 File Offset: 0x00088DE0
		public void tellDead(Vector3 newRagdoll)
		{
			this.isDead = true;
			this._lastDead = Time.realtimeSinceStartup;
			this.updateLife();
			if (!Dedicator.isDedicated)
			{
				this.ragdoll = newRagdoll;
				RagdollTool.ragdollAnimal(base.transform.position, base.transform.rotation, this.skeleton, this.ragdoll, this.id);
			}
			if (Provider.isServer)
			{
				this.stop();
			}
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0008AA54 File Offset: 0x00088E54
		public void tellState(Vector3 newPosition, byte newAngle)
		{
			this.lastUpdatePos = newPosition;
			this.lastUpdateAngle = (float)newAngle * 2f;
			if (this.nsb != null)
			{
				this.nsb.addNewSnapshot(new YawSnapshotInfo(newPosition, (float)newAngle * 2f));
			}
			if (this.isPlayingEat || this.isPlayingGlance)
			{
				this.isPlayingEat = false;
				this.isPlayingGlance = false;
				this.animator.Stop();
			}
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0008AAD0 File Offset: 0x00088ED0
		private void updateLife()
		{
			if (!Dedicator.isDedicated)
			{
				if (this.renderer_0 != null)
				{
					this.renderer_0.enabled = !this.isDead;
				}
				if (this.renderer_1 != null)
				{
					this.renderer_1.enabled = !this.isDead;
				}
				this.skeleton.gameObject.SetActive(!this.isDead);
				base.GetComponent<Collider>().enabled = !this.isDead;
			}
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0008AB60 File Offset: 0x00088F60
		public void updateStates()
		{
			this.lastUpdatePos = base.transform.position;
			this.lastUpdateAngle = base.transform.rotation.eulerAngles.y;
			if (this.nsb != null)
			{
				this.nsb.updateLastSnapshot(new YawSnapshotInfo(base.transform.position, base.transform.rotation.eulerAngles.y));
			}
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0008ABE8 File Offset: 0x00088FE8
		private void reset()
		{
			this.target = base.transform.position;
			this.lastStartle = Time.time;
			this.lastWander = Time.time;
			this.lastStuck = Time.time;
			this.isPlayingEat = false;
			this.isPlayingGlance = false;
			this.isPlayingStartle = false;
			this.isMoving = false;
			this.isRunning = false;
			this._isFleeing = false;
			this.isWandering = false;
			this.isHunting = false;
			this.updateTicking();
			this.isStuck = false;
			this._asset = (AnimalAsset)Assets.find(EAssetType.ANIMAL, this.id);
			this.health = this.asset.health;
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0008AC94 File Offset: 0x00089094
		private void move(float delta)
		{
			Vector3 vector = this.target - base.transform.position;
			vector.y = 0f;
			Vector3 forward = vector;
			float magnitude = vector.magnitude;
			bool flag = magnitude > 0.75f;
			if (!Dedicator.isDedicated && flag && !this.isMoving)
			{
				if (this.isPlayingEat)
				{
					this.animator.Stop();
					this.isPlayingEat = false;
				}
				if (this.isPlayingGlance)
				{
					this.animator.Stop();
					this.isPlayingGlance = false;
				}
				if (this.isPlayingStartle)
				{
					this.animator.Stop();
					this.isPlayingStartle = false;
				}
			}
			this.isMoving = flag;
			this.isRunning = (this.isMoving && (this.isFleeing || this.isHunting));
			float num = Mathf.Clamp01(magnitude / 0.6f);
			Vector3 forward2 = base.transform.forward;
			float a = Vector3.Dot(vector.normalized, forward2);
			float num2 = ((!this.isRunning) ? this.asset.speedWalk : this.asset.speedRun) * Mathf.Max(a, 0.05f) * num;
			if (Time.deltaTime > 0f)
			{
				num2 = Mathf.Clamp(num2, 0f, magnitude / (Time.deltaTime * 2f));
			}
			vector = base.transform.forward * num2;
			vector.y = Physics.gravity.y * 2f;
			if (!this.isMoving)
			{
				vector.x = 0f;
				vector.z = 0f;
				if (!this.isStuck)
				{
					this._isFleeing = false;
					this.isWandering = false;
					this.updateTicking();
				}
			}
			else
			{
				Quaternion quaternion = base.transform.rotation;
				Quaternion b = Quaternion.LookRotation(forward);
				Vector3 eulerAngles = Quaternion.Slerp(quaternion, b, 8f * delta).eulerAngles;
				eulerAngles.z = 0f;
				eulerAngles.x = 0f;
				quaternion = Quaternion.Euler(eulerAngles);
				base.transform.rotation = quaternion;
			}
			this.controller.Move(vector * delta);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0008AEEC File Offset: 0x000892EC
		private void Update()
		{
			if (this.isDead)
			{
				return;
			}
			if (Provider.isServer)
			{
				if (!this.isUpdated)
				{
					if (Mathf.Abs(this.lastUpdatePos.x - base.transform.position.x) > Provider.UPDATE_DISTANCE || Mathf.Abs(this.lastUpdatePos.y - base.transform.position.y) > Provider.UPDATE_DISTANCE || Mathf.Abs(this.lastUpdatePos.z - base.transform.position.z) > Provider.UPDATE_DISTANCE || Mathf.Abs(this.lastUpdateAngle - base.transform.rotation.eulerAngles.y) > 1f)
					{
						this.lastUpdatePos = base.transform.position;
						this.lastUpdateAngle = base.transform.rotation.eulerAngles.y;
						this.isUpdated = true;
						AnimalManager.updates += 1;
						if (this.isStuck && Time.time - this.lastStuck > 0.5f)
						{
							this.isStuck = false;
							this.lastStuck = Time.time;
						}
					}
					else if (this.isMoving)
					{
						if (Time.time - this.lastStuck > 0.125f)
						{
							this.isStuck = true;
						}
					}
					else
					{
						this.isStuck = false;
						this.lastStuck = Time.time;
					}
				}
			}
			else
			{
				if (Mathf.Abs(this.lastUpdatePos.x - base.transform.position.x) > 0.01f || Mathf.Abs(this.lastUpdatePos.y - base.transform.position.y) > 0.01f || Mathf.Abs(this.lastUpdatePos.z - base.transform.position.z) > 0.01f)
				{
					if (!this.isMoving)
					{
						if (this.isPlayingEat)
						{
							this.animator.Stop();
							this.isPlayingEat = false;
						}
						if (this.isPlayingGlance)
						{
							this.animator.Stop();
							this.isPlayingGlance = false;
						}
						if (this.isPlayingStartle)
						{
							this.animator.Stop();
							this.isPlayingStartle = false;
						}
					}
					this.isMoving = true;
					this.isRunning = ((this.lastUpdatePos - base.transform.position).sqrMagnitude > 1f);
				}
				else
				{
					this.isMoving = false;
					this.isRunning = false;
				}
				if (this.nsb != null)
				{
					YawSnapshotInfo yawSnapshotInfo = (YawSnapshotInfo)this.nsb.getCurrentSnapshot();
					base.transform.position = yawSnapshotInfo.pos;
					base.transform.rotation = Quaternion.Euler(0f, yawSnapshotInfo.yaw, 0f);
				}
			}
			if (!Dedicator.isDedicated && !this.isMoving && !this.isPlayingEat && !this.isPlayingGlance && !this.isPlayingAttack)
			{
				if (Time.time - this.lastEat > this.eatDelay)
				{
					this.askEat();
				}
				else if (Time.time - this.lastGlance > this.glanceDelay)
				{
					this.askGlance();
				}
			}
			if (Provider.isServer)
			{
				if (this.isStuck)
				{
					if (Time.time - this.lastStuck > 0.75f)
					{
						this.lastStuck = Time.time;
						this.getWanderTarget();
					}
				}
				else if (!this.isFleeing && !this.isHunting)
				{
					if (Time.time - this.lastWander > this.wanderDelay)
					{
						this.lastWander = Time.time;
						this.wanderDelay = UnityEngine.Random.Range(8f, 16f);
						this.getWanderTarget();
					}
				}
				else
				{
					this.lastWander = Time.time;
				}
			}
			if (this.isPlayingEat)
			{
				if (Time.time - this.lastEat > this.eatTime)
				{
					this.isPlayingEat = false;
				}
			}
			else if (this.isPlayingGlance)
			{
				if (Time.time - this.lastGlance > this.glanceTime)
				{
					this.isPlayingGlance = false;
				}
			}
			else if (this.isPlayingStartle)
			{
				if (Time.time - this.lastStartle > this.startleTime)
				{
					this.isPlayingStartle = false;
				}
			}
			else if (this.isPlayingAttack)
			{
				if (Time.time - this.lastAttack > this.attackTime)
				{
					this.isPlayingAttack = false;
				}
			}
			else if (!Dedicator.isDedicated)
			{
				if (this.isRunning)
				{
					this.animator.Play("Run");
				}
				else if (this.isMoving)
				{
					this.animator.Play("Walk");
				}
				else
				{
					this.animator.Play("Idle");
				}
			}
			if (Provider.isServer && this.health < this.asset.health && Time.time - this.lastRegen > this.asset.regen)
			{
				this.lastRegen = Time.time;
				this.health += 1;
			}
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0008B4A4 File Offset: 0x000898A4
		public void tick()
		{
			float delta = Time.time - this.lastTick;
			this.lastTick = Time.time;
			if (this.isHunting)
			{
				if (this.player != null && !this.player.life.isDead && this.player.stance.stance != EPlayerStance.SWIM)
				{
					this.target = this.player.transform.position;
					float num = Mathf.Pow(this.target.x - base.transform.position.x, 2f) + Mathf.Pow(this.target.z - base.transform.position.z, 2f);
					float num2 = Mathf.Abs(this.target.y - base.transform.position.y);
					if (num < (float)((!(this.player.movement.getVehicle() != null)) ? 5 : 19) && num2 < 2f)
					{
						if (Time.time - this.lastTarget > ((!Dedicator.isDedicated) ? 0.1f : 0.3f))
						{
							if (this.isAttacking)
							{
								if (Time.time - this.lastAttack > this.attackTime / 2f)
								{
									this.isAttacking = false;
									byte b = this.asset.damage;
									b = (byte)((float)b * Provider.modeConfigData.Animals.Damage_Multiplier);
									if (this.player.movement.getVehicle() != null)
									{
										if (this.player.movement.getVehicle().asset != null && this.player.movement.getVehicle().asset.isVulnerableToEnvironment)
										{
											VehicleManager.damage(this.player.movement.getVehicle(), (float)b, 1f, true);
										}
									}
									else
									{
										EPlayerKill eplayerKill;
										DamageTool.damage(this.player, EDeathCause.ANIMAL, ELimb.SKULL, Provider.server, (this.target - base.transform.position).normalized, (float)b, 1f, out eplayerKill);
									}
								}
							}
							else if (Time.time - this.lastAttack > 1f)
							{
								this.isAttacking = true;
								AnimalManager.sendAnimalAttack(this);
							}
						}
					}
					else if (num > 4096f)
					{
						this.player = null;
						this.isHunting = false;
						this.updateTicking();
					}
					else
					{
						this.lastTarget = Time.time;
						this.isAttacking = false;
					}
				}
				else
				{
					this.player = null;
					this.isHunting = false;
					this.updateTicking();
				}
				this.lastWander = Time.time;
			}
			this.move(delta);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0008B7A4 File Offset: 0x00089BA4
		private void Start()
		{
			if (Provider.isServer)
			{
				this.controller = base.GetComponent<CharacterController>();
			}
			else
			{
				this.nsb = new NetworkSnapshotBuffer(Provider.UPDATE_TIME, Provider.UPDATE_DELAY);
			}
			this.reset();
			this.lastEat = Time.time + UnityEngine.Random.Range(4f, 16f);
			this.lastGlance = Time.time + UnityEngine.Random.Range(4f, 16f);
			this.lastWander = Time.time + UnityEngine.Random.Range(8f, 32f);
			this.eatDelay = UnityEngine.Random.Range(4f, 8f);
			this.glanceDelay = UnityEngine.Random.Range(4f, 8f);
			this.wanderDelay = UnityEngine.Random.Range(8f, 16f);
			this.updateLife();
			this.updateStates();
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0008B884 File Offset: 0x00089C84
		private void Awake()
		{
			if (Dedicator.isDedicated)
			{
				this.eatTime = 0.5f;
				this.glanceTime = 0.5f;
				this.startleTime = 0.5f;
				this.attackTime = 0.5f;
			}
			else
			{
				this.animator = base.transform.FindChild("Character").GetComponent<Animation>();
				this.skeleton = this.animator.transform.FindChild("Skeleton");
				if (this.animator.transform.FindChild("Model_0") != null)
				{
					this.renderer_0 = this.animator.transform.FindChild("Model_0").GetComponent<Renderer>();
				}
				if (this.animator.transform.FindChild("Model_1"))
				{
					this.renderer_1 = this.animator.transform.FindChild("Model_1").GetComponent<Renderer>();
				}
				this.eatTime = this.animator["Eat"].clip.length;
				this.glanceTime = this.animator["Glance_0"].clip.length;
				this.startleTime = this.animator["Startle"].clip.length;
				if (this.animator["Attack"] != null)
				{
					this.attackTime = this.animator["Attack"].clip.length;
				}
				else
				{
					this.attackTime = 0.5f;
				}
			}
		}

		// Token: 0x04000D0D RID: 3341
		private Animation animator;

		// Token: 0x04000D0E RID: 3342
		private Transform skeleton;

		// Token: 0x04000D0F RID: 3343
		private Renderer renderer_0;

		// Token: 0x04000D10 RID: 3344
		private Renderer renderer_1;

		// Token: 0x04000D11 RID: 3345
		private float lastEat;

		// Token: 0x04000D12 RID: 3346
		private float lastGlance;

		// Token: 0x04000D13 RID: 3347
		private float lastStartle;

		// Token: 0x04000D14 RID: 3348
		private float lastWander;

		// Token: 0x04000D15 RID: 3349
		private float lastStuck;

		// Token: 0x04000D16 RID: 3350
		private float lastTarget;

		// Token: 0x04000D17 RID: 3351
		private float lastAttack;

		// Token: 0x04000D18 RID: 3352
		private float lastRegen;

		// Token: 0x04000D19 RID: 3353
		private float eatTime;

		// Token: 0x04000D1A RID: 3354
		private float glanceTime;

		// Token: 0x04000D1B RID: 3355
		private float startleTime;

		// Token: 0x04000D1C RID: 3356
		private float attackTime;

		// Token: 0x04000D1D RID: 3357
		private float startedRoar;

		// Token: 0x04000D1E RID: 3358
		private float startedPanic;

		// Token: 0x04000D1F RID: 3359
		private float eatDelay;

		// Token: 0x04000D20 RID: 3360
		private float glanceDelay;

		// Token: 0x04000D21 RID: 3361
		private float wanderDelay;

		// Token: 0x04000D22 RID: 3362
		private bool isPlayingEat;

		// Token: 0x04000D23 RID: 3363
		private bool isPlayingGlance;

		// Token: 0x04000D24 RID: 3364
		private bool isPlayingStartle;

		// Token: 0x04000D25 RID: 3365
		private bool isPlayingAttack;

		// Token: 0x04000D26 RID: 3366
		private Player player;

		// Token: 0x04000D28 RID: 3368
		private Vector3 lastUpdatePos;

		// Token: 0x04000D29 RID: 3369
		private float lastUpdateAngle;

		// Token: 0x04000D2A RID: 3370
		private NetworkSnapshotBuffer nsb;

		// Token: 0x04000D2B RID: 3371
		private bool isMoving;

		// Token: 0x04000D2C RID: 3372
		private bool isRunning;

		// Token: 0x04000D2D RID: 3373
		private bool isTicking;

		// Token: 0x04000D2E RID: 3374
		private bool _isFleeing;

		// Token: 0x04000D2F RID: 3375
		private bool isWandering;

		// Token: 0x04000D30 RID: 3376
		private bool isHunting;

		// Token: 0x04000D31 RID: 3377
		private bool isStuck;

		// Token: 0x04000D32 RID: 3378
		private bool isAttacking;

		// Token: 0x04000D33 RID: 3379
		private float _lastDead;

		// Token: 0x04000D34 RID: 3380
		public bool isDead;

		// Token: 0x04000D35 RID: 3381
		public ushort index;

		// Token: 0x04000D36 RID: 3382
		public ushort id;

		// Token: 0x04000D37 RID: 3383
		public PackInfo pack;

		// Token: 0x04000D38 RID: 3384
		private ushort health;

		// Token: 0x04000D39 RID: 3385
		private Vector3 ragdoll;

		// Token: 0x04000D3A RID: 3386
		private AnimalAsset _asset;

		// Token: 0x04000D3B RID: 3387
		private CharacterController controller;

		// Token: 0x04000D3C RID: 3388
		public bool isUpdated;

		// Token: 0x04000D3D RID: 3389
		private float lastTick;
	}
}
