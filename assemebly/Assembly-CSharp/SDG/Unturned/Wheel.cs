using System;
using SDG.Framework.Utilities;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000500 RID: 1280
	public class Wheel
	{
		// Token: 0x060022ED RID: 8941 RVA: 0x000C3094 File Offset: 0x000C1494
		public Wheel(InteractableVehicle newVehicle, WheelCollider newWheel, bool newSteered, bool newPowered)
		{
			this._vehicle = newVehicle;
			this._wheel = newWheel;
			if (this.wheel != null)
			{
				this.sidewaysFriction = this.wheel.sidewaysFriction;
				this.forwardFriction = this.wheel.forwardFriction;
				this.wheel.forceAppPointDistance = 0f;
			}
			this._isSteered = newSteered;
			this._isPowered = newPowered;
			this.isAlive = true;
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x000C310E File Offset: 0x000C150E
		public InteractableVehicle vehicle
		{
			get
			{
				return this._vehicle;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x000C3116 File Offset: 0x000C1516
		public WheelCollider wheel
		{
			get
			{
				return this._wheel;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x000C311E File Offset: 0x000C151E
		public bool isSteered
		{
			get
			{
				return this._isSteered;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060022F1 RID: 8945 RVA: 0x000C3126 File Offset: 0x000C1526
		public bool isPowered
		{
			get
			{
				return this._isPowered;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060022F2 RID: 8946 RVA: 0x000C312E File Offset: 0x000C152E
		public bool isGrounded
		{
			get
			{
				return this._isGrounded;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x000C3136 File Offset: 0x000C1536
		// (set) Token: 0x060022F4 RID: 8948 RVA: 0x000C3140 File Offset: 0x000C1540
		public bool isAlive
		{
			get
			{
				return this._isAlive;
			}
			set
			{
				if (this.isAlive == value)
				{
					return;
				}
				this._isAlive = value;
				if (this.model != null)
				{
					this.model.gameObject.SetActive(this.isAlive);
				}
				this.updateColliderEnabled();
				this.triggerAliveChanged();
			}
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000C3194 File Offset: 0x000C1594
		protected virtual void triggerAliveChanged()
		{
			if (this.aliveChanged != null)
			{
				this.aliveChanged(this);
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060022F6 RID: 8950 RVA: 0x000C31AD File Offset: 0x000C15AD
		// (set) Token: 0x060022F7 RID: 8951 RVA: 0x000C31B5 File Offset: 0x000C15B5
		public bool isPhysical
		{
			get
			{
				return this._isPhysical;
			}
			set
			{
				this._isPhysical = value;
				this.updateColliderEnabled();
			}
		}

		// Token: 0x1400007C RID: 124
		// (add) Token: 0x060022F8 RID: 8952 RVA: 0x000C31C4 File Offset: 0x000C15C4
		// (remove) Token: 0x060022F9 RID: 8953 RVA: 0x000C31FC File Offset: 0x000C15FC
		public event WheelAliveChangedHandler aliveChanged;

		// Token: 0x060022FA RID: 8954 RVA: 0x000C3232 File Offset: 0x000C1632
		public void askRepair()
		{
			if (this.isAlive)
			{
				return;
			}
			this.isAlive = true;
			this.vehicle.sendTireAliveMaskUpdate();
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x000C3254 File Offset: 0x000C1654
		public void askDamage()
		{
			if (!this.isAlive)
			{
				return;
			}
			this.isAlive = false;
			this.vehicle.sendTireAliveMaskUpdate();
			EffectManager.sendEffect(138, EffectManager.SMALL, this.wheel.transform.position, this.wheel.transform.up);
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x000C32AE File Offset: 0x000C16AE
		protected virtual void updateColliderEnabled()
		{
			if (this.wheel != null)
			{
				this.wheel.gameObject.SetActive(this.isPhysical && this.isAlive);
			}
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x000C32E8 File Offset: 0x000C16E8
		public void reset()
		{
			this.direction = 0f;
			this.steer = 0f;
			this.speed = 0f;
			if (this.wheel != null)
			{
				this.wheel.steerAngle = 0f;
				this.wheel.motorTorque = 0f;
				this.wheel.brakeTorque = this.vehicle.asset.brake * 0.25f;
				this.sidewaysFriction.stiffness = 0.25f;
				this.wheel.sidewaysFriction = this.sidewaysFriction;
				this.forwardFriction.stiffness = 0.25f;
				this.wheel.forwardFriction = this.forwardFriction;
			}
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000C33AC File Offset: 0x000C17AC
		public void simulate(float input_x, float input_y, bool inputBrake, float delta)
		{
			if (this.wheel == null)
			{
				return;
			}
			if (this.isSteered)
			{
				this.direction = input_x;
				this.steer = Mathf.Lerp(this.steer, Mathf.Lerp(this.vehicle.asset.steerMax, this.vehicle.asset.steerMin, this.vehicle.factor), 2f * delta);
			}
			float num = Mathf.Abs(input_y);
			if (this.isPowered)
			{
				if (input_y > 0f)
				{
					if (this.vehicle.asset.engine == EEngine.PLANE)
					{
						if (this.vehicle.speed < 0f)
						{
							this.speed = Mathf.Lerp(this.speed, this.vehicle.asset.speedMax * num / 2f, delta / 4f);
						}
						else
						{
							this.speed = Mathf.Lerp(this.speed, this.vehicle.asset.speedMax * num / 2f, delta / 8f);
						}
					}
					else if (this.vehicle.speed < 0f)
					{
						this.speed = Mathf.Lerp(this.speed, this.vehicle.asset.speedMax * num, 2f * delta);
					}
					else
					{
						this.speed = Mathf.Lerp(this.speed, this.vehicle.asset.speedMax * num, delta);
					}
				}
				else if (input_y < 0f)
				{
					if (this.vehicle.speed > 0f)
					{
						this.speed = Mathf.Lerp(this.speed, this.vehicle.asset.speedMin * num, 2f * delta);
					}
					else
					{
						this.speed = Mathf.Lerp(this.speed, this.vehicle.asset.speedMin * num, delta);
					}
				}
				else
				{
					this.speed = Mathf.Lerp(this.speed, 0f, delta);
				}
			}
			if (inputBrake)
			{
				this.speed = 0f;
				this.wheel.motorTorque = 0f;
				this.wheel.brakeTorque = this.vehicle.asset.brake * (1f - this.vehicle.slip * 0.5f);
			}
			else
			{
				this.wheel.brakeTorque = 0f;
			}
			RaycastHit raycastHit;
			this._isGrounded = PhysicsUtility.raycast(new Ray(this.wheel.transform.position, -this.wheel.transform.up), out raycastHit, this.wheel.suspensionDistance + this.wheel.radius, RayMasks.BLOCK_COLLISION, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x000C3698 File Offset: 0x000C1A98
		public void update(float delta)
		{
			if (this.wheel == null)
			{
				return;
			}
			this.wheel.steerAngle = Mathf.Lerp(this.wheel.steerAngle, this.direction * this.steer, 4f * delta);
			if (this.vehicle.asset.hasSleds)
			{
				this.sidewaysFriction.stiffness = Mathf.Lerp(this.wheel.sidewaysFriction.stiffness, 0.25f, 4f * delta);
				this.forwardFriction.stiffness = Mathf.Lerp(this.wheel.forwardFriction.stiffness, 0.25f, 4f * delta);
			}
			else
			{
				this.sidewaysFriction.stiffness = Mathf.Lerp(this.wheel.sidewaysFriction.stiffness, 1f - this.vehicle.slip * 0.75f, 4f * delta);
				this.forwardFriction.stiffness = Mathf.Lerp(this.wheel.forwardFriction.stiffness, 2f - this.vehicle.slip * 1.5f, 4f * delta);
			}
			this.wheel.sidewaysFriction = this.sidewaysFriction;
			this.wheel.forwardFriction = this.forwardFriction;
			if (this.speed > 0f)
			{
				if (this.vehicle.speed < 0f)
				{
					this.wheel.motorTorque = Mathf.Lerp(this.wheel.motorTorque, this.speed, 4f * delta);
				}
				else if (this.vehicle.speed < this.vehicle.asset.speedMax)
				{
					this.wheel.motorTorque = Mathf.Lerp(this.wheel.motorTorque, this.speed, 2f * delta);
				}
				else
				{
					this.wheel.motorTorque = Mathf.Lerp(this.wheel.motorTorque, this.speed / 2f, 2f * delta);
				}
			}
			else if (this.vehicle.speed > 0f)
			{
				this.wheel.motorTorque = Mathf.Lerp(this.wheel.motorTorque, this.speed, 4f * delta);
			}
			else if (this.vehicle.speed > this.vehicle.asset.speedMin)
			{
				this.wheel.motorTorque = Mathf.Lerp(this.wheel.motorTorque, this.speed, 2f * delta);
			}
			else
			{
				this.wheel.motorTorque = Mathf.Lerp(this.wheel.motorTorque, this.speed / 2f, 2f * delta);
			}
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x000C3994 File Offset: 0x000C1D94
		public void checkForTraps()
		{
			if (this.wheel == null)
			{
				return;
			}
			if (!this.isAlive)
			{
				return;
			}
			if (Provider.isServer && this.vehicle.asset != null && this.vehicle.asset.canTiresBeDamaged)
			{
				RaycastHit raycastHit;
				Physics.Raycast(new Ray(this.wheel.transform.position, -this.wheel.transform.up), out raycastHit, this.wheel.suspensionDistance + this.wheel.radius, RayMasks.BARRICADE);
				if (raycastHit.transform != null && raycastHit.transform.CompareTag("Barricade"))
				{
					InteractableTrapDamageTires component = raycastHit.transform.GetComponent<InteractableTrapDamageTires>();
					if (component != null)
					{
						this.askDamage();
					}
				}
			}
		}

		// Token: 0x040014FB RID: 5371
		private InteractableVehicle _vehicle;

		// Token: 0x040014FC RID: 5372
		private WheelCollider _wheel;

		// Token: 0x040014FD RID: 5373
		public Transform model;

		// Token: 0x040014FE RID: 5374
		public Quaternion rest;

		// Token: 0x040014FF RID: 5375
		private WheelFrictionCurve forwardFriction;

		// Token: 0x04001500 RID: 5376
		private WheelFrictionCurve sidewaysFriction;

		// Token: 0x04001501 RID: 5377
		private bool _isSteered;

		// Token: 0x04001502 RID: 5378
		private bool _isPowered;

		// Token: 0x04001503 RID: 5379
		private bool _isGrounded;

		// Token: 0x04001504 RID: 5380
		protected bool _isAlive;

		// Token: 0x04001505 RID: 5381
		private float direction;

		// Token: 0x04001506 RID: 5382
		private float steer;

		// Token: 0x04001507 RID: 5383
		private float speed;

		// Token: 0x04001508 RID: 5384
		protected bool _isPhysical;
	}
}
