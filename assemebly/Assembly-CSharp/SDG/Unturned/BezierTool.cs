using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000727 RID: 1831
	public class BezierTool
	{
		// Token: 0x060033C5 RID: 13253 RVA: 0x001503D4 File Offset: 0x0014E7D4
		public static Vector3 getPosition(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
		{
			t = Mathf.Clamp01(t);
			float num = 1f - t;
			return num * num * num * a + 3f * num * num * t * b + 3f * num * t * t * c + t * t * t * d;
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x00150440 File Offset: 0x0014E840
		public static Vector3 getVelocity(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
		{
			t = Mathf.Clamp01(t);
			float num = 1f - t;
			return 3f * num * num * (b - a) + 6f * num * t * (c - b) + 3f * t * t * (d - c);
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x001504AC File Offset: 0x0014E8AC
		public static float getLengthEstimate(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
		{
			return ((d - a).magnitude + (d - c).magnitude + (b - c).magnitude + (b - a).magnitude) / 2f;
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x00150500 File Offset: 0x0014E900
		public static float getTFromDistance(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float distance, float uniformInterval, float intervalTolerance, int attempts, float cachedLength = -1f)
		{
			if (distance < 0f)
			{
				return 0f;
			}
			float num;
			if (cachedLength < 0f)
			{
				num = BezierTool.getLengthEstimate(a, b, c, d);
			}
			else
			{
				num = cachedLength;
			}
			if (distance >= num)
			{
				return 1f;
			}
			attempts = Mathf.Max(1, attempts);
			int num2 = Mathf.CeilToInt(num / uniformInterval);
			float num3 = 1f / (float)num2;
			float num4 = num3;
			float num5 = uniformInterval;
			Vector3 vector = a;
			double num6 = 0.0;
			double num7 = 0.0;
			while (num7 < 1.0)
			{
				Vector3 vector2 = vector;
				for (int i = 0; i < attempts; i++)
				{
					float t = (float)(num7 + (double)num4);
					vector2 = BezierTool.getPosition(a, a + b, d + c, d, t);
					num5 = (vector2 - vector).magnitude;
					if (i < attempts - 1)
					{
						float num8 = Mathf.Abs(num5 - uniformInterval);
						if (num8 < intervalTolerance)
						{
							break;
						}
						num4 *= intervalTolerance / num5;
					}
				}
				double num9 = num6 + (double)num5;
				if ((double)distance >= num6 && (double)distance <= num9)
				{
					float num10 = (float)(((double)distance - num6) / (double)num5);
					num10 *= num4;
					return (float)(num7 + (double)num10);
				}
				vector = vector2;
				num6 += (double)num5;
				num7 += (double)num4;
				num4 = num3;
			}
			Debug.LogWarning(string.Concat(new object[]
			{
				"Failed to find T along curve from distance!\nDistance: ",
				distance,
				" Length: ",
				num,
				"\nInterval: ",
				uniformInterval,
				" Tolerance: ",
				intervalTolerance,
				" Attempts: ",
				attempts
			}));
			return 0.5f;
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x001506DC File Offset: 0x0014EADC
		public static float getLengthBruteForce(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float uniformInterval, float intervalTolerance, int attempts)
		{
			float lengthEstimate = BezierTool.getLengthEstimate(a, b, c, d);
			attempts = Mathf.Max(1, attempts);
			int num = Mathf.CeilToInt(lengthEstimate / uniformInterval);
			float num2 = 1f / (float)num;
			float num3 = num2;
			float num4 = uniformInterval;
			Vector3 vector = a;
			double num5 = 0.0;
			double num6 = 0.0;
			while (num6 < 1.0)
			{
				Vector3 vector2 = vector;
				for (int i = 0; i < attempts; i++)
				{
					float t = (float)(num6 + (double)num3);
					vector2 = BezierTool.getPosition(a, a + b, d + c, d, t);
					num4 = (vector2 - vector).magnitude;
					if (i < attempts - 1)
					{
						float num7 = Mathf.Abs(num4 - uniformInterval);
						if (num7 < intervalTolerance)
						{
							break;
						}
						if (i < attempts - 1)
						{
							num3 *= intervalTolerance / num4;
						}
					}
				}
				Debug.Log(num4);
				vector = vector2;
				num5 += (double)num4;
				num6 += (double)num3;
				num3 = num2;
			}
			return (float)num5;
		}
	}
}
