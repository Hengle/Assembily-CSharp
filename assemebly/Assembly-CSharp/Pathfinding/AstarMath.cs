using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200000E RID: 14
	public class AstarMath
	{
		// Token: 0x06000088 RID: 136 RVA: 0x000075D8 File Offset: 0x000059D8
		public static int ComputeVertexHash(int x, int y, int z)
		{
			uint num = 2376512323u;
			uint num2 = 3625334849u;
			uint num3 = 3407524639u;
			uint num4 = (uint)((ulong)num * (ulong)((long)x) + (ulong)num2 * (ulong)((long)y) + (ulong)num3 * (ulong)((long)z));
			return (int)(num4 & 1073741823u);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00007614 File Offset: 0x00005A14
		public static Vector3 NearestPoint(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			Vector3 vector = Vector3.Normalize(lineEnd - lineStart);
			float d = Vector3.Dot(point - lineStart, vector);
			return lineStart + d * vector;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000764C File Offset: 0x00005A4C
		public static float NearestPointFactor(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			Vector3 vector = lineEnd - lineStart;
			float magnitude = vector.magnitude;
			vector /= magnitude;
			float num = Vector3.Dot(point - lineStart, vector);
			return num / magnitude;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00007684 File Offset: 0x00005A84
		public static float NearestPointFactor(Int3 lineStart, Int3 lineEnd, Int3 point)
		{
			Int3 rhs = lineEnd - lineStart;
			float sqrMagnitude = rhs.sqrMagnitude;
			return (float)Int3.Dot(point - lineStart, rhs) / sqrMagnitude;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000076B4 File Offset: 0x00005AB4
		public static float NearestPointFactor(Int2 lineStart, Int2 lineEnd, Int2 point)
		{
			Int2 b = lineEnd - lineStart;
			double num = (double)b.sqrMagnitudeLong;
			double num2 = (double)Int2.DotLong(point - lineStart, b) / num;
			return (float)num2;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000076E8 File Offset: 0x00005AE8
		public static Vector3 NearestPointStrict(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			Vector3 a = lineEnd - lineStart;
			float magnitude = a.magnitude;
			Vector3 vector = a / magnitude;
			float value = Vector3.Dot(point - lineStart, vector);
			return lineStart + Mathf.Clamp(value, 0f, magnitude) * vector;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00007734 File Offset: 0x00005B34
		public static Vector3 NearestPointStrictXZ(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			lineStart.y = point.y;
			lineEnd.y = point.y;
			Vector3 vector = lineEnd - lineStart;
			Vector3 value = vector;
			value.y = 0f;
			Vector3 vector2 = Vector3.Normalize(value);
			float value2 = Vector3.Dot(point - lineStart, vector2);
			return lineStart + Mathf.Clamp(value2, 0f, value.magnitude) * vector2;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000077A8 File Offset: 0x00005BA8
		public static float DistancePointSegment(int x, int z, int px, int pz, int qx, int qz)
		{
			float num = (float)(qx - px);
			float num2 = (float)(qz - pz);
			float num3 = (float)(x - px);
			float num4 = (float)(z - pz);
			float num5 = num * num + num2 * num2;
			float num6 = num * num3 + num2 * num4;
			if (num5 > 0f)
			{
				num6 /= num5;
			}
			if (num6 < 0f)
			{
				num6 = 0f;
			}
			else if (num6 > 1f)
			{
				num6 = 1f;
			}
			num3 = (float)px + num6 * num - (float)x;
			num4 = (float)pz + num6 * num2 - (float)z;
			return num3 * num3 + num4 * num4;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00007838 File Offset: 0x00005C38
		public static float DistancePointSegment(Int3 a, Int3 b, Int3 p)
		{
			float num = (float)(b.x - a.x);
			float num2 = (float)(b.z - a.z);
			float num3 = (float)(p.x - a.x);
			float num4 = (float)(p.z - a.z);
			float num5 = num * num + num2 * num2;
			float num6 = num * num3 + num2 * num4;
			if (num5 > 0f)
			{
				num6 /= num5;
			}
			if (num6 < 0f)
			{
				num6 = 0f;
			}
			else if (num6 > 1f)
			{
				num6 = 1f;
			}
			num3 = (float)a.x + num6 * num - (float)p.x;
			num4 = (float)a.z + num6 * num2 - (float)p.z;
			return num3 * num3 + num4 * num4;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00007910 File Offset: 0x00005D10
		public static float DistancePointSegment2(int x, int z, int px, int pz, int qx, int qz)
		{
			Vector3 p = new Vector3((float)x, 0f, (float)z);
			Vector3 a = new Vector3((float)px, 0f, (float)pz);
			Vector3 b = new Vector3((float)qx, 0f, (float)qz);
			return AstarMath.DistancePointSegment2(a, b, p);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00007958 File Offset: 0x00005D58
		public static float DistancePointSegment2(Vector3 a, Vector3 b, Vector3 p)
		{
			float num = b.x - a.x;
			float num2 = b.z - a.z;
			float num3 = Mathf.Abs(num * (p.z - a.z) - (p.x - a.x) * num2);
			float num4 = num * num + num2 * num2;
			if (num4 > 0f)
			{
				return num3 / Mathf.Sqrt(num4);
			}
			return (a - p).magnitude;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000079DC File Offset: 0x00005DDC
		public static float DistancePointSegmentStrict(Vector3 a, Vector3 b, Vector3 p)
		{
			Vector3 a2 = AstarMath.NearestPointStrict(a, b, p);
			return (a2 - p).sqrMagnitude;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00007A01 File Offset: 0x00005E01
		public static float Hermite(float start, float end, float value)
		{
			return Mathf.Lerp(start, end, value * value * (3f - 2f * value));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00007A1C File Offset: 0x00005E1C
		public static Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			t = Mathf.Clamp01(t);
			float num = 1f - t;
			return num * num * num * p0 + 3f * num * num * t * p1 + 3f * num * t * t * p2 + t * t * t * p3;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00007A88 File Offset: 0x00005E88
		public static float MapTo(float startMin, float startMax, float value)
		{
			value -= startMin;
			value /= startMax - startMin;
			value = Mathf.Clamp01(value);
			return value;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00007A9F File Offset: 0x00005E9F
		public static float MapToRange(float targetMin, float targetMax, float value)
		{
			value *= targetMax - targetMin;
			value += targetMin;
			return value;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00007AAE File Offset: 0x00005EAE
		public static float MapTo(float startMin, float startMax, float targetMin, float targetMax, float value)
		{
			value -= startMin;
			value /= startMax - startMin;
			value = Mathf.Clamp01(value);
			value *= targetMax - targetMin;
			value += targetMin;
			return value;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00007AD8 File Offset: 0x00005ED8
		public static string FormatBytes(int bytes)
		{
			double num = (bytes < 0) ? -1.0 : 1.0;
			bytes = ((bytes < 0) ? (-bytes) : bytes);
			if (bytes < 1000)
			{
				return ((double)bytes * num).ToString() + " bytes";
			}
			if (bytes < 1000000)
			{
				return ((double)bytes / 1000.0 * num).ToString("0.0") + " kb";
			}
			if (bytes < 1000000000)
			{
				return ((double)bytes / 1000000.0 * num).ToString("0.0") + " mb";
			}
			return ((double)bytes / 1000000000.0 * num).ToString("0.0") + " gb";
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00007BC8 File Offset: 0x00005FC8
		public static string FormatBytesBinary(int bytes)
		{
			double num = (bytes < 0) ? -1.0 : 1.0;
			bytes = ((bytes < 0) ? (-bytes) : bytes);
			if (bytes < 1024)
			{
				return ((double)bytes * num).ToString() + " bytes";
			}
			if (bytes < 1048576)
			{
				return ((double)bytes / 1024.0 * num).ToString("0.0") + " kb";
			}
			if (bytes < 1073741824)
			{
				return ((double)bytes / 1048576.0 * num).ToString("0.0") + " mb";
			}
			return ((double)bytes / 1073741824.0 * num).ToString("0.0") + " gb";
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00007CB5 File Offset: 0x000060B5
		public static int Bit(int a, int b)
		{
			return a >> b & 1;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00007CC0 File Offset: 0x000060C0
		public static Color IntToColor(int i, float a)
		{
			int num = AstarMath.Bit(i, 1) + AstarMath.Bit(i, 3) * 2 + 1;
			int num2 = AstarMath.Bit(i, 2) + AstarMath.Bit(i, 4) * 2 + 1;
			int num3 = AstarMath.Bit(i, 0) + AstarMath.Bit(i, 5) * 2 + 1;
			return new Color((float)num * 0.25f, (float)num2 * 0.25f, (float)num3 * 0.25f, a);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00007D28 File Offset: 0x00006128
		public static float MagnitudeXZ(Vector3 a, Vector3 b)
		{
			Vector3 vector = a - b;
			return (float)Math.Sqrt((double)(vector.x * vector.x + vector.z * vector.z));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00007D64 File Offset: 0x00006164
		public static float SqrMagnitudeXZ(Vector3 a, Vector3 b)
		{
			Vector3 vector = a - b;
			return vector.x * vector.x + vector.z * vector.z;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00007D98 File Offset: 0x00006198
		public static int Repeat(int i, int n)
		{
			while (i >= n)
			{
				i -= n;
			}
			return i;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00007DAC File Offset: 0x000061AC
		public static float Abs(float a)
		{
			if (a < 0f)
			{
				return -a;
			}
			return a;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00007DBD File Offset: 0x000061BD
		public static int Abs(int a)
		{
			if (a < 0)
			{
				return -a;
			}
			return a;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00007DCA File Offset: 0x000061CA
		public static float Min(float a, float b)
		{
			return (a >= b) ? b : a;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00007DDA File Offset: 0x000061DA
		public static int Min(int a, int b)
		{
			return (a >= b) ? b : a;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00007DEA File Offset: 0x000061EA
		public static uint Min(uint a, uint b)
		{
			return (a >= b) ? b : a;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00007DFA File Offset: 0x000061FA
		public static float Max(float a, float b)
		{
			return (a <= b) ? b : a;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00007E0A File Offset: 0x0000620A
		public static int Max(int a, int b)
		{
			return (a <= b) ? b : a;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00007E1A File Offset: 0x0000621A
		public static uint Max(uint a, uint b)
		{
			return (a <= b) ? b : a;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00007E2A File Offset: 0x0000622A
		public static ushort Max(ushort a, ushort b)
		{
			return (a <= b) ? b : a;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00007E3A File Offset: 0x0000623A
		public static float Sign(float a)
		{
			return (a >= 0f) ? 1f : -1f;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00007E56 File Offset: 0x00006256
		public static int Sign(int a)
		{
			return (a >= 0) ? 1 : -1;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00007E66 File Offset: 0x00006266
		public static float Clamp(float a, float b, float c)
		{
			return (a <= c) ? ((a >= b) ? a : b) : c;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00007E83 File Offset: 0x00006283
		public static int Clamp(int a, int b, int c)
		{
			return (a <= c) ? ((a >= b) ? a : b) : c;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00007EA0 File Offset: 0x000062A0
		public static float Clamp01(float a)
		{
			return (a <= 1f) ? ((a >= 0f) ? a : 0f) : 1f;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00007ECD File Offset: 0x000062CD
		public static int Clamp01(int a)
		{
			return (a <= 1) ? ((a >= 0) ? a : 0) : 1;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00007EEA File Offset: 0x000062EA
		public static float Lerp(float a, float b, float t)
		{
			return a + (b - a) * ((t <= 1f) ? ((t >= 0f) ? t : 0f) : 1f);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00007F1D File Offset: 0x0000631D
		public static int RoundToInt(float v)
		{
			return (int)(v + 0.5f);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00007F27 File Offset: 0x00006327
		public static int RoundToInt(double v)
		{
			return (int)(v + 0.5);
		}
	}
}
