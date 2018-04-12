using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit.Interactable;
using SDG.Framework.IO.FormattedFiles;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x0200012D RID: 301
	public class DevkitHierarchyWorldObject : DevkitHierarchyWorldItem, IDevkitInteractableBeginSelectionHandler, IDevkitInteractableEndSelectionHandler, IDevkitSelectionCopyableHandler, IDevkitSelectionTransformableHandler
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0004E5D9 File Offset: 0x0004C9D9
		[Inspectable("#SDG::Display_Name", null)]
		public string displayName
		{
			get
			{
				if (this.levelObject != null && this.levelObject.asset != null)
				{
					return this.levelObject.asset.objectName;
				}
				return string.Empty;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0004E60C File Offset: 0x0004CA0C
		[Inspectable("#SDG::Internal_Name", null)]
		public string internalName
		{
			get
			{
				if (this.levelObject != null && this.levelObject.asset != null)
				{
					return this.levelObject.asset.name;
				}
				return string.Empty;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0004E63F File Offset: 0x0004CA3F
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x0004E647 File Offset: 0x0004CA47
		public LevelObject levelObject { get; protected set; }

		// Token: 0x06000930 RID: 2352 RVA: 0x0004E650 File Offset: 0x0004CA50
		public void beginSelection(InteractionData data)
		{
			HighlighterTool.highlight(base.transform, Color.yellow);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0004E662 File Offset: 0x0004CA62
		public void endSelection(InteractionData data)
		{
			HighlighterTool.unhighlight(base.transform);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0004E670 File Offset: 0x0004CA70
		public GameObject copySelection()
		{
			GameObject gameObject = new GameObject();
			gameObject.transform.position = base.transform.position;
			gameObject.transform.rotation = base.transform.rotation;
			gameObject.transform.localScale = base.transform.localScale;
			DevkitHierarchyWorldObject devkitHierarchyWorldObject = gameObject.AddComponent<DevkitHierarchyWorldObject>();
			devkitHierarchyWorldObject.GUID = this.GUID;
			devkitHierarchyWorldObject.placementOrigin = this.placementOrigin;
			devkitHierarchyWorldObject.customMaterialOverride = this.customMaterialOverride;
			devkitHierarchyWorldObject.materialIndexOverride = this.materialIndexOverride;
			return gameObject;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0004E700 File Offset: 0x0004CB00
		public void transformSelection()
		{
			byte b;
			byte b2;
			if (Regions.tryGetCoordinate(base.transform.position, out b, out b2) && (this.x != b || this.y != b2))
			{
				LevelObjects.moveDevkitObject(this.levelObject, this.x, this.y, b, b2);
				this.x = b;
				this.y = b2;
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0004E768 File Offset: 0x0004CB68
		protected override void readHierarchyItem(IFormattedFileReader reader)
		{
			base.readHierarchyItem(reader);
			this.GUID = reader.readValue<Guid>("GUID");
			this.placementOrigin = reader.readValue<ELevelObjectPlacementOrigin>("Origin");
			this.customMaterialOverride = reader.readValue<AssetReference<MaterialPaletteAsset>>("Custom_Material_Override");
			if (reader.containsKey("Material_Index_Override"))
			{
				this.materialIndexOverride = reader.readValue<int>("Material_Index_Override");
			}
			else
			{
				this.materialIndexOverride = -1;
			}
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0004E7DC File Offset: 0x0004CBDC
		protected override void writeHierarchyItem(IFormattedFileWriter writer)
		{
			base.writeHierarchyItem(writer);
			writer.writeValue<Guid>("GUID", this.GUID);
			writer.writeValue<ELevelObjectPlacementOrigin>("Origin", this.placementOrigin);
			writer.writeValue<AssetReference<MaterialPaletteAsset>>("Custom_Material_Override", this.customMaterialOverride);
			writer.writeValue<int>("Material_Index_Override", this.materialIndexOverride);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0004E834 File Offset: 0x0004CC34
		protected void OnEnable()
		{
			LevelHierarchy.addItem(this);
			if (this.levelObject != null)
			{
				LevelObjects.registerDevkitObject(this.levelObject, out this.x, out this.y);
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0004E85E File Offset: 0x0004CC5E
		protected void OnDisable()
		{
			if (this.levelObject != null)
			{
				LevelObjects.unregisterDevkitObject(this.levelObject, this.x, this.y);
			}
			LevelHierarchy.removeItem(this);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0004E888 File Offset: 0x0004CC88
		protected void Awake()
		{
			base.name = "World_Object";
			if (Level.isEditor)
			{
				Rigidbody orAddComponent = base.gameObject.getOrAddComponent<Rigidbody>();
				orAddComponent.isKinematic = true;
				orAddComponent.useGravity = false;
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0004E8C4 File Offset: 0x0004CCC4
		protected void Start()
		{
			if (this.levelObject != null)
			{
				return;
			}
			this.levelObject = new LevelObject(base.inspectablePosition, base.inspectableRotation, base.inspectableScale, 0, null, this.GUID, this.placementOrigin, this.instanceID, this.customMaterialOverride, this.materialIndexOverride, true);
			if (this.levelObject.transform == null)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Failed to create LevelObject - GUID: ",
					this.GUID.ToString("N"),
					" InstanceID: ",
					this.instanceID,
					" Position: ",
					base.transform.position
				}), base.gameObject);
				this.levelObject = new LevelObject(base.inspectablePosition, base.inspectableRotation, base.inspectableScale, 0, null, new Guid("62f7de571873436a8c9a203e6304bd8a"), this.placementOrigin, this.instanceID, AssetReference<MaterialPaletteAsset>.invalid, -1, true);
			}
			base.gameObject.tag = "Large";
			base.gameObject.layer = this.levelObject.transform.gameObject.layer;
			this.levelObject.transform.parent = base.transform;
			this.levelObject.transform.localPosition = Vector3.zero;
			this.levelObject.transform.localRotation = Quaternion.identity;
			this.levelObject.transform.localScale = Vector3.one;
			if (this.levelObject.skybox != null)
			{
				this.levelObject.skybox.transform.parent = base.transform;
				this.levelObject.skybox.transform.localPosition = Vector3.zero;
				this.levelObject.skybox.transform.localRotation = Quaternion.identity;
				this.levelObject.skybox.transform.localScale = Vector3.one;
			}
			LevelObjects.registerDevkitObject(this.levelObject, out this.x, out this.y);
		}

		// Token: 0x0400070B RID: 1803
		[Inspectable("#SDG::Custom_Material_Override", null)]
		public AssetReference<MaterialPaletteAsset> customMaterialOverride;

		// Token: 0x0400070C RID: 1804
		[Inspectable("#SDG::Material_Index_Override", null)]
		public int materialIndexOverride = -1;

		// Token: 0x0400070D RID: 1805
		public Guid GUID;

		// Token: 0x0400070E RID: 1806
		public ELevelObjectPlacementOrigin placementOrigin;

		// Token: 0x04000710 RID: 1808
		protected byte x;

		// Token: 0x04000711 RID: 1809
		protected byte y;
	}
}
