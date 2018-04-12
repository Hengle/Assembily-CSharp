using System;
using SDG.Framework.Devkit.Interactable;
using SDG.Framework.Devkit.Visibility;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x0200012B RID: 299
	public class DevkitHierarchyVolume : DevkitHierarchyWorldItem, IDevkitInteractableBeginSelectionHandler, IDevkitInteractableEndSelectionHandler
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0004DF37 File Offset: 0x0004C337
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x0004DF3F File Offset: 0x0004C33F
		public BoxCollider box { get; protected set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0004DF48 File Offset: 0x0004C348
		public virtual VolumeVisibilityGroup visibilityGroupOverride
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0004DF4B File Offset: 0x0004C34B
		public virtual void beginSelection(InteractionData data)
		{
			this.isSelected = true;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0004DF54 File Offset: 0x0004C354
		public virtual void endSelection(InteractionData data)
		{
			this.isSelected = false;
		}

		// Token: 0x0400070A RID: 1802
		public bool isSelected;
	}
}
