using System;
using System.IO;

namespace SDG.Framework.IO.Streams
{
	// Token: 0x020001CD RID: 461
	public class NetworkStream
	{
		// Token: 0x06000DBD RID: 3517 RVA: 0x000612A8 File Offset: 0x0005F6A8
		public NetworkStream(Stream newStream)
		{
			this.stream = newStream;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x000612B7 File Offset: 0x0005F6B7
		// (set) Token: 0x06000DBF RID: 3519 RVA: 0x000612BF File Offset: 0x0005F6BF
		private Stream stream { get; set; }

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000612C8 File Offset: 0x0005F6C8
		public sbyte readSByte()
		{
			return (sbyte)this.stream.ReadByte();
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x000612E4 File Offset: 0x0005F6E4
		public byte readByte()
		{
			return (byte)this.stream.ReadByte();
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00061300 File Offset: 0x0005F700
		public short readInt16()
		{
			byte b = this.readByte();
			byte b2 = this.readByte();
			return (short)((int)b << 8 | (int)b2);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00061324 File Offset: 0x0005F724
		public ushort readUInt16()
		{
			byte b = this.readByte();
			byte b2 = this.readByte();
			return (ushort)((int)b << 8 | (int)b2);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00061348 File Offset: 0x0005F748
		public int readInt32()
		{
			byte b = this.readByte();
			byte b2 = this.readByte();
			byte b3 = this.readByte();
			byte b4 = this.readByte();
			return (int)b << 24 | (int)b2 << 16 | (int)b3 << 8 | (int)b4;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00061384 File Offset: 0x0005F784
		public uint readUInt32()
		{
			byte b = this.readByte();
			byte b2 = this.readByte();
			byte b3 = this.readByte();
			byte b4 = this.readByte();
			return (uint)((int)b << 24 | (int)b2 << 16 | (int)b3 << 8 | (int)b4);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x000613C0 File Offset: 0x0005F7C0
		public long readInt64()
		{
			byte b = this.readByte();
			byte b2 = this.readByte();
			byte b3 = this.readByte();
			byte b4 = this.readByte();
			byte b5 = this.readByte();
			byte b6 = this.readByte();
			byte b7 = this.readByte();
			byte b8 = this.readByte();
			return (long)((int)b << 24 | (int)b2 << 16 | (int)b3 << 8 | (int)b4 << 0 | (int)b5 << 24 | (int)b6 << 16 | (int)b7 << 8 | (int)b8);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00061434 File Offset: 0x0005F834
		public ulong readUInt64()
		{
			byte b = this.readByte();
			byte b2 = this.readByte();
			byte b3 = this.readByte();
			byte b4 = this.readByte();
			byte b5 = this.readByte();
			byte b6 = this.readByte();
			byte b7 = this.readByte();
			byte b8 = this.readByte();
			return (ulong)((long)((int)b << 24 | (int)b2 << 16 | (int)b3 << 8 | (int)b4 << 0 | (int)b5 << 24 | (int)b6 << 16 | (int)b7 << 8 | (int)b8));
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x000614A7 File Offset: 0x0005F8A7
		public char readChar()
		{
			return (char)this.readUInt16();
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x000614B0 File Offset: 0x0005F8B0
		public string readString()
		{
			ushort num = this.readUInt16();
			char[] array = new char[(int)num];
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				char c = this.readChar();
				array[(int)num2] = c;
			}
			return new string(array);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x000614F3 File Offset: 0x0005F8F3
		public void readBytes(byte[] data, ulong offset, ulong length)
		{
			this.stream.Read(data, (int)offset, (int)length);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00061506 File Offset: 0x0005F906
		public void writeSByte(sbyte data)
		{
			this.stream.WriteByte((byte)data);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00061515 File Offset: 0x0005F915
		public void writeByte(byte data)
		{
			this.stream.WriteByte(data);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00061523 File Offset: 0x0005F923
		public void writeInt16(short data)
		{
			this.writeByte((byte)(data >> 8));
			this.writeByte((byte)data);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00061537 File Offset: 0x0005F937
		public void writeUInt16(ushort data)
		{
			this.writeByte((byte)(data >> 8));
			this.writeByte((byte)data);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0006154B File Offset: 0x0005F94B
		public void writeInt32(int data)
		{
			this.writeByte((byte)(data >> 24));
			this.writeByte((byte)(data >> 16));
			this.writeByte((byte)(data >> 8));
			this.writeByte((byte)data);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00061575 File Offset: 0x0005F975
		public void writeUInt32(uint data)
		{
			this.writeByte((byte)(data >> 24));
			this.writeByte((byte)(data >> 16));
			this.writeByte((byte)(data >> 8));
			this.writeByte((byte)data);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x000615A0 File Offset: 0x0005F9A0
		public void writeInt64(long data)
		{
			this.writeByte((byte)(data >> 56));
			this.writeByte((byte)(data >> 48));
			this.writeByte((byte)(data >> 40));
			this.writeByte((byte)(data >> 32));
			this.writeByte((byte)(data >> 24));
			this.writeByte((byte)(data >> 16));
			this.writeByte((byte)(data >> 8));
			this.writeByte((byte)data);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00061604 File Offset: 0x0005FA04
		public void writeUInt64(ulong data)
		{
			this.writeByte((byte)(data >> 56));
			this.writeByte((byte)(data >> 48));
			this.writeByte((byte)(data >> 40));
			this.writeByte((byte)(data >> 32));
			this.writeByte((byte)(data >> 24));
			this.writeByte((byte)(data >> 16));
			this.writeByte((byte)(data >> 8));
			this.writeByte((byte)data);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00061665 File Offset: 0x0005FA65
		public void writeChar(char data)
		{
			this.writeUInt16((ushort)data);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00061670 File Offset: 0x0005FA70
		public void writeString(string data)
		{
			ushort num = (ushort)data.Length;
			char[] array = data.ToCharArray();
			this.writeUInt16(num);
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				char data2 = array[(int)num2];
				this.writeChar(data2);
			}
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x000616B1 File Offset: 0x0005FAB1
		public void writeBytes(byte[] data, ulong offset, ulong length)
		{
			this.stream.Write(data, (int)offset, (int)length);
		}
	}
}
