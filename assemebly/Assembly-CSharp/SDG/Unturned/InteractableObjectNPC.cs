using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004D8 RID: 1240
	public class InteractableObjectNPC : InteractableObject
	{
		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x000B5E3E File Offset: 0x000B423E
		public ObjectNPCAsset npcAsset
		{
			get
			{
				return this._npcAsset;
			}
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x000B5E48 File Offset: 0x000B4248
		private void updateStance()
		{
			this.stanceActive = null;
			if (this.npcAsset.pose == ENPCPose.SIT)
			{
				if (UnityEngine.Random.value < 0.5f)
				{
					this.stanceIdle = "Idle_Sit";
				}
				else
				{
					this.stanceIdle = "Idle_Drive";
				}
				return;
			}
			if (this.npcAsset.pose == ENPCPose.CROUCH)
			{
				this.stanceIdle = "Idle_Crouch";
				return;
			}
			if (this.npcAsset.pose == ENPCPose.PRONE)
			{
				this.stanceIdle = "Idle_Prone";
				return;
			}
			if (this.npcAsset.pose == ENPCPose.UNDER_ARREST)
			{
				this.stanceIdle = "Gesture_Arrest";
				return;
			}
			if (this.npcAsset.pose == ENPCPose.REST)
			{
				this.stanceIdle = "Gesture_Rest";
				return;
			}
			if (this.npcAsset.pose == ENPCPose.SURRENDER)
			{
				this.stanceIdle = "Gesture_Surrender";
				return;
			}
			if (this.hasEquip || this.npcAsset.pose == ENPCPose.ASLEEP)
			{
				this.stanceIdle = "Idle_Stand";
				return;
			}
			if (UnityEngine.Random.value < 0.5f)
			{
				this.stanceIdle = "Idle_Stand";
			}
			else
			{
				this.stanceIdle = "Idle_Hips";
			}
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x000B5F7B File Offset: 0x000B437B
		private void updateIdle()
		{
			this.lastIdle = Time.time;
			this.idleDelay = UnityEngine.Random.Range(5f, 30f);
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x000B5F9D File Offset: 0x000B439D
		private void updateAnimation()
		{
			this.isEquipped = false;
			this.updateStance();
			this.updateIdle();
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x000B5FB4 File Offset: 0x000B43B4
		public override void updateState(Asset asset, byte[] state)
		{
			base.updateState(asset, state);
			if (!this.isInit)
			{
				this.isInit = true;
				this._npcAsset = (asset as ObjectNPCAsset);
				if (!Dedicator.isDedicated)
				{
					Transform transform = base.transform.FindChild("Root");
					Transform transform2 = transform.FindChild("Skeleton");
					this.skull = transform2.FindChild("Spine").FindChild("Skull");
					this.anim = transform.GetComponent<Animation>();
					this.humanAnim = transform.GetComponent<HumanAnimator>();
					transform.localScale = new Vector3((float)((!this.npcAsset.isBackward) ? 1 : -1), 1f, 1f);
					ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, this.npcAsset.primary);
					ItemAsset itemAsset2 = (ItemAsset)Assets.find(EAssetType.ITEM, this.npcAsset.secondary);
					ItemAsset itemAsset3 = (ItemAsset)Assets.find(EAssetType.ITEM, this.npcAsset.tertiary);
					ItemAsset itemAsset4 = null;
					Transform parent = transform2.FindChild("Spine").FindChild("Primary_Melee");
					Transform parent2 = transform2.FindChild("Spine").FindChild("Primary_Large_Gun");
					Transform parent3 = transform2.FindChild("Spine").FindChild("Primary_Small_Gun");
					Transform parent4 = transform2.FindChild("Right_Hip").FindChild("Right_Leg").FindChild("Secondary_Melee");
					Transform parent5 = transform2.FindChild("Right_Hip").FindChild("Right_Leg").FindChild("Secondary_Gun");
					Transform parent6 = transform2.FindChild("Spine").FindChild("Left_Shoulder").FindChild("Left_Arm").FindChild("Left_Hand").FindChild("Left_Hook");
					Transform parent7 = transform2.FindChild("Spine").FindChild("Right_Shoulder").FindChild("Right_Arm").FindChild("Right_Hand").FindChild("Right_Hook");
					this.clothes = transform.GetComponent<HumanClothes>();
					this.clothes.shirt = this.npcAsset.shirt;
					this.clothes.pants = this.npcAsset.pants;
					this.clothes.hat = this.npcAsset.hat;
					this.clothes.backpack = this.npcAsset.backpack;
					this.clothes.vest = this.npcAsset.vest;
					this.clothes.mask = this.npcAsset.mask;
					this.clothes.glasses = this.npcAsset.glasses;
					this.clothes.face = this.npcAsset.face;
					this.clothes.hair = this.npcAsset.hair;
					this.clothes.beard = this.npcAsset.beard;
					this.clothes.skin = this.npcAsset.skin;
					this.clothes.color = this.npcAsset.color;
					this.clothes.apply();
					if (this.npcAsset.primary != 0 && itemAsset != null)
					{
						Material material;
						Transform item = ItemTool.getItem(itemAsset.id, 0, 100, itemAsset.getState(), false, itemAsset, null, out material, null);
						if (this.npcAsset.equipped == ESlotType.PRIMARY)
						{
							if (itemAsset.isBackward)
							{
								item.transform.parent = parent6;
							}
							else
							{
								item.transform.parent = parent7;
							}
							itemAsset4 = itemAsset;
						}
						else if (itemAsset.type == EItemType.MELEE)
						{
							item.transform.parent = parent;
						}
						else if (itemAsset.slot == ESlotType.PRIMARY)
						{
							item.transform.parent = parent2;
						}
						else
						{
							item.transform.parent = parent3;
						}
						item.localPosition = Vector3.zero;
						item.localRotation = Quaternion.Euler(0f, 0f, 90f);
						item.localScale = Vector3.one;
						UnityEngine.Object.Destroy(item.GetComponent<Collider>());
						Layerer.enemy(item);
					}
					if (this.npcAsset.secondary != 0 && itemAsset2 != null)
					{
						Material material2;
						Transform item2 = ItemTool.getItem(itemAsset2.id, 0, 100, itemAsset2.getState(), false, itemAsset2, null, out material2, null);
						if (this.npcAsset.equipped == ESlotType.SECONDARY)
						{
							if (itemAsset2.isBackward)
							{
								item2.transform.parent = parent6;
							}
							else
							{
								item2.transform.parent = parent7;
							}
							itemAsset4 = itemAsset2;
						}
						else if (itemAsset2.type == EItemType.MELEE)
						{
							item2.transform.parent = parent4;
						}
						else
						{
							item2.transform.parent = parent5;
						}
						item2.localPosition = Vector3.zero;
						item2.localRotation = Quaternion.Euler(0f, 0f, 90f);
						item2.localScale = Vector3.one;
						UnityEngine.Object.Destroy(item2.GetComponent<Collider>());
						Layerer.enemy(item2);
					}
					if (this.npcAsset.tertiary != 0 && itemAsset3 != null && this.npcAsset.equipped == ESlotType.TERTIARY)
					{
						Material material3;
						Transform item3 = ItemTool.getItem(itemAsset3.id, 0, 100, itemAsset3.getState(), false, itemAsset3, null, out material3, null);
						if (itemAsset3.isBackward)
						{
							item3.transform.parent = parent6;
						}
						else
						{
							item3.transform.parent = parent7;
						}
						itemAsset4 = itemAsset3;
						item3.localPosition = Vector3.zero;
						item3.localRotation = Quaternion.Euler(0f, 0f, 90f);
						item3.localScale = Vector3.one;
						UnityEngine.Object.Destroy(item3.GetComponent<Collider>());
						Layerer.enemy(item3);
					}
					if (itemAsset4 != null)
					{
						Transform mix = transform2.FindChild("Spine").FindChild("Left_Shoulder");
						Transform mix2 = transform2.FindChild("Spine").FindChild("Right_Shoulder");
						int i = 0;
						while (i < itemAsset4.animations.Length)
						{
							AnimationClip animationClip = itemAsset4.animations[i];
							if (animationClip.name == "Equip")
							{
								this.hasEquip = true;
								goto IL_683;
							}
							if (animationClip.name == "Sprint_Start" || animationClip.name == "Sprint_Stop")
							{
								this.hasSafety = true;
								goto IL_683;
							}
							if (animationClip.name == "Inspect")
							{
								this.hasInspect = true;
								goto IL_683;
							}
							IL_6E3:
							i++;
							continue;
							IL_683:
							this.anim.AddClip(animationClip, animationClip.name);
							this.anim[animationClip.name].AddMixingTransform(mix, true);
							this.anim[animationClip.name].AddMixingTransform(mix2, true);
							this.anim[animationClip.name].layer = 1;
							goto IL_6E3;
						}
					}
					this.anim["Idle_Kick_Left"].AddMixingTransform(transform2.FindChild("Left_Hip"), true);
					this.anim["Idle_Kick_Left"].layer = 2;
					this.anim["Idle_Kick_Right"].AddMixingTransform(transform2.FindChild("Right_Hip"), true);
					this.anim["Idle_Kick_Right"].layer = 2;
					this.updateAnimation();
				}
			}
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000B6730 File Offset: 0x000B4B30
		public override void use()
		{
			DialogueAsset dialogueAsset = Assets.find(EAssetType.NPC, this.npcAsset.dialogue) as DialogueAsset;
			if (dialogueAsset == null)
			{
				Debug.LogWarning("Failed to find NPC dialogue: " + this.npcAsset.dialogue);
				return;
			}
			ObjectManager.useObjectNPC(base.transform);
			Player.player.quests.checkNPC = this;
			PlayerLifeUI.close();
			PlayerLifeUI.npc = this;
			this.isLookingAtPlayer = true;
			PlayerNPCDialogueUI.open(dialogueAsset, null);
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x000B67AF File Offset: 0x000B4BAF
		public override bool checkUseable()
		{
			return !PlayerUI.window.showCursor && this.npcAsset.dialogue != 0;
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x000B67D4 File Offset: 0x000B4BD4
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			message = EPlayerMessage.TALK;
			text = string.Empty;
			color = Color.white;
			return !PlayerUI.window.showCursor;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x000B67FC File Offset: 0x000B4BFC
		private void Update()
		{
			if (Dedicator.isDedicated)
			{
				return;
			}
			this.humanAnim.lean = this.npcAsset.poseLean;
			this.humanAnim.pitch = this.npcAsset.posePitch;
			this.humanAnim.offset = this.npcAsset.poseHeadOffset;
			if (!string.IsNullOrEmpty(this.stanceActive) && Time.time - this.lastIdle < this.anim[this.stanceActive].length)
			{
				this.anim.CrossFade(this.stanceActive);
			}
			else
			{
				this.stanceActive = null;
				this.anim.CrossFade(this.stanceIdle);
			}
			if (!this.isEquipped)
			{
				this.isEquipped = true;
				if (this.hasEquip)
				{
					this.anim.Play("Equip");
				}
			}
			if (this.hasSafety)
			{
				if (this.isLookingAtPlayer || this.npcAsset.pose == ENPCPose.PASSIVE)
				{
					if (!this.isSafe)
					{
						this.isSafe = true;
						this.anim.Play("Sprint_Start");
					}
					return;
				}
				if (this.isSafe)
				{
					this.isSafe = false;
					this.anim.Play("Sprint_Stop");
					this.updateIdle();
				}
			}
			if (Time.time - this.lastIdle > this.idleDelay)
			{
				this.updateIdle();
				if (this.hasInspect && UnityEngine.Random.value < 0.1f)
				{
					this.anim.Play("Inspect");
				}
				else if (!this.hasEquip && UnityEngine.Random.value < 0.5f)
				{
					if (UnityEngine.Random.value < 0.25f)
					{
						this.updateStance();
					}
					else if (this.npcAsset.pose == ENPCPose.STAND)
					{
						this.stanceActive = "Idle_Hands_" + UnityEngine.Random.Range(0, 5);
					}
				}
				else if (this.npcAsset.pose == ENPCPose.STAND || this.npcAsset.pose == ENPCPose.PASSIVE || this.npcAsset.pose == ENPCPose.UNDER_ARREST || this.npcAsset.pose == ENPCPose.SURRENDER)
				{
					float value = UnityEngine.Random.value;
					if (value < 0.1f)
					{
						if (UnityEngine.Random.value < 0.5f)
						{
							this.anim.Play("Idle_Kick_Left");
						}
						else
						{
							this.anim.Play("Idle_Kick_Right");
						}
					}
					else
					{
						this.stanceActive = "Idle_Paranoid_" + UnityEngine.Random.Range(0, 6);
					}
				}
			}
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x000B6ABC File Offset: 0x000B4EBC
		private void LateUpdate()
		{
			if (Dedicator.isDedicated || this.skull == null || Player.player == null)
			{
				return;
			}
			if (this.npcAsset.pose == ENPCPose.ASLEEP)
			{
				return;
			}
			Vector3 vector = Player.player.look.aim.position + new Vector3(0f, -0.45f, 0f) - this.skull.position;
			if ((this.isLookingAtPlayer || vector.sqrMagnitude < 4f) && Vector3.Dot(vector, -base.transform.up) > 0.15f)
			{
				this.headBlend = Mathf.Lerp(this.headBlend, 1f, 4f * Time.deltaTime);
				if (this.npcAsset.isBackward)
				{
					this.headRotation = Quaternion.Lerp(this.headRotation, Quaternion.LookRotation(vector, Vector3.up) * Quaternion.Euler(0f, 0f, 90f), 4f * Time.deltaTime);
				}
				else
				{
					this.headRotation = Quaternion.Lerp(this.headRotation, Quaternion.LookRotation(vector, Vector3.up) * Quaternion.Euler(0f, 0f, -90f), 4f * Time.deltaTime);
				}
			}
			else
			{
				this.headBlend = Mathf.Lerp(this.headBlend, 0f, 4f * Time.deltaTime);
			}
			if (this.headBlend < 0.01f)
			{
				return;
			}
			this.skull.rotation = Quaternion.Lerp(this.skull.rotation, this.headRotation, this.headBlend);
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x000B6C95 File Offset: 0x000B5095
		private void OnEnable()
		{
			if (!Dedicator.isDedicated)
			{
				this.updateAnimation();
			}
		}

		// Token: 0x040013DB RID: 5083
		protected ObjectNPCAsset _npcAsset;

		// Token: 0x040013DC RID: 5084
		public bool isLookingAtPlayer;

		// Token: 0x040013DD RID: 5085
		private bool isInit;

		// Token: 0x040013DE RID: 5086
		private Animation anim;

		// Token: 0x040013DF RID: 5087
		private HumanAnimator humanAnim;

		// Token: 0x040013E0 RID: 5088
		private HumanClothes clothes;

		// Token: 0x040013E1 RID: 5089
		private Transform skull;

		// Token: 0x040013E2 RID: 5090
		private bool hasEquip;

		// Token: 0x040013E3 RID: 5091
		private bool hasSafety;

		// Token: 0x040013E4 RID: 5092
		private bool hasInspect;

		// Token: 0x040013E5 RID: 5093
		private bool isEquipped;

		// Token: 0x040013E6 RID: 5094
		private bool isSafe;

		// Token: 0x040013E7 RID: 5095
		private string stanceIdle;

		// Token: 0x040013E8 RID: 5096
		private string stanceActive;

		// Token: 0x040013E9 RID: 5097
		private float lastIdle;

		// Token: 0x040013EA RID: 5098
		private float idleDelay;

		// Token: 0x040013EB RID: 5099
		private float headBlend;

		// Token: 0x040013EC RID: 5100
		private Quaternion headRotation;
	}
}
