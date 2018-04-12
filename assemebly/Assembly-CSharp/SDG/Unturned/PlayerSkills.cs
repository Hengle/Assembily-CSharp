﻿using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000647 RID: 1607
	public class PlayerSkills : PlayerCaller
	{
		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002E33 RID: 11827 RVA: 0x00128203 File Offset: 0x00126603
		public Skill[][] skills
		{
			get
			{
				return this._skills;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002E34 RID: 11828 RVA: 0x0012820B File Offset: 0x0012660B
		public EPlayerBoost boost
		{
			get
			{
				return this._boost;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002E35 RID: 11829 RVA: 0x00128213 File Offset: 0x00126613
		public uint experience
		{
			get
			{
				return this._experience;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002E36 RID: 11830 RVA: 0x0012821B File Offset: 0x0012661B
		public int reputation
		{
			get
			{
				return this._reputation;
			}
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x00128224 File Offset: 0x00126624
		[SteamCall]
		public void tellExperience(CSteamID steamID, uint newExperience)
		{
			if (base.channel.checkServer(steamID))
			{
				if (base.channel.isOwner && newExperience > this.experience && Level.info.type != ELevelType.HORDE && this.wasLoaded)
				{
					int num;
					if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Found_Experience", out num))
					{
						Provider.provider.statisticsService.userStatisticsService.setStatistic("Found_Experience", num + (int)(newExperience - this.experience));
					}
					PlayerUI.message(EPlayerMessage.EXPERIENCE, (newExperience - this.experience).ToString());
				}
				this._experience = newExperience;
				if (this.onExperienceUpdated != null)
				{
					this.onExperienceUpdated(this.experience);
				}
			}
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x00128300 File Offset: 0x00126700
		[SteamCall]
		public void tellReputation(CSteamID steamID, int newReputation)
		{
			if (base.channel.checkServer(steamID))
			{
				if (base.channel.isOwner && newReputation != this.reputation && Level.info.type != ELevelType.HORDE && this.wasLoaded)
				{
					bool flag2;
					if (newReputation <= -200)
					{
						bool flag;
						if (Provider.provider.achievementsService.getAchievement("Villain", out flag) && !flag)
						{
							Provider.provider.achievementsService.setAchievement("Villain");
						}
					}
					else if (newReputation >= 200 && Provider.provider.achievementsService.getAchievement("Paragon", out flag2) && !flag2)
					{
						Provider.provider.achievementsService.setAchievement("Paragon");
					}
					string text = (newReputation - this.reputation).ToString();
					if (newReputation > this.reputation)
					{
						text = '+' + text;
					}
					PlayerUI.message(EPlayerMessage.REPUTATION, text);
				}
				this._reputation = newReputation;
				if (this.onReputationUpdated != null)
				{
					this.onReputationUpdated(this.reputation);
				}
			}
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x00128438 File Offset: 0x00126838
		[SteamCall]
		public void tellBoost(CSteamID steamID, byte newBoost)
		{
			if (base.channel.checkServer(steamID))
			{
				this._boost = (EPlayerBoost)newBoost;
				if (this.onBoostUpdated != null)
				{
					this.onBoostUpdated(this.boost);
				}
				this.wasLoaded = true;
			}
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x00128478 File Offset: 0x00126878
		[SteamCall]
		public void tellSkill(CSteamID steamID, byte speciality, byte index, byte level)
		{
			if (base.channel.checkServer(steamID))
			{
				if ((int)index >= this.skills[(int)speciality].Length)
				{
					return;
				}
				this.skills[(int)speciality][(int)index].level = level;
				if (base.channel.isOwner)
				{
					bool flag = true;
					bool flag2 = true;
					bool flag3 = true;
					for (int i = 0; i < this.skills[0].Length; i++)
					{
						if (this.skills[0][i].level < this.skills[0][i].max)
						{
							flag = false;
							break;
						}
					}
					for (int j = 0; j < this.skills[1].Length; j++)
					{
						if (this.skills[1][j].level < this.skills[1][j].max)
						{
							flag2 = false;
							break;
						}
					}
					for (int k = 0; k < this.skills[2].Length; k++)
					{
						if (this.skills[2][k].level < this.skills[2][k].max)
						{
							flag3 = false;
							break;
						}
					}
					bool flag4;
					if (flag && Provider.provider.achievementsService.getAchievement("Offense", out flag4) && !flag4)
					{
						Provider.provider.achievementsService.setAchievement("Offense");
					}
					bool flag5;
					if (flag2 && Provider.provider.achievementsService.getAchievement("Defense", out flag5) && !flag5)
					{
						Provider.provider.achievementsService.setAchievement("Defense");
					}
					bool flag6;
					if (flag3 && Provider.provider.achievementsService.getAchievement("Support", out flag6) && !flag6)
					{
						Provider.provider.achievementsService.setAchievement("Support");
					}
					bool flag7;
					if (flag && flag2 && flag3 && Provider.provider.achievementsService.getAchievement("Mastermind", out flag7) && !flag7)
					{
						Provider.provider.achievementsService.setAchievement("Mastermind");
					}
				}
				if (this.onSkillsUpdated != null)
				{
					this.onSkillsUpdated();
				}
			}
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x001286BF File Offset: 0x00126ABF
		public float mastery(int speciality, int index)
		{
			return this.skills[speciality][index].mastery;
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x001286D0 File Offset: 0x00126AD0
		public uint cost(int speciality, int index)
		{
			if (Level.info != null && Level.info.type != ELevelType.ARENA)
			{
				byte b = 0;
				while ((int)b < PlayerSkills.SKILLSETS[(int)((byte)base.channel.owner.skillset)].Length)
				{
					SpecialitySkillPair specialitySkillPair = PlayerSkills.SKILLSETS[(int)((byte)base.channel.owner.skillset)][(int)b];
					if (speciality == specialitySkillPair.speciality && index == specialitySkillPair.skill)
					{
						return this.skills[speciality][index].cost / 2u;
					}
					b += 1;
				}
			}
			return this.skills[speciality][index].cost;
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x00128778 File Offset: 0x00126B78
		public void askSpend(uint cost)
		{
			if (base.channel.isOwner)
			{
				base.channel.send("tellExperience", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					this.experience - cost
				});
			}
			else
			{
				this._experience -= cost;
				base.channel.send("tellExperience", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					this.experience
				});
			}
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x001287F8 File Offset: 0x00126BF8
		public void askAward(uint award)
		{
			if (base.channel.isOwner)
			{
				base.channel.send("tellExperience", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					this.experience + award
				});
			}
			else
			{
				this._experience += award;
				base.channel.send("tellExperience", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					this.experience
				});
			}
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x00128878 File Offset: 0x00126C78
		public void askRep(int rep)
		{
			base.channel.send("tellReputation", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
			{
				this.reputation + rep
			});
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x001288B0 File Offset: 0x00126CB0
		public void askPay(uint pay)
		{
			pay = (uint)(pay * Provider.modeConfigData.Players.Experience_Multiplier);
			if (base.channel.isOwner)
			{
				base.channel.send("tellExperience", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					this.experience + pay
				});
			}
			else
			{
				this._experience += pay;
				base.channel.send("tellExperience", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					this.experience
				});
			}
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x00128946 File Offset: 0x00126D46
		public void modRep(int rep)
		{
			this._reputation += rep;
			if (this.onReputationUpdated != null)
			{
				this.onReputationUpdated(this.reputation);
			}
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x00128972 File Offset: 0x00126D72
		public void modXp(uint xp)
		{
			this._experience += xp;
			if (this.onExperienceUpdated != null)
			{
				this.onExperienceUpdated(this.experience);
			}
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x0012899E File Offset: 0x00126D9E
		public void modXp2(uint xp)
		{
			this._experience -= xp;
			if (this.onExperienceUpdated != null)
			{
				this.onExperienceUpdated(this.experience);
			}
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x001289CC File Offset: 0x00126DCC
		[SteamCall]
		public void askUpgrade(CSteamID steamID, byte speciality, byte index, bool force)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (!base.player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if (speciality >= PlayerSkills.SPECIALITIES)
				{
					return;
				}
				if ((int)index >= this.skills[(int)speciality].Length)
				{
					return;
				}
				Skill skill = this.skills[(int)speciality][(int)index];
				bool flag = false;
				while (this.experience >= this.cost((int)speciality, (int)index) && skill.level < skill.max)
				{
					this._experience -= this.cost((int)speciality, (int)index);
					Skill skill2 = skill;
					skill2.level += 1;
					flag = true;
					if (!force)
					{
						IL_BB:
						if (flag)
						{
							base.channel.send("tellExperience", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								this.experience
							});
							base.channel.send("tellSkill", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
							{
								speciality,
								index,
								skill.level
							});
							return;
						}
						return;
					}
				}
				goto IL_BB;
			}
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x00128AFC File Offset: 0x00126EFC
		[SteamCall]
		public void askBoost(CSteamID steamID)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (!base.player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if (this.experience >= PlayerSkills.BOOST_COST)
				{
					this._experience -= PlayerSkills.BOOST_COST;
					byte b;
					do
					{
						b = (byte)UnityEngine.Random.Range(1, (int)(PlayerSkills.BOOST_COUNT + 1));
					}
					while (b == (byte)this.boost);
					this._boost = (EPlayerBoost)b;
					base.channel.send("tellExperience", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						this.experience
					});
					base.channel.send("tellBoost", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						(byte)this.boost
					});
				}
			}
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x00128BCC File Offset: 0x00126FCC
		[SteamCall]
		public void askPurchase(CSteamID steamID, byte index)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (!base.player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if ((int)index >= LevelNodes.nodes.Count)
				{
					return;
				}
				PurchaseNode purchaseNode = null;
				try
				{
					purchaseNode = (PurchaseNode)LevelNodes.nodes[(int)index];
				}
				catch
				{
					return;
				}
				if (this.experience >= purchaseNode.cost)
				{
					this._experience -= purchaseNode.cost;
					base.channel.send("tellExperience", ESteamCall.OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						this.experience
					});
					ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, purchaseNode.id);
					if (itemAsset.type == EItemType.GUN && base.player.inventory.has(purchaseNode.id) != null)
					{
						base.player.inventory.tryAddItem(new Item(((ItemGunAsset)itemAsset).getMagazineID(), EItemOrigin.ADMIN), true);
					}
					else
					{
						base.player.inventory.tryAddItem(new Item(purchaseNode.id, EItemOrigin.ADMIN), true);
					}
				}
			}
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x00128D10 File Offset: 0x00127110
		public void sendUpgrade(byte speciality, byte index, bool force)
		{
			base.channel.send("askUpgrade", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				speciality,
				index,
				force
			});
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x00128D46 File Offset: 0x00127146
		public void sendBoost()
		{
			base.channel.send("askBoost", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x00128D64 File Offset: 0x00127164
		public void sendPurchase(PurchaseNode node)
		{
			int num = LevelNodes.nodes.FindIndex((Node x) => x == node);
			base.channel.send("askPurchase", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
			{
				(byte)num
			});
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x00128DB8 File Offset: 0x001271B8
		[SteamCall]
		public void tellSkills(CSteamID steamID, byte speciality, byte[] newLevels)
		{
			if (base.channel.checkServer(steamID))
			{
				byte b = 0;
				while ((int)b < newLevels.Length)
				{
					if ((int)b >= this.skills[(int)speciality].Length)
					{
						break;
					}
					this.skills[(int)speciality][(int)b].level = newLevels[(int)b];
					b += 1;
				}
				if (this.onSkillsUpdated != null)
				{
					this.onSkillsUpdated();
				}
			}
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x00128E28 File Offset: 0x00127228
		[SteamCall]
		public void askSkills(CSteamID steamID)
		{
			if (Provider.isServer)
			{
				byte b = 0;
				while ((int)b < this.skills.Length)
				{
					byte[] array = new byte[this.skills[(int)b].Length];
					byte b2 = 0;
					while ((int)b2 < array.Length)
					{
						array[(int)b2] = this.skills[(int)b][(int)b2].level;
						b2 += 1;
					}
					base.channel.send("tellSkills", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						b,
						array
					});
					b += 1;
				}
				base.channel.send("tellExperience", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					this.experience
				});
				base.channel.send("tellReputation", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					this.reputation
				});
				base.channel.send("tellBoost", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					(byte)this.boost
				});
			}
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x00128F2C File Offset: 0x0012732C
		private void onLifeUpdated(bool isDead)
		{
			if (isDead && Provider.isServer)
			{
				if (Level.info == null || Level.info.type == ELevelType.SURVIVAL)
				{
					float num = (!base.player.life.wasPvPDeath) ? Provider.modeConfigData.Players.Lose_Skills_PvE : Provider.modeConfigData.Players.Lose_Skills_PvP;
					byte b = 0;
					while ((int)b < this.skills.Length)
					{
						byte[] array = new byte[this.skills[(int)b].Length];
						byte b2 = 0;
						while ((int)b2 < array.Length)
						{
							bool flag = true;
							byte b3 = 0;
							while ((int)b3 < PlayerSkills.SKILLSETS[(int)((byte)base.channel.owner.skillset)].Length)
							{
								SpecialitySkillPair specialitySkillPair = PlayerSkills.SKILLSETS[(int)((byte)base.channel.owner.skillset)][(int)b3];
								if ((int)b == specialitySkillPair.speciality && (int)b2 == specialitySkillPair.skill)
								{
									flag = false;
									break;
								}
								b3 += 1;
							}
							if (flag)
							{
								array[(int)b2] = (byte)((float)this.skills[(int)b][(int)b2].level * num);
							}
							else
							{
								array[(int)b2] = this.skills[(int)b][(int)b2].level;
							}
							b2 += 1;
						}
						base.channel.send("tellSkills", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							b,
							array
						});
						b += 1;
					}
					this._experience = (uint)(this.experience * num);
				}
				else
				{
					byte b4 = 0;
					while ((int)b4 < this.skills.Length)
					{
						byte b5 = 0;
						while ((int)b5 < this.skills[(int)b4].Length)
						{
							this.skills[(int)b4][(int)b5].level = 0;
							b5 += 1;
						}
						b4 += 1;
					}
					byte b6 = 0;
					while ((int)b6 < this.skills.Length)
					{
						byte[] array2 = new byte[this.skills[(int)b6].Length];
						byte b7 = 0;
						while ((int)b7 < array2.Length)
						{
							array2[(int)b7] = this.skills[(int)b6][(int)b7].level;
							b7 += 1;
						}
						base.channel.send("tellSkills", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
						{
							b6,
							array2
						});
						b6 += 1;
					}
					if (Level.info.type == ELevelType.ARENA)
					{
						this._experience = 0u;
					}
					else
					{
						this._experience = (uint)(this.experience * 0.75f);
					}
				}
				this._boost = EPlayerBoost.NONE;
				base.channel.send("tellExperience", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					this.experience
				});
				base.channel.send("tellBoost", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					(byte)this.boost
				});
			}
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x0012921A File Offset: 0x0012761A
		public void init()
		{
			base.channel.send("askSkills", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x00129238 File Offset: 0x00127638
		private void Start()
		{
			this._skills = new Skill[(int)PlayerSkills.SPECIALITIES][];
			this.skills[0] = new Skill[7];
			this.skills[0][0] = new Skill(0, 7, 10u, 1f);
			this.skills[0][1] = new Skill(0, 7, 10u, 1f);
			this.skills[0][2] = new Skill(0, 5, 10u, 0.5f);
			this.skills[0][3] = new Skill(0, 5, 10u, 0.5f);
			this.skills[0][4] = new Skill(0, 5, 10u, 0.5f);
			this.skills[0][5] = new Skill(0, 5, 10u, 0.5f);
			this.skills[0][6] = new Skill(0, 5, 20u, 0.5f);
			this.skills[1] = new Skill[7];
			this.skills[1][0] = new Skill(0, 7, 10u, 1f);
			this.skills[1][1] = new Skill(0, 5, 10u, 0.5f);
			this.skills[1][2] = new Skill(0, 5, 10u, 0.5f);
			this.skills[1][3] = new Skill(0, 5, 10u, 0.5f);
			this.skills[1][4] = new Skill(0, 5, 10u, 0.5f);
			this.skills[1][5] = new Skill(0, 5, 10u, 0.5f);
			this.skills[1][6] = new Skill(0, 5, 10u, 0.5f);
			this.skills[2] = new Skill[8];
			this.skills[2][0] = new Skill(0, 7, 10u, 1f);
			this.skills[2][1] = new Skill(0, 3, 20u, 1.5f);
			this.skills[2][2] = new Skill(0, 5, 10u, 0.5f);
			this.skills[2][3] = new Skill(0, 3, 20u, 1.5f);
			this.skills[2][4] = new Skill(0, 5, 10u, 0.5f);
			this.skills[2][5] = new Skill(0, 7, 10u, 1f);
			this.skills[2][6] = new Skill(0, 5, 10u, 0.5f);
			this.skills[2][7] = new Skill(0, 3, 20u, 1.5f);
			if (Provider.isServer)
			{
				this.load();
				PlayerLife life = base.player.life;
				life.onLifeUpdated = (LifeUpdated)Delegate.Combine(life.onLifeUpdated, new LifeUpdated(this.onLifeUpdated));
			}
			else
			{
				this._experience = uint.MaxValue;
				this._reputation = 0;
			}
			base.Invoke("init", 0.1f);
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x001294F0 File Offset: 0x001278F0
		public void load()
		{
			if (PlayerSavedata.fileExists(base.channel.owner.playerID, "/Player/Skills.dat") && Level.info.type == ELevelType.SURVIVAL)
			{
				Block block = PlayerSavedata.readBlock(base.channel.owner.playerID, "/Player/Skills.dat", 0);
				byte b = block.readByte();
				if (b > 4)
				{
					this._experience = block.readUInt32();
					if (b >= 7)
					{
						this._reputation = block.readInt32();
					}
					else
					{
						this._reputation = 0;
					}
					this._boost = (EPlayerBoost)block.readByte();
					if (b >= 6)
					{
						byte b2 = 0;
						while ((int)b2 < this.skills.Length)
						{
							if (this.skills[(int)b2] != null)
							{
								byte b3 = 0;
								while ((int)b3 < this.skills[(int)b2].Length)
								{
									this.skills[(int)b2][(int)b3].level = block.readByte();
									if (this.skills[(int)b2][(int)b3].level > this.skills[(int)b2][(int)b3].max)
									{
										this.skills[(int)b2][(int)b3].level = this.skills[(int)b2][(int)b3].max;
									}
									b3 += 1;
								}
							}
							b2 += 1;
						}
					}
				}
			}
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x0012962C File Offset: 0x00127A2C
		public void save()
		{
			Block block = new Block();
			block.writeByte(PlayerSkills.SAVEDATA_VERSION);
			block.writeUInt32(this.experience);
			block.writeInt32(this.reputation);
			block.writeByte((byte)this.boost);
			byte b = 0;
			while ((int)b < this.skills.Length)
			{
				if (this.skills[(int)b] != null)
				{
					byte b2 = 0;
					while ((int)b2 < this.skills[(int)b].Length)
					{
						block.writeByte(this.skills[(int)b][(int)b2].level);
						b2 += 1;
					}
				}
				b += 1;
			}
			PlayerSavedata.writeBlock(base.channel.owner.playerID, "/Player/Skills.dat", block);
		}

		// Token: 0x04001D9E RID: 7582
		public static readonly SpecialitySkillPair[][] SKILLSETS = new SpecialitySkillPair[][]
		{
			new SpecialitySkillPair[0],
			new SpecialitySkillPair[]
			{
				new SpecialitySkillPair(0, 3),
				new SpecialitySkillPair(1, 4)
			},
			new SpecialitySkillPair[]
			{
				new SpecialitySkillPair(0, 4),
				new SpecialitySkillPair(1, 3)
			},
			new SpecialitySkillPair[]
			{
				new SpecialitySkillPair(0, 1),
				new SpecialitySkillPair(0, 2)
			},
			new SpecialitySkillPair[]
			{
				new SpecialitySkillPair(2, 5),
				new SpecialitySkillPair(1, 6)
			},
			new SpecialitySkillPair[]
			{
				new SpecialitySkillPair(2, 4),
				new SpecialitySkillPair(0, 5)
			},
			new SpecialitySkillPair[]
			{
				new SpecialitySkillPair(1, 5),
				new SpecialitySkillPair(2, 2)
			},
			new SpecialitySkillPair[]
			{
				new SpecialitySkillPair(2, 1),
				new SpecialitySkillPair(2, 7),
				new SpecialitySkillPair(2, 6)
			},
			new SpecialitySkillPair[]
			{
				new SpecialitySkillPair(2, 3),
				new SpecialitySkillPair(1, 1)
			},
			new SpecialitySkillPair[]
			{
				new SpecialitySkillPair(0, 6),
				new SpecialitySkillPair(1, 0)
			},
			new SpecialitySkillPair[]
			{
				new SpecialitySkillPair(1, 2),
				new SpecialitySkillPair(2, 0)
			}
		};

		// Token: 0x04001D9F RID: 7583
		public static readonly byte SAVEDATA_VERSION = 7;

		// Token: 0x04001DA0 RID: 7584
		public static readonly byte SPECIALITIES = 3;

		// Token: 0x04001DA1 RID: 7585
		public static readonly byte BOOST_COUNT = 4;

		// Token: 0x04001DA2 RID: 7586
		public static readonly uint BOOST_COST = 25u;

		// Token: 0x04001DA3 RID: 7587
		public ExperienceUpdated onExperienceUpdated;

		// Token: 0x04001DA4 RID: 7588
		public ReputationUpdated onReputationUpdated;

		// Token: 0x04001DA5 RID: 7589
		public BoostUpdated onBoostUpdated;

		// Token: 0x04001DA6 RID: 7590
		public SkillsUpdated onSkillsUpdated;

		// Token: 0x04001DA7 RID: 7591
		private Skill[][] _skills;

		// Token: 0x04001DA8 RID: 7592
		private EPlayerBoost _boost;

		// Token: 0x04001DA9 RID: 7593
		private uint _experience;

		// Token: 0x04001DAA RID: 7594
		private int _reputation;

		// Token: 0x04001DAB RID: 7595
		private bool wasLoaded;
	}
}
