using System;
using SDG.Framework.IO.FormattedFiles;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200068A RID: 1674
	public struct RegionCoord : IFormattedFileReadable, IFormattedFileWritable, IEquatable<RegionCoord>
	{
		// Token: 0x060030B0 RID: 12464 RVA: 0x0013F4F0 File Offset: 0x0013D8F0
		public RegionCoord(byte new_x, byte new_y)
		{
			this.x = new_x;
			this.y = new_y;
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x0013F500 File Offset: 0x0013D900
		public RegionCoord(Vector3 position)
		{
			Regions.tryGetCoordinate(position, out this.x, out this.y);
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x0013F515 File Offset: 0x0013D915
		public void read(IFormattedFileReader reader)
		{
			reader = reader.readObject();
			this.x = reader.readValue<byte>("X");
			this.y = reader.readValue<byte>("Y");
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x0013F541 File Offset: 0x0013D941
		public void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			writer.writeValue<byte>("X", this.x);
			writer.writeValue<byte>("Y", this.y);
			writer.endObject();
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x0013F571 File Offset: 0x0013D971
		public static bool operator ==(RegionCoord a, RegionCoord b)
		{
			return a.x == b.x && a.y == b.y;
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x0013F599 File Offset: 0x0013D999
		public static bool operator !=(RegionCoord a, RegionCoord b)
		{
			return !(a == b);
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x0013F5A8 File Offset: 0x0013D9A8
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			RegionCoord regionCoord = (RegionCoord)obj;
			return this.x == regionCoord.x && this.y == regionCoord.y;
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x0013F5E8 File Offset: 0x0013D9E8
		public override int GetHashCode()
		{
			return (int)(this.x ^ this.y);
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x0013F5F8 File Offset: 0x0013D9F8
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				'(',
				this.x.ToString(),
				", ",
				this.y.ToString(),
				')'
			});
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x0013F654 File Offset: 0x0013DA54
		public bool Equals(RegionCoord other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x04002030 RID: 8240
		public static RegionCoord ZERO = new RegionCoord(0, 0);

		// Token: 0x04002031 RID: 8241
		public byte x;

		// Token: 0x04002032 RID: 8242
		public byte y;
	}
}
