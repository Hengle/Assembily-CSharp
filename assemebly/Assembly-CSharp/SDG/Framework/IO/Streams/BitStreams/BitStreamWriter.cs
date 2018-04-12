using System;
using System.IO;

namespace SDG.Framework.IO.Streams.BitStreams
{
	// Token: 0x020001C9 RID: 457
	public class BitStreamWriter
	{
		// Token: 0x06000D9D RID: 3485 RVA: 0x00060C0D File Offset: 0x0005F00D
		public BitStreamWriter(Stream newStream)
		{
			this.stream = newStream;
			this.reset();
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x00060C22 File Offset: 0x0005F022
		// (set) Token: 0x06000D9F RID: 3487 RVA: 0x00060C2A File Offset: 0x0005F02A
		public Stream stream { get; protected set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x00060C33 File Offset: 0x0005F033
		// (set) Token: 0x06000DA1 RID: 3489 RVA: 0x00060C3B File Offset: 0x0005F03B
		private byte buffer { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x00060C44 File Offset: 0x0005F044
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x00060C4C File Offset: 0x0005F04C
		private byte bitIndex { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x00060C55 File Offset: 0x0005F055
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x00060C5D File Offset: 0x0005F05D
		private byte bitsAvailable { get; set; }

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00060C66 File Offset: 0x0005F066
		public void writeBit(byte data)
		{
			this.writeBits(data, 1);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00060C70 File Offset: 0x0005F070
		public void writeBits(byte data, byte length)
		{
			if (length > this.bitsAvailable)
			{
				byte b = length - this.bitsAvailable;
				this.writeBits((byte)(data >> (int)b), this.bitsAvailable);
				this.writeBits(data, b);
			}
			else
			{
				byte b2 = 8 - length - this.bitIndex;
				byte b3 = (byte)(255 >> (int)(8 - length));
				this.buffer |= (byte)((data & b3) << (int)b2);
				this.bitIndex += length;
				this.bitsAvailable -= length;
				if (this.bitIndex == 8 && this.bitsAvailable == 0)
				{
					this.emptyBuffer();
				}
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00060D1F File Offset: 0x0005F11F
		private void emptyBuffer()
		{
			this.stream.WriteByte(this.buffer);
			this.reset();
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00060D38 File Offset: 0x0005F138
		public void flush()
		{
			if (this.bitsAvailable == 8)
			{
				return;
			}
			this.emptyBuffer();
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00060D4D File Offset: 0x0005F14D
		public void reset()
		{
			this.buffer = 0;
			this.bitIndex = 0;
			this.bitsAvailable = 8;
		}
	}
}
