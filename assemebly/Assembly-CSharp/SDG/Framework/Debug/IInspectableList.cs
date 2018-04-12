using System;

namespace SDG.Framework.Debug
{
	// Token: 0x02000104 RID: 260
	public interface IInspectableList
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000826 RID: 2086
		// (remove) Token: 0x06000827 RID: 2087
		event InspectableListAddedHandler inspectorAdded;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000828 RID: 2088
		// (remove) Token: 0x06000829 RID: 2089
		event InspectableListRemovedHandler inspectorRemoved;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600082A RID: 2090
		// (remove) Token: 0x0600082B RID: 2091
		event InspectableListChangedHandler inspectorChanged;

		// Token: 0x0600082C RID: 2092
		void inspectorAdd(object instance);

		// Token: 0x0600082D RID: 2093
		void inspectorRemove(object instance);

		// Token: 0x0600082E RID: 2094
		void inspectorSet(int index);

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600082F RID: 2095
		// (set) Token: 0x06000830 RID: 2096
		bool canInspectorAdd { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000831 RID: 2097
		// (set) Token: 0x06000832 RID: 2098
		bool canInspectorRemove { get; set; }
	}
}
