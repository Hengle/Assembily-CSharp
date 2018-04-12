using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.IO.FormattedFiles;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Foliage
{
	// Token: 0x020001AF RID: 431
	public class FoliageVolume : DevkitHierarchyVolume, IDevkitHierarchySpawnable
	{
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0005F2FC File Offset: 0x0005D6FC
		// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x0005F304 File Offset: 0x0005D704
		[Inspectable("#SDG::Devkit.Foliage.Volume.Mode", null)]
		public FoliageVolume.EFoliageVolumeMode mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				if (!base.enabled)
				{
					this._mode = value;
					return;
				}
				FoliageVolumeSystem.removeVolume(this);
				this._mode = value;
				FoliageVolumeSystem.addVolume(this);
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0005F32C File Offset: 0x0005D72C
		public void devkitHierarchySpawn()
		{
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0005F330 File Offset: 0x0005D730
		protected override void readHierarchyItem(IFormattedFileReader reader)
		{
			base.readHierarchyItem(reader);
			this.mode = reader.readValue<FoliageVolume.EFoliageVolumeMode>("Mode");
			if (reader.containsKey("Instanced_Meshes"))
			{
				this.instancedMeshes = reader.readValue<bool>("Instanced_Meshes");
			}
			if (reader.containsKey("Resources"))
			{
				this.resources = reader.readValue<bool>("Resources");
			}
			if (reader.containsKey("Objects"))
			{
				this.objects = reader.readValue<bool>("Objects");
			}
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0005F3B8 File Offset: 0x0005D7B8
		protected override void writeHierarchyItem(IFormattedFileWriter writer)
		{
			base.writeHierarchyItem(writer);
			writer.writeValue<FoliageVolume.EFoliageVolumeMode>("Mode", this.mode);
			writer.writeValue<bool>("Instanced_Meshes", this.instancedMeshes);
			writer.writeValue<bool>("Resources", this.resources);
			writer.writeValue<bool>("Objects", this.objects);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0005F410 File Offset: 0x0005D810
		protected virtual void updateBoxEnabled()
		{
			base.box.enabled = (Dedicator.isDedicated || FoliageVolumeSystem.foliageVisibilityGroup.isVisible);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0005F434 File Offset: 0x0005D834
		protected virtual void handleVisibilityGroupIsVisibleChanged(IVisibilityGroup group)
		{
			this.updateBoxEnabled();
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0005F43C File Offset: 0x0005D83C
		protected void OnEnable()
		{
			LevelHierarchy.addItem(this);
			FoliageVolumeSystem.addVolume(this);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0005F44A File Offset: 0x0005D84A
		protected void OnDisable()
		{
			FoliageVolumeSystem.removeVolume(this);
			LevelHierarchy.removeItem(this);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0005F458 File Offset: 0x0005D858
		protected void Awake()
		{
			base.name = "Foliage_Volume";
			base.gameObject.layer = LayerMasks.TRAP;
			base.box = base.gameObject.getOrAddComponent<BoxCollider>();
			base.box.isTrigger = true;
			this.updateBoxEnabled();
			this._mode = FoliageVolume.EFoliageVolumeMode.SUBTRACTIVE;
			this.instancedMeshes = true;
			this.resources = true;
			this.objects = true;
			FoliageVolumeSystem.foliageVisibilityGroup.isVisibleChanged += this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0005F4D6 File Offset: 0x0005D8D6
		protected void OnDestroy()
		{
			FoliageVolumeSystem.foliageVisibilityGroup.isVisibleChanged -= this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x040008D0 RID: 2256
		protected FoliageVolume.EFoliageVolumeMode _mode;

		// Token: 0x040008D1 RID: 2257
		[Inspectable("#SDG::Devkit.Foliage.Volume.Instanced_Meshes", null)]
		public bool instancedMeshes;

		// Token: 0x040008D2 RID: 2258
		[Inspectable("#SDG::Devkit.Foliage.Volume.Resources", null)]
		public bool resources;

		// Token: 0x040008D3 RID: 2259
		[Inspectable("#SDG::Devkit.Foliage.Volume.Objects", null)]
		public bool objects;

		// Token: 0x020001B0 RID: 432
		public enum EFoliageVolumeMode
		{
			// Token: 0x040008D5 RID: 2261
			ADDITIVE,
			// Token: 0x040008D6 RID: 2262
			SUBTRACTIVE
		}
	}
}
