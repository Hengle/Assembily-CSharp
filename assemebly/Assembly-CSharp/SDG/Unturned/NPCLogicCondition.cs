using System;

namespace SDG.Unturned
{
	// Token: 0x02000406 RID: 1030
	public class NPCLogicCondition : INPCCondition
	{
		// Token: 0x06001BCF RID: 7119 RVA: 0x00097B7B File Offset: 0x00095F7B
		public NPCLogicCondition(ENPCLogicType newLogicType, string newText, bool newShouldReset) : base(newText, newShouldReset)
		{
			this.logicType = newLogicType;
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x00097B8C File Offset: 0x00095F8C
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x00097B94 File Offset: 0x00095F94
		public ENPCLogicType logicType { get; protected set; }

		// Token: 0x06001BD2 RID: 7122 RVA: 0x00097B9D File Offset: 0x00095F9D
		protected bool doesLogicPass<T>(T a, T b) where T : IComparable
		{
			return NPCTool.doesLogicPass<T>(this.logicType, a, b);
		}
	}
}
