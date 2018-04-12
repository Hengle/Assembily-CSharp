using System;
using SDG.Framework.IO.FormattedFiles;
using UnityEngine;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001DA RID: 474
	public struct LandscapeCoord : IFormattedFileReadable, IFormattedFileWritable, IEquatable<LandscapeCoord>
	{
		// Token: 0x06000E35 RID: 3637 RVA: 0x000630F8 File Offset: 0x000614F8
		public LandscapeCoord(int new_x, int new_y)
		{
			this.x = new_x;
			this.y = new_y;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00063108 File Offset: 0x00061508
		public LandscapeCoord(Vector3 position)
		{
			this.x = Mathf.FloorToInt(position.x / Landscape.TILE_SIZE);
			this.y = Mathf.FloorToInt(position.z / Landscape.TILE_SIZE);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0006313A File Offset: 0x0006153A
		public void read(IFormattedFileReader reader)
		{
			reader = reader.readObject();
			this.x = reader.readValue<int>("X");
			this.y = reader.readValue<int>("Y");
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00063166 File Offset: 0x00061566
		public void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			writer.writeValue<int>("X", this.x);
			writer.writeValue<int>("Y", this.y);
			writer.endObject();
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00063196 File Offset: 0x00061596
		public static bool operator ==(LandscapeCoord a, LandscapeCoord b)
		{
			return a.x == b.x && a.y == b.y;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x000631BE File Offset: 0x000615BE
		public static bool operator !=(LandscapeCoord a, LandscapeCoord b)
		{
			return !(a == b);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x000631CC File Offset: 0x000615CC
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			LandscapeCoord landscapeCoord = (LandscapeCoord)obj;
			return this.x.Equals(landscapeCoord.x) && this.y.Equals(landscapeCoord.y);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00063214 File Offset: 0x00061614
		public override int GetHashCode()
		{
			return this.x ^ this.y;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00063224 File Offset: 0x00061624
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

		// Token: 0x06000E3E RID: 3646 RVA: 0x00063280 File Offset: 0x00061680
		public bool Equals(LandscapeCoord other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x04000919 RID: 2329
		public static LandscapeCoord ZERO = new LandscapeCoord(0, 0);

		// Token: 0x0400091A RID: 2330
		public int x;

		// Token: 0x0400091B RID: 2331
		public int y;
	}
}
