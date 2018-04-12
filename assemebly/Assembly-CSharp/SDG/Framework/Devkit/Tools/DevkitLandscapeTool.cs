using System;
using System.Collections.Generic;
using SDG.Framework.Devkit.Transactions;
using SDG.Framework.Landscapes;
using SDG.Framework.Rendering;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using SDG.Framework.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit.Tools
{
	// Token: 0x0200015F RID: 351
	public class DevkitLandscapeTool : IDevkitTool
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00052E42 File Offset: 0x00051242
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x00052E4C File Offset: 0x0005124C
		public static DevkitLandscapeTool.EDevkitLandscapeToolMode toolMode
		{
			get
			{
				return DevkitLandscapeTool._toolMode;
			}
			set
			{
				if (DevkitLandscapeTool.toolMode == value)
				{
					return;
				}
				DevkitLandscapeTool.EDevkitLandscapeToolMode toolMode = DevkitLandscapeTool.toolMode;
				DevkitLandscapeTool._toolMode = value;
				if (DevkitLandscapeTool.toolModeChanged != null)
				{
					DevkitLandscapeTool.toolModeChanged(toolMode, DevkitLandscapeTool.toolMode);
				}
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000A7D RID: 2685 RVA: 0x00052E8C File Offset: 0x0005128C
		// (remove) Token: 0x06000A7E RID: 2686 RVA: 0x00052EC0 File Offset: 0x000512C0
		public static event DevkitLandscapeTool.DevkitLandscapeToolModeChangedHandler toolModeChanged;

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00052EF4 File Offset: 0x000512F4
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x00052EFC File Offset: 0x000512FC
		public static LandscapeTile selectedTile
		{
			get
			{
				return DevkitLandscapeTool._selectedTile;
			}
			set
			{
				if (DevkitLandscapeTool.selectedTile == value)
				{
					return;
				}
				LandscapeTile selectedTile = DevkitLandscapeTool.selectedTile;
				DevkitLandscapeTool._selectedTile = value;
				if (DevkitLandscapeTool.selectedTileChanged != null)
				{
					DevkitLandscapeTool.selectedTileChanged(selectedTile, DevkitLandscapeTool.selectedTile);
				}
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000A81 RID: 2689 RVA: 0x00052F3C File Offset: 0x0005133C
		// (remove) Token: 0x06000A82 RID: 2690 RVA: 0x00052F70 File Offset: 0x00051370
		public static event DevkitLandscapeTool.DevkitLandscapeToolSelectedTileChangedHandler selectedTileChanged;

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00052FA4 File Offset: 0x000513A4
		public virtual float heightmapAdjustSensitivity
		{
			get
			{
				return DevkitLandscapeToolHeightmapOptions.adjustSensitivity;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00052FAB File Offset: 0x000513AB
		public virtual float heightmapFlattenSensitivity
		{
			get
			{
				return DevkitLandscapeToolHeightmapOptions.flattenSensitivity;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00052FB2 File Offset: 0x000513B2
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x00052FBE File Offset: 0x000513BE
		public virtual float heightmapBrushRadius
		{
			get
			{
				return DevkitLandscapeToolHeightmapOptions.instance.brushRadius;
			}
			set
			{
				DevkitLandscapeToolHeightmapOptions.instance.brushRadius = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00052FCB File Offset: 0x000513CB
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x00052FD7 File Offset: 0x000513D7
		public virtual float heightmapBrushFalloff
		{
			get
			{
				return DevkitLandscapeToolHeightmapOptions.instance.brushFalloff;
			}
			set
			{
				DevkitLandscapeToolHeightmapOptions.instance.brushFalloff = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x00052FE4 File Offset: 0x000513E4
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x00052FF0 File Offset: 0x000513F0
		public virtual float heightmapBrushStrength
		{
			get
			{
				return DevkitLandscapeToolHeightmapOptions.instance.brushStrength;
			}
			set
			{
				DevkitLandscapeToolHeightmapOptions.instance.brushStrength = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00052FFD File Offset: 0x000513FD
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x00053009 File Offset: 0x00051409
		public virtual float heightmapFlattenTarget
		{
			get
			{
				return DevkitLandscapeToolHeightmapOptions.instance.flattenTarget;
			}
			set
			{
				DevkitLandscapeToolHeightmapOptions.instance.flattenTarget = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00053016 File Offset: 0x00051416
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x00053022 File Offset: 0x00051422
		public virtual uint heightmapMaxPreviewSamples
		{
			get
			{
				return DevkitLandscapeToolHeightmapOptions.instance.maxPreviewSamples;
			}
			set
			{
				DevkitLandscapeToolHeightmapOptions.instance.maxPreviewSamples = value;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0005302F File Offset: 0x0005142F
		public virtual float splatmapPaintSensitivity
		{
			get
			{
				return DevkitLandscapeToolSplatmapOptions.paintSensitivity;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x00053036 File Offset: 0x00051436
		// (set) Token: 0x06000A91 RID: 2705 RVA: 0x00053042 File Offset: 0x00051442
		public virtual float splatmapBrushRadius
		{
			get
			{
				return DevkitLandscapeToolSplatmapOptions.instance.brushRadius;
			}
			set
			{
				DevkitLandscapeToolSplatmapOptions.instance.brushRadius = value;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0005304F File Offset: 0x0005144F
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x0005305B File Offset: 0x0005145B
		public virtual float splatmapBrushFalloff
		{
			get
			{
				return DevkitLandscapeToolSplatmapOptions.instance.brushFalloff;
			}
			set
			{
				DevkitLandscapeToolSplatmapOptions.instance.brushFalloff = value;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x00053068 File Offset: 0x00051468
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x00053074 File Offset: 0x00051474
		public virtual float splatmapBrushStrength
		{
			get
			{
				return DevkitLandscapeToolSplatmapOptions.instance.brushStrength;
			}
			set
			{
				DevkitLandscapeToolSplatmapOptions.instance.brushStrength = value;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x00053081 File Offset: 0x00051481
		// (set) Token: 0x06000A97 RID: 2711 RVA: 0x0005308D File Offset: 0x0005148D
		public virtual bool splatmapUseWeightTarget
		{
			get
			{
				return DevkitLandscapeToolSplatmapOptions.instance.useWeightTarget;
			}
			set
			{
				DevkitLandscapeToolSplatmapOptions.instance.useWeightTarget = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0005309A File Offset: 0x0005149A
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x000530A6 File Offset: 0x000514A6
		public virtual float splatmapWeightTarget
		{
			get
			{
				return DevkitLandscapeToolSplatmapOptions.instance.weightTarget;
			}
			set
			{
				DevkitLandscapeToolSplatmapOptions.instance.weightTarget = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x000530B3 File Offset: 0x000514B3
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x000530BF File Offset: 0x000514BF
		public virtual uint splatmapMaxPreviewSamples
		{
			get
			{
				return DevkitLandscapeToolSplatmapOptions.instance.maxPreviewSamples;
			}
			set
			{
				DevkitLandscapeToolSplatmapOptions.instance.maxPreviewSamples = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x000530CC File Offset: 0x000514CC
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x000530D3 File Offset: 0x000514D3
		public static AssetReference<LandscapeMaterialAsset> splatmapMaterialTarget
		{
			get
			{
				return DevkitLandscapeTool._splatmapMaterialTarget;
			}
			set
			{
				DevkitLandscapeTool._splatmapMaterialTarget = value;
				DevkitLandscapeTool.splatmapMaterialTargetAsset = Assets.find<LandscapeMaterialAsset>(DevkitLandscapeTool.splatmapMaterialTarget);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x000530EA File Offset: 0x000514EA
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x00053103 File Offset: 0x00051503
		protected virtual float brushRadius
		{
			get
			{
				if (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.HEIGHTMAP)
				{
					return this.heightmapBrushRadius;
				}
				return this.splatmapBrushRadius;
			}
			set
			{
				if (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.HEIGHTMAP)
				{
					this.heightmapBrushRadius = value;
				}
				else
				{
					this.splatmapBrushRadius = value;
				}
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x00053122 File Offset: 0x00051522
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x0005313B File Offset: 0x0005153B
		protected virtual float brushFalloff
		{
			get
			{
				if (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.HEIGHTMAP)
				{
					return this.heightmapBrushFalloff;
				}
				return this.splatmapBrushFalloff;
			}
			set
			{
				if (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.HEIGHTMAP)
				{
					this.heightmapBrushFalloff = value;
				}
				else
				{
					this.splatmapBrushFalloff = value;
				}
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0005315A File Offset: 0x0005155A
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x00053173 File Offset: 0x00051573
		protected virtual float brushStrength
		{
			get
			{
				if (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.HEIGHTMAP)
				{
					return this.heightmapBrushStrength;
				}
				return this.splatmapBrushStrength;
			}
			set
			{
				if (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.HEIGHTMAP)
				{
					this.heightmapBrushStrength = value;
				}
				else
				{
					this.splatmapBrushStrength = value;
				}
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00053192 File Offset: 0x00051592
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x000531AB File Offset: 0x000515AB
		protected virtual uint maxPreviewSamples
		{
			get
			{
				if (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.HEIGHTMAP)
				{
					return this.heightmapMaxPreviewSamples;
				}
				return this.splatmapMaxPreviewSamples;
			}
			set
			{
				if (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.HEIGHTMAP)
				{
					this.heightmapMaxPreviewSamples = value;
				}
				else
				{
					this.splatmapMaxPreviewSamples = value;
				}
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x000531CA File Offset: 0x000515CA
		protected virtual bool isChangingBrush
		{
			get
			{
				return this.isChangingBrushRadius || this.isChangingBrushFalloff || this.isChangingBrushStrength || this.isChangingWeightTarget;
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000531F6 File Offset: 0x000515F6
		protected virtual void beginChangeHotkeyTransaction()
		{
			DevkitTransactionUtility.beginGenericTransaction();
			DevkitTransactionUtility.recordObjectDelta(DevkitLandscapeToolHeightmapOptions.instance);
			DevkitTransactionUtility.recordObjectDelta(DevkitLandscapeToolSplatmapOptions.instance);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00053211 File Offset: 0x00051611
		protected virtual void endChangeHotkeyTransaction()
		{
			DevkitTransactionUtility.endGenericTransaction();
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00053218 File Offset: 0x00051618
		public virtual void update()
		{
			Ray pointerToWorldRay = DevkitInput.pointerToWorldRay;
			Plane plane = default(Plane);
			plane.SetNormalAndPosition(Vector3.up, Vector3.zero);
			float num;
			this.isPointerOnTilePlane = plane.Raycast(pointerToWorldRay, out num);
			this.tilePlanePosition = pointerToWorldRay.origin + pointerToWorldRay.direction * num;
			this.pointerTileCoord = new LandscapeCoord(this.tilePlanePosition);
			this.isTileVisible = this.isPointerOnTilePlane;
			this.previewSamples.Clear();
			RaycastHit raycastHit;
			this.isPointerOnLandscape = Physics.Raycast(pointerToWorldRay, out raycastHit, 8192f, RayMasks.GROUND | RayMasks.GROUND2);
			this.pointerWorldPosition = raycastHit.point;
			if (!DevkitNavigation.isNavigating && DevkitInput.canEditorReceiveInput)
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
				if (Input.GetKeyDown(KeyCode.G))
				{
					this.isChangingWeightTarget = true;
					this.beginChangeHotkeyTransaction();
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
			if (Input.GetKeyUp(KeyCode.G))
			{
				this.isChangingWeightTarget = false;
				this.endChangeHotkeyTransaction();
			}
			if (this.isChangingBrush)
			{
				Plane plane2 = default(Plane);
				plane2.SetNormalAndPosition(Vector3.up, this.brushWorldPosition);
				float d;
				plane2.Raycast(pointerToWorldRay, out d);
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
				if (this.isChangingWeightTarget)
				{
					this.splatmapWeightTarget = Mathf.Clamp01((this.changePlanePosition - this.brushWorldPosition).magnitude / this.brushRadius);
				}
			}
			else
			{
				this.brushWorldPosition = this.pointerWorldPosition;
				if (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.HEIGHTMAP && DevkitLandscapeTool.heightmapMode == DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode.FLATTEN)
				{
					Plane plane3 = default(Plane);
					plane3.SetNormalAndPosition(Vector3.up, new Vector3(0f, this.heightmapFlattenTarget, 0f));
					float d2;
					if (plane3.Raycast(pointerToWorldRay, out d2))
					{
						this.flattenPlanePosition = pointerToWorldRay.origin + pointerToWorldRay.direction * d2;
						this.brushWorldPosition = this.flattenPlanePosition;
						if (!this.isPointerOnLandscape)
						{
							this.isPointerOnLandscape = Landscape.isPointerInTile(this.brushWorldPosition);
						}
					}
					else
					{
						this.flattenPlanePosition = new Vector3(this.brushWorldPosition.x, this.heightmapFlattenTarget, this.brushWorldPosition.z);
					}
				}
			}
			this.isBrushVisible = (this.isPointerOnLandscape || this.isChangingBrush);
			if (!DevkitNavigation.isNavigating && DevkitInput.canEditorReceiveInput)
			{
				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					DevkitLandscapeTool.toolMode = DevkitLandscapeTool.EDevkitLandscapeToolMode.HEIGHTMAP;
				}
				if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					DevkitLandscapeTool.toolMode = DevkitLandscapeTool.EDevkitLandscapeToolMode.SPLATMAP;
				}
				if (Input.GetKeyDown(KeyCode.Alpha3))
				{
					DevkitLandscapeTool.toolMode = DevkitLandscapeTool.EDevkitLandscapeToolMode.TILE;
				}
				if (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.TILE)
				{
					if (Input.GetKeyDown(KeyCode.Mouse0))
					{
						if (this.isPointerOnTilePlane)
						{
							LandscapeTile tile = Landscape.getTile(this.pointerTileCoord);
							if (tile == null)
							{
								if (num < 4096f)
								{
									LandscapeTile landscapeTile = Landscape.addTile(this.pointerTileCoord);
									if (landscapeTile != null)
									{
										landscapeTile.readHeightmaps();
										landscapeTile.readSplatmaps();
										Landscape.linkNeighbors();
										Landscape.reconcileNeighbors(landscapeTile);
										Landscape.applyLOD();
									}
									DevkitLandscapeTool.selectedTile = landscapeTile;
								}
								else
								{
									DevkitLandscapeTool.selectedTile = null;
								}
							}
							else if (DevkitLandscapeTool.selectedTile != null && DevkitLandscapeTool.selectedTile.coord == this.pointerTileCoord)
							{
								DevkitLandscapeTool.selectedTile = null;
							}
							else
							{
								DevkitLandscapeTool.selectedTile = tile;
							}
						}
						else
						{
							DevkitLandscapeTool.selectedTile = null;
						}
					}
					if (Input.GetKeyDown(KeyCode.Delete) && DevkitLandscapeTool.selectedTile != null)
					{
						Landscape.removeTile(DevkitLandscapeTool.selectedTile.coord);
						DevkitLandscapeTool.selectedTile = null;
					}
					if (Input.GetKeyDown(KeyCode.F) && DevkitLandscapeTool.selectedTile != null)
					{
						DevkitNavigation.focus(DevkitLandscapeTool.selectedTile.worldBounds);
					}
				}
				else if (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.HEIGHTMAP)
				{
					if (Input.GetKeyDown(KeyCode.Q))
					{
						DevkitLandscapeTool.heightmapMode = DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode.ADJUST;
					}
					if (Input.GetKeyDown(KeyCode.W))
					{
						DevkitLandscapeTool.heightmapMode = DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode.FLATTEN;
					}
					if (Input.GetKeyDown(KeyCode.E))
					{
						DevkitLandscapeTool.heightmapMode = DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode.SMOOTH;
					}
					if (Input.GetKeyDown(KeyCode.R))
					{
						DevkitLandscapeTool.heightmapMode = DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode.RAMP;
					}
					if (DevkitLandscapeTool.heightmapMode == DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode.FLATTEN)
					{
						if (Input.GetKeyDown(KeyCode.LeftAlt))
						{
							this.isSamplingFlattenTarget = true;
							Sleek2Pointer.cursor = Resources.Load<Texture2D>("UI/Cursors/Cursor_Eyedropper");
							Sleek2Pointer.hotspot = new Vector2(0f, 20f);
						}
						if (Input.GetKeyUp(KeyCode.Mouse0) && this.isSamplingFlattenTarget)
						{
							RaycastHit raycastHit2;
							if (Physics.Raycast(pointerToWorldRay, out raycastHit2, 8192f))
							{
								this.heightmapFlattenTarget = raycastHit2.point.y;
							}
							Sleek2Pointer.cursor = null;
							this.isSamplingFlattenTarget = false;
						}
					}
					if (!this.isSamplingFlattenTarget && this.isPointerOnLandscape)
					{
						if (DevkitLandscapeTool.heightmapMode == DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode.RAMP)
						{
							if (Input.GetKeyDown(KeyCode.Mouse0))
							{
								this.heightmapRampBeginPosition = this.pointerWorldPosition;
								this.isSamplingRampPositions = true;
								DevkitTransactionManager.beginTransaction(new TranslatedText(new TranslationReference("#SDG::Devkit.Transactions.Heightmap")));
								Landscape.clearHeightmapTransactions();
							}
							if (Input.GetKeyDown(KeyCode.Escape))
							{
								this.isSamplingRampPositions = false;
							}
							this.heightmapRampEndPosition = this.pointerWorldPosition;
							if (this.isSamplingRampPositions)
							{
								Vector2 vector = new Vector2(this.heightmapRampBeginPosition.x - this.heightmapRampEndPosition.x, this.heightmapRampBeginPosition.z - this.heightmapRampEndPosition.z);
								if (vector.magnitude > 1f)
								{
									Vector3 vector2 = new Vector3(Mathf.Min(this.heightmapRampBeginPosition.x, this.heightmapRampEndPosition.x), Mathf.Min(this.heightmapRampBeginPosition.y, this.heightmapRampEndPosition.y), Mathf.Min(this.heightmapRampBeginPosition.z, this.heightmapRampEndPosition.z));
									Vector3 vector3 = new Vector3(Mathf.Max(this.heightmapRampBeginPosition.x, this.heightmapRampEndPosition.x), Mathf.Max(this.heightmapRampBeginPosition.y, this.heightmapRampEndPosition.y), Mathf.Max(this.heightmapRampBeginPosition.z, this.heightmapRampEndPosition.z));
									vector2.x -= this.heightmapBrushRadius;
									vector2.z -= this.heightmapBrushRadius;
									vector3.x += this.heightmapBrushRadius;
									vector3.z += this.heightmapBrushRadius;
									Bounds worldBounds = new Bounds((vector2 + vector3) / 2f, vector3 - vector2);
									Landscape.getHeightmapVertices(worldBounds, new Landscape.LandscapeGetHeightmapVerticesHandler(this.handleHeightmapGetVerticesRamp));
								}
							}
						}
						else
						{
							if (Input.GetKeyDown(KeyCode.Mouse0))
							{
								DevkitTransactionManager.beginTransaction(new TranslatedText(new TranslationReference("#SDG::Devkit.Transactions.Heightmap")));
								Landscape.clearHeightmapTransactions();
							}
							Bounds worldBounds2 = new Bounds(this.brushWorldPosition, new Vector3(this.heightmapBrushRadius * 2f, 0f, this.heightmapBrushRadius * 2f));
							Landscape.getHeightmapVertices(worldBounds2, new Landscape.LandscapeGetHeightmapVerticesHandler(this.handleHeightmapGetVerticesBrush));
							if (Input.GetKey(KeyCode.Mouse0))
							{
								if (DevkitLandscapeTool.heightmapMode == DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode.ADJUST)
								{
									Landscape.writeHeightmap(worldBounds2, new Landscape.LandscapeWriteHeightmapHandler(this.handleHeightmapWriteAdjust));
								}
								else if (DevkitLandscapeTool.heightmapMode == DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode.FLATTEN)
								{
									worldBounds2.center = this.flattenPlanePosition;
									Landscape.writeHeightmap(worldBounds2, new Landscape.LandscapeWriteHeightmapHandler(this.handleHeightmapWriteFlatten));
								}
								else if (DevkitLandscapeTool.heightmapMode == DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode.SMOOTH)
								{
									if (DevkitLandscapeToolHeightmapOptions.instance.smoothMethod == DevkitLandscapeTool.EDevkitLandscapeToolHeightmapSmoothMethod.BRUSH_AVERAGE)
									{
										this.heightmapSmoothSampleCount = 0;
										this.heightmapSmoothSampleAverage = 0f;
										Landscape.readHeightmap(worldBounds2, new Landscape.LandscapeReadHeightmapHandler(this.handleHeightmapReadSmooth));
										if (this.heightmapSmoothSampleCount > 0)
										{
											this.heightmapSmoothTarget = this.heightmapSmoothSampleAverage / (float)this.heightmapSmoothSampleCount;
										}
										else
										{
											this.heightmapSmoothTarget = 0f;
										}
									}
									Landscape.writeHeightmap(worldBounds2, new Landscape.LandscapeWriteHeightmapHandler(this.handleHeightmapWriteSmooth));
								}
							}
						}
					}
				}
				else if (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.SPLATMAP)
				{
					if (Input.GetKeyDown(KeyCode.Q))
					{
						DevkitLandscapeTool.splatmapMode = DevkitLandscapeTool.EDevkitLandscapeToolSplatmapMode.PAINT;
					}
					if (Input.GetKeyDown(KeyCode.W))
					{
						DevkitLandscapeTool.splatmapMode = DevkitLandscapeTool.EDevkitLandscapeToolSplatmapMode.AUTO;
					}
					if (Input.GetKeyDown(KeyCode.E))
					{
						DevkitLandscapeTool.splatmapMode = DevkitLandscapeTool.EDevkitLandscapeToolSplatmapMode.SMOOTH;
					}
					if (Input.GetKeyDown(KeyCode.LeftAlt))
					{
						this.isSamplingLayer = true;
						Sleek2Pointer.cursor = Resources.Load<Texture2D>("UI/Cursors/Cursor_Eyedropper");
						Sleek2Pointer.hotspot = new Vector2(0f, 20f);
					}
					if (Input.GetKeyUp(KeyCode.Mouse0) && this.isSamplingLayer)
					{
						AssetReference<LandscapeMaterialAsset> splatmapMaterialTarget;
						if (this.isPointerOnLandscape && Landscape.getSplatmapMaterial(raycastHit.point, out splatmapMaterialTarget))
						{
							DevkitLandscapeTool.splatmapMaterialTarget = splatmapMaterialTarget;
						}
						Sleek2Pointer.cursor = null;
						this.isSamplingLayer = false;
					}
					if (!this.isSamplingLayer && this.isPointerOnLandscape)
					{
						if (Input.GetKeyDown(KeyCode.Mouse0))
						{
							DevkitTransactionManager.beginTransaction(new TranslatedText(new TranslationReference("#SDG::Devkit.Transactions.Splatmap")));
							Landscape.clearSplatmapTransactions();
						}
						Bounds worldBounds3 = new Bounds(this.brushWorldPosition, new Vector3(this.splatmapBrushRadius * 2f, 0f, this.splatmapBrushRadius * 2f));
						if (DevkitLandscapeToolSplatmapOptions.instance.previewMethod == DevkitLandscapeTool.EDevkitLandscapeToolSplatmapPreviewMethod.BRUSH_ALPHA)
						{
							Landscape.getSplatmapVertices(worldBounds3, new Landscape.LandscapeGetSplatmapVerticesHandler(this.handleSplatmapGetVerticesBrush));
						}
						else if (DevkitLandscapeToolSplatmapOptions.instance.previewMethod == DevkitLandscapeTool.EDevkitLandscapeToolSplatmapPreviewMethod.WEIGHT)
						{
							Landscape.readSplatmap(worldBounds3, new Landscape.LandscapeReadSplatmapHandler(this.handleSplatmapReadWeights));
						}
						if (Input.GetKey(KeyCode.Mouse0))
						{
							if (DevkitLandscapeTool.splatmapMode == DevkitLandscapeTool.EDevkitLandscapeToolSplatmapMode.PAINT)
							{
								Landscape.writeSplatmap(worldBounds3, new Landscape.LandscapeWriteSplatmapHandler(this.handleSplatmapWritePaint));
							}
							else if (DevkitLandscapeTool.splatmapMode == DevkitLandscapeTool.EDevkitLandscapeToolSplatmapMode.AUTO)
							{
								Landscape.writeSplatmap(worldBounds3, new Landscape.LandscapeWriteSplatmapHandler(this.handleSplatmapWriteAuto));
							}
							else if (DevkitLandscapeTool.splatmapMode == DevkitLandscapeTool.EDevkitLandscapeToolSplatmapMode.SMOOTH)
							{
								if (DevkitLandscapeToolSplatmapOptions.instance.smoothMethod == DevkitLandscapeTool.EDevkitLandscapeToolSplatmapSmoothMethod.BRUSH_AVERAGE)
								{
									this.splatmapSmoothSampleCount = 0;
									this.splatmapSmoothSampleAverage.Clear();
									Landscape.readSplatmap(worldBounds3, new Landscape.LandscapeReadSplatmapHandler(this.handleSplatmapReadSmooth));
								}
								Landscape.writeSplatmap(worldBounds3, new Landscape.LandscapeWriteSplatmapHandler(this.handleSplatmapWriteSmooth));
							}
						}
					}
				}
			}
			if (Input.GetKeyUp(KeyCode.LeftAlt))
			{
				if (this.isSamplingFlattenTarget)
				{
					Sleek2Pointer.cursor = null;
					this.isSamplingFlattenTarget = false;
				}
				if (this.isSamplingLayer)
				{
					Sleek2Pointer.cursor = null;
					this.isSamplingLayer = false;
				}
			}
			if (Input.GetKeyUp(KeyCode.Mouse0))
			{
				if (this.isSamplingRampPositions)
				{
					if (this.isPointerOnLandscape)
					{
						Vector2 vector4 = new Vector2(this.heightmapRampBeginPosition.x - this.heightmapRampEndPosition.x, this.heightmapRampBeginPosition.z - this.heightmapRampEndPosition.z);
						if (vector4.magnitude > 1f)
						{
							Vector3 vector5 = new Vector3(Mathf.Min(this.heightmapRampBeginPosition.x, this.heightmapRampEndPosition.x), Mathf.Min(this.heightmapRampBeginPosition.y, this.heightmapRampEndPosition.y), Mathf.Min(this.heightmapRampBeginPosition.z, this.heightmapRampEndPosition.z));
							Vector3 vector6 = new Vector3(Mathf.Max(this.heightmapRampBeginPosition.x, this.heightmapRampEndPosition.x), Mathf.Max(this.heightmapRampBeginPosition.y, this.heightmapRampEndPosition.y), Mathf.Max(this.heightmapRampBeginPosition.z, this.heightmapRampEndPosition.z));
							vector5.x -= this.heightmapBrushRadius;
							vector5.z -= this.heightmapBrushRadius;
							vector6.x += this.heightmapBrushRadius;
							vector6.z += this.heightmapBrushRadius;
							Bounds worldBounds4 = new Bounds((vector5 + vector6) / 2f, vector6 - vector5);
							Landscape.writeHeightmap(worldBounds4, new Landscape.LandscapeWriteHeightmapHandler(this.handleHeightmapWriteRamp));
						}
					}
					this.isSamplingRampPositions = false;
				}
				DevkitTransactionManager.endTransaction();
				if (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.HEIGHTMAP)
				{
					Landscape.applyLOD();
				}
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00053F74 File Offset: 0x00052374
		public virtual void equip()
		{
			GLRenderer.render += this.handleGLRender;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00053F87 File Offset: 0x00052387
		public virtual void dequip()
		{
			GLRenderer.render -= this.handleGLRender;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00053F9A File Offset: 0x0005239A
		protected float getBrushAlpha(float distance)
		{
			if (distance < this.brushFalloff)
			{
				return 1f;
			}
			return (1f - distance) / (1f - this.brushFalloff);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00053FC4 File Offset: 0x000523C4
		protected void handleHeightmapReadSmooth(LandscapeCoord tileCoord, HeightmapCoord heightmapCoord, Vector3 worldPosition, float currentHeight)
		{
			Vector2 vector = new Vector2(worldPosition.x - this.brushWorldPosition.x, worldPosition.z - this.brushWorldPosition.z);
			float num = vector.magnitude / this.heightmapBrushRadius;
			if (num > 1f)
			{
				return;
			}
			this.heightmapSmoothSampleCount++;
			this.heightmapSmoothSampleAverage += currentHeight;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00054038 File Offset: 0x00052438
		protected void handleHeightmapGetVerticesBrush(LandscapeCoord tileCoord, HeightmapCoord heightmapCoord, Vector3 worldPosition)
		{
			Vector2 vector = new Vector2(worldPosition.x - this.brushWorldPosition.x, worldPosition.z - this.brushWorldPosition.z);
			float num = vector.magnitude / this.heightmapBrushRadius;
			if (num > 1f)
			{
				return;
			}
			float brushAlpha = this.getBrushAlpha(num);
			this.previewSamples.Add(new LandscapePreviewSample(worldPosition, brushAlpha));
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000540A8 File Offset: 0x000524A8
		protected void handleHeightmapGetVerticesRamp(LandscapeCoord tileCoord, HeightmapCoord heightmapCoord, Vector3 worldPosition)
		{
			Vector2 a = new Vector2(this.heightmapRampEndPosition.x - this.heightmapRampBeginPosition.x, this.heightmapRampEndPosition.z - this.heightmapRampBeginPosition.z);
			float magnitude = a.magnitude;
			Vector2 vector = a / magnitude;
			Vector2 rhs = vector.Cross();
			Vector2 a2 = new Vector2(worldPosition.x - this.heightmapRampBeginPosition.x, worldPosition.z - this.heightmapRampBeginPosition.z);
			float magnitude2 = a2.magnitude;
			Vector2 lhs = a2 / magnitude2;
			float num = Vector2.Dot(lhs, vector);
			if (num < 0f)
			{
				return;
			}
			float num2 = magnitude2 * num / magnitude;
			if (num2 > 1f)
			{
				return;
			}
			float num3 = Vector2.Dot(lhs, rhs);
			float num4 = Mathf.Abs(magnitude2 * num3 / this.heightmapBrushRadius);
			if (num4 > 1f)
			{
				return;
			}
			float brushAlpha = this.getBrushAlpha(num4);
			this.previewSamples.Add(new LandscapePreviewSample(worldPosition, brushAlpha));
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000541B8 File Offset: 0x000525B8
		protected void handleSplatmapGetVerticesBrush(LandscapeCoord tileCoord, SplatmapCoord splatmapCoord, Vector3 worldPosition)
		{
			Vector2 vector = new Vector2(worldPosition.x - this.brushWorldPosition.x, worldPosition.z - this.brushWorldPosition.z);
			float num = vector.magnitude / this.splatmapBrushRadius;
			if (num > 1f)
			{
				return;
			}
			float brushAlpha = this.getBrushAlpha(num);
			this.previewSamples.Add(new LandscapePreviewSample(worldPosition, brushAlpha));
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00054228 File Offset: 0x00052628
		protected float handleHeightmapWriteAdjust(LandscapeCoord tileCoord, HeightmapCoord heightmapCoord, Vector3 worldPosition, float currentHeight)
		{
			Vector2 vector = new Vector2(worldPosition.x - this.brushWorldPosition.x, worldPosition.z - this.brushWorldPosition.z);
			float num = vector.magnitude / this.heightmapBrushRadius;
			if (num > 1f)
			{
				return currentHeight;
			}
			float brushAlpha = this.getBrushAlpha(num);
			float num2 = Time.deltaTime * this.heightmapBrushStrength * brushAlpha;
			num2 *= this.heightmapAdjustSensitivity;
			if (Input.GetKey(KeyCode.LeftShift))
			{
				num2 = -num2;
			}
			currentHeight += num2;
			return currentHeight;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000542BC File Offset: 0x000526BC
		protected float handleHeightmapWriteFlatten(LandscapeCoord tileCoord, HeightmapCoord heightmapCoord, Vector3 worldPosition, float currentHeight)
		{
			Vector2 vector = new Vector2(worldPosition.x - this.brushWorldPosition.x, worldPosition.z - this.brushWorldPosition.z);
			float num = vector.magnitude / this.heightmapBrushRadius;
			if (num > 1f)
			{
				return currentHeight;
			}
			float brushAlpha = this.getBrushAlpha(num);
			float num2 = (this.heightmapFlattenTarget + Landscape.TILE_HEIGHT / 2f) / Landscape.TILE_HEIGHT - currentHeight;
			float num3 = Time.deltaTime * this.heightmapBrushStrength * brushAlpha;
			num2 = Mathf.Clamp(num2, -num3, num3);
			num2 *= this.heightmapFlattenSensitivity;
			currentHeight += num2;
			return currentHeight;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00054364 File Offset: 0x00052764
		protected float handleHeightmapWriteSmooth(LandscapeCoord tileCoord, HeightmapCoord heightmapCoord, Vector3 worldPosition, float currentHeight)
		{
			Vector2 vector = new Vector2(worldPosition.x - this.brushWorldPosition.x, worldPosition.z - this.brushWorldPosition.z);
			float num = vector.magnitude / this.heightmapBrushRadius;
			if (num > 1f)
			{
				return currentHeight;
			}
			float brushAlpha = this.getBrushAlpha(num);
			if (DevkitLandscapeToolHeightmapOptions.instance.smoothMethod == DevkitLandscapeTool.EDevkitLandscapeToolHeightmapSmoothMethod.PIXEL_AVERAGE)
			{
				this.heightmapSmoothSampleCount = 0;
				this.heightmapSmoothSampleAverage = 0f;
				LandscapeCoord tileCoord2 = tileCoord;
				HeightmapCoord heightmapCoord2 = new HeightmapCoord(heightmapCoord.x, heightmapCoord.y - 1);
				LandscapeUtility.cleanHeightmapCoord(ref tileCoord2, ref heightmapCoord2);
				float num2;
				if (Landscape.getHeight01(tileCoord2, heightmapCoord2, out num2))
				{
					this.heightmapSmoothSampleCount++;
					this.heightmapSmoothSampleAverage += num2;
				}
				tileCoord2 = tileCoord;
				heightmapCoord2 = new HeightmapCoord(heightmapCoord.x + 1, heightmapCoord.y);
				LandscapeUtility.cleanHeightmapCoord(ref tileCoord2, ref heightmapCoord2);
				if (Landscape.getHeight01(tileCoord2, heightmapCoord2, out num2))
				{
					this.heightmapSmoothSampleCount++;
					this.heightmapSmoothSampleAverage += num2;
				}
				tileCoord2 = tileCoord;
				heightmapCoord2 = new HeightmapCoord(heightmapCoord.x, heightmapCoord.y + 1);
				LandscapeUtility.cleanHeightmapCoord(ref tileCoord2, ref heightmapCoord2);
				if (Landscape.getHeight01(tileCoord2, heightmapCoord2, out num2))
				{
					this.heightmapSmoothSampleCount++;
					this.heightmapSmoothSampleAverage += num2;
				}
				tileCoord2 = tileCoord;
				heightmapCoord2 = new HeightmapCoord(heightmapCoord.x - 1, heightmapCoord.y);
				LandscapeUtility.cleanHeightmapCoord(ref tileCoord2, ref heightmapCoord2);
				if (Landscape.getHeight01(tileCoord2, heightmapCoord2, out num2))
				{
					this.heightmapSmoothSampleCount++;
					this.heightmapSmoothSampleAverage += num2;
				}
				if (this.heightmapSmoothSampleCount > 0)
				{
					this.heightmapSmoothTarget = this.heightmapSmoothSampleAverage / (float)this.heightmapSmoothSampleCount;
				}
				else
				{
					this.heightmapSmoothTarget = currentHeight;
				}
			}
			currentHeight = Mathf.Lerp(currentHeight, this.heightmapSmoothTarget, Time.deltaTime * this.heightmapBrushStrength * brushAlpha);
			return currentHeight;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0005456C File Offset: 0x0005296C
		protected float handleHeightmapWriteRamp(LandscapeCoord tileCoord, HeightmapCoord heightmapCoord, Vector3 worldPosition, float currentHeight)
		{
			Vector2 a = new Vector2(this.heightmapRampEndPosition.x - this.heightmapRampBeginPosition.x, this.heightmapRampEndPosition.z - this.heightmapRampBeginPosition.z);
			float magnitude = a.magnitude;
			Vector2 vector = a / magnitude;
			Vector2 rhs = vector.Cross();
			Vector2 a2 = new Vector2(worldPosition.x - this.heightmapRampBeginPosition.x, worldPosition.z - this.heightmapRampBeginPosition.z);
			float magnitude2 = a2.magnitude;
			Vector2 lhs = a2 / magnitude2;
			float num = Vector2.Dot(lhs, vector);
			if (num < 0f)
			{
				return currentHeight;
			}
			float num2 = magnitude2 * num / magnitude;
			if (num2 > 1f)
			{
				return currentHeight;
			}
			float num3 = Vector2.Dot(lhs, rhs);
			float num4 = Mathf.Abs(magnitude2 * num3 / this.heightmapBrushRadius);
			if (num4 > 1f)
			{
				return currentHeight;
			}
			float brushAlpha = this.getBrushAlpha(num4);
			float a3 = (this.heightmapRampBeginPosition.y + Landscape.TILE_HEIGHT / 2f) / Landscape.TILE_HEIGHT;
			float b = (this.heightmapRampEndPosition.y + Landscape.TILE_HEIGHT / 2f) / Landscape.TILE_HEIGHT;
			currentHeight = Mathf.Lerp(currentHeight, Mathf.Lerp(a3, b, num2), brushAlpha);
			return Mathf.Clamp01(currentHeight);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000546C8 File Offset: 0x00052AC8
		protected void handleSplatmapReadSmooth(LandscapeCoord tileCoord, SplatmapCoord splatmapCoord, Vector3 worldPosition, float[] currentWeights)
		{
			LandscapeTile tile = Landscape.getTile(tileCoord);
			if (tile.materials == null)
			{
				return;
			}
			Vector2 vector = new Vector2(worldPosition.x - this.brushWorldPosition.x, worldPosition.z - this.brushWorldPosition.z);
			float num = vector.magnitude / this.splatmapBrushRadius;
			if (num > 1f)
			{
				return;
			}
			for (int i = 0; i < Landscape.SPLATMAP_LAYERS; i++)
			{
				AssetReference<LandscapeMaterialAsset> assetReference = tile.materials[i];
				if (assetReference.isValid)
				{
					if (!this.splatmapSmoothSampleAverage.ContainsKey(assetReference))
					{
						this.splatmapSmoothSampleAverage.Add(assetReference, 0f);
					}
					Dictionary<AssetReference<LandscapeMaterialAsset>, float> dictionary;
					AssetReference<LandscapeMaterialAsset> key;
					(dictionary = this.splatmapSmoothSampleAverage)[key = assetReference] = dictionary[key] + currentWeights[i];
					this.splatmapSmoothSampleCount++;
				}
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000547B8 File Offset: 0x00052BB8
		protected void handleSplatmapReadWeights(LandscapeCoord tileCoord, SplatmapCoord splatmapCoord, Vector3 worldPosition, float[] currentWeights)
		{
			LandscapeTile tile = Landscape.getTile(tileCoord);
			if (tile.materials == null)
			{
				return;
			}
			Vector2 vector = new Vector2(worldPosition.x - this.brushWorldPosition.x, worldPosition.z - this.brushWorldPosition.z);
			float num = vector.magnitude / this.splatmapBrushRadius;
			if (num > 1f)
			{
				return;
			}
			int splatmapTargetMaterialLayerIndex = this.getSplatmapTargetMaterialLayerIndex(tile, DevkitLandscapeTool.splatmapMaterialTarget);
			float newWeight;
			if (splatmapTargetMaterialLayerIndex == -1)
			{
				newWeight = 0f;
			}
			else
			{
				newWeight = currentWeights[splatmapTargetMaterialLayerIndex];
			}
			this.previewSamples.Add(new LandscapePreviewSample(worldPosition, newWeight));
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0005485C File Offset: 0x00052C5C
		protected int getSplatmapTargetMaterialLayerIndex(LandscapeTile tile, AssetReference<LandscapeMaterialAsset> targetMaterial)
		{
			if (!targetMaterial.isValid)
			{
				return -1;
			}
			int num = -1;
			for (int i = 0; i < Landscape.SPLATMAP_LAYERS; i++)
			{
				if (tile.materials[i] == targetMaterial)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				for (int j = 0; j < Landscape.SPLATMAP_LAYERS; j++)
				{
					if (!tile.materials[j].isValid)
					{
						tile.materials[j] = targetMaterial;
						tile.updatePrototypes();
						num = j;
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00054900 File Offset: 0x00052D00
		protected void blendSplatmapWeights(float[] currentWeights, int targetMaterialLayer, float targetWeight, float speed)
		{
			int splatmapHighestWeightLayerIndex = Landscape.getSplatmapHighestWeightLayerIndex(currentWeights, targetMaterialLayer);
			for (int i = 0; i < Landscape.SPLATMAP_LAYERS; i++)
			{
				float num;
				if (i == targetMaterialLayer)
				{
					num = targetWeight;
				}
				else if (i == splatmapHighestWeightLayerIndex)
				{
					num = 1f - targetWeight;
				}
				else
				{
					num = 0f;
				}
				float num2 = num - currentWeights[i];
				num2 *= speed;
				currentWeights[i] += num2;
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0005496C File Offset: 0x00052D6C
		protected void handleSplatmapWritePaint(LandscapeCoord tileCoord, SplatmapCoord splatmapCoord, Vector3 worldPosition, float[] currentWeights)
		{
			LandscapeTile tile = Landscape.getTile(tileCoord);
			if (tile.materials == null)
			{
				return;
			}
			int splatmapTargetMaterialLayerIndex = this.getSplatmapTargetMaterialLayerIndex(tile, DevkitLandscapeTool.splatmapMaterialTarget);
			if (splatmapTargetMaterialLayerIndex == -1)
			{
				return;
			}
			Vector2 vector = new Vector2(worldPosition.x - this.brushWorldPosition.x, worldPosition.z - this.brushWorldPosition.z);
			float num = vector.magnitude / this.splatmapBrushRadius;
			if (num > 1f)
			{
				return;
			}
			float targetWeight = 0.5f;
			if (Input.GetKey(KeyCode.LeftControl) || this.splatmapUseWeightTarget)
			{
				targetWeight = this.splatmapWeightTarget;
			}
			else if (DevkitLandscapeToolSplatmapOptions.instance.useAutoFoundation || DevkitLandscapeToolSplatmapOptions.instance.useAutoSlope)
			{
				bool flag = false;
				if (DevkitLandscapeToolSplatmapOptions.instance.useAutoFoundation)
				{
					int num2 = Physics.SphereCastNonAlloc(worldPosition + new Vector3(0f, DevkitLandscapeTool.splatmapMaterialTargetAsset.autoRayLength, 0f), DevkitLandscapeToolSplatmapOptions.instance.autoRayRadius, Vector3.down, DevkitLandscapeTool.FOUNDATION_HITS, DevkitLandscapeToolSplatmapOptions.instance.autoRayLength, (int)DevkitLandscapeToolSplatmapOptions.instance.autoRayMask, QueryTriggerInteraction.Ignore);
					if (num2 > 0)
					{
						bool flag2 = false;
						for (int i = 0; i < num2; i++)
						{
							RaycastHit raycastHit = DevkitLandscapeTool.FOUNDATION_HITS[i];
							DevkitHierarchyWorldObject component = raycastHit.transform.GetComponent<DevkitHierarchyWorldObject>();
							if (component == null || component.levelObject == null || component.levelObject.asset == null)
							{
								flag2 = true;
								break;
							}
							if (!component.levelObject.asset.isSnowshoe)
							{
								flag2 = true;
								break;
							}
						}
						if (flag2)
						{
							targetWeight = 1f;
							flag = true;
						}
					}
				}
				Vector3 to;
				if (!flag && DevkitLandscapeToolSplatmapOptions.instance.useAutoSlope && Landscape.getNormal(worldPosition, out to))
				{
					float num3 = Vector3.Angle(Vector3.up, to);
					if (num3 >= DevkitLandscapeToolSplatmapOptions.instance.autoMinAngleBegin && num3 <= DevkitLandscapeToolSplatmapOptions.instance.autoMaxAngleEnd)
					{
						if (num3 < DevkitLandscapeToolSplatmapOptions.instance.autoMinAngleEnd)
						{
							targetWeight = Mathf.InverseLerp(DevkitLandscapeToolSplatmapOptions.instance.autoMinAngleBegin, DevkitLandscapeToolSplatmapOptions.instance.autoMinAngleEnd, num3);
						}
						else if (num3 > DevkitLandscapeToolSplatmapOptions.instance.autoMaxAngleBegin)
						{
							targetWeight = 1f - Mathf.InverseLerp(DevkitLandscapeToolSplatmapOptions.instance.autoMaxAngleBegin, DevkitLandscapeToolSplatmapOptions.instance.autoMaxAngleEnd, num3);
						}
						else
						{
							targetWeight = 1f;
						}
						flag = true;
					}
				}
				if (!flag)
				{
					return;
				}
			}
			else if (Input.GetKey(KeyCode.LeftShift))
			{
				targetWeight = 0f;
			}
			else
			{
				targetWeight = 1f;
			}
			float brushAlpha = this.getBrushAlpha(num);
			float speed = Time.deltaTime * this.splatmapBrushStrength * brushAlpha * this.splatmapPaintSensitivity;
			this.blendSplatmapWeights(currentWeights, splatmapTargetMaterialLayerIndex, targetWeight, speed);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00054C60 File Offset: 0x00053060
		protected void handleSplatmapWriteAuto(LandscapeCoord tileCoord, SplatmapCoord splatmapCoord, Vector3 worldPosition, float[] currentWeights)
		{
			if (DevkitLandscapeTool.splatmapMaterialTargetAsset == null)
			{
				return;
			}
			LandscapeTile tile = Landscape.getTile(tileCoord);
			if (tile.materials == null)
			{
				return;
			}
			int splatmapTargetMaterialLayerIndex = this.getSplatmapTargetMaterialLayerIndex(tile, DevkitLandscapeTool.splatmapMaterialTarget);
			if (splatmapTargetMaterialLayerIndex == -1)
			{
				return;
			}
			Vector2 vector = new Vector2(worldPosition.x - this.brushWorldPosition.x, worldPosition.z - this.brushWorldPosition.z);
			float num = vector.magnitude / this.splatmapBrushRadius;
			if (num > 1f)
			{
				return;
			}
			float targetWeight = 1f;
			bool flag = false;
			if (DevkitLandscapeTool.splatmapMaterialTargetAsset.useAutoFoundation)
			{
				int num2 = Physics.SphereCastNonAlloc(worldPosition + new Vector3(0f, DevkitLandscapeTool.splatmapMaterialTargetAsset.autoRayLength, 0f), DevkitLandscapeTool.splatmapMaterialTargetAsset.autoRayRadius, Vector3.down, DevkitLandscapeTool.FOUNDATION_HITS, DevkitLandscapeTool.splatmapMaterialTargetAsset.autoRayLength, (int)DevkitLandscapeTool.splatmapMaterialTargetAsset.autoRayMask, QueryTriggerInteraction.Ignore);
				if (num2 > 0)
				{
					bool flag2 = false;
					for (int i = 0; i < num2; i++)
					{
						RaycastHit raycastHit = DevkitLandscapeTool.FOUNDATION_HITS[i];
						DevkitHierarchyWorldObject component = raycastHit.transform.GetComponent<DevkitHierarchyWorldObject>();
						if (component == null || component.levelObject == null || component.levelObject.asset == null)
						{
							flag2 = true;
							break;
						}
						if (!component.levelObject.asset.isSnowshoe)
						{
							flag2 = true;
							break;
						}
					}
					if (flag2)
					{
						targetWeight = 1f;
						flag = true;
					}
				}
			}
			Vector3 to;
			if (!flag && DevkitLandscapeTool.splatmapMaterialTargetAsset.useAutoSlope && Landscape.getNormal(worldPosition, out to))
			{
				float num3 = Vector3.Angle(Vector3.up, to);
				if (num3 >= DevkitLandscapeTool.splatmapMaterialTargetAsset.autoMinAngleBegin && num3 <= DevkitLandscapeTool.splatmapMaterialTargetAsset.autoMaxAngleEnd)
				{
					if (num3 < DevkitLandscapeTool.splatmapMaterialTargetAsset.autoMinAngleEnd)
					{
						targetWeight = Mathf.InverseLerp(DevkitLandscapeTool.splatmapMaterialTargetAsset.autoMinAngleBegin, DevkitLandscapeTool.splatmapMaterialTargetAsset.autoMinAngleEnd, num3);
					}
					else if (num3 > DevkitLandscapeTool.splatmapMaterialTargetAsset.autoMaxAngleBegin)
					{
						targetWeight = 1f - Mathf.InverseLerp(DevkitLandscapeTool.splatmapMaterialTargetAsset.autoMaxAngleBegin, DevkitLandscapeTool.splatmapMaterialTargetAsset.autoMaxAngleEnd, num3);
					}
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			float brushAlpha = this.getBrushAlpha(num);
			float speed = Time.deltaTime * this.splatmapBrushStrength * brushAlpha * this.splatmapPaintSensitivity;
			this.blendSplatmapWeights(currentWeights, splatmapTargetMaterialLayerIndex, targetWeight, speed);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00054EE8 File Offset: 0x000532E8
		protected void handleSplatmapWriteSmooth(LandscapeCoord tileCoord, SplatmapCoord splatmapCoord, Vector3 worldPosition, float[] currentWeights)
		{
			Vector2 vector = new Vector2(worldPosition.x - this.brushWorldPosition.x, worldPosition.z - this.brushWorldPosition.z);
			float num = vector.magnitude / this.splatmapBrushRadius;
			if (num > 1f)
			{
				return;
			}
			if (DevkitLandscapeToolSplatmapOptions.instance.smoothMethod == DevkitLandscapeTool.EDevkitLandscapeToolSplatmapSmoothMethod.PIXEL_AVERAGE)
			{
				this.splatmapSmoothSampleCount = 0;
				this.splatmapSmoothSampleAverage.Clear();
				LandscapeCoord coord = tileCoord;
				SplatmapCoord splatmapCoord2 = new SplatmapCoord(splatmapCoord.x, splatmapCoord.y - 1);
				LandscapeUtility.cleanSplatmapCoord(ref coord, ref splatmapCoord2);
				LandscapeTile tile = Landscape.getTile(coord);
				if (tile != null && tile.materials != null)
				{
					for (int i = 0; i < Landscape.SPLATMAP_LAYERS; i++)
					{
						AssetReference<LandscapeMaterialAsset> assetReference = tile.materials[i];
						if (assetReference.isValid)
						{
							if (!this.splatmapSmoothSampleAverage.ContainsKey(assetReference))
							{
								this.splatmapSmoothSampleAverage.Add(assetReference, 0f);
							}
							Dictionary<AssetReference<LandscapeMaterialAsset>, float> dictionary;
							AssetReference<LandscapeMaterialAsset> key;
							(dictionary = this.splatmapSmoothSampleAverage)[key = assetReference] = dictionary[key] + currentWeights[i];
							this.splatmapSmoothSampleCount++;
						}
					}
				}
				coord = tileCoord;
				splatmapCoord2 = new SplatmapCoord(splatmapCoord.x + 1, splatmapCoord.y);
				LandscapeUtility.cleanSplatmapCoord(ref coord, ref splatmapCoord2);
				tile = Landscape.getTile(coord);
				if (tile != null && tile.materials != null)
				{
					for (int j = 0; j < Landscape.SPLATMAP_LAYERS; j++)
					{
						AssetReference<LandscapeMaterialAsset> assetReference2 = tile.materials[j];
						if (assetReference2.isValid)
						{
							if (!this.splatmapSmoothSampleAverage.ContainsKey(assetReference2))
							{
								this.splatmapSmoothSampleAverage.Add(assetReference2, 0f);
							}
							Dictionary<AssetReference<LandscapeMaterialAsset>, float> dictionary;
							AssetReference<LandscapeMaterialAsset> key2;
							(dictionary = this.splatmapSmoothSampleAverage)[key2 = assetReference2] = dictionary[key2] + currentWeights[j];
							this.splatmapSmoothSampleCount++;
						}
					}
				}
				coord = tileCoord;
				splatmapCoord2 = new SplatmapCoord(splatmapCoord.x, splatmapCoord.y + 1);
				LandscapeUtility.cleanSplatmapCoord(ref coord, ref splatmapCoord2);
				tile = Landscape.getTile(coord);
				if (tile != null && tile.materials != null)
				{
					for (int k = 0; k < Landscape.SPLATMAP_LAYERS; k++)
					{
						AssetReference<LandscapeMaterialAsset> assetReference3 = tile.materials[k];
						if (assetReference3.isValid)
						{
							if (!this.splatmapSmoothSampleAverage.ContainsKey(assetReference3))
							{
								this.splatmapSmoothSampleAverage.Add(assetReference3, 0f);
							}
							Dictionary<AssetReference<LandscapeMaterialAsset>, float> dictionary;
							AssetReference<LandscapeMaterialAsset> key3;
							(dictionary = this.splatmapSmoothSampleAverage)[key3 = assetReference3] = dictionary[key3] + currentWeights[k];
							this.splatmapSmoothSampleCount++;
						}
					}
				}
				coord = tileCoord;
				splatmapCoord2 = new SplatmapCoord(splatmapCoord.x - 1, splatmapCoord.y);
				LandscapeUtility.cleanSplatmapCoord(ref coord, ref splatmapCoord2);
				tile = Landscape.getTile(coord);
				if (tile != null && tile.materials != null)
				{
					for (int l = 0; l < Landscape.SPLATMAP_LAYERS; l++)
					{
						AssetReference<LandscapeMaterialAsset> assetReference4 = tile.materials[l];
						if (assetReference4.isValid)
						{
							if (!this.splatmapSmoothSampleAverage.ContainsKey(assetReference4))
							{
								this.splatmapSmoothSampleAverage.Add(assetReference4, 0f);
							}
							Dictionary<AssetReference<LandscapeMaterialAsset>, float> dictionary;
							AssetReference<LandscapeMaterialAsset> key4;
							(dictionary = this.splatmapSmoothSampleAverage)[key4 = assetReference4] = dictionary[key4] + currentWeights[l];
							this.splatmapSmoothSampleCount++;
						}
					}
				}
			}
			if (this.splatmapSmoothSampleCount <= 0)
			{
				return;
			}
			LandscapeTile tile2 = Landscape.getTile(tileCoord);
			if (tile2.materials == null)
			{
				return;
			}
			float brushAlpha = this.getBrushAlpha(num);
			float num2 = Time.deltaTime * this.splatmapBrushStrength * brushAlpha;
			float num3 = 0f;
			for (int m = 0; m < Landscape.SPLATMAP_LAYERS; m++)
			{
				if (this.splatmapSmoothSampleAverage.ContainsKey(tile2.materials[m]))
				{
					num3 += this.splatmapSmoothSampleAverage[tile2.materials[m]] / (float)this.splatmapSmoothSampleCount;
				}
			}
			num3 = 1f / num3;
			for (int n = 0; n < Landscape.SPLATMAP_LAYERS; n++)
			{
				float num4;
				if (this.splatmapSmoothSampleAverage.ContainsKey(tile2.materials[n]))
				{
					num4 = this.splatmapSmoothSampleAverage[tile2.materials[n]] / (float)this.splatmapSmoothSampleCount * num3;
				}
				else
				{
					num4 = 0f;
				}
				float num5 = num4 - currentWeights[n];
				num5 *= num2;
				currentWeights[n] += num5;
			}
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000553C4 File Offset: 0x000537C4
		protected void handleGLCircleOffset(ref Vector3 position)
		{
			Landscape.getWorldHeight(position, out position.y);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000553D8 File Offset: 0x000537D8
		protected void handleGLRender()
		{
			GLUtility.matrix = MathUtility.IDENTITY_MATRIX;
			if (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.TILE)
			{
				GLUtility.LINE_FLAT_COLOR.SetPass(0);
				GL.Begin(1);
				if (DevkitLandscapeTool.selectedTile != null && DevkitLandscapeTool.selectedTile.coord != this.pointerTileCoord)
				{
					GL.Color(Color.yellow);
					GLUtility.line(new Vector3((float)DevkitLandscapeTool.selectedTile.coord.x * Landscape.TILE_SIZE, 0f, (float)DevkitLandscapeTool.selectedTile.coord.y * Landscape.TILE_SIZE), new Vector3((float)(DevkitLandscapeTool.selectedTile.coord.x + 1) * Landscape.TILE_SIZE, 0f, (float)DevkitLandscapeTool.selectedTile.coord.y * Landscape.TILE_SIZE));
					GLUtility.line(new Vector3((float)DevkitLandscapeTool.selectedTile.coord.x * Landscape.TILE_SIZE, 0f, (float)DevkitLandscapeTool.selectedTile.coord.y * Landscape.TILE_SIZE), new Vector3((float)DevkitLandscapeTool.selectedTile.coord.x * Landscape.TILE_SIZE, 0f, (float)(DevkitLandscapeTool.selectedTile.coord.y + 1) * Landscape.TILE_SIZE));
					GLUtility.line(new Vector3((float)(DevkitLandscapeTool.selectedTile.coord.x + 1) * Landscape.TILE_SIZE, 0f, (float)(DevkitLandscapeTool.selectedTile.coord.y + 1) * Landscape.TILE_SIZE), new Vector3((float)(DevkitLandscapeTool.selectedTile.coord.x + 1) * Landscape.TILE_SIZE, 0f, (float)DevkitLandscapeTool.selectedTile.coord.y * Landscape.TILE_SIZE));
					GLUtility.line(new Vector3((float)(DevkitLandscapeTool.selectedTile.coord.x + 1) * Landscape.TILE_SIZE, 0f, (float)(DevkitLandscapeTool.selectedTile.coord.y + 1) * Landscape.TILE_SIZE), new Vector3((float)DevkitLandscapeTool.selectedTile.coord.x * Landscape.TILE_SIZE, 0f, (float)(DevkitLandscapeTool.selectedTile.coord.y + 1) * Landscape.TILE_SIZE));
				}
				if (this.isTileVisible && DevkitInput.canEditorReceiveInput)
				{
					GL.Color((Landscape.getTile(this.pointerTileCoord) != null) ? ((DevkitLandscapeTool.selectedTile == null || !(DevkitLandscapeTool.selectedTile.coord == this.pointerTileCoord)) ? Color.white : Color.red) : Color.green);
					GLUtility.line(new Vector3((float)this.pointerTileCoord.x * Landscape.TILE_SIZE, 0f, (float)this.pointerTileCoord.y * Landscape.TILE_SIZE), new Vector3((float)(this.pointerTileCoord.x + 1) * Landscape.TILE_SIZE, 0f, (float)this.pointerTileCoord.y * Landscape.TILE_SIZE));
					GLUtility.line(new Vector3((float)this.pointerTileCoord.x * Landscape.TILE_SIZE, 0f, (float)this.pointerTileCoord.y * Landscape.TILE_SIZE), new Vector3((float)this.pointerTileCoord.x * Landscape.TILE_SIZE, 0f, (float)(this.pointerTileCoord.y + 1) * Landscape.TILE_SIZE));
					GLUtility.line(new Vector3((float)(this.pointerTileCoord.x + 1) * Landscape.TILE_SIZE, 0f, (float)(this.pointerTileCoord.y + 1) * Landscape.TILE_SIZE), new Vector3((float)(this.pointerTileCoord.x + 1) * Landscape.TILE_SIZE, 0f, (float)this.pointerTileCoord.y * Landscape.TILE_SIZE));
					GLUtility.line(new Vector3((float)(this.pointerTileCoord.x + 1) * Landscape.TILE_SIZE, 0f, (float)(this.pointerTileCoord.y + 1) * Landscape.TILE_SIZE), new Vector3((float)this.pointerTileCoord.x * Landscape.TILE_SIZE, 0f, (float)(this.pointerTileCoord.y + 1) * Landscape.TILE_SIZE));
				}
				GL.End();
			}
			else if (this.isBrushVisible && DevkitInput.canEditorReceiveInput)
			{
				if ((long)this.previewSamples.Count <= (long)((ulong)this.maxPreviewSamples))
				{
					GLUtility.LINE_FLAT_COLOR.SetPass(0);
					GL.Begin(4);
					float num = Mathf.Lerp(0.1f, 1f, this.brushRadius / 256f);
					Vector3 size = new Vector3(num, num, num);
					foreach (LandscapePreviewSample landscapePreviewSample in this.previewSamples)
					{
						GL.Color(Color.Lerp(Color.red, Color.green, landscapePreviewSample.weight));
						GLUtility.boxSolid(landscapePreviewSample.position, size);
					}
					GL.End();
				}
				GLUtility.LINE_FLAT_COLOR.SetPass(0);
				GL.Begin(1);
				if (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.HEIGHTMAP && DevkitLandscapeTool.heightmapMode == DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode.RAMP)
				{
					if (this.isSamplingRampPositions)
					{
						Vector3 normalized = (this.heightmapRampEndPosition - this.heightmapRampBeginPosition).normalized;
						Vector3 a = Vector3.Cross(Vector3.up, normalized);
						GL.Color(new Color(0.5f, 0.5f, 0f, 0.5f));
						GLUtility.line(this.heightmapRampBeginPosition - a * this.brushRadius, this.heightmapRampEndPosition - a * this.brushRadius);
						GLUtility.line(this.heightmapRampBeginPosition + a * this.brushRadius, this.heightmapRampEndPosition + a * this.brushRadius);
						GL.Color(Color.yellow);
						GLUtility.line(this.heightmapRampBeginPosition - a * this.brushRadius * this.heightmapBrushFalloff, this.heightmapRampEndPosition - a * this.brushRadius * this.heightmapBrushFalloff);
						GLUtility.line(this.heightmapRampBeginPosition + a * this.brushRadius * this.heightmapBrushFalloff, this.heightmapRampEndPosition + a * this.brushRadius * this.heightmapBrushFalloff);
					}
					else if (this.isChangingBrushRadius || this.isChangingBrushFalloff)
					{
						Vector3 normalized2 = (this.pointerWorldPosition - this.brushWorldPosition).normalized;
						Vector3 b = Vector3.Cross(Vector3.up, normalized2);
						GL.Color(new Color(0.5f, 0.5f, 0f, 0.5f));
						GLUtility.line(this.brushWorldPosition - normalized2 * this.brushRadius - b, this.brushWorldPosition - normalized2 * this.brushRadius + b);
						GLUtility.line(this.brushWorldPosition + normalized2 * this.brushRadius - b, this.brushWorldPosition + normalized2 * this.brushRadius + b);
						GL.Color(Color.yellow);
						GLUtility.line(this.brushWorldPosition - normalized2 * this.brushRadius * this.heightmapBrushFalloff - b, this.brushWorldPosition - normalized2 * this.brushRadius * this.heightmapBrushFalloff + b);
						GLUtility.line(this.brushWorldPosition + normalized2 * this.brushRadius * this.heightmapBrushFalloff - b, this.brushWorldPosition + normalized2 * this.brushRadius * this.heightmapBrushFalloff + b);
					}
				}
				else
				{
					Color color;
					if (this.isChangingBrushStrength)
					{
						color = Color.Lerp(Color.red, Color.green, this.brushStrength);
					}
					else if (this.isChangingWeightTarget)
					{
						color = Color.Lerp(Color.red, Color.green, this.splatmapWeightTarget);
					}
					else
					{
						color = Color.yellow;
					}
					GL.Color(color / 2f);
					GLUtility.circle(this.brushWorldPosition, this.brushRadius, new Vector3(1f, 0f, 0f), new Vector3(0f, 0f, 1f), new GLCircleOffsetHandler(this.handleGLCircleOffset));
					if (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.HEIGHTMAP && DevkitLandscapeTool.heightmapMode == DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode.FLATTEN)
					{
						GLUtility.circle(this.flattenPlanePosition, this.brushRadius, new Vector3(1f, 0f, 0f), new Vector3(0f, 0f, 1f), 0f);
					}
					GL.Color(color);
					GLUtility.circle(this.brushWorldPosition, this.brushRadius * this.brushFalloff, new Vector3(1f, 0f, 0f), new Vector3(0f, 0f, 1f), new GLCircleOffsetHandler(this.handleGLCircleOffset));
					if (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.HEIGHTMAP && DevkitLandscapeTool.heightmapMode == DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode.FLATTEN)
					{
						GLUtility.circle(this.flattenPlanePosition, this.brushRadius * this.brushFalloff, new Vector3(1f, 0f, 0f), new Vector3(0f, 0f, 1f), 0f);
					}
				}
				GL.End();
			}
		}

		// Token: 0x0400077A RID: 1914
		private static readonly RaycastHit[] FOUNDATION_HITS = new RaycastHit[4];

		// Token: 0x0400077B RID: 1915
		protected static DevkitLandscapeTool.EDevkitLandscapeToolMode _toolMode;

		// Token: 0x0400077D RID: 1917
		protected static LandscapeTile _selectedTile;

		// Token: 0x0400077F RID: 1919
		public static DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode heightmapMode;

		// Token: 0x04000780 RID: 1920
		public static DevkitLandscapeTool.EDevkitLandscapeToolSplatmapMode splatmapMode;

		// Token: 0x04000781 RID: 1921
		protected static LandscapeMaterialAsset splatmapMaterialTargetAsset;

		// Token: 0x04000782 RID: 1922
		protected static AssetReference<LandscapeMaterialAsset> _splatmapMaterialTarget;

		// Token: 0x04000783 RID: 1923
		protected int heightmapSmoothSampleCount;

		// Token: 0x04000784 RID: 1924
		protected float heightmapSmoothSampleAverage;

		// Token: 0x04000785 RID: 1925
		protected float heightmapSmoothTarget;

		// Token: 0x04000786 RID: 1926
		protected int splatmapSmoothSampleCount;

		// Token: 0x04000787 RID: 1927
		protected Dictionary<AssetReference<LandscapeMaterialAsset>, float> splatmapSmoothSampleAverage = new Dictionary<AssetReference<LandscapeMaterialAsset>, float>();

		// Token: 0x04000788 RID: 1928
		protected Vector3 heightmapRampBeginPosition;

		// Token: 0x04000789 RID: 1929
		protected Vector3 heightmapRampEndPosition;

		// Token: 0x0400078A RID: 1930
		protected Vector3 tilePlanePosition;

		// Token: 0x0400078B RID: 1931
		protected Vector3 pointerWorldPosition;

		// Token: 0x0400078C RID: 1932
		protected Vector3 brushWorldPosition;

		// Token: 0x0400078D RID: 1933
		protected Vector3 changePlanePosition;

		// Token: 0x0400078E RID: 1934
		protected Vector3 flattenPlanePosition;

		// Token: 0x0400078F RID: 1935
		protected bool isPointerOnLandscape;

		// Token: 0x04000790 RID: 1936
		protected bool isPointerOnTilePlane;

		// Token: 0x04000791 RID: 1937
		protected bool isBrushVisible;

		// Token: 0x04000792 RID: 1938
		protected bool isTileVisible;

		// Token: 0x04000793 RID: 1939
		protected LandscapeCoord pointerTileCoord;

		// Token: 0x04000794 RID: 1940
		protected List<LandscapePreviewSample> previewSamples = new List<LandscapePreviewSample>();

		// Token: 0x04000795 RID: 1941
		protected bool isChangingBrushRadius;

		// Token: 0x04000796 RID: 1942
		protected bool isChangingBrushFalloff;

		// Token: 0x04000797 RID: 1943
		protected bool isChangingBrushStrength;

		// Token: 0x04000798 RID: 1944
		protected bool isChangingWeightTarget;

		// Token: 0x04000799 RID: 1945
		protected bool isSamplingFlattenTarget;

		// Token: 0x0400079A RID: 1946
		protected bool isSamplingRampPositions;

		// Token: 0x0400079B RID: 1947
		protected bool isSamplingLayer;

		// Token: 0x02000160 RID: 352
		public enum EDevkitLandscapeToolMode
		{
			// Token: 0x0400079D RID: 1949
			HEIGHTMAP,
			// Token: 0x0400079E RID: 1950
			SPLATMAP,
			// Token: 0x0400079F RID: 1951
			TILE
		}

		// Token: 0x02000161 RID: 353
		public enum EDevkitLandscapeToolHeightmapMode
		{
			// Token: 0x040007A1 RID: 1953
			ADJUST,
			// Token: 0x040007A2 RID: 1954
			FLATTEN,
			// Token: 0x040007A3 RID: 1955
			SMOOTH,
			// Token: 0x040007A4 RID: 1956
			RAMP
		}

		// Token: 0x02000162 RID: 354
		public enum EDevkitLandscapeToolSplatmapMode
		{
			// Token: 0x040007A6 RID: 1958
			PAINT,
			// Token: 0x040007A7 RID: 1959
			AUTO,
			// Token: 0x040007A8 RID: 1960
			SMOOTH
		}

		// Token: 0x02000163 RID: 355
		public enum EDevkitLandscapeToolHeightmapSmoothMethod
		{
			// Token: 0x040007AA RID: 1962
			BRUSH_AVERAGE,
			// Token: 0x040007AB RID: 1963
			PIXEL_AVERAGE
		}

		// Token: 0x02000164 RID: 356
		public enum EDevkitLandscapeToolSplatmapSmoothMethod
		{
			// Token: 0x040007AD RID: 1965
			BRUSH_AVERAGE,
			// Token: 0x040007AE RID: 1966
			PIXEL_AVERAGE
		}

		// Token: 0x02000165 RID: 357
		public enum EDevkitLandscapeToolSplatmapPreviewMethod
		{
			// Token: 0x040007B0 RID: 1968
			BRUSH_ALPHA,
			// Token: 0x040007B1 RID: 1969
			WEIGHT
		}

		// Token: 0x02000166 RID: 358
		// (Invoke) Token: 0x06000AC0 RID: 2752
		public delegate void DevkitLandscapeToolModeChangedHandler(DevkitLandscapeTool.EDevkitLandscapeToolMode oldMode, DevkitLandscapeTool.EDevkitLandscapeToolMode newMode);

		// Token: 0x02000167 RID: 359
		// (Invoke) Token: 0x06000AC4 RID: 2756
		public delegate void DevkitLandscapeToolSelectedTileChangedHandler(LandscapeTile oldSelectedTile, LandscapeTile newSelectedTile);
	}
}
