using System;
using SDG.Framework.Devkit;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007BE RID: 1982
	public class UseableStructure : Useable
	{
		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x060039F4 RID: 14836 RVA: 0x001BAE82 File Offset: 0x001B9282
		private bool isUseable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime;
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x060039F5 RID: 14837 RVA: 0x001BAE98 File Offset: 0x001B9298
		private bool isConstructable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime * 0.8f;
			}
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x001BAEB4 File Offset: 0x001B92B4
		[SteamCall]
		public void askStructure(CSteamID steamID, Vector3 newPoint, float newAngle)
		{
			if (base.channel.checkOwner(steamID))
			{
				if ((newPoint - base.player.look.aim.position).sqrMagnitude < 256f)
				{
					this.point = newPoint;
					this.angle = newAngle;
					this.isValid = this.checkClaims();
				}
				this.wasAsked = true;
			}
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x001BAF20 File Offset: 0x001B9320
		private bool checkOverlap(int count)
		{
			for (int i = 0; i < count; i++)
			{
				Collider collider = UseableStructure.checkColliders[i];
				if (!(collider == null) && !(collider.transform == null))
				{
					ushort id;
					if (collider.CompareTag("Structure") && ushort.TryParse(collider.name, out id))
					{
						ItemStructureAsset itemStructureAsset = Assets.find(EAssetType.ITEM, id) as ItemStructureAsset;
						if (itemStructureAsset != null && (itemStructureAsset.construct == EConstruct.FLOOR || itemStructureAsset.construct == EConstruct.FLOOR_POLY || itemStructureAsset.construct == EConstruct.ROOF || itemStructureAsset.construct == EConstruct.ROOF_POLY))
						{
							goto IL_9C;
						}
					}
					return true;
				}
				IL_9C:;
			}
			return false;
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x001BAFD5 File Offset: 0x001B93D5
		private bool check()
		{
			return this.checkSpace() && this.checkClaims();
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x001BAFF4 File Offset: 0x001B93F4
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
			if (!ClaimManager.checkCanBuild(this.point, base.channel.owner.playerID.steamID, base.player.quests.groupID, false))
			{
				if (base.channel.isOwner)
				{
					PlayerUI.hint(null, EPlayerMessage.CLAIM);
				}
				return false;
			}
			if (PlayerClipVolumeUtility.isPointInsideVolume(this.point))
			{
				if (base.channel.isOwner)
				{
					PlayerUI.hint(null, EPlayerMessage.BOUNDS);
				}
				return false;
			}
			if (!LevelPlayers.checkCanBuild(this.point))
			{
				if (base.channel.isOwner)
				{
					PlayerUI.hint(null, EPlayerMessage.SPAWN);
				}
				return false;
			}
			if (Dedicator.isDedicated)
			{
				this.boundsRotation = Quaternion.Euler(-90f, this.angle + (float)(this.rotate * 90), 0f);
			}
			else
			{
				this.boundsRotation = this.help.rotation;
			}
			if (Physics.OverlapBoxNonAlloc(this.point + this.boundsRotation * this.boundsCenter, this.boundsOverlap, UseableStructure.checkColliders, this.boundsRotation, RayMasks.BLOCK_CHAR_BUILDABLE_OVERLAP, QueryTriggerInteraction.Collide) > 0)
			{
				if (base.channel.isOwner)
				{
					PlayerUI.hint(null, EPlayerMessage.BLOCKED);
				}
				return false;
			}
			return true;
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x001BB190 File Offset: 0x001B9590
		private bool checkSpace()
		{
			this.angle = base.player.look.yaw;
			if (((ItemStructureAsset)base.player.equipment.asset).construct == EConstruct.FLOOR || ((ItemStructureAsset)base.player.equipment.asset).construct == EConstruct.FLOOR_POLY)
			{
				Physics.SphereCast(base.player.look.aim.position, 0.1f, base.player.look.aim.forward, out this.hit, ((ItemStructureAsset)base.player.equipment.asset).range, RayMasks.STRUCTURE_INTERACT);
				if (this.hit.transform != null)
				{
					if (this.hit.transform.CompareTag("Structure"))
					{
						ItemStructureAsset itemStructureAsset = (ItemStructureAsset)Assets.find(EAssetType.ITEM, ushort.Parse(this.hit.transform.name));
						if (itemStructureAsset == null || (itemStructureAsset.construct != EConstruct.FLOOR && itemStructureAsset.construct != EConstruct.FLOOR_POLY))
						{
							this.point = this.hit.point;
							if (base.channel.isOwner)
							{
								PlayerUI.hint(null, EPlayerMessage.ROOF);
							}
							return false;
						}
						float num = float.MaxValue;
						Transform transform = null;
						for (int i = 0; i < 4; i++)
						{
							Transform transform2 = this.hit.transform.FindChild("Side_" + i);
							if (transform2 == null)
							{
								break;
							}
							float sqrMagnitude = (this.hit.point - transform2.position).sqrMagnitude;
							if (sqrMagnitude < num)
							{
								num = sqrMagnitude;
								transform = transform2;
							}
						}
						if (!(transform != null))
						{
							this.point = this.hit.point;
							if (base.channel.isOwner)
							{
								PlayerUI.hint(null, EPlayerMessage.BLOCKED);
							}
							return false;
						}
						this.angle = transform.transform.rotation.eulerAngles.y;
						this.point = transform.transform.position + transform.transform.forward * StructureManager.WALL;
						Vector3 vector = this.point;
						if (((ItemStructureAsset)base.player.equipment.asset).construct == EConstruct.FLOOR_POLY)
						{
							vector -= transform.transform.forward;
						}
						if (Physics.OverlapSphereNonAlloc(vector, 1f, UseableStructure.checkColliders, RayMasks.BLOCK_STRUCTURE) > 0)
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
						this.point = this.hit.point;
						if (!this.hit.transform.CompareTag("Ground") || this.hit.normal.y <= 0.5f)
						{
							if (base.channel.isOwner)
							{
								PlayerUI.hint(null, EPlayerMessage.GROUND);
							}
							return false;
						}
						if (!Level.checkSafe(this.point))
						{
							PlayerUI.hint(null, EPlayerMessage.BOUNDS);
							return false;
						}
						if (Physics.OverlapSphereNonAlloc(this.point, 1f, UseableStructure.checkColliders, RayMasks.BLOCK_STRUCTURE) > 0)
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
				this.point = Vector3.zero;
				return false;
			}
			else if (((ItemStructureAsset)base.player.equipment.asset).construct == EConstruct.WALL || ((ItemStructureAsset)base.player.equipment.asset).construct == EConstruct.RAMPART)
			{
				Physics.SphereCast(base.player.look.aim.position, 0.1f, base.player.look.aim.forward, out this.hit, ((ItemStructureAsset)base.player.equipment.asset).range, RayMasks.WALLS_INTERACT);
				if (!(this.hit.transform != null))
				{
					this.point = Vector3.zero;
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.WALL);
					}
					return false;
				}
				if (!this.hit.transform.CompareTag("Logic") || this.hit.transform.name.IndexOf("Wall") <= -1)
				{
					this.point = Vector3.zero;
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.WALL);
					}
					return false;
				}
				this.point = this.hit.transform.position;
				this.angle = this.hit.transform.rotation.eulerAngles.y;
				if (((ItemStructureAsset)base.player.equipment.asset).construct == EConstruct.RAMPART)
				{
					this.point += Vector3.down * 1.225f;
				}
				if (this.checkOverlap(Physics.OverlapSphereNonAlloc(this.hit.transform.position, 0.33f, UseableStructure.checkColliders, RayMasks.BLOCK_STRUCTURE)))
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
				if (this.checkOverlap(Physics.OverlapSphereNonAlloc(this.hit.transform.position + Vector3.up * 1.5f, 0.33f, UseableStructure.checkColliders, RayMasks.BLOCK_STRUCTURE)))
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
				if (this.checkOverlap(Physics.OverlapSphereNonAlloc(this.hit.transform.position - Vector3.up * 1.5f, 0.33f, UseableStructure.checkColliders, RayMasks.BLOCK_STRUCTURE)))
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
				if (Physics.OverlapSphereNonAlloc(this.point - this.hit.transform.up * StructureManager.WALL, 0.01f, UseableStructure.checkColliders, RayMasks.STRUCTURE) == 0)
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, (((ItemStructureAsset)base.player.equipment.asset).construct != EConstruct.RAMPART) ? EPlayerMessage.PILLAR : EPlayerMessage.POST);
					}
					return false;
				}
				if (Physics.OverlapSphereNonAlloc(this.point + this.hit.transform.up * StructureManager.WALL, 0.01f, UseableStructure.checkColliders, RayMasks.STRUCTURE) == 0)
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, (((ItemStructureAsset)base.player.equipment.asset).construct != EConstruct.RAMPART) ? EPlayerMessage.PILLAR : EPlayerMessage.POST);
					}
					return false;
				}
				return true;
			}
			else if (((ItemStructureAsset)base.player.equipment.asset).construct == EConstruct.ROOF || ((ItemStructureAsset)base.player.equipment.asset).construct == EConstruct.ROOF_POLY)
			{
				Physics.SphereCast(base.player.look.aim.position, 0.1f, base.player.look.aim.forward, out this.hit, ((ItemStructureAsset)base.player.equipment.asset).range, RayMasks.ROOFS_INTERACT);
				if (this.hit.transform != null)
				{
					if (this.hit.transform.CompareTag("Logic") && this.hit.transform.name.IndexOf("Roof") > -1)
					{
						this.point = this.hit.transform.position;
						this.angle = this.hit.transform.rotation.eulerAngles.y;
						for (int j = 0; j < 4; j++)
						{
							Transform transform3 = this.hit.transform.parent.FindChild("Pillar_" + j);
							if (transform3 == null)
							{
								break;
							}
							if (Physics.OverlapSphereNonAlloc(transform3.position, 0.01f, UseableStructure.checkColliders, RayMasks.STRUCTURE) == 0)
							{
								if (base.channel.isOwner)
								{
									PlayerUI.hint(null, EPlayerMessage.PILLAR);
								}
								return false;
							}
						}
						Vector3 vector2 = this.point;
						if (((ItemStructureAsset)base.player.equipment.asset).construct == EConstruct.ROOF_POLY)
						{
							vector2 += this.hit.transform.up;
						}
						if (Physics.OverlapSphereNonAlloc(vector2, 1f, UseableStructure.checkColliders, RayMasks.BLOCK_STRUCTURE) > 0)
						{
							if (base.channel.isOwner)
							{
								PlayerUI.hint(null, EPlayerMessage.BLOCKED);
							}
							return false;
						}
						if (((ItemStructureAsset)base.player.equipment.asset).construct == EConstruct.ROOF && Physics.OverlapSphereNonAlloc(vector2 + this.hit.transform.right * StructureManager.WALL * 0.5f, 0.33f, UseableStructure.checkColliders, RayMasks.BLOCK_STRUCTURE) > 0)
						{
							if (base.channel.isOwner)
							{
								PlayerUI.hint(null, EPlayerMessage.BLOCKED);
							}
							return false;
						}
						if (Physics.OverlapSphereNonAlloc(vector2 + Vector3.down * 2f, 0.33f, UseableStructure.checkColliders, RayMasks.BLOCK_STRUCTURE) > 0)
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
						if (!this.hit.transform.CompareTag("Structure"))
						{
							this.point = Vector3.zero;
							if (base.channel.isOwner)
							{
								PlayerUI.hint(null, EPlayerMessage.ROOF);
							}
							return false;
						}
						ItemStructureAsset itemStructureAsset2 = (ItemStructureAsset)Assets.find(EAssetType.ITEM, ushort.Parse(this.hit.transform.name));
						if (itemStructureAsset2 == null || (itemStructureAsset2.construct != EConstruct.FLOOR && itemStructureAsset2.construct != EConstruct.ROOF && itemStructureAsset2.construct != EConstruct.FLOOR_POLY && itemStructureAsset2.construct != EConstruct.ROOF_POLY))
						{
							this.point = Vector3.zero;
							if (base.channel.isOwner)
							{
								PlayerUI.hint(null, EPlayerMessage.ROOF);
							}
							return false;
						}
						float num2 = float.MaxValue;
						Transform transform4 = null;
						Vector3 vector3 = Vector3.zero;
						for (int k = 0; k < 4; k++)
						{
							Transform transform5 = this.hit.transform.FindChild("Side_" + k);
							if (transform5 == null)
							{
								break;
							}
							float sqrMagnitude2 = (this.hit.point - transform5.position).sqrMagnitude;
							if (sqrMagnitude2 < num2)
							{
								num2 = sqrMagnitude2;
								transform4 = transform5;
							}
						}
						if (transform4 != null)
						{
							this.angle = transform4.transform.rotation.eulerAngles.y;
							this.point = transform4.transform.position + transform4.transform.forward * StructureManager.WALL;
							vector3 = this.point;
							if (((ItemStructureAsset)base.player.equipment.asset).construct == EConstruct.ROOF_POLY)
							{
								vector3 -= transform4.transform.forward;
							}
						}
						else
						{
							this.point = Vector3.zero;
							if (base.channel.isOwner)
							{
								PlayerUI.hint(null, EPlayerMessage.ROOF);
							}
						}
						if (Physics.OverlapSphereNonAlloc(vector3, 1f, UseableStructure.checkColliders, RayMasks.BLOCK_STRUCTURE) > 0)
						{
							if (base.channel.isOwner)
							{
								PlayerUI.hint(null, EPlayerMessage.BLOCKED);
							}
							return false;
						}
						if (((ItemStructureAsset)base.player.equipment.asset).construct == EConstruct.ROOF && Physics.OverlapSphereNonAlloc(vector3 + this.hit.transform.right * StructureManager.WALL * 0.5f, 0.33f, UseableStructure.checkColliders, RayMasks.BLOCK_STRUCTURE) > 0)
						{
							if (base.channel.isOwner)
							{
								PlayerUI.hint(null, EPlayerMessage.BLOCKED);
							}
							return false;
						}
						if (Physics.OverlapSphereNonAlloc(vector3 + Vector3.down * 2f, 0.33f, UseableStructure.checkColliders, RayMasks.BLOCK_STRUCTURE) > 0)
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
				this.point = Vector3.zero;
				return false;
			}
			else
			{
				if (((ItemStructureAsset)base.player.equipment.asset).construct != EConstruct.PILLAR && ((ItemStructureAsset)base.player.equipment.asset).construct != EConstruct.POST)
				{
					this.point = Vector3.zero;
					return false;
				}
				Physics.SphereCast(base.player.look.aim.position, 0.1f, base.player.look.aim.forward, out this.hit, ((ItemStructureAsset)base.player.equipment.asset).range, RayMasks.CORNERS_INTERACT);
				if (!(this.hit.transform != null))
				{
					this.point = Vector3.zero;
					return false;
				}
				if (!this.hit.transform.CompareTag("Logic") || this.hit.transform.name.IndexOf("Pillar") <= -1)
				{
					this.point = Vector3.zero;
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.CORNER);
					}
					return false;
				}
				this.point = this.hit.transform.position;
				this.angle = this.hit.transform.rotation.eulerAngles.y;
				if (((ItemStructureAsset)base.player.equipment.asset).construct == EConstruct.POST)
				{
					this.point += Vector3.down * 1.225f;
				}
				if (this.checkOverlap(Physics.OverlapSphereNonAlloc(this.point, 0.01f, UseableStructure.checkColliders, RayMasks.BLOCK_STRUCTURE)))
				{
					if (base.channel.isOwner)
					{
						PlayerUI.hint(null, EPlayerMessage.BLOCKED);
					}
					return false;
				}
				return true;
			}
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x001BC106 File Offset: 0x001BA506
		private void construct()
		{
			this.startedUse = Time.realtimeSinceStartup;
			this.isUsing = true;
			this.isConstructing = true;
			base.player.animator.play("Use", false);
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x001BC137 File Offset: 0x001BA537
		[SteamCall]
		public void askConstruct(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.construct();
			}
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x001BC168 File Offset: 0x001BA568
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
					base.channel.send("askStructure", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						this.point,
						this.angle
					});
				}
				base.player.equipment.isBusy = true;
				this.construct();
				if (Provider.isServer)
				{
					base.channel.send("askConstruct", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
				}
			}
			else if (Dedicator.isDedicated && this.wasAsked)
			{
				base.player.equipment.dequip();
			}
		}

		// Token: 0x060039FE RID: 14846 RVA: 0x001BC270 File Offset: 0x001BA670
		public override void startSecondary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			if (((ItemStructureAsset)base.player.equipment.asset).construct == EConstruct.FLOOR || ((ItemStructureAsset)base.player.equipment.asset).construct == EConstruct.ROOF)
			{
				this.rotate += 1;
			}
			else
			{
				this.rotate += 2;
			}
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x001BC2F8 File Offset: 0x001BA6F8
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			this.useTime = base.player.animator.getAnimationLength("Use");
			if (Dedicator.isDedicated)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(((ItemStructureAsset)base.player.equipment.asset).clip, Vector3.zero, Quaternion.identity);
				gameObject.name = "Helper";
				Collider component = gameObject.GetComponent<Collider>();
				if (component != null)
				{
					this.boundsUse = true;
					this.boundsCenter = gameObject.transform.InverseTransformPoint(component.bounds.center);
					this.boundsExtents = component.bounds.extents;
				}
				UnityEngine.Object.Destroy(gameObject);
				this.boundsOverlap = this.boundsExtents + new Vector3(0.5f, 0.5f, 0.5f);
			}
			if (base.channel.isOwner)
			{
				this.help = StructureTool.getStructure(base.player.equipment.itemID, 0);
				this.help.position = Vector3.zero;
				this.help.rotation = Quaternion.identity;
				Collider component2 = this.help.GetComponent<Collider>();
				if (component2 != null)
				{
					this.boundsUse = true;
					this.boundsCenter = this.help.InverseTransformPoint(component2.bounds.center);
					this.boundsExtents = component2.bounds.extents;
					UnityEngine.Object.Destroy(component2);
				}
				this.boundsOverlap = this.boundsExtents + new Vector3(0.5f, 0.5f, 0.5f);
				this.help.rotation = Quaternion.Euler(-90f, 0f, 0f);
				HighlighterTool.help(this.help, this.isValid);
				if (this.help.FindChild("Clip") != null)
				{
					UnityEngine.Object.Destroy(this.help.FindChild("Clip").gameObject);
				}
				if (this.help.FindChild("Nav") != null)
				{
					UnityEngine.Object.Destroy(this.help.FindChild("Nav").gameObject);
				}
				if (this.help.FindChild("Cutter") != null)
				{
					UnityEngine.Object.Destroy(this.help.FindChild("Cutter").gameObject);
				}
				if (this.help.FindChild("Roof") != null)
				{
					UnityEngine.Object.Destroy(this.help.FindChild("Roof").gameObject);
				}
				if (this.help.FindChild("Block") != null)
				{
					UnityEngine.Object.Destroy(this.help.FindChild("Block").gameObject);
				}
				for (int i = 0; i < 4; i++)
				{
					if (!(this.help.FindChild("Wall") != null))
					{
						break;
					}
					UnityEngine.Object.Destroy(this.help.FindChild("Wall").gameObject);
				}
				for (int j = 0; j < 4; j++)
				{
					if (!(this.help.FindChild("Pillar") != null))
					{
						break;
					}
					UnityEngine.Object.Destroy(this.help.FindChild("Pillar").gameObject);
				}
			}
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x001BC6A0 File Offset: 0x001BAAA0
		public override void dequip()
		{
			if (base.channel.isOwner && this.help != null)
			{
				UnityEngine.Object.Destroy(this.help.gameObject);
			}
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x001BC6D4 File Offset: 0x001BAAD4
		public override void simulate(uint simulation, bool inputSteady)
		{
			if (this.isUsing && this.isUseable)
			{
				base.player.equipment.isBusy = false;
				if (Provider.isServer)
				{
					if (this.boundsUse && Physics.OverlapBoxNonAlloc(this.point + this.boundsRotation * this.boundsCenter, this.boundsOverlap, UseableStructure.checkColliders, this.boundsRotation, RayMasks.BLOCK_CHAR_BUILDABLE_OVERLAP, QueryTriggerInteraction.Collide) > 0)
					{
						base.player.equipment.dequip();
					}
					else
					{
						ItemStructureAsset itemStructureAsset = (ItemStructureAsset)base.player.equipment.asset;
						if (itemStructureAsset != null)
						{
							base.player.sendStat(EPlayerStat.FOUND_BUILDABLES);
							StructureManager.dropStructure(new Structure(base.player.equipment.itemID), this.point, 0f, this.angle + (float)(this.rotate * 90), 0f, base.channel.owner.playerID.steamID.m_SteamID, base.player.quests.groupID.m_SteamID);
						}
						base.player.equipment.use();
					}
				}
			}
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x001BC81C File Offset: 0x001BAC1C
		public override void tick()
		{
			if (this.isConstructing && this.isConstructable)
			{
				this.isConstructing = false;
				if (!Dedicator.isDedicated)
				{
					base.player.playSound(((ItemStructureAsset)base.player.equipment.asset).use);
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
				if (this.check())
				{
					if (!this.isValid)
					{
						this.isValid = true;
						HighlighterTool.help(this.help, this.isValid);
					}
				}
				else if (this.isValid)
				{
					this.isValid = false;
					HighlighterTool.help(this.help, this.isValid);
				}
				this.offset = Mathf.Lerp(this.offset, (float)(this.rotate * 90), 8f * Time.deltaTime);
				this.help.position = this.point;
				this.help.rotation = Quaternion.Euler(-90f, this.angle + this.offset, 0f);
			}
		}

		// Token: 0x04002CE8 RID: 11496
		private static Collider[] checkColliders = new Collider[4];

		// Token: 0x04002CE9 RID: 11497
		private Transform help;

		// Token: 0x04002CEA RID: 11498
		private bool boundsUse;

		// Token: 0x04002CEB RID: 11499
		private Vector3 boundsCenter;

		// Token: 0x04002CEC RID: 11500
		private Vector3 boundsExtents;

		// Token: 0x04002CED RID: 11501
		private Vector3 boundsOverlap;

		// Token: 0x04002CEE RID: 11502
		private Quaternion boundsRotation;

		// Token: 0x04002CEF RID: 11503
		private float startedUse;

		// Token: 0x04002CF0 RID: 11504
		private float useTime;

		// Token: 0x04002CF1 RID: 11505
		private bool isConstructing;

		// Token: 0x04002CF2 RID: 11506
		private bool isUsing;

		// Token: 0x04002CF3 RID: 11507
		private bool isValid;

		// Token: 0x04002CF4 RID: 11508
		private bool wasAsked;

		// Token: 0x04002CF5 RID: 11509
		private RaycastHit hit;

		// Token: 0x04002CF6 RID: 11510
		private Vector3 point;

		// Token: 0x04002CF7 RID: 11511
		private float angle;

		// Token: 0x04002CF8 RID: 11512
		private float offset;

		// Token: 0x04002CF9 RID: 11513
		private byte rotate;
	}
}
