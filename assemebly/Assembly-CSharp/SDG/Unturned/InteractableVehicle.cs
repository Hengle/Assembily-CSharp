using System;
using System.Collections.Generic;
using SDG.Framework.Devkit;
using SDG.Framework.Landscapes;
using SDG.Framework.Utilities;
using SDG.Framework.Water;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004FA RID: 1274
	public class InteractableVehicle : Interactable, ILandscaleHoleVolumeInteractionHandler
	{
		// Token: 0x14000074 RID: 116
		// (add) Token: 0x0600223F RID: 8767 RVA: 0x000BBC88 File Offset: 0x000BA088
		// (remove) Token: 0x06002240 RID: 8768 RVA: 0x000BBCC0 File Offset: 0x000BA0C0
		public event VehiclePassengersUpdated onPassengersUpdated;

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x06002241 RID: 8769 RVA: 0x000BBCF8 File Offset: 0x000BA0F8
		// (remove) Token: 0x06002242 RID: 8770 RVA: 0x000BBD30 File Offset: 0x000BA130
		public event VehicleLockUpdated onLockUpdated;

		// Token: 0x14000076 RID: 118
		// (add) Token: 0x06002243 RID: 8771 RVA: 0x000BBD68 File Offset: 0x000BA168
		// (remove) Token: 0x06002244 RID: 8772 RVA: 0x000BBDA0 File Offset: 0x000BA1A0
		public event VehicleHeadlightsUpdated onHeadlightsUpdated;

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06002245 RID: 8773 RVA: 0x000BBDD8 File Offset: 0x000BA1D8
		// (remove) Token: 0x06002246 RID: 8774 RVA: 0x000BBE10 File Offset: 0x000BA210
		public event VehicleTaillightsUpdated onTaillightsUpdated;

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x06002247 RID: 8775 RVA: 0x000BBE48 File Offset: 0x000BA248
		// (remove) Token: 0x06002248 RID: 8776 RVA: 0x000BBE80 File Offset: 0x000BA280
		public event VehicleSirensUpdated onSirensUpdated;

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x06002249 RID: 8777 RVA: 0x000BBEB8 File Offset: 0x000BA2B8
		// (remove) Token: 0x0600224A RID: 8778 RVA: 0x000BBEF0 File Offset: 0x000BA2F0
		public event VehicleBlimpUpdated onBlimpUpdated;

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x0600224B RID: 8779 RVA: 0x000BBF28 File Offset: 0x000BA328
		// (remove) Token: 0x0600224C RID: 8780 RVA: 0x000BBF60 File Offset: 0x000BA360
		public event VehicleBatteryChangedHandler batteryChanged;

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x0600224D RID: 8781 RVA: 0x000BBF98 File Offset: 0x000BA398
		// (remove) Token: 0x0600224E RID: 8782 RVA: 0x000BBFD0 File Offset: 0x000BA3D0
		public event VehicleSkinChangedHandler skinChanged;

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x000BC006 File Offset: 0x000BA406
		// (set) Token: 0x06002250 RID: 8784 RVA: 0x000BC00E File Offset: 0x000BA40E
		public Road road { get; protected set; }

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x000BC017 File Offset: 0x000BA417
		// (set) Token: 0x06002252 RID: 8786 RVA: 0x000BC01F File Offset: 0x000BA41F
		public float timeInsideSafezone { get; protected set; }

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x000BC028 File Offset: 0x000BA428
		public bool usesFuel
		{
			get
			{
				return !this.asset.isStaminaPowered;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x000BC038 File Offset: 0x000BA438
		public bool usesBattery
		{
			get
			{
				return !this.asset.isStaminaPowered;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x000BC048 File Offset: 0x000BA448
		public bool usesHealth
		{
			get
			{
				return this.asset.engine != EEngine.TRAIN;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x000BC05B File Offset: 0x000BA45B
		// (set) Token: 0x06002257 RID: 8791 RVA: 0x000BC063 File Offset: 0x000BA463
		public bool isBoosting { get; protected set; }

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x000BC06C File Offset: 0x000BA46C
		// (set) Token: 0x06002259 RID: 8793 RVA: 0x000BC074 File Offset: 0x000BA474
		public bool isEngineOn { get; protected set; }

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x000BC07D File Offset: 0x000BA47D
		public bool hasBattery
		{
			get
			{
				return !this.usesBattery || this.batteryCharge > 0;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x000BC095 File Offset: 0x000BA495
		public bool isBatteryFull
		{
			get
			{
				return !this.usesBattery || this.batteryCharge >= 10000;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x000BC0B4 File Offset: 0x000BA4B4
		public bool landscapeHoleAutoIgnoreTerrainCollision
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000BC0B8 File Offset: 0x000BA4B8
		public void landscapeHoleBeginCollision(LandscapeHoleVolume volume, List<TerrainCollider> terrainColliders)
		{
			foreach (Wheel wheel in this.tires)
			{
				if (!(wheel.wheel == null))
				{
					foreach (TerrainCollider collider in terrainColliders)
					{
						Physics.IgnoreCollision(wheel.wheel, collider, true);
					}
				}
			}
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000BC14C File Offset: 0x000BA54C
		public void landscapeHoleEndCollision(LandscapeHoleVolume volume, List<TerrainCollider> terrainColliders)
		{
			foreach (Wheel wheel in this.tires)
			{
				if (!(wheel.wheel == null))
				{
					foreach (TerrainCollider collider in terrainColliders)
					{
						Physics.IgnoreCollision(wheel.wheel, collider, false);
					}
				}
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x000BC1E0 File Offset: 0x000BA5E0
		public bool canUseHorn
		{
			get
			{
				return Time.realtimeSinceStartup - this.horned > 0.5f && (!this.usesBattery || this.hasBattery);
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x000BC20F File Offset: 0x000BA60F
		public bool canUseTurret
		{
			get
			{
				return !this.isDead;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x000BC21A File Offset: 0x000BA61A
		public bool canTurnOnLights
		{
			get
			{
				return (!this.usesBattery || this.hasBattery) && !this.isUnderwater;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x000BC23E File Offset: 0x000BA63E
		public bool isRefillable
		{
			get
			{
				return this.usesFuel && this.fuel < this.asset.fuel && !this.isDriven && !this.isExploded;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x000BC278 File Offset: 0x000BA678
		public bool isSiphonable
		{
			get
			{
				return this.usesFuel && this.fuel > 0 && !this.isDriven && !this.isExploded;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x000BC2A8 File Offset: 0x000BA6A8
		public bool isRepaired
		{
			get
			{
				return this.health == this.asset.health;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06002265 RID: 8805 RVA: 0x000BC2BD File Offset: 0x000BA6BD
		public bool isDriven
		{
			get
			{
				return this.passengers != null && this.passengers[0].player != null;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06002266 RID: 8806 RVA: 0x000BC2E0 File Offset: 0x000BA6E0
		public bool isDriver
		{
			get
			{
				return !Dedicator.isDedicated && this.checkDriver(Provider.client);
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x000BC2FC File Offset: 0x000BA6FC
		public bool isEmpty
		{
			get
			{
				byte b = 0;
				while ((int)b < this.passengers.Length)
				{
					if (this.passengers[(int)b].player != null)
					{
						return false;
					}
					b += 1;
				}
				return true;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x000BC338 File Offset: 0x000BA738
		public bool isDrowned
		{
			get
			{
				return this._isDrowned;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06002269 RID: 8809 RVA: 0x000BC340 File Offset: 0x000BA740
		public bool isUnderwater
		{
			get
			{
				if (this.waterCenterTransform != null)
				{
					return WaterUtility.isPointUnderwater(this.waterCenterTransform.position);
				}
				return WaterUtility.isPointUnderwater(base.transform.position + new Vector3(0f, 1f, 0f));
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x000BC398 File Offset: 0x000BA798
		public bool isBatteryReplaceable
		{
			get
			{
				return this.usesBattery && !this.isBatteryFull && !this.isDriven && !this.isExploded;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x000BC3C7 File Offset: 0x000BA7C7
		public bool isTireReplaceable
		{
			get
			{
				return !this.isDriven && !this.isExploded && this.asset.canTiresBeDamaged;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x000BC3ED File Offset: 0x000BA7ED
		public bool canBeDamaged
		{
			get
			{
				return this.asset.engine != EEngine.TRAIN;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x0600226D RID: 8813 RVA: 0x000BC400 File Offset: 0x000BA800
		public bool isGoingToRespawn
		{
			get
			{
				return this.isExploded || this.isDrowned;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x000BC418 File Offset: 0x000BA818
		public bool isAutoClearable
		{
			get
			{
				if (this.isExploded)
				{
					return true;
				}
				if (this.isUnderwater && this.buoyancy == null)
				{
					return true;
				}
				if (this.asset != null)
				{
					if (this.asset.engine == EEngine.BOAT && this.fuel == 0)
					{
						return true;
					}
					if (this.asset.engine == EEngine.TRAIN)
					{
						return false;
					}
				}
				return false;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x000BC48D File Offset: 0x000BA88D
		public float lastDead
		{
			get
			{
				return this._lastDead;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x000BC495 File Offset: 0x000BA895
		public float lastUnderwater
		{
			get
			{
				return this._lastUnderwater;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x000BC49D File Offset: 0x000BA89D
		public float lastExploded
		{
			get
			{
				return this._lastExploded;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x000BC4A5 File Offset: 0x000BA8A5
		public float slip
		{
			get
			{
				return this._slip;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06002273 RID: 8819 RVA: 0x000BC4AD File Offset: 0x000BA8AD
		public bool isDead
		{
			get
			{
				return this.health == 0;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002274 RID: 8820 RVA: 0x000BC4B8 File Offset: 0x000BA8B8
		public float factor
		{
			get
			{
				return this._factor;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x000BC4C0 File Offset: 0x000BA8C0
		public float speed
		{
			get
			{
				return this._speed;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x000BC4C8 File Offset: 0x000BA8C8
		public float physicsSpeed
		{
			get
			{
				return this._physicsSpeed;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x000BC4D0 File Offset: 0x000BA8D0
		public float spedometer
		{
			get
			{
				return this._spedometer;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x000BC4D8 File Offset: 0x000BA8D8
		public int turn
		{
			get
			{
				return this._turn;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x000BC4E0 File Offset: 0x000BA8E0
		public float steer
		{
			get
			{
				return this._steer;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600227A RID: 8826 RVA: 0x000BC4E8 File Offset: 0x000BA8E8
		// (set) Token: 0x0600227B RID: 8827 RVA: 0x000BC4F0 File Offset: 0x000BA8F0
		public TrainCar[] trainCars { get; protected set; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x000BC4F9 File Offset: 0x000BA8F9
		public bool sirensOn
		{
			get
			{
				return this._sirensOn;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x000BC501 File Offset: 0x000BA901
		public Transform headlights
		{
			get
			{
				return this._headlights;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x000BC509 File Offset: 0x000BA909
		public bool headlightsOn
		{
			get
			{
				return this._headlightsOn;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x000BC511 File Offset: 0x000BA911
		public Transform taillights
		{
			get
			{
				return this._taillights;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x000BC519 File Offset: 0x000BA919
		public bool taillightsOn
		{
			get
			{
				return this._taillightsOn;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06002281 RID: 8833 RVA: 0x000BC521 File Offset: 0x000BA921
		public CSteamID lockedOwner
		{
			get
			{
				return this._lockedOwner;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x000BC529 File Offset: 0x000BA929
		public CSteamID lockedGroup
		{
			get
			{
				return this._lockedGroup;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06002283 RID: 8835 RVA: 0x000BC531 File Offset: 0x000BA931
		public bool isLocked
		{
			get
			{
				return this._isLocked;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x000BC539 File Offset: 0x000BA939
		public bool isSkinned
		{
			get
			{
				return this.skinID != 0;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x000BC547 File Offset: 0x000BA947
		public VehicleAsset asset
		{
			get
			{
				return this._asset;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x000BC54F File Offset: 0x000BA94F
		public Passenger[] passengers
		{
			get
			{
				return this._passengers;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x000BC557 File Offset: 0x000BA957
		public Passenger[] turrets
		{
			get
			{
				return this._turrets;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06002288 RID: 8840 RVA: 0x000BC55F File Offset: 0x000BA95F
		// (set) Token: 0x06002289 RID: 8841 RVA: 0x000BC567 File Offset: 0x000BA967
		public Wheel[] tires { get; protected set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600228A RID: 8842 RVA: 0x000BC570 File Offset: 0x000BA970
		private bool usesGravity
		{
			get
			{
				return this.asset.engine != EEngine.TRAIN;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x000BC583 File Offset: 0x000BA983
		private bool isKinematic
		{
			get
			{
				return !this.usesGravity;
			}
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x000BC58E File Offset: 0x000BA98E
		public void replaceBattery(Player player, byte quality)
		{
			this.giveBatteryItem(player);
			VehicleManager.sendVehicleBatteryCharge(this, (ushort)(quality * 100));
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000BC5A3 File Offset: 0x000BA9A3
		public void stealBattery(Player player)
		{
			if (!this.giveBatteryItem(player))
			{
				return;
			}
			VehicleManager.sendVehicleBatteryCharge(this, 0);
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x000BC5BC File Offset: 0x000BA9BC
		protected bool giveBatteryItem(Player player)
		{
			byte b = (byte)Mathf.FloorToInt((float)this.batteryCharge / 100f);
			if (b == 0)
			{
				return false;
			}
			Item item = new Item(1450, 1, b);
			player.inventory.forceAddItem(item, false);
			return true;
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x0600228F RID: 8847 RVA: 0x000BC600 File Offset: 0x000BAA00
		// (set) Token: 0x06002290 RID: 8848 RVA: 0x000BC650 File Offset: 0x000BAA50
		public byte tireAliveMask
		{
			get
			{
				int num = 0;
				byte b = 0;
				while ((int)b < Mathf.Min(8, this.tires.Length))
				{
					if (this.tires[(int)b].isAlive)
					{
						int num2 = 1 << (int)b;
						num |= num2;
					}
					b += 1;
				}
				return (byte)num;
			}
			set
			{
				byte b = 0;
				while ((int)b < Mathf.Min(8, this.tires.Length))
				{
					if (!(this.tires[(int)b].wheel == null))
					{
						int num = 1 << (int)b;
						this.tires[(int)b].isAlive = (((int)value & num) == num);
					}
					b += 1;
				}
			}
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x000BC6B6 File Offset: 0x000BAAB6
		public void sendTireAliveMaskUpdate()
		{
			VehicleManager.sendVehicleTireAliveMask(this, this.tireAliveMask);
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x000BC6C4 File Offset: 0x000BAAC4
		public void askRepairTire(int index)
		{
			if (index < 0)
			{
				return;
			}
			this.tires[index].askRepair();
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000BC6DB File Offset: 0x000BAADB
		public void askDamageTire(int index)
		{
			if (index < 0)
			{
				return;
			}
			if (this.asset != null && !this.asset.canTiresBeDamaged)
			{
				return;
			}
			this.tires[index].askDamage();
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x000BC710 File Offset: 0x000BAB10
		public int getHitTireIndex(Vector3 position)
		{
			for (int i = 0; i < this.tires.Length; i++)
			{
				WheelCollider wheelCollider = this.tires[i].wheel;
				if (!(wheelCollider == null))
				{
					if ((wheelCollider.transform.position - position).sqrMagnitude < wheelCollider.radius * wheelCollider.radius)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000BC784 File Offset: 0x000BAB84
		public int getClosestAliveTireIndex(Vector3 position, bool isAlive)
		{
			int result = -1;
			float num = 16f;
			for (int i = 0; i < this.tires.Length; i++)
			{
				if (this.tires[i].isAlive == isAlive)
				{
					if (!(this.tires[i].wheel == null))
					{
						float sqrMagnitude = (this.tires[i].wheel.transform.position - position).sqrMagnitude;
						if (sqrMagnitude < num)
						{
							result = i;
							num = sqrMagnitude;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x000BC81C File Offset: 0x000BAC1C
		public void getDisplayFuel(out ushort currentFuel, out ushort maxFuel)
		{
			if (this.usesFuel)
			{
				currentFuel = this.fuel;
				maxFuel = this.asset.fuel;
			}
			else
			{
				if (this.passengers[0].player != null && this.passengers[0].player.player != null)
				{
					currentFuel = (ushort)this.passengers[0].player.player.life.stamina;
				}
				else if (Player.player != null)
				{
					currentFuel = (ushort)Player.player.life.stamina;
				}
				else
				{
					currentFuel = 0;
				}
				maxFuel = 100;
			}
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x000BC8CD File Offset: 0x000BACCD
		public void askBurnFuel(ushort amount)
		{
			if (amount == 0 || this.isExploded)
			{
				return;
			}
			if (amount >= this.fuel)
			{
				this.fuel = 0;
			}
			else
			{
				this.fuel -= amount;
			}
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x000BC908 File Offset: 0x000BAD08
		public void askFillFuel(ushort amount)
		{
			if (amount == 0 || this.isExploded)
			{
				return;
			}
			if (amount >= this.asset.fuel - this.fuel)
			{
				this.fuel = this.asset.fuel;
			}
			else
			{
				this.fuel += amount;
			}
			VehicleManager.sendVehicleFuel(this, this.fuel);
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x000BC970 File Offset: 0x000BAD70
		public void askBurnBattery(ushort amount)
		{
			if (amount == 0 || this.isExploded)
			{
				return;
			}
			if (amount >= this.batteryCharge)
			{
				this.batteryCharge = 0;
			}
			else
			{
				this.batteryCharge -= amount;
			}
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000BC9AC File Offset: 0x000BADAC
		public void askChargeBattery(ushort amount)
		{
			if (amount == 0 || this.isExploded)
			{
				return;
			}
			if (amount >= 10000 - this.batteryCharge)
			{
				this.batteryCharge = 10000;
			}
			else
			{
				this.batteryCharge += amount;
			}
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x000BC9FC File Offset: 0x000BADFC
		public void sendBatteryChargeUpdate()
		{
			VehicleManager.sendVehicleBatteryCharge(this, this.batteryCharge);
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x000BCA0C File Offset: 0x000BAE0C
		public void askDamage(ushort amount, bool canRepair)
		{
			if (amount == 0)
			{
				return;
			}
			if (this.isDead)
			{
				if (!canRepair)
				{
					this.explode();
				}
				return;
			}
			if (amount >= this.health)
			{
				this.health = 0;
			}
			else
			{
				this.health -= amount;
			}
			VehicleManager.sendVehicleHealth(this, this.health);
			if (this.isDead && !canRepair)
			{
				this.explode();
			}
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x000BCA84 File Offset: 0x000BAE84
		public void askRepair(ushort amount)
		{
			if (amount == 0 || this.isExploded)
			{
				return;
			}
			if (amount >= this.asset.health - this.health)
			{
				this.health = this.asset.health;
			}
			else
			{
				this.health += amount;
			}
			VehicleManager.sendVehicleHealth(this, this.health);
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000BCAEC File Offset: 0x000BAEEC
		private void explode()
		{
			Vector3 force = new Vector3(UnityEngine.Random.Range(this.asset.minExplosionForce.x, this.asset.maxExplosionForce.x), UnityEngine.Random.Range(this.asset.minExplosionForce.y, this.asset.maxExplosionForce.y), UnityEngine.Random.Range(this.asset.minExplosionForce.z, this.asset.maxExplosionForce.z));
			base.GetComponent<Rigidbody>().AddForce(force);
			base.GetComponent<Rigidbody>().AddTorque(16f, 0f, 0f);
			this.dropTrunkItems();
			if (this.asset.isExplosive)
			{
				List<EPlayerKill> list;
				DamageTool.explode(base.transform.position, 8f, EDeathCause.VEHICLE, CSteamID.Nil, 200f, 200f, 200f, 0f, 0f, 500f, 2000f, 500f, out list, EExplosionDamageType.CONVENTIONAL, 32f, true, false);
			}
			for (int i = 0; i < this.passengers.Length; i++)
			{
				Passenger passenger = this.passengers[i];
				if (passenger != null)
				{
					SteamPlayer player = passenger.player;
					if (player != null)
					{
						Player player2 = player.player;
						if (!(player2 == null))
						{
							if (!player2.life.isDead)
							{
								if (this.asset.isExplosive)
								{
									EPlayerKill eplayerKill;
									player2.life.askDamage(101, Vector3.up * 101f, EDeathCause.VEHICLE, ELimb.SPINE, CSteamID.Nil, out eplayerKill);
								}
								else
								{
									VehicleManager.forceRemovePlayer(this, player.playerID.steamID);
								}
							}
						}
					}
				}
			}
			int num = UnityEngine.Random.Range(3, 7);
			for (int j = 0; j < num; j++)
			{
				float f = UnityEngine.Random.Range(0f, 6.28318548f);
				ItemManager.dropItem(new Item(67, EItemOrigin.NATURE), base.transform.position + new Vector3(Mathf.Sin(f) * 3f, 1f, Mathf.Cos(f) * 3f), false, Dedicator.isDedicated, true);
			}
			VehicleManager.sendVehicleExploded(this);
			if (this.asset.isExplosive)
			{
				EffectManager.sendEffect(this.asset.explosion, EffectManager.LARGE, base.transform.position);
			}
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x000BCD88 File Offset: 0x000BB188
		public bool checkEnter(CSteamID enemyPlayer, CSteamID enemyGroup)
		{
			return !this.isHooked && ((Provider.isServer && !Dedicator.isDedicated) || !this.isLocked || enemyPlayer == this.lockedOwner || (this.lockedGroup != CSteamID.Nil && enemyGroup == this.lockedGroup));
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x000BCDFC File Offset: 0x000BB1FC
		public override bool checkUseable()
		{
			return !(Player.player == null) && (base.transform.position - Player.player.transform.position).sqrMagnitude <= 100f && !this.isExploded && this.checkEnter(Provider.client, Player.player.quests.groupID);
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x000BCE74 File Offset: 0x000BB274
		public override void use()
		{
			VehicleManager.enterVehicle(this);
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x000BCE7C File Offset: 0x000BB27C
		public override bool checkHighlight(out Color color)
		{
			color = ItemTool.getRarityColorHighlight(this.asset.rarity);
			return true;
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x000BCE98 File Offset: 0x000BB298
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			if (this.checkUseable())
			{
				message = EPlayerMessage.VEHICLE_ENTER;
				text = this.asset.vehicleName;
				color = ItemTool.getRarityColorUI(this.asset.rarity);
			}
			else
			{
				if (Player.player == null || (base.transform.position - Player.player.transform.position).sqrMagnitude > 100f)
				{
					message = EPlayerMessage.BLOCKED;
				}
				else
				{
					message = EPlayerMessage.LOCKED;
				}
				text = string.Empty;
				color = Color.white;
			}
			return !this.isExploded;
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x000BCF44 File Offset: 0x000BB344
		public void updateVehicle()
		{
			this.lastUpdatedPos = base.transform.position;
			if (this.nsb != null)
			{
				this.nsb.updateLastSnapshot(new TransformSnapshotInfo(base.transform.position, base.transform.rotation));
			}
			this.real = base.transform.position;
			this.isRecovering = false;
			this.lastRecover = Time.realtimeSinceStartup;
			this.isFrozen = false;
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000BCFC4 File Offset: 0x000BB3C4
		public void updatePhysics()
		{
			if (this.checkDriver(Provider.client) || (Provider.isServer && !this.isDriven))
			{
				base.GetComponent<Rigidbody>().useGravity = this.usesGravity;
				base.GetComponent<Rigidbody>().isKinematic = this.isKinematic;
				this.isPhysical = true;
				if (!this.isExploded)
				{
					if (this.tires != null)
					{
						for (int i = 0; i < this.tires.Length; i++)
						{
							this.tires[i].isPhysical = true;
						}
					}
					if (this.buoyancy != null)
					{
						this.buoyancy.gameObject.SetActive(true);
					}
				}
			}
			else
			{
				base.GetComponent<Rigidbody>().useGravity = false;
				base.GetComponent<Rigidbody>().isKinematic = true;
				this.isPhysical = false;
				if (this.tires != null)
				{
					for (int j = 0; j < this.tires.Length; j++)
					{
						this.tires[j].isPhysical = false;
					}
				}
				if (this.buoyancy != null)
				{
					this.buoyancy.gameObject.SetActive(false);
				}
			}
			Transform transform = base.transform.FindChild("Cog");
			if (transform)
			{
				base.GetComponent<Rigidbody>().centerOfMass = transform.localPosition;
			}
			else
			{
				base.GetComponent<Rigidbody>().centerOfMass = new Vector3(0f, -0.25f, 0f);
			}
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x000BD148 File Offset: 0x000BB548
		public void updateEngine()
		{
			this.tellTaillights(this.isDriven && this.canTurnOnLights);
			if (!Dedicator.isDedicated)
			{
				foreach (GameObject gameObject in this.sirenGameObjects)
				{
					AudioSource component = gameObject.GetComponent<AudioSource>();
					if (component != null)
					{
						component.enabled = this.isDriven;
					}
				}
			}
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000BD1E0 File Offset: 0x000BB5E0
		public void tellLocked(CSteamID owner, CSteamID group, bool locked)
		{
			this._lockedOwner = owner;
			this._lockedGroup = group;
			this._isLocked = locked;
			if (this.onLockUpdated != null)
			{
				this.onLockUpdated();
			}
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000BD20D File Offset: 0x000BB60D
		public void tellSkin(ushort newSkinID, ushort newMythicID)
		{
			this.skinID = newSkinID;
			this.mythicID = newMythicID;
			this.updateSkin();
			if (this.skinChanged != null)
			{
				this.skinChanged();
			}
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x000BD23C File Offset: 0x000BB63C
		public void updateSkin()
		{
			if (Dedicator.isDedicated)
			{
				return;
			}
			this.skinAsset = (Assets.find(EAssetType.SKIN, this.skinID) as SkinAsset);
			if (this.tempMesh != null)
			{
				HighlighterTool.remesh(base.transform, this.tempMesh, this.tempMesh, false);
			}
			if (this.tempMaterial != null)
			{
				Material material;
				HighlighterTool.rematerialize(base.transform, this.tempMaterial, out material);
			}
			if (this.effectSystems != null)
			{
				for (int i = 0; i < this.effectSystems.Length; i++)
				{
					Transform transform = this.effectSystems[i];
					if (transform != null)
					{
						UnityEngine.Object.Destroy(transform.gameObject);
					}
				}
			}
			if (this.skinAsset != null)
			{
				VehicleAsset vehicleAsset = Assets.find(EAssetType.VEHICLE, this.asset.sharedSkinLookupID) as VehicleAsset;
				if (this.mythicID != 0)
				{
					if (this.effectSlotsRoot == null)
					{
						this.effectSlotsRoot = base.transform.FindChild("Effect_Slots");
						if (this.effectSlotsRoot == null)
						{
							this.effectSlotsRoot = UnityEngine.Object.Instantiate<GameObject>(vehicleAsset.vehicle.transform.FindChild("Effect_Slots").gameObject).transform;
							this.effectSlotsRoot.parent = base.transform;
							this.effectSlotsRoot.name = "Effect_Slots";
							this.effectSlotsRoot.localPosition = Vector3.zero;
							this.effectSlotsRoot.localRotation = Quaternion.identity;
							this.effectSlotsRoot.localScale = Vector3.one;
						}
						this.effectSlots = new Transform[this.effectSlotsRoot.childCount];
						for (int j = 0; j < this.effectSlots.Length; j++)
						{
							this.effectSlots[j] = this.effectSlotsRoot.GetChild(j);
						}
						this.effectSystems = new Transform[this.effectSlots.Length];
					}
					ItemTool.applyEffect(this.effectSlots, this.effectSystems, this.mythicID, EEffectType.AREA);
				}
				if (this.skinAsset.overrideMeshes != null && this.skinAsset.overrideMeshes.Count > 0)
				{
					if (this.tempMesh == null)
					{
						this.tempMesh = new List<Mesh>();
					}
					HighlighterTool.remesh(base.transform, this.skinAsset.overrideMeshes, this.tempMesh, false);
				}
				if (this.skinAsset.primarySkin != null)
				{
					if (this.skinAsset.isPattern)
					{
						Material material2 = UnityEngine.Object.Instantiate<Material>(this.skinAsset.primarySkin);
						material2.SetTexture("_AlbedoBase", vehicleAsset.albedoBase);
						material2.SetTexture("_MetallicBase", vehicleAsset.metallicBase);
						material2.SetTexture("_EmissionBase", vehicleAsset.emissionBase);
						HighlighterTool.rematerialize(base.transform, material2, out this.tempMaterial);
					}
					else
					{
						HighlighterTool.rematerialize(base.transform, this.skinAsset.primarySkin, out this.tempMaterial);
					}
				}
			}
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x000BD548 File Offset: 0x000BB948
		public void tellSirens(bool on)
		{
			this._sirensOn = on;
			if (!Dedicator.isDedicated)
			{
				foreach (GameObject gameObject in this.sirenGameObjects)
				{
					gameObject.SetActive(this.sirensOn);
				}
				if (this.sirenMaterials != null)
				{
					for (int i = 0; i < this.sirenMaterials.Length; i++)
					{
						if (this.sirenMaterials[i] != null)
						{
							this.sirenMaterials[i].SetColor("_EmissionColor", Color.black);
						}
					}
				}
			}
			if (this.onSirensUpdated != null)
			{
				this.onSirensUpdated();
			}
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000BD620 File Offset: 0x000BBA20
		public void tellBlimp(bool on)
		{
			this.isBlimpFloating = on;
			if (this.asset.engine != EEngine.BLIMP)
			{
				return;
			}
			int childCount = this.buoyancy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				this.buoyancy.GetChild(i).GetComponent<Buoyancy>().enabled = this.isBlimpFloating;
			}
			if (this.onBlimpUpdated != null)
			{
				this.onBlimpUpdated();
			}
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x000BD698 File Offset: 0x000BBA98
		public void tellHeadlights(bool on)
		{
			this._headlightsOn = on;
			if (!Dedicator.isDedicated)
			{
				if (this.headlights != null)
				{
					this.headlights.gameObject.SetActive(this.headlightsOn);
				}
				if (this.headlightsMaterial != null)
				{
					this.headlightsMaterial.SetColor("_EmissionColor", (!this.headlightsOn) ? Color.black : this.headlightsMaterial.color);
				}
			}
			if (this.onHeadlightsUpdated != null)
			{
				this.onHeadlightsUpdated();
			}
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000BD734 File Offset: 0x000BBB34
		public void tellTaillights(bool on)
		{
			this._taillightsOn = on;
			if (!Dedicator.isDedicated)
			{
				if (this.taillights != null)
				{
					this.taillights.gameObject.SetActive(this.taillightsOn);
				}
				if (this.taillightsMaterial != null)
				{
					this.taillightsMaterial.SetColor("_EmissionColor", (!this.taillightsOn) ? Color.black : this.taillightsMaterial.color);
				}
				else if (this.taillightMaterials != null)
				{
					for (int i = 0; i < this.taillightMaterials.Length; i++)
					{
						if (this.taillightMaterials[i] != null)
						{
							this.taillightMaterials[i].SetColor("_EmissionColor", (!this.taillightsOn) ? Color.black : this.taillightMaterials[i].color);
						}
					}
				}
			}
			if (this.onTaillightsUpdated != null)
			{
				this.onTaillightsUpdated();
			}
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x000BD840 File Offset: 0x000BBC40
		public void tellHorn()
		{
			this.horned = Time.realtimeSinceStartup;
			if (!Dedicator.isDedicated && this.clipAudioSource != null)
			{
				this.clipAudioSource.pitch = 1f;
				this.clipAudioSource.PlayOneShot(this.asset.horn);
			}
			if (Provider.isServer)
			{
				AlertTool.alert(base.transform.position, 32f);
			}
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x000BD8B8 File Offset: 0x000BBCB8
		public void tellFuel(ushort newFuel)
		{
			this.fuel = newFuel;
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x000BD8C1 File Offset: 0x000BBCC1
		public void tellBatteryCharge(ushort newBatteryCharge)
		{
			this.batteryCharge = newBatteryCharge;
			if (this.batteryCharge == 0)
			{
				this.isEngineOn = false;
			}
			if (this.batteryChanged != null)
			{
				this.batteryChanged();
			}
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x000BD8F4 File Offset: 0x000BBCF4
		public void tellExploded()
		{
			this.clearHooked();
			this.isExploded = true;
			this._lastExploded = Time.realtimeSinceStartup;
			if (this.sirensOn)
			{
				this.tellSirens(false);
			}
			if (this.isBlimpFloating)
			{
				this.tellBlimp(false);
			}
			if (this.headlightsOn)
			{
				this.tellHeadlights(false);
			}
			if (this.tires != null)
			{
				for (int i = 0; i < this.tires.Length; i++)
				{
					this.tires[i].isPhysical = false;
				}
			}
			if (this.buoyancy != null)
			{
				this.buoyancy.gameObject.SetActive(false);
			}
			if (!Dedicator.isDedicated)
			{
				if (this.asset.isExplosive)
				{
					HighlighterTool.color(base.transform, new Color(0.25f, 0.25f, 0.25f));
				}
				this.updateFires();
				if (this.tires != null)
				{
					for (int j = 0; j < this.tires.Length; j++)
					{
						if (!(this.tires[j].model == null))
						{
							this.tires[j].model.transform.parent = Level.effects;
							this.tires[j].model.GetComponent<Collider>().enabled = true;
							Rigidbody orAddComponent = this.tires[j].model.gameObject.getOrAddComponent<Rigidbody>();
							orAddComponent.interpolation = RigidbodyInterpolation.Interpolate;
							orAddComponent.collisionDetectionMode = CollisionDetectionMode.Discrete;
							orAddComponent.drag = 0.5f;
							orAddComponent.angularDrag = 0.1f;
							UnityEngine.Object.Destroy(this.tires[j].model.gameObject, 8f);
							if (j % 2 == 0)
							{
								orAddComponent.AddForce(-this.tires[j].model.right * 512f + Vector3.up * 128f);
							}
							else
							{
								orAddComponent.AddForce(this.tires[j].model.right * 512f + Vector3.up * 128f);
							}
						}
					}
				}
				if (this.rotors != null)
				{
					for (int k = 0; k < this.rotors.Length; k++)
					{
						UnityEngine.Object.Destroy(this.rotors[k].prop.gameObject);
					}
				}
				if (this.exhausts != null)
				{
					for (int l = 0; l < this.exhausts.Length; l++)
					{
						this.exhausts[l].emission.rateOverTime = 0f;
					}
				}
				if (this.asset.isExplosive)
				{
					if (this.front != null)
					{
						HighlighterTool.color(this.front, new Color(0.25f, 0.25f, 0.25f));
					}
					if (this.turrets != null)
					{
						for (int m = 0; m < this.turrets.Length; m++)
						{
							HighlighterTool.color(this.turrets[m].turretYaw, new Color(0.25f, 0.25f, 0.25f));
							HighlighterTool.color(this.turrets[m].turretPitch, new Color(0.25f, 0.25f, 0.25f));
						}
					}
				}
			}
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x000BDC68 File Offset: 0x000BC068
		public void updateFires()
		{
			if (!Dedicator.isDedicated)
			{
				if (this.fire != null)
				{
					this.fire.gameObject.SetActive((this.isExploded || this.isDead) && !this.isUnderwater);
				}
				if (this.smoke_0 != null)
				{
					this.smoke_0.gameObject.SetActive((this.isExploded || this.health < InteractableVehicle.HEALTH_0) && !this.isUnderwater);
				}
				if (this.smoke_1 != null)
				{
					this.smoke_1.gameObject.SetActive((this.isExploded || this.health < InteractableVehicle.HEALTH_1) && !this.isUnderwater);
				}
			}
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000BDD52 File Offset: 0x000BC152
		public void tellHealth(ushort newHealth)
		{
			this.health = newHealth;
			if (this.isDead)
			{
				this._lastDead = Time.realtimeSinceStartup;
			}
			this.updateFires();
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x000BDD78 File Offset: 0x000BC178
		public void tellRecov(Vector3 newPosition, int newRecov)
		{
			this.lastTick = Time.realtimeSinceStartup;
			base.GetComponent<Rigidbody>().MovePosition(newPosition);
			this.isFrozen = true;
			base.GetComponent<Rigidbody>().useGravity = false;
			base.GetComponent<Rigidbody>().isKinematic = true;
			if (this.passengers[0] != null && this.passengers[0].player != null && this.passengers[0].player.player != null && this.passengers[0].player.player.input != null)
			{
				this.passengers[0].player.player.input.recov = newRecov;
			}
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x000BDE38 File Offset: 0x000BC238
		public void tellState(Vector3 newPosition, byte newAngle_X, byte newAngle_Y, byte newAngle_Z, byte newSpeed, byte newPhysicsSpeed, byte newTurn)
		{
			if (this.isDriver)
			{
				return;
			}
			this.lastTick = Time.realtimeSinceStartup;
			this.lastUpdatedPos = newPosition;
			if (this.nsb != null)
			{
				this.nsb.addNewSnapshot(new TransformSnapshotInfo(newPosition, Quaternion.Euler(MeasurementTool.byteToAngle2(newAngle_X), MeasurementTool.byteToAngle2(newAngle_Y), MeasurementTool.byteToAngle2(newAngle_Z))));
			}
			if (this.asset.engine == EEngine.TRAIN)
			{
				this.roadPosition = newPosition.x;
			}
			this._speed = (float)(newSpeed - 128);
			this._physicsSpeed = (float)(newPhysicsSpeed - 128);
			this._turn = (int)(newTurn - 1);
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x000BDEE2 File Offset: 0x000BC2E2
		public bool checkDriver(CSteamID steamID)
		{
			return this.isDriven && this.passengers[0].player.playerID.steamID == steamID;
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000BDF10 File Offset: 0x000BC310
		protected void grantTrunkAccess(Player player)
		{
			if (Provider.isServer && this.trunkItems != null && this.trunkItems.height > 0)
			{
				player.inventory.isStoring = true;
				player.inventory.isStorageTrunk = true;
				player.inventory.storage = null;
				player.inventory.updateItems(PlayerInventory.STORAGE, this.trunkItems);
				player.inventory.sendStorage();
			}
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000BDF88 File Offset: 0x000BC388
		protected void revokeTrunkAccess(Player player)
		{
			if (Provider.isServer && this.trunkItems != null && this.trunkItems.height > 0)
			{
				player.inventory.isStoring = false;
				player.inventory.isStorageTrunk = false;
				player.inventory.updateItems(PlayerInventory.STORAGE, null);
				player.inventory.sendStorage();
			}
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000BDFF0 File Offset: 0x000BC3F0
		protected void dropTrunkItems()
		{
			if (Provider.isServer && this.trunkItems != null)
			{
				for (byte b = 0; b < this.trunkItems.getItemCount(); b += 1)
				{
					ItemJar item = this.trunkItems.getItem(b);
					ItemManager.dropItem(item.item, base.transform.position, false, true, true);
				}
				this.trunkItems.clear();
				this.trunkItems = null;
				if (this.passengers[0].player != null && this.passengers[0].player.player != null)
				{
					this.revokeTrunkAccess(this.passengers[0].player.player);
				}
			}
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x000BE0B0 File Offset: 0x000BC4B0
		public void addPlayer(byte seat, CSteamID steamID)
		{
			SteamPlayer steamPlayer = PlayerTool.getSteamPlayer(steamID);
			if (steamPlayer != null)
			{
				this.passengers[(int)seat].player = steamPlayer;
				if (steamPlayer.player != null)
				{
					steamPlayer.player.movement.setVehicle(this, seat, this.passengers[(int)seat].seat, Vector3.zero, 0, false);
					if (this.passengers[(int)seat].turret != null)
					{
						steamPlayer.player.equipment.turretEquipClient();
						if (Provider.isServer)
						{
							steamPlayer.player.equipment.turretEquipServer(this.passengers[(int)seat].turret.itemID, this.passengers[(int)seat].state);
						}
					}
				}
				this.updatePhysics();
				if (seat == 0)
				{
					this.grantTrunkAccess(steamPlayer.player);
				}
			}
			if (seat == 0)
			{
				this.isEngineOn = ((!this.usesBattery || this.hasBattery) && !this.isUnderwater);
			}
			this.updateEngine();
			if (seat == 0 && (this.asset.isStaminaPowered || this.fuel > 0) && !Dedicator.isDedicated && !this.isUnderwater)
			{
				if (this.clipAudioSource != null && this.isEngineOn)
				{
					this.clipAudioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
					this.clipAudioSource.PlayOneShot(this.asset.ignition);
				}
				if (this.engineAudioSource != null)
				{
					this.engineAudioSource.pitch = this.asset.pitchIdle;
				}
				if (this.engineAdditiveAudioSource != null)
				{
					this.engineAdditiveAudioSource.pitch = this.asset.pitchIdle;
				}
			}
			if (this.onPassengersUpdated != null)
			{
				this.onPassengersUpdated();
			}
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x000BE2A0 File Offset: 0x000BC6A0
		public void removePlayer(byte seat, Vector3 point, byte angle, bool forceUpdate)
		{
			if ((int)seat < this.passengers.Length)
			{
				SteamPlayer player = this.passengers[(int)seat].player;
				if (player.player != null)
				{
					if (this.passengers[(int)seat].turret != null)
					{
						player.player.equipment.turretDequipClient();
						if (Provider.isServer)
						{
							player.player.equipment.turretDequipServer();
						}
					}
					player.player.movement.setVehicle(null, 0, LevelPlayers.models, point, angle, forceUpdate);
				}
				this.passengers[(int)seat].player = null;
				this.updatePhysics();
				if (Provider.isServer)
				{
					VehicleManager.sendVehicleFuel(this, this.fuel);
					VehicleManager.sendVehicleBatteryCharge(this, this.batteryCharge);
				}
				if (seat == 0)
				{
					this.revokeTrunkAccess(player.player);
				}
			}
			if (seat == 0)
			{
				this.isEngineOn = false;
			}
			this.updateEngine();
			if (seat == 0)
			{
				this.altSpeedInput = 0f;
				this.altSpeedOutput = 0f;
				if (!Dedicator.isDedicated)
				{
					if (this.engineAudioSource != null)
					{
						this.engineAudioSource.volume = 0f;
					}
					if (this.engineAdditiveAudioSource != null)
					{
						this.engineAdditiveAudioSource.volume = 0f;
					}
					if (this.windZone != null)
					{
						this.windZone.windMain = 0f;
					}
				}
				if (this.tires != null)
				{
					for (int i = 0; i < this.tires.Length; i++)
					{
						this.tires[i].reset();
					}
				}
			}
			if (this.onPassengersUpdated != null)
			{
				this.onPassengersUpdated();
			}
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x000BE45C File Offset: 0x000BC85C
		public void swapPlayer(byte fromSeat, byte toSeat)
		{
			if ((int)fromSeat < this.passengers.Length && (int)toSeat < this.passengers.Length)
			{
				SteamPlayer player = this.passengers[(int)fromSeat].player;
				if (player.player != null)
				{
					if (this.passengers[(int)fromSeat].turret != null)
					{
						player.player.equipment.turretDequipClient();
						if (Provider.isServer)
						{
							player.player.equipment.turretDequipServer();
						}
					}
					player.player.movement.setVehicle(this, toSeat, this.passengers[(int)toSeat].seat, Vector3.zero, 0, false);
					if (this.passengers[(int)toSeat].turret != null)
					{
						player.player.equipment.turretEquipClient();
						if (Provider.isServer)
						{
							player.player.equipment.turretEquipServer(this.passengers[(int)toSeat].turret.itemID, this.passengers[(int)toSeat].state);
						}
					}
				}
				this.passengers[(int)fromSeat].player = null;
				this.passengers[(int)toSeat].player = player;
				this.updatePhysics();
				if (Provider.isServer)
				{
					VehicleManager.sendVehicleFuel(this, this.fuel);
					VehicleManager.sendVehicleBatteryCharge(this, this.batteryCharge);
				}
				if (fromSeat == 0)
				{
					this.revokeTrunkAccess(player.player);
				}
				if (toSeat == 0)
				{
					this.grantTrunkAccess(player.player);
				}
			}
			if (toSeat == 0)
			{
				this.isEngineOn = ((!this.usesBattery || this.hasBattery) && !this.isUnderwater);
			}
			if (fromSeat == 0)
			{
				this.isEngineOn = false;
			}
			this.updateEngine();
			if (fromSeat == 0)
			{
				this.altSpeedInput = 0f;
				this.altSpeedOutput = 0f;
				if (!Dedicator.isDedicated)
				{
					if (this.engineAudioSource != null)
					{
						this.engineAudioSource.volume = 0f;
					}
					if (this.engineAdditiveAudioSource != null)
					{
						this.engineAdditiveAudioSource.volume = 0f;
					}
					if (this.windZone != null)
					{
						this.windZone.windMain = 0f;
					}
				}
				if (this.tires != null)
				{
					for (int i = 0; i < this.tires.Length; i++)
					{
						this.tires[i].reset();
					}
				}
			}
			if (this.onPassengersUpdated != null)
			{
				this.onPassengersUpdated();
			}
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x000BE6DC File Offset: 0x000BCADC
		public bool tryAddPlayer(out byte seat, Player player)
		{
			seat = byte.MaxValue;
			if (this.isExploded)
			{
				return false;
			}
			if (!this.isExitable)
			{
				return false;
			}
			byte b = (player.animator.gesture != EPlayerGesture.ARREST_START) ? 0 : 1;
			while ((int)b < this.passengers.Length)
			{
				if (this.passengers[(int)b] != null && this.passengers[(int)b].player == null)
				{
					seat = b;
					return true;
				}
				b += 1;
			}
			return false;
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x000BE764 File Offset: 0x000BCB64
		public void forceRemoveAllPlayers()
		{
			for (int i = 0; i < this.passengers.Length; i++)
			{
				Passenger passenger = this.passengers[i];
				if (passenger != null)
				{
					SteamPlayer player = passenger.player;
					if (player != null)
					{
						Player player2 = player.player;
						if (!(player2 == null))
						{
							if (!player2.life.isDead)
							{
								VehicleManager.forceRemovePlayer(this, player.playerID.steamID);
							}
						}
					}
				}
			}
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x000BE7F0 File Offset: 0x000BCBF0
		public bool forceRemovePlayer(out byte seat, CSteamID player, out Vector3 point, out byte angle)
		{
			seat = byte.MaxValue;
			point = Vector3.zero;
			angle = 0;
			byte b = 0;
			while ((int)b < this.passengers.Length)
			{
				if (this.passengers[(int)b] != null && this.passengers[(int)b].player != null && this.passengers[(int)b].player.playerID.steamID == player)
				{
					seat = b;
					this.getExit(seat, out point, out angle);
					return true;
				}
				b += 1;
			}
			return false;
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x000BE880 File Offset: 0x000BCC80
		public bool tryRemovePlayer(out byte seat, CSteamID player, out Vector3 point, out byte angle)
		{
			seat = byte.MaxValue;
			point = Vector3.zero;
			angle = 0;
			byte b = 0;
			while ((int)b < this.passengers.Length)
			{
				if (this.passengers[(int)b] != null && this.passengers[(int)b].player != null && this.passengers[(int)b].player.playerID.steamID == player)
				{
					seat = b;
					return this.getExit(seat, out point, out angle);
				}
				b += 1;
			}
			return false;
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x000BE910 File Offset: 0x000BCD10
		public bool trySwapPlayer(Player player, byte toSeat, out byte fromSeat)
		{
			fromSeat = byte.MaxValue;
			if ((int)toSeat >= this.passengers.Length)
			{
				return false;
			}
			if (player.animator.gesture == EPlayerGesture.ARREST_START && toSeat < 1)
			{
				return false;
			}
			byte b = 0;
			while ((int)b < this.passengers.Length)
			{
				if (this.passengers[(int)b] != null && this.passengers[(int)b].player != null && this.passengers[(int)b].player.player == player)
				{
					if (toSeat != b)
					{
						fromSeat = b;
						return this.passengers[(int)toSeat].player == null;
					}
					return false;
				}
				else
				{
					b += 1;
				}
			}
			return false;
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060022C2 RID: 8898 RVA: 0x000BE9C8 File Offset: 0x000BCDC8
		public bool isExitable
		{
			get
			{
				Vector3 vector;
				byte b;
				return this.getExit(0, out vector, out b);
			}
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x000BE9E0 File Offset: 0x000BCDE0
		protected bool isExitSafe(Vector3 point, Vector3 direction, float distance)
		{
			RaycastHit raycastHit;
			PhysicsUtility.raycast(new Ray(point, direction), out raycastHit, distance, RayMasks.BLOCK_EXIT, QueryTriggerInteraction.UseGlobal);
			return raycastHit.transform == null || (raycastHit.transform.IsChildOf(base.transform) && this.isExitSafe(raycastHit.point + direction * 0.01f, direction, distance - raycastHit.distance - 0.01f));
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000BEA60 File Offset: 0x000BCE60
		protected Vector3 getExitGroundPoint(Vector3 exitPoint, float length = 3f)
		{
			RaycastHit raycastHit;
			PhysicsUtility.raycast(new Ray(exitPoint, Vector3.down), out raycastHit, length, RayMasks.BLOCK_EXIT, QueryTriggerInteraction.UseGlobal);
			if (raycastHit.transform != null)
			{
				exitPoint = raycastHit.point;
			}
			return exitPoint + new Vector3(0f, 0.5f, 0f);
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x000BEABC File Offset: 0x000BCEBC
		protected bool getExitSidePoint(float side, ref Vector3 point)
		{
			float num = PlayerStance.RADIUS + 0.1f;
			float num2 = this.asset.exit + Mathf.Abs(this.speed) * 0.1f + num;
			if (this.isExitSafe(this.center.position + this.center.up, this.center.right * side, num2))
			{
				point = this.getExitGroundPoint(this.center.position + this.center.up + this.center.right * side * (num2 - num), 3f);
				return true;
			}
			side = -side;
			if (this.isExitSafe(this.center.position + this.center.up, this.center.right * side, num2))
			{
				point = this.getExitGroundPoint(this.center.position + this.center.up + this.center.right * side * (num2 - num), 3f);
				return true;
			}
			return false;
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x000BEC04 File Offset: 0x000BD004
		public bool getExit(byte seat, out Vector3 point, out byte angle)
		{
			point = this.center.position;
			angle = MeasurementTool.angleToByte(this.center.rotation.eulerAngles.y);
			if (seat % 2 == 0)
			{
				if (this.getExitSidePoint(-1f, ref point))
				{
					return true;
				}
			}
			else if (this.getExitSidePoint(1f, ref point))
			{
				return true;
			}
			if (this.isExitSafe(this.center.position + this.center.up, Vector3.up, 3f))
			{
				point = this.center.position + this.center.up;
				return true;
			}
			point = this.getExitGroundPoint(new Vector3(this.center.position.x, Level.HEIGHT, this.center.position.z), 1024f);
			return true;
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000BED0C File Offset: 0x000BD10C
		public void simulate(uint simulation, int recov, bool inputStamina, Vector3 point, Quaternion angle, float newSpeed, float newPhysicsSpeed, int newTurn, float delta)
		{
			if (this.asset.useStaminaBoost)
			{
				bool flag = this.passengers[0].player != null && this.passengers[0].player.player != null && this.passengers[0].player.player.life.stamina > 0;
				if (inputStamina && flag)
				{
					this.isBoosting = true;
				}
				else
				{
					this.isBoosting = false;
				}
			}
			else
			{
				this.isBoosting = false;
			}
			if (this.isRecovering)
			{
				if (recov < this.passengers[0].player.player.input.recov)
				{
					if (Time.realtimeSinceStartup - this.lastRecover > 5f)
					{
						this.lastRecover = Time.realtimeSinceStartup;
						VehicleManager.sendVehicleRecov(this, this.real, this.passengers[0].player.player.input.recov);
					}
					return;
				}
				this.isRecovering = false;
				this.isFrozen = false;
			}
			bool flag2 = Dedicator.serverVisibility == ESteamServerVisibility.LAN || PlayerMovement.forceTrustClient;
			if (!flag2)
			{
				if (this.asset.engine == EEngine.CAR)
				{
					if (Mathf.Pow(point.x - this.real.x, 2f) + Mathf.Pow(point.z - this.real.z, 2f) > ((!this.usesFuel || this.fuel != 0) ? this.asset.sqrDelta : 0.5f))
					{
						this.isRecovering = true;
						this.lastRecover = Time.realtimeSinceStartup;
						this.passengers[0].player.player.input.recov++;
						VehicleManager.sendVehicleRecov(this, this.real, this.passengers[0].player.player.input.recov);
						return;
					}
					if (point.y - this.real.y > 1f)
					{
						this.isRecovering = true;
						this.lastRecover = Time.realtimeSinceStartup;
						this.passengers[0].player.player.input.recov++;
						VehicleManager.sendVehicleRecov(this, this.real, this.passengers[0].player.player.input.recov);
						return;
					}
				}
				else if (this.asset.engine == EEngine.BOAT)
				{
					if (Mathf.Pow(point.x - this.real.x, 2f) + Mathf.Pow(point.z - this.real.z, 2f) > ((!WaterUtility.isPointUnderwater(point + new Vector3(0f, -4f, 0f))) ? 0.5f : this.asset.sqrDelta))
					{
						this.isRecovering = true;
						this.lastRecover = Time.realtimeSinceStartup;
						this.passengers[0].player.player.input.recov++;
						VehicleManager.sendVehicleRecov(this, this.real, this.passengers[0].player.player.input.recov);
						return;
					}
					if (point.y - this.real.y > 0.25f)
					{
						this.isRecovering = true;
						this.lastRecover = Time.realtimeSinceStartup;
						this.passengers[0].player.player.input.recov++;
						VehicleManager.sendVehicleRecov(this, this.real, this.passengers[0].player.player.input.recov);
						return;
					}
				}
				else if (this.asset.engine != EEngine.TRAIN)
				{
					if (Mathf.Pow(point.x - this.real.x, 2f) + Mathf.Pow(point.z - this.real.z, 2f) > this.asset.sqrDelta)
					{
						this.isRecovering = true;
						this.lastRecover = Time.realtimeSinceStartup;
						this.passengers[0].player.player.input.recov++;
						VehicleManager.sendVehicleRecov(this, this.real, this.passengers[0].player.player.input.recov);
						return;
					}
				}
			}
			if (this.usesFuel)
			{
				if (this.asset.engine == EEngine.CAR)
				{
					if (simulation - this.lastBurnFuel > 5u)
					{
						this.lastBurnFuel = simulation;
						this.askBurnFuel(1);
					}
				}
				else if (simulation - this.lastBurnFuel > 2u)
				{
					this.lastBurnFuel = simulation;
					this.askBurnFuel(1);
				}
			}
			this._speed = newSpeed;
			this._physicsSpeed = newSpeed;
			this._turn = newTurn;
			this.real = point;
			Vector3 pos;
			if (this.asset.engine == EEngine.TRAIN)
			{
				this.roadPosition = this.clampRoadPosition(point.x);
				this.teleportTrain();
				pos = new Vector3(this.roadPosition, 0f, 0f);
			}
			else
			{
				base.GetComponent<Rigidbody>().MovePosition(point);
				base.GetComponent<Rigidbody>().MoveRotation(angle);
				pos = point;
			}
			if (this.updates != null && (Mathf.Abs(this.lastUpdatedPos.x - this.real.x) > Provider.UPDATE_DISTANCE || Mathf.Abs(this.lastUpdatedPos.y - this.real.y) > Provider.UPDATE_DISTANCE || Mathf.Abs(this.lastUpdatedPos.z - this.real.z) > Provider.UPDATE_DISTANCE))
			{
				this.lastUpdatedPos = this.real;
				this.updates.Add(new VehicleStateUpdate(pos, angle));
			}
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x000BF348 File Offset: 0x000BD748
		public void clearHooked()
		{
			foreach (HookInfo hookInfo in this.hooked)
			{
				if (!(hookInfo.vehicle == null))
				{
					hookInfo.vehicle.isHooked = false;
				}
			}
			this.hooked.Clear();
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x000BF3CC File Offset: 0x000BD7CC
		public void useHook()
		{
			if (this.hooked.Count > 0)
			{
				this.clearHooked();
			}
			else
			{
				int num = Physics.OverlapSphereNonAlloc(this.hook.position, 3f, InteractableVehicle.grab, RayMasks.VEHICLE);
				for (int i = 0; i < num; i++)
				{
					InteractableVehicle vehicle = DamageTool.getVehicle(InteractableVehicle.grab[i].transform);
					if (!(vehicle == null) && !(vehicle == this) && vehicle.isEmpty && !vehicle.isHooked && vehicle.asset.engine != EEngine.TRAIN)
					{
						HookInfo hookInfo = new HookInfo();
						hookInfo.target = vehicle.transform;
						hookInfo.vehicle = vehicle;
						hookInfo.deltaPosition = this.hook.InverseTransformPoint(vehicle.transform.position);
						hookInfo.deltaRotation = Quaternion.FromToRotation(this.hook.forward, vehicle.transform.forward);
						this.hooked.Add(hookInfo);
						vehicle.isHooked = true;
					}
				}
			}
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x000BF4E8 File Offset: 0x000BD8E8
		public void simulate(uint simulation, int recov, int input_x, int input_y, float look_x, float look_y, bool inputBrake, bool inputStamina, float delta)
		{
			float num = (float)input_y;
			if (this.asset.useStaminaBoost)
			{
				bool flag = this.passengers[0].player != null && this.passengers[0].player.player != null && this.passengers[0].player.player.life.stamina > 0;
				if (inputStamina && flag)
				{
					this.isBoosting = true;
				}
				else
				{
					this.isBoosting = false;
					num *= this.asset.staminaBoost;
				}
			}
			else
			{
				this.isBoosting = false;
			}
			if (this.isFrozen)
			{
				this.isFrozen = false;
				base.GetComponent<Rigidbody>().useGravity = this.usesGravity;
				base.GetComponent<Rigidbody>().isKinematic = this.isKinematic;
				return;
			}
			if ((this.usesFuel && this.fuel == 0) || this.isUnderwater || this.isDead || !this.isEngineOn)
			{
				num = 0f;
			}
			this._factor = Mathf.InverseLerp(0f, (this.speed >= 0f) ? this.asset.speedMax : this.asset.speedMin, this.speed);
			bool flag2 = false;
			if (this.tires != null)
			{
				for (int i = 0; i < this.tires.Length; i++)
				{
					this.tires[i].simulate((float)input_x, num, inputBrake, delta);
					if (this.tires[i].isGrounded)
					{
						flag2 = true;
					}
				}
			}
			switch (this.asset.engine)
			{
			case EEngine.CAR:
				if (flag2)
				{
					base.GetComponent<Rigidbody>().AddForce(-base.transform.up * this.factor * 40f);
				}
				if (this.buoyancy != null)
				{
					float num2 = Mathf.Lerp(this.asset.steerMax, this.asset.steerMin, this.factor);
					this.speedTraction = Mathf.Lerp(this.speedTraction, (float)((!WaterUtility.isPointUnderwater(base.transform.position + new Vector3(0f, -1f, 0f))) ? 0 : 1), 4f * Time.deltaTime);
					if (num > 0f)
					{
						this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, this.asset.speedMax, delta / 4f);
					}
					else if (num < 0f)
					{
						this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, this.asset.speedMin, delta / 4f);
					}
					else
					{
						this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, 0f, delta / 8f);
					}
					this.altSpeedOutput = this.altSpeedInput * this.speedTraction;
					Vector3 forward = base.transform.forward;
					forward.y = 0f;
					base.GetComponent<Rigidbody>().AddForce(forward.normalized * this.altSpeedOutput * 2f * this.speedTraction);
					base.GetComponent<Rigidbody>().AddRelativeTorque((float)input_y * -2.5f * this.speedTraction, (float)input_x * num2 / 8f * this.speedTraction, (float)input_x * -2.5f * this.speedTraction);
				}
				break;
			case EEngine.PLANE:
			{
				float num3 = Mathf.Lerp(this.asset.airSteerMax, this.asset.airSteerMin, this.factor);
				if (num > 0f)
				{
					this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, this.asset.speedMax, delta);
				}
				else if (num < 0f)
				{
					this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, 0f, delta / 8f);
				}
				else
				{
					this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, 0f, delta / 16f);
				}
				this.altSpeedOutput = this.altSpeedInput;
				base.GetComponent<Rigidbody>().AddForce(base.transform.forward * this.altSpeedOutput * 2f);
				base.GetComponent<Rigidbody>().AddForce(Mathf.Lerp(0f, 1f, base.transform.InverseTransformDirection(base.GetComponent<Rigidbody>().velocity).z / this.asset.speedMax) * this.asset.lift * -Physics.gravity);
				if (this.tires == null || this.tires.Length == 0 || (!this.tires[0].isGrounded && !this.tires[1].isGrounded))
				{
					base.GetComponent<Rigidbody>().AddRelativeTorque(Mathf.Clamp(look_y, -this.asset.airTurnResponsiveness, this.asset.airTurnResponsiveness) * num3, (float)input_x * this.asset.airTurnResponsiveness * num3 / 4f, Mathf.Clamp(look_x, -this.asset.airTurnResponsiveness, this.asset.airTurnResponsiveness) * -num3 / 2f);
				}
				if ((this.tires == null || this.tires.Length == 0) && num < 0f)
				{
					base.GetComponent<Rigidbody>().AddForce(base.transform.forward * this.asset.speedMin * 4f);
				}
				break;
			}
			case EEngine.HELICOPTER:
			{
				float num4 = Mathf.Lerp(this.asset.steerMax, this.asset.steerMin, this.factor);
				if (num > 0f)
				{
					this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, this.asset.speedMax, delta / 4f);
				}
				else if (num < 0f)
				{
					this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, 0f, delta / 8f);
				}
				else
				{
					this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, 0f, delta / 16f);
				}
				this.altSpeedOutput = this.altSpeedInput;
				base.GetComponent<Rigidbody>().AddForce(base.transform.up * this.altSpeedOutput * 3f);
				base.GetComponent<Rigidbody>().AddRelativeTorque(Mathf.Clamp(look_y, -2f, 2f) * num4, (float)input_x * num4 / 2f, Mathf.Clamp(look_x, -2f, 2f) * -num4 / 4f);
				break;
			}
			case EEngine.BLIMP:
			{
				float num5 = Mathf.Lerp(this.asset.steerMax, this.asset.steerMin, this.factor);
				if (num > 0f)
				{
					this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, this.asset.speedMax, delta / 4f);
				}
				else if (num < 0f)
				{
					this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, this.asset.speedMin, delta / 4f);
				}
				else
				{
					this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, 0f, delta / 8f);
				}
				this.altSpeedOutput = this.altSpeedInput;
				base.GetComponent<Rigidbody>().AddForce(base.transform.forward * this.altSpeedOutput * 2f);
				if (!this.isBlimpFloating)
				{
					base.GetComponent<Rigidbody>().AddForce(-Physics.gravity * 0.5f);
				}
				base.GetComponent<Rigidbody>().AddRelativeTorque(Mathf.Clamp(look_y, -this.asset.airTurnResponsiveness, this.asset.airTurnResponsiveness) * num5 / 4f, (float)input_x * this.asset.airTurnResponsiveness * num5 * 2f, Mathf.Clamp(look_x, -this.asset.airTurnResponsiveness, this.asset.airTurnResponsiveness) * -num5 / 4f);
				break;
			}
			case EEngine.BOAT:
			{
				float num6 = Mathf.Lerp(this.asset.steerMax, this.asset.steerMin, this.factor);
				this.speedTraction = Mathf.Lerp(this.speedTraction, (float)((!WaterUtility.isPointUnderwater(base.transform.position + new Vector3(0f, -1f, 0f))) ? 0 : 1), 4f * Time.deltaTime);
				if (num > 0f)
				{
					this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, this.asset.speedMax, delta / 4f);
				}
				else if (num < 0f)
				{
					this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, this.asset.speedMin, delta / 4f);
				}
				else
				{
					this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, 0f, delta / 8f);
				}
				this.altSpeedOutput = this.altSpeedInput * this.speedTraction;
				Vector3 forward2 = base.transform.forward;
				forward2.y = 0f;
				base.GetComponent<Rigidbody>().AddForce(forward2.normalized * this.altSpeedOutput * 4f * this.speedTraction);
				if (this.tires == null || this.tires.Length == 0 || (!this.tires[0].isGrounded && !this.tires[1].isGrounded))
				{
					base.GetComponent<Rigidbody>().AddRelativeTorque(num * -10f * this.speedTraction, (float)input_x * num6 / 2f * this.speedTraction, (float)input_x * -5f * this.speedTraction);
				}
				break;
			}
			case EEngine.TRAIN:
				if (num > 0f)
				{
					this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, this.asset.speedMax, delta / 8f);
				}
				else if (num < 0f)
				{
					this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, this.asset.speedMin, delta / 8f);
				}
				else
				{
					this.altSpeedInput = Mathf.Lerp(this.altSpeedInput, 0f, delta / 8f);
				}
				this.altSpeedOutput = this.altSpeedInput;
				break;
			}
			if (this.asset.engine == EEngine.CAR)
			{
				this._speed = base.transform.InverseTransformDirection(base.GetComponent<Rigidbody>().velocity).z;
				this._physicsSpeed = this._speed;
			}
			else if (this.asset.engine == EEngine.TRAIN)
			{
				this._speed = this.altSpeedOutput;
				this._physicsSpeed = this.altSpeedOutput;
			}
			else
			{
				this._speed = this.altSpeedOutput;
				this._physicsSpeed = base.transform.InverseTransformDirection(base.GetComponent<Rigidbody>().velocity).z;
			}
			this._turn = input_x;
			if (this.usesFuel)
			{
				if (this.asset.engine == EEngine.CAR)
				{
					if (simulation - this.lastBurnFuel > 5u)
					{
						this.lastBurnFuel = simulation;
						this.askBurnFuel(1);
					}
				}
				else if (simulation - this.lastBurnFuel > 2u)
				{
					this.lastBurnFuel = simulation;
					this.askBurnFuel(1);
				}
			}
			this.lastUpdatedPos = base.transform.position;
			if (this.nsb != null)
			{
				this.nsb.updateLastSnapshot(new TransformSnapshotInfo(base.transform.position, base.transform.rotation));
			}
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000C0138 File Offset: 0x000BE538
		private void moveTrain(Vector3 frontPosition, Vector3 frontNormal, Vector3 frontDirection, Vector3 backPosition, Vector3 backNormal, Vector3 backDirection, TrainCar car)
		{
			Vector3 a = (frontPosition + backPosition) / 2f;
			Vector3 vector = Vector3.Lerp(backNormal, frontNormal, 0.5f);
			Vector3 normalized = (frontPosition - backPosition).normalized;
			Quaternion rotation = Quaternion.LookRotation(frontDirection, frontNormal);
			Quaternion rotation2 = Quaternion.LookRotation(backDirection, backNormal);
			Quaternion quaternion = Quaternion.LookRotation(normalized, vector);
			car.root.GetComponent<Rigidbody>().MovePosition(a + vector * this.asset.trainTrackOffset);
			car.root.GetComponent<Rigidbody>().MoveRotation(quaternion);
			car.root.position = a + vector * this.asset.trainTrackOffset;
			car.root.rotation = quaternion;
			car.trackFront.position = a + normalized * this.asset.trainWheelOffset;
			car.trackFront.rotation = rotation;
			car.trackBack.position = a - normalized * this.asset.trainWheelOffset;
			car.trackBack.rotation = rotation2;
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000C0264 File Offset: 0x000BE664
		private void teleportTrain()
		{
			foreach (TrainCar trainCar in this.trainCars)
			{
				Vector3 frontPosition;
				Vector3 frontNormal;
				Vector3 frontDirection;
				this.road.getTrackData(this.clampRoadPosition(this.roadPosition + trainCar.trackPositionOffset + this.asset.trainWheelOffset), out frontPosition, out frontNormal, out frontDirection);
				Vector3 backPosition;
				Vector3 backNormal;
				Vector3 backDirection;
				this.road.getTrackData(this.clampRoadPosition(this.roadPosition + trainCar.trackPositionOffset - this.asset.trainWheelOffset), out backPosition, out backNormal, out backDirection);
				this.moveTrain(frontPosition, frontNormal, frontDirection, backPosition, backNormal, backDirection, trainCar);
			}
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x000C0304 File Offset: 0x000BE704
		private void linkTrain()
		{
			Vector3 a = this.trainCars[0].root.position + this.trainCars[0].root.forward * -this.asset.trainCarLength / 2f;
			for (int i = 1; i < this.trainCars.Length; i++)
			{
				Vector3 b = this.trainCars[i].root.position + this.trainCars[i].root.forward * this.asset.trainCarLength / 2f;
				Vector3 a2 = a - b;
				float sqrMagnitude = a2.sqrMagnitude;
				if (sqrMagnitude > 1f)
				{
					float d = Mathf.Clamp01((sqrMagnitude - 1f) / 8f);
					this.trainCars[i].root.position += a2 * d;
				}
				a = this.trainCars[i].root.position + this.trainCars[i].root.forward * -this.asset.trainCarLength / 2f;
			}
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x000C0450 File Offset: 0x000BE850
		private TrainCar getTrainCar(Transform root)
		{
			Transform trackFront = root.FindChild("Objects").FindChild("Track_Front");
			Transform trackBack = root.FindChild("Objects").FindChild("Track_Back");
			return new TrainCar
			{
				root = root,
				trackFront = trackFront,
				trackBack = trackBack
			};
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x000C04A8 File Offset: 0x000BE8A8
		private float clampRoadPosition(float newRoadPosition)
		{
			if (!this.road.isLoop)
			{
				return Mathf.Clamp(newRoadPosition, 0.5f + this.asset.trainWheelOffset, this.road.trackSampledLength - (float)(this.trainCars.Length - 1) * this.asset.trainCarLength - this.asset.trainWheelOffset - 0.5f);
			}
			if (newRoadPosition < 0f)
			{
				return this.road.trackSampledLength + newRoadPosition;
			}
			if (newRoadPosition > this.road.trackSampledLength)
			{
				return newRoadPosition - this.road.trackSampledLength;
			}
			return newRoadPosition;
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x000C054C File Offset: 0x000BE94C
		private void Update()
		{
			if (this.asset == null)
			{
				return;
			}
			float deltaTime = Time.deltaTime;
			if (Provider.isServer && this.hooked != null)
			{
				for (int i = 0; i < this.hooked.Count; i++)
				{
					HookInfo hookInfo = this.hooked[i];
					if (hookInfo != null && !(hookInfo.target == null))
					{
						hookInfo.target.position = this.hook.TransformPoint(hookInfo.deltaPosition);
						hookInfo.target.rotation = this.hook.rotation * hookInfo.deltaRotation;
					}
				}
			}
			if (Dedicator.isDedicated)
			{
				if (this.isPhysical && this.updates != null && this.updates.Count == 0 && (Mathf.Abs(this.lastUpdatedPos.x - base.transform.position.x) > Provider.UPDATE_DISTANCE || Mathf.Abs(this.lastUpdatedPos.y - base.transform.position.y) > Provider.UPDATE_DISTANCE || Mathf.Abs(this.lastUpdatedPos.z - base.transform.position.z) > Provider.UPDATE_DISTANCE))
				{
					this.lastUpdatedPos = base.transform.position;
					Vector3 position;
					if (this.asset.engine == EEngine.TRAIN)
					{
						position = new Vector3(this.roadPosition, 0f, 0f);
					}
					else
					{
						position = base.transform.position;
					}
					this.updates.Add(new VehicleStateUpdate(position, base.transform.rotation));
				}
			}
			else
			{
				this._steer = Mathf.Lerp(this.steer, (float)this.turn * this.asset.steerMax, 4f * deltaTime);
				this._spedometer = Mathf.Lerp(this.spedometer, this.speed, 4f * deltaTime);
				if (!this.isExploded)
				{
					if (this.isDriven)
					{
						this.fly += (this.spedometer + 8f) * 89f * Time.deltaTime;
					}
					this.spin += this.spedometer * 45f * Time.deltaTime;
					if (this.tires != null)
					{
						for (int j = 0; j < this.tires.Length; j++)
						{
							if (!(this.tires[j].model == null))
							{
								this.tires[j].model.localRotation = this.tires[j].rest;
								if (j < ((this.asset.engine != EEngine.CAR) ? 1 : 2) && !this.asset.hasCrawler)
								{
									this.tires[j].model.Rotate(0f, this.steer, 0f, Space.Self);
								}
								this.tires[j].model.Rotate(this.spin, 0f, 0f, Space.Self);
							}
						}
					}
					if (this.front != null)
					{
						this.front.localRotation = this.restFront;
						this.front.transform.Rotate(0f, 0f, this.steer, Space.Self);
					}
					if (this.rotors != null)
					{
						for (int k = 0; k < this.rotors.Length; k++)
						{
							Rotor rotor = this.rotors[k];
							if (rotor == null || rotor.prop == null || rotor.material_0 == null || rotor.material_1 == null)
							{
								break;
							}
							rotor.prop.localRotation = rotor.rest * Quaternion.Euler(0f, this.fly, 0f);
							Color color = rotor.material_0.color;
							if (this.asset.engine == EEngine.PLANE)
							{
								color.a = Mathf.Lerp(1f, 0f, (this.spedometer - 16f) / 8f);
							}
							else
							{
								color.a = Mathf.Lerp(1f, 0f, (this.spedometer - 8f) / 8f);
							}
							rotor.material_0.color = color;
							color.a = (1f - color.a) * 0.25f;
							rotor.material_1.color = color;
						}
					}
					float num = Mathf.Max(0f, Mathf.InverseLerp(0f, this.asset.speedMax, this.physicsSpeed));
					if (this.exhausts != null)
					{
						for (int l = 0; l < this.exhausts.Length; l++)
						{
							this.exhausts[l].emission.rateOverTime = (float)this.exhausts[l].main.maxParticles * num;
						}
					}
					if (this.wheel != null)
					{
						this.wheel.transform.localRotation = this.restWheel;
						this.wheel.transform.Rotate(0f, -this.steer, 0f, Space.Self);
					}
					if (this.pedalLeft != null && this.pedalRight != null && this.passengers[0].player != null && this.passengers[0].player.player != null)
					{
						Transform thirdSkeleton = this.passengers[0].player.player.animator.thirdSkeleton;
						Transform transform = thirdSkeleton.FindChild("Left_Hip").FindChild("Left_Leg").FindChild("Left_Foot");
						Transform transform2 = thirdSkeleton.FindChild("Right_Hip").FindChild("Right_Leg").FindChild("Right_Foot");
						if (this.passengers[0].player.hand)
						{
							this.pedalLeft.position = transform2.position + transform2.right * 0.325f;
							this.pedalRight.position = transform.position + transform.right * 0.325f;
						}
						else
						{
							this.pedalLeft.position = transform.position + transform.right * -0.325f;
							this.pedalRight.position = transform2.position + transform2.right * -0.325f;
						}
					}
				}
				if (this.isDriven && !this.isUnderwater)
				{
					if (this.asset.engine == EEngine.HELICOPTER)
					{
						if (this.engineAudioSource != null)
						{
							this.engineAudioSource.pitch = Mathf.Lerp(this.engineAudioSource.pitch, 0.5f + Mathf.Abs(this.spedometer) * 0.03f, 2f * deltaTime);
							this.engineAudioSource.volume = Mathf.Lerp(this.engineAudioSource.volume, (!this.asset.isStaminaPowered && (this.fuel <= 0 || !this.isEngineOn)) ? 0f : (0.25f + Mathf.Abs(this.spedometer) * 0.03f), 0.125f * deltaTime);
						}
						if (this.windZone != null)
						{
							this.windZone.windMain = Mathf.Lerp(this.windZone.windMain, (!this.asset.isStaminaPowered && this.fuel <= 0) ? 0f : (Mathf.Abs(this.spedometer) * 0.1f), 0.125f * deltaTime);
						}
					}
					else if (this.asset.engine == EEngine.BLIMP)
					{
						if (this.engineAudioSource != null)
						{
							this.engineAudioSource.pitch = Mathf.Lerp(this.engineAudioSource.pitch, 0.5f + Mathf.Abs(this.spedometer) * 0.1f, 2f * deltaTime);
							this.engineAudioSource.volume = Mathf.Lerp(this.engineAudioSource.volume, (!this.asset.isStaminaPowered && (this.fuel <= 0 || !this.isEngineOn)) ? 0f : (0.25f + Mathf.Abs(this.spedometer) * 0.1f), 0.125f * deltaTime);
						}
						if (this.windZone != null)
						{
							this.windZone.windMain = Mathf.Lerp(this.windZone.windMain, (!this.asset.isStaminaPowered && this.fuel <= 0) ? 0f : (Mathf.Abs(this.spedometer) * 0.5f), 0.125f * deltaTime);
						}
					}
					else
					{
						if (this.engineAudioSource != null)
						{
							this.engineAudioSource.pitch = Mathf.Lerp(this.engineAudioSource.pitch, this.asset.pitchIdle + Mathf.Abs(this.spedometer) * this.asset.pitchDrive, 2f * deltaTime);
							this.engineAudioSource.volume = Mathf.Lerp(this.engineAudioSource.volume, (!this.asset.isStaminaPowered && (this.fuel <= 0 || !this.isEngineOn)) ? 0f : 0.75f, 2f * deltaTime);
						}
						if (this.engineAdditiveAudioSource != null)
						{
							this.engineAdditiveAudioSource.pitch = Mathf.Lerp(this.engineAdditiveAudioSource.pitch, this.asset.pitchIdle + Mathf.Abs(this.spedometer) * this.asset.pitchDrive, 2f * deltaTime);
							this.engineAdditiveAudioSource.volume = Mathf.Lerp(this.engineAdditiveAudioSource.volume, Mathf.Lerp(0f, 0.75f, Mathf.Abs(this.spedometer) / 8f), 2f * deltaTime);
						}
					}
				}
			}
			if (!Provider.isServer && !this.isPhysical && this.asset.engine != EEngine.TRAIN && this.nsb != null)
			{
				TransformSnapshotInfo transformSnapshotInfo = (TransformSnapshotInfo)this.nsb.getCurrentSnapshot();
				base.GetComponent<Rigidbody>().MovePosition(transformSnapshotInfo.pos);
				base.GetComponent<Rigidbody>().MoveRotation(transformSnapshotInfo.rot);
			}
			if (this.headlightsOn && !this.canTurnOnLights)
			{
				this.tellHeadlights(false);
			}
			if (this.taillightsOn && !this.canTurnOnLights)
			{
				this.tellTaillights(false);
			}
			if (this.sirensOn && !this.canTurnOnLights)
			{
				this.tellSirens(false);
			}
			if (this.isUnderwater)
			{
				if (!this.isDrowned)
				{
					this._lastUnderwater = Time.realtimeSinceStartup;
					this._isDrowned = true;
					this.tellSirens(false);
					this.tellBlimp(false);
					this.tellHeadlights(false);
					this.updateFires();
					if (!Dedicator.isDedicated)
					{
						if (this.engineAudioSource != null)
						{
							this.engineAudioSource.volume = 0f;
						}
						if (this.engineAdditiveAudioSource != null)
						{
							this.engineAdditiveAudioSource.volume = 0f;
						}
						if (this.windZone != null)
						{
							this.windZone.windMain = 0f;
						}
					}
				}
			}
			else if (this._isDrowned)
			{
				this._isDrowned = false;
				this.updateFires();
			}
			if (this.isDriver)
			{
				if (!this.asset.hasTraction)
				{
					bool flag = LevelLighting.isPositionSnowy(base.transform.position);
					AmbianceVolume ambianceVolume;
					if (!flag && Level.info.configData.Use_Snow_Volumes && AmbianceUtility.isPointInsideVolume(base.transform.position, out ambianceVolume))
					{
						flag = ambianceVolume.canSnow;
					}
					flag &= (LevelLighting.snowyness == ELightingSnow.BLIZZARD);
					this._slip = Mathf.Lerp(this._slip, (float)((!flag) ? 0 : 1), Time.deltaTime * 0.05f);
				}
				else
				{
					this._slip = 0f;
				}
				if (this.tires != null)
				{
					for (int m = 0; m < this.tires.Length; m++)
					{
						if (this.tires[m] == null)
						{
							break;
						}
						this.tires[m].update(deltaTime);
					}
				}
				if (this.asset.engine == EEngine.TRAIN && this.road != null)
				{
					foreach (TrainCar trainCar in this.trainCars)
					{
						Vector3 frontPosition;
						Vector3 frontNormal;
						Vector3 frontDirection;
						this.road.getTrackData(this.clampRoadPosition(this.roadPosition + trainCar.trackPositionOffset + this.asset.trainWheelOffset), out frontPosition, out frontNormal, out frontDirection);
						Vector3 backPosition;
						Vector3 backNormal;
						Vector3 backDirection;
						this.road.getTrackData(this.clampRoadPosition(this.roadPosition + trainCar.trackPositionOffset - this.asset.trainWheelOffset), out backPosition, out backNormal, out backDirection);
						this.moveTrain(frontPosition, frontNormal, frontDirection, backPosition, backNormal, backDirection, trainCar);
					}
					float num2 = this.altSpeedOutput * deltaTime;
					Transform transform3;
					if (this.altSpeedOutput > 0f)
					{
						transform3 = this.overlapFront;
					}
					else
					{
						transform3 = this.overlapBack;
					}
					Vector3 vector = transform3.position + transform3.forward * num2 / 2f;
					Vector3 size = transform3.GetComponent<BoxCollider>().size;
					size.z = num2;
					bool flag2 = false;
					int num3 = Physics.OverlapBoxNonAlloc(vector, size / 2f, InteractableVehicle.grab, transform3.rotation, RayMasks.BLOCK_TRAIN, QueryTriggerInteraction.Ignore);
					for (int num4 = 0; num4 < num3; num4++)
					{
						bool flag3 = false;
						for (int num5 = 0; num5 < this.trainCars.Length; num5++)
						{
							if (InteractableVehicle.grab[num4].transform.IsChildOf(this.trainCars[num5].root) || InteractableVehicle.grab[num4].transform == this.trainCars[num5].root)
							{
								flag3 = true;
								break;
							}
						}
						if (!flag3)
						{
							if (InteractableVehicle.grab[num4].CompareTag("Vehicle"))
							{
								Rigidbody component = InteractableVehicle.grab[num4].GetComponent<Rigidbody>();
								if (!component.isKinematic)
								{
									component.AddForce(base.transform.forward * this.altSpeedOutput, ForceMode.VelocityChange);
								}
							}
							flag2 = true;
							break;
						}
					}
					if (flag2)
					{
						if (this.altSpeedOutput > 0f)
						{
							if (this.altSpeedInput > 0f)
							{
								this.altSpeedInput = 0f;
							}
						}
						else if (this.altSpeedInput < 0f)
						{
							this.altSpeedInput = 0f;
						}
					}
					else
					{
						this.roadPosition += num2;
						this.roadPosition = this.clampRoadPosition(this.roadPosition);
					}
				}
			}
			if (!Dedicator.isDedicated && this.road != null)
			{
				foreach (TrainCar trainCar2 in this.trainCars)
				{
					Vector3 b;
					Vector3 b2;
					Vector3 b3;
					this.road.getTrackData(this.clampRoadPosition(this.roadPosition + trainCar2.trackPositionOffset + this.asset.trainWheelOffset), out b, out b2, out b3);
					trainCar2.currentFrontPosition = Vector3.Lerp(trainCar2.currentFrontPosition, b, 8f * Time.deltaTime);
					trainCar2.currentFrontNormal = Vector3.Lerp(trainCar2.currentFrontNormal, b2, 8f * Time.deltaTime);
					trainCar2.currentFrontDirection = Vector3.Lerp(trainCar2.currentFrontDirection, b3, 8f * Time.deltaTime);
					Vector3 b4;
					Vector3 b5;
					Vector3 b6;
					this.road.getTrackData(this.clampRoadPosition(this.roadPosition + trainCar2.trackPositionOffset - this.asset.trainWheelOffset), out b4, out b5, out b6);
					trainCar2.currentBackPosition = Vector3.Lerp(trainCar2.currentBackPosition, b4, 8f * Time.deltaTime);
					trainCar2.currentBackNormal = Vector3.Lerp(trainCar2.currentBackNormal, b5, 8f * Time.deltaTime);
					trainCar2.currentBackDirection = Vector3.Lerp(trainCar2.currentBackDirection, b6, 8f * Time.deltaTime);
					this.moveTrain(trainCar2.currentFrontPosition, trainCar2.currentFrontNormal, trainCar2.currentFrontDirection, trainCar2.currentBackPosition, trainCar2.currentBackNormal, trainCar2.currentBackDirection, trainCar2);
				}
			}
			if (Provider.isServer)
			{
				if (this.isDriven)
				{
					if (this.tires != null)
					{
						for (int num7 = 0; num7 < this.tires.Length; num7++)
						{
							if (this.tires[num7] == null)
							{
								break;
							}
							this.tires[num7].checkForTraps();
						}
					}
				}
				else
				{
					this._speed = base.transform.InverseTransformDirection(base.GetComponent<Rigidbody>().velocity).z;
					this._physicsSpeed = this._speed;
					this._turn = 0;
					this.real = base.transform.position;
				}
				if (this.isDead && !this.isExploded && !this.isUnderwater && Time.realtimeSinceStartup - this.lastDead > InteractableVehicle.EXPLODE)
				{
					this.explode();
				}
			}
			if (!Provider.isServer && !this.isPhysical && Time.realtimeSinceStartup - this.lastTick > Provider.UPDATE_TIME * 2f)
			{
				this.lastTick = Time.realtimeSinceStartup;
				this._speed = 0f;
				this._physicsSpeed = 0f;
				this._turn = 0;
			}
			if (this.sirensOn && !Dedicator.isDedicated && Time.realtimeSinceStartup - this.lastWeeoo > 0.33f)
			{
				this.lastWeeoo = Time.realtimeSinceStartup;
				this.sirenState = !this.sirenState;
				foreach (GameObject gameObject in this.sirenGameObjects_0)
				{
					gameObject.SetActive(!this.sirenState);
				}
				foreach (GameObject gameObject2 in this.sirenGameObjects_1)
				{
					gameObject2.SetActive(this.sirenState);
				}
				if (this.sirenMaterials != null)
				{
					if (this.sirenMaterials[0] != null)
					{
						this.sirenMaterials[0].SetColor("_EmissionColor", this.sirenState ? Color.black : this.sirenMaterials[0].color);
					}
					if (this.sirenMaterials[1] != null)
					{
						this.sirenMaterials[1].SetColor("_EmissionColor", (!this.sirenState) ? Color.black : this.sirenMaterials[1].color);
					}
				}
			}
			if (this.usesBattery)
			{
				this.batteryBuffer += deltaTime * 20f;
				ushort num8 = (ushort)Mathf.FloorToInt(this.batteryBuffer);
				this.batteryBuffer -= (float)num8;
				if (num8 > 0)
				{
					if (this.isEngineOn && this.isDriven)
					{
						this.askChargeBattery(num8);
					}
					else if (this.headlightsOn || this.sirensOn)
					{
						this.askBurnBattery(num8);
					}
				}
			}
			if (Provider.isServer)
			{
				SafezoneNode safezoneNode;
				if (LevelNodes.isPointInsideSafezone(base.transform.position, out safezoneNode))
				{
					this.timeInsideSafezone += deltaTime;
					if (Provider.modeConfigData != null && Provider.modeConfigData.Vehicles.Unlocked_After_Seconds_In_Safezone > 0f && this.timeInsideSafezone > Provider.modeConfigData.Vehicles.Unlocked_After_Seconds_In_Safezone && this.isEmpty && this.isLocked)
					{
						VehicleManager.unlockVehicle(this, null);
					}
				}
				else
				{
					this.timeInsideSafezone = -1f;
				}
			}
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x000C1AF0 File Offset: 0x000BFEF0
		protected virtual void handleTireAliveChanged(Wheel wheel)
		{
			if (this.isPhysical)
			{
				base.GetComponent<Rigidbody>().WakeUp();
			}
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x000C1B08 File Offset: 0x000BFF08
		public void safeInit()
		{
			this._asset = (VehicleAsset)Assets.find(EAssetType.VEHICLE, this.id);
			if (!Dedicator.isDedicated)
			{
				this.fire = base.transform.FindChild("Fire");
				LightLODTool.applyLightLOD(this.fire);
				this.smoke_0 = base.transform.FindChild("Smoke_0");
				this.smoke_1 = base.transform.FindChild("Smoke_1");
			}
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x000C1B84 File Offset: 0x000BFF84
		public void init()
		{
			this.safeInit();
			if (!Provider.isServer)
			{
				this.nsb = new NetworkSnapshotBuffer(Provider.UPDATE_TIME, Provider.UPDATE_DELAY);
			}
			if (Provider.isServer)
			{
				if (this.fuel == 65535)
				{
					if (Provider.mode == EGameMode.TUTORIAL)
					{
						this.fuel = 0;
					}
					else
					{
						this.fuel = (ushort)UnityEngine.Random.Range((int)this.asset.fuelMin, (int)this.asset.fuelMax);
					}
				}
				if (this.health == 65535)
				{
					this.health = (ushort)UnityEngine.Random.Range((int)this.asset.healthMin, (int)this.asset.healthMax);
				}
				if (this.batteryCharge == 65535)
				{
					if (this.usesBattery)
					{
						if (UnityEngine.Random.value < Provider.modeConfigData.Vehicles.Has_Battery_Chance)
						{
							this.batteryCharge = (ushort)UnityEngine.Random.Range(10000f * Provider.modeConfigData.Vehicles.Min_Battery_Charge, 10000f * Provider.modeConfigData.Vehicles.Max_Battery_Charge);
						}
						else
						{
							this.batteryCharge = 0;
						}
					}
					else
					{
						this.batteryCharge = 10000;
					}
				}
			}
			if (!Dedicator.isDedicated)
			{
				base.transform.FindAllChildrenWithName("Sirens", this.sirenGameObjects);
				base.transform.FindAllChildrenWithName("Siren_0", this.sirenGameObjects_0);
				base.transform.FindAllChildrenWithName("Siren_1", this.sirenGameObjects_1);
				foreach (GameObject gameObject in this.sirenGameObjects)
				{
					LightLODTool.applyLightLOD(gameObject.transform);
				}
				this.sirenMaterials = new Material[2];
				List<GameObject> list = new List<GameObject>();
				base.transform.FindAllChildrenWithName("Siren_0_Model", list);
				foreach (GameObject gameObject2 in list)
				{
					if (this.sirenMaterials[0] == null)
					{
						this.sirenMaterials[0] = gameObject2.GetComponent<Renderer>().material;
					}
					else
					{
						gameObject2.GetComponent<Renderer>().sharedMaterial = this.sirenMaterials[0];
					}
				}
				list.Clear();
				base.transform.FindAllChildrenWithName("Siren_1_Model", list);
				foreach (GameObject gameObject3 in list)
				{
					if (this.sirenMaterials[1] == null)
					{
						this.sirenMaterials[1] = gameObject3.GetComponent<Renderer>().material;
					}
					else
					{
						gameObject3.GetComponent<Renderer>().sharedMaterial = this.sirenMaterials[1];
					}
				}
				this._headlights = base.transform.FindChild("Headlights");
				LightLODTool.applyLightLOD(this.headlights);
				Transform transform = base.transform.FindChildRecursive("Headlights_Model");
				if (transform != null)
				{
					this.headlightsMaterial = transform.GetComponent<Renderer>().material;
				}
				this._taillights = base.transform.FindChild("Taillights");
				LightLODTool.applyLightLOD(this.taillights);
				Transform transform2 = base.transform.FindChildRecursive("Taillights_Model");
				if (transform2 != null)
				{
					this.taillightsMaterial = transform2.GetComponent<Renderer>().material;
				}
				else
				{
					InteractableVehicle.materials.Clear();
					for (int i = 0; i < 4; i++)
					{
						Transform transform3 = base.transform.FindChild("Taillight_" + i + "_Model");
						if (transform3 == null)
						{
							break;
						}
						InteractableVehicle.materials.Add(transform3.GetComponent<Renderer>().material);
					}
					if (InteractableVehicle.materials.Count > 0)
					{
						this.taillightMaterials = InteractableVehicle.materials.ToArray();
					}
				}
				if ((this.asset.engine == EEngine.HELICOPTER || this.asset.engine == EEngine.BLIMP) && this.clipAudioSource != null)
				{
					this.windZone = this.clipAudioSource.gameObject.AddComponent<WindZone>();
					this.windZone.mode = WindZoneMode.Spherical;
					this.windZone.radius = 64f;
					this.windZone.windMain = 0f;
					this.windZone.windTurbulence = 0f;
					this.windZone.windPulseFrequency = 0f;
					this.windZone.windPulseMagnitude = 0f;
				}
			}
			this._sirensOn = false;
			this._headlightsOn = false;
			this._taillightsOn = false;
			this.waterCenterTransform = base.transform.FindChild("Water_Center");
			Transform transform4 = base.transform.FindChild("Seats");
			if (transform4 == null)
			{
				Debug.LogError("Vehicle [" + this.id + "] missing seats!");
			}
			Transform transform5 = base.transform.FindChild("Objects");
			Transform transform6 = base.transform.FindChild("Turrets");
			this._passengers = new Passenger[transform4.childCount];
			for (int j = 0; j < this.passengers.Length; j++)
			{
				Transform newSeat = transform4.FindChild("Seat_" + j);
				Transform newObj = null;
				if (transform5 != null)
				{
					newObj = transform5.FindChild("Seat_" + j);
				}
				Transform transform7 = null;
				Transform newTurretPitch = null;
				Transform newTurretAim = null;
				if (transform6 != null)
				{
					Transform transform8 = transform6.FindChild("Turret_" + j);
					if (transform8 != null)
					{
						transform7 = transform8.FindChild("Yaw");
						if (transform7 != null)
						{
							Transform transform9 = transform7.FindChild("Seats");
							if (transform9 != null)
							{
								newSeat = transform9.FindChild("Seat_" + j);
							}
							Transform transform10 = transform7.FindChild("Objects");
							if (transform10 != null)
							{
								newObj = transform10.FindChild("Seat_" + j);
							}
							newTurretPitch = transform7.FindChild("Pitch");
						}
						newTurretAim = transform8.FindChildRecursive("Aim");
					}
				}
				this.passengers[j] = new Passenger(newSeat, newObj, transform7, newTurretPitch, newTurretAim);
			}
			this._turrets = new Passenger[this.asset.turrets.Length];
			for (int k = 0; k < this.turrets.Length; k++)
			{
				TurretInfo turretInfo = this.asset.turrets[k];
				if ((int)turretInfo.seatIndex < this.passengers.Length)
				{
					this.passengers[(int)turretInfo.seatIndex].turret = turretInfo;
					this._turrets[k] = this.passengers[(int)turretInfo.seatIndex];
				}
			}
			Transform transform11 = base.transform.FindChild("Tires");
			if (transform11 != null)
			{
				this.tires = new Wheel[transform11.childCount];
				for (int l = 0; l < transform11.childCount; l++)
				{
					Wheel wheel = new Wheel(this, (WheelCollider)transform11.FindChild("Tire_" + l).GetComponent<Collider>(), l < 2, l >= transform11.childCount - 2);
					wheel.reset();
					wheel.aliveChanged += this.handleTireAliveChanged;
					this.tires[l] = wheel;
				}
			}
			else
			{
				this.tires = new Wheel[0];
			}
			this.buoyancy = base.transform.FindChild("Buoyancy");
			if (this.buoyancy != null)
			{
				for (int m = 0; m < this.buoyancy.childCount; m++)
				{
					Transform child = this.buoyancy.GetChild(m);
					child.gameObject.AddComponent<Buoyancy>().density = (float)(this.buoyancy.childCount * 500);
					if (this.asset.engine == EEngine.BLIMP)
					{
						child.GetComponent<Buoyancy>().overrideSurfaceElevation = Level.info.configData.Blimp_Altitude;
					}
				}
			}
			this.hook = base.transform.FindChild("Hook");
			this.hooked = new List<HookInfo>();
			this.center = base.transform.FindChild("Center");
			if (this.center == null)
			{
				this.center = base.transform;
			}
			Transform transform12 = base.transform.FindChild("DepthMask");
			if (transform12 != null)
			{
				transform12.GetComponent<Renderer>().sharedMaterial = (Material)Resources.Load("Materials/DepthMask");
			}
			if (!Dedicator.isDedicated)
			{
				List<Wheel> list2 = new List<Wheel>(this.tires);
				Transform transform13 = base.transform.FindChild("Wheels");
				if (transform13 != null)
				{
					for (int n = 0; n < list2.Count; n++)
					{
						int num = -1;
						float num2 = 16f;
						for (int num3 = 0; num3 < transform13.childCount; num3++)
						{
							Transform child2 = transform13.GetChild(num3);
							float sqrMagnitude = (list2[n].wheel.transform.position - child2.position).sqrMagnitude;
							if (sqrMagnitude < num2)
							{
								num = num3;
								num2 = sqrMagnitude;
							}
						}
						if (num != -1)
						{
							Transform transform14 = transform13.GetChild(num);
							if (transform14.childCount == 0)
							{
								Transform transform15 = base.transform.FindChildRecursive("Wheel_" + num);
								if (transform15 != null)
								{
									transform14 = transform15;
								}
							}
							list2[n].model = transform14;
							list2[n].rest = transform14.localRotation;
						}
					}
					if (transform13.childCount != list2.Count)
					{
						for (int num4 = 0; num4 < transform13.childCount; num4++)
						{
							Transform transform16 = transform13.GetChild(num4);
							if (transform16.childCount != 0)
							{
								for (int num5 = 0; num5 < this.tires.Length; num5++)
								{
									if (list2[num5].model == transform16)
									{
										transform16 = null;
										break;
									}
								}
								if (!(transform16 == null))
								{
									list2.Add(new Wheel(this, null, false, false)
									{
										model = transform16,
										rest = transform16.localRotation
									});
								}
							}
						}
					}
				}
				this.tires = list2.ToArray();
				this.wheel = base.transform.FindChild("Objects").FindChild("Steer");
				this.pedalLeft = base.transform.FindChild("Objects").FindChild("Pedal_Left");
				this.pedalRight = base.transform.FindChild("Objects").FindChild("Pedal_Right");
				Transform transform17 = base.transform.FindChild("Rotors");
				if (transform17 != null)
				{
					this.rotors = new Rotor[transform17.childCount];
					for (int num6 = 0; num6 < transform17.childCount; num6++)
					{
						Transform child3 = transform17.GetChild(num6);
						Rotor rotor = new Rotor();
						rotor.prop = child3;
						rotor.material_0 = child3.FindChild("Model_0").GetComponent<Renderer>().material;
						rotor.material_1 = child3.FindChild("Model_1").GetComponent<Renderer>().material;
						rotor.rest = child3.localRotation;
						this.rotors[num6] = rotor;
					}
				}
				else
				{
					this.rotors = new Rotor[0];
				}
				Transform transform18 = base.transform.FindChild("Exhaust");
				if (transform18 != null)
				{
					this.exhausts = new ParticleSystem[transform18.childCount];
					for (int num7 = 0; num7 < transform18.childCount; num7++)
					{
						Transform child4 = transform18.GetChild(num7);
						this.exhausts[num7] = child4.GetComponent<ParticleSystem>();
					}
				}
				else
				{
					this.exhausts = new ParticleSystem[0];
				}
				if (this.wheel != null)
				{
					this.restWheel = this.wheel.localRotation;
				}
				this.front = base.transform.FindChild("Objects").FindChild("Front");
				if (this.front != null)
				{
					this.restFront = this.front.localRotation;
				}
				this.tellFuel(this.fuel);
				this.tellHealth(this.health);
				this.tellBatteryCharge(this.batteryCharge);
				this.updateSkin();
			}
			if (this.isExploded)
			{
				this.tellExploded();
			}
			if (this.asset.engine == EEngine.TRAIN)
			{
				Transform transform19 = base.transform.FindChild("Train_Cars");
				int childCount = transform19.childCount;
				this.trainCars = new TrainCar[1 + childCount];
				this.trainCars[0] = this.getTrainCar(base.transform);
				for (int num8 = 1; num8 <= childCount; num8++)
				{
					Transform transform20 = transform19.FindChild("Train_Car_" + num8);
					transform20.parent = LevelVehicles.models;
					transform20.GetOrAddComponent<VehicleRef>().vehicle = this;
					TrainCar trainCar = this.getTrainCar(transform20);
					trainCar.trackPositionOffset = (float)num8 * -this.asset.trainCarLength;
					this.trainCars[num8] = trainCar;
				}
				foreach (TrainCar trainCar2 in this.trainCars)
				{
					if (this.overlapFront == null)
					{
						this.overlapFront = trainCar2.root.FindChild("Overlap_Front");
					}
					if (this.overlapBack == null)
					{
						this.overlapBack = trainCar2.root.FindChild("Overlap_Back");
					}
					if (this.overlapFront != null && this.overlapBack != null)
					{
						break;
					}
				}
				foreach (LevelTrainAssociation levelTrainAssociation in Level.info.configData.Trains)
				{
					if (levelTrainAssociation.VehicleID == this.id)
					{
						this.roadIndex = levelTrainAssociation.RoadIndex;
						break;
					}
				}
				this.road = LevelRoads.getRoad((int)this.roadIndex);
				this.teleportTrain();
			}
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x000C2B28 File Offset: 0x000C0F28
		private void Awake()
		{
			if (!Dedicator.isDedicated)
			{
				this.clipAudioSource = base.transform.FindChild("Sound").GetComponent<AudioSource>();
				this.engineAudioSource = base.GetComponent<AudioSource>();
				if (this.engineAudioSource != null)
				{
					this.engineAudioSource.maxDistance *= 2f;
				}
				Transform transform = base.transform.FindChild("Engine_Additive");
				if (transform != null)
				{
					this.engineAdditiveAudioSource = transform.GetComponent<AudioSource>();
					this.engineAdditiveAudioSource.maxDistance *= 2f;
				}
			}
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x000C2BD0 File Offset: 0x000C0FD0
		private void initBumper(Transform bumper, bool reverse, bool instakill)
		{
			if (bumper == null)
			{
				return;
			}
			if (Provider.isServer)
			{
				Bumper bumper2 = bumper.gameObject.AddComponent<Bumper>();
				bumper2.reverse = reverse;
				bumper2.instakill = instakill;
				bumper2.init(this);
			}
			else
			{
				UnityEngine.Object.Destroy(bumper.gameObject);
			}
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x000C2C28 File Offset: 0x000C1028
		private void initBumpers(Transform root)
		{
			if (!Provider.isServer)
			{
				Transform transform = root.FindChildRecursive("Nav");
				if (transform != null)
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
			Transform bumper = root.FindChildRecursive("Bumper");
			this.initBumper(bumper, false, this.asset.engine == EEngine.TRAIN);
			Transform bumper2 = root.FindChildRecursive("Bumper_Front");
			this.initBumper(bumper2, false, this.asset.engine == EEngine.TRAIN);
			Transform bumper3 = root.FindChildRecursive("Bumper_Back");
			this.initBumper(bumper3, true, this.asset.engine == EEngine.TRAIN);
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000C2CC8 File Offset: 0x000C10C8
		private void Start()
		{
			if (this.trainCars != null && this.trainCars.Length > 0)
			{
				foreach (TrainCar trainCar in this.trainCars)
				{
					this.initBumpers(trainCar.root);
				}
			}
			else
			{
				this.initBumpers(base.transform);
			}
			this.updateVehicle();
			this.updatePhysics();
			this.updateEngine();
			this.updates = new List<VehicleStateUpdate>();
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x000C2D48 File Offset: 0x000C1148
		private void OnDestroy()
		{
			this.dropTrunkItems();
			if (this.isExploded && !Dedicator.isDedicated)
			{
				HighlighterTool.destroyMaterials(base.transform);
				if (this.turrets != null)
				{
					for (int i = 0; i < this.turrets.Length; i++)
					{
						HighlighterTool.destroyMaterials(this.turrets[i].turretYaw);
						HighlighterTool.destroyMaterials(this.turrets[i].turretPitch);
					}
				}
			}
			if (this.headlightsMaterial != null)
			{
				UnityEngine.Object.DestroyImmediate(this.headlightsMaterial);
			}
			if (this.taillightsMaterial != null)
			{
				UnityEngine.Object.DestroyImmediate(this.taillightsMaterial);
			}
			else if (this.taillightMaterials != null)
			{
				for (int j = 0; j < this.taillightMaterials.Length; j++)
				{
					if (this.taillightMaterials[j] != null)
					{
						UnityEngine.Object.DestroyImmediate(this.taillightMaterials[j]);
					}
				}
			}
			if (this.sirenMaterials != null)
			{
				for (int k = 0; k < this.sirenMaterials.Length; k++)
				{
					if (this.sirenMaterials[k] != null)
					{
						UnityEngine.Object.DestroyImmediate(this.sirenMaterials[k]);
					}
				}
			}
			if (this.rotors == null)
			{
				return;
			}
			for (int l = 0; l < this.rotors.Length; l++)
			{
				if (this.rotors[l].material_0 != null)
				{
					UnityEngine.Object.DestroyImmediate(this.rotors[l].material_0);
					this.rotors[l].material_0 = null;
				}
				if (this.rotors[l].material_1 != null)
				{
					UnityEngine.Object.DestroyImmediate(this.rotors[l].material_1);
					this.rotors[l].material_1 = null;
				}
			}
		}

		// Token: 0x0400147C RID: 5244
		private static Collider[] grab = new Collider[4];

		// Token: 0x0400147D RID: 5245
		private static List<Material> materials = new List<Material>();

		// Token: 0x0400147E RID: 5246
		private static readonly float EXPLODE = 4f;

		// Token: 0x0400147F RID: 5247
		private static readonly ushort HEALTH_0 = 100;

		// Token: 0x04001480 RID: 5248
		private static readonly ushort HEALTH_1 = 200;

		// Token: 0x04001489 RID: 5257
		public uint instanceID;

		// Token: 0x0400148A RID: 5258
		public ushort id;

		// Token: 0x0400148B RID: 5259
		public Items trunkItems;

		// Token: 0x0400148C RID: 5260
		public ushort skinID;

		// Token: 0x0400148D RID: 5261
		public ushort mythicID;

		// Token: 0x0400148E RID: 5262
		protected SkinAsset skinAsset;

		// Token: 0x0400148F RID: 5263
		private List<Mesh> tempMesh;

		// Token: 0x04001490 RID: 5264
		protected Material tempMaterial;

		// Token: 0x04001491 RID: 5265
		protected Transform effectSlotsRoot;

		// Token: 0x04001492 RID: 5266
		protected Transform[] effectSlots;

		// Token: 0x04001493 RID: 5267
		protected Transform[] effectSystems;

		// Token: 0x04001494 RID: 5268
		public ushort roadIndex;

		// Token: 0x04001495 RID: 5269
		public float roadPosition;

		// Token: 0x04001497 RID: 5271
		public ushort fuel;

		// Token: 0x04001498 RID: 5272
		public ushort health;

		// Token: 0x04001499 RID: 5273
		public ushort batteryCharge;

		// Token: 0x0400149C RID: 5276
		private uint lastBurnFuel;

		// Token: 0x0400149D RID: 5277
		private uint lastBurnBattery;

		// Token: 0x0400149E RID: 5278
		private float horned;

		// Token: 0x040014A0 RID: 5280
		private bool _isDrowned;

		// Token: 0x040014A1 RID: 5281
		private float _lastDead;

		// Token: 0x040014A2 RID: 5282
		private float _lastUnderwater;

		// Token: 0x040014A3 RID: 5283
		private float _lastExploded;

		// Token: 0x040014A4 RID: 5284
		private float _slip;

		// Token: 0x040014A5 RID: 5285
		public bool isExploded;

		// Token: 0x040014A6 RID: 5286
		private float _factor;

		// Token: 0x040014A7 RID: 5287
		private float _speed;

		// Token: 0x040014A8 RID: 5288
		private float _physicsSpeed;

		// Token: 0x040014A9 RID: 5289
		private float _spedometer;

		// Token: 0x040014AA RID: 5290
		private int _turn;

		// Token: 0x040014AB RID: 5291
		private float spin;

		// Token: 0x040014AC RID: 5292
		private float _steer;

		// Token: 0x040014AD RID: 5293
		private float fly;

		// Token: 0x040014AE RID: 5294
		private Rotor[] rotors;

		// Token: 0x040014AF RID: 5295
		private ParticleSystem[] exhausts;

		// Token: 0x040014B0 RID: 5296
		private Transform wheel;

		// Token: 0x040014B2 RID: 5298
		private Transform overlapFront;

		// Token: 0x040014B3 RID: 5299
		private Transform overlapBack;

		// Token: 0x040014B4 RID: 5300
		private Transform pedalLeft;

		// Token: 0x040014B5 RID: 5301
		private Transform pedalRight;

		// Token: 0x040014B6 RID: 5302
		private Transform front;

		// Token: 0x040014B7 RID: 5303
		private Quaternion restWheel;

		// Token: 0x040014B8 RID: 5304
		private Quaternion restFront;

		// Token: 0x040014B9 RID: 5305
		private Transform waterCenterTransform;

		// Token: 0x040014BA RID: 5306
		private Transform fire;

		// Token: 0x040014BB RID: 5307
		private Transform smoke_0;

		// Token: 0x040014BC RID: 5308
		private Transform smoke_1;

		// Token: 0x040014BD RID: 5309
		[Obsolete]
		public bool isUpdated;

		// Token: 0x040014BE RID: 5310
		public List<VehicleStateUpdate> updates;

		// Token: 0x040014BF RID: 5311
		private Material[] sirenMaterials;

		// Token: 0x040014C0 RID: 5312
		private bool sirenState;

		// Token: 0x040014C1 RID: 5313
		private List<GameObject> sirenGameObjects = new List<GameObject>();

		// Token: 0x040014C2 RID: 5314
		private List<GameObject> sirenGameObjects_0 = new List<GameObject>();

		// Token: 0x040014C3 RID: 5315
		private List<GameObject> sirenGameObjects_1 = new List<GameObject>();

		// Token: 0x040014C4 RID: 5316
		private bool _sirensOn;

		// Token: 0x040014C5 RID: 5317
		private Transform _headlights;

		// Token: 0x040014C6 RID: 5318
		private Material headlightsMaterial;

		// Token: 0x040014C7 RID: 5319
		private bool _headlightsOn;

		// Token: 0x040014C8 RID: 5320
		private Transform _taillights;

		// Token: 0x040014C9 RID: 5321
		private Material taillightsMaterial;

		// Token: 0x040014CA RID: 5322
		private Material[] taillightMaterials;

		// Token: 0x040014CB RID: 5323
		private bool _taillightsOn;

		// Token: 0x040014CC RID: 5324
		private CSteamID _lockedOwner;

		// Token: 0x040014CD RID: 5325
		private CSteamID _lockedGroup;

		// Token: 0x040014CE RID: 5326
		private bool _isLocked;

		// Token: 0x040014CF RID: 5327
		private VehicleAsset _asset;

		// Token: 0x040014D0 RID: 5328
		public float lastSeat;

		// Token: 0x040014D1 RID: 5329
		private Passenger[] _passengers;

		// Token: 0x040014D2 RID: 5330
		private Passenger[] _turrets;

		// Token: 0x040014D4 RID: 5332
		public bool isHooked;

		// Token: 0x040014D5 RID: 5333
		private Transform buoyancy;

		// Token: 0x040014D6 RID: 5334
		private Transform hook;

		// Token: 0x040014D7 RID: 5335
		private List<HookInfo> hooked;

		// Token: 0x040014D8 RID: 5336
		private Transform center;

		// Token: 0x040014D9 RID: 5337
		private Vector3 lastUpdatedPos;

		// Token: 0x040014DA RID: 5338
		private NetworkSnapshotBuffer nsb;

		// Token: 0x040014DB RID: 5339
		private Vector3 real;

		// Token: 0x040014DC RID: 5340
		private float lastTick;

		// Token: 0x040014DD RID: 5341
		private float lastWeeoo;

		// Token: 0x040014DE RID: 5342
		private AudioSource clipAudioSource;

		// Token: 0x040014DF RID: 5343
		private AudioSource engineAudioSource;

		// Token: 0x040014E0 RID: 5344
		private AudioSource engineAdditiveAudioSource;

		// Token: 0x040014E1 RID: 5345
		private WindZone windZone;

		// Token: 0x040014E2 RID: 5346
		private bool isRecovering;

		// Token: 0x040014E3 RID: 5347
		private float lastRecover;

		// Token: 0x040014E4 RID: 5348
		private bool isPhysical;

		// Token: 0x040014E5 RID: 5349
		private bool isFrozen;

		// Token: 0x040014E6 RID: 5350
		public bool isBlimpFloating;

		// Token: 0x040014E7 RID: 5351
		private float altSpeedInput;

		// Token: 0x040014E8 RID: 5352
		private float altSpeedOutput;

		// Token: 0x040014E9 RID: 5353
		private float speedTraction;

		// Token: 0x040014EA RID: 5354
		private float batteryBuffer;
	}
}
