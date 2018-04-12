using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000405 RID: 1029
	public class NPCItemReward : INPCReward
	{
		// Token: 0x06001BBB RID: 7099 RVA: 0x000987C8 File Offset: 0x00096BC8
		public NPCItemReward(ushort newID, byte newAmount, ushort newSight, ushort newTactical, ushort newGrip, ushort newBarrel, ushort newMagazine, byte newAmmo, string newText) : base(newText)
		{
			this.id = newID;
			this.amount = newAmount;
			this.sight = newSight;
			this.tactical = newTactical;
			this.grip = newGrip;
			this.barrel = newBarrel;
			this.magazine = newMagazine;
			this.ammo = newAmmo;
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x0009881A File Offset: 0x00096C1A
		// (set) Token: 0x06001BBD RID: 7101 RVA: 0x00098822 File Offset: 0x00096C22
		public ushort id { get; protected set; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x0009882B File Offset: 0x00096C2B
		// (set) Token: 0x06001BBF RID: 7103 RVA: 0x00098833 File Offset: 0x00096C33
		public byte amount { get; protected set; }

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x0009883C File Offset: 0x00096C3C
		// (set) Token: 0x06001BC1 RID: 7105 RVA: 0x00098844 File Offset: 0x00096C44
		public ushort sight { get; protected set; }

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x0009884D File Offset: 0x00096C4D
		// (set) Token: 0x06001BC3 RID: 7107 RVA: 0x00098855 File Offset: 0x00096C55
		public ushort tactical { get; protected set; }

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x0009885E File Offset: 0x00096C5E
		// (set) Token: 0x06001BC5 RID: 7109 RVA: 0x00098866 File Offset: 0x00096C66
		public ushort grip { get; protected set; }

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x0009886F File Offset: 0x00096C6F
		// (set) Token: 0x06001BC7 RID: 7111 RVA: 0x00098877 File Offset: 0x00096C77
		public ushort barrel { get; protected set; }

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x00098880 File Offset: 0x00096C80
		// (set) Token: 0x06001BC9 RID: 7113 RVA: 0x00098888 File Offset: 0x00096C88
		public ushort magazine { get; protected set; }

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x00098891 File Offset: 0x00096C91
		// (set) Token: 0x06001BCB RID: 7115 RVA: 0x00098899 File Offset: 0x00096C99
		public byte ammo { get; protected set; }

		// Token: 0x06001BCC RID: 7116 RVA: 0x000988A4 File Offset: 0x00096CA4
		public override void grantReward(Player player, bool shouldSend)
		{
			if (!Provider.isServer)
			{
				return;
			}
			Item item;
			if (this.sight > 0 || this.tactical > 0 || this.grip > 0 || this.barrel > 0 || this.magazine > 0)
			{
				ItemGunAsset itemGunAsset = Assets.find(EAssetType.ITEM, this.id) as ItemGunAsset;
				byte[] state = itemGunAsset.getState((this.sight <= 0) ? itemGunAsset.sightID : this.sight, (this.tactical <= 0) ? itemGunAsset.tacticalID : this.tactical, (this.grip <= 0) ? itemGunAsset.gripID : this.grip, (this.barrel <= 0) ? itemGunAsset.barrelID : this.barrel, (this.magazine <= 0) ? itemGunAsset.getMagazineID() : this.magazine, (this.ammo <= 0) ? itemGunAsset.ammoMax : this.ammo);
				item = new Item(this.id, 1, 100, state);
			}
			else
			{
				item = new Item(this.id, EItemOrigin.CRAFT);
			}
			for (byte b = 0; b < this.amount; b += 1)
			{
				player.inventory.forceAddItem(item, false, false);
			}
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x00098A08 File Offset: 0x00096E08
		public override string formatReward(Player player)
		{
			if (string.IsNullOrEmpty(this.text))
			{
				this.text = PlayerNPCQuestUI.localization.read("Reward_Item");
			}
			ItemAsset itemAsset = Assets.find(EAssetType.ITEM, this.id) as ItemAsset;
			string arg;
			if (itemAsset != null)
			{
				arg = string.Concat(new string[]
				{
					"<color=",
					Palette.hex(ItemTool.getRarityColorUI(itemAsset.rarity)),
					">",
					itemAsset.itemName,
					"</color>"
				});
			}
			else
			{
				arg = "?";
			}
			return string.Format(this.text, this.amount, arg);
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x00098ABC File Offset: 0x00096EBC
		public override Sleek createUI(Player player)
		{
			string text = this.formatReward(player);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, this.id);
			if (itemAsset == null)
			{
				return null;
			}
			SleekBox sleekBox = new SleekBox();
			if (itemAsset.size_y == 1)
			{
				sleekBox.sizeOffset_Y = (int)(itemAsset.size_y * 50 + 10);
			}
			else
			{
				sleekBox.sizeOffset_Y = (int)(itemAsset.size_y * 25 + 10);
			}
			sleekBox.sizeScale_X = 1f;
			SleekImageTexture sleekImageTexture = new SleekImageTexture();
			sleekImageTexture.positionOffset_X = 5;
			sleekImageTexture.positionOffset_Y = 5;
			if (itemAsset.size_y == 1)
			{
				sleekImageTexture.sizeOffset_X = (int)(itemAsset.size_x * 50);
				sleekImageTexture.sizeOffset_Y = (int)(itemAsset.size_y * 50);
			}
			else
			{
				sleekImageTexture.sizeOffset_X = (int)(itemAsset.size_x * 25);
				sleekImageTexture.sizeOffset_Y = (int)(itemAsset.size_y * 25);
			}
			sleekBox.add(sleekImageTexture);
			ItemTool.getIcon(this.id, 100, itemAsset.getState(false), itemAsset, sleekImageTexture.sizeOffset_X, sleekImageTexture.sizeOffset_Y, new ItemIconReady(sleekImageTexture.updateTexture));
			sleekBox.add(new SleekLabel
			{
				positionOffset_X = 10 + sleekImageTexture.sizeOffset_X,
				sizeOffset_X = -15 - sleekImageTexture.sizeOffset_X,
				sizeScale_X = 1f,
				sizeScale_Y = 1f,
				fontAlignment = TextAnchor.MiddleLeft,
				foregroundTint = ESleekTint.NONE,
				isRich = true,
				text = text
			});
			return sleekBox;
		}
	}
}
