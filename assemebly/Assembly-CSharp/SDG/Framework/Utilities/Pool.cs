using System;
using System.Collections.Generic;

namespace SDG.Framework.Utilities
{
	// Token: 0x02000306 RID: 774
	public class Pool<T>
	{
		// Token: 0x0600161F RID: 5663 RVA: 0x00084625 File Offset: 0x00082A25
		public Pool()
		{
			this.pool = new Queue<T>();
		}

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06001620 RID: 5664 RVA: 0x00084638 File Offset: 0x00082A38
		// (remove) Token: 0x06001621 RID: 5665 RVA: 0x00084670 File Offset: 0x00082A70
		public event Pool<T>.PoolClaimedHandler claimed;

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06001622 RID: 5666 RVA: 0x000846A8 File Offset: 0x00082AA8
		// (remove) Token: 0x06001623 RID: 5667 RVA: 0x000846E0 File Offset: 0x00082AE0
		public event Pool<T>.PoolReleasedHandler released;

		// Token: 0x06001624 RID: 5668 RVA: 0x00084716 File Offset: 0x00082B16
		public void empty()
		{
			this.pool.Clear();
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00084723 File Offset: 0x00082B23
		public void warmup(uint count)
		{
			this.warmup(count, null);
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x00084730 File Offset: 0x00082B30
		public void warmup(uint count, Pool<T>.PoolClaimHandler callback)
		{
			if (callback == null)
			{
				callback = new Pool<T>.PoolClaimHandler(this.handleClaim);
			}
			for (uint num = 0u; num < count; num += 1u)
			{
				T item = callback(this);
				this.release(item);
			}
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00084772 File Offset: 0x00082B72
		public T claim()
		{
			return this.claim(null);
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0008477C File Offset: 0x00082B7C
		public T claim(Pool<T>.PoolClaimHandler callback)
		{
			T t;
			if (this.pool.Count > 0)
			{
				t = this.pool.Dequeue();
			}
			else if (callback != null)
			{
				t = callback(this);
			}
			else
			{
				t = this.handleClaim(this);
			}
			this.triggerClaimed(t);
			return t;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x000847CE File Offset: 0x00082BCE
		public void release(T item)
		{
			this.release(item, null);
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x000847D8 File Offset: 0x00082BD8
		public void release(T item, Pool<T>.PoolReleasedHandler callback)
		{
			if (item == null)
			{
				return;
			}
			if (callback != null)
			{
				callback(this, item);
			}
			this.triggerReleased(item);
			this.pool.Enqueue(item);
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00084807 File Offset: 0x00082C07
		protected T handleClaim(Pool<T> pool)
		{
			return Activator.CreateInstance<T>();
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0008480E File Offset: 0x00082C0E
		protected void triggerClaimed(T item)
		{
			if (this.claimed != null)
			{
				this.claimed(this, item);
			}
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00084828 File Offset: 0x00082C28
		protected void triggerReleased(T item)
		{
			if (this.released != null)
			{
				this.released(this, item);
			}
		}

		// Token: 0x04000C2D RID: 3117
		protected Queue<T> pool;

		// Token: 0x02000307 RID: 775
		// (Invoke) Token: 0x0600162F RID: 5679
		public delegate T PoolClaimHandler(Pool<T> pool);

		// Token: 0x02000308 RID: 776
		// (Invoke) Token: 0x06001633 RID: 5683
		public delegate void PoolReleaseHandler(Pool<T> pool, T item);

		// Token: 0x02000309 RID: 777
		// (Invoke) Token: 0x06001637 RID: 5687
		public delegate void PoolClaimedHandler(Pool<T> pool, T item);

		// Token: 0x0200030A RID: 778
		// (Invoke) Token: 0x0600163B RID: 5691
		public delegate void PoolReleasedHandler(Pool<T> pool, T item);
	}
}
