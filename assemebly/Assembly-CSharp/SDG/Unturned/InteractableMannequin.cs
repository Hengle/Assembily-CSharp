using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004D4 RID: 1236
	public class InteractableMannequin : Interactable, IManualOnDestroy
	{
		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x0600211D RID: 8477 RVA: 0x000B4A65 File Offset: 0x000B2E65
		public CSteamID owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x000B4A6D File Offset: 0x000B2E6D
		public CSteamID group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x000B4A75 File Offset: 0x000B2E75
		public bool isUpdatable
		{
			get
			{
				return Time.realtimeSinceStartup - this.updated > 0.5f;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x000B4A8A File Offset: 0x000B2E8A
		// (set) Token: 0x06002121 RID: 8481 RVA: 0x000B4A92 File Offset: 0x000B2E92
		public HumanClothes clothes { get; private set; }

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06002122 RID: 8482 RVA: 0x000B4A9B File Offset: 0x000B2E9B
		public int visualShirt
		{
			get
			{
				return this.clothes.visualShirt;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06002123 RID: 8483 RVA: 0x000B4AA8 File Offset: 0x000B2EA8
		public int visualPants
		{
			get
			{
				return this.clothes.visualPants;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06002124 RID: 8484 RVA: 0x000B4AB5 File Offset: 0x000B2EB5
		public int visualHat
		{
			get
			{
				return this.clothes.visualHat;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x000B4AC2 File Offset: 0x000B2EC2
		public int visualBackpack
		{
			get
			{
				return this.clothes.visualBackpack;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06002126 RID: 8486 RVA: 0x000B4ACF File Offset: 0x000B2ECF
		public int visualVest
		{
			get
			{
				return this.clothes.visualVest;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06002127 RID: 8487 RVA: 0x000B4ADC File Offset: 0x000B2EDC
		public int visualMask
		{
			get
			{
				return this.clothes.visualMask;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06002128 RID: 8488 RVA: 0x000B4AE9 File Offset: 0x000B2EE9
		public int visualGlasses
		{
			get
			{
				return this.clothes.visualGlasses;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x000B4AF6 File Offset: 0x000B2EF6
		public ushort shirt
		{
			get
			{
				return this.clothes.shirt;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600212A RID: 8490 RVA: 0x000B4B03 File Offset: 0x000B2F03
		public ushort pants
		{
			get
			{
				return this.clothes.pants;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x0600212B RID: 8491 RVA: 0x000B4B10 File Offset: 0x000B2F10
		public ushort hat
		{
			get
			{
				return this.clothes.hat;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x0600212C RID: 8492 RVA: 0x000B4B1D File Offset: 0x000B2F1D
		public ushort backpack
		{
			get
			{
				return this.clothes.backpack;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x0600212D RID: 8493 RVA: 0x000B4B2A File Offset: 0x000B2F2A
		public ushort vest
		{
			get
			{
				return this.clothes.vest;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x000B4B37 File Offset: 0x000B2F37
		public ushort mask
		{
			get
			{
				return this.clothes.mask;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x000B4B44 File Offset: 0x000B2F44
		public ushort glasses
		{
			get
			{
				return this.clothes.glasses;
			}
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000B4B54 File Offset: 0x000B2F54
		public bool checkUpdate(CSteamID enemyPlayer, CSteamID enemyGroup)
		{
			return (Provider.isServer && !Dedicator.isDedicated) || !this.isLocked || enemyPlayer == this.owner || (this.group != CSteamID.Nil && enemyGroup == this.group);
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000B4BBC File Offset: 0x000B2FBC
		public byte getComp(bool mirror, byte pose)
		{
			byte b = (!mirror) ? 0 : 1;
			return (byte)((int)b << 7 | (int)pose);
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x000B4BE0 File Offset: 0x000B2FE0
		public void applyPose(byte poseComp)
		{
			this.pose_comp = poseComp;
			this.mirror = ((poseComp >> 7 & 1) == 1);
			this.pose = (poseComp & 127);
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x000B4C02 File Offset: 0x000B3002
		public void setPose(byte poseComp)
		{
			this.applyPose(poseComp);
			this.updatePose();
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x000B4C14 File Offset: 0x000B3014
		public void rebuildState()
		{
			Block block = new Block();
			block.write(this.owner, this.group);
			block.writeInt32(this.visualShirt);
			block.writeInt32(this.visualPants);
			block.writeInt32(this.visualHat);
			block.writeInt32(this.visualBackpack);
			block.writeInt32(this.visualVest);
			block.writeInt32(this.visualMask);
			block.writeInt32(this.visualGlasses);
			block.writeUInt16(this.clothes.shirt);
			block.writeByte(this.shirtQuality);
			block.writeUInt16(this.clothes.pants);
			block.writeByte(this.pantsQuality);
			block.writeUInt16(this.clothes.hat);
			block.writeByte(this.hatQuality);
			block.writeUInt16(this.clothes.backpack);
			block.writeByte(this.backpackQuality);
			block.writeUInt16(this.clothes.vest);
			block.writeByte(this.vestQuality);
			block.writeUInt16(this.clothes.mask);
			block.writeByte(this.maskQuality);
			block.writeUInt16(this.clothes.glasses);
			block.writeByte(this.glassesQuality);
			block.writeByteArray(this.shirtState);
			block.writeByteArray(this.pantsState);
			block.writeByteArray(this.hatState);
			block.writeByteArray(this.backpackState);
			block.writeByteArray(this.vestState);
			block.writeByteArray(this.maskState);
			block.writeByteArray(this.glassesState);
			block.writeByte(this.pose_comp);
			int size;
			byte[] bytes = block.getBytes(out size);
			BarricadeManager.updateState(base.transform, bytes, size);
			this.updated = Time.realtimeSinceStartup;
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x000B4DE4 File Offset: 0x000B31E4
		public void updateVisuals(int newVisualShirt, int newVisualPants, int newVisualHat, int newVisualBackpack, int newVisualVest, int newVisualMask, int newVisualGlasses)
		{
			this.clothes.visualShirt = newVisualShirt;
			this.clothes.visualPants = newVisualPants;
			this.clothes.visualHat = newVisualHat;
			this.clothes.visualBackpack = newVisualBackpack;
			this.clothes.visualVest = newVisualVest;
			this.clothes.visualMask = newVisualMask;
			this.clothes.visualGlasses = newVisualGlasses;
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x000B4E49 File Offset: 0x000B3249
		public void clearVisuals()
		{
			this.updateVisuals(0, 0, 0, 0, 0, 0, 0);
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x000B4E58 File Offset: 0x000B3258
		public void updateClothes(ushort newShirt, byte newShirtQuality, byte[] newShirtState, ushort newPants, byte newPantsQuality, byte[] newPantsState, ushort newHat, byte newHatQuality, byte[] newHatState, ushort newBackpack, byte newBackpackQuality, byte[] newBackpackState, ushort newVest, byte newVestQuality, byte[] newVestState, ushort newMask, byte newMaskQuality, byte[] newMaskState, ushort newGlasses, byte newGlassesQuality, byte[] newGlassesState)
		{
			this.clothes.shirt = newShirt;
			this.shirtQuality = newShirtQuality;
			this.shirtState = newShirtState;
			this.clothes.pants = newPants;
			this.pantsQuality = newPantsQuality;
			this.pantsState = newPantsState;
			this.clothes.hat = newHat;
			this.hatQuality = newHatQuality;
			this.hatState = newHatState;
			this.clothes.backpack = newBackpack;
			this.backpackQuality = newBackpackQuality;
			this.backpackState = newBackpackState;
			this.clothes.vest = newVest;
			this.vestQuality = newVestQuality;
			this.vestState = newVestState;
			this.clothes.mask = newMask;
			this.maskQuality = newMaskQuality;
			this.maskState = newMaskState;
			this.clothes.glasses = newGlasses;
			this.glassesQuality = newGlassesQuality;
			this.glassesState = newGlassesState;
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x000B4F30 File Offset: 0x000B3330
		public void dropClothes()
		{
			if (this.shirt != 0)
			{
				ItemManager.dropItem(new Item(this.shirt, 1, this.shirtQuality, this.shirtState), base.transform.position, false, true, true);
			}
			if (this.pants != 0)
			{
				ItemManager.dropItem(new Item(this.pants, 1, this.pantsQuality, this.pantsState), base.transform.position, false, true, true);
			}
			if (this.hat != 0)
			{
				ItemManager.dropItem(new Item(this.hat, 1, this.hatQuality, this.hatState), base.transform.position, false, true, true);
			}
			if (this.backpack != 0)
			{
				ItemManager.dropItem(new Item(this.backpack, 1, this.backpackQuality, this.backpackState), base.transform.position, false, true, true);
			}
			if (this.vest != 0)
			{
				ItemManager.dropItem(new Item(this.vest, 1, this.vestQuality, this.vestState), base.transform.position, false, true, true);
			}
			if (this.mask != 0)
			{
				ItemManager.dropItem(new Item(this.mask, 1, this.maskQuality, this.maskState), base.transform.position, false, true, true);
			}
			if (this.glasses != 0)
			{
				ItemManager.dropItem(new Item(this.glasses, 1, this.glassesQuality, this.glassesState), base.transform.position, false, true, true);
			}
			this.clearClothes();
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x000B50C0 File Offset: 0x000B34C0
		public void clearClothes()
		{
			this.updateClothes(0, 0, new byte[0], 0, 0, new byte[0], 0, 0, new byte[0], 0, 0, new byte[0], 0, 0, new byte[0], 0, 0, new byte[0], 0, 0, new byte[0]);
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x000B510C File Offset: 0x000B350C
		public void updatePose()
		{
			string animation;
			switch (this.pose)
			{
			case 0:
				animation = "T";
				break;
			case 1:
				animation = "Classic";
				break;
			case 2:
				animation = "Lie";
				break;
			default:
				return;
			}
			if (this.anim != null)
			{
				this.anim.transform.localScale = new Vector3((float)((!this.mirror) ? 1 : -1), 1f, 1f);
				this.anim.Play(animation);
			}
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x000B51AC File Offset: 0x000B35AC
		public void updateState(byte[] state)
		{
			Block block = new Block(state);
			this._owner = new CSteamID((ulong)block.read(Types.UINT64_TYPE));
			this._group = new CSteamID((ulong)block.read(Types.UINT64_TYPE));
			this.clothes.skin = new Color32(210, 210, 210, byte.MaxValue);
			this.clothes.visualShirt = block.readInt32();
			this.clothes.visualPants = block.readInt32();
			this.clothes.visualHat = block.readInt32();
			this.clothes.visualBackpack = block.readInt32();
			this.clothes.visualVest = block.readInt32();
			this.clothes.visualMask = block.readInt32();
			this.clothes.visualGlasses = block.readInt32();
			this.clothes.shirt = block.readUInt16();
			this.shirtQuality = block.readByte();
			this.clothes.pants = block.readUInt16();
			this.pantsQuality = block.readByte();
			this.clothes.hat = block.readUInt16();
			this.hatQuality = block.readByte();
			this.clothes.backpack = block.readUInt16();
			this.backpackQuality = block.readByte();
			this.clothes.vest = block.readUInt16();
			this.vestQuality = block.readByte();
			this.clothes.mask = block.readUInt16();
			this.maskQuality = block.readByte();
			this.clothes.glasses = block.readUInt16();
			this.glassesQuality = block.readByte();
			this.shirtState = block.readByteArray();
			this.pantsState = block.readByteArray();
			this.hatState = block.readByteArray();
			this.backpackState = block.readByteArray();
			this.vestState = block.readByteArray();
			this.maskState = block.readByteArray();
			this.glassesState = block.readByteArray();
			this.clothes.apply();
			this.setPose(block.readByte());
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x000B53CC File Offset: 0x000B37CC
		public override void updateState(Asset asset, byte[] state)
		{
			this.isLocked = ((ItemBarricadeAsset)asset).isLocked;
			Transform transform = base.transform.FindChild("Root");
			this.anim = transform.GetComponent<Animation>();
			this.clothes = transform.GetComponent<HumanClothes>();
			this.updateState(state);
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x000B541A File Offset: 0x000B381A
		public override bool checkUseable()
		{
			return this.checkUpdate(Provider.client, Player.player.quests.groupID) && !PlayerUI.window.showCursor;
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x000B544C File Offset: 0x000B384C
		public override void use()
		{
			if (Input.GetKey(ControlsSettings.other))
			{
				if (Player.player.equipment.useable is UseableClothing)
				{
					BarricadeManager.updateMannequin(base.transform, EMannequinUpdateMode.ADD);
				}
				else
				{
					BarricadeManager.updateMannequin(base.transform, EMannequinUpdateMode.REMOVE);
				}
			}
			else
			{
				PlayerBarricadeMannequinUI.open(this);
				PlayerLifeUI.close();
			}
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x000B54AE File Offset: 0x000B38AE
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			if (this.checkUseable())
			{
				message = EPlayerMessage.USE;
			}
			else
			{
				message = EPlayerMessage.LOCKED;
			}
			text = string.Empty;
			color = Color.white;
			return !PlayerUI.window.showCursor;
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x000B54E7 File Offset: 0x000B38E7
		public void ManualOnDestroy()
		{
			if (!Provider.isServer)
			{
				return;
			}
			this.dropClothes();
		}

		// Token: 0x040013B5 RID: 5045
		private CSteamID _owner;

		// Token: 0x040013B6 RID: 5046
		private CSteamID _group;

		// Token: 0x040013B7 RID: 5047
		private bool isLocked;

		// Token: 0x040013B8 RID: 5048
		public byte pose_comp;

		// Token: 0x040013B9 RID: 5049
		public bool mirror;

		// Token: 0x040013BA RID: 5050
		public byte pose;

		// Token: 0x040013BB RID: 5051
		private float updated;

		// Token: 0x040013BC RID: 5052
		private Animation anim;

		// Token: 0x040013BE RID: 5054
		public byte shirtQuality;

		// Token: 0x040013BF RID: 5055
		public byte pantsQuality;

		// Token: 0x040013C0 RID: 5056
		public byte hatQuality;

		// Token: 0x040013C1 RID: 5057
		public byte backpackQuality;

		// Token: 0x040013C2 RID: 5058
		public byte vestQuality;

		// Token: 0x040013C3 RID: 5059
		public byte maskQuality;

		// Token: 0x040013C4 RID: 5060
		public byte glassesQuality;

		// Token: 0x040013C5 RID: 5061
		public byte[] shirtState;

		// Token: 0x040013C6 RID: 5062
		public byte[] pantsState;

		// Token: 0x040013C7 RID: 5063
		public byte[] hatState;

		// Token: 0x040013C8 RID: 5064
		public byte[] backpackState;

		// Token: 0x040013C9 RID: 5065
		public byte[] vestState;

		// Token: 0x040013CA RID: 5066
		public byte[] maskState;

		// Token: 0x040013CB RID: 5067
		public byte[] glassesState;
	}
}
