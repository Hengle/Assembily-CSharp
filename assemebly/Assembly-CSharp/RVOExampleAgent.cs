using System;
using System.Collections.Generic;
using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class RVOExampleAgent : MonoBehaviour
{
	// Token: 0x060003BF RID: 959 RVA: 0x0001CC1A File Offset: 0x0001B01A
	public void Awake()
	{
		this.seeker = base.GetComponent<Seeker>();
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x0001CC28 File Offset: 0x0001B028
	public void Start()
	{
		this.SetTarget(-base.transform.position);
		this.controller = base.GetComponent<RVOController>();
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x0001CC4C File Offset: 0x0001B04C
	public void SetTarget(Vector3 target)
	{
		this.target = target;
		this.RecalculatePath();
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x0001CC5C File Offset: 0x0001B05C
	public void SetColor(Color col)
	{
		if (this.rends == null)
		{
			this.rends = base.GetComponentsInChildren<MeshRenderer>();
		}
		foreach (MeshRenderer meshRenderer in this.rends)
		{
			Color color = meshRenderer.material.GetColor("_TintColor");
			AnimationCurve curve = AnimationCurve.Linear(0f, color.r, 1f, col.r);
			AnimationCurve curve2 = AnimationCurve.Linear(0f, color.g, 1f, col.g);
			AnimationCurve curve3 = AnimationCurve.Linear(0f, color.b, 1f, col.b);
			AnimationClip animationClip = new AnimationClip();
			animationClip.SetCurve(string.Empty, typeof(Material), "_TintColor.r", curve);
			animationClip.SetCurve(string.Empty, typeof(Material), "_TintColor.g", curve2);
			animationClip.SetCurve(string.Empty, typeof(Material), "_TintColor.b", curve3);
			Animation animation = meshRenderer.gameObject.GetComponent<Animation>();
			if (animation == null)
			{
				animation = meshRenderer.gameObject.AddComponent<Animation>();
			}
			animationClip.wrapMode = WrapMode.Once;
			animation.AddClip(animationClip, "ColorAnim");
			animation.Play("ColorAnim");
		}
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x0001CDB8 File Offset: 0x0001B1B8
	public void RecalculatePath()
	{
		this.canSearchAgain = false;
		this.nextRepath = Time.time + this.repathRate * (UnityEngine.Random.value + 0.5f);
		this.seeker.StartPath(base.transform.position, this.target, new OnPathDelegate(this.OnPathComplete));
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x0001CE14 File Offset: 0x0001B214
	public void OnPathComplete(Path _p)
	{
		ABPath abpath = _p as ABPath;
		this.canSearchAgain = true;
		if (this.path != null)
		{
			this.path.Release(this);
		}
		this.path = abpath;
		abpath.Claim(this);
		if (abpath.error)
		{
			this.wp = 0;
			this.vectorPath = null;
			return;
		}
		Vector3 originalStartPoint = abpath.originalStartPoint;
		Vector3 position = base.transform.position;
		originalStartPoint.y = position.y;
		float magnitude = (position - originalStartPoint).magnitude;
		this.wp = 0;
		this.vectorPath = abpath.vectorPath;
		for (float num = 0f; num <= magnitude; num += this.moveNextDist * 0.6f)
		{
			this.wp--;
			Vector3 a = originalStartPoint + (position - originalStartPoint) * num;
			Vector3 b;
			do
			{
				this.wp++;
				b = this.vectorPath[this.wp];
				b.y = a.y;
			}
			while ((a - b).sqrMagnitude < this.moveNextDist * this.moveNextDist && this.wp != this.vectorPath.Count - 1);
		}
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x0001CF68 File Offset: 0x0001B368
	public void Update()
	{
		if (Time.time >= this.nextRepath && this.canSearchAgain)
		{
			this.RecalculatePath();
		}
		Vector3 vector = Vector3.zero;
		Vector3 position = base.transform.position;
		if (this.vectorPath != null && this.vectorPath.Count != 0)
		{
			Vector3 vector2 = this.vectorPath[this.wp];
			vector2.y = position.y;
			while ((position - vector2).sqrMagnitude < this.moveNextDist * this.moveNextDist && this.wp != this.vectorPath.Count - 1)
			{
				this.wp++;
				vector2 = this.vectorPath[this.wp];
				vector2.y = position.y;
			}
			vector = vector2 - position;
			float magnitude = vector.magnitude;
			if (magnitude > 0f)
			{
				float num = Mathf.Min(magnitude, this.controller.maxSpeed);
				vector *= num / magnitude;
			}
		}
		this.controller.Move(vector);
	}

	// Token: 0x04000322 RID: 802
	public float repathRate = 1f;

	// Token: 0x04000323 RID: 803
	private float nextRepath;

	// Token: 0x04000324 RID: 804
	private Vector3 target;

	// Token: 0x04000325 RID: 805
	private bool canSearchAgain = true;

	// Token: 0x04000326 RID: 806
	private RVOController controller;

	// Token: 0x04000327 RID: 807
	private Path path;

	// Token: 0x04000328 RID: 808
	private List<Vector3> vectorPath;

	// Token: 0x04000329 RID: 809
	private int wp;

	// Token: 0x0400032A RID: 810
	public float moveNextDist = 1f;

	// Token: 0x0400032B RID: 811
	private Seeker seeker;

	// Token: 0x0400032C RID: 812
	private MeshRenderer[] rends;
}
