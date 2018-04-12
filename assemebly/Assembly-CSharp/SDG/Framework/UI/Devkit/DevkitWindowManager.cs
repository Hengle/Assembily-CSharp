using System;
using System.Collections.Generic;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit
{
	// Token: 0x02000243 RID: 579
	public class DevkitWindowManager
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x0006E510 File Offset: 0x0006C910
		// (set) Token: 0x060010E3 RID: 4323 RVA: 0x0006E517 File Offset: 0x0006C917
		public static List<Sleek2PopoutContainer> containers
		{
			get
			{
				return DevkitWindowManager._containers;
			}
			set
			{
				DevkitWindowManager._containers = value;
			}
		}

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x060010E4 RID: 4324 RVA: 0x0006E520 File Offset: 0x0006C920
		// (remove) Token: 0x060010E5 RID: 4325 RVA: 0x0006E554 File Offset: 0x0006C954
		public static event DevkitToolbarCreatedHandler toolbarCreated;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x060010E6 RID: 4326 RVA: 0x0006E588 File Offset: 0x0006C988
		// (remove) Token: 0x060010E7 RID: 4327 RVA: 0x0006E5BC File Offset: 0x0006C9BC
		public static event DevkitActivityChangedHandler activityChanged;

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0006E5F0 File Offset: 0x0006C9F0
		// (set) Token: 0x060010E9 RID: 4329 RVA: 0x0006E5F7 File Offset: 0x0006C9F7
		public static Sleek2Container editor { get; protected set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x0006E5FF File Offset: 0x0006C9FF
		// (set) Token: 0x060010EB RID: 4331 RVA: 0x0006E606 File Offset: 0x0006CA06
		public static Sleek2Toolbar toolbar { get; protected set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x0006E60E File Offset: 0x0006CA0E
		// (set) Token: 0x060010ED RID: 4333 RVA: 0x0006E615 File Offset: 0x0006CA15
		public static Sleek2WindowPartition partition { get; protected set; }

		// Token: 0x060010EE RID: 4334 RVA: 0x0006E61D File Offset: 0x0006CA1D
		public static void addWindow(Sleek2Window window)
		{
			if (DevkitWindowManager.partition == null)
			{
				return;
			}
			DevkitWindowManager.partition.addWindow(window);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0006E636 File Offset: 0x0006CA36
		public static T addContainer<T>() where T : Sleek2PopoutContainer, new()
		{
			return DevkitWindowManager.addContainer(typeof(T)) as T;
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0006E654 File Offset: 0x0006CA54
		public static Sleek2PopoutContainer addContainer(Type type)
		{
			Sleek2PopoutContainer sleek2PopoutContainer = Activator.CreateInstance(type) as Sleek2PopoutContainer;
			sleek2PopoutContainer.transform.anchorMin = Vector2.zero;
			sleek2PopoutContainer.transform.anchorMax = Vector2.one;
			sleek2PopoutContainer.transform.pivot = Vector2.zero;
			sleek2PopoutContainer.transform.sizeDelta = Vector2.zero;
			sleek2PopoutContainer.transform.SetParent(DevkitCanvas.instance.transform, false);
			DevkitWindowManager.containers.Add(sleek2PopoutContainer);
			return sleek2PopoutContainer;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0006E6CF File Offset: 0x0006CACF
		public static void removeContainer(Sleek2PopoutContainer container)
		{
			DevkitWindowManager.containers.Remove(container);
			container.destroy();
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0006E6E4 File Offset: 0x0006CAE4
		public static void resetLayout()
		{
			if (DevkitWindowManager.partition != null)
			{
				DevkitWindowManager.partition.destroy();
				DevkitWindowManager.partition = null;
			}
			DevkitWindowManager.partition = new Sleek2WindowPartition();
			DevkitWindowManager.partition.transform.anchorMin = Vector2.zero;
			DevkitWindowManager.partition.transform.anchorMax = Vector2.one;
			DevkitWindowManager.partition.transform.offsetMin = Vector2.zero;
			DevkitWindowManager.partition.transform.offsetMax = Vector2.zero;
			DevkitWindowManager.editor.bodyPanel.addElement(DevkitWindowManager.partition);
			if (DevkitWindowManager.containers != null)
			{
				for (int i = 0; i < DevkitWindowManager.containers.Count; i++)
				{
					DevkitWindowManager.containers[i].destroy();
				}
				DevkitWindowManager.containers.Clear();
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0006E7B8 File Offset: 0x0006CBB8
		// (set) Token: 0x060010F4 RID: 4340 RVA: 0x0006E7C0 File Offset: 0x0006CBC0
		public static bool isActive
		{
			get
			{
				return DevkitWindowManager._isActive;
			}
			set
			{
				if (DevkitWindowManager.isActive == value)
				{
					return;
				}
				DevkitWindowManager._isActive = value;
				if (DevkitWindowManager.isActive && DevkitWindowManager.editor == null)
				{
					DevkitWindowManager.editor = new Sleek2Container();
					DevkitWindowManager.editor.name = "Editor";
					DevkitWindowManager.editor.transform.reset();
					DevkitWindowManager.editor.transform.SetParent(DevkitCanvas.instance.transform, false);
					DevkitWindowManager.toolbar = new Sleek2Toolbar();
					DevkitWindowManager.editor.headerPanel.addElement(DevkitWindowManager.toolbar);
					DevkitWindowManager.partition = new Sleek2WindowPartition();
					DevkitWindowManager.partition.transform.anchorMin = Vector2.zero;
					DevkitWindowManager.partition.transform.anchorMax = Vector2.one;
					DevkitWindowManager.partition.transform.offsetMin = Vector2.zero;
					DevkitWindowManager.partition.transform.offsetMax = Vector2.zero;
					DevkitWindowManager.editor.bodyPanel.addElement(DevkitWindowManager.partition);
					DevkitWindowManager.triggerToolbarCreated();
					DevkitWindowLayout.load("Default");
				}
				if (DevkitWindowManager.editor != null)
				{
					DevkitWindowManager.editor.transform.gameObject.SetActive(DevkitWindowManager.isActive);
				}
				if (DevkitCanvas.instance != null)
				{
					DevkitCanvas.instance.gameObject.SetActive(DevkitWindowManager.isActive || DevkitWindowManager.containers.Count > 0);
				}
				DevkitWindowManager.triggerActivityChanged();
			}
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0006E931 File Offset: 0x0006CD31
		protected static void triggerToolbarCreated()
		{
			if (DevkitWindowManager.toolbarCreated != null)
			{
				DevkitWindowManager.toolbarCreated();
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0006E947 File Offset: 0x0006CD47
		protected static void triggerActivityChanged()
		{
			if (DevkitWindowManager.activityChanged != null)
			{
				DevkitWindowManager.activityChanged();
			}
		}

		// Token: 0x04000A1F RID: 2591
		protected static List<Sleek2PopoutContainer> _containers = new List<Sleek2PopoutContainer>();

		// Token: 0x04000A25 RID: 2597
		protected static bool _isActive;
	}
}
