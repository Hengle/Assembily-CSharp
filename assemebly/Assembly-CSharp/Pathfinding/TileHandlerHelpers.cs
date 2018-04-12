using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000DA RID: 218
	public class TileHandlerHelpers : MonoBehaviour
	{
		// Token: 0x06000732 RID: 1842 RVA: 0x00046796 File Offset: 0x00044B96
		private void OnEnable()
		{
			NavmeshCut.OnDestroyCallback += this.HandleOnDestroyCallback;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x000467A9 File Offset: 0x00044BA9
		private void OnDisable()
		{
			NavmeshCut.OnDestroyCallback -= this.HandleOnDestroyCallback;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x000467BC File Offset: 0x00044BBC
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

		// Token: 0x06000735 RID: 1845 RVA: 0x00046804 File Offset: 0x00044C04
		private void Start()
		{
			if (AstarPath.active == null)
			{
				Debug.LogWarning("No AstarPath object in the scene or no RecastGraph on that AstarPath object");
			}
			for (int i = 0; i < AstarPath.active.astarData.graphs.Length; i++)
			{
				RecastGraph graph = (RecastGraph)AstarPath.active.astarData.graphs[i];
				TileHandler tileHandler = new TileHandler(graph);
				tileHandler.CreateTileTypesFromGraph();
				this.handlers.Add(tileHandler);
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0004687D File Offset: 0x00044C7D
		private void HandleOnDestroyCallback(NavmeshCut obj)
		{
			this.forcedReloadBounds.Add(obj.LastBounds);
			this.lastUpdateTime = -999f;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0004689B File Offset: 0x00044C9B
		private void Update()
		{
			if (this.updateInterval == -1f || Time.realtimeSinceStartup - this.lastUpdateTime < this.updateInterval || this.handlers.Count == 0)
			{
				return;
			}
			this.ForceUpdate();
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000468DC File Offset: 0x00044CDC
		public void ForceUpdate()
		{
			if (this.handlers.Count == 0)
			{
				throw new Exception("Cannot update graphs. No TileHandler. Do not call this method in Awake.");
			}
			if (this.updateIndex >= this.handlers.Count - 1)
			{
				this.updateIndex = -1;
			}
			this.updateIndex++;
			this.lastUpdateTime = Time.realtimeSinceStartup;
			TileHandler tileHandler = this.handlers[this.updateIndex];
			List<NavmeshCut> allInRange = NavmeshCut.GetAllInRange(tileHandler.graph.forcedBounds);
			if (this.forcedReloadBounds.Count == 0)
			{
				int num = 0;
				for (int i = 0; i < allInRange.Count; i++)
				{
					if (allInRange[i].RequiresUpdate())
					{
						num++;
						break;
					}
				}
				if (num == 0)
				{
					ListPool<NavmeshCut>.Release(allInRange);
					return;
				}
			}
			bool flag = tileHandler.StartBatchLoad();
			for (int j = 0; j < this.forcedReloadBounds.Count; j++)
			{
				tileHandler.ReloadInBounds(this.forcedReloadBounds[j]);
			}
			for (int k = 0; k < allInRange.Count; k++)
			{
				if (allInRange[k].enabled)
				{
					if (allInRange[k].RequiresUpdate())
					{
						tileHandler.ReloadInBounds(allInRange[k].LastBounds);
						tileHandler.ReloadInBounds(allInRange[k].GetBounds());
					}
				}
				else if (allInRange[k].RequiresUpdate())
				{
					tileHandler.ReloadInBounds(allInRange[k].LastBounds);
				}
			}
			for (int l = 0; l < allInRange.Count; l++)
			{
				if (allInRange[l].RequiresUpdate())
				{
					allInRange[l].NotifyUpdated();
				}
			}
			ListPool<NavmeshCut>.Release(allInRange);
			if (flag)
			{
				tileHandler.EndBatchLoad();
			}
			this.forcedReloadBounds.Clear();
		}

		// Token: 0x04000615 RID: 1557
		private List<TileHandler> handlers = new List<TileHandler>();

		// Token: 0x04000616 RID: 1558
		public float updateInterval;

		// Token: 0x04000617 RID: 1559
		private int updateIndex = -1;

		// Token: 0x04000618 RID: 1560
		private float lastUpdateTime = -999f;

		// Token: 0x04000619 RID: 1561
		private List<Bounds> forcedReloadBounds = new List<Bounds>();
	}
}
