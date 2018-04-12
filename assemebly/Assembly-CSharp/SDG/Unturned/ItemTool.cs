using System;
using System.Collections.Generic;
using SDG.Provider;
using UnityEngine;
using UnityEngine.Rendering;

namespace SDG.Unturned
{
	// Token: 0x02000738 RID: 1848
	public class ItemTool : MonoBehaviour
	{
		// Token: 0x06003417 RID: 13335 RVA: 0x001536D4 File Offset: 0x00151AD4
		public static string filterRarityRichText(string desc)
		{
			if (!string.IsNullOrEmpty(desc))
			{
				desc = desc.Replace("color=common", "color=#ffffff");
				desc = desc.Replace("color=gold", "color=#d2bf22");
				desc = desc.Replace("color=uncommon", "color=#1f871f");
				desc = desc.Replace("color=rare", "color=#4b64fa");
				desc = desc.Replace("color=epic", "color=#964bfa");
				desc = desc.Replace("color=legendary", "color=#c832fa");
				desc = desc.Replace("color=mythical", "color=#fa3219");
				desc = desc.Replace("color=red", "color=#bf1f1f");
				desc = desc.Replace("color=green", "color=#1f871f");
				desc = desc.Replace("color=blue", "color=#3298c8");
				desc = desc.Replace("color=orange", "color=#ab8019");
				desc = desc.Replace("color=yellow", "color=#dcb413");
				desc = desc.Replace("color=purple", "color=#6a466d");
			}
			return desc;
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x001537D8 File Offset: 0x00151BD8
		public static Color getRarityColorHighlight(EItemRarity rarity)
		{
			switch (rarity)
			{
			case EItemRarity.COMMON:
				return ItemTool.RARITY_COMMON_HIGHLIGHT;
			case EItemRarity.UNCOMMON:
				return ItemTool.RARITY_UNCOMMON_HIGHLIGHT;
			case EItemRarity.RARE:
				return ItemTool.RARITY_RARE_HIGHLIGHT;
			case EItemRarity.EPIC:
				return ItemTool.RARITY_EPIC_HIGHLIGHT;
			case EItemRarity.LEGENDARY:
				return ItemTool.RARITY_LEGENDARY_HIGHLIGHT;
			case EItemRarity.MYTHICAL:
				return ItemTool.RARITY_MYTHICAL_HIGHLIGHT;
			default:
				return Color.white;
			}
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x00153834 File Offset: 0x00151C34
		public static Color getRarityColorUI(EItemRarity rarity)
		{
			switch (rarity)
			{
			case EItemRarity.COMMON:
				return ItemTool.RARITY_COMMON_UI;
			case EItemRarity.UNCOMMON:
				return ItemTool.RARITY_UNCOMMON_UI;
			case EItemRarity.RARE:
				return ItemTool.RARITY_RARE_UI;
			case EItemRarity.EPIC:
				return ItemTool.RARITY_EPIC_UI;
			case EItemRarity.LEGENDARY:
				return ItemTool.RARITY_LEGENDARY_UI;
			case EItemRarity.MYTHICAL:
				return ItemTool.RARITY_MYTHICAL_UI;
			default:
				return Color.white;
			}
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x0015388D File Offset: 0x00151C8D
		public static Color getQualityColor(float quality)
		{
			if (quality < 0.5f)
			{
				return Color.Lerp(Palette.COLOR_R, Palette.COLOR_Y, quality * 2f);
			}
			return Color.Lerp(Palette.COLOR_Y, Palette.COLOR_G, (quality - 0.5f) * 2f);
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x001538D0 File Offset: 0x00151CD0
		private static Transform getEffectSystem(ushort mythicID, EEffectType type)
		{
			MythicAsset mythicAsset = (MythicAsset)Assets.find(EAssetType.MYTHIC, mythicID);
			if (mythicAsset == null)
			{
				return null;
			}
			UnityEngine.Object @object;
			switch (type)
			{
			case EEffectType.AREA:
				@object = mythicAsset.systemArea;
				break;
			case EEffectType.HOOK:
				@object = mythicAsset.systemHook;
				break;
			case EEffectType.THIRD:
				@object = mythicAsset.systemThird;
				break;
			case EEffectType.FIRST:
				@object = mythicAsset.systemFirst;
				break;
			default:
				return null;
			}
			if (@object == null)
			{
				return null;
			}
			Transform transform = ((GameObject)UnityEngine.Object.Instantiate(@object)).transform;
			transform.name = "System";
			return transform;
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x0015396C File Offset: 0x00151D6C
		public static void applyEffect(Transform[] bones, Transform[] systems, ushort mythicID, EEffectType type)
		{
			if (mythicID == 0)
			{
				return;
			}
			if (bones == null || systems == null)
			{
				return;
			}
			for (int i = 0; i < bones.Length; i++)
			{
				systems[i] = ItemTool.applyEffect(bones[i], mythicID, type);
			}
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x001539B0 File Offset: 0x00151DB0
		public static Transform applyEffect(Transform model, ushort mythicID, EEffectType type)
		{
			if (mythicID == 0)
			{
				return null;
			}
			if (model == null)
			{
				return null;
			}
			Transform transform = model.FindChild("Effect");
			Transform effectSystem = ItemTool.getEffectSystem(mythicID, type);
			if (effectSystem != null)
			{
				if (transform != null)
				{
					effectSystem.parent = transform;
					MythicLockee mythicLockee = effectSystem.gameObject.AddComponent<MythicLockee>();
					MythicLocker mythicLocker = transform.gameObject.AddComponent<MythicLocker>();
					mythicLocker.system = mythicLockee;
					mythicLockee.locker = mythicLocker;
				}
				else
				{
					effectSystem.parent = model;
					MythicLockee mythicLockee2 = effectSystem.gameObject.AddComponent<MythicLockee>();
					MythicLocker mythicLocker2 = model.gameObject.AddComponent<MythicLocker>();
					mythicLocker2.system = mythicLockee2;
					mythicLockee2.locker = mythicLocker2;
				}
				effectSystem.localPosition = Vector3.zero;
				effectSystem.localRotation = Quaternion.identity;
			}
			return effectSystem;
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x00153A7C File Offset: 0x00151E7C
		public static bool tryForceGiveItem(Player player, ushort id, byte amount)
		{
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, id);
			if (itemAsset == null || itemAsset.isPro)
			{
				return false;
			}
			for (int i = 0; i < (int)amount; i++)
			{
				Item item = new Item(id, EItemOrigin.ADMIN);
				player.inventory.forceAddItem(item, true);
			}
			return true;
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x00153AD4 File Offset: 0x00151ED4
		public static bool checkUseable(byte page, ushort id)
		{
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, id);
			if (itemAsset == null)
			{
				return false;
			}
			if (itemAsset.slot == ESlotType.NONE)
			{
				return itemAsset.isUseable;
			}
			if (itemAsset.slot == ESlotType.PRIMARY)
			{
				return page == 0 && itemAsset.isUseable;
			}
			return itemAsset.slot == ESlotType.SECONDARY && (page == 0 || page == 1) && itemAsset.isUseable;
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x00153B48 File Offset: 0x00151F48
		public static Transform getItem(ushort id, ushort skin, byte quality, byte[] state, bool viewmodel, GetStatTrackerValueHandler statTrackerCallback)
		{
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, id);
			return ItemTool.getItem(id, skin, quality, state, viewmodel, itemAsset, statTrackerCallback);
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x00153B70 File Offset: 0x00151F70
		public static Transform getItem(ushort id, ushort skin, byte quality, byte[] state, bool viewmodel, ItemAsset itemAsset, List<Mesh> outTempMeshes, out Material tempMaterial, GetStatTrackerValueHandler statTrackerCallback)
		{
			SkinAsset skinAsset = null;
			if (skin != 0)
			{
				skinAsset = (SkinAsset)Assets.find(EAssetType.SKIN, skin);
			}
			return ItemTool.getItem(id, skin, quality, state, viewmodel, itemAsset, skinAsset, outTempMeshes, out tempMaterial, statTrackerCallback);
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x00153BA8 File Offset: 0x00151FA8
		public static Transform getItem(ushort id, ushort skin, byte quality, byte[] state, bool viewmodel, ItemAsset itemAsset, GetStatTrackerValueHandler statTrackerCallback)
		{
			SkinAsset skinAsset = null;
			if (skin != 0)
			{
				skinAsset = (SkinAsset)Assets.find(EAssetType.SKIN, skin);
			}
			return ItemTool.getItem(id, skin, quality, state, viewmodel, itemAsset, skinAsset, statTrackerCallback);
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x00153BDC File Offset: 0x00151FDC
		public static Transform getItem(ushort id, ushort skin, byte quality, byte[] state, bool viewmodel, ItemAsset itemAsset, SkinAsset skinAsset, GetStatTrackerValueHandler statTrackerCallback)
		{
			Material material;
			return ItemTool.getItem(id, skin, quality, state, viewmodel, itemAsset, skinAsset, null, out material, statTrackerCallback);
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x00153C00 File Offset: 0x00152000
		public static Transform getItem(ushort id, ushort skin, byte quality, byte[] state, bool viewmodel, ItemAsset itemAsset, SkinAsset skinAsset, List<Mesh> outTempMeshes, out Material tempMaterial, GetStatTrackerValueHandler statTrackerCallback)
		{
			tempMaterial = null;
			if (itemAsset != null && itemAsset.item != null)
			{
				if (id != itemAsset.id)
				{
					Debug.LogError("ID and asset ID are not in sync!");
				}
				Transform transform = UnityEngine.Object.Instantiate<GameObject>(itemAsset.item).transform;
				transform.name = id.ToString();
				if (viewmodel)
				{
					Layerer.viewmodel(transform);
				}
				if (skinAsset != null)
				{
					if (skinAsset.overrideMeshes != null && skinAsset.overrideMeshes.Count > 0)
					{
						HighlighterTool.remesh(transform, skinAsset.overrideMeshes, outTempMeshes, true);
					}
					else if (outTempMeshes != null)
					{
						outTempMeshes.Clear();
					}
					if (skinAsset.primarySkin != null)
					{
						if (skinAsset.isPattern)
						{
							Material material = UnityEngine.Object.Instantiate<Material>(skinAsset.primarySkin);
							material.SetTexture("_AlbedoBase", itemAsset.albedoBase);
							material.SetTexture("_MetallicBase", itemAsset.metallicBase);
							material.SetTexture("_EmissionBase", itemAsset.emissionBase);
							HighlighterTool.rematerialize(transform, material, out tempMaterial);
						}
						else
						{
							HighlighterTool.rematerialize(transform, skinAsset.primarySkin, out tempMaterial);
						}
					}
				}
				else if (outTempMeshes != null)
				{
					outTempMeshes.Clear();
				}
				if (itemAsset.type == EItemType.GUN)
				{
					Attachments attachments = transform.gameObject.AddComponent<Attachments>();
					attachments.isSkinned = true;
					attachments.updateGun((ItemGunAsset)itemAsset, skinAsset);
					attachments.updateAttachments(state, viewmodel);
				}
				EStatTrackerType estatTrackerType;
				int num;
				if (!Dedicator.isDedicated && statTrackerCallback != null && statTrackerCallback(out estatTrackerType, out num))
				{
					StatTracker statTracker = transform.gameObject.AddComponent<StatTracker>();
					statTracker.statTrackerCallback = statTrackerCallback;
					statTracker.updateStatTracker(viewmodel);
				}
				return transform;
			}
			Transform transform2 = new GameObject().transform;
			transform2.name = id.ToString();
			if (viewmodel)
			{
				transform2.tag = "Viewmodel";
				transform2.gameObject.layer = LayerMasks.VIEWMODEL;
			}
			else
			{
				transform2.tag = "Item";
				transform2.gameObject.layer = LayerMasks.ITEM;
			}
			return transform2;
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x00153E30 File Offset: 0x00152230
		public static Texture2D getCard(Transform item, Transform hook_0, Transform hook_1, int width, int height, float size, float range)
		{
			if (item == null)
			{
				return null;
			}
			item.position = new Vector3(-256f, -256f, 0f);
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 16, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
			temporary.name = "Card_Render";
			RenderTexture.active = temporary;
			ItemTool.tool.GetComponent<Camera>().targetTexture = temporary;
			ItemTool.tool.GetComponent<Camera>().orthographicSize = size;
			Texture2D texture2D = new Texture2D(width * 2, height, TextureFormat.ARGB32, false, false);
			texture2D.name = "Card_Atlas";
			texture2D.filterMode = FilterMode.Point;
			texture2D.wrapMode = TextureWrapMode.Clamp;
			bool fog = RenderSettings.fog;
			AmbientMode ambientMode = RenderSettings.ambientMode;
			Color ambientSkyColor = RenderSettings.ambientSkyColor;
			Color ambientEquatorColor = RenderSettings.ambientEquatorColor;
			Color ambientGroundColor = RenderSettings.ambientGroundColor;
			RenderSettings.fog = false;
			RenderSettings.ambientMode = AmbientMode.Trilight;
			RenderSettings.ambientSkyColor = Color.white;
			RenderSettings.ambientEquatorColor = Color.white;
			RenderSettings.ambientGroundColor = Color.white;
			if (Provider.isConnected)
			{
				LevelLighting.setEnabled(false);
			}
			ItemTool.tool.GetComponent<Camera>().cullingMask = RayMasks.RESOURCE;
			ItemTool.tool.GetComponent<Camera>().farClipPlane = range;
			ItemTool.tool.transform.position = hook_0.position;
			ItemTool.tool.transform.rotation = hook_0.rotation;
			ItemTool.tool.GetComponent<Camera>().Render();
			texture2D.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
			ItemTool.tool.transform.position = hook_1.position;
			ItemTool.tool.transform.rotation = hook_1.rotation;
			ItemTool.tool.GetComponent<Camera>().Render();
			texture2D.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), width, 0);
			if (Provider.isConnected)
			{
				LevelLighting.setEnabled(true);
			}
			RenderSettings.fog = fog;
			RenderSettings.ambientMode = ambientMode;
			RenderSettings.ambientSkyColor = ambientSkyColor;
			RenderSettings.ambientEquatorColor = ambientEquatorColor;
			RenderSettings.ambientGroundColor = ambientGroundColor;
			item.position = new Vector3(0f, -256f, -256f);
			UnityEngine.Object.Destroy(item.gameObject);
			for (int i = 0; i < texture2D.width; i++)
			{
				for (int j = 0; j < texture2D.height; j++)
				{
					Color32 c = texture2D.GetPixel(i, j);
					if (c.r == 255 && c.g == 255 && c.b == 255)
					{
						c.a = 0;
					}
					else
					{
						c.a = byte.MaxValue;
					}
					texture2D.SetPixel(i, j, c);
				}
			}
			texture2D.Apply();
			RenderTexture.ReleaseTemporary(temporary);
			return texture2D;
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x001540FC File Offset: 0x001524FC
		public static void getIcon(ushort id, byte quality, byte[] state, ItemIconReady callback)
		{
			ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, id);
			ItemTool.getIcon(id, quality, state, itemAsset, callback);
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x00154120 File Offset: 0x00152520
		public static void getIcon(ushort id, byte quality, byte[] state, ItemAsset itemAsset, ItemIconReady callback)
		{
			ItemTool.getIcon(id, quality, state, itemAsset, (int)(itemAsset.size_x * 50), (int)(itemAsset.size_y * 50), callback);
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x00154140 File Offset: 0x00152540
		public static void getIcon(ushort id, byte quality, byte[] state, ItemAsset itemAsset, int x, int y, ItemIconReady callback)
		{
			ushort num = 0;
			SkinAsset skinAsset = null;
			string empty = string.Empty;
			string empty2 = string.Empty;
			int num2;
			if (Player.player != null && Player.player.channel.owner.getItemSkinItemDefID(id, out num2) && num2 != 0)
			{
				num = Provider.provider.economyService.getInventorySkinID(num2);
				skinAsset = (SkinAsset)Assets.find(EAssetType.SKIN, num);
				Player.player.channel.owner.getTagsAndDynamicPropsForItem(num2, out empty, out empty2);
			}
			ItemTool.getIcon(id, num, quality, state, itemAsset, skinAsset, empty, empty2, x, y, false, callback);
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x001541E0 File Offset: 0x001525E0
		public static void getIcon(ushort id, ushort skin, byte quality, byte[] state, ItemAsset itemAsset, SkinAsset skinAsset, string tags, string dynamic_props, int x, int y, bool scale, ItemIconReady callback)
		{
			if (itemAsset != null && id != itemAsset.id)
			{
				Debug.LogError("ID and item asset ID are not in sync!");
			}
			if (skinAsset != null && skin != skinAsset.id)
			{
				Debug.LogError("ID and skin asset ID are not in sync!");
			}
			Texture2D texture2D;
			if (state.Length == 0 && skin == 0 && x == (int)(itemAsset.size_x * 50) && y == (int)(itemAsset.size_y * 50) && ItemTool.cache.TryGetValue(id, out texture2D))
			{
				if (texture2D != null)
				{
					callback(texture2D);
					return;
				}
				ItemTool.cache.Remove(id);
			}
			ItemIconInfo itemIconInfo = new ItemIconInfo();
			itemIconInfo.id = id;
			itemIconInfo.skin = skin;
			itemIconInfo.quality = quality;
			itemIconInfo.state = state;
			itemIconInfo.itemAsset = itemAsset;
			itemIconInfo.skinAsset = skinAsset;
			itemIconInfo.tags = tags;
			itemIconInfo.dynamic_props = dynamic_props;
			itemIconInfo.x = x;
			itemIconInfo.y = y;
			itemIconInfo.scale = scale;
			itemIconInfo.callback = callback;
			ItemTool.icons.Enqueue(itemIconInfo);
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x00154300 File Offset: 0x00152700
		public static Texture2D captureIcon(ushort id, ushort skin, Transform model, Transform icon, int width, int height, float orthoSize)
		{
			ItemTool.tool.transform.position = icon.position;
			ItemTool.tool.transform.rotation = icon.rotation;
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 16, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
			temporary.name = string.Concat(new object[]
			{
				"Render_",
				id,
				"_",
				skin
			});
			RenderTexture.active = temporary;
			ItemTool.tool.GetComponent<Camera>().targetTexture = temporary;
			ItemTool.tool.GetComponent<Camera>().orthographicSize = orthoSize;
			bool fog = RenderSettings.fog;
			AmbientMode ambientMode = RenderSettings.ambientMode;
			Color ambientSkyColor = RenderSettings.ambientSkyColor;
			Color ambientEquatorColor = RenderSettings.ambientEquatorColor;
			Color ambientGroundColor = RenderSettings.ambientGroundColor;
			RenderSettings.fog = false;
			RenderSettings.ambientMode = AmbientMode.Trilight;
			RenderSettings.ambientSkyColor = Color.white;
			RenderSettings.ambientEquatorColor = Color.white;
			RenderSettings.ambientGroundColor = Color.white;
			if (Provider.isConnected)
			{
				LevelLighting.setEnabled(false);
			}
			ItemTool.tool.GetComponent<Light>().enabled = true;
			ItemTool.tool.GetComponent<Camera>().cullingMask = (RayMasks.ITEM | RayMasks.VEHICLE | RayMasks.MEDIUM | RayMasks.SMALL);
			ItemTool.tool.GetComponent<Camera>().farClipPlane = 16f;
			ItemTool.tool.GetComponent<Camera>().Render();
			ItemTool.tool.GetComponent<Light>().enabled = false;
			if (Provider.isConnected)
			{
				LevelLighting.setEnabled(true);
			}
			RenderSettings.fog = fog;
			RenderSettings.ambientMode = ambientMode;
			RenderSettings.ambientSkyColor = ambientSkyColor;
			RenderSettings.ambientEquatorColor = ambientEquatorColor;
			RenderSettings.ambientGroundColor = ambientGroundColor;
			model.position = new Vector3(0f, -256f, -256f);
			UnityEngine.Object.Destroy(model.gameObject);
			Texture2D texture2D = new Texture2D(width, height, TextureFormat.ARGB32, false, true);
			texture2D.name = string.Concat(new object[]
			{
				"Icon_",
				id,
				"_",
				skin
			});
			texture2D.filterMode = FilterMode.Point;
			texture2D.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
			texture2D.Apply();
			RenderTexture.ReleaseTemporary(temporary);
			return texture2D;
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x00154530 File Offset: 0x00152930
		private bool getIconStatTrackerValue(out EStatTrackerType type, out int kills)
		{
			DynamicEconDetails dynamicEconDetails = new DynamicEconDetails(this.currentIconTags, this.currentIconDynamicProps);
			return dynamicEconDetails.getStatTrackerValue(out type, out kills);
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x0015455C File Offset: 0x0015295C
		private void Update()
		{
			if (ItemTool.icons == null || ItemTool.icons.Count == 0)
			{
				return;
			}
			ItemIconInfo itemIconInfo = ItemTool.icons.Dequeue();
			if (itemIconInfo == null)
			{
				return;
			}
			if (itemIconInfo.itemAsset == null)
			{
				return;
			}
			this.currentIconTags = itemIconInfo.tags;
			this.currentIconDynamicProps = itemIconInfo.dynamic_props;
			Transform item = ItemTool.getItem(itemIconInfo.id, itemIconInfo.skin, itemIconInfo.quality, itemIconInfo.state, false, itemIconInfo.itemAsset, itemIconInfo.skinAsset, new GetStatTrackerValueHandler(this.getIconStatTrackerValue));
			item.position = new Vector3(-256f, -256f, 0f);
			Transform transform;
			if (itemIconInfo.scale && itemIconInfo.skin != 0)
			{
				if (itemIconInfo.itemAsset.size2_z == 0f)
				{
					item.position = new Vector3(0f, -256f, -256f);
					UnityEngine.Object.Destroy(item.gameObject);
					Assets.errors.Add("Failed to create a skin icon of size 0 for " + itemIconInfo.id + ".");
					return;
				}
				transform = item.FindChild("Icon2");
				if (transform == null)
				{
					item.position = new Vector3(0f, -256f, -256f);
					UnityEngine.Object.Destroy(item.gameObject);
					Assets.errors.Add("Failed to find a skin icon hook on " + itemIconInfo.id + ".");
					return;
				}
			}
			else
			{
				if (itemIconInfo.itemAsset.size_z == 0f)
				{
					item.position = new Vector3(0f, -256f, -256f);
					UnityEngine.Object.Destroy(item.gameObject);
					Assets.errors.Add("Failed to create an item icon of size 0 for " + itemIconInfo.id + ".");
					return;
				}
				transform = item.FindChild("Icon");
				if (transform == null)
				{
					item.position = new Vector3(0f, -256f, -256f);
					UnityEngine.Object.Destroy(item.gameObject);
					Assets.errors.Add("Failed to find an item icon hook on " + itemIconInfo.id + ".");
					return;
				}
			}
			float num = itemIconInfo.itemAsset.size_z;
			if (itemIconInfo.scale)
			{
				if (itemIconInfo.skin != 0)
				{
					num = itemIconInfo.itemAsset.size2_z;
				}
				else
				{
					float num2 = (float)itemIconInfo.itemAsset.size_x / (float)itemIconInfo.itemAsset.size_y;
					num *= num2;
				}
			}
			Texture2D texture2D = ItemTool.captureIcon(itemIconInfo.id, itemIconInfo.skin, item, transform, itemIconInfo.x, itemIconInfo.y, num);
			if (itemIconInfo.callback != null)
			{
				itemIconInfo.callback(texture2D);
			}
			if (itemIconInfo.state.Length == 0 && itemIconInfo.skin == 0 && itemIconInfo.x == (int)(itemIconInfo.itemAsset.size_x * 50) && itemIconInfo.y == (int)(itemIconInfo.itemAsset.size_y * 50) && !ItemTool.cache.ContainsKey(itemIconInfo.id))
			{
				ItemTool.cache.Add(itemIconInfo.id, texture2D);
			}
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x001548A7 File Offset: 0x00152CA7
		private void Start()
		{
			ItemTool.tool = this;
			ItemTool.icons = new Queue<ItemIconInfo>();
		}

		// Token: 0x0400238B RID: 9099
		private static readonly Color RARITY_COMMON_HIGHLIGHT = Color.white;

		// Token: 0x0400238C RID: 9100
		private static readonly Color RARITY_COMMON_UI = Color.white;

		// Token: 0x0400238D RID: 9101
		private static readonly Color RARITY_UNCOMMON_HIGHLIGHT = Color.green;

		// Token: 0x0400238E RID: 9102
		private static readonly Color RARITY_UNCOMMON_UI = new Color(0.121568628f, 0.5294118f, 0.121568628f);

		// Token: 0x0400238F RID: 9103
		private static readonly Color RARITY_RARE_HIGHLIGHT = Color.blue;

		// Token: 0x04002390 RID: 9104
		private static readonly Color RARITY_RARE_UI = new Color(0.294117659f, 0.392156869f, 0.980392158f);

		// Token: 0x04002391 RID: 9105
		private static readonly Color RARITY_EPIC_HIGHLIGHT = new Color(0.33f, 0f, 1f);

		// Token: 0x04002392 RID: 9106
		private static readonly Color RARITY_EPIC_UI = new Color(0.5882353f, 0.294117659f, 0.980392158f);

		// Token: 0x04002393 RID: 9107
		private static readonly Color RARITY_LEGENDARY_HIGHLIGHT = Color.magenta;

		// Token: 0x04002394 RID: 9108
		private static readonly Color RARITY_LEGENDARY_UI = new Color(0.784313738f, 0.196078435f, 0.980392158f);

		// Token: 0x04002395 RID: 9109
		private static readonly Color RARITY_MYTHICAL_HIGHLIGHT = Color.red;

		// Token: 0x04002396 RID: 9110
		private static readonly Color RARITY_MYTHICAL_UI = new Color(0.980392158f, 0.196078435f, 0.09803922f);

		// Token: 0x04002397 RID: 9111
		private static ItemTool tool;

		// Token: 0x04002398 RID: 9112
		private static Dictionary<ushort, Texture2D> cache = new Dictionary<ushort, Texture2D>();

		// Token: 0x04002399 RID: 9113
		private static Queue<ItemIconInfo> icons;

		// Token: 0x0400239A RID: 9114
		private string currentIconTags;

		// Token: 0x0400239B RID: 9115
		private string currentIconDynamicProps;
	}
}
