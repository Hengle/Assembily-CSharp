using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000690 RID: 1680
	public class ControlsSettings
	{
		// Token: 0x060030CE RID: 12494 RVA: 0x0013F998 File Offset: 0x0013DD98
		static ControlsSettings()
		{
			for (int i = 0; i < ControlsSettings.bindings.Length; i++)
			{
				ControlsSettings.bindings[i] = new ControlBinding(KeyCode.F);
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x060030D0 RID: 12496 RVA: 0x0013FB7A File Offset: 0x0013DF7A
		public static float look
		{
			get
			{
				return ControlsSettings.sensitivity * 2f;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060030D1 RID: 12497 RVA: 0x0013FB87 File Offset: 0x0013DF87
		public static ControlBinding[] bindings
		{
			get
			{
				return ControlsSettings._bindings;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060030D2 RID: 12498 RVA: 0x0013FB8E File Offset: 0x0013DF8E
		public static KeyCode left
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.LEFT].key;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060030D3 RID: 12499 RVA: 0x0013FBA0 File Offset: 0x0013DFA0
		public static KeyCode up
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.UP].key;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060030D4 RID: 12500 RVA: 0x0013FBB2 File Offset: 0x0013DFB2
		public static KeyCode right
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.RIGHT].key;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x060030D5 RID: 12501 RVA: 0x0013FBC4 File Offset: 0x0013DFC4
		public static KeyCode down
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.DOWN].key;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060030D6 RID: 12502 RVA: 0x0013FBD6 File Offset: 0x0013DFD6
		public static KeyCode jump
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.JUMP].key;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060030D7 RID: 12503 RVA: 0x0013FBE8 File Offset: 0x0013DFE8
		public static KeyCode leanLeft
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.LEAN_LEFT].key;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060030D8 RID: 12504 RVA: 0x0013FBFA File Offset: 0x0013DFFA
		public static KeyCode leanRight
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.LEAN_RIGHT].key;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060030D9 RID: 12505 RVA: 0x0013FC0C File Offset: 0x0013E00C
		public static KeyCode rollLeft
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.ROLL_LEFT].key;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060030DA RID: 12506 RVA: 0x0013FC1E File Offset: 0x0013E01E
		public static KeyCode rollRight
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.ROLL_RIGHT].key;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060030DB RID: 12507 RVA: 0x0013FC30 File Offset: 0x0013E030
		public static KeyCode pitchUp
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.PITCH_UP].key;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060030DC RID: 12508 RVA: 0x0013FC42 File Offset: 0x0013E042
		public static KeyCode pitchDown
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.PITCH_DOWN].key;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060030DD RID: 12509 RVA: 0x0013FC54 File Offset: 0x0013E054
		public static KeyCode primary
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.PRIMARY].key;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060030DE RID: 12510 RVA: 0x0013FC66 File Offset: 0x0013E066
		public static KeyCode yawLeft
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.YAW_LEFT].key;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060030DF RID: 12511 RVA: 0x0013FC78 File Offset: 0x0013E078
		public static KeyCode yawRight
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.YAW_RIGHT].key;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060030E0 RID: 12512 RVA: 0x0013FC8A File Offset: 0x0013E08A
		public static KeyCode thrustIncrease
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.THRUST_INCREASE].key;
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060030E1 RID: 12513 RVA: 0x0013FC9C File Offset: 0x0013E09C
		public static KeyCode thrustDecrease
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.THRUST_DECREASE].key;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060030E2 RID: 12514 RVA: 0x0013FCAE File Offset: 0x0013E0AE
		public static KeyCode locker
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.LOCKER].key;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060030E3 RID: 12515 RVA: 0x0013FCC0 File Offset: 0x0013E0C0
		public static KeyCode secondary
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.SECONDARY].key;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060030E4 RID: 12516 RVA: 0x0013FCD2 File Offset: 0x0013E0D2
		public static KeyCode reload
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.RELOAD].key;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060030E5 RID: 12517 RVA: 0x0013FCE4 File Offset: 0x0013E0E4
		public static KeyCode attach
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.ATTACH].key;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060030E6 RID: 12518 RVA: 0x0013FCF6 File Offset: 0x0013E0F6
		public static KeyCode firemode
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.FIREMODE].key;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060030E7 RID: 12519 RVA: 0x0013FD08 File Offset: 0x0013E108
		public static KeyCode dashboard
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.DASHBOARD].key;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060030E8 RID: 12520 RVA: 0x0013FD1A File Offset: 0x0013E11A
		public static KeyCode inventory
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.INVENTORY].key;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060030E9 RID: 12521 RVA: 0x0013FD2C File Offset: 0x0013E12C
		public static KeyCode crafting
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.CRAFTING].key;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060030EA RID: 12522 RVA: 0x0013FD3E File Offset: 0x0013E13E
		public static KeyCode skills
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.SKILLS].key;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060030EB RID: 12523 RVA: 0x0013FD50 File Offset: 0x0013E150
		public static KeyCode map
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.MAP].key;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060030EC RID: 12524 RVA: 0x0013FD62 File Offset: 0x0013E162
		public static KeyCode quests
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.QUESTS].key;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x060030ED RID: 12525 RVA: 0x0013FD74 File Offset: 0x0013E174
		public static KeyCode players
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.PLAYERS].key;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x060030EE RID: 12526 RVA: 0x0013FD86 File Offset: 0x0013E186
		public static KeyCode voice
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.VOICE].key;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x060030EF RID: 12527 RVA: 0x0013FD98 File Offset: 0x0013E198
		public static KeyCode interact
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.INTERACT].key;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x060030F0 RID: 12528 RVA: 0x0013FDAA File Offset: 0x0013E1AA
		public static KeyCode crouch
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.CROUCH].key;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x060030F1 RID: 12529 RVA: 0x0013FDBC File Offset: 0x0013E1BC
		public static KeyCode prone
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.PRONE].key;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x0013FDCE File Offset: 0x0013E1CE
		public static KeyCode stance
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.STANCE].key;
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x060030F3 RID: 12531 RVA: 0x0013FDE0 File Offset: 0x0013E1E0
		public static KeyCode sprint
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.SPRINT].key;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x060030F4 RID: 12532 RVA: 0x0013FDF2 File Offset: 0x0013E1F2
		public static KeyCode modify
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.MODIFY].key;
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x060030F5 RID: 12533 RVA: 0x0013FE04 File Offset: 0x0013E204
		public static KeyCode snap
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.SNAP].key;
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x060030F6 RID: 12534 RVA: 0x0013FE16 File Offset: 0x0013E216
		public static KeyCode focus
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.FOCUS].key;
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x060030F7 RID: 12535 RVA: 0x0013FE28 File Offset: 0x0013E228
		public static KeyCode ascend
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.ASCEND].key;
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x060030F8 RID: 12536 RVA: 0x0013FE3A File Offset: 0x0013E23A
		public static KeyCode descend
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.DESCEND].key;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x060030F9 RID: 12537 RVA: 0x0013FE4C File Offset: 0x0013E24C
		public static KeyCode tool_0
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.TOOL_0].key;
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x060030FA RID: 12538 RVA: 0x0013FE5E File Offset: 0x0013E25E
		public static KeyCode tool_1
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.TOOL_1].key;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x060030FB RID: 12539 RVA: 0x0013FE70 File Offset: 0x0013E270
		public static KeyCode tool_2
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.TOOL_2].key;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x0013FE82 File Offset: 0x0013E282
		public static KeyCode tool_3
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.TOOL_3].key;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x060030FD RID: 12541 RVA: 0x0013FE94 File Offset: 0x0013E294
		public static KeyCode terminal
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.TERMINAL].key;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x0013FEA6 File Offset: 0x0013E2A6
		public static KeyCode screenshot
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.SCREENSHOT].key;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x060030FF RID: 12543 RVA: 0x0013FEB8 File Offset: 0x0013E2B8
		public static KeyCode refreshAssets
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.REFRESH_ASSETS].key;
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x0013FECA File Offset: 0x0013E2CA
		public static KeyCode clipboardDebug
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.CLIPBOARD_DEBUG].key;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06003101 RID: 12545 RVA: 0x0013FEDC File Offset: 0x0013E2DC
		public static KeyCode hud
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.HUD].key;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x0013FEEE File Offset: 0x0013E2EE
		public static KeyCode other
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.OTHER].key;
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06003103 RID: 12547 RVA: 0x0013FF00 File Offset: 0x0013E300
		public static KeyCode global
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.GLOBAL].key;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x0013FF12 File Offset: 0x0013E312
		public static KeyCode local
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.LOCAL].key;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06003105 RID: 12549 RVA: 0x0013FF24 File Offset: 0x0013E324
		public static KeyCode group
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.GROUP].key;
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x0013FF36 File Offset: 0x0013E336
		public static KeyCode gesture
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.GESTURE].key;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06003107 RID: 12551 RVA: 0x0013FF48 File Offset: 0x0013E348
		public static KeyCode vision
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.VISION].key;
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x0013FF5A File Offset: 0x0013E35A
		public static KeyCode tactical
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.TACTICAL].key;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06003109 RID: 12553 RVA: 0x0013FF6C File Offset: 0x0013E36C
		public static KeyCode perspective
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.PERSPECTIVE].key;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x0013FF7E File Offset: 0x0013E37E
		public static KeyCode dequip
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.DEQUIP].key;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x0013FF90 File Offset: 0x0013E390
		public static KeyCode inspect
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.INSPECT].key;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x0013FFA2 File Offset: 0x0013E3A2
		public static KeyCode rotate
		{
			get
			{
				return ControlsSettings.bindings[(int)ControlsSettings.ROTATE].key;
			}
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x0013FFB4 File Offset: 0x0013E3B4
		private static bool isTooImportantToMessUp(KeyCode key)
		{
			return key == KeyCode.Mouse0 || key == KeyCode.Mouse1;
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x0013FFD4 File Offset: 0x0013E3D4
		public static void bind(byte index, KeyCode key)
		{
			if (index == ControlsSettings.HUD)
			{
				if (ControlsSettings.isTooImportantToMessUp(key))
				{
					key = KeyCode.Home;
				}
			}
			else if (index == ControlsSettings.OTHER)
			{
				if (ControlsSettings.isTooImportantToMessUp(key))
				{
					key = KeyCode.LeftControl;
				}
			}
			else if (index == ControlsSettings.TERMINAL)
			{
				if (ControlsSettings.isTooImportantToMessUp(key))
				{
					key = KeyCode.BackQuote;
				}
			}
			else if (index == ControlsSettings.REFRESH_ASSETS && ControlsSettings.isTooImportantToMessUp(key))
			{
				key = KeyCode.PageUp;
			}
			if (ControlsSettings.bindings[(int)index] == null)
			{
				ControlsSettings.bindings[(int)index] = new ControlBinding(key);
				return;
			}
			ControlsSettings.bindings[(int)index].key = key;
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x00140088 File Offset: 0x0013E488
		public static void restoreDefaults()
		{
			ControlsSettings.bind(ControlsSettings.LEFT, KeyCode.A);
			ControlsSettings.bind(ControlsSettings.RIGHT, KeyCode.D);
			ControlsSettings.bind(ControlsSettings.UP, KeyCode.W);
			ControlsSettings.bind(ControlsSettings.DOWN, KeyCode.S);
			ControlsSettings.bind(ControlsSettings.JUMP, KeyCode.Space);
			ControlsSettings.bind(ControlsSettings.LEAN_LEFT, KeyCode.Q);
			ControlsSettings.bind(ControlsSettings.LEAN_RIGHT, KeyCode.E);
			ControlsSettings.bind(ControlsSettings.PRIMARY, KeyCode.Mouse0);
			ControlsSettings.bind(ControlsSettings.SECONDARY, KeyCode.Mouse1);
			ControlsSettings.bind(ControlsSettings.INTERACT, KeyCode.F);
			ControlsSettings.bind(ControlsSettings.CROUCH, KeyCode.X);
			ControlsSettings.bind(ControlsSettings.PRONE, KeyCode.Z);
			ControlsSettings.bind(ControlsSettings.STANCE, KeyCode.O);
			ControlsSettings.bind(ControlsSettings.SPRINT, KeyCode.LeftShift);
			ControlsSettings.bind(ControlsSettings.RELOAD, KeyCode.R);
			ControlsSettings.bind(ControlsSettings.ATTACH, KeyCode.T);
			ControlsSettings.bind(ControlsSettings.FIREMODE, KeyCode.V);
			ControlsSettings.bind(ControlsSettings.DASHBOARD, KeyCode.Tab);
			ControlsSettings.bind(ControlsSettings.INVENTORY, KeyCode.G);
			ControlsSettings.bind(ControlsSettings.CRAFTING, KeyCode.Y);
			ControlsSettings.bind(ControlsSettings.SKILLS, KeyCode.U);
			ControlsSettings.bind(ControlsSettings.MAP, KeyCode.M);
			ControlsSettings.bind(ControlsSettings.QUESTS, KeyCode.I);
			ControlsSettings.bind(ControlsSettings.PLAYERS, KeyCode.P);
			ControlsSettings.bind(ControlsSettings.VOICE, KeyCode.LeftAlt);
			ControlsSettings.bind(ControlsSettings.MODIFY, KeyCode.LeftShift);
			ControlsSettings.bind(ControlsSettings.SNAP, KeyCode.LeftControl);
			ControlsSettings.bind(ControlsSettings.FOCUS, KeyCode.F);
			ControlsSettings.bind(ControlsSettings.ASCEND, KeyCode.Q);
			ControlsSettings.bind(ControlsSettings.DESCEND, KeyCode.E);
			ControlsSettings.bind(ControlsSettings.TOOL_0, KeyCode.Q);
			ControlsSettings.bind(ControlsSettings.TOOL_1, KeyCode.W);
			ControlsSettings.bind(ControlsSettings.TOOL_2, KeyCode.E);
			ControlsSettings.bind(ControlsSettings.TOOL_3, KeyCode.R);
			ControlsSettings.bind(ControlsSettings.TERMINAL, KeyCode.BackQuote);
			ControlsSettings.bind(ControlsSettings.SCREENSHOT, KeyCode.Insert);
			ControlsSettings.bind(ControlsSettings.REFRESH_ASSETS, KeyCode.PageUp);
			ControlsSettings.bind(ControlsSettings.CLIPBOARD_DEBUG, KeyCode.PageDown);
			ControlsSettings.bind(ControlsSettings.HUD, KeyCode.Home);
			ControlsSettings.bind(ControlsSettings.OTHER, KeyCode.LeftControl);
			ControlsSettings.bind(ControlsSettings.GLOBAL, KeyCode.J);
			ControlsSettings.bind(ControlsSettings.LOCAL, KeyCode.K);
			ControlsSettings.bind(ControlsSettings.GROUP, KeyCode.L);
			ControlsSettings.bind(ControlsSettings.GESTURE, KeyCode.C);
			ControlsSettings.bind(ControlsSettings.VISION, KeyCode.N);
			ControlsSettings.bind(ControlsSettings.TACTICAL, KeyCode.B);
			ControlsSettings.bind(ControlsSettings.PERSPECTIVE, KeyCode.H);
			ControlsSettings.bind(ControlsSettings.DEQUIP, KeyCode.CapsLock);
			ControlsSettings.bind(ControlsSettings.ROLL_LEFT, KeyCode.LeftArrow);
			ControlsSettings.bind(ControlsSettings.ROLL_RIGHT, KeyCode.RightArrow);
			ControlsSettings.bind(ControlsSettings.PITCH_UP, KeyCode.UpArrow);
			ControlsSettings.bind(ControlsSettings.PITCH_DOWN, KeyCode.DownArrow);
			ControlsSettings.bind(ControlsSettings.YAW_LEFT, KeyCode.A);
			ControlsSettings.bind(ControlsSettings.YAW_RIGHT, KeyCode.D);
			ControlsSettings.bind(ControlsSettings.THRUST_INCREASE, KeyCode.W);
			ControlsSettings.bind(ControlsSettings.THRUST_DECREASE, KeyCode.S);
			ControlsSettings.bind(ControlsSettings.LOCKER, KeyCode.O);
			ControlsSettings.bind(ControlsSettings.INSPECT, KeyCode.F);
			ControlsSettings.bind(ControlsSettings.ROTATE, KeyCode.R);
			ControlsSettings.aiming = EControlMode.HOLD;
			ControlsSettings.crouching = EControlMode.TOGGLE;
			ControlsSettings.proning = EControlMode.TOGGLE;
			ControlsSettings.sprinting = EControlMode.HOLD;
			ControlsSettings.leaning = EControlMode.HOLD;
			ControlsSettings.sensitivity = 1f;
			ControlsSettings.invert = false;
			ControlsSettings.invertFlight = false;
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x001403C0 File Offset: 0x0013E7C0
		public static void load()
		{
			ControlsSettings.restoreDefaults();
			if (ReadWrite.fileExists("/Controls.dat", true))
			{
				Block block = ReadWrite.readBlock("/Controls.dat", true, 0);
				if (block != null)
				{
					byte b = block.readByte();
					if (b > 10)
					{
						ControlsSettings.sensitivity = block.readSingle();
						if (b < 16)
						{
							ControlsSettings.sensitivity = 1f;
						}
						ControlsSettings.invert = block.readBoolean();
						if (b > 13)
						{
							ControlsSettings.invertFlight = block.readBoolean();
						}
						else
						{
							ControlsSettings.invertFlight = false;
						}
						if (b > 11)
						{
							ControlsSettings.aiming = (EControlMode)block.readByte();
							ControlsSettings.crouching = (EControlMode)block.readByte();
							ControlsSettings.proning = (EControlMode)block.readByte();
						}
						else
						{
							ControlsSettings.aiming = EControlMode.HOLD;
							ControlsSettings.crouching = EControlMode.TOGGLE;
							ControlsSettings.proning = EControlMode.TOGGLE;
						}
						if (b > 12)
						{
							ControlsSettings.sprinting = (EControlMode)block.readByte();
						}
						else
						{
							ControlsSettings.sprinting = EControlMode.HOLD;
						}
						if (b > 14)
						{
							ControlsSettings.leaning = (EControlMode)block.readByte();
						}
						else
						{
							ControlsSettings.leaning = EControlMode.HOLD;
						}
						byte b2 = block.readByte();
						for (byte b3 = 0; b3 < b2; b3 += 1)
						{
							if ((int)b3 >= ControlsSettings.bindings.Length)
							{
								block.readByte();
							}
							else
							{
								ushort key = block.readUInt16();
								ControlsSettings.bind(b3, (KeyCode)key);
							}
						}
						if (b < 17)
						{
							ControlsSettings.bind(ControlsSettings.DEQUIP, KeyCode.CapsLock);
						}
					}
				}
			}
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x00140520 File Offset: 0x0013E920
		public static void save()
		{
			Block block = new Block();
			block.writeByte(ControlsSettings.SAVEDATA_VERSION);
			block.writeSingle(ControlsSettings.sensitivity);
			block.writeBoolean(ControlsSettings.invert);
			block.writeBoolean(ControlsSettings.invertFlight);
			block.writeByte((byte)ControlsSettings.aiming);
			block.writeByte((byte)ControlsSettings.crouching);
			block.writeByte((byte)ControlsSettings.proning);
			block.writeByte((byte)ControlsSettings.sprinting);
			block.writeByte((byte)ControlsSettings.leaning);
			block.writeByte((byte)ControlsSettings.bindings.Length);
			byte b = 0;
			while ((int)b < ControlsSettings.bindings.Length)
			{
				ControlBinding controlBinding = ControlsSettings.bindings[(int)b];
				block.writeUInt16((ushort)controlBinding.key);
				b += 1;
			}
			ReadWrite.writeBlock("/Controls.dat", true, block);
		}

		// Token: 0x04002038 RID: 8248
		public static readonly byte SAVEDATA_VERSION = 17;

		// Token: 0x04002039 RID: 8249
		public static readonly byte LEFT = 0;

		// Token: 0x0400203A RID: 8250
		public static readonly byte RIGHT = 1;

		// Token: 0x0400203B RID: 8251
		public static readonly byte UP = 2;

		// Token: 0x0400203C RID: 8252
		public static readonly byte DOWN = 3;

		// Token: 0x0400203D RID: 8253
		public static readonly byte JUMP = 4;

		// Token: 0x0400203E RID: 8254
		public static readonly byte LEAN_LEFT = 5;

		// Token: 0x0400203F RID: 8255
		public static readonly byte LEAN_RIGHT = 6;

		// Token: 0x04002040 RID: 8256
		public static readonly byte PRIMARY = 7;

		// Token: 0x04002041 RID: 8257
		public static readonly byte SECONDARY = 8;

		// Token: 0x04002042 RID: 8258
		public static readonly byte INTERACT = 9;

		// Token: 0x04002043 RID: 8259
		public static readonly byte CROUCH = 10;

		// Token: 0x04002044 RID: 8260
		public static readonly byte PRONE = 11;

		// Token: 0x04002045 RID: 8261
		public static readonly byte SPRINT = 12;

		// Token: 0x04002046 RID: 8262
		public static readonly byte RELOAD = 13;

		// Token: 0x04002047 RID: 8263
		public static readonly byte ATTACH = 14;

		// Token: 0x04002048 RID: 8264
		public static readonly byte FIREMODE = 15;

		// Token: 0x04002049 RID: 8265
		public static readonly byte DASHBOARD = 16;

		// Token: 0x0400204A RID: 8266
		public static readonly byte INVENTORY = 17;

		// Token: 0x0400204B RID: 8267
		public static readonly byte CRAFTING = 18;

		// Token: 0x0400204C RID: 8268
		public static readonly byte SKILLS = 19;

		// Token: 0x0400204D RID: 8269
		public static readonly byte MAP = 20;

		// Token: 0x0400204E RID: 8270
		public static readonly byte QUESTS = 54;

		// Token: 0x0400204F RID: 8271
		public static readonly byte PLAYERS = 21;

		// Token: 0x04002050 RID: 8272
		public static readonly byte VOICE = 22;

		// Token: 0x04002051 RID: 8273
		public static readonly byte MODIFY = 23;

		// Token: 0x04002052 RID: 8274
		public static readonly byte SNAP = 24;

		// Token: 0x04002053 RID: 8275
		public static readonly byte FOCUS = 25;

		// Token: 0x04002054 RID: 8276
		public static readonly byte ASCEND = 51;

		// Token: 0x04002055 RID: 8277
		public static readonly byte DESCEND = 52;

		// Token: 0x04002056 RID: 8278
		public static readonly byte TOOL_0 = 26;

		// Token: 0x04002057 RID: 8279
		public static readonly byte TOOL_1 = 27;

		// Token: 0x04002058 RID: 8280
		public static readonly byte TOOL_2 = 28;

		// Token: 0x04002059 RID: 8281
		public static readonly byte TOOL_3 = 50;

		// Token: 0x0400205A RID: 8282
		public static readonly byte TERMINAL = 55;

		// Token: 0x0400205B RID: 8283
		public static readonly byte SCREENSHOT = 56;

		// Token: 0x0400205C RID: 8284
		public static readonly byte REFRESH_ASSETS = 57;

		// Token: 0x0400205D RID: 8285
		public static readonly byte CLIPBOARD_DEBUG = 58;

		// Token: 0x0400205E RID: 8286
		public static readonly byte HUD = 29;

		// Token: 0x0400205F RID: 8287
		public static readonly byte OTHER = 30;

		// Token: 0x04002060 RID: 8288
		public static readonly byte GLOBAL = 31;

		// Token: 0x04002061 RID: 8289
		public static readonly byte LOCAL = 32;

		// Token: 0x04002062 RID: 8290
		public static readonly byte GROUP = 33;

		// Token: 0x04002063 RID: 8291
		public static readonly byte GESTURE = 34;

		// Token: 0x04002064 RID: 8292
		public static readonly byte VISION = 35;

		// Token: 0x04002065 RID: 8293
		public static readonly byte TACTICAL = 36;

		// Token: 0x04002066 RID: 8294
		public static readonly byte PERSPECTIVE = 37;

		// Token: 0x04002067 RID: 8295
		public static readonly byte DEQUIP = 38;

		// Token: 0x04002068 RID: 8296
		public static readonly byte STANCE = 39;

		// Token: 0x04002069 RID: 8297
		public static readonly byte ROLL_LEFT = 40;

		// Token: 0x0400206A RID: 8298
		public static readonly byte ROLL_RIGHT = 41;

		// Token: 0x0400206B RID: 8299
		public static readonly byte PITCH_UP = 42;

		// Token: 0x0400206C RID: 8300
		public static readonly byte PITCH_DOWN = 43;

		// Token: 0x0400206D RID: 8301
		public static readonly byte YAW_LEFT = 44;

		// Token: 0x0400206E RID: 8302
		public static readonly byte YAW_RIGHT = 45;

		// Token: 0x0400206F RID: 8303
		public static readonly byte THRUST_INCREASE = 46;

		// Token: 0x04002070 RID: 8304
		public static readonly byte THRUST_DECREASE = 47;

		// Token: 0x04002071 RID: 8305
		public static readonly byte LOCKER = 53;

		// Token: 0x04002072 RID: 8306
		public static readonly byte INSPECT = 48;

		// Token: 0x04002073 RID: 8307
		public static readonly byte ROTATE = 49;

		// Token: 0x04002074 RID: 8308
		public static float sensitivity;

		// Token: 0x04002075 RID: 8309
		public static bool invert;

		// Token: 0x04002076 RID: 8310
		public static bool invertFlight;

		// Token: 0x04002077 RID: 8311
		public static EControlMode aiming;

		// Token: 0x04002078 RID: 8312
		public static EControlMode crouching;

		// Token: 0x04002079 RID: 8313
		public static EControlMode proning;

		// Token: 0x0400207A RID: 8314
		public static EControlMode sprinting;

		// Token: 0x0400207B RID: 8315
		public static EControlMode leaning;

		// Token: 0x0400207C RID: 8316
		private static ControlBinding[] _bindings = new ControlBinding[59];
	}
}
