using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000C5 RID: 197
	[AddComponentMenu("Pathfinding/Modifiers/Alternative Path")]
	[Serializable]
	public class AlternativePath : MonoModifier
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00041FC9 File Offset: 0x000403C9
		public override ModifierData input
		{
			get
			{
				return ModifierData.Original;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00041FCD File Offset: 0x000403CD
		public override ModifierData output
		{
			get
			{
				return ModifierData.All;
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00041FD0 File Offset: 0x000403D0
		public override void Apply(Path p, ModifierData source)
		{
			if (this == null)
			{
				return;
			}
			object obj = this.lockObject;
			lock (obj)
			{
				this.toBeApplied = p.path.ToArray();
				if (!this.waitingForApply)
				{
					this.waitingForApply = true;
					AstarPath.OnPathPreSearch = (OnPathDelegate)Delegate.Combine(AstarPath.OnPathPreSearch, new OnPathDelegate(this.ApplyNow));
				}
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00042058 File Offset: 0x00040458
		public new void OnDestroy()
		{
			this.destroyed = true;
			object obj = this.lockObject;
			lock (obj)
			{
				if (!this.waitingForApply)
				{
					this.waitingForApply = true;
					AstarPath.OnPathPreSearch = (OnPathDelegate)Delegate.Combine(AstarPath.OnPathPreSearch, new OnPathDelegate(this.ClearOnDestroy));
				}
			}
			this.OnDestroy();
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x000420D0 File Offset: 0x000404D0
		private void ClearOnDestroy(Path p)
		{
			object obj = this.lockObject;
			lock (obj)
			{
				AstarPath.OnPathPreSearch = (OnPathDelegate)Delegate.Remove(AstarPath.OnPathPreSearch, new OnPathDelegate(this.ClearOnDestroy));
				this.waitingForApply = false;
				this.InversePrevious();
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00042134 File Offset: 0x00040534
		private void InversePrevious()
		{
			int seed = this.prevSeed;
			this.rnd = new System.Random(seed);
			if (this.prevNodes != null)
			{
				bool flag = false;
				int num = this.rnd.Next(this.randomStep);
				for (int i = num; i < this.prevNodes.Length; i += this.rnd.Next(1, this.randomStep))
				{
					if ((ulong)this.prevNodes[i].Penalty < (ulong)((long)this.prevPenalty))
					{
						flag = true;
					}
					this.prevNodes[i].Penalty = (uint)((ulong)this.prevNodes[i].Penalty - (ulong)((long)this.prevPenalty));
				}
				if (flag)
				{
					Debug.LogWarning("Penalty for some nodes has been reset while this modifier was active. Penalties might not be correctly set.");
				}
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000421F0 File Offset: 0x000405F0
		private void ApplyNow(Path somePath)
		{
			object obj = this.lockObject;
			lock (obj)
			{
				this.waitingForApply = false;
				AstarPath.OnPathPreSearch = (OnPathDelegate)Delegate.Remove(AstarPath.OnPathPreSearch, new OnPathDelegate(this.ApplyNow));
				this.InversePrevious();
				if (!this.destroyed)
				{
					int seed = this.seedGenerator.Next();
					this.rnd = new System.Random(seed);
					if (this.toBeApplied != null)
					{
						int num = this.rnd.Next(this.randomStep);
						for (int i = num; i < this.toBeApplied.Length; i += this.rnd.Next(1, this.randomStep))
						{
							this.toBeApplied[i].Penalty = (uint)((ulong)this.toBeApplied[i].Penalty + (ulong)((long)this.penalty));
						}
					}
					this.prevPenalty = this.penalty;
					this.prevSeed = seed;
					this.prevNodes = this.toBeApplied;
				}
			}
		}

		// Token: 0x04000597 RID: 1431
		public int penalty = 1000;

		// Token: 0x04000598 RID: 1432
		public int randomStep = 10;

		// Token: 0x04000599 RID: 1433
		private GraphNode[] prevNodes;

		// Token: 0x0400059A RID: 1434
		private int prevSeed;

		// Token: 0x0400059B RID: 1435
		private int prevPenalty;

		// Token: 0x0400059C RID: 1436
		private bool waitingForApply;

		// Token: 0x0400059D RID: 1437
		private object lockObject = new object();

		// Token: 0x0400059E RID: 1438
		private System.Random rnd = new System.Random();

		// Token: 0x0400059F RID: 1439
		private System.Random seedGenerator = new System.Random();

		// Token: 0x040005A0 RID: 1440
		private bool destroyed;

		// Token: 0x040005A1 RID: 1441
		private GraphNode[] toBeApplied;
	}
}
