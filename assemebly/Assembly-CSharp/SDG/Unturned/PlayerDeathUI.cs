using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200079B RID: 1947
	public class PlayerDeathUI
	{
		// Token: 0x0600387F RID: 14463 RVA: 0x00198F68 File Offset: 0x00197368
		public PlayerDeathUI()
		{
			PlayerDeathUI.localization = Localization.read("/Player/PlayerDeath.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Player/Icons/PlayerDeath/PlayerDeath.unity3d");
			PlayerDeathUI.container = new Sleek();
			PlayerDeathUI.container.positionScale_Y = 1f;
			PlayerDeathUI.container.positionOffset_X = 10;
			PlayerDeathUI.container.positionOffset_Y = 10;
			PlayerDeathUI.container.sizeOffset_X = -20;
			PlayerDeathUI.container.sizeOffset_Y = -20;
			PlayerDeathUI.container.sizeScale_X = 1f;
			PlayerDeathUI.container.sizeScale_Y = 1f;
			PlayerUI.container.add(PlayerDeathUI.container);
			PlayerDeathUI.active = false;
			PlayerDeathUI.causeBox = new SleekBox();
			PlayerDeathUI.causeBox.positionOffset_Y = -25;
			PlayerDeathUI.causeBox.positionScale_Y = 0.8f;
			PlayerDeathUI.causeBox.sizeOffset_Y = 50;
			PlayerDeathUI.causeBox.sizeScale_X = 1f;
			PlayerDeathUI.container.add(PlayerDeathUI.causeBox);
			PlayerDeathUI.homeButton = new SleekButtonIcon((Texture2D)bundle.load("Home"));
			PlayerDeathUI.homeButton.positionOffset_X = -205;
			PlayerDeathUI.homeButton.positionOffset_Y = 35;
			PlayerDeathUI.homeButton.positionScale_X = 0.5f;
			PlayerDeathUI.homeButton.positionScale_Y = 0.8f;
			PlayerDeathUI.homeButton.sizeOffset_X = 200;
			PlayerDeathUI.homeButton.sizeOffset_Y = 30;
			PlayerDeathUI.homeButton.text = PlayerDeathUI.localization.format("Home_Button");
			PlayerDeathUI.homeButton.tooltip = PlayerDeathUI.localization.format("Home_Button_Tooltip");
			PlayerDeathUI.homeButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton = PlayerDeathUI.homeButton;
			if (PlayerDeathUI.<>f__mg$cache0 == null)
			{
				PlayerDeathUI.<>f__mg$cache0 = new ClickedButton(PlayerDeathUI.onClickedHomeButton);
			}
			sleekButton.onClickedButton = PlayerDeathUI.<>f__mg$cache0;
			PlayerDeathUI.container.add(PlayerDeathUI.homeButton);
			PlayerDeathUI.respawnButton = new SleekButtonIcon((Texture2D)bundle.load("Respawn"));
			PlayerDeathUI.respawnButton.positionOffset_X = 5;
			PlayerDeathUI.respawnButton.positionOffset_Y = 35;
			PlayerDeathUI.respawnButton.positionScale_X = 0.5f;
			PlayerDeathUI.respawnButton.positionScale_Y = 0.8f;
			PlayerDeathUI.respawnButton.sizeOffset_X = 200;
			PlayerDeathUI.respawnButton.sizeOffset_Y = 30;
			PlayerDeathUI.respawnButton.text = PlayerDeathUI.localization.format("Respawn_Button");
			PlayerDeathUI.respawnButton.tooltip = PlayerDeathUI.localization.format("Respawn_Button_Tooltip");
			PlayerDeathUI.respawnButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			SleekButton sleekButton2 = PlayerDeathUI.respawnButton;
			if (PlayerDeathUI.<>f__mg$cache1 == null)
			{
				PlayerDeathUI.<>f__mg$cache1 = new ClickedButton(PlayerDeathUI.onClickedRespawnButton);
			}
			sleekButton2.onClickedButton = PlayerDeathUI.<>f__mg$cache1;
			PlayerDeathUI.container.add(PlayerDeathUI.respawnButton);
			bundle.unload();
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x00199230 File Offset: 0x00197630
		public static void open()
		{
			if (PlayerDeathUI.active)
			{
				return;
			}
			PlayerDeathUI.active = true;
			PlayerLifeUI.close();
			if (PlayerLife.deathCause == EDeathCause.BLEEDING)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Bleeding");
			}
			else if (PlayerLife.deathCause == EDeathCause.BONES)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Bones");
			}
			else if (PlayerLife.deathCause == EDeathCause.FREEZING)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Freezing");
			}
			else if (PlayerLife.deathCause == EDeathCause.BURNING)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Burning");
			}
			else if (PlayerLife.deathCause == EDeathCause.FOOD)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Food");
			}
			else if (PlayerLife.deathCause == EDeathCause.WATER)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Water");
			}
			else if (PlayerLife.deathCause == EDeathCause.GUN || PlayerLife.deathCause == EDeathCause.MELEE || PlayerLife.deathCause == EDeathCause.PUNCH || PlayerLife.deathCause == EDeathCause.ROADKILL || PlayerLife.deathCause == EDeathCause.GRENADE || PlayerLife.deathCause == EDeathCause.MISSILE || PlayerLife.deathCause == EDeathCause.CHARGE || PlayerLife.deathCause == EDeathCause.SPLASH)
			{
				SteamPlayer steamPlayer = PlayerTool.getSteamPlayer(PlayerLife.deathKiller);
				string text;
				string text2;
				if (steamPlayer != null)
				{
					text = steamPlayer.playerID.characterName;
					text2 = steamPlayer.playerID.playerName;
				}
				else
				{
					text = "?";
					text2 = "?";
				}
				string text3 = string.Empty;
				if (PlayerLife.deathLimb == ELimb.LEFT_FOOT || PlayerLife.deathLimb == ELimb.LEFT_LEG || PlayerLife.deathLimb == ELimb.RIGHT_FOOT || PlayerLife.deathLimb == ELimb.RIGHT_LEG)
				{
					text3 = PlayerDeathUI.localization.format("Leg");
				}
				else if (PlayerLife.deathLimb == ELimb.LEFT_HAND || PlayerLife.deathLimb == ELimb.LEFT_ARM || PlayerLife.deathLimb == ELimb.RIGHT_HAND || PlayerLife.deathLimb == ELimb.RIGHT_ARM)
				{
					text3 = PlayerDeathUI.localization.format("Arm");
				}
				else if (PlayerLife.deathLimb == ELimb.SPINE)
				{
					text3 = PlayerDeathUI.localization.format("Spine");
				}
				else if (PlayerLife.deathLimb == ELimb.SKULL)
				{
					text3 = PlayerDeathUI.localization.format("Skull");
				}
				if (PlayerLife.deathCause == EDeathCause.GUN)
				{
					PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Gun", new object[]
					{
						text3,
						text,
						text2
					});
				}
				else if (PlayerLife.deathCause == EDeathCause.MELEE)
				{
					PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Melee", new object[]
					{
						text3,
						text,
						text2
					});
				}
				else if (PlayerLife.deathCause == EDeathCause.PUNCH)
				{
					PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Punch", new object[]
					{
						text3,
						text,
						text2
					});
				}
				else if (PlayerLife.deathCause == EDeathCause.ROADKILL)
				{
					PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Roadkill", new object[]
					{
						text,
						text2
					});
				}
				else if (PlayerLife.deathCause == EDeathCause.GRENADE)
				{
					PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Grenade", new object[]
					{
						text,
						text2
					});
				}
				else if (PlayerLife.deathCause == EDeathCause.MISSILE)
				{
					PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Missile", new object[]
					{
						text,
						text2
					});
				}
				else if (PlayerLife.deathCause == EDeathCause.CHARGE)
				{
					PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Charge", new object[]
					{
						text,
						text2
					});
				}
				else if (PlayerLife.deathCause == EDeathCause.SPLASH)
				{
					PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Splash", new object[]
					{
						text,
						text2
					});
				}
			}
			else if (PlayerLife.deathCause == EDeathCause.ZOMBIE)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Zombie");
			}
			else if (PlayerLife.deathCause == EDeathCause.ANIMAL)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Animal");
			}
			else if (PlayerLife.deathCause == EDeathCause.SUICIDE)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Suicide");
			}
			else if (PlayerLife.deathCause == EDeathCause.KILL)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Kill");
			}
			else if (PlayerLife.deathCause == EDeathCause.INFECTION)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Infection");
			}
			else if (PlayerLife.deathCause == EDeathCause.BREATH)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Breath");
			}
			else if (PlayerLife.deathCause == EDeathCause.ZOMBIE)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Zombie");
			}
			else if (PlayerLife.deathCause == EDeathCause.VEHICLE)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Vehicle");
			}
			else if (PlayerLife.deathCause == EDeathCause.SHRED)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Shred");
			}
			else if (PlayerLife.deathCause == EDeathCause.LANDMINE)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Landmine");
			}
			else if (PlayerLife.deathCause == EDeathCause.ARENA)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Arena");
			}
			else if (PlayerLife.deathCause == EDeathCause.SENTRY)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Sentry");
			}
			else if (PlayerLife.deathCause == EDeathCause.ACID)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Acid");
			}
			else if (PlayerLife.deathCause == EDeathCause.BOULDER)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Boulder");
			}
			else if (PlayerLife.deathCause == EDeathCause.BURNER)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Burner");
			}
			else if (PlayerLife.deathCause == EDeathCause.SPIT)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Spit");
			}
			else if (PlayerLife.deathCause == EDeathCause.SPARK)
			{
				PlayerDeathUI.causeBox.text = PlayerDeathUI.localization.format("Spark");
			}
			if (PlayerLife.deathCause != EDeathCause.SUICIDE && OptionsSettings.music)
			{
				MainCamera.instance.GetComponent<AudioSource>().Play();
			}
			PlayerDeathUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x00199970 File Offset: 0x00197D70
		public static void close()
		{
			if (!PlayerDeathUI.active)
			{
				return;
			}
			PlayerDeathUI.active = false;
			PlayerLifeUI.open();
			MainCamera.instance.GetComponent<AudioSource>().Stop();
			PlayerDeathUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x001999BC File Offset: 0x00197DBC
		private static void onClickedHomeButton(SleekButton button)
		{
			if (!Provider.isServer && Provider.isPvP)
			{
				if (Time.realtimeSinceStartup - Player.player.life.lastDeath < Provider.modeConfigData.Gameplay.Timer_Home)
				{
					return;
				}
			}
			else if (Time.realtimeSinceStartup - Player.player.life.lastRespawn < Provider.modeConfigData.Gameplay.Timer_Respawn)
			{
				return;
			}
			Player.player.life.sendRespawn(true);
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x00199A4A File Offset: 0x00197E4A
		private static void onClickedRespawnButton(SleekButton button)
		{
			if (Time.realtimeSinceStartup - Player.player.life.lastRespawn < Provider.modeConfigData.Gameplay.Timer_Respawn)
			{
				return;
			}
			Player.player.life.sendRespawn(false);
		}

		// Token: 0x04002AC0 RID: 10944
		private static Sleek container;

		// Token: 0x04002AC1 RID: 10945
		public static Local localization;

		// Token: 0x04002AC2 RID: 10946
		public static bool active;

		// Token: 0x04002AC3 RID: 10947
		private static SleekBox causeBox;

		// Token: 0x04002AC4 RID: 10948
		public static SleekButtonIcon homeButton;

		// Token: 0x04002AC5 RID: 10949
		public static SleekButtonIcon respawnButton;

		// Token: 0x04002AC6 RID: 10950
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x04002AC7 RID: 10951
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;
	}
}
