using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200000D RID: 13
	internal class AstarSplines
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000074B8 File Offset: 0x000058B8
		public static Vector3 CatmullRom(Vector3 previous, Vector3 start, Vector3 end, Vector3 next, float elapsedTime)
		{
			float num = elapsedTime * elapsedTime;
			float num2 = num * elapsedTime;
			return previous * (-0.5f * num2 + num - 0.5f * elapsedTime) + start * (1.5f * num2 + -2.5f * num + 1f) + end * (-1.5f * num2 + 2f * num + 0.5f * elapsedTime) + next * (0.5f * num2 - 0.5f * num);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00007544 File Offset: 0x00005944
		public static Vector3 CatmullRomOLD(Vector3 previous, Vector3 start, Vector3 end, Vector3 next, float elapsedTime)
		{
			float num = elapsedTime * elapsedTime;
			float num2 = num * elapsedTime;
			return previous * (-0.5f * num2 + num - 0.5f * elapsedTime) + start * (1.5f * num2 + -2.5f * num + 1f) + end * (-1.5f * num2 + 2f * num + 0.5f * elapsedTime) + next * (0.5f * num2 - 0.5f * num);
		}
	}
}
