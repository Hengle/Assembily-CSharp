using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000082 RID: 130
	[Serializable]
	public class GraphCollision
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x00021A98 File Offset: 0x0001FE98
		public void Initialize(Matrix4x4 matrix, float scale)
		{
			this.up = matrix.MultiplyVector(Vector3.up);
			this.upheight = this.up * this.height;
			this.finalRadius = this.diameter * scale * 0.5f;
			this.finalRaycastRadius = this.thickRaycastDiameter * scale * 0.5f;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00021AF8 File Offset: 0x0001FEF8
		public bool Check(Vector3 position)
		{
			if (!this.collisionCheck)
			{
				return true;
			}
			if (this.use2D)
			{
				ColliderType colliderType = this.type;
				if (colliderType == ColliderType.Capsule)
				{
					throw new Exception("Capsule mode cannot be used with 2D since capsules don't exist in 2D");
				}
				if (colliderType != ColliderType.Sphere)
				{
					return Physics2D.OverlapPoint(position, this.mask) == null;
				}
				return Physics2D.OverlapCircle(position, this.finalRadius, this.mask) == null;
			}
			else
			{
				position += this.up * this.collisionOffset;
				ColliderType colliderType2 = this.type;
				if (colliderType2 == ColliderType.Capsule)
				{
					return !Physics.CheckCapsule(position, position + this.upheight, this.finalRadius, this.mask);
				}
				if (colliderType2 == ColliderType.Sphere)
				{
					return !Physics.CheckSphere(position, this.finalRadius, this.mask);
				}
				RayDirection rayDirection = this.rayDirection;
				if (rayDirection == RayDirection.Both)
				{
					return !Physics.Raycast(position, this.up, this.height, this.mask) && !Physics.Raycast(position + this.upheight, -this.up, this.height, this.mask);
				}
				if (rayDirection != RayDirection.Up)
				{
					return !Physics.Raycast(position + this.upheight, -this.up, this.height, this.mask);
				}
				return !Physics.Raycast(position, this.up, this.height, this.mask);
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00021CB8 File Offset: 0x000200B8
		public Vector3 CheckHeight(Vector3 position)
		{
			RaycastHit raycastHit;
			bool flag;
			return this.CheckHeight(position, out raycastHit, out flag);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00021CD0 File Offset: 0x000200D0
		public Vector3 CheckHeight(Vector3 position, out RaycastHit hit, out bool walkable)
		{
			walkable = true;
			if (!this.heightCheck || this.use2D)
			{
				hit = default(RaycastHit);
				return position;
			}
			if (this.thickRaycast)
			{
				Ray ray = new Ray(position + this.up * this.fromHeight, -this.up);
				if (Physics.SphereCast(ray, this.finalRaycastRadius, out hit, this.fromHeight + 0.005f, this.heightMask))
				{
					return AstarMath.NearestPoint(ray.origin, ray.origin + ray.direction, hit.point);
				}
				if (this.unwalkableWhenNoGround)
				{
					walkable = false;
				}
			}
			else
			{
				if (Physics.Raycast(position + this.up * this.fromHeight, -this.up, out hit, this.fromHeight + 0.005f, this.heightMask))
				{
					return hit.point;
				}
				if (this.unwalkableWhenNoGround)
				{
					walkable = false;
				}
			}
			return position;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00021DF0 File Offset: 0x000201F0
		public Vector3 Raycast(Vector3 origin, out RaycastHit hit, out bool walkable)
		{
			walkable = true;
			if (!this.heightCheck || this.use2D)
			{
				hit = default(RaycastHit);
				return origin - this.up * this.fromHeight;
			}
			if (this.thickRaycast)
			{
				Ray ray = new Ray(origin, -this.up);
				if (Physics.SphereCast(ray, this.finalRaycastRadius, out hit, this.fromHeight + 0.005f, this.heightMask))
				{
					return AstarMath.NearestPoint(ray.origin, ray.origin + ray.direction, hit.point);
				}
				if (this.unwalkableWhenNoGround)
				{
					walkable = false;
				}
			}
			else
			{
				if (Physics.Raycast(origin, -this.up, out hit, this.fromHeight + 0.005f, this.heightMask))
				{
					return hit.point;
				}
				if (this.unwalkableWhenNoGround)
				{
					walkable = false;
				}
			}
			return origin - this.up * this.fromHeight;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00021F10 File Offset: 0x00020310
		public RaycastHit[] CheckHeightAll(Vector3 position)
		{
			if (!this.heightCheck || this.use2D)
			{
				return new RaycastHit[]
				{
					new RaycastHit
					{
						point = position,
						distance = 0f
					}
				};
			}
			if (this.thickRaycast)
			{
				Debug.LogWarning("Thick raycast cannot be used with CheckHeightAll. Disabling thick raycast...");
				this.thickRaycast = false;
			}
			List<RaycastHit> list = new List<RaycastHit>();
			bool flag = true;
			Vector3 vector = position + this.up * this.fromHeight;
			Vector3 vector2 = Vector3.zero;
			int num = 0;
			for (;;)
			{
				RaycastHit item;
				this.Raycast(vector, out item, out flag);
				if (item.transform == null)
				{
					break;
				}
				if (item.point != vector2 || list.Count == 0)
				{
					vector = item.point - this.up * 0.005f;
					vector2 = item.point;
					num = 0;
					list.Add(item);
				}
				else
				{
					vector -= this.up * 0.001f;
					num++;
					if (num > 10)
					{
						goto Block_5;
					}
				}
			}
			goto IL_166;
			Block_5:
			Debug.LogError(string.Concat(new object[]
			{
				"Infinite Loop when raycasting. Please report this error (arongranberg.com)\n",
				vector,
				" : ",
				vector2
			}));
			IL_166:
			return list.ToArray();
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0002208C File Offset: 0x0002048C
		public void SerializeSettings(GraphSerializationContext ctx)
		{
			ctx.writer.Write((int)this.type);
			ctx.writer.Write(this.diameter);
			ctx.writer.Write(this.height);
			ctx.writer.Write(this.collisionOffset);
			ctx.writer.Write((int)this.rayDirection);
			ctx.writer.Write(this.mask);
			ctx.writer.Write(this.heightMask);
			ctx.writer.Write(this.fromHeight);
			ctx.writer.Write(this.thickRaycast);
			ctx.writer.Write(this.thickRaycastDiameter);
			ctx.writer.Write(this.unwalkableWhenNoGround);
			ctx.writer.Write(this.use2D);
			ctx.writer.Write(this.collisionCheck);
			ctx.writer.Write(this.heightCheck);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00022194 File Offset: 0x00020594
		public void DeserializeSettings(GraphSerializationContext ctx)
		{
			this.type = (ColliderType)ctx.reader.ReadInt32();
			this.diameter = ctx.reader.ReadSingle();
			this.height = ctx.reader.ReadSingle();
			this.collisionOffset = ctx.reader.ReadSingle();
			this.rayDirection = (RayDirection)ctx.reader.ReadInt32();
			this.mask = ctx.reader.ReadInt32();
			this.heightMask = ctx.reader.ReadInt32();
			this.fromHeight = ctx.reader.ReadSingle();
			this.thickRaycast = ctx.reader.ReadBoolean();
			this.thickRaycastDiameter = ctx.reader.ReadSingle();
			this.unwalkableWhenNoGround = ctx.reader.ReadBoolean();
			this.use2D = ctx.reader.ReadBoolean();
			this.collisionCheck = ctx.reader.ReadBoolean();
			this.heightCheck = ctx.reader.ReadBoolean();
		}

		// Token: 0x0400039F RID: 927
		public ColliderType type = ColliderType.Capsule;

		// Token: 0x040003A0 RID: 928
		public float diameter = 1f;

		// Token: 0x040003A1 RID: 929
		public float height = 2f;

		// Token: 0x040003A2 RID: 930
		public float collisionOffset;

		// Token: 0x040003A3 RID: 931
		public RayDirection rayDirection = RayDirection.Both;

		// Token: 0x040003A4 RID: 932
		public LayerMask mask;

		// Token: 0x040003A5 RID: 933
		public LayerMask heightMask = -1;

		// Token: 0x040003A6 RID: 934
		public float fromHeight = 100f;

		// Token: 0x040003A7 RID: 935
		public bool thickRaycast;

		// Token: 0x040003A8 RID: 936
		public float thickRaycastDiameter = 1f;

		// Token: 0x040003A9 RID: 937
		public bool unwalkableWhenNoGround = true;

		// Token: 0x040003AA RID: 938
		public bool use2D;

		// Token: 0x040003AB RID: 939
		public bool collisionCheck = true;

		// Token: 0x040003AC RID: 940
		public bool heightCheck = true;

		// Token: 0x040003AD RID: 941
		public Vector3 up;

		// Token: 0x040003AE RID: 942
		private Vector3 upheight;

		// Token: 0x040003AF RID: 943
		private float finalRadius;

		// Token: 0x040003B0 RID: 944
		private float finalRaycastRadius;

		// Token: 0x040003B1 RID: 945
		public const float RaycastErrorMargin = 0.005f;
	}
}
