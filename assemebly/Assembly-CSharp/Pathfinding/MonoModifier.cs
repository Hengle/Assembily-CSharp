using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000CB RID: 203
	[Serializable]
	public abstract class MonoModifier : MonoBehaviour, IPathModifier
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x00040AAF File Offset: 0x0003EEAF
		public void OnEnable()
		{
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00040AB1 File Offset: 0x0003EEB1
		public void OnDisable()
		{
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00040AB3 File Offset: 0x0003EEB3
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x00040ABB File Offset: 0x0003EEBB
		public int Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.priority = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060006CA RID: 1738
		public abstract ModifierData input { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060006CB RID: 1739
		public abstract ModifierData output { get; }

		// Token: 0x060006CC RID: 1740 RVA: 0x00040AC4 File Offset: 0x0003EEC4
		public void Awake()
		{
			this.seeker = base.GetComponent<Seeker>();
			if (this.seeker != null)
			{
				this.seeker.RegisterModifier(this);
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00040AEF File Offset: 0x0003EEEF
		public void OnDestroy()
		{
			if (this.seeker != null)
			{
				this.seeker.DeregisterModifier(this);
			}
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00040B0E File Offset: 0x0003EF0E
		[Obsolete]
		public virtual void ApplyOriginal(Path p)
		{
		}

		// Token: 0x060006CF RID: 1743
		public abstract void Apply(Path p, ModifierData source);

		// Token: 0x060006D0 RID: 1744 RVA: 0x00040B10 File Offset: 0x0003EF10
		[Obsolete]
		public virtual void PreProcess(Path p)
		{
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00040B14 File Offset: 0x0003EF14
		[Obsolete]
		public virtual Vector3[] Apply(GraphNode[] path, Vector3 start, Vector3 end, int startIndex, int endIndex, NavGraph graph)
		{
			Vector3[] array = new Vector3[endIndex - startIndex];
			for (int i = startIndex; i < endIndex; i++)
			{
				array[i - startIndex] = (Vector3)path[i].position;
			}
			return array;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00040B5D File Offset: 0x0003EF5D
		[Obsolete]
		public virtual Vector3[] Apply(Vector3[] path, Vector3 start, Vector3 end)
		{
			return path;
		}

		// Token: 0x040005AE RID: 1454
		[NonSerialized]
		public Seeker seeker;

		// Token: 0x040005AF RID: 1455
		public int priority;
	}
}
