using System;
using System.Collections.Generic;
using SDG.Framework.Devkit.Interactable;
using SDG.Framework.UI.Devkit.InspectorUI;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000134 RID: 308
	public class DevkitSelectionManager
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x0004F500 File Offset: 0x0004D900
		public static void select(DevkitSelection select)
		{
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftControl))
			{
				if (DevkitSelectionManager.selection.Contains(select))
				{
					DevkitSelectionManager.remove(select);
				}
				else
				{
					DevkitSelectionManager.add(select);
				}
			}
			else
			{
				DevkitSelectionManager.clear();
				DevkitSelectionManager.add(select);
			}
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0004F55C File Offset: 0x0004D95C
		public static void add(DevkitSelection select)
		{
			if (select.gameObject == null)
			{
				return;
			}
			if (DevkitSelectionManager.selection.Contains(select))
			{
				return;
			}
			if (DevkitSelectionManager.beginSelection(select))
			{
				DevkitSelectionManager.selection.Add(select);
			}
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0004F599 File Offset: 0x0004D999
		public static void remove(DevkitSelection select)
		{
			if (DevkitSelectionManager.selection.Remove(select))
			{
				DevkitSelectionManager.endSelection(select);
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0004F5B4 File Offset: 0x0004D9B4
		public static void clear()
		{
			foreach (DevkitSelection select in DevkitSelectionManager.selection)
			{
				DevkitSelectionManager.endSelection(select);
			}
			DevkitSelectionManager.selection.Clear();
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0004F61C File Offset: 0x0004DA1C
		public static bool beginDrag(DevkitSelection select)
		{
			if (select.gameObject == null)
			{
				return false;
			}
			DevkitSelectionManager.data.collider = select.collider;
			DevkitSelectionManager.beginDragHandlers.Clear();
			select.gameObject.GetComponentsInChildren<IDevkitInteractableBeginDragHandler>(DevkitSelectionManager.beginDragHandlers);
			foreach (IDevkitInteractableBeginHoverHandler devkitInteractableBeginHoverHandler in DevkitSelectionManager.beginHoverHandlers)
			{
				IDevkitInteractableBeginDragHandler devkitInteractableBeginDragHandler = (IDevkitInteractableBeginDragHandler)devkitInteractableBeginHoverHandler;
				devkitInteractableBeginDragHandler.beginDrag(DevkitSelectionManager.data);
			}
			return DevkitSelectionManager.beginDragHandlers.Count > 0;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0004F6D0 File Offset: 0x0004DAD0
		public static bool beginHover(DevkitSelection select)
		{
			if (select.gameObject == null)
			{
				return false;
			}
			DevkitSelectionManager.data.collider = select.collider;
			DevkitSelectionManager.beginHoverHandlers.Clear();
			select.gameObject.GetComponentsInChildren<IDevkitInteractableBeginHoverHandler>(DevkitSelectionManager.beginHoverHandlers);
			foreach (IDevkitInteractableBeginHoverHandler devkitInteractableBeginHoverHandler in DevkitSelectionManager.beginHoverHandlers)
			{
				devkitInteractableBeginHoverHandler.beginHover(DevkitSelectionManager.data);
			}
			return DevkitSelectionManager.beginHoverHandlers.Count > 0;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0004F77C File Offset: 0x0004DB7C
		public static bool beginSelection(DevkitSelection select)
		{
			if (select.gameObject == null)
			{
				return false;
			}
			DevkitSelectionManager.data.collider = select.collider;
			DevkitSelectionManager.beginSelectionHandlers.Clear();
			select.gameObject.GetComponentsInChildren<IDevkitInteractableBeginSelectionHandler>(DevkitSelectionManager.beginSelectionHandlers);
			foreach (IDevkitInteractableBeginSelectionHandler devkitInteractableBeginSelectionHandler in DevkitSelectionManager.beginSelectionHandlers)
			{
				devkitInteractableBeginSelectionHandler.beginSelection(DevkitSelectionManager.data);
			}
			if (DevkitSelectionManager.beginSelectionHandlers.Count > 0)
			{
				InspectorWindow.inspect(DevkitSelectionManager.beginSelectionHandlers[0]);
			}
			return DevkitSelectionManager.beginSelectionHandlers.Count > 0;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0004F848 File Offset: 0x0004DC48
		public static bool continueDrag(DevkitSelection select)
		{
			if (select.gameObject == null)
			{
				return false;
			}
			DevkitSelectionManager.data.collider = select.collider;
			DevkitSelectionManager.continueDragHandlers.Clear();
			select.gameObject.GetComponentsInChildren<IDevkitInteractableContinueDragHandler>(DevkitSelectionManager.continueDragHandlers);
			foreach (IDevkitInteractableContinueDragHandler devkitInteractableContinueDragHandler in DevkitSelectionManager.continueDragHandlers)
			{
				devkitInteractableContinueDragHandler.continueDrag(DevkitSelectionManager.data);
			}
			return DevkitSelectionManager.continueDragHandlers.Count > 0;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0004F8F4 File Offset: 0x0004DCF4
		public static bool endDrag(DevkitSelection select)
		{
			if (select.gameObject == null)
			{
				return false;
			}
			DevkitSelectionManager.data.collider = select.collider;
			DevkitSelectionManager.endDragHandlers.Clear();
			select.gameObject.GetComponentsInChildren<IDevkitInteractableEndDragHandler>(DevkitSelectionManager.endDragHandlers);
			foreach (IDevkitInteractableEndDragHandler devkitInteractableEndDragHandler in DevkitSelectionManager.endDragHandlers)
			{
				devkitInteractableEndDragHandler.endDrag(DevkitSelectionManager.data);
			}
			return DevkitSelectionManager.endDragHandlers.Count > 0;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0004F9A0 File Offset: 0x0004DDA0
		public static bool endHover(DevkitSelection select)
		{
			if (select.gameObject == null)
			{
				return false;
			}
			DevkitSelectionManager.data.collider = select.collider;
			DevkitSelectionManager.endHoverHandlers.Clear();
			select.gameObject.GetComponentsInChildren<IDevkitInteractableEndHoverHandler>(DevkitSelectionManager.endHoverHandlers);
			foreach (IDevkitInteractableEndHoverHandler devkitInteractableEndHoverHandler in DevkitSelectionManager.endHoverHandlers)
			{
				devkitInteractableEndHoverHandler.endHover(DevkitSelectionManager.data);
			}
			return DevkitSelectionManager.endHoverHandlers.Count > 0;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0004FA4C File Offset: 0x0004DE4C
		public static bool endSelection(DevkitSelection select)
		{
			if (select.gameObject == null)
			{
				return false;
			}
			DevkitSelectionManager.data.collider = select.collider;
			DevkitSelectionManager.endSelectionHandlers.Clear();
			select.gameObject.GetComponentsInChildren<IDevkitInteractableEndSelectionHandler>(DevkitSelectionManager.endSelectionHandlers);
			foreach (IDevkitInteractableEndSelectionHandler devkitInteractableEndSelectionHandler in DevkitSelectionManager.endSelectionHandlers)
			{
				devkitInteractableEndSelectionHandler.endSelection(DevkitSelectionManager.data);
			}
			return DevkitSelectionManager.endSelectionHandlers.Count > 0;
		}

		// Token: 0x04000723 RID: 1827
		protected static List<IDevkitInteractableBeginDragHandler> beginDragHandlers = new List<IDevkitInteractableBeginDragHandler>();

		// Token: 0x04000724 RID: 1828
		protected static List<IDevkitInteractableBeginHoverHandler> beginHoverHandlers = new List<IDevkitInteractableBeginHoverHandler>();

		// Token: 0x04000725 RID: 1829
		protected static List<IDevkitInteractableBeginSelectionHandler> beginSelectionHandlers = new List<IDevkitInteractableBeginSelectionHandler>();

		// Token: 0x04000726 RID: 1830
		protected static List<IDevkitInteractableContinueDragHandler> continueDragHandlers = new List<IDevkitInteractableContinueDragHandler>();

		// Token: 0x04000727 RID: 1831
		protected static List<IDevkitInteractableEndDragHandler> endDragHandlers = new List<IDevkitInteractableEndDragHandler>();

		// Token: 0x04000728 RID: 1832
		protected static List<IDevkitInteractableEndHoverHandler> endHoverHandlers = new List<IDevkitInteractableEndHoverHandler>();

		// Token: 0x04000729 RID: 1833
		protected static List<IDevkitInteractableEndSelectionHandler> endSelectionHandlers = new List<IDevkitInteractableEndSelectionHandler>();

		// Token: 0x0400072A RID: 1834
		public static HashSet<DevkitSelection> selection = new HashSet<DevkitSelection>();

		// Token: 0x0400072B RID: 1835
		public static InteractionData data = new InteractionData();
	}
}
