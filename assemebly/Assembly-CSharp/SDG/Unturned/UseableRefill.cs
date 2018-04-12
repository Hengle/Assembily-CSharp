using System;
using SDG.Framework.Water;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007BD RID: 1981
	public class UseableRefill : Useable
	{
		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x060039E6 RID: 14822 RVA: 0x001BA3AC File Offset: 0x001B87AC
		private bool isUseable
		{
			get
			{
				if (this.refillMode == ERefillMode.USE)
				{
					return Time.realtimeSinceStartup - this.startedUse > this.useTime;
				}
				return this.refillMode == ERefillMode.REFILL && Time.realtimeSinceStartup - this.startedUse > this.refillTime;
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x060039E7 RID: 14823 RVA: 0x001BA3FC File Offset: 0x001B87FC
		private ERefillWaterType waterType
		{
			get
			{
				if (base.player.equipment.state != null && base.player.equipment.state.Length > 0)
				{
					return (ERefillWaterType)base.player.equipment.state[0];
				}
				return ERefillWaterType.EMPTY;
			}
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x001BA44C File Offset: 0x001B884C
		private void use()
		{
			this.startedUse = Time.realtimeSinceStartup;
			this.isUsing = true;
			base.player.animator.play("Use", false);
			if (!Dedicator.isDedicated)
			{
				base.player.playSound(((ItemRefillAsset)base.player.equipment.asset).use);
			}
			if (Provider.isServer)
			{
				AlertTool.alert(base.transform.position, 8f);
			}
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x001BA4CF File Offset: 0x001B88CF
		[SteamCall]
		public void askUse(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.use();
			}
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x001BA4FD File Offset: 0x001B88FD
		private void refill()
		{
			this.startedUse = Time.realtimeSinceStartup;
			this.isUsing = true;
			base.player.animator.play("Refill", false);
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x001BA527 File Offset: 0x001B8927
		[SteamCall]
		public void askRefill(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.refill();
			}
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x001BA558 File Offset: 0x001B8958
		private bool fire(bool mode, out ERefillWaterType newWaterType)
		{
			newWaterType = ERefillWaterType.EMPTY;
			if (base.channel.isOwner)
			{
				Ray ray = new Ray(base.player.look.aim.position, base.player.look.aim.forward);
				RaycastInfo raycastInfo = DamageTool.raycast(ray, 3f, RayMasks.DAMAGE_CLIENT);
				if (!(raycastInfo.transform != null))
				{
					return false;
				}
				InteractableRainBarrel component = raycastInfo.transform.GetComponent<InteractableRainBarrel>();
				InteractableTank component2 = raycastInfo.transform.GetComponent<InteractableTank>();
				InteractableObjectResource component3 = raycastInfo.transform.GetComponent<InteractableObjectResource>();
				WaterVolume waterVolume;
				if (WaterUtility.isPointUnderwater(raycastInfo.point, out waterVolume))
				{
					if (mode)
					{
						return false;
					}
					if (this.waterType != ERefillWaterType.EMPTY)
					{
						return false;
					}
					if (waterVolume == null)
					{
						newWaterType = ERefillWaterType.SALTY;
					}
					else
					{
						newWaterType = waterVolume.waterType;
					}
				}
				else if (component != null)
				{
					if (mode)
					{
						if (this.waterType != ERefillWaterType.CLEAN)
						{
							return false;
						}
						if (component.isFull)
						{
							return false;
						}
						newWaterType = ERefillWaterType.EMPTY;
					}
					else
					{
						if (this.waterType == ERefillWaterType.CLEAN)
						{
							return false;
						}
						if (!component.isFull)
						{
							return false;
						}
						newWaterType = ERefillWaterType.CLEAN;
					}
				}
				else if (component2 != null)
				{
					if (component2.source != ETankSource.WATER)
					{
						return false;
					}
					if (mode)
					{
						if (this.waterType != ERefillWaterType.CLEAN)
						{
							return false;
						}
						if (component2.amount == component2.capacity)
						{
							return false;
						}
						newWaterType = ERefillWaterType.EMPTY;
					}
					else
					{
						if (this.waterType == ERefillWaterType.CLEAN)
						{
							return false;
						}
						if (component2.amount == 0)
						{
							return false;
						}
						newWaterType = ERefillWaterType.CLEAN;
					}
				}
				else
				{
					if (!(component3 != null))
					{
						return false;
					}
					if (component3.objectAsset.interactability != EObjectInteractability.WATER)
					{
						return false;
					}
					if (mode)
					{
						if (this.waterType == ERefillWaterType.EMPTY)
						{
							return false;
						}
						if (component3.amount == component3.capacity)
						{
							return false;
						}
						newWaterType = ERefillWaterType.EMPTY;
					}
					else
					{
						if (this.waterType == ERefillWaterType.CLEAN || this.waterType == ERefillWaterType.DIRTY)
						{
							return false;
						}
						if (component3.amount == 0)
						{
							return false;
						}
						newWaterType = ERefillWaterType.DIRTY;
					}
				}
				base.player.input.sendRaycast(raycastInfo);
			}
			if (Provider.isServer)
			{
				if (!base.player.input.hasInputs())
				{
					return false;
				}
				InputInfo input = base.player.input.getInput(true);
				if (input == null)
				{
					return false;
				}
				if ((input.point - base.player.look.aim.position).sqrMagnitude > 49f)
				{
					return false;
				}
				WaterVolume waterVolume2;
				if (WaterUtility.isPointUnderwater(input.point, out waterVolume2))
				{
					if (mode)
					{
						return false;
					}
					if (this.waterType != ERefillWaterType.EMPTY)
					{
						return false;
					}
					if (waterVolume2 == null)
					{
						newWaterType = ERefillWaterType.SALTY;
					}
					else
					{
						newWaterType = waterVolume2.waterType;
					}
				}
				else if (input.type == ERaycastInfoType.BARRICADE)
				{
					if (input.transform == null || !input.transform.CompareTag("Barricade"))
					{
						return false;
					}
					InteractableRainBarrel component4 = input.transform.GetComponent<InteractableRainBarrel>();
					InteractableTank component5 = input.transform.GetComponent<InteractableTank>();
					if (component4 != null)
					{
						if (mode)
						{
							if (this.waterType != ERefillWaterType.CLEAN)
							{
								return false;
							}
							if (component4.isFull)
							{
								return false;
							}
							BarricadeManager.updateRainBarrel(component4.transform, true, true);
							newWaterType = ERefillWaterType.EMPTY;
						}
						else
						{
							if (this.waterType == ERefillWaterType.CLEAN)
							{
								return false;
							}
							if (!component4.isFull)
							{
								return false;
							}
							BarricadeManager.updateRainBarrel(component4.transform, false, true);
							newWaterType = ERefillWaterType.CLEAN;
						}
					}
					else
					{
						if (!(component5 != null))
						{
							return false;
						}
						if (component5.source != ETankSource.WATER)
						{
							return false;
						}
						if (mode)
						{
							if (this.waterType != ERefillWaterType.CLEAN)
							{
								return false;
							}
							if (component5.amount == component5.capacity)
							{
								return false;
							}
							BarricadeManager.updateTank(input.transform, (ushort)((byte)(component5.amount + 1)));
							newWaterType = ERefillWaterType.EMPTY;
						}
						else
						{
							if (this.waterType == ERefillWaterType.CLEAN)
							{
								return false;
							}
							if (component5.amount == 0)
							{
								return false;
							}
							BarricadeManager.updateTank(input.transform, (ushort)((byte)(component5.amount - 1)));
							newWaterType = ERefillWaterType.CLEAN;
						}
					}
				}
				else if (input.type == ERaycastInfoType.OBJECT)
				{
					if (input.transform == null)
					{
						return false;
					}
					InteractableObjectResource component6 = input.transform.GetComponent<InteractableObjectResource>();
					if (component6 == null || component6.objectAsset.interactability != EObjectInteractability.WATER)
					{
						return false;
					}
					if (mode)
					{
						if (this.waterType == ERefillWaterType.EMPTY)
						{
							return false;
						}
						if (component6.amount == component6.capacity)
						{
							return false;
						}
						ObjectManager.updateObjectResource(component6.transform, (ushort)((byte)(component6.amount + 1)), true);
						newWaterType = ERefillWaterType.EMPTY;
					}
					else
					{
						if (this.waterType == ERefillWaterType.CLEAN || this.waterType == ERefillWaterType.DIRTY)
						{
							return false;
						}
						if (component6.amount == 0)
						{
							return false;
						}
						ObjectManager.updateObjectResource(component6.transform, (ushort)((byte)(component6.amount - 1)), true);
						newWaterType = ERefillWaterType.DIRTY;
					}
				}
			}
			return true;
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x001BAAA0 File Offset: 0x001B8EA0
		private void msg()
		{
			EPlayerMessage message;
			switch (this.waterType)
			{
			case ERefillWaterType.EMPTY:
				message = EPlayerMessage.EMPTY;
				break;
			case ERefillWaterType.CLEAN:
				message = EPlayerMessage.CLEAN;
				break;
			case ERefillWaterType.SALTY:
				message = EPlayerMessage.SALTY;
				break;
			case ERefillWaterType.DIRTY:
				message = EPlayerMessage.DIRTY;
				break;
			default:
				message = EPlayerMessage.FULL;
				break;
			}
			PlayerUI.message(message, string.Empty);
		}

		// Token: 0x060039EE RID: 14830 RVA: 0x001BAB04 File Offset: 0x001B8F04
		private void start(ERefillWaterType newWaterType)
		{
			base.player.equipment.isBusy = true;
			this.startedUse = Time.realtimeSinceStartup;
			this.isUsing = true;
			this.refillMode = ERefillMode.REFILL;
			this.refill();
			base.player.equipment.quality = ((newWaterType != ERefillWaterType.DIRTY) ? 100 : 0);
			base.player.equipment.updateQuality();
			base.player.equipment.state[0] = (byte)newWaterType;
			base.player.equipment.updateState();
			if (base.channel.isOwner)
			{
				this.msg();
			}
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x001BABAC File Offset: 0x001B8FAC
		public override void startPrimary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			if (this.isUseable)
			{
				ERefillWaterType erefillWaterType;
				if (this.fire(true, out erefillWaterType))
				{
					this.start(erefillWaterType);
				}
				else if (this.waterType != ERefillWaterType.EMPTY)
				{
					base.player.equipment.isBusy = true;
					this.startedUse = Time.realtimeSinceStartup;
					this.isUsing = true;
					this.refillMode = ERefillMode.USE;
					this.refillWaterType = this.waterType;
					this.use();
					base.player.equipment.quality = ((erefillWaterType != ERefillWaterType.DIRTY) ? 100 : 0);
					base.player.equipment.updateQuality();
					base.player.equipment.state[0] = 0;
					base.player.equipment.updateState();
					if (Provider.isServer)
					{
						base.channel.send("askUse", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
					}
					if (base.channel.isOwner)
					{
						this.msg();
					}
				}
			}
		}

		// Token: 0x060039F0 RID: 14832 RVA: 0x001BACC8 File Offset: 0x001B90C8
		public override void startSecondary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			ERefillWaterType newWaterType;
			if (this.isUseable && this.fire(false, out newWaterType))
			{
				this.start(newWaterType);
			}
		}

		// Token: 0x060039F1 RID: 14833 RVA: 0x001BAD0C File Offset: 0x001B910C
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			this.useTime = base.player.animator.getAnimationLength("Use");
			this.refillTime = base.player.animator.getAnimationLength("Refill");
			if (base.channel.isOwner)
			{
				this.msg();
			}
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x001BAD7C File Offset: 0x001B917C
		public override void simulate(uint simulation, bool inputSteady)
		{
			if (this.isUsing && this.isUseable)
			{
				base.player.equipment.isBusy = false;
				this.isUsing = false;
				if (this.refillMode == ERefillMode.USE && Provider.isServer)
				{
					float num;
					switch (this.refillWaterType)
					{
					case ERefillWaterType.CLEAN:
						num = 1f;
						break;
					case ERefillWaterType.SALTY:
						num = 0.25f;
						break;
					case ERefillWaterType.DIRTY:
						num = 0.6f;
						break;
					default:
						num = 0f;
						break;
					}
					base.player.life.askDrink((byte)((float)((ItemRefillAsset)base.player.equipment.asset).water * num));
					base.player.life.askInfect((byte)((float)((ItemRefillAsset)base.player.equipment.asset).water * (1f - num)));
				}
			}
		}

		// Token: 0x04002CE2 RID: 11490
		private float startedUse;

		// Token: 0x04002CE3 RID: 11491
		private float useTime;

		// Token: 0x04002CE4 RID: 11492
		private float refillTime;

		// Token: 0x04002CE5 RID: 11493
		private bool isUsing;

		// Token: 0x04002CE6 RID: 11494
		private ERefillMode refillMode;

		// Token: 0x04002CE7 RID: 11495
		private ERefillWaterType refillWaterType;
	}
}
