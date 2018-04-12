using System;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020000B7 RID: 183
	public class Utility
	{
		// Token: 0x06000647 RID: 1607 RVA: 0x0003E87C File Offset: 0x0003CC7C
		public static Color GetColor(int i)
		{
			while (i >= Utility.colors.Length)
			{
				i -= Utility.colors.Length;
			}
			while (i < 0)
			{
				i += Utility.colors.Length;
			}
			return Utility.colors[i];
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0003E8CD File Offset: 0x0003CCCD
		public static int Bit(int a, int b)
		{
			return (a & 1 << b) >> b;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0003E8DC File Offset: 0x0003CCDC
		public static Color IntToColor(int i, float a)
		{
			int num = Utility.Bit(i, 1) + Utility.Bit(i, 3) * 2 + 1;
			int num2 = Utility.Bit(i, 2) + Utility.Bit(i, 4) * 2 + 1;
			int num3 = Utility.Bit(i, 0) + Utility.Bit(i, 5) * 2 + 1;
			return new Color((float)num * 0.25f, (float)num2 * 0.25f, (float)num3 * 0.25f, a);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0003E944 File Offset: 0x0003CD44
		public static float TriangleArea2(Vector3 a, Vector3 b, Vector3 c)
		{
			return Mathf.Abs(a.x * b.z + b.x * c.z + c.x * a.z - a.x * c.z - c.x * b.z - b.x * a.z);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0003E9B8 File Offset: 0x0003CDB8
		public static float TriangleArea(Vector3 a, Vector3 b, Vector3 c)
		{
			return (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0003EA04 File Offset: 0x0003CE04
		public static float Min(float a, float b, float c)
		{
			a = ((a >= b) ? b : a);
			return (a >= c) ? c : a;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0003EA24 File Offset: 0x0003CE24
		public static float Max(float a, float b, float c)
		{
			a = ((a <= b) ? b : a);
			return (a <= c) ? c : a;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0003EA44 File Offset: 0x0003CE44
		public static int Max(int a, int b, int c, int d)
		{
			a = ((a <= b) ? b : a);
			a = ((a <= c) ? c : a);
			return (a <= d) ? d : a;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0003EA74 File Offset: 0x0003CE74
		public static int Min(int a, int b, int c, int d)
		{
			a = ((a >= b) ? b : a);
			a = ((a >= c) ? c : a);
			return (a >= d) ? d : a;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0003EAA4 File Offset: 0x0003CEA4
		public static float Max(float a, float b, float c, float d)
		{
			a = ((a <= b) ? b : a);
			a = ((a <= c) ? c : a);
			return (a <= d) ? d : a;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0003EAD4 File Offset: 0x0003CED4
		public static float Min(float a, float b, float c, float d)
		{
			a = ((a >= b) ? b : a);
			a = ((a >= c) ? c : a);
			return (a >= d) ? d : a;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0003EB04 File Offset: 0x0003CF04
		public static string ToMillis(float v)
		{
			return (v * 1000f).ToString("0");
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0003EB25 File Offset: 0x0003CF25
		public static void StartTimer()
		{
			Utility.lastStartTime = Time.realtimeSinceStartup;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0003EB31 File Offset: 0x0003CF31
		public static void EndTimer(string label)
		{
			Debug.Log(label + ", process took " + Utility.ToMillis(Time.realtimeSinceStartup - Utility.lastStartTime) + "ms to complete");
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0003EB58 File Offset: 0x0003CF58
		public static void StartTimerAdditive(bool reset)
		{
			if (reset)
			{
				Utility.additiveTimer = 0f;
			}
			Utility.lastAdditiveTimerStart = Time.realtimeSinceStartup;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0003EB74 File Offset: 0x0003CF74
		public static void EndTimerAdditive(string label, bool log)
		{
			Utility.additiveTimer += Time.realtimeSinceStartup - Utility.lastAdditiveTimerStart;
			if (log)
			{
				Debug.Log(label + ", process took " + Utility.ToMillis(Utility.additiveTimer) + "ms to complete");
			}
			Utility.lastAdditiveTimerStart = Time.realtimeSinceStartup;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0003EBC6 File Offset: 0x0003CFC6
		public static void CopyVector(float[] a, int i, Vector3 v)
		{
			a[i] = v.x;
			a[i + 1] = v.y;
			a[i + 2] = v.z;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0003EBEC File Offset: 0x0003CFEC
		public static int ClipPoly(float[] vIn, int n, float[] vOut, float pnx, float pnz, float pd)
		{
			float[] array = Utility.clipPolygonCache;
			for (int i = 0; i < n; i++)
			{
				array[i] = pnx * vIn[i * 3] + pnz * vIn[i * 3 + 2] + pd;
			}
			int num = 0;
			int j = 0;
			int num2 = n - 1;
			while (j < n)
			{
				bool flag = array[num2] >= 0f;
				bool flag2 = array[j] >= 0f;
				if (flag != flag2)
				{
					float num3 = array[num2] / (array[num2] - array[j]);
					vOut[num * 3] = vIn[num2 * 3] + (vIn[j * 3] - vIn[num2 * 3]) * num3;
					vOut[num * 3 + 1] = vIn[num2 * 3 + 1] + (vIn[j * 3 + 1] - vIn[num2 * 3 + 1]) * num3;
					vOut[num * 3 + 2] = vIn[num2 * 3 + 2] + (vIn[j * 3 + 2] - vIn[num2 * 3 + 2]) * num3;
					num++;
				}
				if (flag2)
				{
					vOut[num * 3] = vIn[j * 3];
					vOut[num * 3 + 1] = vIn[j * 3 + 1];
					vOut[num * 3 + 2] = vIn[j * 3 + 2];
					num++;
				}
				num2 = j;
				j++;
			}
			return num;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0003ED14 File Offset: 0x0003D114
		public static int ClipPolygon(float[] vIn, int n, float[] vOut, float multi, float offset, int axis)
		{
			float[] array = Utility.clipPolygonCache;
			for (int i = 0; i < n; i++)
			{
				array[i] = multi * vIn[i * 3 + axis] + offset;
			}
			int num = 0;
			int j = 0;
			int num2 = n - 1;
			while (j < n)
			{
				bool flag = array[num2] >= 0f;
				bool flag2 = array[j] >= 0f;
				if (flag != flag2)
				{
					int num3 = num * 3;
					int num4 = j * 3;
					int num5 = num2 * 3;
					float num6 = array[num2] / (array[num2] - array[j]);
					vOut[num3] = vIn[num5] + (vIn[num4] - vIn[num5]) * num6;
					vOut[num3 + 1] = vIn[num5 + 1] + (vIn[num4 + 1] - vIn[num5 + 1]) * num6;
					vOut[num3 + 2] = vIn[num5 + 2] + (vIn[num4 + 2] - vIn[num5 + 2]) * num6;
					num++;
				}
				if (flag2)
				{
					int num7 = num * 3;
					int num8 = j * 3;
					vOut[num7] = vIn[num8];
					vOut[num7 + 1] = vIn[num8 + 1];
					vOut[num7 + 2] = vIn[num8 + 2];
					num++;
				}
				num2 = j;
				j++;
			}
			return num;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0003EE38 File Offset: 0x0003D238
		public static int ClipPolygonY(float[] vIn, int n, float[] vOut, float multi, float offset, int axis)
		{
			float[] array = Utility.clipPolygonCache;
			for (int i = 0; i < n; i++)
			{
				array[i] = multi * vIn[i * 3 + axis] + offset;
			}
			int num = 0;
			int j = 0;
			int num2 = n - 1;
			while (j < n)
			{
				bool flag = array[num2] >= 0f;
				bool flag2 = array[j] >= 0f;
				if (flag != flag2)
				{
					vOut[num * 3 + 1] = vIn[num2 * 3 + 1] + (vIn[j * 3 + 1] - vIn[num2 * 3 + 1]) * (array[num2] / (array[num2] - array[j]));
					num++;
				}
				if (flag2)
				{
					vOut[num * 3 + 1] = vIn[j * 3 + 1];
					num++;
				}
				num2 = j;
				j++;
			}
			return num;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0003EF00 File Offset: 0x0003D300
		public static int ClipPolygon(Vector3[] vIn, int n, Vector3[] vOut, float multi, float offset, int axis)
		{
			float[] array = Utility.clipPolygonCache;
			for (int i = 0; i < n; i++)
			{
				array[i] = multi * vIn[i][axis] + offset;
			}
			int num = 0;
			int j = 0;
			int num2 = n - 1;
			while (j < n)
			{
				bool flag = array[num2] >= 0f;
				bool flag2 = array[j] >= 0f;
				if (flag != flag2)
				{
					float d = array[num2] / (array[num2] - array[j]);
					vOut[num] = vIn[num2] + (vIn[j] - vIn[num2]) * d;
					num++;
				}
				if (flag2)
				{
					vOut[num] = vIn[j];
					num++;
				}
				num2 = j;
				j++;
			}
			return num;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0003EFFC File Offset: 0x0003D3FC
		public static int ClipPolygon(Int3[] vIn, int n, Int3[] vOut, int multi, int offset, int axis)
		{
			int[] array = Utility.clipPolygonIntCache;
			for (int i = 0; i < n; i++)
			{
				array[i] = multi * vIn[i][axis] + offset;
			}
			int num = 0;
			int j = 0;
			int num2 = n - 1;
			while (j < n)
			{
				bool flag = array[num2] >= 0;
				bool flag2 = array[j] >= 0;
				if (flag != flag2)
				{
					double rhs = (double)array[num2] / (double)(array[num2] - array[j]);
					vOut[num] = vIn[num2] + (vIn[j] - vIn[num2]) * rhs;
					num++;
				}
				if (flag2)
				{
					vOut[num] = vIn[j];
					num++;
				}
				num2 = j;
				j++;
			}
			return num;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0003F0F0 File Offset: 0x0003D4F0
		public static bool IntersectXAxis(out Vector3 intersection, Vector3 start1, Vector3 dir1, float x)
		{
			float x2 = dir1.x;
			if (x2 == 0f)
			{
				intersection = Vector3.zero;
				return false;
			}
			float num = x - start1.x;
			float num2 = num / x2;
			num2 = Mathf.Clamp01(num2);
			intersection = start1 + dir1 * num2;
			return true;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0003F148 File Offset: 0x0003D548
		public static bool IntersectZAxis(out Vector3 intersection, Vector3 start1, Vector3 dir1, float z)
		{
			float num = -dir1.z;
			if (num == 0f)
			{
				intersection = Vector3.zero;
				return false;
			}
			float num2 = start1.z - z;
			float num3 = num2 / num;
			num3 = Mathf.Clamp01(num3);
			intersection = start1 + dir1 * num3;
			return true;
		}

		// Token: 0x04000533 RID: 1331
		public static Color[] colors = new Color[]
		{
			Color.green,
			Color.blue,
			Color.red,
			Color.yellow,
			Color.cyan,
			Color.white,
			Color.black
		};

		// Token: 0x04000534 RID: 1332
		public static float lastStartTime;

		// Token: 0x04000535 RID: 1333
		public static float lastAdditiveTimerStart;

		// Token: 0x04000536 RID: 1334
		public static float additiveTimer;

		// Token: 0x04000537 RID: 1335
		private static float[] clipPolygonCache = new float[21];

		// Token: 0x04000538 RID: 1336
		private static int[] clipPolygonIntCache = new int[21];
	}
}
