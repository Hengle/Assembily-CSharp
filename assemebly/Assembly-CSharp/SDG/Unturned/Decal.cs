using System;
using SDG.Framework.Devkit.Interactable;
using SDG.Framework.Devkit.Visibility;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000482 RID: 1154
	public class Decal : MonoBehaviour, IDevkitInteractableBeginSelectionHandler, IDevkitInteractableEndSelectionHandler
	{
		// Token: 0x06001E74 RID: 7796 RVA: 0x000A7187 File Offset: 0x000A5587
		public virtual void beginSelection(InteractionData data)
		{
			this.isSelected = true;
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x000A7190 File Offset: 0x000A5590
		public virtual void endSelection(InteractionData data)
		{
			this.isSelected = false;
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x000A719C File Offset: 0x000A559C
		private MeshRenderer getMesh()
		{
			MeshRenderer component = base.transform.parent.GetComponent<MeshRenderer>();
			if (component == null)
			{
				Transform transform = base.transform.parent.FindChild("Mesh");
				if (transform != null)
				{
					component = transform.GetComponent<MeshRenderer>();
				}
			}
			return component;
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x000A71F0 File Offset: 0x000A55F0
		private void onGraphicsSettingsApplied()
		{
			MeshRenderer mesh = this.getMesh();
			if (mesh != null)
			{
				mesh.enabled = (GraphicsSettings.renderMode == ERenderMode.FORWARD);
			}
			if (GraphicsSettings.renderMode == ERenderMode.DEFERRED)
			{
				DecalSystem.add(this);
			}
			else
			{
				DecalSystem.remove(this);
			}
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x000A7239 File Offset: 0x000A5639
		protected virtual void updateBoxEnabled()
		{
			if (this.box != null)
			{
				this.box.enabled = (!Dedicator.isDedicated && DecalSystem.decalVisibilityGroup.isVisible);
			}
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x000A726E File Offset: 0x000A566E
		protected virtual void handleVisibilityGroupIsVisibleChanged(IVisibilityGroup group)
		{
			this.updateBoxEnabled();
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x000A7276 File Offset: 0x000A5676
		private void Awake()
		{
			this.box = base.transform.parent.GetComponent<BoxCollider>();
			this.updateBoxEnabled();
			DecalSystem.decalVisibilityGroup.isVisibleChanged += this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x000A72AC File Offset: 0x000A56AC
		private void Start()
		{
			if (Dedicator.isDedicated)
			{
				return;
			}
			MeshRenderer mesh = this.getMesh();
			if (mesh != null)
			{
				mesh.enabled = (GraphicsSettings.renderMode == ERenderMode.FORWARD);
			}
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x000A72E5 File Offset: 0x000A56E5
		private void OnEnable()
		{
			if (Dedicator.isDedicated)
			{
				return;
			}
			if (GraphicsSettings.renderMode == ERenderMode.DEFERRED)
			{
				DecalSystem.add(this);
			}
			GraphicsSettings.graphicsSettingsApplied += this.onGraphicsSettingsApplied;
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x000A7313 File Offset: 0x000A5713
		private void OnDisable()
		{
			if (Dedicator.isDedicated)
			{
				return;
			}
			GraphicsSettings.graphicsSettingsApplied -= this.onGraphicsSettingsApplied;
			DecalSystem.remove(this);
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x000A7337 File Offset: 0x000A5737
		protected void OnDestroy()
		{
			DecalSystem.decalVisibilityGroup.isVisibleChanged -= this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x000A7350 File Offset: 0x000A5750
		private void DrawGizmo(bool selected)
		{
			Gizmos.color = ((!selected) ? Color.red : Color.yellow);
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x000A738B File Offset: 0x000A578B
		private void OnDrawGizmos()
		{
			this.DrawGizmo(false);
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x000A7394 File Offset: 0x000A5794
		private void OnDrawGizmosSelected()
		{
			this.DrawGizmo(true);
		}

		// Token: 0x04001216 RID: 4630
		public EDecalType type;

		// Token: 0x04001217 RID: 4631
		public Material material;

		// Token: 0x04001218 RID: 4632
		public bool isSelected;

		// Token: 0x04001219 RID: 4633
		public float lodBias = 1f;

		// Token: 0x0400121A RID: 4634
		protected BoxCollider box;
	}
}
