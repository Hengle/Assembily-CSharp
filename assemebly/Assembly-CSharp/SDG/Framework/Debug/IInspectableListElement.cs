using System;
using SDG.Framework.Translations;

namespace SDG.Framework.Debug
{
	// Token: 0x02000105 RID: 261
	public interface IInspectableListElement
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000833 RID: 2099
		string inspectableListIndexInternalName { get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000834 RID: 2100
		TranslationReference inspectableListIndexDisplayName { get; }
	}
}
