using System;

namespace SDG.Unturned
{
	// Token: 0x02000396 RID: 918
	public class DialogueAsset : Asset
	{
		// Token: 0x060019BC RID: 6588 RVA: 0x00090EE8 File Offset: 0x0008F2E8
		public DialogueAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (id < 2000 && !bundle.hasResource && !data.has("Bypass_ID_Limit"))
			{
				throw new NotSupportedException("ID < 2000");
			}
			this.messages = new DialogueMessage[(int)data.readByte("Messages")];
			byte b = 0;
			while ((int)b < this.messages.Length)
			{
				DialoguePage[] array = new DialoguePage[(int)data.readByte("Message_" + b + "_Pages")];
				byte b2 = 0;
				while ((int)b2 < array.Length)
				{
					string text = localization.format(string.Concat(new object[]
					{
						"Message_",
						b,
						"_Page_",
						b2
					}));
					text = ItemTool.filterRarityRichText(text);
					if (string.IsNullOrEmpty(text))
					{
						Assets.errors.Add(string.Concat(new object[]
						{
							"Dialogue ",
							id,
							" missing message ",
							b,
							" page ",
							b2
						}));
						throw new NotSupportedException(string.Concat(new object[]
						{
							"Missing message ",
							b,
							" page ",
							b2
						}));
					}
					array[(int)b2] = new DialoguePage(text);
					b2 += 1;
				}
				byte[] array2 = new byte[(int)data.readByte("Message_" + b + "_Responses")];
				byte b3 = 0;
				while ((int)b3 < array2.Length)
				{
					array2[(int)b3] = data.readByte(string.Concat(new object[]
					{
						"Message_",
						b,
						"_Response_",
						b3
					}));
					b3 += 1;
				}
				ushort newPrev = data.readUInt16("Message_" + b + "_Prev");
				INPCCondition[] array3 = new INPCCondition[(int)data.readByte("Message_" + b + "_Conditions")];
				NPCTool.readConditions(data, localization, "Message_" + b + "_Condition_", array3, string.Concat(new object[]
				{
					"dialogue ",
					id,
					" message ",
					b
				}));
				INPCReward[] array4 = new INPCReward[(int)data.readByte("Message_" + b + "_Rewards")];
				NPCTool.readRewards(data, localization, "Message_" + b + "_Reward_", array4, string.Concat(new object[]
				{
					"dialogue ",
					id,
					" message ",
					b
				}));
				this.messages[(int)b] = new DialogueMessage(b, array, array2, newPrev, array3, array4);
				b += 1;
			}
			this.responses = new DialogueResponse[(int)data.readByte("Responses")];
			byte b4 = 0;
			while ((int)b4 < this.responses.Length)
			{
				byte[] array5 = new byte[(int)data.readByte("Response_" + b4 + "_Messages")];
				byte b5 = 0;
				while ((int)b5 < array5.Length)
				{
					array5[(int)b5] = data.readByte(string.Concat(new object[]
					{
						"Response_",
						b4,
						"_Message_",
						b5
					}));
					b5 += 1;
				}
				ushort newDialogue = data.readUInt16("Response_" + b4 + "_Dialogue");
				ushort newQuest = data.readUInt16("Response_" + b4 + "_Quest");
				ushort newVendor = data.readUInt16("Response_" + b4 + "_Vendor");
				string text2 = localization.format("Response_" + b4);
				text2 = ItemTool.filterRarityRichText(text2);
				if (string.IsNullOrEmpty(text2))
				{
					Assets.errors.Add(string.Concat(new object[]
					{
						"Dialogue ",
						id,
						" missing response ",
						b4
					}));
					throw new NotSupportedException("Missing response " + b4);
				}
				INPCCondition[] array6 = new INPCCondition[(int)data.readByte("Response_" + b4 + "_Conditions")];
				NPCTool.readConditions(data, localization, "Response_" + b4 + "_Condition_", array6, string.Concat(new object[]
				{
					"dialogue ",
					id,
					" response ",
					b4
				}));
				INPCReward[] array7 = new INPCReward[(int)data.readByte("Response_" + b4 + "_Rewards")];
				NPCTool.readRewards(data, localization, "Response_" + b4 + "_Reward_", array7, string.Concat(new object[]
				{
					"dialogue ",
					id,
					" response ",
					b4
				}));
				this.responses[(int)b4] = new DialogueResponse(b4, array5, newDialogue, newQuest, newVendor, text2, array6, array7);
				b4 += 1;
			}
			bundle.unload();
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x00091471 File Offset: 0x0008F871
		// (set) Token: 0x060019BE RID: 6590 RVA: 0x00091479 File Offset: 0x0008F879
		public DialogueMessage[] messages { get; protected set; }

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x00091482 File Offset: 0x0008F882
		// (set) Token: 0x060019C0 RID: 6592 RVA: 0x0009148A File Offset: 0x0008F88A
		public DialogueResponse[] responses { get; protected set; }

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x00091493 File Offset: 0x0008F893
		public override EAssetType assetCategory
		{
			get
			{
				return EAssetType.NPC;
			}
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00091498 File Offset: 0x0008F898
		public int getAvailableMessage(Player player)
		{
			for (int i = 0; i < this.messages.Length; i++)
			{
				DialogueMessage dialogueMessage = this.messages[i];
				if (dialogueMessage.areConditionsMet(player))
				{
					return i;
				}
			}
			return -1;
		}
	}
}
