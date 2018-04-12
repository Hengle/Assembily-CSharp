using System;
using System.Collections.Generic;
using HighlightingSystem;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000733 RID: 1843
	public class HighlighterTool
	{
		// Token: 0x060033F9 RID: 13305 RVA: 0x00152700 File Offset: 0x00150B00
		public static void color(Transform target, Color color)
		{
			if (target == null)
			{
				return;
			}
			if (target.GetComponent<Renderer>() != null)
			{
				target.GetComponent<Renderer>().material.color = color;
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					Transform transform = target.FindChild("Model_" + i);
					if (!(transform == null))
					{
						if (transform.GetComponent<Renderer>() != null)
						{
							transform.GetComponent<Renderer>().material.color = color;
						}
					}
				}
			}
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x001527A0 File Offset: 0x00150BA0
		public static void destroyMaterials(Transform target)
		{
			if (target == null)
			{
				return;
			}
			if (target.GetComponent<Renderer>() != null)
			{
				UnityEngine.Object.DestroyImmediate(target.GetComponent<Renderer>().material);
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					Transform transform = target.FindChild("Model_" + i);
					if (!(transform == null))
					{
						if (transform.GetComponent<Renderer>() != null)
						{
							UnityEngine.Object.DestroyImmediate(transform.GetComponent<Renderer>().material);
						}
					}
				}
			}
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x0015283B File Offset: 0x00150C3B
		public static void help(Transform target, bool isValid)
		{
			HighlighterTool.help(target, isValid, false);
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x00152848 File Offset: 0x00150C48
		public static void help(Transform target, bool isValid, bool isRecursive)
		{
			Material sharedMaterial = (!isValid) ? ((Material)Resources.Load("Materials/Bad")) : ((Material)Resources.Load("Materials/Good"));
			if (target.GetComponent<Renderer>() != null)
			{
				target.GetComponent<Renderer>().sharedMaterial = sharedMaterial;
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					Transform transform;
					if (isRecursive)
					{
						transform = target.FindChildRecursive("Model_" + i);
					}
					else
					{
						transform = target.FindChild("Model_" + i);
					}
					if (!(transform == null))
					{
						if (transform.GetComponent<Renderer>() != null)
						{
							transform.GetComponent<Renderer>().sharedMaterial = sharedMaterial;
						}
					}
				}
			}
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x0015291C File Offset: 0x00150D1C
		public static void guide(Transform target)
		{
			Material sharedMaterial = (Material)Resources.Load("Materials/Guide");
			HighlighterTool.renderers.Clear();
			target.GetComponentsInChildren<Renderer>(true, HighlighterTool.renderers);
			for (int i = 0; i < HighlighterTool.renderers.Count; i++)
			{
				if (!(HighlighterTool.renderers[i].transform != target) || HighlighterTool.renderers[i].name.IndexOf("Model") != -1)
				{
					HighlighterTool.renderers[i].sharedMaterial = sharedMaterial;
				}
			}
			List<Collider> list = new List<Collider>();
			target.GetComponentsInChildren<Collider>(list);
			for (int j = 0; j < list.Count; j++)
			{
				UnityEngine.Object.Destroy(list[j]);
			}
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x001529EC File Offset: 0x00150DEC
		public static void highlight(Transform target, Color color)
		{
			if (target.CompareTag("Player") || target.CompareTag("Enemy") || target.CompareTag("Zombie") || target.CompareTag("Animal") || target.CompareTag("Agent"))
			{
				return;
			}
			Highlighter highlighter = target.GetComponent<Highlighter>();
			if (highlighter == null)
			{
				highlighter = target.gameObject.AddComponent<Highlighter>();
			}
			highlighter.ConstantOn(color);
			highlighter.SeeThroughOff();
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x00152A78 File Offset: 0x00150E78
		public static void unhighlight(Transform target)
		{
			Highlighter component = target.GetComponent<Highlighter>();
			if (component == null)
			{
				return;
			}
			UnityEngine.Object.DestroyImmediate(component);
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x00152AA0 File Offset: 0x00150EA0
		public static void skin(Transform target, Material skin)
		{
			if (target.GetComponent<Renderer>() != null)
			{
				target.GetComponent<Renderer>().sharedMaterial = skin;
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					Transform transform = target.FindChild("Model_" + i);
					if (!(transform == null))
					{
						if (transform.GetComponent<Renderer>() != null)
						{
							transform.GetComponent<Renderer>().sharedMaterial = skin;
						}
					}
				}
			}
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x00152B28 File Offset: 0x00150F28
		public static Material getMaterial(Transform target)
		{
			if (target == null)
			{
				return null;
			}
			Renderer component = target.GetComponent<Renderer>();
			if (component != null)
			{
				return component.sharedMaterial;
			}
			for (int i = 0; i < 4; i++)
			{
				Transform transform = target.FindChild("Model_" + i);
				if (transform == null)
				{
					return null;
				}
				component = transform.GetComponent<Renderer>();
				if (component != null)
				{
					return component.sharedMaterial;
				}
			}
			return null;
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x00152BB0 File Offset: 0x00150FB0
		public static Material getMaterialInstance(Transform target)
		{
			if (target == null)
			{
				return null;
			}
			Renderer component = target.GetComponent<Renderer>();
			if (component != null)
			{
				return component.material;
			}
			Material material = null;
			Material y = null;
			for (int i = 0; i < 4; i++)
			{
				Transform transform = target.FindChild("Model_" + i);
				if (transform == null)
				{
					break;
				}
				component = transform.GetComponent<Renderer>();
				if (component != null)
				{
					if (material == null)
					{
						y = component.sharedMaterial;
						material = component.material;
					}
					else if (component.sharedMaterial == y)
					{
						component.sharedMaterial = material;
					}
				}
			}
			return material;
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x00152C74 File Offset: 0x00151074
		public static void remesh(Transform target, List<Mesh> newMeshes, List<Mesh> outOldMeshes, bool extendLODs = false)
		{
			if (newMeshes == null || newMeshes.Count < 1)
			{
				return;
			}
			if (outOldMeshes != null && outOldMeshes != newMeshes)
			{
				outOldMeshes.Clear();
			}
			MeshFilter component = target.GetComponent<MeshFilter>();
			if (component != null)
			{
				Mesh sharedMesh = component.sharedMesh;
				component.sharedMesh = newMeshes[0];
				if (outOldMeshes != null)
				{
					if (outOldMeshes == newMeshes)
					{
						newMeshes[0] = sharedMesh;
					}
					else
					{
						outOldMeshes.Add(sharedMesh);
					}
				}
			}
			else
			{
				int num = 4;
				if (!extendLODs)
				{
					num = Mathf.Min(num, newMeshes.Count);
				}
				for (int i = 0; i < num; i++)
				{
					Transform transform = target.FindChild("Model_" + i);
					if (!(transform == null))
					{
						component = transform.GetComponent<MeshFilter>();
						if (component != null)
						{
							Mesh sharedMesh2 = component.sharedMesh;
							component.sharedMesh = ((i >= newMeshes.Count) ? newMeshes[0] : newMeshes[i]);
							if (outOldMeshes != null)
							{
								if (outOldMeshes == newMeshes)
								{
									if (i < newMeshes.Count)
									{
										newMeshes[i] = sharedMesh2;
									}
									else
									{
										newMeshes.Add(sharedMesh2);
									}
								}
								else
								{
									outOldMeshes.Add(sharedMesh2);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x00152DC4 File Offset: 0x001511C4
		public static void rematerialize(Transform target, Material newMaterial, out Material oldMaterial)
		{
			oldMaterial = null;
			Renderer component = target.GetComponent<Renderer>();
			if (component != null)
			{
				oldMaterial = component.sharedMaterial;
				component.sharedMaterial = newMaterial;
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					Transform transform = target.FindChild("Model_" + i);
					if (!(transform == null))
					{
						component = transform.GetComponent<Renderer>();
						if (component != null)
						{
							oldMaterial = component.sharedMaterial;
							component.sharedMaterial = newMaterial;
						}
					}
				}
			}
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x00152E58 File Offset: 0x00151258
		private static HighlighterBatch getBatchable()
		{
			if (HighlighterTool.batchablePoolIndex < HighlighterTool.batchablePool.Count)
			{
				HighlighterBatch highlighterBatch = HighlighterTool.batchablePool[HighlighterTool.batchablePoolIndex];
				highlighterBatch.texture = null;
				highlighterBatch.meshes.Clear();
				highlighterBatch.renderers.Clear();
				HighlighterTool.batchablePoolIndex++;
				return highlighterBatch;
			}
			HighlighterBatch highlighterBatch2 = new HighlighterBatch();
			HighlighterTool.batchablePool.Add(highlighterBatch2);
			HighlighterTool.batchablePoolIndex++;
			return highlighterBatch2;
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x00152ED4 File Offset: 0x001512D4
		private static void checkBatchable(List<GameObject> list, MeshFilter mesh, MeshRenderer renderer)
		{
			if (mesh != null && mesh.sharedMesh != null && renderer != null && renderer.sharedMaterials != null && renderer.sharedMaterials.Length == 1)
			{
				Texture2D texture2D = (Texture2D)renderer.sharedMaterial.mainTexture;
				HighlighterShaderGroup highlighterShaderGroup = null;
				if (texture2D != null && texture2D.wrapMode == TextureWrapMode.Clamp && texture2D.width <= 128 && texture2D.height <= 128)
				{
					if (renderer.sharedMaterial.shader.name == "Standard")
					{
						if (renderer.sharedMaterial.GetFloat("_Mode") == 0f && texture2D.filterMode == FilterMode.Point)
						{
							highlighterShaderGroup = HighlighterTool.batchableOpaque;
						}
					}
					else if (renderer.sharedMaterial.shader.name == "Custom/Card")
					{
						highlighterShaderGroup = HighlighterTool.batchableCard;
					}
					else if (renderer.sharedMaterial.shader.name == "Custom/Foliage" && texture2D.filterMode == FilterMode.Trilinear)
					{
						highlighterShaderGroup = HighlighterTool.batchableFoliage;
					}
				}
				if (highlighterShaderGroup != null)
				{
					HighlighterBatch highlighterBatch = null;
					if (!highlighterShaderGroup.batchableTextures.TryGetValue(texture2D, out highlighterBatch))
					{
						highlighterBatch = HighlighterTool.getBatchable();
						highlighterBatch.texture = texture2D;
						highlighterShaderGroup.batchableTextures.Add(texture2D, highlighterBatch);
					}
					if (highlighterBatch != null)
					{
						List<MeshFilter> list2;
						if (!highlighterBatch.meshes.TryGetValue(mesh.sharedMesh, out list2))
						{
							list2 = new List<MeshFilter>();
							highlighterBatch.meshes.Add(mesh.sharedMesh, list2);
						}
						list2.Add(mesh);
						highlighterBatch.renderers.Add(renderer);
						list.Add(mesh.gameObject);
					}
				}
				else
				{
					List<GameObject> list3 = null;
					if (!HighlighterTool.batchableMaterials.TryGetValue(renderer.sharedMaterial, out list3))
					{
						list3 = new List<GameObject>();
						HighlighterTool.batchableMaterials.Add(renderer.sharedMaterial, list3);
					}
					list3.Add(mesh.gameObject);
				}
			}
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x001530E8 File Offset: 0x001514E8
		private static void batch(HighlighterShaderGroup group)
		{
			Material materialTemplate = group.materialTemplate;
			Dictionary<Texture2D, HighlighterBatch> batchableTextures = group.batchableTextures;
			if (batchableTextures.Count > 0)
			{
				Texture2D texture2D = new Texture2D(16, 16);
				texture2D.name = "Atlas";
				texture2D.wrapMode = TextureWrapMode.Clamp;
				texture2D.filterMode = group.filterMode;
				HighlighterBatch[] array = new HighlighterBatch[batchableTextures.Count];
				batchableTextures.Values.CopyTo(array, 0);
				Texture2D[] array2 = new Texture2D[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					HighlighterBatch highlighterBatch = array[i];
					Texture2D texture = highlighterBatch.texture;
					RenderTexture temporary = RenderTexture.GetTemporary(texture.width, texture.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
					Graphics.Blit(texture, temporary);
					RenderTexture active = RenderTexture.active;
					RenderTexture.active = temporary;
					Texture2D texture2D2 = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false, true);
					texture2D2.name = "Copy";
					texture2D2.ReadPixels(new Rect(0f, 0f, (float)texture.width, (float)texture.height), 0, 0);
					texture2D2.Apply();
					array2[i] = texture2D2;
					RenderTexture.active = active;
					RenderTexture.ReleaseTemporary(temporary);
				}
				Rect[] array3 = texture2D.PackTextures(array2, 0, 1024, true);
				if (array3 != null)
				{
					Material material = UnityEngine.Object.Instantiate<Material>(materialTemplate);
					material.name = "Material";
					material.mainTexture = texture2D;
					for (int j = 0; j < array.Length; j++)
					{
						HighlighterBatch highlighterBatch2 = array[j];
						List<MeshFilter>[] array4 = new List<MeshFilter>[highlighterBatch2.meshes.Count];
						highlighterBatch2.meshes.Values.CopyTo(array4, 0);
						for (int k = 0; k < array4.Length; k++)
						{
							Mesh mesh = array4[k][0].mesh;
							Vector2[] uv = mesh.uv;
							for (int l = 0; l < uv.Length; l++)
							{
								uv[l].x = array3[j].x + uv[l].x * array3[j].width;
								uv[l].y = array3[j].y + uv[l].y * array3[j].height;
							}
							mesh.uv = uv;
							if (array4[k].Count > 1)
							{
								for (int m = 1; m < array4[k].Count; m++)
								{
									array4[k][m].sharedMesh = mesh;
								}
							}
						}
						for (int n = 0; n < highlighterBatch2.renderers.Count; n++)
						{
							highlighterBatch2.renderers[n].sharedMaterial = material;
						}
					}
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(texture2D);
				}
				for (int num = 0; num < array2.Length; num++)
				{
					UnityEngine.Object.DestroyImmediate(array2[num]);
				}
			}
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x00153404 File Offset: 0x00151804
		public static void beginBatch()
		{
			if (HighlighterTool.batchableOpaque == null)
			{
				HighlighterTool.batchableOpaque = new HighlighterShaderGroup();
				HighlighterTool.batchableOpaque.materialTemplate = (Material)Resources.Load("Level/Opaque");
			}
			if (HighlighterTool.batchableCard == null)
			{
				HighlighterTool.batchableCard = new HighlighterShaderGroup();
				HighlighterTool.batchableCard.materialTemplate = (Material)Resources.Load("Level/Card");
			}
			if (HighlighterTool.batchableFoliage == null)
			{
				HighlighterTool.batchableFoliage = new HighlighterShaderGroup();
				HighlighterTool.batchableFoliage.materialTemplate = (Material)Resources.Load("Level/Foliage");
				HighlighterTool.batchableFoliage.filterMode = FilterMode.Trilinear;
			}
			HighlighterTool.batchableOpaque.batchableTextures.Clear();
			HighlighterTool.batchableCard.batchableTextures.Clear();
			HighlighterTool.batchableFoliage.batchableTextures.Clear();
			HighlighterTool.batchableGameObjects.Clear();
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x001534DC File Offset: 0x001518DC
		public static void collectBatch(List<GameObject> targets)
		{
			if (targets.Count == 0)
			{
				return;
			}
			HighlighterTool.batchableMaterials.Clear();
			List<GameObject> list = new List<GameObject>();
			HighlighterTool.batchableGameObjects.Add(list);
			for (int i = 0; i < targets.Count; i++)
			{
				GameObject gameObject = targets[i];
				HighlighterTool.lods.Clear();
				gameObject.GetComponentsInChildren<MeshFilter>(HighlighterTool.lods);
				for (int j = 0; j < HighlighterTool.lods.Count; j++)
				{
					MeshFilter meshFilter = HighlighterTool.lods[j];
					MeshRenderer component = meshFilter.gameObject.GetComponent<MeshRenderer>();
					HighlighterTool.checkBatchable(list, meshFilter, component);
				}
			}
			if (HighlighterTool.batchableMaterials.Count > 0)
			{
				List<GameObject>[] array = new List<GameObject>[HighlighterTool.batchableMaterials.Count];
				HighlighterTool.batchableMaterials.Values.CopyTo(array, 0);
				for (int k = 0; k < array.Length; k++)
				{
					if (array[k].Count >= 2)
					{
						StaticBatchingUtility.Combine(array[k].ToArray(), Level.roots.gameObject);
					}
				}
			}
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x00153600 File Offset: 0x00151A00
		public static void endBatch()
		{
			HighlighterTool.batch(HighlighterTool.batchableOpaque);
			HighlighterTool.batch(HighlighterTool.batchableCard);
			HighlighterTool.batch(HighlighterTool.batchableFoliage);
			for (int i = 0; i < HighlighterTool.batchableGameObjects.Count; i++)
			{
				if (HighlighterTool.batchableGameObjects[i].Count != 0)
				{
					StaticBatchingUtility.Combine(HighlighterTool.batchableGameObjects[i].ToArray(), Level.roots.gameObject);
				}
			}
		}

		// Token: 0x0400236A RID: 9066
		private static List<Renderer> renderers = new List<Renderer>();

		// Token: 0x0400236B RID: 9067
		private static List<MeshFilter> lods = new List<MeshFilter>();

		// Token: 0x0400236C RID: 9068
		private static HighlighterShaderGroup batchableOpaque;

		// Token: 0x0400236D RID: 9069
		private static HighlighterShaderGroup batchableCard;

		// Token: 0x0400236E RID: 9070
		private static HighlighterShaderGroup batchableFoliage;

		// Token: 0x0400236F RID: 9071
		private static List<List<GameObject>> batchableGameObjects = new List<List<GameObject>>();

		// Token: 0x04002370 RID: 9072
		private static Dictionary<Material, List<GameObject>> batchableMaterials = new Dictionary<Material, List<GameObject>>();

		// Token: 0x04002371 RID: 9073
		private static List<HighlighterBatch> batchablePool = new List<HighlighterBatch>();

		// Token: 0x04002372 RID: 9074
		private static int batchablePoolIndex = 0;
	}
}
