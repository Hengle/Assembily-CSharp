using System;
using System.Collections.Generic;
using SDG.Framework.Water;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004C0 RID: 1216
	public class Buoyancy : MonoBehaviour
	{
		// Token: 0x06002083 RID: 8323 RVA: 0x000B29A0 File Offset: 0x000B0DA0
		private void FixedUpdate()
		{
			for (int i = 0; i < this.voxels.Count; i++)
			{
				Vector3 vector = base.transform.TransformPoint(this.voxels[i]);
				bool flag;
				float num;
				if (this.overrideSurfaceElevation < 0f)
				{
					WaterUtility.getUnderwaterInfo(vector, out flag, out num);
				}
				else
				{
					flag = (vector.y < this.overrideSurfaceElevation);
					num = this.overrideSurfaceElevation;
				}
				if (flag)
				{
					if (!Dedicator.isDedicated)
					{
						num += Mathf.Sin((vector.x + vector.z) * 8f + Time.time) * 0.1f;
					}
					if (vector.y - this.voxelHalfHeight < num)
					{
						Vector3 pointVelocity = this.rootRigidbody.GetPointVelocity(vector);
						Vector3 a = -pointVelocity * Buoyancy.DAMPER * this.rootRigidbody.mass;
						Vector3 force = a + Mathf.Sqrt(Mathf.Clamp01((num - vector.y) / (2f * this.voxelHalfHeight) + 0.5f)) * this.localArchimedesForce;
						this.rootRigidbody.AddForceAtPosition(force, vector);
					}
				}
			}
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x000B2AE0 File Offset: 0x000B0EE0
		private void Awake()
		{
			this.rootRigidbody = base.gameObject.GetComponentInParent<Rigidbody>();
			this.volumeCollider = base.GetComponent<Collider>();
			Vector3 position = base.transform.position;
			Quaternion rotation = base.transform.rotation;
			base.transform.position = Vector3.zero;
			base.transform.rotation = Quaternion.identity;
			Bounds bounds = this.volumeCollider.bounds;
			if (bounds.size.x < bounds.size.y)
			{
				this.voxelHalfHeight = bounds.size.x;
			}
			else
			{
				this.voxelHalfHeight = bounds.size.y;
			}
			if (bounds.size.z < this.voxelHalfHeight)
			{
				this.voxelHalfHeight = bounds.size.z;
			}
			this.voxelHalfHeight /= (float)(2 * this.slicesPerAxis);
			this.voxels = new List<Vector3>(this.slicesPerAxis * this.slicesPerAxis * this.slicesPerAxis);
			for (int i = 0; i < this.slicesPerAxis; i++)
			{
				for (int j = 0; j < this.slicesPerAxis; j++)
				{
					for (int k = 0; k < this.slicesPerAxis; k++)
					{
						float x = bounds.min.x + bounds.size.x / (float)this.slicesPerAxis * (0.5f + (float)i);
						float y = bounds.min.y + bounds.size.y / (float)this.slicesPerAxis * (0.5f + (float)j);
						float z = bounds.min.z + bounds.size.z / (float)this.slicesPerAxis * (0.5f + (float)k);
						Vector3 vector = base.transform.InverseTransformPoint(new Vector3(x, y, z));
						bool flag = true;
						for (int l = 0; l < Buoyancy.DIRECTIONS.Length; l++)
						{
							if (!this.volumeCollider.Raycast(new Ray(vector - Buoyancy.DIRECTIONS[l] * 1000f, Buoyancy.DIRECTIONS[l]), out Buoyancy.insideHit, 1000f))
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							this.voxels.Add(vector);
						}
					}
				}
			}
			if (this.voxels.Count == 0)
			{
				this.voxels.Add(bounds.center);
			}
			base.transform.position = position;
			base.transform.rotation = rotation;
			float num = this.rootRigidbody.mass / this.density;
			float y2 = Buoyancy.WATER_DENSITY * Mathf.Abs(Physics.gravity.y) * num;
			this.localArchimedesForce = new Vector3(0f, y2, 0f) / (float)this.voxels.Count;
		}

		// Token: 0x0400135C RID: 4956
		private static readonly float DAMPER = 0.1f;

		// Token: 0x0400135D RID: 4957
		private static readonly float WATER_DENSITY = 1000f;

		// Token: 0x0400135E RID: 4958
		private static readonly Vector3[] DIRECTIONS = new Vector3[]
		{
			Vector3.up,
			Vector3.down,
			Vector3.left,
			Vector3.right,
			Vector3.forward,
			Vector3.back
		};

		// Token: 0x0400135F RID: 4959
		private static RaycastHit insideHit;

		// Token: 0x04001360 RID: 4960
		public float density = 500f;

		// Token: 0x04001361 RID: 4961
		public int slicesPerAxis = 2;

		// Token: 0x04001362 RID: 4962
		private float voxelHalfHeight;

		// Token: 0x04001363 RID: 4963
		private Vector3 localArchimedesForce;

		// Token: 0x04001364 RID: 4964
		private List<Vector3> voxels;

		// Token: 0x04001365 RID: 4965
		private Rigidbody rootRigidbody;

		// Token: 0x04001366 RID: 4966
		private Collider volumeCollider;

		// Token: 0x04001367 RID: 4967
		public float overrideSurfaceElevation = -1f;
	}
}
