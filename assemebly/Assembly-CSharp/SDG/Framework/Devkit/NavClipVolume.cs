using System;
using SDG.Framework.Devkit.Visibility;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000156 RID: 342
	public class NavClipVolume : DevkitHierarchyVolume, IDevkitHierarchySpawnable
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x000510FD File Offset: 0x0004F4FD
		public override VolumeVisibilityGroup visibilityGroupOverride
		{
			get
			{
				return PlayerClipVolumeSystem.navClipVisibilityGroup;
			}
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00051104 File Offset: 0x0004F504
		public void devkitHierarchySpawn()
		{
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00051106 File Offset: 0x0004F506
		protected virtual void updateBoxEnabled()
		{
			base.box.enabled = (Provider.isServer || (Level.isEditor && PlayerClipVolumeSystem.navClipVisibilityGroup.isVisible));
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00051137 File Offset: 0x0004F537
		protected virtual void handleVisibilityGroupIsVisibleChanged(IVisibilityGroup group)
		{
			this.updateBoxEnabled();
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0005113F File Offset: 0x0004F53F
		protected void OnEnable()
		{
			LevelHierarchy.addItem(this);
			PlayerClipVolumeSystem.addVolume(this);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0005114D File Offset: 0x0004F54D
		protected void OnDisable()
		{
			PlayerClipVolumeSystem.removeVolume(this);
			LevelHierarchy.removeItem(this);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0005115C File Offset: 0x0004F55C
		protected void Awake()
		{
			base.name = "Nav_Clip_Volume";
			base.gameObject.layer = LayerMasks.NAVMESH;
			base.box = base.gameObject.getOrAddComponent<BoxCollider>();
			this.updateBoxEnabled();
			PlayerClipVolumeSystem.navClipVisibilityGroup.isVisibleChanged += this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000511B2 File Offset: 0x0004F5B2
		protected void OnDestroy()
		{
			PlayerClipVolumeSystem.navClipVisibilityGroup.isVisibleChanged -= this.handleVisibilityGroupIsVisibleChanged;
		}
	}
}
