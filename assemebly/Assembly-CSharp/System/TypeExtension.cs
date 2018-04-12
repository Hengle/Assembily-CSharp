using System;
using SDG.Framework.Debug;
using SDG.Framework.Translations;
using SDG.Unturned;

namespace System
{
	// Token: 0x02000196 RID: 406
	public static class TypeExtension
	{
		// Token: 0x06000BFA RID: 3066 RVA: 0x0005B854 File Offset: 0x00059C54
		public static TranslatedText getTranslatedNameText(this Type type)
		{
			object[] customAttributes = type.GetCustomAttributes(typeof(InspectableAttribute), false);
			if (customAttributes.Length > 0)
			{
				InspectableAttribute inspectableAttribute = customAttributes[0] as InspectableAttribute;
				return new TranslatedText(inspectableAttribute.name);
			}
			return new TranslatedTextFallback(type.Name);
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0005B89C File Offset: 0x00059C9C
		public static TypeReference<T> getReferenceTo<T>(this Type type)
		{
			return new TypeReference<T>(type.AssemblyQualifiedName);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0005B8A9 File Offset: 0x00059CA9
		public static object getDefaultValue(this Type type)
		{
			if (type.IsValueType)
			{
				return Activator.CreateInstance(type);
			}
			return null;
		}
	}
}
