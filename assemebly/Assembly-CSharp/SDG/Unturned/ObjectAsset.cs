using System;
using System.Text;
using SDG.Framework.Debug;
using SDG.Framework.Foliage;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000419 RID: 1049
	public class ObjectAsset : Asset
	{
		// Token: 0x06001C46 RID: 7238 RVA: 0x000999C8 File Offset: 0x00097DC8
		public ObjectAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (id < 2000 && !bundle.hasResource && !data.has("Bypass_ID_Limit"))
			{
				throw new NotSupportedException("ID < 2000");
			}
			this._objectName = localization.format("Name");
			this.type = (EObjectType)Enum.Parse(typeof(EObjectType), data.readString("Type"), true);
			if (this.type == EObjectType.NPC)
			{
				if (Dedicator.isDedicated)
				{
					this._modelGameObject = (GameObject)Resources.Load("Characters/NPC_Server");
				}
				else
				{
					this._modelGameObject = (GameObject)Resources.Load("Characters/NPC_Client");
				}
				this.useScale = true;
				this.interactability = EObjectInteractability.NPC;
			}
			else if (this.type == EObjectType.DECAL)
			{
				float num = data.readSingle("Decal_X");
				float num2 = data.readSingle("Decal_Y");
				float num3 = 1f;
				if (data.has("Decal_LOD_Bias"))
				{
					num3 = data.readSingle("Decal_LOD_Bias");
				}
				Texture2D texture2D = (Texture2D)bundle.load("Decal");
				if (texture2D == null)
				{
					Assets.errors.Add(this.objectName + " is missing a decal texture. It will show as pure white without one.");
				}
				bool flag = data.has("Decal_Alpha");
				this._modelGameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>((!flag) ? "Materials/Decal_Template_Masked" : "Materials/Decal_Template_Alpha"));
				this._modelGameObject.hideFlags = HideFlags.HideAndDontSave;
				UnityEngine.Object.DontDestroyOnLoad(this._modelGameObject);
				BoxCollider component = this.modelGameObject.GetComponent<BoxCollider>();
				component.size = new Vector3(num2, num, 1f);
				Decal component2 = this.modelGameObject.transform.FindChild("Decal").GetComponent<Decal>();
				Material material = UnityEngine.Object.Instantiate<Material>(component2.material);
				material.name = "Decal_Deferred";
				material.hideFlags = HideFlags.DontSave;
				material.SetTexture("_MainTex", texture2D);
				component2.material = material;
				component2.lodBias = num3;
				component2.transform.localScale = new Vector3(num, num2, 1f);
				MeshRenderer component3 = this.modelGameObject.transform.FindChild("Mesh").GetComponent<MeshRenderer>();
				Material material2 = UnityEngine.Object.Instantiate<Material>(component3.sharedMaterial);
				material2.name = "Decal_Forward";
				material2.hideFlags = HideFlags.DontSave;
				material2.SetTexture("_MainTex", texture2D);
				component3.sharedMaterial = material2;
				component3.transform.localScale = new Vector3(num2, num, 1f);
				this.useScale = true;
			}
			else
			{
				if (Dedicator.isDedicated)
				{
					this._modelGameObject = (GameObject)bundle.load("Clip");
					if (this.modelGameObject == null && this.type != EObjectType.SMALL)
					{
						Assets.errors.Add(this.objectName + " is missing collision data. Highly recommended to fix.");
					}
				}
				else
				{
					this._modelGameObject = (GameObject)bundle.load("Object");
					if (this.modelGameObject == null)
					{
						throw new NotSupportedException("Missing object gameobject");
					}
					this._skyboxGameObject = (GameObject)bundle.load("Skybox");
				}
				if (this.modelGameObject != null)
				{
					if (Mathf.Abs(this.modelGameObject.transform.localScale.x - 1f) > 0.01f || Mathf.Abs(this.modelGameObject.transform.localScale.y - 1f) > 0.01f || Mathf.Abs(this.modelGameObject.transform.localScale.z - 1f) > 0.01f)
					{
						this.useScale = false;
						Assets.errors.Add(this.objectName + " should have a scale of one.");
					}
					else
					{
						this.useScale = true;
					}
					Transform transform = this.modelGameObject.transform.FindChild("Block");
					if (transform != null && transform.GetComponent<Collider>() != null && transform.GetComponent<Collider>().sharedMaterial == null)
					{
						Assets.errors.Add(this.objectName + " has a clip object but no physics material.");
					}
					Transform transform2 = this.modelGameObject.transform.FindChild("Model_0");
					if (this.type == EObjectType.SMALL)
					{
						if (!this.modelGameObject.CompareTag("Small"))
						{
							Assets.errors.Add(this.objectName + " is set up as small, but is not tagged as small.");
						}
						if (this.modelGameObject.layer != LayerMasks.SMALL)
						{
							Assets.errors.Add(this.objectName + " is set up as small, but is not layered as small.");
						}
						if (transform2 != null)
						{
							if (!transform2.CompareTag("Small"))
							{
								Assets.errors.Add(this.objectName + " is set up as small, but is not tagged as small.");
							}
							if (transform2.gameObject.layer != LayerMasks.SMALL)
							{
								Assets.errors.Add(this.objectName + " is set up as small, but is not layered as small.");
							}
						}
					}
					else if (this.type == EObjectType.MEDIUM)
					{
						if (!this.modelGameObject.CompareTag("Medium"))
						{
							Assets.errors.Add(this.objectName + " is set up as medium, but is not tagged as medium.");
						}
						if (this.modelGameObject.layer != LayerMasks.MEDIUM)
						{
							Assets.errors.Add(this.objectName + " is set up as medium, but is not layered as medium.");
						}
						if (transform2 != null)
						{
							if (!transform2.CompareTag("Medium"))
							{
								Assets.errors.Add(this.objectName + " is set up as medium, but is not tagged as medium.");
							}
							if (transform2.gameObject.layer != LayerMasks.MEDIUM)
							{
								Assets.errors.Add(this.objectName + " is set up as medium, but is not layered as medium.");
							}
						}
					}
					else if (this.type == EObjectType.LARGE)
					{
						if (!this.modelGameObject.CompareTag("Large"))
						{
							Assets.errors.Add(this.objectName + " is set up as large, but is not tagged as large.");
						}
						if (this.modelGameObject.layer != LayerMasks.LARGE)
						{
							Assets.errors.Add(this.objectName + " is set up as large, but is not layered as large.");
						}
						if (transform2 != null)
						{
							if (!transform2.CompareTag("Large"))
							{
								Assets.errors.Add(this.objectName + " is set up as large, but is not tagged as large.");
							}
							if (transform2.gameObject.layer != LayerMasks.LARGE)
							{
								Assets.errors.Add(this.objectName + " is set up as large, but is not layered as large.");
							}
						}
					}
				}
				this._navGameObject = (GameObject)bundle.load("Nav");
				if (this.navGameObject == null && this.type == EObjectType.LARGE)
				{
					Assets.errors.Add(this.objectName + " is missing navigation data. Highly recommended to fix.");
				}
				if (this.navGameObject != null)
				{
					if (!this.navGameObject.CompareTag("Navmesh"))
					{
						Assets.errors.Add(this.objectName + " is set up as navmesh, but is not tagged as navmesh.");
					}
					if (this.navGameObject.layer != LayerMasks.NAVMESH)
					{
						Assets.errors.Add(this.objectName + " is set up as navmesh, but is not layered as navmesh.");
					}
				}
				this._slotsGameObject = (GameObject)bundle.load("Slots");
				this._triggersGameObject = (GameObject)bundle.load("Triggers");
				this.isSnowshoe = data.has("Snowshoe");
				if (data.has("Chart"))
				{
					this.chart = (EObjectChart)Enum.Parse(typeof(EObjectChart), data.readString("Chart"), true);
				}
				else
				{
					this.chart = EObjectChart.NONE;
				}
				this.isFuel = data.has("Fuel");
				this.isRefill = data.has("Refill");
				this.isSoft = data.has("Soft");
				this.isCollisionImportant = data.has("Collision_Important");
				if (this.isFuel || this.isRefill)
				{
					Assets.errors.Add(this.objectName + " is using the legacy fuel/water system.");
				}
				if (data.has("LOD"))
				{
					this.lod = (EObjectLOD)Enum.Parse(typeof(EObjectLOD), data.readString("LOD"), true);
					this.lodBias = data.readSingle("LOD_Bias");
					if (this.lodBias < 0.01f)
					{
						this.lodBias = 1f;
					}
					this.lodCenter = data.readVector3("LOD_Center");
					this.lodSize = data.readVector3("LOD_Size");
				}
				if (data.has("Interactability"))
				{
					this.interactability = (EObjectInteractability)Enum.Parse(typeof(EObjectInteractability), data.readString("Interactability"), true);
					this.interactabilityRemote = data.has("Interactability_Remote");
					this.interactabilityDelay = data.readSingle("Interactability_Delay");
					this.interactabilityReset = data.readSingle("Interactability_Reset");
					if (data.has("Interactability_Hint"))
					{
						this.interactabilityHint = (EObjectInteractabilityHint)Enum.Parse(typeof(EObjectInteractabilityHint), data.readString("Interactability_Hint"), true);
					}
					this.interactabilityEmission = data.has("Interactability_Emission");
					if (this.interactability == EObjectInteractability.NOTE)
					{
						ushort num4 = data.readUInt16("Interactability_Text_Lines");
						StringBuilder stringBuilder = new StringBuilder();
						for (ushort num5 = 0; num5 < num4; num5 += 1)
						{
							string value = localization.format("Interactability_Text_Line_" + num5);
							stringBuilder.AppendLine(value);
						}
						this.interactabilityText = stringBuilder.ToString();
					}
					else
					{
						this.interactabilityText = localization.read("Interact");
					}
					if (data.has("Interactability_Power"))
					{
						this.interactabilityPower = (EObjectInteractabilityPower)Enum.Parse(typeof(EObjectInteractabilityPower), data.readString("Interactability_Power"), true);
					}
					else
					{
						this.interactabilityPower = EObjectInteractabilityPower.NONE;
					}
					if (data.has("Interactability_Editor"))
					{
						this.interactabilityEditor = (EObjectInteractabilityEditor)Enum.Parse(typeof(EObjectInteractabilityEditor), data.readString("Interactability_Editor"), true);
					}
					else
					{
						this.interactabilityEditor = EObjectInteractabilityEditor.NONE;
					}
					if (data.has("Interactability_Nav"))
					{
						this.interactabilityNav = (EObjectInteractabilityNav)Enum.Parse(typeof(EObjectInteractabilityNav), data.readString("Interactability_Nav"), true);
					}
					else
					{
						this.interactabilityNav = EObjectInteractabilityNav.NONE;
					}
					this.interactabilityDrops = new ushort[(int)data.readByte("Interactability_Drops")];
					byte b = 0;
					while ((int)b < this.interactabilityDrops.Length)
					{
						this.interactabilityDrops[(int)b] = data.readUInt16("Interactability_Drop_" + b);
						b += 1;
					}
					this.interactabilityRewardID = data.readUInt16("Interactability_Reward_ID");
					this.interactabilityEffect = data.readUInt16("Interactability_Effect");
					this.interactabilityConditions = new INPCCondition[(int)data.readByte("Interactability_Conditions")];
					NPCTool.readConditions(data, localization, "Interactability_Condition_", this.interactabilityConditions, "object " + id);
					this.interactabilityRewards = new INPCReward[(int)data.readByte("Interactability_Rewards")];
					NPCTool.readRewards(data, localization, "Interactability_Reward_", this.interactabilityRewards, "object " + id);
					this.interactabilityResource = data.readUInt16("Interactability_Resource");
					this.interactabilityResourceState = BitConverter.GetBytes(this.interactabilityResource);
				}
				else
				{
					this.interactability = EObjectInteractability.NONE;
					this.interactabilityPower = EObjectInteractabilityPower.NONE;
					this.interactabilityEditor = EObjectInteractabilityEditor.NONE;
				}
				if (this.interactability == EObjectInteractability.RUBBLE)
				{
					this.rubble = EObjectRubble.DESTROY;
					this.rubbleReset = data.readSingle("Interactability_Reset");
					this.rubbleHealth = data.readUInt16("Interactability_Health");
					this.rubbleEffect = data.readUInt16("Interactability_Effect");
					this.rubbleFinale = data.readUInt16("Interactability_Finale");
					this.rubbleRewardID = data.readUInt16("Interactability_Reward_ID");
					this.rubbleRewardXP = data.readUInt32("Interactability_Reward_XP");
					this.rubbleIsVulnerable = !data.has("Interactability_Invulnerable");
					this.rubbleProofExplosion = data.has("Interactability_Proof_Explosion");
				}
				else if (data.has("Rubble"))
				{
					this.rubble = (EObjectRubble)Enum.Parse(typeof(EObjectRubble), data.readString("Rubble"), true);
					this.rubbleReset = data.readSingle("Rubble_Reset");
					this.rubbleHealth = data.readUInt16("Rubble_Health");
					this.rubbleEffect = data.readUInt16("Rubble_Effect");
					this.rubbleFinale = data.readUInt16("Rubble_Finale");
					this.rubbleRewardID = data.readUInt16("Rubble_Reward_ID");
					this.rubbleRewardXP = data.readUInt32("Rubble_Reward_XP");
					this.rubbleIsVulnerable = !data.has("Rubble_Invulnerable");
					this.rubbleProofExplosion = data.has("Rubble_Proof_Explosion");
					if (data.has("Rubble_Editor"))
					{
						this.rubbleEditor = (EObjectRubbleEditor)Enum.Parse(typeof(EObjectRubbleEditor), data.readString("Rubble_Editor"), true);
					}
					else
					{
						this.rubbleEditor = EObjectRubbleEditor.ALIVE;
					}
				}
				if (data.has("Foliage"))
				{
					this.foliage = new AssetReference<FoliageInfoCollectionAsset>(new Guid(data.readString("Foliage")));
				}
				this.useWaterHeightTransparentSort = data.has("Use_Water_Height_Transparent_Sort");
				this.shouldAddNightLightScript = data.has("Add_Night_Light_Script");
				if (data.has("Material_Palette"))
				{
					this.materialPalette = new AssetReference<MaterialPaletteAsset>(data.readGUID("Material_Palette"));
				}
				if (data.has("Landmark_Quality"))
				{
					this.landmarkQuality = (EGraphicQuality)Enum.Parse(typeof(EGraphicQuality), data.readString("Landmark_Quality"), true);
				}
				else
				{
					this.landmarkQuality = EGraphicQuality.LOW;
				}
			}
			this.conditions = new INPCCondition[(int)data.readByte("Conditions")];
			NPCTool.readConditions(data, localization, "Condition_", this.conditions, "object " + id);
			bundle.unload();
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x0009A8C4 File Offset: 0x00098CC4
		public string objectName
		{
			get
			{
				return this._objectName;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x0009A8CC File Offset: 0x00098CCC
		public GameObject modelGameObject
		{
			get
			{
				return this._modelGameObject;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x0009A8D4 File Offset: 0x00098CD4
		public GameObject skyboxGameObject
		{
			get
			{
				return this._skyboxGameObject;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001C4A RID: 7242 RVA: 0x0009A8DC File Offset: 0x00098CDC
		public GameObject navGameObject
		{
			get
			{
				return this._navGameObject;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001C4B RID: 7243 RVA: 0x0009A8E4 File Offset: 0x00098CE4
		public GameObject slotsGameObject
		{
			get
			{
				return this._slotsGameObject;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001C4C RID: 7244 RVA: 0x0009A8EC File Offset: 0x00098CEC
		public GameObject triggersGameObject
		{
			get
			{
				return this._triggersGameObject;
			}
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x0009A8F4 File Offset: 0x00098CF4
		public virtual byte[] getState()
		{
			byte[] array;
			if (this.interactability == EObjectInteractability.BINARY_STATE)
			{
				array = new byte[]
				{
					(!Level.isEditor || this.interactabilityEditor == EObjectInteractabilityEditor.NONE) ? 0 : 1
				};
			}
			else if (this.interactability == EObjectInteractability.WATER || this.interactability == EObjectInteractability.FUEL)
			{
				array = new byte[]
				{
					this.interactabilityResourceState[0],
					this.interactabilityResourceState[1]
				};
			}
			else
			{
				array = null;
			}
			if (this.rubble == EObjectRubble.DESTROY)
			{
				if (array != null)
				{
					byte[] array2 = new byte[array.Length + 1];
					Array.Copy(array, array2, array.Length);
					array = array2;
				}
				else
				{
					array = new byte[1];
				}
				array[array.Length - 1] = ((!Level.isEditor || this.rubbleEditor != EObjectRubbleEditor.DEAD) ? byte.MaxValue : 0);
			}
			return array;
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x0009A9D3 File Offset: 0x00098DD3
		public override EAssetType assetCategory
		{
			get
			{
				return EAssetType.OBJECT;
			}
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x0009A9D8 File Offset: 0x00098DD8
		public bool areConditionsMet(Player player)
		{
			if (this.conditions != null)
			{
				for (int i = 0; i < this.conditions.Length; i++)
				{
					if (!this.conditions[i].isConditionMet(player))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x0009AA20 File Offset: 0x00098E20
		public bool areInteractabilityConditionsMet(Player player)
		{
			if (this.interactabilityConditions != null)
			{
				for (int i = 0; i < this.interactabilityConditions.Length; i++)
				{
					if (!this.interactabilityConditions[i].isConditionMet(player))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x0009AA68 File Offset: 0x00098E68
		public void applyInteractabilityConditions(Player player, bool shouldSend)
		{
			if (this.interactabilityConditions != null)
			{
				for (int i = 0; i < this.interactabilityConditions.Length; i++)
				{
					this.interactabilityConditions[i].applyCondition(player, shouldSend);
				}
			}
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x0009AAA8 File Offset: 0x00098EA8
		public void grantInteractabilityRewards(Player player, bool shouldSend)
		{
			if (this.interactabilityRewards != null)
			{
				for (int i = 0; i < this.interactabilityRewards.Length; i++)
				{
					this.interactabilityRewards[i].grantReward(player, shouldSend);
				}
			}
		}

		// Token: 0x04001085 RID: 4229
		protected string _objectName;

		// Token: 0x04001086 RID: 4230
		[Inspectable("#SDG::Asset.Object.Type.Name", null)]
		public EObjectType type;

		// Token: 0x04001087 RID: 4231
		protected GameObject _modelGameObject;

		// Token: 0x04001088 RID: 4232
		protected GameObject _skyboxGameObject;

		// Token: 0x04001089 RID: 4233
		private GameObject _navGameObject;

		// Token: 0x0400108A RID: 4234
		private GameObject _slotsGameObject;

		// Token: 0x0400108B RID: 4235
		private GameObject _triggersGameObject;

		// Token: 0x0400108C RID: 4236
		[Inspectable("#SDG::Asset.Object.Is_Snowshoe.Name", null)]
		public bool isSnowshoe;

		// Token: 0x0400108D RID: 4237
		[Inspectable("#SDG::Asset.Object.Chart.Name", null)]
		public EObjectChart chart;

		// Token: 0x0400108E RID: 4238
		[Inspectable("#SDG::Asset.Object.Is_Fuel.Name", null)]
		public bool isFuel;

		// Token: 0x0400108F RID: 4239
		[Inspectable("#SDG::Asset.Object.Is_Refill.Name", null)]
		public bool isRefill;

		// Token: 0x04001090 RID: 4240
		[Inspectable("#SDG::Asset.Object.Is_Soft.Name", null)]
		public bool isSoft;

		// Token: 0x04001091 RID: 4241
		[Inspectable("#SDG::Asset.Object.Is_Collision_Important.Name", null)]
		public bool isCollisionImportant;

		// Token: 0x04001092 RID: 4242
		[Inspectable("#SDG::Asset.Object.Use_Scale.Name", null)]
		public bool useScale;

		// Token: 0x04001093 RID: 4243
		[Inspectable("#SDG::Asset.Object.LOD.Name", null)]
		public EObjectLOD lod;

		// Token: 0x04001094 RID: 4244
		[Inspectable("#SDG::Asset.Object.LOD_Bias.Name", null)]
		public float lodBias;

		// Token: 0x04001095 RID: 4245
		[Inspectable("#SDG::Asset.Object.LOD_Center.Name", null)]
		public Vector3 lodCenter;

		// Token: 0x04001096 RID: 4246
		[Inspectable("#SDG::Asset.Object.LOD_Size.Name", null)]
		public Vector3 lodSize;

		// Token: 0x04001097 RID: 4247
		[Inspectable("#SDG::Asset.Object.Conditions.Name", null)]
		public INPCCondition[] conditions;

		// Token: 0x04001098 RID: 4248
		[Inspectable("#SDG::Asset.Object.Interactability.Name", null)]
		public EObjectInteractability interactability;

		// Token: 0x04001099 RID: 4249
		[Inspectable("#SDG::Asset.Object.Interactability_Remote.Name", null)]
		public bool interactabilityRemote;

		// Token: 0x0400109A RID: 4250
		[Inspectable("#SDG::Asset.Object.Interactability_Delay.Name", null)]
		public float interactabilityDelay;

		// Token: 0x0400109B RID: 4251
		[Inspectable("#SDG::Asset.Object.Interactability_Emission.Name", null)]
		public bool interactabilityEmission;

		// Token: 0x0400109C RID: 4252
		[Inspectable("#SDG::Asset.Object.Interactability_Hint.Name", null)]
		public EObjectInteractabilityHint interactabilityHint;

		// Token: 0x0400109D RID: 4253
		[Inspectable("#SDG::Asset.Object.Interactability_Text.Name", null)]
		public string interactabilityText;

		// Token: 0x0400109E RID: 4254
		[Inspectable("#SDG::Asset.Object.Interactability_Power.Name", null)]
		public EObjectInteractabilityPower interactabilityPower;

		// Token: 0x0400109F RID: 4255
		[Inspectable("#SDG::Asset.Object.Interactability_Editor.Name", null)]
		public EObjectInteractabilityEditor interactabilityEditor;

		// Token: 0x040010A0 RID: 4256
		[Inspectable("#SDG::Asset.Object.Interactability_Nav.Name", null)]
		public EObjectInteractabilityNav interactabilityNav;

		// Token: 0x040010A1 RID: 4257
		[Inspectable("#SDG::Asset.Object.Interactability_Reset.Name", null)]
		public float interactabilityReset;

		// Token: 0x040010A2 RID: 4258
		[Inspectable("#SDG::Asset.Object.Interactability_Resource.Name", null)]
		public ushort interactabilityResource;

		// Token: 0x040010A3 RID: 4259
		private byte[] interactabilityResourceState;

		// Token: 0x040010A4 RID: 4260
		[Inspectable("#SDG::Asset.Object.Interactability_Drops.Name", null)]
		public ushort[] interactabilityDrops;

		// Token: 0x040010A5 RID: 4261
		[Inspectable("#SDG::Asset.Object.Interactability_Reward_ID.Name", null)]
		public ushort interactabilityRewardID;

		// Token: 0x040010A6 RID: 4262
		[Inspectable("#SDG::Asset.Object.Interactability_Effect.Name", null)]
		public ushort interactabilityEffect;

		// Token: 0x040010A7 RID: 4263
		[Inspectable("#SDG::Asset.Object.Interactability_Conditions.Name", null)]
		public INPCCondition[] interactabilityConditions;

		// Token: 0x040010A8 RID: 4264
		[Inspectable("#SDG::Asset.Object.Interactability_Rewards.Name", null)]
		public INPCReward[] interactabilityRewards;

		// Token: 0x040010A9 RID: 4265
		[Inspectable("#SDG::Asset.Object.Rubble.Name", null)]
		public EObjectRubble rubble;

		// Token: 0x040010AA RID: 4266
		[Inspectable("#SDG::Asset.Object.Rubble_Reset.Name", null)]
		public float rubbleReset;

		// Token: 0x040010AB RID: 4267
		[Inspectable("#SDG::Asset.Object.Rubble_Health.Name", null)]
		public ushort rubbleHealth;

		// Token: 0x040010AC RID: 4268
		[Inspectable("#SDG::Asset.Object.Rubble_Effect.Name", null)]
		public ushort rubbleEffect;

		// Token: 0x040010AD RID: 4269
		[Inspectable("#SDG::Asset.Object.Rubble_Finale.Name", null)]
		public ushort rubbleFinale;

		// Token: 0x040010AE RID: 4270
		[Inspectable("#SDG::Asset.Object.Rubble_Editor.Name", null)]
		public EObjectRubbleEditor rubbleEditor;

		// Token: 0x040010AF RID: 4271
		[Inspectable("#SDG::Asset.Object.Rubble_Reward_ID.Name", null)]
		public ushort rubbleRewardID;

		// Token: 0x040010B0 RID: 4272
		[Inspectable("#SDG::Asset.Object.Rubble_Reward_XP.Name", null)]
		public uint rubbleRewardXP;

		// Token: 0x040010B1 RID: 4273
		[Inspectable("#SDG::Asset.Object.Rubble_Vulnerable.Name", null)]
		public bool rubbleIsVulnerable;

		// Token: 0x040010B2 RID: 4274
		[Inspectable("#SDG::Asset.Object.Rubble_Proof_Explosion.Name", null)]
		public bool rubbleProofExplosion;

		// Token: 0x040010B3 RID: 4275
		[Inspectable("#SDG::Asset.Object.Foliage.Name", null)]
		public AssetReference<FoliageInfoCollectionAsset> foliage;

		// Token: 0x040010B4 RID: 4276
		[Inspectable("#SDG::Asset.Object.Use_Water_Height_Transparent_Sort.Name", null)]
		public bool useWaterHeightTransparentSort;

		// Token: 0x040010B5 RID: 4277
		[Inspectable("#SDG::Asset.Object.Material_Palettte.Name", null)]
		public AssetReference<MaterialPaletteAsset> materialPalette;

		// Token: 0x040010B6 RID: 4278
		[Inspectable("#SDG::Asset.Object.Landmark_Quality.Name", null)]
		public EGraphicQuality landmarkQuality;

		// Token: 0x040010B7 RID: 4279
		public bool shouldAddNightLightScript;
	}
}
