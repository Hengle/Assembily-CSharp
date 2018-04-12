using System;
using SDG.Framework.Water;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007B6 RID: 1974
	public class UseableFisher : Useable
	{
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06003967 RID: 14695 RVA: 0x001AA04A File Offset: 0x001A844A
		private bool isCastable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedCast > this.castTime;
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06003968 RID: 14696 RVA: 0x001AA060 File Offset: 0x001A8460
		private bool isReelable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedReel > this.reelTime;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06003969 RID: 14697 RVA: 0x001AA078 File Offset: 0x001A8478
		private bool isBobable
		{
			get
			{
				return (!this.isCasting) ? (Time.realtimeSinceStartup - this.startedReel > this.reelTime * 0.75f) : (Time.realtimeSinceStartup - this.startedCast > this.castTime * 0.45f);
			}
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x001AA0CC File Offset: 0x001A84CC
		private void reel()
		{
			if (!Dedicator.isDedicated)
			{
				base.player.playSound(((ItemFisherAsset)base.player.equipment.asset).reel);
			}
			base.player.animator.play("Reel", false);
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x001AA11E File Offset: 0x001A851E
		private void startStrength()
		{
			PlayerLifeUI.close();
			if (this.castStrengthBox != null)
			{
				this.castStrengthBox.isVisible = true;
			}
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x001AA13C File Offset: 0x001A853C
		private void stopStrength()
		{
			PlayerLifeUI.open();
			if (this.castStrengthBox != null)
			{
				this.castStrengthBox.isVisible = false;
			}
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x001AA15C File Offset: 0x001A855C
		[SteamCall]
		public void askCatch(CSteamID steamID)
		{
			if (base.channel.checkOwner(steamID) && (Time.realtimeSinceStartup - this.lastLuck > this.luckTime - 2.4f || (this.hasLuckReset && Time.realtimeSinceStartup - this.lastLuck < 1f)))
			{
				this.isCatch = true;
			}
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x001AA1BF File Offset: 0x001A85BF
		[SteamCall]
		public void askReel(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.reel();
			}
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x001AA1F0 File Offset: 0x001A85F0
		private void cast()
		{
			if (!Dedicator.isDedicated)
			{
				base.player.playSound(((ItemFisherAsset)base.player.equipment.asset).cast);
			}
			base.player.animator.play("Cast", false);
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x001AA242 File Offset: 0x001A8642
		[SteamCall]
		public void askCast(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.cast();
			}
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x001AA270 File Offset: 0x001A8670
		public override void startPrimary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			if (this.isFishing)
			{
				this.isFishing = false;
				base.player.equipment.isBusy = true;
				this.startedReel = Time.realtimeSinceStartup;
				this.isReeling = true;
				if (base.channel.isOwner)
				{
					this.isBobbing = true;
					if (Time.realtimeSinceStartup - this.lastLuck > this.luckTime - 1.4f && Time.realtimeSinceStartup - this.lastLuck < this.luckTime)
					{
						base.channel.send("askCatch", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[0]);
					}
				}
				this.reel();
				if (Provider.isServer)
				{
					if (this.isCatch)
					{
						this.isCatch = false;
						ushort num = SpawnTableTool.resolve(((ItemFisherAsset)base.player.equipment.asset).rewardID);
						if (num != 0)
						{
							base.player.inventory.forceAddItem(new Item(num, EItemOrigin.NATURE), false);
						}
						base.player.sendStat(EPlayerStat.FOUND_FISHES);
						base.player.skills.askPay(3u);
					}
					base.channel.send("askReel", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
					AlertTool.alert(base.transform.position, 8f);
				}
			}
			else
			{
				this.isStrengthening = true;
				this.strengthTime = 0u;
				this.strengthMultiplier = 0f;
				if (base.channel.isOwner)
				{
					this.startStrength();
				}
			}
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x001AA40C File Offset: 0x001A880C
		public override void stopPrimary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			if (!this.isStrengthening)
			{
				return;
			}
			this.isStrengthening = false;
			if (base.channel.isOwner)
			{
				this.stopStrength();
			}
			this.isFishing = true;
			base.player.equipment.isBusy = true;
			this.startedCast = Time.realtimeSinceStartup;
			this.isCasting = true;
			if (base.channel.isOwner)
			{
				this.isBobbing = true;
			}
			this.resetLuck();
			this.hasLuckReset = false;
			this.cast();
			if (Provider.isServer)
			{
				base.channel.send("askCast", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
				AlertTool.alert(base.transform.position, 8f);
			}
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x001AA4E4 File Offset: 0x001A88E4
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			this.castTime = base.player.animator.getAnimationLength("Cast");
			this.reelTime = base.player.animator.getAnimationLength("Reel");
			if (base.channel.isOwner)
			{
				this.firstHook = base.player.equipment.firstModel.FindChild("Hook");
				this.thirdHook = base.player.equipment.thirdModel.FindChild("Hook");
				this.firstLine = (LineRenderer)base.player.equipment.firstModel.FindChild("Line").GetComponent<Renderer>();
				this.firstLine.tag = "Viewmodel";
				this.firstLine.gameObject.layer = LayerMasks.VIEWMODEL;
				this.firstLine.gameObject.SetActive(true);
				this.thirdLine = (LineRenderer)base.player.equipment.thirdModel.FindChild("Line").GetComponent<Renderer>();
				this.thirdLine.gameObject.SetActive(true);
				this.castStrengthBox = new SleekBox();
				this.castStrengthBox.positionOffset_X = -20;
				this.castStrengthBox.positionOffset_Y = -110;
				this.castStrengthBox.positionScale_X = 0.5f;
				this.castStrengthBox.positionScale_Y = 0.5f;
				this.castStrengthBox.sizeOffset_X = 40;
				this.castStrengthBox.sizeOffset_Y = 220;
				PlayerUI.container.add(this.castStrengthBox);
				this.castStrengthBox.isVisible = false;
				this.castStrengthArea = new Sleek();
				this.castStrengthArea.positionOffset_X = 10;
				this.castStrengthArea.positionOffset_Y = 10;
				this.castStrengthArea.sizeOffset_X = -20;
				this.castStrengthArea.sizeOffset_Y = -20;
				this.castStrengthArea.sizeScale_X = 1f;
				this.castStrengthArea.sizeScale_Y = 1f;
				this.castStrengthBox.add(this.castStrengthArea);
				this.castStrengthBar = new SleekImageTexture();
				this.castStrengthBar.sizeScale_X = 1f;
				this.castStrengthBar.sizeScale_Y = 1f;
				this.castStrengthBar.texture = (Texture2D)Resources.Load("Materials/Pixel");
				this.castStrengthArea.add(this.castStrengthBar);
			}
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x001AA76C File Offset: 0x001A8B6C
		public override void dequip()
		{
			if (base.channel.isOwner)
			{
				if (this.bob != null)
				{
					UnityEngine.Object.Destroy(this.bob.gameObject);
				}
				if (this.castStrengthBox != null)
				{
					PlayerUI.container.remove(this.castStrengthBox);
				}
				if (this.isStrengthening)
				{
					PlayerLifeUI.open();
				}
			}
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x001AA7D8 File Offset: 0x001A8BD8
		public override void tock(uint clock)
		{
			if (!this.isStrengthening)
			{
				return;
			}
			this.strengthTime += 1u;
			uint num = (uint)(100 + base.player.skills.skills[2][4].level * 20);
			this.strengthMultiplier = 1f - Mathf.Abs(Mathf.Sin((this.strengthTime + num / 2u) % num / num * 3.14159274f));
			this.strengthMultiplier *= this.strengthMultiplier;
			if (base.channel.isOwner && this.castStrengthBar != null)
			{
				this.castStrengthBar.positionScale_Y = 1f - this.strengthMultiplier;
				this.castStrengthBar.sizeScale_Y = this.strengthMultiplier;
				this.castStrengthBar.backgroundColor = ItemTool.getQualityColor(this.strengthMultiplier);
			}
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x001AA8BC File Offset: 0x001A8CBC
		public override void tick()
		{
			if (!base.player.equipment.isEquipped)
			{
				return;
			}
			if (base.channel.isOwner)
			{
				if (this.isBobable && this.isBobbing)
				{
					if (this.isCasting)
					{
						this.bob = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Fishers/Bob"))).transform;
						this.bob.name = "Bob";
						this.bob.parent = Level.effects;
						this.bob.position = base.player.look.aim.position + base.player.look.aim.forward;
						this.bob.GetComponent<Rigidbody>().AddForce(base.player.look.aim.forward * Mathf.Lerp(500f, 1000f, this.strengthMultiplier));
						this.isBobbing = false;
						this.isLuring = true;
					}
					else if (this.isReeling && this.bob != null)
					{
						UnityEngine.Object.Destroy(this.bob.gameObject);
					}
				}
				if (this.bob != null)
				{
					if (base.player.look.perspective == EPlayerPerspective.FIRST)
					{
						Vector3 position = MainCamera.instance.WorldToViewportPoint(this.bob.position);
						Vector3 position2 = base.player.animator.view.GetComponent<Camera>().ViewportToWorldPoint(position);
						this.firstLine.SetPosition(0, this.firstHook.position);
						this.firstLine.SetPosition(1, position2);
					}
					else
					{
						this.thirdLine.SetPosition(0, this.thirdHook.position);
						this.thirdLine.SetPosition(1, this.bob.position);
					}
				}
				else if (base.player.look.perspective == EPlayerPerspective.FIRST)
				{
					this.firstLine.SetPosition(0, Vector3.zero);
					this.firstLine.SetPosition(1, Vector3.zero);
				}
				else
				{
					this.thirdLine.SetPosition(0, Vector3.zero);
					this.thirdLine.SetPosition(1, Vector3.zero);
				}
			}
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x001AAB1C File Offset: 0x001A8F1C
		public override void simulate(uint simulation, bool inputSteady)
		{
			if (this.isCasting && this.isCastable)
			{
				base.player.equipment.isBusy = false;
				this.isCasting = false;
			}
			else if (this.isReeling && this.isReelable)
			{
				base.player.equipment.isBusy = false;
				this.isReeling = false;
			}
			if (!base.channel.isOwner && Time.realtimeSinceStartup - this.lastLuck > this.luckTime && !this.isReeling)
			{
				this.resetLuck();
				this.hasLuckReset = true;
			}
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x001AABC9 File Offset: 0x001A8FC9
		private void resetLuck()
		{
			this.lastLuck = Time.realtimeSinceStartup;
			this.luckTime = 54.2f - this.strengthMultiplier * 33.5f;
			this.hasSplashed = false;
			this.hasTugged = false;
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x001AABFC File Offset: 0x001A8FFC
		private void Update()
		{
			if (this.bob != null)
			{
				if (this.isLuring)
				{
					bool flag;
					float num;
					WaterUtility.getUnderwaterInfo(this.bob.position, out flag, out num);
					if (flag && this.bob.position.y < num - 4f)
					{
						this.bob.GetComponent<Rigidbody>().useGravity = false;
						this.bob.GetComponent<Rigidbody>().isKinematic = true;
						this.water = this.bob.position;
						this.water.y = num;
						this.isLuring = false;
					}
				}
				else
				{
					if (Time.realtimeSinceStartup - this.lastLuck > this.luckTime)
					{
						if (!this.isReeling)
						{
							this.resetLuck();
							this.hasLuckReset = true;
						}
					}
					else if (Time.realtimeSinceStartup - this.lastLuck > this.luckTime - 1.4f)
					{
						if (!this.hasTugged)
						{
							this.hasTugged = true;
							base.player.playSound(((ItemFisherAsset)base.player.equipment.asset).tug);
							base.player.animator.play("Tug", false);
						}
					}
					else if (Time.realtimeSinceStartup - this.lastLuck > this.luckTime - 2.4f && !this.hasSplashed)
					{
						this.hasSplashed = true;
						Transform transform = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Fishers/Splash"))).transform;
						transform.name = "Splash";
						transform.parent = Level.effects;
						transform.position = this.water;
						transform.rotation = Quaternion.Euler(-90f, UnityEngine.Random.Range(0f, 360f), 0f);
						UnityEngine.Object.Destroy(transform.gameObject, 8f);
					}
					if (Time.realtimeSinceStartup - this.lastLuck > this.luckTime - 1.4f)
					{
						this.bob.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(this.bob.position, this.water + Vector3.down * 4f + Vector3.left * UnityEngine.Random.Range(-4f, 4f) + Vector3.forward * UnityEngine.Random.Range(-4f, 4f), 4f * Time.deltaTime));
					}
					else
					{
						this.bob.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(this.bob.position, this.water + Vector3.up * Mathf.Sin(Time.time) * 0.25f, 4f * Time.deltaTime));
					}
				}
			}
		}

		// Token: 0x04002C41 RID: 11329
		private float startedCast;

		// Token: 0x04002C42 RID: 11330
		private float startedReel;

		// Token: 0x04002C43 RID: 11331
		private float castTime;

		// Token: 0x04002C44 RID: 11332
		private float reelTime;

		// Token: 0x04002C45 RID: 11333
		private bool isStrengthening;

		// Token: 0x04002C46 RID: 11334
		private bool isCasting;

		// Token: 0x04002C47 RID: 11335
		private bool isReeling;

		// Token: 0x04002C48 RID: 11336
		private bool isFishing;

		// Token: 0x04002C49 RID: 11337
		private bool isBobbing;

		// Token: 0x04002C4A RID: 11338
		private bool isLuring;

		// Token: 0x04002C4B RID: 11339
		private bool isCatch;

		// Token: 0x04002C4C RID: 11340
		private Transform bob;

		// Token: 0x04002C4D RID: 11341
		private Transform firstHook;

		// Token: 0x04002C4E RID: 11342
		private Transform thirdHook;

		// Token: 0x04002C4F RID: 11343
		private LineRenderer firstLine;

		// Token: 0x04002C50 RID: 11344
		private LineRenderer thirdLine;

		// Token: 0x04002C51 RID: 11345
		private Vector3 water;

		// Token: 0x04002C52 RID: 11346
		private uint strengthTime;

		// Token: 0x04002C53 RID: 11347
		private float strengthMultiplier;

		// Token: 0x04002C54 RID: 11348
		private float lastLuck;

		// Token: 0x04002C55 RID: 11349
		private float luckTime;

		// Token: 0x04002C56 RID: 11350
		private bool hasLuckReset;

		// Token: 0x04002C57 RID: 11351
		private bool hasSplashed;

		// Token: 0x04002C58 RID: 11352
		private bool hasTugged;

		// Token: 0x04002C59 RID: 11353
		private SleekBox castStrengthBox;

		// Token: 0x04002C5A RID: 11354
		private Sleek castStrengthArea;

		// Token: 0x04002C5B RID: 11355
		private SleekImageTexture castStrengthBar;
	}
}
