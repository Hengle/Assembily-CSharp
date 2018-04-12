using System;

namespace SDG.Framework.Debug
{
	// Token: 0x02000100 RID: 256
	public class GCCollectCommand
	{
		// Token: 0x06000819 RID: 2073 RVA: 0x0004CAEB File Offset: 0x0004AEEB
		[TerminalCommandMethod("gc.collect", "force garbage collection, only useful for debugging")]
		public static void gc_collect()
		{
			GC.Collect();
			TerminalUtility.printCommandPass("Garbage collected!");
		}
	}
}
