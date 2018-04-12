using System;
using SDG.Framework.Translations;

namespace SDG.Framework.Utilities
{
	// Token: 0x02000311 RID: 785
	public static class TranslationUtility
	{
		// Token: 0x0600165A RID: 5722 RVA: 0x00084B48 File Offset: 0x00082F48
		public static bool tryParse(string value, out TranslationReference text)
		{
			string newNamespace;
			string newToken;
			bool result = TranslationUtility.tryParse(value, out newNamespace, out newToken);
			text = new TranslationReference(newNamespace, newToken);
			return result;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00084B6C File Offset: 0x00082F6C
		public static bool tryParse(string value, out string ns, out string token)
		{
			if (string.IsNullOrEmpty(value))
			{
				ns = null;
				token = null;
				return false;
			}
			string[] array = value.Split(TranslationUtility.DELIMITERS, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 2)
			{
				ns = array[0];
				token = array[1];
				return true;
			}
			ns = null;
			token = null;
			return false;
		}

		// Token: 0x04000C39 RID: 3129
		private static readonly string[] DELIMITERS = new string[]
		{
			"#",
			"::"
		};
	}
}
