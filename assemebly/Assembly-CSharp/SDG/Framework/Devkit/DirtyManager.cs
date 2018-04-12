using System;
using System.Collections.Generic;

namespace SDG.Framework.Devkit
{
	// Token: 0x0200013B RID: 315
	public class DirtyManager
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0004FD8C File Offset: 0x0004E18C
		public static List<IDirtyable> dirty
		{
			get
			{
				return DirtyManager._dirty;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0004FD93 File Offset: 0x0004E193
		public static HashSet<IDirtyable> notSaveable
		{
			get
			{
				return DirtyManager._notSaveable;
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600099A RID: 2458 RVA: 0x0004FD9C File Offset: 0x0004E19C
		// (remove) Token: 0x0600099B RID: 2459 RVA: 0x0004FDD0 File Offset: 0x0004E1D0
		public static event MarkedDirtyHandler markedDirty;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600099C RID: 2460 RVA: 0x0004FE04 File Offset: 0x0004E204
		// (remove) Token: 0x0600099D RID: 2461 RVA: 0x0004FE38 File Offset: 0x0004E238
		public static event MarkedCleanHandler markedClean;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600099E RID: 2462 RVA: 0x0004FE6C File Offset: 0x0004E26C
		// (remove) Token: 0x0600099F RID: 2463 RVA: 0x0004FEA0 File Offset: 0x0004E2A0
		public static event SaveableChangedHandler saveableChanged;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060009A0 RID: 2464 RVA: 0x0004FED4 File Offset: 0x0004E2D4
		// (remove) Token: 0x060009A1 RID: 2465 RVA: 0x0004FF08 File Offset: 0x0004E308
		public static event DirtySaved saved;

		// Token: 0x060009A2 RID: 2466 RVA: 0x0004FF3C File Offset: 0x0004E33C
		public static void markDirty(IDirtyable item)
		{
			DirtyManager.dirty.Add(item);
			DirtyManager.triggerMarkedDirty(item);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0004FF4F File Offset: 0x0004E34F
		public static void markClean(IDirtyable item)
		{
			if (DirtyManager.isSaving)
			{
				return;
			}
			DirtyManager.dirty.Remove(item);
			DirtyManager.triggerMarkedClean(item);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0004FF6E File Offset: 0x0004E36E
		public static bool checkSaveable(IDirtyable item)
		{
			return !DirtyManager.notSaveable.Contains(item);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0004FF7E File Offset: 0x0004E37E
		public static void toggleSaveable(IDirtyable item)
		{
			if (!DirtyManager.notSaveable.Remove(item))
			{
				DirtyManager.notSaveable.Add(item);
				DirtyManager.triggerSaveableChanged(item, true);
			}
			else
			{
				DirtyManager.triggerSaveableChanged(item, false);
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0004FFB0 File Offset: 0x0004E3B0
		public static void save()
		{
			DirtyManager.isSaving = true;
			for (int i = DirtyManager.dirty.Count - 1; i >= 0; i--)
			{
				IDirtyable dirtyable = DirtyManager.dirty[i];
				if (!DirtyManager.notSaveable.Contains(dirtyable))
				{
					dirtyable.save();
					dirtyable.isDirty = false;
					DirtyManager.dirty.RemoveAt(i);
				}
			}
			DirtyManager.isSaving = false;
			DirtyManager.triggerSaved();
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00050024 File Offset: 0x0004E424
		protected static void triggerMarkedDirty(IDirtyable item)
		{
			if (DirtyManager.markedDirty != null)
			{
				DirtyManager.markedDirty(item);
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0005003B File Offset: 0x0004E43B
		protected static void triggerMarkedClean(IDirtyable item)
		{
			if (DirtyManager.markedClean != null)
			{
				DirtyManager.markedClean(item);
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00050052 File Offset: 0x0004E452
		protected static void triggerSaveableChanged(IDirtyable item, bool isSaveable)
		{
			if (DirtyManager.saveableChanged != null)
			{
				DirtyManager.saveableChanged(item, isSaveable);
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0005006A File Offset: 0x0004E46A
		protected static void triggerSaved()
		{
			if (DirtyManager.saved != null)
			{
				DirtyManager.saved();
			}
		}

		// Token: 0x0400072D RID: 1837
		protected static List<IDirtyable> _dirty = new List<IDirtyable>();

		// Token: 0x0400072E RID: 1838
		public static HashSet<IDirtyable> _notSaveable = new HashSet<IDirtyable>();

		// Token: 0x04000733 RID: 1843
		protected static bool isSaving;
	}
}
