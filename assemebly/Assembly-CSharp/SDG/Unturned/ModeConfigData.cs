using System;

namespace SDG.Unturned
{
	// Token: 0x020006A2 RID: 1698
	public class ModeConfigData
	{
		// Token: 0x060031C1 RID: 12737 RVA: 0x0014329C File Offset: 0x0014169C
		public ModeConfigData(EGameMode mode)
		{
			this.Items = new ItemsConfigData(mode);
			this.Vehicles = new VehiclesConfigData(mode);
			this.Zombies = new ZombiesConfigData(mode);
			this.Animals = new AnimalsConfigData(mode);
			this.Barricades = new BarricadesConfigData(mode);
			this.Structures = new StructuresConfigData(mode);
			this.Players = new PlayersConfigData(mode);
			this.Objects = new ObjectConfigData(mode);
			this.Events = new EventsConfigData(mode);
			this.Gameplay = new GameplayConfigData(mode);
		}

		// Token: 0x04002117 RID: 8471
		public ItemsConfigData Items;

		// Token: 0x04002118 RID: 8472
		public VehiclesConfigData Vehicles;

		// Token: 0x04002119 RID: 8473
		public ZombiesConfigData Zombies;

		// Token: 0x0400211A RID: 8474
		public AnimalsConfigData Animals;

		// Token: 0x0400211B RID: 8475
		public BarricadesConfigData Barricades;

		// Token: 0x0400211C RID: 8476
		public StructuresConfigData Structures;

		// Token: 0x0400211D RID: 8477
		public PlayersConfigData Players;

		// Token: 0x0400211E RID: 8478
		public ObjectConfigData Objects;

		// Token: 0x0400211F RID: 8479
		public EventsConfigData Events;

		// Token: 0x04002120 RID: 8480
		public GameplayConfigData Gameplay;
	}
}
