using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000384 RID: 900
	public class AnimalAsset : Asset
	{
		// Token: 0x060018FE RID: 6398 RVA: 0x0008CD50 File Offset: 0x0008B150
		public AnimalAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			if (id < 50 && !bundle.hasResource && !data.has("Bypass_ID_Limit"))
			{
				throw new NotSupportedException("ID < 50");
			}
			this._animalName = localization.format("Name");
			this._client = (GameObject)bundle.load("Animal_Client");
			this._server = (GameObject)bundle.load("Animal_Server");
			this._dedicated = (GameObject)bundle.load("Animal_Dedicated");
			this._ragdoll = (GameObject)bundle.load("Ragdoll");
			if (this.client == null)
			{
				Assets.errors.Add(this.animalName + " is missing client data. Highly recommended to fix.");
			}
			if (this.server == null)
			{
				Assets.errors.Add(this.animalName + " is missing server data. Highly recommended to fix.");
			}
			if (this.dedicated == null)
			{
				Assets.errors.Add(this.animalName + " is missing dedicated data. Highly recommended to fix.");
			}
			if (this.ragdoll == null)
			{
				Assets.errors.Add(this.animalName + " is missing ragdoll data. Highly recommended to fix.");
			}
			this._speedRun = data.readSingle("Speed_Run");
			this._speedWalk = data.readSingle("Speed_Walk");
			this._behaviour = (EAnimalBehaviour)Enum.Parse(typeof(EAnimalBehaviour), data.readString("Behaviour"), true);
			this._health = data.readUInt16("Health");
			this._regen = data.readSingle("Regen");
			if (!data.has("Regen"))
			{
				this._regen = 10f;
			}
			this._damage = data.readByte("Damage");
			this._meat = data.readUInt16("Meat");
			this._pelt = data.readUInt16("Pelt");
			this._rewardID = data.readUInt16("Reward_ID");
			if (data.has("Reward_Min"))
			{
				this._rewardMin = data.readByte("Reward_Min");
			}
			else
			{
				this._rewardMin = 3;
			}
			if (data.has("Reward_Max"))
			{
				this._rewardMax = data.readByte("Reward_Max");
			}
			else
			{
				this._rewardMax = 4;
			}
			this._roars = new AudioClip[(int)data.readByte("Roars")];
			byte b = 0;
			while ((int)b < this.roars.Length)
			{
				this.roars[(int)b] = (AudioClip)bundle.load("Roar_" + b);
				b += 1;
			}
			this._panics = new AudioClip[(int)data.readByte("Panics")];
			byte b2 = 0;
			while ((int)b2 < this.panics.Length)
			{
				this.panics[(int)b2] = (AudioClip)bundle.load("Panic_" + b2);
				b2 += 1;
			}
			this._rewardXP = data.readUInt32("Reward_XP");
			bundle.unload();
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x0008D08C File Offset: 0x0008B48C
		public string animalName
		{
			get
			{
				return this._animalName;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x0008D094 File Offset: 0x0008B494
		public GameObject client
		{
			get
			{
				return this._client;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x0008D09C File Offset: 0x0008B49C
		public GameObject server
		{
			get
			{
				return this._server;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x0008D0A4 File Offset: 0x0008B4A4
		public GameObject dedicated
		{
			get
			{
				return this._dedicated;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x0008D0AC File Offset: 0x0008B4AC
		public GameObject ragdoll
		{
			get
			{
				return this._ragdoll;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x0008D0B4 File Offset: 0x0008B4B4
		public float speedRun
		{
			get
			{
				return this._speedRun;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x0008D0BC File Offset: 0x0008B4BC
		public float speedWalk
		{
			get
			{
				return this._speedWalk;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x0008D0C4 File Offset: 0x0008B4C4
		public EAnimalBehaviour behaviour
		{
			get
			{
				return this._behaviour;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x0008D0CC File Offset: 0x0008B4CC
		public ushort health
		{
			get
			{
				return this._health;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001908 RID: 6408 RVA: 0x0008D0D4 File Offset: 0x0008B4D4
		public uint rewardXP
		{
			get
			{
				return this._rewardXP;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x0008D0DC File Offset: 0x0008B4DC
		public float regen
		{
			get
			{
				return this._regen;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600190A RID: 6410 RVA: 0x0008D0E4 File Offset: 0x0008B4E4
		public byte damage
		{
			get
			{
				return this._damage;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x0008D0EC File Offset: 0x0008B4EC
		public ushort meat
		{
			get
			{
				return this._meat;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x0008D0F4 File Offset: 0x0008B4F4
		public ushort pelt
		{
			get
			{
				return this._pelt;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x0008D0FC File Offset: 0x0008B4FC
		public byte rewardMin
		{
			get
			{
				return this._rewardMin;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x0600190E RID: 6414 RVA: 0x0008D104 File Offset: 0x0008B504
		public byte rewardMax
		{
			get
			{
				return this._rewardMax;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x0008D10C File Offset: 0x0008B50C
		public ushort rewardID
		{
			get
			{
				return this._rewardID;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x0008D114 File Offset: 0x0008B514
		public AudioClip[] roars
		{
			get
			{
				return this._roars;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x0008D11C File Offset: 0x0008B51C
		public AudioClip[] panics
		{
			get
			{
				return this._panics;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x0008D124 File Offset: 0x0008B524
		public override EAssetType assetCategory
		{
			get
			{
				return EAssetType.ANIMAL;
			}
		}

		// Token: 0x04000D6B RID: 3435
		protected string _animalName;

		// Token: 0x04000D6C RID: 3436
		protected GameObject _client;

		// Token: 0x04000D6D RID: 3437
		protected GameObject _server;

		// Token: 0x04000D6E RID: 3438
		protected GameObject _dedicated;

		// Token: 0x04000D6F RID: 3439
		protected GameObject _ragdoll;

		// Token: 0x04000D70 RID: 3440
		protected float _speedRun;

		// Token: 0x04000D71 RID: 3441
		protected float _speedWalk;

		// Token: 0x04000D72 RID: 3442
		private EAnimalBehaviour _behaviour;

		// Token: 0x04000D73 RID: 3443
		protected ushort _health;

		// Token: 0x04000D74 RID: 3444
		protected uint _rewardXP;

		// Token: 0x04000D75 RID: 3445
		protected float _regen;

		// Token: 0x04000D76 RID: 3446
		protected byte _damage;

		// Token: 0x04000D77 RID: 3447
		protected ushort _meat;

		// Token: 0x04000D78 RID: 3448
		protected ushort _pelt;

		// Token: 0x04000D79 RID: 3449
		private byte _rewardMin;

		// Token: 0x04000D7A RID: 3450
		private byte _rewardMax;

		// Token: 0x04000D7B RID: 3451
		private ushort _rewardID;

		// Token: 0x04000D7C RID: 3452
		protected AudioClip[] _roars;

		// Token: 0x04000D7D RID: 3453
		protected AudioClip[] _panics;
	}
}
