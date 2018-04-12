using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000764 RID: 1892
	public class MenuConfigurationControlsUI
	{
		// Token: 0x060035E0 RID: 13792 RVA: 0x0016B0D8 File Offset: 0x001694D8
		public MenuConfigurationControlsUI()
		{
			MenuConfigurationControlsUI.localization = Localization.read("/Menu/Configuration/MenuConfigurationControls.dat");
			MenuConfigurationControlsUI.container = new Sleek();
			MenuConfigurationControlsUI.container.positionOffset_X = 10;
			MenuConfigurationControlsUI.container.positionOffset_Y = 10;
			MenuConfigurationControlsUI.container.positionScale_Y = 1f;
			MenuConfigurationControlsUI.container.sizeOffset_X = -20;
			MenuConfigurationControlsUI.container.sizeOffset_Y = -20;
			MenuConfigurationControlsUI.container.sizeScale_X = 1f;
			MenuConfigurationControlsUI.container.sizeScale_Y = 1f;
			if (Provider.isConnected)
			{
				PlayerUI.container.add(MenuConfigurationControlsUI.container);
			}
			else
			{
				MenuUI.container.add(MenuConfigurationControlsUI.container);
			}
			MenuConfigurationControlsUI.active = false;
			MenuConfigurationControlsUI.binding = byte.MaxValue;
			MenuConfigurationControlsUI.controlsBox = new SleekScrollBox();
			MenuConfigurationControlsUI.controlsBox.positionOffset_X = -200;
			MenuConfigurationControlsUI.controlsBox.positionOffset_Y = 100;
			MenuConfigurationControlsUI.controlsBox.positionScale_X = 0.5f;
			MenuConfigurationControlsUI.controlsBox.sizeOffset_X = 430;
			MenuConfigurationControlsUI.controlsBox.sizeOffset_Y = -200;
			MenuConfigurationControlsUI.controlsBox.sizeScale_Y = 1f;
			MenuConfigurationControlsUI.controlsBox.area = new Rect(0f, 0f, 5f, (float)(380 + (ControlsSettings.bindings.Length + (MenuConfigurationControlsUI.layouts.Length - 1) * 2) * 40 - 10));
			MenuConfigurationControlsUI.container.add(MenuConfigurationControlsUI.controlsBox);
			MenuConfigurationControlsUI.sensitivityField = new SleekSingleField();
			MenuConfigurationControlsUI.sensitivityField.positionOffset_Y = 100;
			MenuConfigurationControlsUI.sensitivityField.sizeOffset_X = 200;
			MenuConfigurationControlsUI.sensitivityField.sizeOffset_Y = 30;
			MenuConfigurationControlsUI.sensitivityField.addLabel(MenuConfigurationControlsUI.localization.format("Sensitivity_Field_Label"), ESleekSide.RIGHT);
			SleekSingleField sleekSingleField = MenuConfigurationControlsUI.sensitivityField;
			if (MenuConfigurationControlsUI.<>f__mg$cache0 == null)
			{
				MenuConfigurationControlsUI.<>f__mg$cache0 = new TypedSingle(MenuConfigurationControlsUI.onTypedSensitivityField);
			}
			sleekSingleField.onTypedSingle = MenuConfigurationControlsUI.<>f__mg$cache0;
			MenuConfigurationControlsUI.controlsBox.add(MenuConfigurationControlsUI.sensitivityField);
			MenuConfigurationControlsUI.invertToggle = new SleekToggle();
			MenuConfigurationControlsUI.invertToggle.sizeOffset_X = 40;
			MenuConfigurationControlsUI.invertToggle.sizeOffset_Y = 40;
			MenuConfigurationControlsUI.invertToggle.addLabel(MenuConfigurationControlsUI.localization.format("Invert_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle = MenuConfigurationControlsUI.invertToggle;
			if (MenuConfigurationControlsUI.<>f__mg$cache1 == null)
			{
				MenuConfigurationControlsUI.<>f__mg$cache1 = new Toggled(MenuConfigurationControlsUI.onToggledInvertToggle);
			}
			sleekToggle.onToggled = MenuConfigurationControlsUI.<>f__mg$cache1;
			MenuConfigurationControlsUI.controlsBox.add(MenuConfigurationControlsUI.invertToggle);
			MenuConfigurationControlsUI.invertFlightToggle = new SleekToggle();
			MenuConfigurationControlsUI.invertFlightToggle.positionOffset_Y = 50;
			MenuConfigurationControlsUI.invertFlightToggle.sizeOffset_X = 40;
			MenuConfigurationControlsUI.invertFlightToggle.sizeOffset_Y = 40;
			MenuConfigurationControlsUI.invertFlightToggle.addLabel(MenuConfigurationControlsUI.localization.format("Invert_Flight_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle2 = MenuConfigurationControlsUI.invertFlightToggle;
			if (MenuConfigurationControlsUI.<>f__mg$cache2 == null)
			{
				MenuConfigurationControlsUI.<>f__mg$cache2 = new Toggled(MenuConfigurationControlsUI.onToggledInvertFlightToggle);
			}
			sleekToggle2.onToggled = MenuConfigurationControlsUI.<>f__mg$cache2;
			MenuConfigurationControlsUI.controlsBox.add(MenuConfigurationControlsUI.invertFlightToggle);
			MenuConfigurationControlsUI.aimingButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationControlsUI.localization.format("Hold")),
				new GUIContent(MenuConfigurationControlsUI.localization.format("Toggle"))
			});
			MenuConfigurationControlsUI.aimingButton.positionOffset_Y = 140;
			MenuConfigurationControlsUI.aimingButton.sizeOffset_X = 200;
			MenuConfigurationControlsUI.aimingButton.sizeOffset_Y = 30;
			MenuConfigurationControlsUI.aimingButton.addLabel(MenuConfigurationControlsUI.localization.format("Aiming_Label"), ESleekSide.RIGHT);
			SleekButtonState sleekButtonState = MenuConfigurationControlsUI.aimingButton;
			if (MenuConfigurationControlsUI.<>f__mg$cache3 == null)
			{
				MenuConfigurationControlsUI.<>f__mg$cache3 = new SwappedState(MenuConfigurationControlsUI.onSwappedAimingState);
			}
			sleekButtonState.onSwappedState = MenuConfigurationControlsUI.<>f__mg$cache3;
			MenuConfigurationControlsUI.controlsBox.add(MenuConfigurationControlsUI.aimingButton);
			MenuConfigurationControlsUI.crouchingButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationControlsUI.localization.format("Hold")),
				new GUIContent(MenuConfigurationControlsUI.localization.format("Toggle"))
			});
			MenuConfigurationControlsUI.crouchingButton.positionOffset_Y = 180;
			MenuConfigurationControlsUI.crouchingButton.sizeOffset_X = 200;
			MenuConfigurationControlsUI.crouchingButton.sizeOffset_Y = 30;
			MenuConfigurationControlsUI.crouchingButton.addLabel(MenuConfigurationControlsUI.localization.format("Crouching_Label"), ESleekSide.RIGHT);
			SleekButtonState sleekButtonState2 = MenuConfigurationControlsUI.crouchingButton;
			if (MenuConfigurationControlsUI.<>f__mg$cache4 == null)
			{
				MenuConfigurationControlsUI.<>f__mg$cache4 = new SwappedState(MenuConfigurationControlsUI.onSwappedCrouchingState);
			}
			sleekButtonState2.onSwappedState = MenuConfigurationControlsUI.<>f__mg$cache4;
			MenuConfigurationControlsUI.controlsBox.add(MenuConfigurationControlsUI.crouchingButton);
			MenuConfigurationControlsUI.proningButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationControlsUI.localization.format("Hold")),
				new GUIContent(MenuConfigurationControlsUI.localization.format("Toggle"))
			});
			MenuConfigurationControlsUI.proningButton.positionOffset_Y = 220;
			MenuConfigurationControlsUI.proningButton.sizeOffset_X = 200;
			MenuConfigurationControlsUI.proningButton.sizeOffset_Y = 30;
			MenuConfigurationControlsUI.proningButton.addLabel(MenuConfigurationControlsUI.localization.format("Proning_Label"), ESleekSide.RIGHT);
			SleekButtonState sleekButtonState3 = MenuConfigurationControlsUI.proningButton;
			if (MenuConfigurationControlsUI.<>f__mg$cache5 == null)
			{
				MenuConfigurationControlsUI.<>f__mg$cache5 = new SwappedState(MenuConfigurationControlsUI.onSwappedProningState);
			}
			sleekButtonState3.onSwappedState = MenuConfigurationControlsUI.<>f__mg$cache5;
			MenuConfigurationControlsUI.controlsBox.add(MenuConfigurationControlsUI.proningButton);
			MenuConfigurationControlsUI.sprintingButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationControlsUI.localization.format("Hold")),
				new GUIContent(MenuConfigurationControlsUI.localization.format("Toggle"))
			});
			MenuConfigurationControlsUI.sprintingButton.positionOffset_Y = 260;
			MenuConfigurationControlsUI.sprintingButton.sizeOffset_X = 200;
			MenuConfigurationControlsUI.sprintingButton.sizeOffset_Y = 30;
			MenuConfigurationControlsUI.sprintingButton.addLabel(MenuConfigurationControlsUI.localization.format("Sprinting_Label"), ESleekSide.RIGHT);
			SleekButtonState sleekButtonState4 = MenuConfigurationControlsUI.sprintingButton;
			if (MenuConfigurationControlsUI.<>f__mg$cache6 == null)
			{
				MenuConfigurationControlsUI.<>f__mg$cache6 = new SwappedState(MenuConfigurationControlsUI.onSwappedSprintingState);
			}
			sleekButtonState4.onSwappedState = MenuConfigurationControlsUI.<>f__mg$cache6;
			MenuConfigurationControlsUI.controlsBox.add(MenuConfigurationControlsUI.sprintingButton);
			MenuConfigurationControlsUI.leaningButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationControlsUI.localization.format("Hold")),
				new GUIContent(MenuConfigurationControlsUI.localization.format("Toggle"))
			});
			MenuConfigurationControlsUI.leaningButton.positionOffset_Y = 300;
			MenuConfigurationControlsUI.leaningButton.sizeOffset_X = 200;
			MenuConfigurationControlsUI.leaningButton.sizeOffset_Y = 30;
			MenuConfigurationControlsUI.leaningButton.addLabel(MenuConfigurationControlsUI.localization.format("Leaning_Label"), ESleekSide.RIGHT);
			SleekButtonState sleekButtonState5 = MenuConfigurationControlsUI.leaningButton;
			if (MenuConfigurationControlsUI.<>f__mg$cache7 == null)
			{
				MenuConfigurationControlsUI.<>f__mg$cache7 = new SwappedState(MenuConfigurationControlsUI.onSwappedLeaningState);
			}
			sleekButtonState5.onSwappedState = MenuConfigurationControlsUI.<>f__mg$cache7;
			MenuConfigurationControlsUI.controlsBox.add(MenuConfigurationControlsUI.leaningButton);
			MenuConfigurationControlsUI.buttons = new SleekButton[ControlsSettings.bindings.Length];
			byte b = 0;
			byte b2 = 0;
			while ((int)b2 < MenuConfigurationControlsUI.layouts.Length)
			{
				SleekBox sleekBox = new SleekBox();
				sleekBox.positionOffset_Y = 340 + (int)((b + b2 * 2) * 40);
				sleekBox.sizeOffset_X = -30;
				sleekBox.sizeOffset_Y = 30;
				sleekBox.sizeScale_X = 1f;
				sleekBox.text = MenuConfigurationControlsUI.localization.format("Layout_" + b2);
				MenuConfigurationControlsUI.controlsBox.add(sleekBox);
				byte b3 = 0;
				while ((int)b3 < MenuConfigurationControlsUI.layouts[(int)b2].Length)
				{
					SleekButton sleekButton = new SleekButton();
					sleekButton.positionOffset_Y = (int)((b3 + 1) * 40);
					sleekButton.sizeOffset_Y = 30;
					sleekButton.sizeScale_X = 1f;
					SleekButton sleekButton2 = sleekButton;
					if (MenuConfigurationControlsUI.<>f__mg$cache8 == null)
					{
						MenuConfigurationControlsUI.<>f__mg$cache8 = new ClickedButton(MenuConfigurationControlsUI.onClickedKeyButton);
					}
					sleekButton2.onClickedButton = MenuConfigurationControlsUI.<>f__mg$cache8;
					sleekBox.add(sleekButton);
					MenuConfigurationControlsUI.buttons[(int)MenuConfigurationControlsUI.layouts[(int)b2][(int)b3]] = sleekButton;
					b += 1;
					b3 += 1;
				}
				b2 += 1;
			}
			MenuConfigurationControlsUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuConfigurationControlsUI.backButton.positionOffset_Y = -50;
			MenuConfigurationControlsUI.backButton.positionScale_Y = 1f;
			MenuConfigurationControlsUI.backButton.sizeOffset_X = 200;
			MenuConfigurationControlsUI.backButton.sizeOffset_Y = 50;
			MenuConfigurationControlsUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuConfigurationControlsUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton3 = MenuConfigurationControlsUI.backButton;
			if (MenuConfigurationControlsUI.<>f__mg$cache9 == null)
			{
				MenuConfigurationControlsUI.<>f__mg$cache9 = new ClickedButton(MenuConfigurationControlsUI.onClickedBackButton);
			}
			sleekButton3.onClickedButton = MenuConfigurationControlsUI.<>f__mg$cache9;
			MenuConfigurationControlsUI.backButton.fontSize = 14;
			MenuConfigurationControlsUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuConfigurationControlsUI.container.add(MenuConfigurationControlsUI.backButton);
			MenuConfigurationControlsUI.defaultButton = new SleekButton();
			MenuConfigurationControlsUI.defaultButton.positionOffset_X = -200;
			MenuConfigurationControlsUI.defaultButton.positionOffset_Y = -50;
			MenuConfigurationControlsUI.defaultButton.positionScale_X = 1f;
			MenuConfigurationControlsUI.defaultButton.positionScale_Y = 1f;
			MenuConfigurationControlsUI.defaultButton.sizeOffset_X = 200;
			MenuConfigurationControlsUI.defaultButton.sizeOffset_Y = 50;
			MenuConfigurationControlsUI.defaultButton.text = MenuPlayConfigUI.localization.format("Default");
			MenuConfigurationControlsUI.defaultButton.tooltip = MenuPlayConfigUI.localization.format("Default_Tooltip");
			SleekButton sleekButton4 = MenuConfigurationControlsUI.defaultButton;
			if (MenuConfigurationControlsUI.<>f__mg$cacheA == null)
			{
				MenuConfigurationControlsUI.<>f__mg$cacheA = new ClickedButton(MenuConfigurationControlsUI.onClickedDefaultButton);
			}
			sleekButton4.onClickedButton = MenuConfigurationControlsUI.<>f__mg$cacheA;
			MenuConfigurationControlsUI.defaultButton.fontSize = 14;
			MenuConfigurationControlsUI.container.add(MenuConfigurationControlsUI.defaultButton);
			MenuConfigurationControlsUI.updateAll();
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x0016BA16 File Offset: 0x00169E16
		public static void open()
		{
			if (MenuConfigurationControlsUI.active)
			{
				return;
			}
			MenuConfigurationControlsUI.active = true;
			MenuConfigurationControlsUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x0016BA43 File Offset: 0x00169E43
		public static void close()
		{
			if (!MenuConfigurationControlsUI.active)
			{
				return;
			}
			MenuConfigurationControlsUI.active = false;
			MenuConfigurationControlsUI.binding = byte.MaxValue;
			MenuConfigurationControlsUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x0016BA7A File Offset: 0x00169E7A
		public static void cancel()
		{
			MenuConfigurationControlsUI.binding = byte.MaxValue;
			SleekRender.allowInput = true;
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x0016BA8C File Offset: 0x00169E8C
		public static void bind(KeyCode key)
		{
			ControlsSettings.bind(MenuConfigurationControlsUI.binding, key);
			MenuConfigurationControlsUI.updateButton(MenuConfigurationControlsUI.binding);
			MenuConfigurationControlsUI.cancel();
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x0016BAA8 File Offset: 0x00169EA8
		public static string getKeyCodeText(KeyCode key)
		{
			if (MenuConfigurationControlsUI.localizationKeyCodes == null)
			{
				MenuConfigurationControlsUI.localizationKeyCodes = Localization.read("/Shared/KeyCodes.dat");
			}
			string text = key.ToString();
			if (MenuConfigurationControlsUI.localizationKeyCodes.has(text))
			{
				text = MenuConfigurationControlsUI.localizationKeyCodes.format(text);
			}
			return text;
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x0016BAFC File Offset: 0x00169EFC
		public static void updateButton(byte index)
		{
			KeyCode key = ControlsSettings.bindings[(int)index].key;
			string keyCodeText = MenuConfigurationControlsUI.getKeyCodeText(key);
			MenuConfigurationControlsUI.buttons[(int)index].text = MenuConfigurationControlsUI.localization.format("Key_" + index + "_Button", new object[]
			{
				keyCodeText
			});
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x0016BB52 File Offset: 0x00169F52
		private static void onTypedSensitivityField(SleekSingleField field, float state)
		{
			ControlsSettings.sensitivity = state;
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x0016BB5A File Offset: 0x00169F5A
		private static void onToggledInvertToggle(SleekToggle toggle, bool state)
		{
			ControlsSettings.invert = state;
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x0016BB62 File Offset: 0x00169F62
		private static void onToggledInvertFlightToggle(SleekToggle toggle, bool state)
		{
			ControlsSettings.invertFlight = state;
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x0016BB6A File Offset: 0x00169F6A
		private static void onSwappedAimingState(SleekButtonState button, int index)
		{
			ControlsSettings.aiming = (EControlMode)index;
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x0016BB72 File Offset: 0x00169F72
		private static void onSwappedCrouchingState(SleekButtonState button, int index)
		{
			ControlsSettings.crouching = (EControlMode)index;
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x0016BB7A File Offset: 0x00169F7A
		private static void onSwappedProningState(SleekButtonState button, int index)
		{
			ControlsSettings.proning = (EControlMode)index;
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x0016BB82 File Offset: 0x00169F82
		private static void onSwappedSprintingState(SleekButtonState button, int index)
		{
			ControlsSettings.sprinting = (EControlMode)index;
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x0016BB8A File Offset: 0x00169F8A
		private static void onSwappedLeaningState(SleekButtonState button, int index)
		{
			ControlsSettings.leaning = (EControlMode)index;
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x0016BB94 File Offset: 0x00169F94
		private static void onClickedKeyButton(SleekButton button)
		{
			SleekRender.allowInput = false;
			MenuConfigurationControlsUI.binding = 0;
			while ((int)MenuConfigurationControlsUI.binding < MenuConfigurationControlsUI.buttons.Length)
			{
				if (MenuConfigurationControlsUI.buttons[(int)MenuConfigurationControlsUI.binding] == button)
				{
					break;
				}
				MenuConfigurationControlsUI.binding += 1;
			}
			button.text = MenuConfigurationControlsUI.localization.format("Key_" + MenuConfigurationControlsUI.binding + "_Button", new object[]
			{
				"?"
			});
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x0016BC20 File Offset: 0x0016A020
		public static void bindOnGUI()
		{
			if (MenuConfigurationControlsUI.binding != 255)
			{
				if (Event.current.type == EventType.KeyDown)
				{
					if (Event.current.keyCode == KeyCode.Backspace)
					{
						MenuConfigurationControlsUI.updateButton(MenuConfigurationControlsUI.binding);
						MenuConfigurationControlsUI.cancel();
					}
					else if (Event.current.keyCode != KeyCode.Escape && Event.current.keyCode != KeyCode.Insert)
					{
						MenuConfigurationControlsUI.bind(Event.current.keyCode);
					}
				}
				else if (Event.current.type == EventType.MouseDown)
				{
					if (Event.current.button == 0)
					{
						MenuConfigurationControlsUI.bind(KeyCode.Mouse0);
					}
					else if (Event.current.button == 1)
					{
						MenuConfigurationControlsUI.bind(KeyCode.Mouse1);
					}
					else if (Event.current.button == 2)
					{
						MenuConfigurationControlsUI.bind(KeyCode.Mouse2);
					}
					else if (Event.current.button == 3)
					{
						MenuConfigurationControlsUI.bind(KeyCode.Mouse3);
					}
					else if (Event.current.button == 4)
					{
						MenuConfigurationControlsUI.bind(KeyCode.Mouse4);
					}
					else if (Event.current.button == 5)
					{
						MenuConfigurationControlsUI.bind(KeyCode.Mouse5);
					}
					else if (Event.current.button == 6)
					{
						MenuConfigurationControlsUI.bind(KeyCode.Mouse6);
					}
				}
				else if (Event.current.shift)
				{
					MenuConfigurationControlsUI.bind(KeyCode.LeftShift);
				}
			}
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x0016BDAC File Offset: 0x0016A1AC
		public static void bindUpdate()
		{
			if (MenuConfigurationControlsUI.binding != 255)
			{
				if (Input.GetKeyDown(KeyCode.Mouse3))
				{
					MenuConfigurationControlsUI.bind(KeyCode.Mouse3);
				}
				else if (Input.GetKeyDown(KeyCode.Mouse4))
				{
					MenuConfigurationControlsUI.bind(KeyCode.Mouse4);
				}
				else if (Input.GetKeyDown(KeyCode.Mouse5))
				{
					MenuConfigurationControlsUI.bind(KeyCode.Mouse5);
				}
				else if (Input.GetKeyDown(KeyCode.Mouse6))
				{
					MenuConfigurationControlsUI.bind(KeyCode.Mouse6);
				}
			}
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x0016BE3B File Offset: 0x0016A23B
		private static void onClickedBackButton(SleekButton button)
		{
			if (Player.player != null)
			{
				PlayerPauseUI.open();
			}
			else
			{
				MenuConfigurationUI.open();
			}
			MenuConfigurationControlsUI.close();
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x0016BE61 File Offset: 0x0016A261
		private static void onClickedDefaultButton(SleekButton button)
		{
			ControlsSettings.restoreDefaults();
			MenuConfigurationControlsUI.updateAll();
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x0016BE70 File Offset: 0x0016A270
		private static void updateAll()
		{
			byte b = 0;
			while ((int)b < MenuConfigurationControlsUI.layouts.Length)
			{
				byte b2 = 0;
				while ((int)b2 < MenuConfigurationControlsUI.layouts[(int)b].Length)
				{
					MenuConfigurationControlsUI.updateButton(MenuConfigurationControlsUI.layouts[(int)b][(int)b2]);
					b2 += 1;
				}
				b += 1;
			}
			MenuConfigurationControlsUI.leaningButton.state = (int)ControlsSettings.leaning;
			MenuConfigurationControlsUI.sprintingButton.state = (int)ControlsSettings.sprinting;
			MenuConfigurationControlsUI.proningButton.state = (int)ControlsSettings.proning;
			MenuConfigurationControlsUI.crouchingButton.state = (int)ControlsSettings.crouching;
			MenuConfigurationControlsUI.aimingButton.state = (int)ControlsSettings.aiming;
			MenuConfigurationControlsUI.sensitivityField.state = ControlsSettings.sensitivity;
			MenuConfigurationControlsUI.invertToggle.state = ControlsSettings.invert;
			MenuConfigurationControlsUI.invertFlightToggle.state = ControlsSettings.invert;
		}

		// Token: 0x040025F2 RID: 9714
		private static byte[][] layouts = new byte[][]
		{
			new byte[]
			{
				ControlsSettings.UP,
				ControlsSettings.DOWN,
				ControlsSettings.LEFT,
				ControlsSettings.RIGHT,
				ControlsSettings.JUMP,
				ControlsSettings.SPRINT
			},
			new byte[]
			{
				ControlsSettings.CROUCH,
				ControlsSettings.PRONE,
				ControlsSettings.STANCE,
				ControlsSettings.LEAN_LEFT,
				ControlsSettings.LEAN_RIGHT,
				ControlsSettings.PERSPECTIVE,
				ControlsSettings.GESTURE
			},
			new byte[]
			{
				ControlsSettings.INTERACT,
				ControlsSettings.PRIMARY,
				ControlsSettings.SECONDARY
			},
			new byte[]
			{
				ControlsSettings.RELOAD,
				ControlsSettings.ATTACH,
				ControlsSettings.FIREMODE,
				ControlsSettings.TACTICAL,
				ControlsSettings.VISION,
				ControlsSettings.INSPECT,
				ControlsSettings.ROTATE,
				ControlsSettings.DEQUIP
			},
			new byte[]
			{
				ControlsSettings.VOICE,
				ControlsSettings.GLOBAL,
				ControlsSettings.LOCAL,
				ControlsSettings.GROUP
			},
			new byte[]
			{
				ControlsSettings.HUD,
				ControlsSettings.OTHER,
				ControlsSettings.DASHBOARD,
				ControlsSettings.INVENTORY,
				ControlsSettings.CRAFTING,
				ControlsSettings.SKILLS,
				ControlsSettings.MAP,
				ControlsSettings.QUESTS,
				ControlsSettings.PLAYERS
			},
			new byte[]
			{
				ControlsSettings.LOCKER,
				ControlsSettings.ROLL_LEFT,
				ControlsSettings.ROLL_RIGHT,
				ControlsSettings.PITCH_UP,
				ControlsSettings.PITCH_DOWN,
				ControlsSettings.YAW_LEFT,
				ControlsSettings.YAW_RIGHT,
				ControlsSettings.THRUST_INCREASE,
				ControlsSettings.THRUST_DECREASE
			},
			new byte[]
			{
				ControlsSettings.MODIFY,
				ControlsSettings.SNAP,
				ControlsSettings.FOCUS,
				ControlsSettings.ASCEND,
				ControlsSettings.DESCEND,
				ControlsSettings.TOOL_0,
				ControlsSettings.TOOL_1,
				ControlsSettings.TOOL_2,
				ControlsSettings.TOOL_3,
				ControlsSettings.TERMINAL,
				ControlsSettings.SCREENSHOT,
				ControlsSettings.REFRESH_ASSETS,
				ControlsSettings.CLIPBOARD_DEBUG
			}
		};

		// Token: 0x040025F3 RID: 9715
		private static Local localization;

		// Token: 0x040025F4 RID: 9716
		private static Local localizationKeyCodes;

		// Token: 0x040025F5 RID: 9717
		private static Sleek container;

		// Token: 0x040025F6 RID: 9718
		public static bool active;

		// Token: 0x040025F7 RID: 9719
		private static SleekButtonIcon backButton;

		// Token: 0x040025F8 RID: 9720
		private static SleekButton defaultButton;

		// Token: 0x040025F9 RID: 9721
		private static SleekSingleField sensitivityField;

		// Token: 0x040025FA RID: 9722
		private static SleekToggle invertToggle;

		// Token: 0x040025FB RID: 9723
		private static SleekToggle invertFlightToggle;

		// Token: 0x040025FC RID: 9724
		private static SleekScrollBox controlsBox;

		// Token: 0x040025FD RID: 9725
		private static SleekButton[] buttons;

		// Token: 0x040025FE RID: 9726
		private static SleekButtonState aimingButton;

		// Token: 0x040025FF RID: 9727
		private static SleekButtonState crouchingButton;

		// Token: 0x04002600 RID: 9728
		private static SleekButtonState proningButton;

		// Token: 0x04002601 RID: 9729
		private static SleekButtonState sprintingButton;

		// Token: 0x04002602 RID: 9730
		private static SleekButtonState leaningButton;

		// Token: 0x04002603 RID: 9731
		public static byte binding;

		// Token: 0x04002604 RID: 9732
		[CompilerGenerated]
		private static TypedSingle <>f__mg$cache0;

		// Token: 0x04002605 RID: 9733
		[CompilerGenerated]
		private static Toggled <>f__mg$cache1;

		// Token: 0x04002606 RID: 9734
		[CompilerGenerated]
		private static Toggled <>f__mg$cache2;

		// Token: 0x04002607 RID: 9735
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache3;

		// Token: 0x04002608 RID: 9736
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache4;

		// Token: 0x04002609 RID: 9737
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache5;

		// Token: 0x0400260A RID: 9738
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache6;

		// Token: 0x0400260B RID: 9739
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache7;

		// Token: 0x0400260C RID: 9740
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache8;

		// Token: 0x0400260D RID: 9741
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache9;

		// Token: 0x0400260E RID: 9742
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheA;
	}
}
