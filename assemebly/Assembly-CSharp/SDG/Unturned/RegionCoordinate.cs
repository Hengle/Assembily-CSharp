using System;

namespace SDG.Unturned
{
	// Token: 0x0200068D RID: 1677
	public struct RegionCoordinate
	{
		// Token: 0x060030C3 RID: 12483 RVA: 0x0013F688 File Offset: 0x0013DA88
		public RegionCoordinate(byte x, byte y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x0013F698 File Offset: 0x0013DA98
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

		// Token: 0x04002033 RID: 8243
		public byte x;

		// Token: 0x04002034 RID: 8244
		public byte y;
	}
}
