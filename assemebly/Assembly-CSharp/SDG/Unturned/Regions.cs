using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200068E RID: 1678
	public class Regions
	{
		// Token: 0x060030C6 RID: 12486 RVA: 0x0013F6EC File Offset: 0x0013DAEC
		public static void getRegionsInRadius(Vector3 center, float radius, List<RegionCoordinate> result)
		{
			byte b;
			byte b2;
			if (!Regions.tryGetCoordinate(center, out b, out b2))
			{
				return;
			}
			result.Add(new RegionCoordinate(b, b2));
			Vector3 vector;
			Regions.tryGetPoint((int)b, (int)b2, out vector);
			byte b3 = b;
			byte b4 = b2;
			if (center.x - radius <= vector.x)
			{
				b3 -= 1;
			}
			else if (center.x + radius >= vector.x + (float)Regions.REGION_SIZE)
			{
				b3 += 1;
			}
			if (center.z - radius <= vector.z)
			{
				b4 -= 1;
			}
			else if (center.z + radius >= vector.z + (float)Regions.REGION_SIZE)
			{
				b4 += 1;
			}
			if (b3 != b && Regions.checkSafe((int)b3, (int)b2))
			{
				result.Add(new RegionCoordinate(b3, b2));
			}
			if (b4 != b2 && Regions.checkSafe((int)b, (int)b4))
			{
				result.Add(new RegionCoordinate(b, b4));
			}
			if (b3 != b && b4 != b2 && Regions.checkSafe((int)b3, (int)b4))
			{
				result.Add(new RegionCoordinate(b3, b4));
			}
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x0013F818 File Offset: 0x0013DC18
		public static bool tryGetCoordinate(Vector3 point, out byte x, out byte y)
		{
			x = byte.MaxValue;
			y = byte.MaxValue;
			if (Regions.checkSafe(point))
			{
				x = (byte)((point.x + 4096f) / (float)Regions.REGION_SIZE);
				y = (byte)((point.z + 4096f) / (float)Regions.REGION_SIZE);
				return true;
			}
			return false;
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x0013F870 File Offset: 0x0013DC70
		public static bool tryGetPoint(int x, int y, out Vector3 point)
		{
			point = Vector3.zero;
			if (Regions.checkSafe(x, y))
			{
				point.x = (float)(x * (int)Regions.REGION_SIZE - 4096);
				point.z = (float)(y * (int)Regions.REGION_SIZE - 4096);
				return true;
			}
			return false;
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x0013F8C0 File Offset: 0x0013DCC0
		public static bool checkSafe(Vector3 point)
		{
			return point.x >= -4096f && point.z >= -4096f && point.x < 4096f && point.z < 4096f;
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x0013F914 File Offset: 0x0013DD14
		public static bool checkSafe(int x, int y)
		{
			return x >= 0 && y >= 0 && x < (int)Regions.WORLD_SIZE && y < (int)Regions.WORLD_SIZE;
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x0013F93D File Offset: 0x0013DD3D
		public static bool checkArea(byte x_0, byte y_0, byte x_1, byte y_1, byte area)
		{
			return x_0 >= x_1 - area && y_0 >= y_1 - area && x_0 <= x_1 + area && y_0 <= y_1 + area;
		}

		// Token: 0x04002035 RID: 8245
		public static readonly byte WORLD_SIZE = 64;

		// Token: 0x04002036 RID: 8246
		public static readonly byte REGION_SIZE = (byte)(8192 / (int)Regions.WORLD_SIZE);
	}
}
