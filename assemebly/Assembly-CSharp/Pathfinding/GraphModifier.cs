using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000020 RID: 32
	public abstract class GraphModifier : MonoBehaviour
	{
		// Token: 0x0600015F RID: 351 RVA: 0x0000D38C File Offset: 0x0000B78C
		public static void FindAllModifiers()
		{
			GraphModifier[] array = UnityEngine.Object.FindObjectsOfType(typeof(GraphModifier)) as GraphModifier[];
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnEnable();
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000D3CC File Offset: 0x0000B7CC
		public static void TriggerEvent(GraphModifier.EventType type)
		{
			if (!Application.isPlaying)
			{
				GraphModifier.FindAllModifiers();
			}
			GraphModifier graphModifier = GraphModifier.root;
			switch (type)
			{
			case GraphModifier.EventType.PostScan:
				while (graphModifier != null)
				{
					graphModifier.OnPostScan();
					graphModifier = graphModifier.next;
				}
				break;
			case GraphModifier.EventType.PreScan:
				while (graphModifier != null)
				{
					graphModifier.OnPreScan();
					graphModifier = graphModifier.next;
				}
				break;
			default:
				if (type != GraphModifier.EventType.PostUpdate)
				{
					if (type == GraphModifier.EventType.PostCacheLoad)
					{
						while (graphModifier != null)
						{
							graphModifier.OnPostCacheLoad();
							graphModifier = graphModifier.next;
						}
					}
				}
				else
				{
					while (graphModifier != null)
					{
						graphModifier.OnGraphsPostUpdate();
						graphModifier = graphModifier.next;
					}
				}
				break;
			case GraphModifier.EventType.LatePostScan:
				while (graphModifier != null)
				{
					graphModifier.OnLatePostScan();
					graphModifier = graphModifier.next;
				}
				break;
			case GraphModifier.EventType.PreUpdate:
				while (graphModifier != null)
				{
					graphModifier.OnGraphsPreUpdate();
					graphModifier = graphModifier.next;
				}
				break;
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000D4FD File Offset: 0x0000B8FD
		protected virtual void OnEnable()
		{
			this.OnDisable();
			if (GraphModifier.root == null)
			{
				GraphModifier.root = this;
			}
			else
			{
				this.next = GraphModifier.root;
				GraphModifier.root.prev = this;
				GraphModifier.root = this;
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000D53C File Offset: 0x0000B93C
		protected virtual void OnDisable()
		{
			if (GraphModifier.root == this)
			{
				GraphModifier.root = this.next;
				if (GraphModifier.root != null)
				{
					GraphModifier.root.prev = null;
				}
			}
			else
			{
				if (this.prev != null)
				{
					this.prev.next = this.next;
				}
				if (this.next != null)
				{
					this.next.prev = this.prev;
				}
			}
			this.prev = null;
			this.next = null;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000D5D6 File Offset: 0x0000B9D6
		public virtual void OnPostScan()
		{
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000D5D8 File Offset: 0x0000B9D8
		public virtual void OnPreScan()
		{
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000D5DA File Offset: 0x0000B9DA
		public virtual void OnLatePostScan()
		{
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000D5DC File Offset: 0x0000B9DC
		public virtual void OnPostCacheLoad()
		{
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000D5DE File Offset: 0x0000B9DE
		public virtual void OnGraphsPreUpdate()
		{
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000D5E0 File Offset: 0x0000B9E0
		public virtual void OnGraphsPostUpdate()
		{
		}

		// Token: 0x0400013D RID: 317
		private static GraphModifier root;

		// Token: 0x0400013E RID: 318
		private GraphModifier prev;

		// Token: 0x0400013F RID: 319
		private GraphModifier next;

		// Token: 0x02000021 RID: 33
		public enum EventType
		{
			// Token: 0x04000141 RID: 321
			PostScan = 1,
			// Token: 0x04000142 RID: 322
			PreScan,
			// Token: 0x04000143 RID: 323
			LatePostScan = 4,
			// Token: 0x04000144 RID: 324
			PreUpdate = 8,
			// Token: 0x04000145 RID: 325
			PostUpdate = 16,
			// Token: 0x04000146 RID: 326
			PostCacheLoad = 32
		}
	}
}
