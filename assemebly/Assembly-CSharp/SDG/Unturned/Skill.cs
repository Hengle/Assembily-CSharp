using System;

namespace SDG.Unturned
{
	// Token: 0x0200064F RID: 1615
	public class Skill
	{
		// Token: 0x06002EB6 RID: 11958 RVA: 0x00131C84 File Offset: 0x00130084
		public Skill(byte newLevel, byte newMax, uint newCost, float newDifficulty)
		{
			this.level = newLevel;
			this.max = newMax;
			this._cost = newCost;
			this.difficulty = newDifficulty;
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002EB7 RID: 11959 RVA: 0x00131CA9 File Offset: 0x001300A9
		public float mastery
		{
			get
			{
				if (this.level == 0)
				{
					return 0f;
				}
				if (this.level >= this.max)
				{
					return 1f;
				}
				return (float)this.level / (float)this.max;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002EB8 RID: 11960 RVA: 0x00131CE2 File Offset: 0x001300E2
		public uint cost
		{
			get
			{
				return (uint)(this._cost * ((float)this.level * this.difficulty + 1f));
			}
		}

		// Token: 0x04001E3F RID: 7743
		public byte level;

		// Token: 0x04001E40 RID: 7744
		public byte max;

		// Token: 0x04001E41 RID: 7745
		private uint _cost;

		// Token: 0x04001E42 RID: 7746
		private float difficulty;
	}
}
