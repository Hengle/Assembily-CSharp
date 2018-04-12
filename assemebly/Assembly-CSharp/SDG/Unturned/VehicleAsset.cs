using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000429 RID: 1065
	public class VehicleAsset : Asset, ISkinableAsset
	{
		// Token: 0x06001CDC RID: 7388 RVA: 0x0009C460 File Offset: 0x0009A860
		public VehicleAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (id < 200 && !bundle.hasResource && !data.has("Bypass_ID_Limit"))
			{
				throw new NotSupportedException("ID < 200");
			}
			this._vehicleName = localization.format("Name");
			this._vehicle = (GameObject)bundle.load("Vehicle");
			if (this.vehicle == null)
			{
				throw new NotSupportedException("Missing vehicle gameobject");
			}
			this._clip = (GameObject)bundle.load("Clip");
			this._size2_z = data.readSingle("Size2_Z");
			this._sharedSkinName = data.readString("Shared_Skin_Name");
			if (data.has("Shared_Skin_Lookup_ID"))
			{
				this._sharedSkinLookupID = data.readUInt16("Shared_Skin_Lookup_ID");
			}
			else
			{
				this._sharedSkinLookupID = id;
			}
			if (data.has("Engine"))
			{
				this._engine = (EEngine)Enum.Parse(typeof(EEngine), data.readString("Engine"), true);
				if (this.engine == EEngine.BOAT || this.engine == EEngine.BLIMP)
				{
					if (this.vehicle.transform.FindChild("Buoyancy") == null)
					{
						this._engine = EEngine.CAR;
					}
				}
				else if (this.engine != EEngine.CAR && this.engine != EEngine.TRAIN && this.vehicle.transform.FindChild("Rotors") == null)
				{
					this._engine = EEngine.CAR;
				}
			}
			else
			{
				this._engine = EEngine.CAR;
			}
			if (data.has("Rarity"))
			{
				this._rarity = (EItemRarity)Enum.Parse(typeof(EItemRarity), data.readString("Rarity"), true);
			}
			else
			{
				this._rarity = EItemRarity.COMMON;
			}
			this._hasHeadlights = (this.vehicle.transform.FindChild("Headlights") != null);
			this._hasSirens = (this.vehicle.transform.FindChild("Sirens") != null);
			this._hasHook = (this.vehicle.transform.FindChild("Hook") != null);
			this._hasZip = data.has("Zip");
			this._hasBicycle = data.has("Bicycle");
			this.isReclined = data.has("Reclined");
			this._hasCrawler = data.has("Crawler");
			this._hasLockMouse = data.has("LockMouse");
			this._hasTraction = data.has("Traction");
			this._hasSleds = data.has("Sleds");
			this._ignition = (AudioClip)bundle.load("Ignition");
			this._horn = (AudioClip)bundle.load("Horn");
			if (this.clip == null)
			{
				Assets.errors.Add(this.vehicleName + " is missing collision data. Highly recommended to fix.");
			}
			if (this.vehicle != null && this.vehicle.transform.FindChild("Seats") == null)
			{
				Assets.errors.Add(this.vehicleName + " vehicle is missing seats.");
			}
			if (this.clip != null && this.clip.transform.FindChild("Seats") == null)
			{
				Assets.errors.Add(this.vehicleName + " clip is missing seats.");
			}
			if (data.has("Pitch_Idle"))
			{
				this._pitchIdle = data.readSingle("Pitch_Idle");
			}
			else
			{
				this._pitchIdle = 0.5f;
				AudioSource component = this.vehicle.GetComponent<AudioSource>();
				if (component != null)
				{
					AudioClip clip = component.clip;
					if (clip != null)
					{
						if (clip.name == "Engine_Large")
						{
							this._pitchIdle = 0.625f;
						}
						else if (clip.name == "Engine_Small")
						{
							this._pitchIdle = 0.75f;
						}
					}
					else
					{
						Assets.errors.Add(this.vehicleName + " missing engine audio!");
					}
				}
			}
			if (data.has("Pitch_Drive"))
			{
				this._pitchDrive = data.readSingle("Pitch_Drive");
			}
			else
			{
				this._pitchDrive = 0.05f;
				AudioSource component2 = this.vehicle.GetComponent<AudioSource>();
				if (component2 != null)
				{
					AudioClip clip2 = component2.clip;
					if (clip2 != null)
					{
						if (clip2.name == "Engine_Large")
						{
							this._pitchDrive = 0.025f;
						}
						else if (clip2.name == "Engine_Small")
						{
							this._pitchDrive = 0.075f;
						}
					}
					else
					{
						Assets.errors.Add(this.vehicleName + " missing engine audio!");
					}
				}
			}
			this._speedMin = data.readSingle("Speed_Min");
			this._speedMax = data.readSingle("Speed_Max");
			if (this.engine != EEngine.TRAIN)
			{
				this._speedMax *= 1.25f;
			}
			this._steerMin = data.readSingle("Steer_Min");
			this._steerMax = data.readSingle("Steer_Max") * 0.75f;
			this._brake = data.readSingle("Brake");
			this._lift = data.readSingle("Lift");
			this._fuelMin = data.readUInt16("Fuel_Min");
			this._fuelMax = data.readUInt16("Fuel_Max");
			this._fuel = data.readUInt16("Fuel");
			this._healthMin = data.readUInt16("Health_Min");
			this._healthMax = data.readUInt16("Health_Max");
			this._health = data.readUInt16("Health");
			this._explosion = data.readUInt16("Explosion");
			if (data.has("Explosion_Min_Force_Y"))
			{
				this.minExplosionForce = data.readVector3("Explosion_Min_Force");
			}
			else
			{
				this.minExplosionForce = new Vector3(0f, 1024f, 0f);
			}
			if (data.has("Explosion_Max_Force_Y"))
			{
				this.maxExplosionForce = data.readVector3("Explosion_Max_Force");
			}
			else
			{
				this.maxExplosionForce = new Vector3(0f, 1024f, 0f);
			}
			if (data.has("Exit"))
			{
				this._exit = data.readSingle("Exit");
			}
			else
			{
				this._exit = 2f;
			}
			if (data.has("Cam_Follow_Distance"))
			{
				this._camFollowDistance = data.readSingle("Cam_Follow_Distance");
			}
			else
			{
				this._camFollowDistance = 5.5f;
			}
			this._camDriverOffset = data.readSingle("Cam_Driver_Offset");
			if (data.has("Bumper_Multiplier"))
			{
				this._bumperMultiplier = data.readSingle("Bumper_Multiplier");
			}
			else
			{
				this._bumperMultiplier = 1f;
			}
			if (data.has("Passenger_Explosion_Armor"))
			{
				this._passengerExplosionArmor = data.readSingle("Passenger_Explosion_Armor");
			}
			else
			{
				this._passengerExplosionArmor = 1f;
			}
			if (this.engine == EEngine.HELICOPTER || this.engine == EEngine.BLIMP)
			{
				this._sqrDelta = Mathf.Pow(this.speedMax * 0.125f, 2f);
			}
			else
			{
				this._sqrDelta = Mathf.Pow(this.speedMax * 0.1f, 2f);
			}
			this._turrets = new TurretInfo[(int)data.readByte("Turrets")];
			byte b = 0;
			while ((int)b < this.turrets.Length)
			{
				TurretInfo turretInfo = new TurretInfo();
				turretInfo.seatIndex = data.readByte("Turret_" + b + "_Seat_Index");
				turretInfo.itemID = data.readUInt16("Turret_" + b + "_Item_ID");
				turretInfo.yawMin = data.readSingle("Turret_" + b + "_Yaw_Min");
				turretInfo.yawMax = data.readSingle("Turret_" + b + "_Yaw_Max");
				turretInfo.pitchMin = data.readSingle("Turret_" + b + "_Pitch_Min");
				turretInfo.pitchMax = data.readSingle("Turret_" + b + "_Pitch_Max");
				turretInfo.useAimCamera = !data.has("Turret_" + b + "_Ignore_Aim_Camera");
				turretInfo.aimOffset = data.readSingle("Turret_" + b + "_Aim_Offset");
				this._turrets[(int)b] = turretInfo;
				b += 1;
			}
			this.isVulnerable = !data.has("Invulnerable");
			this.isVulnerableToExplosions = !data.has("Explosions_Invulnerable");
			this.isVulnerableToEnvironment = !data.has("Environment_Invulnerable");
			this.isVulnerableToBumper = !data.has("Bumper_Invulnerable");
			this.canTiresBeDamaged = !data.has("Tires_Invulnerable");
			if (data.has("Air_Turn_Responsiveness"))
			{
				this.airTurnResponsiveness = data.readSingle("Air_Turn_Responsiveness");
			}
			else
			{
				this.airTurnResponsiveness = 2f;
			}
			if (data.has("Air_Steer_Min"))
			{
				this.airSteerMin = data.readSingle("Air_Steer_Min");
			}
			else
			{
				this.airSteerMin = this.steerMin;
			}
			if (data.has("Air_Steer_Max"))
			{
				this.airSteerMax = data.readSingle("Air_Steer_Max");
			}
			else
			{
				this.airSteerMax = this.steerMax;
			}
			this.bicycleAnimSpeed = data.readSingle("Bicycle_Anim_Speed");
			this.staminaBoost = data.readSingle("Stamina_Boost");
			this.useStaminaBoost = data.has("Stamina_Boost");
			this.isStaminaPowered = data.has("Stamina_Powered");
			this.supportsMobileBuildables = data.has("Supports_Mobile_Buildables");
			this.trunkStorage_X = data.readByte("Trunk_Storage_X");
			this.trunkStorage_Y = data.readByte("Trunk_Storage_Y");
			this.trainTrackOffset = data.readSingle("Train_Track_Offset");
			this.trainWheelOffset = data.readSingle("Train_Wheel_Offset");
			this.trainCarLength = data.readSingle("Train_Car_Length");
			this._shouldVerifyHash = !data.has("Bypass_Hash_Verification");
			if (!Dedicator.isDedicated)
			{
				this._albedoBase = (Texture2D)bundle.load("Albedo_Base");
				this._metallicBase = (Texture2D)bundle.load("Metallic_Base");
				this._emissionBase = (Texture2D)bundle.load("Emission_Base");
			}
			bundle.unload();
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x0009CFB5 File Offset: 0x0009B3B5
		public bool shouldVerifyHash
		{
			get
			{
				return this._shouldVerifyHash;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001CDE RID: 7390 RVA: 0x0009CFBD File Offset: 0x0009B3BD
		public string vehicleName
		{
			get
			{
				return this._vehicleName;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x0009CFC5 File Offset: 0x0009B3C5
		public float size2_z
		{
			get
			{
				return this._size2_z;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x0009CFCD File Offset: 0x0009B3CD
		public string sharedSkinName
		{
			get
			{
				return this._sharedSkinName;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x0009CFD5 File Offset: 0x0009B3D5
		public ushort sharedSkinLookupID
		{
			get
			{
				return this._sharedSkinLookupID;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x0009CFDD File Offset: 0x0009B3DD
		public EEngine engine
		{
			get
			{
				return this._engine;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001CE3 RID: 7395 RVA: 0x0009CFE5 File Offset: 0x0009B3E5
		public EItemRarity rarity
		{
			get
			{
				return this._rarity;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x0009CFED File Offset: 0x0009B3ED
		public GameObject vehicle
		{
			get
			{
				return this._vehicle;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x0009CFF5 File Offset: 0x0009B3F5
		public GameObject clip
		{
			get
			{
				return this._clip;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x0009CFFD File Offset: 0x0009B3FD
		public AudioClip ignition
		{
			get
			{
				return this._ignition;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x0009D005 File Offset: 0x0009B405
		public AudioClip horn
		{
			get
			{
				return this._horn;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x0009D00D File Offset: 0x0009B40D
		public float pitchIdle
		{
			get
			{
				return this._pitchIdle;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x0009D015 File Offset: 0x0009B415
		public float pitchDrive
		{
			get
			{
				return this._pitchDrive;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x0009D01D File Offset: 0x0009B41D
		public float speedMin
		{
			get
			{
				return this._speedMin;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x0009D025 File Offset: 0x0009B425
		public float speedMax
		{
			get
			{
				return this._speedMax;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x0009D02D File Offset: 0x0009B42D
		public float steerMin
		{
			get
			{
				return this._steerMin;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x0009D035 File Offset: 0x0009B435
		public float steerMax
		{
			get
			{
				return this._steerMax;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x0009D03D File Offset: 0x0009B43D
		public float brake
		{
			get
			{
				return this._brake;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x0009D045 File Offset: 0x0009B445
		public float lift
		{
			get
			{
				return this._lift;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x0009D04D File Offset: 0x0009B44D
		public ushort fuelMin
		{
			get
			{
				return this._fuelMin;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x0009D055 File Offset: 0x0009B455
		public ushort fuelMax
		{
			get
			{
				return this._fuelMax;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x0009D05D File Offset: 0x0009B45D
		public ushort fuel
		{
			get
			{
				return this._fuel;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x0009D065 File Offset: 0x0009B465
		public ushort healthMin
		{
			get
			{
				return this._healthMin;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001CF4 RID: 7412 RVA: 0x0009D06D File Offset: 0x0009B46D
		public ushort healthMax
		{
			get
			{
				return this._healthMax;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x0009D075 File Offset: 0x0009B475
		public ushort health
		{
			get
			{
				return this._health;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x0009D07D File Offset: 0x0009B47D
		public ushort explosion
		{
			get
			{
				return this._explosion;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x0009D085 File Offset: 0x0009B485
		// (set) Token: 0x06001CF8 RID: 7416 RVA: 0x0009D08D File Offset: 0x0009B48D
		public Vector3 minExplosionForce { get; set; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x0009D096 File Offset: 0x0009B496
		// (set) Token: 0x06001CFA RID: 7418 RVA: 0x0009D09E File Offset: 0x0009B49E
		public Vector3 maxExplosionForce { get; set; }

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x0009D0A7 File Offset: 0x0009B4A7
		public bool isExplosive
		{
			get
			{
				return this.explosion != 0;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x0009D0B5 File Offset: 0x0009B4B5
		public bool hasHeadlights
		{
			get
			{
				return this._hasHeadlights;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x0009D0BD File Offset: 0x0009B4BD
		public bool hasSirens
		{
			get
			{
				return this._hasSirens;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x0009D0C5 File Offset: 0x0009B4C5
		public bool hasHook
		{
			get
			{
				return this._hasHook;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001CFF RID: 7423 RVA: 0x0009D0CD File Offset: 0x0009B4CD
		public bool hasZip
		{
			get
			{
				return this._hasZip;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x0009D0D5 File Offset: 0x0009B4D5
		public bool hasBicycle
		{
			get
			{
				return this._hasBicycle;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x0009D0DD File Offset: 0x0009B4DD
		// (set) Token: 0x06001D02 RID: 7426 RVA: 0x0009D0E5 File Offset: 0x0009B4E5
		public bool isReclined { get; protected set; }

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x0009D0EE File Offset: 0x0009B4EE
		public bool hasCrawler
		{
			get
			{
				return this._hasCrawler;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x0009D0F6 File Offset: 0x0009B4F6
		public bool hasLockMouse
		{
			get
			{
				return this._hasLockMouse;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x0009D0FE File Offset: 0x0009B4FE
		public bool hasTraction
		{
			get
			{
				return this._hasTraction;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x0009D106 File Offset: 0x0009B506
		public bool hasSleds
		{
			get
			{
				return this._hasSleds;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x0009D10E File Offset: 0x0009B50E
		public float exit
		{
			get
			{
				return this._exit;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x0009D116 File Offset: 0x0009B516
		public float sqrDelta
		{
			get
			{
				return this._sqrDelta;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x0009D11E File Offset: 0x0009B51E
		public float camFollowDistance
		{
			get
			{
				return this._camFollowDistance;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x0009D126 File Offset: 0x0009B526
		public float camDriverOffset
		{
			get
			{
				return this._camDriverOffset;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x0009D12E File Offset: 0x0009B52E
		public float bumperMultiplier
		{
			get
			{
				return this._bumperMultiplier;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001D0C RID: 7436 RVA: 0x0009D136 File Offset: 0x0009B536
		public float passengerExplosionArmor
		{
			get
			{
				return this._passengerExplosionArmor;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x0009D13E File Offset: 0x0009B53E
		public TurretInfo[] turrets
		{
			get
			{
				return this._turrets;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x0009D146 File Offset: 0x0009B546
		public Texture albedoBase
		{
			get
			{
				return this._albedoBase;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x0009D14E File Offset: 0x0009B54E
		public Texture metallicBase
		{
			get
			{
				return this._metallicBase;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x0009D156 File Offset: 0x0009B556
		public Texture emissionBase
		{
			get
			{
				return this._emissionBase;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x0009D15E File Offset: 0x0009B55E
		// (set) Token: 0x06001D12 RID: 7442 RVA: 0x0009D166 File Offset: 0x0009B566
		public float airTurnResponsiveness { get; protected set; }

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001D13 RID: 7443 RVA: 0x0009D16F File Offset: 0x0009B56F
		// (set) Token: 0x06001D14 RID: 7444 RVA: 0x0009D177 File Offset: 0x0009B577
		public float airSteerMin { get; protected set; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x0009D180 File Offset: 0x0009B580
		// (set) Token: 0x06001D16 RID: 7446 RVA: 0x0009D188 File Offset: 0x0009B588
		public float airSteerMax { get; protected set; }

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x0009D191 File Offset: 0x0009B591
		// (set) Token: 0x06001D18 RID: 7448 RVA: 0x0009D199 File Offset: 0x0009B599
		public float bicycleAnimSpeed { get; protected set; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x0009D1A2 File Offset: 0x0009B5A2
		// (set) Token: 0x06001D1A RID: 7450 RVA: 0x0009D1AA File Offset: 0x0009B5AA
		public float trainTrackOffset { get; protected set; }

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x0009D1B3 File Offset: 0x0009B5B3
		// (set) Token: 0x06001D1C RID: 7452 RVA: 0x0009D1BB File Offset: 0x0009B5BB
		public float trainWheelOffset { get; protected set; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x0009D1C4 File Offset: 0x0009B5C4
		// (set) Token: 0x06001D1E RID: 7454 RVA: 0x0009D1CC File Offset: 0x0009B5CC
		public float trainCarLength { get; protected set; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x0009D1D5 File Offset: 0x0009B5D5
		// (set) Token: 0x06001D20 RID: 7456 RVA: 0x0009D1DD File Offset: 0x0009B5DD
		public float staminaBoost { get; protected set; }

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x0009D1E6 File Offset: 0x0009B5E6
		// (set) Token: 0x06001D22 RID: 7458 RVA: 0x0009D1EE File Offset: 0x0009B5EE
		public bool useStaminaBoost { get; protected set; }

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x0009D1F7 File Offset: 0x0009B5F7
		// (set) Token: 0x06001D24 RID: 7460 RVA: 0x0009D1FF File Offset: 0x0009B5FF
		public bool isStaminaPowered { get; protected set; }

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x0009D208 File Offset: 0x0009B608
		// (set) Token: 0x06001D26 RID: 7462 RVA: 0x0009D210 File Offset: 0x0009B610
		public bool supportsMobileBuildables { get; protected set; }

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x0009D219 File Offset: 0x0009B619
		// (set) Token: 0x06001D28 RID: 7464 RVA: 0x0009D221 File Offset: 0x0009B621
		public byte trunkStorage_X { get; set; }

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x0009D22A File Offset: 0x0009B62A
		// (set) Token: 0x06001D2A RID: 7466 RVA: 0x0009D232 File Offset: 0x0009B632
		public byte trunkStorage_Y { get; set; }

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x0009D23B File Offset: 0x0009B63B
		public override EAssetType assetCategory
		{
			get
			{
				return EAssetType.VEHICLE;
			}
		}

		// Token: 0x04001110 RID: 4368
		protected bool _shouldVerifyHash;

		// Token: 0x04001111 RID: 4369
		protected string _vehicleName;

		// Token: 0x04001112 RID: 4370
		protected float _size2_z;

		// Token: 0x04001113 RID: 4371
		protected string _sharedSkinName;

		// Token: 0x04001114 RID: 4372
		protected ushort _sharedSkinLookupID;

		// Token: 0x04001115 RID: 4373
		protected EEngine _engine;

		// Token: 0x04001116 RID: 4374
		protected EItemRarity _rarity;

		// Token: 0x04001117 RID: 4375
		protected GameObject _vehicle;

		// Token: 0x04001118 RID: 4376
		protected GameObject _clip;

		// Token: 0x04001119 RID: 4377
		protected AudioClip _ignition;

		// Token: 0x0400111A RID: 4378
		protected AudioClip _horn;

		// Token: 0x0400111B RID: 4379
		protected float _pitchIdle;

		// Token: 0x0400111C RID: 4380
		protected float _pitchDrive;

		// Token: 0x0400111D RID: 4381
		protected float _speedMin;

		// Token: 0x0400111E RID: 4382
		protected float _speedMax;

		// Token: 0x0400111F RID: 4383
		protected float _steerMin;

		// Token: 0x04001120 RID: 4384
		protected float _steerMax;

		// Token: 0x04001121 RID: 4385
		protected float _brake;

		// Token: 0x04001122 RID: 4386
		protected float _lift;

		// Token: 0x04001123 RID: 4387
		protected ushort _fuelMin;

		// Token: 0x04001124 RID: 4388
		protected ushort _fuelMax;

		// Token: 0x04001125 RID: 4389
		protected ushort _fuel;

		// Token: 0x04001126 RID: 4390
		protected ushort _healthMin;

		// Token: 0x04001127 RID: 4391
		protected ushort _healthMax;

		// Token: 0x04001128 RID: 4392
		protected ushort _health;

		// Token: 0x04001129 RID: 4393
		protected ushort _explosion;

		// Token: 0x0400112C RID: 4396
		protected bool _hasHeadlights;

		// Token: 0x0400112D RID: 4397
		protected bool _hasSirens;

		// Token: 0x0400112E RID: 4398
		protected bool _hasHook;

		// Token: 0x0400112F RID: 4399
		protected bool _hasZip;

		// Token: 0x04001130 RID: 4400
		protected bool _hasBicycle;

		// Token: 0x04001132 RID: 4402
		protected bool _hasCrawler;

		// Token: 0x04001133 RID: 4403
		protected bool _hasLockMouse;

		// Token: 0x04001134 RID: 4404
		protected bool _hasTraction;

		// Token: 0x04001135 RID: 4405
		protected bool _hasSleds;

		// Token: 0x04001136 RID: 4406
		protected float _exit;

		// Token: 0x04001137 RID: 4407
		protected float _sqrDelta;

		// Token: 0x04001138 RID: 4408
		protected float _camFollowDistance;

		// Token: 0x04001139 RID: 4409
		protected float _camDriverOffset;

		// Token: 0x0400113A RID: 4410
		protected float _bumperMultiplier;

		// Token: 0x0400113B RID: 4411
		protected float _passengerExplosionArmor;

		// Token: 0x0400113C RID: 4412
		protected TurretInfo[] _turrets;

		// Token: 0x0400113D RID: 4413
		protected Texture2D _albedoBase;

		// Token: 0x0400113E RID: 4414
		protected Texture2D _metallicBase;

		// Token: 0x0400113F RID: 4415
		protected Texture2D _emissionBase;

		// Token: 0x04001140 RID: 4416
		public bool isVulnerable;

		// Token: 0x04001141 RID: 4417
		public bool isVulnerableToExplosions;

		// Token: 0x04001142 RID: 4418
		public bool isVulnerableToEnvironment;

		// Token: 0x04001143 RID: 4419
		public bool isVulnerableToBumper;

		// Token: 0x04001144 RID: 4420
		public bool canTiresBeDamaged;
	}
}
