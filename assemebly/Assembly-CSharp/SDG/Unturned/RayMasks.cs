using System;

namespace SDG.Unturned
{
	// Token: 0x020007CB RID: 1995
	public class RayMasks
	{
		// Token: 0x04002D6A RID: 11626
		public static readonly int DEFAULT = 1 << LayerMasks.DEFAULT;

		// Token: 0x04002D6B RID: 11627
		public static readonly int TRANSPARENT_FX = 1 << LayerMasks.TRANSPARENT_FX;

		// Token: 0x04002D6C RID: 11628
		public static readonly int IGNORE_RAYCAST = 1 << LayerMasks.IGNORE_RAYCAST;

		// Token: 0x04002D6D RID: 11629
		public static readonly int WATER = 1 << LayerMasks.WATER;

		// Token: 0x04002D6E RID: 11630
		public static readonly int UI = 1 << LayerMasks.UI;

		// Token: 0x04002D6F RID: 11631
		public static readonly int LOGIC = 1 << LayerMasks.LOGIC;

		// Token: 0x04002D70 RID: 11632
		public static readonly int PLAYER = 1 << LayerMasks.PLAYER;

		// Token: 0x04002D71 RID: 11633
		public static readonly int ENEMY = 1 << LayerMasks.ENEMY;

		// Token: 0x04002D72 RID: 11634
		public static readonly int VIEWMODEL = 1 << LayerMasks.VIEWMODEL;

		// Token: 0x04002D73 RID: 11635
		public static readonly int DEBRIS = 1 << LayerMasks.DEBRIS;

		// Token: 0x04002D74 RID: 11636
		public static readonly int ITEM = 1 << LayerMasks.ITEM;

		// Token: 0x04002D75 RID: 11637
		public static readonly int RESOURCE = 1 << LayerMasks.RESOURCE;

		// Token: 0x04002D76 RID: 11638
		public static readonly int LARGE = 1 << LayerMasks.LARGE;

		// Token: 0x04002D77 RID: 11639
		public static readonly int MEDIUM = 1 << LayerMasks.MEDIUM;

		// Token: 0x04002D78 RID: 11640
		public static readonly int SMALL = 1 << LayerMasks.SMALL;

		// Token: 0x04002D79 RID: 11641
		public static readonly int SKY = 1 << LayerMasks.SKY;

		// Token: 0x04002D7A RID: 11642
		public static readonly int ENVIRONMENT = 1 << LayerMasks.ENVIRONMENT;

		// Token: 0x04002D7B RID: 11643
		public static readonly int GROUND = 1 << LayerMasks.GROUND;

		// Token: 0x04002D7C RID: 11644
		public static readonly int CLIP = 1 << LayerMasks.CLIP;

		// Token: 0x04002D7D RID: 11645
		public static readonly int NAVMESH = 1 << LayerMasks.NAVMESH;

		// Token: 0x04002D7E RID: 11646
		public static readonly int ENTITY = 1 << LayerMasks.ENTITY;

		// Token: 0x04002D7F RID: 11647
		public static readonly int AGENT = 1 << LayerMasks.AGENT;

		// Token: 0x04002D80 RID: 11648
		public static readonly int LADDER = 1 << LayerMasks.LADDER;

		// Token: 0x04002D81 RID: 11649
		public static readonly int VEHICLE = 1 << LayerMasks.VEHICLE;

		// Token: 0x04002D82 RID: 11650
		public static readonly int BARRICADE = 1 << LayerMasks.BARRICADE;

		// Token: 0x04002D83 RID: 11651
		public static readonly int STRUCTURE = 1 << LayerMasks.STRUCTURE;

		// Token: 0x04002D84 RID: 11652
		public static readonly int TIRE = 1 << LayerMasks.TIRE;

		// Token: 0x04002D85 RID: 11653
		public static readonly int TRAP = 1 << LayerMasks.TRAP;

		// Token: 0x04002D86 RID: 11654
		public static readonly int GROUND2 = 1 << LayerMasks.GROUND2;

		// Token: 0x04002D87 RID: 11655
		public static readonly int REFLECTION = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND;

		// Token: 0x04002D88 RID: 11656
		public static readonly int CHART = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND;

		// Token: 0x04002D89 RID: 11657
		public static readonly int FOLIAGE_FOCUS = RayMasks.GROUND | RayMasks.GROUND2 | RayMasks.LARGE | RayMasks.MEDIUM;

		// Token: 0x04002D8A RID: 11658
		public static readonly int POWER_INTERACT = RayMasks.BARRICADE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.SMALL | RayMasks.RESOURCE;

		// Token: 0x04002D8B RID: 11659
		public static readonly int BARRICADE_INTERACT = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.BARRICADE | RayMasks.STRUCTURE | RayMasks.VEHICLE;

		// Token: 0x04002D8C RID: 11660
		public static readonly int STRUCTURE_INTERACT = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.STRUCTURE;

		// Token: 0x04002D8D RID: 11661
		public static readonly int ROOFS_INTERACT = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.STRUCTURE | RayMasks.GROUND2;

