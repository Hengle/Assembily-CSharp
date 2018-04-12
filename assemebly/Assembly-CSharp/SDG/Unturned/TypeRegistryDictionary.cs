using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x02000426 RID: 1062
	public class TypeRegistryDictionary
	{
		// Token: 0x06001CD3 RID: 7379 RVA: 0x0009C34F File Offset: 0x0009A74F
		public TypeRegistryDictionary(Type newBaseType)
		{
			this.baseType = newBaseType;
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x0009C36C File Offset: 0x0009A76C
		public Type getType(string key)
		{
			Type result = null;
			this.typesDictionary.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0009C38C File Offset: 0x0009A78C
		public void addType(string key, Type value)
		{
			if (this.baseType.IsAssignableFrom(value))
			{
				this.typesDictionary.Add(key, value);
				return;
			}
			throw new ArgumentException(this.baseType + " is not assignable from " + value, "value");
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0009C3D8 File Offset: 0x0009A7D8
		public void removeType(string key)
		{
			this.typesDictionary.Remove(key);
		}

		// Token: 0x04001104 RID: 4356
		private Type baseType;

		// Token: 0x04001105 RID: 4357
		private Dictionary<string, Type> typesDictionary = new Dictionary<string, Type>();
	}
}
