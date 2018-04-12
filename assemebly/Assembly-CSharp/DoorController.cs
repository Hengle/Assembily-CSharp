using System;
using Pathfinding;
using UnityEngine;

// Token: 0x0200007A RID: 122
public class DoorController : MonoBehaviour
{
	// Token: 0x060003F8 RID: 1016 RVA: 0x000205E3 File Offset: 0x0001E9E3
	public void Start()
	{
		this.bounds = base.GetComponent<Collider>().bounds;
		this.SetState(this.open);
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00020602 File Offset: 0x0001EA02
	private void OnGUI()
	{
		if (GUI.Button(new Rect(5f, this.yOffset, 100f, 22f), "Toggle Door"))
		{
			this.SetState(!this.open);
		}
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x0002063C File Offset: 0x0001EA3C
	public void SetState(bool open)
	{
		this.open = open;
		if (this.updateGraphsWithGUO)
		{
			GraphUpdateObject graphUpdateObject = new GraphUpdateObject(this.bounds);
			int num = (!open) ? this.closedtag : this.opentag;
			if (num > 31)
			{
				Debug.LogError("tag > 31");
				return;
			}
			graphUpdateObject.modifyTag = true;
			graphUpdateObject.setTag = num;
			graphUpdateObject.updatePhysics = false;
			AstarPath.active.UpdateGraphs(graphUpdateObject);
		}
		if (open)
		{
			base.GetComponent<Animation>().Play("Open");
		}
		else
		{
			base.GetComponent<Animation>().Play("Close");
		}
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x000206DF File Offset: 0x0001EADF
	private void Update()
	{
	}

	// Token: 0x04000375 RID: 885
	private bool open;

	// Token: 0x04000376 RID: 886
	public int opentag = 1;

	// Token: 0x04000377 RID: 887
	public int closedtag = 1;

	// Token: 0x04000378 RID: 888
	public bool updateGraphsWithGUO = true;

	// Token: 0x04000379 RID: 889
	public float yOffset = 5f;

	// Token: 0x0400037A RID: 890
	private Bounds bounds;
}
