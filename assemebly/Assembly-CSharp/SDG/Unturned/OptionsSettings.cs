using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SDG.Unturned
{
	// Token: 0x0200069E RID: 1694
	public class OptionsSettings
	{
		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x060031B6 RID: 12726 RVA: 0x00142A25 File Offset: 0x00140E25
		// (set) Token: 0x060031B7 RID: 12727 RVA: 0x00142A2C File Offset: 0x00140E2C
		public static float fov
		{
			get
			{
				return OptionsSettings._fov;
			}
			set
			{
				OptionsSettings._fov = value;
				OptionsSettings._view = (float)OptionsSettings.MIN_FOV + (float)OptionsSettings.MAX_FOV * value;
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x060031B8 RID: 12728 RVA: 0x00142A48 File Offset: 0x00140E48
		public static float view
		{
			get
			{
				return OptionsSettings._view;
			}
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x00142A50 File Offset: 0x00140E50
		public static void apply()
		{
			if (!Level.isLoaded && MainCamera.instance != null && !Level.isVR && !Dedicator.isVR)
			{
				MainCamera.instance.fieldOfView = OptionsSettings.view;
			}
			if (SceneManager.GetActiveScene().buildIndex <= Level.MENU)
			{
				MenuConfigurationOptions.apply();
			}
			AudioListener.volume = OptionsSettings.volume;
			if (LevelLighting.dayAudio != null)
			{
				if (!LevelLighting.dayAudio.enabled && OptionsSettings.ambience)
				{
					LevelLighting.dayAudio.enabled = true;
					LevelLighting.dayAudio.Play();
				}
				else
				{
					LevelLighting.dayAudio.enabled = OptionsSettings.ambience;
				}
			}
			if (LevelLighting.nightAudio != null)
			{
				if (!LevelLighting.nightAudio.enabled && OptionsSettings.ambience)
				{
					LevelLighting.nightAudio.enabled = true;
					LevelLighting.nightAudio.Play();
				}
				else
				{
					LevelLighting.nightAudio.enabled = OptionsSettings.ambience;
				}
			}
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x00142B64 File Offset: 0x00140F64
		public static void restoreDefaults()
		{
			OptionsSettings.music = true;
			OptionsSettings.splashscreen = true;
			OptionsSettings.timer = false;
			OptionsSettings.fov = 0.75f;
			OptionsSettings.volume = 1f;
			OptionsSettings.voice = 1f;
			OptionsSettings.debug = false;
			OptionsSettings.gore = true;
			OptionsSettings.filter = false;
			OptionsSettings.chatText = true;
			OptionsSettings.chatVoiceIn = true;
			OptionsSettings.chatVoiceOut = true;
			OptionsSettings.metric = true;
			OptionsSettings.talk = false;
			OptionsSettings.hints = true;
			OptionsSettings.ambience = true;
			OptionsSettings.proUI = true;
			OptionsSettings.hitmarker = false;
			OptionsSettings.streamer = false;
			OptionsSettings.featuredWorkshop = true;
			OptionsSettings.matchmakingShowAllMaps = false;
			OptionsSettings.showHotbar = true;
			OptionsSettings.minMatchmakingPlayers = 12;
			OptionsSettings.maxMatchmakingPing = 300;
			OptionsSettings.crosshairColor = Color.white;
			OptionsSettings.hitmarkerColor = Color.white;
			OptionsSettings.criticalHitmarkerColor = Color.red;
			OptionsSettings.cursorColor = Color.white;
			OptionsSettings.backgroundColor = new Color(0.9f, 0.9f, 0.9f);
			OptionsSettings.foregroundColor = new Color(0.9f, 0.9f, 0.9f);
			OptionsSettings.fontColor = new Color(0.9f, 0.9f, 0.9f);
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x00142C88 File Offset: 0x00141088
		public static void load()
		{
			OptionsSettings.restoreDefaults();
			if (ReadWrite.fileExists("/Options.dat", true))
			{
				Block block = ReadWrite.readBlock("/Options.dat", true, 0);
				if (block != null)
				{
					byte b = block.readByte();
					if (b > 2)
					{
						OptionsSettings.music = block.readBoolean();
						if (b < 31)
						{
							OptionsSettings.splashscreen = true;
						}
						else
						{
							OptionsSettings.splashscreen = block.readBoolean();
						}
						if (b < 20)
						{
							OptionsSettings.timer = false;
						}
						else
						{
							OptionsSettings.timer = block.readBoolean();
						}
						if (b < 10)
						{
							block.readBoolean();
						}
						if (b > 7)
						{
							OptionsSettings.fov = block.readSingle();
						}
						else
						{
							OptionsSettings.fov = block.readSingle() * 0.5f;
						}
						if (b < 24)
						{
							OptionsSettings.fov *= 1.5f;
							OptionsSettings.fov = Mathf.Clamp01(OptionsSettings.fov);
						}
						if (b > 4)
						{
							OptionsSettings.volume = block.readSingle();
						}
						else
						{
							OptionsSettings.volume = 1f;
						}
						if (b > 22)
						{
							OptionsSettings.voice = block.readSingle();
						}
						else
						{
							OptionsSettings.voice = 1f;
						}
						OptionsSettings.debug = block.readBoolean();
						OptionsSettings.gore = block.readBoolean();
						OptionsSettings.filter = block.readBoolean();
						OptionsSettings.chatText = block.readBoolean();
						if (b > 8)
						{
							OptionsSettings.chatVoiceIn = block.readBoolean();
						}
						else
						{
							OptionsSettings.chatVoiceIn = true;
						}
						OptionsSettings.chatVoiceOut = block.readBoolean();
						OptionsSettings.metric = block.readBoolean();
						if (b > 24)
						{
							OptionsSettings.talk = block.readBoolean();
						}
						else
						{
							OptionsSettings.talk = false;
						}
						if (b > 3)
						{
							OptionsSettings.hints = block.readBoolean();
						}
						else
						{
							OptionsSettings.hints = true;
						}
						if (b > 13)
						{
							OptionsSettings.ambience = block.readBoolean();
						}
						else
						{
							OptionsSettings.ambience = true;
						}
						if (b > 12)
						{
							OptionsSettings.proUI = block.readBoolean();
						}
						else
						{
							OptionsSettings.proUI = true;
						}
						if (b > 20)
						{
							OptionsSettings.hitmarker = block.readBoolean();
						}
						else
						{
							OptionsSettings.hitmarker = false;
						}
						if (b > 21)
						{
							OptionsSettings.streamer = block.readBoolean();
						}
						else
						{
							OptionsSettings.streamer = false;
						}
						if (b > 25)
						{
							OptionsSettings.featuredWorkshop = block.readBoolean();
						}
						else
						{
							OptionsSettings.featuredWorkshop = true;
						}
						if (b > 28)
						{
							OptionsSettings.matchmakingShowAllMaps = block.readBoolean();
						}
						else
						{
							OptionsSettings.matchmakingShowAllMaps = false;
						}
						if (b > 29)
						{
							OptionsSettings.showHotbar = block.readBoolean();
						}
						else
						{
							OptionsSettings.showHotbar = true;
						}
						if (b > 27)
						{
							OptionsSettings.minMatchmakingPlayers = block.readInt32();
						}
						else
						{
							OptionsSettings.minMatchmakingPlayers = 12;
						}
						if (b > 26)
						{
							OptionsSettings.maxMatchmakingPing = block.readInt32();
						}
						else
						{
							OptionsSettings.maxMatchmakingPing = 300;
						}
						if (b > 6)
						{
							OptionsSettings.crosshairColor = block.readColor();
							OptionsSettings.hitmarkerColor = block.readColor();
							OptionsSettings.criticalHitmarkerColor = block.readColor();
							OptionsSettings.cursorColor = block.readColor();
						}
						else
						{
							OptionsSettings.crosshairColor = Color.white;
							OptionsSettings.hitmarkerColor = Color.white;
							OptionsSettings.criticalHitmarkerColor = Color.red;
							OptionsSettings.cursorColor = Color.white;
						}
						if (b > 18)
						{
							OptionsSettings.backgroundColor = block.readColor();
							OptionsSettings.foregroundColor = block.readColor();
							OptionsSettings.fontColor = block.readColor();
						}
						else
						{
							OptionsSettings.backgroundColor = new Color(0.9f, 0.9f, 0.9f);
							OptionsSettings.foregroundColor = new Color(0.9f, 0.9f, 0.9f);
							OptionsSettings.fontColor = new Color(0.9f, 0.9f, 0.9f);
						}
						return;
					}
				}
			}
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x0014303C File Offset: 0x0014143C
		public static void save()
		{
			Block block = new Block();
			block.writeByte(OptionsSettings.SAVEDATA_VERSION);
			block.writeBoolean(OptionsSettings.music);
			block.writeBoolean(OptionsSettings.splashscreen);
			block.writeBoolean(OptionsSettings.timer);
			block.writeSingle(OptionsSettings.fov);
			block.writeSingle(OptionsSettings.volume);
			block.writeSingle(OptionsSettings.voice);
			block.writeBoolean(OptionsSettings.debug);
			block.writeBoolean(OptionsSettings.gore);
			block.writeBoolean(OptionsSettings.filter);
			block.writeBoolean(OptionsSettings.chatText);
			block.writeBoolean(OptionsSettings.chatVoiceIn);
			block.writeBoolean(OptionsSettings.chatVoiceOut);
			block.writeBoolean(OptionsSettings.metric);
			block.writeBoolean(OptionsSettings.talk);
			block.writeBoolean(OptionsSettings.hints);
			block.writeBoolean(OptionsSettings.ambience);
			block.writeBoolean(OptionsSettings.proUI);
			block.writeBoolean(OptionsSettings.hitmarker);
			block.writeBoolean(OptionsSettings.streamer);
			block.writeBoolean(OptionsSettings.featuredWorkshop);
			block.writeBoolean(OptionsSettings.matchmakingShowAllMaps);
			block.writeBoolean(OptionsSettings.showHotbar);
			block.writeInt32(OptionsSettings.minMatchmakingPlayers);
			block.writeInt32(OptionsSettings.maxMatchmakingPing);
			block.writeColor(OptionsSettings.crosshairColor);
			block.writeColor(OptionsSettings.hitmarkerColor);
			block.writeColor(OptionsSettings.criticalHitmarkerColor);
			block.writeColor(OptionsSettings.cursorColor);
			block.writeColor(OptionsSettings.backgroundColor);
			block.writeColor(OptionsSettings.foregroundColor);
			block.writeColor(OptionsSettings.fontColor);
			ReadWrite.writeBlock("/Options.dat", true, block);
		}

		// Token: 0x040020E6 RID: 8422
		public static readonly byte SAVEDATA_VERSION = 31;

		// Token: 0x040020E7 RID: 8423
		public static readonly byte MIN_FOV = 60;

		// Token: 0x040020E8 RID: 8424
		public static readonly byte MAX_FOV = 40;

		// Token: 0x040020E9 RID: 8425
		private static float _fov;

		// Token: 0x040020EA RID: 8426
		private static float _view;

		// Token: 0x040020EB RID: 8427
		public static float volume;

		// Token: 0x040020EC RID: 8428
		public static float voice;

		// Token: 0x040020ED RID: 8429
		public static bool debug;

		// Token: 0x040020EE RID: 8430
		public static bool music;

		// Token: 0x040020EF RID: 8431
		public static bool splashscreen;

		// Token: 0x040020F0 RID: 8432
		public static bool timer;

		// Token: 0x040020F1 RID: 8433
		public static bool gore;

		// Token: 0x040020F2 RID: 8434
		public static bool filter;

		// Token: 0x040020F3 RID: 8435
		public static bool chatText;

		// Token: 0x040020F4 RID: 8436
		public static bool chatVoiceIn;

		// Token: 0x040020F5 RID: 8437
		public static bool chatVoiceOut;

		// Token: 0x040020F6 RID: 8438
		public static bool metric;

		// Token: 0x040020F7 RID: 8439
		public static bool talk;

		// Token: 0x040020F8 RID: 8440
		public static bool hints;

		// Token: 0x040020F9 RID: 8441
		public static bool ambience;

		// Token: 0x040020FA RID: 8442
		public static bool proUI;

		// Token: 0x040020FB RID: 8443
		public static bool hitmarker;

		// Token: 0x040020FC RID: 8444
		public static bool streamer;

		// Token: 0x040020FD RID: 8445
		public static bool featuredWorkshop;

		// Token: 0x040020FE RID: 8446
		public static bool matchmakingShowAllMaps;

		// Token: 0x040020FF RID: 8447
		public static bool showHotbar;

		// Token: 0x04002100 RID: 8448
		public static int minMatchmakingPlayers;

		// Token: 0x04002101 RID: 8449
		public static int maxMatchmakingPing;

		// Token: 0x04002102 RID: 8450
		public static Color crosshairColor;

		// Token: 0x04002103 RID: 8451
		public static Color hitmarkerColor;

		// Token: 0x04002104 RID: 8452
		public static Color criticalHitmarkerColor;

		// Token: 0x04002105 RID: 8453
		public static Color cursorColor;

		// Token: 0x04002106 RID: 8454
		public static Color backgroundColor;

		// Token: 0x04002107 RID: 8455
		public static Color foregroundColor;

		// Token: 0x04002108 RID: 8456
		public static Color fontColor;
	}
}
