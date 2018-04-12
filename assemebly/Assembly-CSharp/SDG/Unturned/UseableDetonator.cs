using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007B4 RID: 1972
	public class UseableDetonator : Useable
	{
		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06003955 RID: 14677 RVA: 0x001A95DC File Offset: 0x001A79DC
		private bool isUseable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06003956 RID: 14678 RVA: 0x001A95F2 File Offset: 0x001A79F2
		private bool isDetonatable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime * 0.33f;
			}
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x001A9610 File Offset: 0x001A7A10
		private void plunge()
		{
			this.startedUse = Time.realtimeSinceStartup;
			this.isUsing = true;
			base.player.animator.play("Use", false);
			if (!Dedicator.isDedicated)
			{
				base.player.playSound(((ItemDetonatorAsset)base.player.equipment.asset).use);
			}
			if (Provider.isServer)
			{
				AlertTool.alert(base.transform.position, 8f);
			}
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x001A9693 File Offset: 0x001A7A93
		[SteamCall]
		public void askPlunge(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.plunge();
			}
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x001A96C4 File Offset: 0x001A7AC4
		public override void startPrimary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			if (this.isUseable)
			{
				if (base.channel.isOwner)
				{
					for (int i = 0; i < this.charges.Count; i++)
					{
						InteractableCharge interactableCharge = this.charges[i];
						if (!(interactableCharge == null))
						{
							RaycastInfo info = new RaycastInfo(interactableCharge.transform);
							base.player.input.sendRaycast(info);
						}
					}
					this.charges.Clear();
				}
				if (Provider.isServer)
				{
					this.charges.Clear();
					if (base.player.input.hasInputs())
					{
						int inputCount = base.player.input.getInputCount();
						for (int j = 0; j < inputCount; j++)
						{
							InputInfo input = base.player.input.getInput(false);
							if (input != null)
							{
								if (input.type == ERaycastInfoType.BARRICADE)
								{
									if (!(input.transform == null) && input.transform.CompareTag("Barricade"))
									{
										InteractableCharge component = input.transform.GetComponent<InteractableCharge>();
										if (!(component == null))
										{
											if (!((!Dedicator.isDedicated) ? (!component.hasOwnership) : (!OwnershipTool.checkToggle(base.channel.owner.playerID.steamID, component.owner, base.player.quests.groupID, component.group))))
											{
												this.charges.Add(component);
											}
										}
									}
								}
							}
						}
					}
				}
				base.player.equipment.isBusy = true;
				this.startedUse = Time.realtimeSinceStartup;
				this.isUsing = true;
				this.isDetonating = true;
				this.plunge();
				if (Provider.isServer)
				{
					base.player.life.markAggressive(false, true);
					base.channel.send("askPlunge", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
				}
			}
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x001A9904 File Offset: 0x001A7D04
		public override void startSecondary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			if (base.channel.isOwner && !this.isUsing && this.target != null)
			{
				if (this.target.isSelected)
				{
					this.target.deselect();
					this.charges.Remove(this.target);
				}
				else
				{
					this.target.select();
					this.charges.Add(this.target);
					if (this.charges.Count > 8)
					{
						if (this.charges[0] != null)
						{
							this.charges[0].deselect();
						}
						this.charges.RemoveAt(0);
					}
				}
			}
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x001A99E8 File Offset: 0x001A7DE8
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			this.useTime = base.player.animator.getAnimationLength("Use");
			if (base.channel.isOwner)
			{
				this.chargePoint = Vector3.zero;
				this.foundInRadius = new List<InteractableCharge>();
				this.chargesInRadius = new List<InteractableCharge>();
			}
			this.charges = new List<InteractableCharge>();
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x001A9A64 File Offset: 0x001A7E64
		public override void dequip()
		{
			if (base.channel.isOwner)
			{
				for (int i = 0; i < this.chargesInRadius.Count; i++)
				{
					InteractableCharge interactableCharge = this.chargesInRadius[i];
					interactableCharge.unhighlight();
				}
			}
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x001A9AB0 File Offset: 0x001A7EB0
		public override void simulate(uint simulation, bool inputSteady)
		{
			if (this.isDetonating && this.isDetonatable)
			{
				if (this.charges.Count > 0)
				{
					if (simulation - this.lastExplosion > 1u)
					{
						this.lastExplosion = simulation;
						InteractableCharge interactableCharge = this.charges[0];
						this.charges.RemoveAt(0);
						if (interactableCharge != null)
						{
							interactableCharge.detonate(base.channel.owner.playerID.steamID);
						}
					}
				}
				else
				{
					this.isDetonating = false;
				}
			}
			if (this.isUsing && this.isUseable && this.charges.Count == 0)
			{
				base.player.equipment.isBusy = false;
				this.isUsing = false;
			}
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x001A9B84 File Offset: 0x001A7F84
		public override void tick()
		{
			if (base.channel.isOwner)
			{
				if ((base.transform.position - this.chargePoint).sqrMagnitude > 1f)
				{
					this.chargePoint = base.transform.position;
					this.foundInRadius.Clear();
					PowerTool.checkInteractables<InteractableCharge>(this.chargePoint, 64f, this.foundInRadius);
					for (int i = this.chargesInRadius.Count - 1; i >= 0; i--)
					{
						InteractableCharge interactableCharge = this.chargesInRadius[i];
						if (interactableCharge == null)
						{
							this.chargesInRadius.RemoveAtFast(i);
						}
						else if (!this.foundInRadius.Contains(interactableCharge))
						{
							interactableCharge.unhighlight();
							this.chargesInRadius.RemoveAtFast(i);
						}
					}
					for (int j = 0; j < this.foundInRadius.Count; j++)
					{
						InteractableCharge interactableCharge2 = this.foundInRadius[j];
						if (!(interactableCharge2 == null))
						{
							if (interactableCharge2.hasOwnership)
							{
								if (!this.chargesInRadius.Contains(interactableCharge2))
								{
									interactableCharge2.highlight();
									this.chargesInRadius.Add(interactableCharge2);
								}
							}
						}
					}
				}
				InteractableCharge x = null;
				float num = 0.98f;
				for (int k = 0; k < this.chargesInRadius.Count; k++)
				{
					InteractableCharge interactableCharge3 = this.chargesInRadius[k];
					if (!(interactableCharge3 == null))
					{
						float num2 = Vector3.Dot((interactableCharge3.transform.position - MainCamera.instance.transform.position).normalized, MainCamera.instance.transform.forward);
						if (num2 > num)
						{
							x = interactableCharge3;
							num = num2;
						}
					}
				}
				if (x != this.target)
				{
					if (this.target != null)
					{
						this.target.untarget();
					}
					this.target = x;
					if (this.target != null)
					{
						this.target.target();
					}
				}
			}
		}

		// Token: 0x04002C34 RID: 11316
		private float startedUse;

		// Token: 0x04002C35 RID: 11317
		private float useTime;

		// Token: 0x04002C36 RID: 11318
		private uint lastExplosion;

		// Token: 0x04002C37 RID: 11319
		private bool isUsing;

		// Token: 0x04002C38 RID: 11320
		private bool isDetonating;

		// Token: 0x04002C39 RID: 11321
		private Vector3 chargePoint;

		// Token: 0x04002C3A RID: 11322
		private List<InteractableCharge> foundInRadius;

		// Token: 0x04002C3B RID: 11323
		private List<InteractableCharge> chargesInRadius;

		// Token: 0x04002C3C RID: 11324
		private List<InteractableCharge> charges;

		// Token: 0x04002C3D RID: 11325
		private InteractableCharge target;
	}
}
