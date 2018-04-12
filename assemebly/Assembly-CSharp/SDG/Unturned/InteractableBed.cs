using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004C7 RID: 1223
	public class InteractableBed : Interactable
	{
		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x000B3476 File Offset: 0x000B1876
		public CSteamID owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x000B347E File Offset: 0x000B187E
		public bool isClaimed
		{
			get
			{
				return this.owner != CSteamID.Nil;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x000B3490 File Offset: 0x000B1890
		public bool isClaimable
		{
			get
			{
				return Time.realtimeSinceStartup - this.claimed > 0.75f;
			}
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x000B34A5 File Offset: 0x000B18A5
		public bool checkClaim(CSteamID enemy)
		{
			return (Provider.isServer && !Dedicator.isDedicated) || !this.isClaimed || enemy == this.owner;
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x000B34D6 File Offset: 0x000B18D6
		public void updateClaim(CSteamID newOwner)
		{
			this.claimed = Time.realtimeSinceStartup;
			this._owner = newOwner;
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x000B34EA File Offset: 0x000B18EA
		public override void updateState(Asset asset, byte[] state)
		{
			this._owner = new CSteamID(BitConverter.ToUInt64(state, 0));
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x000B34FE File Offset: 0x000B18FE
		public override bool checkUseable()
		{
			return this.checkClaim(Provider.client);
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x000B350B File Offset: 0x000B190B
		public override void use()
		{
			BarricadeManager.claimBed(base.transform);
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x000B3518 File Offset: 0x000B1918
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			text = string.Empty;
			color = Color.white;
			if (this.checkUseable())
			{
				if (this.isClaimed)
				{
					message = EPlayerMessage.BED_OFF;
				}
				else
				{
					message = EPlayerMessage.BED_ON;
				}
			}
			else
			{
				message = EPlayerMessage.BED_CLAIMED;
			}
			return true;
		}

		// Token: 0x04001376 RID: 4982
		private CSteamID _owner;

		// Token: 0x04001377 RID: 4983
		private float claimed;
	}
}
