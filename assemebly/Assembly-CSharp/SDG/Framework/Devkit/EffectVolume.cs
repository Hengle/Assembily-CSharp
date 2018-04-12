using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.IO.FormattedFiles;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x0200013C RID: 316
	public class EffectVolume : DevkitHierarchyVolume, IDevkitHierarchySpawnable
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x00050096 File Offset: 0x0004E496
		public EffectVolume()
		{
			this._emissionMultiplier = 1f;
			this._audioRangeMultiplier = 1f;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x000500B4 File Offset: 0x0004E4B4
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x000500BC File Offset: 0x0004E4BC
		[Inspectable("#SDG::ID", null)]
		public ushort id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
				if (this.effect != null)
				{
					UnityEngine.Object.Destroy(this.effect.gameObject);
					this.effect = null;
				}
				EffectAsset effectAsset = Assets.find(EAssetType.EFFECT, this.id) as EffectAsset;
				if (effectAsset != null)
				{
					this.effect = UnityEngine.Object.Instantiate<GameObject>(effectAsset.effect).transform;
					this.effect.name = "Effect";
					this.effect.transform.parent = base.transform;
					this.effect.transform.localPosition = new Vector3(0f, 0f, 0f);
					this.effect.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
					this.effect.transform.localScale = new Vector3(1f, 1f, 1f);
					ParticleSystem component = this.effect.GetComponent<ParticleSystem>();
					if (component != null)
					{
						this.maxParticlesBase = component.main.maxParticles;
						this.rateOverTimeBase = component.emission.rateOverTimeMultiplier;
					}
					AudioSource component2 = this.effect.GetComponent<AudioSource>();
					if (component2 != null && component2.clip != null)
					{
						component2.time = UnityEngine.Random.Range(0f, component2.clip.length);
					}
				}
				if (this.effect != null)
				{
					this.applyEmission();
					this.applyAudioRange();
				}
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0005025E File Offset: 0x0004E65E
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x00050266 File Offset: 0x0004E666
		[Inspectable("#SDG::Emission", null)]
		public float emissionMultiplier
		{
			get
			{
				return this._emissionMultiplier;
			}
			set
			{
				this._emissionMultiplier = value;
				if (this.effect != null)
				{
					this.applyEmission();
				}
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00050286 File Offset: 0x0004E686
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x0005028E File Offset: 0x0004E68E
		[Inspectable("#SDG::Audio_Range", null)]
		public float audioRangeMultiplier
		{
			get
			{
				return this._audioRangeMultiplier;
			}
			set
			{
				this._audioRangeMultiplier = value;
				if (this.effect != null)
				{
					this.applyAudioRange();
				}
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x000502B0 File Offset: 0x0004E6B0
		protected virtual void applyEmission()
		{
			if (this.effect == null)
			{
				return;
			}
			ParticleSystem component = this.effect.GetComponent<ParticleSystem>();
			if (component == null)
			{
				return;
			}
			component.main.maxParticles = (int)((float)this.maxParticlesBase * this.emissionMultiplier);
			component.emission.rateOverTimeMultiplier = this.rateOverTimeBase * this.emissionMultiplier;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00050320 File Offset: 0x0004E720
		protected virtual void applyAudioRange()
		{
			if (this.effect == null)
			{
				return;
			}
			AudioSource component = this.effect.GetComponent<AudioSource>();
			if (component == null)
			{
				return;
			}
			component.maxDistance = this.audioRangeMultiplier;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00050364 File Offset: 0x0004E764
		public void devkitHierarchySpawn()
		{
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00050366 File Offset: 0x0004E766
		protected virtual void updateBoxEnabled()
		{
			base.box.enabled = (Level.isEditor && EffectVolumeSystem.effectVisibilityGroup.isVisible);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0005038A File Offset: 0x0004E78A
		protected virtual void handleVisibilityGroupIsVisibleChanged(IVisibilityGroup group)
		{
			this.updateBoxEnabled();
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00050394 File Offset: 0x0004E794
		protected override void readHierarchyItem(IFormattedFileReader reader)
		{
			base.readHierarchyItem(reader);
			if (reader.containsKey("Emission"))
			{
				this._emissionMultiplier = reader.readValue<float>("Emission");
			}
			if (reader.containsKey("Audio_Range"))
			{
				this._audioRangeMultiplier = reader.readValue<float>("Audio_Range");
			}
			this.id = reader.readValue<ushort>("ID");
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x000503FB File Offset: 0x0004E7FB
		protected override void writeHierarchyItem(IFormattedFileWriter writer)
		{
			base.writeHierarchyItem(writer);
			writer.writeValue<ushort>("ID", this.id);
			writer.writeValue<float>("Emission", this.emissionMultiplier);
			writer.writeValue<float>("Audio_Range", this.audioRangeMultiplier);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00050437 File Offset: 0x0004E837
		protected void OnEnable()
		{
			LevelHierarchy.addItem(this);
			EffectVolumeSystem.addVolume(this);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00050445 File Offset: 0x0004E845
		protected void OnDisable()
		{
			EffectVolumeSystem.removeVolume(this);
			LevelHierarchy.removeItem(this);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00050454 File Offset: 0x0004E854
		protected void Awake()
		{
			base.name = "Effect_Volume";
			base.gameObject.layer = LayerMasks.CLIP;
			base.box = base.gameObject.getOrAddComponent<BoxCollider>();
			this.updateBoxEnabled();
			EffectVolumeSystem.effectVisibilityGroup.isVisibleChanged += this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x000504AA File Offset: 0x0004E8AA
		protected void Start()
		{
			this.effect = base.transform.FindChild("Effect");
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x000504C2 File Offset: 0x0004E8C2
		protected void OnDestroy()
		{
			EffectVolumeSystem.effectVisibilityGroup.isVisibleChanged -= this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x04000734 RID: 1844
		[SerializeField]
		protected ushort _id;

		// Token: 0x04000735 RID: 1845
		[SerializeField]
		protected int maxParticlesBase;

		// Token: 0x04000736 RID: 1846
		[SerializeField]
		protected float rateOverTimeBase;

		// Token: 0x04000737 RID: 1847
		[SerializeField]
		protected float _emissionMultiplier;

		// Token: 0x04000738 RID: 1848
		[SerializeField]
		protected float _audioRangeMultiplier;

		// Token: 0x04000739 RID: 1849
		protected Transform effect;
	}
}
