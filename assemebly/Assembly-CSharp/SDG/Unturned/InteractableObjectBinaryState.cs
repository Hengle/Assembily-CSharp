using System;
using Pathfinding;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004D6 RID: 1238
	public class InteractableObjectBinaryState : InteractableObject
	{
		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x000B5533 File Offset: 0x000B3933
		public bool isUsed
		{
			get
			{
				return this._isUsed;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x000B553B File Offset: 0x000B393B
		public bool isUsable
		{
			get
			{
				return Time.realtimeSinceStartup - this.lastUsed > base.objectAsset.interactabilityDelay && (base.objectAsset.interactabilityPower == EObjectInteractabilityPower.NONE || base.isWired);
			}
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x000B5575 File Offset: 0x000B3975
		public bool checkCanReset(float multiplier)
		{
			return this.isUsed && base.objectAsset.interactabilityReset > 1f && Time.realtimeSinceStartup - this.lastUsed > base.objectAsset.interactabilityReset * multiplier;
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x000B55B8 File Offset: 0x000B39B8
		private void initAnimationComponent()
		{
			Transform transform = base.transform.FindChild("Root");
			if (transform != null)
			{
				this.animationComponent = transform.GetComponent<Animation>();
			}
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x000B55F0 File Offset: 0x000B39F0
		private void updateAnimationComponent(bool applyInstantly)
		{
			if (this.animationComponent != null)
			{
				if (this.isUsed)
				{
					this.animationComponent.Play("Open");
				}
				else
				{
					this.animationComponent.Play("Close");
				}
				if (applyInstantly)
				{
					if (this.isUsed)
					{
						this.animationComponent["Open"].normalizedTime = 1f;
					}
					else
					{
						this.animationComponent["Close"].normalizedTime = 1f;
					}
				}
			}
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x000B568A File Offset: 0x000B3A8A
		private void initAudioSourceComponent()
		{
			this.audioSourceComponent = base.transform.GetComponent<AudioSource>();
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x000B569D File Offset: 0x000B3A9D
		private void updateAudioSourceComponent()
		{
			if (this.audioSourceComponent != null && !Dedicator.isDedicated)
			{
				this.audioSourceComponent.Play();
			}
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x000B56C8 File Offset: 0x000B3AC8
		private void initCutComponent()
		{
			if (base.objectAsset.interactabilityNav != EObjectInteractabilityNav.NONE)
			{
				Transform transform = base.transform.FindChild("Nav");
				if (transform != null)
				{
					Transform transform2 = transform.FindChild("Blocker");
					if (transform2 != null)
					{
						this.cutComponent = transform2.GetComponent<NavmeshCut>();
						this.cutHeight = this.cutComponent.height;
					}
				}
			}
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x000B5738 File Offset: 0x000B3B38
		private void updateCutComponent()
		{
			if (this.cutComponent != null)
			{
				if ((base.objectAsset.interactabilityNav == EObjectInteractabilityNav.ON && !this.isUsed) || (base.objectAsset.interactabilityNav == EObjectInteractabilityNav.OFF && this.isUsed))
				{
					this.cutHeight = this.cutComponent.height;
					this.cutComponent.height = 0f;
				}
				else
				{
					this.cutComponent.height = this.cutHeight;
				}
				this.cutComponent.ForceUpdate();
			}
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x000B57D0 File Offset: 0x000B3BD0
		private void initToggleGameObject()
		{
			Transform transform = base.transform.FindChildRecursive("Toggle");
			LightLODTool.applyLightLOD(transform);
			if (transform != null)
			{
				this.material = HighlighterTool.getMaterialInstance(transform.parent);
				this.toggleGameObject = transform.gameObject;
			}
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x000B5820 File Offset: 0x000B3C20
		private void updateToggleGameObject()
		{
			if (this.toggleGameObject != null)
			{
				if (base.objectAsset.interactabilityPower == EObjectInteractabilityPower.STAY)
				{
					if (this.material != null)
					{
						this.material.SetColor("_EmissionColor", (!this.isUsed || !base.isWired) ? Color.black : Color.white);
					}
					this.toggleGameObject.SetActive(this.isUsed && base.isWired);
				}
				else
				{
					if (this.material != null)
					{
						this.material.SetColor("_EmissionColor", (!this.isUsed) ? Color.black : Color.white);
					}
					this.toggleGameObject.SetActive(this.isUsed);
				}
			}
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x000B5905 File Offset: 0x000B3D05
		public void updateToggle(bool newUsed)
		{
			this.lastUsed = Time.realtimeSinceStartup;
			this._isUsed = newUsed;
			this.updateAnimationComponent(false);
			this.updateCutComponent();
			this.updateAudioSourceComponent();
			this.updateToggleGameObject();
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x000B5932 File Offset: 0x000B3D32
		protected override void updateWired()
		{
			this.updateToggleGameObject();
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x000B593C File Offset: 0x000B3D3C
		public override void updateState(Asset asset, byte[] state)
		{
			base.updateState(asset, state);
			this._isUsed = (state[0] == 1);
			if (!this.isInit)
			{
				this.isInit = true;
				this.initAnimationComponent();
				this.initCutComponent();
				this.initAudioSourceComponent();
				this.initToggleGameObject();
			}
			this.updateAnimationComponent(true);
			this.updateCutComponent();
			this.updateToggleGameObject();
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x000B599A File Offset: 0x000B3D9A
		public override void use()
		{
			ObjectManager.toggleObjectBinaryState(base.transform);
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x000B59A7 File Offset: 0x000B3DA7
		public override bool checkInteractable()
		{
			return !base.objectAsset.interactabilityRemote;
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x000B59B7 File Offset: 0x000B3DB7
		public override bool checkUseable()
		{
			return (base.objectAsset.interactabilityPower == EObjectInteractabilityPower.NONE || base.isWired) && base.objectAsset.areInteractabilityConditionsMet(Player.player);
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x000B59E8 File Offset: 0x000B3DE8
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			for (int i = 0; i < base.objectAsset.interactabilityConditions.Length; i++)
			{
				INPCCondition inpccondition = base.objectAsset.interactabilityConditions[i];
				if (!inpccondition.isConditionMet(Player.player))
				{
					message = EPlayerMessage.CONDITION;
					text = inpccondition.formatCondition(Player.player);
					color = Color.white;
					return true;
				}
			}
			if (base.objectAsset.interactabilityPower != EObjectInteractabilityPower.NONE && !base.isWired)
			{
				message = EPlayerMessage.POWER;
			}
			else if (this.isUsed)
			{
				switch (base.objectAsset.interactabilityHint)
				{
				case EObjectInteractabilityHint.DOOR:
					message = EPlayerMessage.DOOR_CLOSE;
					break;
				case EObjectInteractabilityHint.SWITCH:
					message = EPlayerMessage.SPOT_OFF;
					break;
				case EObjectInteractabilityHint.FIRE:
					message = EPlayerMessage.FIRE_OFF;
					break;
				case EObjectInteractabilityHint.GENERATOR:
					message = EPlayerMessage.GENERATOR_OFF;
					break;
				case EObjectInteractabilityHint.USE:
					message = EPlayerMessage.USE;
					break;
				default:
					message = EPlayerMessage.NONE;
					break;
				}
			}
			else
			{
				switch (base.objectAsset.interactabilityHint)
				{
				case EObjectInteractabilityHint.DOOR:
					message = EPlayerMessage.DOOR_OPEN;
					break;
				case EObjectInteractabilityHint.SWITCH:
					message = EPlayerMessage.SPOT_ON;
					break;
				case EObjectInteractabilityHint.FIRE:
					message = EPlayerMessage.FIRE_ON;
					break;
				case EObjectInteractabilityHint.GENERATOR:
					message = EPlayerMessage.GENERATOR_ON;
					break;
				case EObjectInteractabilityHint.USE:
					message = EPlayerMessage.USE;
					break;
				default:
					message = EPlayerMessage.NONE;
					break;
				}
			}
			text = string.Empty;
			color = Color.white;
			return true;
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x000B5B56 File Offset: 0x000B3F56
		private void OnEnable()
		{
			this.updateAnimationComponent(true);
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x000B5B5F File Offset: 0x000B3F5F
		private void OnDestroy()
		{
			if (this.material != null)
			{
				UnityEngine.Object.DestroyImmediate(this.material);
			}
		}

		// Token: 0x040013CD RID: 5069
		private bool _isUsed;

		// Token: 0x040013CE RID: 5070
		private bool isInit;

		// Token: 0x040013CF RID: 5071
		private float lastUsed = -9999f;

		// Token: 0x040013D0 RID: 5072
		private Animation animationComponent;

		// Token: 0x040013D1 RID: 5073
		private AudioSource audioSourceComponent;

		// Token: 0x040013D2 RID: 5074
		private NavmeshCut cutComponent;

		// Token: 0x040013D3 RID: 5075
		private float cutHeight;

		// Token: 0x040013D4 RID: 5076
		private Material material;

		// Token: 0x040013D5 RID: 5077
		private GameObject toggleGameObject;
	}
}
