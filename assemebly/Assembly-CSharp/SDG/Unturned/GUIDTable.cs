using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x020003B8 RID: 952
	public class GUIDTable
	{
		// Token: 0x1400006F RID: 111
		// (add) Token: 0x060019FF RID: 6655 RVA: 0x00091A34 File Offset: 0x0008FE34
		// (remove) Token: 0x06001A00 RID: 6656 RVA: 0x00091A68 File Offset: 0x0008FE68
		public static event GUIDTableMappingAddedHandler mappingAdded;

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x06001A01 RID: 6657 RVA: 0x00091A9C File Offset: 0x0008FE9C
		// (remove) Token: 0x06001A02 RID: 6658 RVA: 0x00091AD0 File Offset: 0x0008FED0
		public static event GUIDTableCleared cleared;

		// Token: 0x06001A03 RID: 6659 RVA: 0x00091B04 File Offset: 0x0008FF04
		public static Guid resolveIndex(GUIDTableIndex index)
		{
			Guid result;
			if (GUIDTable.indexToGUIDDictionary.TryGetValue(index, out result))
			{
				return result;
			}
			return Guid.Empty;
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00091B2C File Offset: 0x0008FF2C
		public static GUIDTableIndex resolveGUID(Guid GUID)
		{
			GUIDTableIndex result;
			if (GUIDTable.GUIDToIndexDictionary.TryGetValue(GUID, out result))
			{
				return result;
			}
			return GUIDTableIndex.invalid;
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x00091B52 File Offset: 0x0008FF52
		public static List<KeyValuePair<GUIDTableIndex, Guid>> toList()
		{
			return GUIDTable.tableList;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00091B59 File Offset: 0x0008FF59
		public static void addMapping(GUIDTableIndex index, Guid GUID)
		{
			GUIDTable.indexToGUIDDictionary.Add(index, GUID);
			GUIDTable.GUIDToIndexDictionary.Add(GUID, index);
			GUIDTable.tableList.Add(new KeyValuePair<GUIDTableIndex, Guid>(index, GUID));
			GUIDTable.triggerMappingAdded(index, GUID);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x00091B8C File Offset: 0x0008FF8C
		public static GUIDTableIndex add(Guid GUID)
		{
			GUIDTableIndex guidtableIndex = GUIDTable.createIndex();
			GUIDTable.addMapping(guidtableIndex, GUID);
			return guidtableIndex;
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00091BA7 File Offset: 0x0008FFA7
		public static void clear()
		{
			GUIDTable.indexToGUIDDictionary.Clear();
			GUIDTable.GUIDToIndexDictionary.Clear();
			GUIDTable.tableList.Clear();
			GUIDTable.currentTableIndex = 0;
			GUIDTable.triggerCleared();
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x00091BD7 File Offset: 0x0008FFD7
		private static void triggerMappingAdded(GUIDTableIndex index, Guid GUID)
		{
			if (GUIDTable.mappingAdded != null)
			{
				GUIDTable.mappingAdded(index, GUID);
			}
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00091BEF File Offset: 0x0008FFEF
		private static void triggerCleared()
		{
			if (GUIDTable.cleared != null)
			{
				GUIDTable.cleared();
			}
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00091C08 File Offset: 0x00090008
		private static GUIDTableIndex createIndex()
		{
			GUIDTableIndex result = GUIDTable.currentTableIndex;
			GUIDTable.currentTableIndex.index = GUIDTable.currentTableIndex.index + 1;
			return result;
		}

		// Token: 0x04000EE9 RID: 3817
		private static Dictionary<GUIDTableIndex, Guid> indexToGUIDDictionary = new Dictionary<GUIDTableIndex, Guid>();

		// Token: 0x04000EEA RID: 3818
		private static Dictionary<Guid, GUIDTableIndex> GUIDToIndexDictionary = new Dictionary<Guid, GUIDTableIndex>();

		// Token: 0x04000EEB RID: 3819
		private static List<KeyValuePair<GUIDTableIndex, Guid>> tableList = new List<KeyValuePair<GUIDTableIndex, Guid>>();

		// Token: 0x04000EEC RID: 3820
		private static GUIDTableIndex currentTableIndex = 0;
	}
}
