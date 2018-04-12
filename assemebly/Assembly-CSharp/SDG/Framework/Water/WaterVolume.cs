using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.IO.FormattedFiles;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Water
{
	// Token: 0x02000315 RID: 789
	public class WaterVolume : DevkitHierarchyVolume, IDevkitHierarchySpawnable
	{
		// Token: 0x06001671 RID: 5745 RVA: 0x00085075 File Offset: 0x00083475
		public WaterVolume()
		{
			this._isSurfaceVisible = true;
			this.waterType = ERefillWaterType.SALTY;
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x0008508B File Offset: 0x0008348B
		// (set) Token: 0x06001673 RID: 5747 RVA: 0x00085093 File Offset: 0x00083493
		public GameObject waterPlane { get; protected set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x0008509C File Offset: 0x0008349C
		// (set) Token: 0x06001675 RID: 5749 RVA: 0x000850A4 File Offset: 0x000834A4
		public Material sea { get; protected set; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x000850AD File Offset: 0x000834AD
		// (set) Token: 0x06001677 RID: 5751 RVA: 0x000850B5 File Offset: 0x000834B5
		public PlanarReflection planarReflection { get; protected set; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x000850BE File Offset: 0x000834BE
		// (set) Token: 0x06001679 RID: 5753 RVA: 0x000850C6 File Offset: 0x000834C6
		[Inspectable("#SDG::Devkit.Water.Volume.Is_Surface_Visible", null)]
		public bool isSurfaceVisible
		{
			get
			{
				return this._isSurfaceVisible;
			}
			set
			{
				this._isSurfaceVisible = value;
				if (this.waterPlane != null)
				{
					this.waterPlane.SetActive(this.isSurfaceVisible);
				}
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x000850F1 File Offset: 0x000834F1
		// (set) Token: 0x0600167B RID: 5755 RVA: 0x000850F9 File Offset: 0x000834F9
		[Inspectable("#SDG::Devkit.Water.Volume.Is_Reflection_Visible", null)]
		public bool isReflectionVisible
		{
			get
			{
				return this._isReflectionVisible;
			}
			set
			{
				this._isReflectionVisible = value;
				this.updatePlanarReflection();
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x00085108 File Offset: 0x00083508
		// (set) Token: 0x0600167D RID: 5757 RVA: 0x00085110 File Offset: 0x00083510
		[Inspectable("#SDG::Devkit.Water.Volume.Is_Sea_Level", null)]
		public bool isSeaLevel
		{
			get
			{
				return this._isSeaLevel;
			}
			set
			{
				this._isSeaLevel = value;
				if (this.isSeaLevel)
				{
					WaterSystem.seaLevelVolume = this;
				}
			}
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0008512A File Offset: 0x0008352A
		protected virtual void updatePlanarReflection()
		{
			if (this.planarReflection != null)
			{
				this.planarReflection.enabled = (this.isReflectionVisible && GraphicsSettings.waterQuality == EGraphicQuality.ULTRA);
			}
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00085160 File Offset: 0x00083560
		protected virtual void createWaterPlanes()
		{
			if (!Dedicator.isDedicated && this.waterPlane == null)
			{
				this.waterPlane = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Level/Water_Plane"));
				this.waterPlane.name = "Plane";
				this.waterPlane.transform.parent = base.transform;
				this.waterPlane.transform.localPosition = new Vector3(0f, 0.5f, 0f);
				this.waterPlane.transform.localRotation = Quaternion.identity;
				this.waterPlane.transform.localScale = new Vector3(1f, 1f, 1f);
				this.waterPlane.SetActive(this.isSurfaceVisible);
				this.planarReflection = this.waterPlane.GetComponent<PlanarReflection>();
				int num = Mathf.Max(1, Mathf.FloorToInt(base.transform.localScale.x / (float)WaterVolume.WATER_SURFACE_TILE_SIZE));
				int num2 = Mathf.Max(1, Mathf.FloorToInt(base.transform.localScale.z / (float)WaterVolume.WATER_SURFACE_TILE_SIZE));
				float num3 = 1f / (float)num;
				float num4 = 1f / (float)num2;
				for (int i = 0; i < num; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Level/Water_Tile"));
						gameObject.name = string.Concat(new object[]
						{
							"Tile_",
							i,
							"_",
							j
						});
						gameObject.transform.parent = this.waterPlane.transform;
						gameObject.transform.localPosition = new Vector3(-0.5f + num3 / 2f + (float)i * num3, 0f, -0.5f + num4 / 2f + (float)j * num4);
						gameObject.transform.localRotation = Quaternion.identity;
						gameObject.transform.localScale = new Vector3(0.01f * num3, 0.01f, 0.01f * num4);
						if (this.sea == null)
						{
							this.sea = gameObject.GetComponent<Renderer>().material;
						}
						else
						{
							gameObject.GetComponent<Renderer>().material = this.sea;
						}
						gameObject.GetComponent<WaterTile>().reflection = this.planarReflection;
					}
				}
				this.planarReflection.sharedMaterial = this.sea;
				this.applyGraphicsSettings();
				LevelLighting.updateLighting();
			}
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0008540C File Offset: 0x0008380C
		public void beginCollision(Collider collider)
		{
			if (collider == null)
			{
				return;
			}
			IWaterVolumeInteractionHandler component = collider.gameObject.GetComponent<IWaterVolumeInteractionHandler>();
			if (component != null)
			{
				component.waterBeginCollision(this);
			}
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00085440 File Offset: 0x00083840
		public void endCollision(Collider collider)
		{
			if (collider == null)
			{
				return;
			}
			IWaterVolumeInteractionHandler component = collider.gameObject.GetComponent<IWaterVolumeInteractionHandler>();
			if (component != null)
			{
				component.waterEndCollision(this);
			}
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00085473 File Offset: 0x00083873
		public void devkitHierarchySpawn()
		{
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00085478 File Offset: 0x00083878
		protected override void readHierarchyItem(IFormattedFileReader reader)
		{
			base.readHierarchyItem(reader);
			this.isSurfaceVisible = reader.readValue<bool>("Is_Surface_Visible");
			this.isReflectionVisible = reader.readValue<bool>("Is_Reflection_Visible");
			this.isSeaLevel = reader.readValue<bool>("Is_Sea_Level");
			this.waterType = reader.readValue<ERefillWaterType>("Water_Type");
			this.createWaterPlanes();
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x000854D8 File Offset: 0x000838D8
		protected override void writeHierarchyItem(IFormattedFileWriter writer)
		{
			base.writeHierarchyItem(writer);
			writer.writeValue<bool>("Is_Surface_Visible", this.isSurfaceVisible);
			writer.writeValue<bool>("Is_Reflection_Visible", this.isReflectionVisible);
			writer.writeValue<bool>("Is_Sea_Level", this.isSeaLevel);
			writer.writeValue<ERefillWaterType>("Water_Type", this.waterType);
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00085530 File Offset: 0x00083930
		public void OnTriggerEnter(Collider other)
		{
			this.beginCollision(other);
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00085539 File Offset: 0x00083939
		public void OnTriggerExit(Collider other)
		{
			this.endCollision(other);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00085542 File Offset: 0x00083942
		protected virtual void updateBoxEnabled()
		{
			base.box.enabled = (!Level.isEditor || WaterSystem.waterVisibilityGroup.isVisible);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00085566 File Offset: 0x00083966
		protected virtual void handleVisibilityGroupIsVisibleChanged(IVisibilityGroup group)
		{
			this.updateBoxEnabled();
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0008556E File Offset: 0x0008396E
		protected virtual void applyGraphicsSettings()
		{
			this.updatePlanarReflection();
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00085576 File Offset: 0x00083976
		protected void OnEnable()
		{
			LevelHierarchy.addItem(this);
			WaterSystem.addVolume(this);
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00085584 File Offset: 0x00083984
		protected void OnDisable()
		{
			WaterSystem.removeVolume(this);
			LevelHierarchy.removeItem(this);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00085594 File Offset: 0x00083994
		protected void Awake()
		{
			base.name = "Water_Volume";
			base.gameObject.layer = LayerMasks.TRAP;
			base.box = base.gameObject.getOrAddComponent<BoxCollider>();
			base.box.isTrigger = true;
			this.updateBoxEnabled();
			WaterSystem.waterVisibilityGroup.isVisibleChanged += this.handleVisibilityGroupIsVisibleChanged;
			if (!Dedicator.isDedicated)
			{
				GraphicsSettings.graphicsSettingsApplied += this.applyGraphicsSettings;
			}
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00085612 File Offset: 0x00083A12
		protected void Start()
		{
			this.createWaterPlanes();
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0008561A File Offset: 0x00083A1A
		protected void OnDestroy()
		{
			if (!Dedicator.isDedicated)
			{
				GraphicsSettings.graphicsSettingsApplied -= this.applyGraphicsSettings;
			}
			WaterSystem.waterVisibilityGroup.isVisibleChanged -= this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x04000C3E RID: 3134
		public static readonly int WATER_SURFACE_TILE_SIZE = 1024;

		// Token: 0x04000C42 RID: 3138
		[SerializeField]
		protected bool _isSurfaceVisible;

		// Token: 0x04000C43 RID: 3139
		[SerializeField]
		protected bool _isReflectionVisible;

		// Token: 0x04000C44 RID: 3140
		[SerializeField]
		protected bool _isSeaLevel;

		// Token: 0x04000C45 RID: 3141
		[Inspectable("#SDG::Devkit.Water.Volume.Water_Type", null)]
		public ERefillWaterType waterType;
	}
}
