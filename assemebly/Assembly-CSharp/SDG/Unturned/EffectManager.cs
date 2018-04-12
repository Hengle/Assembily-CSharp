using System;
using System.Collections.Generic;
using SDG.Framework.Landscapes;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Unturned
{
	// Token: 0x0200059A RID: 1434
	public class EffectManager : SteamCaller
	{
		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060027E6 RID: 10214 RVA: 0x000F1E8D File Offset: 0x000F028D
		public static EffectManager instance
		{
			get
			{
				return EffectManager.manager;
			}
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x000F1E94 File Offset: 0x000F0294
		public static GameObject Instantiate(GameObject element)
		{
			GameObject gameObject = EffectManager.pool.Instantiate(element);
			ParticleSystem component = gameObject.GetComponent<ParticleSystem>();
			if (component != null)
			{
				component.Stop(true);
				component.Clear(true);
			}
			gameObject.transform.parent = Level.effects;
			gameObject.tag = "Debris";
			gameObject.layer = LayerMasks.DEBRIS;
			return gameObject;
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x000F1EF5 File Offset: 0x000F02F5
		public static void Destroy(GameObject element)
		{
			if (element == null)
			{
				return;
			}
			EffectManager.pool.Destroy(element);
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x000F1F0F File Offset: 0x000F030F
		public static void Destroy(GameObject element, float t)
		{
			if (element == null)
			{
				return;
			}
			EffectManager.pool.Destroy(element, t);
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x000F1F2C File Offset: 0x000F032C
		[SteamCall]
		public void tellEffectClearByID(CSteamID steamID, ushort id)
		{
			if (base.channel.checkServer(steamID))
			{
				string b = id.ToString();
				for (int i = 0; i < Level.effects.childCount; i++)
				{
					Transform child = Level.effects.GetChild(i);
					if (child.gameObject.activeSelf)
					{
						if (!(child.name != b))
						{
							EffectManager.Destroy(child.gameObject);
						}
					}
				}
			}
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x000F1FB5 File Offset: 0x000F03B5
		public static void askEffectClearByID(ushort id, CSteamID steamID)
		{
			if (Provider.isServer)
			{
				EffectManager.manager.channel.send("tellEffectClearByID", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					id
				});
			}
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x000F1FE8 File Offset: 0x000F03E8
		[SteamCall]
		public void tellEffectClearAll(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID))
			{
				for (int i = 0; i < Level.effects.childCount; i++)
				{
					Transform child = Level.effects.GetChild(i);
					if (child.gameObject.activeSelf)
					{
						if (!(child.name == "System"))
						{
							EffectManager.Destroy(child.gameObject);
						}
					}
				}
			}
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x000F2067 File Offset: 0x000F0467
		public static void askEffectClearAll()
		{
			if (Provider.isServer)
			{
				EffectManager.manager.channel.send("tellEffectClearAll", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
			}
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x000F2090 File Offset: 0x000F0490
		public static void sendEffect(ushort id, byte x, byte y, byte area, Vector3 point, Vector3 normal)
		{
			EffectManager.manager.channel.send("tellEffectPointNormal", ESteamCall.CLIENTS, x, y, area, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				id,
				point,
				normal
			});
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x000F20DC File Offset: 0x000F04DC
		public static void sendEffect(ushort id, byte x, byte y, byte area, Vector3 point)
		{
			EffectManager.manager.channel.send("tellEffectPoint", ESteamCall.CLIENTS, x, y, area, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				id,
				point
			});
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x000F211C File Offset: 0x000F051C
		public static void sendEffectReliable(ushort id, byte x, byte y, byte area, Vector3 point, Vector3 normal)
		{
			EffectManager.manager.channel.send("tellEffectPointNormal", ESteamCall.CLIENTS, x, y, area, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				point,
				normal
			});
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000F2168 File Offset: 0x000F0568
		public static void sendEffectReliable(ushort id, byte x, byte y, byte area, Vector3 point)
		{
			EffectManager.manager.channel.send("tellEffectPoint", ESteamCall.CLIENTS, x, y, area, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				point
			});
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x000F21A8 File Offset: 0x000F05A8
		public static void sendEffect(ushort id, float radius, Vector3 point, Vector3 normal)
		{
			EffectManager.manager.channel.send("tellEffectPointNormal", ESteamCall.CLIENTS, point, radius, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				id,
				point,
				normal
			});
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000F21F0 File Offset: 0x000F05F0
		public static void sendEffect(ushort id, float radius, Vector3 point)
		{
			EffectManager.manager.channel.send("tellEffectPoint", ESteamCall.CLIENTS, point, radius, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				id,
				point
			});
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000F2230 File Offset: 0x000F0630
		public static void sendEffectReliable(ushort id, float radius, Vector3 point, Vector3 normal)
		{
			EffectManager.manager.channel.send("tellEffectPointNormal", ESteamCall.CLIENTS, point, radius, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				point,
				normal
			});
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000F2278 File Offset: 0x000F0678
		public static void sendEffectReliable(ushort id, float radius, Vector3 point)
		{
			EffectManager.manager.channel.send("tellEffectPoint", ESteamCall.CLIENTS, point, radius, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				point
			});
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000F22B6 File Offset: 0x000F06B6
		public static void sendEffect(ushort id, CSteamID steamID, Vector3 point, Vector3 normal)
		{
			EffectManager.manager.channel.send("tellEffectPointNormal", steamID, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				id,
				point,
				normal
			});
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x000F22F0 File Offset: 0x000F06F0
		public static void sendEffect(ushort id, CSteamID steamID, Vector3 point)
		{
			EffectManager.manager.channel.send("tellEffectPoint", steamID, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				id,
				point
			});
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x000F2321 File Offset: 0x000F0721
		public static void sendEffectReliable(ushort id, CSteamID steamID, Vector3 point, Vector3 normal)
		{
			EffectManager.manager.channel.send("tellEffectPointNormal", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				point,
				normal
			});
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000F235B File Offset: 0x000F075B
		public static void sendEffectReliable(ushort id, CSteamID steamID, Vector3 point)
		{
			EffectManager.manager.channel.send("tellEffectPoint", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				point
			});
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x000F238C File Offset: 0x000F078C
		public static void sendUIEffect(ushort id, short key, bool reliable)
		{
			EffectManager.manager.channel.send("tellUIEffect", ESteamCall.CLIENTS, (!reliable) ? ESteamPacket.UPDATE_UNRELIABLE_BUFFER : ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				key
			});
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000F23CC File Offset: 0x000F07CC
		public static void sendUIEffect(ushort id, short key, bool reliable, string arg0)
		{
			EffectManager.manager.channel.send("tellUIEffect1Arg", ESteamCall.CLIENTS, (!reliable) ? ESteamPacket.UPDATE_UNRELIABLE_BUFFER : ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				key,
				arg0
			});
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x000F241C File Offset: 0x000F081C
		public static void sendUIEffect(ushort id, short key, bool reliable, string arg0, string arg1)
		{
			EffectManager.manager.channel.send("tellUIEffect2Args", ESteamCall.CLIENTS, (!reliable) ? ESteamPacket.UPDATE_UNRELIABLE_BUFFER : ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				key,
				arg0,
				arg1
			});
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x000F2470 File Offset: 0x000F0870
		public static void sendUIEffect(ushort id, short key, bool reliable, string arg0, string arg1, string arg2)
		{
			EffectManager.manager.channel.send("tellUIEffect3Args", ESteamCall.CLIENTS, (!reliable) ? ESteamPacket.UPDATE_UNRELIABLE_BUFFER : ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				key,
				arg0,
				arg1,
				arg2
			});
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x000F24C8 File Offset: 0x000F08C8
		public static void sendUIEffect(ushort id, short key, bool reliable, string arg0, string arg1, string arg2, string arg3)
		{
			EffectManager.manager.channel.send("tellUIEffect4Args", ESteamCall.CLIENTS, (!reliable) ? ESteamPacket.UPDATE_UNRELIABLE_BUFFER : ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				key,
				arg0,
				arg1,
				arg2,
				arg3
			});
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x000F2524 File Offset: 0x000F0924
		public static void sendUIEffect(ushort id, short key, CSteamID steamID, bool reliable)
		{
			EffectManager.manager.channel.send("tellUIEffect", steamID, (!reliable) ? ESteamPacket.UPDATE_UNRELIABLE_BUFFER : ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				key
			});
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x000F2564 File Offset: 0x000F0964
		public static void sendUIEffect(ushort id, short key, CSteamID steamID, bool reliable, string arg0)
		{
			EffectManager.manager.channel.send("tellUIEffect1Arg", steamID, (!reliable) ? ESteamPacket.UPDATE_UNRELIABLE_BUFFER : ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				key,
				arg0
			});
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x000F25B4 File Offset: 0x000F09B4
		public static void sendUIEffect(ushort id, short key, CSteamID steamID, bool reliable, string arg0, string arg1)
		{
			EffectManager.manager.channel.send("tellUIEffect2Args", steamID, (!reliable) ? ESteamPacket.UPDATE_UNRELIABLE_BUFFER : ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				key,
				arg0,
				arg1
			});
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x000F2608 File Offset: 0x000F0A08
		public static void sendUIEffect(ushort id, short key, CSteamID steamID, bool reliable, string arg0, string arg1, string arg2)
		{
			EffectManager.manager.channel.send("tellUIEffect3Args", steamID, (!reliable) ? ESteamPacket.UPDATE_UNRELIABLE_BUFFER : ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				key,
				arg0,
				arg1,
				arg2
			});
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x000F2660 File Offset: 0x000F0A60
		public static void sendUIEffect(ushort id, short key, CSteamID steamID, bool reliable, string arg0, string arg1, string arg2, string arg3)
		{
			EffectManager.manager.channel.send("tellUIEffect4Args", steamID, (!reliable) ? ESteamPacket.UPDATE_UNRELIABLE_BUFFER : ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				id,
				key,
				arg0,
				arg1,
				arg2,
				arg3
			});
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000F26BD File Offset: 0x000F0ABD
		[SteamCall]
		public void tellEffectPointNormal(CSteamID steamID, ushort id, Vector3 point, Vector3 normal)
		{
			if (base.channel.checkServer(steamID))
			{
				EffectManager.effect(id, point, normal);
			}
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000F26DA File Offset: 0x000F0ADA
		[SteamCall]
		public void tellEffectPoint(CSteamID steamID, ushort id, Vector3 point)
		{
			if (base.channel.checkServer(steamID))
			{
				EffectManager.effect(id, point, Vector3.up);
			}
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x000F26FA File Offset: 0x000F0AFA
		[SteamCall]
		public void tellUIEffect(CSteamID steamID, ushort id, short key)
		{
			if (!base.channel.checkServer(steamID))
			{
				return;
			}
			EffectManager.createUIEffect(id, key);
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000F2716 File Offset: 0x000F0B16
		[SteamCall]
		public void tellUIEffect1Arg(CSteamID steamID, ushort id, short key, string arg0)
		{
			if (!base.channel.checkServer(steamID))
			{
				return;
			}
			EffectManager.createAndFormatUIEffect(id, key, new object[]
			{
				arg0
			});
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x000F273D File Offset: 0x000F0B3D
		[SteamCall]
		public void tellUIEffect2Args(CSteamID steamID, ushort id, short key, string arg0, string arg1)
		{
			if (!base.channel.checkServer(steamID))
			{
				return;
			}
			EffectManager.createAndFormatUIEffect(id, key, new object[]
			{
				arg0,
				arg1
			});
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x000F2769 File Offset: 0x000F0B69
		[SteamCall]
		public void tellUIEffect3Args(CSteamID steamID, ushort id, short key, string arg0, string arg1, string arg2)
		{
			if (!base.channel.checkServer(steamID))
			{
				return;
			}
			EffectManager.createAndFormatUIEffect(id, key, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x000F279A File Offset: 0x000F0B9A
		[SteamCall]
		public void tellUIEffect4Args(CSteamID steamID, ushort id, short key, string arg0, string arg1, string arg2, string arg3)
		{
			if (!base.channel.checkServer(steamID))
			{
				return;
			}
			EffectManager.createAndFormatUIEffect(id, key, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			});
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x000F27D0 File Offset: 0x000F0BD0
		public static Transform createAndFormatUIEffect(ushort id, short key, params object[] args)
		{
			Transform transform = EffectManager.createUIEffect(id, key);
			EffectManager.formatTextIntoUIEffect(transform, args);
			return transform;
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000F27F0 File Offset: 0x000F0BF0
		public static Transform createUIEffect(ushort id, short key)
		{
			EffectAsset effectAsset = (EffectAsset)Assets.find(EAssetType.EFFECT, id);
			if (effectAsset == null)
			{
				return null;
			}
			Transform transform = UnityEngine.Object.Instantiate<GameObject>(effectAsset.effect).transform;
			transform.name = id.ToString();
			transform.SetParent(Level.effects);
			if (key == -1)
			{
				if (effectAsset.lifetime > 1.401298E-45f)
				{
					EffectManager.Destroy(transform.gameObject, effectAsset.lifetime + UnityEngine.Random.Range(-effectAsset.lifetimeSpread, effectAsset.lifetimeSpread));
				}
			}
			else
			{
				GameObject element;
				if (EffectManager.indexedUIEffects.TryGetValue(key, out element))
				{
					EffectManager.Destroy(element);
					EffectManager.indexedUIEffects.Remove(key);
				}
				EffectManager.indexedUIEffects.Add(key, transform.gameObject);
			}
			return transform;
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x000F28B8 File Offset: 0x000F0CB8
		public static void formatTextIntoUIEffect(Transform effect, params object[] args)
		{
			EffectManager.formattingComponents.Clear();
			effect.GetComponentsInChildren<Text>(EffectManager.formattingComponents);
			foreach (Text text in EffectManager.formattingComponents)
			{
				text.text = string.Format(text.text, args);
			}
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x000F2934 File Offset: 0x000F0D34
		public static Transform effect(ushort id, Vector3 point, Vector3 normal)
		{
			EffectAsset effectAsset = (EffectAsset)Assets.find(EAssetType.EFFECT, id);
			if (effectAsset == null)
			{
				return null;
			}
			if (effectAsset.splatterTemperature != EPlayerTemperature.NONE)
			{
				Transform transform = new GameObject().transform;
				transform.name = "Temperature";
				transform.parent = Level.effects;
				transform.position = point + Vector3.down * -2f;
				transform.localScale = Vector3.one * 6f;
				transform.gameObject.SetActive(false);
				transform.gameObject.AddComponent<TemperatureTrigger>().temperature = effectAsset.splatterTemperature;
				transform.gameObject.SetActive(true);
				UnityEngine.Object.Destroy(transform.gameObject, effectAsset.splatterLifetime - effectAsset.splatterLifetimeSpread);
			}
			if (Dedicator.isDedicated)
			{
				if (!effectAsset.spawnOnDedicatedServer)
				{
					return null;
				}
			}
			else if (GraphicsSettings.effectQuality == EGraphicQuality.OFF && !effectAsset.splatterLiquid)
			{
				return null;
			}
			Quaternion quaternion = Quaternion.LookRotation(normal);
			if (effectAsset.randomizeRotation)
			{
				quaternion *= Quaternion.Euler(0f, 0f, (float)UnityEngine.Random.Range(0, 360));
			}
			Transform transform2 = EffectManager.pool.Instantiate(effectAsset.effect, point, quaternion).transform;
			transform2.name = id.ToString();
			transform2.parent = Level.effects;
			if (effectAsset.splatter > 0 && (!effectAsset.gore || OptionsSettings.gore))
			{
				for (int i = 0; i < (int)(effectAsset.splatter * ((effectAsset.splatterLiquid || !(Player.player != null) || Player.player.skills.boost != EPlayerBoost.SPLATTERIFIC) ? 1 : 8)); i++)
				{
					RaycastHit raycastHit;
					if (effectAsset.splatterLiquid)
					{
						float f = UnityEngine.Random.Range(0f, 6.28318548f);
						float num = UnityEngine.Random.Range(1f, 6f);
						Ray ray = new Ray(point + new Vector3(Mathf.Cos(f) * num, 0f, Mathf.Sin(f) * num), Vector3.down);
						int splatter = RayMasks.SPLATTER;
						LandscapeHoleUtility.raycastIgnoreLandscapeIfNecessary(ray, 8f, ref splatter);
						Physics.Raycast(ray, out raycastHit, 8f, splatter);
					}
					else
					{
						Ray ray2 = new Ray(point, -2f * normal + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)));
						int splatter2 = RayMasks.SPLATTER;
						LandscapeHoleUtility.raycastIgnoreLandscapeIfNecessary(ray2, 8f, ref splatter2);
						Physics.Raycast(ray2, out raycastHit, 8f, splatter2);
					}
					if (raycastHit.transform != null)
					{
						EPhysicsMaterial material = DamageTool.getMaterial(raycastHit.point, raycastHit.transform, raycastHit.collider);
						if (!PhysicsTool.isMaterialDynamic(material))
						{
							float num2 = UnityEngine.Random.Range(1f, 2f);
							Transform transform3 = EffectManager.pool.Instantiate(effectAsset.splatters[UnityEngine.Random.Range(0, effectAsset.splatters.Length)], raycastHit.point + raycastHit.normal * UnityEngine.Random.Range(0.04f, 0.06f), Quaternion.LookRotation(raycastHit.normal) * Quaternion.Euler(0f, 0f, (float)UnityEngine.Random.Range(0, 360))).transform;
							transform3.name = "Splatter";
							transform3.parent = Level.effects;
							transform3.localScale = new Vector3(num2, num2, num2);
							transform3.gameObject.SetActive(true);
							if (effectAsset.splatterLifetime > 1.401298E-45f)
							{
								EffectManager.pool.Destroy(transform3.gameObject, effectAsset.splatterLifetime + UnityEngine.Random.Range(-effectAsset.splatterLifetimeSpread, effectAsset.splatterLifetimeSpread));
							}
							else
							{
								EffectManager.pool.Destroy(transform3.gameObject, GraphicsSettings.effect);
							}
						}
					}
				}
			}
			if (effectAsset.gore)
			{
				transform2.GetComponent<ParticleSystem>().emission.enabled = OptionsSettings.gore;
			}
			if (!effectAsset.isStatic && transform2.GetComponent<AudioSource>() != null)
			{
				transform2.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.9f, 1.1f);
			}
			if (effectAsset.lifetime > 1.401298E-45f)
			{
				EffectManager.pool.Destroy(transform2.gameObject, effectAsset.lifetime + UnityEngine.Random.Range(-effectAsset.lifetimeSpread, effectAsset.lifetimeSpread));
			}
			else
			{
				float num3 = 0f;
				MeshRenderer component = transform2.GetComponent<MeshRenderer>();
				if (component == null)
				{
					ParticleSystem component2 = transform2.GetComponent<ParticleSystem>();
					if (component2 != null)
					{
						if (component2.main.loop)
						{
							num3 = component2.main.startLifetime.constantMax;
						}
						else
						{
							num3 = component2.main.duration + component2.main.startLifetime.constantMax;
						}
					}
					AudioSource component3 = transform2.GetComponent<AudioSource>();
					if (component3 != null && component3.clip != null && component3.clip.length > num3)
					{
						num3 = component3.clip.length;
					}
				}
				if (num3 < 1.401298E-45f)
				{
					num3 = GraphicsSettings.effect;
				}
				EffectManager.pool.Destroy(transform2.gameObject, num3);
			}
			if (effectAsset.blast > 0 && GraphicsSettings.blast && GraphicsSettings.renderMode == ERenderMode.DEFERRED)
			{
				EffectManager.effect(effectAsset.blast, point, new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), 1f, UnityEngine.Random.Range(-0.1f, 0.1f)));
			}
			return transform2;
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000F2F40 File Offset: 0x000F1340
		private void onLevelLoaded(int level)
		{
			EffectManager.pool = new GameObjectPoolDictionary();
			EffectManager.indexedUIEffects = new Dictionary<short, GameObject>();
			if (Dedicator.isDedicated)
			{
				return;
			}
			Asset[] array = Assets.find(EAssetType.EFFECT);
			for (int i = 0; i < array.Length; i++)
			{
				EffectAsset effectAsset = array[i] as EffectAsset;
				if (effectAsset != null && !(effectAsset.effect == null) && effectAsset.preload != 0)
				{
					EffectManager.pool.Instantiate(effectAsset.effect, Level.effects, effectAsset.id.ToString(), (int)effectAsset.preload);
					if (effectAsset.splatter > 0 && effectAsset.splatterPreload > 0)
					{
						for (int j = 0; j < effectAsset.splatters.Length; j++)
						{
							EffectManager.pool.Instantiate(effectAsset.splatters[j], Level.effects, "Splatter", (int)effectAsset.splatterPreload);
						}
					}
				}
			}
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000F3036 File Offset: 0x000F1436
		private void Start()
		{
			EffectManager.manager = this;
			Level.onPrePreLevelLoaded = (PrePreLevelLoaded)Delegate.Combine(Level.onPrePreLevelLoaded, new PrePreLevelLoaded(this.onLevelLoaded));
		}

		// Token: 0x04001902 RID: 6402
		public static readonly float SMALL = 64f;

		// Token: 0x04001903 RID: 6403
		public static readonly float MEDIUM = 128f;

		// Token: 0x04001904 RID: 6404
		public static readonly float LARGE = 256f;

		// Token: 0x04001905 RID: 6405
		public static readonly float INSANE = 512f;

		// Token: 0x04001906 RID: 6406
		private static List<Text> formattingComponents = new List<Text>();

		// Token: 0x04001907 RID: 6407
		private static EffectManager manager;

		// Token: 0x04001908 RID: 6408
		private static GameObjectPoolDictionary pool;

		// Token: 0x04001909 RID: 6409
		private static Dictionary<short, GameObject> indexedUIEffects;
	}
}