		// Token: 0x04002D8E RID: 11662
		public static readonly int CORNERS_INTERACT = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.STRUCTURE | RayMasks.SKY;

		// Token: 0x04002D8F RID: 11663
		public static readonly int WALLS_INTERACT = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.STRUCTURE | RayMasks.UI;

		// Token: 0x04002D90 RID: 11664
		public static readonly int LADDERS_INTERACT = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.STRUCTURE | RayMasks.VEHICLE | RayMasks.TRANSPARENT_FX;

		// Token: 0x04002D91 RID: 11665
		public static readonly int SLOTS_INTERACT = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.LADDER | RayMasks.VEHICLE | RayMasks.BARRICADE | RayMasks.STRUCTURE | RayMasks.LOGIC;

		// Token: 0x04002D92 RID: 11666
		public static readonly int LADDER_INTERACT = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.LADDER | RayMasks.VEHICLE | RayMasks.BARRICADE | RayMasks.STRUCTURE;

		// Token: 0x04002D93 RID: 11667
		public static readonly int CLOTHING_INTERACT = RayMasks.PLAYER | RayMasks.ENEMY | RayMasks.ITEM;

		// Token: 0x04002D94 RID: 11668
		public static readonly int PLAYER_INTERACT = RayMasks.ENEMY | RayMasks.ITEM | RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.SMALL | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.VEHICLE | RayMasks.BARRICADE | RayMasks.STRUCTURE | RayMasks.DEFAULT | RayMasks.WATER;

		// Token: 0x04002D95 RID: 11669
		public static readonly int EDITOR_INTERACT = RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.SMALL | RayMasks.BARRICADE | RayMasks.STRUCTURE;

		// Token: 0x04002D96 RID: 11670
		public static readonly int EDITOR_WORLD = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.SMALL | RayMasks.GROUND | RayMasks.GROUND2 | RayMasks.BARRICADE | RayMasks.STRUCTURE;

		// Token: 0x04002D97 RID: 11671
		public static readonly int EDITOR_LOGIC = RayMasks.LOGIC | RayMasks.SKY;

		// Token: 0x04002D98 RID: 11672
		public static readonly int EDITOR_VR = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.GROUND | RayMasks.GROUND2;

		// Token: 0x04002D99 RID: 11673
		public static readonly int EDITOR_BUILDABLE = RayMasks.RESOURCE | RayMasks.BARRICADE | RayMasks.STRUCTURE;

		// Token: 0x04002D9A RID: 11674
		public static readonly int BLOCK_LADDER = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.VEHICLE | RayMasks.BARRICADE | RayMasks.STRUCTURE | RayMasks.CLIP;

		// Token: 0x04002D9B RID: 11675
		public static readonly int BLOCK_PICKUP = RayMasks.MEDIUM | RayMasks.LARGE | RayMasks.BARRICADE | RayMasks.STRUCTURE;

		// Token: 0x04002D9C RID: 11676
		public static readonly int BLOCK_LASER = RayMasks.ENEMY | RayMasks.ITEM | RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.ENTITY | RayMasks.VEHICLE | RayMasks.BARRICADE | RayMasks.STRUCTURE;

		// Token: 0x04002D9D RID: 11677
		public static readonly int BLOCK_RESOURCE = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT;

		// Token: 0x04002D9E RID: 11678
		public static readonly int BLOCK_ITEM = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.BARRICADE | RayMasks.STRUCTURE;

		// Token: 0x04002D9F RID: 11679
		public static readonly int BLOCK_VEHICLE = RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND;

		// Token: 0x04002DA0 RID: 11680
		public static readonly int BLOCK_STANCE = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.VEHICLE | RayMasks.BARRICADE | RayMasks.STRUCTURE;

		// Token: 0x04002DA1 RID: 11681
		public static readonly int BLOCK_NAVMESH = RayMasks.NAVMESH | RayMasks.RESOURCE | RayMasks.ENVIRONMENT;

		// Token: 0x04002DA2 RID: 11682
		public static readonly int BLOCK_KILLCAM = RayMasks.LARGE | RayMasks.RESOURCE | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.BARRICADE | RayMasks.STRUCTURE | RayMasks.VEHICLE;

		// Token: 0x04002DA3 RID: 11683
		public static readonly int BLOCK_PLAYERCAM = RayMasks.LARGE | RayMasks.RESOURCE | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.BARRICADE | RayMasks.STRUCTURE | RayMasks.VEHICLE;

		// Token: 0x04002DA4 RID: 11684
		public static readonly int BLOCK_VEHICLECAM = RayMasks.LARGE | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.STRUCTURE;

		// Token: 0x04002DA5 RID: 11685
		public static readonly int BLOCK_VISION = RayMasks.LARGE | RayMasks.MEDIUM;

		// Token: 0x04002DA6 RID: 11686
		public static readonly int BLOCK_COLLISION = RayMasks.CLIP | RayMasks.GROUND | RayMasks.ENVIRONMENT | RayMasks.MEDIUM | RayMasks.LARGE | RayMasks.RESOURCE | RayMasks.VEHICLE | RayMasks.BARRICADE | RayMasks.STRUCTURE;

