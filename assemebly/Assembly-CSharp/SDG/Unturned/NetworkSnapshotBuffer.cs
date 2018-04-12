using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005DD RID: 1501
	public class NetworkSnapshotBuffer
	{
		// Token: 0x06002A51 RID: 10833 RVA: 0x00107C6C File Offset: 0x0010606C
		public NetworkSnapshotBuffer(float newDuration, float newDelay)
		{
			this.snapshots = new NetworkSnapshot[8];
			this.readIndex = 0;
			this.readCount = 0;
			this.writeIndex = 0;
			this.writeCount = 0;
			this.readDuration = newDuration;
			this.readDelay = newDelay;
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002A52 RID: 10834 RVA: 0x00107CAA File Offset: 0x001060AA
		// (set) Token: 0x06002A53 RID: 10835 RVA: 0x00107CB2 File Offset: 0x001060B2
		public NetworkSnapshot[] snapshots { get; private set; }

		// Token: 0x06002A54 RID: 10836 RVA: 0x00107CBC File Offset: 0x001060BC
		public ISnapshotInfo getCurrentSnapshot()
		{
			int num = this.writeCount - this.readCount;
			if (num <= 0)
			{
				this.readLast = Time.realtimeSinceStartup;
				return this.lastInfo;
			}
			if (num > 4)
			{
				if (this.writeIndex == 0)
				{
					this.readIndex = this.snapshots.Length - 1;
				}
				else
				{
					this.readIndex = this.writeIndex - 1;
				}
				this.readCount = this.writeCount - 1;
				this.lastInfo = this.snapshots[this.readIndex].info;
				this.readLast = Time.realtimeSinceStartup;
				return this.lastInfo;
			}
			if (Time.realtimeSinceStartup - this.readLast > this.readDuration && num > 1)
			{
				this.lastInfo = this.snapshots[this.readIndex].info;
				this.readLast += this.readDuration;
				this.incrementReadIndex();
			}
			if (Time.realtimeSinceStartup - this.snapshots[this.readIndex].timestamp < this.readDelay)
			{
				this.readLast = Time.realtimeSinceStartup;
				return this.lastInfo;
			}
			float delta = Mathf.Clamp01((Time.realtimeSinceStartup - this.readLast) / this.readDuration);
			return this.lastInfo.lerp(this.snapshots[this.readIndex].info, delta);
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x00107E28 File Offset: 0x00106228
		public void updateLastSnapshot(ISnapshotInfo info)
		{
			this.readIndex = 0;
			this.readCount = 0;
			this.writeIndex = 0;
			this.writeCount = 0;
			this.lastInfo = info;
			this.readLast = Time.realtimeSinceStartup;
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x00107E58 File Offset: 0x00106258
		public void addNewSnapshot(ISnapshotInfo info)
		{
			this.snapshots[this.writeIndex].info = info;
			this.snapshots[this.writeIndex].timestamp = Time.realtimeSinceStartup;
			this.incrementWriteIndex();
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x00107E92 File Offset: 0x00106292
		private void incrementReadIndex()
		{
			this.readIndex++;
			if (this.readIndex == this.snapshots.Length)
			{
				this.readIndex = 0;
			}
			this.readCount++;
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x00107ECA File Offset: 0x001062CA
		private void incrementWriteIndex()
		{
			this.writeIndex++;
			if (this.writeIndex == this.snapshots.Length)
			{
				this.writeIndex = 0;
			}
			this.writeCount++;
		}

		// Token: 0x04001A4B RID: 6731
		private int readIndex;

		// Token: 0x04001A4C RID: 6732
		private int readCount;

		// Token: 0x04001A4D RID: 6733
		private int writeIndex;

		// Token: 0x04001A4E RID: 6734
		private int writeCount;

		// Token: 0x04001A4F RID: 6735
		private ISnapshotInfo lastInfo;

		// Token: 0x04001A50 RID: 6736
		private float readLast;

		// Token: 0x04001A51 RID: 6737
		private float readDuration;

		// Token: 0x04001A52 RID: 6738
		private float readDelay;
	}
}
