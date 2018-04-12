﻿using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000497 RID: 1175
	public class EditorNodes : MonoBehaviour
	{
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x000A87D3 File Offset: 0x000A6BD3
		// (set) Token: 0x06001ED0 RID: 7888 RVA: 0x000A87DC File Offset: 0x000A6BDC
		public static bool isNoding
		{
			get
			{
				return EditorNodes._isNoding;
			}
			set
			{
				EditorNodes._isNoding = value;
				EditorNodes.location.gameObject.SetActive(EditorNodes.isNoding && EditorNodes.nodeType == ENodeType.LOCATION);
				EditorNodes.safezone.gameObject.SetActive(EditorNodes.isNoding && EditorNodes.nodeType == ENodeType.SAFEZONE);
				EditorNodes.purchase.gameObject.SetActive(EditorNodes.isNoding && EditorNodes.nodeType == ENodeType.PURCHASE);
				EditorNodes.arena.gameObject.SetActive(EditorNodes.isNoding && EditorNodes.nodeType == ENodeType.ARENA);
				EditorNodes.deadzone.gameObject.SetActive(EditorNodes.isNoding && EditorNodes.nodeType == ENodeType.DEADZONE);
				EditorNodes.airdrop.gameObject.SetActive(EditorNodes.isNoding && EditorNodes.nodeType == ENodeType.AIRDROP);
				EditorNodes.effect.gameObject.SetActive(EditorNodes.isNoding && EditorNodes.nodeType == ENodeType.EFFECT);
				if (!EditorNodes.isNoding)
				{
					EditorNodes.select(null);
				}
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x000A88FB File Offset: 0x000A6CFB
		// (set) Token: 0x06001ED2 RID: 7890 RVA: 0x000A8904 File Offset: 0x000A6D04
		public static ENodeType nodeType
		{
			get
			{
				return EditorNodes._nodeType;
			}
			set
			{
				EditorNodes._nodeType = value;
				EditorNodes.location.gameObject.SetActive(EditorNodes.nodeType == ENodeType.LOCATION);
				EditorNodes.safezone.gameObject.SetActive(EditorNodes.nodeType == ENodeType.SAFEZONE);
				EditorNodes.purchase.gameObject.SetActive(EditorNodes.nodeType == ENodeType.PURCHASE);
				EditorNodes.arena.gameObject.SetActive(EditorNodes.nodeType == ENodeType.ARENA);
				EditorNodes.deadzone.gameObject.SetActive(EditorNodes.nodeType == ENodeType.DEADZONE);
				EditorNodes.airdrop.gameObject.SetActive(EditorNodes.nodeType == ENodeType.AIRDROP);
				EditorNodes.effect.gameObject.SetActive(EditorNodes.nodeType == ENodeType.EFFECT);
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x000A89B8 File Offset: 0x000A6DB8
		public static Node node
		{
			get
			{
				return EditorNodes._node;
			}
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x000A89C0 File Offset: 0x000A6DC0
		private static void select(Transform select)
		{
			if (EditorNodes.selection != null)
			{
				if (EditorNodes.node.type == ENodeType.SAFEZONE)
				{
					EditorNodes.selection.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/Safezone");
				}
				else if (EditorNodes.node.type == ENodeType.PURCHASE)
				{
					EditorNodes.selection.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/Purchase");
				}
				else if (EditorNodes.node.type == ENodeType.ARENA)
				{
					EditorNodes.selection.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/Arena");
				}
				else if (EditorNodes.node.type == ENodeType.DEADZONE)
				{
					EditorNodes.selection.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/Deadzone");
				}
				else if (EditorNodes.node.type == ENodeType.EFFECT)
				{
					EditorNodes.selection.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/Effect");
				}
				else
				{
					EditorNodes.selection.GetComponent<Renderer>().material.color = Color.white;
				}
			}
			if (EditorNodes.selection == select || select == null)
			{
				EditorNodes.selection = null;
				EditorNodes._node = null;
			}
			else
			{
				EditorNodes.selection = select;
				EditorNodes._node = LevelNodes.getNode(EditorNodes.selection);
				if (EditorNodes.node.type == ENodeType.SAFEZONE || EditorNodes.node.type == ENodeType.PURCHASE || EditorNodes.node.type == ENodeType.ARENA || EditorNodes.node.type == ENodeType.DEADZONE || EditorNodes.node.type == ENodeType.EFFECT)
				{
					EditorNodes.selection.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/Remove");
				}
				else
				{
					EditorNodes.selection.GetComponent<Renderer>().material.color = Color.red;
				}
			}
			EditorEnvironmentNodesUI.updateSelection(EditorNodes.node);
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x000A8BD0 File Offset: 0x000A6FD0
		private void Update()
		{
			if (!EditorNodes.isNoding)
			{
				return;
			}
			if (!EditorInteract.isFlying && GUIUtility.hotControl == 0)
			{
				if (EditorInteract.worldHit.transform != null)
				{
					EditorNodes.location.position = EditorInteract.worldHit.point;
					EditorNodes.safezone.position = EditorInteract.worldHit.point;
					EditorNodes.purchase.position = EditorInteract.worldHit.point;
					EditorNodes.arena.position = EditorInteract.worldHit.point;
					EditorNodes.deadzone.position = EditorInteract.worldHit.point;
					EditorNodes.airdrop.position = EditorInteract.worldHit.point;
					EditorNodes.effect.position = EditorInteract.worldHit.point;
				}
				if ((Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace)) && EditorNodes.selection != null)
				{
					LevelNodes.removeNode(EditorNodes.selection);
				}
				if (Input.GetKeyDown(ControlsSettings.tool_2) && EditorInteract.worldHit.transform != null && EditorNodes.selection != null)
				{
					Vector3 point = EditorInteract.worldHit.point;
					EditorNodes.node.move(point);
				}
				if (Input.GetKeyDown(ControlsSettings.primary))
				{
					if (EditorInteract.logicHit.transform != null)
					{
						if (EditorInteract.logicHit.transform.name == "Node")
						{
							EditorNodes.select(EditorInteract.logicHit.transform);
						}
					}
					else if (EditorInteract.worldHit.transform != null)
					{
						Vector3 point2 = EditorInteract.worldHit.point;
						EditorNodes.select(LevelNodes.addNode(point2, EditorNodes.nodeType));
					}
				}
			}
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x000A8DDC File Offset: 0x000A71DC
		private void Start()
		{
			EditorNodes._isNoding = false;
			EditorNodes.location = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Location"))).transform;
			EditorNodes.location.name = "Location";
			EditorNodes.location.parent = Level.editing;
			EditorNodes.location.gameObject.SetActive(false);
			EditorNodes.location.GetComponent<Renderer>().material.color = Color.red;
			UnityEngine.Object.Destroy(EditorNodes.location.GetComponent<Collider>());
			EditorNodes.safezone = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Safezone"))).transform;
			EditorNodes.safezone.name = "Safezone";
			EditorNodes.safezone.parent = Level.editing;
			EditorNodes.safezone.gameObject.SetActive(false);
			EditorNodes.safezone.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/Remove");
			UnityEngine.Object.Destroy(EditorNodes.safezone.GetComponent<Collider>());
			EditorNodes.purchase = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Purchase"))).transform;
			EditorNodes.purchase.name = "Purchase";
			EditorNodes.purchase.parent = Level.editing;
			EditorNodes.purchase.gameObject.SetActive(false);
			EditorNodes.purchase.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/Remove");
			UnityEngine.Object.Destroy(EditorNodes.purchase.GetComponent<Collider>());
			EditorNodes.arena = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Arena"))).transform;
			EditorNodes.arena.name = "Arena";
			EditorNodes.arena.parent = Level.editing;
			EditorNodes.arena.gameObject.SetActive(false);
			EditorNodes.arena.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/Remove");
			UnityEngine.Object.Destroy(EditorNodes.arena.GetComponent<Collider>());
			EditorNodes.deadzone = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Deadzone"))).transform;
			EditorNodes.deadzone.name = "Deadzone";
			EditorNodes.deadzone.parent = Level.editing;
			EditorNodes.deadzone.gameObject.SetActive(false);
			EditorNodes.deadzone.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/Remove");
			UnityEngine.Object.Destroy(EditorNodes.deadzone.GetComponent<Collider>());
			EditorNodes.airdrop = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Airdrop"))).transform;
			EditorNodes.airdrop.name = "Airdrop";
			EditorNodes.airdrop.parent = Level.editing;
			EditorNodes.airdrop.gameObject.SetActive(false);
			EditorNodes.airdrop.GetComponent<Renderer>().material.color = Color.red;
			UnityEngine.Object.Destroy(EditorNodes.airdrop.GetComponent<Collider>());
			EditorNodes.effect = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Effect"))).transform;
			EditorNodes.effect.name = "Effect";
			EditorNodes.effect.parent = Level.editing;
			EditorNodes.effect.gameObject.SetActive(false);
			EditorNodes.effect.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/Remove");
			UnityEngine.Object.Destroy(EditorNodes.effect.GetComponent<Collider>());
		}

		// Token: 0x04001280 RID: 4736
		private static bool _isNoding;

		// Token: 0x04001281 RID: 4737
		private static ENodeType _nodeType;

		// Token: 0x04001282 RID: 4738
		private static Node _node;

		// Token: 0x04001283 RID: 4739
		private static Transform selection;

		// Token: 0x04001284 RID: 4740
		private static Transform location;

		// Token: 0x04001285 RID: 4741
		private static Transform purchase;

		// Token: 0x04001286 RID: 4742
		private static Transform safezone;

		// Token: 0x04001287 RID: 4743
		private static Transform arena;

		// Token: 0x04001288 RID: 4744
		private static Transform deadzone;

		// Token: 0x04001289 RID: 4745
		private static Transform airdrop;

		// Token: 0x0400128A RID: 4746
		private static Transform effect;
	}
}
