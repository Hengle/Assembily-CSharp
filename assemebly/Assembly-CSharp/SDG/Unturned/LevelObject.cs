using System;
using System.Collections.Generic;
using SDG.Framework.Foliage;
using UnityEngine;
using UnityEngine.Rendering;

namespace SDG.Unturned
{
	// Token: 0x02000556 RID: 1366
	public class LevelObject
	{
		// Token: 0x06002563 RID: 9571 RVA: 0x000D9788 File Offset: 0x000D7B88
		public LevelObject(Vector3 newPoint, Quaternion newRotation, Vector3 newScale, ushort newID, string newName, Guid newGUID, ELevelObjectPlacementOrigin newPlacementOrigin, uint newInstanceID) : this(newPoint, newRotation, newScale, newID, newName, newGUID, newPlacementOrigin, newInstanceID, AssetReference<MaterialPaletteAsset>.invalid, -1, false)
		{
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000D97B0 File Offset: 0x000D7BB0
		public LevelObject(Vector3 newPoint, Quaternion newRotation, Vector3 newScale, ushort newID, string newName, Guid newGUID, ELevelObjectPlacementOrigin newPlacementOrigin, uint newInstanceID, AssetReference<MaterialPaletteAsset> customMaterialOverride, int materialIndexOverride, bool isHierarchyItem)
		{
			this._id = newID;
			this._name = newName;
			this._GUID = newGUID;
			this._instanceID = newInstanceID;
			if (this.GUID == Guid.Empty)
			{
				this._asset = (ObjectAsset)Assets.find(EAssetType.OBJECT, this.name);
				if (this.asset == null || this.asset.id != this.id)
				{
					this._asset = (ObjectAsset)Assets.find(EAssetType.OBJECT, this.id);
				}
				if (this.asset != null)
				{
					this._GUID = this.asset.GUID;
				}
			}
			else
			{
				this._asset = Assets.find<ObjectAsset>(new AssetReference<ObjectAsset>(this.GUID));
			}
			if (this.asset == null)
			{
				return;
			}
			this._name = this.asset.name;
			this.state = this.asset.getState();
			this.placementOrigin = newPlacementOrigin;
			this.areConditionsMet = true;
			this.haveConditionsBeenChecked = false;
			GameObject modelGameObject = this.asset.modelGameObject;
			if (Dedicator.isDedicated)
			{
				if (modelGameObject != null)
				{
					this._transform = UnityEngine.Object.Instantiate<GameObject>(modelGameObject).transform;
					this.transform.name = ((!isHierarchyItem) ? this.id.ToString() : this.GUID.ToString("N"));
					this.transform.parent = LevelObjects.models;
					this.transform.position = newPoint;
					this.transform.rotation = newRotation;
					this.isDecal = this.transform.FindChild("Decal");
					if (this.asset.useScale)
					{
						this.transform.localScale = newScale;
					}
				}
				this.renderers = null;
			}
			else if (modelGameObject != null)
			{
				this._transform = UnityEngine.Object.Instantiate<GameObject>(modelGameObject).transform;
				this.transform.name = ((!isHierarchyItem) ? this.id.ToString() : this.GUID.ToString("N"));
				this.transform.parent = LevelObjects.models;
				this.transform.position = newPoint;
				this.transform.rotation = newRotation;
				this.isDecal = this.transform.FindChild("Decal");
				if (this.asset.useScale)
				{
					this.transform.localScale = newScale;
				}
				if (this.asset.useWaterHeightTransparentSort)
				{
					this.transform.gameObject.AddComponent<WaterHeightTransparentSort>();
				}
				if (this.asset.shouldAddNightLightScript)
				{
					NightLight nightLight = this.transform.gameObject.AddComponent<NightLight>();
					Transform transform = this.transform.FindChild("Light");
					if (transform)
					{
						nightLight.target = transform.GetComponent<Light>();
					}
				}
				this.renderers = new List<Renderer>();
				Material material = null;
				AssetReference<MaterialPaletteAsset> reference = customMaterialOverride;
				if (!reference.isValid)
				{
					reference = this.asset.materialPalette;
				}
				if (reference.isValid)
				{
					MaterialPaletteAsset materialPaletteAsset = Assets.find<MaterialPaletteAsset>(reference);
					if (materialPaletteAsset != null)
					{
						int index;
						if (materialIndexOverride == -1)
						{
							UnityEngine.Random.State state = UnityEngine.Random.state;
							UnityEngine.Random.InitState((int)this.instanceID);
							index = UnityEngine.Random.Range(0, materialPaletteAsset.materials.Count);
							UnityEngine.Random.state = state;
						}
						else
						{
							index = Mathf.Clamp(materialIndexOverride, 0, materialPaletteAsset.materials.Count - 1);
						}
						material = Assets.load<Material>(materialPaletteAsset.materials[index]);
					}
				}
				GameObject skyboxGameObject = this.asset.skyboxGameObject;
				if (skyboxGameObject != null)
				{
					this._skybox = UnityEngine.Object.Instantiate<GameObject>(skyboxGameObject).transform;
					this.skybox.name = this.id.ToString() + "_Skybox";
					this.skybox.parent = LevelObjects.models;
					this.skybox.position = newPoint;
					this.skybox.rotation = newRotation;
					if (this.asset.useScale)
					{
						this.skybox.localScale = newScale;
					}
					if (this.isLandmarkQualityMet)
					{
						this.enableSkybox();
					}
					else
					{
						this.disableSkybox();
					}
					this.skybox.GetComponentsInChildren<Renderer>(true, this.renderers);
					for (int i = 0; i < this.renderers.Count; i++)
					{
						this.renderers[i].shadowCastingMode = ShadowCastingMode.Off;
						if (material != null)
						{
							this.renderers[i].sharedMaterial = material;
						}
					}
					this.renderers.Clear();
				}
				this.transform.GetComponentsInChildren<Renderer>(true, this.renderers);
				if (material != null)
				{
					for (int j = 0; j < this.renderers.Count; j++)
					{
						this.renderers[j].sharedMaterial = material;
					}
				}
				if (this.asset.isCollisionImportant && Provider.isServer && !Dedicator.isDedicated)
				{
					this.enableCollision();
				}
				else
				{
					this.disableCollision();
				}
				this.disableVisual();
			}
			if (this.transform != null)
			{
				if (this.isDecal && !Level.isEditor && this.asset.interactability == EObjectInteractability.NONE && this.asset.rubble == EObjectRubble.NONE)
				{
					Collider component = this.transform.GetComponent<Collider>();
					if (component != null)
					{
						UnityEngine.Object.Destroy(component);
					}
				}
				if (Level.isEditor)
				{
					if (isHierarchyItem)
					{
						Rigidbody component2 = this.transform.GetComponent<Rigidbody>();
						if (component2 != null)
						{
							UnityEngine.Object.Destroy(component2);
						}
					}
					else
					{
						Rigidbody rigidbody = this.transform.GetComponent<Rigidbody>();
						if (rigidbody == null)
						{
							rigidbody = this.transform.gameObject.AddComponent<Rigidbody>();
							rigidbody.useGravity = false;
							rigidbody.isKinematic = true;
						}
					}
				}
				else if (this.asset.interactability == EObjectInteractability.NONE && this.asset.rubble == EObjectRubble.NONE)
				{
					Rigidbody component3 = this.transform.GetComponent<Rigidbody>();
					if (component3 != null)
					{
						UnityEngine.Object.Destroy(component3);
					}
					if (this.asset.type == EObjectType.SMALL)
					{
						Collider component4 = this.transform.GetComponent<Collider>();
						if (component4 != null)
						{
							UnityEngine.Object.Destroy(component4);
						}
					}
				}
				if ((Level.isEditor || Provider.isServer) && this.asset.type != EObjectType.SMALL)
				{
					GameObject navGameObject = this.asset.navGameObject;
					if (navGameObject != null)
					{
						Transform transform2 = UnityEngine.Object.Instantiate<GameObject>(navGameObject).transform;
						transform2.name = "Nav";
						transform2.parent = this.transform;
						transform2.localPosition = Vector3.zero;
						transform2.localRotation = Quaternion.identity;
						transform2.localScale = Vector3.one;
						if (Level.isEditor)
						{
							Rigidbody rigidbody2 = transform2.GetComponent<Rigidbody>();
							if (rigidbody2 == null)
							{
								rigidbody2 = transform2.gameObject.AddComponent<Rigidbody>();
								rigidbody2.useGravity = false;
								rigidbody2.isKinematic = true;
							}
						}
						else
						{
							LevelObject.reuseableRigidbodyList.Clear();
							transform2.GetComponentsInChildren<Rigidbody>(LevelObject.reuseableRigidbodyList);
							foreach (Rigidbody obj in LevelObject.reuseableRigidbodyList)
							{
								UnityEngine.Object.Destroy(obj);
							}
						}
					}
				}
				if (Provider.isServer)
				{
					GameObject triggersGameObject = this.asset.triggersGameObject;
					if (triggersGameObject != null)
					{
						Transform transform3 = UnityEngine.Object.Instantiate<GameObject>(triggersGameObject).transform;
						transform3.name = "Triggers";
						transform3.parent = this.transform;
						transform3.localPosition = Vector3.zero;
						transform3.localRotation = Quaternion.identity;
						transform3.localScale = Vector3.one;
					}
				}
				if (this.asset.type != EObjectType.SMALL)
				{
					if (Level.isEditor)
					{
						Transform transform4 = this.transform.FindChild("Block");
						if (transform4 != null && this.transform.GetComponent<Collider>() == null)
						{
							BoxCollider boxCollider = (BoxCollider)transform4.GetComponent<Collider>();
							BoxCollider boxCollider2 = this.transform.gameObject.AddComponent<BoxCollider>();
							boxCollider2.center = boxCollider.center;
							boxCollider2.size = boxCollider.size;
						}
					}
					else if (Provider.isClient)
					{
						GameObject slotsGameObject = this.asset.slotsGameObject;
						if (slotsGameObject != null)
						{
							Transform transform5 = UnityEngine.Object.Instantiate<GameObject>(slotsGameObject).transform;
							transform5.name = "Slots";
							transform5.parent = this.transform;
							transform5.localPosition = Vector3.zero;
							transform5.localRotation = Quaternion.identity;
							transform5.localScale = Vector3.one;
							LevelObject.reuseableRigidbodyList.Clear();
							transform5.GetComponentsInChildren<Rigidbody>(LevelObject.reuseableRigidbodyList);
							foreach (Rigidbody obj2 in LevelObject.reuseableRigidbodyList)
							{
								UnityEngine.Object.Destroy(obj2);
							}
						}
					}
					if (this.asset.slotsGameObject != null)
					{
					}
				}
				if (this.asset.interactability != EObjectInteractability.NONE)
				{
					if (this.asset.interactability == EObjectInteractability.BINARY_STATE)
					{
						this._interactableObj = this.transform.gameObject.AddComponent<InteractableObjectBinaryState>();
					}
					else if (this.asset.interactability == EObjectInteractability.DROPPER)
					{
						this._interactableObj = this.transform.gameObject.AddComponent<InteractableObjectDropper>();
					}
					else if (this.asset.interactability == EObjectInteractability.NOTE)
					{
						this._interactableObj = this.transform.gameObject.AddComponent<InteractableObjectNote>();
					}
					else if (this.asset.interactability == EObjectInteractability.WATER || this.asset.interactability == EObjectInteractability.FUEL)
					{
						this._interactableObj = this.transform.gameObject.AddComponent<InteractableObjectResource>();
					}
					else if (this.asset.interactability == EObjectInteractability.NPC)
					{
						this._interactableObj = this.transform.gameObject.AddComponent<InteractableObjectNPC>();
					}
					else if (this.asset.interactability == EObjectInteractability.QUEST)
					{
						this._interactableObj = this.transform.gameObject.AddComponent<InteractableObjectQuest>();
					}
					if (this.interactable != null)
					{
						this.interactable.updateState(this.asset, this.state);
					}
				}
				if (this.asset.rubble != EObjectRubble.NONE)
				{
					if (this.asset.rubble == EObjectRubble.DESTROY)
					{
						this._rubble = this.transform.gameObject.AddComponent<InteractableObjectRubble>();
					}
					if (this.rubble != null)
					{
						this.rubble.updateState(this.asset, this.state);
					}
					if (this.asset.rubbleEditor == EObjectRubbleEditor.DEAD && Level.isEditor)
					{
						Transform transform6 = this.transform.FindChild("Editor");
						if (transform6 != null)
						{
							transform6.gameObject.SetActive(true);
						}
					}
				}
				if (this.asset.conditions != null && this.asset.conditions.Length > 0 && !Level.isEditor && !Dedicator.isDedicated)
				{
					this.areConditionsMet = false;
					Player.onPlayerCreated = (PlayerCreated)Delegate.Combine(Player.onPlayerCreated, new PlayerCreated(this.onPlayerCreated));
				}
				if (this.asset.foliage.isValid)
				{
					FoliageSurfaceComponent foliageSurfaceComponent = this.transform.gameObject.AddComponent<FoliageSurfaceComponent>();
					foliageSurfaceComponent.foliage = this.asset.foliage;
					foliageSurfaceComponent.surfaceCollider = this.transform.gameObject.GetComponent<Collider>();
				}
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x000DA448 File Offset: 0x000D8848
		public Transform transform
		{
			get
			{
				return this._transform;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x000DA450 File Offset: 0x000D8850
		public Transform skybox
		{
			get
			{
				return this._skybox;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002567 RID: 9575 RVA: 0x000DA458 File Offset: 0x000D8858
		public ushort id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002568 RID: 9576 RVA: 0x000DA460 File Offset: 0x000D8860
		public string name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002569 RID: 9577 RVA: 0x000DA468 File Offset: 0x000D8868
		public Guid GUID
		{
			get
			{
				return this._GUID;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600256A RID: 9578 RVA: 0x000DA470 File Offset: 0x000D8870
		public uint instanceID
		{
			get
			{
				return this._instanceID;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x0600256B RID: 9579 RVA: 0x000DA478 File Offset: 0x000D8878
		public ObjectAsset asset
		{
			get
			{
				return this._asset;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x0600256C RID: 9580 RVA: 0x000DA480 File Offset: 0x000D8880
		public InteractableObject interactable
		{
			get
			{
				return this._interactableObj;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x000DA488 File Offset: 0x000D8888
		public InteractableObjectRubble rubble
		{
			get
			{
				return this._rubble;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x0600256E RID: 9582 RVA: 0x000DA490 File Offset: 0x000D8890
		// (set) Token: 0x0600256F RID: 9583 RVA: 0x000DA498 File Offset: 0x000D8898
		public ELevelObjectPlacementOrigin placementOrigin { get; protected set; }

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002570 RID: 9584 RVA: 0x000DA4A1 File Offset: 0x000D88A1
		// (set) Token: 0x06002571 RID: 9585 RVA: 0x000DA4A9 File Offset: 0x000D88A9
		public bool isCollisionEnabled { get; private set; }

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06002572 RID: 9586 RVA: 0x000DA4B2 File Offset: 0x000D88B2
		// (set) Token: 0x06002573 RID: 9587 RVA: 0x000DA4BA File Offset: 0x000D88BA
		public bool isVisualEnabled { get; private set; }

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x000DA4C3 File Offset: 0x000D88C3
		// (set) Token: 0x06002575 RID: 9589 RVA: 0x000DA4CB File Offset: 0x000D88CB
		public bool isSkyboxEnabled { get; private set; }

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002576 RID: 9590 RVA: 0x000DA4D4 File Offset: 0x000D88D4
		public bool isLandmarkQualityMet
		{
			get
			{
				return this.asset != null && !Dedicator.isDedicated && GraphicsSettings.landmarkQuality >= this.asset.landmarkQuality;
			}
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000DA504 File Offset: 0x000D8904
		public void enableCollision()
		{
			this.isCollisionEnabled = true;
			if (Dedicator.isDedicated)
			{
				return;
			}
			if (this.transform != null && this.areConditionsMet)
			{
				this.transform.gameObject.SetActive(true);
			}
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000DA550 File Offset: 0x000D8950
		public void enableVisual()
		{
			this.isVisualEnabled = true;
			if (Dedicator.isDedicated)
			{
				return;
			}
			if (this.isDecal)
			{
				return;
			}
			if (this.renderers != null && this.renderers.Count > 0)
			{
				for (int i = 0; i < this.renderers.Count; i++)
				{
					if (this.renderers[i] != null)
					{
						this.renderers[i].enabled = true;
					}
				}
			}
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000DA5DC File Offset: 0x000D89DC
		public void enableSkybox()
		{
			this.isSkyboxEnabled = true;
			if (Dedicator.isDedicated)
			{
				return;
			}
			if (this.skybox != null && this.areConditionsMet)
			{
				this.skybox.gameObject.SetActive(true);
			}
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000DA628 File Offset: 0x000D8A28
		public void disableCollision()
		{
			this.isCollisionEnabled = false;
			if (Dedicator.isDedicated)
			{
				return;
			}
			if (this.asset != null && this.asset.isCollisionImportant && Provider.isServer && !Dedicator.isDedicated)
			{
				return;
			}
			if (this.transform != null)
			{
				this.transform.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000DA69C File Offset: 0x000D8A9C
		public void disableVisual()
		{
			this.isVisualEnabled = false;
			if (Dedicator.isDedicated)
			{
				return;
			}
			if (this.isDecal)
			{
				return;
			}
			if (this.renderers != null && this.renderers.Count > 0)
			{
				for (int i = 0; i < this.renderers.Count; i++)
				{
					if (this.renderers[i] != null)
					{
						this.renderers[i].enabled = false;
					}
				}
			}
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000DA728 File Offset: 0x000D8B28
		public void disableSkybox()
		{
			this.isSkyboxEnabled = false;
			if (Dedicator.isDedicated)
			{
				return;
			}
			if (this.skybox != null)
			{
				this.skybox.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x000DA760 File Offset: 0x000D8B60
		public void destroy()
		{
			if (this.transform)
			{
				UnityEngine.Object.Destroy(this.transform.gameObject);
			}
			if (this.skybox)
			{
				UnityEngine.Object.Destroy(this.skybox.gameObject);
			}
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000DA7B0 File Offset: 0x000D8BB0
		private void updateConditions()
		{
			if (this.asset == null)
			{
				return;
			}
			bool flag = this.asset.areConditionsMet(Player.player);
			if (this.areConditionsMet != flag || !this.haveConditionsBeenChecked)
			{
				this.areConditionsMet = flag;
				this.haveConditionsBeenChecked = true;
				if (this.areConditionsMet)
				{
					if (this.isCollisionEnabled && this.transform != null)
					{
						this.transform.gameObject.SetActive(true);
					}
					if (this.skybox != null)
					{
						this.skybox.gameObject.SetActive(this.isSkyboxEnabled);
					}
				}
				else
				{
					if (this.transform != null)
					{
						this.transform.gameObject.SetActive(false);
					}
					if (this.skybox != null)
					{
						this.skybox.gameObject.SetActive(false);
					}
				}
			}
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000DA8A7 File Offset: 0x000D8CA7
		private void onExternalConditionsUpdated()
		{
			this.updateConditions();
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000DA8AF File Offset: 0x000D8CAF
		private void onFlagsUpdated()
		{
			this.updateConditions();
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000DA8B8 File Offset: 0x000D8CB8
		private void onFlagUpdated(ushort id)
		{
			if (this.asset == null)
			{
				return;
			}
			for (int i = 0; i < this.asset.conditions.Length; i++)
			{
				NPCFlagCondition npcflagCondition = this.asset.conditions[i] as NPCFlagCondition;
				if (npcflagCondition != null && npcflagCondition.id == id)
				{
					this.updateConditions();
					return;
				}
			}
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000DA91C File Offset: 0x000D8D1C
		private void onPlayerCreated(Player player)
		{
			if (player.channel.isOwner)
			{
				Player.onPlayerCreated = (PlayerCreated)Delegate.Remove(Player.onPlayerCreated, new PlayerCreated(this.onPlayerCreated));
				PlayerQuests quests = Player.player.quests;
				quests.onExternalConditionsUpdated = (ExternalConditionsUpdated)Delegate.Combine(quests.onExternalConditionsUpdated, new ExternalConditionsUpdated(this.onExternalConditionsUpdated));
				PlayerQuests quests2 = Player.player.quests;
				quests2.onFlagsUpdated = (FlagsUpdated)Delegate.Combine(quests2.onFlagsUpdated, new FlagsUpdated(this.onFlagsUpdated));
				PlayerQuests quests3 = Player.player.quests;
				quests3.onFlagUpdated = (FlagUpdated)Delegate.Combine(quests3.onFlagUpdated, new FlagUpdated(this.onFlagUpdated));
				this.updateConditions();
			}
		}

		// Token: 0x0400175E RID: 5982
		private static List<Rigidbody> reuseableRigidbodyList = new List<Rigidbody>();

		// Token: 0x0400175F RID: 5983
		public bool isSpeciallyCulled;

		// Token: 0x04001760 RID: 5984
		private bool isDecal;

		// Token: 0x04001761 RID: 5985
		private Transform _transform;

		// Token: 0x04001762 RID: 5986
		private Transform _skybox;

		// Token: 0x04001763 RID: 5987
		private List<Renderer> renderers;

		// Token: 0x04001764 RID: 5988
		private ushort _id;

		// Token: 0x04001765 RID: 5989
		private string _name;

		// Token: 0x04001766 RID: 5990
		private Guid _GUID;

		// Token: 0x04001767 RID: 5991
		private uint _instanceID;

		// Token: 0x04001768 RID: 5992
		public byte[] state;

		// Token: 0x04001769 RID: 5993
		private ObjectAsset _asset;

		// Token: 0x0400176A RID: 5994
		private InteractableObject _interactableObj;

		// Token: 0x0400176B RID: 5995
		private InteractableObjectRubble _rubble;

		// Token: 0x04001770 RID: 6000
		private bool areConditionsMet;

		// Token: 0x04001771 RID: 6001
		private bool haveConditionsBeenChecked;
	}
}
