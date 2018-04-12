using System;

namespace SDG.Unturned
{
	// Token: 0x02000416 RID: 1046
	public class NPCTimeOfDayCondition : NPCLogicCondition
	{
		// Token: 0x06001C2B RID: 7211 RVA: 0x0009958B File Offset: 0x0009798B
		public NPCTimeOfDayCondition(int newSecond, ENPCLogicType newLogicType, string newText, bool newShouldReset) : base(newLogicType, newText, newShouldReset)
		{
			this.second = newSecond;
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001C2C RID: 7212 RVA: 0x0009959E File Offset: 0x0009799E
		// (set) Token: 0x06001C2D RID: 7213 RVA: 0x000995A6 File Offset: 0x000979A6
		public int second { get; protected set; }

		// Token: 0x06001C2E RID: 7214 RVA: 0x000995B0 File Offset: 0x000979B0
		public override bool isConditionMet(Player player)
		{
			float num;
			if (LightingManager.day < LevelLighting.bias)
			{
				num = LightingManager.day / LevelLighting.bias;
				num /= 2f;
			}
			else
			{
				num = (LightingManager.day - LevelLighting.bias) / (1f - LevelLighting.bias);
				num = 0.5f + num / 2f;
			}
			int a = (int)(num * 86400f);
			return base.doesLogicPass<int>(a, this.second);
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x00099624 File Offset: 0x00097A24
		public override string formatCondition(Player player)
		{
			if (string.IsNullOrEmpty(this.text))
			{
				return null;
			}
			int num = this.second / 3600;
			int num2 = this.second / 60 - num * 60;
			int num3 = this.second - num * 3600 - num2 * 60;
			string arg = string.Format("{0:D2}:{1:D2}:{2:D2}", num, num2, num3);
			return string.Format(this.text, arg);
		}
	}
}
