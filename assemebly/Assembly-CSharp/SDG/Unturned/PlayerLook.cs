using System;
using SDG.Framework.Foliage;
using SDG.Framework.Utilities;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200062B RID: 1579
	public class PlayerLook : PlayerCaller
	{
		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x0011EDF4 File Offset: 0x0011D1F4
		private float heightLook
		{
			get
			{
				if (base.player.stance.stance == EPlayerStance.DRIVING || base.player.stance.stance == EPlayerStance.SITTING)
				{
					return PlayerLook.HEIGHT_LOOK_SIT;
				}
				if (base.player.stance.stance == EPlayerStance.STAND || base.player.stance.stance == EPlayerStance.SPRINT || base.player.stance.stance == EPlayerStance.CLIMB || base.player.stance.stance == EPlayerStance.SWIM || base.player.stance.stance == EPlayerStance.DRIVING || base.player.stance.stance == EPlayerStance.SITTING)
				{
					return PlayerLook.HEIGHT_LOOK_STAND;
				}
				if (base.player.stance.stance == EPlayerStance.CROUCH)
				{
					return PlayerLook.HEIGHT_LOOK_CROUCH;
				}
				if (base.player.stance.stance == EPlayerStance.PRONE)
				{
					return PlayerLook.HEIGHT_LOOK_PRONE;
				}
				return 0f;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x0011EEFC File Offset: 0x0011D2FC
		private float heightCamera
		{
			get
			{
				if (base.player.stance.stance == EPlayerStance.DRIVING || base.player.stance.stance == EPlayerStance.SITTING)
				{
					return PlayerLook.HEIGHT_CAMERA_SIT;
				}
				if (base.player.stance.stance == EPlayerStance.STAND || base.player.stance.stance == EPlayerStance.SPRINT || base.player.stance.stance == EPlayerStance.CLIMB || base.player.stance.stance == EPlayerStance.SWIM || base.player.stance.stance == EPlayerStance.DRIVING || base.player.stance.stance == EPlayerStance.SITTING)
				{
					return PlayerLook.HEIGHT_CAMERA_STAND;
				}
				if (base.player.stance.stance == EPlayerStance.CROUCH)
				{
					return PlayerLook.HEIGHT_CAMERA_CROUCH;
				}
				if (base.player.stance.stance == EPlayerStance.PRONE)
				{
					return PlayerLook.HEIGHT_CAMERA_PRONE;
				}
				return 0f;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x0011F001 File Offset: 0x0011D401
		public Camera characterCamera
		{
			get
			{
				return this._characterCamera;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002CF7 RID: 11511 RVA: 0x0011F009 File Offset: 0x0011D409
		public Camera scopeCamera
		{
			get
			{
				return this._scopeCamera;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06002CF8 RID: 11512 RVA: 0x0011F011 File Offset: 0x0011D411
		public Camera highlightCamera
		{
			get
			{
				return this._highlightCamera;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002CF9 RID: 11513 RVA: 0x0011F019 File Offset: 0x0011D419
		public bool isScopeActive
		{
			get
			{
				return this._isScopeActive;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06002CFA RID: 11514 RVA: 0x0011F021 File Offset: 0x0011D421
		public Transform aim
		{
			get
			{
				return this._aim;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06002CFB RID: 11515 RVA: 0x0011F029 File Offset: 0x0011D429
		public float pitch
		{
			get
			{
				return this._pitch;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06002CFC RID: 11516 RVA: 0x0011F031 File Offset: 0x0011D431
		public float yaw
		{
			get
			{
				return this._yaw;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06002CFD RID: 11517 RVA: 0x0011F039 File Offset: 0x0011D439
		public float look_x
		{
			get
			{
				return this._look_x;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06002CFE RID: 11518 RVA: 0x0011F041 File Offset: 0x0011D441
		public float look_y
		{
			get
			{
				return this._look_y;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002CFF RID: 11519 RVA: 0x0011F049 File Offset: 0x0011D449
		public float orbitPitch
		{
			get
			{
				return this._orbitPitch;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06002D00 RID: 11520 RVA: 0x0011F051 File Offset: 0x0011D451
		public float orbitYaw
		{
			get
			{
				return this._orbitYaw;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06002D01 RID: 11521 RVA: 0x0011F059 File Offset: 0x0011D459
		public bool isCam
		{
			get
			{
				return this.isOrbiting || this.isTracking || this.isLocking || this.isFocusing;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06002D02 RID: 11522 RVA: 0x0011F085 File Offset: 0x0011D485
		public EPlayerPerspective perspective
		{
			get
			{
				return this._perspective;
			}
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x0011F090 File Offset: 0x0011D490
		public void updateScope(EGraphicQuality quality)
		{
			if (quality == EGraphicQuality.OFF)
			{
				this.scopeCamera.targetTexture = null;
			}
			else if (quality == EGraphicQuality.LOW)
			{
				this.scopeCamera.targetTexture = (RenderTexture)Resources.Load("RenderTextures/Scope_Low");
			}
			else if (quality == EGraphicQuality.MEDIUM)
			{
				this.scopeCamera.targetTexture = (RenderTexture)Resources.Load("RenderTextures/Scope_Medium");
			}
			else if (quality == EGraphicQuality.HIGH)
			{
				this.scopeCamera.targetTexture = (RenderTexture)Resources.Load("RenderTextures/Scope_High");
			}
			else if (quality == EGraphicQuality.ULTRA)
			{
				this.scopeCamera.targetTexture = (RenderTexture)Resources.Load("RenderTextures/Scope_Ultra");
			}
			this.scopeCamera.enabled = (this.isScopeActive && this.scopeCamera.targetTexture != null && this.scopeVision == ELightingVision.NONE);
			if (base.player.equipment.asset != null && base.player.equipment.asset.type == EItemType.GUN)
			{
				base.player.equipment.useable.updateState(base.player.equipment.state);
			}
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x0011F1D4 File Offset: 0x0011D5D4
		public void enableScope(float zoom, ELightingVision vision)
		{
			this.scopeCamera.fieldOfView = zoom;
			this._isScopeActive = true;
			this.scopeVision = vision;
			this.scopeCamera.enabled = (this.scopeCamera.targetTexture != null && this.scopeVision == ELightingVision.NONE);
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x0011F228 File Offset: 0x0011D628
		public void disableScope()
		{
			this.scopeCamera.enabled = false;
			this._isScopeActive = false;
			this.scopeVision = ELightingVision.NONE;
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x0011F244 File Offset: 0x0011D644
		public void enableOverlay()
		{
			if (this.scopeVision == ELightingVision.NONE)
			{
				return;
			}
			if (this.scopeCamera.targetTexture != null)
			{
				return;
			}
			this.enableVision();
			this.isOverlayActive = true;
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x0011F278 File Offset: 0x0011D678
		public void setPerspective(EPlayerPerspective newPerspective)
		{
			this._perspective = newPerspective;
			if (this.perspective == EPlayerPerspective.FIRST)
			{
				MainCamera.instance.transform.parent = base.player.first;
				MainCamera.instance.transform.localPosition = Vector3.up * this.eyes;
				this.isOrbiting = false;
				this.isTracking = false;
				this.isLocking = false;
				this.isFocusing = false;
			}
			else
			{
				MainCamera.instance.transform.parent = base.player.transform;
			}
			GraphicsSettings.mainProfile.chromaticAberration.enabled = (this.perspective == EPlayerPerspective.THIRD && GraphicsSettings.chromaticAberration);
			GraphicsSettings.mainProfile.grain.enabled = (this.perspective == EPlayerPerspective.THIRD && GraphicsSettings.filmGrain);
			GraphicsSettings.viewProfile.chromaticAberration.enabled = (this.perspective == EPlayerPerspective.FIRST && GraphicsSettings.chromaticAberration);
			GraphicsSettings.viewProfile.grain.enabled = (this.perspective == EPlayerPerspective.FIRST && GraphicsSettings.filmGrain);
			if (this.onPerspectiveUpdated != null)
			{
				this.onPerspectiveUpdated(this.perspective);
			}
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x0011F3B1 File Offset: 0x0011D7B1
		private void enableVision()
		{
			this.tempVision = LevelLighting.vision;
			LevelLighting.vision = this.scopeVision;
			LevelLighting.updateLighting();
			LevelLighting.updateLocal();
			PlayerLifeUI.updateGrayscale();
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x0011F3D8 File Offset: 0x0011D7D8
		public void disableOverlay()
		{
			if (this.perspective != EPlayerPerspective.FIRST)
			{
				this.tempVision = ELightingVision.NONE;
			}
			if (!this.isOverlayActive)
			{
				return;
			}
			this.disableVision();
			this.isOverlayActive = false;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x0011F405 File Offset: 0x0011D805
		private void disableVision()
		{
			LevelLighting.vision = this.tempVision;
			LevelLighting.updateLighting();
			LevelLighting.updateLocal();
			PlayerLifeUI.updateGrayscale();
			this.tempVision = ELightingVision.NONE;
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x0011F428 File Offset: 0x0011D828
		public void enableZoom(float zoom)
		{
			this.fov = zoom;
			this.isZoomed = true;
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x0011F438 File Offset: 0x0011D838
		public void disableZoom()
		{
			this.fov = 0f;
			this.isZoomed = false;
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x0011F44C File Offset: 0x0011D84C
		public void updateRot()
		{
			if (this.pitch < 0f)
			{
				this.angle = 0;
			}
			else if (this.pitch > 180f)
			{
				this.angle = 180;
			}
			else
			{
				this.angle = (byte)this.pitch;
			}
			this.rot = MeasurementTool.angleToByte(this.yaw);
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x0011F4B4 File Offset: 0x0011D8B4
		public void updateLook()
		{
			this.sensitivity = 1f;
			this._pitch = 90f;
			this._yaw = base.transform.localRotation.eulerAngles.y;
			this.updateRot();
			if (base.channel.isOwner && this.perspective == EPlayerPerspective.FIRST)
			{
				MainCamera.instance.transform.localRotation = Quaternion.Euler(this.pitch - 90f, 0f, 0f);
				MainCamera.instance.transform.localPosition = Vector3.up * this.eyes;
			}
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x0011F564 File Offset: 0x0011D964
		public void recoil(float x, float y, float h, float v)
		{
			this._yaw += x;
			this._pitch -= y;
			this.recoil_x += x * h;
			this.recoil_y += y * v;
			if ((double)(Time.realtimeSinceStartup - this.lastRecoil) < 0.2)
			{
				this.recoil_x *= 0.6f;
				this.recoil_y *= 0.6f;
			}
			this.lastRecoil = Time.realtimeSinceStartup;
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x0011F5F8 File Offset: 0x0011D9F8
		public void simulate(float look_x, float look_y, float delta)
		{
			this._pitch = look_y;
			this._yaw = look_x;
			this.checkPitch();
			this.updateRot();
			if (base.player.stance.stance == EPlayerStance.DRIVING || base.player.stance.stance == EPlayerStance.SITTING)
			{
				base.transform.localRotation = Quaternion.identity;
			}
			else
			{
				base.transform.localRotation = Quaternion.Euler(0f, this.yaw, 0f);
			}
			if (base.player.movement.getVehicle() != null && base.player.movement.getVehicle().passengers[(int)base.player.movement.getSeat()].turret != null)
			{
				Passenger passenger = base.player.movement.getVehicle().passengers[(int)base.player.movement.getSeat()];
				if (passenger.turretYaw != null)
				{
					passenger.turretYaw.localRotation = passenger.rotationYaw * Quaternion.Euler(0f, this.yaw, 0f);
				}
				if (passenger.turretPitch != null)
				{
					passenger.turretPitch.localRotation = passenger.rotationPitch * Quaternion.Euler(this.pitch - 90f, 0f, 0f);
				}
			}
			this.updateAim(delta);
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x0011F778 File Offset: 0x0011DB78
		private void checkPitch()
		{
			if (base.player.stance.stance == EPlayerStance.DRIVING || base.player.stance.stance == EPlayerStance.SITTING)
			{
				if (base.player.movement.getVehicle() != null && base.player.movement.getVehicle().passengers[(int)base.player.movement.getSeat()].turret != null)
				{
					Passenger passenger = base.player.movement.getVehicle().passengers[(int)base.player.movement.getSeat()];
					if (this.pitch < passenger.turret.pitchMin)
					{
						this._pitch = passenger.turret.pitchMin;
					}
					else if (this.pitch > passenger.turret.pitchMax)
					{
						this._pitch = passenger.turret.pitchMax;
					}
				}
				else if (this.pitch < PlayerLook.MIN_ANGLE_SIT)
				{
					this._pitch = PlayerLook.MIN_ANGLE_SIT;
				}
				else if (this.pitch > PlayerLook.MAX_ANGLE_SIT)
				{
					this._pitch = PlayerLook.MAX_ANGLE_SIT;
				}
			}
			else if (base.player.stance.stance == EPlayerStance.STAND || base.player.stance.stance == EPlayerStance.SPRINT)
			{
				if (this.pitch < PlayerLook.MIN_ANGLE_STAND)
				{
					this._pitch = PlayerLook.MIN_ANGLE_STAND;
				}
				else if (this.pitch > PlayerLook.MAX_ANGLE_STAND)
				{
					this._pitch = PlayerLook.MAX_ANGLE_STAND;
				}
			}
			else if (base.player.stance.stance == EPlayerStance.CLIMB)
			{
				if (this.pitch < PlayerLook.MIN_ANGLE_CLIMB)
				{
					this._pitch = PlayerLook.MIN_ANGLE_CLIMB;
				}
				else if (this.pitch > PlayerLook.MAX_ANGLE_CLIMB)
				{
					this._pitch = PlayerLook.MAX_ANGLE_CLIMB;
				}
			}
			else if (base.player.stance.stance == EPlayerStance.SWIM)
			{
				if (this.pitch < PlayerLook.MIN_ANGLE_SWIM)
				{
					this._pitch = PlayerLook.MIN_ANGLE_SWIM;
				}
				else if (this.pitch > PlayerLook.MAX_ANGLE_SWIM)
				{
					this._pitch = PlayerLook.MAX_ANGLE_SWIM;
				}
			}
			else if (base.player.stance.stance == EPlayerStance.CROUCH)
			{
				if (this.pitch < PlayerLook.MIN_ANGLE_CROUCH)
				{
					this._pitch = PlayerLook.MIN_ANGLE_CROUCH;
				}
				else if (this.pitch > PlayerLook.MAX_ANGLE_CROUCH)
				{
					this._pitch = PlayerLook.MAX_ANGLE_CROUCH;
				}
			}
			else if (base.player.stance.stance == EPlayerStance.PRONE)
			{
				if (this.pitch < PlayerLook.MIN_ANGLE_PRONE)
				{
					this._pitch = PlayerLook.MIN_ANGLE_PRONE;
				}
				else if (this.pitch > PlayerLook.MAX_ANGLE_PRONE)
				{
					this._pitch = PlayerLook.MAX_ANGLE_PRONE;
				}
			}
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x0011FA7C File Offset: 0x0011DE7C
		public void updateAim(float delta)
		{
			if (base.player.movement.getVehicle() != null && base.player.movement.getVehicle().passengers[(int)base.player.movement.getSeat()].turret != null && base.player.movement.getVehicle().passengers[(int)base.player.movement.getSeat()].turret.useAimCamera)
			{
				Passenger passenger = base.player.movement.getVehicle().passengers[(int)base.player.movement.getSeat()];
				if (passenger.turretAim != null)
				{
					this.aim.position = passenger.turretAim.position;
					this.aim.rotation = passenger.turretAim.rotation;
				}
			}
			else
			{
				this.aim.localPosition = Vector3.Lerp(this.aim.localPosition, Vector3.up * this.heightLook, 4f * delta);
				if (base.player.stance.stance == EPlayerStance.SITTING || base.player.stance.stance == EPlayerStance.DRIVING)
				{
					this.aim.parent.localRotation = Quaternion.Euler(0f, this.yaw, 0f);
				}
				else
				{
					this.aim.parent.localRotation = Quaternion.Lerp(this.aim.parent.localRotation, Quaternion.Euler(0f, 0f, (float)base.player.animator.lean * HumanAnimator.LEAN), 4f * delta);
				}
				this.aim.localRotation = Quaternion.Euler(this.pitch - 90f + base.player.animator.viewSway.x, base.player.animator.viewSway.y, 0f);
			}
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x0011FC98 File Offset: 0x0011E098
		private void onDamaged(byte damage)
		{
			if (damage > 25)
			{
				damage = 25;
			}
			if ((double)UnityEngine.Random.value < 0.5)
			{
				this.dodge -= (float)(2 * damage) * (1f - base.player.skills.mastery(1, 3) * 0.75f);
			}
			else
			{
				this.dodge += (float)(2 * damage) * (1f - base.player.skills.mastery(1, 3) * 0.75f);
			}
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x0011FD2C File Offset: 0x0011E12C
		private void onVisionUpdated(bool isViewing)
		{
			if (isViewing)
			{
				this.warp_x = (((double)UnityEngine.Random.value >= 0.25) ? 1 : -1);
				this.warp_y = (((double)UnityEngine.Random.value >= 0.25) ? 1 : -1);
			}
			else
			{
				this.warp_x = 1;
				this.warp_y = 1;
			}
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x0011FD94 File Offset: 0x0011E194
		private void onLifeUpdated(bool isDead)
		{
			if (isDead)
			{
				PlayerLook.killcam = base.transform.rotation.eulerAngles.y;
			}
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x0011FDC8 File Offset: 0x0011E1C8
		private void onSeated(bool isDriver, bool inVehicle, bool wasVehicle, InteractableVehicle oldVehicle, InteractableVehicle newVehicle)
		{
			if (!wasVehicle)
			{
				this._orbitPitch = 22.5f;
				this._orbitYaw = 0f;
			}
			if (Provider.cameraMode == ECameraMode.VEHICLE && this.perspective == EPlayerPerspective.THIRD && !isDriver)
			{
				this.setPerspective(EPlayerPerspective.FIRST);
			}
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x0011FE18 File Offset: 0x0011E218
		private void Update()
		{
			if (base.channel.isOwner)
			{
				if (base.channel.owner.isAdmin && this.perspective == EPlayerPerspective.THIRD && Input.GetKey(KeyCode.LeftShift))
				{
					if (Input.GetKeyDown(KeyCode.F1))
					{
						this.isOrbiting = !this.isOrbiting;
						if (this.isOrbiting && !this.isTracking && !this.isLocking && !this.isFocusing)
						{
							this.isTracking = true;
						}
					}
					if (Input.GetKeyDown(KeyCode.F2))
					{
						this.isTracking = !this.isTracking;
						if (this.isTracking)
						{
							this.isLocking = false;
							this.isFocusing = false;
						}
					}
					if (Input.GetKeyDown(KeyCode.F3))
					{
						this.isLocking = !this.isLocking;
						if (this.isLocking)
						{
							this.isTracking = false;
							this.isFocusing = false;
							this.lockPosition = base.player.first.position;
						}
					}
					if (Input.GetKeyDown(KeyCode.F4))
					{
						this.isFocusing = !this.isFocusing;
						if (this.isFocusing)
						{
							this.isTracking = false;
							this.isLocking = false;
							this.lockPosition = base.player.first.position;
						}
					}
					if (Input.GetKeyDown(KeyCode.F5))
					{
						this.isSmoothing = !this.isSmoothing;
					}
					if (Input.GetKeyDown(KeyCode.F6))
					{
						if (PlayerWorkzoneUI.active)
						{
							PlayerWorkzoneUI.close();
							PlayerLifeUI.open();
						}
						else
						{
							PlayerWorkzoneUI.open();
							PlayerLifeUI.close();
						}
					}
				}
				this.eyes = Mathf.Lerp(this.eyes, this.heightLook, 4f * Time.deltaTime);
				Camera instance = MainCamera.instance;
				if (!base.player.life.isDead && !PlayerUI.window.showCursor && Input.GetKeyDown(ControlsSettings.perspective) && (Provider.cameraMode == ECameraMode.BOTH || (Provider.cameraMode == ECameraMode.VEHICLE && base.player.stance.stance == EPlayerStance.DRIVING)))
				{
					EPlayerPerspective perspective;
					if (this.perspective == EPlayerPerspective.FIRST)
					{
						perspective = EPlayerPerspective.THIRD;
					}
					else
					{
						perspective = EPlayerPerspective.FIRST;
					}
					this.setPerspective(perspective);
				}
				if (this.isCam)
				{
					instance.fieldOfView = OptionsSettings.view;
				}
				else
				{
					instance.fieldOfView = Mathf.Lerp(instance.fieldOfView, (this.fov <= 1f) ? (OptionsSettings.view + (float)((base.player.stance.stance != EPlayerStance.SPRINT) ? 0 : 10)) : this.fov, 8f * Time.deltaTime);
				}
				this.highlightCamera.fieldOfView = instance.fieldOfView;
				this._look_x = 0f;
				this._look_y = 0f;
				if (!PlayerUI.window.showCursor && !this.isIgnoringInput)
				{
					if (this.isOrbiting)
					{
						if (!base.player.workzone.isBuilding || Input.GetKey(ControlsSettings.secondary))
						{
							this._orbitYaw += ControlsSettings.look * Input.GetAxis("mouse_x") * (float)this.warp_x;
							if (ControlsSettings.invert)
							{
								this._orbitPitch += ControlsSettings.look * Input.GetAxis("mouse_y") * (float)this.warp_y;
							}
							else
							{
								this._orbitPitch -= ControlsSettings.look * Input.GetAxis("mouse_y") * (float)this.warp_y;
							}
						}
					}
					else
					{
						if (this.perspective == EPlayerPerspective.FIRST || this.isTracking || this.isLocking || this.isFocusing)
						{
							this._look_x = ControlsSettings.look * Input.GetAxis("mouse_x") * (float)this.warp_x;
							this._look_y = ControlsSettings.look * -Input.GetAxis("mouse_y") * (float)this.warp_y;
						}
						if (Input.GetKey(ControlsSettings.rollLeft))
						{
							this._look_x = ((!(base.player.movement.getVehicle() != null)) ? -1f : (-base.player.movement.getVehicle().asset.airTurnResponsiveness));
						}
						else if (Input.GetKey(ControlsSettings.rollRight))
						{
							this._look_x = ((!(base.player.movement.getVehicle() != null)) ? 1f : base.player.movement.getVehicle().asset.airTurnResponsiveness);
						}
						if (Input.GetKey(ControlsSettings.pitchUp))
						{
							this._look_y = ((!(base.player.movement.getVehicle() != null)) ? -1f : (-base.player.movement.getVehicle().asset.airTurnResponsiveness));
						}
						else if (Input.GetKey(ControlsSettings.pitchDown))
						{
							this._look_y = ((!(base.player.movement.getVehicle() != null)) ? 1f : base.player.movement.getVehicle().asset.airTurnResponsiveness);
						}
						if (ControlsSettings.invertFlight)
						{
							this._look_y *= -1f;
						}
						if (base.player.movement.getVehicle() != null && this.perspective == EPlayerPerspective.THIRD)
						{
							this._orbitYaw += ControlsSettings.look * Input.GetAxis("mouse_x") * (float)this.warp_x;
							this._orbitYaw = this.orbitYaw % 360f;
						}
						else if (base.player.movement.getVehicle() == null || !base.player.movement.getVehicle().asset.hasLockMouse || !base.player.movement.getVehicle().isDriver)
						{
							this._yaw += ControlsSettings.look * ((this.perspective != EPlayerPerspective.FIRST) ? 1f : this.sensitivity) * Input.GetAxis("mouse_x") * (float)this.warp_x;
							this._yaw = this.yaw % 360f;
						}
						if (base.player.movement.getVehicle() != null && this.perspective == EPlayerPerspective.THIRD)
						{
							if (ControlsSettings.invert)
							{
								this._orbitPitch += ControlsSettings.look * Input.GetAxis("mouse_y") * (float)this.warp_y;
							}
							else
							{
								this._orbitPitch -= ControlsSettings.look * Input.GetAxis("mouse_y") * (float)this.warp_y;
							}
						}
						else if (base.player.movement.getVehicle() == null || !base.player.movement.getVehicle().asset.hasLockMouse || !base.player.movement.getVehicle().isDriver)
						{
							if (ControlsSettings.invert)
							{
								this._pitch += ControlsSettings.look * ((this.perspective != EPlayerPerspective.FIRST) ? 1f : this.sensitivity) * Input.GetAxis("mouse_y") * (float)this.warp_y;
							}
							else
							{
								this._pitch -= ControlsSettings.look * ((this.perspective != EPlayerPerspective.FIRST) ? 1f : this.sensitivity) * Input.GetAxis("mouse_y") * (float)this.warp_y;
							}
						}
					}
				}
				if (float.IsInfinity(this.yaw) || float.IsNaN(this.yaw))
				{
					this._yaw = 0f;
				}
				if (float.IsInfinity(this.pitch) || float.IsNaN(this.pitch))
				{
					this._pitch = 0f;
				}
				if (float.IsInfinity(this.orbitYaw) || float.IsNaN(this.orbitYaw))
				{
					this._orbitYaw = 0f;
				}
				if (float.IsInfinity(this.orbitPitch) || float.IsNaN(this.orbitPitch))
				{
					this._orbitPitch = 0f;
				}
				this._yaw -= Mathf.Lerp(0f, this.recoil_x, 4f * Time.deltaTime);
				this._pitch += Mathf.Lerp(0f, this.recoil_y, 4f * Time.deltaTime);
				this.recoil_x = Mathf.Lerp(this.recoil_x, 0f, 4f * Time.deltaTime);
				this.recoil_y = Mathf.Lerp(this.recoil_y, 0f, 4f * Time.deltaTime);
				this.dodge = Mathf.LerpAngle(this.dodge, 0f, 4f * Time.deltaTime);
				this.checkPitch();
				if (this.orbitPitch > 90f)
				{
					this._orbitPitch = 90f;
				}
				else if (this.orbitPitch < -90f)
				{
					this._orbitPitch = -90f;
				}
				PlayerLook._characterYaw = Mathf.Lerp(PlayerLook._characterYaw, PlayerLook.characterYaw + 180f, 4f * Time.deltaTime);
				this.characterCamera.transform.rotation = Quaternion.Euler(20f, PlayerLook._characterYaw, 0f);
				this.characterCamera.transform.position = base.player.character.position - this.characterCamera.transform.forward * 3.5f + Vector3.up * PlayerLook.characterHeight;
				if (base.player.life.isDead)
				{
					PlayerLook.killcam += -16f * Time.deltaTime;
					instance.transform.rotation = Quaternion.Lerp(instance.transform.rotation, Quaternion.Euler(32f, PlayerLook.killcam, 0f), 2f * Time.deltaTime);
				}
				else
				{
					if ((base.player.stance.stance == EPlayerStance.DRIVING || base.player.stance.stance == EPlayerStance.SITTING) && this.perspective == EPlayerPerspective.THIRD)
					{
						instance.transform.localRotation = Quaternion.Euler(this.orbitPitch, this.orbitYaw, 0f);
					}
					else if (base.player.stance.stance == EPlayerStance.DRIVING)
					{
						if (this.yaw < -160f)
						{
							this._yaw = -160f;
						}
						else if (this.yaw > 160f)
						{
							this._yaw = 160f;
						}
						instance.transform.localRotation = Quaternion.Euler(this.pitch - 90f, this.yaw / 10f, 0f);
						instance.transform.Rotate(base.transform.up, this.yaw, Space.World);
					}
					else if (base.player.stance.stance == EPlayerStance.SITTING)
					{
						if (base.player.movement.getVehicle() != null && base.player.movement.getVehicle().passengers[(int)base.player.movement.getSeat()].turret != null)
						{
							Passenger passenger = base.player.movement.getVehicle().passengers[(int)base.player.movement.getSeat()];
							if (this.yaw < passenger.turret.yawMin)
							{
								this._yaw = passenger.turret.yawMin;
							}
							else if (this.yaw > passenger.turret.yawMax)
							{
								this._yaw = passenger.turret.yawMax;
							}
						}
						else if (this.yaw < -90f)
						{
							this._yaw = -90f;
						}
						else if (this.yaw > 90f)
						{
							this._yaw = 90f;
						}
						instance.transform.localRotation = Quaternion.Euler(this.pitch - 90f + base.player.animator.viewSway.x, base.player.animator.viewSway.y, 0f);
						instance.transform.Rotate(base.transform.up, this.yaw, Space.World);
					}
					else
					{
						if (this.perspective == EPlayerPerspective.FIRST)
						{
							instance.transform.localRotation = Quaternion.Euler(this.pitch - 90f + base.player.animator.viewSway.x, base.player.animator.viewSway.y, this.dodge);
						}
						else
						{
							instance.transform.localRotation = Quaternion.Euler(this.pitch - 90f + base.player.animator.viewSway.x, base.player.animator.shoulder * -5f + base.player.animator.viewSway.y, 0f);
						}
						base.transform.localRotation = Quaternion.Euler(0f, this.yaw, 0f);
					}
					if (this.isCam)
					{
						if (this.isFocusing)
						{
							if (this.isSmoothing)
							{
								this.smoothRotation = Quaternion.Lerp(this.smoothRotation, Quaternion.LookRotation(base.player.first.position + Vector3.up - (this.lockPosition + this.orbitPosition)), 4f * Time.deltaTime);
								instance.transform.rotation = this.smoothRotation;
							}
							else
							{
								instance.transform.rotation = Quaternion.LookRotation(base.player.first.position + Vector3.up - (this.lockPosition + this.orbitPosition));
							}
						}
						else if (this.isSmoothing)
						{
							this.smoothRotation = Quaternion.Lerp(this.smoothRotation, Quaternion.Euler(this.orbitPitch, this.orbitYaw, 0f), 4f * Time.deltaTime);
							instance.transform.rotation = this.smoothRotation;
						}
						else
						{
							instance.transform.rotation = Quaternion.Euler(this.orbitPitch, this.orbitYaw, 0f);
						}
					}
				}
				if (base.player.life.isDead)
				{
					this.cam = instance.transform.forward * -4f;
					PhysicsUtility.raycast(new Ray(base.player.first.position + Vector3.up, this.cam), out this.hit, 4f, RayMasks.BLOCK_KILLCAM, QueryTriggerInteraction.UseGlobal);
					if (this.hit.transform != null)
					{
						this.cam = this.hit.point + this.cam.normalized * -0.5f;
					}
					else
					{
						this.cam = base.player.first.position + Vector3.up + this.cam;
					}
					instance.transform.position = this.cam;
				}
				else
				{
					if (this.isCam)
					{
						if (this.isLocking || this.isFocusing)
						{
							instance.transform.position = this.lockPosition + this.orbitPosition;
						}
						else if (this.isOrbiting || this.isTracking)
						{
							if (this.isSmoothing)
							{
								this.smoothPosition = Vector3.Lerp(this.smoothPosition, this.orbitPosition, 4f * Time.deltaTime);
								instance.transform.position = base.player.first.position + this.smoothPosition;
							}
							else
							{
								instance.transform.position = base.player.first.position + this.orbitPosition;
							}
						}
					}
					else if ((base.player.stance.stance == EPlayerStance.DRIVING || base.player.stance.stance == EPlayerStance.SITTING) && this.perspective == EPlayerPerspective.THIRD)
					{
						float num = base.player.movement.getVehicle().asset.camFollowDistance + Mathf.Abs(base.player.movement.getVehicle().spedometer) * 0.1f;
						this.cam = instance.transform.forward * -num;
						PhysicsUtility.raycast(new Ray(base.player.first.transform.position + Vector3.up * this.eyes, this.cam), out this.hit, num, RayMasks.BLOCK_VEHICLECAM, QueryTriggerInteraction.UseGlobal);
						if (this.hit.transform != null)
						{
							this.cam = this.hit.point + this.cam.normalized * -0.5f;
						}
						else
						{
							Transform transform = base.player.movement.getVehicle().transform.FindChild("Camera_Focus");
							if (transform != null)
							{
								this.cam = transform.position + this.cam;
							}
							else
							{
								this.cam = base.player.first.transform.position + Vector3.up * this.eyes + this.cam;
							}
						}
						PhysicsUtility.raycast(new Ray(this.cam, instance.transform.right), out this.hit, 0.5f, RayMasks.BLOCK_PLAYERCAM, QueryTriggerInteraction.UseGlobal);
						if (this.hit.transform != null)
						{
							this.cam = this.hit.point + instance.transform.right * -0.5f;
						}
						else
						{
							PhysicsUtility.raycast(new Ray(this.cam, -instance.transform.right), out this.hit, 0.5f, RayMasks.BLOCK_PLAYERCAM, QueryTriggerInteraction.UseGlobal);
							if (this.hit.transform != null)
							{
								this.cam = this.hit.point + instance.transform.right * 0.5f;
							}
						}
						PhysicsUtility.raycast(new Ray(this.cam, instance.transform.up), out this.hit, 0.5f, RayMasks.BLOCK_PLAYERCAM, QueryTriggerInteraction.UseGlobal);
						if (this.hit.transform != null)
						{
							this.cam = this.hit.point + instance.transform.up * -0.5f;
						}
						else
						{
							PhysicsUtility.raycast(new Ray(this.cam, -instance.transform.up), out this.hit, 0.5f, RayMasks.BLOCK_PLAYERCAM, QueryTriggerInteraction.UseGlobal);
							if (this.hit.transform != null)
							{
								this.cam = this.hit.point + instance.transform.up * 0.5f;
							}
						}
						instance.transform.position = this.cam;
					}
					else if (base.player.stance.stance == EPlayerStance.DRIVING)
					{
						if (this.yaw > 0f)
						{
							instance.transform.localPosition = Vector3.Lerp(instance.transform.localPosition, Vector3.up * (this.heightLook + base.player.movement.getVehicle().asset.camDriverOffset) - Vector3.left * this.yaw / 360f, 4f * Time.deltaTime);
						}
						else
						{
							instance.transform.localPosition = Vector3.Lerp(instance.transform.localPosition, Vector3.up * (this.heightLook + base.player.movement.getVehicle().asset.camDriverOffset) - Vector3.left * this.yaw / 240f, 4f * Time.deltaTime);
						}
					}
					else if (this.perspective == EPlayerPerspective.FIRST)
					{
						instance.transform.localPosition = Vector3.up * this.eyes;
					}
					else
					{
						if (Provider.modeConfigData.Gameplay.Allow_Shoulder_Camera)
						{
							this.cam = instance.transform.forward * -1.5f + instance.transform.up * 0.25f + instance.transform.right * base.player.animator.shoulder * 1f;
						}
						else
						{
							this.cam = instance.transform.forward * -1.5f + instance.transform.up * 0.5f + instance.transform.right * base.player.animator.shoulder2 * 0.5f;
						}
						PhysicsUtility.raycast(new Ray(base.player.first.position + Vector3.up * this.eyes, this.cam), out this.hit, 2f, RayMasks.BLOCK_PLAYERCAM, QueryTriggerInteraction.UseGlobal);
						if (this.hit.transform != null)
						{
							Vector3 normalized = this.cam.normalized;
							RaycastHit raycastHit;
							PhysicsUtility.raycast(new Ray(this.hit.point, -normalized), out raycastHit, 1f, RayMasks.BLOCK_PLAYERCAM, QueryTriggerInteraction.UseGlobal);
							if (raycastHit.transform != null)
							{
								this.cam = base.player.first.position + Vector3.up * this.eyes;
							}
							else
							{
								this.cam = this.hit.point + normalized * -0.5f;
							}
						}
						else
						{
							this.cam = base.player.first.position + Vector3.up * this.eyes + this.cam;
						}
						PhysicsUtility.raycast(new Ray(this.cam, instance.transform.right * Mathf.Sign(base.player.animator.shoulder)), out this.hit, 0.5f, RayMasks.BLOCK_PLAYERCAM, QueryTriggerInteraction.UseGlobal);
						if (this.hit.transform != null)
						{
							this.cam = this.hit.point + instance.transform.right * Mathf.Sign(base.player.animator.shoulder) * -0.5f;
						}
						PhysicsUtility.raycast(new Ray(this.cam, instance.transform.up), out this.hit, 0.5f, RayMasks.BLOCK_PLAYERCAM, QueryTriggerInteraction.UseGlobal);
						if (this.hit.transform != null)
						{
							this.cam = this.hit.point + instance.transform.up * -0.5f;
						}
						else
						{
							PhysicsUtility.raycast(new Ray(this.cam, -instance.transform.up), out this.hit, 0.5f, RayMasks.BLOCK_PLAYERCAM, QueryTriggerInteraction.UseGlobal);
							if (this.hit.transform != null)
							{
								this.cam = this.hit.point + instance.transform.up * 0.5f;
							}
						}
						instance.transform.position = this.cam;
					}
					PlayerLook.characterHeight = Mathf.Lerp(PlayerLook.characterHeight, this.heightCamera, 4f * Time.deltaTime);
				}
				if (base.player.movement.getVehicle() != null && base.player.movement.getVehicle().asset.engine == EEngine.PLANE && base.player.movement.getVehicle().spedometer > 16f)
				{
					LevelLighting.updateLocal(instance.transform.position, Mathf.Lerp(0f, 1f, (base.player.movement.getVehicle().spedometer - 16f) / 8f), base.player.movement.effectNode);
				}
				else if (base.player.movement.getVehicle() != null && (base.player.movement.getVehicle().asset.engine == EEngine.HELICOPTER || base.player.movement.getVehicle().asset.engine == EEngine.BLIMP) && base.player.movement.getVehicle().spedometer > 4f)
				{
					LevelLighting.updateLocal(instance.transform.position, Mathf.Lerp(0f, 1f, (base.player.movement.getVehicle().spedometer - 8f) / 8f), base.player.movement.effectNode);
				}
				else
				{
					LevelLighting.updateLocal(instance.transform.position, 0f, base.player.movement.effectNode);
				}
				base.player.animator.viewmodelLock.rotation = instance.transform.rotation;
				if (this.isScopeActive && this.scopeCamera.targetTexture != null && this.scopeVision != ELightingVision.NONE)
				{
					this.enableVision();
					this.scopeCamera.Render();
					this.disableVision();
				}
				if (base.player.movement.getVehicle() != null && base.player.movement.getVehicle().passengers[(int)base.player.movement.getSeat()].turret != null)
				{
					Passenger passenger2 = base.player.movement.getVehicle().passengers[(int)base.player.movement.getSeat()];
					if (passenger2.turretYaw != null)
					{
						passenger2.turretYaw.localRotation = passenger2.rotationYaw * Quaternion.Euler(0f, this.yaw, 0f);
					}
					if (passenger2.turretPitch != null)
					{
						passenger2.turretPitch.localRotation = passenger2.rotationPitch * Quaternion.Euler(this.pitch - 90f, 0f, 0f);
					}
					if (this.perspective == EPlayerPerspective.FIRST && base.player.movement.getVehicle().passengers[(int)base.player.movement.getSeat()].turret.useAimCamera)
					{
						instance.transform.position = passenger2.turretAim.position;
						instance.transform.rotation = passenger2.turretAim.rotation;
					}
				}
				if (FoliageSettings.drawFocus)
				{
					if (this.isZoomed || (this.isScopeActive && this.scopeCamera.targetTexture != null))
					{
						FoliageSystem.isFocused = true;
						RaycastHit raycastHit2;
						if (Physics.Raycast(MainCamera.instance.transform.position, MainCamera.instance.transform.forward, out raycastHit2, FoliageSettings.focusDistance, RayMasks.FOLIAGE_FOCUS))
						{
							FoliageSystem.focusPosition = raycastHit2.point;
							if (this.scopeCamera.targetTexture != null)
							{
								FoliageSystem.focusCamera = this.scopeCamera;
							}
							else
							{
								FoliageSystem.focusCamera = MainCamera.instance;
							}
						}
					}
					else
					{
						FoliageSystem.isFocused = false;
					}
				}
			}
			else if (!Provider.isServer)
			{
				if (base.player.stance.stance == EPlayerStance.DRIVING || base.player.stance.stance == EPlayerStance.SITTING)
				{
					base.transform.localRotation = Quaternion.identity;
				}
				else
				{
					this._pitch = base.player.movement.snapshot.pitch;
					this._yaw = base.player.movement.snapshot.yaw;
					base.transform.localRotation = Quaternion.Euler(0f, this.yaw, 0f);
				}
				if (base.player.movement.getVehicle() != null && base.player.movement.getVehicle().passengers[(int)base.player.movement.getSeat()].turret != null)
				{
					Passenger passenger3 = base.player.movement.getVehicle().passengers[(int)base.player.movement.getSeat()];
					if (passenger3.turretYaw != null)
					{
						passenger3.turretYaw.localRotation = passenger3.rotationYaw * Quaternion.Euler(0f, base.player.movement.snapshot.yaw, 0f);
					}
					if (passenger3.turretPitch != null)
					{
						passenger3.turretPitch.localRotation = passenger3.rotationPitch * Quaternion.Euler(base.player.movement.snapshot.pitch - 90f, 0f, 0f);
					}
				}
			}
			if (!Dedicator.isDedicated)
			{
				this.updateAim(Time.deltaTime);
			}
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x00121DD0 File Offset: 0x001201D0
		private void Start()
		{
			this._aim = base.transform.FindChild("Aim").FindChild("Fire");
			this.updateLook();
			this.warp_x = 1;
			this.warp_y = 1;
			if (base.channel.isOwner)
			{
				if (Provider.cameraMode == ECameraMode.THIRD)
				{
					this._perspective = EPlayerPerspective.THIRD;
					MainCamera.instance.transform.parent = base.player.transform;
				}
				else
				{
					this._perspective = EPlayerPerspective.FIRST;
				}
				MainCamera.instance.fieldOfView = OptionsSettings.view;
				PlayerLook.characterHeight = 0f;
				PlayerLook._characterYaw = 180f;
				PlayerLook.characterYaw = 0f;
				this.dodge = 0f;
				if (base.player.character != null)
				{
					this._characterCamera = base.player.character.FindChild("Camera").GetComponent<Camera>();
				}
				this._scopeCamera = MainCamera.instance.transform.FindChild("Scope").GetComponent<Camera>();
				this.scopeCamera.layerCullDistances = MainCamera.instance.layerCullDistances;
				this.scopeCamera.layerCullSpherical = MainCamera.instance.layerCullSpherical;
				this.scopeCamera.fieldOfView = 10f;
				this._highlightCamera = MainCamera.instance.transform.FindChild("HighlightCamera").GetComponent<Camera>();
				this.highlightCamera.fieldOfView = MainCamera.instance.fieldOfView;
				LevelLighting.updateLighting();
				PlayerLife life = base.player.life;
				life.onVisionUpdated = (VisionUpdated)Delegate.Combine(life.onVisionUpdated, new VisionUpdated(this.onVisionUpdated));
				PlayerLife life2 = base.player.life;
				life2.onLifeUpdated = (LifeUpdated)Delegate.Combine(life2.onLifeUpdated, new LifeUpdated(this.onLifeUpdated));
				PlayerLife life3 = base.player.life;
				life3.onDamaged = (Damaged)Delegate.Combine(life3.onDamaged, new Damaged(this.onDamaged));
				PlayerMovement movement = base.player.movement;
				movement.onSeated = (Seated)Delegate.Combine(movement.onSeated, new Seated(this.onSeated));
			}
		}

		// Token: 0x04001CDD RID: 7389
		private static readonly float HEIGHT_LOOK_SIT = 1.6f;

		// Token: 0x04001CDE RID: 7390
		private static readonly float HEIGHT_LOOK_STAND = 1.75f;

		// Token: 0x04001CDF RID: 7391
		private static readonly float HEIGHT_LOOK_CROUCH = 1.2f;

		// Token: 0x04001CE0 RID: 7392
		private static readonly float HEIGHT_LOOK_PRONE = 0.35f;

		// Token: 0x04001CE1 RID: 7393
		private static readonly float HEIGHT_CAMERA_SIT = 0.7f;

		// Token: 0x04001CE2 RID: 7394
		private static readonly float HEIGHT_CAMERA_STAND = 1.05f;

		// Token: 0x04001CE3 RID: 7395
		private static readonly float HEIGHT_CAMERA_CROUCH = 0.95f;

		// Token: 0x04001CE4 RID: 7396
		private static readonly float HEIGHT_CAMERA_PRONE = 0.3f;

		// Token: 0x04001CE5 RID: 7397
		private static readonly float MIN_ANGLE_SIT = 60f;

		// Token: 0x04001CE6 RID: 7398
		private static readonly float MAX_ANGLE_SIT = 120f;

		// Token: 0x04001CE7 RID: 7399
		private static readonly float MIN_ANGLE_CLIMB = 45f;

		// Token: 0x04001CE8 RID: 7400
		private static readonly float MAX_ANGLE_CLIMB = 100f;

		// Token: 0x04001CE9 RID: 7401
		private static readonly float MIN_ANGLE_SWIM = 45f;

		// Token: 0x04001CEA RID: 7402
		private static readonly float MAX_ANGLE_SWIM = 135f;

		// Token: 0x04001CEB RID: 7403
		private static readonly float MIN_ANGLE_STAND;

		// Token: 0x04001CEC RID: 7404
		private static readonly float MAX_ANGLE_STAND = 180f;

		// Token: 0x04001CED RID: 7405
		private static readonly float MIN_ANGLE_CROUCH = 20f;

		// Token: 0x04001CEE RID: 7406
		private static readonly float MAX_ANGLE_CROUCH = 160f;

		// Token: 0x04001CEF RID: 7407
		private static readonly float MIN_ANGLE_PRONE = 60f;

		// Token: 0x04001CF0 RID: 7408
		private static readonly float MAX_ANGLE_PRONE = 120f;

		// Token: 0x04001CF1 RID: 7409
		public PerspectiveUpdated onPerspectiveUpdated;

		// Token: 0x04001CF2 RID: 7410
		private Camera _characterCamera;

		// Token: 0x04001CF3 RID: 7411
		private Camera _scopeCamera;

		// Token: 0x04001CF4 RID: 7412
		private Camera _highlightCamera;

		// Token: 0x04001CF5 RID: 7413
		private bool _isScopeActive;

		// Token: 0x04001CF6 RID: 7414
		private bool isOverlayActive;

		// Token: 0x04001CF7 RID: 7415
		private ELightingVision scopeVision;

		// Token: 0x04001CF8 RID: 7416
		private ELightingVision tempVision;

		// Token: 0x04001CF9 RID: 7417
		private Transform _aim;

		// Token: 0x04001CFA RID: 7418
		private static float characterHeight;

		// Token: 0x04001CFB RID: 7419
		private static float _characterYaw;

		// Token: 0x04001CFC RID: 7420
		public static float characterYaw;

		// Token: 0x04001CFD RID: 7421
		private static float killcam;

		// Token: 0x04001CFE RID: 7422
		private int warp_x;

		// Token: 0x04001CFF RID: 7423
		private int warp_y;

		// Token: 0x04001D00 RID: 7424
		private float _pitch;

		// Token: 0x04001D01 RID: 7425
		private float _yaw;

		// Token: 0x04001D02 RID: 7426
		private float _look_x;

		// Token: 0x04001D03 RID: 7427
		private float _look_y;

		// Token: 0x04001D04 RID: 7428
		private float _orbitPitch;

		// Token: 0x04001D05 RID: 7429
		private float _orbitYaw;

		// Token: 0x04001D06 RID: 7430
		public Vector3 lockPosition;

		// Token: 0x04001D07 RID: 7431
		public Vector3 orbitPosition;

		// Token: 0x04001D08 RID: 7432
		public bool isOrbiting;

		// Token: 0x04001D09 RID: 7433
		public bool isTracking;

		// Token: 0x04001D0A RID: 7434
		public bool isLocking;

		// Token: 0x04001D0B RID: 7435
		public bool isFocusing;

		// Token: 0x04001D0C RID: 7436
		public bool isSmoothing;

		// Token: 0x04001D0D RID: 7437
		public bool isIgnoringInput;

		// Token: 0x04001D0E RID: 7438
		private Vector3 smoothPosition;

		// Token: 0x04001D0F RID: 7439
		private Quaternion smoothRotation;

		// Token: 0x04001D10 RID: 7440
		public byte angle;

		// Token: 0x04001D11 RID: 7441
		public byte rot;

		// Token: 0x04001D12 RID: 7442
		private float recoil_x;

		// Token: 0x04001D13 RID: 7443
		private float recoil_y;

		// Token: 0x04001D14 RID: 7444
		private float lastRecoil;

		// Token: 0x04001D15 RID: 7445
		private float lastTick;

		// Token: 0x04001D16 RID: 7446
		public byte lastAngle;

		// Token: 0x04001D17 RID: 7447
		public byte lastRot;

		// Token: 0x04001D18 RID: 7448
		private float dodge;

		// Token: 0x04001D19 RID: 7449
		private float fov;

		// Token: 0x04001D1A RID: 7450
		private float eyes;

		// Token: 0x04001D1B RID: 7451
		private RaycastHit hit;

		// Token: 0x04001D1C RID: 7452
		private Vector3 cam;

		// Token: 0x04001D1D RID: 7453
		public float sensitivity;

		// Token: 0x04001D1E RID: 7454
		private EPlayerPerspective _perspective;

		// Token: 0x04001D1F RID: 7455
		protected bool isZoomed;
	}
}
