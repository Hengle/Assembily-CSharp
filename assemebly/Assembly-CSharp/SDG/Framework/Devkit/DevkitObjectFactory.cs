using System;
using SDG.Framework.Devkit.Transactions;
using SDG.Framework.Translations;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000131 RID: 305
	public class DevkitObjectFactory
	{
		// Token: 0x06000966 RID: 2406 RVA: 0x0004F384 File Offset: 0x0004D784
		public static void instantiate(ObjectAsset asset, Vector3 position, Quaternion rotation, Vector3 scale)
		{
			if (asset == null)
			{
				return;
			}
			if (!Level.isEditor)
			{
				return;
			}
			TranslationReference newReference = new TranslationReference("#SDG::Devkit.Transactions.Spawn");
			TranslatedText translatedText = new TranslatedText(newReference);
			translatedText.format(asset.objectName);
			DevkitTransactionManager.beginTransaction(translatedText);
			DevkitHierarchyWorldObject devkitHierarchyWorldObject = LevelObjects.addDevkitObject(asset.GUID, position, rotation, scale, ELevelObjectPlacementOrigin.MANUAL);
			DevkitTransactionUtility.recordInstantiation(devkitHierarchyWorldObject.gameObject);
			DevkitTransactionManager.endTransaction();
		}
	}
}
