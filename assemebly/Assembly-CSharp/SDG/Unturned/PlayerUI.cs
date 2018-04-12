using System;
using SDG.Framework.UI.Devkit;
using SDG.Framework.Water;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200064B RID: 1611
	public class PlayerUI : MonoBehaviour
	{
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002E6D RID: 11885 RVA: 0x0012A862 File Offset: 0x00128C62
		// (set) Token: 0x06002E6E RID: 11886 RVA: 0x0012A869 File Offset: 0x00128C69
		public static bool isBlindfolded
		{
			get
			{
				return PlayerUI._isBlindfolded;
			}
			set
			{
				if (PlayerUI.isBlindfolded == value)
				{
					return;
				}
				PlayerUI._isBlindfolded = value;
				PlayerUI.isBlindfoldedChanged();
			}
		}

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x06002E6F RID: 11887 RVA: 0x0012A888 File Offset: 0x00128C88
		// (remove) Token: 0x06002E70 RID: 11888 RVA: 0x0012A8BC File Offset: 0x00128CBC
		public static event IsBlindfoldedChangedHandler isBlindfoldedChanged;

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002E71 RID: 11889 RVA: 0x0012A8F0 File Offset: 0x00128CF0
		public static EChatMode chat
		{
			get
			{
				return PlayerUI._chat;
			}
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x0012A8F7 File Offset: 0x00128CF7
		public static void rebuild()
		{
			PlayerUI.ui.Invoke("init", 0.1f);
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x0012A90D File Offset: 0x00128D0D
		public void build()
		{
			if (PlayerUI.window != null)
			{
				PlayerUI.window.build();
			}
			LoadingUI.rebuild();
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x0012A928 File Offset: 0x00128D28
		public void init()
		{
			GraphicsSettings.resize();
			base.Invoke("build", 0.1f);
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x0012A93F File Offset: 0x00128D3F
		public static void stun(float amount)
		{
			PlayerUI.stunColor.a = amount * 5f;
			MainCamera.instance.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/General/Stun"), amount);
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x0012A974 File Offset: 0x00128D74
		public static void pain(float amount)
		{
			Color backgroundColor = PlayerLifeUI.painImage.backgroundColor;
			backgroundColor.a = amount * 0.75f;
			PlayerLifeUI.painImage.backgroundColor = backgroundColor;
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x0012A9A8 File Offset: 0x00128DA8
		public static void hitmark(int index, Vector3 point, bool worldspace, EPlayerHit newHit)
		{
			if (!PlayerUI.window.isEnabled || (PlayerUI.isOverlayed && !PlayerUI.isReverting))
			{
				return;
			}
			if (index < 0 || index >= PlayerLifeUI.hitmarkers.Length)
			{
				return;
			}
			if (!Provider.modeConfigData.Gameplay.Hitmarkers)
			{
				return;
			}
			HitmarkerInfo hitmarkerInfo = PlayerLifeUI.hitmarkers[index];
			hitmarkerInfo.lastHit = Time.realtimeSinceStartup;
			hitmarkerInfo.hit = newHit;
			hitmarkerInfo.point = point;
			hitmarkerInfo.worldspace = (worldspace || OptionsSettings.hitmarker);
			if (newHit == EPlayerHit.CRITICAL)
			{
				MainCamera.instance.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/General/Hit"), 0.5f);
			}
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x0012AA62 File Offset: 0x00128E62
		public static void enableDot()
		{
			PlayerLifeUI.dotImage.isVisible = true;
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x0012AA6F File Offset: 0x00128E6F
		public static void disableDot()
		{
			PlayerLifeUI.dotImage.isVisible = false;
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x0012AA7C File Offset: 0x00128E7C
		public static void updateScope(bool isScoped)
		{
			PlayerLifeUI.scopeOverlay.isVisible = isScoped;
			PlayerUI.container.isVisible = !isScoped;
			PlayerUI.isOverlayed = isScoped;
			if (PlayerUI.isOverlayed)
			{
				PlayerUI.isReverting = PlayerUI.window.isEnabled;
				PlayerUI.wasOverlayed = true;
				PlayerUI.window.isEnabled = true;
			}
			else if (PlayerUI.wasOverlayed)
			{
				PlayerUI.window.isEnabled = PlayerUI.isReverting;
				PlayerUI.wasOverlayed = false;
			}
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x0012AAF8 File Offset: 0x00128EF8
		public static void updateBinoculars(bool isBinoculars)
		{
			PlayerLifeUI.binocularsOverlay.isVisible = isBinoculars;
			PlayerUI.container.isVisible = !isBinoculars;
			PlayerUI.isOverlayed = isBinoculars;
			if (PlayerUI.isOverlayed)
			{
				PlayerUI.isReverting = PlayerUI.window.isEnabled;
				PlayerUI.wasOverlayed = true;
				PlayerUI.window.isEnabled = true;
			}
			else if (PlayerUI.wasOverlayed)
			{
				PlayerUI.window.isEnabled = PlayerUI.isReverting;
				PlayerUI.wasOverlayed = false;
			}
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x0012AB74 File Offset: 0x00128F74
		public static void resetCrosshair()
		{
			if (Provider.modeConfigData.Gameplay.Crosshair)
			{
				PlayerLifeUI.crosshairLeftImage.positionOffset_X = -4;
				PlayerLifeUI.crosshairLeftImage.positionOffset_Y = -4;
				PlayerLifeUI.crosshairRightImage.positionOffset_X = -4;
				PlayerLifeUI.crosshairRightImage.positionOffset_Y = -4;
				PlayerLifeUI.crosshairDownImage.positionOffset_X = -4;
				PlayerLifeUI.crosshairDownImage.positionOffset_Y = -4;
				PlayerLifeUI.crosshairUpImage.positionOffset_X = -4;
				PlayerLifeUI.crosshairUpImage.positionOffset_Y = -4;
			}
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x0012ABF8 File Offset: 0x00128FF8
		public static void updateCrosshair(float spread)
		{
			if (Provider.modeConfigData.Gameplay.Crosshair)
			{
				PlayerLifeUI.crosshairLeftImage.lerpPositionOffset((int)(-spread * 400f) - 4, -4, ESleekLerp.EXPONENTIAL, 10f);
				PlayerLifeUI.crosshairRightImage.lerpPositionOffset((int)(spread * 400f) - 4, -4, ESleekLerp.EXPONENTIAL, 10f);
				PlayerLifeUI.crosshairDownImage.lerpPositionOffset(-4, (int)(spread * 400f) - 4, ESleekLerp.EXPONENTIAL, 10f);
				PlayerLifeUI.crosshairUpImage.lerpPositionOffset(-4, (int)(-spread * 400f) - 4, ESleekLerp.EXPONENTIAL, 10f);
			}
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x0012AC8C File Offset: 0x0012908C
		public static void enableCrosshair()
		{
			if (Provider.modeConfigData.Gameplay.Crosshair)
			{
				PlayerLifeUI.crosshairLeftImage.isVisible = true;
				PlayerLifeUI.crosshairRightImage.isVisible = true;
				PlayerLifeUI.crosshairDownImage.isVisible = true;
				PlayerLifeUI.crosshairUpImage.isVisible = true;
			}
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x0012ACDC File Offset: 0x001290DC
		public static void disableCrosshair()
		{
			if (Provider.modeConfigData.Gameplay.Crosshair)
			{
				PlayerLifeUI.crosshairLeftImage.isVisible = false;
				PlayerLifeUI.crosshairRightImage.isVisible = false;
				PlayerLifeUI.crosshairDownImage.isVisible = false;
				PlayerLifeUI.crosshairUpImage.isVisible = false;
			}
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x0012AD29 File Offset: 0x00129129
		public static void hint(Transform transform, EPlayerMessage message)
		{
			PlayerUI.hint(transform, message, string.Empty, Color.white, new object[0]);
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x0012AD44 File Offset: 0x00129144
		public static void hint(Transform transform, EPlayerMessage message, string text, Color color, params object[] objects)
		{
			if (PlayerUI.messageBox == null)
			{
				return;
			}
			PlayerUI.lastHinted = true;
			PlayerUI.isHinted = true;
			if (message == EPlayerMessage.ENEMY)
			{
				if (objects.Length == 1)
				{
					SteamPlayer steamPlayer = (SteamPlayer)objects[0];
					if (PlayerUI.messagePlayer != null && PlayerUI.messagePlayer.player != steamPlayer)
					{
						PlayerUI.container.remove(PlayerUI.messagePlayer);
						PlayerUI.messagePlayer = null;
					}
					if (PlayerUI.messagePlayer == null)
					{
						PlayerUI.messagePlayer = new SleekPlayer(steamPlayer, false, SleekPlayer.ESleekPlayerDisplayContext.NONE);
						PlayerUI.messagePlayer.positionOffset_X = -150;
						PlayerUI.messagePlayer.positionOffset_Y = -130;
						PlayerUI.messagePlayer.positionScale_X = 0.5f;
						PlayerUI.messagePlayer.positionScale_Y = 1f;
						PlayerUI.messagePlayer.sizeOffset_X = 300;
						PlayerUI.messagePlayer.sizeOffset_Y = 50;
						PlayerUI.container.add(PlayerUI.messagePlayer);
					}
				}
				PlayerUI.messageBox.isVisible = false;
				PlayerUI.messagePlayer.isVisible = true;
				return;
			}
			PlayerUI.messageBox.isVisible = true;
			if (PlayerUI.messagePlayer != null)
			{
				PlayerUI.messagePlayer.isVisible = false;
			}
			PlayerUI.messageIcon_0.positionOffset_Y = 45;
			PlayerUI.messageProgress_0.positionOffset_Y = 50;
			PlayerUI.messageIcon_1.positionOffset_Y = 75;
			PlayerUI.messageProgress_1.positionOffset_Y = 80;
			PlayerUI.messageIcon_2.positionOffset_Y = 105;
			PlayerUI.messageProgress_2.positionOffset_Y = 110;
			if (message == EPlayerMessage.VEHICLE_ENTER)
			{
				InteractableVehicle interactableVehicle = (InteractableVehicle)PlayerInteract.interactable;
				int num = 45;
				PlayerUI.messageIcon_0.isVisible = true;
				PlayerUI.messageProgress_0.isVisible = true;
				PlayerUI.messageIcon_0.positionOffset_Y = num;
				PlayerUI.messageProgress_0.positionOffset_Y = num + 5;
				num += 30;
				PlayerUI.messageIcon_1.isVisible = interactableVehicle.usesHealth;
				PlayerUI.messageProgress_1.isVisible = interactableVehicle.usesHealth;
				if (interactableVehicle.usesHealth)
				{
					PlayerUI.messageIcon_1.positionOffset_Y = num;
					PlayerUI.messageProgress_1.positionOffset_Y = num + 5;
					num += 30;
				}
				PlayerUI.messageIcon_2.isVisible = interactableVehicle.usesBattery;
				PlayerUI.messageProgress_2.isVisible = interactableVehicle.usesBattery;
				if (interactableVehicle.usesBattery)
				{
					PlayerUI.messageIcon_2.positionOffset_Y = num;
					PlayerUI.messageProgress_2.positionOffset_Y = num + 5;
					num += 30;
				}
				PlayerUI.messageBox.sizeOffset_Y = num - 5;
				ushort num2;
				ushort num3;
				interactableVehicle.getDisplayFuel(out num2, out num3);
				PlayerUI.messageProgress_0.state = (float)num2 / (float)num3;
				PlayerUI.messageProgress_0.color = Palette.COLOR_Y;
				PlayerUI.messageIcon_0.texture = (Texture2D)PlayerLifeUI.icons.load("Fuel");
				if (interactableVehicle.usesHealth)
				{
					PlayerUI.messageProgress_1.state = (float)interactableVehicle.health / (float)interactableVehicle.asset.health;
					PlayerUI.messageProgress_1.color = Palette.COLOR_R;
					PlayerUI.messageIcon_1.texture = (Texture2D)PlayerLifeUI.icons.load("Health");
				}
				if (interactableVehicle.usesBattery)
				{
					PlayerUI.messageProgress_2.state = (float)interactableVehicle.batteryCharge / 10000f;
					PlayerUI.messageProgress_2.color = Palette.COLOR_Y;
					PlayerUI.messageIcon_2.texture = (Texture2D)PlayerLifeUI.icons.load("Stamina");
				}
				PlayerUI.messageQualityImage.isVisible = false;
				PlayerUI.messageAmountLabel.isVisible = false;
			}
			else if (message == EPlayerMessage.GENERATOR_ON || message == EPlayerMessage.GENERATOR_OFF || message == EPlayerMessage.GROW || message == EPlayerMessage.VOLUME_WATER || message == EPlayerMessage.VOLUME_FUEL)
			{
				PlayerUI.messageBox.sizeOffset_Y = 70;
				PlayerUI.messageProgress_0.isVisible = true;
				PlayerUI.messageIcon_0.isVisible = true;
				PlayerUI.messageProgress_1.isVisible = false;
				PlayerUI.messageIcon_1.isVisible = false;
				PlayerUI.messageProgress_2.isVisible = false;
				PlayerUI.messageIcon_2.isVisible = false;
				if (message == EPlayerMessage.GENERATOR_ON || message == EPlayerMessage.GENERATOR_OFF)
				{
					InteractableGenerator interactableGenerator = (InteractableGenerator)PlayerInteract.interactable;
					PlayerUI.messageProgress_0.state = (float)interactableGenerator.fuel / (float)interactableGenerator.capacity;
					PlayerUI.messageIcon_0.texture = (Texture2D)PlayerLifeUI.icons.load("Fuel");
				}
				else if (message == EPlayerMessage.GROW)
				{
					InteractableFarm interactableFarm = (InteractableFarm)PlayerInteract.interactable;
					float num4 = 0f;
					if (interactableFarm.planted > 0u && Provider.time > interactableFarm.planted)
					{
						num4 = Provider.time - interactableFarm.planted;
					}
					PlayerUI.messageProgress_0.state = num4 / interactableFarm.growth;
					PlayerUI.messageIcon_0.texture = (Texture2D)PlayerLifeUI.icons.load("Grow");
				}
				else if (message == EPlayerMessage.VOLUME_WATER)
				{
					if (PlayerInteract.interactable is InteractableObjectResource)
					{
						InteractableObjectResource interactableObjectResource = (InteractableObjectResource)PlayerInteract.interactable;
						PlayerUI.messageProgress_0.state = (float)interactableObjectResource.amount / (float)interactableObjectResource.capacity;
					}
					else if (PlayerInteract.interactable is InteractableTank)
					{
						InteractableTank interactableTank = (InteractableTank)PlayerInteract.interactable;
						PlayerUI.messageProgress_0.state = (float)interactableTank.amount / (float)interactableTank.capacity;
					}
					else if (PlayerInteract.interactable is InteractableRainBarrel)
					{
						InteractableRainBarrel interactableRainBarrel = (InteractableRainBarrel)PlayerInteract.interactable;
						PlayerUI.messageProgress_0.state = ((!interactableRainBarrel.isFull) ? 0f : 1f);
						if (interactableRainBarrel.isFull)
						{
							text = PlayerLifeUI.localization.format("Full");
						}
						else
						{
							text = PlayerLifeUI.localization.format("Empty");
						}
					}
					PlayerUI.messageIcon_0.texture = (Texture2D)PlayerLifeUI.icons.load("Water");
				}
				else if (message == EPlayerMessage.VOLUME_FUEL)
				{
					if (PlayerInteract.interactable is InteractableObjectResource)
					{
						InteractableObjectResource interactableObjectResource2 = (InteractableObjectResource)PlayerInteract.interactable;
						PlayerUI.messageProgress_0.state = (float)interactableObjectResource2.amount / (float)interactableObjectResource2.capacity;
					}
					else if (PlayerInteract.interactable is InteractableTank)
					{
						InteractableTank interactableTank2 = (InteractableTank)PlayerInteract.interactable;
						PlayerUI.messageProgress_0.state = (float)interactableTank2.amount / (float)interactableTank2.capacity;
					}
					else if (PlayerInteract.interactable is InteractableOil)
					{
						InteractableOil interactableOil = (InteractableOil)PlayerInteract.interactable;
						PlayerUI.messageProgress_0.state = (float)interactableOil.fuel / (float)interactableOil.capacity;
					}
					PlayerUI.messageIcon_0.texture = (Texture2D)PlayerLifeUI.icons.load("Fuel");
				}
				if (message == EPlayerMessage.GROW)
				{
					PlayerUI.messageProgress_0.color = Palette.COLOR_G;
				}
				else if (message == EPlayerMessage.VOLUME_WATER)
				{
					PlayerUI.messageProgress_0.color = Palette.COLOR_B;
				}
				else
				{
					PlayerUI.messageProgress_0.color = Palette.COLOR_Y;
				}
				PlayerUI.messageQualityImage.isVisible = false;
				PlayerUI.messageAmountLabel.isVisible = false;
			}
			else if (message == EPlayerMessage.ITEM)
			{
				PlayerUI.messageBox.sizeOffset_Y = 70;
				if (objects.Length == 2)
				{
					if (((ItemAsset)objects[1]).showQuality)
					{
						PlayerUI.messageQualityImage.backgroundColor = ItemTool.getQualityColor((float)((Item)objects[0]).quality / 100f);
						PlayerUI.messageAmountLabel.text = ((Item)objects[0]).quality + "%";
						PlayerUI.messageQualityImage.isVisible = true;
						PlayerUI.messageAmountLabel.isVisible = true;
					}
					else if (((ItemAsset)objects[1]).amount > 1)
					{
						PlayerUI.messageQualityImage.backgroundColor = Color.white;
						PlayerUI.messageAmountLabel.text = "x" + ((Item)objects[0]).amount;
						PlayerUI.messageQualityImage.isVisible = false;
						PlayerUI.messageAmountLabel.isVisible = true;
					}
					else
					{
						PlayerUI.messageQualityImage.isVisible = false;
						PlayerUI.messageAmountLabel.isVisible = false;
					}
				}
				PlayerUI.messageQualityImage.foregroundColor = PlayerUI.messageQualityImage.backgroundColor;
				PlayerUI.messageAmountLabel.backgroundColor = PlayerUI.messageQualityImage.backgroundColor;
				PlayerUI.messageAmountLabel.foregroundColor = PlayerUI.messageQualityImage.backgroundColor;
				PlayerUI.messageProgress_0.isVisible = false;
				PlayerUI.messageIcon_0.isVisible = false;
				PlayerUI.messageProgress_1.isVisible = false;
				PlayerUI.messageIcon_1.isVisible = false;
				PlayerUI.messageProgress_2.isVisible = false;
				PlayerUI.messageIcon_2.isVisible = false;
			}
			else
			{
				PlayerUI.messageBox.sizeOffset_Y = 50;
				PlayerUI.messageQualityImage.isVisible = false;
				PlayerUI.messageAmountLabel.isVisible = false;
				PlayerUI.messageProgress_0.isVisible = false;
				PlayerUI.messageIcon_0.isVisible = false;
				PlayerUI.messageProgress_1.isVisible = false;
				PlayerUI.messageIcon_1.isVisible = false;
				PlayerUI.messageProgress_2.isVisible = false;
				PlayerUI.messageIcon_2.isVisible = false;
			}
			PlayerUI.messageLabel.isRich = (message == EPlayerMessage.CONDITION);
			PlayerUI.messageBox.sizeOffset_X = 200;
			if (message == EPlayerMessage.ITEM)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Item", new object[]
				{
					text,
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.VEHICLE_ENTER)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Vehicle_Enter", new object[]
				{
					text,
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.DOOR_OPEN)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Door_Open", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.DOOR_CLOSE)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Door_Close", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.LOCKED)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Locked");
			}
			else if (message == EPlayerMessage.BLOCKED)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Blocked");
			}
			else if (message == EPlayerMessage.PILLAR)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Pillar");
			}
			else if (message == EPlayerMessage.POST)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Post");
			}
			else if (message == EPlayerMessage.ROOF)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Roof");
			}
			else if (message == EPlayerMessage.WALL)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Wall");
			}
			else if (message == EPlayerMessage.CORNER)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Corner");
			}
			else if (message == EPlayerMessage.GROUND)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Ground");
			}
			else if (message == EPlayerMessage.DOORWAY)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Doorway");
			}
			else if (message == EPlayerMessage.WINDOW)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Window");
			}
			else if (message == EPlayerMessage.GARAGE)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Garage");
			}
			else if (message == EPlayerMessage.BED_ON)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Bed_On", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact),
					text
				});
			}
			else if (message == EPlayerMessage.BED_OFF)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Bed_Off", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact),
					text
				});
			}
			else if (message == EPlayerMessage.BED_CLAIMED)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Bed_Claimed");
			}
			else if (message == EPlayerMessage.BOUNDS)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Bounds");
			}
			else if (message == EPlayerMessage.STORAGE)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Storage", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.FARM)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Farm", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.GROW)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Grow");
			}
			else if (message == EPlayerMessage.SOIL)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Soil");
			}
			else if (message == EPlayerMessage.FIRE_ON)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Fire_On", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.FIRE_OFF)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Fire_Off", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.FORAGE)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Forage", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.GENERATOR_ON)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Generator_On", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.GENERATOR_OFF)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Generator_Off", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.SPOT_ON)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Spot_On", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.SPOT_OFF)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Spot_Off", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.PURCHASE)
			{
				if (objects.Length == 2)
				{
					PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Purchase", new object[]
					{
						objects[0],
						objects[1],
						MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
					});
				}
			}
			else if (message == EPlayerMessage.POWER)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Power");
			}
			else if (message == EPlayerMessage.USE)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Use", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_MOVE)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Move", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.left),
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.right),
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.up),
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.down)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_LOOK)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Look");
			}
			else if (message == EPlayerMessage.TUTORIAL_JUMP)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Jump", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.jump)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_PERSPECTIVE)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Perspective", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.perspective)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_RUN)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Run", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.sprint)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_INVENTORY)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Inventory", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_SURVIVAL)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Survival", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.inventory),
					MenuConfigurationControlsUI.getKeyCodeText(KeyCode.Mouse1)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_GUN)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Gun", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.secondary),
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.primary)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_LADDER)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Ladder");
			}
			else if (message == EPlayerMessage.TUTORIAL_CRAFT)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Craft", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.attach),
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.crafting)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_SKILLS)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Skills", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.skills)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_SWIM)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Swim", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.jump)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_MEDICAL)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Medical", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.primary)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_VEHICLE)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Vehicle", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.secondary),
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.primary),
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_CROUCH)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Crouch", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.crouch)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_PRONE)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Prone", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.prone)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_EDUCATED)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Educated", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(KeyCode.Escape)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_HARVEST)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Harvest", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_FISH)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Fish", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.primary)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_BUILD)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Build");
			}
			else if (message == EPlayerMessage.TUTORIAL_HORN)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Horn", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.primary)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_LIGHTS)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Lights", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.secondary)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_SIRENS)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Sirens", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.other)
				});
			}
			else if (message == EPlayerMessage.TUTORIAL_FARM)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Farm");
			}
			else if (message == EPlayerMessage.TUTORIAL_POWER)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Power");
			}
			else if (message == EPlayerMessage.TUTORIAL_FIRE)
			{
				PlayerUI.messageBox.sizeOffset_X = 600;
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Tutorial_Fire", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.crafting)
				});
			}
			else if (message == EPlayerMessage.CLAIM)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Claim");
			}
			else if (message == EPlayerMessage.UNDERWATER)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Underwater");
			}
			else if (message == EPlayerMessage.NAV)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Nav");
			}
			else if (message == EPlayerMessage.SPAWN)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Spawn");
			}
			else if (message == EPlayerMessage.MOBILE)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Mobile");
			}
			else if (message == EPlayerMessage.OIL)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Oil");
			}
			else if (message == EPlayerMessage.VOLUME_WATER)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Volume_Water", new object[]
				{
					text
				});
			}
			else if (message == EPlayerMessage.VOLUME_FUEL)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Volume_Fuel");
			}
			else if (message == EPlayerMessage.TRAPDOOR)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Trapdoor");
			}
			else if (message == EPlayerMessage.TALK)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Talk", new object[]
				{
					MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
				});
			}
			else if (message == EPlayerMessage.CONDITION)
			{
				PlayerUI.messageLabel.text = text;
			}
			else if (message == EPlayerMessage.INTERACT)
			{
				PlayerUI.messageLabel.text = string.Format(text, MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact));
			}
			else if (message == EPlayerMessage.SAFEZONE)
			{
				PlayerUI.messageLabel.text = PlayerLifeUI.localization.format("Safezone");
			}
			PlayerUI.messageBox.backgroundColor = color;
			PlayerUI.messageBox.foregroundColor = color;
			PlayerUI.messageLabel.backgroundColor = color;
			PlayerUI.messageLabel.foregroundColor = color;
			if (transform != null && MainCamera.instance != null)
			{
				Vector3 vector = MainCamera.instance.WorldToScreenPoint(transform.position);
				PlayerUI.messageBox.positionOffset_X = (int)(vector.x - (float)(PlayerUI.messageBox.sizeOffset_X / 2));
				PlayerUI.messageBox.positionOffset_Y = (int)((float)Screen.height - vector.y + 10f);
				PlayerUI.messageBox.positionScale_X = 0f;
				PlayerUI.messageBox.positionScale_Y = 0f;
			}
			else
			{
				PlayerUI.messageBox.positionOffset_X = -PlayerUI.messageBox.sizeOffset_X / 2;
				if (PlayerUI.messageBox2.isVisible)
				{
					PlayerUI.messageBox.positionOffset_Y = -80 - PlayerUI.messageBox.sizeOffset_Y - 10 - PlayerUI.messageBox2.sizeOffset_Y;
				}
				else
				{
					PlayerUI.messageBox.positionOffset_Y = -80 - PlayerUI.messageBox.sizeOffset_Y;
				}
				PlayerUI.messageBox.positionScale_X = 0.5f;
				PlayerUI.messageBox.positionScale_Y = 1f;
			}
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x0012C730 File Offset: 0x0012AB30
		public static void hint2(EPlayerMessage message, float progress, float data)
		{
			if (!PlayerUI.isMessaged)
			{
				PlayerUI.messageBox2.isVisible = true;
				PlayerUI.lastHinted2 = true;
				PlayerUI.isHinted2 = true;
				if (message == EPlayerMessage.SALVAGE)
				{
					PlayerUI.messageBox2.sizeOffset_Y = 100;
					PlayerUI.messageBox2.positionOffset_Y = -80 - PlayerUI.messageBox2.sizeOffset_Y;
					PlayerUI.messageIcon2.isVisible = true;
					PlayerUI.messageProgress2_0.isVisible = true;
					PlayerUI.messageProgress2_1.isVisible = true;
					PlayerUI.messageIcon2.texture = (Texture2D)PlayerLifeUI.icons.load("Health");
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Salvage", new object[]
					{
						ControlsSettings.interact
					});
					PlayerUI.messageProgress2_0.state = progress;
					PlayerUI.messageProgress2_0.color = Palette.COLOR_P;
					PlayerUI.messageProgress2_1.state = data;
					PlayerUI.messageProgress2_1.color = Palette.COLOR_R;
				}
			}
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x0012C828 File Offset: 0x0012AC28
		public static void message(EPlayerMessage message, string text)
		{
			if (!OptionsSettings.hints && message != EPlayerMessage.EXPERIENCE && message != EPlayerMessage.MOON_ON && message != EPlayerMessage.MOON_OFF && message != EPlayerMessage.SAFEZONE_ON && message != EPlayerMessage.SAFEZONE_OFF && message != EPlayerMessage.WAVE_ON && message != EPlayerMessage.MOON_OFF && message != EPlayerMessage.DEADZONE_ON && message != EPlayerMessage.DEADZONE_OFF && message != EPlayerMessage.REPUTATION)
			{
				return;
			}
			if (message == EPlayerMessage.NONE)
			{
				PlayerUI.messageBox2.isVisible = false;
				PlayerUI.lastMessage = -999f;
				PlayerUI.isMessaged = false;
			}
			else
			{
				if ((message == EPlayerMessage.EXPERIENCE || message == EPlayerMessage.REPUTATION) && (PlayerNPCDialogueUI.active || PlayerNPCQuestUI.active || PlayerNPCVendorUI.active))
				{
					return;
				}
				PlayerUI.messageBox2.sizeOffset_Y = 50;
				PlayerUI.messageBox2.positionOffset_Y = -80 - PlayerUI.messageBox2.sizeOffset_Y;
				PlayerUI.messageBox2.isVisible = true;
				PlayerUI.messageIcon2.isVisible = false;
				PlayerUI.messageProgress2_0.isVisible = false;
				PlayerUI.messageProgress2_1.isVisible = false;
				PlayerUI.lastMessage = Time.realtimeSinceStartup;
				PlayerUI.isMessaged = true;
				if (message == EPlayerMessage.SPACE)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Space");
				}
				if (message == EPlayerMessage.RELOAD)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Reload", new object[]
					{
						MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.reload)
					});
				}
				else if (message == EPlayerMessage.SAFETY)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Safety", new object[]
					{
						MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.firemode)
					});
				}
				else if (message == EPlayerMessage.VEHICLE_EXIT)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Vehicle_Exit", new object[]
					{
						MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.interact)
					});
				}
				else if (message == EPlayerMessage.VEHICLE_SWAP)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Vehicle_Swap", new object[]
					{
						Player.player.movement.getVehicle().passengers.Length
					});
				}
				else if (message == EPlayerMessage.LIGHT)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Light", new object[]
					{
						MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.tactical)
					});
				}
				else if (message == EPlayerMessage.LASER)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Laser", new object[]
					{
						MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.tactical)
					});
				}
				else if (message == EPlayerMessage.LASER)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Laser", new object[]
					{
						MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.tactical)
					});
				}
				else if (message == EPlayerMessage.RANGEFINDER)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Rangefinder", new object[]
					{
						MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.tactical)
					});
				}
				else if (message == EPlayerMessage.EXPERIENCE)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Experience", new object[]
					{
						text
					});
				}
				else if (message == EPlayerMessage.EMPTY)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Empty");
				}
				else if (message == EPlayerMessage.FULL)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Full");
				}
				else if (message == EPlayerMessage.MOON_ON)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Moon_On");
				}
				else if (message == EPlayerMessage.MOON_OFF)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Moon_Off");
				}
				else if (message == EPlayerMessage.SAFEZONE_ON)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Safezone_On");
				}
				else if (message == EPlayerMessage.SAFEZONE_OFF)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Safezone_Off");
				}
				else if (message == EPlayerMessage.WAVE_ON)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Wave_On");
				}
				else if (message == EPlayerMessage.WAVE_OFF)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Wave_Off");
				}
				else if (message == EPlayerMessage.DEADZONE_ON)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Deadzone_On");
				}
				else if (message == EPlayerMessage.DEADZONE_OFF)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Deadzone_Off");
				}
				else if (message == EPlayerMessage.BUSY)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Busy");
				}
				else if (message == EPlayerMessage.FUEL)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Fuel", new object[]
					{
						text
					});
				}
				else if (message == EPlayerMessage.CLEAN)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Clean");
				}
				else if (message == EPlayerMessage.SALTY)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Salty");
				}
				else if (message == EPlayerMessage.DIRTY)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Dirty");
				}
				else if (message == EPlayerMessage.REPUTATION)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Reputation", new object[]
					{
						text
					});
				}
				else if (message == EPlayerMessage.BAYONET)
				{
					PlayerUI.messageLabel2.text = PlayerLifeUI.localization.format("Bayonet", new object[]
					{
						MenuConfigurationControlsUI.getKeyCodeText(ControlsSettings.tactical)
					});
				}
			}
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x0012CE10 File Offset: 0x0012B210
		private void onVisionUpdated(bool isViewing)
		{
			if (isViewing && (double)UnityEngine.Random.value < 0.5)
			{
				float value = UnityEngine.Random.value;
				if ((double)value < 0.25)
				{
					this.zone.reverbPreset = AudioReverbPreset.Drugged;
				}
				else if ((double)value < 0.5)
				{
					this.zone.reverbPreset = AudioReverbPreset.Psychotic;
				}
				else if ((double)value < 0.75)
				{
					this.zone.reverbPreset = AudioReverbPreset.Arena;
				}
				else
				{
					this.zone.reverbPreset = AudioReverbPreset.SewerPipe;
				}
				this.zone.enabled = true;
			}
			else
			{
				this.zone.enabled = false;
			}
			if (isViewing && (double)UnityEngine.Random.value < 0.5)
			{
				this.twirlScale = UnityEngine.Random.Range(2f, 8f);
				this.twirlSize = UnityEngine.Random.Range(2f, 32f);
				this.twirlSpeed = UnityEngine.Random.Range(0.25f, 1f);
				this.twirl.enabled = true;
			}
			else
			{
				this.twirl.enabled = false;
				this.twirl.angle = 0f;
			}
			if (isViewing && (double)UnityEngine.Random.value < 0.5)
			{
				this.vignetteScale = UnityEngine.Random.Range(2f, 8f);
				this.vignetteSize = UnityEngine.Random.Range(0f, 16f);
				this.vignetteSpeed = UnityEngine.Random.Range(0.25f, 1f);
				this.blurScale = UnityEngine.Random.Range(2f, 8f);
				this.blurSize = UnityEngine.Random.Range(0f, 64f);
				this.blurSpeed = UnityEngine.Random.Range(0.25f, 1f);
				this.spreadScale = UnityEngine.Random.Range(2f, 8f);
				this.spreadSize = UnityEngine.Random.Range(0f, 2f);
				this.spreadSpeed = UnityEngine.Random.Range(0.25f, 1f);
				this.chromaScale = UnityEngine.Random.Range(2f, 8f);
				this.chromaSize = UnityEngine.Random.Range(0f, 64f);
				this.chromaSpeed = UnityEngine.Random.Range(0.25f, 1f);
				this.vignetting.enabled = true;
			}
			else
			{
				this.vignetting.enabled = false;
				this.vignetting.intensity = 0f;
				this.vignetting.blur = 0f;
				this.vignetting.blurSpread = 0f;
				this.vignetting.chromaticAberration = 0f;
			}
			if (isViewing && UnityEngine.Random.value < 0.5f)
			{
				this.colors.saturation = UnityEngine.Random.Range(1f, 2f);
				float value2 = UnityEngine.Random.value;
				if ((double)value2 < 0.25)
				{
					this.colors.redChannel = AnimationCurve.Linear(0f, UnityEngine.Random.Range(0f, 1f), 1f, UnityEngine.Random.Range(0f, 1f));
					this.colors.greenChannel = AnimationCurve.Linear(0f, 0f, 1f, 1f);
					this.colors.blueChannel = AnimationCurve.Linear(0f, 0f, 1f, 1f);
				}
				else if ((double)value2 < 0.5)
				{
					this.colors.redChannel = AnimationCurve.Linear(0f, 0f, 1f, 1f);
					this.colors.greenChannel = AnimationCurve.Linear(0f, UnityEngine.Random.Range(0f, 1f), 1f, UnityEngine.Random.Range(0f, 1f));
					this.colors.blueChannel = AnimationCurve.Linear(0f, 0f, 1f, 1f);
				}
				else if ((double)value2 < 0.75)
				{
					this.colors.redChannel = AnimationCurve.Linear(0f, 0f, 1f, 1f);
					this.colors.greenChannel = AnimationCurve.Linear(0f, 0f, 1f, 1f);
					this.colors.blueChannel = AnimationCurve.Linear(0f, UnityEngine.Random.Range(0f, 1f), 1f, UnityEngine.Random.Range(0f, 1f));
				}
				else
				{
					this.colors.redChannel = AnimationCurve.Linear(0f, UnityEngine.Random.Range(0f, 1f), 1f, UnityEngine.Random.Range(0f, 1f));
					this.colors.greenChannel = AnimationCurve.Linear(0f, UnityEngine.Random.Range(0f, 1f), 1f, UnityEngine.Random.Range(0f, 1f));
					this.colors.blueChannel = AnimationCurve.Linear(0f, UnityEngine.Random.Range(0f, 1f), 1f, UnityEngine.Random.Range(0f, 1f));
				}
				this.colors.UpdateParameters();
				this.colors.enabled = true;
			}
			else
			{
				this.colors.enabled = false;
			}
			if (isViewing && (double)UnityEngine.Random.value < 0.5)
			{
				this.fishScale = UnityEngine.Random.Range(2f, 8f);
				this.fishSize_X = UnityEngine.Random.Range(0.1f, 0.6f);
				this.fishSize_Y = UnityEngine.Random.Range(0.1f, 0.6f);
				this.fishSpeed = UnityEngine.Random.Range(0.25f, 1f);
				this.fish.enabled = true;
			}
			else
			{
				this.fish.enabled = false;
				this.fish.strengthX = 0f;
				this.fish.strengthY = 0f;
			}
			if (isViewing && (double)UnityEngine.Random.value < 0.5)
			{
				this.motionScale = UnityEngine.Random.Range(2f, 8f);
				this.motionSize = UnityEngine.Random.Range(0.1f, 0.92f);
				this.motionSpeed = UnityEngine.Random.Range(0.25f, 1f);
				this.motion.enabled = true;
			}
			else
			{
				this.motion.enabled = false;
				this.motion.blurAmount = 0f;
			}
			if (isViewing && (double)UnityEngine.Random.value < 0.5)
			{
				this.contrastScale = UnityEngine.Random.Range(2f, 8f);
				this.contrastSize = UnityEngine.Random.Range(-3f, 3f);
				this.contrastSpeed = UnityEngine.Random.Range(0.25f, 1f);
				this.contrast.enabled = true;
			}
			else
			{
				this.contrast.enabled = false;
				this.contrast.intensity = 0f;
			}
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x0012D544 File Offset: 0x0012B944
		private void onLifeUpdated(bool isDead)
		{
			PlayerUI.isLocked = false;
			if (isDead)
			{
				PlayerLifeUI.close();
				PlayerDashboardUI.close();
				PlayerBarricadeSignUI.close();
				PlayerBarricadeStereoUI.close();
				PlayerBarricadeLibraryUI.close();
				PlayerBarricadeMannequinUI.close();
				PlayerBrowserRequestUI.close();
				PlayerNPCDialogueUI.close();
				PlayerNPCQuestUI.close();
				PlayerNPCVendorUI.close();
				PlayerWorkzoneUI.close();
				PlayerDeathUI.open();
			}
			else
			{
				PlayerDeathUI.close();
			}
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x0012D5A3 File Offset: 0x0012B9A3
		private void onGlassesUpdated(ushort newGlasses, byte newGlassesQuality, byte[] newGlassesState)
		{
			PlayerUI.isBlindfolded = (Player.player.clothing.glassesAsset != null && Player.player.clothing.glassesAsset.isBlindfold);
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x0012D5D5 File Offset: 0x0012B9D5
		private void onMoonUpdated(bool isFullMoon)
		{
			if (isFullMoon)
			{
				PlayerUI.message(EPlayerMessage.MOON_ON, string.Empty);
			}
			else
			{
				PlayerUI.message(EPlayerMessage.MOON_OFF, string.Empty);
			}
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x0012D5FC File Offset: 0x0012B9FC
		private void OnGUI()
		{
			if (PlayerUI.window == null)
			{
				return;
			}
			if (PlayerUI.isBlindfolded)
			{
				SleekRender.drawImageTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), PlayerUI.stunTexture, Color.black);
			}
			if (Event.current.isKey && Event.current.type == EventType.KeyUp)
			{
				if (Event.current.keyCode == KeyCode.Escape)
				{
					if (PlayerLifeUI.chatting)
					{
						PlayerLifeUI.closeChat();
					}
				}
				else if (Event.current.keyCode == KeyCode.Return)
				{
					if (PlayerLifeUI.chatting)
					{
						if (PlayerLifeUI.chatField.text != string.Empty)
						{
							ChatManager.sendChat(PlayerUI.chat, PlayerLifeUI.chatField.text);
						}
						PlayerLifeUI.closeChat();
					}
					else if (PlayerLifeUI.active && !PlayerUI.window.showCursor)
					{
						PlayerLifeUI.openChat();
					}
				}
				else if (Event.current.keyCode == ControlsSettings.global)
				{
					if (PlayerLifeUI.active && !PlayerUI.window.showCursor && !PlayerLifeUI.chatting)
					{
						PlayerUI._chat = EChatMode.GLOBAL;
						PlayerLifeUI.openChat();
					}
				}
				else if (Event.current.keyCode == ControlsSettings.local)
				{
					if (PlayerLifeUI.active && !PlayerUI.window.showCursor && !PlayerLifeUI.chatting)
					{
						PlayerUI._chat = EChatMode.LOCAL;
						PlayerLifeUI.openChat();
					}
				}
				else if (Event.current.keyCode == ControlsSettings.group && PlayerLifeUI.active && !PlayerUI.window.showCursor && !PlayerLifeUI.chatting)
				{
					PlayerUI._chat = EChatMode.GROUP;
					PlayerLifeUI.openChat();
				}
			}
			if (PlayerLifeUI.chatting)
			{
				GUI.SetNextControlName("Chat");
			}
			PlayerUI.window.draw(false);
			SleekRender.drawImageTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), PlayerUI.stunTexture, PlayerUI.stunColor);
			if (PlayerLifeUI.chatting && GUI.GetNameOfFocusedControl() != "Chat")
			{
				GUI.FocusControl("Chat");
			}
			PlayerDashboardInventoryUI.update();
			MenuConfigurationControlsUI.bindOnGUI();
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x0012D84C File Offset: 0x0012BC4C
		private void Update()
		{
			if (PlayerUI.window == null)
			{
				return;
			}
			MenuConfigurationControlsUI.bindUpdate();
			if (Player.player != null && MainCamera.instance != null && PlayerGroupUI.groups != null && PlayerGroupUI.groups.Count == Provider.clients.Count)
			{
				for (int i = 0; i < PlayerGroupUI.groups.Count; i++)
				{
					SleekLabel sleekLabel = PlayerGroupUI.groups[i];
					SteamPlayer steamPlayer = Provider.clients[i];
					if (sleekLabel != null && steamPlayer != null)
					{
						if (Provider.modeConfigData.Gameplay.Group_HUD && steamPlayer.playerID.steamID != Provider.client && steamPlayer.player.quests.isMemberOfSameGroupAs(Player.player) && steamPlayer.model != null)
						{
							Vector3 vector = MainCamera.instance.WorldToScreenPoint(steamPlayer.model.position + Vector3.up * 3f);
							if (vector.z > 0f && (steamPlayer.model.position - Player.player.transform.position).sqrMagnitude < 262144f)
							{
								sleekLabel.positionOffset_X = (int)(vector.x - 100f);
								sleekLabel.positionOffset_Y = (int)((float)Screen.height - vector.y - 15f);
								sleekLabel.isVisible = true;
							}
							else
							{
								sleekLabel.isVisible = false;
							}
						}
						else
						{
							sleekLabel.isVisible = false;
						}
					}
				}
			}
			PlayerLifeUI.updateCompass();
			PlayerLifeUI.updateHotbar();
			PlayerLifeUI.updateStatTracker();
			if (PlayerLifeUI.painImage != null)
			{
				Color backgroundColor = PlayerLifeUI.painImage.backgroundColor;
				backgroundColor.a = Mathf.Lerp(PlayerLifeUI.painImage.backgroundColor.a, 0f, 2f * Time.deltaTime);
				PlayerLifeUI.painImage.backgroundColor = backgroundColor;
			}
			PlayerUI.stunColor.a = Mathf.Max(0f, PlayerUI.stunColor.a - Time.deltaTime);
			if (PlayerLifeUI.hitmarkers != null && MainCamera.instance != null)
			{
				for (int j = 0; j < PlayerLifeUI.hitmarkers.Length; j++)
				{
					HitmarkerInfo hitmarkerInfo = PlayerLifeUI.hitmarkers[j];
					if (hitmarkerInfo != null)
					{
						if (hitmarkerInfo.hit != EPlayerHit.NONE)
						{
							bool flag = Time.realtimeSinceStartup - hitmarkerInfo.lastHit < PlayerUI.HIT_TIME;
							Vector3 vector2;
							if (hitmarkerInfo.worldspace)
							{
								vector2 = MainCamera.instance.WorldToViewportPoint(hitmarkerInfo.point);
								vector2.y = 1f - vector2.y;
							}
							else
							{
								vector2 = new Vector3(0.5f, 0.5f, 1f);
							}
							hitmarkerInfo.hitEntitiyImage.isVisible = (hitmarkerInfo.hit == EPlayerHit.ENTITIY && flag && vector2.z > 0f);
							hitmarkerInfo.hitCriticalImage.isVisible = (hitmarkerInfo.hit == EPlayerHit.CRITICAL && flag && vector2.z > 0f);
							hitmarkerInfo.hitBuildImage.isVisible = (hitmarkerInfo.hit == EPlayerHit.BUILD && flag && vector2.z > 0f);
							hitmarkerInfo.hitEntitiyImage.positionScale_X = vector2.x;
							hitmarkerInfo.hitEntitiyImage.positionScale_Y = vector2.y;
							hitmarkerInfo.hitCriticalImage.positionScale_X = vector2.x;
							hitmarkerInfo.hitCriticalImage.positionScale_Y = vector2.y;
							hitmarkerInfo.hitBuildImage.positionScale_X = vector2.x;
							hitmarkerInfo.hitBuildImage.positionScale_Y = vector2.y;
							if (!flag)
							{
								hitmarkerInfo.hit = EPlayerHit.NONE;
							}
						}
					}
				}
			}
			if (PlayerUI.isHinted)
			{
				if (!PlayerUI.lastHinted)
				{
					PlayerUI.isHinted = false;
					if (PlayerUI.messageBox != null)
					{
						PlayerUI.messageBox.isVisible = false;
					}
					if (PlayerUI.messagePlayer != null)
					{
						PlayerUI.messagePlayer.isVisible = false;
					}
				}
				PlayerUI.lastHinted = false;
			}
			if (PlayerUI.isMessaged)
			{
				if (Time.realtimeSinceStartup - PlayerUI.lastMessage > PlayerUI.MESSAGE_TIME)
				{
					PlayerUI.isMessaged = false;
					if (!PlayerUI.isHinted2 && PlayerUI.messageBox2 != null)
					{
						PlayerUI.messageBox2.isVisible = false;
					}
				}
			}
			else if (PlayerUI.isHinted2)
			{
				if (!PlayerUI.lastHinted2)
				{
					PlayerUI.isHinted2 = false;
					if (PlayerUI.messageBox2 != null)
					{
						PlayerUI.messageBox2.isVisible = false;
					}
				}
				PlayerUI.lastHinted2 = false;
			}
			if (PlayerLifeUI.isVoteMessaged && Time.realtimeSinceStartup - PlayerLifeUI.lastVoteMessage > 2f)
			{
				PlayerLifeUI.isVoteMessaged = false;
				if (PlayerLifeUI.voteBox != null)
				{
					PlayerLifeUI.voteBox.isVisible = false;
				}
			}
			if (Provider.isServer && (MenuConfigurationOptionsUI.active || MenuConfigurationDisplayUI.active || MenuConfigurationGraphicsUI.active || MenuConfigurationControlsUI.active || PlayerPauseUI.active))
			{
				Time.timeScale = 0f;
			}
			else
			{
				Time.timeScale = 1f;
			}
			if (MenuConfigurationControlsUI.binding == 255)
			{
				if ((Input.GetKeyDown(ControlsSettings.left) || Input.GetKeyDown(ControlsSettings.up) || Input.GetKeyDown(ControlsSettings.right) || Input.GetKeyDown(ControlsSettings.down)) && PlayerDashboardUI.active)
				{
					PlayerDashboardUI.close();
					if (!Player.player.life.isDead)
					{
						PlayerLifeUI.open();
					}
				}
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					if (PlayerDashboardUI.active)
					{
						PlayerDashboardUI.close();
						if (!Player.player.life.isDead)
						{
							PlayerLifeUI.open();
						}
					}
					else if (!Player.player.life.isDead)
					{
						if (PlayerBarricadeSignUI.active)
						{
							PlayerBarricadeSignUI.close();
							PlayerLifeUI.open();
						}
						else if (PlayerBarricadeStereoUI.active)
						{
							PlayerBarricadeStereoUI.close();
							PlayerLifeUI.open();
						}
						else if (PlayerBarricadeLibraryUI.active)
						{
							PlayerBarricadeLibraryUI.close();
							PlayerLifeUI.open();
						}
						else if (PlayerBarricadeMannequinUI.active)
						{
							PlayerBarricadeMannequinUI.close();
							PlayerLifeUI.open();
						}
						else if (PlayerBrowserRequestUI.active)
						{
							PlayerBrowserRequestUI.close();
							PlayerLifeUI.open();
						}
						else if (PlayerNPCDialogueUI.active)
						{
							PlayerNPCDialogueUI.close();
							PlayerLifeUI.open();
						}
						else if (PlayerNPCQuestUI.active)
						{
							PlayerNPCQuestUI.closeNicely();
						}
						else if (PlayerNPCVendorUI.active)
						{
							PlayerNPCVendorUI.closeNicely();
						}
						else if (PlayerWorkzoneUI.active)
						{
							PlayerWorkzoneUI.close();
							PlayerLifeUI.open();
						}
						else if (MenuConfigurationOptionsUI.active)
						{
							MenuConfigurationOptionsUI.close();
							PlayerPauseUI.open();
						}
						else if (MenuConfigurationDisplayUI.active)
						{
							MenuConfigurationDisplayUI.close();
							PlayerPauseUI.open();
						}
						else if (MenuConfigurationGraphicsUI.active)
						{
							MenuConfigurationGraphicsUI.close();
							PlayerPauseUI.open();
						}
						else if (MenuConfigurationControlsUI.active)
						{
							MenuConfigurationControlsUI.close();
							PlayerPauseUI.open();
						}
						else if (PlayerPauseUI.active)
						{
							PlayerPauseUI.close();
							if (!Player.player.life.isDead)
							{
								PlayerLifeUI.open();
							}
						}
						else if (PlayerLifeUI.chatting)
						{
							PlayerLifeUI.closeChat();
						}
						else
						{
							PlayerLifeUI.close();
							PlayerDashboardUI.close();
							PlayerPauseUI.open();
						}
					}
				}
				if (PlayerDeathUI.active)
				{
					if (PlayerDeathUI.homeButton != null)
					{
						if (!Provider.isServer && Provider.isPvP)
						{
							if (Time.realtimeSinceStartup - Player.player.life.lastDeath < Provider.modeConfigData.Gameplay.Timer_Home)
							{
								PlayerDeathUI.homeButton.text = PlayerDeathUI.localization.format("Home_Button_Timer", new object[]
								{
									Mathf.Ceil(Provider.modeConfigData.Gameplay.Timer_Home - (Time.realtimeSinceStartup - Player.player.life.lastDeath))
								});
							}
							else
							{
								PlayerDeathUI.homeButton.text = PlayerDeathUI.localization.format("Home_Button");
							}
						}
						else if (Time.realtimeSinceStartup - Player.player.life.lastRespawn < Provider.modeConfigData.Gameplay.Timer_Respawn)
						{
							PlayerDeathUI.homeButton.text = PlayerDeathUI.localization.format("Home_Button_Timer", new object[]
							{
								Mathf.Ceil(Provider.modeConfigData.Gameplay.Timer_Respawn - (Time.realtimeSinceStartup - Player.player.life.lastRespawn))
							});
						}
						else
						{
							PlayerDeathUI.homeButton.text = PlayerDeathUI.localization.format("Home_Button");
						}
					}
					if (PlayerDeathUI.respawnButton != null)
					{
						if (Time.realtimeSinceStartup - Player.player.life.lastRespawn < Provider.modeConfigData.Gameplay.Timer_Respawn)
						{
							PlayerDeathUI.respawnButton.text = PlayerDeathUI.localization.format("Respawn_Button_Timer", new object[]
							{
								Mathf.Ceil(Provider.modeConfigData.Gameplay.Timer_Respawn - (Time.realtimeSinceStartup - Player.player.life.lastRespawn))
							});
						}
						else
						{
							PlayerDeathUI.respawnButton.text = PlayerDeathUI.localization.format("Respawn_Button");
						}
					}
				}
				if (PlayerPauseUI.active && PlayerPauseUI.exitButton != null)
				{
					if (!Provider.isServer && Provider.isPvP && Provider.clients.Count > 1 && (!Player.player.movement.isSafe || !Player.player.movement.isSafeInfo.noWeapons) && Time.realtimeSinceStartup - PlayerPauseUI.lastLeave < Provider.modeConfigData.Gameplay.Timer_Exit)
					{
						PlayerPauseUI.exitButton.text = PlayerPauseUI.localization.format("Exit_Button_Timer", new object[]
						{
							Mathf.Ceil(Provider.modeConfigData.Gameplay.Timer_Exit - (Time.realtimeSinceStartup - PlayerPauseUI.lastLeave))
						});
					}
					else
					{
						PlayerPauseUI.exitButton.text = PlayerPauseUI.localization.format("Exit_Button_Text");
					}
				}
				if (PlayerNPCDialogueUI.active)
				{
					PlayerNPCDialogueUI.updateText();
				}
				if (PlayerDashboardInformationUI.active)
				{
					PlayerDashboardInformationUI.refreshDynamicMap();
				}
				if (!Player.player.life.isDead)
				{
					if (Input.GetKeyDown(ControlsSettings.dashboard))
					{
						if (PlayerDashboardUI.active)
						{
							PlayerDashboardUI.close();
							PlayerLifeUI.open();
						}
						else if (PlayerBarricadeSignUI.active)
						{
							PlayerBarricadeSignUI.close();
							PlayerLifeUI.open();
						}
						else if (PlayerBarricadeStereoUI.active)
						{
							PlayerBarricadeStereoUI.close();
							PlayerLifeUI.open();
						}
						else if (PlayerBarricadeLibraryUI.active)
						{
							PlayerBarricadeLibraryUI.close();
							PlayerLifeUI.open();
						}
						else if (PlayerBarricadeMannequinUI.active)
						{
							PlayerBarricadeMannequinUI.close();
							PlayerLifeUI.open();
						}
						else if (PlayerNPCDialogueUI.active)
						{
							PlayerNPCDialogueUI.close();
							PlayerLifeUI.open();
						}
						else if (PlayerNPCQuestUI.active)
						{
							PlayerNPCQuestUI.closeNicely();
						}
						else if (PlayerNPCVendorUI.active)
						{
							PlayerNPCVendorUI.closeNicely();
						}
						else if (!PlayerUI.window.showCursor && !PlayerLifeUI.chatting)
						{
							PlayerLifeUI.close();
							PlayerPauseUI.close();
							PlayerDashboardUI.open();
						}
					}
					if (Input.GetKeyDown(ControlsSettings.inventory))
					{
						if (PlayerDashboardUI.active && PlayerDashboardInventoryUI.active)
						{
							PlayerDashboardUI.close();
							PlayerLifeUI.open();
						}
						else if (PlayerDashboardUI.active)
						{
							PlayerDashboardCraftingUI.close();
							PlayerDashboardSkillsUI.close();
							PlayerDashboardInformationUI.close();
							PlayerDashboardInventoryUI.open();
						}
						else if (!PlayerUI.window.showCursor && !PlayerLifeUI.chatting)
						{
							PlayerLifeUI.close();
							PlayerPauseUI.close();
							PlayerDashboardInventoryUI.active = true;
							PlayerDashboardCraftingUI.active = false;
							PlayerDashboardSkillsUI.active = false;
							PlayerDashboardInformationUI.active = false;
							PlayerDashboardUI.open();
						}
					}
					if (Input.GetKeyDown(ControlsSettings.crafting) && Level.info != null && Level.info.type != ELevelType.HORDE)
					{
						if (PlayerDashboardUI.active && PlayerDashboardCraftingUI.active)
						{
							PlayerDashboardUI.close();
							PlayerLifeUI.open();
						}
						else if (PlayerDashboardUI.active)
						{
							PlayerDashboardInventoryUI.close();
							PlayerDashboardSkillsUI.close();
							PlayerDashboardInformationUI.close();
							PlayerDashboardCraftingUI.open();
						}
						else if (!PlayerUI.window.showCursor && !PlayerLifeUI.chatting)
						{
							PlayerLifeUI.close();
							PlayerPauseUI.close();
							PlayerDashboardInventoryUI.active = false;
							PlayerDashboardCraftingUI.active = true;
							PlayerDashboardSkillsUI.active = false;
							PlayerDashboardInformationUI.active = false;
							PlayerDashboardUI.open();
						}
					}
					if (Input.GetKeyDown(ControlsSettings.skills) && Level.info != null && Level.info.type != ELevelType.HORDE)
					{
						if (PlayerDashboardUI.active && PlayerDashboardSkillsUI.active)
						{
							PlayerDashboardUI.close();
							PlayerLifeUI.open();
						}
						else if (PlayerDashboardUI.active)
						{
							PlayerDashboardInventoryUI.close();
							PlayerDashboardCraftingUI.close();
							PlayerDashboardInformationUI.close();
							PlayerDashboardSkillsUI.open();
						}
						else if (!PlayerUI.window.showCursor && !PlayerLifeUI.chatting)
						{
							PlayerLifeUI.close();
							PlayerPauseUI.close();
							PlayerDashboardInventoryUI.active = false;
							PlayerDashboardCraftingUI.active = false;
							PlayerDashboardSkillsUI.active = true;
							PlayerDashboardInformationUI.active = false;
							PlayerDashboardUI.open();
						}
					}
					if (Input.GetKeyDown(ControlsSettings.map) || Input.GetKeyDown(ControlsSettings.quests) || Input.GetKeyDown(ControlsSettings.players))
					{
						if (PlayerDashboardUI.active && PlayerDashboardInformationUI.active)
						{
							PlayerDashboardUI.close();
							PlayerLifeUI.open();
						}
						else
						{
							if (Input.GetKeyDown(ControlsSettings.quests))
							{
								PlayerDashboardInformationUI.openQuests();
							}
							else if (Input.GetKeyDown(ControlsSettings.players))
							{
								PlayerDashboardInformationUI.openPlayers();
							}
							if (PlayerDashboardUI.active)
							{
								PlayerDashboardInventoryUI.close();
								PlayerDashboardCraftingUI.close();
								PlayerDashboardSkillsUI.close();
								PlayerDashboardInformationUI.open();
							}
							else if (!PlayerUI.window.showCursor && !PlayerLifeUI.chatting)
							{
								PlayerLifeUI.close();
								PlayerPauseUI.close();
								PlayerDashboardInventoryUI.active = false;
								PlayerDashboardCraftingUI.active = false;
								PlayerDashboardSkillsUI.active = false;
								PlayerDashboardInformationUI.active = true;
								PlayerDashboardUI.open();
							}
						}
					}
					if (Input.GetKeyDown(ControlsSettings.gesture))
					{
						if (PlayerLifeUI.active && !PlayerUI.window.showCursor && !PlayerLifeUI.chatting)
						{
							PlayerLifeUI.openGestures();
						}
					}
					else if (Input.GetKeyUp(ControlsSettings.gesture) && PlayerLifeUI.active)
					{
						PlayerLifeUI.closeGestures();
					}
				}
				if (PlayerUI.window != null)
				{
					if (Input.GetKeyDown(ControlsSettings.screenshot))
					{
						Provider.takeScreenshot();
					}
					if (Input.GetKeyDown(ControlsSettings.hud))
					{
						DevkitWindowManager.isActive = false;
						PlayerUI.window.isEnabled = !PlayerUI.window.isEnabled;
						PlayerUI.window.drawCursorWhileDisabled = false;
					}
					if (Input.GetKeyDown(ControlsSettings.terminal))
					{
						DevkitWindowManager.isActive = !DevkitWindowManager.isActive;
						PlayerUI.window.isEnabled = !DevkitWindowManager.isActive;
						PlayerUI.window.drawCursorWhileDisabled = DevkitWindowManager.isActive;
					}
				}
				if (Input.GetKeyDown(ControlsSettings.refreshAssets) && Provider.isServer)
				{
					Assets.refresh();
				}
				if (Input.GetKeyDown(ControlsSettings.clipboardDebug))
				{
					string text = string.Empty;
					for (int k = 0; k < Player.player.quests.flagsList.Count; k++)
					{
						if (k > 0)
						{
							text += "\n";
						}
						text += string.Format("{0, 5} {1, 5}", Player.player.quests.flagsList[k].id, Player.player.quests.flagsList[k].value);
					}
					GUIUtility.systemCopyBuffer = text;
				}
			}
			PlayerUI.window.showCursor = (PlayerPauseUI.active || MenuConfigurationOptionsUI.active || MenuConfigurationDisplayUI.active || MenuConfigurationGraphicsUI.active || MenuConfigurationControlsUI.active || PlayerDashboardUI.active || PlayerDeathUI.active || PlayerLifeUI.chatting || PlayerLifeUI.gesturing || PlayerBarricadeSignUI.active || PlayerBarricadeStereoUI.active || PlayerBarricadeLibraryUI.active || PlayerBarricadeMannequinUI.active || PlayerBrowserRequestUI.active || PlayerNPCDialogueUI.active || PlayerNPCQuestUI.active || PlayerNPCVendorUI.active || (PlayerWorkzoneUI.active && !Input.GetKey(ControlsSettings.secondary)) || PlayerUI.isLocked);
			if (this.blur != null)
			{
				if ((PlayerUI.window.showCursor && !MenuConfigurationGraphicsUI.active && !PlayerNPCDialogueUI.active && !PlayerNPCQuestUI.active && !PlayerNPCVendorUI.active && !PlayerWorkzoneUI.active) || (WaterUtility.isPointUnderwater(MainCamera.instance.transform.position) && (Player.player.clothing.glassesAsset == null || !Player.player.clothing.glassesAsset.proofWater)) || (Player.player.look.isScopeActive && GraphicsSettings.scopeQuality != EGraphicQuality.OFF && Player.player.look.perspective == EPlayerPerspective.FIRST && Player.player.equipment.useable != null && ((UseableGun)Player.player.equipment.useable).isAiming))
				{
					if (!this.blur.enabled)
					{
						this.blur.enabled = true;
					}
				}
				else if (this.blur.enabled)
				{
					this.blur.enabled = false;
				}
			}
			if (this.twirl != null && this.twirl.enabled)
			{
				this.twirl.angle = Mathf.Lerp(this.twirl.angle, Mathf.Sin(Time.realtimeSinceStartup / this.twirlScale) * this.twirlSize, Time.deltaTime * this.twirlSpeed);
			}
			if (this.vignetting != null && this.vignetting.enabled)
			{
				this.vignetting.intensity = Mathf.Lerp(this.vignetting.intensity, Mathf.Sin(Time.realtimeSinceStartup / this.vignetteScale) * this.vignetteSize, Time.deltaTime * this.vignetteSpeed);
				this.vignetting.blur = Mathf.Lerp(this.vignetting.blur, Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup / this.blurScale)) * this.blurSize, Time.deltaTime * this.blurSpeed);
				this.vignetting.blurSpread = Mathf.Lerp(this.vignetting.blurSpread, Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup / this.spreadScale)) * this.spreadSize, Time.deltaTime * this.spreadSpeed);
				this.vignetting.chromaticAberration = Mathf.Lerp(this.vignetting.chromaticAberration, Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup / this.chromaScale)) * this.chromaSize, Time.deltaTime * this.chromaSpeed);
			}
			if (this.fish != null && this.fish.enabled)
			{
				this.fish.strengthX = Mathf.Lerp(this.fish.strengthX, 0.4f + Mathf.Sin(Time.realtimeSinceStartup / this.fishScale) * this.fishSize_X, Time.deltaTime * this.fishSpeed);
				this.fish.strengthY = Mathf.Lerp(this.fish.strengthY, 0.4f + Mathf.Cos(Time.realtimeSinceStartup / this.fishScale) * this.fishSize_Y, Time.deltaTime * this.fishSpeed);
			}
			if (this.motion != null && this.motion.enabled)
			{
				this.motion.blurAmount = Mathf.Lerp(this.motion.blurAmount, Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup / this.motionScale)) * this.motionSize, Time.deltaTime * this.motionSpeed);
			}
			if (this.contrast != null && this.contrast.enabled)
			{
				this.contrast.intensity = Mathf.Lerp(this.contrast.intensity, Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup / this.contrastScale)) * this.contrastSize, Time.deltaTime * this.contrastSpeed);
			}
			PlayerUI.window.updateDebug();
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x0012EDCC File Offset: 0x0012D1CC
		private void Awake()
		{
			AudioListener component = LoadingUI.loader.GetComponent<AudioListener>();
			if (component != null)
			{
				UnityEngine.Object.Destroy(component);
			}
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x0012EDF8 File Offset: 0x0012D1F8
		private void Start()
		{
			PlayerUI.isLocked = false;
			PlayerUI._chat = EChatMode.GLOBAL;
			PlayerUI.window = new SleekWindow();
			PlayerUI.ui = this;
			PlayerUI.container = new Sleek();
			PlayerUI.container.sizeScale_X = 1f;
			PlayerUI.container.sizeScale_Y = 1f;
			PlayerUI.window.add(PlayerUI.container);
			PlayerUI.isOverlayed = false;
			PlayerUI.wasOverlayed = false;
			PlayerUI.isReverting = true;
			OptionsSettings.apply();
			GraphicsSettings.apply();
			new PlayerGroupUI();
			new PlayerDashboardUI();
			new PlayerPauseUI();
			new PlayerLifeUI();
			new PlayerDeathUI();
			new PlayerBarricadeSignUI();
			new PlayerBarricadeStereoUI();
			new PlayerBarricadeLibraryUI();
			new PlayerBarricadeMannequinUI();
			new PlayerBrowserRequestUI();
			new PlayerNPCDialogueUI();
			new PlayerNPCQuestUI();
			new PlayerNPCVendorUI();
			new PlayerWorkzoneUI();
			PlayerUI.messageBox = new SleekBox();
			PlayerUI.messageBox.positionOffset_X = -200;
			PlayerUI.messageBox.positionScale_X = 0.5f;
			PlayerUI.messageBox.positionScale_Y = 1f;
			PlayerUI.messageBox.sizeOffset_X = 400;
			PlayerUI.messageBox.backgroundTint = ESleekTint.NONE;
			PlayerUI.messageBox.foregroundTint = ESleekTint.NONE;
			PlayerUI.container.add(PlayerUI.messageBox);
			PlayerUI.messageBox.isVisible = false;
			PlayerUI.messageLabel = new SleekLabel();
			PlayerUI.messageLabel.positionOffset_X = 5;
			PlayerUI.messageLabel.positionOffset_Y = 5;
			PlayerUI.messageLabel.sizeOffset_X = -10;
			PlayerUI.messageLabel.sizeOffset_Y = 40;
			PlayerUI.messageLabel.sizeScale_X = 1f;
			PlayerUI.messageLabel.fontSize = 14;
			PlayerUI.messageLabel.foregroundTint = ESleekTint.NONE;
			PlayerUI.messageBox.add(PlayerUI.messageLabel);
			PlayerUI.messageIcon_0 = new SleekImageTexture();
			PlayerUI.messageIcon_0.positionOffset_X = 5;
			PlayerUI.messageIcon_0.positionOffset_Y = 45;
			PlayerUI.messageIcon_0.sizeOffset_X = 20;
			PlayerUI.messageIcon_0.sizeOffset_Y = 20;
			PlayerUI.messageBox.add(PlayerUI.messageIcon_0);
			PlayerUI.messageIcon_0.isVisible = false;
			PlayerUI.messageIcon_1 = new SleekImageTexture();
			PlayerUI.messageIcon_1.positionOffset_X = 5;
			PlayerUI.messageIcon_1.positionOffset_Y = 75;
			PlayerUI.messageIcon_1.sizeOffset_X = 20;
			PlayerUI.messageIcon_1.sizeOffset_Y = 20;
			PlayerUI.messageBox.add(PlayerUI.messageIcon_1);
			PlayerUI.messageIcon_1.isVisible = false;
			PlayerUI.messageIcon_2 = new SleekImageTexture();
			PlayerUI.messageIcon_2.positionOffset_X = 5;
			PlayerUI.messageIcon_2.positionOffset_Y = 105;
			PlayerUI.messageIcon_2.sizeOffset_X = 20;
			PlayerUI.messageIcon_2.sizeOffset_Y = 20;
			PlayerUI.messageBox.add(PlayerUI.messageIcon_2);
			PlayerUI.messageIcon_2.isVisible = false;
			PlayerUI.messageProgress_0 = new SleekProgress(string.Empty);
			PlayerUI.messageProgress_0.positionOffset_X = 30;
			PlayerUI.messageProgress_0.positionOffset_Y = 50;
			PlayerUI.messageProgress_0.sizeOffset_X = -40;
			PlayerUI.messageProgress_0.sizeOffset_Y = 10;
			PlayerUI.messageProgress_0.sizeScale_X = 1f;
			PlayerUI.messageBox.add(PlayerUI.messageProgress_0);
			PlayerUI.messageProgress_0.isVisible = false;
			PlayerUI.messageProgress_1 = new SleekProgress(string.Empty);
			PlayerUI.messageProgress_1.positionOffset_X = 30;
			PlayerUI.messageProgress_1.positionOffset_Y = 80;
			PlayerUI.messageProgress_1.sizeOffset_X = -40;
			PlayerUI.messageProgress_1.sizeOffset_Y = 10;
			PlayerUI.messageProgress_1.sizeScale_X = 1f;
			PlayerUI.messageBox.add(PlayerUI.messageProgress_1);
			PlayerUI.messageProgress_1.isVisible = false;
			PlayerUI.messageProgress_2 = new SleekProgress(string.Empty);
			PlayerUI.messageProgress_2.positionOffset_X = 30;
			PlayerUI.messageProgress_2.positionOffset_Y = 110;
			PlayerUI.messageProgress_2.sizeOffset_X = -40;
			PlayerUI.messageProgress_2.sizeOffset_Y = 10;
			PlayerUI.messageProgress_2.sizeScale_X = 1f;
			PlayerUI.messageBox.add(PlayerUI.messageProgress_2);
			PlayerUI.messageProgress_2.isVisible = false;
			PlayerUI.messageQualityImage = new SleekImageTexture((Texture2D)PlayerDashboardInventoryUI.icons.load("Quality_0"));
			PlayerUI.messageQualityImage.positionOffset_X = -30;
			PlayerUI.messageQualityImage.positionOffset_Y = -30;
			PlayerUI.messageQualityImage.positionScale_X = 1f;
			PlayerUI.messageQualityImage.positionScale_Y = 1f;
			PlayerUI.messageQualityImage.sizeOffset_X = 20;
			PlayerUI.messageQualityImage.sizeOffset_Y = 20;
			PlayerUI.messageBox.add(PlayerUI.messageQualityImage);
			PlayerUI.messageQualityImage.isVisible = false;
			PlayerUI.messageAmountLabel = new SleekLabel();
			PlayerUI.messageAmountLabel.positionOffset_X = 10;
			PlayerUI.messageAmountLabel.positionOffset_Y = -40;
			PlayerUI.messageAmountLabel.positionScale_Y = 1f;
			PlayerUI.messageAmountLabel.sizeOffset_X = -20;
			PlayerUI.messageAmountLabel.sizeOffset_Y = 30;
			PlayerUI.messageAmountLabel.sizeScale_X = 1f;
			PlayerUI.messageAmountLabel.fontAlignment = TextAnchor.LowerLeft;
			PlayerUI.messageAmountLabel.foregroundTint = ESleekTint.NONE;
			PlayerUI.messageBox.add(PlayerUI.messageAmountLabel);
			PlayerUI.messageAmountLabel.isVisible = false;
			PlayerUI.messageBox2 = new SleekBox();
			PlayerUI.messageBox2.positionOffset_X = -200;
			PlayerUI.messageBox2.positionScale_X = 0.5f;
			PlayerUI.messageBox2.positionScale_Y = 1f;
			PlayerUI.messageBox2.sizeOffset_X = 400;
			PlayerUI.messageBox2.backgroundTint = ESleekTint.NONE;
			PlayerUI.messageBox2.foregroundTint = ESleekTint.NONE;
			PlayerUI.container.add(PlayerUI.messageBox2);
			PlayerUI.messageBox2.isVisible = false;
			PlayerUI.messageLabel2 = new SleekLabel();
			PlayerUI.messageLabel2.positionOffset_X = 5;
			PlayerUI.messageLabel2.positionOffset_Y = 5;
			PlayerUI.messageLabel2.sizeOffset_X = -10;
			PlayerUI.messageLabel2.sizeOffset_Y = 40;
			PlayerUI.messageLabel2.sizeScale_X = 1f;
			PlayerUI.messageLabel2.fontSize = 14;
			PlayerUI.messageLabel2.foregroundTint = ESleekTint.NONE;
			PlayerUI.messageBox2.add(PlayerUI.messageLabel2);
			PlayerUI.messageIcon2 = new SleekImageTexture();
			PlayerUI.messageIcon2.positionOffset_X = 5;
			PlayerUI.messageIcon2.positionOffset_Y = 75;
			PlayerUI.messageIcon2.sizeOffset_X = 20;
			PlayerUI.messageIcon2.sizeOffset_Y = 20;
			PlayerUI.messageBox2.add(PlayerUI.messageIcon2);
			PlayerUI.messageIcon2.isVisible = false;
			PlayerUI.messageProgress2_0 = new SleekProgress(string.Empty);
			PlayerUI.messageProgress2_0.positionOffset_X = 5;
			PlayerUI.messageProgress2_0.positionOffset_Y = 50;
			PlayerUI.messageProgress2_0.sizeOffset_X = -10;
			PlayerUI.messageProgress2_0.sizeOffset_Y = 10;
			PlayerUI.messageProgress2_0.sizeScale_X = 1f;
			PlayerUI.messageBox2.add(PlayerUI.messageProgress2_0);
			PlayerUI.messageProgress2_1 = new SleekProgress(string.Empty);
			PlayerUI.messageProgress2_1.positionOffset_X = 30;
			PlayerUI.messageProgress2_1.positionOffset_Y = 80;
			PlayerUI.messageProgress2_1.sizeOffset_X = -40;
			PlayerUI.messageProgress2_1.sizeOffset_Y = 10;
			PlayerUI.messageProgress2_1.sizeScale_X = 1f;
			PlayerUI.messageBox2.add(PlayerUI.messageProgress2_1);
			PlayerUI.stunTexture = (Texture2D)Resources.Load("Materials/Pixel");
			PlayerUI.stunColor = Color.white;
			PlayerUI.stunColor.a = 0f;
			PlayerUI.isBlindfolded = false;
			PlayerLife life = Player.player.life;
			life.onVisionUpdated = (VisionUpdated)Delegate.Combine(life.onVisionUpdated, new VisionUpdated(this.onVisionUpdated));
			PlayerLife life2 = Player.player.life;
			life2.onLifeUpdated = (LifeUpdated)Delegate.Combine(life2.onLifeUpdated, new LifeUpdated(this.onLifeUpdated));
			PlayerClothing clothing = Player.player.clothing;
			clothing.onGlassesUpdated = (GlassesUpdated)Delegate.Combine(clothing.onGlassesUpdated, new GlassesUpdated(this.onGlassesUpdated));
			LightingManager.onMoonUpdated = (MoonUpdated)Delegate.Combine(LightingManager.onMoonUpdated, new MoonUpdated(this.onMoonUpdated));
			this.blur = base.GetComponent<BlurEffect>();
			this.zone = base.GetComponent<AudioReverbZone>();
			this.twirl = Player.player.animator.view.GetComponent<TwirlEffect>();
			this.vignetting = Player.player.animator.view.GetComponent<Vignetting>();
			this.colors = Player.player.animator.view.GetComponent<ColorCorrectionCurves>();
			this.fish = Player.player.animator.view.GetComponent<Fisheye>();
			this.motion = Player.player.animator.view.GetComponent<MotionBlur>();
			this.contrast = Player.player.animator.view.GetComponent<ContrastEnhance>();
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x0012F661 File Offset: 0x0012DA61
		private void OnDestroy()
		{
			if (PlayerUI.window == null)
			{
				return;
			}
			PlayerUI.window.destroy();
		}

		// Token: 0x04001DC8 RID: 7624
		public static readonly float MESSAGE_TIME = 2f;

		// Token: 0x04001DC9 RID: 7625
		public static readonly float HIT_TIME = 0.33f;

		// Token: 0x04001DCA RID: 7626
		public static SleekWindow window;

		// Token: 0x04001DCB RID: 7627
		public static Sleek container;

		// Token: 0x04001DCC RID: 7628
		private static PlayerUI ui;

		// Token: 0x04001DCD RID: 7629
		private static SleekPlayer messagePlayer;

		// Token: 0x04001DCE RID: 7630
		public static SleekBox messageBox;

		// Token: 0x04001DCF RID: 7631
		private static SleekLabel messageLabel;

		// Token: 0x04001DD0 RID: 7632
		private static SleekProgress messageProgress_0;

		// Token: 0x04001DD1 RID: 7633
		private static SleekProgress messageProgress_1;

		// Token: 0x04001DD2 RID: 7634
		private static SleekProgress messageProgress_2;

		// Token: 0x04001DD3 RID: 7635
		private static SleekImageTexture messageIcon_0;

		// Token: 0x04001DD4 RID: 7636
		private static SleekImageTexture messageIcon_1;

		// Token: 0x04001DD5 RID: 7637
		private static SleekImageTexture messageIcon_2;

		// Token: 0x04001DD6 RID: 7638
		private static SleekImageTexture messageQualityImage;

		// Token: 0x04001DD7 RID: 7639
		private static SleekLabel messageAmountLabel;

		// Token: 0x04001DD8 RID: 7640
		public static SleekBox messageBox2;

		// Token: 0x04001DD9 RID: 7641
		private static SleekLabel messageLabel2;

		// Token: 0x04001DDA RID: 7642
		private static SleekProgress messageProgress2_0;

		// Token: 0x04001DDB RID: 7643
		private static SleekProgress messageProgress2_1;

		// Token: 0x04001DDC RID: 7644
		private static SleekImageTexture messageIcon2;

		// Token: 0x04001DDD RID: 7645
		private static Texture2D stunTexture;

		// Token: 0x04001DDE RID: 7646
		private static Color stunColor;

		// Token: 0x04001DDF RID: 7647
		private static bool _isBlindfolded;

		// Token: 0x04001DE1 RID: 7649
		public static bool isLocked;

		// Token: 0x04001DE2 RID: 7650
		private BlurEffect blur;

		// Token: 0x04001DE3 RID: 7651
		private AudioReverbZone zone;

		// Token: 0x04001DE4 RID: 7652
		private TwirlEffect twirl;

		// Token: 0x04001DE5 RID: 7653
		private Vignetting vignetting;

		// Token: 0x04001DE6 RID: 7654
		private ColorCorrectionCurves colors;

		// Token: 0x04001DE7 RID: 7655
		private Fisheye fish;

		// Token: 0x04001DE8 RID: 7656
		private MotionBlur motion;

		// Token: 0x04001DE9 RID: 7657
		private ContrastEnhance contrast;

		// Token: 0x04001DEA RID: 7658
		private float twirlScale;

		// Token: 0x04001DEB RID: 7659
		private float twirlSize;

		// Token: 0x04001DEC RID: 7660
		private float twirlSpeed;

		// Token: 0x04001DED RID: 7661
		private float vignetteScale;

		// Token: 0x04001DEE RID: 7662
		private float vignetteSize;

		// Token: 0x04001DEF RID: 7663
		private float vignetteSpeed;

		// Token: 0x04001DF0 RID: 7664
		private float blurScale;

		// Token: 0x04001DF1 RID: 7665
		private float blurSize;

		// Token: 0x04001DF2 RID: 7666
		private float blurSpeed;

		// Token: 0x04001DF3 RID: 7667
		private float spreadScale;

		// Token: 0x04001DF4 RID: 7668
		private float spreadSize;

		// Token: 0x04001DF5 RID: 7669
		private float spreadSpeed;

		// Token: 0x04001DF6 RID: 7670
		private float chromaScale;

		// Token: 0x04001DF7 RID: 7671
		private float chromaSize;

		// Token: 0x04001DF8 RID: 7672
		private float chromaSpeed;

		// Token: 0x04001DF9 RID: 7673
		private float fishScale;

		// Token: 0x04001DFA RID: 7674
		private float fishSize_X;

		// Token: 0x04001DFB RID: 7675
		private float fishSize_Y;

		// Token: 0x04001DFC RID: 7676
		private float fishSpeed;

		// Token: 0x04001DFD RID: 7677
		private float motionScale;

		// Token: 0x04001DFE RID: 7678
		private float motionSize;

		// Token: 0x04001DFF RID: 7679
		private float motionSpeed;

		// Token: 0x04001E00 RID: 7680
		private float contrastScale;

		// Token: 0x04001E01 RID: 7681
		private float contrastSize;

		// Token: 0x04001E02 RID: 7682
		private float contrastSpeed;

		// Token: 0x04001E03 RID: 7683
		private static float lastMessage;

		// Token: 0x04001E04 RID: 7684
		private static bool isMessaged;

		// Token: 0x04001E05 RID: 7685
		private static bool lastHinted;

		// Token: 0x04001E06 RID: 7686
		private static bool isHinted;

		// Token: 0x04001E07 RID: 7687
		private static bool lastHinted2;

		// Token: 0x04001E08 RID: 7688
		private static bool isHinted2;

		// Token: 0x04001E09 RID: 7689
		private static bool isOverlayed;

		// Token: 0x04001E0A RID: 7690
		private static bool wasOverlayed;

		// Token: 0x04001E0B RID: 7691
		private static bool isReverting;

		// Token: 0x04001E0C RID: 7692
		private static EChatMode _chat;
	}
}
