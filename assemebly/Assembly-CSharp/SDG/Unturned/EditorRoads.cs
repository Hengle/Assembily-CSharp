using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200049B RID: 1179
	public class EditorRoads : MonoBehaviour
	{
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x000AB720 File Offset: 0x000A9B20
		// (set) Token: 0x06001F00 RID: 7936 RVA: 0x000AB727 File Offset: 0x000A9B27
		public static bool isPaving
		{
			get
			{
				return EditorRoads._isPaving;
			}
			set
			{
				EditorRoads._isPaving = value;
				EditorRoads.highlighter.gameObject.SetActive(EditorRoads.isPaving);
				if (!EditorRoads.isPaving)
				{
					EditorRoads.select(null);
				}
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x000AB753 File Offset: 0x000A9B53
		public static Road road
		{
			get
			{
				return EditorRoads._road;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x000AB75A File Offset: 0x000A9B5A
		public static RoadPath path
		{
			get
			{
				return EditorRoads._path;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001F03 RID: 7939 RVA: 0x000AB761 File Offset: 0x000A9B61
		public static RoadJoint joint
		{
			get
			{
				return EditorRoads._joint;
			}
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x000AB768 File Offset: 0x000A9B68
		private static void select(Transform target)
		{
			if (EditorRoads.road != null)
			{
				if (EditorRoads.tangentIndex > -1)
				{
					EditorRoads.path.unhighlightTangent(EditorRoads.tangentIndex);
				}
				else if (EditorRoads.vertexIndex > -1)
				{
					EditorRoads.path.unhighlightVertex();
				}
			}
			if (EditorRoads.selection == target || target == null)
			{
				EditorRoads.deselect();
			}
			else
			{
				EditorRoads.selection = target;
				EditorRoads._road = LevelRoads.getRoad(EditorRoads.selection, out EditorRoads.vertexIndex, out EditorRoads.tangentIndex);
				if (EditorRoads.road != null)
				{
					EditorRoads._path = EditorRoads.road.paths[EditorRoads.vertexIndex];
					EditorRoads._joint = EditorRoads.road.joints[EditorRoads.vertexIndex];
					if (EditorRoads.tangentIndex > -1)
					{
						EditorRoads.path.highlightTangent(EditorRoads.tangentIndex);
					}
					else if (EditorRoads.vertexIndex > -1)
					{
						EditorRoads.path.highlightVertex();
					}
				}
				else
				{
					EditorRoads._path = null;
					EditorRoads._joint = null;
				}
			}
			EditorEnvironmentRoadsUI.updateSelection(EditorRoads.road, EditorRoads.joint);
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x000AB888 File Offset: 0x000A9C88
		private static void deselect()
		{
			EditorRoads.selection = null;
			EditorRoads._road = null;
			EditorRoads._path = null;
			EditorRoads._joint = null;
			EditorRoads.vertexIndex = -1;
			EditorRoads.tangentIndex = -1;
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x000AB8B0 File Offset: 0x000A9CB0
		private void Update()
		{
			if (!EditorRoads.isPaving)
			{
				return;
			}
			if (!EditorInteract.isFlying && GUIUtility.hotControl == 0)
			{
				if (EditorInteract.worldHit.transform != null)
				{
					EditorRoads.highlighter.position = EditorInteract.worldHit.point;
				}
				if ((Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace)) && EditorRoads.selection != null && EditorRoads.road != null)
				{
					if (Input.GetKey(ControlsSettings.other))
					{
						LevelRoads.removeRoad(EditorRoads.road);
					}
					else
					{
						EditorRoads.road.removeVertex(EditorRoads.vertexIndex);
					}
					EditorRoads.deselect();
				}
				if (Input.GetKeyDown(ControlsSettings.tool_2) && EditorInteract.worldHit.transform != null)
				{
					Vector3 point = EditorInteract.worldHit.point;
					if (EditorRoads.road != null)
					{
						if (EditorRoads.tangentIndex > -1)
						{
							EditorRoads.road.moveTangent(EditorRoads.vertexIndex, EditorRoads.tangentIndex, point - EditorRoads.joint.vertex);
						}
						else if (EditorRoads.vertexIndex > -1)
						{
							EditorRoads.road.moveVertex(EditorRoads.vertexIndex, point);
						}
					}
				}
				if (Input.GetKeyDown(ControlsSettings.primary))
				{
					if (EditorInteract.logicHit.transform != null)
					{
						if (EditorInteract.logicHit.transform.name.IndexOf("Path") != -1 || EditorInteract.logicHit.transform.name.IndexOf("Tangent") != -1)
						{
							EditorRoads.select(EditorInteract.logicHit.transform);
						}
					}
					else if (EditorInteract.worldHit.transform != null)
					{
						Vector3 point2 = EditorInteract.worldHit.point;
						if (EditorRoads.road != null)
						{
							if (EditorRoads.tangentIndex > -1)
							{
								EditorRoads.select(EditorRoads.road.addVertex(EditorRoads.vertexIndex + EditorRoads.tangentIndex, point2));
							}
							else
							{
								float num = Vector3.Dot(point2 - EditorRoads.joint.vertex, EditorRoads.joint.getTangent(0));
								float num2 = Vector3.Dot(point2 - EditorRoads.joint.vertex, EditorRoads.joint.getTangent(1));
								if (num > num2)
								{
									EditorRoads.select(EditorRoads.road.addVertex(EditorRoads.vertexIndex, point2));
								}
								else
								{
									EditorRoads.select(EditorRoads.road.addVertex(EditorRoads.vertexIndex + 1, point2));
								}
							}
						}
						else
						{
							EditorRoads.select(LevelRoads.addRoad(point2));
						}
					}
				}
			}
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x000ABB78 File Offset: 0x000A9F78
		private void Start()
		{
			EditorRoads._isPaving = false;
			EditorRoads.highlighter = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Highlighter"))).transform;
			EditorRoads.highlighter.name = "Highlighter";
			EditorRoads.highlighter.parent = Level.editing;
			EditorRoads.highlighter.gameObject.SetActive(false);
			EditorRoads.highlighter.GetComponent<Renderer>().material.color = Color.red;
			EditorRoads.deselect();
		}

		// Token: 0x040012AD RID: 4781
		private static bool _isPaving;

		// Token: 0x040012AE RID: 4782
		public static byte selected;

		// Token: 0x040012AF RID: 4783
		private static Road _road;

		// Token: 0x040012B0 RID: 4784
		private static RoadPath _path;

		// Token: 0x040012B1 RID: 4785
		private static RoadJoint _joint;

		// Token: 0x040012B2 RID: 4786
		private static int vertexIndex;

		// Token: 0x040012B3 RID: 4787
		private static int tangentIndex;

		// Token: 0x040012B4 RID: 4788
		private static Transform selection;

		// Token: 0x040012B5 RID: 4789
		private static Transform highlighter;
	}
}
