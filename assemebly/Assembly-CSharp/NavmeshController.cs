﻿using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class NavmeshController : MonoBehaviour
{
	// Token: 0x06000012 RID: 18 RVA: 0x0000299E File Offset: 0x00000D9E
	public void Start()
	{
		AstarPath.OnAwakeSettings = (OnVoidDelegate)Delegate.Combine(AstarPath.OnAwakeSettings, new OnVoidDelegate(this.OnAstarAwake));
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000029C0 File Offset: 0x00000DC0
	private void OnAstarAwake()
	{
		AstarPath.OnLatePostScan = (OnScanDelegate)Delegate.Combine(AstarPath.OnLatePostScan, new OnScanDelegate(this.OnRescan));
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000029E2 File Offset: 0x00000DE2
	private void OnDisable()
	{
		AstarPath.OnAwakeSettings = (OnVoidDelegate)Delegate.Remove(AstarPath.OnAwakeSettings, new OnVoidDelegate(this.OnAstarAwake));
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002A04 File Offset: 0x00000E04
	private void OnRescan(AstarPath active)
	{
		this.Teleport();
		Debug.LogWarning("On Rescan");
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002A16 File Offset: 0x00000E16
	public void Teleport()
	{
		this.prevNode = null;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002A20 File Offset: 0x00000E20
	public Vector3 SimpleMove(Vector3 currentPosition, Vector3 direction)
	{
		this.forwardPlanning = ((this.forwardPlanning >= 0.01f) ? this.forwardPlanning : 0.01f);
		if (this.controller == null)
		{
			this.controller = base.GetComponent<CharacterController>();
		}
		if (this.controller == null)
		{
			Debug.LogError("No CharacterController is attached to the GameObject");
			return direction;
		}
		direction = this.ClampMove(currentPosition, direction);
		this.controller.SimpleMove(direction);
		return direction;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002AA8 File Offset: 0x00000EA8
	public Vector3 ClampMove(Vector3 currentPosition, Vector3 direction)
	{
		this.forwardPlanning = ((this.forwardPlanning >= 0.01f) ? this.forwardPlanning : 0.01f);
		Vector3 vector = currentPosition + direction * this.forwardPlanning;
		vector = this.ClampToNavmesh(vector);
		direction = (vector - currentPosition) * (1f / this.forwardPlanning);
		return direction;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002B14 File Offset: 0x00000F14
	public Vector3 ClampToNavmesh(Vector3 target)
	{
		if (this.prevNode == null)
		{
			this.prevNode = AstarPath.active.GetNearest(base.transform.position).node;
			this.prevPos = base.transform.position;
		}
		Vector3 result;
		this.prevNode = this.ClampAlongNavmesh(this.prevPos, this.prevNode, target, out result);
		this.prevPos = result;
		return result;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002B84 File Offset: 0x00000F84
	public GraphNode ClampAlongNavmesh(Vector3 startPos, GraphNode _startNode, Vector3 endPos, out Vector3 clampedPos)
	{
		TriangleMeshNode triangleMeshNode = (TriangleMeshNode)_startNode;
		clampedPos = endPos;
		Stack<TriangleMeshNode> stack = this.tmpStack;
		List<TriangleMeshNode> list = this.tmpClosed;
		stack.Clear();
		list.Clear();
		float num = float.PositiveInfinity;
		TriangleMeshNode result = null;
		Vector3 vector = (startPos + endPos) / 2f;
		float num2 = AstarMath.MagnitudeXZ(startPos, endPos) / 2f;
		Vector3 vector2 = startPos;
		stack.Push(triangleMeshNode);
		list.Add(triangleMeshNode);
		INavmesh navmesh = AstarData.GetGraph(triangleMeshNode) as INavmesh;
		if (navmesh == null)
		{
			return triangleMeshNode;
		}
		while (stack.Count > 0)
		{
			TriangleMeshNode triangleMeshNode2 = stack.Pop();
			int tileIndex = ((RecastGraph)navmesh).GetTileIndex(triangleMeshNode2.GetVertexIndex(0));
			Int3[] verts = ((RecastGraph)navmesh).GetTiles()[tileIndex].verts;
			int vertexArrayIndex = triangleMeshNode2.GetVertexArrayIndex(triangleMeshNode2.v0);
			int vertexArrayIndex2 = triangleMeshNode2.GetVertexArrayIndex(triangleMeshNode2.v1);
			int vertexArrayIndex3 = triangleMeshNode2.GetVertexArrayIndex(triangleMeshNode2.v2);
			if (NavMeshGraph.ContainsPoint(vertexArrayIndex, vertexArrayIndex2, vertexArrayIndex3, endPos, verts))
			{
				result = triangleMeshNode2;
				vector2 = endPos;
				break;
			}
			int i = 0;
			int i2 = 2;
			while (i < 3)
			{
				int vertexIndex = triangleMeshNode2.GetVertexIndex(i2);
				int vertexIndex2 = triangleMeshNode2.GetVertexIndex(i);
				bool flag = true;
				TriangleMeshNode triangleMeshNode3 = null;
				for (int j = 0; j < triangleMeshNode2.connections.Length; j++)
				{
					triangleMeshNode3 = (triangleMeshNode2.connections[j] as TriangleMeshNode);
					if (triangleMeshNode3 != null)
					{
						int k = 0;
						int i3 = 2;
						while (k < 3)
						{
							int vertexIndex3 = triangleMeshNode3.GetVertexIndex(i3);
							int vertexIndex4 = triangleMeshNode3.GetVertexIndex(k);
							if ((vertexIndex3 == vertexIndex && vertexIndex4 == vertexIndex2) || (vertexIndex3 == vertexIndex2 && vertexIndex4 == vertexIndex))
							{
								flag = false;
								break;
							}
							i3 = k++;
						}
						if (!flag)
						{
							break;
						}
					}
				}
				if (flag)
				{
					Vector3 vector3 = AstarMath.NearestPointStrictXZ((Vector3)verts[vertexIndex], (Vector3)verts[vertexIndex2], endPos);
					float num3 = AstarMath.MagnitudeXZ(vector3, endPos);
					if (num3 < num)
					{
						vector2 = vector3;
						num = num3;
						result = triangleMeshNode2;
					}
				}
				else if (!list.Contains(triangleMeshNode3))
				{
					list.Add(triangleMeshNode3);
					Vector3 vector3 = AstarMath.NearestPointStrictXZ((Vector3)verts[vertexIndex], (Vector3)verts[vertexIndex2], vector);
					float num3 = AstarMath.MagnitudeXZ(vector3, vector);
					if (num3 <= num2)
					{
						stack.Push(triangleMeshNode3);
					}
				}
				i2 = i++;
			}
		}
		clampedPos = vector2;
		return result;
	}

	// Token: 0x0400001F RID: 31
	public float forwardPlanning;

	// Token: 0x04000020 RID: 32
	protected Vector3 prevPos;

	// Token: 0x04000021 RID: 33
	protected GraphNode prevNode;

	// Token: 0x04000022 RID: 34
	protected CharacterController controller;

	// Token: 0x04000023 RID: 35
	private Stack<TriangleMeshNode> tmpStack = new Stack<TriangleMeshNode>(16);

	// Token: 0x04000024 RID: 36
	private List<TriangleMeshNode> tmpClosed = new List<TriangleMeshNode>(32);
}
