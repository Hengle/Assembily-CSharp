using System;
using SDG.Framework.Devkit;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000490 RID: 1168
	public class EditorArea : MonoBehaviour
	{
		// Token: 0x14000071 RID: 113
		// (add) Token: 0x06001E9D RID: 7837 RVA: 0x000A7858 File Offset: 0x000A5C58
		// (remove) Token: 0x06001E9E RID: 7838 RVA: 0x000A788C File Offset: 0x000A5C8C
		public static event EditorAreaRegisteredHandler registered;

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001E9F RID: 7839 RVA: 0x000A78C0 File Offset: 0x000A5CC0
		// (set) Token: 0x06001EA0 RID: 7840 RVA: 0x000A78C7 File Offset: 0x000A5CC7
		public static EditorArea instance { get; protected set; }

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x000A78CF File Offset: 0x000A5CCF
		public byte region_x
		{
			get
			{
				return this._region_x;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x000A78D7 File Offset: 0x000A5CD7
		public byte region_y
		{
			get
			{
				return this._region_y;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x000A78DF File Offset: 0x000A5CDF
		public byte bound
		{
			get
			{
				return this._bound;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001EA4 RID: 7844 RVA: 0x000A78E7 File Offset: 0x000A5CE7
		// (set) Token: 0x06001EA5 RID: 7845 RVA: 0x000A78EF File Offset: 0x000A5CEF
		public IAmbianceNode effectNode { get; private set; }

		// Token: 0x06001EA6 RID: 7846 RVA: 0x000A78F8 File Offset: 0x000A5CF8
		protected void triggerRegistered()
		{
			if (EditorArea.registered != null)
			{
				EditorArea.registered(this);
			}
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x000A7910 File Offset: 0x000A5D10
		private void Update()
		{
			byte b;
			byte b2;
			if (Regions.tryGetCoordinate(base.transform.position, out b, out b2) && (b != this.region_x || b2 != this.region_y))
			{
				byte region_x = this.region_x;
				byte region_y = this.region_y;
				this._region_x = b;
				this._region_y = b2;
				if (this.onRegionUpdated != null)
				{
					this.onRegionUpdated(region_x, region_y, b, b2);
				}
			}
			byte b3;
			LevelNavigation.tryGetBounds(base.transform.position, out b3);
			if (b3 != this.bound)
			{
				byte bound = this.bound;
				this._bound = b3;
				if (this.onBoundUpdated != null)
				{
					this.onBoundUpdated(bound, b3);
				}
			}
			this.effectNode = null;
			for (int i = 0; i < LevelNodes.nodes.Count; i++)
			{
				Node node = LevelNodes.nodes[i];
				if (node.type == ENodeType.EFFECT)
				{
					EffectNode effectNode = (EffectNode)node;
					if (effectNode.shape == ENodeShape.SPHERE)
					{
						if ((base.transform.position - effectNode.point).sqrMagnitude < effectNode.editorRadius)
						{
							this.effectNode = effectNode;
							break;
						}
					}
					else if (effectNode.shape == ENodeShape.BOX && Mathf.Abs(base.transform.position.x - effectNode.point.x) < effectNode.bounds.x && Mathf.Abs(base.transform.position.y - effectNode.point.y) < effectNode.bounds.y && Mathf.Abs(base.transform.position.z - effectNode.point.z) < effectNode.bounds.z)
					{
						this.effectNode = effectNode;
						break;
					}
				}
			}
			AmbianceVolume effectNode2;
			if (this.effectNode == null && AmbianceUtility.isPointInsideVolume(base.transform.position, out effectNode2))
			{
				this.effectNode = effectNode2;
			}
			LevelLighting.updateLocal(MainCamera.instance.transform.position, 0f, this.effectNode);
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x000A7B82 File Offset: 0x000A5F82
		private void Start()
		{
			this._region_x = byte.MaxValue;
			this._region_y = byte.MaxValue;
			this._bound = byte.MaxValue;
			EditorArea.instance = this;
			LevelLighting.updateLighting();
			this.triggerRegistered();
		}

		// Token: 0x04001262 RID: 4706
		public EditorRegionUpdated onRegionUpdated;

		// Token: 0x04001263 RID: 4707
		public EditorBoundUpdated onBoundUpdated;

		// Token: 0x04001264 RID: 4708
		private byte _region_x;

		// Token: 0x04001265 RID: 4709
		private byte _region_y;

		// Token: 0x04001266 RID: 4710
		private byte _bound;
	}
}
