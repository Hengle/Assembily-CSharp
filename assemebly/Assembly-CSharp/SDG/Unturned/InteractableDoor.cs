using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004CB RID: 1227
	public class InteractableDoor : Interactable
	{
		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x000B3B22 File Offset: 0x000B1F22
		public CSteamID owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060020CE RID: 8398 RVA: 0x000B3B2A File Offset: 0x000B1F2A
		public CSteamID group
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x000B3B32 File Offset: 0x000B1F32
		public bool isOpen
		{
			get
			{
				return this._isOpen;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x000B3B3A File Offset: 0x000B1F3A
		public bool isOpenable
		{
			get
			{
				return Time.realtimeSinceStartup - this.opened > 0.75f;
			}
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x000B3B50 File Offset: 0x000B1F50
		public bool checkToggle(CSteamID enemyPlayer, CSteamID enemyGroup)
		{
			return (!Provider.isServer || !(this.placeholderCollider != null) || Physics.OverlapBoxNonAlloc(this.placeholderCollider.transform.position + this.placeholderCollider.transform.rotation * this.placeholderCollider.center, this.placeholderCollider.size, InteractableDoor.checkColliders, this.placeholderCollider.transform.rotation, (!base.transform.parent.CompareTag("Vehicle")) ? RayMasks.BLOCK_CHAR_HINGE_OVERLAP : RayMasks.BLOCK_CHAR_HINGE_OVERLAP_ON_VEHICLE, QueryTriggerInteraction.Collide) <= 0) && ((Provider.isServer && !Dedicator.isDedicated) || !this.isLocked || enemyPlayer == this.owner || (this.group != CSteamID.Nil && enemyGroup == this.group));
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x000B3C60 File Offset: 0x000B2060
		public void updateToggle(bool newOpen)
		{
			this.opened = Time.realtimeSinceStartup;
			this._isOpen = newOpen;
			if (this.isOpen)
			{
				base.GetComponent<Animation>().Play("Open");
			}
			else
			{
				base.GetComponent<Animation>().Play("Close");
			}
			if (!Dedicator.isDedicated)
			{
				base.GetComponent<AudioSource>().Play();
			}
			if (Provider.isServer)
			{
				AlertTool.alert(base.transform.position, 8f);
			}
			if (this.barrierTransform != null)
			{
				this.barrierTransform.gameObject.SetActive(!this.isOpen);
			}
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x000B3D10 File Offset: 0x000B2110
		public override void updateState(Asset asset, byte[] state)
		{
			this.isLocked = ((ItemBarricadeAsset)asset).isLocked;
			this._owner = new CSteamID(BitConverter.ToUInt64(state, 0));
			this._group = new CSteamID(BitConverter.ToUInt64(state, 8));
			this._isOpen = (state[16] == 1);
			if (this.isOpen)
			{
				base.GetComponent<Animation>().Play("Open");
			}
			else
			{
				base.GetComponent<Animation>().Play("Close");
			}
			Transform transform = base.transform.FindChild("Placeholder");
			if (transform != null)
			{
				this.placeholderCollider = transform.GetComponent<BoxCollider>();
			}
			else
			{
				this.placeholderCollider = null;
			}
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x000B3DC8 File Offset: 0x000B21C8
		protected virtual void Update()
		{
			if (this.placeholderCollider == null || this.barrierTransform == null)
			{
				return;
			}
			this.barrierTransform.position = this.placeholderCollider.transform.position;
			this.barrierTransform.rotation = this.placeholderCollider.transform.rotation;
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x000B3E30 File Offset: 0x000B2230
		protected virtual void Start()
		{
			if (base.transform.parent != null && !base.transform.parent.CompareTag("Vehicle") && this.placeholderCollider != null)
			{
				this.barrierTransform = UnityEngine.Object.Instantiate<GameObject>(this.placeholderCollider.gameObject).transform;
				this.barrierTransform.position = this.placeholderCollider.transform.position;
				this.barrierTransform.rotation = this.placeholderCollider.transform.rotation;
				this.barrierTransform.tag = "Clip";
				this.barrierTransform.gameObject.layer = LayerMasks.CLIP;
				this.barrierTransform.parent = LevelBarricades.models;
				Rigidbody component = this.barrierTransform.GetComponent<Rigidbody>();
				if (component != null)
				{
					UnityEngine.Object.Destroy(component);
				}
				this.barrierTransform.gameObject.SetActive(!this.isOpen);
			}
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000B3F3B File Offset: 0x000B233B
		protected virtual void OnDestroy()
		{
			if (this.barrierTransform != null)
			{
				UnityEngine.Object.Destroy(this.barrierTransform.gameObject);
			}
		}

		// Token: 0x0400138C RID: 5004
		private static Collider[] checkColliders = new Collider[1];

		// Token: 0x0400138D RID: 5005
		private CSteamID _owner;

		// Token: 0x0400138E RID: 5006
		private CSteamID _group;

		// Token: 0x0400138F RID: 5007
		private bool _isOpen;

		// Token: 0x04001390 RID: 5008
		private bool isLocked;

		// Token: 0x04001391 RID: 5009
		private float opened;

		// Token: 0x04001392 RID: 5010
		private Transform barrierTransform;

		// Token: 0x04001393 RID: 5011
		private BoxCollider placeholderCollider;
	}
}
