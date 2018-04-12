using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit.Interactable;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.IO.FormattedFiles;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x0200015A RID: 346
	public class Spawnpoint : DevkitHierarchyWorldItem, IDevkitInteractableBeginSelectionHandler, IDevkitInteractableEndSelectionHandler, IDevkitHierarchySpawnable
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x000515B7 File Offset: 0x0004F9B7
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x000515BF File Offset: 0x0004F9BF
		public SphereCollider sphere { get; protected set; }

		// Token: 0x06000A48 RID: 2632 RVA: 0x000515C8 File Offset: 0x0004F9C8
		public virtual void beginSelection(InteractionData data)
		{
			this.isSelected = true;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000515D1 File Offset: 0x0004F9D1
		public virtual void endSelection(InteractionData data)
		{
			this.isSelected = false;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000515DA File Offset: 0x0004F9DA
		public void devkitHierarchySpawn()
		{
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000515DC File Offset: 0x0004F9DC
		protected virtual void updateSphereEnabled()
		{
			this.sphere.enabled = (Level.isEditor && SpawnpointSystem.spawnpointVisibilityGroup.isVisible);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00051600 File Offset: 0x0004FA00
		protected virtual void handleVisibilityGroupIsVisibleChanged(IVisibilityGroup group)
		{
			this.updateSphereEnabled();
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00051608 File Offset: 0x0004FA08
		protected override void readHierarchyItem(IFormattedFileReader reader)
		{
			base.readHierarchyItem(reader);
			this.id = reader.readValue<string>("ID");
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00051622 File Offset: 0x0004FA22
		protected override void writeHierarchyItem(IFormattedFileWriter writer)
		{
			base.writeHierarchyItem(writer);
			writer.writeValue("ID", this.id);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0005163C File Offset: 0x0004FA3C
		protected void OnEnable()
		{
			LevelHierarchy.addItem(this);
			SpawnpointSystem.addSpawnpoint(this);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0005164A File Offset: 0x0004FA4A
		protected void OnDisable()
		{
			SpawnpointSystem.removeSpawnpoint(this);
			LevelHierarchy.removeItem(this);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00051658 File Offset: 0x0004FA58
		protected void Awake()
		{
			base.name = "Spawnpoint";
			base.gameObject.layer = LayerMasks.TRAP;
			this.sphere = base.gameObject.getOrAddComponent<SphereCollider>();
			this.sphere.radius = 0.5f;
			this.updateSphereEnabled();
			SpawnpointSystem.spawnpointVisibilityGroup.isVisibleChanged += this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000516BE File Offset: 0x0004FABE
		protected void OnDestroy()
		{
			SpawnpointSystem.spawnpointVisibilityGroup.isVisibleChanged -= this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x04000754 RID: 1876
		[Inspectable("#SDG::Devkit.Spawns.Spawnpoint.ID.Name", null)]
		public string id;

		// Token: 0x04000756 RID: 1878
		public bool isSelected;
	}
}
