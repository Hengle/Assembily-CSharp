using System;
using SDG.Framework.UI.Devkit;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005D7 RID: 1495
	public class MenuUI : MonoBehaviour
	{
		// Token: 0x06002A39 RID: 10809 RVA: 0x00106DAC File Offset: 0x001051AC
		private static void alertText()
		{
			if (MenuUI.alertBox == null || MenuUI.originLabel == null || MenuUI.packageButton == null)
			{
				return;
			}
			MenuUI.alertBox.positionOffset_Y = -25;
			MenuUI.alertBox.sizeOffset_Y = 50;
			MenuUI.originLabel.isVisible = false;
			MenuUI.packageButton.isVisible = false;
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x00106E08 File Offset: 0x00105208
		private static void alertItem()
		{
			if (MenuUI.alertBox == null || MenuUI.originLabel == null || MenuUI.packageButton == null)
			{
				return;
			}
			MenuUI.alertBox.text = string.Empty;
			MenuUI.alertBox.positionOffset_Y = -150;
			MenuUI.alertBox.sizeOffset_Y = 300;
			MenuUI.originLabel.isVisible = true;
			MenuUI.packageButton.isVisible = true;
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x00106E78 File Offset: 0x00105278
		public static void openAlert()
		{
			if (MenuUI.alertBox != null)
			{
				MenuUI.alertBox.lerpPositionScale(0f, 0.5f, ESleekLerp.EXPONENTIAL, 20f);
			}
			if (MenuUI.container != null)
			{
				MenuUI.container.lerpPositionScale(-1f, 0f, ESleekLerp.EXPONENTIAL, 20f);
			}
			MenuDashboardUI.setCanvasActive(false);
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x00106ED3 File Offset: 0x001052D3
		public static void openAlert(string message)
		{
			MenuUI.alertText();
			if (MenuUI.alertBox != null)
			{
				MenuUI.alertBox.text = message;
			}
			MenuUI.openAlert();
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x00106EF4 File Offset: 0x001052F4
		public static void closeAlert()
		{
			if (MenuUI.alertBox != null)
			{
				MenuUI.alertBox.lerpPositionScale(1f, 0.5f, ESleekLerp.EXPONENTIAL, 20f);
			}
			if (MenuUI.container != null)
			{
				MenuUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
			}
			MenuDashboardUI.setCanvasActive(true);
			SleekRender.allowInput = true;
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x00106F55 File Offset: 0x00105355
		public static void alert(string message)
		{
			MenuUI.alert(message, 4f);
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x00106F62 File Offset: 0x00105362
		public static void alert(string message, float duration)
		{
			MenuUI.openAlert(message);
			MenuUI.isAlerting = true;
			MenuUI.lastAlert = Time.realtimeSinceStartup;
			MenuUI.alertTime = duration;
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x00106F80 File Offset: 0x00105380
		public static void alert(string origin, ulong instance, int item, ushort quantity)
		{
			if (MenuUI.originLabel != null)
			{
				MenuUI.originLabel.text = origin;
				MenuUI.originLabel.foregroundColor = Provider.provider.economyService.getInventoryColor(item);
			}
			if (MenuUI.packageButton != null)
			{
				MenuUI.packageButton.updateInventory(instance, item, quantity, false, true);
			}
			MenuUI.alertItem();
			MenuUI.openAlert();
			MenuUI.isAlerting = true;
			MenuUI.lastAlert = Time.realtimeSinceStartup;
			MenuUI.alertTime = 4f;
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x00106FF9 File Offset: 0x001053F9
		public static void rebuild()
		{
			MenuUI.ui.Invoke("init", 0.1f);
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x0010700F File Offset: 0x0010540F
		public void build()
		{
			if (MenuUI.window != null)
			{
				MenuUI.window.build();
			}
			LoadingUI.rebuild();
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x0010702A File Offset: 0x0010542A
		public void init()
		{
			GraphicsSettings.resize();
			base.Invoke("build", 0.1f);
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x00107044 File Offset: 0x00105444
		public static void closeAll()
		{
			MenuPauseUI.close();
			MenuCreditsUI.close();
			MenuTitleUI.close();
			MenuDashboardUI.close();
			MenuPlayUI.close();
			MenuPlaySingleplayerUI.close();
			MenuPlayMatchmakingUI.close();
			MenuPlayLobbiesUI.close();
			MenuPlayConnectUI.close();
			MenuPlayServersUI.close();
			MenuPlayServerInfoUI.close();
			MenuPlayConfigUI.close();
			MenuSurvivorsUI.close();
			MenuSurvivorsCharacterUI.close();
			MenuSurvivorsAppearanceUI.close();
			MenuSurvivorsClothingUI.close();
			MenuSurvivorsGroupUI.close();
			MenuSurvivorsClothingBoxUI.close();
			MenuSurvivorsClothingDeleteUI.close();
			MenuSurvivorsClothingInspectUI.close();
			MenuSurvivorsClothingItemUI.close();
			MenuConfigurationUI.close();
			MenuConfigurationOptionsUI.close();
			MenuConfigurationDisplayUI.close();
			MenuConfigurationGraphicsUI.close();
			MenuConfigurationControlsUI.close();
			MenuWorkshopUI.close();
			MenuWorkshopEditorUI.close();
			MenuWorkshopSubmitUI.close();
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x001070E2 File Offset: 0x001054E2
		private void OnGUI()
		{
			if (MenuUI.window == null)
			{
				return;
			}
			MenuUI.window.draw(false);
			MenuSurvivorsClothingBoxUI.update();
			MenuConfigurationControlsUI.bindOnGUI();
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x00107104 File Offset: 0x00105504
		private void Update()
		{
			if (MenuUI.window == null)
			{
				return;
			}
			MenuConfigurationControlsUI.bindUpdate();
			if (MenuConfigurationControlsUI.binding == 255)
			{
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					if (Provider.provider.matchmakingService.isAttemptingServerQuery)
					{
						Provider.provider.matchmakingService.cancel();
					}
					else if (MenuUI.isAlerting)
					{
						MenuUI.closeAlert();
						MenuUI.isAlerting = false;
					}
					else if (MenuPauseUI.active)
					{
						MenuPauseUI.close();
						MenuDashboardUI.open();
						MenuTitleUI.open();
					}
					else if (MenuCreditsUI.active)
					{
						MenuCreditsUI.close();
						MenuPauseUI.open();
					}
					else if (MenuTitleUI.active)
					{
						MenuPauseUI.open();
						MenuDashboardUI.close();
						MenuTitleUI.close();
					}
					else if (MenuPlayConfigUI.active)
					{
						MenuPlayConfigUI.close();
						MenuPlaySingleplayerUI.open();
					}
					else if (MenuPlayServerInfoUI.active)
					{
						MenuPlayServerInfoUI.close();
						MenuPlayServerInfoUI.EServerInfoOpenContext openContext = MenuPlayServerInfoUI.openContext;
						if (openContext != MenuPlayServerInfoUI.EServerInfoOpenContext.CONNECT)
						{
							if (openContext != MenuPlayServerInfoUI.EServerInfoOpenContext.SERVERS)
							{
								if (openContext == MenuPlayServerInfoUI.EServerInfoOpenContext.MATCHMAKING)
								{
									MenuPlayMatchmakingUI.open();
								}
							}
							else
							{
								MenuPlayServersUI.open();
							}
						}
						else
						{
							MenuPlayConnectUI.open();
						}
					}
					else if (MenuPlayConnectUI.active || MenuPlayServersUI.active || MenuPlaySingleplayerUI.active || MenuPlayMatchmakingUI.active || MenuPlayLobbiesUI.active)
					{
						MenuPlayConnectUI.close();
						MenuPlayServersUI.close();
						MenuPlaySingleplayerUI.close();
						MenuPlayMatchmakingUI.close();
						MenuPlayLobbiesUI.close();
						MenuPlayUI.open();
					}
					else if (MenuSurvivorsClothingItemUI.active)
					{
						MenuSurvivorsClothingItemUI.close();
						MenuSurvivorsClothingUI.open();
					}
					else if (MenuSurvivorsClothingBoxUI.active)
					{
						if (!MenuSurvivorsClothingBoxUI.isUnboxing)
						{
							MenuSurvivorsClothingBoxUI.close();
							MenuSurvivorsClothingItemUI.open();
						}
					}
					else if (MenuSurvivorsClothingInspectUI.active || MenuSurvivorsClothingDeleteUI.active)
					{
						MenuSurvivorsClothingInspectUI.close();
						MenuSurvivorsClothingDeleteUI.close();
						MenuSurvivorsClothingItemUI.open();
					}
					else if (MenuSurvivorsCharacterUI.active || MenuSurvivorsAppearanceUI.active || MenuSurvivorsGroupUI.active || MenuSurvivorsClothingUI.active)
					{
						MenuSurvivorsCharacterUI.close();
						MenuSurvivorsAppearanceUI.close();
						MenuSurvivorsGroupUI.close();
						MenuSurvivorsClothingUI.close();
						MenuSurvivorsUI.open();
					}
					else if (MenuConfigurationOptionsUI.active || MenuConfigurationControlsUI.active || MenuConfigurationGraphicsUI.active || MenuConfigurationDisplayUI.active)
					{
						MenuConfigurationOptionsUI.close();
						MenuConfigurationControlsUI.close();
						MenuConfigurationGraphicsUI.close();
						MenuConfigurationDisplayUI.close();
						MenuConfigurationUI.open();
					}
					else if (MenuWorkshopSubmitUI.active || MenuWorkshopEditorUI.active || MenuWorkshopErrorUI.active || MenuWorkshopLocalizationUI.active || MenuWorkshopSpawnsUI.active || MenuWorkshopModulesUI.active)
					{
						MenuWorkshopSubmitUI.close();
						MenuWorkshopEditorUI.close();
						MenuWorkshopErrorUI.close();
						MenuWorkshopLocalizationUI.close();
						MenuWorkshopSpawnsUI.close();
						MenuWorkshopModulesUI.close();
						MenuWorkshopUI.open();
					}
					else
					{
						MenuPlayUI.close();
						MenuSurvivorsUI.close();
						MenuConfigurationUI.close();
						MenuWorkshopUI.close();
						MenuDashboardUI.open();
						MenuTitleUI.open();
					}
				}
				if (MenuUI.window != null)
				{
					if (Input.GetKeyDown(ControlsSettings.screenshot))
					{
						Provider.takeScreenshot();
					}
					if (Input.GetKeyDown(ControlsSettings.hud))
					{
						DevkitWindowManager.isActive = false;
						MenuUI.window.isEnabled = !MenuUI.window.isEnabled;
						MenuUI.window.drawCursorWhileDisabled = false;
					}
					if (Input.GetKeyDown(ControlsSettings.terminal))
					{
						DevkitWindowManager.isActive = !DevkitWindowManager.isActive;
						MenuUI.window.isEnabled = !DevkitWindowManager.isActive;
						MenuUI.window.drawCursorWhileDisabled = DevkitWindowManager.isActive;
					}
				}
			}
			if (Input.GetKeyDown(ControlsSettings.refreshAssets))
			{
				Assets.refresh();
			}
			if (Input.GetKeyDown(ControlsSettings.clipboardDebug) && MenuSurvivorsAppearanceUI.active)
			{
				string text = string.Empty;
				text = text + "Face " + Characters.active.face;
				text = text + "\nHair " + Characters.active.hair;
				text = text + "\nBeard " + Characters.active.beard;
				text = text + "\nColor_Skin " + Palette.hex(Characters.active.skin);
				text = text + "\nColor_Hair " + Palette.hex(Characters.active.color);
				if (Characters.active.hand)
				{
					text += "\nBackward";
				}
				GUIUtility.systemCopyBuffer = text;
			}
			if (MenuUI.isAlerting && Time.realtimeSinceStartup - MenuUI.lastAlert > MenuUI.alertTime)
			{
				MenuUI.closeAlert();
				MenuUI.isAlerting = false;
			}
			MenuUI.window.showCursor = true;
			MenuUI.window.updateDebug();
			if (MenuPlayUI.active || MenuPlayConnectUI.active || MenuPlayServersUI.active || MenuPlayServerInfoUI.active || MenuPlaySingleplayerUI.active || MenuPlayMatchmakingUI.active || MenuPlayLobbiesUI.active || MenuPlayConfigUI.active)
			{
				this.target = this.play;
			}
			else if (MenuSurvivorsUI.active || MenuSurvivorsCharacterUI.active || MenuSurvivorsAppearanceUI.active || MenuSurvivorsGroupUI.active || MenuSurvivorsClothingUI.active || MenuSurvivorsClothingItemUI.active || MenuSurvivorsClothingInspectUI.active || MenuSurvivorsClothingDeleteUI.active || MenuSurvivorsClothingBoxUI.active)
			{
				this.target = this.survivors;
			}
			else if (MenuConfigurationUI.active || MenuConfigurationOptionsUI.active || MenuConfigurationControlsUI.active || MenuConfigurationGraphicsUI.active || MenuConfigurationDisplayUI.active)
			{
				this.target = this.configuration;
			}
			else if (MenuWorkshopUI.active || MenuWorkshopSubmitUI.active || MenuWorkshopEditorUI.active || MenuWorkshopErrorUI.active || MenuWorkshopLocalizationUI.active || MenuWorkshopSpawnsUI.active || MenuWorkshopModulesUI.active)
			{
				this.target = this.workshop;
			}
			else
			{
				this.target = this.title;
			}
			if (this.target == this.title)
			{
				if (MenuUI.hasTitled)
				{
					base.transform.position = Vector3.Lerp(base.transform.position, this.target.position, Time.deltaTime * 4f);
					base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.target.rotation, Time.deltaTime * 4f);
				}
				else
				{
					base.transform.position = Vector3.Lerp(base.transform.position, this.target.position, Time.deltaTime);
					base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.target.rotation, Time.deltaTime);
				}
			}
			else
			{
				MenuUI.hasTitled = true;
				base.transform.position = Vector3.Lerp(base.transform.position, this.target.position, Time.deltaTime * 4f);
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.target.rotation, Time.deltaTime * 4f);
			}
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x00107878 File Offset: 0x00105C78
		private void Start()
		{
			if (!Dedicator.isDedicated)
			{
				this.title = base.transform.parent.FindChild("Title");
				this.play = base.transform.parent.FindChild("Play");
				this.survivors = base.transform.parent.FindChild("Survivors");
				this.configuration = base.transform.parent.FindChild("Configuration");
				this.workshop = base.transform.parent.FindChild("Workshop");
				MenuUI.window = new SleekWindow();
				MenuUI.container = new Sleek();
				MenuUI.container.sizeScale_X = 1f;
				MenuUI.container.sizeScale_Y = 1f;
				MenuUI.window.add(MenuUI.container);
				MenuUI.alertBox = new SleekBox();
				MenuUI.alertBox.positionOffset_X = 10;
				MenuUI.alertBox.positionOffset_Y = -25;
				MenuUI.alertBox.positionScale_X = 1f;
				MenuUI.alertBox.positionScale_Y = 0.5f;
				MenuUI.alertBox.sizeScale_X = 1f;
				MenuUI.alertBox.sizeOffset_X = -20;
				MenuUI.alertBox.sizeOffset_Y = 50;
				MenuUI.alertBox.fontSize = 14;
				MenuUI.window.add(MenuUI.alertBox);
				MenuUI.originLabel = new SleekLabel();
				MenuUI.originLabel.sizeOffset_Y = 50;
				MenuUI.originLabel.sizeScale_X = 1f;
				MenuUI.originLabel.fontSize = 18;
				MenuUI.alertBox.add(MenuUI.originLabel);
				MenuUI.originLabel.isVisible = false;
				MenuUI.packageButton = new SleekInventory();
				MenuUI.packageButton.positionOffset_X = -100;
				MenuUI.packageButton.positionOffset_Y = 75;
				MenuUI.packageButton.positionScale_X = 0.5f;
				MenuUI.packageButton.sizeOffset_X = 200;
				MenuUI.packageButton.sizeOffset_Y = 200;
				MenuUI.alertBox.add(MenuUI.packageButton);
				MenuUI.packageButton.isVisible = false;
				OptionsSettings.apply();
				GraphicsSettings.apply();
				new MenuDashboardUI();
				if (MenuUI.hasPanned && this.title != null)
				{
					base.transform.position = this.title.position;
					base.transform.rotation = this.title.rotation;
				}
				MenuUI.hasPanned = true;
			}
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x00107AEA File Offset: 0x00105EEA
		private void Awake()
		{
			MenuUI.ui = this;
			Time.timeScale = 1f;
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x00107AFC File Offset: 0x00105EFC
		private void OnDestroy()
		{
			if (MenuUI.window == null)
			{
				return;
			}
			MenuUI.window.destroy();
		}

		// Token: 0x04001A2F RID: 6703
		public static SleekWindow window;

		// Token: 0x04001A30 RID: 6704
		public static Sleek container;

		// Token: 0x04001A31 RID: 6705
		private static MenuUI ui;

		// Token: 0x04001A32 RID: 6706
		private static SleekBox alertBox;

		// Token: 0x04001A33 RID: 6707
		private static SleekLabel originLabel;

		// Token: 0x04001A34 RID: 6708
		private static SleekInventory packageButton;

		// Token: 0x04001A35 RID: 6709
		private static bool isAlerting;

		// Token: 0x04001A36 RID: 6710
		private static float lastAlert;

		// Token: 0x04001A37 RID: 6711
		private static float alertTime;

		// Token: 0x04001A38 RID: 6712
		private Transform title;

		// Token: 0x04001A39 RID: 6713
		private Transform play;

		// Token: 0x04001A3A RID: 6714
		private Transform survivors;

		// Token: 0x04001A3B RID: 6715
		private Transform configuration;

		// Token: 0x04001A3C RID: 6716
		private Transform workshop;

		// Token: 0x04001A3D RID: 6717
		private Transform target;

		// Token: 0x04001A3E RID: 6718
		private static bool hasPanned;

		// Token: 0x04001A3F RID: 6719
		private static bool hasTitled;

		// Token: 0x04001A40 RID: 6720
		private GameObject go;
	}
}
