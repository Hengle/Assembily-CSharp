using System;
using SDG.Provider;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006E6 RID: 1766
	public class SleekInventory : Sleek
	{
		// Token: 0x060032A5 RID: 12965 RVA: 0x001483F4 File Offset: 0x001467F4
		public SleekInventory()
		{
			base.init();
			this.button = new SleekButton();
			this.button.sizeScale_X = 1f;
			this.button.sizeScale_Y = 1f;
			this.button.backgroundTint = ESleekTint.NONE;
			this.button.foregroundTint = ESleekTint.NONE;
			this.button.onClickedButton = new ClickedButton(this.onClickedButton);
			base.add(this.button);
			this.button.isClickable = false;
			this.icon = new SleekImageTexture();
			this.icon.positionOffset_X = 5;
			this.icon.positionOffset_Y = 5;
			this.icon.sizeScale_X = 1f;
			this.icon.sizeScale_Y = 1f;
			this.icon.sizeOffset_X = -10;
			this.icon.constraint = ESleekConstraint.XY;
			base.add(this.icon);
			this.icon.isVisible = false;
			this.equippedIcon = new SleekImageTexture();
			this.equippedIcon.positionOffset_X = 5;
			this.equippedIcon.positionOffset_Y = 5;
			this.equippedIcon.backgroundTint = ESleekTint.FOREGROUND;
			base.add(this.equippedIcon);
			this.equippedIcon.isVisible = false;
			this.statTrackerLabel = new SleekLabel();
			this.statTrackerLabel.positionOffset_Y = -30;
			this.statTrackerLabel.positionScale_Y = 1f;
			this.statTrackerLabel.sizeOffset_Y = 30;
			this.statTrackerLabel.sizeScale_X = 1f;
			this.statTrackerLabel.foregroundTint = ESleekTint.NONE;
			this.statTrackerLabel.fontAlignment = TextAnchor.LowerLeft;
			this.statTrackerLabel.fontStyle = FontStyle.Italic;
			base.add(this.statTrackerLabel);
			this.statTrackerLabel.isVisible = false;
			this.nameLabel = new SleekLabel();
			this.nameLabel.positionScale_Y = 1f;
			this.nameLabel.sizeScale_X = 1f;
			this.nameLabel.foregroundTint = ESleekTint.NONE;
			base.add(this.nameLabel);
			this.nameLabel.isVisible = false;
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x060032A6 RID: 12966 RVA: 0x0014860A File Offset: 0x00146A0A
		public ItemAsset itemAsset
		{
			get
			{
				return this._itemAsset;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060032A7 RID: 12967 RVA: 0x00148612 File Offset: 0x00146A12
		public VehicleAsset vehicleAsset
		{
			get
			{
				return this._vehicleAsset;
			}
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x0014861C File Offset: 0x00146A1C
		public void updateInventory(ulong instance, int item, ushort quantity, bool isClickable, bool isLarge)
		{
			this.button.isClickable = isClickable;
			if (isLarge)
			{
				this.icon.sizeOffset_Y = -70;
				this.nameLabel.fontSize = 18;
				this.nameLabel.positionOffset_Y = -70;
				this.nameLabel.sizeOffset_Y = 70;
				this.equippedIcon.sizeOffset_X = 20;
				this.equippedIcon.sizeOffset_Y = 20;
				this.statTrackerLabel.fontSize = 12;
			}
			else
			{
				this.icon.sizeOffset_Y = -50;
				this.nameLabel.fontSize = 12;
				this.nameLabel.positionOffset_Y = -50;
				this.nameLabel.sizeOffset_Y = 50;
				this.equippedIcon.sizeOffset_X = 10;
				this.equippedIcon.sizeOffset_Y = 10;
				this.statTrackerLabel.fontSize = 8;
			}
			if (item != 0)
			{
				if (item < 0)
				{
					this._itemAsset = null;
					this._vehicleAsset = null;
					this.icon.texture = (Texture2D)Resources.Load("Economy/Mystery" + ((!isLarge) ? "/Icon_Small" : "/Icon_Large"));
					this.icon.isVisible = true;
					this.nameLabel.text = MenuSurvivorsClothingUI.localization.format("Mystery_" + item + "_Text");
					this.button.tooltip = MenuSurvivorsClothingUI.localization.format("Mystery_Tooltip");
					this.button.backgroundColor = Palette.MYTHICAL;
					this.equippedIcon.isVisible = false;
				}
				else
				{
					ushort num;
					ushort num2;
					Provider.provider.economyService.getInventoryTargetID(item, out num, out num2);
					if (num == 0 && num2 == 0)
					{
						this._itemAsset = null;
						this._vehicleAsset = null;
						this.icon.texture = null;
						this.icon.isVisible = true;
						this.nameLabel.text = "itemdefid: " + item;
						this.button.tooltip = "itemdefid: " + item;
						this.button.backgroundColor = Color.white;
						this.equippedIcon.isVisible = false;
						this.statTrackerLabel.isVisible = false;
					}
					else
					{
						if (num != 0)
						{
							this._itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, num);
							if (this.itemAsset != null)
							{
								if (this.itemAsset.proPath == null || this.itemAsset.proPath.Length == 0)
								{
									ushort inventorySkinID = Provider.provider.economyService.getInventorySkinID(item);
									SkinAsset skinAsset = (SkinAsset)Assets.find(EAssetType.SKIN, inventorySkinID);
									if (skinAsset != null)
									{
										this.icon.texture = (Texture2D)Resources.Load(string.Concat(new string[]
										{
											"Economy/Skins/",
											this.itemAsset.name,
											"/",
											skinAsset.name,
											(!isLarge) ? "/Icon_Small" : "/Icon_Large"
										}));
										this.icon.isVisible = true;
									}
									else
									{
										this.icon.isVisible = false;
									}
								}
								else
								{
									this.icon.texture = (Texture2D)Resources.Load("Economy" + this.itemAsset.proPath + ((!isLarge) ? "/Icon_Small" : "/Icon_Large"));
									this.icon.isVisible = true;
								}
							}
							else
							{
								this.icon.texture = null;
								this.icon.isVisible = true;
							}
						}
						else
						{
							this._itemAsset = null;
						}
						if (num2 != 0)
						{
							this._vehicleAsset = (VehicleAsset)Assets.find(EAssetType.VEHICLE, num2);
							if (this.vehicleAsset != null)
							{
								ushort inventorySkinID2 = Provider.provider.economyService.getInventorySkinID(item);
								SkinAsset skinAsset2 = (SkinAsset)Assets.find(EAssetType.SKIN, inventorySkinID2);
								if (skinAsset2 != null)
								{
									this.icon.texture = (Texture2D)Resources.Load(string.Concat(new string[]
									{
										"Economy/Skins/",
										this.vehicleAsset.sharedSkinName,
										"/",
										skinAsset2.name,
										(!isLarge) ? "/Icon_Small" : "/Icon_Large"
									}));
									this.icon.isVisible = true;
								}
								else
								{
									this.icon.isVisible = false;
								}
							}
							else
							{
								this.icon.texture = null;
								this.icon.isVisible = true;
							}
						}
						else
						{
							this._vehicleAsset = null;
						}
						this.nameLabel.text = Provider.provider.economyService.getInventoryName(item);
						if (quantity > 1)
						{
							SleekLabel sleekLabel = this.nameLabel;
							sleekLabel.text = sleekLabel.text + " x" + quantity;
						}
						this.button.tooltip = Provider.provider.economyService.getInventoryType(item);
						this.button.backgroundColor = Provider.provider.economyService.getInventoryColor(item);
						bool isVisible;
						if (this.itemAsset == null || this.itemAsset.proPath == null || this.itemAsset.proPath.Length == 0)
						{
							isVisible = Characters.isSkinEquipped(instance);
						}
						else
						{
							isVisible = Characters.isCosmeticEquipped(instance);
						}
						this.equippedIcon.isVisible = isVisible;
						if (this.equippedIcon.isVisible && this.equippedIcon.texture == null)
						{
							this.equippedIcon.texture = (Texture2D)MenuSurvivorsClothingUI.icons.load("Equip");
						}
					}
				}
				this.nameLabel.isVisible = true;
				EStatTrackerType type;
				int num3;
				if (!Provider.provider.economyService.getInventoryStatTrackerValue(instance, out type, out num3))
				{
					this.statTrackerLabel.isVisible = false;
				}
				else
				{
					this.statTrackerLabel.isVisible = true;
					this.statTrackerLabel.foregroundColor = Provider.provider.economyService.getStatTrackerColor(type);
					this.statTrackerLabel.text = num3.ToString("D7");
				}
			}
			else
			{
				this._itemAsset = null;
				this.button.tooltip = string.Empty;
				this.button.backgroundColor = Color.white;
				this.icon.isVisible = false;
				this.nameLabel.isVisible = false;
				this.equippedIcon.isVisible = false;
				this.statTrackerLabel.isVisible = false;
			}
			this.button.foregroundColor = this.button.backgroundColor;
			this.nameLabel.foregroundColor = this.button.backgroundColor;
			this.nameLabel.backgroundColor = this.button.backgroundColor;
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x00148CDC File Offset: 0x001470DC
		private void onClickedButton(SleekButton button)
		{
			if (this.onClickedInventory != null)
			{
				this.onClickedInventory(this);
			}
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x00148CF5 File Offset: 0x001470F5
		public override void draw(bool ignoreCulling)
		{
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x04002271 RID: 8817
		private ItemAsset _itemAsset;

		// Token: 0x04002272 RID: 8818
		private VehicleAsset _vehicleAsset;

		// Token: 0x04002273 RID: 8819
		private SleekButton button;

		// Token: 0x04002274 RID: 8820
		private SleekImageTexture icon;

		// Token: 0x04002275 RID: 8821
		private SleekLabel nameLabel;

		// Token: 0x04002276 RID: 8822
		private SleekImageTexture equippedIcon;

		// Token: 0x04002277 RID: 8823
		private SleekLabel statTrackerLabel;

		// Token: 0x04002278 RID: 8824
		public ClickedInventory onClickedInventory;
	}
}
