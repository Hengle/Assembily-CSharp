using System;
using SDG.Framework.Debug;

namespace SDG.Unturned
{
	// Token: 0x02000424 RID: 1060
	public interface ITypeReference
	{
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001CBF RID: 7359
		// (set) Token: 0x06001CC0 RID: 7360
		[Inspectable("#SDG::Asset.TypeReference.Name.Name", null)]
		string assemblyQualifiedName { get; set; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001CC1 RID: 7361
		Type type { get; }

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001CC2 RID: 7362
		bool isValid { get; }
	}
}
