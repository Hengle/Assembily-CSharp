using System;
using SDG.Framework.Devkit.Visibility;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000127 RID: 295
	public class DeadzoneVolume : DevkitHierarchyVolume, IDeadzoneNode, IDevkitHierarchySpawnable
	{
		// Token: 0x060008FF RID: 2303 RVA: 0x0004E453 File Offset: 0x0004C853
		public void devkitHierarchySpawn()
		{
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0004E455 File Offset: 0x0004C855
		protected virtual void updateBoxEnabled()
		{
			base.box.enabled = (Level.isEditor && DeadzoneSystem.deadzoneVisibilityGroup.isVisible);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0004E479 File Offset: 0x0004C879
		protected virtual void handleVisibilityGroupIsVisibleChanged(IVisibilityGroup group)
		{
			this.updateBoxEnabled();
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0004E481 File Offset: 0x0004C881
		protected void OnEnable()
		{
			LevelHierarchy.addItem(this);
			DeadzoneSystem.addVolume(this);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0004E48F File Offset: 0x0004C88F
		protected void OnDisable()
		{
			DeadzoneSystem.removeVolume(this);
			LevelHierarchy.removeItem(this);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0004E4A0 File Offset: 0x0004C8A0
		protected void Awake()
		{
			base.name = "Deadzone_Volume";
			base.gameObject.layer = LayerMasks.TRAP;
			base.box = base.gameObject.getOrAddComponent<BoxCollider>();
			base.box.isTrigger = true;
			this.updateBoxEnabled();
			DeadzoneSystem.deadzoneVisibilityGroup.isVisibleChanged += this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0004E502 File Offset: 0x0004C902
		protected void OnDestroy()
		{
			DeadzoneSystem.deadzoneVisibilityGroup.isVisibleChanged -= this.handleVisibilityGroupIsVisibleChanged;
		}
	}
}
