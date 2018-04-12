using System;
using System.Collections.Generic;
using SDG.Framework.Devkit;
using SDG.Framework.Devkit.Visibility;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001DF RID: 479
	public class LandscapeHoleVolume : DevkitHierarchyVolume, IDevkitHierarchySpawnable
	{
		// Token: 0x06000E61 RID: 3681 RVA: 0x00063900 File Offset: 0x00061D00
		public void beginCollision(Collider collider)
		{
			if (collider == null || this.terrainColliders == null)
			{
				return;
			}
			ILandscaleHoleVolumeInteractionHandler component = collider.gameObject.GetComponent<ILandscaleHoleVolumeInteractionHandler>();
			if (component != null)
			{
				component.landscapeHoleBeginCollision(this, this.terrainColliders);
			}
			if (component == null || component.landscapeHoleAutoIgnoreTerrainCollision)
			{
				foreach (TerrainCollider collider2 in this.terrainColliders)
				{
					Physics.IgnoreCollision(collider, collider2, true);
				}
			}
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x000639A8 File Offset: 0x00061DA8
		public void endCollision(Collider collider)
		{
			if (collider == null || this.terrainColliders == null)
			{
				return;
			}
			ILandscaleHoleVolumeInteractionHandler component = collider.gameObject.GetComponent<ILandscaleHoleVolumeInteractionHandler>();
			if (component != null)
			{
				component.landscapeHoleEndCollision(this, this.terrainColliders);
			}
			if (component == null || component.landscapeHoleAutoIgnoreTerrainCollision)
			{
				foreach (TerrainCollider collider2 in this.terrainColliders)
				{
					Physics.IgnoreCollision(collider, collider2, false);
				}
			}
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00063A50 File Offset: 0x00061E50
		public void devkitHierarchySpawn()
		{
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00063A52 File Offset: 0x00061E52
		public void OnTriggerEnter(Collider other)
		{
			this.beginCollision(other);
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00063A5B File Offset: 0x00061E5B
		public void OnTriggerExit(Collider other)
		{
			this.endCollision(other);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00063A64 File Offset: 0x00061E64
		protected void findTerrainColliders()
		{
			this.terrainColliders.Clear();
			Bounds bounds = base.box.bounds;
			LandscapeBounds landscapeBounds = new LandscapeBounds(bounds);
			for (int i = landscapeBounds.min.x; i <= landscapeBounds.max.x; i++)
			{
				for (int j = landscapeBounds.min.y; j <= landscapeBounds.max.y; j++)
				{
					LandscapeCoord coord = new LandscapeCoord(i, j);
					LandscapeTile tile = Landscape.getTile(coord);
					if (tile != null)
					{
						this.terrainColliders.Add(tile.collider);
					}
				}
			}
			if (LevelGround.terrain != null)
			{
				this.terrainColliders.Add(LevelGround.terrain.transform.GetComponent<TerrainCollider>());
			}
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00063B3A File Offset: 0x00061F3A
		protected void handleLandscapeLoaded()
		{
			this.findTerrainColliders();
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00063B42 File Offset: 0x00061F42
		protected virtual void updateBoxEnabled()
		{
			base.box.enabled = (!Level.isEditor || LandscapeHoleSystem.holeVisibilityGroup.isVisible);
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00063B66 File Offset: 0x00061F66
		protected virtual void handleVisibilityGroupIsVisibleChanged(IVisibilityGroup group)
		{
			this.updateBoxEnabled();
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00063B6E File Offset: 0x00061F6E
		protected void OnEnable()
		{
			LevelHierarchy.addItem(this);
			LandscapeHoleSystem.addVolume(this);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00063B7C File Offset: 0x00061F7C
		protected void OnDisable()
		{
			LandscapeHoleSystem.removeVolume(this);
			LevelHierarchy.removeItem(this);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00063B8C File Offset: 0x00061F8C
		protected void Awake()
		{
			base.name = "Landscape_Hole_Volume";
			base.gameObject.layer = LayerMasks.TRAP;
			base.box = base.gameObject.getOrAddComponent<BoxCollider>();
			base.box.isTrigger = true;
			this.updateBoxEnabled();
			this.terrainColliders = new List<TerrainCollider>();
			this.findTerrainColliders();
			Landscape.loaded += this.handleLandscapeLoaded;
			LandscapeHoleSystem.holeVisibilityGroup.isVisibleChanged += this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00063C10 File Offset: 0x00062010
		protected void OnDestroy()
		{
			Landscape.loaded -= this.handleLandscapeLoaded;
			LandscapeHoleSystem.holeVisibilityGroup.isVisibleChanged -= this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x0400092A RID: 2346
		protected List<TerrainCollider> terrainColliders;
	}
}
