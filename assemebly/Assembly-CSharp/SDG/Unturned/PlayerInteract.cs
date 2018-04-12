using System;
using SDG.Framework.Utilities;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000613 RID: 1555
	public class PlayerInteract : PlayerCaller
	{
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x00117DB6 File Offset: 0x001161B6
		public static Interactable interactable
		{
			get
			{
				return PlayerInteract._interactable;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002C08 RID: 11272 RVA: 0x00117DBD File Offset: 0x001161BD
		public static Interactable2 interactable2
		{
			get
			{
				return PlayerInteract._interactable2;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x00117DC4 File Offset: 0x001161C4
		private float salvageTime
		{
			get
			{
				if (Provider.isServer || base.channel.owner.isAdmin)
				{
					return 1f;
				}
				return 8f;
			}
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x00117DF0 File Offset: 0x001161F0
		private void hotkey(byte button)
		{
			VehicleManager.swapVehicle(button);
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x00117DF8 File Offset: 0x001161F8
		[SteamCall]
		public void askInspect(CSteamID steamID)
		{
			if (base.channel.checkOwner(steamID))
			{
				if (!base.player.tryToPerformRateLimitedAction())
				{
					return;
				}
				if (base.player.equipment.canInspect)
				{
					base.channel.send("tellInspect", ESteamCall.ALL, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
				}
			}
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x00117E55 File Offset: 0x00116255
		[SteamCall]
		public void tellInspect(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID))
			{
				base.player.equipment.inspect();
			}
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x00117E78 File Offset: 0x00116278
		private void onPurchaseUpdated(PurchaseNode node)
		{
			if (node == null)
			{
				PlayerInteract.purchaseAsset = null;
			}
			else
			{
				PlayerInteract.purchaseAsset = (ItemAsset)Assets.find(EAssetType.ITEM, node.id);
			}
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x00117EA4 File Offset: 0x001162A4
		private void Update()
		{
			if (base.channel.isOwner)
			{
				if (base.player.stance.stance != EPlayerStance.DRIVING && base.player.stance.stance != EPlayerStance.SITTING && !base.player.life.isDead && !base.player.workzone.isBuilding)
				{
					if (Time.realtimeSinceStartup - PlayerInteract.lastInteract > 0.1f)
					{
						PlayerInteract.lastInteract = Time.realtimeSinceStartup;
						if (base.player.look.isCam)
						{
							PhysicsUtility.raycast(new Ray(base.player.look.aim.position, base.player.look.aim.forward), out PlayerInteract.hit, 4f, RayMasks.PLAYER_INTERACT, QueryTriggerInteraction.UseGlobal);
						}
						else
						{
							PhysicsUtility.raycast(new Ray(MainCamera.instance.transform.position, MainCamera.instance.transform.forward), out PlayerInteract.hit, (float)((base.player.look.perspective != EPlayerPerspective.THIRD) ? 4 : 6), RayMasks.PLAYER_INTERACT, QueryTriggerInteraction.UseGlobal);
						}
					}
					if (PlayerInteract.hit.transform != PlayerInteract.focus)
					{
						if (PlayerInteract.focus != null && PlayerInteract.interactable != null)
						{
							InteractableDoorHinge component = PlayerInteract.focus.GetComponent<InteractableDoorHinge>();
							if (component != null)
							{
								HighlighterTool.unhighlight(PlayerInteract.focus.parent.parent);
							}
							else
							{
								HighlighterTool.unhighlight(PlayerInteract.focus);
							}
						}
						PlayerInteract.focus = null;
						PlayerInteract.target = null;
						PlayerInteract._interactable = null;
						PlayerInteract._interactable2 = null;
						if (PlayerInteract.hit.transform != null)
						{
							PlayerInteract.focus = PlayerInteract.hit.transform;
							PlayerInteract._interactable = PlayerInteract.focus.GetComponent<Interactable>();
							PlayerInteract._interactable2 = PlayerInteract.focus.GetComponent<Interactable2>();
							if (PlayerInteract.interactable != null)
							{
								PlayerInteract.target = PlayerInteract.focus.FindChildRecursive("Target");
								if (PlayerInteract.interactable.checkInteractable())
								{
									if (PlayerUI.window.isEnabled)
									{
										if (PlayerInteract.interactable.checkUseable())
										{
											Color color;
											if (!PlayerInteract.interactable.checkHighlight(out color))
											{
												color = Color.green;
											}
											InteractableDoorHinge component2 = PlayerInteract.focus.GetComponent<InteractableDoorHinge>();
											if (component2 != null)
											{
												HighlighterTool.highlight(PlayerInteract.focus.parent.parent, color);
											}
											else
											{
												HighlighterTool.highlight(PlayerInteract.focus, color);
											}
										}
										else
										{
											Color color = Color.red;
											InteractableDoorHinge component3 = PlayerInteract.focus.GetComponent<InteractableDoorHinge>();
											if (component3 != null)
											{
												HighlighterTool.highlight(PlayerInteract.focus.parent.parent, color);
											}
											else
											{
												HighlighterTool.highlight(PlayerInteract.focus, color);
											}
										}
									}
								}
								else
								{
									PlayerInteract.target = null;
									PlayerInteract._interactable = null;
								}
							}
						}
					}
				}
				else
				{
					if (PlayerInteract.focus != null && PlayerInteract.interactable != null)
					{
						InteractableDoorHinge component4 = PlayerInteract.focus.GetComponent<InteractableDoorHinge>();
						if (component4 != null)
						{
							HighlighterTool.unhighlight(PlayerInteract.focus.parent.parent);
						}
						else
						{
							HighlighterTool.unhighlight(PlayerInteract.focus);
						}
					}
					PlayerInteract.focus = null;
					PlayerInteract.target = null;
					PlayerInteract._interactable = null;
					PlayerInteract._interactable2 = null;
				}
			}
			if (base.channel.isOwner && !base.player.life.isDead)
			{
				if (PlayerInteract.interactable != null)
				{
					EPlayerMessage message;
					string text;
					Color color2;
					if (PlayerInteract.interactable.checkHint(out message, out text, out color2) && !PlayerUI.window.showCursor)
					{
						if (PlayerInteract.interactable.CompareTag("Item"))
						{
							PlayerUI.hint((!(PlayerInteract.target != null)) ? PlayerInteract.focus : PlayerInteract.target, message, text, color2, new object[]
							{
								((InteractableItem)PlayerInteract.interactable).item,
								((InteractableItem)PlayerInteract.interactable).asset
							});
						}
						else
						{
							PlayerUI.hint((!(PlayerInteract.target != null)) ? PlayerInteract.focus : PlayerInteract.target, message, text, color2, new object[0]);
						}
					}
				}
				else if (PlayerInteract.purchaseAsset != null && base.player.movement.purchaseNode != null && !PlayerUI.window.showCursor)
				{
					PlayerUI.hint(null, EPlayerMessage.PURCHASE, string.Empty, Color.white, new object[]
					{
						PlayerInteract.purchaseAsset.itemName,
						base.player.movement.purchaseNode.cost
					});
				}
				else if (PlayerInteract.focus != null && PlayerInteract.focus.CompareTag("Enemy"))
				{
					Player player = DamageTool.getPlayer(PlayerInteract.focus);
					if (player != null && player != Player.player && !PlayerUI.window.showCursor)
					{
						PlayerUI.hint(null, EPlayerMessage.ENEMY, string.Empty, Color.white, new object[]
						{
							player.channel.owner
						});
					}
				}
				EPlayerMessage message2;
				float data;
				if (PlayerInteract.interactable2 != null && PlayerInteract.interactable2.checkHint(out message2, out data) && !PlayerUI.window.showCursor)
				{
					PlayerUI.hint2(message2, (!PlayerInteract.isHoldingKey) ? 0f : ((Time.realtimeSinceStartup - PlayerInteract.lastKeyDown) / this.salvageTime), data);
				}
				if ((base.player.stance.stance == EPlayerStance.DRIVING || base.player.stance.stance == EPlayerStance.SITTING) && !Input.GetKey(KeyCode.LeftShift))
				{
					if (Input.GetKeyDown(KeyCode.F1))
					{
						this.hotkey(0);
					}
					if (Input.GetKeyDown(KeyCode.F2))
					{
						this.hotkey(1);
					}
					if (Input.GetKeyDown(KeyCode.F3))
					{
						this.hotkey(2);
					}
					if (Input.GetKeyDown(KeyCode.F4))
					{
						this.hotkey(3);
					}
					if (Input.GetKeyDown(KeyCode.F5))
					{
						this.hotkey(4);
					}
					if (Input.GetKeyDown(KeyCode.F6))
					{
						this.hotkey(5);
					}
					if (Input.GetKeyDown(KeyCode.F7))
					{
						this.hotkey(6);
					}
					if (Input.GetKeyDown(KeyCode.F8))
					{
						this.hotkey(7);
					}
					if (Input.GetKeyDown(KeyCode.F9))
					{
						this.hotkey(8);
					}
					if (Input.GetKeyDown(KeyCode.F10))
					{
						this.hotkey(9);
					}
				}
				if (Input.GetKeyDown(ControlsSettings.interact))
				{
					PlayerInteract.lastKeyDown = Time.realtimeSinceStartup;
					PlayerInteract.isHoldingKey = true;
				}
				if (Input.GetKeyDown(ControlsSettings.inspect) && ControlsSettings.inspect != ControlsSettings.interact && base.player.equipment.canInspect)
				{
					base.channel.send("askInspect", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
				}
				if (PlayerInteract.isHoldingKey)
				{
					if (Input.GetKeyUp(ControlsSettings.interact))
					{
						PlayerInteract.isHoldingKey = false;
						if (PlayerUI.window.showCursor)
						{
							if (base.player.inventory.isStoring && base.player.inventory.shouldInteractCloseStorage)
							{
								PlayerDashboardUI.close();
								PlayerLifeUI.open();
							}
							else if (PlayerBarricadeSignUI.active)
							{
								PlayerBarricadeSignUI.close();
								PlayerLifeUI.open();
							}
							else if (PlayerBarricadeStereoUI.active)
							{
								PlayerBarricadeStereoUI.close();
								PlayerLifeUI.open();
							}
							else if (PlayerBarricadeLibraryUI.active)
							{
								PlayerBarricadeLibraryUI.close();
								PlayerLifeUI.open();
							}
							else if (PlayerBarricadeMannequinUI.active)
							{
								PlayerBarricadeMannequinUI.close();
								PlayerLifeUI.open();
							}
							else if (PlayerNPCDialogueUI.active)
							{
								if (PlayerNPCDialogueUI.dialogueAnimating)
								{
									PlayerNPCDialogueUI.skipText();
								}
								else if (PlayerNPCDialogueUI.dialogueHasNextPage)
								{
									PlayerNPCDialogueUI.nextPage();
								}
								else
								{
									PlayerNPCDialogueUI.close();
									PlayerLifeUI.open();
								}
							}
							else if (PlayerNPCQuestUI.active)
							{
								PlayerNPCQuestUI.closeNicely();
							}
							else if (PlayerNPCVendorUI.active)
							{
								PlayerNPCVendorUI.closeNicely();
							}
						}
						else if (base.player.stance.stance == EPlayerStance.DRIVING || base.player.stance.stance == EPlayerStance.SITTING)
						{
							VehicleManager.exitVehicle();
						}
						else if (PlayerInteract.focus != null && PlayerInteract.interactable != null)
						{
							if (PlayerInteract.interactable.checkUseable())
							{
								PlayerInteract.interactable.use();
							}
						}
						else if (PlayerInteract.purchaseAsset != null)
						{
							if (base.player.skills.experience >= base.player.movement.purchaseNode.cost)
							{
								base.player.skills.sendPurchase(base.player.movement.purchaseNode);
							}
						}
						else if (ControlsSettings.inspect == ControlsSettings.interact && base.player.equipment.canInspect)
						{
							base.channel.send("askInspect", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
						}
					}
					else if (Time.realtimeSinceStartup - PlayerInteract.lastKeyDown > this.salvageTime)
					{
						PlayerInteract.isHoldingKey = false;
						if (!PlayerUI.window.showCursor && PlayerInteract.interactable2 != null)
						{
							PlayerInteract.interactable2.use();
						}
					}
				}
			}
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x00118889 File Offset: 0x00116C89
		private void Start()
		{
			if (base.channel.isOwner)
			{
				PlayerMovement movement = base.player.movement;
				movement.onPurchaseUpdated = (PurchaseUpdated)Delegate.Combine(movement.onPurchaseUpdated, new PurchaseUpdated(this.onPurchaseUpdated));
			}
		}

		// Token: 0x04001C7A RID: 7290
		private static Transform focus;

		// Token: 0x04001C7B RID: 7291
		private static Transform target;

		// Token: 0x04001C7C RID: 7292
		private static ItemAsset purchaseAsset;

		// Token: 0x04001C7D RID: 7293
		private static Interactable _interactable;

		// Token: 0x04001C7E RID: 7294
		private static Interactable2 _interactable2;

		// Token: 0x04001C7F RID: 7295
		private static RaycastHit hit;

		// Token: 0x04001C80 RID: 7296
		private Color highlight;

		// Token: 0x04001C81 RID: 7297
		private static float lastInteract;

		// Token: 0x04001C82 RID: 7298
		private static float lastKeyDown;

		// Token: 0x04001C83 RID: 7299
		private static bool isHoldingKey;
	}
}
