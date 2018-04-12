using System;
using SDG.Framework.Translations;
using UnityEngine;

namespace SDG.Framework.Devkit.Transactions
{
	// Token: 0x02000181 RID: 385
	public class DevkitTransactionUtility
	{
		// Token: 0x06000B9A RID: 2970 RVA: 0x0005AB04 File Offset: 0x00058F04
		public static void beginGenericTransaction()
		{
			TranslatedText translatedText = new TranslatedText(new TranslationReference("SDG", "Devkit.Transactions.Generic"));
			translatedText.format();
			DevkitTransactionManager.beginTransaction(translatedText);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0005AB33 File Offset: 0x00058F33
		public static void endGenericTransaction()
		{
			DevkitTransactionManager.endTransaction();
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0005AB3A File Offset: 0x00058F3A
		public static void recordInstantiation(GameObject go)
		{
			DevkitTransactionManager.recordTransaction(new DevkitGameObjectInstantiationTransaction(go));
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0005AB47 File Offset: 0x00058F47
		public static void recordObjectDelta(object instance)
		{
			DevkitTransactionManager.recordTransaction(new DevkitObjectDeltaTransaction(instance));
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0005AB54 File Offset: 0x00058F54
		public static void recordDestruction(GameObject go)
		{
			DevkitTransactionManager.recordTransaction(new DevkitGameObjectDestructionTransaction(go));
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0005AB61 File Offset: 0x00058F61
		public static void recordTransformChangeParent(Transform transform, Transform parent)
		{
			DevkitTransactionManager.recordTransaction(new DevkitTransformChangeParentTransaction(transform, parent));
		}
	}
}
