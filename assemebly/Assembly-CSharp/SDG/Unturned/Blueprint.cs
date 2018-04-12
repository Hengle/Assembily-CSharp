using System;

namespace SDG.Unturned
{
	// Token: 0x02000504 RID: 1284
	public class Blueprint
	{
		// Token: 0x06002315 RID: 8981 RVA: 0x000C3CCC File Offset: 0x000C20CC
		public Blueprint(ushort newSource, byte newID, EBlueprintType newType, BlueprintSupply[] newSupplies, BlueprintOutput[] newOutputs, ushort newTool, bool newToolCritical, ushort newBuild, byte newLevel, EBlueprintSkill newSkill, bool newTransferState, string newMap, INPCCondition[] newQuestConditions, INPCReward[] newQuestRewards)
		{
			this._source = newSource;
			this._id = newID;
			this._type = newType;
			this._supplies = newSupplies;
			this._outputs = newOutputs;
			this._tool = newTool;
			this._toolCritical = newToolCritical;
			this._build = newBuild;
			this._level = newLevel;
			this._skill = newSkill;
			this._transferState = newTransferState;
			this.map = newMap;
			this.questConditions = newQuestConditions;
			this.questRewards = newQuestRewards;
			this.hasSupplies = false;
			this.hasTool = false;
			this.tools = 0;
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06002316 RID: 8982 RVA: 0x000C3D61 File Offset: 0x000C2161
		public ushort source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06002317 RID: 8983 RVA: 0x000C3D69 File Offset: 0x000C2169
		public byte id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x000C3D71 File Offset: 0x000C2171
		public EBlueprintType type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x000C3D79 File Offset: 0x000C2179
		public BlueprintSupply[] supplies
		{
			get
			{
				return this._supplies;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x000C3D81 File Offset: 0x000C2181
		public BlueprintOutput[] outputs
		{
			get
			{
				return this._outputs;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x000C3D89 File Offset: 0x000C2189
		public ushort tool
		{
			get
			{
				return this._tool;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x000C3D91 File Offset: 0x000C2191
		public bool toolCritical
		{
			get
			{
				return this._toolCritical;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x000C3D99 File Offset: 0x000C2199
		public ushort build
		{
			get
			{
				return this._build;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x000C3DA1 File Offset: 0x000C21A1
		public byte level
		{
			get
			{
				return this._level;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x000C3DA9 File Offset: 0x000C21A9
		public EBlueprintSkill skill
		{
			get
			{
				return this._skill;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06002320 RID: 8992 RVA: 0x000C3DB1 File Offset: 0x000C21B1
		public bool transferState
		{
			get
			{
				return this._transferState;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06002321 RID: 8993 RVA: 0x000C3DB9 File Offset: 0x000C21B9
		// (set) Token: 0x06002322 RID: 8994 RVA: 0x000C3DC1 File Offset: 0x000C21C1
		public string map { get; private set; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x000C3DCA File Offset: 0x000C21CA
		// (set) Token: 0x06002324 RID: 8996 RVA: 0x000C3DD2 File Offset: 0x000C21D2
		public INPCCondition[] questConditions { get; protected set; }

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x000C3DDB File Offset: 0x000C21DB
		// (set) Token: 0x06002326 RID: 8998 RVA: 0x000C3DE3 File Offset: 0x000C21E3
		public INPCReward[] questRewards { get; protected set; }

		// Token: 0x06002327 RID: 8999 RVA: 0x000C3DEC File Offset: 0x000C21EC
		public bool areConditionsMet(Player player)
		{
			if (this.questConditions != null)
			{
				for (int i = 0; i < this.questConditions.Length; i++)
				{
					if (!this.questConditions[i].isConditionMet(player))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x000C3E34 File Offset: 0x000C2234
		public void applyConditions(Player player, bool shouldSend)
		{
			if (this.questConditions != null)
			{
				for (int i = 0; i < this.questConditions.Length; i++)
				{
					this.questConditions[i].applyCondition(player, shouldSend);
				}
			}
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x000C3E74 File Offset: 0x000C2274
		public void grantRewards(Player player, bool shouldSend)
		{
			if (this.questRewards != null)
			{
				for (int i = 0; i < this.questRewards.Length; i++)
				{
					this.questRewards[i].grantReward(player, shouldSend);
				}
			}
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000C3EB4 File Offset: 0x000C22B4
		public override string ToString()
		{
			string text = string.Empty;
			text += this.type;
			text += ": ";
			byte b = 0;
			while ((int)b < this.supplies.Length)
			{
				if (b > 0)
				{
					text += " + ";
				}
				text += this.supplies[(int)b].id;
				text += "x";
				text += this.supplies[(int)b].amount;
				b += 1;
			}
			text += " = ";
			byte b2 = 0;
			while ((int)b2 < this.outputs.Length)
			{
				if (b2 > 0)
				{
					text += " + ";
				}
				text += this.outputs[(int)b2].id;
				text += "x";
				text += this.outputs[(int)b2].amount;
				b2 += 1;
			}
			return text;
		}

		// Token: 0x04001516 RID: 5398
		private ushort _source;

		// Token: 0x04001517 RID: 5399
		private byte _id;

		// Token: 0x04001518 RID: 5400
		private EBlueprintType _type;

		// Token: 0x04001519 RID: 5401
		private BlueprintSupply[] _supplies;

		// Token: 0x0400151A RID: 5402
		private BlueprintOutput[] _outputs;

		// Token: 0x0400151B RID: 5403
		private ushort _tool;

		// Token: 0x0400151C RID: 5404
		private bool _toolCritical;

		// Token: 0x0400151D RID: 5405
		private ushort _build;

		// Token: 0x0400151E RID: 5406
		private byte _level;

		// Token: 0x0400151F RID: 5407
		private EBlueprintSkill _skill;

		// Token: 0x04001520 RID: 5408
		private bool _transferState;

		// Token: 0x04001522 RID: 5410
		public bool hasSupplies;

		// Token: 0x04001523 RID: 5411
		public bool hasTool;

		// Token: 0x04001524 RID: 5412
		public bool hasItem;

		// Token: 0x04001525 RID: 5413
		public bool hasSkills;

		// Token: 0x04001526 RID: 5414
		public ushort tools;

		// Token: 0x04001527 RID: 5415
		public ushort products;

		// Token: 0x04001528 RID: 5416
		public ushort items;
	}
}
