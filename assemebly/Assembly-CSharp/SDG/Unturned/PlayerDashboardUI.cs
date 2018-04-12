using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200079A RID: 1946
	public class PlayerDashboardUI
	{
		// Token: 0x06003878 RID: 14456 RVA: 0x001988C4 File Offset: 0x00196CC4
		public PlayerDashboardUI()
		{
			Local local = Localization.read("/Player/PlayerDashboard.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Player/Icons/PlayerDashboard/PlayerDashboard.unity3d");
			PlayerDashboardUI.container = new Sleek();
			PlayerDashboardUI.container.positionScale_Y = -1f;
			PlayerDashboardUI.container.positionOffset_X = 10;
			PlayerDashboardUI.container.positionOffset_Y = 10;
			PlayerDashboardUI.container.sizeOffset_X = -20;
			PlayerDashboardUI.container.sizeOffset_Y = -20;
			PlayerDashboardUI.container.sizeScale_X = 1f;
			PlayerDashboardUI.container.sizeScale_Y = 1f;
			PlayerUI.container.add(PlayerDashboardUI.container);
			PlayerDashboardUI.active = false;
			PlayerDashboardUI.inventoryButton = new SleekButtonIcon((Texture2D)bundle.load("Inventory"));
			PlayerDashboardUI.inventoryButton.sizeOffset_X = -5;
			PlayerDashboardUI.inventoryButton.sizeOffset_Y = 50;
			PlayerDashboardUI.inventoryButton.sizeScale_X = 0.25f;
			PlayerDashboardUI.inventoryButton.text = local.format("Inventory", new object[]
			{
				ControlsSettings.inventory
			});
			PlayerDashboardUI.inventoryButton.tooltip = local.format("Inventory_Tooltip");
			SleekButton sleekButton = PlayerDashboardUI.inventoryButton;
			if (PlayerDashboardUI.<>f__mg$cache0 == null)
			{
				PlayerDashboardUI.<>f__mg$cache0 = new ClickedButton(PlayerDashboardUI.onClickedInventoryButton);
			}
			sleekButton.onClickedButton = PlayerDashboardUI.<>f__mg$cache0;
			PlayerDashboardUI.inventoryButton.fontSize = 14;
			PlayerDashboardUI.inventoryButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			PlayerDashboardUI.container.add(PlayerDashboardUI.inventoryButton);
			PlayerDashboardUI.craftingButton = new SleekButtonIcon((Texture2D)bundle.load("Crafting"));
			PlayerDashboardUI.craftingButton.positionOffset_X = 5;
			PlayerDashboardUI.craftingButton.positionScale_X = 0.25f;
			PlayerDashboardUI.craftingButton.sizeOffset_X = -10;
			PlayerDashboardUI.craftingButton.sizeOffset_Y = 50;
			PlayerDashboardUI.craftingButton.sizeScale_X = 0.25f;
			PlayerDashboardUI.craftingButton.text = local.format("Crafting", new object[]
			{
				ControlsSettings.crafting
			});
			PlayerDashboardUI.craftingButton.tooltip = local.format("Crafting_Tooltip");
			SleekButton sleekButton2 = PlayerDashboardUI.craftingButton;
			if (PlayerDashboardUI.<>f__mg$cache1 == null)
			{
				PlayerDashboardUI.<>f__mg$cache1 = new ClickedButton(PlayerDashboardUI.onClickedCraftingButton);
			}
			sleekButton2.onClickedButton = PlayerDashboardUI.<>f__mg$cache1;
			PlayerDashboardUI.craftingButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			PlayerDashboardUI.craftingButton.fontSize = 14;
			PlayerDashboardUI.container.add(PlayerDashboardUI.craftingButton);
			PlayerDashboardUI.skillsButton = new SleekButtonIcon((Texture2D)bundle.load("Skills"));
			PlayerDashboardUI.skillsButton.positionOffset_X = 5;
			PlayerDashboardUI.skillsButton.positionScale_X = 0.5f;
			PlayerDashboardUI.skillsButton.sizeOffset_X = -10;
			PlayerDashboardUI.skillsButton.sizeOffset_Y = 50;
			PlayerDashboardUI.skillsButton.sizeScale_X = 0.25f;
			PlayerDashboardUI.skillsButton.text = local.format("Skills", new object[]
			{
				ControlsSettings.skills
			});
			PlayerDashboardUI.skillsButton.tooltip = local.format("Skills_Tooltip");
			PlayerDashboardUI.skillsButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton3 = PlayerDashboardUI.skillsButton;
			if (PlayerDashboardUI.<>f__mg$cache2 == null)
			{
				PlayerDashboardUI.<>f__mg$cache2 = new ClickedButton(PlayerDashboardUI.onClickedSkillsButton);
			}
			sleekButton3.onClickedButton = PlayerDashboardUI.<>f__mg$cache2;
			PlayerDashboardUI.skillsButton.fontSize = 14;
			PlayerDashboardUI.container.add(PlayerDashboardUI.skillsButton);
			PlayerDashboardUI.informationButton = new SleekButtonIcon((Texture2D)bundle.load("Information"));
			PlayerDashboardUI.informationButton.positionOffset_X = 5;
			PlayerDashboardUI.informationButton.positionScale_X = 0.75f;
			PlayerDashboardUI.informationButton.sizeOffset_X = -5;
			PlayerDashboardUI.informationButton.sizeOffset_Y = 50;
			PlayerDashboardUI.informationButton.sizeScale_X = 0.25f;
			PlayerDashboardUI.informationButton.text = local.format("Information", new object[]
			{
				ControlsSettings.map
			});
			PlayerDashboardUI.informationButton.tooltip = local.format("Information_Tooltip");
			PlayerDashboardUI.informationButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton4 = PlayerDashboardUI.informationButton;
			if (PlayerDashboardUI.<>f__mg$cache3 == null)
			{
				PlayerDashboardUI.<>f__mg$cache3 = new ClickedButton(PlayerDashboardUI.onClickedInformationButton);
			}
			sleekButton4.onClickedButton = PlayerDashboardUI.<>f__mg$cache3;
			PlayerDashboardUI.informationButton.fontSize = 14;
			PlayerDashboardUI.container.add(PlayerDashboardUI.informationButton);
			if (Level.info != null && Level.info.type == ELevelType.HORDE)
			{
				PlayerDashboardUI.inventoryButton.sizeScale_X = 0.5f;
				PlayerDashboardUI.craftingButton.isVisible = false;
				PlayerDashboardUI.skillsButton.isVisible = false;
				PlayerDashboardUI.informationButton.positionScale_X = 0.5f;
				PlayerDashboardUI.informationButton.sizeScale_X = 0.5f;
			}
			bundle.unload();
			new PlayerDashboardInventoryUI();
			new PlayerDashboardCraftingUI();
			new PlayerDashboardSkillsUI();
			new PlayerDashboardInformationUI();
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x00198D74 File Offset: 0x00197174
		public static void open()
		{
			if (PlayerDashboardUI.active)
			{
				return;
			}
			PlayerDashboardUI.active = true;
			if (PlayerDashboardInventoryUI.active)
			{
				PlayerDashboardInventoryUI.active = false;
				PlayerDashboardInventoryUI.open();
			}
			else if (PlayerDashboardCraftingUI.active)
			{
				PlayerDashboardCraftingUI.active = false;
				PlayerDashboardCraftingUI.open();
			}
			else if (PlayerDashboardSkillsUI.active)
			{
				PlayerDashboardSkillsUI.active = false;
				PlayerDashboardSkillsUI.open();
			}
			else if (PlayerDashboardInformationUI.active)
			{
				PlayerDashboardInformationUI.active = false;
				PlayerDashboardInformationUI.open();
			}
			PlayerDashboardUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x00198E10 File Offset: 0x00197210
		public static void close()
		{
			if (!PlayerDashboardUI.active)
			{
				return;
			}
			PlayerDashboardUI.active = false;
			if (PlayerDashboardInventoryUI.active)
			{
				PlayerDashboardInventoryUI.close();
				PlayerDashboardInventoryUI.active = true;
			}
			else if (PlayerDashboardCraftingUI.active)
			{
				PlayerDashboardCraftingUI.close();
				PlayerDashboardCraftingUI.active = true;
			}
			else if (PlayerDashboardSkillsUI.active)
			{
				PlayerDashboardSkillsUI.close();
				PlayerDashboardSkillsUI.active = true;
			}
			else if (PlayerDashboardInformationUI.active)
			{
				PlayerDashboardInformationUI.close();
				PlayerDashboardInformationUI.active = true;
			}
			PlayerDashboardUI.container.lerpPositionScale(0f, -1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x00198EAB File Offset: 0x001972AB
		private static void onClickedInventoryButton(SleekButton button)
		{
			PlayerDashboardCraftingUI.close();
			PlayerDashboardSkillsUI.close();
			PlayerDashboardInformationUI.close();
			if (PlayerDashboardInventoryUI.active)
			{
				PlayerDashboardUI.close();
				PlayerLifeUI.open();
			}
			else
			{
				PlayerDashboardInventoryUI.open();
			}
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x00198EDA File Offset: 0x001972DA
		private static void onClickedCraftingButton(SleekButton button)
		{
			PlayerDashboardInventoryUI.close();
			PlayerDashboardSkillsUI.close();
			PlayerDashboardInformationUI.close();
			if (PlayerDashboardCraftingUI.active)
			{
				PlayerDashboardUI.close();
				PlayerLifeUI.open();
			}
			else
			{
				PlayerDashboardCraftingUI.open();
			}
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x00198F09 File Offset: 0x00197309
		private static void onClickedSkillsButton(SleekButton button)
		{
			PlayerDashboardInventoryUI.close();
			PlayerDashboardCraftingUI.close();
			PlayerDashboardInformationUI.close();
			if (PlayerDashboardSkillsUI.active)
			{
				PlayerDashboardUI.close();
				PlayerLifeUI.open();
			}
			else
			{
				PlayerDashboardSkillsUI.open();
			}
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x00198F38 File Offset: 0x00197338
		private static void onClickedInformationButton(SleekButton button)
		{
			PlayerDashboardInventoryUI.close();
			PlayerDashboardCraftingUI.close();
			PlayerDashboardSkillsUI.close();
			if (PlayerDashboardInformationUI.active)
			{
				PlayerDashboardUI.close();
				PlayerLifeUI.open();
			}
			else
			{
				PlayerDashboardInformationUI.open();
			}
		}

		// Token: 0x04002AB6 RID: 10934
		public static Sleek container;

		// Token: 0x04002AB7 RID: 10935
		public static bool active;

		// Token: 0x04002AB8 RID: 10936
		private static SleekButtonIcon inventoryButton;

		// Token: 0x04002AB9 RID: 10937
		private static SleekButtonIcon craftingButton;

		// Token: 0x04002ABA RID: 10938
		private static SleekButtonIcon skillsButton;

		// Token: 0x04002ABB RID: 10939
		private static SleekButtonIcon informationButton;

		// Token: 0x04002ABC RID: 10940
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x04002ABD RID: 10941
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x04002ABE RID: 10942
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x04002ABF RID: 10943
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;
	}
}
