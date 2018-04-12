using System;

namespace Pathfinding.Serialization
{
	// Token: 0x0200004E RID: 78
	public class SerializeSettings
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0001A6A8 File Offset: 0x00018AA8
		public static SerializeSettings Settings
		{
			get
			{
				return new SerializeSettings
				{
					nodes = false
				};
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0001A6C4 File Offset: 0x00018AC4
		public static SerializeSettings All
		{
			get
			{
				return new SerializeSettings
				{
					nodes = true
				};
			}
		}

		// Token: 0x0400027A RID: 634
		public bool nodes = true;

		// Token: 0x0400027B RID: 635
		public bool prettyPrint;

		// Token: 0x0400027C RID: 636
		public bool editorSettings;
	}
}
