using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

// Token: 0x0200007B RID: 123
public class DynamicGridObstacle : MonoBehaviour
{
	// Token: 0x060003FD RID: 1021 RVA: 0x000206FF File Offset: 0x0001EAFF
	private void Start()
	{
		this.col = base.GetComponent<Collider>();
		if (base.GetComponent<Collider>() == null)
		{
			Debug.LogError("A collider must be attached to the GameObject for DynamicGridObstacle to work");
		}
		base.StartCoroutine(this.UpdateGraphs());
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x00020738 File Offset: 0x0001EB38
	private IEnumerator UpdateGraphs()
	{
		if (this.col == null || AstarPath.active == null)
		{
			Debug.LogWarning("No collider is attached to the GameObject. Canceling check");
			yield break;
		}
		while (this.col)
		{
			while (this.isWaitingForUpdate)
			{
				yield return new WaitForSeconds(this.checkTime);
			}
			Bounds newBounds = this.col.bounds;
			Bounds merged = newBounds;
			merged.Encapsulate(this.prevBounds);
			Vector3 minDiff = merged.min - newBounds.min;
			Vector3 maxDiff = merged.max - newBounds.max;
			if (Mathf.Abs(minDiff.x) > this.updateError || Mathf.Abs(minDiff.y) > this.updateError || Mathf.Abs(minDiff.z) > this.updateError || Mathf.Abs(maxDiff.x) > this.updateError || Mathf.Abs(maxDiff.y) > this.updateError || Mathf.Abs(maxDiff.z) > this.updateError)
			{
				this.isWaitingForUpdate = true;
				this.DoUpdateGraphs();
			}
			yield return new WaitForSeconds(this.checkTime);
		}
		this.OnDestroy();
		yield break;
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x00020754 File Offset: 0x0001EB54
	public void OnDestroy()
	{
		if (AstarPath.active != null)
		{
			GraphUpdateObject ob = new GraphUpdateObject(this.prevBounds);
			AstarPath.active.UpdateGraphs(ob);
		}
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x00020788 File Offset: 0x0001EB88
	public void DoUpdateGraphs()
	{
		if (this.col == null)
		{
			return;
		}
		this.isWaitingForUpdate = false;
		Bounds bounds = this.col.bounds;
		Bounds bounds2 = bounds;
		bounds2.Encapsulate(this.prevBounds);
		if (this.BoundsVolume(bounds2) < this.BoundsVolume(bounds) + this.BoundsVolume(this.prevBounds))
		{
			AstarPath.active.UpdateGraphs(bounds2);
		}
		else
		{
			AstarPath.active.UpdateGraphs(this.prevBounds);
			AstarPath.active.UpdateGraphs(bounds);
		}
		this.prevBounds = bounds;
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x0002081C File Offset: 0x0001EC1C
	public float BoundsVolume(Bounds b)
	{
		return Math.Abs(b.size.x * b.size.y * b.size.z);
	}

	// Token: 0x0400037B RID: 891
	private Collider col;

	// Token: 0x0400037C RID: 892
	public float updateError = 1f;

	// Token: 0x0400037D RID: 893
	public float checkTime = 0.2f;

	// Token: 0x0400037E RID: 894
	private Bounds prevBounds;

	// Token: 0x0400037F RID: 895
	private bool isWaitingForUpdate;
}
