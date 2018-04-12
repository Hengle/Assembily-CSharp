using System;
using SDG.Framework.Debug;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.Translations;

namespace SDG.Framework.Devkit.Visibility
{
	// Token: 0x0200018A RID: 394
	public interface IVisibilityGroup : IInspectableListElement, IFormattedFileReadable, IFormattedFileWritable
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000BBF RID: 3007
		// (set) Token: 0x06000BBE RID: 3006
		string internalName { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000BC1 RID: 3009
		// (set) Token: 0x06000BC0 RID: 3008
		TranslationReference displayName { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000BC2 RID: 3010
		// (set) Token: 0x06000BC3 RID: 3011
		bool isVisible { get; set; }

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000BC4 RID: 3012
		// (remove) Token: 0x06000BC5 RID: 3013
		event VisibilityGroupIsVisibleChangedHandler isVisibleChanged;
	}
}