		// Token: 0x04002DA7 RID: 11687
		public static readonly int BLOCK_GRASS = RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT;

		// Token: 0x04002DA8 RID: 11688
		public static readonly int BLOCK_LEAN = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.VEHICLE | RayMasks.BARRICADE | RayMasks.STRUCTURE;

		// Token: 0x04002DA9 RID: 11689
		public static readonly int BLOCK_EXIT = RayMasks.CLIP | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.RESOURCE | RayMasks.GROUND | RayMasks.BARRICADE | RayMasks.STRUCTURE;

		// Token: 0x04002DAA RID: 11690
		public static readonly int BLOCK_TIRE = RayMasks.CLIP | RayMasks.GROUND | RayMasks.ENVIRONMENT | RayMasks.MEDIUM | RayMasks.LARGE | RayMasks.RESOURCE;

		// Token: 0x04002DAB RID: 11691
		public static readonly int BLOCK_BARRICADE = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.BARRICADE | RayMasks.STRUCTURE | RayMasks.PLAYER | RayMasks.ENEMY;

		// Token: 0x04002DAC RID: 11692
		public static readonly int BLOCK_DOOR_OPENING = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.BARRICADE | RayMasks.STRUCTURE | RayMasks.PLAYER | RayMasks.ENEMY;

		// Token: 0x04002DAD RID: 11693
		public static readonly int BLOCK_STRUCTURE = RayMasks.VEHICLE | RayMasks.BARRICADE | RayMasks.STRUCTURE | RayMasks.PLAYER | RayMasks.ENEMY;

		// Token: 0x04002DAE RID: 11694
		public static readonly int BLOCK_EXPLOSION = RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.GROUND | RayMasks.BARRICADE | RayMasks.STRUCTURE | RayMasks.RESOURCE;

		// Token: 0x04002DAF RID: 11695
		public static readonly int BLOCK_EXPLOSION_PENETRATE_BUILDABLES = RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.GROUND | RayMasks.RESOURCE;

		// Token: 0x04002DB0 RID: 11696
		public static readonly int BLOCK_WIND = RayMasks.LARGE | RayMasks.BARRICADE | RayMasks.STRUCTURE;

		// Token: 0x04002DB1 RID: 11697
		public static readonly int BLOCK_FRAME = RayMasks.BARRICADE | RayMasks.IGNORE_RAYCAST;

		// Token: 0x04002DB2 RID: 11698
		public static readonly int BLOCK_WINDOW = RayMasks.BARRICADE | RayMasks.IGNORE_RAYCAST;

		// Token: 0x04002DB3 RID: 11699
		public static readonly int BLOCK_SENTRY = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.VEHICLE | RayMasks.BARRICADE | RayMasks.STRUCTURE;

		// Token: 0x04002DB4 RID: 11700
		public static readonly int BLOCK_CHAR_BUILDABLE_OVERLAP = RayMasks.PLAYER;

		// Token: 0x04002DB5 RID: 11701
		public static readonly int BLOCK_CHAR_HINGE_OVERLAP = RayMasks.PLAYER | RayMasks.VEHICLE;

		// Token: 0x04002DB6 RID: 11702
		public static readonly int BLOCK_CHAR_HINGE_OVERLAP_ON_VEHICLE = RayMasks.PLAYER;

		// Token: 0x04002DB7 RID: 11703
		public static readonly int BLOCK_TRAIN = RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.BARRICADE | RayMasks.STRUCTURE | RayMasks.VEHICLE;

		// Token: 0x04002DB8 RID: 11704
		public static readonly int WAYPOINT = RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.RESOURCE | RayMasks.BARRICADE | RayMasks.STRUCTURE | RayMasks.ENVIRONMENT | RayMasks.GROUND;

		// Token: 0x04002DB9 RID: 11705
		public static readonly int DAMAGE_PHYSICS = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.VEHICLE | RayMasks.BARRICADE | RayMasks.STRUCTURE;

		// Token: 0x04002DBA RID: 11706
		public static readonly int DAMAGE_CLIENT = RayMasks.ENEMY | RayMasks.ENTITY | RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.SMALL | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.VEHICLE | RayMasks.BARRICADE | RayMasks.STRUCTURE;

		// Token: 0x04002DBB RID: 11707
		public static readonly int DAMAGE_SERVER = RayMasks.RESOURCE | RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND | RayMasks.BARRICADE | RayMasks.STRUCTURE;

		// Token: 0x04002DBC RID: 11708
		public static readonly int DAMAGE_ZOMBIE = RayMasks.VEHICLE | RayMasks.BARRICADE | RayMasks.STRUCTURE;

		// Token: 0x04002DBD RID: 11709
		public static readonly int SPLATTER = RayMasks.LARGE | RayMasks.MEDIUM | RayMasks.ENVIRONMENT | RayMasks.GROUND;
	}
}
