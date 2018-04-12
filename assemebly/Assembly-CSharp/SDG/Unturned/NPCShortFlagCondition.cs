using System;

namespace SDG.Unturned
{
	// Token: 0x02000412 RID: 1042
	public class NPCShortFlagCondition : NPCFlagCondition
	{
		// Token: 0x06001C15 RID: 7189 RVA: 0x000993A7 File Offset: 0x000977A7
		public NPCShortFlagCondition(ushort newID, short newValue, bool newAllowUnset, ENPCLogicType newLogicType, string newText, bool newShouldReset) : base(newID, newAllowUnset, newLogicType, newText, newShouldReset)
		{
			this.value = newValue;
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x000993BE File Offset: 0x000977BE
		// (set) Token: 0x06001C17 RID: 7191 RVA: 0x000993C6 File Offset: 0x000977C6
		public short value { get; protected set; }

		// Token: 0x06001C18 RID: 7192 RVA: 0x000993D0 File Offset: 0x000977D0
		public override bool isConditionMet(Player player)
		{
			short a;
			if (player.quests.getFlag(base.id, out a))
			{
				return base.doesLogicPass<short>(a, this.value);
			}
			return base.allowUnset;
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00099409 File Offset: 0x00097809
		public override void applyCondition(Player player, bool shouldSend)
		{
			if (!this.shouldReset)
			{
				return;
			}
			if (shouldSend)
			{
				player.quests.sendRemoveFlag(base.id);
			}
			else
			{
				player.quests.removeFlag(base.id);
			}
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00099444 File Offset: 0x00097844
		public override string formatCondition(Player player)
		{
			if (string.IsNullOrEmpty(this.text))
			{
				return null;
			}
			short num;
			if (!player.quests.getFlag(base.id, out num))
			{
				if (base.allowUnset)
				{
					num = this.value;
				}
				else
				{
					num = 0;
				}
			}
			return string.Format(this.text, num, this.value);
		}
	}
}
