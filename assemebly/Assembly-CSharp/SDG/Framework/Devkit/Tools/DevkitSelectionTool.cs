using System;
using System.Collections.Generic;
using SDG.Framework.Devkit.Interactable;
using SDG.Framework.Devkit.Transactions;
using SDG.Framework.Rendering;
using SDG.Framework.Translations;
using SDG.Framework.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit.Tools
{
	// Token: 0x02000173 RID: 371
	public class DevkitSelectionTool : IDevkitTool
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x000587C1 File Offset: 0x00056BC1
		// (set) Token: 0x06000B2C RID: 2860 RVA: 0x000587C8 File Offset: 0x00056BC8
		public static DevkitSelectionTool instance { get; protected set; }

		// Token: 0x06000B2D RID: 2861 RVA: 0x000587D0 File Offset: 0x00056BD0
		protected void transformSelection()
		{
			foreach (DevkitSelection devkitSelection in DevkitSelectionManager.selection)
			{
				if (!(devkitSelection.gameObject == null))
				{
					IDevkitSelectionTransformableHandler component = devkitSelection.gameObject.GetComponent<IDevkitSelectionTransformableHandler>();
					if (component != null)
					{
						component.transformSelection();
					}
				}
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00058854 File Offset: 0x00056C54
		protected void handlePositionTransformed(DevkitPositionHandle handle, Vector3 delta)
		{
			foreach (DevkitSelection devkitSelection in DevkitSelectionManager.selection)
			{
				if (!(devkitSelection.gameObject == null))
				{
					devkitSelection.transform.position += delta;
				}
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x000588D8 File Offset: 0x00056CD8
		protected void handleRotationTransformed(DevkitRotationHandle handle, Vector3 axis, float delta)
		{
			Matrix4x4 lhs = Matrix4x4.TRS(this.handlePosition, this.handleRotation, Vector3.one);
			Matrix4x4 inverse = lhs.inverse;
			Matrix4x4 rhs = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(axis * delta), Vector3.one);
			lhs *= rhs;
			foreach (DevkitSelection devkitSelection in DevkitSelectionManager.selection)
			{
				if (!(devkitSelection.gameObject == null))
				{
					Matrix4x4 rhs2 = inverse * devkitSelection.transform.localToWorldMatrix;
					Matrix4x4 matrix = lhs * rhs2;
					devkitSelection.transform.position = matrix.GetPosition();
					devkitSelection.transform.rotation = matrix.GetRotation();
				}
			}
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x000589CC File Offset: 0x00056DCC
		protected void handleScaleTransformed(DevkitScaleHandle handle, Vector3 delta)
		{
			foreach (DevkitSelection devkitSelection in DevkitSelectionManager.selection)
			{
				if (!(devkitSelection.gameObject == null))
				{
					Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
					this.meshFilters.Clear();
					if (devkitSelection.gameObject.CompareTag("Small") || devkitSelection.gameObject.CompareTag("Medium") || devkitSelection.gameObject.CompareTag("Large"))
					{
						devkitSelection.gameObject.GetComponentsInChildren<MeshFilter>(this.meshFilters);
						foreach (MeshFilter meshFilter in this.meshFilters)
						{
							Mesh sharedMesh = meshFilter.sharedMesh;
							if (!(sharedMesh == null))
							{
								Matrix4x4 matrix4x = meshFilter.transform.localToWorldMatrix * devkitSelection.transform.worldToLocalMatrix;
								Vector3 center = matrix4x.MultiplyPoint3x4(sharedMesh.bounds.center);
								Vector3 size = matrix4x.MultiplyPoint3x4(sharedMesh.bounds.size);
								bounds.Encapsulate(new Bounds(center, size));
							}
						}
					}
					if (this.meshFilters.Count > 0)
					{
						delta.x *= 1f / bounds.size.x;
						delta.y *= 1f / bounds.size.y;
						delta.z *= 1f / bounds.size.z;
					}
					devkitSelection.transform.localScale += delta;
				}
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00058C14 File Offset: 0x00057014
		protected void instantiate(Vector3 point, Vector3 normal)
		{
			IDevkitSelectionToolInstantiationInfo instantiationInfo = DevkitSelectionToolOptions.instance.instantiationInfo;
			if (instantiationInfo == null)
			{
				return;
			}
			point += normal * DevkitSelectionToolOptions.instance.surfaceOffset;
			instantiationInfo.position = point;
			if (DevkitSelectionToolOptions.instance.surfaceAlign)
			{
				Quaternion rotation = Quaternion.LookRotation(normal) * Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));
				instantiationInfo.rotation = rotation;
			}
			instantiationInfo.scale = Vector3.one;
			instantiationInfo.instantiate();
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00058CA4 File Offset: 0x000570A4
		protected void handleTeleportTransformed(Vector3 point, Vector3 normal)
		{
			point += normal * DevkitSelectionToolOptions.instance.surfaceOffset;
			Quaternion rotation = Quaternion.LookRotation(normal) * Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));
			this.moveHandle(point, rotation, Vector3.one, DevkitSelectionToolOptions.instance.surfaceAlign, false);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00058D0C File Offset: 0x0005710C
		protected void moveHandle(Vector3 position, Quaternion rotation, Vector3 scale, bool doRotation, bool doScale)
		{
			DevkitTransactionManager.beginTransaction(new TranslatedText(new TranslationReference("#SDG::Devkit.Transactions.Transform")));
			Matrix4x4 lhs = Matrix4x4.TRS(this.handlePosition, this.handleRotation, Vector3.one);
			Matrix4x4 inverse = lhs.inverse;
			Matrix4x4 rhs = Matrix4x4.TRS(Vector3.zero, Quaternion.Inverse(this.handleRotation) * rotation, Vector3.one);
			lhs *= rhs;
			Vector3 b = position - this.handlePosition;
			foreach (DevkitSelection devkitSelection in DevkitSelectionManager.selection)
			{
				if (!(devkitSelection.gameObject == null))
				{
					DevkitTransactionUtility.recordObjectDelta(devkitSelection.transform);
					if (doRotation)
					{
						Matrix4x4 rhs2 = inverse * devkitSelection.transform.localToWorldMatrix;
						Matrix4x4 matrix = lhs * rhs2;
						devkitSelection.transform.position = matrix.GetPosition();
						devkitSelection.transform.rotation = matrix.GetRotation();
					}
					devkitSelection.transform.position += b;
					if (doScale)
					{
						devkitSelection.transform.localScale = scale;
					}
				}
			}
			this.transformSelection();
			DevkitTransactionManager.endTransaction();
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00058E70 File Offset: 0x00057270
		public virtual void update()
		{
			if (this.copySelectionDelay.Count > 0)
			{
				DevkitSelectionManager.clear();
				foreach (GameObject newGameObject in this.copySelectionDelay)
				{
					DevkitSelectionManager.add(new DevkitSelection(newGameObject, null));
				}
				this.copySelectionDelay.Clear();
			}
			if (!DevkitNavigation.isNavigating)
			{
				if (DevkitInput.canEditorReceiveInput)
				{
					if (Input.GetKeyDown(KeyCode.Q))
					{
						this.mode = DevkitSelectionTool.ESelectionMode.POSITION;
					}
					if (Input.GetKeyDown(KeyCode.W))
					{
						this.mode = DevkitSelectionTool.ESelectionMode.ROTATION;
					}
					if (Input.GetKeyDown(KeyCode.R))
					{
						this.mode = DevkitSelectionTool.ESelectionMode.SCALE;
					}
				}
				Ray pointerToWorldRay = DevkitInput.pointerToWorldRay;
				RaycastHit raycastHit;
				if (!Physics.Raycast(pointerToWorldRay, out raycastHit, 8192f, RayMasks.LOGIC))
				{
					Physics.Raycast(pointerToWorldRay, out raycastHit, 8192f, (int)DevkitSelectionToolOptions.instance.selectionMask);
					if (DevkitInput.canEditorReceiveInput && Input.GetKeyDown(KeyCode.E) && raycastHit.transform != null)
					{
						if (DevkitSelectionManager.selection.Count > 0)
						{
							this.handleTeleportTransformed(raycastHit.point, raycastHit.normal);
						}
						else
						{
							this.instantiate(raycastHit.point, raycastHit.normal);
						}
					}
				}
				if (DevkitInput.canEditorReceiveInput)
				{
					if (Input.GetKeyDown(KeyCode.Mouse0))
					{
						this.drag = new DevkitSelection((!(raycastHit.transform != null)) ? null : raycastHit.transform.gameObject, raycastHit.collider);
						if (this.drag.isValid)
						{
							DevkitSelectionManager.data.point = raycastHit.point;
							this.isDragging = DevkitSelectionManager.beginDrag(this.drag);
							if (this.isDragging)
							{
								DevkitTransactionManager.beginTransaction(new TranslatedText(new TranslationReference("#SDG::Devkit.Transactions.Transform")));
								foreach (DevkitSelection devkitSelection in DevkitSelectionManager.selection)
								{
									DevkitTransactionUtility.recordObjectDelta(devkitSelection.transform);
								}
							}
						}
						if (!this.isDragging)
						{
							DevkitSelectionManager.data.point = raycastHit.point;
							this.beginAreaSelect = DevkitInput.pointerViewportPoint;
							this.beginAreaSelectTime = Time.time;
						}
					}
					if (Input.GetKey(KeyCode.Mouse0) && !this.isDragging && !this.isAreaSelecting && Time.time - this.beginAreaSelectTime > 0.1f)
					{
						this.isAreaSelecting = true;
						this.areaSelection.Clear();
						if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
						{
							DevkitSelectionManager.clear();
						}
					}
				}
				if (this.isDragging && this.drag.collider != null)
				{
					DevkitSelectionManager.data.point = raycastHit.point;
					DevkitSelectionManager.continueDrag(this.drag);
				}
				if (this.isAreaSelecting)
				{
					Vector3 pointerViewportPoint = DevkitInput.pointerViewportPoint;
					Vector2 vector;
					Vector2 vector2;
					if (pointerViewportPoint.x < this.beginAreaSelect.x)
					{
						vector.x = pointerViewportPoint.x;
						vector2.x = this.beginAreaSelect.x;
					}
					else
					{
						vector.x = this.beginAreaSelect.x;
						vector2.x = pointerViewportPoint.x;
					}
					if (pointerViewportPoint.y < this.beginAreaSelect.y)
					{
						vector.y = pointerViewportPoint.y;
						vector2.y = this.beginAreaSelect.y;
					}
					else
					{
						vector.y = this.beginAreaSelect.y;
						vector2.y = pointerViewportPoint.y;
					}
					int selectionMask = (int)DevkitSelectionToolOptions.instance.selectionMask;
					foreach (IDevkitHierarchyItem devkitHierarchyItem in LevelHierarchy.instance.items)
					{
						int num = 1 << devkitHierarchyItem.areaSelectGameObject.layer;
						if ((num & selectionMask) == num)
						{
							Vector3 vector3 = MainCamera.instance.WorldToViewportPoint(devkitHierarchyItem.areaSelectCenter);
							DevkitSelection devkitSelection2 = new DevkitSelection(devkitHierarchyItem.areaSelectGameObject, null);
							if (vector3.z > 0f && vector3.x > vector.x && vector3.x < vector2.x && vector3.y > vector.y && vector3.y < vector2.y)
							{
								if (!this.areaSelection.Contains(devkitSelection2))
								{
									this.areaSelection.Add(devkitSelection2);
									DevkitSelectionManager.add(devkitSelection2);
								}
							}
							else if (this.areaSelection.Contains(devkitSelection2))
							{
								this.areaSelection.Remove(devkitSelection2);
								DevkitSelectionManager.remove(devkitSelection2);
							}
						}
					}
				}
				if (Input.GetKeyUp(KeyCode.Mouse0))
				{
					if (this.isDragging)
					{
						if (this.drag.isValid)
						{
							DevkitSelectionManager.data.point = raycastHit.point;
							DevkitSelectionManager.endDrag(this.drag);
						}
						this.drag = DevkitSelection.invalid;
						this.isDragging = false;
						this.transformSelection();
						DevkitTransactionManager.endTransaction();
					}
					else if (this.isAreaSelecting)
					{
						this.isAreaSelecting = false;
					}
					else if (DevkitInput.canEditorReceiveInput)
					{
						DevkitSelectionManager.select(this.drag);
					}
				}
				if (raycastHit.transform != this.hover.transform || raycastHit.collider != this.hover.collider)
				{
					if (this.hover.isValid)
					{
						DevkitSelectionManager.data.point = raycastHit.point;
						DevkitSelectionManager.endHover(this.hover);
					}
					this.hover.transform = raycastHit.transform;
					this.hover.collider = raycastHit.collider;
					if (this.hover.isValid)
					{
						DevkitSelectionManager.data.point = raycastHit.point;
						DevkitSelectionManager.beginHover(this.hover);
					}
				}
			}
			if (DevkitSelectionManager.selection.Count > 0)
			{
				this.handlePosition = Vector3.zero;
				this.handleRotation = Quaternion.identity;
				bool flag = !DevkitSelectionToolOptions.instance.localSpace;
				foreach (DevkitSelection devkitSelection3 in DevkitSelectionManager.selection)
				{
					if (!(devkitSelection3.gameObject == null))
					{
						this.handlePosition += devkitSelection3.transform.position;
						if (!flag)
						{
							this.handleRotation = devkitSelection3.transform.rotation;
							flag = true;
						}
					}
				}
				this.handlePosition /= (float)DevkitSelectionManager.selection.Count;
				this.positionGameObject.SetActive(this.mode == DevkitSelectionTool.ESelectionMode.POSITION);
				this.positionHandle.suggestTransform(this.handlePosition, this.handleRotation);
				this.rotationGameObject.SetActive(this.mode == DevkitSelectionTool.ESelectionMode.ROTATION);
				this.rotationHandle.suggestTransform(this.handlePosition, this.handleRotation);
				this.scaleGameObject.SetActive(this.mode == DevkitSelectionTool.ESelectionMode.SCALE);
				this.scaleHandle.suggestTransform(this.handlePosition, this.handleRotation);
				if (DevkitInput.canEditorReceiveInput)
				{
					if (Input.GetKeyDown(KeyCode.C))
					{
						this.copyBuffer.Clear();
						foreach (DevkitSelection devkitSelection4 in DevkitSelectionManager.selection)
						{
							this.copyBuffer.Add(devkitSelection4.gameObject);
						}
					}
					if (Input.GetKeyDown(KeyCode.V))
					{
						TranslationReference newReference = new TranslationReference("#SDG::Devkit.Transactions.Paste");
						TranslatedText name = new TranslatedText(newReference);
						DevkitTransactionManager.beginTransaction(name);
						foreach (GameObject gameObject in this.copyBuffer)
						{
							IDevkitSelectionCopyableHandler component = gameObject.GetComponent<IDevkitSelectionCopyableHandler>();
							GameObject gameObject2;
							if (component != null)
							{
								gameObject2 = component.copySelection();
							}
							else
							{
								gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
							}
							IDevkitHierarchyItem component2 = gameObject2.GetComponent<IDevkitHierarchyItem>();
							if (component2 != null)
							{
								component2.instanceID = LevelHierarchy.generateUniqueInstanceID();
							}
							DevkitTransactionUtility.recordInstantiation(gameObject2);
							this.copySelectionDelay.Add(gameObject2);
						}
						DevkitTransactionManager.endTransaction();
					}
					if (Input.GetKeyDown(KeyCode.Delete))
					{
						TranslationReference newReference2 = new TranslationReference("#SDG::Devkit.Transactions.Delete_Selection");
						TranslatedText name2 = new TranslatedText(newReference2);
						DevkitTransactionManager.beginTransaction(name2);
						foreach (DevkitSelection devkitSelection5 in DevkitSelectionManager.selection)
						{
							DevkitTransactionUtility.recordDestruction(devkitSelection5.gameObject);
						}
						DevkitSelectionManager.clear();
						DevkitTransactionManager.endTransaction();
					}
					if (Input.GetKeyDown(KeyCode.B))
					{
						this.referencePosition = this.handlePosition;
						this.referenceRotation = this.handleRotation;
						this.referenceScale = Vector3.one;
						this.hasReferenceScale = false;
						if (DevkitSelectionManager.selection.Count == 1)
						{
							foreach (DevkitSelection devkitSelection6 in DevkitSelectionManager.selection)
							{
								if (!(devkitSelection6.gameObject == null))
								{
									this.referenceScale = devkitSelection6.transform.localScale;
									this.hasReferenceScale = true;
								}
							}
						}
					}
					if (Input.GetKeyDown(KeyCode.N))
					{
						this.moveHandle(this.referencePosition, this.referenceRotation, this.referenceScale, true, this.hasReferenceScale && DevkitSelectionManager.selection.Count == 1);
					}
					if (Input.GetKeyDown(KeyCode.F))
					{
						List<Collider> list = ListPool<Collider>.claim();
						List<Renderer> list2 = ListPool<Renderer>.claim();
						Bounds bounds = new Bounds(this.handlePosition, Vector3.zero);
						foreach (DevkitSelection devkitSelection7 in DevkitSelectionManager.selection)
						{
							if (!(devkitSelection7.gameObject == null))
							{
								list.Clear();
								devkitSelection7.gameObject.GetComponentsInChildren<Collider>(list);
								foreach (Collider collider in list)
								{
									bounds.Encapsulate(collider.bounds);
								}
								list2.Clear();
								devkitSelection7.gameObject.GetComponentsInChildren<Renderer>(list2);
								foreach (Renderer renderer in list2)
								{
									bounds.Encapsulate(renderer.bounds);
								}
							}
						}
						ListPool<Collider>.release(list);
						ListPool<Renderer>.release(list2);
						DevkitNavigation.focus(bounds);
					}
				}
			}
			else
			{
				this.positionGameObject.SetActive(false);
				this.rotationGameObject.SetActive(false);
				this.scaleGameObject.SetActive(false);
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00059B48 File Offset: 0x00057F48
		public virtual void equip()
		{
			GLRenderer.render += this.handleGLRender;
			this.mode = DevkitSelectionTool.ESelectionMode.POSITION;
			this.positionGameObject = new GameObject();
			this.positionGameObject.name = "Position_Handle";
			this.positionGameObject.SetActive(false);
			this.positionHandle = this.positionGameObject.AddComponent<DevkitPositionHandle>();
			this.positionHandle.transformed += this.handlePositionTransformed;
			this.rotationGameObject = new GameObject();
			this.rotationGameObject.name = "Rotation_Handle";
			this.rotationGameObject.SetActive(false);
			this.rotationHandle = this.rotationGameObject.AddComponent<DevkitRotationHandle>();
			this.rotationHandle.transformed += this.handleRotationTransformed;
			this.scaleGameObject = new GameObject();
			this.scaleGameObject.name = "Scale_Handle";
			this.scaleGameObject.SetActive(false);
			this.scaleHandle = this.scaleGameObject.AddComponent<DevkitScaleHandle>();
			this.scaleHandle.transformed += this.handleScaleTransformed;
			DevkitSelectionTool.instance = this;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00059C60 File Offset: 0x00058060
		public virtual void dequip()
		{
			GLRenderer.render -= this.handleGLRender;
			UnityEngine.Object.Destroy(this.positionGameObject);
			UnityEngine.Object.Destroy(this.rotationGameObject);
			UnityEngine.Object.Destroy(this.scaleGameObject);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00059C94 File Offset: 0x00058094
		protected void handleGLRender()
		{
			if (!this.isAreaSelecting)
			{
				return;
			}
			GLUtility.LINE_FLAT_COLOR.SetPass(0);
			GL.Begin(1);
			GL.Color(Color.yellow);
			GLUtility.matrix = MathUtility.IDENTITY_MATRIX;
			Vector3 vector = this.beginAreaSelect;
			vector.z = 16f;
			Vector3 pointerViewportPoint = DevkitInput.pointerViewportPoint;
			pointerViewportPoint.z = 16f;
			Vector3 position = vector;
			position.x = pointerViewportPoint.x;
			Vector3 position2 = pointerViewportPoint;
			position2.x = vector.x;
			Vector3 v = MainCamera.instance.ViewportToWorldPoint(vector);
			Vector3 v2 = MainCamera.instance.ViewportToWorldPoint(position);
			Vector3 v3 = MainCamera.instance.ViewportToWorldPoint(position2);
			Vector3 v4 = MainCamera.instance.ViewportToWorldPoint(pointerViewportPoint);
			GL.Vertex(v);
			GL.Vertex(v2);
			GL.Vertex(v2);
			GL.Vertex(v4);
			GL.Vertex(v4);
			GL.Vertex(v3);
			GL.Vertex(v3);
			GL.Vertex(v);
			GL.End();
		}

		// Token: 0x0400080E RID: 2062
		protected List<GameObject> copyBuffer = new List<GameObject>();

		// Token: 0x0400080F RID: 2063
		protected List<GameObject> copySelectionDelay = new List<GameObject>();

		// Token: 0x04000810 RID: 2064
		protected List<MeshFilter> meshFilters = new List<MeshFilter>();

		// Token: 0x04000811 RID: 2065
		protected DevkitSelectionTool.ESelectionMode mode;

		// Token: 0x04000812 RID: 2066
		protected DevkitSelection hover;

		// Token: 0x04000813 RID: 2067
		protected DevkitSelection drag;

		// Token: 0x04000814 RID: 2068
		protected Vector3 handlePosition;

		// Token: 0x04000815 RID: 2069
		protected Quaternion handleRotation;

		// Token: 0x04000816 RID: 2070
		protected Vector3 referencePosition;

		// Token: 0x04000817 RID: 2071
		protected Quaternion referenceRotation;

		// Token: 0x04000818 RID: 2072
		protected Vector3 referenceScale;

		// Token: 0x04000819 RID: 2073
		protected bool hasReferenceScale;

		// Token: 0x0400081A RID: 2074
		protected GameObject positionGameObject;

		// Token: 0x0400081B RID: 2075
		protected DevkitPositionHandle positionHandle;

		// Token: 0x0400081C RID: 2076
		protected GameObject rotationGameObject;

		// Token: 0x0400081D RID: 2077
		protected DevkitRotationHandle rotationHandle;

		// Token: 0x0400081E RID: 2078
		protected GameObject scaleGameObject;

		// Token: 0x0400081F RID: 2079
		protected DevkitScaleHandle scaleHandle;

		// Token: 0x04000820 RID: 2080
		protected Vector3 beginAreaSelect;

		// Token: 0x04000821 RID: 2081
		protected float beginAreaSelectTime;

		// Token: 0x04000822 RID: 2082
		protected bool isAreaSelecting;

		// Token: 0x04000823 RID: 2083
		protected bool isDragging;

		// Token: 0x04000824 RID: 2084
		protected HashSet<DevkitSelection> areaSelection = new HashSet<DevkitSelection>();

		// Token: 0x02000174 RID: 372
		public enum ESelectionMode
		{
			// Token: 0x04000826 RID: 2086
			POSITION,
			// Token: 0x04000827 RID: 2087
			ROTATION,
			// Token: 0x04000828 RID: 2088
			SCALE
		}
	}
}
