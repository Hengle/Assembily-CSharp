using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000569 RID: 1385
	public class ResourceSpawnpoint
	{
		// Token: 0x06002630 RID: 9776 RVA: 0x000E0554 File Offset: 0x000DE954
		public ResourceSpawnpoint(byte newType, ushort newID, Vector3 newPoint, bool newGenerated)
		{
			this.type = newType;
			this.id = newID;
			this._point = newPoint;
			this._isGenerated = newGenerated;
			this._asset = (ResourceAsset)Assets.find(EAssetType.RESOURCE, this.id);
			if (this.asset != null)
			{
				this.health = this.asset.health;
				this.isAlive = true;
				float num = Mathf.Sin((this.point.x + 4096f) * 32f + (this.point.z + 4096f) * 32f);
				this._angle = Quaternion.Euler(num * 5f, num * 360f, 0f);
				this._scale = new Vector3(1.1f + this.asset.scale + num * this.asset.scale, 1.1f + this.asset.scale + num * this.asset.scale, 1.1f + this.asset.scale + num * this.asset.scale);
				GameObject gameObject = null;
				if (this.asset.modelGameObject != null)
				{
					gameObject = this.asset.modelGameObject;
				}
				if (gameObject != null)
				{
					this._model = UnityEngine.Object.Instantiate<GameObject>(gameObject).transform;
					this.model.name = this.id.ToString();
					this.model.position = this.point + Vector3.down * this.scale.y * 0.75f;
					this.model.rotation = this.angle;
					this.model.localScale = this.scale;
					this.model.parent = LevelGround.models;
					if (Dedicator.isDedicated)
					{
						this.isEnabled = true;
					}
					else
					{
						this.model.gameObject.SetActive(false);
						if (!Level.isEditor && this.asset.isForage)
						{
							this.model.FindChild("Forage").gameObject.AddComponent<InteractableForage>();
						}
						if (this.asset.skyboxGameObject != null)
						{
							this._skybox = UnityEngine.Object.Instantiate<GameObject>(this.asset.skyboxGameObject).transform;
							this.skybox.name = this.id.ToString() + "_Skybox";
							this.skybox.parent = LevelGround.models;
							this.skybox.position = this.point + Vector3.down * this.scale.y * 0.75f;
							this.skybox.rotation = this.angle * Quaternion.Euler(-90f, 0f, 0f);
							this.skybox.localScale = new Vector3(this.skybox.localScale.x * this.scale.x, this.skybox.localScale.z * this.scale.z, this.skybox.localScale.z * this.scale.z);
							if (this.asset.skyboxMaterial != null)
							{
								this.skybox.GetComponent<MeshRenderer>().sharedMaterial = this.asset.skyboxMaterial;
							}
							if (GraphicsSettings.landmarkQuality >= EGraphicQuality.MEDIUM)
							{
								this.enableSkybox();
							}
							else
							{
								this.disableSkybox();
							}
						}
					}
				}
				GameObject gameObject2 = null;
				if (this.asset.stumpGameObject != null)
				{
					gameObject2 = this.asset.stumpGameObject;
				}
				if (gameObject2 != null)
				{
					this._stump = UnityEngine.Object.Instantiate<GameObject>(gameObject2).transform;
					this.stump.name = this.id.ToString();
					this.stump.position = this.point + Vector3.down * this.scale.y * 0.75f;
					this.stump.rotation = this.angle;
					this.stump.localScale = this.scale;
					this.stump.parent = LevelGround.models;
					this.stump.gameObject.SetActive(false);
					if (this.asset.isSpeedTree)
					{
						this.stumpCollider = this.stump.GetComponent<Collider>();
						this.stumpCollider.enabled = false;
					}
				}
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x000E0A40 File Offset: 0x000DEE40
		public float lastDead
		{
			get
			{
				return this._lastDead;
			}
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x000E0A48 File Offset: 0x000DEE48
		public bool checkCanReset(float multiplier)
		{
			return this.isDead && this.asset != null && this.asset.reset > 1f && Time.realtimeSinceStartup - this.lastDead > this.asset.reset * multiplier;
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x000E0A9E File Offset: 0x000DEE9E
		public bool isDead
		{
			get
			{
				return this.health == 0;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x000E0AA9 File Offset: 0x000DEEA9
		public Vector3 point
		{
			get
			{
				return this._point;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x000E0AB1 File Offset: 0x000DEEB1
		public bool isGenerated
		{
			get
			{
				return this._isGenerated;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x000E0AB9 File Offset: 0x000DEEB9
		public Quaternion angle
		{
			get
			{
				return this._angle;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06002637 RID: 9783 RVA: 0x000E0AC1 File Offset: 0x000DEEC1
		public Vector3 scale
		{
			get
			{
				return this._scale;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002638 RID: 9784 RVA: 0x000E0AC9 File Offset: 0x000DEEC9
		public ResourceAsset asset
		{
			get
			{
				return this._asset;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06002639 RID: 9785 RVA: 0x000E0AD1 File Offset: 0x000DEED1
		// (set) Token: 0x0600263A RID: 9786 RVA: 0x000E0AD9 File Offset: 0x000DEED9
		public bool isEnabled { get; private set; }

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x000E0AE2 File Offset: 0x000DEEE2
		// (set) Token: 0x0600263C RID: 9788 RVA: 0x000E0AEA File Offset: 0x000DEEEA
		public bool isSkyboxEnabled { get; private set; }

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x0600263D RID: 9789 RVA: 0x000E0AF3 File Offset: 0x000DEEF3
		public Transform model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x000E0AFB File Offset: 0x000DEEFB
		public Transform stump
		{
			get
			{
				return this._stump;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x000E0B03 File Offset: 0x000DEF03
		public Transform skybox
		{
			get
			{
				return this._skybox;
			}
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x000E0B0B File Offset: 0x000DEF0B
		public void askDamage(ushort amount)
		{
			if (amount == 0 || this.isDead)
			{
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
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000E0B48 File Offset: 0x000DEF48
		public void wipe()
		{
			if (!this.isAlive)
			{
				return;
			}
			this.isAlive = false;
			if (this.asset != null)
			{
				this.health = 0;
				if (this.asset.isForage)
				{
					if (!Dedicator.isDedicated)
					{
						this.model.FindChild("Forage").gameObject.SetActive(false);
					}
				}
				else
				{
					if (this.model != null)
					{
						this.model.gameObject.SetActive(false);
					}
					if (this.stump != null)
					{
						this.stump.gameObject.SetActive(this.isEnabled);
					}
					if (this.stumpCollider != null)
					{
						this.stumpCollider.enabled = true;
					}
				}
			}
			if (this.skybox)
			{
				this.skybox.gameObject.SetActive(false);
			}
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x000E0C3C File Offset: 0x000DF03C
		public void revive()
		{
			if (this.isAlive)
			{
				return;
			}
			this.isAlive = true;
			if (this.asset != null)
			{
				if (this.asset.isForage)
				{
					if (!Dedicator.isDedicated)
					{
						this.model.FindChild("Forage").gameObject.SetActive(true);
					}
					this.health = this.asset.health;
				}
				else
				{
					if (this.model != null)
					{
						this.model.gameObject.SetActive(this.isEnabled);
					}
					this.health = this.asset.health;
					if (this.stump != null)
					{
						this.stump.gameObject.SetActive(this.isEnabled && this.asset.isSpeedTree);
					}
					if (this.stumpCollider != null)
					{
						this.stumpCollider.enabled = false;
					}
				}
			}
			if (this.skybox)
			{
				this.skybox.gameObject.SetActive(this.isSkyboxEnabled);
			}
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x000E0D68 File Offset: 0x000DF168
		public void kill(Vector3 ragdoll)
		{
			if (!this.isAlive)
			{
				return;
			}
			this.isAlive = false;
			this._lastDead = Time.realtimeSinceStartup;
			if (this.asset != null)
			{
				this.health = 0;
				if (this.asset.isForage)
				{
					if (!Dedicator.isDedicated)
					{
						this.model.FindChild("Forage").gameObject.SetActive(false);
					}
				}
				else
				{
					if (this.model != null)
					{
						this.model.gameObject.SetActive(false);
					}
					if (this.stump != null)
					{
						this.stump.gameObject.SetActive(this.isEnabled);
					}
					if (this.stumpCollider != null)
					{
						this.stumpCollider.enabled = true;
					}
					if (!Dedicator.isDedicated && this.asset.hasDebris && GraphicsSettings.debris)
					{
						ragdoll.y += 8f;
						ragdoll.x += UnityEngine.Random.Range(-16f, 16f);
						ragdoll.z += UnityEngine.Random.Range(-16f, 16f);
						ragdoll *= (float)((!(Player.player != null) || Player.player.skills.boost != EPlayerBoost.FLIGHT) ? 2 : 4);
						if (this.model != null && this.asset.modelGameObject != null)
						{
							Transform transform;
							if (this.asset.debrisGameObject == null)
							{
								transform = UnityEngine.Object.Instantiate<GameObject>(this.asset.modelGameObject).transform;
							}
							else
							{
								transform = UnityEngine.Object.Instantiate<GameObject>(this.asset.debrisGameObject).transform;
							}
							transform.name = this.id.ToString();
							if (this.asset.isSpeedTree)
							{
								transform.position = this.model.position;
							}
							else
							{
								transform.position = this.model.position + Vector3.up;
							}
							transform.rotation = this.model.rotation;
							transform.localScale = this.model.localScale;
							transform.parent = Level.effects;
							transform.tag = "Debris";
							transform.gameObject.layer = LayerMasks.DEBRIS;
							transform.gameObject.AddComponent<Rigidbody>();
							transform.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
							transform.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
							transform.GetComponent<Rigidbody>().AddForce(ragdoll);
							transform.GetComponent<Rigidbody>().drag = 1f;
							transform.GetComponent<Rigidbody>().angularDrag = 1f;
							UnityEngine.Object.Destroy(transform.gameObject, 8f);
							if (this.stump != null && this.isEnabled && this.stumpCollider == null)
							{
								Collider component = transform.GetComponent<Collider>();
								if (component != null)
								{
									this.stump.GetComponents<Collider>(ResourceSpawnpoint.colliders);
									for (int i = 0; i < ResourceSpawnpoint.colliders.Count; i++)
									{
										Physics.IgnoreCollision(component, ResourceSpawnpoint.colliders[i]);
									}
								}
							}
						}
					}
				}
			}
			if (this.skybox)
			{
				this.skybox.gameObject.SetActive(false);
			}
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x000E10F8 File Offset: 0x000DF4F8
		public void forceFullEnable()
		{
			this.isEnabled = true;
			if (this.model != null)
			{
				this.model.gameObject.SetActive(true);
			}
			if (this.stump != null)
			{
				this.stump.gameObject.SetActive(true);
			}
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x000E1150 File Offset: 0x000DF550
		public void enable()
		{
			this.isEnabled = true;
			if (this.model != null)
			{
				this.model.gameObject.SetActive(this.isAlive);
			}
			if (this.stump != null)
			{
				this.stump.gameObject.SetActive(!this.isAlive || this.asset.isSpeedTree);
			}
			if (this.stumpCollider != null)
			{
				this.stumpCollider.enabled = !this.isAlive;
			}
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000E11EA File Offset: 0x000DF5EA
		public void enableSkybox()
		{
			this.isSkyboxEnabled = true;
			if (this.skybox != null)
			{
				this.skybox.gameObject.SetActive(this.isAlive);
			}
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x000E121C File Offset: 0x000DF61C
		public void disable()
		{
			this.isEnabled = false;
			if (this.model != null)
			{
				this.model.gameObject.SetActive(false);
			}
			if (this.stump != null)
			{
				this.stump.gameObject.SetActive(false);
			}
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x000E1274 File Offset: 0x000DF674
		public void disableSkybox()
		{
			this.isSkyboxEnabled = false;
			if (this.skybox != null)
			{
				this.skybox.gameObject.SetActive(false);
			}
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x000E12A0 File Offset: 0x000DF6A0
		public void destroy()
		{
			if (this.model)
			{
				UnityEngine.Object.Destroy(this.model.gameObject);
			}
			if (this.stump)
			{
				UnityEngine.Object.Destroy(this.stump.gameObject);
			}
			if (this.skybox)
			{
				UnityEngine.Object.Destroy(this.skybox.gameObject);
			}
		}

		// Token: 0x040017DF RID: 6111
		private static List<Collider> colliders = new List<Collider>();

		// Token: 0x040017E0 RID: 6112
		public byte type;

		// Token: 0x040017E1 RID: 6113
		public ushort id;

		// Token: 0x040017E2 RID: 6114
		private float _lastDead;

		// Token: 0x040017E3 RID: 6115
		private bool isAlive;

		// Token: 0x040017E4 RID: 6116
		private Vector3 _point;

		// Token: 0x040017E5 RID: 6117
		private bool _isGenerated;

		// Token: 0x040017E6 RID: 6118
		private Quaternion _angle;

		// Token: 0x040017E7 RID: 6119
		private Vector3 _scale;

		// Token: 0x040017E8 RID: 6120
		private ResourceAsset _asset;

		// Token: 0x040017EB RID: 6123
		private Transform _model;

		// Token: 0x040017EC RID: 6124
		private Transform _stump;

		// Token: 0x040017ED RID: 6125
		private Collider stumpCollider;

		// Token: 0x040017EE RID: 6126
		private Transform _skybox;

		// Token: 0x040017EF RID: 6127
		public ushort health;
	}
}
