using System;

namespace SDG.Unturned
{
	// Token: 0x02000646 RID: 1606
	public class SpecialitySkillPair
	{
		// Token: 0x06002E2D RID: 11821 RVA: 0x001281C3 File Offset: 0x001265C3
		public SpecialitySkillPair(int newSpeciality, int newSkill)
		{
			this.speciality = newSpeciality;
			this.skill = newSkill;
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002E2E RID: 11822 RVA: 0x001281D9 File Offset: 0x001265D9
		// (set) Token: 0x06002E2F RID: 11823 RVA: 0x001281E1 File Offset: 0x001265E1
		public int speciality { get; private set; }

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002E30 RID: 11824 RVA: 0x001281EA File Offset: 0x001265EA
		// (set) Token: 0x06002E31 RID: 11825 RVA: 0x001281F2 File Offset: 0x001265F2
		public int skill { get; private set; }
	}
}
