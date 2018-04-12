using System;
using SDG.Framework.Debug;

namespace SDG.Unturned
{
	// Token: 0x02000391 RID: 913
	public interface IContentReference
	{
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001992 RID: 6546
		// (set) Token: 0x06001993 RID: 6547
		[Inspectable("#SDG::Asset.ContentReference.Name.Name", null)]
		string name { get; set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001994 RID: 6548
		// (set) Token: 0x06001995 RID: 6549
		[Inspectable("#SDG::Asset.ContentReference.Path.Name", null)]
		string path { get; set; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001996 RID: 6550
		bool isValid { get; }
	}
}
