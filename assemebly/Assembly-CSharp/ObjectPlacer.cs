using System;
using Pathfinding;
using UnityEngine;

// Token: 0x0200007F RID: 127
public class ObjectPlacer : MonoBehaviour
{
	// Token: 0x0600040D RID: 1037 RVA: 0x00021015 File Offset: 0x0001F415
	private void Start()
	{
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x00021017 File Offset: 0x0001F417
	private void Update()
	{
		if (Input.GetKeyDown("p"))
		{
			this.PlaceObject();
		}
		if (Input.GetKeyDown("r"))
		{
			this.RemoveObject();
		}
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x00021044 File Offset: 0x0001F444
	public void PlaceObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, float.PositiveInfinity))
		{
			Vector3 point = raycastHit.point;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.go, point, Quaternion.identity);
			if (this.issueGUOs)
			{
				Bounds bounds = gameObject.GetComponent<Collider>().bounds;
				GraphUpdateObject ob = new GraphUpdateObject(bounds);
				AstarPath.active.UpdateGraphs(ob);
				if (this.direct)
				{
					AstarPath.active.FlushGraphUpdates();
				}
			}
		}
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x000210D0 File Offset: 0x0001F4D0
	public void RemoveObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, float.PositiveInfinity))
		{
			if (raycastHit.collider.isTrigger || raycastHit.transform.gameObject.name == "Ground")
			{
				return;
			}
			Bounds bounds = raycastHit.collider.bounds;
			UnityEngine.Object.Destroy(raycastHit.collider);
			UnityEngine.Object.Destroy(raycastHit.collider.gameObject);
			if (this.issueGUOs)
			{
				GraphUpdateObject ob = new GraphUpdateObject(bounds);
				AstarPath.active.UpdateGraphs(ob, 0f);
				if (this.direct)
				{
					AstarPath.active.FlushGraphUpdates();
				}
			}
		}
	}

	// Token: 0x0400038B RID: 907
	public GameObject go;

	// Token: 0x0400038C RID: 908
	public bool direct;

	// Token: 0x0400038D RID: 909
	public bool issueGUOs = true;
}
