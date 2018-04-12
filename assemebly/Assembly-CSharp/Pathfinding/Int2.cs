using System;

namespace Pathfinding
{
	// Token: 0x02000023 RID: 35
	public struct Int2
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00010CBD File Offset: 0x0000F0BD
		public Int2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00010CCD File Offset: 0x0000F0CD
		public int sqrMagnitude
		{
			get
			{
				return this.x * this.x + this.y * this.y;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00010CEA File Offset: 0x0000F0EA
		public long sqrMagnitudeLong
		{
			get
			{
				return (long)this.x * (long)this.x + (long)this.y * (long)this.y;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00010D0B File Offset: 0x0000F10B
		public static Int2 operator +(Int2 a, Int2 b)
		{
			return new Int2(a.x + b.x, a.y + b.y);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00010D30 File Offset: 0x0000F130
		public static Int2 operator -(Int2 a, Int2 b)
		{
			return new Int2(a.x - b.x, a.y - b.y);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00010D55 File Offset: 0x0000F155
		public static bool operator ==(Int2 a, Int2 b)
		{
			return a.x == b.x && a.y == b.y;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00010D7D File Offset: 0x0000F17D
		public static bool operator !=(Int2 a, Int2 b)
		{
			return a.x != b.x || a.y != b.y;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00010DA8 File Offset: 0x0000F1A8
		public static int Dot(Int2 a, Int2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00010DC9 File Offset: 0x0000F1C9
		public static long DotLong(Int2 a, Int2 b)
		{
			return (long)a.x * (long)b.x + (long)a.y * (long)b.y;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00010DF0 File Offset: 0x0000F1F0
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			Int2 @int = (Int2)o;
			return this.x == @int.x && this.y == @int.y;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00010E30 File Offset: 0x0000F230
		public override int GetHashCode()
		{
			return this.x * 49157 + this.y * 98317;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00010E4C File Offset: 0x0000F24C
		public static Int2 Rotate(Int2 v, int r)
		{
			r %= 4;
			return new Int2(v.x * Int2.Rotations[r * 4] + v.y * Int2.Rotations[r * 4 + 1], v.x * Int2.Rotations[r * 4 + 2] + v.y * Int2.Rotations[r * 4 + 3]);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00010EAF File Offset: 0x0000F2AF
		public static Int2 Min(Int2 a, Int2 b)
		{
			return new Int2(Math.Min(a.x, b.x), Math.Min(a.y, b.y));
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00010EDC File Offset: 0x0000F2DC
		public static Int2 Max(Int2 a, Int2 b)
		{
			return new Int2(Math.Max(a.x, b.x), Math.Max(a.y, b.y));
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00010F09 File Offset: 0x0000F309
		public static Int2 FromInt3XZ(Int3 o)
		{
			return new Int2(o.x, o.z);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00010F1E File Offset: 0x0000F31E
		public static Int3 ToInt3XZ(Int2 o)
		{
			return new Int3(o.x, 0, o.y);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00010F34 File Offset: 0x0000F334
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"(",
				this.x,
				", ",
				this.y,
				")"
			});
		}

		// Token: 0x0400014E RID: 334
		public int x;

		// Token: 0x0400014F RID: 335
		public int y;

		// Token: 0x04000150 RID: 336
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
