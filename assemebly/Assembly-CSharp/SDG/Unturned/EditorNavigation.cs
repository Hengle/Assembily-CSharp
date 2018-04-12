using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000496 RID: 1174
	public class EditorNavigation : MonoBehaviour
	{
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x000A84FA File Offset: 0x000A68FA
		// (set) Token: 0x06001EC9 RID: 7881 RVA: 0x000A8501 File Offset: 0x000A6901
		public static bool isPathfinding
		{
			get
			{
				return EditorNavigation._isPathfinding;
			}
			set
			{
				EditorNavigation._isPathfinding = value;
				EditorNavigation.marker.gameObject.SetActive(EditorNavigation.isPathfinding);
				if (!EditorNavigation.isPathfinding)
				{
					EditorNavigation.select(null);
				}
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001ECA RID: 7882 RVA: 0x000A852D File Offset: 0x000A692D
		public static Flag flag
		{
			get
			{
				return EditorNavigation._flag;
			}
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x000A8534 File Offset: 0x000A6934
		private static void select(Transform select)
		{
			if (EditorNavigation.selection != null)
			{
				EditorNavigation.selection.GetComponent<Renderer>().material.color = Color.white;
			}
			if (EditorNavigation.selection == select || select == null)
			{
				EditorNavigation.selection = null;
				EditorNavigation._flag = null;
			}
			else
			{
				EditorNavigation.selection = select;
				EditorNavigation._flag = LevelNavigation.getFlag(EditorNavigation.selection);
				EditorNavigation.selection.GetComponent<Renderer>().material.color = Color.red;
			}
			EditorEnvironmentNavigationUI.updateSelection(EditorNavigation.flag);
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x000A85D0 File Offset: 0x000A69D0
		private void Update()
		{
			if (!EditorNavigation.isPathfinding)
			{
				return;
			}
			if (!EditorInteract.isFlying && GUIUtility.hotControl == 0)
			{
				if (EditorInteract.worldHit.transform != null)
				{
					EditorNavigation.marker.position = EditorInteract.worldHit.point;
				}
				if ((Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace)) && EditorNavigation.selection != null)
				{
					LevelNavigation.removeFlag(EditorNavigation.selection);
				}
				if (Input.GetKeyDown(ControlsSettings.tool_2) && EditorInteract.worldHit.transform != null && EditorNavigation.selection != null)
				{
					Vector3 point = EditorInteract.worldHit.point;
					EditorNavigation.flag.move(point);
				}
				if (Input.GetKeyDown(ControlsSettings.primary))
				{
					if (EditorInteract.logicHit.transform != null)
					{
						if (EditorInteract.logicHit.transform.name == "Flag")
						{
							EditorNavigation.select(EditorInteract.logicHit.transform);
						}
					}
					else if (EditorInteract.worldHit.transform != null)
					{
						Vector3 point2 = EditorInteract.worldHit.point;
						EditorNavigation.select(LevelNavigation.addFlag(point2));
					}
				}
			}
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x000A8744 File Offset: 0x000A6B44
		private void Start()
		{
			EditorNavigation._isPathfinding = false;
			EditorNavigation.marker = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Marker"))).transform;
			EditorNavigation.marker.name = "Marker";
			EditorNavigation.marker.parent = Level.editing;
			EditorNavigation.marker.gameObject.SetActive(false);
			EditorNavigation.marker.GetComponent<Renderer>().material.color = Color.red;
			UnityEngine.Object.Destroy(EditorNavigation.marker.GetComponent<Collider>());
		}

		// Token: 0x0400127C RID: 4732
		private static bool _isPathfinding;

		// Token: 0x0400127D RID: 4733
		private static Flag _flag;

		// Token: 0x0400127E RID: 4734
		private static Transform selection;

		// Token: 0x0400127F RID: 4735
		private static Transform marker;
	}
}
