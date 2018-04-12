using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005AF RID: 1455
	public class LightingManager : SteamCaller
	{
		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060028A7 RID: 10407 RVA: 0x000F6F66 File Offset: 0x000F5366
		public static float day
		{
			get
			{
				return LightingManager.time / LightingManager.cycle;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060028A8 RID: 10408 RVA: 0x000F6F77 File Offset: 0x000F5377
		// (set) Token: 0x060028A9 RID: 10409 RVA: 0x000F6F80 File Offset: 0x000F5380
		public static uint cycle
		{
			get
			{
				return LightingManager._cycle;
			}
			set
			{
				LightingManager._offset = Provider.time - (uint)(LightingManager.day * value);
				LightingManager._cycle = value;
				if (Provider.isServer)
				{
					LightingManager.manager.updateLighting();
					LightingManager.manager.channel.send("tellLightingCycle", ESteamCall.OTHERS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						LightingManager.cycle
					});
				}
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x000F6FE6 File Offset: 0x000F53E6
		// (set) Token: 0x060028AB RID: 10411 RVA: 0x000F6FF0 File Offset: 0x000F53F0
		public static uint time
		{
			get
			{
				return LightingManager._time;
			}
			set
			{
				value %= LightingManager.cycle;
				LightingManager._offset = Provider.time - value;
				LightingManager._time = value;
				LightingManager.manager.updateLighting();
				LightingManager.manager.channel.send("tellLightingOffset", ESteamCall.OTHERS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					LightingManager.offset
				});
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060028AC RID: 10412 RVA: 0x000F704C File Offset: 0x000F544C
		public static uint offset
		{
			get
			{
				return LightingManager._offset;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x000F7053 File Offset: 0x000F5453
		public static bool hasRain
		{
			get
			{
				return LightingManager._hasRain;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x060028AE RID: 10414 RVA: 0x000F705A File Offset: 0x000F545A
		public static bool hasSnow
		{
			get
			{
				return LightingManager._hasSnow;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x000F708D File Offset: 0x000F548D
		// (set) Token: 0x060028AF RID: 10415 RVA: 0x000F7061 File Offset: 0x000F5461
		public static bool isFullMoon
		{
			get
			{
				return LightingManager._isFullMoon;
			}
			set
			{
				if (value != LightingManager.isFullMoon)
				{
					LightingManager._isFullMoon = value;
					if (LightingManager.onMoonUpdated != null)
					{
						LightingManager.onMoonUpdated(LightingManager.isFullMoon);
					}
				}
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x000F7094 File Offset: 0x000F5494
		public static bool isDaytime
		{
			get
			{
				return LightingManager.day < LevelLighting.bias;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x000F70A2 File Offset: 0x000F54A2
		public static bool isNighttime
		{
			get
			{
				return !LightingManager.isDaytime;
			}
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x000F70AC File Offset: 0x000F54AC
		[SteamCall]
		public void tellLighting(CSteamID steamID, uint serverTime, uint newCycle, uint newOffset, byte moon, byte wind, byte rain, byte snow)
		{
			if (base.channel.checkServer(steamID))
			{
				Provider.time = serverTime;
				LightingManager._cycle = newCycle;
				LightingManager._offset = newOffset;
				this.updateLighting();
				LevelLighting.moon = moon;
				LightingManager.isCycled = (LightingManager.day > LevelLighting.bias);
				LightingManager.isFullMoon = (LightingManager.isCycled && LevelLighting.moon == 2);
				if (LightingManager.onDayNightUpdated != null)
				{
					LightingManager.onDayNightUpdated(LightingManager.isDaytime);
				}
				LevelLighting.wind = (float)wind * 2f;
				LevelLighting.rainyness = (ELightingRain)rain;
				LevelLighting.snowyness = (ELightingSnow)snow;
				if (LightingManager.onRainUpdated != null)
				{
					LightingManager.onRainUpdated(LevelLighting.rainyness);
				}
				if (LightingManager.onSnowUpdated != null)
				{
					LightingManager.onSnowUpdated(LevelLighting.snowyness);
				}
				Level.isLoadingLighting = false;
			}
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x000F7184 File Offset: 0x000F5584
		[SteamCall]
		public void askLighting(CSteamID steamID)
		{
			base.channel.send("tellLighting", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				Provider.time,
				LightingManager.cycle,
				LightingManager.offset,
				LevelLighting.moon,
				MeasurementTool.angleToByte(LevelLighting.wind),
				(byte)LevelLighting.rainyness,
				(byte)LevelLighting.snowyness
			});
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x000F720C File Offset: 0x000F560C
		[SteamCall]
		public void tellLightingCycle(CSteamID steamID, uint newScale)
		{
			if (base.channel.checkServer(steamID))
			{
				LightingManager._offset = Provider.time - (uint)(LightingManager.day * newScale);
				LightingManager._cycle = newScale;
				this.updateLighting();
			}
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x000F7240 File Offset: 0x000F5640
		[SteamCall]
		public void tellLightingOffset(CSteamID steamID, uint newCycle)
		{
			if (base.channel.checkServer(steamID))
			{
				LightingManager._offset = newCycle;
				this.updateLighting();
			}
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x000F725F File Offset: 0x000F565F
		[SteamCall]
		public void tellLightingWind(CSteamID steamID, byte newWind)
		{
			if (base.channel.checkServer(steamID))
			{
				LevelLighting.wind = (float)newWind;
			}
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x000F7279 File Offset: 0x000F5679
		[SteamCall]
		public void tellLightingRain(CSteamID steamID, byte newRain)
		{
			if (base.channel.checkServer(steamID))
			{
				LevelLighting.rainyness = (ELightingRain)newRain;
				if (LightingManager.onRainUpdated != null)
				{
					LightingManager.onRainUpdated(LevelLighting.rainyness);
				}
			}
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x000F72AB File Offset: 0x000F56AB
		[SteamCall]
		public void tellLightingSnow(CSteamID steamID, byte newSnow)
		{
			if (base.channel.checkServer(steamID))
			{
				LevelLighting.snowyness = (ELightingSnow)newSnow;
				if (LightingManager.onSnowUpdated != null)
				{
					LightingManager.onSnowUpdated(LevelLighting.snowyness);
				}
			}
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000F72DD File Offset: 0x000F56DD
		private void onClientConnected()
		{
			if (Level.info.type != ELevelType.SURVIVAL)
			{
				return;
			}
			base.channel.send("askLighting", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x000F7308 File Offset: 0x000F5708
		private void updateLighting()
		{
			LightingManager._time = (Provider.time - LightingManager.offset) % LightingManager.cycle;
			if (Provider.isServer && Time.time - LightingManager.lastWind > LightingManager.windDelay)
			{
				LightingManager.windDelay = (float)UnityEngine.Random.Range(45, 75);
				LightingManager.lastWind = Time.time;
				LevelLighting.wind = (float)UnityEngine.Random.Range(0, 360);
				LightingManager.manager.channel.send("tellLightingWind", ESteamCall.OTHERS, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					MeasurementTool.angleToByte(LevelLighting.wind)
				});
			}
			if (LightingManager.day > LevelLighting.bias)
			{
				if (!LightingManager.isCycled)
				{
					LightingManager.isCycled = true;
					if (LevelLighting.moon < LevelLighting.MOON_CYCLES - 1)
					{
						LevelLighting.moon += 1;
						LightingManager.isFullMoon = (LevelLighting.moon == 2);
					}
					else
					{
						LevelLighting.moon = 0;
						LightingManager.isFullMoon = false;
					}
					if (LightingManager.onDayNightUpdated != null)
					{
						LightingManager.onDayNightUpdated(false);
					}
				}
			}
			else if (LightingManager.isCycled)
			{
				LightingManager.isCycled = false;
				LightingManager.isFullMoon = false;
				if (LightingManager.onDayNightUpdated != null)
				{
					LightingManager.onDayNightUpdated(true);
				}
			}
			if (!Dedicator.isDedicated)
			{
				LevelLighting.time = LightingManager.day;
			}
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x000F7457 File Offset: 0x000F5857
		private void onPrePreLevelLoaded(int level)
		{
			LightingManager.onDayNightUpdated = null;
			LightingManager.onMoonUpdated = null;
			LightingManager.onRainUpdated = null;
			LightingManager.onSnowUpdated = null;
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x000F7474 File Offset: 0x000F5874
		private void onLevelLoaded(int level)
		{
			if (level > Level.SETUP)
			{
				LightingManager.lastRain = Time.realtimeSinceStartup;
				LightingManager.lastSnow = Time.realtimeSinceStartup;
				if (Level.info != null && Level.info.type != ELevelType.SURVIVAL)
				{
					LightingManager._cycle = 3600u;
					LightingManager._offset = 0u;
					if (Level.info.type == ELevelType.HORDE)
					{
						LightingManager._time = (uint)((LevelLighting.bias + (1f - LevelLighting.bias) / 2f) * LightingManager.cycle);
						LightingManager._isFullMoon = true;
					}
					else if (Level.info.type == ELevelType.ARENA)
					{
						LightingManager._time = (uint)(LevelLighting.transition * LightingManager.cycle);
						LightingManager._isFullMoon = false;
					}
					LightingManager.windDelay = (float)UnityEngine.Random.Range(45, 75);
					LevelLighting.wind = (float)UnityEngine.Random.Range(0, 360);
					LevelLighting.rainyness = ELightingRain.NONE;
					LevelLighting.snowyness = ELightingSnow.NONE;
					Level.isLoadingLighting = false;
					if (!Dedicator.isDedicated)
					{
						LevelLighting.time = LightingManager.day;
						LevelLighting.moon = 2;
					}
					return;
				}
				LightingManager._cycle = 3600u;
				LightingManager._time = 0u;
				LightingManager._offset = 0u;
				LightingManager._isFullMoon = false;
				LightingManager.isCycled = false;
				if (LightingManager.onDayNightUpdated != null)
				{
					LightingManager.onDayNightUpdated(true);
				}
				LightingManager.windDelay = (float)UnityEngine.Random.Range(45, 75);
				LevelLighting.wind = (float)UnityEngine.Random.Range(0, 360);
				if (Provider.isServer)
				{
					LightingManager.load();
					this.updateLighting();
					Level.isLoadingLighting = false;
				}
			}
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x000F75F4 File Offset: 0x000F59F4
		private void Update()
		{
			if (!Level.isLoaded || Level.info == null)
			{
				return;
			}
			if (Level.isEditor)
			{
				LevelLighting.updateLighting();
			}
			else if (Level.info.type == ELevelType.SURVIVAL)
			{
				this.updateLighting();
			}
			if (Provider.isServer)
			{
				if (LevelLighting.canRain)
				{
					if (!LightingManager.hasRain)
					{
						LightingManager.rainFrequency = (uint)(UnityEngine.Random.Range(Provider.modeConfigData.Events.Rain_Frequency_Min, Provider.modeConfigData.Events.Rain_Frequency_Max) * LightingManager.cycle * LevelLighting.rainFreq);
						LightingManager.rainDuration = (uint)(UnityEngine.Random.Range(Provider.modeConfigData.Events.Rain_Duration_Min, Provider.modeConfigData.Events.Rain_Duration_Max) * LightingManager.cycle * LevelLighting.rainDur);
						LightingManager._hasRain = true;
						LightingManager.lastRain = Time.realtimeSinceStartup;
					}
					switch (LevelLighting.rainyness)
					{
					case ELightingRain.NONE:
						if (LightingManager.rainFrequency > 0u)
						{
							if (Time.realtimeSinceStartup - LightingManager.lastRain > 1f)
							{
								LightingManager.rainFrequency -= 1u;
								LightingManager.lastRain = Time.realtimeSinceStartup;
							}
						}
						else
						{
							LightingManager.manager.channel.send("tellLightingRain", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								1
							});
							LightingManager.lastRain = Time.realtimeSinceStartup;
						}
						break;
					case ELightingRain.PRE_DRIZZLE:
						if (Time.realtimeSinceStartup - LightingManager.lastRain > 20f)
						{
							LightingManager.manager.channel.send("tellLightingRain", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								2
							});
							LightingManager.lastRain = Time.realtimeSinceStartup;
						}
						break;
					case ELightingRain.DRIZZLE:
						if (LightingManager.rainDuration > 0u)
						{
							if (Time.realtimeSinceStartup - LightingManager.lastRain > 1f)
							{
								LightingManager.rainDuration -= 1u;
								LightingManager.lastRain = Time.realtimeSinceStartup;
							}
						}
						else
						{
							LightingManager.manager.channel.send("tellLightingRain", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								3
							});
							LightingManager.lastRain = Time.realtimeSinceStartup;
						}
						break;
					case ELightingRain.POST_DRIZZLE:
						if (Time.realtimeSinceStartup - LightingManager.lastRain > 20f)
						{
							LightingManager.manager.channel.send("tellLightingRain", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								0
							});
							LightingManager._hasRain = false;
						}
						break;
					}
				}
				if (LevelLighting.canSnow)
				{
					if (!LightingManager.hasSnow)
					{
						LightingManager.snowFrequency = (uint)(UnityEngine.Random.Range(Provider.modeConfigData.Events.Snow_Frequency_Min, Provider.modeConfigData.Events.Snow_Frequency_Max) * LightingManager.cycle * LevelLighting.snowFreq);
						LightingManager.snowDuration = (uint)(UnityEngine.Random.Range(Provider.modeConfigData.Events.Snow_Duration_Min, Provider.modeConfigData.Events.Snow_Duration_Max) * LightingManager.cycle * LevelLighting.snowDur);
						LightingManager._hasSnow = true;
						LightingManager.lastSnow = Time.realtimeSinceStartup;
					}
					switch (LevelLighting.snowyness)
					{
					case ELightingSnow.NONE:
						if (LightingManager.snowFrequency > 0u)
						{
							if (Time.realtimeSinceStartup - LightingManager.lastSnow > 1f)
							{
								LightingManager.snowFrequency -= 1u;
								LightingManager.lastSnow = Time.realtimeSinceStartup;
							}
						}
						else
						{
							LightingManager.manager.channel.send("tellLightingSnow", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								1
							});
							LightingManager.lastSnow = Time.realtimeSinceStartup;
						}
						break;
					case ELightingSnow.PRE_BLIZZARD:
						if (Time.realtimeSinceStartup - LightingManager.lastSnow > 20f)
						{
							LightingManager.manager.channel.send("tellLightingSnow", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								2
							});
							LightingManager.lastSnow = Time.realtimeSinceStartup;
						}
						break;
					case ELightingSnow.BLIZZARD:
						if (LightingManager.snowDuration > 0u)
						{
							if (Time.realtimeSinceStartup - LightingManager.lastSnow > 1f)
							{
								LightingManager.snowDuration -= 1u;
								LightingManager.lastSnow = Time.realtimeSinceStartup;
							}
						}
						else
						{
							LightingManager.manager.channel.send("tellLightingSnow", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								3
							});
							LightingManager.lastSnow = Time.realtimeSinceStartup;
						}
						break;
					case ELightingSnow.POST_BLIZZARD:
						if (Time.realtimeSinceStartup - LightingManager.lastSnow > 20f)
						{
							LightingManager.manager.channel.send("tellLightingSnow", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								0
							});
							LightingManager._hasSnow = false;
						}
						break;
					}
				}
			}
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x000F7A90 File Offset: 0x000F5E90
		private void Start()
		{
			LightingManager.manager = this;
			Level.onPrePreLevelLoaded = (PrePreLevelLoaded)Delegate.Combine(Level.onPrePreLevelLoaded, new PrePreLevelLoaded(this.onPrePreLevelLoaded));
			Level.onLevelLoaded = (LevelLoaded)Delegate.Combine(Level.onLevelLoaded, new LevelLoaded(this.onLevelLoaded));
			Provider.onClientConnected = (Provider.ClientConnected)Delegate.Combine(Provider.onClientConnected, new Provider.ClientConnected(this.onClientConnected));
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000F7B04 File Offset: 0x000F5F04
		public static void load()
		{
			if (LevelSavedata.fileExists("/Lighting.dat"))
			{
				River river = LevelSavedata.openRiver("/Lighting.dat", true);
				byte b = river.readByte();
				if (b > 0)
				{
					LightingManager._cycle = river.readUInt32();
					LightingManager._time = river.readUInt32();
					if (b > 1)
					{
						LightingManager.rainFrequency = river.readUInt32();
						LightingManager.rainDuration = river.readUInt32();
						LightingManager._hasRain = river.readBoolean();
						LevelLighting.rainyness = (ELightingRain)river.readByte();
					}
					else
					{
						LightingManager._hasRain = false;
						LevelLighting.rainyness = ELightingRain.NONE;
					}
					if (b > 2)
					{
						LightingManager.snowFrequency = river.readUInt32();
						LightingManager.snowDuration = river.readUInt32();
						LightingManager._hasSnow = river.readBoolean();
						LevelLighting.snowyness = (ELightingSnow)river.readByte();
					}
					else
					{
						LightingManager._hasSnow = false;
						LevelLighting.snowyness = ELightingSnow.NONE;
					}
					LightingManager._offset = Provider.time - LightingManager.time;
					return;
				}
			}
			LightingManager._time = (uint)(LightingManager.cycle * LevelLighting.transition);
			LightingManager._offset = Provider.time - LightingManager.time;
			LightingManager._hasRain = false;
			LevelLighting.rainyness = ELightingRain.NONE;
			LightingManager._hasSnow = false;
			LevelLighting.snowyness = ELightingSnow.NONE;
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x000F7C24 File Offset: 0x000F6024
		public static void save()
		{
			River river = LevelSavedata.openRiver("/Lighting.dat", false);
			river.writeByte(LightingManager.SAVEDATA_VERSION);
			river.writeUInt32(LightingManager.cycle);
			river.writeUInt32(LightingManager.time);
			river.writeUInt32(LightingManager.rainFrequency);
			river.writeUInt32(LightingManager.rainDuration);
			river.writeBoolean(LightingManager.hasRain);
			river.writeByte((byte)LevelLighting.rainyness);
			river.writeUInt32(LightingManager.snowFrequency);
			river.writeUInt32(LightingManager.snowDuration);
			river.writeBoolean(LightingManager.hasSnow);
			river.writeByte((byte)LevelLighting.snowyness);
		}

		// Token: 0x04001967 RID: 6503
		public static readonly byte SAVEDATA_VERSION = 3;

		// Token: 0x04001968 RID: 6504
		public static DayNightUpdated onDayNightUpdated;

		// Token: 0x04001969 RID: 6505
		public static MoonUpdated onMoonUpdated;

		// Token: 0x0400196A RID: 6506
		public static RainUpdated onRainUpdated;

		// Token: 0x0400196B RID: 6507
		public static SnowUpdated onSnowUpdated;

		// Token: 0x0400196C RID: 6508
		private static LightingManager manager;

		// Token: 0x0400196D RID: 6509
		private static uint _cycle;

		// Token: 0x0400196E RID: 6510
		private static uint _time;

		// Token: 0x0400196F RID: 6511
		private static uint _offset;

		// Token: 0x04001970 RID: 6512
		public static uint rainFrequency;

		// Token: 0x04001971 RID: 6513
		public static uint rainDuration;

		// Token: 0x04001972 RID: 6514
		private static bool _hasRain;

		// Token: 0x04001973 RID: 6515
		private static float lastRain;

		// Token: 0x04001974 RID: 6516
		public static uint snowFrequency;

		// Token: 0x04001975 RID: 6517
		public static uint snowDuration;

		// Token: 0x04001976 RID: 6518
		private static bool _hasSnow;

		// Token: 0x04001977 RID: 6519
		private static float lastSnow;

		// Token: 0x04001978 RID: 6520
		private static bool isCycled;

		// Token: 0x04001979 RID: 6521
		private static bool _isFullMoon;

		// Token: 0x0400197A RID: 6522
		private static float lastWind;

		// Token: 0x0400197B RID: 6523
		private static float windDelay;
	}
}
