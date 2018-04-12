using System;
using System.Collections.Generic;

namespace SDG.Framework.Debug
{
	// Token: 0x0200010D RID: 269
	public class InspectableList<T> : List<T>, IInspectableList
	{
		// Token: 0x06000846 RID: 2118 RVA: 0x0004CBA2 File Offset: 0x0004AFA2
		public InspectableList()
		{
			this.canInspectorAdd = true;
			this.canInspectorRemove = true;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0004CBB8 File Offset: 0x0004AFB8
		public InspectableList(int capacity) : base(capacity)
		{
			this.canInspectorAdd = false;
			this.canInspectorRemove = false;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0004CBCF File Offset: 0x0004AFCF
		public InspectableList(IEnumerable<T> collection) : base(collection)
		{
			this.canInspectorAdd = true;
			this.canInspectorRemove = true;
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000849 RID: 2121 RVA: 0x0004CBE8 File Offset: 0x0004AFE8
		// (remove) Token: 0x0600084A RID: 2122 RVA: 0x0004CC20 File Offset: 0x0004B020
		public event InspectableListAddedHandler inspectorAdded;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600084B RID: 2123 RVA: 0x0004CC58 File Offset: 0x0004B058
		// (remove) Token: 0x0600084C RID: 2124 RVA: 0x0004CC90 File Offset: 0x0004B090
		public event InspectableListRemovedHandler inspectorRemoved;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600084D RID: 2125 RVA: 0x0004CCC8 File Offset: 0x0004B0C8
		// (remove) Token: 0x0600084E RID: 2126 RVA: 0x0004CD00 File Offset: 0x0004B100
		public event InspectableListChangedHandler inspectorChanged;

		// Token: 0x0600084F RID: 2127 RVA: 0x0004CD36 File Offset: 0x0004B136
		public new void Add(T item)
		{
			base.Add(item);
			this.triggerChanged();
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0004CD48 File Offset: 0x0004B148
		public new bool Remove(T item)
		{
			bool result = base.Remove(item);
			this.triggerChanged();
			return result;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0004CD64 File Offset: 0x0004B164
		public new void RemoveAt(int index)
		{
			base.RemoveAt(index);
			this.triggerChanged();
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0004CD73 File Offset: 0x0004B173
		public virtual void inspectorAdd(object instance)
		{
			this.triggerAdded(instance);
			this.triggerChanged();
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0004CD82 File Offset: 0x0004B182
		public virtual void inspectorRemove(object instance)
		{
			this.triggerRemoved(instance);
			this.triggerChanged();
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0004CD91 File Offset: 0x0004B191
		public virtual void inspectorSet(int index)
		{
			this.triggerChanged();
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x0004CD99 File Offset: 0x0004B199
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x0004CDA1 File Offset: 0x0004B1A1
		public virtual bool canInspectorAdd { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x0004CDAA File Offset: 0x0004B1AA
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x0004CDB2 File Offset: 0x0004B1B2
		public virtual bool canInspectorRemove { get; set; }

		// Token: 0x06000859 RID: 2137 RVA: 0x0004CDBC File Offset: 0x0004B1BC
		protected virtual void triggerAdded(object instance)
		{
			InspectableListAddedHandler inspectableListAddedHandler = this.inspectorAdded;
			if (inspectableListAddedHandler != null)
			{
				inspectableListAddedHandler(this, instance);
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0004CDE0 File Offset: 0x0004B1E0
		protected virtual void triggerRemoved(object instance)
		{
			InspectableListRemovedHandler inspectableListRemovedHandler = this.inspectorRemoved;
			if (inspectableListRemovedHandler != null)
			{
				inspectableListRemovedHandler(this, instance);
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0004CE04 File Offset: 0x0004B204
		protected virtual void triggerChanged()
		{
			InspectableListChangedHandler inspectableListChangedHandler = this.inspectorChanged;
			if (inspectableListChangedHandler != null)
			{
				inspectableListChangedHandler(this);
			}
		}
	}
}
