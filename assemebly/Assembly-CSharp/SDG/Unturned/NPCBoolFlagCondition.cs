using System;

namespace SDG.Unturned
{
	// Token: 0x020003FB RID: 1019
	public class NPCBoolFlagCondition : NPCFlagCondition
	{
		// Token: 0x06001B75 RID: 7029 RVA: 0x00097BE9 File Offset: 0x00095FE9
		public NPCBoolFlagCondition(ushort newID, bool newValue, bool newAllowUnset, ENPCLogicType newLogicType, string newText, bool newShouldReset) : base(newID, newAllowUnset, newLogicType, newText, newShouldReset)
		{
			this.value = newValue;
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x00097C00 File Offset: 0x00096000
		// (set) Token: 0x06001B77 RID: 7031 RVA: 0x00097C08 File Offset: 0x00096008
		public bool value { get; protected set; }

		// Token: 0x06001B78 RID: 7032 RVA: 0x00097C14 File Offset: 0x00096014
		public override bool isConditionMet(Player player)
		{
			short num;
			if (player.quests.getFlag(base.id, out num))
			{
				return base.doesLogicPass<bool>(num == 1, this.value);
			}
			return base.allowUnset;
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x00097C50 File Offset: 0x00096050
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

		// Token: 0x06001B7A RID: 7034 RVA: 0x00097C8B File Offset: 0x0009608B
		public override string formatCondition(Player player)
		{
			if (string.IsNullOrEmpty(this.text))
			{
				return null;
			}
			return string.Format(this.text, (!this.isConditionMet(player)) ? 0 : 1);
		}
	}
}
