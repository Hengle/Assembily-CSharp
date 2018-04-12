using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004D2 RID: 1234
	public class InteractableLibrary : Interactable
	{
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06002111 RID: 8465 RVA: 0x000B48D9 File Offset: 0x000B2CD9
		public CSteamID owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06002112 RID: 8466 RVA: 0x000B48E1 File Offset: 0x000B2CE1
		public CSteamID group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06002113 RID: 8467 RVA: 0x000B48E9 File Offset: 0x000B2CE9
		public uint amount
		{
			get
			{
				return this._amount;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x000B48F1 File Offset: 0x000B2CF1
		public uint capacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x000B48F9 File Offset: 0x000B2CF9
		public byte tax
		{
			get
			{
				return this._tax;
			}
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x000B4904 File Offset: 0x000B2D04
		public bool checkTransfer(CSteamID enemyPlayer, CSteamID enemyGroup)
		{
			return (Provider.isServer && !Dedicator.isDedicated) || !this.isLocked || enemyPlayer == this.owner || (this.group != CSteamID.Nil && enemyGroup == this.group);
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x000B496A File Offset: 0x000B2D6A
		public void updateAmount(uint newAmount)
		{
			this._amount = newAmount;
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x000B4974 File Offset: 0x000B2D74
		public override void updateState(Asset asset, byte[] state)
		{
			this.isLocked = ((ItemBarricadeAsset)asset).isLocked;
			this._capacity = ((ItemLibraryAsset)asset).capacity;
			this._tax = ((ItemLibraryAsset)asset).tax;
			this._owner = new CSteamID(BitConverter.ToUInt64(state, 0));
			this._group = new CSteamID(BitConverter.ToUInt64(state, 8));
			this._amount = BitConverter.ToUInt32(state, 16);
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x000B49E6 File Offset: 0x000B2DE6
		public override bool checkUseable()
		{
			return this.checkTransfer(Provider.client, Player.player.quests.groupID) && !PlayerUI.window.showCursor;
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x000B4A17 File Offset: 0x000B2E17
		public override void use()
		{
			PlayerBarricadeLibraryUI.open(this);
			PlayerLifeUI.close();
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x000B4A24 File Offset: 0x000B2E24
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			if (this.checkUseable())
			{
				message = EPlayerMessage.USE;
			}
			else
			{
				message = EPlayerMessage.LOCKED;
			}
			text = string.Empty;
			color = Color.white;
			return !PlayerUI.window.showCursor;
		}

		// Token: 0x040013AA RID: 5034
		private CSteamID _owner;

		// Token: 0x040013AB RID: 5035
		private CSteamID _group;

		// Token: 0x040013AC RID: 5036
		private uint _amount;

		// Token: 0x040013AD RID: 5037
		private uint _capacity;

		// Token: 0x040013AE RID: 5038
		private byte _tax;

		// Token: 0x040013AF RID: 5039
		private bool isLocked;
	}
}
