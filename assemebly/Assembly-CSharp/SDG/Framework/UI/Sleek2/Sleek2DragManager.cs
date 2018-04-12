using System;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002BB RID: 699
	public class Sleek2DragManager
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x00080D2F File Offset: 0x0007F12F
		// (set) Token: 0x0600144F RID: 5199 RVA: 0x00080D36 File Offset: 0x0007F136
		public static object item
		{
			get
			{
				return Sleek2DragManager._item;
			}
			set
			{
				if (Sleek2DragManager.item == value)
				{
					return;
				}
				Sleek2DragManager._item = value;
				Sleek2DragManager.dropped = false;
				Sleek2DragManager.triggerItemChanged();
			}
		}

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06001450 RID: 5200 RVA: 0x00080D58 File Offset: 0x0007F158
		// (remove) Token: 0x06001451 RID: 5201 RVA: 0x00080D8C File Offset: 0x0007F18C
		public static event Sleek2DragItemChangedHandler itemChanged;

		// Token: 0x06001452 RID: 5202 RVA: 0x00080DC0 File Offset: 0x0007F1C0
		protected static void triggerItemChanged()
		{
			if (Sleek2DragManager.itemChanged != null)
			{
				Sleek2DragManager.itemChanged();
			}
		}

		// Token: 0x04000BB7 RID: 2999
		protected static object _item;

		// Token: 0x04000BB8 RID: 3000
		public static bool dropped;

		// Token: 0x04000BB9 RID: 3001
		public static bool isDragging;
	}
}
