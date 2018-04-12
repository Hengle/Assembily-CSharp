using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000D9 RID: 217
	public class TileHandlerHelper : MonoBehaviour
	{
		// Token: 0x06000729 RID: 1833 RVA: 0x0004641D File Offset: 0x0004481D
		public void UseSpecifiedHandler(TileHandler handler)
		{
			this.handler = handler;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00046426 File Offset: 0x00044826
		private void OnEnable()
		{
			NavmeshCut.OnDestroyCallback += this.HandleOnDestroyCallback;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00046439 File Offset: 0x00044839
		private void OnDisable()
		{
			NavmeshCut.OnDestroyCallback -= this.HandleOnDestroyCallback;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0004644C File Offset: 0x0004484C
		public void DiscardPending()
		{
			List<NavmeshCut> all = NavmeshCut.GetAll();
			for (int i = 0; i < all.Count; i++)
			{
				if (all[i].RequiresUpdate())
				{
					all[i].NotifyUpdated();
				}
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00046494 File Offset: 0x00044894
		private void Start()
		{
			if (UnityEngine.Object.FindObjectsOfType(typeof(TileHandlerHelper)).Length > 1)
			{
				Debug.LogError("There should only be one TileHandlerHelper per scene. Destroying.");
				UnityEngine.Object.Destroy(this);
				return;
			}
			if (this.handler == null)
			{
				if (AstarPath.active == null || AstarPath.active.astarData.recastGraph == null)
				{
					Debug.LogWarning("No AstarPath object in the scene or no RecastGraph on that AstarPath object");
				}
				if (AstarPath.active.astarData.recastGraph != null)
				{
					this.handler = new TileHandler(AstarPath.active.astarData.recastGraph);
					this.handler.CreateTileTypesFromGraph();
				}
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0004653B File Offset: 0x0004493B
		private void HandleOnDestroyCallback(NavmeshCut obj)
		{
			Debug.Log("adding " + obj.LastBounds);
			this.forcedReloadBounds.Add(obj.LastBounds);
			this.lastUpdateTime = -999f;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00046573 File Offset: 0x00044973
		private void Update()
		{
			if (this.updateInterval == -1f || Time.realtimeSinceStartup - this.lastUpdateTime < this.updateInterval || this.handler == null)
			{
				return;
			}
			this.ForceUpdate();
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000465B0 File Offset: 0x000449B0
		public void ForceUpdate()
		{
			if (this.handler == null)
			{
				throw new Exception("Cannot update graphs. No TileHandler. Do not call this method in Awake.");
			}
			this.lastUpdateTime = Time.realtimeSinceStartup;
			List<NavmeshCut> all = NavmeshCut.GetAll();
			if (this.forcedReloadBounds.Count == 0)
			{
				int num = 0;
				for (int i = 0; i < all.Count; i++)
				{
					if (all[i].RequiresUpdate())
					{
						num++;
						break;
					}
				}
				if (num == 0)
				{
					return;
				}
			}
			bool flag = this.handler.StartBatchLoad();
			for (int j = 0; j < this.forcedReloadBounds.Count; j++)
			{
				this.handler.ReloadInBounds(this.forcedReloadBounds[j]);
			}
			this.forcedReloadBounds.Clear();
			for (int k = 0; k < all.Count; k++)
			{
				if (all[k].enabled)
				{
					if (all[k].RequiresUpdate())
					{
						this.handler.ReloadInBounds(all[k].LastBounds);
						this.handler.ReloadInBounds(all[k].GetBounds());
					}
				}
				else if (all[k].RequiresUpdate())
				{
					this.handler.ReloadInBounds(all[k].LastBounds);
				}
			}
			for (int l = 0; l < all.Count; l++)
			{
				if (all[l].RequiresUpdate())
				{
					all[l].NotifyUpdated();
				}
			}
			if (flag)
			{
				this.handler.EndBatchLoad();
			}
		}

		// Token: 0x04000611 RID: 1553
		private TileHandler handler;

		// Token: 0x04000612 RID: 1554
		public float updateInterval;

		// Token: 0x04000613 RID: 1555
		private float lastUpdateTime = -999f;

		// Token: 0x04000614 RID: 1556
		private List<Bounds> forcedReloadBounds = new List<Bounds>();
	}
}
