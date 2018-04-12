using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200064E RID: 1614
	public class PlayerWorkzone : PlayerCaller
	{
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002E9D RID: 11933 RVA: 0x0012FF1A File Offset: 0x0012E31A
		// (set) Token: 0x06002E9E RID: 11934 RVA: 0x0012FF24 File Offset: 0x0012E324
		public bool isBuilding
		{
			get
			{
				return this._isBuilding;
			}
			set
			{
				this._isBuilding = value;
				if (!this.isBuilding)
				{
					this.handle.gameObject.SetActive(false);
					this.clearSelection();
				}
				base.player.look.highlightCamera.gameObject.SetActive(this.isBuilding);
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002E9F RID: 11935 RVA: 0x0012FF7A File Offset: 0x0012E37A
		public Vector2 dragStart
		{
			get
			{
				return this._dragStart;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002EA0 RID: 11936 RVA: 0x0012FF82 File Offset: 0x0012E382
		public Vector2 dragEnd
		{
			get
			{
				return this._dragEnd;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x0012FF8A File Offset: 0x0012E38A
		public bool isDragging
		{
			get
			{
				return this._isDragging;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06002EA2 RID: 11938 RVA: 0x0012FF92 File Offset: 0x0012E392
		// (set) Token: 0x06002EA3 RID: 11939 RVA: 0x0012FF9A File Offset: 0x0012E39A
		public EDragType handleType
		{
			get
			{
				return this._handleType;
			}
			set
			{
				this._handleType = value;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06002EA4 RID: 11940 RVA: 0x0012FFA3 File Offset: 0x0012E3A3
		// (set) Token: 0x06002EA5 RID: 11941 RVA: 0x0012FFAC File Offset: 0x0012E3AC
		public EDragMode dragMode
		{
			get
			{
				return this._dragMode;
			}
			set
			{
				this._dragMode = value;
				this.transformHandle.gameObject.SetActive(this.dragMode == EDragMode.TRANSFORM);
				this.planeHandle.gameObject.SetActive(this.dragMode == EDragMode.TRANSFORM);
				this.rotateHandle.gameObject.SetActive(this.dragMode == EDragMode.ROTATE);
				this.calculateHandleOffsets();
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002EA6 RID: 11942 RVA: 0x00130011 File Offset: 0x0012E411
		// (set) Token: 0x06002EA7 RID: 11943 RVA: 0x00130019 File Offset: 0x0012E419
		public EDragCoordinate dragCoordinate
		{
			get
			{
				return this._dragCoordinate;
			}
			set
			{
				this._dragCoordinate = value;
				this.calculateHandleOffsets();
			}
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x00130028 File Offset: 0x0012E428
		public void applySelection()
		{
			for (int i = 0; i < this.selection.Count; i++)
			{
				if (!(this.selection[i].transform == null))
				{
					Vector3 position = this.selection[i].transform.position;
					Transform parent = this.selection[i].transform.parent;
					this.selection[i].transform.position = this.selection[i].point;
					this.selection[i].transform.parent = this.selection[i].parent;
					Vector3 eulerAngles = this.selection[i].transform.rotation.eulerAngles;
					if (this.selection[i].transform.CompareTag("Barricade"))
					{
						BarricadeManager.transformBarricade(this.selection[i].transform, position, eulerAngles.x, eulerAngles.y, eulerAngles.z);
					}
					else if (this.selection[i].transform.CompareTag("Structure"))
					{
						StructureManager.transformStructure(this.selection[i].transform, position, eulerAngles.x, eulerAngles.y, eulerAngles.z);
					}
					this.selection[i].transform.parent = parent;
					this.selection[i].transform.position = position;
				}
			}
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x001301DC File Offset: 0x0012E5DC
		public void pointSelection()
		{
			for (int i = 0; i < this.selection.Count; i++)
			{
				if (!(this.selection[i].transform == null))
				{
					this.selection[i].point = this.selection[i].transform.position;
				}
			}
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x0013024D File Offset: 0x0012E64D
		public void addSelection(Transform select)
		{
			HighlighterTool.highlight(select, Color.yellow);
			this.selection.Add(new WorkzoneSelection(select, select.parent));
			this.calculateHandleOffsets();
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x00130278 File Offset: 0x0012E678
		public void removeSelection(Transform select)
		{
			for (int i = 0; i < this.selection.Count; i++)
			{
				if (this.selection[i].transform == select)
				{
					HighlighterTool.unhighlight(select);
					this.selection[i].transform.parent = this.selection[i].parent;
					this.selection.RemoveAt(i);
					break;
				}
			}
			this.calculateHandleOffsets();
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x00130304 File Offset: 0x0012E704
		private void clearSelection()
		{
			for (int i = 0; i < this.selection.Count; i++)
			{
				if (this.selection[i].transform != null)
				{
					HighlighterTool.unhighlight(this.selection[i].transform);
					this.selection[i].transform.parent = this.selection[i].parent;
				}
			}
			this.selection.Clear();
			this.calculateHandleOffsets();
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x00130398 File Offset: 0x0012E798
		public bool containsSelection(Transform select)
		{
			for (int i = 0; i < this.selection.Count; i++)
			{
				if (this.selection[i].transform == select)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x001303E0 File Offset: 0x0012E7E0
		private void calculateHandleOffsets()
		{
			if (this.selection.Count == 0)
			{
				this.handle.rotation = Quaternion.identity;
				this.handle.gameObject.SetActive(false);
				return;
			}
			for (int i = 0; i < this.selection.Count; i++)
			{
				if (!(this.selection[i].transform == null))
				{
					this.selection[i].transform.parent = null;
				}
			}
			if (this.dragCoordinate == EDragCoordinate.GLOBAL)
			{
				this.handle.position = Vector3.zero;
				for (int j = 0; j < this.selection.Count; j++)
				{
					if (!(this.selection[j].transform == null))
					{
						this.handle.position += this.selection[j].transform.position;
					}
				}
				this.handle.position /= (float)this.selection.Count;
				this.handle.rotation = Quaternion.identity;
			}
			else
			{
				for (int k = 0; k < this.selection.Count; k++)
				{
					if (!(this.selection[k].transform == null))
					{
						this.handle.position = this.selection[k].transform.position;
						this.handle.rotation = this.selection[k].transform.rotation;
						break;
					}
				}
			}
			this.handle.gameObject.SetActive(true);
			this.updateGroup();
			for (int l = 0; l < this.selection.Count; l++)
			{
				if (!(this.selection[l].transform == null))
				{
					this.selection[l].transform.parent = this.group;
				}
			}
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x00130622 File Offset: 0x0012EA22
		private void updateGroup()
		{
			this.group.position = this.handle.transform.position;
			this.group.rotation = this.handle.transform.rotation;
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x0013065C File Offset: 0x0012EA5C
		private void transformGroup(Vector3 normal, Vector3 dir)
		{
			Vector3 b = MainCamera.instance.WorldToScreenPoint(this.transformOrigin);
			Vector3 vector = MainCamera.instance.WorldToScreenPoint(this.transformOrigin + normal) - b;
			Vector3 lhs = Input.mousePosition - this.mouseOrigin;
			float num = Vector3.Dot(lhs, vector.normalized) / vector.magnitude;
			if (Input.GetKey(ControlsSettings.snap))
			{
				num = (float)((int)(num / this.snapTransform)) * this.snapTransform;
			}
			Vector3 position = this.transformOrigin + num * normal;
			position.x = Mathf.Clamp(position.x, (float)(-(float)Level.size), (float)Level.size);
			position.y = Mathf.Clamp(position.y, 0f, Level.HEIGHT);
			position.z = Mathf.Clamp(position.z, (float)(-(float)Level.size), (float)Level.size);
			this.handle.position = position;
			this.updateGroup();
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x00130764 File Offset: 0x0012EB64
		private void planeGroup(Vector3 normal)
		{
			this.handlePlane.SetNormalAndPosition(normal, this.transformOrigin);
			float d;
			this.handlePlane.Raycast(this.ray, out d);
			Vector3 position = this.ray.origin + this.ray.direction * d - this.handleOffset + Vector3.Project(this.handleOffset, normal);
			if (Input.GetKey(ControlsSettings.snap))
			{
				position.x = (float)((int)(position.x / this.snapTransform)) * this.snapTransform;
				position.y = (float)((int)(position.y / this.snapTransform)) * this.snapTransform;
				position.z = (float)((int)(position.z / this.snapTransform)) * this.snapTransform;
			}
			position.x = Mathf.Clamp(position.x, (float)(-(float)Level.size), (float)Level.size);
			position.y = Mathf.Clamp(position.y, 0f, Level.HEIGHT);
			position.z = Mathf.Clamp(position.z, (float)(-(float)Level.size), (float)Level.size);
			this.handle.position = position;
			this.updateGroup();
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x001308B0 File Offset: 0x0012ECB0
		private void rotateGroup(Vector3 normal, Vector3 axis)
		{
			Vector3 vector = axis * (Input.mousePosition.x - this.mouseOrigin.x) * (float)((!this.rotateInverted) ? 1 : -1);
			float num = vector.x + vector.y + vector.z;
			if (Input.GetKey(ControlsSettings.snap))
			{
				num = (float)((int)(num / this.snapRotation)) * this.snapRotation;
			}
			num = (float)(Mathf.RoundToInt(num / 2f) * 2);
			if (Vector3.Dot(MainCamera.instance.transform.forward, normal) < 0f)
			{
				this.handle.rotation = this.rotateOrigin * Quaternion.Euler(axis * num);
			}
			else
			{
				this.handle.rotation = this.rotateOrigin * Quaternion.Euler(-axis * num);
			}
			this.updateGroup();
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x001309B4 File Offset: 0x0012EDB4
		private void Update()
		{
			if (!this.isBuilding)
			{
				return;
			}
			this.ray = MainCamera.instance.ScreenPointToRay(Input.mousePosition);
			Physics.Raycast(this.ray, out this.worldHit, 256f, RayMasks.EDITOR_WORLD);
			Physics.Raycast(this.ray, out this.buildableHit, 256f, RayMasks.EDITOR_BUILDABLE);
			Physics.Raycast(this.ray, out this.logicHit, 256f, RayMasks.VIEWMODEL);
			if (GUIUtility.hotControl == 0)
			{
				if (Input.GetKey(ControlsSettings.secondary))
				{
					this.handleType = EDragType.NONE;
					if (this.isDragging)
					{
						this._dragStart = Vector2.zero;
						this._dragEnd = Vector2.zero;
						this._isDragging = false;
						if (this.onDragStopped != null)
						{
							this.onDragStopped();
						}
						this.clearSelection();
					}
					return;
				}
				if (this.handleType != EDragType.NONE)
				{
					if (!Input.GetKey(ControlsSettings.primary))
					{
						this.applySelection();
						this.handleType = EDragType.NONE;
					}
					else
					{
						if (this.handleType == EDragType.TRANSFORM_X)
						{
							this.transformGroup(this.handle.right, this.handle.up);
						}
						else if (this.handleType == EDragType.TRANSFORM_Y)
						{
							this.transformGroup(this.handle.up, this.handle.right);
						}
						else if (this.handleType == EDragType.TRANSFORM_Z)
						{
							this.transformGroup(this.handle.forward, this.handle.up);
						}
						else if (this.handleType == EDragType.PLANE_X)
						{
							this.planeGroup(this.handle.right);
						}
						else if (this.handleType == EDragType.PLANE_Y)
						{
							this.planeGroup(this.handle.up);
						}
						else if (this.handleType == EDragType.PLANE_Z)
						{
							this.planeGroup(this.handle.forward);
						}
						if (this.handleType == EDragType.ROTATION_X)
						{
							this.rotateGroup(this.handle.right, Vector3.right);
						}
						else if (this.handleType == EDragType.ROTATION_Y)
						{
							this.rotateGroup(this.handle.up, Vector3.up);
						}
						else if (this.handleType == EDragType.ROTATION_Z)
						{
							this.rotateGroup(this.handle.forward, Vector3.forward);
						}
					}
				}
				if (Input.GetKeyDown(ControlsSettings.tool_0))
				{
					this.dragMode = EDragMode.TRANSFORM;
				}
				if (Input.GetKeyDown(ControlsSettings.tool_1))
				{
					this.dragMode = EDragMode.ROTATE;
				}
				if (Input.GetKeyDown(KeyCode.B) && this.selection.Count > 0 && Input.GetKey(KeyCode.LeftControl))
				{
					this.copyPosition = this.handle.position;
					this.copyRotation = this.handle.rotation;
				}
				if (Input.GetKeyDown(KeyCode.N) && this.selection.Count > 0 && this.copyPosition != Vector3.zero && Input.GetKey(KeyCode.LeftControl))
				{
					this.pointSelection();
					this.handle.position = this.copyPosition;
					this.handle.rotation = this.copyRotation;
					this.updateGroup();
					this.applySelection();
				}
				if (this.handleType == EDragType.NONE)
				{
					if (Input.GetKeyDown(ControlsSettings.primary))
					{
						if (this.logicHit.transform != null && (this.logicHit.transform.name == "Arrow_X" || this.logicHit.transform.name == "Arrow_Y" || this.logicHit.transform.name == "Arrow_Z" || this.logicHit.transform.name == "Plane_X" || this.logicHit.transform.name == "Plane_Y" || this.logicHit.transform.name == "Plane_Z" || this.logicHit.transform.name == "Circle_X" || this.logicHit.transform.name == "Circle_Y" || this.logicHit.transform.name == "Circle_Z"))
						{
							this.mouseOrigin = Input.mousePosition;
							this.transformOrigin = this.handle.position;
							this.rotateOrigin = this.handle.rotation;
							this.handleOffset = this.logicHit.point - this.handle.position;
							this.pointSelection();
							if (this.logicHit.transform.name == "Arrow_X")
							{
								this.handleType = EDragType.TRANSFORM_X;
							}
							else if (this.logicHit.transform.name == "Arrow_Y")
							{
								this.handleType = EDragType.TRANSFORM_Y;
							}
							else if (this.logicHit.transform.name == "Arrow_Z")
							{
								this.handleType = EDragType.TRANSFORM_Z;
							}
							else if (this.logicHit.transform.name == "Plane_X")
							{
								this.handleType = EDragType.PLANE_X;
							}
							else if (this.logicHit.transform.name == "Plane_Y")
							{
								this.handleType = EDragType.PLANE_Y;
							}
							else if (this.logicHit.transform.name == "Plane_Z")
							{
								this.handleType = EDragType.PLANE_Z;
							}
							else if (this.logicHit.transform.name == "Circle_X")
							{
								this.rotateInverted = (Vector3.Dot(this.logicHit.point - this.handle.position, MainCamera.instance.transform.up) < 0f);
								this.handleType = EDragType.ROTATION_X;
							}
							else if (this.logicHit.transform.name == "Circle_Y")
							{
								this.rotateInverted = (Vector3.Dot(this.logicHit.point - this.handle.position, MainCamera.instance.transform.up) < 0f);
								this.handleType = EDragType.ROTATION_Y;
							}
							else if (this.logicHit.transform.name == "Circle_Z")
							{
								this.rotateInverted = (Vector3.Dot(this.logicHit.point - this.handle.position, MainCamera.instance.transform.up) < 0f);
								this.handleType = EDragType.ROTATION_Z;
							}
						}
						else
						{
							Transform transform = this.buildableHit.transform;
							if (transform != null && (transform.CompareTag("Barricade") || transform.CompareTag("Structure")))
							{
								InteractableDoorHinge component = transform.GetComponent<InteractableDoorHinge>();
								if (component != null)
								{
									transform = component.transform.parent.parent;
								}
								if (Input.GetKey(ControlsSettings.modify))
								{
									if (this.containsSelection(transform))
									{
										this.removeSelection(transform);
									}
									else
									{
										this.addSelection(transform);
									}
								}
								else if (this.containsSelection(transform))
								{
									this.clearSelection();
								}
								else
								{
									this.clearSelection();
									this.addSelection(transform);
								}
							}
							else
							{
								if (!this.isDragging)
								{
									this._dragStart.x = PlayerUI.window.mouse_x;
									this._dragStart.y = PlayerUI.window.mouse_y;
								}
								if (!Input.GetKey(ControlsSettings.modify))
								{
									this.clearSelection();
								}
							}
						}
					}
					else if (Input.GetKey(ControlsSettings.primary) && this.dragStart.x != 0f)
					{
						this._dragEnd.x = PlayerUI.window.mouse_x;
						this._dragEnd.y = PlayerUI.window.mouse_y;
						if (this.isDragging || Mathf.Abs(this.dragEnd.x - this.dragStart.x) > 50f || Mathf.Abs(this.dragEnd.x - this.dragStart.x) > 50f)
						{
							int num = (int)this.dragStart.x;
							int num2 = (int)this.dragStart.y;
							if (this.dragEnd.x < this.dragStart.x)
							{
								num = (int)this.dragEnd.x;
							}
							if (this.dragEnd.y < this.dragStart.y)
							{
								num2 = (int)this.dragEnd.y;
							}
							int num3 = (int)this.dragEnd.x;
							int num4 = (int)this.dragEnd.y;
							if (this.dragStart.x > this.dragEnd.x)
							{
								num3 = (int)this.dragStart.x;
							}
							if (this.dragStart.y > this.dragEnd.y)
							{
								num4 = (int)this.dragStart.y;
							}
							if (this.onDragStarted != null)
							{
								this.onDragStarted(num, num2, num3, num4);
							}
							if (!this.isDragging)
							{
								this._isDragging = true;
								this.dragable.Clear();
								byte region_x = Player.player.movement.region_x;
								byte region_y = Player.player.movement.region_y;
								if (Regions.checkSafe((int)region_x, (int)region_y))
								{
									for (int i = 0; i < BarricadeManager.plants.Count; i++)
									{
										BarricadeRegion barricadeRegion = BarricadeManager.plants[i];
										for (int j = 0; j < barricadeRegion.drops.Count; j++)
										{
											BarricadeDrop barricadeDrop = barricadeRegion.drops[j];
											if (!(barricadeDrop.model == null))
											{
												Vector3 newScreen = MainCamera.instance.WorldToScreenPoint(barricadeDrop.model.position);
												if (newScreen.z >= 0f)
												{
													newScreen.y = (float)Screen.height - newScreen.y;
													this.dragable.Add(new EditorDrag(barricadeDrop.model, newScreen));
												}
											}
										}
									}
									for (int k = (int)(region_x - 1); k <= (int)(region_x + 1); k++)
									{
										for (int l = (int)(region_y - 1); l <= (int)(region_y + 1); l++)
										{
											if (Regions.checkSafe((int)((byte)k), (int)((byte)l)))
											{
												for (int m = 0; m < BarricadeManager.regions[k, l].drops.Count; m++)
												{
													BarricadeDrop barricadeDrop2 = BarricadeManager.regions[k, l].drops[m];
													if (!(barricadeDrop2.model == null))
													{
														Vector3 newScreen2 = MainCamera.instance.WorldToScreenPoint(barricadeDrop2.model.position);
														if (newScreen2.z >= 0f)
														{
															newScreen2.y = (float)Screen.height - newScreen2.y;
															this.dragable.Add(new EditorDrag(barricadeDrop2.model, newScreen2));
														}
													}
												}
												for (int n = 0; n < StructureManager.regions[k, l].drops.Count; n++)
												{
													StructureDrop structureDrop = StructureManager.regions[k, l].drops[n];
													if (structureDrop != null)
													{
														Vector3 newScreen3 = MainCamera.instance.WorldToScreenPoint(structureDrop.model.position);
														if (newScreen3.z >= 0f)
														{
															newScreen3.y = (float)Screen.height - newScreen3.y;
															this.dragable.Add(new EditorDrag(structureDrop.model, newScreen3));
														}
													}
												}
											}
										}
									}
								}
							}
							if (!Input.GetKey(ControlsSettings.modify))
							{
								for (int num5 = 0; num5 < this.selection.Count; num5++)
								{
									if (!(this.selection[num5].transform == null))
									{
										Vector3 vector = MainCamera.instance.WorldToScreenPoint(this.selection[num5].transform.position);
										if (vector.z < 0f)
										{
											this.removeSelection(this.selection[num5].transform);
										}
										else
										{
											vector.y = (float)Screen.height - vector.y;
											if (vector.x < (float)num || vector.y < (float)num2 || vector.x > (float)num3 || vector.y > (float)num4)
											{
												this.removeSelection(this.selection[num5].transform);
											}
										}
									}
								}
							}
							for (int num6 = 0; num6 < this.dragable.Count; num6++)
							{
								EditorDrag editorDrag = this.dragable[num6];
								if (!(editorDrag.transform == null))
								{
									if (!(editorDrag.transform.parent == this.group))
									{
										if (editorDrag.screen.x >= (float)num && editorDrag.screen.y >= (float)num2 && editorDrag.screen.x <= (float)num3 && editorDrag.screen.y <= (float)num4)
										{
											this.addSelection(editorDrag.transform);
										}
									}
								}
							}
						}
					}
					if (this.selection.Count > 0 && Input.GetKeyDown(ControlsSettings.tool_2) && this.worldHit.transform != null)
					{
						this.pointSelection();
						this.handle.position = this.worldHit.point;
						if (Input.GetKey(ControlsSettings.snap))
						{
							this.handle.position += this.worldHit.normal * this.snapTransform;
						}
						this.updateGroup();
						this.applySelection();
					}
				}
			}
			if (Input.GetKeyUp(ControlsSettings.primary) && this.dragStart.x != 0f)
			{
				this._dragStart = Vector2.zero;
				if (this.isDragging)
				{
					this._dragEnd = Vector2.zero;
					this._isDragging = false;
					if (this.onDragStopped != null)
					{
						this.onDragStopped();
					}
				}
			}
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x00131990 File Offset: 0x0012FD90
		private void LateUpdate()
		{
			if (this.selection.Count > 0)
			{
				float magnitude = (this.handle.position - MainCamera.instance.transform.position).magnitude;
				this.handle.localScale = new Vector3(0.1f * magnitude, 0.1f * magnitude, 0.1f * magnitude);
				if (this.dragMode == EDragMode.TRANSFORM || this.dragMode == EDragMode.SCALE)
				{
					float num = Vector3.Dot(MainCamera.instance.transform.position - this.handle.transform.position, this.handle.transform.right);
					float num2 = Vector3.Dot(MainCamera.instance.transform.position - this.handle.transform.position, this.handle.transform.up);
					float num3 = Vector3.Dot(MainCamera.instance.transform.position - this.handle.transform.position, this.handle.transform.forward);
					this.transformHandle.localScale = new Vector3((num >= 0f) ? 1f : -1f, (num2 >= 0f) ? 1f : -1f, (num3 >= 0f) ? 1f : -1f);
					this.planeHandle.localScale = this.transformHandle.localScale;
				}
			}
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x00131B38 File Offset: 0x0012FF38
		private void Start()
		{
			this._isBuilding = false;
			this._dragStart = Vector2.zero;
			this._dragEnd = Vector2.zero;
			this._isDragging = false;
			this.selection = new List<WorkzoneSelection>();
			this.handlePlane = default(Plane);
			this.group = new GameObject().transform;
			this.group.name = "Group";
			this.group.parent = Level.editing;
			this.handle = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Handles"))).transform;
			this.handle.name = "Handle";
			this.handle.gameObject.SetActive(false);
			this.handle.parent = Level.editing;
			Layerer.relayer(this.handle, LayerMasks.VIEWMODEL);
			this.transformHandle = this.handle.FindChild("Transform");
			this.planeHandle = this.handle.FindChild("Plane");
			this.rotateHandle = this.handle.FindChild("Rotate");
			this.dragMode = EDragMode.TRANSFORM;
			this.dragCoordinate = EDragCoordinate.GLOBAL;
			this.dragable = new List<EditorDrag>();
			this.snapTransform = 1f;
			this.snapRotation = 15f;
		}

		// Token: 0x04001E21 RID: 7713
		public DragStarted onDragStarted;

		// Token: 0x04001E22 RID: 7714
		public DragStopped onDragStopped;

		// Token: 0x04001E23 RID: 7715
		public float snapTransform;

		// Token: 0x04001E24 RID: 7716
		public float snapRotation;

		// Token: 0x04001E25 RID: 7717
		private bool _isBuilding;

		// Token: 0x04001E26 RID: 7718
		private Ray ray;

		// Token: 0x04001E27 RID: 7719
		private RaycastHit worldHit;

		// Token: 0x04001E28 RID: 7720
		private RaycastHit buildableHit;

		// Token: 0x04001E29 RID: 7721
		private RaycastHit logicHit;

		// Token: 0x04001E2A RID: 7722
		private Vector2 _dragStart;

		// Token: 0x04001E2B RID: 7723
		private Vector2 _dragEnd;

		// Token: 0x04001E2C RID: 7724
		private bool _isDragging;

		// Token: 0x04001E2D RID: 7725
		private List<WorkzoneSelection> selection;

		// Token: 0x04001E2E RID: 7726
		private Vector3 copyPosition;

		// Token: 0x04001E2F RID: 7727
		private Quaternion copyRotation;

		// Token: 0x04001E30 RID: 7728
		private Transform group;

		// Token: 0x04001E31 RID: 7729
		private Transform handle;

		// Token: 0x04001E32 RID: 7730
		private Transform transformHandle;

		// Token: 0x04001E33 RID: 7731
		private Transform planeHandle;

		// Token: 0x04001E34 RID: 7732
		private Transform rotateHandle;

		// Token: 0x04001E35 RID: 7733
		private Vector3 handleOffset;

		// Token: 0x04001E36 RID: 7734
		private Plane handlePlane;

		// Token: 0x04001E37 RID: 7735
		private EDragType _handleType;

		// Token: 0x04001E38 RID: 7736
		private EDragMode _dragMode;

		// Token: 0x04001E39 RID: 7737
		private EDragCoordinate _dragCoordinate;

		// Token: 0x04001E3A RID: 7738
		private Vector3 transformOrigin;

		// Token: 0x04001E3B RID: 7739
		private Quaternion rotateOrigin;

		// Token: 0x04001E3C RID: 7740
		private Vector3 mouseOrigin;

		// Token: 0x04001E3D RID: 7741
		private bool rotateInverted;

		// Token: 0x04001E3E RID: 7742
		private List<EditorDrag> dragable;
	}
}
