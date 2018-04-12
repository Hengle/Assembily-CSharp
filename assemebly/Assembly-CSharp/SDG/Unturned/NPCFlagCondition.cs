using System;

namespace SDG.Unturned
{
	// Token: 0x02000401 RID: 1025
	public class NPCFlagCondition : NPCLogicCondition
	{
		// Token: 0x06001B9A RID: 7066 RVA: 0x00097BAC File Offset: 0x00095FAC
		public NPCFlagCondition(ushort newID, bool newAllowUnset, ENPCLogicType newLogicType, string newText, bool newShouldReset) : base(newLogicType, newText, newShouldReset)
		{
			this.id = newID;
			this.allowUnset = newAllowUnset;
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x00097BC7 File Offset: 0x00095FC7
		// (set) Token: 0x06001B9C RID: 7068 RVA: 0x00097BCF File Offset: 0x00095FCF
		public ushort id { get; protected set; }

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x00097BD8 File Offset: 0x00095FD8
		// (set) Token: 0x06001B9E RID: 7070 RVA: 0x00097BE0 File Offset: 0x00095FE0
		public bool allowUnset { get; protected set; }
	}
}
