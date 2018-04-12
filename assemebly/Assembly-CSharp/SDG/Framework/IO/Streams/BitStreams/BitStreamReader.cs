using System;
using System.IO;

namespace SDG.Framework.IO.Streams.BitStreams
{
	// Token: 0x020001C8 RID: 456
	public class BitStreamReader
	{
		// Token: 0x06000D90 RID: 3472 RVA: 0x00060ABF File Offset: 0x0005EEBF
		public BitStreamReader(Stream newStream)
		{
			this.stream = newStream;
			this.reset();
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00060AD4 File Offset: 0x0005EED4
		// (set) Token: 0x06000D92 RID: 3474 RVA: 0x00060ADC File Offset: 0x0005EEDC
		public Stream stream { get; protected set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x00060AE5 File Offset: 0x0005EEE5
		// (set) Token: 0x06000D94 RID: 3476 RVA: 0x00060AED File Offset: 0x0005EEED
		private byte buffer { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00060AF6 File Offset: 0x0005EEF6
		// (set) Token: 0x06000D96 RID: 3478 RVA: 0x00060AFE File Offset: 0x0005EEFE
		private byte bitIndex { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x00060B07 File Offset: 0x0005EF07
		// (set) Token: 0x06000D98 RID: 3480 RVA: 0x00060B0F File Offset: 0x0005EF0F
		private byte bitsAvailable { get; set; }

		// Token: 0x06000D99 RID: 3481 RVA: 0x00060B18 File Offset: 0x0005EF18
		public void readBit(ref byte data)
		{
			this.readBits(ref data, 1);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00060B24 File Offset: 0x0005EF24
		public void readBits(ref byte data, byte length)
		{
			if (this.bitIndex == 8 && this.bitsAvailable == 0)
			{
				this.fillBuffer();
			}
			if (length > this.bitsAvailable)
			{
				byte b = length - this.bitsAvailable;
				this.readBits(ref data, this.bitsAvailable);
				data = (byte)(data << (int)b);
				this.readBits(ref data, b);
			}
			else
			{
				byte b2 = 8 - length - this.bitIndex;
				byte b3 = (byte)(255 >> (int)(8 - length));
				data |= (byte)(this.buffer >> (int)b2 & (int)b3);
				this.bitIndex += length;
				this.bitsAvailable -= length;
			}
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00060BD4 File Offset: 0x0005EFD4
		private void fillBuffer()
		{
			this.buffer = (byte)this.stream.ReadByte();
			this.bitIndex = 0;
			this.bitsAvailable = 8;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00060BF6 File Offset: 0x0005EFF6
		public void reset()
		{
			this.buffer = 0;
			this.bitIndex = 8;
			this.bitsAvailable = 0;
		}
	}
}
