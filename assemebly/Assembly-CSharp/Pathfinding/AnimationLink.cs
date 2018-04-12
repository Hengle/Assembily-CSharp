using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000017 RID: 23
	public class AnimationLink : NodeLink2
	{
		// Token: 0x06000145 RID: 325 RVA: 0x0000EB0C File Offset: 0x0000CF0C
		private static Transform SearchRec(Transform tr, string name)
		{
			int childCount = tr.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Transform child = tr.GetChild(i);
				if (child.name == name)
				{
					return child;
				}
				Transform transform = AnimationLink.SearchRec(child, name);
				if (transform != null)
				{
					return transform;
				}
			}
			return null;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000EB64 File Offset: 0x0000CF64
		public void CalculateOffsets(List<Vector3> trace, out Vector3 endPosition)
		{
			endPosition = base.transform.position;
			if (this.referenceMesh == null)
			{
				return;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.referenceMesh, base.transform.position, base.transform.rotation);
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			Transform transform = AnimationLink.SearchRec(gameObject.transform, this.boneRoot);
			if (transform == null)
			{
				throw new Exception("Could not find root transform");
			}
			Animation animation = gameObject.GetComponent<Animation>();
			if (animation == null)
			{
				animation = gameObject.AddComponent<Animation>();
			}
			for (int i = 0; i < this.sequence.Length; i++)
			{
				animation.AddClip(this.sequence[i].clip, this.sequence[i].clip.name);
			}
			Vector3 a = Vector3.zero;
			Vector3 vector = base.transform.position;
			Vector3 b = Vector3.zero;
			for (int j = 0; j < this.sequence.Length; j++)
			{
				AnimationLink.LinkClip linkClip = this.sequence[j];
				if (linkClip == null)
				{
					endPosition = vector;
					return;
				}
				animation[linkClip.clip.name].enabled = true;
				animation[linkClip.clip.name].weight = 1f;
				for (int k = 0; k < linkClip.loopCount; k++)
				{
					animation[linkClip.clip.name].normalizedTime = 0f;
					animation.Sample();
					Vector3 vector2 = transform.position - base.transform.position;
					if (j > 0)
					{
						vector += a - vector2;
					}
					else
					{
						b = vector2;
					}
					for (int l = 0; l <= 20; l++)
					{
						float num = (float)l / 20f;
						animation[linkClip.clip.name].normalizedTime = num;
						animation.Sample();
						Vector3 item = vector + (transform.position - base.transform.position) + linkClip.velocity * num * linkClip.clip.length;
						trace.Add(item);
					}
					vector += linkClip.velocity * 1f * linkClip.clip.length;
					animation[linkClip.clip.name].normalizedTime = 1f;
					animation.Sample();
					Vector3 vector3 = transform.position - base.transform.position;
					a = vector3;
				}
				animation[linkClip.clip.name].enabled = false;
				animation[linkClip.clip.name].weight = 0f;
			}
			vector += a - b;
			UnityEngine.Object.DestroyImmediate(gameObject);
			endPosition = vector;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000EE88 File Offset: 0x0000D288
		public override void OnDrawGizmosSelected()
		{
			base.OnDrawGizmosSelected();
			List<Vector3> list = ListPool<Vector3>.Claim();
			Vector3 zero = Vector3.zero;
			this.CalculateOffsets(list, out zero);
			Gizmos.color = Color.blue;
			for (int i = 0; i < list.Count - 1; i++)
			{
				Gizmos.DrawLine(list[i], list[i + 1]);
			}
		}

		// Token: 0x040000FE RID: 254
		public string clip;

		// Token: 0x040000FF RID: 255
		public float animSpeed = 1f;

		// Token: 0x04000100 RID: 256
		public bool reverseAnim = true;

		// Token: 0x04000101 RID: 257
		public GameObject referenceMesh;

		// Token: 0x04000102 RID: 258
		public AnimationLink.LinkClip[] sequence;

		// Token: 0x04000103 RID: 259
		public string boneRoot = "bn_COG_Root";

		// Token: 0x02000018 RID: 24
		[Serializable]
		public class LinkClip
		{
			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000149 RID: 329 RVA: 0x0000EEF7 File Offset: 0x0000D2F7
			public string name
			{
				get
				{
					return (!(this.clip != null)) ? string.Empty : this.clip.name;
				}
			}

			// Token: 0x04000104 RID: 260
			public AnimationClip clip;

			// Token: 0x04000105 RID: 261
			public Vector3 velocity;

			// Token: 0x04000106 RID: 262
			public int loopCount = 1;
		}
	}
}
