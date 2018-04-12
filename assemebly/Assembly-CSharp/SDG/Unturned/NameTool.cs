using System;

namespace SDG.Unturned
{
	// Token: 0x0200073E RID: 1854
	public class NameTool
	{
		// Token: 0x0600344A RID: 13386 RVA: 0x00156667 File Offset: 0x00154A67
		public static bool checkNames(string input, string name)
		{
			return input.Length <= name.Length && name.ToLower().IndexOf(input.ToLower()) != -1;
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x00156694 File Offset: 0x00154A94
		public static bool isValid(string name)
		{
			foreach (int num in name.ToCharArray())
			{
				if (num <= 31)
				{
					return false;
				}
				if (num >= 126)
				{
					return false;
				}
				if (num == 47 || num == 92 || num == 96)
				{
					return false;
				}
			}
			return true;
		}
	}
}
