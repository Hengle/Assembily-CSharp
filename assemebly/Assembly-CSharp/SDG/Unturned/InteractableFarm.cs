using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004CD RID: 1229
	public class InteractableFarm : Interactable
	{
		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x000B4001 File Offset: 0x000B2401
		public uint planted
		{
			get
			{
				return this._planted;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060020DE RID: 8414 RVA: 0x000B4009 File Offset: 0x000B2409
		public uint growth
		{
			get
			{
				return this._growth;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x000B4011 File Offset: 0x000B2411
		public ushort grow
		{
			get
			{
				return this._grow;
			}
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x000B4019 File Offset: 0x000B2419
		public void updatePlanted(uint newPlanted)
		{
			this._planted = newPlanted;
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x000B4022 File Offset: 0x000B2422
		public override void updateState(Asset asset, byte[] state)
		{
			this._growth = ((ItemFarmAsset)asset).growth;
			this._grow = ((ItemFarmAsset)asset).grow;
			this._planted = BitConverter.ToUInt32(state, 0);
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x000B4053 File Offset: 0x000B2453
		public bool checkFarm()
		{
			return this.planted > 0u && Provider.time > this.planted && Provider.time - this.planted > this.growth;
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x000B4088 File Offset: 0x000B2488
		public override bool checkUseable()
		{
			return this.checkFarm();
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x000B4090 File Offset: 0x000B2490
		public override void use()
		{
			BarricadeManager.farm(base.transform);
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000B409D File Offset: 0x000B249D
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			if (this.checkUseable())
			{
				message = EPlayerMessage.FARM;
			}
			else
			{
				message = EPlayerMessage.GROW;
			}
			text = string.Empty;
			color = Color.white;
			return true;
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000B40CC File Offset: 0x000B24CC
		private void onRainUpdated(ELightingRain rain)
		{
			if (rain != ELightingRain.POST_DRIZZLE)
			{
				return;
			}
			if (Physics.Raycast(base.transform.position + Vector3.up, Vector3.up, 32f, RayMasks.BLOCK_WIND))
			{
				return;
			}
			this.updatePlanted(1u);
			if (Provider.isServer)
			{
				BarricadeManager.updateFarm(base.transform, this.planted, false);
			}
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000B4134 File Offset: 0x000B2534
		private void Update()
		{
			if (!Dedicator.isDedicated && !this.isGrown && this.checkFarm())
			{
				this.isGrown = true;
				Transform transform = base.transform.FindChild("Foliage_0");
				if (transform != null)
				{
					transform.gameObject.SetActive(false);
				}
				Transform transform2 = base.transform.FindChild("Foliage_1");
				if (transform2 != null)
				{
					transform2.gameObject.SetActive(true);
				}
			}
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000B41BA File Offset: 0x000B25BA
		private void OnEnable()
		{
			LightingManager.onRainUpdated = (RainUpdated)Delegate.Combine(LightingManager.onRainUpdated, new RainUpdated(this.onRainUpdated));
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000B41DC File Offset: 0x000B25DC
		private void OnDisable()
		{
			LightingManager.onRainUpdated = (RainUpdated)Delegate.Remove(LightingManager.onRainUpdated, new RainUpdated(this.onRainUpdated));
		}

		// Token: 0x04001395 RID: 5013
		private uint _planted;

		// Token: 0x04001396 RID: 5014
		private uint _growth;

		// Token: 0x04001397 RID: 5015
		private ushort _grow;

		// Token: 0x04001398 RID: 5016
		private bool isGrown;
	}
}
