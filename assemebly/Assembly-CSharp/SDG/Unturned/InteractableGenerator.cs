using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004D0 RID: 1232
	public class InteractableGenerator : Interactable, IManualOnDestroy
	{
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060020F6 RID: 8438 RVA: 0x000B4374 File Offset: 0x000B2774
		public ushort capacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x000B437C File Offset: 0x000B277C
		public float wirerange
		{
			get
			{
				return this._wirerange;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x000B4384 File Offset: 0x000B2784
		public float sqrWirerange
		{
			get
			{
				return this._sqrWirerange;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x000B438C File Offset: 0x000B278C
		public bool isRefillable
		{
			get
			{
				return this.fuel < this.capacity;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060020FA RID: 8442 RVA: 0x000B439C File Offset: 0x000B279C
		public bool isSiphonable
		{
			get
			{
				return this.fuel > 0;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x000B43A7 File Offset: 0x000B27A7
		public bool isPowered
		{
			get
			{
				return this._isPowered;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060020FC RID: 8444 RVA: 0x000B43AF File Offset: 0x000B27AF
		public ushort fuel
		{
			get
			{
				return this._fuel;
			}
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x000B43B7 File Offset: 0x000B27B7
		public void askBurn(ushort amount)
		{
			if (amount == 0)
			{
				return;
			}
			if (amount >= this.fuel)
			{
				this._fuel = 0;
			}
			else
			{
				this._fuel -= amount;
			}
			if (Provider.isServer)
			{
				this.updateState();
			}
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x000B43F8 File Offset: 0x000B27F8
		public void askFill(ushort amount)
		{
			if (amount == 0)
			{
				return;
			}
			if (amount >= this.capacity - this.fuel)
			{
				this._fuel = this.capacity;
			}
			else
			{
				this._fuel += amount;
			}
			if (Provider.isServer)
			{
				this.updateState();
			}
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000B444F File Offset: 0x000B284F
		public void tellFuel(ushort newFuel)
		{
			this._fuel = newFuel;
			this.updateWire();
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000B445E File Offset: 0x000B285E
		public void updatePowered(bool newPowered)
		{
			this._isPowered = newPowered;
			this.updateWire();
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000B4470 File Offset: 0x000B2870
		public override void updateState(Asset asset, byte[] state)
		{
			this._capacity = ((ItemGeneratorAsset)asset).capacity;
			this._wirerange = ((ItemGeneratorAsset)asset).wirerange;
			this._sqrWirerange = this.wirerange * this.wirerange;
			this.burn = ((ItemGeneratorAsset)asset).burn;
			this._isPowered = (state[0] == 1);
			this._fuel = BitConverter.ToUInt16(state, 1);
			if (!Dedicator.isDedicated)
			{
				this.engine = base.transform.FindChild("Engine");
			}
			if (Provider.isServer)
			{
				this.metadata = state;
			}
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x000B450D File Offset: 0x000B290D
		public override void use()
		{
			BarricadeManager.toggleGenerator(base.transform);
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x000B451A File Offset: 0x000B291A
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			if (this.isPowered)
			{
				message = EPlayerMessage.GENERATOR_OFF;
			}
			else
			{
				message = EPlayerMessage.GENERATOR_ON;
			}
			text = string.Empty;
			color = Color.white;
			return true;
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x000B4547 File Offset: 0x000B2947
		private void updateState()
		{
			if (this.metadata == null)
			{
				return;
			}
			BitConverter.GetBytes(this.fuel).CopyTo(this.metadata, 1);
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x000B456C File Offset: 0x000B296C
		private void updateWire()
		{
			if (this.engine != null)
			{
				this.engine.gameObject.SetActive(this.isPowered && this.fuel > 0);
			}
			ushort maxValue = ushort.MaxValue;
			if (base.isPlant)
			{
				byte b;
				byte b2;
				BarricadeRegion barricadeRegion;
				BarricadeManager.tryGetPlant(base.transform.parent, out b, out b2, out maxValue, out barricadeRegion);
			}
			List<InteractablePower> list = PowerTool.checkPower(base.transform.position, this.wirerange, maxValue);
			for (int i = 0; i < list.Count; i++)
			{
				InteractablePower interactablePower = list[i];
				if (interactablePower.isWired)
				{
					if (!this.isPowered || this.fuel == 0)
					{
						bool flag = false;
						List<InteractableGenerator> list2 = PowerTool.checkGenerators(interactablePower.transform.position, 64f, maxValue);
						for (int j = 0; j < list2.Count; j++)
						{
							if (list2[j] != this && list2[j].isPowered && list2[j].fuel > 0 && (list2[j].transform.position - interactablePower.transform.position).sqrMagnitude < list2[j].sqrWirerange)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							interactablePower.updateWired(false);
						}
					}
				}
				else if (this.isPowered && this.fuel > 0)
				{
					interactablePower.updateWired(true);
				}
			}
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x000B472B File Offset: 0x000B2B2B
		public void ManualOnDestroy()
		{
			this.updatePowered(false);
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x000B4734 File Offset: 0x000B2B34
		private void Start()
		{
			this.updateWire();
			this.lastBurn = Time.realtimeSinceStartup;
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x000B4748 File Offset: 0x000B2B48
		private void Update()
		{
			if (Time.realtimeSinceStartup - this.lastBurn > this.burn)
			{
				this.lastBurn = Time.realtimeSinceStartup;
				if (this.isPowered)
				{
					if (this.fuel > 0)
					{
						this.isWiring = true;
						this.askBurn(1);
					}
					else if (this.isWiring)
					{
						this.isWiring = false;
						this.updateWire();
					}
				}
			}
		}

		// Token: 0x0400139C RID: 5020
		private ushort _capacity;

		// Token: 0x0400139D RID: 5021
		private float _wirerange;

		// Token: 0x0400139E RID: 5022
		private float _sqrWirerange;

		// Token: 0x0400139F RID: 5023
		private float burn;

		// Token: 0x040013A0 RID: 5024
		private bool _isPowered;

		// Token: 0x040013A1 RID: 5025
		private ushort _fuel;

		// Token: 0x040013A2 RID: 5026
		private Transform engine;

		// Token: 0x040013A3 RID: 5027
		private float lastBurn;

		// Token: 0x040013A4 RID: 5028
		private bool isWiring;

		// Token: 0x040013A5 RID: 5029
		private byte[] metadata;
	}
}
