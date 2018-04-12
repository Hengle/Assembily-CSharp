﻿using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006E9 RID: 1769
	public class SleekItem : Sleek
	{
		// Token: 0x060032B3 RID: 12979 RVA: 0x00148D00 File Offset: 0x00147100
		public SleekItem(ItemJar jar)
		{
			base.init();
			this.button = new SleekButton();
			this.button.positionOffset_X = 1;
			this.button.positionOffset_Y = 1;
			this.button.sizeOffset_X = -2;
			this.button.sizeOffset_Y = -2;
			this.button.sizeScale_X = 1f;
			this.button.sizeScale_Y = 1f;
			this.button.backgroundTint = ESleekTint.NONE;
			this.button.foregroundTint = ESleekTint.NONE;
			this.button.onClickedButton = new ClickedButton(this.onClickedButton);
			base.add(this.button);
			this.icon = new SleekImageTexture();
			base.add(this.icon);
			this.icon.isVisible = false;
			this.icon.isAngled = true;
			this.amountLabel = new SleekLabel();
			this.amountLabel.positionScale_Y = 1f;
			this.amountLabel.sizeOffset_Y = 30;
			this.amountLabel.sizeScale_X = 1f;
			this.amountLabel.fontAlignment = TextAnchor.LowerLeft;
			this.amountLabel.foregroundTint = ESleekTint.NONE;
			base.add(this.amountLabel);
			this.amountLabel.isVisible = false;
			this.qualityImage = new SleekImageTexture();
			this.qualityImage.positionScale_X = 1f;
			this.qualityImage.positionScale_Y = 1f;
			base.add(this.qualityImage);
			this.qualityImage.isVisible = false;
			this.hotkeyLabel = new SleekLabel();
			this.hotkeyLabel.positionOffset_X = 5;
			this.hotkeyLabel.positionOffset_Y = 5;
			this.hotkeyLabel.sizeOffset_X = -10;
			this.hotkeyLabel.sizeOffset_Y = 30;
			this.hotkeyLabel.sizeScale_X = 1f;
			this.hotkeyLabel.fontAlignment = TextAnchor.UpperRight;
			base.add(this.hotkeyLabel);
			this.hotkeyLabel.isVisible = false;
			this.updateItem(jar);
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x00148F10 File Offset: 0x00147310
		public SleekItem()
		{
			base.init();
			this.button = new SleekButton();
			this.button.positionOffset_X = 1;
			this.button.positionOffset_Y = 1;
			this.button.sizeOffset_X = -2;
			this.button.sizeOffset_Y = -2;
			this.button.sizeScale_X = 1f;
			this.button.sizeScale_Y = 1f;
			this.button.backgroundTint = ESleekTint.NONE;
			this.button.foregroundTint = ESleekTint.NONE;
			base.add(this.button);
			this.icon = new SleekImageTexture();
			base.add(this.icon);
			this.icon.isVisible = false;
			this.icon.isAngled = true;
			this.amountLabel = new SleekLabel();
			this.amountLabel.positionScale_Y = 1f;
			this.amountLabel.sizeOffset_Y = 30;
			this.amountLabel.sizeScale_X = 1f;
			this.amountLabel.fontAlignment = TextAnchor.LowerLeft;
			this.amountLabel.foregroundTint = ESleekTint.NONE;
			base.add(this.amountLabel);
			this.amountLabel.isVisible = false;
			this.qualityImage = new SleekImageTexture();
			this.qualityImage.positionScale_X = 1f;
			this.qualityImage.positionScale_Y = 1f;
			base.add(this.qualityImage);
			this.qualityImage.isVisible = false;
			this.hotkeyLabel = new SleekLabel();
			this.hotkeyLabel.positionOffset_X = 5;
			this.hotkeyLabel.positionOffset_Y = 5;
			this.hotkeyLabel.sizeOffset_X = -10;
			this.hotkeyLabel.sizeOffset_Y = 30;
			this.hotkeyLabel.sizeScale_X = 1f;
			this.hotkeyLabel.fontAlignment = TextAnchor.UpperRight;
			base.add(this.hotkeyLabel);
			this.hotkeyLabel.isVisible = false;
			this.isTemporary = true;
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x060032B5 RID: 12981 RVA: 0x00149107 File Offset: 0x00147507
		public ItemJar jar
		{
			get
			{
				return this._jar;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x060032B6 RID: 12982 RVA: 0x0014910F File Offset: 0x0014750F
		public int hotkey
		{
			get
			{
				return (int)this._hotkey;
			}
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x00149118 File Offset: 0x00147518
		public void enable()
		{
			Color backgroundColor = this.button.backgroundColor;
			backgroundColor.a = 1f;
			this.button.backgroundColor = backgroundColor;
			Color backgroundColor2 = this.icon.backgroundColor;
			backgroundColor2.a = 1f;
			this.icon.backgroundColor = backgroundColor2;
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x00149170 File Offset: 0x00147570
		public void disable()
		{
			Color backgroundColor = this.button.backgroundColor;
			backgroundColor.a = 0.5f;
			this.button.backgroundColor = backgroundColor;
			Color backgroundColor2 = this.icon.backgroundColor;
			backgroundColor2.a = 0.5f;
			this.icon.backgroundColor = backgroundColor2;
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x001491C8 File Offset: 0x001475C8
		public void updateHotkey(byte index)
		{
			this._hotkey = index;
			if (this.hotkey == 255)
			{
				this.hotkeyLabel.text = string.Empty;
				this.hotkeyLabel.isVisible = false;
			}
			else
			{
				this.hotkeyLabel.text = (this.hotkey + 1).ToString();
				this.hotkeyLabel.isVisible = true;
			}
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x0014923C File Offset: 0x0014763C
		public void updateItem(ItemJar newJar)
		{
			this._jar = newJar;
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, this.jar.item.id);
			if (itemAsset != null)
			{
				if (!this.isTemporary)
				{
					this.button.tooltip = itemAsset.itemName;
				}
				if (this.jar.rot % 2 == 0)
				{
					base.sizeOffset_X = (int)(itemAsset.size_x * 50);
					base.sizeOffset_Y = (int)(itemAsset.size_y * 50);
					this.icon.positionOffset_X = 0;
					this.icon.positionOffset_Y = 0;
				}
				else
				{
					base.sizeOffset_X = (int)(itemAsset.size_y * 50);
					base.sizeOffset_Y = (int)(itemAsset.size_x * 50);
					int num = Mathf.Abs((int)(itemAsset.size_y - itemAsset.size_x));
					if (itemAsset.size_x > itemAsset.size_y)
					{
						this.icon.positionOffset_X = -num * 25;
						this.icon.positionOffset_Y = num * 25;
					}
					else
					{
						this.icon.positionOffset_X = num * 25;
						this.icon.positionOffset_Y = -num * 25;
					}
				}
				this.icon.angle = (float)(this.jar.rot * 90);
				this.icon.sizeOffset_X = (int)(itemAsset.size_x * 50);
				this.icon.sizeOffset_Y = (int)(itemAsset.size_y * 50);
				this.icon.isVisible = false;
				ItemTool.getIcon(this.jar.item.id, this.jar.item.quality, this.jar.item.state, itemAsset, new ItemIconReady(this.onItemIconReady));
				if (itemAsset.size_x == 1 || itemAsset.size_y == 1)
				{
					this.amountLabel.positionOffset_X = 0;
					this.amountLabel.positionOffset_Y = -30;
					this.amountLabel.sizeOffset_X = 0;
					this.amountLabel.fontSize = 10;
					this.hotkeyLabel.fontSize = 10;
				}
				else
				{
					this.amountLabel.positionOffset_X = 5;
					this.amountLabel.positionOffset_Y = -35;
					this.amountLabel.sizeOffset_X = -10;
					this.amountLabel.fontSize = 12;
					this.hotkeyLabel.fontSize = 12;
				}
				this.button.backgroundColor = ItemTool.getRarityColorUI(itemAsset.rarity);
				this.button.foregroundColor = this.button.backgroundColor;
				if (itemAsset.showQuality)
				{
					if (itemAsset.size_x == 1 || itemAsset.size_y == 1)
					{
						this.qualityImage.positionOffset_X = -15;
						this.qualityImage.positionOffset_Y = -15;
						this.qualityImage.sizeOffset_X = 10;
						this.qualityImage.sizeOffset_Y = 10;
						this.qualityImage.texture = (Texture2D)PlayerDashboardInventoryUI.icons.load("Quality_1");
					}
					else
					{
						this.qualityImage.positionOffset_X = -30;
						this.qualityImage.positionOffset_Y = -30;
						this.qualityImage.sizeOffset_X = 20;
						this.qualityImage.sizeOffset_Y = 20;
						this.qualityImage.texture = (Texture2D)PlayerDashboardInventoryUI.icons.load("Quality_0");
					}
					this.qualityImage.backgroundColor = ItemTool.getQualityColor((float)this.jar.item.quality / 100f);
					this.qualityImage.foregroundColor = this.qualityImage.backgroundColor;
					this.amountLabel.text = this.jar.item.quality + "%";
					this.amountLabel.backgroundColor = this.qualityImage.backgroundColor;
					this.amountLabel.foregroundColor = this.qualityImage.backgroundColor;
					this.qualityImage.isVisible = true;
					this.amountLabel.isVisible = true;
				}
				else
				{
					this.qualityImage.isVisible = false;
					if (itemAsset.amount > 1)
					{
						this.amountLabel.text = "x" + this.jar.item.amount;
						this.amountLabel.backgroundColor = Color.white;
						this.amountLabel.foregroundColor = Color.white;
						this.amountLabel.isVisible = true;
					}
					else
					{
						this.amountLabel.isVisible = false;
					}
				}
			}
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x001496B8 File Offset: 0x00147AB8
		private void onClickedButton(SleekButton button)
		{
			if (Event.current.button == 0)
			{
				if (this.onDraggedItem != null)
				{
					this.onDraggedItem(this);
				}
			}
			else if (this.onClickedItem != null)
			{
				this.onClickedItem(this);
			}
			Event.current.Use();
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x00149711 File Offset: 0x00147B11
		public override void draw(bool ignoreCulling)
		{
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x0014971A File Offset: 0x00147B1A
		private void onItemIconReady(Texture2D texture)
		{
			this.icon.texture = texture;
			this.icon.isVisible = true;
		}

		// Token: 0x04002279 RID: 8825
		private ItemJar _jar;

		// Token: 0x0400227A RID: 8826
		private byte _hotkey = byte.MaxValue;

		// Token: 0x0400227B RID: 8827
		private SleekButton button;

		// Token: 0x0400227C RID: 8828
		private SleekImageTexture icon;

		// Token: 0x0400227D RID: 8829
		private SleekLabel amountLabel;

		// Token: 0x0400227E RID: 8830
		private SleekImageTexture qualityImage;

		// Token: 0x0400227F RID: 8831
		private SleekLabel hotkeyLabel;

		// Token: 0x04002280 RID: 8832
		public ClickedItem onClickedItem;

		// Token: 0x04002281 RID: 8833
		public DraggedItem onDraggedItem;

		// Token: 0x04002282 RID: 8834
		private bool isTemporary;
	}
}
