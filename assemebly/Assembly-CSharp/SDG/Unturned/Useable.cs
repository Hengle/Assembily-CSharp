using System;

namespace SDG.Unturned
{
	// Token: 0x020007AB RID: 1963
	public class Useable : PlayerCaller
	{
		// Token: 0x06003905 RID: 14597 RVA: 0x001A2F20 File Offset: 0x001A1320
		public virtual void startPrimary()
		{
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x001A2F22 File Offset: 0x001A1322
		public virtual void stopPrimary()
		{
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x001A2F24 File Offset: 0x001A1324
		public virtual void startSecondary()
		{
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x001A2F26 File Offset: 0x001A1326
		public virtual void stopSecondary()
		{
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06003909 RID: 14601 RVA: 0x001A2F28 File Offset: 0x001A1328
		public virtual bool canInspect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x001A2F2B File Offset: 0x001A132B
		public virtual void equip()
		{
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x001A2F2D File Offset: 0x001A132D
		public virtual void dequip()
		{
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x001A2F2F File Offset: 0x001A132F
		public virtual void tick()
		{
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x001A2F31 File Offset: 0x001A1331
		public virtual void simulate(uint simulation, bool inputSteady)
		{
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x001A2F33 File Offset: 0x001A1333
		public virtual void tock(uint clock)
		{
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x001A2F35 File Offset: 0x001A1335
		public virtual void updateState(byte[] newState)
		{
		}
	}
}
