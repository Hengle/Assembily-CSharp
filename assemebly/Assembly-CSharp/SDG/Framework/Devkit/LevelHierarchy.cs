using System;
using System.Collections.Generic;
using System.IO;
using SDG.Framework.Devkit.Transactions;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.IO.FormattedFiles.KeyValueTables;
using SDG.Framework.Modules;
using SDG.Framework.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000155 RID: 341
	public class LevelHierarchy : IModuleNexus, IDirtyable
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00050AC8 File Offset: 0x0004EEC8
		// (set) Token: 0x06000A03 RID: 2563 RVA: 0x00050ACF File Offset: 0x0004EECF
		public static LevelHierarchy instance { get; protected set; }

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000A04 RID: 2564 RVA: 0x00050AD8 File Offset: 0x0004EED8
		// (remove) Token: 0x06000A05 RID: 2565 RVA: 0x00050B0C File Offset: 0x0004EF0C
		public static event LevelHiearchyItemAdded itemAdded;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000A06 RID: 2566 RVA: 0x00050B40 File Offset: 0x0004EF40
		// (remove) Token: 0x06000A07 RID: 2567 RVA: 0x00050B74 File Offset: 0x0004EF74
		public static event LevelHierarchyItemRemoved itemRemoved;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000A08 RID: 2568 RVA: 0x00050BA8 File Offset: 0x0004EFA8
		// (remove) Token: 0x06000A09 RID: 2569 RVA: 0x00050BDC File Offset: 0x0004EFDC
		public static event LevelHierarchyLoaded loaded;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000A0A RID: 2570 RVA: 0x00050C10 File Offset: 0x0004F010
		// (remove) Token: 0x06000A0B RID: 2571 RVA: 0x00050C44 File Offset: 0x0004F044
		public static event LevelHierarchyReady ready;

		// Token: 0x06000A0C RID: 2572 RVA: 0x00050C78 File Offset: 0x0004F078
		public static uint generateUniqueInstanceID()
		{
			uint num = LevelHierarchy.availableInstanceID;
			LevelHierarchy.availableInstanceID = num + 1u;
			return num;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00050C94 File Offset: 0x0004F094
		public static void initItem(IDevkitHierarchyItem item)
		{
			if (item.instanceID == 0u)
			{
				item.instanceID = LevelHierarchy.generateUniqueInstanceID();
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00050CAC File Offset: 0x0004F0AC
		public static void addItem(IDevkitHierarchyItem item)
		{
			LevelHierarchy.instance.items.Add(item);
			LevelHierarchy.triggerItemAdded(item);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00050CC4 File Offset: 0x0004F0C4
		public static void removeItem(IDevkitHierarchyItem item)
		{
			LevelHierarchy.instance.items.Remove(item);
			LevelHierarchy.triggerItemRemoved(item);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00050CDD File Offset: 0x0004F0DD
		protected static void triggerItemAdded(IDevkitHierarchyItem item)
		{
			if (LevelHierarchy.itemAdded != null)
			{
				LevelHierarchy.itemAdded(item);
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00050CF4 File Offset: 0x0004F0F4
		protected static void triggerItemRemoved(IDevkitHierarchyItem item)
		{
			if (Level.isExiting)
			{
				return;
			}
			if (LevelHierarchy.itemRemoved != null)
			{
				LevelHierarchy.itemRemoved(item);
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00050D16 File Offset: 0x0004F116
		protected static void triggerLoaded()
		{
			if (LevelHierarchy.loaded != null)
			{
				LevelHierarchy.loaded();
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00050D2C File Offset: 0x0004F12C
		protected static void triggerReady()
		{
			if (LevelHierarchy.ready != null)
			{
				LevelHierarchy.ready();
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00050D42 File Offset: 0x0004F142
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x00050D4A File Offset: 0x0004F14A
		public List<IDevkitHierarchyItem> items { get; protected set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x00050D53 File Offset: 0x0004F153
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x00050D5B File Offset: 0x0004F15B
		public bool isDirty
		{
			get
			{
				return this._isDirty;
			}
			set
			{
				if (this.isDirty == value)
				{
					return;
				}
				this._isDirty = value;
				if (this.isDirty)
				{
					DirtyManager.markDirty(this);
				}
				else
				{
					DirtyManager.markClean(this);
				}
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00050D90 File Offset: 0x0004F190
		public void load()
		{
			string path = Level.info.path + "/Level.hierarchy";
			if (File.Exists(path))
			{
				using (StreamReader streamReader = new StreamReader(path))
				{
					IFormattedFileReader reader = new KeyValueTableReader(streamReader);
					this.read(reader);
				}
			}
			LevelHierarchy.triggerLoaded();
			TimeUtility.updated += this.handleLoadUpdated;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00050E0C File Offset: 0x0004F20C
		public void save()
		{
			string path = Level.info.path + "/Level.hierarchy";
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (StreamWriter streamWriter = new StreamWriter(path))
			{
				IFormattedFileWriter writer = new KeyValueTableWriter(streamWriter);
				this.write(writer);
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00050E80 File Offset: 0x0004F280
		public virtual void read(IFormattedFileReader reader)
		{
			if (reader.containsKey("Available_Instance_ID"))
			{
				LevelHierarchy.availableInstanceID = reader.readValue<uint>("Available_Instance_ID");
			}
			else
			{
				LevelHierarchy.availableInstanceID = 1u;
			}
			int num = reader.readArrayLength("Items");
			for (int i = 0; i < num; i++)
			{
				IFormattedFileReader formattedFileReader = reader.readObject(i);
				Type type = formattedFileReader.readValue<Type>("Type");
				if (type == null)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Level hierarchy item index ",
						i,
						" missing type: ",
						formattedFileReader.readValue("Type")
					}));
				}
				else
				{
					IDevkitHierarchyItem devkitHierarchyItem;
					if (typeof(MonoBehaviour).IsAssignableFrom(type))
					{
						devkitHierarchyItem = (new GameObject
						{
							name = type.Name
						}.AddComponent(type) as IDevkitHierarchyItem);
					}
					else
					{
						devkitHierarchyItem = (Activator.CreateInstance(type) as IDevkitHierarchyItem);
					}
					if (devkitHierarchyItem != null)
					{
						if (formattedFileReader.containsKey("Instance_ID"))
						{
							devkitHierarchyItem.instanceID = formattedFileReader.readValue<uint>("Instance_ID");
						}
						if (devkitHierarchyItem.instanceID == 0u)
						{
							devkitHierarchyItem.instanceID = LevelHierarchy.generateUniqueInstanceID();
						}
						formattedFileReader.readKey("Item");
						devkitHierarchyItem.read(formattedFileReader);
					}
				}
			}
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00050FCC File Offset: 0x0004F3CC
		public virtual void write(IFormattedFileWriter writer)
		{
			writer.writeValue<uint>("Available_Instance_ID", LevelHierarchy.availableInstanceID);
			writer.beginArray("Items");
			for (int i = 0; i < this.items.Count; i++)
			{
				writer.beginObject();
				IDevkitHierarchyItem devkitHierarchyItem = this.items[i];
				writer.writeValue<Type>("Type", devkitHierarchyItem.GetType());
				writer.writeValue<uint>("Instance_ID", devkitHierarchyItem.instanceID);
				writer.writeValue<IDevkitHierarchyItem>("Item", devkitHierarchyItem);
				writer.endObject();
			}
			writer.endArray();
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0005105D File Offset: 0x0004F45D
		public void initialize()
		{
			LevelHierarchy.instance = this;
			this.items = new List<IDevkitHierarchyItem>();
			Level.loadingSteps += this.handleLoadingStep;
			DevkitTransactionManager.transactionsChanged += this.handleTransactionsChanged;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00051092 File Offset: 0x0004F492
		public void shutdown()
		{
			Level.loadingSteps -= this.handleLoadingStep;
			DevkitTransactionManager.transactionsChanged -= this.handleTransactionsChanged;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x000510B6 File Offset: 0x0004F4B6
		protected void handleLoadingStep()
		{
			this.items.Clear();
			this.load();
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x000510C9 File Offset: 0x0004F4C9
		protected void handleLoadUpdated()
		{
			TimeUtility.updated -= this.handleLoadUpdated;
			LevelHierarchy.triggerReady();
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x000510E1 File Offset: 0x0004F4E1
		protected void handleTransactionsChanged()
		{
			if (!Level.isEditor)
			{
				return;
			}
			this.isDirty = true;
		}

		// Token: 0x0400074C RID: 1868
		private static uint availableInstanceID;

		// Token: 0x0400074E RID: 1870
		protected bool _isDirty;
	}
}
