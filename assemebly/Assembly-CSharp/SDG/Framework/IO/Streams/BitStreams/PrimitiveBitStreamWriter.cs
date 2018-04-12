using System;
using System.IO;

namespace SDG.Framework.IO.Streams.BitStreams
{
	// Token: 0x020001CB RID: 459
	public class PrimitiveBitStreamWriter : BitStreamWriter
	{
		// Token: 0x06000DB3 RID: 3507 RVA: 0x0006106F File Offset: 0x0005F46F
		public PrimitiveBitStreamWriter(Stream newStream) : base(newStream)
		{
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00061078 File Offset: 0x0005F478
		public void writeByte(byte data)
		{
			base.writeBits(data, 8);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00061082 File Offset: 0x0005F482
		public void writeInt16(short data)
		{
			this.writeByte((byte)(data >> 8));
			this.writeByte((byte)data);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00061098 File Offset: 0x0005F498
		public void writeInt16(short data, byte length)
		{
			if (length == 16)
			{
				this.writeInt16(data);
			}
			else if (length > 8)
			{
				base.writeBits((byte)(data >> 8), length - 8);
				this.writeByte((byte)data);
			}
			else if (length == 8)
			{
				this.writeByte((byte)data);
			}
			else
			{
				base.writeBits((byte)data, length);
			}
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x000610F8 File Offset: 0x0005F4F8
		public void writeUInt16(ushort data)
		{
			this.writeByte((byte)(data >> 8));
			this.writeByte((byte)data);
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0006110C File Offset: 0x0005F50C
		public void writeUInt16(ushort data, byte length)
		{
			if (length == 16)
			{
				this.writeUInt16(data);
			}
			else if (length > 8)
			{
				base.writeBits((byte)(data >> 8), length - 8);
				this.writeByte((byte)data);
			}
			else if (length == 8)
			{
				this.writeByte((byte)data);
			}
			else
			{
				base.writeBits((byte)data, length);
			}
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0006116C File Offset: 0x0005F56C
		public void writeInt32(int data)
		{
			this.writeByte((byte)(data >> 24));
			this.writeByte((byte)(data >> 16));
			this.writeByte((byte)(data >> 8));
			this.writeByte((byte)data);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00061198 File Offset: 0x0005F598
		public void writeInt32(int data, byte length)
		{
			if (length == 32)
			{
				this.writeInt32(data);
			}
			else if (length > 24)
			{
				base.writeBits((byte)(data >> 24), length - 8);
				this.writeByte((byte)(data >> 16));
				this.writeByte((byte)(data >> 8));
				this.writeByte((byte)data);
			}
			else if (length == 24)
			{
				this.writeByte((byte)(data >> 16));
				this.writeByte((byte)(data >> 8));
				this.writeByte((byte)data);
			}
			else if (length > 16)
			{
				base.writeBits((byte)(data >> 16), length - 8);
				this.writeByte((byte)(data >> 8));
				this.writeByte((byte)data);
			}
			else if (length == 16)
			{
				this.writeByte((byte)(data >> 8));
				this.writeByte((byte)data);
			}
			else if (length > 8)
			{
				base.writeBits((byte)(data >> 8), length - 8);
				this.writeByte((byte)data);
			}
			else if (length == 8)
			{
				this.writeByte((byte)data);
			}
			else
			{
				base.writeBits((byte)data, length);
			}
		}
	}
}
