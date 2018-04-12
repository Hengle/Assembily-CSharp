using System;
using SDG.Framework.IO.FormattedFiles;
using UnityEngine;

namespace SDG.Framework.Foliage
{
	// Token: 0x0200019A RID: 410
	public struct FoliageCoord : IFormattedFileReadable, IFormattedFileWritable, IEquatable<FoliageCoord>
	{
		// Token: 0x06000C01 RID: 3073 RVA: 0x0005B968 File Offset: 0x00059D68
		public FoliageCoord(int new_x, int new_y)
		{
			this.x = new_x;
			this.y = new_y;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0005B978 File Offset: 0x00059D78
		public FoliageCoord(Vector3 position)
		{
			this.x = Mathf.FloorToInt(position.x / FoliageSystem.TILE_SIZE);
			this.y = Mathf.FloorToInt(position.z / FoliageSystem.TILE_SIZE);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0005B9AA File Offset: 0x00059DAA
		public void read(IFormattedFileReader reader)
		{
			reader = reader.readObject();
			this.x = reader.readValue<int>("X");
			this.y = reader.readValue<int>("Y");
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0005B9D6 File Offset: 0x00059DD6
		public void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			writer.writeValue<int>("X", this.x);
			writer.writeValue<int>("Y", this.y);
			writer.endObject();
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0005BA06 File Offset: 0x00059E06
		public static bool operator ==(FoliageCoord a, FoliageCoord b)
		{
			return a.x == b.x && a.y == b.y;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0005BA2E File Offset: 0x00059E2E
		public static bool operator !=(FoliageCoord a, FoliageCoord b)
		{
			return !(a == b);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0005BA3C File Offset: 0x00059E3C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			FoliageCoord foliageCoord = (FoliageCoord)obj;
			return this.x == foliageCoord.x && this.y == foliageCoord.y;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0005BA7C File Offset: 0x00059E7C
		public override int GetHashCode()
		{
			return this.x ^ this.y;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0005BA8C File Offset: 0x00059E8C
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

		// Token: 0x06000C0A RID: 3082 RVA: 0x0005BAE8 File Offset: 0x00059EE8
		public bool Equals(FoliageCoord other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x04000876 RID: 2166
		public static FoliageCoord ZERO = new FoliageCoord(0, 0);

		// Token: 0x04000877 RID: 2167
		public int x;

		// Token: 0x04000878 RID: 2168
		public int y;
	}
}
