using System;
using SDG.Framework.Debug;

namespace SDG.Unturned
{
	// Token: 0x0200047C RID: 1148
	public class AnimalDamageMultiplier : IDamageMultiplier
	{
		// Token: 0x06001E69 RID: 7785 RVA: 0x000A66DB File Offset: 0x000A4ADB
		public AnimalDamageMultiplier(float newDamage, float newLeg, float newSpine, float newSkull)
		{
			this.damage = newDamage;
			this.leg = newLeg;
			this.spine = newSpine;
			this.skull = newSkull;
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x000A6700 File Offset: 0x000A4B00
		public float multiply(ELimb limb)
		{
			switch (limb)
			{
			case ELimb.LEFT_BACK:
				return this.damage * this.leg * Provider.modeConfigData.Animals.Armor_Multiplier;
			case ELimb.RIGHT_BACK:
				return this.damage * this.leg * Provider.modeConfigData.Animals.Armor_Multiplier;
			case ELimb.LEFT_FRONT:
				return this.damage * this.leg * Provider.modeConfigData.Animals.Armor_Multiplier;
			case ELimb.RIGHT_FRONT:
				return this.damage * this.leg * Provider.modeConfigData.Animals.Armor_Multiplier;
			case ELimb.SPINE:
				return this.damage * this.spine * Provider.modeConfigData.Animals.Armor_Multiplier;
			case ELimb.SKULL:
				return this.damage * this.skull * Provider.modeConfigData.Animals.Armor_Multiplier;
			default:
				return this.damage;
			}
		}

		// Token: 0x040011F5 RID: 4597
		public static readonly float MULTIPLIER_EASY = 1.25f;

		// Token: 0x040011F6 RID: 4598
		public static readonly float MULTIPLIER_HARD = 0.75f;

		// Token: 0x040011F7 RID: 4599
		[Inspectable("#SDG::Asset.Animal_Damage_Multiplier.Damage.Name", null)]
		public float damage;

		// Token: 0x040011F8 RID: 4600
		[Inspectable("#SDG::Asset.Animal_Damage_Multiplier.Leg.Name", null)]
		public float leg;

		// Token: 0x040011F9 RID: 4601
		[Inspectable("#SDG::Asset.Animal_Damage_Multiplier.Spine.Name", null)]
		public float spine;

		// Token: 0x040011FA RID: 4602
		[Inspectable("#SDG::Asset.Animal_Damage_Multiplier.Skull.Name", null)]
		public float skull;
	}
}
