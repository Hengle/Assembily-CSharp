using System;
using System.Collections.Generic;
using SDG.Framework.Debug;
using SDG.Framework.Translations;

namespace SDG.Framework.Devkit.Transactions
{
	// Token: 0x02000180 RID: 384
	public class DevkitTransactionManager
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0005A73E File Offset: 0x00058B3E
		// (set) Token: 0x06000B81 RID: 2945 RVA: 0x0005A745 File Offset: 0x00058B45
		[TerminalCommandProperty("transactions.history_length", "how many transactions to remember before forgetting", 25)]
		public static uint historyLength
		{
			get
			{
				return DevkitTransactionManager._historyLength;
			}
			set
			{
				DevkitTransactionManager._historyLength = value;
				TerminalUtility.printCommandPass("Set history_length to: " + DevkitTransactionManager.historyLength);
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000B82 RID: 2946 RVA: 0x0005A768 File Offset: 0x00058B68
		// (remove) Token: 0x06000B83 RID: 2947 RVA: 0x0005A79C File Offset: 0x00058B9C
		public static event DevkitTransactionPerformedHandler transactionPerformed;

		// Token: 0x06000B84 RID: 2948 RVA: 0x0005A7D0 File Offset: 0x00058BD0
		protected static void triggerTransactionPerformed(DevkitTransactionGroup group)
		{
			if (DevkitTransactionManager.transactionPerformed != null)
			{
				DevkitTransactionManager.transactionPerformed(group);
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000B85 RID: 2949 RVA: 0x0005A7E8 File Offset: 0x00058BE8
		// (remove) Token: 0x06000B86 RID: 2950 RVA: 0x0005A81C File Offset: 0x00058C1C
		public static event DevkitTransactionsChangedHandler transactionsChanged;

		// Token: 0x06000B87 RID: 2951 RVA: 0x0005A850 File Offset: 0x00058C50
		protected static void triggerTransactionsChanged()
		{
			if (DevkitTransactionManager.transactionsChanged != null)
			{
				DevkitTransactionManager.transactionsChanged();
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0005A866 File Offset: 0x00058C66
		public static bool canUndo
		{
			get
			{
				return DevkitTransactionManager.undoable.Count > 0;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0005A875 File Offset: 0x00058C75
		public static bool canRedo
		{
			get
			{
				return DevkitTransactionManager.redoable.Count > 0;
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0005A884 File Offset: 0x00058C84
		public static IEnumerable<DevkitTransactionGroup> getUndoable()
		{
			return DevkitTransactionManager.undoable;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0005A88B File Offset: 0x00058C8B
		public static IEnumerable<DevkitTransactionGroup> getRedoable()
		{
			return DevkitTransactionManager.redoable;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0005A894 File Offset: 0x00058C94
		public static DevkitTransactionGroup undo()
		{
			if (!DevkitTransactionManager.canUndo)
			{
				return null;
			}
			DevkitTransactionGroup devkitTransactionGroup = DevkitTransactionManager.popUndo();
			devkitTransactionGroup.undo();
			DevkitTransactionManager.pushRedo(devkitTransactionGroup);
			DevkitTransactionManager.triggerTransactionPerformed(devkitTransactionGroup);
			return devkitTransactionGroup;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0005A8C8 File Offset: 0x00058CC8
		public static DevkitTransactionGroup redo()
		{
			if (!DevkitTransactionManager.canRedo)
			{
				return null;
			}
			DevkitTransactionGroup devkitTransactionGroup = DevkitTransactionManager.popRedo();
			devkitTransactionGroup.redo();
			DevkitTransactionManager.pushUndo(devkitTransactionGroup);
			DevkitTransactionManager.triggerTransactionPerformed(devkitTransactionGroup);
			return devkitTransactionGroup;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0005A8FA File Offset: 0x00058CFA
		public static void beginTransaction(TranslatedText name)
		{
			if (DevkitTransactionManager.transactionDepth == 0)
			{
				DevkitTransactionManager.clearRedo();
				DevkitTransactionManager.pendingGroup = new DevkitTransactionGroup(name);
			}
			DevkitTransactionManager.transactionDepth++;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0005A922 File Offset: 0x00058D22
		public static void recordTransaction(IDevkitTransaction transaction)
		{
			if (DevkitTransactionManager.pendingGroup == null)
			{
				return;
			}
			DevkitTransactionManager.pendingGroup.record(transaction);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0005A93C File Offset: 0x00058D3C
		public static void endTransaction()
		{
			if (DevkitTransactionManager.transactionDepth == 0)
			{
				return;
			}
			DevkitTransactionManager.transactionDepth--;
			if (DevkitTransactionManager.transactionDepth == 0)
			{
				DevkitTransactionManager.pendingGroup.end();
				if (DevkitTransactionManager.pendingGroup.delta)
				{
					DevkitTransactionManager.pushUndo(DevkitTransactionManager.pendingGroup);
				}
				else
				{
					DevkitTransactionManager.pendingGroup.forget();
				}
				DevkitTransactionManager.pendingGroup = null;
				DevkitTransactionManager.triggerTransactionsChanged();
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0005A9A7 File Offset: 0x00058DA7
		public static void resetTransactions()
		{
			DevkitTransactionManager.clearUndo();
			DevkitTransactionManager.clearRedo();
			DevkitTransactionManager.pendingGroup = null;
			DevkitTransactionManager.transactionDepth = 0;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0005A9C0 File Offset: 0x00058DC0
		protected static void pushUndo(DevkitTransactionGroup group)
		{
			if ((long)DevkitTransactionManager.undoable.Count >= (long)((ulong)DevkitTransactionManager.historyLength))
			{
				DevkitTransactionManager.undoable.First.Value.forget();
				DevkitTransactionManager.undoable.RemoveFirst();
			}
			DevkitTransactionManager.undoable.AddLast(group);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0005AA10 File Offset: 0x00058E10
		protected static DevkitTransactionGroup popUndo()
		{
			DevkitTransactionGroup value = DevkitTransactionManager.undoable.Last.Value;
			DevkitTransactionManager.undoable.RemoveLast();
			return value;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0005AA38 File Offset: 0x00058E38
		protected static void clearUndo()
		{
			while (DevkitTransactionManager.undoable.Count > 0)
			{
				DevkitTransactionGroup value = DevkitTransactionManager.undoable.Last.Value;
				DevkitTransactionManager.undoable.RemoveLast();
				value.forget();
			}
			DevkitTransactionManager.undoable.Clear();
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0005AA84 File Offset: 0x00058E84
		protected static void pushRedo(DevkitTransactionGroup group)
		{
			DevkitTransactionManager.redoable.Push(group);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0005AA91 File Offset: 0x00058E91
		protected static DevkitTransactionGroup popRedo()
		{
			return DevkitTransactionManager.redoable.Pop();
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0005AAA0 File Offset: 0x00058EA0
		protected static void clearRedo()
		{
			while (DevkitTransactionManager.redoable.Count > 0)
			{
				DevkitTransactionGroup devkitTransactionGroup = DevkitTransactionManager.redoable.Pop();
				devkitTransactionGroup.forget();
			}
			DevkitTransactionManager.redoable.Clear();
		}

		// Token: 0x04000843 RID: 2115
		private static uint _historyLength = 25u;

		// Token: 0x04000846 RID: 2118
		protected static LinkedList<DevkitTransactionGroup> undoable = new LinkedList<DevkitTransactionGroup>();

		// Token: 0x04000847 RID: 2119
		protected static Stack<DevkitTransactionGroup> redoable = new Stack<DevkitTransactionGroup>();

		// Token: 0x04000848 RID: 2120
		protected static DevkitTransactionGroup pendingGroup;

		// Token: 0x04000849 RID: 2121
		protected static int transactionDepth;
	}
}
