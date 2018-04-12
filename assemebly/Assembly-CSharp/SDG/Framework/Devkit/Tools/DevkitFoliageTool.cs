using System;
using System.Collections.Generic;
using SDG.Framework.Devkit.Transactions;
using SDG.Framework.Foliage;
using SDG.Framework.Rendering;
using SDG.Framework.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit.Tools
{
	// Token: 0x0200015C RID: 348
	public class DevkitFoliageTool : IDevkitTool
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00051915 File Offset: 0x0004FD15
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x00051921 File Offset: 0x0004FD21
		public virtual float brushRadius
		{
			get
			{
				return DevkitFoliageToolOptions.instance.brushRadius;
			}
			set
			{
				DevkitFoliageToolOptions.instance.brushRadius = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0005192E File Offset: 0x0004FD2E
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x0005193A File Offset: 0x0004FD3A
		public virtual float brushFalloff
		{
			get
			{
				return DevkitFoliageToolOptions.instance.brushFalloff;
			}
			set
			{
				DevkitFoliageToolOptions.instance.brushFalloff = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00051947 File Offset: 0x0004FD47
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x00051953 File Offset: 0x0004FD53
		public virtual float brushStrength
		{
			get
			{
				return DevkitFoliageToolOptions.instance.brushStrength;
			}
			set
			{
				DevkitFoliageToolOptions.instance.brushStrength = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00051960 File Offset: 0x0004FD60
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x0005196C File Offset: 0x0004FD6C
		public virtual uint maxPreviewSamples
		{
			get
			{
				return DevkitFoliageToolOptions.instance.maxPreviewSamples;
			}
			set
			{
				DevkitFoliageToolOptions.instance.maxPreviewSamples = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00051979 File Offset: 0x0004FD79
		protected virtual bool isChangingBrush
		{
			get
			{
				return this.isChangingBrushRadius || this.isChangingBrushFalloff || this.isChangingBrushStrength;
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0005199A File Offset: 0x0004FD9A
		protected virtual void beginChangeHotkeyTransaction()
		{
			DevkitTransactionUtility.beginGenericTransaction();
			DevkitTransactionUtility.recordObjectDelta(DevkitFoliageToolOptions.instance);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x000519AB File Offset: 0x0004FDAB
		protected virtual void endChangeHotkeyTransaction()
		{
			DevkitTransactionUtility.endGenericTransaction();
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000519B4 File Offset: 0x0004FDB4
		protected virtual void addFoliage(FoliageInfoAsset foliageAsset, float weightMultiplier)
		{
			if (foliageAsset == null)
			{
				return;
			}
			float num = 3.14159274f * this.brushRadius * this.brushRadius;
			float newRadius = Mathf.Sqrt(foliageAsset.density / DevkitFoliageToolOptions.instance.densityTarget / 3.14159274f);
			float num2;
			if (!this.addWeights.TryGetValue(foliageAsset, out num2))
			{
				this.addWeights.Add(foliageAsset, 0f);
			}
			num2 += DevkitFoliageToolOptions.addSensitivity * num * this.brushStrength * weightMultiplier * Time.deltaTime;
			if (num2 > 1f)
			{
				this.previewSamples.Clear();
				int num3 = Mathf.FloorToInt(num2);
				num2 -= (float)num3;
				for (int i = 0; i < num3; i++)
				{
					float num4 = this.brushRadius * UnityEngine.Random.value;
					float brushAlpha = this.getBrushAlpha(num4);
					if (UnityEngine.Random.value >= brushAlpha)
					{
						float f = 6.28318548f * UnityEngine.Random.value;
						float x = Mathf.Cos(f) * num4;
						float z = Mathf.Sin(f) * num4;
						Ray ray = new Ray(this.brushWorldPosition + new Vector3(x, this.brushRadius, z), new Vector3(0f, -1f, 0f));
						RaycastHit raycastHit;
						if (PhysicsUtility.raycast(ray, out raycastHit, this.brushRadius * 2f, (int)DevkitFoliageToolOptions.instance.surfaceMask, QueryTriggerInteraction.UseGlobal))
						{
							SphereVolume sphereVolume = new SphereVolume(raycastHit.point, newRadius);
							if (foliageAsset.getInstanceCountInVolume(sphereVolume) <= 0)
							{
								if (Input.GetKey(KeyCode.Mouse0))
								{
									foliageAsset.addFoliageToSurface(raycastHit.point, raycastHit.normal, false, false);
								}
							}
						}
					}
				}
			}
			this.addWeights[foliageAsset] = num2;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00051B74 File Offset: 0x0004FF74
		protected virtual void removeInstances(FoliageTile foliageTile, FoliageInstanceList list, float sqrBrushRadius, float sqrBrushFalloffRadius, ref int sampleCount)
		{
			for (int i = list.matrices.Count - 1; i >= 0; i--)
			{
				List<Matrix4x4> list2 = list.matrices[i];
				List<bool> list3 = list.clearWhenBaked[i];
				for (int j = list2.Count - 1; j >= 0; j--)
				{
					if (!list3[j])
					{
						Matrix4x4 matrix = list2[j];
						Vector3 position = matrix.GetPosition();
						float sqrMagnitude = (position - this.brushWorldPosition).sqrMagnitude;
						if (sqrMagnitude < sqrBrushRadius)
						{
							bool flag = sqrMagnitude < sqrBrushFalloffRadius;
							this.previewSamples.Add(new FoliagePreviewSample(position, (!flag) ? (Color.red / 2f) : Color.red));
							if (Input.GetKey(KeyCode.Mouse0) && flag && sampleCount > 0)
							{
								foliageTile.removeInstance(list, i, j);
								sampleCount--;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00051C80 File Offset: 0x00050080
		public virtual void update()
		{
			Ray pointerToWorldRay = DevkitInput.pointerToWorldRay;
			RaycastHit raycastHit;
			this.isPointerOnWorld = PhysicsUtility.raycast(pointerToWorldRay, out raycastHit, 8192f, (int)DevkitFoliageToolOptions.instance.surfaceMask, QueryTriggerInteraction.UseGlobal);
			this.pointerWorldPosition = raycastHit.point;
			this.previewSamples.Clear();
			if (!DevkitNavigation.isNavigating && DevkitInput.canEditorReceiveInput)
			{
				if (Input.GetKeyDown(KeyCode.Q))
				{
					this.mode = DevkitFoliageTool.EFoliageMode.PAINT;
				}
				if (Input.GetKeyDown(KeyCode.W))
				{
					this.mode = DevkitFoliageTool.EFoliageMode.EXACT;
				}
				if (this.mode == DevkitFoliageTool.EFoliageMode.PAINT)
				{
					if (Input.GetKeyDown(KeyCode.B))
					{
						this.isChangingBrushRadius = true;
						this.beginChangeHotkeyTransaction();
					}
					if (Input.GetKeyDown(KeyCode.F))
					{
						this.isChangingBrushFalloff = true;
						this.beginChangeHotkeyTransaction();
					}
					if (Input.GetKeyDown(KeyCode.V))
					{
						this.isChangingBrushStrength = true;
						this.beginChangeHotkeyTransaction();
					}
				}
			}
			if (Input.GetKeyUp(KeyCode.B))
			{
				this.isChangingBrushRadius = false;
				this.endChangeHotkeyTransaction();
			}
			if (Input.GetKeyUp(KeyCode.F))
			{
				this.isChangingBrushFalloff = false;
				this.endChangeHotkeyTransaction();
			}
			if (Input.GetKeyUp(KeyCode.V))
			{
				this.isChangingBrushStrength = false;
				this.endChangeHotkeyTransaction();
			}
			if (this.isChangingBrush)
			{
				Plane plane = default(Plane);
				plane.SetNormalAndPosition(Vector3.up, this.brushWorldPosition);
				float d;
				plane.Raycast(pointerToWorldRay, out d);
				this.changePlanePosition = pointerToWorldRay.origin + pointerToWorldRay.direction * d;
				if (this.isChangingBrushRadius)
				{
					this.brushRadius = (this.changePlanePosition - this.brushWorldPosition).magnitude;
				}
				if (this.isChangingBrushFalloff)
				{
					this.brushFalloff = Mathf.Clamp01((this.changePlanePosition - this.brushWorldPosition).magnitude / this.brushRadius);
				}
				if (this.isChangingBrushStrength)
				{
					this.brushStrength = (this.changePlanePosition - this.brushWorldPosition).magnitude / this.brushRadius;
				}
			}
			else
			{
				this.brushWorldPosition = this.pointerWorldPosition;
			}
			this.isBrushVisible = (this.isPointerOnWorld || this.isChangingBrush);
			if (!DevkitNavigation.isNavigating && DevkitInput.canEditorReceiveInput)
			{
				if (this.mode == DevkitFoliageTool.EFoliageMode.PAINT)
				{
					Bounds worldBounds = new Bounds(this.brushWorldPosition, new Vector3(this.brushRadius * 2f, 0f, this.brushRadius * 2f));
					float num = this.brushRadius * this.brushRadius;
					float num2 = num * this.brushFalloff * this.brushFalloff;
					float num3 = 3.14159274f * this.brushRadius * this.brushRadius;
					if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftShift))
					{
						this.removeWeight += DevkitFoliageToolOptions.removeSensitivity * num3 * this.brushStrength * Time.deltaTime;
						int num4 = 0;
						if (this.removeWeight > 1f)
						{
							num4 = Mathf.FloorToInt(this.removeWeight);
							this.removeWeight -= (float)num4;
						}
						FoliageBounds foliageBounds = new FoliageBounds(worldBounds);
						for (int i = foliageBounds.min.x; i <= foliageBounds.max.x; i++)
						{
							for (int j = foliageBounds.min.y; j <= foliageBounds.max.y; j++)
							{
								FoliageCoord tileCoord = new FoliageCoord(i, j);
								FoliageTile tile = FoliageSystem.getTile(tileCoord);
								if (tile != null)
								{
									if (!tile.hasInstances)
									{
										tile.readInstancesOnThread();
									}
									if (Input.GetKey(KeyCode.LeftControl))
									{
										if (DevkitFoliageTool.selectedInstanceAsset != null)
										{
											FoliageInstanceList list;
											if (tile.instances.TryGetValue(DevkitFoliageTool.selectedInstanceAsset.getReferenceTo<FoliageInstancedMeshInfoAsset>(), out list))
											{
												this.removeInstances(tile, list, num, num2, ref num4);
											}
										}
										else if (DevkitFoliageTool.selectedCollectionAsset != null)
										{
											foreach (FoliageInfoCollectionAsset.FoliageInfoCollectionElement foliageInfoCollectionElement in DevkitFoliageTool.selectedCollectionAsset.elements)
											{
												FoliageInstancedMeshInfoAsset foliageInstancedMeshInfoAsset = Assets.find<FoliageInfoAsset>(foliageInfoCollectionElement.asset) as FoliageInstancedMeshInfoAsset;
												FoliageInstanceList list2;
												if (foliageInstancedMeshInfoAsset != null && tile.instances.TryGetValue(foliageInstancedMeshInfoAsset.getReferenceTo<FoliageInstancedMeshInfoAsset>(), out list2))
												{
													this.removeInstances(tile, list2, num, num2, ref num4);
												}
											}
										}
									}
									else
									{
										foreach (KeyValuePair<AssetReference<FoliageInstancedMeshInfoAsset>, FoliageInstanceList> keyValuePair in tile.instances)
										{
											FoliageInstanceList value = keyValuePair.Value;
											this.removeInstances(tile, value, num, num2, ref num4);
										}
									}
								}
							}
						}
						RegionBounds regionBounds = new RegionBounds(worldBounds);
						for (byte b = regionBounds.min.x; b <= regionBounds.max.x; b += 1)
						{
							for (byte b2 = regionBounds.min.y; b2 <= regionBounds.max.y; b2 += 1)
							{
								List<ResourceSpawnpoint> list3 = LevelGround.trees[(int)b, (int)b2];
								for (int k = list3.Count - 1; k >= 0; k--)
								{
									ResourceSpawnpoint resourceSpawnpoint = list3[k];
									if (!resourceSpawnpoint.isGenerated)
									{
										if (Input.GetKey(KeyCode.LeftControl))
										{
											if (DevkitFoliageTool.selectedInstanceAsset != null)
											{
												FoliageResourceInfoAsset foliageResourceInfoAsset = DevkitFoliageTool.selectedInstanceAsset as FoliageResourceInfoAsset;
												if (foliageResourceInfoAsset == null || !foliageResourceInfoAsset.resource.isReferenceTo(resourceSpawnpoint.asset))
												{
													goto IL_6BB;
												}
											}
											else if (DevkitFoliageTool.selectedCollectionAsset != null)
											{
												bool flag = false;
												foreach (FoliageInfoCollectionAsset.FoliageInfoCollectionElement foliageInfoCollectionElement2 in DevkitFoliageTool.selectedCollectionAsset.elements)
												{
													FoliageResourceInfoAsset foliageResourceInfoAsset2 = Assets.find<FoliageInfoAsset>(foliageInfoCollectionElement2.asset) as FoliageResourceInfoAsset;
													if (foliageResourceInfoAsset2 != null && foliageResourceInfoAsset2.resource.isReferenceTo(resourceSpawnpoint.asset))
													{
														flag = true;
														break;
													}
												}
												if (!flag)
												{
													goto IL_6BB;
												}
											}
										}
										float sqrMagnitude = (resourceSpawnpoint.point - this.brushWorldPosition).sqrMagnitude;
										if (sqrMagnitude < num)
										{
											bool flag2 = sqrMagnitude < num2;
											this.previewSamples.Add(new FoliagePreviewSample(resourceSpawnpoint.point, (!flag2) ? (Color.red / 2f) : Color.red));
											if (Input.GetKey(KeyCode.Mouse0) && flag2 && num4 > 0)
											{
												resourceSpawnpoint.destroy();
												list3.RemoveAt(k);
												num4--;
											}
										}
									}
									IL_6BB:;
								}
								List<LevelObject> list4 = LevelObjects.objects[(int)b, (int)b2];
								for (int l = list4.Count - 1; l >= 0; l--)
								{
									LevelObject levelObject = list4[l];
									if (levelObject.placementOrigin == ELevelObjectPlacementOrigin.PAINTED)
									{
										if (Input.GetKey(KeyCode.LeftControl))
										{
											if (DevkitFoliageTool.selectedInstanceAsset != null)
											{
												FoliageObjectInfoAsset foliageObjectInfoAsset = DevkitFoliageTool.selectedInstanceAsset as FoliageObjectInfoAsset;
												if (foliageObjectInfoAsset == null || !foliageObjectInfoAsset.obj.isReferenceTo(levelObject.asset))
												{
													goto IL_888;
												}
											}
											else if (DevkitFoliageTool.selectedCollectionAsset != null)
											{
												bool flag3 = false;
												foreach (FoliageInfoCollectionAsset.FoliageInfoCollectionElement foliageInfoCollectionElement3 in DevkitFoliageTool.selectedCollectionAsset.elements)
												{
													FoliageObjectInfoAsset foliageObjectInfoAsset2 = Assets.find<FoliageInfoAsset>(foliageInfoCollectionElement3.asset) as FoliageObjectInfoAsset;
													if (foliageObjectInfoAsset2 != null && foliageObjectInfoAsset2.obj.isReferenceTo(levelObject.asset))
													{
														flag3 = true;
														break;
													}
												}
												if (!flag3)
												{
													goto IL_888;
												}
											}
										}
										float sqrMagnitude2 = (levelObject.transform.position - this.brushWorldPosition).sqrMagnitude;
										if (sqrMagnitude2 < num)
										{
											bool flag4 = sqrMagnitude2 < num2;
											this.previewSamples.Add(new FoliagePreviewSample(levelObject.transform.position, (!flag4) ? (Color.red / 2f) : Color.red));
											if (Input.GetKey(KeyCode.Mouse0) && flag4 && num4 > 0)
											{
												levelObject.destroy();
												list4.RemoveAt(l);
												num4--;
											}
										}
									}
									IL_888:;
								}
							}
						}
					}
					else if (DevkitFoliageTool.selectedInstanceAsset != null)
					{
						this.addFoliage(DevkitFoliageTool.selectedInstanceAsset, 1f);
					}
					else if (DevkitFoliageTool.selectedCollectionAsset != null)
					{
						foreach (FoliageInfoCollectionAsset.FoliageInfoCollectionElement foliageInfoCollectionElement4 in DevkitFoliageTool.selectedCollectionAsset.elements)
						{
							this.addFoliage(Assets.find<FoliageInfoAsset>(foliageInfoCollectionElement4.asset), foliageInfoCollectionElement4.weight);
						}
					}
				}
				else if (Input.GetKeyDown(KeyCode.Mouse0))
				{
					if (DevkitFoliageTool.selectedInstanceAsset != null)
					{
						if (DevkitFoliageTool.selectedInstanceAsset != null)
						{
							DevkitFoliageTool.selectedInstanceAsset.addFoliageToSurface(raycastHit.point, raycastHit.normal, false, false);
						}
					}
					else if (DevkitFoliageTool.selectedCollectionAsset != null)
					{
						FoliageInfoCollectionAsset.FoliageInfoCollectionElement foliageInfoCollectionElement5 = DevkitFoliageTool.selectedCollectionAsset.elements[UnityEngine.Random.Range(0, DevkitFoliageTool.selectedCollectionAsset.elements.Count)];
						FoliageInfoAsset foliageInfoAsset = Assets.find<FoliageInfoAsset>(foliageInfoCollectionElement5.asset);
						if (foliageInfoAsset != null)
						{
							foliageInfoAsset.addFoliageToSurface(raycastHit.point, raycastHit.normal, false, false);
						}
					}
				}
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000526C0 File Offset: 0x00050AC0
		public virtual void equip()
		{
			GLRenderer.render += this.handleGLRender;
			this.mode = DevkitFoliageTool.EFoliageMode.PAINT;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000526DA File Offset: 0x00050ADA
		public virtual void dequip()
		{
			GLRenderer.render -= this.handleGLRender;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000526ED File Offset: 0x00050AED
		protected float getBrushAlpha(float distance)
		{
			if (distance < this.brushFalloff)
			{
				return 1f;
			}
			return (1f - distance) / (1f - this.brushFalloff);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00052718 File Offset: 0x00050B18
		protected void handleGLRender()
		{
			if (this.isBrushVisible && DevkitInput.canEditorReceiveInput)
			{
				GLUtility.matrix = MathUtility.IDENTITY_MATRIX;
				if ((long)this.previewSamples.Count <= (long)((ulong)this.maxPreviewSamples))
				{
					GLUtility.LINE_FLAT_COLOR.SetPass(0);
					GL.Begin(4);
					float num = Mathf.Lerp(0.25f, 1f, this.brushRadius / 256f);
					Vector3 size = new Vector3(num, num, num);
					foreach (FoliagePreviewSample foliagePreviewSample in this.previewSamples)
					{
						GL.Color(foliagePreviewSample.color);
						GLUtility.boxSolid(foliagePreviewSample.position, size);
					}
					GL.End();
				}
				if (this.mode == DevkitFoliageTool.EFoliageMode.PAINT)
				{
					GL.LoadOrtho();
					GLUtility.LINE_FLAT_COLOR.SetPass(0);
					GL.Begin(1);
					Color color;
					if (this.isChangingBrushStrength)
					{
						color = Color.Lerp(Color.red, Color.green, this.brushStrength);
					}
					else
					{
						color = Color.yellow;
					}
					Vector3 vector = MainCamera.instance.WorldToViewportPoint(this.brushWorldPosition);
					vector.z = 0f;
					Vector3 a = MainCamera.instance.WorldToViewportPoint(this.brushWorldPosition + MainCamera.instance.transform.right * this.brushRadius);
					a.z = 0f;
					Vector3 a2 = MainCamera.instance.WorldToViewportPoint(this.brushWorldPosition + MainCamera.instance.transform.up * this.brushRadius);
					a2.z = 0f;
					Vector3 a3 = MainCamera.instance.WorldToViewportPoint(this.brushWorldPosition + MainCamera.instance.transform.right * this.brushRadius * this.brushFalloff);
					a3.z = 0f;
					Vector3 a4 = MainCamera.instance.WorldToViewportPoint(this.brushWorldPosition + MainCamera.instance.transform.up * this.brushRadius * this.brushFalloff);
					a4.z = 0f;
					GL.Color(color / 2f);
					GLUtility.circle(vector, 1f, a - vector, a2 - vector, 64f);
					GL.Color(color);
					GLUtility.circle(vector, 1f, a3 - vector, a4 - vector, 64f);
					GL.End();
				}
				else
				{
					GLUtility.matrix = Matrix4x4.TRS(this.brushWorldPosition, MathUtility.IDENTITY_QUATERNION, new Vector3(1f, 1f, 1f));
					GLUtility.LINE_FLAT_COLOR.SetPass(0);
					GL.Begin(1);
					GL.Color(Color.yellow);
					GLUtility.line(new Vector3(-1f, 0f, 0f), new Vector3(1f, 0f, 0f));
					GLUtility.line(new Vector3(0f, -1f, 0f), new Vector3(0f, 1f, 0f));
					GLUtility.line(new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, 1f));
					GL.End();
				}
			}
		}

		// Token: 0x0400075A RID: 1882
		public static FoliageInfoCollectionAsset selectedCollectionAsset;

		// Token: 0x0400075B RID: 1883
		public static FoliageInfoAsset selectedInstanceAsset;

		// Token: 0x0400075C RID: 1884
		protected DevkitFoliageTool.EFoliageMode mode;

		// Token: 0x0400075D RID: 1885
		protected Vector3 pointerWorldPosition;

		// Token: 0x0400075E RID: 1886
		protected Vector3 brushWorldPosition;

		// Token: 0x0400075F RID: 1887
		protected Vector3 changePlanePosition;

		// Token: 0x04000760 RID: 1888
		protected bool isPointerOnWorld;

		// Token: 0x04000761 RID: 1889
		protected bool isBrushVisible;

		// Token: 0x04000762 RID: 1890
		protected Dictionary<FoliageInfoAsset, float> addWeights = new Dictionary<FoliageInfoAsset, float>();

		// Token: 0x04000763 RID: 1891
		protected float removeWeight;

		// Token: 0x04000764 RID: 1892
		protected List<FoliagePreviewSample> previewSamples = new List<FoliagePreviewSample>();

		// Token: 0x04000765 RID: 1893
		protected bool isChangingBrushRadius;

		// Token: 0x04000766 RID: 1894
		protected bool isChangingBrushFalloff;

		// Token: 0x04000767 RID: 1895
		protected bool isChangingBrushStrength;

		// Token: 0x0200015D RID: 349
		public enum EFoliageMode
		{
			// Token: 0x04000769 RID: 1897
			PAINT,
			// Token: 0x0400076A RID: 1898
			EXACT
		}
	}
}
