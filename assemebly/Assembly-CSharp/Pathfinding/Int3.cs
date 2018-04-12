using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000022 RID: 34
	public struct Int3
	{
		// Token: 0x06000169 RID: 361 RVA: 0x0001043C File Offset: 0x0000E83C
		public Int3(Vector3 position)
		{
			this.x = (int)Math.Round((double)(position.x * 1000f));
			this.y = (int)Math.Round((double)(position.y * 1000f));
			this.z = (int)Math.Round((double)(position.z * 1000f));
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00010497 File Offset: 0x0000E897
		public Int3(int _x, int _y, int _z)
		{
			this.x = _x;
			this.y = _y;
			this.z = _z;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000104AE File Offset: 0x0000E8AE
		public static Int3 zero
		{
			get
			{
				return Int3._zero;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000104B5 File Offset: 0x0000E8B5
		public static bool operator ==(Int3 lhs, Int3 rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000104F0 File Offset: 0x0000E8F0
		public static bool operator !=(Int3 lhs, Int3 rhs)
		{
			return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00010530 File Offset: 0x0000E930
		public static explicit operator Int3(Vector3 ob)
		{
			return new Int3((int)Math.Round((double)(ob.x * 1000f)), (int)Math.Round((double)(ob.y * 1000f)), (int)Math.Round((double)(ob.z * 1000f)));
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0001057E File Offset: 0x0000E97E
		public static explicit operator Vector3(Int3 ob)
		{
			return new Vector3((float)ob.x * 0.001f, (float)ob.y * 0.001f, (float)ob.z * 0.001f);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000105B0 File Offset: 0x0000E9B0
		public static Int3 operator -(Int3 lhs, Int3 rhs)
		{
			lhs.x -= rhs.x;
			lhs.y -= rhs.y;
			lhs.z -= rhs.z;
			return lhs;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000105FD File Offset: 0x0000E9FD
		public static Int3 operator -(Int3 lhs)
		{
			lhs.x = -lhs.x;
			lhs.y = -lhs.y;
			lhs.z = -lhs.z;
			return lhs;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00010630 File Offset: 0x0000EA30
		public static Int3 operator +(Int3 lhs, Int3 rhs)
		{
			lhs.x += rhs.x;
			lhs.y += rhs.y;
			lhs.z += rhs.z;
			return lhs;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0001067D File Offset: 0x0000EA7D
		public static Int3 operator *(Int3 lhs, int rhs)
		{
			lhs.x *= rhs;
			lhs.y *= rhs;
			lhs.z *= rhs;
			return lhs;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000106B0 File Offset: 0x0000EAB0
		public static Int3 operator *(Int3 lhs, float rhs)
		{
			lhs.x = (int)Math.Round((double)((float)lhs.x * rhs));
			lhs.y = (int)Math.Round((double)((float)lhs.y * rhs));
			lhs.z = (int)Math.Round((double)((float)lhs.z * rhs));
			return lhs;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00010708 File Offset: 0x0000EB08
		public static Int3 operator *(Int3 lhs, double rhs)
		{
			lhs.x = (int)Math.Round((double)lhs.x * rhs);
			lhs.y = (int)Math.Round((double)lhs.y * rhs);
			lhs.z = (int)Math.Round((double)lhs.z * rhs);
			return lhs;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0001075C File Offset: 0x0000EB5C
		public static Int3 operator *(Int3 lhs, Vector3 rhs)
		{
			lhs.x = (int)Math.Round((double)((float)lhs.x * rhs.x));
			lhs.y = (int)Math.Round((double)((float)lhs.y * rhs.y));
			lhs.z = (int)Math.Round((double)((float)lhs.z * rhs.z));
			return lhs;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000107C4 File Offset: 0x0000EBC4
		public static Int3 operator /(Int3 lhs, float rhs)
		{
			lhs.x = (int)Math.Round((double)((float)lhs.x / rhs));
			lhs.y = (int)Math.Round((double)((float)lhs.y / rhs));
			lhs.z = (int)Math.Round((double)((float)lhs.z / rhs));
			return lhs;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0001081A File Offset: 0x0000EC1A
		public Int3 DivBy2()
		{
			this.x >>= 1;
			this.y >>= 1;
			this.z >>= 1;
			return this;
		}

		// Token: 0x17000015 RID: 21
		public int this[int i]
		{
			get
			{
				return (i != 0) ? ((i != 1) ? this.z : this.y) : this.x;
			}
			set
			{
				if (i == 0)
				{
					this.x = value;
				}
				else if (i == 1)
				{
					this.y = value;
				}
				else
				{
					this.z = value;
				}
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000108A8 File Offset: 0x0000ECA8
		public static float Angle(Int3 lhs, Int3 rhs)
		{
			double num = (double)Int3.Dot(lhs, rhs) / ((double)lhs.magnitude * (double)rhs.magnitude);
			num = ((num >= -1.0) ? ((num <= 1.0) ? num : 1.0) : -1.0);
			return (float)Math.Acos(num);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00010913 File Offset: 0x0000ED13
		public static int Dot(Int3 lhs, Int3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00010944 File Offset: 0x0000ED44
		public static long DotLong(Int3 lhs, Int3 rhs)
		{
			return (long)lhs.x * (long)rhs.x + (long)lhs.y * (long)rhs.y + (long)lhs.z * (long)rhs.z;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0001097B File Offset: 0x0000ED7B
		public Int3 Normal2D()
		{
			return new Int3(this.z, this.y, -this.x);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00010998 File Offset: 0x0000ED98
		public Int3 NormalizeTo(int newMagn)
		{
			float magnitude = this.magnitude;
			if (magnitude == 0f)
			{
				return this;
			}
			this.x *= newMagn;
			this.y *= newMagn;
			this.z *= newMagn;
			this.x = (int)Math.Round((double)((float)this.x / magnitude));
			this.y = (int)Math.Round((double)((float)this.y / magnitude));
			this.z = (int)Math.Round((double)((float)this.z / magnitude));
			return this;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00010A30 File Offset: 0x0000EE30
		public float magnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00010A66 File Offset: 0x0000EE66
		public int costMagnitude
		{
			get
			{
				return (int)Math.Round((double)this.magnitude);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00010A78 File Offset: 0x0000EE78
		public float worldMagnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3) * 0.001f;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00010AB4 File Offset: 0x0000EEB4
		public float sqrMagnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)(num * num + num2 * num2 + num3 * num3);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00010AE8 File Offset: 0x0000EEE8
		public long sqrMagnitudeLong
		{
			get
			{
				long num = (long)this.x;
				long num2 = (long)this.y;
				long num3 = (long)this.z;
				return num * num + num2 * num2 + num3 * num3;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00010B18 File Offset: 0x0000EF18
		public int unsafeSqrMagnitude
		{
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00010B44 File Offset: 0x0000EF44
		[Obsolete("Same implementation as .magnitude")]
		public float safeMagnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00010B7C File Offset: 0x0000EF7C
		[Obsolete(".sqrMagnitude is now per default safe (.unsafeSqrMagnitude can be used for unsafe operations)")]
		public float safeSqrMagnitude
		{
			get
			{
				float num = (float)this.x * 0.001f;
				float num2 = (float)this.y * 0.001f;
				float num3 = (float)this.z * 0.001f;
				return num * num + num2 * num2 + num3 * num3;
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00010BBE File Offset: 0x0000EFBE
		public static implicit operator string(Int3 ob)
		{
			return ob.ToString();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00010BD0 File Offset: 0x0000EFD0
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"( ",
				this.x,
				", ",
				this.y,
				", ",
				this.z,
				")"
			});
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00010C34 File Offset: 0x0000F034
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			Int3 @int = (Int3)o;
			return this.x == @int.x && this.y == @int.y && this.z == @int.z;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00010C86 File Offset: 0x0000F086
		public override int GetHashCode()
		{
			return this.x * 73856093 ^ this.y * 19349663 ^ this.z * 83492791;
		}

		// Token: 0x04000147 RID: 327
		public int x;

		// Token: 0x04000148 RID: 328
		public int y;

		// Token: 0x04000149 RID: 329
		public int z;

		// Token: 0x0400014A RID: 330
		public const int Precision = 1000;

		// Token: 0x0400014B RID: 331
		public const float FloatPrecision = 1000f;

		// Token: 0x0400014C RID: 332
		public const float PrecisionFactor = 0.001f;

		// Token: 0x0400014D RID: 333
		private static Int3 _zero = new Int3(0, 0, 0);
	}
}
