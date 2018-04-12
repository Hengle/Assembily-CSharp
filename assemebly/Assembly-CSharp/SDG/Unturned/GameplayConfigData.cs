using System;

namespace SDG.Unturned
{
	// Token: 0x020006AD RID: 1709
	public class GameplayConfigData
	{
		// Token: 0x060031CC RID: 12748 RVA: 0x00143D9C File Offset: 0x0014219C
		public GameplayConfigData(EGameMode mode)
		{
			this.Repair_Level_Max = 3u;
			if (mode != EGameMode.HARD)
			{
				this.Hitmarkers = true;
				this.Crosshair = true;
			}
			else
			{
				this.Hitmarkers = false;
				this.Crosshair = false;
			}
			if (mode != EGameMode.EASY)
			{
				this.Ballistics = true;
			}
			else
			{
				this.Ballistics = false;
			}
			this.Chart = (mode == EGameMode.EASY);
			this.Satellite = false;
			this.Compass = false;
			this.Group_Map = (mode != EGameMode.HARD);
			this.Group_HUD = true;
			this.Allow_Static_Groups = true;
			this.Allow_Dynamic_Groups = true;
			this.Allow_Shoulder_Camera = true;
			this.Can_Suicide = true;
			this.Timer_Exit = 10u;
			this.Timer_Respawn = 10u;
			this.Timer_Home = 30u;
			this.Max_Group_Members = 0u;
		}

		// Token: 0x0400219E RID: 8606
		public uint Repair_Level_Max;

		// Token: 0x0400219F RID: 8607
		public bool Hitmarkers;

		// Token: 0x040021A0 RID: 8608
		public bool Crosshair;

		// Token: 0x040021A1 RID: 8609
		public bool Ballistics;

		// Token: 0x040021A2 RID: 8610
		public bool Chart;

		// Token: 0x040021A3 RID: 8611
		public bool Satellite;

		// Token: 0x040021A4 RID: 8612
		public bool Compass;

		// Token: 0x040021A5 RID: 8613
		public bool Group_Map;

		// Token: 0x040021A6 RID: 8614
		public bool Group_HUD;

		// Token: 0x040021A7 RID: 8615
		public bool Allow_Static_Groups;

		// Token: 0x040021A8 RID: 8616
		public bool Allow_Dynamic_Groups;

		// Token: 0x040021A9 RID: 8617
		public bool Allow_Shoulder_Camera;

		// Token: 0x040021AA RID: 8618
		public bool Can_Suicide;

		// Token: 0x040021AB RID: 8619
		public uint Timer_Exit;

		// Token: 0x040021AC RID: 8620
		public uint Timer_Respawn;

		// Token: 0x040021AD RID: 8621
		public uint Timer_Home;

		// Token: 0x040021AE RID: 8622
		public uint Max_Group_Members;
	}
}
