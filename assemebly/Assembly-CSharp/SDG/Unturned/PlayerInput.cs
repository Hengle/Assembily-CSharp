using System;
using System.Collections.Generic;
using SDG.Framework.Utilities;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000612 RID: 1554
	public class PlayerInput : PlayerCaller
	{
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x00116B38 File Offset: 0x00114F38
		public float tick
		{
			get
			{
				return this._tick;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x00116B40 File Offset: 0x00114F40
		public uint simulation
		{
			get
			{
				return this._simulation;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002BF9 RID: 11257 RVA: 0x00116B48 File Offset: 0x00114F48
		public uint clock
		{
			get
			{
				return this._clock;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002BFA RID: 11258 RVA: 0x00116B50 File Offset: 0x00114F50
		// (set) Token: 0x06002BFB RID: 11259 RVA: 0x00116B58 File Offset: 0x00114F58
		public bool[] keys { get; protected set; }

		// Token: 0x06002BFC RID: 11260 RVA: 0x00116B61 File Offset: 0x00114F61
		public bool hasInputs()
		{
			return this.inputs != null && this.inputs.Count > 0;
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x00116B7F File Offset: 0x00114F7F
		public int getInputCount()
		{
			if (this.inputs == null)
			{
				return 0;
			}
			return this.inputs.Count;
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x00116B9C File Offset: 0x00114F9C
		public InputInfo getInput(bool doOcclusionCheck)
		{
			if (this.inputs != null && this.inputs.Count > 0)
			{
				InputInfo inputInfo = this.inputs.Dequeue();
				if (doOcclusionCheck && !this.hasDoneOcclusionCheck)
				{
					this.hasDoneOcclusionCheck = true;
					if (inputInfo != null)
					{
						Vector3 a = inputInfo.point - base.player.look.aim.position;
						float magnitude = a.magnitude;
						Vector3 vector = a / magnitude;
						if (magnitude > 0.025f)
						{
							PhysicsUtility.raycast(new Ray(base.player.look.aim.position, vector), out this.obstruction, magnitude - 0.025f, RayMasks.DAMAGE_SERVER, QueryTriggerInteraction.UseGlobal);
							if (this.obstruction.transform != null)
							{
								return null;
							}
							PhysicsUtility.raycast(new Ray(base.player.look.aim.position + vector * (magnitude - 0.025f), -vector), out this.obstruction, magnitude - 0.025f, RayMasks.DAMAGE_SERVER, QueryTriggerInteraction.UseGlobal);
							if (this.obstruction.transform != null)
							{
								return null;
							}
						}
					}
				}
				return inputInfo;
			}
			return null;
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x00116CE0 File Offset: 0x001150E0
		public bool isRaycastInvalid(RaycastInfo info)
		{
			return info.player == null && info.zombie == null && info.animal == null && info.vehicle == null && info.transform == null;
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x00116D40 File Offset: 0x00115140
		public void sendRaycast(RaycastInfo info)
		{
			if (this.isRaycastInvalid(info))
			{
				return;
			}
			if (Provider.isServer)
			{
				InputInfo inputInfo = new InputInfo();
				inputInfo.animal = info.animal;
				inputInfo.direction = info.direction;
				inputInfo.limb = info.limb;
				inputInfo.material = info.material;
				inputInfo.normal = info.normal;
				inputInfo.player = info.player;
				inputInfo.point = info.point;
				inputInfo.transform = info.transform;
				inputInfo.vehicle = info.vehicle;
				inputInfo.zombie = info.zombie;
				inputInfo.section = info.section;
				if (inputInfo.player != null)
				{
					inputInfo.type = ERaycastInfoType.PLAYER;
				}
				else if (inputInfo.zombie != null)
				{
					inputInfo.type = ERaycastInfoType.ZOMBIE;
				}
				else if (inputInfo.animal != null)
				{
					inputInfo.type = ERaycastInfoType.ANIMAL;
				}
				else if (inputInfo.vehicle != null)
				{
					inputInfo.type = ERaycastInfoType.VEHICLE;
				}
				else if (inputInfo.transform != null)
				{
					if (inputInfo.transform.CompareTag("Barricade"))
					{
						inputInfo.type = ERaycastInfoType.BARRICADE;
					}
					else if (info.transform.CompareTag("Structure"))
					{
						inputInfo.type = ERaycastInfoType.STRUCTURE;
					}
					else if (info.transform.CompareTag("Resource"))
					{
						inputInfo.type = ERaycastInfoType.RESOURCE;
					}
					else if (inputInfo.transform.CompareTag("Small") || inputInfo.transform.CompareTag("Medium") || inputInfo.transform.CompareTag("Large"))
					{
						inputInfo.type = ERaycastInfoType.OBJECT;
					}
					else if (info.transform.CompareTag("Ground") || info.transform.CompareTag("Environment"))
					{
						inputInfo.type = ERaycastInfoType.NONE;
					}
					else
					{
						inputInfo = null;
					}
				}
				else
				{
					inputInfo = null;
				}
				if (inputInfo != null)
				{
					this.inputs.Enqueue(inputInfo);
				}
			}
			else
			{
				PlayerInputPacket playerInputPacket = this.clientsidePackets[this.clientsidePackets.Count - 1];
				if (playerInputPacket.clientsideInputs == null)
				{
					playerInputPacket.clientsideInputs = new List<RaycastInfo>();
				}
				playerInputPacket.clientsideInputs.Add(info);
			}
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x00116FB4 File Offset: 0x001153B4
		[SteamCall]
		public void askInput(CSteamID steamID)
		{
			if (!base.channel.checkOwner(steamID))
			{
				return;
			}
			int num = -1;
			byte b = (byte)base.channel.read(Types.BYTE_TYPE);
			for (byte b2 = 0; b2 < b; b2 += 1)
			{
				byte b3 = (byte)base.channel.read(Types.BYTE_TYPE);
				PlayerInputPacket playerInputPacket;
				if (b3 > 0)
				{
					playerInputPacket = new DrivingPlayerInputPacket();
				}
				else
				{
					playerInputPacket = new WalkingPlayerInputPacket();
				}
				playerInputPacket.read(base.channel);
				if (playerInputPacket.sequence > this.sequence)
				{
					this.sequence = playerInputPacket.sequence;
					this.serversidePackets.Enqueue(playerInputPacket);
					num = playerInputPacket.sequence;
				}
			}
			if (num == -1)
			{
				return;
			}
			base.channel.send("askAck", ESteamCall.OWNER, ESteamPacket.UPDATE_UNRELIABLE_INSTANT, new object[]
			{
				num
			});
			this.lastInputed = Time.realtimeSinceStartup;
			this.hasInputed = true;
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x001170B4 File Offset: 0x001154B4
		[SteamCall]
		public void askAck(CSteamID steamID, int ack)
		{
			if (!base.channel.checkServer(steamID))
			{
				return;
			}
			if (this.clientsidePackets == null)
			{
				return;
			}
			for (int i = this.clientsidePackets.Count - 1; i >= 0; i--)
			{
				PlayerInputPacket playerInputPacket = this.clientsidePackets[i];
				if (playerInputPacket.sequence <= ack)
				{
					this.clientsidePackets.RemoveAt(i);
				}
			}
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x00117124 File Offset: 0x00115524
		private void FixedUpdate()
		{
			if (this.isDismissed)
			{
				return;
			}
			if (base.channel.isOwner)
			{
				if (this.count % PlayerInput.SAMPLES == 0u)
				{
					this._tick = Time.realtimeSinceStartup;
					this.keys[0] = base.player.movement.jump;
					this.keys[1] = base.player.equipment.primary;
					this.keys[2] = base.player.equipment.secondary;
					this.keys[3] = base.player.stance.crouch;
					this.keys[4] = base.player.stance.prone;
					this.keys[5] = base.player.stance.sprint;
					this.keys[6] = base.player.animator.leanLeft;
					this.keys[7] = base.player.animator.leanRight;
					this.keys[8] = false;
					this.analog = (byte)((int)base.player.movement.horizontal << 4 | (int)base.player.movement.vertical);
					base.player.life.simulate(this.simulation);
					base.player.stance.simulate(this.simulation, base.player.stance.crouch, base.player.stance.prone, base.player.stance.sprint);
					this.pitch = base.player.look.pitch;
					this.yaw = base.player.look.yaw;
					base.player.movement.simulate(this.simulation, 0, (int)(base.player.movement.horizontal - 1), (int)(base.player.movement.vertical - 1), base.player.look.look_x, base.player.look.look_y, base.player.movement.jump, base.player.stance.sprint, Vector3.zero, PlayerInput.RATE);
					if (Provider.isServer)
					{
						this.inputs.Clear();
					}
					else
					{
						this.sequence++;
						if (base.player.stance.stance == EPlayerStance.DRIVING)
						{
							this.clientsidePackets.Add(new DrivingPlayerInputPacket());
						}
						else
						{
							this.clientsidePackets.Add(new WalkingPlayerInputPacket());
						}
						PlayerInputPacket playerInputPacket = this.clientsidePackets[this.clientsidePackets.Count - 1];
						playerInputPacket.sequence = this.sequence;
						playerInputPacket.recov = this.recov;
					}
					base.player.equipment.simulate(this.simulation, base.player.equipment.primary, base.player.equipment.secondary, base.player.stance.sprint);
					base.player.animator.simulate(this.simulation, base.player.animator.leanLeft, base.player.animator.leanRight);
					this.buffer += PlayerInput.SAMPLES;
					this._simulation += 1u;
				}
				if (this.consumed < this.buffer)
				{
					this.consumed += 1u;
					base.player.equipment.tock(this.clock);
					this._clock += 1u;
				}
				if (this.consumed == this.buffer && this.clientsidePackets.Count > 0 && !Provider.isServer)
				{
					ushort num = 0;
					byte b = 0;
					while ((int)b < this.keys.Length)
					{
						if (this.keys[(int)b])
						{
							num |= this.flags[(int)b];
						}
						b += 1;
					}
					PlayerInputPacket playerInputPacket2 = this.clientsidePackets[this.clientsidePackets.Count - 1];
					playerInputPacket2.keys = num;
					if (playerInputPacket2 is DrivingPlayerInputPacket)
					{
						DrivingPlayerInputPacket drivingPlayerInputPacket = playerInputPacket2 as DrivingPlayerInputPacket;
						if (base.player.movement.getVehicle() != null && base.player.movement.getVehicle().asset.engine == EEngine.TRAIN)
						{
							drivingPlayerInputPacket.position = new Vector3(base.player.movement.getVehicle().roadPosition, 0f, 0f);
						}
						else
						{
							drivingPlayerInputPacket.position = base.transform.parent.parent.parent.position;
						}
						drivingPlayerInputPacket.angle_x = MeasurementTool.angleToByte2(base.transform.parent.parent.parent.rotation.eulerAngles.x);
						drivingPlayerInputPacket.angle_y = MeasurementTool.angleToByte2(base.transform.parent.parent.parent.rotation.eulerAngles.y);
						drivingPlayerInputPacket.angle_z = MeasurementTool.angleToByte2(base.transform.parent.parent.parent.rotation.eulerAngles.z);
						drivingPlayerInputPacket.speed = (byte)(Mathf.Clamp(base.player.movement.getVehicle().speed, -100f, 100f) + 128f);
						drivingPlayerInputPacket.physicsSpeed = (byte)(Mathf.Clamp(base.player.movement.getVehicle().physicsSpeed, -100f, 100f) + 128f);
						drivingPlayerInputPacket.turn = (byte)(base.player.movement.getVehicle().turn + 1);
					}
					else
					{
						WalkingPlayerInputPacket walkingPlayerInputPacket = playerInputPacket2 as WalkingPlayerInputPacket;
						walkingPlayerInputPacket.analog = this.analog;
						walkingPlayerInputPacket.position = base.transform.localPosition;
						walkingPlayerInputPacket.yaw = this.yaw;
						walkingPlayerInputPacket.pitch = this.pitch;
					}
					base.channel.openWrite();
					while (this.clientsidePackets.Count >= 25)
					{
						this.clientsidePackets.RemoveAt(0);
					}
					base.channel.write((byte)this.clientsidePackets.Count);
					foreach (PlayerInputPacket playerInputPacket3 in this.clientsidePackets)
					{
						if (playerInputPacket3 is DrivingPlayerInputPacket)
						{
							base.channel.write(1);
						}
						else
						{
							base.channel.write(0);
						}
						playerInputPacket3.write(base.channel);
					}
					base.channel.closeWrite("askInput", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_CHUNK_INSTANT);
				}
				this.count += 1u;
			}
			else if (Provider.isServer)
			{
				if (this.serversidePackets.Count > 0)
				{
					PlayerInputPacket playerInputPacket4 = this.serversidePackets.Peek();
					if (playerInputPacket4 is WalkingPlayerInputPacket || this.count % PlayerInput.SAMPLES == 0u)
					{
						if (this.simulation > (uint)((Time.realtimeSinceStartup + 5f - this.tick) / PlayerInput.RATE))
						{
							return;
						}
						playerInputPacket4 = this.serversidePackets.Dequeue();
						if (playerInputPacket4 == null)
						{
							return;
						}
						this.hasDoneOcclusionCheck = false;
						this.inputs = playerInputPacket4.serversideInputs;
						byte b2 = 0;
						while ((int)b2 < this.keys.Length)
						{
							this.keys[(int)b2] = ((playerInputPacket4.keys & this.flags[(int)b2]) == this.flags[(int)b2]);
							b2 += 1;
						}
						if (playerInputPacket4 is DrivingPlayerInputPacket)
						{
							DrivingPlayerInputPacket drivingPlayerInputPacket2 = playerInputPacket4 as DrivingPlayerInputPacket;
							if (!base.player.life.isDead)
							{
								base.player.life.simulate(this.simulation);
								base.player.look.simulate(0f, 0f, PlayerInput.RATE);
								base.player.stance.simulate(this.simulation, false, false, false);
								base.player.movement.simulate(this.simulation, drivingPlayerInputPacket2.recov, this.keys[0], this.keys[5], drivingPlayerInputPacket2.position, MeasurementTool.byteToAngle2(drivingPlayerInputPacket2.angle_x), MeasurementTool.byteToAngle2(drivingPlayerInputPacket2.angle_y), MeasurementTool.byteToAngle2(drivingPlayerInputPacket2.angle_z), (float)(drivingPlayerInputPacket2.speed - 128), (float)(drivingPlayerInputPacket2.physicsSpeed - 128), (int)(drivingPlayerInputPacket2.turn - 1), PlayerInput.RATE);
								base.player.equipment.simulate(this.simulation, this.keys[1], this.keys[2], this.keys[5]);
								base.player.animator.simulate(this.simulation, false, false);
							}
						}
						else
						{
							WalkingPlayerInputPacket walkingPlayerInputPacket2 = playerInputPacket4 as WalkingPlayerInputPacket;
							this.analog = walkingPlayerInputPacket2.analog;
							if (!base.player.life.isDead)
							{
								base.player.life.simulate(this.simulation);
								base.player.look.simulate(walkingPlayerInputPacket2.yaw, walkingPlayerInputPacket2.pitch, PlayerInput.RATE);
								base.player.stance.simulate(this.simulation, this.keys[3], this.keys[4], this.keys[5]);
								base.player.movement.simulate(this.simulation, walkingPlayerInputPacket2.recov, (this.analog >> 4 & 15) - 1, (int)((this.analog & 15) - 1), 0f, 0f, this.keys[0], this.keys[5], walkingPlayerInputPacket2.position, PlayerInput.RATE);
								base.player.equipment.simulate(this.simulation, this.keys[1], this.keys[2], this.keys[5]);
								base.player.animator.simulate(this.simulation, this.keys[6], this.keys[7]);
							}
						}
						this.buffer += PlayerInput.SAMPLES;
						this._simulation += 1u;
						while (this.consumed < this.buffer)
						{
							this.consumed += 1u;
							if (!base.player.life.isDead)
							{
								base.player.equipment.tock(this.clock);
							}
							this._clock += 1u;
						}
					}
					this.count += 1u;
				}
				else
				{
					base.player.movement.simulate();
					if (this.hasInputed && Time.realtimeSinceStartup - this.lastInputed > 10f)
					{
						Provider.dismiss(base.channel.owner.playerID.steamID);
						this.isDismissed = true;
					}
				}
			}
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x00117CA8 File Offset: 0x001160A8
		private void Start()
		{
			this._tick = Time.realtimeSinceStartup;
			this._simulation = 0u;
			this._clock = 0u;
			if (base.channel.isOwner || Provider.isServer)
			{
				this.keys = new bool[9];
				this.flags = new ushort[9];
				byte b = 0;
				while ((int)b < this.keys.Length)
				{
					this.flags[(int)b] = (ushort)(1 << (int)(8 - b));
					b += 1;
				}
			}
			if (base.channel.isOwner && Provider.isServer)
			{
				this.hasDoneOcclusionCheck = false;
				this.inputs = new Queue<InputInfo>();
			}
			if (base.channel.isOwner)
			{
				this.clientsidePackets = new List<PlayerInputPacket>();
			}
			else if (Provider.isServer)
			{
				this.serversidePackets = new Queue<PlayerInputPacket>();
			}
			this.sequence = -1;
			this.recov = -1;
		}

		// Token: 0x04001C63 RID: 7267
		public static readonly uint SAMPLES = 4u;

		// Token: 0x04001C64 RID: 7268
		public static readonly float RATE = 0.08f;

		// Token: 0x04001C65 RID: 7269
		private float _tick;

		// Token: 0x04001C66 RID: 7270
		private uint buffer;

		// Token: 0x04001C67 RID: 7271
		private uint consumed;

		// Token: 0x04001C68 RID: 7272
		private uint count;

		// Token: 0x04001C69 RID: 7273
		private uint _simulation;

		// Token: 0x04001C6A RID: 7274
		private uint _clock;

		// Token: 0x04001C6C RID: 7276
		private byte analog;

		// Token: 0x04001C6D RID: 7277
		private ushort[] flags;

		// Token: 0x04001C6E RID: 7278
		private float pitch;

		// Token: 0x04001C6F RID: 7279
		private float yaw;

		// Token: 0x04001C70 RID: 7280
		private bool hasDoneOcclusionCheck;

		// Token: 0x04001C71 RID: 7281
		private Queue<InputInfo> inputs;

		// Token: 0x04001C72 RID: 7282
		private List<PlayerInputPacket> clientsidePackets;

		// Token: 0x04001C73 RID: 7283
		private Queue<PlayerInputPacket> serversidePackets;

		// Token: 0x04001C74 RID: 7284
		private int sequence;

		// Token: 0x04001C75 RID: 7285
		public int recov;

		// Token: 0x04001C76 RID: 7286
		private RaycastHit obstruction;

		// Token: 0x04001C77 RID: 7287
		private float lastInputed;

		// Token: 0x04001C78 RID: 7288
		private bool hasInputed;

		// Token: 0x04001C79 RID: 7289
		private bool isDismissed;
	}
}
