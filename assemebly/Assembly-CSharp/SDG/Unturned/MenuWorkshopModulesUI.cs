using System;
using System.Runtime.CompilerServices;
using SDG.Framework.Modules;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200078B RID: 1931
	public class MenuWorkshopModulesUI
	{
		// Token: 0x06003794 RID: 14228 RVA: 0x001864F4 File Offset: 0x001848F4
		public MenuWorkshopModulesUI()
		{
			MenuWorkshopModulesUI.localization = Localization.read("/Menu/Workshop/MenuWorkshopModules.dat");
			MenuWorkshopModulesUI.container = new Sleek();
			MenuWorkshopModulesUI.container.positionOffset_X = 10;
			MenuWorkshopModulesUI.container.positionOffset_Y = 10;
			MenuWorkshopModulesUI.container.positionScale_Y = 1f;
			MenuWorkshopModulesUI.container.sizeOffset_X = -20;
			MenuWorkshopModulesUI.container.sizeOffset_Y = -20;
			MenuWorkshopModulesUI.container.sizeScale_X = 1f;
			MenuWorkshopModulesUI.container.sizeScale_Y = 1f;
			MenuUI.container.add(MenuWorkshopModulesUI.container);
			MenuWorkshopModulesUI.active = false;
			MenuWorkshopModulesUI.headerBox = new SleekBox();
			MenuWorkshopModulesUI.headerBox.sizeOffset_Y = 50;
			MenuWorkshopModulesUI.headerBox.sizeScale_X = 1f;
			MenuWorkshopModulesUI.headerBox.fontSize = 14;
			MenuWorkshopModulesUI.headerBox.text = MenuWorkshopModulesUI.localization.format("Header");
			MenuWorkshopModulesUI.container.add(MenuWorkshopModulesUI.headerBox);
			MenuWorkshopModulesUI.moduleBox = new SleekScrollBox();
			MenuWorkshopModulesUI.moduleBox.positionOffset_Y = 60;
			MenuWorkshopModulesUI.moduleBox.sizeOffset_Y = -120;
			MenuWorkshopModulesUI.moduleBox.sizeScale_X = 1f;
			MenuWorkshopModulesUI.moduleBox.sizeScale_Y = 1f;
			MenuWorkshopModulesUI.moduleBox.area = new Rect(0f, 0f, 5f, 0f);
			MenuWorkshopModulesUI.container.add(MenuWorkshopModulesUI.moduleBox);
			if (ModuleHook.modules.Count == 0)
			{
				SleekBox sleekBox = new SleekBox();
				sleekBox.positionOffset_Y = 60;
				sleekBox.sizeOffset_X = -30;
				sleekBox.sizeOffset_Y = 50;
				sleekBox.sizeScale_X = 1f;
				sleekBox.fontSize = 14;
				sleekBox.text = MenuWorkshopModulesUI.localization.format("No_Modules");
				MenuWorkshopModulesUI.container.add(sleekBox);
			}
			else
			{
				for (int i = 0; i < ModuleHook.modules.Count; i++)
				{
					ModuleConfig config = ModuleHook.modules[i].config;
					Local local = Localization.tryRead(config.DirectoryPath, false);
					SleekBox sleekBox2 = new SleekBox();
					sleekBox2.positionOffset_Y = i * 130;
					sleekBox2.sizeOffset_X = -30;
					sleekBox2.sizeOffset_Y = 120;
					sleekBox2.sizeScale_X = 1f;
					SleekToggle sleekToggle = new SleekToggle();
					sleekToggle.positionOffset_X = 5;
					sleekToggle.positionOffset_Y = -20;
					sleekToggle.positionScale_Y = 0.5f;
					sleekToggle.sizeOffset_X = 40;
					sleekToggle.sizeOffset_Y = 40;
					sleekToggle.state = config.IsEnabled;
					SleekToggle sleekToggle2 = sleekToggle;
					if (MenuWorkshopModulesUI.<>f__mg$cache0 == null)
					{
						MenuWorkshopModulesUI.<>f__mg$cache0 = new Toggled(MenuWorkshopModulesUI.onToggledModuleToggle);
					}
					sleekToggle2.onToggled = MenuWorkshopModulesUI.<>f__mg$cache0;
					sleekBox2.add(sleekToggle);
					SleekLabel sleekLabel = new SleekLabel();
					sleekLabel.positionOffset_X = 50;
					sleekLabel.positionOffset_Y = 5;
					sleekLabel.sizeOffset_X = -55;
					sleekLabel.sizeOffset_Y = 30;
					sleekLabel.sizeScale_X = 1f;
					sleekLabel.fontSize = 14;
					sleekLabel.fontAlignment = TextAnchor.UpperLeft;
					sleekLabel.text = local.format("Name");
					sleekBox2.add(sleekLabel);
					SleekLabel sleekLabel2 = new SleekLabel();
					sleekLabel2.positionOffset_X = 50;
					sleekLabel2.positionOffset_Y = 30;
					sleekLabel2.sizeOffset_X = -55;
					sleekLabel2.sizeOffset_Y = 25;
					sleekLabel2.sizeScale_X = 1f;
					sleekLabel2.fontAlignment = TextAnchor.UpperLeft;
					sleekLabel2.text = MenuWorkshopModulesUI.localization.format("Version", new object[]
					{
						config.Version
					});
					sleekBox2.add(sleekLabel2);
					SleekLabel sleekLabel3 = new SleekLabel();
					sleekLabel3.positionOffset_X = 50;
					sleekLabel3.positionOffset_Y = 50;
					sleekLabel3.sizeOffset_X = -55;
					sleekLabel3.sizeOffset_Y = 65;
					sleekLabel3.sizeScale_X = 1f;
					sleekLabel3.fontSize = 12;
					sleekLabel3.fontAlignment = TextAnchor.UpperLeft;
					sleekLabel3.text = local.format("Description");
					sleekBox2.add(sleekLabel3);
					if (ReadWrite.fileExists(config.DirectoryPath + "/Icon.png", false, false))
					{
						byte[] data = ReadWrite.readBytes(config.DirectoryPath + "/Icon.png", false, false);
						Texture2D texture2D = new Texture2D(100, 100, TextureFormat.ARGB32, false, true);
						texture2D.name = "Module_" + config.Name + "_Icon";
						texture2D.hideFlags = HideFlags.HideAndDontSave;
						texture2D.LoadImage(data);
						sleekBox2.add(new SleekImageTexture
						{
							positionOffset_X = 50,
							positionOffset_Y = 10,
							sizeOffset_X = 100,
							sizeOffset_Y = 100,
							texture = texture2D,
							shouldDestroyTexture = true
						});
						sleekLabel.positionOffset_X += 105;
						sleekLabel.sizeOffset_X -= 105;
						sleekLabel2.positionOffset_X += 105;
						sleekLabel2.sizeOffset_X -= 105;
						sleekLabel3.positionOffset_X += 105;
						sleekLabel3.sizeOffset_X -= 105;
					}
					MenuWorkshopModulesUI.moduleBox.add(sleekBox2);
				}
				MenuWorkshopModulesUI.moduleBox.area = new Rect(0f, 0f, 5f, (float)(ModuleHook.modules.Count * 130 - 10));
			}
			MenuWorkshopModulesUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuWorkshopModulesUI.backButton.positionOffset_Y = -50;
			MenuWorkshopModulesUI.backButton.positionScale_Y = 1f;
			MenuWorkshopModulesUI.backButton.sizeOffset_X = 200;
			MenuWorkshopModulesUI.backButton.sizeOffset_Y = 50;
			MenuWorkshopModulesUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuWorkshopModulesUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton = MenuWorkshopModulesUI.backButton;
			if (MenuWorkshopModulesUI.<>f__mg$cache1 == null)
			{
				MenuWorkshopModulesUI.<>f__mg$cache1 = new ClickedButton(MenuWorkshopModulesUI.onClickedBackButton);
			}
			sleekButton.onClickedButton = MenuWorkshopModulesUI.<>f__mg$cache1;
			MenuWorkshopModulesUI.backButton.fontSize = 14;
			MenuWorkshopModulesUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuWorkshopModulesUI.container.add(MenuWorkshopModulesUI.backButton);
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x00186AFD File Offset: 0x00184EFD
		public static void open()
		{
			if (MenuWorkshopModulesUI.active)
			{
				return;
			}
			MenuWorkshopModulesUI.active = true;
			MenuWorkshopModulesUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x00186B2A File Offset: 0x00184F2A
		public static void close()
		{
			if (!MenuWorkshopModulesUI.active)
			{
				return;
			}
			MenuWorkshopModulesUI.active = false;
			MenuWorkshopModulesUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x00186B58 File Offset: 0x00184F58
		private static void onToggledModuleToggle(SleekToggle toggle, bool state)
		{
			int index = MenuWorkshopModulesUI.moduleBox.search(toggle.parent);
			ModuleHook.toggleModuleEnabled(index);
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x00186B7C File Offset: 0x00184F7C
		private static void onClickedBackButton(SleekButton button)
		{
			MenuWorkshopUI.open();
			MenuWorkshopModulesUI.close();
		}

		// Token: 0x0400292A RID: 10538
		private static Local localization;

		// Token: 0x0400292B RID: 10539
		private static Sleek container;

		// Token: 0x0400292C RID: 10540
		public static bool active;

		// Token: 0x0400292D RID: 10541
		private static SleekButtonIcon backButton;

		// Token: 0x0400292E RID: 10542
		private static SleekBox headerBox;

		// Token: 0x0400292F RID: 10543
		private static SleekScrollBox moduleBox;

		// Token: 0x04002930 RID: 10544
		[CompilerGenerated]
		private static Toggled <>f__mg$cache0;

		// Token: 0x04002931 RID: 10545
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;
	}
}
