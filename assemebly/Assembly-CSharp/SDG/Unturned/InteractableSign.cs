using System;
using System.Text;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Unturned
{
	// Token: 0x020004E6 RID: 1254
	public class InteractableSign : Interactable
	{
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060021D8 RID: 8664 RVA: 0x000BB17B File Offset: 0x000B957B
		public CSteamID owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x000BB183 File Offset: 0x000B9583
		public CSteamID group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060021DA RID: 8666 RVA: 0x000BB18B File Offset: 0x000B958B
		public string text
		{
			get
			{
				return this._text;
			}
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000BB194 File Offset: 0x000B9594
		public bool checkUpdate(CSteamID enemyPlayer, CSteamID enemyGroup)
		{
			return (Provider.isServer && !Dedicator.isDedicated) || !this.isLocked || enemyPlayer == this.owner || (this.group != CSteamID.Nil && enemyGroup == this.group);
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x000BB1FC File Offset: 0x000B95FC
		public void updateText(string newText)
		{
			this._text = newText;
			string text = this.text;
			if (OptionsSettings.filter)
			{
				text = ChatManager.filter(text);
			}
			if (this.label_0 != null)
			{
				this.label_0.text = text;
			}
			if (this.label_1 != null)
			{
				this.label_1.text = text;
			}
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x000BB264 File Offset: 0x000B9664
		public override void updateState(Asset asset, byte[] state)
		{
			this.isLocked = ((ItemBarricadeAsset)asset).isLocked;
			if (!Dedicator.isDedicated)
			{
				Transform transform = base.transform.FindChild("Canvas");
				if (transform != null)
				{
					Transform transform2 = transform.FindChild("Label");
					if (transform2 != null)
					{
						this.label_0 = transform2.GetComponent<Text>();
						this.label_1 = null;
					}
					else
					{
						this.label_0 = transform.FindChild("Label_0").GetComponent<Text>();
						this.label_1 = transform.FindChild("Label_1").GetComponent<Text>();
					}
				}
			}
			this._owner = new CSteamID(BitConverter.ToUInt64(state, 0));
			this._group = new CSteamID(BitConverter.ToUInt64(state, 8));
			byte b = state[16];
			if (b > 0)
			{
				this.updateText(Encoding.UTF8.GetString(state, 17, (int)b));
			}
			else
			{
				this.updateText(string.Empty);
			}
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x000BB359 File Offset: 0x000B9759
		public override bool checkUseable()
		{
			return this.checkUpdate(Provider.client, Player.player.quests.groupID) && !PlayerUI.window.showCursor;
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x000BB38A File Offset: 0x000B978A
		public override void use()
		{
			PlayerBarricadeSignUI.open(this);
			PlayerLifeUI.close();
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x000BB397 File Offset: 0x000B9797
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

		// Token: 0x04001434 RID: 5172
		private CSteamID _owner;

		// Token: 0x04001435 RID: 5173
		private CSteamID _group;

		// Token: 0x04001436 RID: 5174
		private string _text;

		// Token: 0x04001437 RID: 5175
		private bool isLocked;

		// Token: 0x04001438 RID: 5176
		private Text label_0;

		// Token: 0x04001439 RID: 5177
		private Text label_1;
	}
}
