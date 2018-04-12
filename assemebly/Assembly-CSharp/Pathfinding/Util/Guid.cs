using System;
using System.Text;

namespace Pathfinding.Util
{
	// Token: 0x020000F1 RID: 241
	public struct Guid
	{
		// Token: 0x060007D7 RID: 2007 RVA: 0x0004B680 File Offset: 0x00049A80
		public Guid(byte[] bytes)
		{
			this._a = ((ulong)bytes[0] << 0 | (ulong)bytes[1] << 8 | (ulong)bytes[2] << 16 | (ulong)bytes[3] << 24 | (ulong)bytes[4] << 32 | (ulong)bytes[5] << 40 | (ulong)bytes[6] << 48 | (ulong)bytes[7] << 56);
			this._b = ((ulong)bytes[8] << 0 | (ulong)bytes[9] << 8 | (ulong)bytes[10] << 16 | (ulong)bytes[11] << 24 | (ulong)bytes[12] << 32 | (ulong)bytes[13] << 40 | (ulong)bytes[14] << 48 | (ulong)bytes[15] << 56);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0004B71C File Offset: 0x00049B1C
		public Guid(string str)
		{
			this._a = 0UL;
			this._b = 0UL;
			if (str.Length < 32)
			{
				throw new FormatException("Invalid Guid format");
			}
			int i = 0;
			int num = 0;
			int num2 = 60;
			while (i < 16)
			{
				if (num >= str.Length)
				{
					throw new FormatException("Invalid Guid format. String too short");
				}
				char c = str[num];
				if (c != '-')
				{
					int num3 = "0123456789ABCDEF".IndexOf(char.ToUpperInvariant(c));
					if (num3 == -1)
					{
						throw new FormatException("Invalid Guid format : " + c + " is not a hexadecimal character");
					}
					this._a |= (ulong)((ulong)((long)num3) << num2);
					num2 -= 4;
					i++;
				}
				num++;
			}
			num2 = 60;
			while (i < 32)
			{
				if (num >= str.Length)
				{
					throw new FormatException("Invalid Guid format. String too short");
				}
				char c2 = str[num];
				if (c2 != '-')
				{
					int num4 = "0123456789ABCDEF".IndexOf(char.ToUpperInvariant(c2));
					if (num4 == -1)
					{
						throw new FormatException("Invalid Guid format : " + c2 + " is not a hexadecimal character");
					}
					this._b |= (ulong)((ulong)((long)num4) << num2);
					num2 -= 4;
					i++;
				}
				num++;
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0004B87D File Offset: 0x00049C7D
		public static Guid Parse(string input)
		{
			return new Guid(input);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0004B888 File Offset: 0x00049C88
		public byte[] ToByteArray()
		{
			byte[] array = new byte[16];
			byte[] bytes = BitConverter.GetBytes(this._a);
			byte[] bytes2 = BitConverter.GetBytes(this._b);
			for (int i = 0; i < 8; i++)
			{
				array[i] = bytes[i];
				array[i + 8] = bytes2[i];
			}
			return array;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0004B8D8 File Offset: 0x00049CD8
		public static Guid NewGuid()
		{
			byte[] array = new byte[16];
			Guid.random.NextBytes(array);
			return new Guid(array);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0004B8FE File Offset: 0x00049CFE
		public static bool operator ==(Guid lhs, Guid rhs)
		{
			return lhs._a == rhs._a && lhs._b == rhs._b;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0004B926 File Offset: 0x00049D26
		public static bool operator !=(Guid lhs, Guid rhs)
		{
			return lhs._a != rhs._a || lhs._b != rhs._b;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0004B954 File Offset: 0x00049D54
		public override bool Equals(object _rhs)
		{
			if (!(_rhs is Guid))
			{
				return false;
			}
			Guid guid = (Guid)_rhs;
			return this._a == guid._a && this._b == guid._b;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0004B99C File Offset: 0x00049D9C
		public override int GetHashCode()
		{
			ulong num = this._a ^ this._b;
			return (int)(num >> 32) ^ (int)num;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0004B9C0 File Offset: 0x00049DC0
		public override string ToString()
		{
			if (Guid.text == null)
			{
				Guid.text = new StringBuilder();
			}
			object obj = Guid.text;
			string result;
			lock (obj)
			{
				Guid.text.Length = 0;
				Guid.text.Append(this._a.ToString("x16")).Append('-').Append(this._b.ToString("x16"));
				result = Guid.text.ToString();
			}
			return result;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0004BA58 File Offset: 0x00049E58
		static Guid()
		{
			// Note: this type is marked as 'beforefieldinit'.
			Guid guid = new Guid(new byte[16]);
			Guid.zeroString = guid.ToString();
			Guid.random = new Random();
		}

		// Token: 0x04000695 RID: 1685
		private const string hex = "0123456789ABCDEF";

		// Token: 0x04000696 RID: 1686
		public static readonly Guid zero = new Guid(new byte[16]);

		// Token: 0x04000697 RID: 1687
		public static readonly string zeroString;

		// Token: 0x04000698 RID: 1688
		private ulong _a;

		// Token: 0x04000699 RID: 1689
		private ulong _b;

		// Token: 0x0400069A RID: 1690
		private static Random random;

		// Token: 0x0400069B RID: 1691
		private static StringBuilder text;
	}
}
