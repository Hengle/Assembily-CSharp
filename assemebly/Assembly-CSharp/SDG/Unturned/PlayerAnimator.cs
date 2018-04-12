using System;
using SDG.Framework.Utilities;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005FD RID: 1533
	public class PlayerAnimator : PlayerCaller
	{
		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002AC6 RID: 10950 RVA: 0x00109894 File Offset: 0x00107C94
		public Transform firstSkeleton
		{
			get
			{
				return this._firstSkeleton;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002AC7 RID: 10951 RVA: 0x0010989C File Offset: 0x00107C9C
		public Transform thirdSkeleton
		{
			get
			{
				return this._thirdSkeleton;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06002AC8 RID: 10952 RVA: 0x001098A4 File Offset: 0x00107CA4
		public Transform view
		{
			get
			{
				return this._view;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002AC9 RID: 10953 RVA: 0x001098AC File Offset: 0x00107CAC
		public Vector3 pos
		{
			get
			{
				return this._pos;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x001098B4 File Offset: 0x00107CB4
		public Vector3 rot
		{
			get
			{
				return this._rot;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06002ACB RID: 10955 RVA: 0x001098BC File Offset: 0x00107CBC
		public bool leanLeft
		{
			get
			{
				return this._leanLeft;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06002ACC RID: 10956 RVA: 0x001098C4 File Offset: 0x00107CC4
		public bool leanRight
		{
			get
			{
				return this._leanRight;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002ACD RID: 10957 RVA: 0x001098CC File Offset: 0x00107CCC
		public int lean
		{
			get
			{
				return this._lean;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002ACE RID: 10958 RVA: 0x001098D4 File Offset: 0x00107CD4
		public float shoulder
		{
			get
			{
				return this._shoulder;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002ACF RID: 10959 RVA: 0x001098DC File Offset: 0x00107CDC
		public float shoulder2
		{
			get
			{
				return this._shoulder2;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x001098E4 File Offset: 0x00107CE4
		public bool side
		{
			get
			{
				return this._side;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06002AD1 RID: 10961 RVA: 0x001098EC File Offset: 0x00107CEC
		public EPlayerGesture gesture
		{
			get
			{
				return this._gesture;
			}
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x001098F4 File Offset: 0x00107CF4
		public void addAnimation(AnimationClip clip)
		{
			if (this.firstAnimator != null)
			{
				this.firstAnimator.addAnimation(clip);
			}
			if (this.thirdAnimator != null)
			{
				this.thirdAnimator.addAnimation(clip);
			}
			if (this.characterAnimator != null)
			{
				this.characterAnimator.addAnimation(clip);
			}
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x00109958 File Offset: 0x00107D58
		public void removeAnimation(AnimationClip clip)
		{
			if (this.firstAnimator != null)
			{
				this.firstAnimator.removeAnimation(clip);
			}
			if (this.thirdAnimator != null)
			{
				this.thirdAnimator.removeAnimation(clip);
			}
			if (this.characterAnimator != null)
			{
				this.characterAnimator.removeAnimation(clip);
			}
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x001099BC File Offset: 0x00107DBC
		public void setAnimationSpeed(string name, float speed)
		{
			if (this.firstAnimator != null)
			{
				this.firstAnimator.setAnimationSpeed(name, speed);
			}
			if (this.thirdAnimator != null)
			{
				this.thirdAnimator.setAnimationSpeed(name, speed);
			}
			if (this.characterAnimator != null)
			{
				this.characterAnimator.setAnimationSpeed(name, speed);
			}
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x00109A24 File Offset: 0x00107E24
		public float getAnimationLength(string name)
		{
			if (this.firstAnimator != null)
			{
				return this.firstAnimator.getAnimationLength(name);
			}
			if (this.thirdAnimator != null)
			{
				return this.thirdAnimator.getAnimationLength(name);
			}
			return 0f;
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x00109A72 File Offset: 0x00107E72
		public void getAnimationSample(string name, float point)
		{
			if (this.firstAnimator != null)
			{
				this.firstAnimator.getAnimationSample(name, point);
			}
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x00109A94 File Offset: 0x00107E94
		public bool checkExists(string name)
		{
			if (this.firstAnimator != null)
			{
				return this.firstAnimator.checkExists(name);
			}
			if (this.thirdAnimator != null)
			{
				return this.thirdAnimator.checkExists(name);
			}
			return this.characterAnimator != null && this.characterAnimator.checkExists(name);
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x00109AFC File Offset: 0x00107EFC
		public void play(string name, bool smooth)
		{
			if (this.gesture != EPlayerGesture.NONE)
			{
				this._gesture = EPlayerGesture.NONE;
			}
			if (this.firstAnimator != null)
			{
				this.firstAnimator.play(name, smooth);
			}
			if (this.thirdAnimator != null)
			{
				this.thirdAnimator.play(name, smooth);
			}
			if (this.characterAnimator != null)
			{
				this.characterAnimator.play(name, smooth);
			}
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x00109B78 File Offset: 0x00107F78
		public void stop(string name)
		{
			if (this.firstAnimator != null)
			{
				this.firstAnimator.stop(name);
			}
			if (this.thirdAnimator != null)
			{
				this.thirdAnimator.stop(name);
			}
			if (this.characterAnimator != null)
			{
				this.characterAnimator.stop(name);
			}
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x00109BDC File Offset: 0x00107FDC
		public void mixAnimation(string name)
		{
			if (this.firstAnimator != null)
			{
				this.firstAnimator.mixAnimation(name);
			}
			if (this.thirdAnimator != null)
			{
				this.thirdAnimator.mixAnimation(name);
			}
			if (this.characterAnimator != null)
			{
				this.characterAnimator.mixAnimation(name);
			}
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x00109C40 File Offset: 0x00108040
		public void mixAnimation(string name, bool mixLeftShoulder, bool mixRightShoulder)
		{
			this.mixAnimation(name, mixLeftShoulder, mixRightShoulder, false);
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x00109C4C File Offset: 0x0010804C
		public void mixAnimation(string name, bool mixLeftShoulder, bool mixRightShoulder, bool mixSkull)
		{
			if (this.firstAnimator != null)
			{
				this.firstAnimator.mixAnimation(name, mixLeftShoulder, mixRightShoulder, mixSkull);
			}
			if (this.thirdAnimator != null)
			{
				this.thirdAnimator.mixAnimation(name, mixLeftShoulder, mixRightShoulder, mixSkull);
			}
			if (this.characterAnimator != null)
			{
				this.characterAnimator.mixAnimation(name, mixLeftShoulder, mixRightShoulder, mixSkull);
			}
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x00109CBC File Offset: 0x001080BC
		public void shake(float shake_x, float shake_y, float shake_z)
		{
			this.viewShake.x = this.viewShake.x + shake_x;
			this.viewShake.y = this.viewShake.y + shake_y;
			this.viewShake.z = this.viewShake.z + shake_z;
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x00109CF7 File Offset: 0x001080F7
		public void fling(float fling_x, float fling_y, float fling_z)
		{
			this.viewFling.x = this.viewFling.x + fling_x;
			this.viewFling.y = this.viewFling.y + fling_y;
			this.viewFling.z = this.viewFling.z + fling_z;
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x00109D34 File Offset: 0x00108134
		public void lockView()
		{
			this.lockPosition = this.view.localPosition;
			this.lockRotation = this.view.localRotation;
			this.view.localPosition = new Vector3(-0.45f, 0f, 0f);
			this.view.localRotation = Quaternion.Euler(0f, 0f, 90f);
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x00109DA1 File Offset: 0x001081A1
		public void unlockView()
		{
			this.view.localPosition = this.lockPosition;
			this.view.localRotation = this.lockRotation;
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06002AE1 RID: 10977 RVA: 0x00109DC8 File Offset: 0x001081C8
		public float bob
		{
			get
			{
				if (Player.player.stance.stance == EPlayerStance.SPRINT)
				{
					return PlayerAnimator.BOB_SPRINT * this._multiplier;
				}
				if (Player.player.stance.stance == EPlayerStance.STAND)
				{
					return PlayerAnimator.BOB_STAND * this._multiplier;
				}
				if (Player.player.stance.stance == EPlayerStance.CROUCH)
				{
					return PlayerAnimator.BOB_CROUCH * this._multiplier;
				}
				if (Player.player.stance.stance == EPlayerStance.PRONE)
				{
					return PlayerAnimator.BOB_PRONE * this._multiplier;
				}
				if (Player.player.stance.stance == EPlayerStance.SWIM)
				{
					return PlayerAnimator.BOB_SWIM * this._multiplier;
				}
				return 0f;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06002AE2 RID: 10978 RVA: 0x00109E84 File Offset: 0x00108284
		public float tilt
		{
			get
			{
				if (Player.player.stance.stance == EPlayerStance.SPRINT)
				{
					return PlayerAnimator.TILT_SPRINT * (1f - this._multiplier / 2f);
				}
				if (Player.player.stance.stance == EPlayerStance.STAND)
				{
					return PlayerAnimator.TILT_STAND * (1f - this._multiplier / 2f);
				}
				if (Player.player.stance.stance == EPlayerStance.CROUCH)
				{
					return PlayerAnimator.TILT_CROUCH * (1f - this._multiplier / 2f);
				}
				if (Player.player.stance.stance == EPlayerStance.PRONE)
				{
					return PlayerAnimator.TILT_PRONE * (1f - this._multiplier / 2f);
				}
				if (Player.player.stance.stance == EPlayerStance.SWIM)
				{
					return PlayerAnimator.TILT_SWIM * (1f - this._multiplier / 2f);
				}
				return 0f;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002AE3 RID: 10979 RVA: 0x00109F7C File Offset: 0x0010837C
		public float roll
		{
			get
			{
				if (Player.player.stance.stance == EPlayerStance.SPRINT)
				{
					return Mathf.Sin(PlayerAnimator.TILT_SPRINT * Time.time * 0.25f) * PlayerAnimator.TILT_SPRINT;
				}
				if (Player.player.stance.stance == EPlayerStance.STAND)
				{
					return Mathf.Sin(PlayerAnimator.TILT_STAND * Time.time * 0.5f) * PlayerAnimator.TILT_STAND * 0.5f;
				}
				if (Player.player.stance.stance == EPlayerStance.SWIM)
				{
					return Mathf.Sin(PlayerAnimator.TILT_SWIM * Time.time * 0.25f) * PlayerAnimator.TILT_SWIM * 0.25f;
				}
				return 0f;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x0010A030 File Offset: 0x00108430
		public float speed
		{
			get
			{
				if (Player.player.stance.stance == EPlayerStance.SPRINT)
				{
					return PlayerAnimator.SPEED_SPRINT;
				}
				if (Player.player.stance.stance == EPlayerStance.STAND)
				{
					return PlayerAnimator.SPEED_STAND;
				}
				if (Player.player.stance.stance == EPlayerStance.CROUCH)
				{
					return PlayerAnimator.SPEED_CROUCH;
				}
				if (Player.player.stance.stance == EPlayerStance.PRONE)
				{
					return PlayerAnimator.SPEED_PRONE;
				}
				if (Player.player.stance.stance == EPlayerStance.SWIM)
				{
					return PlayerAnimator.SPEED_SWIM;
				}
				return 0f;
			}
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x0010A0CC File Offset: 0x001084CC
		private void onLifeUpdated(bool isDead)
		{
			if (this.gesture != EPlayerGesture.NONE)
			{
				if (this.gesture == EPlayerGesture.INVENTORY_START)
				{
					this.stop("Gesture_Inventory");
				}
				else if (this.gesture == EPlayerGesture.SURRENDER_START)
				{
					this.stop("Gesture_Surrender");
				}
				else if (this.gesture == EPlayerGesture.ARREST_START)
				{
					this.stop("Gesture_Arrest");
				}
				else if (this.gesture == EPlayerGesture.REST_START)
				{
					this.stop("Gesture_Rest");
				}
				this.captorID = CSteamID.Nil;
				this.captorItem = 0;
				this.captorStrength = 0;
				this._gesture = EPlayerGesture.NONE;
				if (this.onGestureUpdated != null)
				{
					this.onGestureUpdated(this.gesture);
				}
			}
			if (base.channel.isOwner)
			{
				this.firstRenderer_0.enabled = (!isDead && base.player.look.perspective == EPlayerPerspective.FIRST);
				this.firstSkeleton.gameObject.SetActive(!isDead && base.player.look.perspective == EPlayerPerspective.FIRST);
				if (this.thirdRenderer_0 != null)
				{
					this.thirdRenderer_0.enabled = (!isDead && base.player.look.perspective == EPlayerPerspective.THIRD);
				}
				if (this.thirdRenderer_1 != null)
				{
					this.thirdRenderer_1.enabled = (!isDead && base.player.look.perspective == EPlayerPerspective.THIRD);
				}
				this.thirdSkeleton.gameObject.SetActive(!isDead && base.player.look.perspective == EPlayerPerspective.THIRD);
			}
			else
			{
				if (!Dedicator.isDedicated && !this.isLocked)
				{
					if (this.thirdRenderer_0 != null)
					{
						this.thirdRenderer_0.enabled = !isDead;
					}
					if (this.thirdRenderer_1 != null)
					{
						this.thirdRenderer_1.enabled = !isDead;
					}
				}
				this.thirdSkeleton.gameObject.SetActive(!isDead);
			}
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x0010A2FC File Offset: 0x001086FC
		public void unlock()
		{
			this.isLocked = false;
			if (!base.channel.isOwner && !Dedicator.isDedicated && !base.player.life.isDead)
			{
				if (this.thirdRenderer_0 != null)
				{
					this.thirdRenderer_0.enabled = true;
				}
				if (this.thirdRenderer_1 != null)
				{
					this.thirdRenderer_1.enabled = true;
				}
				this.thirdSkeleton.gameObject.SetActive(true);
			}
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x0010A38A File Offset: 0x0010878A
		[SteamCall]
		public void tellLean(CSteamID steamID, byte newLean)
		{
			if (base.channel.checkServer(steamID))
			{
				this._lean = (int)(newLean - 1);
			}
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x0010A3A8 File Offset: 0x001087A8
		[SteamCall]
		public void tellGesture(CSteamID steamID, byte id)
		{
			if (base.channel.checkServer(steamID))
			{
				if (id == 1 && this.gesture == EPlayerGesture.NONE)
				{
					this.play("Gesture_Inventory", true);
					this._gesture = EPlayerGesture.INVENTORY_START;
				}
				else if (id == 2 && this.gesture == EPlayerGesture.INVENTORY_START)
				{
					this.stop("Gesture_Inventory");
					this._gesture = EPlayerGesture.NONE;
				}
				else if (id == 3)
				{
					this.play("Gesture_Pickup", false);
					this._gesture = EPlayerGesture.NONE;
				}
				else if (id == 4)
				{
					this.play("Punch_Left", false);
					this._gesture = EPlayerGesture.NONE;
					if (!Dedicator.isDedicated)
					{
						base.player.playSound((AudioClip)Resources.Load("Sounds/General/Punch"));
					}
				}
				else if (id == 5)
				{
					this.play("Punch_Right", false);
					this._gesture = EPlayerGesture.NONE;
					if (!Dedicator.isDedicated)
					{
						base.player.playSound((AudioClip)Resources.Load("Sounds/General/Punch"));
					}
				}
				else if (id == 6 && this.gesture == EPlayerGesture.NONE)
				{
					this.play("Gesture_Surrender", true);
					this._gesture = EPlayerGesture.SURRENDER_START;
				}
				else if (id == 7 && this.gesture == EPlayerGesture.SURRENDER_START)
				{
					this.stop("Gesture_Surrender");
					this._gesture = EPlayerGesture.NONE;
				}
				else if (id == 13 && this.gesture == EPlayerGesture.NONE)
				{
					this.play("Gesture_Rest", true);
					this._gesture = EPlayerGesture.REST_START;
				}
				else if (id == 14 && this.gesture == EPlayerGesture.REST_START)
				{
					this.stop("Gesture_Rest");
					this._gesture = EPlayerGesture.NONE;
				}
				else if (id == 11)
				{
					this.play("Gesture_Arrest", true);
					this._gesture = EPlayerGesture.ARREST_START;
				}
				else if (id == 12 && this.gesture == EPlayerGesture.ARREST_START)
				{
					this.stop("Gesture_Arrest");
					this._gesture = EPlayerGesture.NONE;
				}
				else if (id == 8 && this.gesture == EPlayerGesture.NONE)
				{
					this.play("Gesture_Point", false);
					this._gesture = EPlayerGesture.NONE;
				}
				else if (id == 9 && this.gesture == EPlayerGesture.NONE)
				{
					this.play("Gesture_Wave", false);
					this._gesture = EPlayerGesture.NONE;
				}
				else if (id == 10 && this.gesture == EPlayerGesture.NONE)
				{
					this.play("Gesture_Salute", false);
					this._gesture = EPlayerGesture.NONE;
				}
				else if (id == 15 && this.gesture == EPlayerGesture.NONE)
				{
					this.play("Gesture_Facepalm", false);
					this._gesture = EPlayerGesture.NONE;
				}
				if (this.onGestureUpdated != null)
				{
					this.onGestureUpdated(this.gesture);
				}
			}
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x0010A67C File Offset: 0x00108A7C
		[SteamCall]
		public void askGesture(CSteamID steamID, byte id)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (!base.player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if (id == 2 && base.player.inventory.isStoring && base.player.inventory.shouldInventoryStopGestureCloseStorage)
				{
					base.player.inventory.isStoring = false;
					base.player.inventory.isStorageTrunk = false;
					if (base.player.inventory.storage != null)
					{
						base.player.inventory.storage.isOpen = false;
						base.player.inventory.storage.opener = null;
						base.player.inventory.storage = null;
					}
					base.player.inventory.updateItems(PlayerInventory.STORAGE, null);
				}
				if (this.gesture == EPlayerGesture.ARREST_START)
				{
					return;
				}
				if (base.player.equipment.isSelected)
				{
					return;
				}
				if (base.player.stance.stance == EPlayerStance.PRONE || base.player.stance.stance == EPlayerStance.DRIVING || base.player.stance.stance == EPlayerStance.SITTING)
				{
					return;
				}
				if (id == 1 || id == 2 || id == 6 || id == 7 || id == 8 || id == 9 || id == 10 || id == 15 || id == 13 || id == 14)
				{
					this.sendGesture((EPlayerGesture)id, id != 1 && id != 2);
				}
			}
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x0010A844 File Offset: 0x00108C44
		public void sendGesture(EPlayerGesture gesture, bool all)
		{
			if (!Dedicator.isDedicated && gesture == EPlayerGesture.INVENTORY_STOP && base.player.inventory.isStoring && base.player.inventory.shouldInventoryStopGestureCloseStorage)
			{
				base.player.inventory.isStoring = false;
				base.player.inventory.isStorageTrunk = false;
				if (base.player.inventory.storage != null)
				{
					base.player.inventory.storage.isOpen = false;
					base.player.inventory.storage.opener = null;
					base.player.inventory.storage = null;
				}
				base.player.inventory.updateItems(PlayerInventory.STORAGE, null);
			}
			if (Provider.isServer)
			{
				if (gesture == EPlayerGesture.REST_START && base.player.stance.stance != EPlayerStance.CROUCH)
				{
					base.player.stance.checkStance(EPlayerStance.CROUCH, true);
					if (base.player.stance.stance != EPlayerStance.CROUCH)
					{
						return;
					}
				}
				if (all)
				{
					base.channel.send("tellGesture", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						(byte)gesture
					});
				}
				else
				{
					base.channel.send("tellGesture", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						(byte)gesture
					});
				}
			}
			else
			{
				if (gesture != EPlayerGesture.INVENTORY_STOP)
				{
					if (base.player.equipment.isSelected)
					{
						return;
					}
					if (base.player.stance.stance == EPlayerStance.PRONE || base.player.stance.stance == EPlayerStance.DRIVING || base.player.stance.stance == EPlayerStance.SITTING)
					{
						return;
					}
				}
				base.channel.send("askGesture", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					(byte)gesture
				});
			}
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x0010AA48 File Offset: 0x00108E48
		private void updateState(CharacterAnimator charAnim)
		{
			if (base.player.movement.isMoving)
			{
				if (base.player.stance.stance == EPlayerStance.CLIMB)
				{
					charAnim.state("Move_Climb");
				}
				else if (base.player.stance.stance == EPlayerStance.SWIM)
				{
					charAnim.state("Move_Swim");
				}
				else if (base.player.stance.stance == EPlayerStance.SPRINT)
				{
					charAnim.state("Move_Run");
				}
				else if (base.player.stance.stance == EPlayerStance.STAND)
				{
					charAnim.state("Move_Walk");
				}
				else if (base.player.stance.stance == EPlayerStance.CROUCH)
				{
					charAnim.state("Move_Crouch");
				}
				else if (base.player.stance.stance == EPlayerStance.PRONE)
				{
					charAnim.state("Move_Prone");
				}
			}
			else if (base.player.stance.stance == EPlayerStance.DRIVING)
			{
				if (base.player.movement.getVehicle() != null && base.player.movement.getVehicle().asset.hasZip)
				{
					charAnim.state("Idle_Zip");
				}
				else if (base.player.movement.getVehicle() != null && base.player.movement.getVehicle().asset.hasBicycle)
				{
					charAnim.state("Idle_Bicycle");
					charAnim.setAnimationSpeed("Idle_Bicycle", base.player.movement.getVehicle().speed * base.player.movement.getVehicle().asset.bicycleAnimSpeed);
				}
				else if (base.player.movement.getVehicle() != null && base.player.movement.getVehicle().asset.isReclined)
				{
					charAnim.state("Idle_Reclined");
				}
				else
				{
					charAnim.state("Idle_Drive");
				}
			}
			else if (base.player.stance.stance == EPlayerStance.SITTING)
			{
				if (base.player.movement.getVehicle() != null && base.player.movement.getVehicle().passengers[(int)base.player.movement.getSeat()].turret != null)
				{
					charAnim.state("Idle_Drive");
				}
				else
				{
					charAnim.state("Idle_Sit");
				}
			}
			else if (base.player.stance.stance == EPlayerStance.CLIMB)
			{
				charAnim.state("Idle_Climb");
			}
			else if (base.player.stance.stance == EPlayerStance.SWIM)
			{
				charAnim.state("Idle_Swim");
			}
			else if (base.player.stance.stance == EPlayerStance.STAND || base.player.stance.stance == EPlayerStance.SPRINT)
			{
				charAnim.state("Idle_Stand");
			}
			else if (base.player.stance.stance == EPlayerStance.CROUCH)
			{
				charAnim.state("Idle_Crouch");
			}
			else if (base.player.stance.stance == EPlayerStance.PRONE)
			{
				charAnim.state("Idle_Prone");
			}
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x0010ADDC File Offset: 0x001091DC
		private void updateHuman(HumanAnimator humanAnim)
		{
			humanAnim.lean = (float)((!base.player.channel.owner.hand) ? this.lean : (-(float)this.lean));
			if (base.player.stance.stance == EPlayerStance.DRIVING || base.player.stance.stance == EPlayerStance.SITTING)
			{
				humanAnim.pitch = 90f;
			}
			else
			{
				humanAnim.pitch = base.player.look.pitch;
			}
			if (base.player.stance.stance == EPlayerStance.CROUCH)
			{
				humanAnim.offset = 0.1f;
			}
			else if (base.player.stance.stance == EPlayerStance.PRONE)
			{
				humanAnim.offset = 0.2f;
			}
			else
			{
				humanAnim.offset = 0f;
			}
			if (!base.channel.isOwner && Provider.isServer)
			{
				humanAnim.force();
			}
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x0010AEE8 File Offset: 0x001092E8
		private void onLanded(float fall)
		{
			if (fall < -1f)
			{
				if (base.player.movement.totalGravityMultiplier < 0.67f)
				{
					if (fall < -5f)
					{
						fall = -5f;
					}
				}
				else if (fall < -15f)
				{
					fall = -15f;
				}
				this.viewTilt += fall * Vector3.left;
			}
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x0010AF60 File Offset: 0x00109360
		public void simulate(uint simulation, bool inputLeanLeft, bool inputLeanRight)
		{
			if (base.player.stance.stance != EPlayerStance.CLIMB && base.player.stance.stance != EPlayerStance.SPRINT && base.player.stance.stance != EPlayerStance.DRIVING && base.player.stance.stance != EPlayerStance.SITTING)
			{
				if (inputLeanLeft)
				{
					PhysicsUtility.raycast(new Ray(base.transform.position + Vector3.up, -base.transform.right), out this.wall, 1.2f, RayMasks.BLOCK_LEAN, QueryTriggerInteraction.UseGlobal);
					if (this.wall.transform == null)
					{
						this._lean = 1;
						this.leanObstructed = false;
					}
					else
					{
						this._lean = 0;
						this.leanObstructed = true;
					}
				}
				else if (inputLeanRight)
				{
					PhysicsUtility.raycast(new Ray(base.transform.position + Vector3.up, base.transform.right), out this.wall, 1.2f, RayMasks.BLOCK_LEAN, QueryTriggerInteraction.UseGlobal);
					if (this.wall.transform == null)
					{
						this._lean = -1;
						this.leanObstructed = false;
					}
					else
					{
						this._lean = 0;
						this.leanObstructed = true;
					}
				}
				else
				{
					this._lean = 0;
					this.leanObstructed = false;
				}
			}
			else
			{
				this._lean = 0;
				this.leanObstructed = false;
			}
			if (this.lastLean != this.lean)
			{
				this.lastLean = this.lean;
				if (Provider.isServer)
				{
					if ((this.lean == -1 || this.lean == 1) && this.captorStrength > 0)
					{
						this.captorStrength -= 1;
						if (this.captorStrength == 0)
						{
							this.captorID = CSteamID.Nil;
							this.captorItem = 0;
							this.sendGesture(EPlayerGesture.ARREST_STOP, true);
							EffectManager.sendEffect(36, EffectManager.MEDIUM, base.transform.position, Vector3.up);
						}
					}
					base.channel.send("tellLean", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
					{
						(byte)(this.lean + 1)
					});
				}
			}
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x0010B1AC File Offset: 0x001095AC
		[SteamCall]
		public void askEmote(CSteamID steamID)
		{
			if (Provider.isServer && this.gesture != EPlayerGesture.NONE)
			{
				base.channel.send("tellGesture", steamID, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
				{
					(byte)this.gesture
				});
			}
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x0010B1EC File Offset: 0x001095EC
		private void onPerspectiveUpdated(EPlayerPerspective newPerspective)
		{
			this.firstRenderer_0.enabled = (newPerspective == EPlayerPerspective.FIRST);
			this.firstSkeleton.gameObject.SetActive(newPerspective == EPlayerPerspective.FIRST);
			this.thirdRenderer_0.enabled = (newPerspective == EPlayerPerspective.THIRD);
			this.thirdRenderer_1.enabled = (newPerspective == EPlayerPerspective.THIRD);
			this.thirdSkeleton.gameObject.SetActive(newPerspective == EPlayerPerspective.THIRD);
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x0010B250 File Offset: 0x00109650
		private void Update()
		{
			if (base.channel.isOwner)
			{
				if (!PlayerUI.window.showCursor)
				{
					if (!base.player.look.isOrbiting)
					{
						if (ControlsSettings.leaning == EControlMode.TOGGLE)
						{
							if (Input.GetKeyDown(ControlsSettings.leanLeft))
							{
								if (base.player.look.perspective == EPlayerPerspective.FIRST || this.side)
								{
									if (this.leanLeft)
									{
										this._leanLeft = false;
										this._leanRight = false;
									}
									else
									{
										this._leanLeft = true;
										this._leanRight = false;
									}
								}
								if (!this.side && this.leanRight)
								{
									this._leanLeft = false;
									this._leanRight = false;
								}
								this._side = true;
							}
							if (Input.GetKeyDown(ControlsSettings.leanRight))
							{
								if (base.player.look.perspective == EPlayerPerspective.FIRST || !this.side)
								{
									if (this.leanRight)
									{
										this._leanLeft = false;
										this._leanRight = false;
									}
									else
									{
										this._leanLeft = false;
										this._leanRight = true;
									}
								}
								if (this.side && this.leanLeft)
								{
									this._leanLeft = false;
									this._leanRight = false;
								}
								this._side = false;
							}
						}
						else
						{
							if (Input.GetKeyDown(ControlsSettings.leanLeft))
							{
								this._side = true;
								this.lastTwitch = Time.realtimeSinceStartup;
							}
							if (Input.GetKeyDown(ControlsSettings.leanRight))
							{
								this._side = false;
								this.lastTwitch = Time.realtimeSinceStartup;
							}
							if (Input.GetKey(ControlsSettings.leanLeft))
							{
								if (base.player.look.perspective == EPlayerPerspective.FIRST || Time.realtimeSinceStartup - this.lastTwitch > 0.075f)
								{
									this._leanLeft = true;
								}
								else
								{
									this._leanLeft = false;
								}
							}
							else
							{
								this._leanLeft = false;
							}
							if (Input.GetKey(ControlsSettings.leanRight))
							{
								if (base.player.look.perspective == EPlayerPerspective.FIRST || Time.realtimeSinceStartup - this.lastTwitch > 0.075f)
								{
									this._leanRight = true;
								}
								else
								{
									this._leanRight = false;
								}
							}
							else
							{
								this._leanRight = false;
							}
						}
					}
				}
				else
				{
					this._leanLeft = false;
					this._leanRight = false;
				}
				if (this.firstAnimator != null)
				{
					if (this.firstAnimator.getAnimationPlaying())
					{
						this.firstAnimator.state("Idle_Stand");
					}
					else
					{
						this.updateState(this.firstAnimator);
					}
				}
				if (this.thirdAnimator != null)
				{
					this.updateState(this.thirdAnimator);
					this.updateHuman((HumanAnimator)this.thirdAnimator);
				}
				this._multiplier = Mathf.Lerp(this._multiplier, this.multiplier, 16f * Time.deltaTime);
				this._pref = Mathf.Lerp(this._pref, this.pref, 16f * Time.deltaTime);
				if (base.player.movement.isMoving)
				{
					this.viewBob.x = Mathf.Lerp(this.viewBob.x, Mathf.Sin(this.speed * Time.time) * this.bob, 16f * Time.deltaTime);
					this.viewBob.y = Mathf.Lerp(this.viewBob.y, Mathf.Abs(Mathf.Sin(this.speed * Time.time) * this.bob), 16f * Time.deltaTime);
				}
				else
				{
					this.viewBob.x = Mathf.Lerp(this.viewBob.x, 0f, 4f * Time.deltaTime);
					this.viewBob.y = Mathf.Lerp(this.viewBob.y, 0f, 4f * Time.deltaTime);
				}
				this.viewAdjust = Vector3.Lerp(this.viewAdjust, this.viewOffset - this.viewShake - this.viewFling, 16f * Time.deltaTime);
				this.viewShake = Vector3.Lerp(this.viewShake, Vector3.zero, 4f * Time.deltaTime);
				this.viewFling = Vector3.Lerp(this.viewFling, Vector3.zero, 16f * Time.deltaTime);
				this.viewTilt = Vector3.Lerp(this.viewTilt, Vector3.zero, 8f * Time.deltaTime);
				this._pos.x = -this.viewBob.y - this.viewAdjust.y;
				this._pos.y = this.viewBob.x + this.viewAdjust.x;
				this._pos.z = this.viewBob.z + this.viewAdjust.z;
				this._pos.x = this._pos.x + Provider.preferenceData.Viewmodel.Offset_Vertical * this._pref;
				this._pos.y = this._pos.y + Provider.preferenceData.Viewmodel.Offset_Horizontal * this._pref;
				this._pos.z = this._pos.z - Provider.preferenceData.Viewmodel.Offset_Depth * this._pref;
				if (base.player.stance.stance == EPlayerStance.DRIVING)
				{
					this.viewPoint.x = Mathf.Lerp(this.viewPoint.x, -this.driveOffset.y - 0.65f - Mathf.Abs(base.player.look.yaw) / 90f * 0.25f, 8f * Time.deltaTime);
					this.viewPoint.y = Mathf.Lerp(this.viewPoint.y, this.driveOffset.x + (float)((!base.channel.owner.hand) ? 1 : -1) * base.player.movement.getVehicle().steer * -0.01f, 8f * Time.deltaTime);
					this.viewPoint.z = Mathf.Lerp(this.viewPoint.z, this.driveOffset.z - 0.25f, 8f * Time.deltaTime);
				}
				else
				{
					this.viewPoint.x = this.pos.x - 0.45f;
					this.viewPoint.y = this.pos.y;
					this.viewPoint.z = this.pos.z;
				}
				this.view.transform.localPosition = this.viewPoint;
				if (base.player.life.health < 25)
				{
					this.view.transform.localPosition += new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)) * 0.005f * (1f - (float)Player.player.life.health / 25f) * (1f - base.player.skills.mastery(1, 3) * 0.75f);
				}
				if (base.player.movement.isMoving)
				{
					this._rot.x = Mathf.Lerp(this.rot.x, base.player.movement.move.z * this.tilt * this.multiplier + this.roll * this.multiplier + (float)((!base.player.movement.isGrounded) ? -5 : 0), 4f * Time.deltaTime);
					this._rot.z = Mathf.Lerp(this.rot.z, base.player.movement.move.x * this.tilt + this.roll * this.multiplier, 4f * Time.deltaTime);
				}
				else
				{
					this._rot.x = Mathf.Lerp(this.rot.x, (float)((!base.player.movement.isGrounded) ? -5 : 0), 4f * Time.deltaTime);
					this._rot.z = Mathf.Lerp(this.rot.z, 0f, 4f * Time.deltaTime);
				}
				this.viewRot = this._rot + this.viewTilt;
				float num = Mathf.Clamp(base.player.look.yaw - this.lastYaw, -5f, 5f);
				this.lastYaw = base.player.look.yaw;
				this.smoothYaw += num;
				this.smoothYaw = Mathf.Lerp(this.smoothYaw, 0f, 4f * Time.deltaTime);
				this.viewRot.z = this.viewRot.z + this.smoothYaw * 0.1f;
				if (base.player.stance.stance == EPlayerStance.DRIVING)
				{
					this.view.transform.localRotation = Quaternion.Lerp(this.view.transform.localRotation, Quaternion.Euler(base.player.look.yaw * 60f / MainCamera.instance.fieldOfView * (float)((!base.channel.owner.hand) ? -1 : 1), (base.player.look.pitch - 90f) * 60f / MainCamera.instance.fieldOfView, 90f + base.player.movement.getVehicle().steer * (float)((!base.channel.owner.hand) ? 1 : -1)), 8f * Time.deltaTime);
				}
				else if (base.player.stance.stance == EPlayerStance.CLIMB)
				{
					this.view.transform.localRotation = Quaternion.Lerp(this.view.transform.localRotation, Quaternion.Euler(0f, (base.player.look.pitch - 90f) * 60f / MainCamera.instance.fieldOfView, 90f), 8f * Time.deltaTime);
				}
				else
				{
					this.view.transform.localRotation = Quaternion.Euler(0f, -this.viewRot.x, this.viewRot.z + 90f);
				}
				base.player.first.transform.localRotation = Quaternion.Lerp(base.player.first.transform.localRotation, Quaternion.Euler(0f, 0f, (float)this.lean * HumanAnimator.LEAN), ((!this.leanObstructed) ? 4f : 16f) * Time.deltaTime);
				this.cam.fieldOfView = Mathf.Lerp(Provider.preferenceData.Viewmodel.Field_Of_View_Aim, Provider.preferenceData.Viewmodel.Field_Of_View_Hip, this._pref);
				if (Provider.modeConfigData.Gameplay.Allow_Shoulder_Camera)
				{
					this._shoulder = Mathf.Lerp(this.shoulder, (float)((!this.side) ? 1 : -1), 8f * Time.deltaTime);
				}
				else
				{
					this._shoulder = 0f;
				}
				this._shoulder2 = Mathf.Lerp(this.shoulder2, (float)(-(float)this.lean), 8f * Time.deltaTime);
			}
			else if (this.thirdAnimator != null)
			{
				this.updateState(this.thirdAnimator);
				this.updateHuman((HumanAnimator)this.thirdAnimator);
			}
			if (this.characterAnimator != null)
			{
				this.updateState(this.characterAnimator);
				this.updateHuman(this.characterAnimator);
			}
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x0010BF24 File Offset: 0x0010A324
		public void init()
		{
			base.channel.send("askEmote", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x0010BF40 File Offset: 0x0010A340
		private void Start()
		{
			this.isLocked = true;
			if (base.channel.isOwner)
			{
				if (base.player.first != null)
				{
					this.viewmodelLock = new GameObject().transform;
					this.viewmodelLock.name = "View";
					this.viewmodelLock.transform.localPosition = Vector3.zero;
					this.firstAnimator = MainCamera.instance.transform.FindChild("Viewmodel").GetComponent<CharacterAnimator>();
					Vector3 localPosition = this.firstAnimator.transform.localPosition;
					Quaternion localRotation = this.firstAnimator.transform.localRotation;
					this.firstAnimator.transform.parent = this.viewmodelLock;
					this.firstAnimator.transform.localPosition = localPosition;
					this.firstAnimator.transform.localRotation = localRotation;
					this.firstAnimator.transform.localScale = new Vector3((float)((!base.channel.owner.hand) ? 1 : -1), 1f, 1f);
					this.firstRenderer_0 = (SkinnedMeshRenderer)this.firstAnimator.transform.FindChild("Model_0").GetComponent<Renderer>();
					this._firstSkeleton = this.firstAnimator.transform.FindChild("Skeleton");
				}
				if (base.player.third != null)
				{
					this.thirdAnimator = base.player.third.GetComponent<CharacterAnimator>();
					this.thirdAnimator.transform.localScale = new Vector3((float)((!base.channel.owner.hand) ? 1 : -1), 1f, 1f);
					this.thirdRenderer_0 = (SkinnedMeshRenderer)this.thirdAnimator.transform.FindChild("Model_0").GetComponent<Renderer>();
					this.thirdRenderer_1 = (SkinnedMeshRenderer)this.thirdAnimator.transform.FindChild("Model_1").GetComponent<Renderer>();
					this._thirdSkeleton = this.thirdAnimator.transform.FindChild("Skeleton");
					this.thirdSkeleton.FindChild("Spine").GetComponent<Collider>().enabled = false;
					this.thirdSkeleton.FindChild("Spine").FindChild("Skull").GetComponent<Collider>().enabled = false;
					this.thirdSkeleton.FindChild("Spine").FindChild("Left_Shoulder").FindChild("Left_Arm").GetComponent<Collider>().enabled = false;
					this.thirdSkeleton.FindChild("Spine").FindChild("Right_Shoulder").FindChild("Right_Arm").GetComponent<Collider>().enabled = false;
					this.thirdSkeleton.FindChild("Left_Hip").FindChild("Left_Leg").GetComponent<Collider>().enabled = false;
					this.thirdSkeleton.FindChild("Right_Hip").FindChild("Right_Leg").GetComponent<Collider>().enabled = false;
				}
				if (Provider.cameraMode == ECameraMode.THIRD)
				{
					this.thirdRenderer_0.enabled = true;
					this.thirdRenderer_1.enabled = true;
					this.thirdSkeleton.gameObject.SetActive(true);
				}
				else
				{
					this.firstRenderer_0.enabled = true;
					this.firstSkeleton.gameObject.SetActive(true);
				}
				this._view = this.firstSkeleton.FindChild("Spine").FindChild("Skull").FindChild("ViewmodelCamera");
				this.cam = this.view.GetComponent<Camera>();
				this.viewOffset = Vector3.zero;
				this.driveOffset = Vector3.zero;
				this.viewSway = Vector3.zero;
				this.viewShake = Vector3.zero;
				this.viewFling = Vector3.zero;
				this.viewBob = Vector3.zero;
				this.viewPoint = Vector3.zero;
				this._multiplier = 1f;
				this.multiplier = 1f;
				this._pref = 1f;
				this.pref = 1f;
				if (base.player.character != null)
				{
					this.characterAnimator = base.player.character.GetComponent<HumanAnimator>();
					this.characterAnimator.transform.localScale = new Vector3((float)((!base.channel.owner.hand) ? 1 : -1), 1f, 1f);
				}
				PlayerMovement movement = base.player.movement;
				movement.onLanded = (Landed)Delegate.Combine(movement.onLanded, new Landed(this.onLanded));
				this._side = base.player.channel.owner.hand;
				PlayerLook look = base.player.look;
				look.onPerspectiveUpdated = (PerspectiveUpdated)Delegate.Combine(look.onPerspectiveUpdated, new PerspectiveUpdated(this.onPerspectiveUpdated));
			}
			else if (base.player.third != null)
			{
				this.thirdAnimator = base.player.third.GetComponent<CharacterAnimator>();
				this.thirdAnimator.transform.localScale = new Vector3((float)((!base.channel.owner.hand) ? 1 : -1), 1f, 1f);
				if (!Dedicator.isDedicated)
				{
					this.thirdRenderer_0 = (SkinnedMeshRenderer)this.thirdAnimator.transform.FindChild("Model_0").GetComponent<Renderer>();
					this.thirdRenderer_1 = (SkinnedMeshRenderer)this.thirdAnimator.transform.FindChild("Model_1").GetComponent<Renderer>();
				}
				this._thirdSkeleton = this.thirdAnimator.transform.FindChild("Skeleton");
			}
			if (Dedicator.isDedicated)
			{
				this.thirdSkeleton.gameObject.SetActive(true);
			}
			this.mixAnimation("Gesture_Inventory", true, true, true);
			this.mixAnimation("Gesture_Pickup", false, true);
			this.mixAnimation("Punch_Left", true, false);
			this.mixAnimation("Punch_Right", false, true);
			this.mixAnimation("Gesture_Point", false, true);
			this.mixAnimation("Gesture_Surrender", true, true);
			this.mixAnimation("Gesture_Arrest", true, true);
			this.mixAnimation("Gesture_Wave", true, true, true);
			this.mixAnimation("Gesture_Salute", false, true);
			this.mixAnimation("Gesture_Rest");
			this.mixAnimation("Gesture_Facepalm", false, true, true);
			PlayerLife life = base.player.life;
			life.onLifeUpdated = (LifeUpdated)Delegate.Combine(life.onLifeUpdated, new LifeUpdated(this.onLifeUpdated));
			if (Provider.isServer)
			{
				this.load();
			}
			base.Invoke("init", 0.1f);
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x0010C614 File Offset: 0x0010AA14
		public void load()
		{
			if (PlayerSavedata.fileExists(base.channel.owner.playerID, "/Player/Anim.dat") && Level.info.type == ELevelType.SURVIVAL)
			{
				Block block = PlayerSavedata.readBlock(base.channel.owner.playerID, "/Player/Anim.dat", 0);
				byte b = block.readByte();
				this._gesture = (EPlayerGesture)block.readByte();
				this.captorID = block.readSteamID();
				if (b > 1)
				{
					this.captorItem = block.readUInt16();
				}
				else
				{
					this.captorItem = 0;
				}
				this.captorStrength = block.readUInt16();
				if (this.gesture != EPlayerGesture.ARREST_START)
				{
					this._gesture = EPlayerGesture.NONE;
				}
				return;
			}
			this._gesture = EPlayerGesture.NONE;
			this.captorID = CSteamID.Nil;
			this.captorItem = 0;
			this.captorStrength = 0;
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x0010C6EC File Offset: 0x0010AAEC
		public void save()
		{
			if (base.player.life.isDead)
			{
				if (PlayerSavedata.fileExists(base.channel.owner.playerID, "/Player/Anim.dat"))
				{
					PlayerSavedata.deleteFile(base.channel.owner.playerID, "/Player/Anim.dat");
				}
			}
			else
			{
				Block block = new Block();
				block.writeByte(PlayerAnimator.SAVEDATA_VERSION);
				block.writeByte((byte)this.gesture);
				block.writeSteamID(this.captorID);
				block.writeUInt16(this.captorItem);
				block.writeUInt16(this.captorStrength);
				PlayerSavedata.writeBlock(base.channel.owner.playerID, "/Player/Anim.dat", block);
			}
		}

		// Token: 0x04001B8A RID: 7050
		public static readonly byte SAVEDATA_VERSION = 2;

		// Token: 0x04001B8B RID: 7051
		private static readonly float BOB_SPRINT = 0.075f;

		// Token: 0x04001B8C RID: 7052
		private static readonly float BOB_STAND = 0.05f;

		// Token: 0x04001B8D RID: 7053
		private static readonly float BOB_CROUCH = 0.025f;

		// Token: 0x04001B8E RID: 7054
		private static readonly float BOB_PRONE = 0.0125f;

		// Token: 0x04001B8F RID: 7055
		private static readonly float BOB_SWIM = 0.025f;

		// Token: 0x04001B90 RID: 7056
		private static readonly float TILT_SPRINT = 5f;

		// Token: 0x04001B91 RID: 7057
		private static readonly float TILT_STAND = 3f;

		// Token: 0x04001B92 RID: 7058
		private static readonly float TILT_CROUCH = 2f;

		// Token: 0x04001B93 RID: 7059
		private static readonly float TILT_PRONE = 1f;

		// Token: 0x04001B94 RID: 7060
		private static readonly float TILT_SWIM = 10f;

		// Token: 0x04001B95 RID: 7061
		private static readonly float SPEED_SPRINT = 10f;

		// Token: 0x04001B96 RID: 7062
		private static readonly float SPEED_STAND = 8f;

		// Token: 0x04001B97 RID: 7063
		private static readonly float SPEED_CROUCH = 6f;

		// Token: 0x04001B98 RID: 7064
		private static readonly float SPEED_PRONE = 4f;

		// Token: 0x04001B99 RID: 7065
		private static readonly float SPEED_SWIM = 6f;

		// Token: 0x04001B9A RID: 7066
		public GestureUpdated onGestureUpdated;

		// Token: 0x04001B9B RID: 7067
		public Transform viewmodelLock;

		// Token: 0x04001B9C RID: 7068
		private CharacterAnimator firstAnimator;

		// Token: 0x04001B9D RID: 7069
		private CharacterAnimator thirdAnimator;

		// Token: 0x04001B9E RID: 7070
		private HumanAnimator characterAnimator;

		// Token: 0x04001B9F RID: 7071
		private SkinnedMeshRenderer firstRenderer_0;

		// Token: 0x04001BA0 RID: 7072
		private SkinnedMeshRenderer thirdRenderer_0;

		// Token: 0x04001BA1 RID: 7073
		private SkinnedMeshRenderer thirdRenderer_1;

		// Token: 0x04001BA2 RID: 7074
		private Transform _firstSkeleton;

		// Token: 0x04001BA3 RID: 7075
		private Transform _thirdSkeleton;

		// Token: 0x04001BA4 RID: 7076
		private Transform _view;

		// Token: 0x04001BA5 RID: 7077
		private Camera cam;

		// Token: 0x04001BA6 RID: 7078
		public Vector3 viewOffset;

		// Token: 0x04001BA7 RID: 7079
		public Vector3 driveOffset;

		// Token: 0x04001BA8 RID: 7080
		public Vector3 viewSway;

		// Token: 0x04001BA9 RID: 7081
		private float _multiplier;

		// Token: 0x04001BAA RID: 7082
		public float multiplier;

		// Token: 0x04001BAB RID: 7083
		private float _pref;

		// Token: 0x04001BAC RID: 7084
		public float pref;

		// Token: 0x04001BAD RID: 7085
		private Vector3 viewAdjust;

		// Token: 0x04001BAE RID: 7086
		private Vector3 viewShake;

		// Token: 0x04001BAF RID: 7087
		private Vector3 viewFling;

		// Token: 0x04001BB0 RID: 7088
		private Vector3 viewBob;

		// Token: 0x04001BB1 RID: 7089
		private Vector3 viewPoint;

		// Token: 0x04001BB2 RID: 7090
		private Vector3 viewTilt;

		// Token: 0x04001BB3 RID: 7091
		private Vector3 lockPosition;

		// Token: 0x04001BB4 RID: 7092
		private Quaternion lockRotation;

		// Token: 0x04001BB5 RID: 7093
		private bool isLocked;

		// Token: 0x04001BB6 RID: 7094
		private Vector3 _pos;

		// Token: 0x04001BB7 RID: 7095
		private Vector3 _rot;

		// Token: 0x04001BB8 RID: 7096
		private Vector3 viewRot;

		// Token: 0x04001BB9 RID: 7097
		private float lastYaw;

		// Token: 0x04001BBA RID: 7098
		private float smoothYaw;

		// Token: 0x04001BBB RID: 7099
		private bool _leanLeft;

		// Token: 0x04001BBC RID: 7100
		private bool _leanRight;

		// Token: 0x04001BBD RID: 7101
		private bool leanObstructed;

		// Token: 0x04001BBE RID: 7102
		private float lastTwitch;

		// Token: 0x04001BBF RID: 7103
		private int lastLean;

		// Token: 0x04001BC0 RID: 7104
		private int _lean;

		// Token: 0x04001BC1 RID: 7105
		private float _shoulder;

		// Token: 0x04001BC2 RID: 7106
		private float _shoulder2;

		// Token: 0x04001BC3 RID: 7107
		private bool _side;

		// Token: 0x04001BC4 RID: 7108
		private EPlayerGesture _gesture;

		// Token: 0x04001BC5 RID: 7109
		public CSteamID captorID;

		// Token: 0x04001BC6 RID: 7110
		public ushort captorItem;

		// Token: 0x04001BC7 RID: 7111
		public ushort captorStrength;

		// Token: 0x04001BC8 RID: 7112
		private RaycastHit wall;
	}
}
