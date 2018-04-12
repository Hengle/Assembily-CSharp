using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// Token: 0x0200000A RID: 10
[AddComponentMenu("Pathfinding/Seeker")]
public class Seeker : MonoBehaviour
{
	// Token: 0x06000049 RID: 73 RVA: 0x00005B09 File Offset: 0x00003F09
	public Path GetCurrentPath()
	{
		return this.path;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00005B11 File Offset: 0x00003F11
	public void Awake()
	{
		this.onPathDelegate = new OnPathDelegate(this.OnPathComplete);
		this.onPartialPathDelegate = new OnPathDelegate(this.OnPartialPathComplete);
		this.startEndModifier.Awake(this);
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00005B43 File Offset: 0x00003F43
	public void OnDestroy()
	{
		this.ReleaseClaimedPath();
		this.startEndModifier.OnDestroy(this);
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00005B57 File Offset: 0x00003F57
	public void ReleaseClaimedPath()
	{
		if (this.prevPath != null)
		{
			this.prevPath.ReleaseSilent(this);
			this.prevPath = null;
		}
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00005B77 File Offset: 0x00003F77
	public void RegisterModifier(IPathModifier mod)
	{
		if (this.modifiers == null)
		{
			this.modifiers = new List<IPathModifier>(1);
		}
		this.modifiers.Add(mod);
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00005B9C File Offset: 0x00003F9C
	public void DeregisterModifier(IPathModifier mod)
	{
		if (this.modifiers == null)
		{
			return;
		}
		this.modifiers.Remove(mod);
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00005BB7 File Offset: 0x00003FB7
	public void PostProcess(Path p)
	{
		this.RunModifiers(Seeker.ModifierPass.PostProcess, p);
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00005BC4 File Offset: 0x00003FC4
	public void RunModifiers(Seeker.ModifierPass pass, Path p)
	{
		bool flag = true;
		while (flag)
		{
			flag = false;
			for (int i = 0; i < this.modifiers.Count - 1; i++)
			{
				if (this.modifiers[i].Priority < this.modifiers[i + 1].Priority)
				{
					IPathModifier value = this.modifiers[i];
					this.modifiers[i] = this.modifiers[i + 1];
					this.modifiers[i + 1] = value;
					flag = true;
				}
			}
		}
		if (pass != Seeker.ModifierPass.PreProcess)
		{
			if (pass != Seeker.ModifierPass.PostProcessOriginal)
			{
				if (pass == Seeker.ModifierPass.PostProcess)
				{
					if (this.postProcessPath != null)
					{
						this.postProcessPath(p);
					}
				}
			}
			else if (this.postProcessOriginalPath != null)
			{
				this.postProcessOriginalPath(p);
			}
		}
		else if (this.preProcessPath != null)
		{
			this.preProcessPath(p);
		}
		if (this.modifiers.Count == 0)
		{
			return;
		}
		ModifierData modifierData = ModifierData.All;
		IPathModifier pathModifier = this.modifiers[0];
		for (int j = 0; j < this.modifiers.Count; j++)
		{
			MonoModifier monoModifier = this.modifiers[j] as MonoModifier;
			if (!(monoModifier != null) || monoModifier.enabled)
			{
				if (pass != Seeker.ModifierPass.PreProcess)
				{
					if (pass != Seeker.ModifierPass.PostProcessOriginal)
					{
						if (pass == Seeker.ModifierPass.PostProcess)
						{
							ModifierData modifierData2 = ModifierConverter.Convert(p, modifierData, this.modifiers[j].input);
							if (modifierData2 != ModifierData.None)
							{
								this.modifiers[j].Apply(p, modifierData2);
								modifierData = this.modifiers[j].output;
							}
							else
							{
								Debug.Log(string.Concat(new string[]
								{
									"Error converting ",
									(j <= 0) ? "original" : pathModifier.GetType().Name,
									"'s output to ",
									this.modifiers[j].GetType().Name,
									"'s input.\nTry rearranging the modifier priorities on the Seeker."
								}));
								modifierData = ModifierData.None;
							}
							pathModifier = this.modifiers[j];
						}
					}
					else
					{
						this.modifiers[j].ApplyOriginal(p);
					}
				}
				else
				{
					this.modifiers[j].PreProcess(p);
				}
				if (modifierData == ModifierData.None)
				{
					break;
				}
			}
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00005E5B File Offset: 0x0000425B
	public bool IsDone()
	{
		return this.path == null || this.path.GetState() >= PathState.Returned;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00005E7C File Offset: 0x0000427C
	public void OnPathComplete(Path p)
	{
		this.OnPathComplete(p, true, true);
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00005E88 File Offset: 0x00004288
	public void OnPathComplete(Path p, bool runModifiers, bool sendCallbacks)
	{
		if (p != null && p != this.path && sendCallbacks)
		{
			return;
		}
		if (this == null || p == null || p != this.path)
		{
			return;
		}
		if (!this.path.error && runModifiers)
		{
			this.RunModifiers(Seeker.ModifierPass.PostProcessOriginal, this.path);
			this.RunModifiers(Seeker.ModifierPass.PostProcess, this.path);
		}
		if (sendCallbacks)
		{
			p.Claim(this);
			this.lastCompletedNodePath = p.path;
			this.lastCompletedVectorPath = p.vectorPath;
			if (this.tmpPathCallback != null)
			{
				this.tmpPathCallback(p);
			}
			if (this.pathCallback != null)
			{
				this.pathCallback(p);
			}
			if (this.prevPath != null)
			{
				this.prevPath.ReleaseSilent(this);
			}
			this.prevPath = p;
			if (!this.drawGizmos)
			{
				this.ReleaseClaimedPath();
			}
		}
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00005F7F File Offset: 0x0000437F
	public void OnPartialPathComplete(Path p)
	{
		this.OnPathComplete(p, true, false);
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00005F8A File Offset: 0x0000438A
	public void OnMultiPathComplete(Path p)
	{
		this.OnPathComplete(p, false, true);
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00005F98 File Offset: 0x00004398
	public ABPath GetNewPath(Vector3 start, Vector3 end)
	{
		return ABPath.Construct(start, end, null);
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00005FAF File Offset: 0x000043AF
	public Path StartPath(Vector3 start, Vector3 end)
	{
		return this.StartPath(start, end, null, -1);
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00005FBB File Offset: 0x000043BB
	public Path StartPath(Vector3 start, Vector3 end, OnPathDelegate callback)
	{
		return this.StartPath(start, end, callback, -1);
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00005FC8 File Offset: 0x000043C8
	public Path StartPath(Vector3 start, Vector3 end, OnPathDelegate callback, int graphMask)
	{
		Path newPath = this.GetNewPath(start, end);
		return this.StartPath(newPath, callback, graphMask);
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00005FE8 File Offset: 0x000043E8
	public Path StartPath(Path p, OnPathDelegate callback = null, int graphMask = -1)
	{
		p.enabledTags = this.traversableTags.tagsChange;
		p.tagPenalties = this.tagPenalties;
		if (this.path != null && this.path.GetState() <= PathState.Processing && this.lastPathID == (uint)this.path.pathID)
		{
			this.path.Error();
			this.path.LogError("Canceled path because a new one was requested.\nThis happens when a new path is requested from the seeker when one was already being calculated.\nFor example if a unit got a new order, you might request a new path directly instead of waiting for the now invalid path to be calculated. Which is probably what you want.\nIf you are getting this a lot, you might want to consider how you are scheduling path requests.");
		}
		this.path = p;
		Path path = this.path;
		path.callback = (OnPathDelegate)Delegate.Combine(path.callback, this.onPathDelegate);
		this.path.nnConstraint.graphMask = graphMask;
		this.tmpPathCallback = callback;
		this.lastPathID = (uint)this.path.pathID;
		this.RunModifiers(Seeker.ModifierPass.PreProcess, this.path);
		AstarPath.StartPath(this.path, false);
		return this.path;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x000060D0 File Offset: 0x000044D0
	public MultiTargetPath StartMultiTargetPath(Vector3 start, Vector3[] endPoints, bool pathsForAll, OnPathDelegate callback = null, int graphMask = -1)
	{
		MultiTargetPath multiTargetPath = MultiTargetPath.Construct(start, endPoints, null, null);
		multiTargetPath.pathsForAll = pathsForAll;
		return this.StartMultiTargetPath(multiTargetPath, callback, graphMask);
	}

	// Token: 0x0600005C RID: 92 RVA: 0x000060FC File Offset: 0x000044FC
	public MultiTargetPath StartMultiTargetPath(Vector3[] startPoints, Vector3 end, bool pathsForAll, OnPathDelegate callback = null, int graphMask = -1)
	{
		MultiTargetPath multiTargetPath = MultiTargetPath.Construct(startPoints, end, null, null);
		multiTargetPath.pathsForAll = pathsForAll;
		return this.StartMultiTargetPath(multiTargetPath, callback, graphMask);
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00006128 File Offset: 0x00004528
	public MultiTargetPath StartMultiTargetPath(MultiTargetPath p, OnPathDelegate callback = null, int graphMask = -1)
	{
		if (this.path != null && this.path.GetState() <= PathState.Processing && this.lastPathID == (uint)this.path.pathID)
		{
			this.path.ForceLogError("Canceled path because a new one was requested");
		}
		OnPathDelegate[] array = new OnPathDelegate[p.targetPoints.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = this.onPartialPathDelegate;
		}
		p.callbacks = array;
		p.callback = (OnPathDelegate)Delegate.Combine(p.callback, new OnPathDelegate(this.OnMultiPathComplete));
		p.nnConstraint.graphMask = graphMask;
		this.path = p;
		this.tmpPathCallback = callback;
		this.lastPathID = (uint)this.path.pathID;
		this.RunModifiers(Seeker.ModifierPass.PreProcess, this.path);
		AstarPath.StartPath(this.path, false);
		return p;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00006210 File Offset: 0x00004610
	public IEnumerator DelayPathStart(Path p)
	{
		yield return null;
		this.RunModifiers(Seeker.ModifierPass.PreProcess, p);
		AstarPath.StartPath(p, false);
		yield break;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00006234 File Offset: 0x00004634
	public void OnDrawGizmos()
	{
		if (this.lastCompletedNodePath == null || !this.drawGizmos)
		{
			return;
		}
		if (this.detailedGizmos)
		{
			Gizmos.color = new Color(0.7f, 0.5f, 0.1f, 0.5f);
			if (this.lastCompletedNodePath != null)
			{
				for (int i = 0; i < this.lastCompletedNodePath.Count - 1; i++)
				{
					Gizmos.DrawLine((Vector3)this.lastCompletedNodePath[i].position, (Vector3)this.lastCompletedNodePath[i + 1].position);
				}
			}
		}
		Gizmos.color = new Color(0f, 1f, 0f, 1f);
		if (this.lastCompletedVectorPath != null)
		{
			for (int j = 0; j < this.lastCompletedVectorPath.Count - 1; j++)
			{
				Gizmos.DrawLine(this.lastCompletedVectorPath[j], this.lastCompletedVectorPath[j + 1]);
			}
		}
	}

	// Token: 0x04000065 RID: 101
	public bool drawGizmos = true;

	// Token: 0x04000066 RID: 102
	public bool detailedGizmos;

	// Token: 0x04000067 RID: 103
	[HideInInspector]
	public bool saveGetNearestHints = true;

	// Token: 0x04000068 RID: 104
	public StartEndModifier startEndModifier = new StartEndModifier();

	// Token: 0x04000069 RID: 105
	[HideInInspector]
	public TagMask traversableTags = new TagMask(-1, -1);

	// Token: 0x0400006A RID: 106
	[HideInInspector]
	public int[] tagPenalties = new int[32];

	// Token: 0x0400006B RID: 107
	public OnPathDelegate pathCallback;

	// Token: 0x0400006C RID: 108
	public OnPathDelegate preProcessPath;

	// Token: 0x0400006D RID: 109
	public OnPathDelegate postProcessOriginalPath;

	// Token: 0x0400006E RID: 110
	public OnPathDelegate postProcessPath;

	// Token: 0x0400006F RID: 111
	[NonSerialized]
	public List<Vector3> lastCompletedVectorPath;

	// Token: 0x04000070 RID: 112
	[NonSerialized]
	public List<GraphNode> lastCompletedNodePath;

	// Token: 0x04000071 RID: 113
	[NonSerialized]
	protected Path path;

	// Token: 0x04000072 RID: 114
	private Path prevPath;

	// Token: 0x04000073 RID: 115
	private GraphNode startHint;

	// Token: 0x04000074 RID: 116
	private GraphNode endHint;

	// Token: 0x04000075 RID: 117
	private OnPathDelegate onPathDelegate;

	// Token: 0x04000076 RID: 118
	private OnPathDelegate onPartialPathDelegate;

	// Token: 0x04000077 RID: 119
	private OnPathDelegate tmpPathCallback;

	// Token: 0x04000078 RID: 120
	protected uint lastPathID;

	// Token: 0x04000079 RID: 121
	private List<IPathModifier> modifiers = new List<IPathModifier>();

	// Token: 0x0200000B RID: 11
	public enum ModifierPass
	{
		// Token: 0x0400007B RID: 123
		PreProcess,
		// Token: 0x0400007C RID: 124
		PostProcessOriginal,
		// Token: 0x0400007D RID: 125
		PostProcess
	}
}
