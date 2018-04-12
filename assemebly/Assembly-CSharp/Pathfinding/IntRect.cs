using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200005B RID: 91
	public struct IntRect
	{
		// Token: 0x0600037B RID: 891 RVA: 0x0001AF1F File Offset: 0x0001931F
		public IntRect(int xmin, int ymin, int xmax, int ymax)
		{
			this.xmin = xmin;
			this.xmax = xmax;
			this.ymin = ymin;
			this.ymax = ymax;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001AF3E File Offset: 0x0001933E
		public bool Contains(int x, int y)
		{
			return x >= this.xmin && y >= this.ymin && x <= this.xmax && y <= this.ymax;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0001AF73 File Offset: 0x00019373
		public int Width
		{
			get
			{
				return this.xmax - this.xmin + 1;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0001AF84 File Offset: 0x00019384
		public int Height
		{
			get
			{
				return this.ymax - this.ymin + 1;
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001AF95 File Offset: 0x00019395
		public bool IsValid()
		{
			return this.xmin <= this.xmax && this.ymin <= this.ymax;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001AFBC File Offset: 0x000193BC
		public static bool operator ==(IntRect a, IntRect b)
		{
			return a.xmin == b.xmin && a.xmax == b.xmax && a.ymin == b.ymin && a.ymax == b.ymax;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001B018 File Offset: 0x00019418
		public static bool operator !=(IntRect a, IntRect b)
		{
			return a.xmin != b.xmin || a.xmax != b.xmax || a.ymin != b.ymin || a.ymax != b.ymax;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001B074 File Offset: 0x00019474
		public override bool Equals(object _b)
		{
			IntRect intRect = (IntRect)_b;
			return this.xmin == intRect.xmin && this.xmax == intRect.xmax && this.ymin == intRect.ymin && this.ymax == intRect.ymax;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001B0D0 File Offset: 0x000194D0
		public override int GetHashCode()
		{
			return this.xmin * 131071 ^ this.xmax * 3571 ^ this.ymin * 3109 ^ this.ymax * 7;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001B104 File Offset: 0x00019504
		public static IntRect Intersection(IntRect a, IntRect b)
		{
			IntRect result = new IntRect(Math.Max(a.xmin, b.xmin), Math.Max(a.ymin, b.ymin), Math.Min(a.xmax, b.xmax), Math.Min(a.ymax, b.ymax));
			return result;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001B168 File Offset: 0x00019568
		public static bool Intersects(IntRect a, IntRect b)
		{
			return a.xmin <= b.xmax && a.ymin <= b.ymax && a.xmax >= b.xmin && a.ymax >= b.ymin;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001B1C4 File Offset: 0x000195C4
		public static IntRect Union(IntRect a, IntRect b)
		{
			IntRect result = new IntRect(Math.Min(a.xmin, b.xmin), Math.Min(a.ymin, b.ymin), Math.Max(a.xmax, b.xmax), Math.Max(a.ymax, b.ymax));
			return result;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001B228 File Offset: 0x00019628
		public IntRect ExpandToContain(int x, int y)
		{
			IntRect result = new IntRect(Math.Min(this.xmin, x), Math.Min(this.ymin, y), Math.Max(this.xmax, x), Math.Max(this.ymax, y));
			return result;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001B26D File Offset: 0x0001966D
		public IntRect Expand(int range)
		{
			return new IntRect(this.xmin - range, this.ymin - range, this.xmax + range, this.ymax + range);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001B294 File Offset: 0x00019694
		public IntRect Rotate(int r)
		{
			int num = IntRect.Rotations[r * 4];
			int num2 = IntRect.Rotations[r * 4 + 1];
			int num3 = IntRect.Rotations[r * 4 + 2];
			int num4 = IntRect.Rotations[r * 4 + 3];
			int val = num * this.xmin + num2 * this.ymin;
			int val2 = num3 * this.xmin + num4 * this.ymin;
			int val3 = num * this.xmax + num2 * this.ymax;
			int val4 = num3 * this.xmax + num4 * this.ymax;
			return new IntRect(Math.Min(val, val3), Math.Min(val2, val4), Math.Max(val, val3), Math.Max(val2, val4));
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001B344 File Offset: 0x00019744
		public IntRect Offset(Int2 offset)
		{
			return new IntRect(this.xmin + offset.x, this.ymin + offset.y, this.xmax + offset.x, this.ymax + offset.y);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001B383 File Offset: 0x00019783
		public IntRect Offset(int x, int y)
		{
			return new IntRect(this.xmin + x, this.ymin + y, this.xmax + x, this.ymax + y);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001B3AC File Offset: 0x000197AC
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"[x: ",
				this.xmin,
				"...",
				this.xmax,
				", y: ",
				this.ymin,
				"...",
				this.ymax,
				"]"
			});
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001B428 File Offset: 0x00019828
		public void DebugDraw(Matrix4x4 matrix, Color col)
		{
			Vector3 vector = matrix.MultiplyPoint3x4(new Vector3((float)this.xmin, 0f, (float)this.ymin));
			Vector3 vector2 = matrix.MultiplyPoint3x4(new Vector3((float)this.xmin, 0f, (float)this.ymax));
			Vector3 vector3 = matrix.MultiplyPoint3x4(new Vector3((float)this.xmax, 0f, (float)this.ymax));
			Vector3 vector4 = matrix.MultiplyPoint3x4(new Vector3((float)this.xmax, 0f, (float)this.ymin));
			Debug.DrawLine(vector, vector2, col);
			Debug.DrawLine(vector2, vector3, col);
			Debug.DrawLine(vector3, vector4, col);
			Debug.DrawLine(vector4, vector, col);
		}

		// Token: 0x040002C2 RID: 706
		public int xmin;

		// Token: 0x040002C3 RID: 707
		public int ymin;

		// Token: 0x040002C4 RID: 708
		public int xmax;

		// Token: 0x040002C5 RID: 709
		public int ymax;

		// Token: 0x040002C6 RID: 710
		private static readonly int[] Rotations = new int[]
		{
			1,
			0,
			0,
			1,
			0,
			1,
			-1,
			0,
			-1,
			0,
			0,
			-1,
			0,
			-1,
			1,
			0
		};
	}
}
