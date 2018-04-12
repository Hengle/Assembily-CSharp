using System;

namespace Pathfinding
{
	// Token: 0x020000CA RID: 202
	[Serializable]
	public abstract class PathModifier : IPathModifier
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060006BC RID: 1724
		public abstract ModifierData input { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060006BD RID: 1725
		public abstract ModifierData output { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x000427DE File Offset: 0x00040BDE
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x000427E6 File Offset: 0x00040BE6
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

		// Token: 0x060006C0 RID: 1728 RVA: 0x000427EF File Offset: 0x00040BEF
		public void Awake(Seeker s)
		{
			this.seeker = s;
			if (s != null)
			{
				s.RegisterModifier(this);
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0004280B File Offset: 0x00040C0B
		public void OnDestroy(Seeker s)
		{
			if (s != null)
			{
				s.DeregisterModifier(this);
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00042820 File Offset: 0x00040C20
		[Obsolete]
		public virtual void ApplyOriginal(Path p)
		{
		}

		// Token: 0x060006C3 RID: 1731
		public abstract void Apply(Path p, ModifierData source);

		// Token: 0x060006C4 RID: 1732 RVA: 0x00042822 File Offset: 0x00040C22
		[Obsolete]
		public virtual void PreProcess(Path p)
		{
		}

		// Token: 0x040005AC RID: 1452
		public int priority;

		// Token: 0x040005AD RID: 1453
		[NonSerialized]
		public Seeker seeker;
	}
}
