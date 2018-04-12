﻿using System;
using SDG.Framework.Devkit.Transactions;
using SDG.Framework.Translations;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000136 RID: 310
	public class DevkitTypeFactory
	{
		// Token: 0x06000986 RID: 2438 RVA: 0x0004FCB0 File Offset: 0x0004E0B0
		public static void instantiate(Type type, Vector3 position, Quaternion rotation, Vector3 scale)
		{
			if (!Level.isEditor)
			{
				return;
			}
			TranslationReference newReference = new TranslationReference("#SDG::Devkit.Transactions.Spawn");
			TranslatedText translatedText = new TranslatedText(newReference);
			translatedText.format(type.Name);
			DevkitTransactionManager.beginTransaction(translatedText);
			IDevkitHierarchySpawnable devkitHierarchySpawnable;
			if (typeof(MonoBehaviour).IsAssignableFrom(type))
			{
				GameObject gameObject = new GameObject();
				gameObject.name = type.Name;
				gameObject.transform.position = position;
				gameObject.transform.rotation = rotation;
				gameObject.transform.localScale = scale;
				DevkitTransactionUtility.recordInstantiation(gameObject);
				devkitHierarchySpawnable = (gameObject.AddComponent(type) as IDevkitHierarchySpawnable);
			}
			else
			{
				devkitHierarchySpawnable = (Activator.CreateInstance(type) as IDevkitHierarchySpawnable);
			}
			if (devkitHierarchySpawnable != null)
			{
				devkitHierarchySpawnable.devkitHierarchySpawn();
			}
			IDevkitHierarchyItem devkitHierarchyItem = devkitHierarchySpawnable as IDevkitHierarchyItem;
			if (devkitHierarchyItem != null)
			{
				LevelHierarchy.initItem(devkitHierarchyItem);
			}
			DevkitTransactionManager.endTransaction();
		}
	}
}
