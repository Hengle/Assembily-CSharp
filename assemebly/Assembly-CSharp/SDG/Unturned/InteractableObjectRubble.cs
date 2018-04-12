using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004DE RID: 1246
	public class InteractableObjectRubble : MonoBehaviour
	{
		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x000B71B7 File Offset: 0x000B55B7
		// (set) Token: 0x06002189 RID: 8585 RVA: 0x000B71BF File Offset: 0x000B55BF
		public ObjectAsset asset { get; protected set; }

		// Token: 0x0600218A RID: 8586 RVA: 0x000B71C8 File Offset: 0x000B55C8
		public byte getSectionCount()
		{
			return (byte)this.rubbleInfos.Length;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x000B71D3 File Offset: 0x000B55D3
		public Transform getSection(byte section)
		{
			return this.rubbleInfos[(int)section].section;
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x000B71E4 File Offset: 0x000B55E4
		public bool isAllAlive()
		{
			byte b = 0;
			while ((int)b < this.rubbleInfos.Length)
			{
				RubbleInfo rubbleInfo = this.rubbleInfos[(int)b];
				if (rubbleInfo.isDead)
				{
					return false;
				}
				b += 1;
			}
			return true;
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x000B7224 File Offset: 0x000B5624
		public bool isAllDead()
		{
			byte b = 0;
			while ((int)b < this.rubbleInfos.Length)
			{
				RubbleInfo rubbleInfo = this.rubbleInfos[(int)b];
				if (!rubbleInfo.isDead)
				{
					return false;
				}
				b += 1;
			}
			return true;
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x000B7262 File Offset: 0x000B5662
		public bool isSectionDead(byte section)
		{
			return this.rubbleInfos[(int)section].isDead;
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x000B7274 File Offset: 0x000B5674
		public void askDamage(byte section, ushort amount)
		{
			if (section == 255)
			{
				section = 0;
				while ((int)section < this.rubbleInfos.Length)
				{
					this.rubbleInfos[(int)section].askDamage(amount);
					section += 1;
				}
			}
			else
			{
				this.rubbleInfos[(int)section].askDamage(amount);
			}
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x000B72CC File Offset: 0x000B56CC
		public byte checkCanReset(float multiplier)
		{
			byte b = 0;
			while ((int)b < this.rubbleInfos.Length)
			{
				if (this.rubbleInfos[(int)b].isDead && this.asset.rubbleReset > 1f && Time.realtimeSinceStartup - this.rubbleInfos[(int)b].lastDead > this.asset.rubbleReset * multiplier)
				{
					return b;
				}
				b += 1;
			}
			return byte.MaxValue;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x000B7348 File Offset: 0x000B5748
		public byte getSection(Transform section)
		{
			byte b = 0;
			while ((int)b < this.rubbleInfos.Length)
			{
				RubbleInfo rubbleInfo = this.rubbleInfos[(int)b];
				if (section == rubbleInfo.section || section.parent == rubbleInfo.section || section.parent.parent == rubbleInfo.section)
				{
					return b;
				}
				b += 1;
			}
			return byte.MaxValue;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x000B73C4 File Offset: 0x000B57C4
		public void updateRubble(byte section, bool isAlive, bool playEffect, Vector3 ragdoll)
		{
			RubbleInfo rubbleInfo = this.rubbleInfos[(int)section];
			if (isAlive)
			{
				rubbleInfo.health = this.asset.rubbleHealth;
			}
			else
			{
				rubbleInfo.lastDead = Time.realtimeSinceStartup;
				rubbleInfo.health = 0;
			}
			bool flag = this.isAllDead();
			if (rubbleInfo.aliveGameObject != null)
			{
				rubbleInfo.aliveGameObject.SetActive(!rubbleInfo.isDead);
			}
			if (rubbleInfo.deadGameObject != null)
			{
				rubbleInfo.deadGameObject.SetActive(rubbleInfo.isDead && (!flag || this.asset.rubbleFinale == 0));
			}
			if (this.aliveGameObject != null)
			{
				this.aliveGameObject.SetActive(!flag);
			}
			if (this.deadGameObject != null)
			{
				this.deadGameObject.SetActive(flag);
			}
			if (!Dedicator.isDedicated && playEffect)
			{
				if (rubbleInfo.ragdolls != null && GraphicsSettings.debris && rubbleInfo.isDead)
				{
					for (int i = 0; i < rubbleInfo.ragdolls.Length; i++)
					{
						RubbleRagdollInfo rubbleRagdollInfo = rubbleInfo.ragdolls[i];
						if (rubbleRagdollInfo != null)
						{
							Vector3 vector = ragdoll;
							if (rubbleRagdollInfo.forceTransform != null)
							{
								vector = rubbleRagdollInfo.forceTransform.forward * vector.magnitude * rubbleRagdollInfo.forceTransform.localScale.z;
								vector += rubbleRagdollInfo.forceTransform.right * UnityEngine.Random.Range(-16f, 16f) * rubbleRagdollInfo.forceTransform.localScale.x;
								vector += rubbleRagdollInfo.forceTransform.up * UnityEngine.Random.Range(-16f, 16f) * rubbleRagdollInfo.forceTransform.localScale.y;
							}
							else
							{
								vector.y += 8f;
								vector.x += UnityEngine.Random.Range(-16f, 16f);
								vector.z += UnityEngine.Random.Range(-16f, 16f);
							}
							vector *= (float)((!(Player.player != null) || Player.player.skills.boost != EPlayerBoost.FLIGHT) ? 2 : 4);
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(rubbleRagdollInfo.ragdollGameObject, rubbleRagdollInfo.ragdollGameObject.transform.position, rubbleRagdollInfo.ragdollGameObject.transform.rotation);
							gameObject.name = "Ragdoll";
							gameObject.transform.parent = Level.effects;
							gameObject.transform.localScale = base.transform.localScale;
							gameObject.SetActive(true);
							gameObject.gameObject.AddComponent<Rigidbody>();
							gameObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
							gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
							gameObject.GetComponent<Rigidbody>().AddForce(vector);
							gameObject.GetComponent<Rigidbody>().drag = 0.5f;
							gameObject.GetComponent<Rigidbody>().angularDrag = 0.1f;
							UnityEngine.Object.Destroy(gameObject, 8f);
						}
					}
				}
				if (this.asset.rubbleEffect != 0 && rubbleInfo.isDead)
				{
					if (rubbleInfo.effectTransform != null)
					{
						EffectManager.effect(this.asset.rubbleEffect, rubbleInfo.effectTransform.position, rubbleInfo.effectTransform.forward);
					}
					else
					{
						EffectManager.effect(this.asset.rubbleEffect, rubbleInfo.section.position, Vector3.up);
					}
				}
				if (this.asset.rubbleFinale != 0 && flag)
				{
					if (this.finaleTransform != null)
					{
						EffectManager.effect(this.asset.rubbleFinale, this.finaleTransform.position, this.finaleTransform.forward);
					}
					else
					{
						EffectManager.effect(this.asset.rubbleFinale, base.transform.position, Vector3.up);
					}
				}
			}
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x000B781C File Offset: 0x000B5C1C
		public void updateState(Asset asset, byte[] state)
		{
			this.asset = (asset as ObjectAsset);
			Transform transform = base.transform.FindChild("Sections");
			if (transform != null)
			{
				this.rubbleInfos = new RubbleInfo[transform.childCount];
				for (int i = 0; i < this.rubbleInfos.Length; i++)
				{
					Transform section = transform.FindChild("Section_" + i);
					RubbleInfo rubbleInfo = new RubbleInfo();
					rubbleInfo.section = section;
					this.rubbleInfos[i] = rubbleInfo;
				}
				Transform transform2 = base.transform.FindChild("Alive");
				if (transform2 != null)
				{
					this.aliveGameObject = transform2.gameObject;
				}
				Transform transform3 = base.transform.FindChild("Dead");
				if (transform3 != null)
				{
					this.deadGameObject = transform3.gameObject;
				}
				this.finaleTransform = base.transform.FindChild("Finale");
			}
			else
			{
				this.rubbleInfos = new RubbleInfo[1];
				RubbleInfo rubbleInfo2 = new RubbleInfo();
				rubbleInfo2.section = base.transform;
				this.rubbleInfos[0] = rubbleInfo2;
			}
			byte b = 0;
			while ((int)b < this.rubbleInfos.Length)
			{
				RubbleInfo rubbleInfo3 = this.rubbleInfos[(int)b];
				Transform section2 = rubbleInfo3.section;
				Transform transform4 = section2.FindChild("Alive");
				if (transform4 != null)
				{
					rubbleInfo3.aliveGameObject = transform4.gameObject;
				}
				Transform transform5 = section2.FindChild("Dead");
				if (transform5 != null)
				{
					rubbleInfo3.deadGameObject = transform5.gameObject;
				}
				Transform transform6 = section2.FindChild("Ragdolls");
				if (transform6 != null)
				{
					rubbleInfo3.ragdolls = new RubbleRagdollInfo[transform6.childCount];
					for (int j = 0; j < rubbleInfo3.ragdolls.Length; j++)
					{
						Transform transform7 = transform6.FindChild("Ragdoll_" + j);
						Transform transform8 = transform7.FindChild("Ragdoll");
						if (transform8 != null)
						{
							rubbleInfo3.ragdolls[j] = new RubbleRagdollInfo();
							rubbleInfo3.ragdolls[j].ragdollGameObject = transform8.gameObject;
							rubbleInfo3.ragdolls[j].forceTransform = transform7.FindChild("Force");
						}
					}
				}
				else
				{
					Transform transform9 = section2.FindChild("Ragdoll");
					if (transform9 != null)
					{
						rubbleInfo3.ragdolls = new RubbleRagdollInfo[1];
						rubbleInfo3.ragdolls[0] = new RubbleRagdollInfo();
						rubbleInfo3.ragdolls[0].ragdollGameObject = transform9.gameObject;
						rubbleInfo3.ragdolls[0].forceTransform = section2.FindChild("Force");
					}
				}
				rubbleInfo3.effectTransform = section2.FindChild("Effect");
				b += 1;
			}
			byte b2 = 0;
			while ((int)b2 < this.rubbleInfos.Length)
			{
				bool isAlive = (state[state.Length - 1] & Types.SHIFTS[(int)b2]) == Types.SHIFTS[(int)b2];
				this.updateRubble(b2, isAlive, false, Vector3.zero);
				b2 += 1;
			}
		}

		// Token: 0x040013FB RID: 5115
		private RubbleInfo[] rubbleInfos;

		// Token: 0x040013FC RID: 5116
		private GameObject aliveGameObject;

		// Token: 0x040013FD RID: 5117
		private GameObject deadGameObject;

		// Token: 0x040013FE RID: 5118
		private Transform finaleTransform;
	}
}
