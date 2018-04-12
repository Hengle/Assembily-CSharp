using System;
using System.IO;

namespace SDG.Framework.IO.Streams.BitStreams
{
	// Token: 0x020001CA RID: 458
	public class PrimitiveBitStreamReader : BitStreamReader
	{
		// Token: 0x06000DAB RID: 3499 RVA: 0x00060D64 File Offset: 0x0005F164
		public PrimitiveBitStreamReader(Stream newStream) : base(newStream)
		{
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00060D6D File Offset: 0x0005F16D
		public void readByte(ref byte data)
		{
			base.readBits(ref data, 8);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00060D78 File Offset: 0x0005F178
		public void readInt16(ref short data)
		{
			byte b = 0;
			byte b2 = 0;
			this.readByte(ref b);
			this.readByte(ref b2);
			data = (short)((int)b << 8 | (int)b2);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00060DA4 File Offset: 0x0005F1A4
		public void readInt16(ref short data, byte length)
		{
			if (length == 16)
			{
				this.readInt16(ref data);
			}
			else if (length > 8)
			{
				byte b = 0;
				byte b2 = 0;
				base.readBits(ref b, length - 8);
				this.readByte(ref b2);
				data = (short)((int)b << 8 | (int)b2);
			}
			else if (length == 8)
			{
				byte b3 = 0;
				this.readByte(ref b3);
				data = (short)b3;
			}
			else
			{
				byte b4 = 0;
				base.readBits(ref b4, length);
				data = (short)b4;
			}
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00060E18 File Offset: 0x0005F218
		public void readUInt16(ref ushort data)
		{
			byte b = 0;
			byte b2 = 0;
			this.readByte(ref b);
			this.readByte(ref b2);
			data = (ushort)((int)b << 8 | (int)b2);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00060E44 File Offset: 0x0005F244
		public void readUInt16(ref ushort data, byte length)
		{
			if (length == 16)
			{
				this.readUInt16(ref data);
			}
			else if (length > 8)
			{
				byte b = 0;
				byte b2 = 0;
				base.readBits(ref b, length - 8);
				this.readByte(ref b2);
				data = (ushort)((int)b << 8 | (int)b2);
			}
			else if (length == 8)
			{
				byte b3 = 0;
				this.readByte(ref b3);
				data = (ushort)b3;
			}
			else
			{
				byte b4 = 0;
				base.readBits(ref b4, length);
				data = (ushort)b4;
			}
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00060EB8 File Offset: 0x0005F2B8
		public void readInt32(ref int data)
		{
			byte b = 0;
			byte b2 = 0;
			byte b3 = 0;
			byte b4 = 0;
			this.readByte(ref b);
			this.readByte(ref b2);
			this.readByte(ref b3);
			this.readByte(ref b4);
			data = ((int)b << 24 | (int)b2 << 16 | (int)b3 << 8 | (int)b4);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00060F00 File Offset: 0x0005F300
		public void readInt32(ref int data, byte length)
		{
			if (length == 32)
			{
				this.readInt32(ref data);
			}
			else if (length > 24)
			{
				byte b = 0;
				byte b2 = 0;
				byte b3 = 0;
				byte b4 = 0;
				base.readBits(ref b, length - 8);
				this.readByte(ref b2);
				this.readByte(ref b3);
				this.readByte(ref b4);
				data = ((int)b << 24 | (int)b2 << 16 | (int)b3 << 8 | (int)b4);
			}
			else if (length == 24)
			{
				byte b5 = 0;
				byte b6 = 0;
				byte b7 = 0;
				this.readByte(ref b5);
				this.readByte(ref b6);
				this.readByte(ref b7);
				data = ((int)b5 << 16 | (int)b6 << 8 | (int)b7);
			}
			else if (length > 16)
			{
				byte b8 = 0;
				byte b9 = 0;
				byte b10 = 0;
				base.readBits(ref b8, length - 8);
				this.readByte(ref b9);
				this.readByte(ref b10);
				data = ((int)b8 << 16 | (int)b9 << 8 | (int)b10);
			}
			else if (length == 16)
			{
				byte b11 = 0;
				byte b12 = 0;
				this.readByte(ref b11);
				this.readByte(ref b12);
				data = ((int)b11 << 8 | (int)b12);
			}
			else if (length > 8)
			{
				byte b13 = 0;
				byte b14 = 0;
				base.readBits(ref b13, length - 8);
				this.readByte(ref b14);
				data = ((int)b13 << 8 | (int)b14);
			}
			else if (length == 8)
			{
				byte b15 = 0;
				this.readByte(ref b15);
				data = (int)b15;
			}
			else
			{
				byte b16 = 0;
				base.readBits(ref b16, length);
				data = (int)b16;
			}
		}
	}
}
