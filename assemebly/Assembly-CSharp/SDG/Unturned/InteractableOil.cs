using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004DF RID: 1247
	public class InteractableOil : InteractablePower
	{
		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x000B7B5A File Offset: 0x000B5F5A
		public ushort fuel
		{
			get
			{
				return this._fuel;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06002196 RID: 8598 RVA: 0x000B7B62 File Offset: 0x000B5F62
		public ushort capacity
		{
			get
			{
				return 500;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x000B7B69 File Offset: 0x000B5F69
		public bool isRefillable
		{
			get
			{
				return this.fuel < this.capacity;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x000B7B79 File Offset: 0x000B5F79
		public bool isSiphonable
		{
			get
			{
				return this.fuel > 0;
			}
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x000B7B84 File Offset: 0x000B5F84
		public void tellFuel(ushort newFuel)
		{
			this._fuel = newFuel;
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x000B7B8D File Offset: 0x000B5F8D
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

		// Token: 0x0600219B RID: 8603 RVA: 0x000B7BD0 File Offset: 0x000B5FD0
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

		// Token: 0x0600219C RID: 8604 RVA: 0x000B7C28 File Offset: 0x000B6028
		protected override void updateWired()
		{
			if (this.engine != null)
			{
				this.engine.gameObject.SetActive(base.isWired);
			}
			if (this.root != null)
			{
				if (base.isWired)
				{
					this.root.Play();
					this.root["Drill"].time = UnityEngine.Random.Range(0f, this.root["Drill"].length);
				}
				else
				{
					this.root.Stop();
				}
			}
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x000B7CC8 File Offset: 0x000B60C8
		public override void updateState(Asset asset, byte[] state)
		{
			base.updateState(asset, state);
			this._fuel = BitConverter.ToUInt16(state, 0);
			if (!Dedicator.isDedicated)
			{
				this.engine = base.transform.FindChild("Engine");
				this.root = base.transform.FindChild("Root").GetComponent<Animation>();
			}
			if (Provider.isServer)
			{
				this.metadata = state;
			}
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x000B7D36 File Offset: 0x000B6136
		public override bool checkUseable()
		{
			return this.fuel > 0;
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x000B7D41 File Offset: 0x000B6141
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			message = EPlayerMessage.VOLUME_FUEL;
			text = string.Empty;
			color = Color.white;
			return true;
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x000B7D5A File Offset: 0x000B615A
		private void updateState()
		{
			if (this.metadata == null)
			{
				return;
			}
			BitConverter.GetBytes(this.fuel).CopyTo(this.metadata, 0);
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x000B7D80 File Offset: 0x000B6180
		private void Update()
		{
			if (!base.isWired)
			{
				this.lastDrilled = Time.realtimeSinceStartup;
				return;
			}
			if (Time.realtimeSinceStartup - this.lastDrilled > 2f)
			{
				this.lastDrilled = Time.realtimeSinceStartup;
				if (this.fuel < this.capacity)
				{
					this.askFill(1);
				}
			}
		}

		// Token: 0x04001400 RID: 5120
		private ushort _fuel;

		// Token: 0x04001401 RID: 5121
		private byte[] metadata;

		// Token: 0x04001402 RID: 5122
		private Transform engine;

		// Token: 0x04001403 RID: 5123
		private Animation root;

		// Token: 0x04001404 RID: 5124
		private float lastDrilled;
	}
}
