using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.IO.FormattedFiles;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003D6 RID: 982
	public class ItemGunAsset : ItemWeaponAsset, IDevkitAssetSpawnable
	{
		// Token: 0x06001AB3 RID: 6835 RVA: 0x000950CB File Offset: 0x000934CB
		public ItemGunAsset()
		{
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x000950D4 File Offset: 0x000934D4
		public ItemGunAsset(Bundle bundle, Local localization, byte[] hash) : base(bundle, localization, hash)
		{
			this._shoot = (AudioClip)bundle.load("Shoot");
			this._reload = (AudioClip)bundle.load("Reload");
			this._hammer = (AudioClip)bundle.load("Hammer");
			this._aim = (AudioClip)bundle.load("Aim");
			this._minigun = (AudioClip)bundle.load("Minigun");
			this._projectile = (GameObject)bundle.load("Projectile");
			bundle.unload();
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x00095174 File Offset: 0x00093574
		public ItemGunAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._shoot = (AudioClip)bundle.load("Shoot");
			this._reload = (AudioClip)bundle.load("Reload");
			this._hammer = (AudioClip)bundle.load("Hammer");
			this._aim = (AudioClip)bundle.load("Aim");
			this._minigun = (AudioClip)bundle.load("Minigun");
			this._projectile = (GameObject)bundle.load("Projectile");
			this.ammoMin = data.readByte("Ammo_Min");
			this.ammoMax = data.readByte("Ammo_Max");
			this.sightID = data.readUInt16("Sight");
			this.tacticalID = data.readUInt16("Tactical");
			this.gripID = data.readUInt16("Grip");
			this.barrelID = data.readUInt16("Barrel");
			this.magazineID = data.readUInt16("Magazine");
			int num = data.readInt32("Magazine_Replacements");
			this.magazineReplacements = new MagazineReplacement[num];
			for (int i = 0; i < num; i++)
			{
				ushort id2 = data.readUInt16("Magazine_Replacement_" + i + "_ID");
				string map = data.readString("Magazine_Replacement_" + i + "_Map");
				MagazineReplacement magazineReplacement = default(MagazineReplacement);
				magazineReplacement.id = id2;
				magazineReplacement.map = map;
				this.magazineReplacements[i] = magazineReplacement;
			}
			this.unplace = data.readSingle("Unplace");
			this.replace = data.readSingle("Replace");
			if ((double)this.replace < 0.01)
			{
				this.replace = 1f;
			}
			this.hasSight = data.has("Hook_Sight");
			this.hasTactical = data.has("Hook_Tactical");
			this.hasGrip = data.has("Hook_Grip");
			this.hasBarrel = data.has("Hook_Barrel");
			int num2 = data.readInt32("Magazine_Calibers");
			if (num2 > 0)
			{
				this.magazineCalibers = new ushort[num2];
				for (int j = 0; j < num2; j++)
				{
					this.magazineCalibers[j] = data.readUInt16("Magazine_Caliber_" + j);
				}
				int num3 = data.readInt32("Attachment_Calibers");
				if (num3 > 0)
				{
					this.attachmentCalibers = new ushort[num3];
					for (int k = 0; k < num3; k++)
					{
						this.attachmentCalibers[k] = data.readUInt16("Attachment_Caliber_" + k);
					}
				}
				else
				{
					this.attachmentCalibers = this.magazineCalibers;
				}
			}
			else
			{
				this.magazineCalibers = new ushort[1];
				this.magazineCalibers[0] = data.readUInt16("Caliber");
				this.attachmentCalibers = this.magazineCalibers;
			}
			this.firerate = data.readByte("Firerate");
			this.action = (EAction)Enum.Parse(typeof(EAction), data.readString("Action"), true);
			this.deleteEmptyMagazines = data.has("Delete_Empty_Magazines");
			this.bursts = data.readInt32("Bursts");
			this.hasSafety = data.has("Safety");
			this.hasSemi = data.has("Semi");
			this.hasAuto = data.has("Auto");
			this.hasBurst = (this.bursts > 0);
			this.isTurret = data.has("Turret");
			if (this.hasAuto)
			{
				this.firemode = EFiremode.AUTO;
			}
			else if (this.hasSemi)
			{
				this.firemode = EFiremode.SEMI;
			}
			else if (this.hasBurst)
			{
				this.firemode = EFiremode.BURST;
			}
			else if (this.hasSafety)
			{
				this.firemode = EFiremode.SAFETY;
			}
			this.spreadAim = data.readSingle("Spread_Aim");
			this.spreadHip = data.readSingle("Spread_Hip");
			if (data.has("Recoil_Aim"))
			{
				this.recoilAim = data.readSingle("Recoil_Aim");
				this.useRecoilAim = true;
			}
			else
			{
				this.recoilAim = 1f;
				this.useRecoilAim = false;
			}
			this.recoilMin_x = data.readSingle("Recoil_Min_X");
			this.recoilMin_y = data.readSingle("Recoil_Min_Y");
			this.recoilMax_x = data.readSingle("Recoil_Max_X");
			this.recoilMax_y = data.readSingle("Recoil_Max_Y");
			this.recover_x = data.readSingle("Recover_X");
			this.recover_y = data.readSingle("Recover_Y");
			this.shakeMin_x = data.readSingle("Shake_Min_X");
			this.shakeMin_y = data.readSingle("Shake_Min_Y");
			this.shakeMin_z = data.readSingle("Shake_Min_Z");
			this.shakeMax_x = data.readSingle("Shake_Max_X");
			this.shakeMax_y = data.readSingle("Shake_Max_Y");
			this.shakeMax_z = data.readSingle("Shake_Max_Z");
			this.ballisticSteps = data.readByte("Ballistic_Steps");
			this.ballisticTravel = data.readSingle("Ballistic_Travel");
			if (data.has("Ballistic_Steps"))
			{
				this.ballisticSteps = data.readByte("Ballistic_Steps");
				this.ballisticTravel = data.readSingle("Ballistic_Travel");
			}
			else
			{
				this.ballisticTravel = 10f;
				this.ballisticSteps = (byte)Mathf.CeilToInt(this.range / this.ballisticTravel);
			}
			if (data.has("Ballistic_Drop"))
			{
				this.ballisticDrop = data.readSingle("Ballistic_Drop");
			}
			else
			{
				this.ballisticDrop = 0.002f;
			}
			if (data.has("Ballistic_Force"))
			{
				this.ballisticForce = data.readSingle("Ballistic_Force");
			}
			else
			{
				this.ballisticForce = 0.002f;
			}
			this.projectilePenetrateBuildables = data.has("Projectile_Penetrate_Buildables");
			this.reloadTime = data.readSingle("Reload_Time");
			this.hammerTime = data.readSingle("Hammer_Time");
			this.muzzle = data.readUInt16("Muzzle");
			this.explosion = data.readUInt16("Explosion");
			if (data.has("Shell"))
			{
				this.shell = data.readUInt16("Shell");
			}
			else if (this.action == EAction.Pump || this.action == EAction.Break)
			{
				this.shell = 33;
			}
			else if (this.action != EAction.Rail)
			{
				this.shell = 1;
			}
			else
			{
				this.shell = 0;
			}
			if (data.has("Alert_Radius"))
			{
				this.alertRadius = data.readSingle("Alert_Radius");
			}
			else
			{
				this.alertRadius = 48f;
			}
			bundle.unload();
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x00095892 File Offset: 0x00093C92
		public AudioClip shoot
		{
			get
			{
				return this._shoot;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x0009589A File Offset: 0x00093C9A
		public AudioClip reload
		{
			get
			{
				return this._reload;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x000958A2 File Offset: 0x00093CA2
		public AudioClip hammer
		{
			get
			{
				return this._hammer;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x000958AA File Offset: 0x00093CAA
		public AudioClip aim
		{
			get
			{
				return this._aim;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x000958B2 File Offset: 0x00093CB2
		public AudioClip minigun
		{
			get
			{
				return this._minigun;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001ABB RID: 6843 RVA: 0x000958BA File Offset: 0x00093CBA
		public GameObject projectile
		{
			get
			{
				return this._projectile;
			}
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x000958C2 File Offset: 0x00093CC2
		public void devkitAssetSpawn()
		{
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x000958C4 File Offset: 0x00093CC4
		public override bool isDangerous
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x000958C8 File Offset: 0x00093CC8
		public override string getContext(string desc, byte[] state)
		{
			ushort id = BitConverter.ToUInt16(state, 8);
			ItemMagazineAsset itemMagazineAsset = (ItemMagazineAsset)Assets.find(EAssetType.ITEM, id);
			if (itemMagazineAsset != null)
			{
				desc += PlayerDashboardInventoryUI.localization.format("Ammo", new object[]
				{
					string.Concat(new string[]
					{
						"<color=",
						Palette.hex(ItemTool.getRarityColorUI(itemMagazineAsset.rarity)),
						">",
						itemMagazineAsset.itemName,
						"</color>"
					}),
					state[10],
					itemMagazineAsset.amount
				});
			}
			else
			{
				desc += PlayerDashboardInventoryUI.localization.format("Ammo", new object[]
				{
					PlayerDashboardInventoryUI.localization.format("None"),
					0,
					0
				});
			}
			desc += "\n\n";
			return desc;
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x000959C4 File Offset: 0x00093DC4
		public override byte[] getState(EItemOrigin origin)
		{
			byte[] magazineState = this.getMagazineState(this.getMagazineID());
			byte[] array = new byte[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				100,
				100,
				100,
				100,
				100
			};
			array[0] = this.sightState[0];
			array[1] = this.sightState[1];
			array[2] = this.tacticalState[0];
			array[3] = this.tacticalState[1];
			array[4] = this.gripState[0];
			array[5] = this.gripState[1];
			array[6] = this.barrelState[0];
			array[7] = this.barrelState[1];
			array[8] = magazineState[0];
			array[9] = magazineState[1];
			array[10] = ((origin == EItemOrigin.WORLD && UnityEngine.Random.value >= ((Provider.modeConfigData == null) ? 0.9f : Provider.modeConfigData.Items.Gun_Bullets_Full_Chance)) ? ((byte)Mathf.CeilToInt((float)UnityEngine.Random.Range((int)this.ammoMin, (int)(this.ammoMax + 1)) * ((Provider.modeConfigData == null) ? 1f : Provider.modeConfigData.Items.Gun_Bullets_Multiplier))) : this.ammoMax);
			array[11] = (byte)this.firemode;
			return array;
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00095AE0 File Offset: 0x00093EE0
		public byte[] getState(ushort sight, ushort tactical, ushort grip, ushort barrel, ushort magazine, byte ammo)
		{
			byte[] bytes = BitConverter.GetBytes(sight);
			byte[] bytes2 = BitConverter.GetBytes(tactical);
			byte[] bytes3 = BitConverter.GetBytes(grip);
			byte[] bytes4 = BitConverter.GetBytes(barrel);
			byte[] bytes5 = BitConverter.GetBytes(magazine);
			byte[] array = new byte[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				100,
				100,
				100,
				100,
				100
			};
			array[0] = bytes[0];
			array[1] = bytes[1];
			array[2] = bytes2[0];
			array[3] = bytes2[1];
			array[4] = bytes3[0];
			array[5] = bytes3[1];
			array[6] = bytes4[0];
			array[7] = bytes4[1];
			array[8] = bytes5[0];
			array[9] = bytes5[1];
			array[10] = ammo;
			array[11] = (byte)this.firemode;
			return array;
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x00095B75 File Offset: 0x00093F75
		// (set) Token: 0x06001AC2 RID: 6850 RVA: 0x00095B7D File Offset: 0x00093F7D
		[Inspectable("#SDG::Asset.Item.Gun.Sight_ID.Name", null)]
		public ushort sightID
		{
			get
			{
				return this._sightID;
			}
			set
			{
				this._sightID = value;
				this.sightState = BitConverter.GetBytes(this.sightID);
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x00095B97 File Offset: 0x00093F97
		// (set) Token: 0x06001AC4 RID: 6852 RVA: 0x00095B9F File Offset: 0x00093F9F
		[Inspectable("#SDG::Asset.Item.Gun.Tactical_ID.Name", null)]
		public ushort tacticalID
		{
			get
			{
				return this._tacticalID;
			}
			set
			{
				this._tacticalID = value;
				this.tacticalState = BitConverter.GetBytes(this.tacticalID);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x00095BB9 File Offset: 0x00093FB9
		// (set) Token: 0x06001AC6 RID: 6854 RVA: 0x00095BC1 File Offset: 0x00093FC1
		[Inspectable("#SDG::Asset.Item.Gun.Grip_ID.Name", null)]
		public ushort gripID
		{
			get
			{
				return this._gripID;
			}
			set
			{
				this._gripID = value;
				this.gripState = BitConverter.GetBytes(this.gripID);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x00095BDB File Offset: 0x00093FDB
		// (set) Token: 0x06001AC8 RID: 6856 RVA: 0x00095BE3 File Offset: 0x00093FE3
		[Inspectable("#SDG::Asset.Item.Gun.Barrel_ID.Name", null)]
		public ushort barrelID
		{
			get
			{
				return this._barrelID;
			}
			set
			{
				this._barrelID = value;
				this.barrelState = BitConverter.GetBytes(this.barrelID);
			}
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00095C00 File Offset: 0x00094000
		public ushort getMagazineID()
		{
			if (Level.info != null && this.magazineReplacements != null)
			{
				foreach (MagazineReplacement magazineReplacement in this.magazineReplacements)
				{
					if (magazineReplacement.map == Level.info.name)
					{
						return magazineReplacement.id;
					}
				}
			}
			return this.magazineID;
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x00095C73 File Offset: 0x00094073
		private byte[] getMagazineState(ushort id)
		{
			return BitConverter.GetBytes(id);
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x00095C7B File Offset: 0x0009407B
		// (set) Token: 0x06001ACC RID: 6860 RVA: 0x00095C83 File Offset: 0x00094083
		[Inspectable("#SDG::Asset.Item.Gun.Attachment_Calibers.Name", null)]
		public ushort[] attachmentCalibers { get; private set; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x00095C8C File Offset: 0x0009408C
		// (set) Token: 0x06001ACE RID: 6862 RVA: 0x00095C94 File Offset: 0x00094094
		[Inspectable("#SDG::Asset.Item.Gun.Magazine_Calibers.Name", null)]
		public ushort[] magazineCalibers { get; private set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x00095C9D File Offset: 0x0009409D
		public override bool showQuality
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x00095CA0 File Offset: 0x000940A0
		protected override void readAsset(IFormattedFileReader reader)
		{
			base.readAsset(reader);
			this.ammoMin = reader.readValue<byte>("Ammo_Min");
			this.ammoMax = reader.readValue<byte>("Ammo_Max");
			this.sightID = reader.readValue<ushort>("Sight_ID");
			this.tacticalID = reader.readValue<ushort>("Tactical_ID");
			this.gripID = reader.readValue<ushort>("Grip_ID");
			this.barrelID = reader.readValue<ushort>("Barrel_ID");
			this.magazineID = reader.readValue<ushort>("Magazine_ID");
			this.unplace = reader.readValue<float>("Unplace");
			this.replace = reader.readValue<float>("Replace");
			if ((double)this.replace < 0.01)
			{
				this.replace = 1f;
			}
			this.hasSight = reader.readValue<bool>("Hook_Sight");
			this.hasTactical = reader.readValue<bool>("Hook_Tactical");
			this.hasGrip = reader.readValue<bool>("Hook_Grip");
			this.hasBarrel = reader.readValue<bool>("Hook_Barrel");
			int num = reader.readArrayLength("Magazine_Calibers");
			if (num > 0)
			{
				this.magazineCalibers = new ushort[num];
				for (int i = 0; i < num; i++)
				{
					this.magazineCalibers[i] = reader.readValue<ushort>(i);
				}
				int num2 = reader.readArrayLength("Attachment_Calibers");
				if (num2 > 0)
				{
					this.attachmentCalibers = new ushort[num2];
					for (int j = 0; j < num2; j++)
					{
						this.attachmentCalibers[j] = reader.readValue<ushort>(j);
					}
				}
				else
				{
					this.attachmentCalibers = this.magazineCalibers;
				}
			}
			else
			{
				this.magazineCalibers = new ushort[1];
				this.magazineCalibers[0] = reader.readValue<ushort>("Caliber");
				this.attachmentCalibers = this.magazineCalibers;
			}
			this.firerate = reader.readValue<byte>("Firerate");
			this.action = reader.readValue<EAction>("Action");
			this.deleteEmptyMagazines = reader.readValue<bool>("Delete_Empty_Magazines");
			this.bursts = reader.readValue<int>("Bursts");
			this.hasSafety = reader.readValue<bool>("Safety");
			this.hasSemi = reader.readValue<bool>("Semi");
			this.hasAuto = reader.readValue<bool>("Auto");
			this.hasBurst = (this.bursts > 0);
			this.isTurret = reader.readValue<bool>("Turret");
			if (this.hasAuto)
			{
				this.firemode = EFiremode.AUTO;
			}
			else if (this.hasSemi)
			{
				this.firemode = EFiremode.SEMI;
			}
			else if (this.hasBurst)
			{
				this.firemode = EFiremode.BURST;
			}
			else if (this.hasSafety)
			{
				this.firemode = EFiremode.SAFETY;
			}
			this.spreadAim = reader.readValue<float>("Spread_Aim");
			this.spreadHip = reader.readValue<float>("Spread_Hip");
			this.recoilAim = reader.readValue<float>("Recoil_Aim");
			this.useRecoilAim = reader.readValue<bool>("Use_Recoil_Aim");
			this.recoilMin_x = reader.readValue<float>("Recoil_Min_X");
			this.recoilMin_y = reader.readValue<float>("Recoil_Min_Y");
			this.recoilMax_x = reader.readValue<float>("Recoil_Max_X");
			this.recoilMax_y = reader.readValue<float>("Recoil_Max_Y");
			this.recover_x = reader.readValue<float>("Recover_X");
			this.recover_y = reader.readValue<float>("Recover_Y");
			this.shakeMin_x = reader.readValue<float>("Shake_Min_X");
			this.shakeMin_y = reader.readValue<float>("Shake_Min_Y");
			this.shakeMin_z = reader.readValue<float>("Shake_Min_Z");
			this.shakeMax_x = reader.readValue<float>("Shake_Max_X");
			this.shakeMax_y = reader.readValue<float>("Shake_Max_Y");
			this.shakeMax_z = reader.readValue<float>("Shake_Max_Z");
			this.ballisticSteps = reader.readValue<byte>("Ballistic_Steps");
			this.ballisticTravel = (float)reader.readValue<byte>("Ballistic_Travel");
			this.ballisticDrop = reader.readValue<float>("Ballistic_Drop");
			this.ballisticForce = reader.readValue<float>("Ballistic_Force");
			this.projectilePenetrateBuildables = reader.readValue<bool>("Projectile_Penetrate_Buildables");
			this.reloadTime = reader.readValue<float>("Reload_Time");
			this.hammerTime = reader.readValue<float>("Hammer_Time");
			this.muzzle = reader.readValue<ushort>("Muzzle");
			this.explosion = reader.readValue<ushort>("Explosion");
			this.shell = reader.readValue<ushort>("Shell");
			if (reader.containsKey("Alert_Radius"))
			{
				this.alertRadius = reader.readValue<float>("Alert_Radius");
			}
			else
			{
				this.alertRadius = 48f;
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0009614C File Offset: 0x0009454C
		protected override void writeAsset(IFormattedFileWriter writer)
		{
			base.writeAsset(writer);
			writer.writeValue<byte>("Ammo_Min", this.ammoMin);
			writer.writeValue<byte>("Ammo_Max", this.ammoMax);
			writer.writeValue<ushort>("Sight_ID", this.sightID);
			writer.writeValue<ushort>("Tactical_ID", this.tacticalID);
			writer.writeValue<ushort>("Grip_ID", this.gripID);
			writer.writeValue<ushort>("Barrel_ID", this.barrelID);
			writer.writeValue<ushort>("Magazine_ID", this.magazineID);
			writer.writeValue<float>("Unplace", this.unplace);
			writer.writeValue<float>("Replace", this.replace);
			writer.writeValue<bool>("Hook_Sight", this.hasSight);
			writer.writeValue<bool>("Hook_Tactical", this.hasTactical);
			writer.writeValue<bool>("Hook_Grip", this.hasGrip);
			writer.writeValue<bool>("Hook_Barrel", this.hasBarrel);
			writer.beginArray("Magazine_Calibers");
			foreach (ushort value in this.magazineCalibers)
			{
				writer.writeValue<ushort>(value);
			}
			writer.endArray();
			writer.beginArray("Attachment_Calibers");
			foreach (ushort value2 in this.attachmentCalibers)
			{
				writer.writeValue<ushort>(value2);
			}
			writer.endArray();
			writer.writeValue<byte>("Firerate", this.firerate);
			writer.writeValue<EAction>("Action", this.action);
			writer.writeValue<bool>("Delete_Empty_Magazines", this.deleteEmptyMagazines);
			writer.writeValue<int>("Bursts", this.bursts);
			writer.writeValue<bool>("Safety", this.hasSafety);
			writer.writeValue<bool>("Semi", this.hasSemi);
			writer.writeValue<bool>("Auto", this.hasAuto);
			writer.writeValue<bool>("Turret", this.isTurret);
			writer.writeValue<float>("Spread_Aim", this.spreadAim);
			writer.writeValue<float>("Spread_Hip", this.spreadHip);
			writer.writeValue<float>("Recoil_Aim", this.recoilAim);
			writer.writeValue<bool>("Use_Recoil_Aim", this.useRecoilAim);
			writer.writeValue<float>("Recoil_Min_X", this.recoilMin_x);
			writer.writeValue<float>("Recoil_Min_Y", this.recoilMin_y);
			writer.writeValue<float>("Recoil_Max_X", this.recoilMax_x);
			writer.writeValue<float>("Recoil_Max_Y", this.recoilMax_y);
			writer.writeValue<float>("Recover_X", this.recover_x);
			writer.writeValue<float>("Recover_Y", this.recover_y);
			writer.writeValue<float>("Shake_Min_X", this.shakeMin_x);
			writer.writeValue<float>("Shake_Min_Y", this.shakeMin_y);
			writer.writeValue<float>("Shake_Min_Z", this.shakeMin_z);
			writer.writeValue<float>("Shake_Max_X", this.shakeMax_x);
			writer.writeValue<float>("Shake_Max_Y", this.shakeMax_y);
			writer.writeValue<float>("Shake_Max_Z", this.shakeMax_z);
			writer.writeValue<byte>("Ballistic_Steps", this.ballisticSteps);
			writer.writeValue<float>("Ballistic_Travel", this.ballisticTravel);
			writer.writeValue<float>("Ballistic_Drop", this.ballisticDrop);
			writer.writeValue<float>("Ballistic_Force", this.ballisticForce);
			writer.writeValue<bool>("Projectile_Penetrate_Buildables", this.projectilePenetrateBuildables);
			writer.writeValue<float>("Reload_Time", this.reloadTime);
			writer.writeValue<float>("Hammer_Time", this.hammerTime);
			writer.writeValue<ushort>("Muzzle", this.muzzle);
			writer.writeValue<ushort>("Explosion", this.explosion);
			writer.writeValue<ushort>("Shell", this.shell);
			writer.writeValue<float>("Alert_Radius", this.alertRadius);
		}

		// Token: 0x04000F71 RID: 3953
		protected AudioClip _shoot;

		// Token: 0x04000F72 RID: 3954
		protected AudioClip _reload;

		// Token: 0x04000F73 RID: 3955
		protected AudioClip _hammer;

		// Token: 0x04000F74 RID: 3956
		protected AudioClip _aim;

		// Token: 0x04000F75 RID: 3957
		protected AudioClip _minigun;

		// Token: 0x04000F76 RID: 3958
		protected GameObject _projectile;

		// Token: 0x04000F77 RID: 3959
		[Inspectable("#SDG::Asset.Item.Gun.Alert_Radius.Name", null)]
		public float alertRadius;

		// Token: 0x04000F78 RID: 3960
		[Inspectable("#SDG::Asset.Item.Gun.Ammo_Min.Name", null)]
		public byte ammoMin;

		// Token: 0x04000F79 RID: 3961
		[Inspectable("#SDG::Asset.Item.Gun.Ammo_Max.Name", null)]
		public byte ammoMax;

		// Token: 0x04000F7A RID: 3962
		private ushort _sightID;

		// Token: 0x04000F7B RID: 3963
		private byte[] sightState;

		// Token: 0x04000F7C RID: 3964
		private ushort _tacticalID;

		// Token: 0x04000F7D RID: 3965
		private byte[] tacticalState;

		// Token: 0x04000F7E RID: 3966
		private ushort _gripID;

		// Token: 0x04000F7F RID: 3967
		private byte[] gripState;

		// Token: 0x04000F80 RID: 3968
		private ushort _barrelID;

		// Token: 0x04000F81 RID: 3969
		private byte[] barrelState;

		// Token: 0x04000F82 RID: 3970
		private ushort magazineID;

		// Token: 0x04000F83 RID: 3971
		private MagazineReplacement[] magazineReplacements;

		// Token: 0x04000F84 RID: 3972
		[Inspectable("#SDG::Asset.Item.Gun.Unplace.Name", null)]
		public float unplace;

		// Token: 0x04000F85 RID: 3973
		[Inspectable("#SDG::Asset.Item.Gun.Replace.Name", null)]
		public float replace;

		// Token: 0x04000F86 RID: 3974
		[Inspectable("#SDG::Asset.Item.Gun.Has_Sight.Name", null)]
		public bool hasSight;

		// Token: 0x04000F87 RID: 3975
		[Inspectable("#SDG::Asset.Item.Gun.Has_Tactical.Name", null)]
		public bool hasTactical;

		// Token: 0x04000F88 RID: 3976
		[Inspectable("#SDG::Asset.Item.Gun.Has_Grip.Name", null)]
		public bool hasGrip;

		// Token: 0x04000F89 RID: 3977
		[Inspectable("#SDG::Asset.Item.Gun.Has_Barrel.Name", null)]
		public bool hasBarrel;

		// Token: 0x04000F8C RID: 3980
		[Inspectable("#SDG::Asset.Item.Gun.Firerate.Name", null)]
		public byte firerate;

		// Token: 0x04000F8D RID: 3981
		[Inspectable("#SDG::Asset.Item.Gun.Action.Name", null)]
		public EAction action;

		// Token: 0x04000F8E RID: 3982
		[Inspectable("#SDG::Asset.Item.Gun.Delete_Empty_Magazines.Name", null)]
		public bool deleteEmptyMagazines;

		// Token: 0x04000F8F RID: 3983
		[Inspectable("#SDG::Asset.Item.Gun.Has_Safety.Name", null)]
		public bool hasSafety;

		// Token: 0x04000F90 RID: 3984
		[Inspectable("#SDG::Asset.Item.Gun.Has_Semi.Name", null)]
		public bool hasSemi;

		// Token: 0x04000F91 RID: 3985
		[Inspectable("#SDG::Asset.Item.Gun.Has_Auto.Name", null)]
		public bool hasAuto;

		// Token: 0x04000F92 RID: 3986
		[Inspectable("#SDG::Asset.Item.Gun.Has_Burst.Name", null)]
		public bool hasBurst;

		// Token: 0x04000F93 RID: 3987
		[Inspectable("#SDG::Asset.Item.Gun.Is_Turret.Name", null)]
		public bool isTurret;

		// Token: 0x04000F94 RID: 3988
		[Inspectable("#SDG::Asset.Item.Gun.Bursts.Name", null)]
		public int bursts;

		// Token: 0x04000F95 RID: 3989
		private EFiremode firemode;

		// Token: 0x04000F96 RID: 3990
		[Inspectable("#SDG::Asset.Item.Gun.Spread_Aim.Name", null)]
		public float spreadAim;

		// Token: 0x04000F97 RID: 3991
		[Inspectable("#SDG::Asset.Item.Gun.Spread_Hip.Name", null)]
		public float spreadHip;

		// Token: 0x04000F98 RID: 3992
		[Inspectable("#SDG::Asset.Item.Gun.Recoil_Aim.Name", null)]
		public float recoilAim;

		// Token: 0x04000F99 RID: 3993
		[Inspectable("#SDG::Asset.Item.Gun.Use_Recoil_Aim.Name", null)]
		public bool useRecoilAim;

		// Token: 0x04000F9A RID: 3994
		[Inspectable("#SDG::Asset.Item.Gun.Recoil_Min_X.Name", null)]
		public float recoilMin_x;

		// Token: 0x04000F9B RID: 3995
		[Inspectable("#SDG::Asset.Item.Gun.Recoil_Min_Y.Name", null)]
		public float recoilMin_y;

		// Token: 0x04000F9C RID: 3996
		[Inspectable("#SDG::Asset.Item.Gun.Recoil_Max_X.Name", null)]
		public float recoilMax_x;

		// Token: 0x04000F9D RID: 3997
		[Inspectable("#SDG::Asset.Item.Gun.Recoil_Max_Y.Name", null)]
		public float recoilMax_y;

		// Token: 0x04000F9E RID: 3998
		[Inspectable("#SDG::Asset.Item.Gun.Recover_X.Name", null)]
		public float recover_x;

		// Token: 0x04000F9F RID: 3999
		[Inspectable("#SDG::Asset.Item.Gun.Recover_Y.Name", null)]
		public float recover_y;

		// Token: 0x04000FA0 RID: 4000
		[Inspectable("#SDG::Asset.Item.Gun.Shake_Min_X.Name", null)]
		public float shakeMin_x;

		// Token: 0x04000FA1 RID: 4001
		[Inspectable("#SDG::Asset.Item.Gun.Shake_Min_Y.Name", null)]
		public float shakeMin_y;

		// Token: 0x04000FA2 RID: 4002
		[Inspectable("#SDG::Asset.Item.Gun.Shake_Min_Z.Name", null)]
		public float shakeMin_z;

		// Token: 0x04000FA3 RID: 4003
		[Inspectable("#SDG::Asset.Item.Gun.Shake_Max_X.Name", null)]
		public float shakeMax_x;

		// Token: 0x04000FA4 RID: 4004
		[Inspectable("#SDG::Asset.Item.Gun.Shake_Max_Y.Name", null)]
		public float shakeMax_y;

		// Token: 0x04000FA5 RID: 4005
		[Inspectable("#SDG::Asset.Item.Gun.Shake_Max_Z.Name", null)]
		public float shakeMax_z;

		// Token: 0x04000FA6 RID: 4006
		[Inspectable("#SDG::Asset.Item.Gun.Ballistic_Steps.Name", null)]
		public byte ballisticSteps;

		// Token: 0x04000FA7 RID: 4007
		[Inspectable("#SDG::Asset.Item.Gun.Ballistic_Travel.Name", null)]
		public float ballisticTravel;

		// Token: 0x04000FA8 RID: 4008
		[Inspectable("#SDG::Asset.Item.Gun.Ballistic_Drop.Name", null)]
		public float ballisticDrop;

		// Token: 0x04000FA9 RID: 4009
		[Inspectable("#SDG::Asset.Item.Gun.Ballistic_Force.Name", null)]
		public float ballisticForce;

		// Token: 0x04000FAA RID: 4010
		[Inspectable("#SDG::Asset.Item.Gun.Projectile_Penetrate_Buildables.Name", null)]
		public bool projectilePenetrateBuildables;

		// Token: 0x04000FAB RID: 4011
		[Inspectable("#SDG::Asset.Item.Gun.Reload_Time.Name", null)]
		public float reloadTime;

		// Token: 0x04000FAC RID: 4012
		[Inspectable("#SDG::Asset.Item.Gun.Hammer_Time.Name", null)]
		public float hammerTime;

		// Token: 0x04000FAD RID: 4013
		[Inspectable("#SDG::Asset.Item.Gun.Muzzle.Name", null)]
		public ushort muzzle;

		// Token: 0x04000FAE RID: 4014
		[Inspectable("#SDG::Asset.Item.Gun.Shell.Name", null)]
		public ushort shell;

		// Token: 0x04000FAF RID: 4015
		[Inspectable("#SDG::Asset.Item.Gun.Explosion.Name", null)]
		public ushort explosion;
	}
}
