using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.Landscapes;
using SDG.Framework.Water;
using UnityEngine;
using UnityEngine.Rendering;

namespace SDG.Unturned
{
	// Token: 0x0200054F RID: 1359
	public class LevelLighting
	{
		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060024EA RID: 9450 RVA: 0x000D3ADD File Offset: 0x000D1EDD
		// (set) Token: 0x060024EB RID: 9451 RVA: 0x000D3AE4 File Offset: 0x000D1EE4
		public static bool enableUnderwaterEffects
		{
			get
			{
				return LevelLighting._enableUnderwaterEffects;
			}
			set
			{
				LevelLighting._enableUnderwaterEffects = value;
				TerminalUtility.printCommandPass("Set enable_underwater_effects to: " + LevelLighting.enableUnderwaterEffects);
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060024EC RID: 9452 RVA: 0x000D3B05 File Offset: 0x000D1F05
		private static float fogHeight
		{
			get
			{
				if (Level.info.configData.Use_Legacy_Fog_Height)
				{
					return -128f;
				}
				return -Landscape.TILE_HEIGHT / 2f - 128f;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x000D3B33 File Offset: 0x000D1F33
		private static float fogSize
		{
			get
			{
				if (Level.info.configData.Use_Legacy_Fog_Height)
				{
					return 128f;
				}
				return Landscape.TILE_HEIGHT;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060024EE RID: 9454 RVA: 0x000D3B54 File Offset: 0x000D1F54
		// (set) Token: 0x060024EF RID: 9455 RVA: 0x000D3B5B File Offset: 0x000D1F5B
		public static float azimuth
		{
			get
			{
				return LevelLighting._azimuth;
			}
			set
			{
				LevelLighting._azimuth = value;
				LevelLighting.updateLighting();
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x060024F0 RID: 9456 RVA: 0x000D3B68 File Offset: 0x000D1F68
		public static float transition
		{
			get
			{
				return LevelLighting._transition;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x000D3B6F File Offset: 0x000D1F6F
		// (set) Token: 0x060024F2 RID: 9458 RVA: 0x000D3B78 File Offset: 0x000D1F78
		public static float bias
		{
			get
			{
				return LevelLighting._bias;
			}
			set
			{
				LevelLighting._bias = value;
				if (LevelLighting.bias < 1f - LevelLighting.bias)
				{
					LevelLighting._transition = LevelLighting.bias / 2f * LevelLighting.fade;
				}
				else
				{
					LevelLighting._transition = (1f - LevelLighting.bias) / 2f * LevelLighting.fade;
				}
				LevelLighting.updateLighting();
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060024F3 RID: 9459 RVA: 0x000D3BDC File Offset: 0x000D1FDC
		// (set) Token: 0x060024F4 RID: 9460 RVA: 0x000D3BE4 File Offset: 0x000D1FE4
		public static float fade
		{
			get
			{
				return LevelLighting._fade;
			}
			set
			{
				LevelLighting._fade = value;
				if (LevelLighting.bias < 1f - LevelLighting.bias)
				{
					LevelLighting._transition = LevelLighting.bias / 2f * LevelLighting.fade;
				}
				else
				{
					LevelLighting._transition = (1f - LevelLighting.bias) / 2f * LevelLighting.fade;
				}
				LevelLighting.updateLighting();
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060024F5 RID: 9461 RVA: 0x000D3C48 File Offset: 0x000D2048
		// (set) Token: 0x060024F6 RID: 9462 RVA: 0x000D3C4F File Offset: 0x000D204F
		public static float time
		{
			get
			{
				return LevelLighting._time;
			}
			set
			{
				LevelLighting._time = value;
				LevelLighting.updateLighting();
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060024F8 RID: 9464 RVA: 0x000D3C64 File Offset: 0x000D2064
		// (set) Token: 0x060024F7 RID: 9463 RVA: 0x000D3C5C File Offset: 0x000D205C
		public static float wind
		{
			get
			{
				return LevelLighting._wind;
			}
			set
			{
				LevelLighting._wind = value;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060024F9 RID: 9465 RVA: 0x000D3C6B File Offset: 0x000D206B
		// (set) Token: 0x060024FA RID: 9466 RVA: 0x000D3C72 File Offset: 0x000D2072
		public static float christmasyness { get; private set; }

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060024FB RID: 9467 RVA: 0x000D3C7A File Offset: 0x000D207A
		// (set) Token: 0x060024FC RID: 9468 RVA: 0x000D3C81 File Offset: 0x000D2081
		public static float blizzardyness { get; private set; }

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060024FD RID: 9469 RVA: 0x000D3C89 File Offset: 0x000D2089
		// (set) Token: 0x060024FE RID: 9470 RVA: 0x000D3C90 File Offset: 0x000D2090
		public static float mistyness { get; private set; }

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060024FF RID: 9471 RVA: 0x000D3C98 File Offset: 0x000D2098
		// (set) Token: 0x06002500 RID: 9472 RVA: 0x000D3C9F File Offset: 0x000D209F
		public static float drizzlyness { get; private set; }

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002501 RID: 9473 RVA: 0x000D3CA7 File Offset: 0x000D20A7
		public static byte[] hash
		{
			get
			{
				return LevelLighting._hash;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x000D3CAE File Offset: 0x000D20AE
		public static LightingInfo[] times
		{
			get
			{
				return LevelLighting._times;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002503 RID: 9475 RVA: 0x000D3CB5 File Offset: 0x000D20B5
		// (set) Token: 0x06002504 RID: 9476 RVA: 0x000D3CBC File Offset: 0x000D20BC
		public static float seaLevel
		{
			get
			{
				return LevelLighting._seaLevel;
			}
			set
			{
				LevelLighting._seaLevel = value;
				LevelLighting.updateSea();
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002505 RID: 9477 RVA: 0x000D3CC9 File Offset: 0x000D20C9
		// (set) Token: 0x06002506 RID: 9478 RVA: 0x000D3CD0 File Offset: 0x000D20D0
		public static float snowLevel
		{
			get
			{
				return LevelLighting._snowLevel;
			}
			set
			{
				LevelLighting._snowLevel = value;
			}
		}

		// Token: 0x1400007E RID: 126
		// (add) Token: 0x06002507 RID: 9479 RVA: 0x000D3CD8 File Offset: 0x000D20D8
		// (remove) Token: 0x06002508 RID: 9480 RVA: 0x000D3D0C File Offset: 0x000D210C
		public static event LevelLighting.IsSeaChangedHandler isSeaChanged;

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002509 RID: 9481 RVA: 0x000D3D40 File Offset: 0x000D2140
		// (set) Token: 0x0600250A RID: 9482 RVA: 0x000D3D47 File Offset: 0x000D2147
		public static bool isSea
		{
			get
			{
				return LevelLighting._isSea;
			}
			protected set
			{
				if (LevelLighting.isSea == value)
				{
					return;
				}
				LevelLighting._isSea = value;
				if (LevelLighting.isSeaChanged != null)
				{
					LevelLighting.isSeaChanged(LevelLighting.isSea);
				}
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x000D3D74 File Offset: 0x000D2174
		public static AudioSource effectAudio
		{
			get
			{
				return LevelLighting._effectAudio;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600250C RID: 9484 RVA: 0x000D3D7B File Offset: 0x000D217B
		public static AudioSource dayAudio
		{
			get
			{
				return LevelLighting._dayAudio;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600250D RID: 9485 RVA: 0x000D3D82 File Offset: 0x000D2182
		public static AudioSource nightAudio
		{
			get
			{
				return LevelLighting._nightAudio;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600250E RID: 9486 RVA: 0x000D3D89 File Offset: 0x000D2189
		public static AudioSource waterAudio
		{
			get
			{
				return LevelLighting._waterAudio;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x0600250F RID: 9487 RVA: 0x000D3D90 File Offset: 0x000D2190
		public static AudioSource windAudio
		{
			get
			{
				return LevelLighting._windAudio;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002510 RID: 9488 RVA: 0x000D3D97 File Offset: 0x000D2197
		public static AudioSource belowAudio
		{
			get
			{
				return LevelLighting._belowAudio;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002511 RID: 9489 RVA: 0x000D3D9E File Offset: 0x000D219E
		public static AudioSource rainAudio
		{
			get
			{
				return LevelLighting._rainAudio;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06002512 RID: 9490 RVA: 0x000D3DA5 File Offset: 0x000D21A5
		// (set) Token: 0x06002513 RID: 9491 RVA: 0x000D3DAC File Offset: 0x000D21AC
		public static bool isSkyboxReflectionEnabled
		{
			get
			{
				return LevelLighting._isSkyboxReflectionEnabled;
			}
			set
			{
				LevelLighting._isSkyboxReflectionEnabled = value;
				LevelLighting.updateSkyboxReflections();
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002514 RID: 9492 RVA: 0x000D3DB9 File Offset: 0x000D21B9
		public static Transform bubbles
		{
			get
			{
				return LevelLighting._bubbles;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002515 RID: 9493 RVA: 0x000D3DC0 File Offset: 0x000D21C0
		public static Transform snow
		{
			get
			{
				return LevelLighting._snow;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002516 RID: 9494 RVA: 0x000D3DC7 File Offset: 0x000D21C7
		public static Transform rain
		{
			get
			{
				return LevelLighting._rain;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002517 RID: 9495 RVA: 0x000D3DCE File Offset: 0x000D21CE
		public static WindZone windZone
		{
			get
			{
				return LevelLighting._windZone;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002518 RID: 9496 RVA: 0x000D3DD5 File Offset: 0x000D21D5
		public static Transform clouds
		{
			get
			{
				return LevelLighting._clouds;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x000D3DDC File Offset: 0x000D21DC
		public static Material sea
		{
			get
			{
				return LevelLighting._sea;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600251A RID: 9498 RVA: 0x000D3DE3 File Offset: 0x000D21E3
		public static PlanarReflection reflection
		{
			get
			{
				return LevelLighting._reflection;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600251B RID: 9499 RVA: 0x000D3DEA File Offset: 0x000D21EA
		// (set) Token: 0x0600251C RID: 9500 RVA: 0x000D3DF4 File Offset: 0x000D21F4
		public static byte moon
		{
			get
			{
				return LevelLighting._moon;
			}
			set
			{
				LevelLighting._moon = value;
				if (!Dedicator.isDedicated && (int)LevelLighting.moon < LevelLighting.moons.Length && LevelLighting.moonFlare != null)
				{
					LevelLighting.moonFlare.GetComponent<Renderer>().sharedMaterial = LevelLighting.moons[(int)LevelLighting.moon];
				}
			}
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x000D3E4C File Offset: 0x000D224C
		public static void setEnabled(bool isEnabled)
		{
			LevelLighting.sun.GetComponent<Light>().enabled = isEnabled;
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x000D3E60 File Offset: 0x000D2260
		public static bool isPositionSnowy(Vector3 position)
		{
			return Level.info != null && Level.info.configData.Use_Legacy_Snow_Height && LevelLighting.snowLevel > 0.01f && position.y > LevelLighting.snowLevel * Level.TERRAIN;
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x000D3EB4 File Offset: 0x000D22B4
		public static bool isPositionUnderwater(Vector3 position)
		{
			return Level.info != null && Level.info.configData.Use_Legacy_Water && LevelLighting.seaLevel < 0.99f && position.y < LevelLighting.seaLevel * Level.TERRAIN;
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x000D3F07 File Offset: 0x000D2307
		public static float getWaterSurfaceElevation()
		{
			return LevelLighting.seaLevel * Level.TERRAIN;
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x000D3F14 File Offset: 0x000D2314
		public static Vector4 getSeaVector(string name)
		{
			return LevelLighting.sea.GetVector(name);
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x000D3F24 File Offset: 0x000D2324
		public static void setSeaVector(string name, Vector4 vector)
		{
			LevelLighting.sea.SetVector(name, vector);
			foreach (WaterVolume waterVolume in WaterSystem.volumes)
			{
				if (!(waterVolume.sea == null))
				{
					waterVolume.sea.SetVector(name, vector);
				}
			}
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x000D3FA8 File Offset: 0x000D23A8
		public static Color getSeaColor(string name)
		{
			return LevelLighting.sea.GetColor(name);
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x000D3FB8 File Offset: 0x000D23B8
		public static void setSeaColor(string name, Color color)
		{
			LevelLighting.sea.SetColor(name, color);
			foreach (WaterVolume waterVolume in WaterSystem.volumes)
			{
				if (!(waterVolume.sea == null))
				{
					waterVolume.sea.SetColor(name, color);
				}
			}
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000D403C File Offset: 0x000D243C
		public static float getSeaFloat(string name)
		{
			return LevelLighting.sea.GetFloat(name);
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x000D404C File Offset: 0x000D244C
		public static void setSeaFloat(string name, float value)
		{
			LevelLighting.sea.SetFloat(name, value);
			foreach (WaterVolume waterVolume in WaterSystem.volumes)
			{
				if (!(waterVolume.sea == null))
				{
					waterVolume.sea.SetFloat(name, value);
				}
			}
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x000D40D0 File Offset: 0x000D24D0
		public static void updateLighting()
		{
			if (LevelLighting.sun == null || LevelLighting.sea == null)
			{
				return;
			}
			float t = 0f;
			LevelLighting.setSeaVector("_WorldLightDir", LevelLighting.sunFlare.forward);
			LightingInfo lightingInfo;
			LightingInfo lightingInfo2;
			if (LevelLighting.time < LevelLighting.bias)
			{
				LevelLighting.sun.rotation = Quaternion.Euler(LevelLighting.time / LevelLighting.bias * 180f, LevelLighting.azimuth, 0f);
				if (LevelLighting.time < LevelLighting.transition)
				{
					LevelLighting.dayVolume = Mathf.Lerp(0.5f, 1f, LevelLighting.time / LevelLighting.transition);
					LevelLighting.nightVolume = Mathf.Lerp(0.5f, 0f, LevelLighting.time / LevelLighting.transition);
					lightingInfo = LevelLighting.times[0];
					lightingInfo2 = LevelLighting.times[1];
					t = LevelLighting.time / LevelLighting.transition;
					LevelLighting.setSeaColor("_Foam", Color.Lerp(LevelLighting.FOAM_DAWN, LevelLighting.FOAM_MIDDAY, LevelLighting.time / LevelLighting.transition));
					LevelLighting.setSeaFloat("_Shininess", Mathf.Lerp(LevelLighting.SPECULAR_DAWN, LevelLighting.SPECULAR_MIDDAY, LevelLighting.time / LevelLighting.transition));
					RenderSettings.reflectionIntensity = Mathf.Lerp(LevelLighting.REFLECTION_DAWN, LevelLighting.REFLECTION_MIDDAY, LevelLighting.time / LevelLighting.transition);
				}
				else if (LevelLighting.time < LevelLighting.bias - LevelLighting.transition)
				{
					LevelLighting.dayVolume = 1f;
					LevelLighting.nightVolume = 0f;
					lightingInfo = null;
					lightingInfo2 = LevelLighting.times[1];
					LevelLighting.setSeaColor("_Foam", LevelLighting.FOAM_MIDDAY);
					LevelLighting.setSeaFloat("_Shininess", LevelLighting.SPECULAR_MIDDAY);
					RenderSettings.reflectionIntensity = LevelLighting.REFLECTION_MIDDAY;
				}
				else
				{
					LevelLighting.dayVolume = Mathf.Lerp(1f, 0.5f, (LevelLighting.time - LevelLighting.bias + LevelLighting.transition) / LevelLighting.transition);
					LevelLighting.nightVolume = Mathf.Lerp(0f, 0.5f, (LevelLighting.time - LevelLighting.bias + LevelLighting.transition) / LevelLighting.transition);
					lightingInfo = LevelLighting.times[1];
					lightingInfo2 = LevelLighting.times[2];
					t = (LevelLighting.time - LevelLighting.bias + LevelLighting.transition) / LevelLighting.transition;
					LevelLighting.setSeaColor("_Foam", Color.Lerp(LevelLighting.FOAM_MIDDAY, LevelLighting.FOAM_DUSK, (LevelLighting.time - LevelLighting.bias + LevelLighting.transition) / LevelLighting.transition));
					LevelLighting.setSeaFloat("_Shininess", Mathf.Lerp(LevelLighting.SPECULAR_MIDDAY, LevelLighting.SPECULAR_DUSK, (LevelLighting.time - LevelLighting.bias + LevelLighting.transition) / LevelLighting.transition));
					RenderSettings.reflectionIntensity = Mathf.Lerp(LevelLighting.REFLECTION_MIDDAY, LevelLighting.REFLECTION_DUSK, (LevelLighting.time - LevelLighting.bias + LevelLighting.transition) / LevelLighting.transition);
				}
				LevelLighting.updateStars(1f);
				LevelLighting.auroraBorealisTargetIntensity = 0f;
			}
			else
			{
				LevelLighting.sun.rotation = Quaternion.Euler(180f + (LevelLighting.time - LevelLighting.bias) / (1f - LevelLighting.bias) * 180f, LevelLighting.azimuth, 0f);
				if (LevelLighting.time < LevelLighting.bias + LevelLighting.transition)
				{
					LevelLighting.dayVolume = Mathf.Lerp(0.5f, 0f, (LevelLighting.time - LevelLighting.bias) / LevelLighting.transition);
					LevelLighting.nightVolume = Mathf.Lerp(0.5f, 1f, (LevelLighting.time - LevelLighting.bias) / LevelLighting.transition);
					lightingInfo = LevelLighting.times[2];
					lightingInfo2 = LevelLighting.times[3];
					t = (LevelLighting.time - LevelLighting.bias) / LevelLighting.transition;
					LevelLighting.setSeaColor("_Foam", Color.Lerp(LevelLighting.FOAM_DUSK, LevelLighting.FOAM_MIDNIGHT, (LevelLighting.time - LevelLighting.bias) / LevelLighting.transition));
					LevelLighting.setSeaFloat("_Shininess", Mathf.Lerp(LevelLighting.SPECULAR_DUSK, LevelLighting.SPECULAR_MIDNIGHT, (LevelLighting.time - LevelLighting.bias) / LevelLighting.transition));
					RenderSettings.reflectionIntensity = Mathf.Lerp(LevelLighting.REFLECTION_DUSK, LevelLighting.REFLECTION_MIDNIGHT, (LevelLighting.time - LevelLighting.bias) / LevelLighting.transition);
					LevelLighting.updateStars(Mathf.Lerp(1f, 0.05f, t));
					LevelLighting.auroraBorealisTargetIntensity = 0f;
				}
				else if (LevelLighting.time < 1f - LevelLighting.transition)
				{
					LevelLighting.dayVolume = 0f;
					LevelLighting.nightVolume = 1f;
					lightingInfo = null;
					lightingInfo2 = LevelLighting.times[3];
					LevelLighting.setSeaColor("_Foam", LevelLighting.FOAM_MIDNIGHT);
					LevelLighting.setSeaFloat("_Shininess", LevelLighting.SPECULAR_MIDNIGHT);
					RenderSettings.reflectionIntensity = LevelLighting.REFLECTION_MIDNIGHT;
					LevelLighting.updateStars(0.05f);
					LevelLighting.auroraBorealisTargetIntensity = 1f;
				}
				else
				{
					LevelLighting.dayVolume = Mathf.Lerp(0f, 0.5f, (LevelLighting.time - 1f + LevelLighting.transition) / LevelLighting.transition);
					LevelLighting.nightVolume = Mathf.Lerp(1f, 0.5f, (LevelLighting.time - 1f + LevelLighting.transition) / LevelLighting.transition);
					lightingInfo = LevelLighting.times[3];
					lightingInfo2 = LevelLighting.times[0];
					t = (LevelLighting.time - 1f + LevelLighting.transition) / LevelLighting.transition;
					LevelLighting.setSeaColor("_Foam", Color.Lerp(LevelLighting.FOAM_MIDNIGHT, LevelLighting.FOAM_DAWN, (LevelLighting.time - 1f + LevelLighting.transition) / LevelLighting.transition));
					LevelLighting.setSeaFloat("_Shininess", Mathf.Lerp(LevelLighting.SPECULAR_MIDNIGHT, LevelLighting.SPECULAR_DAWN, (LevelLighting.time - 1f + LevelLighting.transition) / LevelLighting.transition));
					RenderSettings.reflectionIntensity = Mathf.Lerp(LevelLighting.REFLECTION_MIDNIGHT, LevelLighting.REFLECTION_DAWN, (LevelLighting.time - 1f + LevelLighting.transition) / LevelLighting.transition);
					LevelLighting.updateStars(Mathf.Lerp(0.05f, 1f, t));
					LevelLighting.auroraBorealisTargetIntensity = 0f;
				}
			}
			if (lightingInfo == null)
			{
				LevelLighting.sunFlare.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.white, lightingInfo2.colors[0], 0.5f));
				LevelLighting.sun.GetComponent<Light>().color = lightingInfo2.colors[0];
				LevelLighting.sun.GetComponent<Light>().intensity = lightingInfo2.singles[0];
				LevelLighting.sun.GetComponent<Light>().shadowStrength = lightingInfo2.singles[3] * (1f - LevelLighting.drizzlyness * 0.8f);
				LevelLighting.setSeaColor("_BaseColor", lightingInfo2.colors[1]);
				LevelLighting.setSeaColor("_ReflectionColor", lightingInfo2.colors[1]);
				RenderSettings.ambientSkyColor = lightingInfo2.colors[6];
				RenderSettings.ambientEquatorColor = lightingInfo2.colors[7];
				RenderSettings.ambientGroundColor = lightingInfo2.colors[8];
				LevelLighting.skyboxSky = lightingInfo2.colors[3];
				LevelLighting.skyboxEquator = lightingInfo2.colors[4];
				LevelLighting.skyboxGround = lightingInfo2.colors[5];
				LevelLighting.skyboxClouds = lightingInfo2.colors[9];
				LevelLighting.rainColor = lightingInfo2.colors[11];
				LevelLighting.raysColor = lightingInfo2.colors[10];
				LevelLighting.raysIntensity = lightingInfo2.singles[4] * 4f;
				if (MainCamera.instance != null)
				{
					GlobalFog component = MainCamera.instance.GetComponent<GlobalFog>();
					if (component != null)
					{
						if (LevelLighting.seaLevel < 0.99f)
						{
							component.height = LevelLighting.seaLevel * Level.TERRAIN + LevelLighting.fogHeight + lightingInfo2.singles[1] * LevelLighting.fogSize;
						}
						else
						{
							component.height = LevelLighting.fogHeight + lightingInfo2.singles[1] * LevelLighting.fogSize;
						}
						component.globalFogColor = lightingInfo2.colors[2];
					}
				}
				LevelLighting.clouds.GetComponent<ParticleSystem>().emission.rateOverTime = lightingInfo2.singles[2];
			}
			else
			{
				LevelLighting.sunFlare.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.white, Color.Lerp(lightingInfo.colors[0], lightingInfo2.colors[0], t), 0.5f));
				LevelLighting.sun.GetComponent<Light>().color = Color.Lerp(lightingInfo.colors[0], lightingInfo2.colors[0], t);
				LevelLighting.sun.GetComponent<Light>().intensity = Mathf.Lerp(lightingInfo.singles[0], lightingInfo2.singles[0], t);
				LevelLighting.sun.GetComponent<Light>().shadowStrength = Mathf.Lerp(lightingInfo.singles[3], lightingInfo2.singles[3], t) * (1f - LevelLighting.drizzlyness * 0.8f);
				LevelLighting.setSeaColor("_BaseColor", Color.Lerp(lightingInfo.colors[1], lightingInfo2.colors[1], t));
				LevelLighting.setSeaColor("_ReflectionColor", Color.Lerp(lightingInfo.colors[1], lightingInfo2.colors[1], t));
				RenderSettings.ambientSkyColor = Color.Lerp(lightingInfo.colors[6], lightingInfo2.colors[6], t);
				RenderSettings.ambientEquatorColor = Color.Lerp(lightingInfo.colors[7], lightingInfo2.colors[7], t);
				RenderSettings.ambientGroundColor = Color.Lerp(lightingInfo.colors[8], lightingInfo2.colors[8], t);
				LevelLighting.skyboxSky = Color.Lerp(lightingInfo.colors[3], lightingInfo2.colors[3], t);
				LevelLighting.skyboxEquator = Color.Lerp(lightingInfo.colors[4], lightingInfo2.colors[4], t);
				LevelLighting.skyboxGround = Color.Lerp(lightingInfo.colors[5], lightingInfo2.colors[5], t);
				LevelLighting.skyboxClouds = Color.Lerp(lightingInfo.colors[9], lightingInfo2.colors[9], t);
				LevelLighting.rainColor = Color.Lerp(lightingInfo.colors[11], lightingInfo2.colors[11], t);
				LevelLighting.raysColor = Color.Lerp(lightingInfo.colors[10], lightingInfo2.colors[10], t);
				LevelLighting.raysIntensity = Mathf.Lerp(lightingInfo.singles[4], lightingInfo2.singles[4], t) * 4f;
				if (MainCamera.instance != null)
				{
					GlobalFog component2 = MainCamera.instance.GetComponent<GlobalFog>();
					if (component2 != null)
					{
						if (LevelLighting.seaLevel < 0.99f)
						{
							component2.height = LevelLighting.seaLevel * Level.TERRAIN + LevelLighting.fogHeight + Mathf.Lerp(lightingInfo.singles[1], lightingInfo2.singles[1], t) * LevelLighting.fogSize;
						}
						else
						{
							component2.height = LevelLighting.fogHeight + Mathf.Lerp(lightingInfo.singles[1], lightingInfo2.singles[1], t) * LevelLighting.fogSize;
						}
						component2.globalFogColor = Color.Lerp(lightingInfo.colors[2], lightingInfo2.colors[2], t);
					}
				}
				LevelLighting.clouds.GetComponent<ParticleSystem>().emission.rateOverTime = Mathf.Lerp(lightingInfo.singles[2], lightingInfo2.singles[2], t);
			}
			if (LevelLighting.localBlendingFog && MainCamera.instance != null)
			{
				GlobalFog component3 = MainCamera.instance.GetComponent<GlobalFog>();
				if (component3 != null)
				{
					component3.height = Mathf.Lerp(component3.height, LevelLighting.localFogHeight, LevelLighting.localFogBlend);
					component3.globalFogColor = Color.Lerp(component3.globalFogColor, LevelLighting.localFogColor, LevelLighting.localFogBlend);
				}
			}
			if (LevelLighting.localBlendingLight)
			{
				LevelLighting.setSeaColor("_Foam", Color.Lerp(LevelLighting.getSeaColor("_Foam"), Color.black, LevelLighting.localLightingBlend * LevelLighting.PITCH_DARK_WATER_BLEND));
				LevelLighting.setSeaFloat("_Shininess", Mathf.Lerp(LevelLighting.getSeaFloat("_Shininess"), 0f, LevelLighting.localLightingBlend * LevelLighting.PITCH_DARK_WATER_BLEND));
				LevelLighting.setSeaColor("_BaseColor", Color.Lerp(LevelLighting.getSeaColor("_BaseColor"), Color.black, LevelLighting.localLightingBlend * LevelLighting.PITCH_DARK_WATER_BLEND));
				LevelLighting.setSeaColor("_ReflectionColor", Color.Lerp(LevelLighting.getSeaColor("_ReflectionColor"), Color.black, LevelLighting.localLightingBlend * LevelLighting.PITCH_DARK_WATER_BLEND));
				LevelLighting.sun.GetComponent<Light>().color = Color.Lerp(LevelLighting.sun.GetComponent<Light>().color, Color.black, LevelLighting.localLightingBlend);
				LevelLighting.sun.GetComponent<Light>().intensity = Mathf.Lerp(LevelLighting.sun.GetComponent<Light>().intensity, 0f, LevelLighting.localLightingBlend);
				LevelLighting.sun.GetComponent<Light>().shadowStrength = Mathf.Lerp(LevelLighting.sun.GetComponent<Light>().shadowStrength, 0f, LevelLighting.localLightingBlend);
				RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, Color.black, LevelLighting.localLightingBlend);
				RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, Color.black, LevelLighting.localLightingBlend);
				RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, Color.black, LevelLighting.localLightingBlend);
				RenderSettings.ambientMode = AmbientMode.Trilight;
				LevelLighting.skyboxSky = Color.Lerp(LevelLighting.skyboxSky, Color.black, LevelLighting.localLightingBlend);
				LevelLighting.skyboxEquator = Color.Lerp(LevelLighting.skyboxEquator, Color.black, LevelLighting.localLightingBlend);
				LevelLighting.skyboxGround = Color.Lerp(LevelLighting.skyboxGround, Color.black, LevelLighting.localLightingBlend);
				LevelLighting.skyboxClouds = Color.Lerp(LevelLighting.skyboxClouds, Color.black, LevelLighting.localLightingBlend);
				LevelLighting.rainColor = Color.Lerp(LevelLighting.rainColor, Color.black, LevelLighting.localLightingBlend);
			}
			LevelLighting.setSeaColor("_SpecularColor", LevelLighting.sun.GetComponent<Light>().color);
			if (LevelLighting.vision == ELightingVision.MILITARY)
			{
				LevelLighting.setSeaColor("_BaseColor", LevelLighting.NIGHTVISION_MILITARY);
				LevelLighting.setSeaColor("_ReflectionColor", LevelLighting.NIGHTVISION_MILITARY);
				RenderSettings.ambientSkyColor = LevelLighting.NIGHTVISION_MILITARY;
				RenderSettings.ambientEquatorColor = LevelLighting.NIGHTVISION_MILITARY;
				RenderSettings.ambientGroundColor = LevelLighting.NIGHTVISION_MILITARY;
				RenderSettings.ambientMode = AmbientMode.Trilight;
				LevelLighting.skyboxSky = LevelLighting.NIGHTVISION_MILITARY;
				LevelLighting.skyboxEquator = LevelLighting.NIGHTVISION_MILITARY;
				LevelLighting.skyboxGround = LevelLighting.NIGHTVISION_MILITARY;
				LevelLighting.skyboxClouds = LevelLighting.NIGHTVISION_MILITARY;
			}
			else if (LevelLighting.vision == ELightingVision.CIVILIAN)
			{
				LevelLighting.setSeaColor("_BaseColor", LevelLighting.NIGHTVISION_CIVILIAN);
				LevelLighting.setSeaColor("_ReflectionColor", LevelLighting.NIGHTVISION_CIVILIAN);
				RenderSettings.ambientSkyColor = LevelLighting.NIGHTVISION_CIVILIAN;
				RenderSettings.ambientEquatorColor = LevelLighting.NIGHTVISION_CIVILIAN;
				RenderSettings.ambientGroundColor = LevelLighting.NIGHTVISION_CIVILIAN;
				RenderSettings.ambientMode = AmbientMode.Trilight;
				LevelLighting.skyboxSky = LevelLighting.NIGHTVISION_CIVILIAN;
				LevelLighting.skyboxEquator = LevelLighting.NIGHTVISION_CIVILIAN;
				LevelLighting.skyboxGround = LevelLighting.NIGHTVISION_CIVILIAN;
				LevelLighting.skyboxClouds = LevelLighting.NIGHTVISION_CIVILIAN;
			}
			if ((LevelLighting.vision == ELightingVision.MILITARY || LevelLighting.vision == ELightingVision.CIVILIAN) && LevelLighting.localBlendingLight)
			{
				RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, Color.black, LevelLighting.localLightingBlend / 2f);
				RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientSkyColor, Color.black, LevelLighting.localLightingBlend / 2f);
				RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientSkyColor, Color.black, LevelLighting.localLightingBlend / 2f);
				LevelLighting.skyboxSky = Color.Lerp(LevelLighting.skyboxSky, Color.black, LevelLighting.localLightingBlend / 2f);
				LevelLighting.skyboxEquator = Color.Lerp(LevelLighting.skyboxEquator, Color.black, LevelLighting.localLightingBlend / 2f);
				LevelLighting.skyboxGround = Color.Lerp(LevelLighting.skyboxGround, Color.black, LevelLighting.localLightingBlend / 2f);
				LevelLighting.skyboxClouds = Color.Lerp(LevelLighting.skyboxClouds, Color.black, LevelLighting.localLightingBlend / 2f);
			}
			if (Time.time - LevelLighting.lastSkyboxUpdate > 3f)
			{
				LevelLighting.lastSkyboxUpdate = Time.time;
				LevelLighting.skyboxUpdated = false;
			}
			if (MainCamera.instance != null)
			{
				if (LevelLighting.vision == ELightingVision.MILITARY)
				{
					GlobalFog component4 = MainCamera.instance.GetComponent<GlobalFog>();
					if (component4 != null)
					{
						component4.height = 2048f;
						component4.globalFogColor = LevelLighting.NIGHTVISION_MILITARY;
					}
				}
				else if (LevelLighting.vision == ELightingVision.CIVILIAN)
				{
					GlobalFog component5 = MainCamera.instance.GetComponent<GlobalFog>();
					if (component5 != null)
					{
						component5.height = 2048f;
						component5.globalFogColor = LevelLighting.NIGHTVISION_CIVILIAN;
					}
				}
				SunShafts component6 = MainCamera.instance.GetComponent<SunShafts>();
				if (component6 != null)
				{
					component6.sunTransform = LevelLighting.sunFlare;
					component6.sunColor = LevelLighting.raysColor;
					component6.sunShaftIntensity = LevelLighting.raysIntensity * LevelLighting.raysMultiplier;
				}
				LevelLighting.reflection.clearColor = MainCamera.instance.backgroundColor;
				if (Player.player != null)
				{
					Player.player.look.scopeCamera.backgroundColor = MainCamera.instance.backgroundColor;
					GlobalFog component7 = MainCamera.instance.GetComponent<GlobalFog>();
					if (component7 != null)
					{
						GlobalFog component8 = Player.player.look.scopeCamera.GetComponent<GlobalFog>();
						if (component8 != null)
						{
							component8.height = component7.height;
							component8.globalFogColor = component7.globalFogColor;
						}
					}
				}
			}
			LevelLighting.stars.rotation = Quaternion.Euler(Time.realtimeSinceStartup * 0.01f, Time.realtimeSinceStartup * 0.01f, Time.realtimeSinceStartup * 0.01f);
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x000D5340 File Offset: 0x000D3740
		public static void load(ushort size)
		{
			LevelLighting.vision = ELightingVision.NONE;
			LevelLighting.isSea = false;
			LevelLighting.isSnow = false;
			LevelLighting.christmasyness = 0f;
			LevelLighting.blizzardyness = 0f;
			LevelLighting.mistyness = 0f;
			LevelLighting.drizzlyness = 0f;
			LevelLighting.raysMultiplier = 1f;
			LevelLighting.localEffectNode = null;
			LevelLighting.localPlayingEffect = false;
			LevelLighting.localBlendingLight = false;
			LevelLighting.localLightingBlend = 1f;
			LevelLighting.localBlendingFog = false;
			LevelLighting.localFogBlend = 1f;
			LevelLighting.rainBlend = 1f;
			LevelLighting.snowBlend = 1f;
			LevelLighting.auroraBorealisCurrentIntensity = 0f;
			LevelLighting.auroraBorealisTargetIntensity = 0f;
			LevelLighting.currentAudioVolume = 0f;
			LevelLighting.targetAudioVolume = 0f;
			LevelLighting.nextAudioVolumeChangeTime = -1f;
			if (!Dedicator.isDedicated)
			{
				LevelLighting.skybox = (Material)UnityEngine.Object.Instantiate(Resources.Load("Level/Skybox"));
				RenderSettings.skybox = LevelLighting.skybox;
				LevelLighting.lighting = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Level/Lighting"))).transform;
				LevelLighting.lighting.name = "Lighting";
				LevelLighting.lighting.position = Vector3.zero;
				LevelLighting.lighting.rotation = Quaternion.identity;
				LevelLighting.lighting.parent = Level.level;
				LevelLighting.sun = LevelLighting.lighting.FindChild("Sun");
				LevelLighting.sunFlare = LevelLighting.sun.FindChild("Flare_Sun");
				LevelLighting.moonFlare = LevelLighting.sun.FindChild("Flare_Moon");
				LevelLighting.stars = LevelLighting.lighting.FindChild("Stars");
				LevelLighting._bubbles = LevelLighting.lighting.FindChild("Bubbles");
				LevelLighting._snow = LevelLighting.lighting.FindChild("Snow");
				LevelLighting._rain = LevelLighting.lighting.FindChild("Rain");
				LevelLighting._clouds = LevelLighting.lighting.FindChild("Clouds");
				LevelLighting._windZone = LevelLighting.lighting.FindChild("WindZone").GetComponent<WindZone>();
				LevelLighting.reflectionCamera = LevelLighting.lighting.FindChild("Reflection").GetComponent<Camera>();
				LevelLighting.reflectionMap = new Cubemap(16, TextureFormat.ARGB32, false);
				LevelLighting.reflectionMap.name = "Skybox_Reflection";
				LevelLighting.reflectionMapVision = new Cubemap(16, TextureFormat.ARGB32, false);
				LevelLighting.reflectionMapVision.name = "Skybox_Reflection_Vision";
				RenderSettings.defaultReflectionMode = DefaultReflectionMode.Custom;
				LevelLighting.reflectionIndex = 0;
				LevelLighting.reflectionIndexVision = 0;
				LevelLighting.isReflectionBuilding = false;
				LevelLighting.isReflectionBuildingVision = false;
				LevelLighting.mist = LevelLighting.clouds.GetComponent<Renderer>().material;
				LevelLighting.puddles = LevelLighting.lighting.GetComponent<Rain>();
				LevelLighting.auroraBorealisTransform = LevelLighting.lighting.FindChild("Aurora_Borealis");
				LevelLighting.auroraBorealisTransform.gameObject.SetActive(Level.info.configData.Is_Aurora_Borealis_Visible);
				LevelLighting.auroraBorealisMaterial = LevelLighting.auroraBorealisTransform.GetComponent<MeshRenderer>().material;
				LevelLighting.moons = new Material[(int)LevelLighting.MOON_CYCLES];
				for (int i = 0; i < LevelLighting.moons.Length; i++)
				{
					LevelLighting.moons[i] = (Material)Resources.Load("Flares/Moon_" + i);
				}
				LevelLighting._effectAudio = LevelLighting.lighting.FindChild("Effect").GetComponent<AudioSource>();
				LevelLighting._dayAudio = LevelLighting.lighting.FindChild("Day").GetComponent<AudioSource>();
				LevelLighting._nightAudio = LevelLighting.lighting.FindChild("Night").GetComponent<AudioSource>();
				LevelLighting._waterAudio = LevelLighting.lighting.FindChild("Water").GetComponent<AudioSource>();
				LevelLighting._windAudio = LevelLighting.lighting.FindChild("Wind").GetComponent<AudioSource>();
				LevelLighting._belowAudio = LevelLighting.lighting.FindChild("Below").GetComponent<AudioSource>();
				LevelLighting._rainAudio = LevelLighting.lighting.FindChild("Rain").GetComponent<AudioSource>();
				if (ReadWrite.fileExists(Level.info.path + "/Environment/Ambience.unity3d", false, false))
				{
					Bundle bundle = Bundles.getBundle(Level.info.path + "/Environment/Ambience.unity3d", false);
					LevelLighting.dayAudio.clip = (AudioClip)bundle.load("Day");
					LevelLighting.dayAudio.Play();
					LevelLighting.nightAudio.clip = (AudioClip)bundle.load("Night");
					LevelLighting.nightAudio.Play();
					LevelLighting.waterAudio.clip = (AudioClip)bundle.load("Water");
					LevelLighting.waterAudio.Play();
					LevelLighting.windAudio.clip = (AudioClip)bundle.load("Wind");
					LevelLighting.windAudio.Play();
					LevelLighting.belowAudio.clip = (AudioClip)bundle.load("Below");
					LevelLighting.belowAudio.Play();
					LevelLighting.rainAudio.clip = (AudioClip)bundle.load("Rain");
					LevelLighting.rainAudio.Play();
					bundle.unload();
				}
				LevelLighting.water = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Level/Water"))).transform;
				LevelLighting.water.name = "Water";
				LevelLighting.water.parent = Level.level;
				LevelLighting.water.transform.rotation = Quaternion.identity;
				LevelLighting.water.transform.localScale = new Vector3((float)(size * 2) / 100f, 1f, (float)(size * 2) / 100f);
				LevelLighting._sea = LevelLighting.water.GetChild(0).GetChild(0).GetComponent<Renderer>().material;
				for (int j = 0; j < LevelLighting.water.childCount; j++)
				{
					Transform child = LevelLighting.water.GetChild(j);
					for (int k = 0; k < LevelLighting.water.childCount; k++)
					{
						Transform child2 = child.GetChild(k);
						child2.GetComponent<Renderer>().material = LevelLighting.sea;
					}
				}
				LevelLighting._reflection = LevelLighting.water.GetComponent<PlanarReflection>();
			}
			if (ReadWrite.fileExists(Level.info.path + "/Environment/Lighting.dat", false, false))
			{
				Block block = ReadWrite.readBlock(Level.info.path + "/Environment/Lighting.dat", false, false, 0);
				byte b = block.readByte();
				LevelLighting._azimuth = block.readSingle();
				LevelLighting._bias = block.readSingle();
				LevelLighting._fade = block.readSingle();
				LevelLighting._time = block.readSingle();
				LevelLighting.moon = block.readByte();
				if (b >= 5)
				{
					LevelLighting._seaLevel = block.readSingle();
					LevelLighting._snowLevel = block.readSingle();
					if (b > 6)
					{
						LevelLighting.canRain = block.readBoolean();
					}
					else
					{
						LevelLighting.canRain = false;
					}
					if (b > 10)
					{
						LevelLighting.canSnow = block.readBoolean();
					}
					else
					{
						LevelLighting.canSnow = false;
					}
					if (b < 8)
					{
						LevelLighting.rainFreq = 1f;
						LevelLighting.rainDur = 1f;
					}
					else
					{
						LevelLighting.rainFreq = block.readSingle();
						LevelLighting.rainDur = block.readSingle();
					}
					if (b < 11)
					{
						LevelLighting.snowFreq = 1f;
						LevelLighting.snowDur = 1f;
					}
					else
					{
						LevelLighting.snowFreq = block.readSingle();
						LevelLighting.snowDur = block.readSingle();
					}
					LevelLighting._times = new LightingInfo[4];
					for (int l = 0; l < LevelLighting.times.Length; l++)
					{
						Color[] array = new Color[12];
						float[] array2 = new float[5];
						if (b > 9)
						{
							for (int m = 0; m < array.Length; m++)
							{
								array[m] = block.readColor();
							}
							for (int n = 0; n < array2.Length; n++)
							{
								array2[n] = block.readSingle();
							}
						}
						else if (b > 8)
						{
							for (int num = 0; num < array.Length - 1; num++)
							{
								array[num] = block.readColor();
							}
							array[11] = array[3];
							for (int num2 = 0; num2 < array2.Length; num2++)
							{
								array2[num2] = block.readSingle();
							}
						}
						else
						{
							if (b >= 6)
							{
								for (int num3 = 0; num3 < array.Length - 1; num3++)
								{
									array[num3] = block.readColor();
								}
							}
							else
							{
								for (int num4 = 0; num4 < array.Length - 2; num4++)
								{
									array[num4] = block.readColor();
								}
								array[9] = array[2];
							}
							for (int num5 = 0; num5 < array2.Length - 1; num5++)
							{
								array2[num5] = block.readSingle();
							}
							array[10] = array[0];
							array2[4] = 0.25f;
						}
						LightingInfo lightingInfo = new LightingInfo(array, array2);
						LevelLighting.times[l] = lightingInfo;
					}
				}
				else
				{
					LevelLighting._times = new LightingInfo[4];
					for (int num6 = 0; num6 < LevelLighting.times.Length; num6++)
					{
						Color[] newColors = new Color[12];
						float[] newSingles = new float[5];
						LightingInfo lightingInfo2 = new LightingInfo(newColors, newSingles);
						LevelLighting.times[num6] = lightingInfo2;
					}
					LevelLighting.times[0].colors[3] = block.readColor();
					LevelLighting.times[1].colors[3] = block.readColor();
					LevelLighting.times[2].colors[3] = block.readColor();
					LevelLighting.times[3].colors[3] = block.readColor();
					LevelLighting.times[0].colors[4] = LevelLighting.times[0].colors[3];
					LevelLighting.times[1].colors[4] = LevelLighting.times[1].colors[3];
					LevelLighting.times[2].colors[4] = LevelLighting.times[2].colors[3];
					LevelLighting.times[3].colors[4] = LevelLighting.times[3].colors[3];
					LevelLighting.times[0].colors[5] = LevelLighting.times[0].colors[3];
					LevelLighting.times[1].colors[5] = LevelLighting.times[1].colors[3];
					LevelLighting.times[2].colors[5] = LevelLighting.times[2].colors[3];
					LevelLighting.times[3].colors[5] = LevelLighting.times[3].colors[3];
					LevelLighting.times[0].colors[6] = block.readColor();
					LevelLighting.times[1].colors[6] = block.readColor();
					LevelLighting.times[2].colors[6] = block.readColor();
					LevelLighting.times[3].colors[6] = block.readColor();
					LevelLighting.times[0].colors[7] = LevelLighting.times[0].colors[6];
					LevelLighting.times[1].colors[7] = LevelLighting.times[1].colors[6];
					LevelLighting.times[2].colors[7] = LevelLighting.times[2].colors[6];
					LevelLighting.times[3].colors[7] = LevelLighting.times[3].colors[6];
					LevelLighting.times[0].colors[8] = LevelLighting.times[0].colors[6];
					LevelLighting.times[1].colors[8] = LevelLighting.times[1].colors[6];
					LevelLighting.times[2].colors[8] = LevelLighting.times[2].colors[6];
					LevelLighting.times[3].colors[8] = LevelLighting.times[3].colors[6];
					LevelLighting.times[0].colors[2] = block.readColor();
					LevelLighting.times[1].colors[2] = block.readColor();
					LevelLighting.times[2].colors[2] = block.readColor();
					LevelLighting.times[3].colors[2] = block.readColor();
					LevelLighting.times[0].colors[0] = block.readColor();
					LevelLighting.times[1].colors[0] = block.readColor();
					LevelLighting.times[2].colors[0] = block.readColor();
					LevelLighting.times[3].colors[0] = block.readColor();
					LevelLighting.times[0].singles[0] = block.readSingle();
					LevelLighting.times[1].singles[0] = block.readSingle();
					LevelLighting.times[2].singles[0] = block.readSingle();
					LevelLighting.times[3].singles[0] = block.readSingle();
					LevelLighting.times[0].singles[1] = block.readSingle();
					LevelLighting.times[1].singles[1] = block.readSingle();
					LevelLighting.times[2].singles[1] = block.readSingle();
					LevelLighting.times[3].singles[1] = block.readSingle();
					LevelLighting.times[0].singles[2] = block.readSingle();
					LevelLighting.times[1].singles[2] = block.readSingle();
					LevelLighting.times[2].singles[2] = block.readSingle();
					LevelLighting.times[3].singles[2] = block.readSingle();
					LevelLighting.times[0].singles[3] = block.readSingle();
					LevelLighting.times[1].singles[3] = block.readSingle();
					LevelLighting.times[2].singles[3] = block.readSingle();
					LevelLighting.times[3].singles[3] = block.readSingle();
					if (b > 2)
					{
						LevelLighting._seaLevel = block.readSingle();
					}
					else
					{
						LevelLighting._seaLevel = block.readSingle() / 2f;
					}
					if (b > 1)
					{
						LevelLighting._snowLevel = block.readSingle();
					}
					else
					{
						LevelLighting._snowLevel = 0f;
					}
					LevelLighting.canRain = false;
					LevelLighting.canSnow = false;
					LevelLighting.times[0].colors[1] = block.readColor();
					LevelLighting.times[1].colors[1] = block.readColor();
					LevelLighting.times[2].colors[1] = block.readColor();
					LevelLighting.times[3].colors[1] = block.readColor();
				}
				LevelLighting._hash = block.getHash();
			}
			else
			{
				LevelLighting._azimuth = 0.2f;
				LevelLighting._bias = 0.5f;
				LevelLighting._fade = 1f;
				LevelLighting._time = LevelLighting.bias / 2f;
				LevelLighting.moon = 0;
				LevelLighting._seaLevel = 1f;
				LevelLighting._snowLevel = 0f;
				LevelLighting.canRain = true;
				LevelLighting.canSnow = false;
				LevelLighting.rainFreq = 1f;
				LevelLighting.rainDur = 1f;
				LevelLighting.snowFreq = 1f;
				LevelLighting.snowDur = 1f;
				LevelLighting._times = new LightingInfo[4];
				for (int num7 = 0; num7 < LevelLighting.times.Length; num7++)
				{
					Color[] newColors2 = new Color[12];
					float[] newSingles2 = new float[5];
					LightingInfo lightingInfo3 = new LightingInfo(newColors2, newSingles2);
					LevelLighting.times[num7] = lightingInfo3;
				}
				LevelLighting._hash = new byte[20];
			}
			if (LevelLighting.bias < 1f - LevelLighting.bias)
			{
				LevelLighting._transition = LevelLighting.bias / 2f * LevelLighting.fade;
			}
			else
			{
				LevelLighting._transition = (1f - LevelLighting.bias) / 2f * LevelLighting.fade;
			}
			LevelLighting.times[0].colors[1].a = 0.25f;
			LevelLighting.times[1].colors[1].a = 0.5f;
			LevelLighting.times[2].colors[1].a = 0.75f;
			LevelLighting.times[3].colors[1].a = 0.9f;
			LevelLighting.init = false;
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000D656C File Offset: 0x000D496C
		public static void save()
		{
			Block block = new Block();
			block.writeByte(LevelLighting.SAVEDATA_VERSION);
			block.writeSingle(LevelLighting.azimuth);
			block.writeSingle(LevelLighting.bias);
			block.writeSingle(LevelLighting.fade);
			block.writeSingle(LevelLighting.time);
			block.writeByte(LevelLighting.moon);
			block.writeSingle(LevelLighting.seaLevel);
			block.writeSingle(LevelLighting.snowLevel);
			block.writeBoolean(LevelLighting.canRain);
			block.writeBoolean(LevelLighting.canSnow);
			block.writeSingle(LevelLighting.rainFreq);
			block.writeSingle(LevelLighting.rainDur);
			block.writeSingle(LevelLighting.snowFreq);
			block.writeSingle(LevelLighting.snowDur);
			for (int i = 0; i < LevelLighting.times.Length; i++)
			{
				LightingInfo lightingInfo = LevelLighting.times[i];
				for (int j = 0; j < lightingInfo.colors.Length; j++)
				{
					block.writeColor(lightingInfo.colors[j]);
				}
				for (int k = 0; k < lightingInfo.singles.Length; k++)
				{
					block.writeSingle(lightingInfo.singles[k]);
				}
			}
			ReadWrite.writeBlock(Level.info.path + "/Environment/Lighting.dat", false, false, block);
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000D66B1 File Offset: 0x000D4AB1
		public static void updateClouds()
		{
			LevelLighting.clouds.GetComponent<ParticleSystem>().Stop();
			LevelLighting.clouds.GetComponent<ParticleSystem>().Play();
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000D66D4 File Offset: 0x000D4AD4
		private static void updateSea()
		{
			if (Level.info.configData.Use_Legacy_Water)
			{
				if (LevelLighting.seaLevel < 0.99f)
				{
					LevelLighting.water.position = new Vector3(0f, LevelLighting.seaLevel * Level.TERRAIN, 0f);
					LevelLighting.bubbles.gameObject.SetActive(true);
					LevelLighting.water.gameObject.SetActive(true);
				}
				else
				{
					LevelLighting.bubbles.gameObject.SetActive(false);
					LevelLighting.water.gameObject.SetActive(false);
				}
			}
			else
			{
				LevelLighting.bubbles.gameObject.SetActive(true);
				LevelLighting.water.gameObject.SetActive(false);
			}
			LevelLighting.bubbles.GetComponent<ParticleSystem>().Play();
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000D67A1 File Offset: 0x000D4BA1
		public static void updateLocal()
		{
			LevelLighting.updateLocal(LevelLighting.localPoint, LevelLighting.localWindOverride, LevelLighting.localEffectNode);
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000D67B8 File Offset: 0x000D4BB8
		public static void updateLocal(Vector3 point, float windOverride, IAmbianceNode effectNode)
		{
			LevelLighting.localPoint = point;
			LevelLighting.localWindOverride = windOverride;
			if (effectNode != LevelLighting.localEffectNode)
			{
				if (effectNode != null)
				{
					if (LevelLighting.localEffectNode == null || effectNode.id != LevelLighting.localEffectNode.id)
					{
						EffectAsset effectAsset = (EffectAsset)Assets.find(EAssetType.EFFECT, effectNode.id);
						if (effectAsset != null && effectAsset.effect != null)
						{
							AudioSource component = effectAsset.effect.GetComponent<AudioSource>();
							if (component != null)
							{
								LevelLighting.effectAudio.clip = component.clip;
								LevelLighting.effectAudio.Play();
								LevelLighting.localPlayingEffect = true;
							}
							else
							{
								LevelLighting.localPlayingEffect = false;
							}
						}
						else
						{
							LevelLighting.localPlayingEffect = false;
						}
					}
				}
				else
				{
					LevelLighting.localPlayingEffect = false;
				}
			}
			LevelLighting.localEffectNode = effectNode;
			if (LevelLighting.localEffectNode != null && LevelLighting.localEffectNode.noLighting && !Level.isEditor)
			{
				LevelLighting.localLightingBlend = Mathf.Lerp(LevelLighting.localLightingBlend, 1f, 0.25f * Time.deltaTime);
				LevelLighting.localBlendingLight = true;
			}
			else
			{
				LevelLighting.localLightingBlend = Mathf.Lerp(LevelLighting.localLightingBlend, 0f, 0.25f * Time.deltaTime);
				if (LevelLighting.localLightingBlend < 0.01f)
				{
					LevelLighting.localLightingBlend = 0f;
					LevelLighting.localBlendingLight = false;
				}
			}
			AmbianceVolume ambianceVolume = LevelLighting.localEffectNode as AmbianceVolume;
			if (ambianceVolume != null && ambianceVolume.overrideFog)
			{
				LevelLighting.localFogBlend = Mathf.Lerp(LevelLighting.localFogBlend, 1f, 0.05f * Time.deltaTime);
				LevelLighting.localBlendingFog = true;
				LevelLighting.localFogColor = ambianceVolume.fogColor;
				LevelLighting.localFogHeight = ambianceVolume.fogHeight;
			}
			else
			{
				LevelLighting.localFogBlend = Mathf.Lerp(LevelLighting.localFogBlend, 0f, 0.125f * Time.deltaTime);
				if (LevelLighting.localFogBlend < 0.01f)
				{
					LevelLighting.localFogBlend = 0f;
					LevelLighting.localBlendingFog = false;
				}
			}
			if (Level.info != null && Level.info.configData.Use_Rain_Volumes)
			{
				if (ambianceVolume != null && ambianceVolume.canRain)
				{
					LevelLighting.rainBlend = Mathf.Lerp(LevelLighting.rainBlend, 1f, 0.1f * Time.deltaTime);
				}
				else
				{
					LevelLighting.rainBlend = Mathf.Lerp(LevelLighting.rainBlend, 0f, 0.1f * Time.deltaTime);
					if (LevelLighting.rainBlend < 0.01f)
					{
						LevelLighting.rainBlend = 0f;
					}
				}
			}
			else
			{
				LevelLighting.rainBlend = 1f;
			}
			float b = 1f - (LevelLighting.snowLevel * Level.TERRAIN - point.y) / 32f;
			if (Level.info != null && Level.info.configData.Use_Snow_Volumes)
			{
				if (ambianceVolume != null && ambianceVolume.canSnow)
				{
					LevelLighting.snowBlend = Mathf.Lerp(LevelLighting.snowBlend, 1f, 0.1f * Time.deltaTime);
				}
				else if (LevelLighting.snowLevel > 0.01f && Level.info.configData.Use_Legacy_Snow_Height)
				{
					LevelLighting.snowBlend = Mathf.Lerp(LevelLighting.snowBlend, b, 0.1f * Time.deltaTime);
				}
				else
				{
					LevelLighting.snowBlend = Mathf.Lerp(LevelLighting.snowBlend, 0f, 0.1f * Time.deltaTime);
					if (LevelLighting.snowBlend < 0.01f)
					{
						LevelLighting.snowBlend = 0f;
					}
				}
			}
			else if (LevelLighting.snowLevel > 0.01f && Level.info != null && Level.info.configData.Use_Legacy_Snow_Height)
			{
				LevelLighting.snowBlend = Mathf.Lerp(LevelLighting.snowBlend, b, 0.1f * Time.deltaTime);
			}
			else
			{
				LevelLighting.snowBlend = 0f;
			}
			if (!LevelLighting.init)
			{
				LevelLighting.init = true;
				LevelLighting.updateSea();
				LevelLighting.updateLighting();
				LevelLighting.clouds.GetComponent<ParticleSystem>().Play();
				LevelLighting.bubbles.GetComponent<ParticleSystem>().Play();
				LevelLighting.snow.GetComponent<ParticleSystem>().Play();
				LevelLighting.rain.GetComponent<ParticleSystem>().Play();
			}
			LevelLighting.lighting.position = point;
			switch (LevelLighting.snowyness)
			{
			case ELightingSnow.NONE:
				LevelLighting.christmasyness = Mathf.Lerp(LevelLighting.christmasyness, 0f, 0.5f * Time.deltaTime);
				LevelLighting.blizzardyness = Mathf.Lerp(LevelLighting.blizzardyness, 0f, 0.5f * Time.deltaTime);
				break;
			case ELightingSnow.PRE_BLIZZARD:
				LevelLighting.christmasyness = Mathf.Lerp(LevelLighting.christmasyness, LevelLighting.snowBlend * 0.5f, 0.2f * Time.deltaTime);
				LevelLighting.blizzardyness = Mathf.Lerp(LevelLighting.blizzardyness, 0f, 0.5f * Time.deltaTime);
				break;
			case ELightingSnow.BLIZZARD:
				LevelLighting.christmasyness = Mathf.Lerp(LevelLighting.christmasyness, LevelLighting.snowBlend * 0.5f, 0.5f * Time.deltaTime);
				LevelLighting.blizzardyness = Mathf.Lerp(LevelLighting.blizzardyness, LevelLighting.snowBlend, 0.2f * Time.deltaTime);
				break;
			case ELightingSnow.POST_BLIZZARD:
				LevelLighting.christmasyness = Mathf.Lerp(LevelLighting.christmasyness, 0f, 0.2f * Time.deltaTime);
				LevelLighting.blizzardyness = Mathf.Lerp(LevelLighting.blizzardyness, 0f, 0.2f * Time.deltaTime);
				break;
			}
			switch (LevelLighting.rainyness)
			{
			case ELightingRain.NONE:
				LevelLighting.mistyness = Mathf.Lerp(LevelLighting.mistyness, 0f, 0.5f * Time.deltaTime);
				LevelLighting.drizzlyness = Mathf.Lerp(LevelLighting.drizzlyness, 0f, 0.5f * Time.deltaTime);
				break;
			case ELightingRain.PRE_DRIZZLE:
				LevelLighting.mistyness = Mathf.Lerp(LevelLighting.mistyness, LevelLighting.rainBlend * (1f - LevelLighting.blizzardyness) * 0.5f, 0.2f * Time.deltaTime);
				LevelLighting.drizzlyness = Mathf.Lerp(LevelLighting.drizzlyness, 0f, 0.5f * Time.deltaTime);
				break;
			case ELightingRain.DRIZZLE:
				LevelLighting.mistyness = Mathf.Lerp(LevelLighting.mistyness, LevelLighting.rainBlend * (1f - LevelLighting.blizzardyness) * 0.5f, 0.5f * Time.deltaTime);
				LevelLighting.drizzlyness = Mathf.Lerp(LevelLighting.drizzlyness, LevelLighting.rainBlend * (1f - LevelLighting.blizzardyness), 0.2f * Time.deltaTime);
				break;
			case ELightingRain.POST_DRIZZLE:
				LevelLighting.mistyness = Mathf.Lerp(LevelLighting.mistyness, 0f, 0.2f * Time.deltaTime);
				LevelLighting.drizzlyness = Mathf.Lerp(LevelLighting.drizzlyness, 0f, 0.2f * Time.deltaTime);
				break;
			}
			LevelLighting.skybox.SetColor("_SkyColor", LevelLighting.skyboxSky);
			LevelLighting.skybox.SetColor("_EquatorColor", LevelLighting.skyboxEquator);
			LevelLighting.skybox.SetColor("_GroundColor", LevelLighting.skyboxGround);
			Color color = Color.Lerp(new Color(0.8f, 0.8f, 0.8f), Color.black, LevelLighting.mistyness);
			color = Color.Lerp(color, Color.white, LevelLighting.christmasyness);
			Color color2 = Color.Lerp(LevelLighting.skyboxClouds, Color.black, LevelLighting.mistyness);
			color2 = Color.Lerp(color2, Color.white, LevelLighting.christmasyness);
			LevelLighting.mist.SetColor("_Color", color);
			LevelLighting.mist.SetColor("_RimColor", color2);
			if (MainCamera.instance != null)
			{
				MainCamera.instance.backgroundColor = LevelLighting.skyboxSky;
			}
			float num = WaterUtility.getWaterSurfaceElevation(point);
			if (!LevelLighting.enableUnderwaterEffects)
			{
				num = -1024f;
			}
			if (LevelLighting.enableUnderwaterEffects && WaterUtility.isPointUnderwater(point))
			{
				LevelLighting.waterAudio.volume = 0f;
				LevelLighting.belowAudio.volume = 1f;
				RenderSettings.fogColor = LevelLighting.sea.GetColor("_BaseColor");
				RenderSettings.fogDensity = 0.075f;
				if (MainCamera.instance != null)
				{
					MainCamera.instance.backgroundColor = RenderSettings.fogColor;
				}
				if (!LevelLighting.isSea)
				{
					RenderSettings.skybox = null;
					if (MainCamera.instance != null)
					{
						LevelLighting.areFXAllowed = false;
						SunShafts component2 = MainCamera.instance.GetComponent<SunShafts>();
						if (component2 != null)
						{
							component2.enabled = false;
						}
						LevelLighting.raysMultiplier = 0f;
						GlobalFog component3 = MainCamera.instance.GetComponent<GlobalFog>();
						if (component3 != null)
						{
							component3.enabled = false;
						}
						if (Player.player != null)
						{
							GlobalFog component4 = Player.player.look.scopeCamera.GetComponent<GlobalFog>();
							if (component4 != null)
							{
								component4.enabled = false;
							}
						}
					}
				}
				LevelLighting.isSea = true;
			}
			else
			{
				if (point.y < num + 8f && (LevelLighting.localEffectNode == null || !LevelLighting.localEffectNode.noWater))
				{
					LevelLighting.waterAudio.volume = Mathf.Lerp(0f, 0.25f, 1f - (point.y - num) / 8f);
					LevelLighting.belowAudio.volume = 0f;
				}
				else
				{
					LevelLighting.waterAudio.volume = 0f;
					LevelLighting.belowAudio.volume = 0f;
				}
				if (LevelLighting.isSea)
				{
					RenderSettings.skybox = LevelLighting.skybox;
					RenderSettings.fogDensity = 0f;
					if (MainCamera.instance != null)
					{
						LevelLighting.areFXAllowed = true;
						SunShafts component5 = MainCamera.instance.GetComponent<SunShafts>();
						if (component5 != null)
						{
							component5.enabled = (GraphicsSettings.sunShaftsQuality != EGraphicQuality.OFF);
						}
						LevelLighting.raysMultiplier = 1f;
						GlobalFog component6 = MainCamera.instance.GetComponent<GlobalFog>();
						if (component6 != null)
						{
							component6.enabled = GraphicsSettings.fog;
						}
						if (Player.player != null)
						{
							GlobalFog component7 = Player.player.look.scopeCamera.GetComponent<GlobalFog>();
							if (component7 != null)
							{
								component7.enabled = GraphicsSettings.fog;
							}
						}
					}
				}
				LevelLighting.isSea = false;
			}
			Color color3 = LevelLighting.sunFlare.GetComponent<Renderer>().material.GetColor("_Color");
			color3.a = 1f - Mathf.Max(LevelLighting.blizzardyness, LevelLighting.drizzlyness);
			LevelLighting.sunFlare.GetComponent<Renderer>().material.SetColor("_Color", color3);
			Color color4 = LevelLighting.moonFlare.GetComponent<Renderer>().material.GetColor("_Color");
			color4.a = color3.a;
			LevelLighting.moonFlare.GetComponent<Renderer>().material.SetColor("_Color", color4);
			if (!LevelLighting.isSea)
			{
				RenderSettings.fogColor = LevelLighting.skyboxSky;
				RenderSettings.fogDensity = Mathf.Pow(Mathf.Max(LevelLighting.blizzardyness, LevelLighting.drizzlyness), 3f) * 0.0015f;
			}
			if (MainCamera.instance != null && RenderSettings.fogDensity < 0.0299f)
			{
				LevelLighting.raysMultiplier = 1f - Mathf.Max(LevelLighting.blizzardyness, LevelLighting.drizzlyness);
				GlobalFog component8 = MainCamera.instance.GetComponent<GlobalFog>();
				if (component8 != null)
				{
					component8.globalDensity = 0.005f * (1f - Mathf.Max(LevelLighting.blizzardyness, LevelLighting.drizzlyness));
				}
				if (Player.player != null)
				{
					GlobalFog component9 = Player.player.look.scopeCamera.GetComponent<GlobalFog>();
					if (component9 != null)
					{
						component9.globalDensity = 0.005f * (1f - Mathf.Max(LevelLighting.blizzardyness, LevelLighting.drizzlyness));
					}
				}
			}
			LevelLighting.auroraBorealisCurrentIntensity = Mathf.Clamp01(Mathf.Lerp(LevelLighting.auroraBorealisCurrentIntensity, Mathf.Min(1f - LevelLighting.blizzardyness, LevelLighting.auroraBorealisTargetIntensity), 0.1f * Time.deltaTime));
			LevelLighting.updateAuroraBorealis(LevelLighting.auroraBorealisCurrentIntensity);
			if (LevelLighting.blizzardyness > 0.01f)
			{
				LevelLighting.windAudio.volume = Mathf.Lerp(windOverride, 1f, LevelLighting.blizzardyness);
				LevelLighting.snow.GetComponent<ParticleSystem>().emission.rateOverTime = Mathf.Pow(LevelLighting.blizzardyness, 2f) * 1024f;
				LevelLighting.snow.GetComponent<ParticleSystem>().main.startColor = LevelLighting.rainColor;
				LevelLighting.skybox.SetColor("_SkyColor", LevelLighting.skyboxSky);
				LevelLighting.skybox.SetColor("_EquatorColor", Color.Lerp(LevelLighting.skyboxEquator, LevelLighting.skyboxSky, LevelLighting.blizzardyness));
				LevelLighting.skybox.SetColor("_GroundColor", Color.Lerp(LevelLighting.skyboxGround, LevelLighting.skyboxSky, LevelLighting.blizzardyness));
				if (MainCamera.instance != null && RenderSettings.fogDensity < 0.0299f && LevelLighting.blizzardyness > 0.99f)
				{
					if (LevelLighting.areFXAllowed)
					{
						SunShafts component10 = MainCamera.instance.GetComponent<SunShafts>();
						if (component10 != null)
						{
							component10.enabled = false;
						}
					}
					LevelLighting.areFXAllowed = false;
				}
				LevelLighting.isSnow = true;
			}
			else
			{
				LevelLighting.windAudio.volume = windOverride;
				LevelLighting.snow.GetComponent<ParticleSystem>().emission.rateOverTime = 0f;
				if (LevelLighting.isSnow && MainCamera.instance != null)
				{
					LevelLighting.areFXAllowed = true;
					SunShafts component11 = MainCamera.instance.GetComponent<SunShafts>();
					if (component11 != null)
					{
						component11.enabled = (GraphicsSettings.sunShaftsQuality != EGraphicQuality.OFF);
					}
				}
				LevelLighting.isSnow = false;
			}
			Shader.SetGlobalColor("_AlphaParticleLightingColor", LevelLighting.rainColor);
			if (LevelLighting.drizzlyness > 0.01f)
			{
				LevelLighting.rain.GetComponent<ParticleSystem>().emission.rateOverTime = Mathf.Pow(LevelLighting.drizzlyness, 2f) * 2048f;
				LevelLighting.skybox.SetColor("_SkyColor", LevelLighting.skyboxSky);
				LevelLighting.skybox.SetColor("_EquatorColor", Color.Lerp(LevelLighting.skyboxEquator, LevelLighting.skyboxSky, LevelLighting.drizzlyness));
				LevelLighting.skybox.SetColor("_GroundColor", Color.Lerp(LevelLighting.skyboxGround, LevelLighting.skyboxSky, LevelLighting.drizzlyness));
			}
			else
			{
				LevelLighting.rain.GetComponent<ParticleSystem>().emission.rateOverTime = 0f;
			}
			if (LevelLighting.puddles != null)
			{
				if (LevelLighting.rainyness == ELightingRain.DRIZZLE)
				{
					LevelLighting.puddles.Water_Level = Mathf.Lerp(LevelLighting.puddles.Water_Level, LevelLighting.drizzlyness * 0.75f, 0.2f * Time.deltaTime);
				}
				else
				{
					LevelLighting.puddles.Water_Level = Mathf.Lerp(LevelLighting.puddles.Water_Level, 0f, 0.025f * Time.deltaTime);
				}
				LevelLighting.puddles.Intensity = LevelLighting.drizzlyness * 2f;
			}
			if (Time.time > LevelLighting.nextAudioVolumeChangeTime)
			{
				LevelLighting.nextAudioVolumeChangeTime = Time.time + (float)UnityEngine.Random.Range(15, 60);
				LevelLighting.targetAudioVolume = UnityEngine.Random.Range(LevelLighting.AUDIO_MIN, LevelLighting.AUDIO_MAX);
			}
			LevelLighting.currentAudioVolume = Mathf.Lerp(LevelLighting.currentAudioVolume, LevelLighting.targetAudioVolume, 0.1f * Time.deltaTime);
			LevelLighting.effectAudio.volume = Mathf.Lerp(LevelLighting.effectAudio.volume, (float)((!LevelLighting.localPlayingEffect) ? 0 : 1), (!Level.isEditor) ? (0.5f * Time.deltaTime) : 1f);
			LevelLighting.rainAudio.volume = Mathf.Lerp(LevelLighting.rainAudio.volume, LevelLighting.drizzlyness * (1f - LevelLighting.effectAudio.volume), 0.5f * Time.deltaTime);
			LevelLighting.dayAudio.volume = Mathf.Lerp(LevelLighting.dayAudio.volume, LevelLighting.dayVolume * LevelLighting.currentAudioVolume * (1f - LevelLighting.waterAudio.volume * 4f) * (1f - LevelLighting.belowAudio.volume) * (1f - LevelLighting.windAudio.volume) * (1f - LevelLighting.rainAudio.volume) * (1f - LevelLighting.effectAudio.volume), 0.5f * Time.deltaTime);
			LevelLighting.nightAudio.volume = Mathf.Lerp(LevelLighting.nightAudio.volume, LevelLighting.nightVolume * LevelLighting.currentAudioVolume * (1f - LevelLighting.waterAudio.volume * 4f) * (1f - LevelLighting.belowAudio.volume) * (1f - LevelLighting.windAudio.volume) * (1f - LevelLighting.rainAudio.volume) * (1f - LevelLighting.effectAudio.volume), 0.5f * Time.deltaTime);
			LevelLighting.snow.rotation = Quaternion.Slerp(LevelLighting.snow.rotation, Quaternion.Euler(45f, LevelLighting.wind, 0f), 0.5f * Time.deltaTime);
			LevelLighting.snow.position = point + LevelLighting.snow.forward * -32f;
			LevelLighting.rain.rotation = Quaternion.Slerp(LevelLighting.rain.rotation, Quaternion.Euler(75f, LevelLighting.wind, 0f), 0.5f * Time.deltaTime);
			LevelLighting.rain.position = point + LevelLighting.rain.forward * -32f;
			LevelLighting.windZone.transform.rotation = Quaternion.Slerp(LevelLighting.windZone.transform.rotation, Quaternion.Euler(0f, LevelLighting.wind, 0f), 0.5f * Time.deltaTime);
			LevelLighting.windZone.windMain = Mathf.Lerp(LevelLighting.windZone.windMain, (!LevelLighting.isSnow) ? 0.15f : 0.8f, 0.5f * Time.deltaTime);
			point.y = Mathf.Min(point.y - 16f, num - 32f);
			LevelLighting.bubbles.position = point;
			if (!LevelLighting.skyboxUpdated)
			{
				LevelLighting.skyboxUpdated = true;
				if (LevelLighting.vision != ELightingVision.CIVILIAN && LevelLighting.vision != ELightingVision.MILITARY && !LevelLighting.localBlendingLight)
				{
					if (Provider.preferenceData != null && Provider.preferenceData.Graphics.Use_Skybox_Ambience)
					{
						RenderSettings.ambientMode = AmbientMode.Skybox;
						DynamicGI.UpdateEnvironment();
					}
					else
					{
						RenderSettings.ambientMode = AmbientMode.Trilight;
					}
				}
				LevelLighting.isReflectionBuilding = true;
				LevelLighting.isReflectionBuildingVision = true;
			}
			LevelLighting.updateSkyboxReflections();
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000D7AD8 File Offset: 0x000D5ED8
		private static void renderSkyboxReflection(Cubemap target, ref int index, ref bool isBuilding)
		{
			if (!isBuilding)
			{
				return;
			}
			if (target == null || LevelLighting.reflectionCamera == null)
			{
				return;
			}
			int faceMask = 1 << index;
			index++;
			if (index > 5)
			{
				index = 0;
				isBuilding = false;
			}
			LevelLighting.reflectionCamera.RenderToCubemap(target, faceMask);
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000D7B34 File Offset: 0x000D5F34
		public static void updateSkyboxReflections()
		{
			if (LevelLighting.isSkyboxReflectionEnabled)
			{
				if (LevelLighting.vision == ELightingVision.NONE)
				{
					LevelLighting.renderSkyboxReflection(LevelLighting.reflectionMap, ref LevelLighting.reflectionIndex, ref LevelLighting.isReflectionBuilding);
					RenderSettings.customReflection = LevelLighting.reflectionMap;
				}
				else
				{
					LevelLighting.renderSkyboxReflection(LevelLighting.reflectionMapVision, ref LevelLighting.reflectionIndexVision, ref LevelLighting.isReflectionBuildingVision);
					RenderSettings.customReflection = LevelLighting.reflectionMapVision;
				}
			}
			else
			{
				RenderSettings.customReflection = null;
			}
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000D7BA4 File Offset: 0x000D5FA4
		private static void updateStars(float cutoff)
		{
			if (Level.info == null || !Level.info.configData.Has_Atmosphere)
			{
				cutoff = 0.05f;
			}
			LevelLighting.stars.GetComponent<Renderer>().material.SetFloat("_Cutoff", cutoff);
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000D7BF0 File Offset: 0x000D5FF0
		private static void updateAuroraBorealis(float intensity)
		{
			LevelLighting.auroraBorealisMaterial.SetFloat("_Intensity", intensity);
		}

		// Token: 0x040016E1 RID: 5857
		private static bool _enableUnderwaterEffects = true;

		// Token: 0x040016E2 RID: 5858
		public static readonly byte SAVEDATA_VERSION = 11;

		// Token: 0x040016E3 RID: 5859
		public static readonly byte MOON_CYCLES = 5;

		// Token: 0x040016E4 RID: 5860
		public static readonly float CLOUDS = 2f;

		// Token: 0x040016E5 RID: 5861
		public static readonly float AUDIO_MIN = 0.075f;

		// Token: 0x040016E6 RID: 5862
		public static readonly float AUDIO_MAX = 0.15f;

		// Token: 0x040016E7 RID: 5863
		private static readonly Color FOAM_DAWN = new Color(0.125f, 0f, 0f, 0f);

		// Token: 0x040016E8 RID: 5864
		private static readonly Color FOAM_MIDDAY = new Color(0.25f, 0f, 0f, 0f);

		// Token: 0x040016E9 RID: 5865
		private static readonly Color FOAM_DUSK = new Color(0.05f, 0f, 0f, 0f);

		// Token: 0x040016EA RID: 5866
		private static readonly Color FOAM_MIDNIGHT = new Color(0.01f, 0f, 0f, 0f);

		// Token: 0x040016EB RID: 5867
		private static readonly float SPECULAR_DAWN = 5f;

		// Token: 0x040016EC RID: 5868
		private static readonly float SPECULAR_MIDDAY = 50f;

		// Token: 0x040016ED RID: 5869
		private static readonly float SPECULAR_DUSK = 5f;

		// Token: 0x040016EE RID: 5870
		private static readonly float SPECULAR_MIDNIGHT = 50f;

		// Token: 0x040016EF RID: 5871
		private static readonly float PITCH_DARK_WATER_BLEND = 0.9f;

		// Token: 0x040016F0 RID: 5872
		private static readonly float REFLECTION_DAWN = 0.75f;

		// Token: 0x040016F1 RID: 5873
		private static readonly float REFLECTION_MIDDAY = 0.75f;

		// Token: 0x040016F2 RID: 5874
		private static readonly float REFLECTION_DUSK = 0.5f;

		// Token: 0x040016F3 RID: 5875
		private static readonly float REFLECTION_MIDNIGHT = 0.5f;

		// Token: 0x040016F4 RID: 5876
		private static readonly Color NIGHTVISION_MILITARY = new Color(0f, 1f, 0f, 0f);

		// Token: 0x040016F5 RID: 5877
		private static readonly Color NIGHTVISION_CIVILIAN = new Color(0.25f, 0.25f, 0.25f, 0f);

		// Token: 0x040016F6 RID: 5878
		private static float _azimuth;

		// Token: 0x040016F7 RID: 5879
		private static float _transition;

		// Token: 0x040016F8 RID: 5880
		private static float _bias;

		// Token: 0x040016F9 RID: 5881
		private static float _fade;

		// Token: 0x040016FA RID: 5882
		private static float _time;

		// Token: 0x040016FB RID: 5883
		private static float _wind;

		// Token: 0x040016FC RID: 5884
		public static ELightingRain rainyness;

		// Token: 0x040016FD RID: 5885
		public static ELightingSnow snowyness;

		// Token: 0x04001702 RID: 5890
		private static byte[] _hash;

		// Token: 0x04001703 RID: 5891
		private static LightingInfo[] _times;

		// Token: 0x04001704 RID: 5892
		private static float _seaLevel;

		// Token: 0x04001705 RID: 5893
		private static float _snowLevel;

		// Token: 0x04001706 RID: 5894
		public static float rainFreq;

		// Token: 0x04001707 RID: 5895
		public static float rainDur;

		// Token: 0x04001708 RID: 5896
		public static float snowFreq;

		// Token: 0x04001709 RID: 5897
		public static float snowDur;

		// Token: 0x0400170A RID: 5898
		public static bool canRain;

		// Token: 0x0400170B RID: 5899
		public static bool canSnow;

		// Token: 0x0400170C RID: 5900
		public static ELightingVision vision;

		// Token: 0x0400170E RID: 5902
		protected static bool _isSea;

		// Token: 0x0400170F RID: 5903
		private static bool isSnow;

		// Token: 0x04001710 RID: 5904
		private static Material skybox;

		// Token: 0x04001711 RID: 5905
		private static Material mist;

		// Token: 0x04001712 RID: 5906
		private static Transform lighting;

		// Token: 0x04001713 RID: 5907
		private static Rain puddles;

		// Token: 0x04001714 RID: 5908
		private static Transform auroraBorealisTransform;

		// Token: 0x04001715 RID: 5909
		private static Material auroraBorealisMaterial;

		// Token: 0x04001716 RID: 5910
		private static float auroraBorealisCurrentIntensity;

		// Token: 0x04001717 RID: 5911
		private static float auroraBorealisTargetIntensity;

		// Token: 0x04001718 RID: 5912
		private static Color skyboxSky;

		// Token: 0x04001719 RID: 5913
		private static Color skyboxEquator;

		// Token: 0x0400171A RID: 5914
		private static Color skyboxGround;

		// Token: 0x0400171B RID: 5915
		private static Color skyboxClouds;

		// Token: 0x0400171C RID: 5916
		private static bool skyboxUpdated;

		// Token: 0x0400171D RID: 5917
		private static float lastSkyboxUpdate;

		// Token: 0x0400171E RID: 5918
		private static Color rainColor;

		// Token: 0x0400171F RID: 5919
		private static Color raysColor;

		// Token: 0x04001720 RID: 5920
		private static float raysIntensity;

		// Token: 0x04001721 RID: 5921
		private static float raysMultiplier;

		// Token: 0x04001722 RID: 5922
		public static Transform sun;

		// Token: 0x04001723 RID: 5923
		private static Transform sunFlare;

		// Token: 0x04001724 RID: 5924
		private static Transform moonFlare;

		// Token: 0x04001725 RID: 5925
		private static AudioSource _effectAudio;

		// Token: 0x04001726 RID: 5926
		private static AudioSource _dayAudio;

		// Token: 0x04001727 RID: 5927
		private static AudioSource _nightAudio;

		// Token: 0x04001728 RID: 5928
		private static AudioSource _waterAudio;

		// Token: 0x04001729 RID: 5929
		private static AudioSource _windAudio;

		// Token: 0x0400172A RID: 5930
		private static AudioSource _belowAudio;

		// Token: 0x0400172B RID: 5931
		private static AudioSource _rainAudio;

		// Token: 0x0400172C RID: 5932
		private static float currentAudioVolume;

		// Token: 0x0400172D RID: 5933
		private static float targetAudioVolume;

		// Token: 0x0400172E RID: 5934
		private static float nextAudioVolumeChangeTime;

		// Token: 0x0400172F RID: 5935
		private static float dayVolume;

		// Token: 0x04001730 RID: 5936
		private static float nightVolume;

		// Token: 0x04001731 RID: 5937
		private static Transform stars;

		// Token: 0x04001732 RID: 5938
		private static Camera reflectionCamera;

		// Token: 0x04001733 RID: 5939
		private static Cubemap reflectionMap;

		// Token: 0x04001734 RID: 5940
		private static Cubemap reflectionMapVision;

		// Token: 0x04001735 RID: 5941
		private static int reflectionIndex;

		// Token: 0x04001736 RID: 5942
		private static int reflectionIndexVision;

		// Token: 0x04001737 RID: 5943
		private static bool isReflectionBuilding;

		// Token: 0x04001738 RID: 5944
		private static bool isReflectionBuildingVision;

		// Token: 0x04001739 RID: 5945
		private static bool _isSkyboxReflectionEnabled;

		// Token: 0x0400173A RID: 5946
		private static Transform _bubbles;

		// Token: 0x0400173B RID: 5947
		private static Transform _snow;

		// Token: 0x0400173C RID: 5948
		private static Transform _rain;

		// Token: 0x0400173D RID: 5949
		private static WindZone _windZone;

		// Token: 0x0400173E RID: 5950
		private static Transform _clouds;

		// Token: 0x0400173F RID: 5951
		private static Material _sea;

		// Token: 0x04001740 RID: 5952
		private static PlanarReflection _reflection;

		// Token: 0x04001741 RID: 5953
		public static bool areFXAllowed = true;

		// Token: 0x04001742 RID: 5954
		private static Transform water;

		// Token: 0x04001743 RID: 5955
		private static Material[] moons;

		// Token: 0x04001744 RID: 5956
		private static byte _moon;

		// Token: 0x04001745 RID: 5957
		private static bool init;

		// Token: 0x04001746 RID: 5958
		private static Vector3 localPoint;

		// Token: 0x04001747 RID: 5959
		private static float localWindOverride;

		// Token: 0x04001748 RID: 5960
		private static IAmbianceNode localEffectNode;

		// Token: 0x04001749 RID: 5961
		private static bool localPlayingEffect;

		// Token: 0x0400174A RID: 5962
		private static bool localBlendingLight;

		// Token: 0x0400174B RID: 5963
		private static float localLightingBlend;

		// Token: 0x0400174C RID: 5964
		private static bool localBlendingFog;

		// Token: 0x0400174D RID: 5965
		private static float localFogBlend;

		// Token: 0x0400174E RID: 5966
		private static Color localFogColor;

		// Token: 0x0400174F RID: 5967
		private static float localFogHeight;

		// Token: 0x04001750 RID: 5968
		private static float rainBlend;

		// Token: 0x04001751 RID: 5969
		private static float snowBlend;

		// Token: 0x02000550 RID: 1360
		// (Invoke) Token: 0x06002534 RID: 9524
		public delegate void IsSeaChangedHandler(bool isSea);
	}
}
