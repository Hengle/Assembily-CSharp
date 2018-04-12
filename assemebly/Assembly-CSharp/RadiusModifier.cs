using System;
using System.Collections.Generic;
using Pathfinding;
using Pathfinding.Util;
using UnityEngine;

// Token: 0x020000CD RID: 205
[AddComponentMenu("Pathfinding/Modifiers/Radius Offset")]
public class RadiusModifier : MonoModifier
{
	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x060006DA RID: 1754 RVA: 0x00042A35 File Offset: 0x00040E35
	public override ModifierData input
	{
		get
		{
			return ModifierData.Vector;
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x060006DB RID: 1755 RVA: 0x00042A39 File Offset: 0x00040E39
	public override ModifierData output
	{
		get
		{
			return ModifierData.VectorPath;
		}
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x00042A3C File Offset: 0x00040E3C
	private bool CalculateCircleInner(Vector3 p1, Vector3 p2, float r1, float r2, out float a, out float sigma)
	{
		float magnitude = (p1 - p2).magnitude;
		if (r1 + r2 > magnitude)
		{
			a = 0f;
			sigma = 0f;
			return false;
		}
		a = (float)Math.Acos((double)((r1 + r2) / magnitude));
		sigma = (float)Math.Atan2((double)(p2.z - p1.z), (double)(p2.x - p1.x));
		return true;
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x00042AB0 File Offset: 0x00040EB0
	private bool CalculateCircleOuter(Vector3 p1, Vector3 p2, float r1, float r2, out float a, out float sigma)
	{
		float magnitude = (p1 - p2).magnitude;
		if (Math.Abs(r1 - r2) > magnitude)
		{
			a = 0f;
			sigma = 0f;
			return false;
		}
		a = (float)Math.Acos((double)((r1 - r2) / magnitude));
		sigma = (float)Math.Atan2((double)(p2.z - p1.z), (double)(p2.x - p1.x));
		return true;
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x00042B28 File Offset: 0x00040F28
	private RadiusModifier.TangentType CalculateTangentType(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
	{
		bool flag = Polygon.Left(p1, p2, p3);
		bool flag2 = Polygon.Left(p2, p3, p4);
		return (RadiusModifier.TangentType)(1 << (((!flag) ? 0 : 2) + ((!flag2) ? 0 : 1) & 31));
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x00042B68 File Offset: 0x00040F68
	private RadiusModifier.TangentType CalculateTangentTypeSimple(Vector3 p1, Vector3 p2, Vector3 p3)
	{
		bool flag = Polygon.Left(p1, p2, p3);
		bool flag2 = flag;
		return (RadiusModifier.TangentType)(1 << (((!flag2) ? 0 : 2) + ((!flag) ? 0 : 1) & 31));
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x00042BA0 File Offset: 0x00040FA0
	private void DrawCircle(Vector3 p1, float rad, Color col, float start = 0f, float end = 6.28318548f)
	{
		Vector3 start2 = new Vector3((float)Math.Cos((double)start), 0f, (float)Math.Sin((double)start)) * rad + p1;
		for (float num = start; num < end; num += 0.03141593f)
		{
			Vector3 vector = new Vector3((float)Math.Cos((double)num), 0f, (float)Math.Sin((double)num)) * rad + p1;
			Debug.DrawLine(start2, vector, col);
			start2 = vector;
		}
		if ((double)end == 6.2831853071795862)
		{
			Vector3 end2 = new Vector3((float)Math.Cos((double)start), 0f, (float)Math.Sin((double)start)) * rad + p1;
			Debug.DrawLine(start2, end2, col);
		}
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x00042C64 File Offset: 0x00041064
	public override void Apply(Path p, ModifierData source)
	{
		List<Vector3> vectorPath = p.vectorPath;
		List<Vector3> list = this.Apply(vectorPath);
		if (list != vectorPath)
		{
			ListPool<Vector3>.Release(p.vectorPath);
			p.vectorPath = list;
		}
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x00042C9C File Offset: 0x0004109C
	public List<Vector3> Apply(List<Vector3> vs)
	{
		if (vs == null || vs.Count < 3)
		{
			return vs;
		}
		if (this.radi.Length < vs.Count)
		{
			this.radi = new float[vs.Count];
			this.a1 = new float[vs.Count];
			this.a2 = new float[vs.Count];
			this.dir = new bool[vs.Count];
		}
		for (int i = 0; i < vs.Count; i++)
		{
			this.radi[i] = this.radius;
		}
		this.radi[0] = 0f;
		this.radi[vs.Count - 1] = 0f;
		int num = 0;
		for (int j = 0; j < vs.Count - 1; j++)
		{
			num++;
			if (num > 2 * vs.Count)
			{
				Debug.LogWarning("Could not resolve radiuses, the path is too complex. Try reducing the base radius");
				break;
			}
			RadiusModifier.TangentType tangentType;
			if (j == 0)
			{
				tangentType = this.CalculateTangentTypeSimple(vs[j], vs[j + 1], vs[j + 2]);
			}
			else if (j == vs.Count - 2)
			{
				tangentType = this.CalculateTangentTypeSimple(vs[j - 1], vs[j], vs[j + 1]);
			}
			else
			{
				tangentType = this.CalculateTangentType(vs[j - 1], vs[j], vs[j + 1], vs[j + 2]);
			}
			float num4;
			float num5;
			if ((tangentType & RadiusModifier.TangentType.Inner) != (RadiusModifier.TangentType)0)
			{
				float num2;
				float num3;
				if (!this.CalculateCircleInner(vs[j], vs[j + 1], this.radi[j], this.radi[j + 1], out num2, out num3))
				{
					float magnitude = (vs[j + 1] - vs[j]).magnitude;
					this.radi[j] = magnitude * (this.radi[j] / (this.radi[j] + this.radi[j + 1]));
					this.radi[j + 1] = magnitude - this.radi[j];
					this.radi[j] *= 0.99f;
					this.radi[j + 1] *= 0.99f;
					j -= 2;
				}
				else if (tangentType == RadiusModifier.TangentType.InnerRightLeft)
				{
					this.a2[j] = num3 - num2;
					this.a1[j + 1] = num3 - num2 + 3.14159274f;
					this.dir[j] = true;
				}
				else
				{
					this.a2[j] = num3 + num2;
					this.a1[j + 1] = num3 + num2 + 3.14159274f;
					this.dir[j] = false;
				}
			}
			else if (!this.CalculateCircleOuter(vs[j], vs[j + 1], this.radi[j], this.radi[j + 1], out num4, out num5))
			{
				if (j == vs.Count - 2)
				{
					this.radi[j] = (vs[j + 1] - vs[j]).magnitude;
					this.radi[j] *= 0.99f;
					j--;
				}
				else
				{
					if (this.radi[j] > this.radi[j + 1])
					{
						this.radi[j + 1] = this.radi[j] - (vs[j + 1] - vs[j]).magnitude;
					}
					else
					{
						this.radi[j + 1] = this.radi[j] + (vs[j + 1] - vs[j]).magnitude;
					}
					this.radi[j + 1] *= 0.99f;
				}
				j--;
			}
			else if (tangentType == RadiusModifier.TangentType.OuterRight)
			{
				this.a2[j] = num5 - num4;
				this.a1[j + 1] = num5 - num4;
				this.dir[j] = true;
			}
			else
			{
				this.a2[j] = num5 + num4;
				this.a1[j + 1] = num5 + num4;
				this.dir[j] = false;
			}
		}
		List<Vector3> list = ListPool<Vector3>.Claim();
		list.Add(vs[0]);
		if (this.detail < 1f)
		{
			this.detail = 1f;
		}
		float num6 = 6.28318548f / this.detail;
		for (int k = 1; k < vs.Count - 1; k++)
		{
			float num7 = this.a1[k];
			float num8 = this.a2[k];
			float d = this.radi[k];
			if (this.dir[k])
			{
				if (num8 < num7)
				{
					num8 += 6.28318548f;
				}
				for (float num9 = num7; num9 < num8; num9 += num6)
				{
					list.Add(new Vector3((float)Math.Cos((double)num9), 0f, (float)Math.Sin((double)num9)) * d + vs[k]);
				}
			}
			else
			{
				if (num7 < num8)
				{
					num7 += 6.28318548f;
				}
				for (float num10 = num7; num10 > num8; num10 -= num6)
				{
					list.Add(new Vector3((float)Math.Cos((double)num10), 0f, (float)Math.Sin((double)num10)) * d + vs[k]);
				}
			}
		}
		list.Add(vs[vs.Count - 1]);
		return list;
	}

	// Token: 0x040005B0 RID: 1456
	public float radius = 1f;

	// Token: 0x040005B1 RID: 1457
	public float detail = 10f;

	// Token: 0x040005B2 RID: 1458
	private float[] radi = new float[10];

	// Token: 0x040005B3 RID: 1459
	private float[] a1 = new float[10];

	// Token: 0x040005B4 RID: 1460
	private float[] a2 = new float[10];

	// Token: 0x040005B5 RID: 1461
	private bool[] dir = new bool[10];

	// Token: 0x020000CE RID: 206
	private enum TangentType
	{
		// Token: 0x040005B7 RID: 1463
		OuterRight = 1,
		// Token: 0x040005B8 RID: 1464
		InnerRightLeft,
		// Token: 0x040005B9 RID: 1465
		InnerLeftRight = 4,
		// Token: 0x040005BA RID: 1466
		OuterLeft = 8,
		// Token: 0x040005BB RID: 1467
		Outer,
		// Token: 0x040005BC RID: 1468
		Inner = 6
	}
}
